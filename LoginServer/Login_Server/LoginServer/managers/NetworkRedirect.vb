'04:44:22
'02.10.2013
'Autor: LargoWinch

Imports System.IO

Friend Class NetworkRedirect
    Private Shared nb As New NetworkRedirect()
    Public Shared Function getInstance() As NetworkRedirect
        Return nb
    End Function

    Protected redirects As New List(Of NetRedClass)()
    Public GlobalRedirection As NetRedClass = Nothing

    Public Sub New()
        Dim reader As New StreamReader(New FileInfo("sq\server_redirect.txt").FullName)
        Do While Not reader.EndOfStream
            Dim line As String = reader.ReadLine()
            If line.Length = 0 OrElse line.StartsWith("//") Then
                Continue Do
            End If

            Dim i As New NetRedClass()
            Dim sp() As String = line.Split(" "c)
            i.serverId = Short.Parse(sp(0))
            i.mask = sp(1)
            i.setRedirect(sp(2))

            If i.serverId = -1 Then
                GlobalRedirection = i
            Else
                redirects.Add(i)
            End If
        Loop

        Logger.info("NetworkRedirect: " & redirects.Count & "перенаправление. Глобальный " & (If(GlobalRedirection Is Nothing, "выключенный", "включенный")))
    End Sub

    Public Function GetRedirect(ByVal client As L_LoginClient, ByVal serverId As Short) As Byte()
        If GlobalRedirection IsNot Nothing Then
            Dim a() As String = client._address.ToString().Split(":"c)(0).Split("."c), b() As String = GlobalRedirection.mask.Split("."c)
            Dim d(3) As Byte
            For c As Byte = 0 To 3
                d(c) = 0

                If b(c) = "*" Then
                    d(c) = 1
                ElseIf b(c) = a(c) Then
                    d(c) = 1
                ElseIf b(c).Contains("/") Then
                    Dim n As Byte = Byte.Parse(b(c).Split("/"c)(0)), x As Byte = Byte.Parse(b(c).Split("/"c)(1))
                    Dim t As Byte = Byte.Parse(a(c))
                    d(c) = If(t >= n AndAlso t <= x, CByte(1), CByte(0))
                End If
            Next c

            If d.Min() = 1 Then
                Logger.info("Глобальное перенаправление клиентов " & GlobalRedirection.redirect & " на #" & serverId)
                Return GlobalRedirection.redirectBits
            End If
        Else
            If redirects.Count = 0 Then
                Return Nothing
            End If

            For Each nr As NetRedClass In redirects
                If nr.serverId = serverId Then
                    Dim a() As String = client._address.ToString().Split(":"c)(0).Split("."c), b() As String = nr.mask.Split("."c)
                    Dim d(3) As Byte
                    For c As Byte = 0 To 3
                        d(c) = 0

                        If b(c) = "*" Then
                            d(c) = 1
                        ElseIf b(c) = a(c) Then
                            d(c) = 1
                        ElseIf b(c).Contains("/") Then
                            Dim n As Byte = Byte.Parse(b(c).Split("/"c)(0)), x As Byte = Byte.Parse(b(c).Split("/"c)(1))
                            Dim t As Byte = Byte.Parse(a(c))
                            d(c) = If(t >= n AndAlso t <= x, CByte(1), CByte(0))
                        End If
                    Next c

                    If d.Min() = 1 Then
                        Logger.info("Перенаправление клиента " & nr.redirect & " на #" & serverId)
                        Return nr.redirectBits
                    End If
                End If
            Next nr
        End If

        Return Nothing
    End Function
End Class
