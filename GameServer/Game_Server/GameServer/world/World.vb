Public Class World
    Public Shared Function getInstance() As World
        Return world
    End Function

    Private Shared world As New World()

    Private _allPlayers As New SortedList(Of Integer, Player)()

    Public Sub KickAccount(ByVal account As String)
        For Each pl As Player In _allPlayers.Values
            If pl.Gameclient.AccountName = account Then
                pl.sendPacket(New LeaveWorld())
                ' pl.Termination()
                Exit For
            End If
        Next pl
    End Sub

    Public Sub unrealiseEntry(ByVal obj As GameObject, ByVal pkuse As Boolean)
    End Sub

    Public Function getPlayerCount() As Short
        Return CShort(Fix(_allPlayers.Count))
    End Function




End Class
