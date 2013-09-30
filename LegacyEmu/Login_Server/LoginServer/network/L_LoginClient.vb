
Imports System.Net.Sockets
Imports System.Net
Imports L2Crypt
Imports System.Threading

Public Class L_LoginClient
    Public SessionId As Integer
    Public _address As EndPoint
    Public _client As TcpClient
    Public _stream As NetworkStream
    Private _buffer() As Byte
    Private _loginCrypt As L_LoginCrypt
    Public BlowfishKey() As Byte
    Public RsaPair As ScrambledKeyPair


    Public Sub read()
        Try
            _buffer = New Byte(1) {}
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public login1, login2 As Integer
    Public Sub setLoginPair(ByVal key1 As Integer, ByVal key2 As Integer)
        login1 = key1
        login2 = key2
    End Sub

    Public play1, play2 As Integer
    Public Sub setPlayPair(ByVal key1 As Integer, ByVal key2 As Integer)
        play1 = key1
        play2 = key2
    End Sub
End Class