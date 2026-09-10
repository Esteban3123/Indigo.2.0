Imports System.Text.RegularExpressions

Partial Public Class Email

#Region "Methods"

    Function IsValidEmailFormat() As Boolean
        Return Regex.IsMatch(Me.Email1, "^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
    End Function

#End Region

End Class
