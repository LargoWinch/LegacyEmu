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
        'TODO
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
End Class
