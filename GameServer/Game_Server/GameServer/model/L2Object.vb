Public MustInherit Class L2Object
    Public Shared POLY_NONE As Integer = 0
    Public Shared POLY_NPC As Integer = 1
    Public Shared POLY_ITEM As Integer = 2

    Protected _reflection As Long = Long.MinValue

    'Идентификатор объекта
    Protected _objectId As Integer
    Protected _storedId As Long

    'Месторасположение объекта : Используется для элементов/символов, которые видны в мире
    Private _x As Integer
    Private _y As Integer
    Private _z As Integer

    Private _poly_id As Integer

    Private _territories As Array(Of L2Territory) = Nothing


    Public Overridable Function getLoc() As Location
        Return New Location(_x, _y, _z, ())
    End Function

    Public Overridable Function getX() As Integer
        Return _x
    End Function

    Public Overridable Function getY() As Integer
        Return _y
    End Function

    Public Overridable Function getZ() As Integer
        Return _z
    End Function

    Public Overridable Function getHeading() As Integer
        Return 0
    End Function

End Class
