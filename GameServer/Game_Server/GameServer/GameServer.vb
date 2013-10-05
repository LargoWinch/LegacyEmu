Imports System.Net.Sockets

Friend Class GameServer
    Private Shared gs As GameServer
    Public Shared Function getInstance() As GameServer
        If gs Is Nothing Then
            gs = New GameServer()
        End If
        Return gs
    End Function

    Protected _listener As TcpListener

    Public Sub New()
        Console.Title = "C# Lineage II Freya"

        Logger.form()
        Config.init("all")
        'init data



        BlowFishKeygen.genKey()


        Sql.getInstance()

        '  _listener = 

        _listener.Start()

        Dim clientSocket As TcpClient = Nothing
        Do
            clientSocket = _listener.AcceptTcpClient()
        Loop
    End Sub
End Class
