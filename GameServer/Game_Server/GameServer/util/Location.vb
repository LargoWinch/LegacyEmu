Public Class Location
    Public x As Integer
    Public y As Integer
    Public z As Integer
    Public h As Integer
    Public id As Integer = 0

    Public Sub New()
        x = 0
        y = 0
        z = 0
        h = 0
    End Sub

    'Позиция (x, y, z, heading, npcId)
    Public Sub New(ByVal locX As Integer, ByVal locY As Integer, ByVal locZ As Integer, ByVal heading As Integer, ByVal npcId As Integer)
        x = locX
        y = locY
        z = locZ
        h = heading
        id = npcId
    End Sub

    'Позиция (x, y, z, heading)

    Public Sub New(ByVal locX As Integer, ByVal locY As Integer, ByVal locZ As Integer, ByVal heading As Integer)
        x = locX
        y = locY
        z = locZ
        h = heading
    End Sub

    'Позиция (x, y, z)

    Public Sub New(ByVal locX As Integer, ByVal locY As Integer, ByVal locZ As Integer)

    End Sub

    Public Sub New(point As Integer())
        x = point(0)
        y = point(1)
        z = point(2)
        Try
            h = point(3)
        Catch e As Exception
            h = 0
        End Try
    End Sub

    'Парсит Location из строки, где коордтнаты разделены пробелами или запятыми

    Public Overloads Function equals(loc As Location) As [Boolean]
        Return loc.x = x AndAlso loc.y = y AndAlso loc.z = z
    End Function


End Class
