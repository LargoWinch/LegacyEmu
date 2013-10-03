'21:53:10
'30.09.2013
'Autor: LargoWinch

Public Class L_L2Account

    Public Name As String
    Public Password As String
    Public Address As String
    Public ServerId As Byte
    Public Builder As Integer
    Public LastLogin As String
    Public LastAddress As String
    Public Id As Integer
    Public Type As AccountType

    Public TimeEnd As String
    Public Points As Long
    Public Premium As Boolean
End Class


Public Enum AccountType
    Trial
    Limited
    Normal
    Ultra
End Enum
