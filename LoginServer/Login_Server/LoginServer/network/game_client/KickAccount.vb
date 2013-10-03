'19:11:36
'30.09.2013
'Autor: LargoWinch

Friend Class KickAccount
    Inherits L_SendServerPacket
    Private account As String
    Public Sub New(ByVal account As String)
        Me.account = account
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&HA8)
        writeS(account.ToLower())
    End Sub
End Class
