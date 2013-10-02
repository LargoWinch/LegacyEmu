'07:47:03 
'30.09.2013
'Autor: LargoWinch

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

    Public Sub New(ByVal tcpClient As TcpClient)
        ' Logger.extra_info("connection from " & tcpClient.Client.RemoteEndPoint )
        _client = tcpClient
        _stream = tcpClient.GetStream()
        _address = tcpClient.Client.RemoteEndPoint
        SessionId = New Random().Next(Integer.MaxValue)

        initializeNetwork()
    End Sub

    Public Sub initializeNetwork()
        RsaPair = ClientManager.getInstance().getScrambledKeyPair()
        BlowfishKey = ClientManager.getInstance().getBlowfishKey()
        _loginCrypt = New L_LoginCrypt()
        _loginCrypt.updateKey(BlowfishKey)

        CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
        CType(New System.Threading.Thread(AddressOf sendInit), System.Threading.Thread).Start()
    End Sub

    Public Sub sendInit()
        sendPacket(New SM_INIT(Me))
    End Sub

    Public Sub sendPacket(ByVal sbp As L_SendBasePacket)
        sbp.write()
        Dim data() As Byte = sbp.ToByteArray()
        data = _loginCrypt.encrypt(data, 0, data.Length)
        Dim array As New List(Of Byte)()
        array.AddRange(BitConverter.GetBytes(CShort(Fix(data.Length + 2))))
        array.AddRange(data)
        _stream.Write(array.ToArray(), 0, array.Count)
        _stream.Flush()
    End Sub

    Public Sub read()
        Try
            _buffer = New Byte(1) {}
            _stream.BeginRead(_buffer, 0, 2, New AsyncCallback(AddressOf OnReceiveCallbackStatic), Nothing)
        Catch ex As Exception
            close()
            Throw ex
        End Try
    End Sub

    Private Sub OnReceiveCallbackStatic(ByVal result As IAsyncResult)
        Dim rs As Integer = 0
        Try
            rs = _stream.EndRead(result)

            If rs > 0 Then
                Dim Length As Short = BitConverter.ToInt16(_buffer, 0)
                _buffer = New Byte(Length - 2 - 1) {}
                _stream.BeginRead(_buffer, 0, Length - 2, New AsyncCallback(AddressOf OnReceiveCallback), result.AsyncState)
            End If
        Catch s As Exception
            ' Logger.warning(" was closed by force." & _address)
            close()
        End Try
    End Sub

    Public Sub close()
        ClientManager.getInstance().removeClient(Me)
    End Sub

    Private Sub OnReceiveCallback(ByVal result As IAsyncResult)
        _stream.EndRead(result)

        Dim buff(_buffer.Length - 1) As Byte
        _buffer.CopyTo(buff, 0)

        If Not _loginCrypt.decrypt(buff, 0, buff.Length) Then
            Logger.error("blowfish failed on. please restart auth server.")
        Else
            handlePacket(buff)
            CType(New System.Threading.Thread(AddressOf read), System.Threading.Thread).Start()
        End If
    End Sub

    Private Sub handlePacket(ByVal buff() As Byte)
        Dim id As Byte = buff(0)

        'string str = "header: "+buff[0]+"\n";
        'foreach (byte b in buff)
        '    str += b.ToString("x2")+" ";

        'Console.WriteLine(str);
        'File.WriteAllText("header" + buff[0], str);

        Dim msg As L_ReceiveBasePacket = Nothing
        Select Case id
            Case &H0
                msg = New RequestAuthLogin(Me, buff)
            Case &H2
                msg = New RequestServerLogin(Me, buff)
            Case &H5
                msg = New RequestServerList(Me, buff)
            Case &H7
                msg = New AuthGameGuard(Me, buff)

            Case Else
                Logger.warning("LoginClient: received unk request " & id)
        End Select

        If msg IsNot Nothing Then
            CType(New Thread(New ThreadStart(AddressOf msg.run)), Thread).Start()
        End If
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

    Public activeAccount As L_L2Account
End Class