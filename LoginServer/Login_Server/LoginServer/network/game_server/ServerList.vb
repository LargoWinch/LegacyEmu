'03:36:51
'03.10.2013
'Autor: LargoWinch

Public Class ServerList
    Inherits L_SendBasePacket
    Private servers As List(Of L_L2Server)
    Public Sub New(ByVal Client As L_LoginClient)
        MyBase.makeme(Client)
        servers = ServerThreadPool.getInstance().servers
    End Sub
    Protected Friend Overrides Sub write()
        writeC(&H4)
        writeC(CByte(servers.Count))
        writeC(1) 'TODO lc.activeAccount.serverId

        For Each server As L_L2Server In servers
            writeC(server.id)
            writeB(server.GetIP(lc))
            writeD(server.Port)
            writeC(0)
            writeC(1)
            writeH(server.CurPlayers)
            writeH(server.MaxPlayers)

            writeC(server.connected)

            Dim bits As Integer = &H40
            If server.testMode Then
                bits = bits Or &H4
            End If

            writeD(bits)
            writeC(0)
        Next server
    End Sub
End Class
