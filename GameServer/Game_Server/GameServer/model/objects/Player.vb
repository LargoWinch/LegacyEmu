Public Class Player
    Public Gameclient As GameClient

    Public Sub sendPacket(ByVal pk As GameServerPacket)
        Gameclient.sendPacket(pk)
    End Sub

    Public Sub Termination()

        World.getInstance().unrealiseEntry(Me, True)
    End Sub


End Class
