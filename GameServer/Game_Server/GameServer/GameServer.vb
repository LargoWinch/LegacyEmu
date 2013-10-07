Imports System.Net.Sockets

Friend Class GameServer
    Private Shared game As GameServer
    Public Shared Function getInstance() As GameServer
        If game Is Nothing Then
            game = New GameServer
        End If
        Return game
    End Function

    Protected listener As TcpListener

    Public Sub New()
        Console.Title = "C# Lineage II Freya"

        Logger.form()
        Config.init("all")


        AuthThread.getInstance()


    End Sub

    Private Sub accept(ByVal clientSocket As TcpClient)
        ClientManager.getInstance().addClient(clientSocket)
    End Sub
End Class
