'04:23:49
'03.10.2013
'Autor: LargoWinch

Public Class PlayOk
    Inherits L_SendBasePacket
    Public Sub New(ByVal Client As L_LoginClient)
        MyBase.makeme(Client)
    End Sub
    Protected Friend Overrides Sub write()
        writeC(&H7)
        writeD(lc.play1)
        writeD(lc.play1)
        writeB(New Byte() {&HF, &H18, &H75, &H7B, &H7C, &HF, &HEC, &H7C, &H15, &H16, &H8B, &H76, &H18, &H43, &HE1})
    End Sub
End Class
