'21:40:22
'30.09.2013
'Autor: LargoWinch

Imports MySql.Data.MySqlClient

Friend Class AccountManager
    Private Shared acm As New AccountManager()
    Public Shared Function getInstance() As AccountManager
        Return acm
    End Function

    Protected _accounts As New SortedList(Of String, L_L2Account)()

    Public Sub New()
        Dim connection As MySqlConnection = New MySql.Data.MySqlClient.MySqlConnection("Server=" & Config.DATABASE_HOST & ";Database=" & Config.DATABASE_NAME & ";User=" & Config.DATABASE_USER & ";Password=" & Config.DATABASE_PASSWORD)

        Dim cmd As MySqlCommand = connection.CreateCommand()

        connection.Open()

        cmd.CommandText = "SELECT * FROM accounts"
        cmd.CommandType = CommandType.Text

        Dim reader As MySqlDataReader = cmd.ExecuteReader()

        Do While reader.Read()
            Dim account As New L_L2Account()
            account.Name = reader.GetString("account")
            account.Password = reader.GetString("password")
            account.ServerId = CByte(reader.GetInt32("serverId"))
            account.Builder = reader.GetInt32("builder")
            account.Type = CType(System.Enum.Parse(GetType(AccountType), reader.GetString("type")), AccountType)
            account.TimeEnd = reader.GetString("timeEnd")
            account.LastLogin = reader.GetString("lastlogin")
            account.LastAddress = reader.GetString("lastAddress")
            account.Premium = reader.GetInt32("premium") = 1
            account.Points = reader.GetInt64("points")
            _accounts.Add(account.Name.ToLower(), account)
        Loop

        reader.Close()
        connection.Close()
        Logger.extra_info("ACM: загрузка аккаунтов " & _accounts.Count & " ")
    End Sub

    Public Function createAccount(ByVal user As String, ByVal password As String, ByVal address As String) As L_L2Account
        Dim sqb As New Sql_Block("accounts")
        sqb.param("account", user)
        sqb.param("password", password)
        sqb.param("lastlogin", Date.Now.ToLocalTime())
        sqb.param("lastAddress", address)
        sqb.sql_insert()

        Dim account As New L_L2Account()
        account.Name = user
        account.Password = password
        account.Address = address
        account.Builder = 0
        account.ServerId = 0
        account.Type = AccountType.Normal

        _accounts.Add(user, account)

        Logger.extra_info("ACM: создан аккаунт пользователя " & user & ".")
        Return account
    End Function

    Public Function [get](ByVal username As String) As L_L2Account
        If Not _accounts.ContainsKey(username.ToLower()) Then
            Return Nothing
        End If

        Return _accounts(username.ToLower())
    End Function

    Public Sub UpdatePremium(ByVal account As String, ByVal status As Byte, ByVal points As Long)
        Dim acc As L_L2Account = _accounts(account)
        acc.Premium = status = 1
        acc.Points = points

        Dim sqb As New Sql_Block("accounts")
        sqb.param("premium", status)
        sqb.param("points", points)
        sqb.where("account", account)
        sqb.sql_update()
    End Sub
End Class
