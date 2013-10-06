Public Class Player
    Public Gameclient As GameClient

    Public Sub sendPacket(ByVal pk As GameServerPacket)
        Gameclient.sendPacket(pk)
    End Sub

    


End Class
