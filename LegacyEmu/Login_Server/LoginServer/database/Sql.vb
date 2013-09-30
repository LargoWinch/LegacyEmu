'22:25:49
'30.09.2013
'Autor: LargoWinch

Imports MySql.Data.MySqlClient

Friend Class Sql
    Private Shared sql As New Sql()
    Public Shared Function getInstance() As Sql
        Return sql
    End Function

    Protected connectionBuilder As MySqlConnectionStringBuilder

    Public Sub New()
        connectionBuilder = New MySqlConnectionStringBuilder()
        connectionBuilder.Add("DataBase", Config.DATABASE_NAME)
        connectionBuilder.Add("Server", Config.DATABASE_HOST)
        connectionBuilder.Add("User Id", Config.DATABASE_USER)
        connectionBuilder.Add("Password", Config.DATABASE_PASSWORD)

        Console.WriteLine("Соединение с базой данных установлено")
    End Sub

    Public Function connection() As MySqlConnection
        Return New MySqlConnection(connectionBuilder.ConnectionString)
    End Function
End Class

Public Class Sql_Block
    Private values As New List(Of String())()
    Private values2 As New List(Of String())()
    Private _table As String
    Public Sub New(ByVal table As String)
        _table = table
    End Sub

    Public Sub param(ByVal p As String, ByVal v As Object)
        values.Add(New String() {p, v.ToString()})
    End Sub

    Public Sub where(ByVal p As String, ByVal v As Object)
        values2.Add(New String() {p, v.ToString()})
    End Sub

    Public Sub sql_insert()
        Dim connection As MySqlConnection = Sql.getInstance().connection()
        Dim cmd As MySqlCommand = connection.CreateCommand()
        connection.Open()

        Dim pars As String = "", vals As String = ""
        Dim x As Short = 0
        For Each d As String() In values
            pars &= d(0)
            vals &= "'" & d(1) & "'"

            x += 1
            If x < values.Count Then
                pars &= ","
                vals &= ","
            End If
        Next d

        cmd.CommandText = "insert into " & _table & " (" & pars & ") values (" & vals & ")"
        cmd.CommandType = CommandType.Text
        cmd.ExecuteNonQuery()

        connection.Close()
    End Sub

    Public Sub sql_delete()
        Dim connection As MySqlConnection = Sql.getInstance().connection()
        Dim cmd As MySqlCommand = connection.CreateCommand()
        connection.Open()

        Dim str As String = ""
        Dim x As Short = 0
        For Each d As String() In values
            str &= d(0) & "='" & d(1) & "'"

            x += 1
            If x < values.Count Then
                str &= " and "
            End If
        Next d

        cmd.CommandText = "delete from " & _table & " where " & str & ""
        cmd.CommandType = CommandType.Text
        cmd.ExecuteNonQuery()

        connection.Close()
    End Sub

    Public Sub sql_update()
        Dim connection As MySqlConnection = Sql.getInstance().connection()
        Dim cmd As MySqlCommand = connection.CreateCommand()
        connection.Open()

        Dim str As String = "", where As String = ""
        Dim x As Short = 0
        For Each d As String() In values
            str &= d(0) & "='" & d(1) & "'"

            x += 1
            If x < values.Count Then
                str &= ","
            End If
        Next d

        x = 0
        For Each d As String() In values2
            where &= d(0) & "='" & d(1) & "'"

            x += 1
            If x < values2.Count Then
                str &= " and "
            End If
        Next d

        cmd.CommandText = "update " & _table & " set " & str & " where " & where
        cmd.CommandType = CommandType.Text
        cmd.ExecuteNonQuery()

        connection.Close()
    End Sub
End Class
