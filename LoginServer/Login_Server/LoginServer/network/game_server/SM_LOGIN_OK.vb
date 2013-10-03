'01:35:21
'03.10.2013
'Autor: LargoWinch

Public Class SM_LOGIN_OK
    Inherits L_SendBasePacket
    Public Sub New(ByVal Client As L_LoginClient)
        MyBase.makeme(Client)
    End Sub
    Protected Friend Overrides Sub write()
        writeC(&H3)
        writeD(lc.login1)
        writeD(lc.login2)
        writeB(New Byte(7) {})
        writeB(New Byte() {&HDB, &H7, &H0, &H0, &H3C, &HE8, &HB, &H3})
        writeB(New Byte(23) {})
        writeB(New Byte() {&HF1, &H19, &H2D, &HFF, &H91, &HB4, &HEC, &HF8, &HDD, &HFF, &HEF, &H89, &H39, &H1D, &H63})
    End Sub
End Class
