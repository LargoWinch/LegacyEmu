'02:20:22
'02.10.2013
'Autor: LargoWinch

Imports L2Crypt
Imports System.Net.Sockets

Friend Class ClientManager
    Private Shared ab As New ClientManager()
    Public Shared Function getInstance() As ClientManager
        Return ab
    End Function

    Private ScrambleCount As Integer = 1
    Private ScrambledPairs() As ScrambledKeyPair
    Private BlowfishCount As Integer = 20
    Private BlowfishKeys()() As Byte

    Private _loggedClients As New List(Of L_LoginClient)()
    Private _waitingAcc As New SortedList(Of String, L_LoginClient)()

    Public Sub New()
        Logger.info("Загрузка клиент менеджера.")

        Logger.info("Кодированние пары ключей.")

        ScrambledPairs = New ScrambledKeyPair(ScrambleCount - 1) {}

        For i As Integer = 0 To ScrambleCount - 1
            ScrambledPairs(i) = New ScrambledKeyPair(ScrambledKeyPair.genKeyPair())
        Next i

        Logger.info("Кодированние ключей " & ScrambledPairs.Length & ".")
        Logger.info("Случайный blowfish ключ.")

        BlowfishKeys = New Byte(BlowfishCount - 1)() {}

        For i As Integer = 0 To BlowfishCount - 1
            BlowfishKeys(i) = New Byte(15) {}
            CType(New Random(), Random).NextBytes(BlowfishKeys(i))
        Next i

        Logger.info("Случайных " & BlowfishKeys.Length & " blowfish ключей.")
    End Sub

    Protected _banned As NetworkBlock
    Protected _flood As New SortedList(Of String, Date)()

    Public Sub addClient(ByVal client As TcpClient)
        If _banned Is Nothing Then
            _banned = NetworkBlock.getInstance()
        End If

        Dim ip As String = client.Client.RemoteEndPoint.ToString().Split(":"c)(0)
        Console.WriteLine("подключение " & ip)
        If _flood.ContainsKey(ip) Then
            If _flood(ip).CompareTo(Date.Now) = 1 Then
                Logger.warning("активный flooder " & ip)
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
            Logger.error("NetworkBlock: не удалось получить соединение. " & ip & " запрещен.")
            Return
        End If

        Dim lc As New L_LoginClient(client)
        If _loggedClients.Contains(lc) Then
            Return
        End If

        _loggedClients.Add(lc)
    End Sub

    Public Function getScrambledKeyPair() As ScrambledKeyPair
        Return ScrambledPairs(0)
    End Function

    Public Function getBlowfishKey() As Byte()
        Return BlowfishKeys(New Random().Next(BlowfishCount - 1))
    End Function

    Public Sub removeClient(ByVal loginClient As L_LoginClient)
        If Not _loggedClients.Contains(loginClient) Then
            Return
        End If


        _loggedClients.Remove(loginClient)
    End Sub
End Class
