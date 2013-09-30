'21:32:22
'30.09.2013
'Autor: LargoWinch

Imports MySql.Data.MySqlClient

Friend Class DataBaseFactory
    Private Shared instance_Renamed As DataBaseFactory = Nothing
    Private Shared ReadOnly padlock As New Object()

    Public Shared ReadOnly Property Instance() As DataBaseFactory
        Get
            SyncLock padlock
                If instance_Renamed Is Nothing Then
                    instance_Renamed = New DataBaseFactory()
                End If
                Return instance_Renamed
            End SyncLock
        End Get
    End Property

    Private Shared _databaseBufferCount As Integer = 10
    Private Shared _databaseQueue As Queue(Of MySqlConnection)

    Public Sub New()
        _databaseQueue = New Queue(Of MySqlConnection)()
    End Sub

    Public Sub Initialize()
        Console.WriteLine("Инициация Базы Данных...")
        System.Threading.ThreadPool.QueueUserWorkItem(AddressOf ProcessDatabaseQueue)
    End Sub

    Private Shared SELECT_ACCOUNT_BY_USERNAME As String = "SELECT * FROM accounts WHERE account='{0}'"
    Private Shared SELECT_ACCOUNT_BY_USERNAME_PASSWORD As String = "SELECT * FROM accounts WHERE account='{0}' AND password='{1}'"

    Public Shared Function CheckAccountDetails(ByVal username As String, ByVal password As String) As Boolean
        Dim cmd As New MySqlCommand(String.Format(SELECT_ACCOUNT_BY_USERNAME_PASSWORD, username, password), GetDBConnection())
        Dim DataReader As MySqlDataReader = cmd.ExecuteReader()
        cmd.Dispose()
        If DataReader.HasRows Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function accountExists(ByVal user As String) As Boolean
        Dim cmd As New MySqlCommand(String.Format(SELECT_ACCOUNT_BY_USERNAME, user), GetDBConnection())
        Dim DataReader As MySqlDataReader = cmd.ExecuteReader()
        cmd.Dispose()
        If DataReader.HasRows Then
            Return True
        End If
        Return False
    End Function
    Public Shared Function GetDBConnection() As MySqlConnection
        Dim DataBase As MySqlConnection
        If _databaseQueue.Count > 0 Then
            DataBase = _databaseQueue.Dequeue()
            System.Threading.ThreadPool.QueueUserWorkItem(AddressOf ProcessDatabaseQueue)
        Else
            DataBase = New MySqlConnection("Server=" & Config.DATABASE_HOST & ";Database=" & Config.DATABASE_NAME & ";User=" & Config.DATABASE_USER & ";Password=" & Config.DATABASE_PASSWORD)
            DataBase.Open()
        End If
        Return DataBase
    End Function

    Private Shared _processing As Boolean = False

    Private Shared Sub ProcessDatabaseQueue(ByVal obj As Object)
        If _processing Then
            Return
        End If
        _processing = True
        Do While _databaseQueue.Count < _databaseBufferCount
            AddDBConnection("Server=" & Config.DATABASE_HOST & ";Database=" & Config.DATABASE_NAME & ";User=" & Config.DATABASE_USER & ";Password=" & Config.DATABASE_PASSWORD)
        Loop
        _processing = False
    End Sub
    Private Shared Sub AddDBConnection(ByVal ConnectionString As String)
        Dim DataBase As New MySqlConnection(ConnectionString)
        DataBase.Open()
        _databaseQueue.Enqueue(DataBase)
    End Sub
End Class
