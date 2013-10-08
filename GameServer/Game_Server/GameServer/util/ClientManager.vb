Imports System.Net.Sockets

Friend Class ClientManager
    Private Shared cm As New ClientManager()
    Public Shared Function getInstance() As ClientManager
        Return cm
    End Function

    Protected _flood As New SortedList(Of String, Date)()
    Protected _banned As NetworkBlock

    Public clients As New SortedList(Of String, GameClient)()

    Public Sub addClient(ByVal client As TcpClient)
        If _banned Is Nothing Then
            _banned = NetworkBlock.getInstance()
        End If

        Dim ip As String = client.Client.RemoteEndPoint.ToString().Split(":"c)(0)

        If _flood.ContainsKey(ip) Then
            If _flood(ip).CompareTo(Date.Now) = 1 Then
                Logger.warning("active flooder " & ip)
                client.Close()
                Return
            Else
                SyncLock _flood
                    _flood.Remove(ip)
                End SyncLock
            End If
        End If

        _flood.Add(ip, Date.Now.AddMilliseconds(3000))

        If Not _banned.allowed(ip) Then
            client.Close()
            Logger.error("NetworkBlock: connection attemp failed. " & ip & " banned.")
            Return
        End If

        Dim gc As New GameClient(client)

        clients.Add(gc._address.ToString(), gc)
        Logger.extra_info("NetController: " & clients.Count & " active connections")
    End Sub

    Public Sub terminate(ByVal sock As String)
        SyncLock clients
            clients.Remove(sock)
        End SyncLock

        Logger.extra_info("NetController: " & clients.Count & " active connections")
    End Sub
End Class