'21:22:30
'30.09.2013
'Autor: LargoWinch

Public Class L_L2Server
    Public code As String
    Public DefaultAddress() As Byte
    Public thread As ServerThread
    Public info As String
    Public id As Byte

    Public Function GetIP(ByVal client As L_LoginClient) As Byte()
        If DefaultAddress Is Nothing Then
            Dim ip As String = "0.0.0.0"
            If thread IsNot Nothing Then
                ip = thread.wan
            End If

            DefaultAddress = New Byte(3) {}
            Dim w() As String = ip.Split("."c)
            DefaultAddress(0) = Byte.Parse(w(0))
            DefaultAddress(1) = Byte.Parse(w(1))
            DefaultAddress(2) = Byte.Parse(w(2))
            DefaultAddress(3) = Byte.Parse(w(3))
        End If

        '  If thread IsNot Nothing Then
        ' End If

        Return DefaultAddress
    End Function

    Public ReadOnly Property connected() As Byte
        Get
            Return If(thread IsNot Nothing, (If(thread.connected, CByte(1), CByte(0))), CByte(0))
        End Get
    End Property

    Public ReadOnly Property CurPlayers() As Short
        Get
            Return If(thread IsNot Nothing, thread.curp, CShort(Fix(0)))
        End Get
    End Property

    Public ReadOnly Property MaxPlayers() As Short
        Get
            Return If(thread IsNot Nothing, thread.maxp, CShort(Fix(0)))
        End Get
    End Property

    Public ReadOnly Property Port() As Integer
        Get
            Return If(thread IsNot Nothing, thread.port, 0)
        End Get
    End Property

    Public ReadOnly Property testMode() As Boolean
        Get
            Return If(thread IsNot Nothing, thread.testMode, False)
        End Get
    End Property

    Public ReadOnly Property gmonly() As Boolean
        Get
            Return If(thread IsNot Nothing, thread.gmonly, False)
        End Get
    End Property
End Class
