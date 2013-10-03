'21:05:10
'03.10.2013
'Autor: LargoWinch

Friend Class KeyPacket
    Inherits GameServerNetworkPacket
    Private key() As Byte
    Private [next] As Byte
    Public Sub New(ByVal client As GameClient, ByVal n As Byte)
        key = client.enableCrypt()
        [next] = n
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&H2E)
        writeC([next])
        For i As Integer = 0 To 7
            writeC(key(i)) ' key
        Next i
        writeD(&H1)
        writeD(&H1) ' server id
        writeC(&H1)
        writeD(&H0) ' obfuscation key
    End Sub
End Class
