'19:10:36
'30.09.2013
'Autor: LargoWinch

Public Class AcceptPlayer
    Inherits L_SendServerPacket
    Private account As L_L2Account
    Private time As String
    Public Sub New(ByVal account As L_L2Account, ByVal time As String)
        Me.account = account
        Me.time = time
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&HA7)
        writeS(account.name.ToLower())
        writeS(account.type.ToString())
        writeS(account.timeend)
        writeC(If(account.premium, 1, 0))
        writeQ(account.points)
        writeS(time)
    End Sub
End Class
