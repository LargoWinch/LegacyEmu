Public Class MTRandom
    Inherits Random

    Private Const serialVersionUID As Long = -515082678588212038L

    Private Const UPPER_MASK As Integer = &H8000000L
    Private Const LOWER_MASK As Integer = &H7FFFFFFF

    Private Const N As Integer = 624
    Private Const M As Integer = 397
    Private Shared ReadOnly MAGIC() As Integer = {&H0, &H9908B0DL}
    Private Const MAGIC_FACTOR1 As Integer = 1812433253
    Private Const MAGIC_FACTOR2 As Integer = 1664525
    Private Const MAGIC_FACTOR3 As Integer = 1566083941
    Private Const MAGIC_MASK1 As Integer = &H9D2C568L
    Private Const MAGIC_MASK2 As Integer = &HEFC6000L
    Private Const MAGIC_SEED As Integer = 19650218
    Private Const DEFAULT_SEED As Long = 5489L

    <NonSerialized> _
    Private mt() As Integer

End Class
