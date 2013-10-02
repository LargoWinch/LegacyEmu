'18:20:30
'01.10.2013
'Autor: LargoWinch


Imports MySql.Data.MySqlClient
Imports System.Net.Sockets
Imports System.Net

Public Class ServerThreadPool
    Private Shared gsc As New ServerThreadPool()
    Public Shared Function getInstance() As ServerThreadPool
        Return gsc
    End Function

    Public servers As New List(Of L_L2Server)()

    Public Sub New()
        Dim connection As MySqlConnection = Sql.getInstance().connection()
        Dim cmd As MySqlCommand = connection.CreateCommand()

        connection.Open()

        cmd.CommandText = "SELECT * FROM servers"
        cmd.CommandType = CommandType.Text

        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        Do While reader.Read()
            Dim server As New L_L2Server()
            server.id = CByte(reader.GetUInt32("id"))
            server.info = reader.GetString("info")
            server.code = reader.GetString("code")

            servers.Add(server)
        Loop

        reader.Close()
        connection.Close()

        Logger.info("GameServerThread: loaded " & servers.Count & " servers")

    End Sub

    Public Function [get](ByVal serverId As Short) As L_L2Server
        For Each s As L_L2Server In servers
            If s.id = serverId Then
                Return s
            End If
        Next s

        Return Nothing
    End Function

    Protected listener As TcpListener

    Public Sub start()
        listener = New TcpListener(IPAddress.Parse(Config.SERVER_HOST), Config.SERVER_PORT_GAMESERVER)
        listener.Start()
        Logger.extra_info("Auth server listening gameservers at " & Config.SERVER_HOST & ":" & Config.SERVER_PORT_GAMESERVER)
        Do
            verifyClient(listener.AcceptTcpClient())
        Loop
    End Sub

    Private Sub verifyClient(ByVal client As TcpClient)
        Dim st As New ServerThread()
        st.readData(client, Me)
    End Sub

    Public Sub shutdown(ByVal id As Byte)
        For Each s As L_L2Server In servers
            If s.id = id Then
                If s.thread IsNot Nothing Then
                    s.thread.stop()
                End If

                s.thread = Nothing
                Logger.warning("ServerThread: #" & id & " shutted down")
                Exit For
            End If
        Next s
    End Sub

    Public Function LoggedAlready(ByVal account As String) As Boolean
        For Each srv As L_L2Server In servers
            If srv.thread IsNot Nothing Then
                If srv.thread.LoggedAlready(account) Then
                    srv.thread.KickAccount(account)
                    Return True
                End If
            End If
        Next srv

        Return False
    End Function

    Public Sub SendPlayer(ByVal serverId As Byte, ByVal client As L_LoginClient, ByVal time As String)
        For Each srv As L_L2Server In servers
            If srv.id = serverId AndAlso srv.thread IsNot Nothing Then
                srv.thread.SendPlayer(client, time)
                Exit For
            End If
        Next srv

    End Sub
End Class
