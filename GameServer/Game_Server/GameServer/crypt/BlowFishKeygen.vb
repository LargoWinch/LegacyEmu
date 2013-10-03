'20:20:22
'03.10.2013
'Autor: LargoWinch

Friend Class BlowFishKeygen
    Private Shared CRYPT_KEYS_SIZE As Integer = 20
    Private Shared CRYPT_KEYS(CRYPT_KEYS_SIZE - 1)() As Byte
    Private Shared rn As New Random()

    Public Shared Sub genKey()
        For i As Integer = 0 To CRYPT_KEYS_SIZE - 1
            CRYPT_KEYS(i) = New Byte(15) {}

            For j As Integer = 0 To CRYPT_KEYS(i).Length - 1
                CRYPT_KEYS(i)(j) = CByte(rn.Next(255))
            Next j

            CRYPT_KEYS(i)(8) = CByte(&HC8)
            CRYPT_KEYS(i)(9) = CByte(&H27)
            CRYPT_KEYS(i)(10) = CByte(&H93)
            CRYPT_KEYS(i)(11) = CByte(&H1)
            CRYPT_KEYS(i)(12) = CByte(&HA1)
            CRYPT_KEYS(i)(13) = CByte(&H6C)
            CRYPT_KEYS(i)(14) = CByte(&H31)
            CRYPT_KEYS(i)(15) = CByte(&H97)
        Next i
    End Sub

    Public Shared Function getRandomKey() As Byte()
        Return CRYPT_KEYS(rn.Next(CRYPT_KEYS_SIZE))
    End Function
End Class
