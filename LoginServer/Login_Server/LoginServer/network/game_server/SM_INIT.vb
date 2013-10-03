'04:12:31
'03.10.2013
'Autor: LargoWinch

Public Class SM_INIT
    Inherits L_SendBasePacket
    Public Sub New(ByVal Client As L_LoginClient)
        MyBase.makeme(Client)
    End Sub
    Protected Friend Overrides Sub write()
        writeC(&H0)
        writeD(lc.SessionId)
        writeB(New Byte() {&H21, &HC6, &H0, &H0})
        writeB(lc.RsaPair._scrambledModulus)
        writeB(New Byte(15) {})
        writeB(lc.BlowfishKey)
        writeB(New Byte(6) {})
        writeB(New Byte() {&H5C, &HD8, &H4F, &HB3, &H6B, &H6F, &H82, &H83, &H5, &H24, &H8, &H13, &H3E, &H5B, &HB1, &H59})
    End Sub
End Class
