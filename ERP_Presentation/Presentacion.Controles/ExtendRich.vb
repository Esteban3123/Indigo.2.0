Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices

Module ExtendRich
    <Extension()>
    Function CreateExpression(dic As Dictionary(Of String, String)) As String
        Dim lista As List(Of String) = New List(Of String)(New String() {"(", ")", ".", "$", "^", "{", "[", "*", "+", "?", "\", "'", "|"})
        Dim ExpressionString As String = "("
        For Each item As KeyValuePair(Of String, String) In dic
            Dim charData = item.Key.ToCharArray
            Dim itemKey As String = ""
            For Each itemInt As String In charData
                If lista.Contains(itemInt) Then
                    itemKey = itemKey & "\" & itemInt
                Else
                    itemKey = itemKey & itemInt
                End If
            Next
            ExpressionString = ExpressionString & itemKey & "|"
        Next
        ExpressionString = ExpressionString.Substring(0, ExpressionString.Length - 1)
        ExpressionString = ExpressionString & ")"
        Return ExpressionString
    End Function
End Module
