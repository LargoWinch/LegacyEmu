Imports java.util

Namespace Game_Server.util
    Public Class Rnd
        Private Shared _rnd As New Random()

        Public Shared Function [get]() As Single 'получить случайное число от 0 до 1
            Return _rnd.nextFloat()
        End Function

        Public Shared Function [get](ByVal n As Integer) As Integer
            Return CInt(Fix(Math.Floor(_rnd.nextDouble() * n)))
        End Function

        Public Shared Function [get](ByVal min As Integer, ByVal max As Integer) As Integer 'получить случайное число от min до max
            Return min + CInt(Fix(Math.Floor(_rnd.nextDouble() * (max - min + 1))))
        End Function

        Public Shared Function [get](ByVal min As Long, ByVal max As Long) As Long 'получить случайное число от min до max (не макс-1 !)
            Return min + CLng(Fix(Math.Floor(_rnd.nextDouble() * (max - min + 1))))
        End Function

        Public Shared Function nextInt() As Integer
            Return _rnd.nextInt()
        End Function

        Public Shared Function nextDouble() As Double
            Return _rnd.nextDouble()
        End Function

        Public Shared Function nextGaussian() As Double
            Return _rnd.nextGaussian()
        End Function

        Public Shared Function nextBoolean() As Boolean
            Return _rnd.nextBoolean()
        End Function

        Public Shared Function chance_Renamed(ByVal chanceRenamed As Integer) As Boolean
            Return If(chanceRenamed < 1, False, If(chanceRenamed > 99, True, _rnd.next(99) + 1 <= chanceRenamed))
        End Function

        Public Shared Function chance_Renamed(ByVal chanceRenamed As Boolean) As Boolean
            Return _rnd.nextDouble() <= chance_Renamed / 100
        End Function

        Public Shared Function coordsRandomize(ByVal obj As L2Object, ByVal min As Integer, ByVal max As Integer) As Location
            Return obj.getLoc().rnd(min, max, False)
        End Function

        Public Shared Function coordsRandomize(ByVal obj As L2Object, ByVal radius As Integer) As Location
            Return coordsRandomize(obj, 0, radius)
        End Function

        Public Shared Function coordsRandomize(ByVal x As Integer, ByVal y As Integer, ByVal z As Integer, ByVal heading As Integer, ByVal radius_min As Integer, ByVal radius_max As Integer) As Location
            If radius_max = 0 OrElse radius_max < radius_min Then
                Return New Location(x, y, z, heading)
            End If
            Dim radius As Integer = [get](radius_min, radius_max)
            Dim angle As Double = _rnd.nextDouble() * 2 * Math.PI
            Return New Location(CInt(Fix(x + radius * Math.Cos(angle))), CInt(Fix(y + radius * Math.Sin(angle))), z, heading)
        End Function

        Public Shared Function coordsRandomize(ByVal pos As Location, ByVal radius_min As Integer, ByVal radius_max As Integer) As Location
            Return coordsRandomize(pos.x, pos.y, pos.z, pos.h, radius_min, radius_max)
        End Function

    End Class
End Namespace

