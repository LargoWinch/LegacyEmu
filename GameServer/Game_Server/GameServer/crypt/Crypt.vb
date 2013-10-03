'20:15:22
'03.10.2013
'Autor: LargoWinch

Friend Class Crypt
    Private _key(15) As Byte '8];
    Private enabled As Boolean

    Public Sub New()
    End Sub

    Public Sub setKey(ByVal key() As Byte)
        _key(0) = key(0)
        _key(1) = key(1)
        _key(2) = key(2)
        _key(3) = key(3)
        _key(4) = key(4)
        _key(5) = key(5)
        _key(6) = key(6)
        _key(7) = key(7)
        _key(8) = key(8)
        _key(9) = key(9)
        _key(10) = key(10)
        _key(11) = key(11)
        _key(12) = key(12)
        _key(13) = key(13)
        _key(14) = key(14)
        _key(15) = key(15)

        enabled = True
    End Sub

    Public Sub decrypt(ByVal raw() As Byte)
        If Not enabled Then
            Return
        End If

        Dim temp As Integer = 0
        For i As Integer = 0 To raw.Length - 1
            Dim temp2 As Integer = raw(i) And &HFF
            raw(i) = CByte(temp2 Xor _key(i And 15) Xor temp)
            temp = temp2
        Next i

        Dim old As Long = _key(8) And &HFF '0
        old = old Or ((_key(9)) << 8 And &HFF00) '1
        old = old Or ((_key(10)) << &H10 And &HFF0000) '2
        old = old Or ((_key(11)) << &H18 And &HFF000000L) '3

        old += raw.Length

        _key(8) = CByte(old And &HFF) '0
        _key(9) = CByte(old >> &H8 And &HFF) '1
        _key(10) = CByte(old >> &H10 And &HFF) '2
        _key(11) = CByte(old >> &H18 And &HFF) '3
    End Sub

    Public Sub decrypt(ByVal raw() As Byte, ByVal offset As Integer, ByVal size As Integer)
        If Not enabled Then
            Return
        End If

        Dim temp As Integer = 0
        For i As Integer = 0 To size - 1
            Dim temp2 As Integer = raw(offset + i) And &HFF
            raw(offset + i) = CByte(temp2 Xor _key(i And 15) Xor temp)
            temp = temp2
        Next i

        Dim old As Long = _key(8) And &HFF
        old = old Or _key(9) << 8 And &HFF00
        old = old Or _key(10) << &H10 And &HFF0000
        old = old Or _key(11) << &H18 And &HFF000000L

        old += size

        _key(8) = CByte(old And &HFF)
        _key(9) = CByte(old >> &H8 And &HFF)
        _key(10) = CByte(old >> &H10 And &HFF)
        _key(11) = CByte(old >> &H18 And &HFF)
    End Sub

    Public Sub decrypt(ByVal raw() As Byte, ByVal size As Integer)
        If Not enabled Then
            Return
        End If

        Dim temp As UInteger = 0
        For i As UInteger = 0 To size - 1
            Dim temp2 As UInteger = raw(i) And CUInt(&HFF)
            raw(i) = CByte(temp2 Xor _key(i And 15) Xor temp)
            temp = temp2
        Next i

        Dim old As UInteger = (CUInt(_key(8))) And CUInt(&HFF) '0
        old = old Or CUInt((CUInt(_key(9))) << 8 And CUInt(&HFF00)) '1
        old = old Or CUInt((CUInt(_key(10))) << &H10 And CUInt(&HFF0000)) '2
        old = old Or CUInt((CUInt(_key(11))) << &H18 And CUInt(&HFF000000L)) '3

        old += CUInt(size)

        _key(8) = CByte(old And &HFF) '0
        _key(9) = CByte(old >> &H8 And &HFF) '1
        _key(10) = CByte(old >> &H10 And &HFF) '2
        _key(11) = CByte(old >> &H18 And &HFF) '3
    End Sub

    Public Sub encrypt(ByVal raw() As Byte)
        If Not enabled Then
            Return
        End If

        Dim temp As UInteger = 0
        For i As Integer = 0 To raw.Length - 1
            Dim temp2 As UInteger = raw(i) And CUInt(&HFF)
            temp = (temp2 Xor _key(i And 15) Xor temp)
            raw(i) = CByte(temp)
        Next i

        Dim old As UInteger = (CUInt(_key(8))) And CUInt(&HFF)
        old = old Or CUInt((CUInt(_key(9))) << 8 And CUInt(&HFF00))
        old = old Or CUInt((CUInt(_key(10)) << &H10) And CUInt(&HFF0000))
        old = old Or CUInt((CUInt(_key(11)) << &H18) And CUInt(&HFF000000L))

        old += CUInt(raw.Length)

        _key(8) = CByte(old And &HFF)
        _key(9) = CByte(old >> &H8 And &HFF)
        _key(10) = CByte(old >> &H10 And &HFF)
        _key(11) = CByte(old >> &H18 And &HFF)
    End Sub

    Public Sub encrypt(ByVal raw() As Byte, ByVal size As UInteger)
        If Not enabled Then
            Return
        End If

        Dim temp As UInteger = 0
        For i As UInteger = 0 To size - 1
            Dim temp2 As UInteger = raw(i) And CUInt(&HFF)
            temp = (temp2 Xor _key(i And 15) Xor temp)
            raw(i) = CByte(temp)
        Next i

        Dim old As UInteger = (CUInt(_key(8))) And CUInt(&HFF)
        old = old Or CUInt((CUInt(_key(9))) << 8 And CUInt(&HFF00))
        old = old Or CUInt((CUInt(_key(10)) << &H10) And CUInt(&HFF0000))
        old = old Or CUInt((CUInt(_key(11)) << &H18) And CUInt(&HFF000000L))

        old += CUInt(size)

        _key(8) = CByte(old And &HFF)
        _key(9) = CByte(old >> &H8 And &HFF)
        _key(10) = CByte(old >> &H10 And &HFF)
        _key(11) = CByte(old >> &H18 And &HFF)
    End Sub
End Class
