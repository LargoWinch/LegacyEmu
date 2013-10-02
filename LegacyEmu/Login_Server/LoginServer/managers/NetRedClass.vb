'02:59:22
'02.10.2013
'Autor: LargoWinch

Friend Class NetRedClass
    Public mask, redirect As String
    Public serverId As Short
    Public redirectBits() As Byte

    Public Sub setRedirect(ByVal p As String)
        redirect = p
        redirectBits = New Byte(3) {}

        Dim w() As String = redirect.Split("."c)
        redirectBits(0) = Byte.Parse(w(0))
        redirectBits(1) = Byte.Parse(w(1))
        redirectBits(2) = Byte.Parse(w(2))
        redirectBits(3) = Byte.Parse(w(3))
    End Sub
End Class
