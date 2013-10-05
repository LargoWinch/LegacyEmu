Public Class GArray(Of E)
    Implements Collections(Of E)
    <NonSerialized> _
    Protected Friend elementData() As E
    <NonSerialized> _
    Protected Friend modCount As Integer = 0
    Protected Friend size As Integer

    Public Sub New(ByVal initialCapacity As Integer)
        If initialCapacity < 0 Then
            Throw New ArgumentException("Незаконное Емкость:" & initialCapacity)
        End If
        elementData = CType(New Object(initialCapacity - 1) {}, E())
    End Sub

    Public Sub New()
        Me.New(10)
    End Sub

End Class
