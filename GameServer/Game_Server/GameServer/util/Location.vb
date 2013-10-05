Imports Game_Server.Game_Server.util.Rnd

<Serializable> _

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
        Me.New(locX, locY, locZ, 0)
    End Sub

    Public Sub New(ByVal obj As L2Object)
        ' Me.New(obj.)
    End Sub

    Public Sub New(ByVal point() As Integer)
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

    Public Sub New(ByVal s As String)
        Dim xyzh() As String = s.Replace(",", " ").Replace(";", " ").Replace(" ", " ").Trim().Split(" ")
        If xyzh.Length < 3 Then
            Throw New ArgumentException("Cинтаксический анализ расположение от строки не разобрать: " & s)
        End If
        x = Convert.ToInt32(xyzh(0))
        y = Convert.ToInt32(xyzh(1))
        z = Convert.ToInt32(xyzh(2))
        h = If(xyzh.Length < 4, 0, Convert.ToInt32(xyzh(3)))
    End Sub

    Public Overloads Function Equals(ByVal loc As Location) As Boolean
        Return loc.x = x AndAlso loc.y = y AndAlso loc.z = z
    End Function

    Public Overloads Function Equals(ByVal _x As Integer, ByVal _y As Integer, ByVal _z As Integer) As Boolean
        Return _x = x AndAlso _y = y AndAlso _z = z
    End Function

    Public Overloads Function Equals(ByVal _x As Integer, ByVal _y As Integer, ByVal _z As Integer, ByVal _h As Integer) As Boolean
        Return _x = x AndAlso _y = y AndAlso _z = z AndAlso h = _h
    End Function

    Public Overridable Function changeZ(ByVal zDiff As Integer) As Location
        z += zDiff
        Return Me
    End Function

    Public Overridable Function correctGeoZ() As Location
        'TODO
        Return Me
    End Function

    Public Overridable Function correctGeoZ(ByVal refIndex As Integer) As Location
        'TODO
        Return Me
    End Function

    Public Overridable Function setX(ByVal _x As Integer) As Location
        x = _x
        Return Me
    End Function

    Public Overridable Function setY(ByVal _y As Integer) As Location
        y = _y
        Return Me
    End Function

    Public Overridable Function setZ(ByVal _z As Integer) As Location
        z = _z
        Return Me
    End Function

    Public Overridable Function setH(ByVal _h As Integer) As Location
        h = _h
        Return Me
    End Function

    Public Overridable Function setId(ByVal _id As Integer) As Location
        id = _id
        Return Me
    End Function

    Public Overridable Sub [set](ByVal _x As Integer, ByVal _y As Integer, ByVal _z As Integer)
        x = _x
        y = _y
        z = _z
    End Sub

    Public Overridable Sub [set](ByVal _x As Integer, ByVal _y As Integer, ByVal _z As Integer, ByVal _h As Integer)
        x = _x
        y = _y
        z = _z
        h = _h
    End Sub

    Public Overridable Sub [set](ByVal loc As Location)
        x = loc.x
        y = loc.y
        z = loc.z
        h = loc.h
    End Sub

    Public Sub New(ByVal obj As L2Object)
        Me.New(obj.getX(), obj.getY(), obj.getZ(), obj.getHeading())
    End Sub

    Public Overridable Function rnd(ByVal min As Integer, ByVal max As Integer, ByVal change As Boolean) As Location
        Dim loc As Location = Game_Server.util.Rnd.coordsRandomize(Me, min, max)
        If change Then
            x = loc.x
            y = loc.y
            z = loc.z
            Return Me
        End If
        Return loc
    End Function

    

    

    

    Public Overridable Function distance(ByVal loc As Location) As Double
        Return distance(loc.x, loc.y)
    End Function

    Public Overridable Function distance(ByVal _x As Integer, ByVal _y As Integer) As Double
        Dim dx As Long = x - _x
        Dim dy As Long = y - _y
        Return Math.Sqrt(dx * dx + dy * dy)
    End Function

    Public Overridable Function distance3D(ByVal loc As Location) As Double
        Return distance3D(loc.x, loc.y, loc.z)
    End Function

    Public Overridable Function distance3D(ByVal _x As Integer, ByVal _y As Integer, ByVal _z As Integer) As Double
        Dim dx As Long = x - _x
        Dim dy As Long = y - _y
        Dim dz As Long = z - _z
        Return Math.Sqrt(dx * dx + dy * dy + dz * dz)
    End Function

    
    Public NotOverridable Overrides Function ToString() As String
        Return x & "," & y & "," & z & "," & h
    End Function

    Public Overridable Function isNull() As Boolean
        Return x = 0 OrElse y = 0 OrElse z = 0
    End Function

    Public Function toXYZString() As String
        Return x & "," & y & "," & z
    End Function
End Class

