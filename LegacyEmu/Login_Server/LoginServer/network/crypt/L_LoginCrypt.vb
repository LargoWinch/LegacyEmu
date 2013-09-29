'22:09:05 
'29.09.2013
'Autor: LargoWinch

Imports L2Crypt

Public Class L_LoginCrypt
    Private key() As Byte = {CByte(&H6B), CByte(&H60), CByte(&HCB), CByte(&H5B), CByte(&H82), CByte(&HCE), CByte(&H90), CByte(&HB1), CByte(&HCC), CByte(&H2B), CByte(&H6C), CByte(&H55), CByte(&H6C), CByte(&H6C), CByte(&H6C), CByte(&H6C)}

    Private updatedKey As Boolean = False
    Private rnd As New Random()
    Private cipher As BlowfishCipher

    Public Sub New()
        cipher = New BlowfishCipher(key)
    End Sub

    Friend Sub updateKey(ByVal _blowfishKey() As Byte)
        key = _blowfishKey
    End Sub

    Public Function decrypt(ByRef data() As Byte, ByVal offset As Integer, ByVal size As Integer) As Boolean
        cipher.decipher(data, offset, size)

        Return veryfyChecksum(data, offset, size)
    End Function

    Public Function encrypt(ByVal data() As Byte, ByVal offset As Integer, ByVal size As Integer) As Byte()
        size += 4

        If Not updatedKey Then
            size += 4
            size += 8 - size Mod 8
            Array.Resize(data, size)
            encXORPass(data, offset, size, rnd.Next())
            cipher.cipher(data, offset, size)
            cipher.updateKey(key)
            updatedKey = True
        Else
            size += 8 - size Mod 8
            Array.Resize(data, size)
            appendChecksum(data, offset, size)
            cipher.cipher(data, offset, size)
        End If

        Return data
    End Function


    Private Function veryfyChecksum(ByVal data() As Byte, ByVal offset As Integer, ByVal size As Integer) As Boolean
        If (size And 3) <> 0 OrElse size <= 4 Then
            Return False
        End If

        Dim chksum As Long = 0
        Dim count As Integer = size - 4
        Dim check As Long = -1
        Dim i As Integer

        For i = offset To count - 1 Step 4
            check = data(i) And 255
            check = check Or data(i + 1) << 8 And 65280L
            check = check Or data(i + 2) << &H10 And 16711680L
            check = check Or data(i + 3) << &H18 And 4278190080L

            chksum = chksum Xor check
        Next i

        check = data(i) And 255
        check = check Or data(i + 1) << 8 And 65280L
        check = check Or data(i + 2) << &H10 And 16711680L
        check = check Or data(i + 3) << &H18 And 4278190080L

        Return chksum = 0
    End Function

    Public Shared Sub appendChecksum(ByVal raw() As Byte, ByVal offset As Integer, ByVal size As Integer)
        Dim chksum As Long = 0
        Dim count As Integer = size - 4
        Dim ecx As Long
        Dim i As Integer

        For i = offset To count - 1 Step 4
            ecx = raw(i) And &HFF
            ecx = ecx Or raw(i + 1) << &H8 And &HFF00L
            ecx = ecx Or raw(i + 2) << &H10 And &HFF0000L
            ecx = ecx Or raw(i + 3) << &H18 And &HFF000000L

            chksum = chksum Xor ecx
        Next i

        ecx = raw(i) And &HFF
        ecx = ecx Or raw(i + 1) << &H8 And &HFF00L
        ecx = ecx Or raw(i + 2) << &H10 And &HFF0000L
        ecx = ecx Or raw(i + 3) << &H18 And &HFF000000L

        raw(i) = CByte(chksum And &HFF)
        raw(i + 1) = CByte(chksum >> &H8 And &HFF)
        raw(i + 2) = CByte(chksum >> &H10 And &HFF)
        raw(i + 3) = CByte(chksum >> &H18 And &HFF)
    End Sub

    Public Shared Sub encXORPass(ByVal raw() As Byte, ByVal offset As Integer, ByVal size As Integer, ByVal key As Integer)
        Dim [stop] As Integer = size - 8
        Dim pos As Integer = 4 + offset
        Dim edx As Integer
        Dim ecx As Integer = key

        Do While pos < [stop]
            edx = (raw(pos) And &HFF)
            edx = edx Or (raw(pos + 1) And &HFF) << 8
            edx = edx Or (raw(pos + 2) And &HFF) << 16
            edx = edx Or (raw(pos + 3) And &HFF) << 24

            ecx += edx

            edx = edx Xor ecx

            raw(pos) = CByte(edx And &HFF)
            pos += 1
            raw(pos) = CByte(edx >> 8 And &HFF)
            pos += 1
            raw(pos) = CByte(edx >> 16 And &HFF)
            pos += 1
            raw(pos) = CByte(edx >> 24 And &HFF)
            pos += 1
        Loop

        raw(pos) = CByte(ecx And &HFF)
        pos += 1
        raw(pos) = CByte(ecx >> 8 And &HFF)
        pos += 1
        raw(pos) = CByte(ecx >> 16 And &HFF)
        pos += 1
        raw(pos) = CByte(ecx >> 24 And &HFF)
        pos += 1
    End Sub

    Public Shared Function MD5Encryption(ByVal Data As String) As String
        Dim x As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim data_Renamed() As Byte = System.Text.Encoding.ASCII.GetBytes(Data)
        data_Renamed = x.ComputeHash(data_Renamed)
        Dim ret As String = ""

        For i As Integer = 0 To data_Renamed.Length - 1 'XOR hash
            data_Renamed(i) = data_Renamed(i) Xor &H3
        Next i
        For i As Integer = 0 To data_Renamed.Length - 1
            ret &= data_Renamed(i).ToString("x2").ToLower()
        Next i
        Return ret
    End Function
End Class
