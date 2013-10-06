Imports MySql.Data.MySqlClient

Friend Class AuthLogin
    'Назначение: запрос авторизации на game сервере 
    'Формат: 
    '08 
    'XX XX XX XX 00 00 // строка содержащая login 
    'XX XX XX XX // session key #1(дается логин сервером) 
    'XX XX XX XX // session key #2(дается логин сервером) 
    Inherits GameServerRequest
    Public Sub New(ByVal client As GameClient, ByVal data() As Byte)
        MyBase.makeme(client, data)
    End Sub

    Private loginName As String
    Private playKey1 As Integer
    Private playKey2 As Integer
    Private loginKey1 As Integer
    Private loginKey2 As Integer

    Public Overrides Sub read()
        loginName = readS()
        playKey2 = readD()
        playKey1 = readD()
        loginKey2 = readD()
        loginKey1 = readD()
    End Sub

    Public Overrides Sub run()
        Dim tacc As LoginSrvTAccount = AuthThread.getInstance().getTA(loginName.ToLower())

        If tacc Is Nothing Then
            getClient().termination()
            Return
        End If

        getClient().AccountType = tacc.type
        getClient().AccountTimeEnd = tacc.timeEnd
        getClient().AccountTimeLogIn = tacc.timeLogIn
        getClient().AccountPremium = tacc.premium
        getClient().AccountPoints = tacc.points

        getClient().AccountName = loginName

        Dim connection As MySqlConnection '= Sql.getInstance().conn()
        Dim cmd As MySqlCommand = connection.CreateCommand()

        connection.Open()

        cmd.CommandText = "SELECT objId FROM user_data WHERE account = '" & loginName & "' ORDER BY slotId ASC"
        cmd.CommandType = CommandType.Text

        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        Dim users As New List(Of Integer)()
        Do While reader.Read()
            users.Add(reader.GetInt32("objId"))
        Loop

        reader.Close()
        connection.Close()

        Dim slot As Integer = 0
        For Each id As Integer In users
            'Dim p As L2Player = L2Player.restore(id, getClient())
            '   p._slotId = slot
            slot += 1
            '  Client._accountChars.Add(p)
        Next id

        'TODO доделать пакетку!!

    End Sub
End Class
