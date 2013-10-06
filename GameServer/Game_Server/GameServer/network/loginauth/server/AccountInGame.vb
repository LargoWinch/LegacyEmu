Friend Class AccountInGame
    Inherits GameServerPacket
    Private account As String
    Private status As Boolean
    Public Sub New(ByVal account As String, ByVal status As Boolean)
        Me.account = account
        Me.status = status
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&HA2)
        writeS(account.ToLower())
        writeC(If(status, CByte(1), CByte(0)))
    End Sub
End Class
