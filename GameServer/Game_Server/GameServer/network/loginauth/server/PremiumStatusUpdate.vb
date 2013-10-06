Friend Class PremiumStatusUpdate
    Inherits GameServerPacket
    Private account As String
    Private status As Byte
    Private points As Long
    Public Sub New(ByVal account As String, ByVal status As Byte, ByVal points As Long)
        Me.account = account
        Me.status = status
        Me.points = points
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&HA4)
        writeS(account.ToLower())
        writeC(status)
        writeQ(points)
    End Sub
End Class