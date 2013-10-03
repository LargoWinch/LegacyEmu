'20:50:10
'03.10.2013
'Autor: LargoWinch

Imports System.Net
Imports System.Net.Sockets

Public Class GameClient
    Public _address As EndPoint
    Public _client As TcpClient
    Public _stream As NetworkStream
    Private _buffer() As Byte
    Public _blowfishKey() As Byte


    Public Protocol As Integer
    Public IsTerminated As Boolean

    Public TrafficUp As Long = 0, TrafficDown As Long = 0

    Public Sub New(ByVal tcpClient As TcpClient)
        _client = tcpClient
        _stream = tcpClient.GetStream()
        _address = tcpClient.Client.RemoteEndPoint

        CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
    End Sub

    Public Function enableCrypt() As Byte()
        Dim key() As Byte = BlowFishKeygen.getRandomKey()
        Return key
    End Function

    Public Sub termination()
        Console.WriteLine("termination")
        IsTerminated = True
        _stream.Close()
        _client.Close()
    End Sub

    Public Sub read()
        If IsTerminated Then
            Return
        End If

        Try
            _buffer = New Byte(1) {}
            _stream.BeginRead(_buffer, 0, 2, New AsyncCallback(AddressOf OnReceiveCallbackStatic), Nothing)
        Catch
            termination()
        End Try
    End Sub

    Private Sub OnReceiveCallbackStatic(ByVal result As IAsyncResult)
        Dim rs As Integer = 0
        Try
            rs = _stream.EndRead(result)
            If rs > 0 Then
                Dim length As Short = BitConverter.ToInt16(_buffer, 0)
                _buffer = New Byte(length - 2 - 1) {}
                _stream.BeginRead(_buffer, 0, length - 2, New AsyncCallback(AddressOf OnReceiveCallback), result.AsyncState)
            End If
        Catch
            termination()
        End Try
    End Sub

    Private Sub OnReceiveCallback(ByVal result As IAsyncResult)
        If IsTerminated Then
            Return
        End If

        _stream.EndRead(result)

        Dim buff(_buffer.Length - 1) As Byte
        _buffer.CopyTo(buff, 0)
        TrafficUp += _buffer.Length

        CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
    End Sub

    Public AccountName As String
    Public _sessionId As Integer
    Public AccountType As String
    Public AccountTimeEnd As String
    Public AccountTimeLogIn As Date
    Public AccountPremium As Boolean
    Public AccountPoints As Long
End Class
