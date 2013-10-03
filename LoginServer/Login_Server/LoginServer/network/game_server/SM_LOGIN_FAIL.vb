'01:03:41
'03.10.2013
'Autor: LargoWinch

Public Class SM_LOGIN_FAIL
    Inherits L_SendBasePacket
    Public Enum LoginFailReason
        [NOTHING] = 0
        SYSTEM_ERROR_TRY_AGAIN = 1
        PASS_WRONG = 2
        USER_OR_PASS_WRONG = 3
        ACCESS_FAILED_TRY_AGAIN = 4
        INCORRECT_ACCOUNT_INFO = 5
        ACCOUNT_IN_USE = 7
        TOO_YOUNG = 12
        SERVER_OVERLOADED = 15
        SERVER_MAINTENANCE = 16
        CHANGE_TEMP_PASS = 17
        TEMP_PASS_EXPIRED = 18
        NO_TIME_LEFT = 19
        SYSTEM_ERROR = 20
        ACCESS_FAILED = 21
        RESTRICTED_IP = 22
        SECURITY_CARD = 31
        NOT_VERIFY_AGE = 32
        NO_ACCESS_COUPON = 33
        DUAL_BOX = 35
        INACTIVE_REACTIVATE = 36
        ACCEPT_USER_AGREEMENT = 37
        GUARDIAN_CONSENT = 38
        DECLINED_AGREEMENT_OR_WIDTHDRAWL = 39
        ACCOUNT_SUSPENDED = 40
        CHANGE_PASS_QUIZ = 41
        ACCESSED_10_ACCOUNTS = 42
    End Enum

    Private _reason As LoginFailReason
    Public Sub New(ByVal Client As L_LoginClient, ByVal reason As LoginFailReason)
        MyBase.makeme(Client)
        _reason = reason
    End Sub

    Protected Friend Overrides Sub write()
        writeC(&H1)
        writeC(CByte(_reason))
    End Sub
End Class

