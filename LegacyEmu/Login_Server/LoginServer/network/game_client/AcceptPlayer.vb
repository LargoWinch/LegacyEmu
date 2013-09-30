'19:10:36
'30.09.2013
'Autor: LargoWinch

Public Class AcceptPlayer
    Inherits L_SendServerPacket
    Private time As String

    Protected Friend Overrides Sub write()
        writeC(&HA7)
        writeS(time)
    End Sub
End Class
