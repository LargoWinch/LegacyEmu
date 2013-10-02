'18:53:10
'01.10.2013
'Autor: LargoWinch

Imports System.Net.Sockets
Imports System.Threading

Public Class ServerThread
    Private networkstream As NetworkStream
    Private client As TcpClient
    Private buffer() As Byte

    Public wan As String
    Public port As Short
    Public curp As Short = 0, maxp As Short = 1000
    Public info As String
    Public connected As Boolean = False
    Public testMode As Boolean = False
    Public gmonly As Boolean = False
    Public id As Byte

    Public Sub readData(ByVal client As TcpClient, ByVal cn As ServerThreadPool)
        Me.networkstream = client.GetStream()
        Me.client = client
        CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
    End Sub

    Private Sub termination()
        ServerThreadPool.getInstance().shutdown(id)
    End Sub
    Public Sub read()
        Try
            buffer = New Byte(1) {}
            networkstream.BeginRead(buffer, 0, 2, New AsyncCallback(AddressOf OnReceiveCallbackStatic), Nothing)
        Catch e As Exception
            Logger.error("ServerThread: " & e.Message)
            termination()
        End Try
    End Sub

    Private Sub OnReceiveCallbackStatic(ByVal result As IAsyncResult)
        Dim rs As Integer = 0
        Try
            rs = networkstream.EndRead(result)
            If rs > 0 Then
                Dim length As Short = BitConverter.ToInt16(buffer, 0)
                buffer = New Byte(length - 1) {}
                networkstream.BeginRead(buffer, 0, length, New AsyncCallback(AddressOf OnReceiveCallback), result.AsyncState)
            End If
        Catch e As Exception
            Logger.error("ServerThread: " & e.Message)
            termination()
        End Try
    End Sub

    Private Sub OnReceiveCallback(ByVal result As IAsyncResult)
        networkstream.EndRead(result)

        Dim buff(buffer.Length - 1) As Byte
        buffer.CopyTo(buff, 0)
        handlePacket(buff)
        CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
    End Sub

    Private Sub handlePacket(ByVal buff() As Byte)
        Dim id As Byte = buff(0)

        Dim str As String = "header: " & buff(0) & vbLf
        For Each b As Byte In buff
            str &= b.ToString("x2") & " "
        Next b

        Console.WriteLine(str)

        Dim msg As L_ReceiveServerPacket = Nothing
        Select Case id
            Case &HA0
                msg = New RequestLoginServPing(Me, buff)
            Case &HA1
                msg = New RequestLoginAuth(Me, buff)
            Case &HA2
                msg = New RequestPlayerInGame(Me, buff)
            Case &HA3
                msg = New RequestPlayersOnline(Me, buff)
            Case &HA4
                msg = New RequestUpdatePremiumState(Me, buff)
        End Select

        If msg Is Nothing Then
            Return
        End If

        CType(New Thread(New ThreadStart(AddressOf msg.run)), Thread).Start()
    End Sub

    Public Sub sendPacket(ByVal pk As L_SendServerPacket)
        pk.write()
        Dim blist As New List(Of Byte)()
        Dim db() As Byte = pk.ToByteArray()
        Dim len As Short = CShort(Fix(db.Length))
        blist.AddRange(BitConverter.GetBytes(len))
        blist.AddRange(db)
        networkstream.Write(blist.ToArray(), 0, blist.Count)
        networkstream.Flush()
    End Sub

    Public Sub close(ByVal pk As L_SendServerPacket)
        sendPacket(pk)
        ServerThreadPool.getInstance().shutdown(id)
    End Sub

    Public Sub [stop]()
        Try
            networkstream.Close()
            client.Close()
        Catch
        End Try

        activeInGame.Clear()
    End Sub

    Private activeInGame As New List(Of String)()
    Public Sub AccountInGame(ByVal account As String, ByVal status As Byte)
        If status = 1 Then
            If Not activeInGame.Contains(account) Then
                activeInGame.Add(account)
            End If
        Else
            If activeInGame.Contains(account) Then
                activeInGame.Remove(account)
            End If
        End If
    End Sub

    Public Function LoggedAlready(ByVal account As String) As Boolean
        Return activeInGame.Contains(account)
    End Function

    Public Sub SendPlayer(ByVal client As L_LoginClient, ByVal time As String)
        sendPacket(New AcceptPlayer(client.activeAccount, time))
    End Sub

    Public Sub KickAccount(ByVal account As String)
        activeInGame.Remove(account)
        sendPacket(New KickAccount(account))
    End Sub

End Class
