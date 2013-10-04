'00:25:40
'01.10.2013
'Autor: LargoWinch

Imports System.Net.Sockets
Imports System.Net

Module Module1
    Sub Main(ByVal args() As String)
        StartProgram.getInstance()
        Process.GetCurrentProcess().WaitForExit()

    End Sub
End Module
Friend Class StartProgram

    Private Shared auth As New StartProgram()
    Public Shared Function getInstance() As StartProgram
        Return auth
    End Function

    Protected LoginListener As TcpListener

    Public Sub New()
        Console.Title = "Login server Lineage II"

        Console.WriteLine("#======================================================================#")
        Console.WriteLine("#                                                                      #")
        Console.WriteLine("#$$      $$$$$    $$$$    $$$$    $$$$   $$  $$  $$$$$   $$   $$ $$  $$#")
        Console.WriteLine("#$$      $$      $$      $$  $$  $$  $$   $$$$   $$      $$$ $$$ $$  $$#")
        Console.WriteLine("#$$      $$$$    $$ $$$  $$$$$$  $$        $$    $$$$    $$ $ $$ $$  $$#")
        Console.WriteLine("#$$      $$      $$  $$  $$  $$  $$  $$    $$    $$      $$   $$ $$  $$#")
        Console.WriteLine("#$$$$$$  $$$$$    $$$$   $$  $$   $$$$     $$    $$$$$   $$   $$  $$$$ #")
        Console.WriteLine("#----------------------------------------------------------------------#")
        Console.WriteLine("#----------------------------------------------------------------------#")
        Console.WriteLine("#                         VB.NET L2 Emulator                           #")
        Console.WriteLine("#----------------------------------------------------------------------#")
        Console.WriteLine("#              Copyright (c) 2013 Legacy Dev Team                      #")
        Console.WriteLine("#======================================================================#")

        Config.load()
        ClientManager.getInstance()
        DataBaseFactory.Instance.Initialize()
        Sql.getInstance()
        AccountManager.getInstance()
        ServerThreadPool.getInstance()
        NetworkRedirect.getInstance()

        LoginListener = New TcpListener(IPAddress.Parse(Config.SERVER_HOST), Config.SERVER_PORT)
        LoginListener.Start()
        Logger.extra_info("Сервер аутентификации клиента " & Config.SERVER_HOST & ":" & Config.SERVER_PORT)
        CType(New System.Threading.Thread(AddressOf ServerThreadPool.getInstance().start), System.Threading.Thread).Start()
        Dim clientSocket As TcpClient = Nothing
        Do
            clientSocket = LoginListener.AcceptTcpClient()
            accept(clientSocket)
        Loop

    End Sub
    Private Sub accept(ByVal client As TcpClient)
        ClientManager.getInstance().addClient(client)
    End Sub
End Class

