'21:53:55
'30.09.2013
'Autor: LargoWinch

Public Class SM_AUTH_GG
    Inherits L_SendBasePacket
    Public Sub New(ByVal Client As L_LoginClient)
        MyBase.makeme(Client)
    End Sub
    Protected Friend Overrides Sub write()
        writeC(&HB)
        writeD(lc.SessionId)
        writeB(New Byte(15) {})
        writeB(New Byte() {&HE0, &HCD, &H13, &H4B, &H1D, &HD1, &H35, &H75, &HCB, &H83, &H44, &H35, &H24, &HEF, &H60, &H82, &HA6, &H20, &H82})
    End Sub
End Class
