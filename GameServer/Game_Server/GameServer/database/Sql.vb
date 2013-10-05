Imports MySql.Data.MySqlClient

Public Class Sql
    Private Shared sql As New Sql()
    Public Shared Function getInstance() As Sql
        Return sql
    End Function

    Protected connBuilder As MySqlConnectionStringBuilder

    Public Sub New()
        connBuilder = New MySqlConnectionStringBuilder()
        connBuilder.Database = Config.DATABASE_NAME
        connBuilder.Server = Config.DATABASE_HOST
        connBuilder.UserID = Config.DATABASE_USER
        connBuilder.Password = Config.DATABASE_PASSWORD
        connBuilder.Port = 3306

        Logger.info("MySQL:  established")
    End Sub

    Public Function conn() As MySqlConnection
        Return New MySqlConnection(connBuilder.ConnectionString)
    End Function
End Class

Public Class SQL_Block
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

    Private connection As MySqlConnection
    Private cmd As MySqlCommand
    Public Sub [on]()
        connection = Sql.getInstance().conn()
        cmd = connection.CreateCommand()
        connection.Open()
    End Sub

    Public Sub [off]()
        connection.Close()
    End Sub

    Public Sub sql_insert(ByVal ex As Boolean)
        If Not ex Then
            connection = Sql.getInstance().conn()
            cmd = connection.CreateCommand()
            connection.Open()
        End If

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

        If Not ex Then
            connection.Close()
        Else
            values.Clear()
            values2.Clear()
        End If
    End Sub

    Public Sub sql_delete(ByVal ex As Boolean)
        If Not ex Then
            connection = Sql.getInstance().conn()
            cmd = connection.CreateCommand()
            connection.Open()
        End If

        Dim where As String = ""
        Dim x As Short = 0
        For Each d As String() In values2
            where &= d(0) & "='" & d(1) & "'"

            x += 1
            If x < values2.Count Then
                where &= " and "
            End If
        Next d

        cmd.CommandText = "delete from " & _table & " where " & where & ""
        cmd.CommandType = CommandType.Text
        cmd.ExecuteNonQuery()

        If Not ex Then
            connection.Close()
        Else
            values.Clear()
            values2.Clear()
        End If
    End Sub

    Public Sub sql_update(ByVal ex As Boolean)
        If Not ex Then
            connection = Sql.getInstance().conn()
            cmd = connection.CreateCommand()
            connection.Open()
        End If

        Dim str As String = "", where As String = ""
        Dim x As Byte = 0
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
                where &= " and "
            End If
        Next d

        cmd.CommandText = "update " & _table & " set " & str & " where " & where
        cmd.CommandType = CommandType.Text
        cmd.ExecuteNonQuery()

        If Not ex Then
            connection.Close()
        Else
            values.Clear()
            values2.Clear()
        End If
    End Sub
End Class