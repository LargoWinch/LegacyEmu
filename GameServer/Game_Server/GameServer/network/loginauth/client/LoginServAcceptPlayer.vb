Friend Class LoginServAcceptPlayer
    Inherits ReceiveAuthPacket

    Private account As String
    Private type As String
    Private timeEnd As String
    Private premium As Boolean
    Private points As Long
    Private timeLogIn As String

    Public Sub New(ByVal login As AuthThread, ByVal db() As Byte)
        MyBase.makeme(login, db)
    End Sub

    Public Overrides Sub read()
        account = readS()
        Type = readS()
        timeEnd = readS()
        premium = readC() = 1
        points = readQ()
        timeLogIn = readS()
    End Sub


    Public Overrides Sub run()
        Dim ta As New LoginSrvTAccount()
        ta.name = account
        ta.type = type
        ta.timeEnd = timeEnd
        ta.premium = premium

        ta.points = points
        ta.timeLogIn = Date.Parse(timeLogIn)

        AuthThread.getInstance().awaitAccount(ta)
    End Sub
End Class

Public Class LoginSrvTAccount
    Public name As String
    Public type As String
    Public timeEnd As String
    Public premium As Boolean
    Public points As Long
    Public timeLogIn As Date


End Class
