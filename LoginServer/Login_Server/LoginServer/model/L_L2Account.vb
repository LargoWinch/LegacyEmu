'21:53:10
'30.09.2013
'Autor: LargoWinch

Public Class L_L2Account

    Public name As String
    Public password As String
    Public address As String
    Public serverId As Byte
    Public builder As Integer
    Public lastlogin As String
    Public lastaddress As String
    Public id As Integer
    Public type As AccountType

    Public TimeEnd As String
    Public Points As Long
    Public Premium As Boolean
End Class


Public Enum AccountType
    trial
    limited
    normal
    ultra
End Enum
