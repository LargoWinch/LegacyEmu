'22:00:02
'30.09.2013
'Autor: LargoWinch


Imports System.Text
Imports Org.BouncyCastle.Crypto
Imports Org.BouncyCastle.Crypto.Engines

Friend Class RequestAuthLogin
    Inherits L_ReceiveBasePacket
    Public Sub New(ByVal Client As L_LoginClient, ByVal data() As Byte)
        MyBase.makeme(Client, data)
    End Sub

    Protected _raw() As Byte

    Public Overrides Sub read()
        _raw = readB(128)
    End Sub
    Private Function PrepareString(ByVal Value As String) As String
        Dim newStr As String = ""
        For i As Short = 0 To Value.Length - 2
            If Char.IsLetterOrDigit(Value.Chars(i)) Then
                newStr &= Value.Chars(i)
            End If
        Next i
        Return newStr
    End Function
    Public Overrides Sub run()
        Dim username As String = ""
        Dim password As String = ""

        Dim key As CipherParameters = getClient().RsaPair._privateKey
        Dim rsa As New RSAEngine()
        rsa.init(False, key)

        Dim decrypt() As Byte = rsa.processBlock(_raw, 0, 128)

        If decrypt.Length < 128 Then
            Dim temp(127) As Byte
            Array.Copy(decrypt, 0, temp, 128 - decrypt.Length, decrypt.Length)
            decrypt = temp
        End If
        Dim ncotp As Integer = decrypt(&H7C)
        ncotp = ncotp Or decrypt(&H7D) << 8
        ncotp = ncotp Or decrypt(&H7E) << 16
        ncotp = ncotp Or decrypt(&H7F) << 24

        username = PrepareString(Encoding.ASCII.GetString(decrypt, &H5E, 14).ToLower())
        password = PrepareString(Encoding.ASCII.GetString(decrypt, &H6C, 16).ToLower())

        Dim am As AccountManager = AccountManager.getInstance()
        Dim account As L_L2Account = am.get(username)

       
                account = am.createAccount(username, L_LoginCrypt.MD5Encryption(password), getClient()._address.ToString())
                If Config.ShowAgreement Then
                   

            If Not account.Type.Equals("free") Then
                Dim time As Date
                Dim res As Integer = -3
                Select Case account.Type
                    Case AccountType.Trial
                        time = Date.Parse(account.TimeEnd)
                        res = time.CompareTo(Date.Now)
                    Case AccountType.Limited
                        time = Date.Parse(account.TimeEnd)
                        res = time.CompareTo(Date.Now)
                End Select

                If res = -1 Then
                   
                    Return
                End If
            End If

            Return
        End If

        Dim rnd As New Random()

      
    End Sub
End Class
