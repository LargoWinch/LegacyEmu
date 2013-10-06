Imports System.Net.Sockets

Public Class AuthThread
    Private Shared at As New AuthThread
    Public Shared Function getInstance() As AuthThread
        Return at
    End Function

    Protected lclient As TcpClient
    Protected nstream As NetworkStream
    Protected ltimer As System.Timers.Timer
    Public IsConnected As Boolean = False
    Private buffer() As Byte

    Public version As String = "#216"
    Public build As Integer = 0

    Public Sub New()
        connect()
    End Sub

    Public Sub connect()
        IsConnected = False
        Try
            lclient = New TcpClient(Config.AUTH_HOST, Config.AUTH_PORT)
            nstream = lclient.GetStream()
        Catch e1 As SocketException
            Logger.warning("login server is not responding. Retrying")
            If ltimer Is Nothing Then
                ltimer = New System.Timers.Timer()
                ltimer.Interval = 2000
                AddHandler ltimer.Elapsed, AddressOf ltimer_Elapsed
            End If

            If Not ltimer.Enabled Then
                ltimer.Enabled = True
            End If

            Return
        End Try

        If ltimer IsNot Nothing AndAlso ltimer.Enabled Then
            ltimer.Enabled = False
        End If

        IsConnected = True


        sendPacket(New LoginAuth())
        sendPacket(New LoginServPing(Me))
        CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
    End Sub

    Private Sub ltimer_Elapsed(ByVal sender As Object, ByVal e As System.Timers.ElapsedEventArgs)
        connect()
    End Sub

    Public Sub read()
        Try
            buffer = New Byte(1) {}
            nstream.BeginRead(buffer, 0, 2, New AsyncCallback(AddressOf OnReceiveCallbackStatic), Nothing)
        Catch e As Exception
            Logger.error("AuthThread: " & e.Message)
            termination()
        End Try
    End Sub

    Private Sub OnReceiveCallbackStatic(ByVal result As IAsyncResult)
        Dim rs As Integer = 0
        Try
            rs = nstream.EndRead(result)
            If rs > 0 Then
                Dim length As Short = BitConverter.ToInt16(buffer, 0)
                buffer = New Byte(length - 1) {}
                nstream.BeginRead(buffer, 0, length, New AsyncCallback(AddressOf OnReceiveCallback), result.AsyncState)
            End If
        Catch e As Exception
            Logger.error("AuthThread: " & e.Message)
            termination()
        End Try
    End Sub

    Private Sub OnReceiveCallback(ByVal result As IAsyncResult)
        nstream.EndRead(result)

        Dim buff(buffer.Length - 1) As Byte
        buffer.CopyTo(buff, 0)

        PacketHandlerAuth.handlePacket(Me, buff)

        CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
    End Sub

    Private Sub termination()
        If paused Then
            Return
        End If

        Logger.error("AuthThread: переподключение...")
        connect()
    End Sub

    Public Sub UpdatePlayersOnline()
        Dim cnt As Short = World.getInstance().getPlayerCount()
        sendPacket(New PlayerCount(cnt))
    End Sub






    Private awaitingAccounts As New SortedList(Of String, LoginSrvTAccount)()

    Public Sub awaitAccount(ByVal ta As LoginSrvTAccount)
        If awaitingAccounts.ContainsKey(ta.name) Then
            awaitingAccounts.Remove(ta.name)
        End If

        awaitingAccounts.Add(ta.name, ta)
    End Sub

    Public Function getTA(ByVal p As String) As LoginSrvTAccount
        If awaitingAccounts.ContainsKey(p) Then
            Dim ta As LoginSrvTAccount = awaitingAccounts(p)
            awaitingAccounts.Remove(p)
            Return ta
        Else
            Return Nothing
        End If
    End Function

    Public Sub UpdatePremiumState(ByVal client As GameClient)
        sendPacket(New PremiumStatusUpdate(client.AccountName.ToLower(), If(client.AccountPremium, CByte(1), CByte(0)), client.AccountPoints))
    End Sub


    Public Sub loginFail(ByVal code As String)
        paused = True
        Logger.error("AuthThread: " & code & ". Пожалуйста, проверьте конфигурацию сервера. Сервер приостановлен.")
        Try
            nstream.Close()
            lclient.Close()
        Catch
        End Try
    End Sub

    Private paused As Boolean = False

    Public Sub loginOk(ByVal code As String)
        Logger.extra_info("AuthThread: " & code)
    End Sub

    Public Sub setInGameAccount(ByVal account As String, Optional ByVal status As Boolean = False)
        sendPacket(New AccountInGame(account, status))
    End Sub

    Public Sub sendPacket(ByVal pk As GameServerPacket)
        pk.write()

        Dim blist As New List(Of Byte)()
        Dim db() As Byte = pk.ToByteArray()

        Dim len As Short = CShort(Fix(db.Length))
        blist.AddRange(BitConverter.GetBytes(len))
        blist.AddRange(db)

        nstream.Write(blist.ToArray(), 0, blist.Count)
        nstream.Flush()
    End Sub


End Class
