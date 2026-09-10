'***********************************************************************'
' Assembly         : Presentation.Controls                              '
' Author           : Jorge Leonardo Vernaza                             '
' Created          : 01-10-2012                                         '
'                                                                       '
' Last Modified By :                                                    '
' Last Modified On :                                                    '
' Description      :                                                    '
'                                                                       '
' Copyright        : (c) . All rights reserved.                         '
'***********************************************************************'

#Region "Librerias Importadas"
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Net
Imports System.Threading.Tasks
Imports System.Text.RegularExpressions
Imports System.Net.Http
#End Region


Public NotInheritable Class CommonUtilities

    ' Clean up text fields from each SyndicationItem. 
    Public Shared Function ConvertHtml(value As [String]) As [String]
        If value Is Nothing Then
            Return Nothing
        End If

        Dim maxLength As Integer = 200
        Dim strLength As Integer = 0
        Dim fixedString As String = ""

        fixedString = Regex.Replace(value, "<[^>]+>", String.Empty)

        ' Remove newline characters.
        fixedString = fixedString.Replace(Microsoft.VisualBasic.vbCr, "").Replace(Microsoft.VisualBasic.vbCr, "")

        ' Remove encoded HTML characters.
        fixedString = WebUtility.HtmlDecode(fixedString)

        strLength = fixedString.ToString().Length

        If strLength = 0 Then
            Return Nothing

        ElseIf strLength >= maxLength Then
            fixedString = fixedString.Substring(0, maxLength)

            fixedString = fixedString.Substring(0, fixedString.LastIndexOf(" "))
        End If

        fixedString += "..."

        Return fixedString
    End Function


    Public Shared Function ConvertGuid(value As [String]) As [String]

        Dim data As String = ""
        Dim position As Integer = 0

        position = value.IndexOf("http")

        data = value.Substring(position)

        Return data

    End Function

    Public Shared Function timeStampToDateTime(unixTimeStamp As Double) As DateTime
        ' Unix timestamp is seconds past epoch
        Dim dtDateTime As System.DateTime = New DateTime(1970, 1, 1, 0, 0, 0, _
            0)
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime()
        Return dtDateTime
    End Function

    'Public Shared Function StringToAscii(s As String) As Byte()


    '    Dim retval As Byte() = New Byte(s.Length - 1) {}
    '    For ix As Integer = 0 To s.Length - 1
    '        Dim ch As Char = s(ix)
    '        If ch <= &H7F Then
    '            retval(ix) = CByte(Microsoft.VisualBasic.AscW(ch))
    '        Else
    '            retval(ix) = CByte(Microsoft.VisualBasic.AscW("?"c))
    '        End If
    '    Next
    '    Return retval
    'End Function


    Public Shared Function ConvertDay(day As String) As String
        Dim daySpanish As String = ""

        Select Case day
            Case "Monday"
                daySpanish = "Lunes"
                Exit Select
            Case "Tuesday"
                daySpanish = "Martes"
                Exit Select
            Case "Wednesday"
                daySpanish = "Miercoles"
                Exit Select
            Case "Thursday"
                daySpanish = "Jueves"
                Exit Select
            Case "Friday"
                daySpanish = "Viernes"
                Exit Select
            Case "Saturday"
                daySpanish = "Sabado"
                Exit Select
            Case "Sunday"
                daySpanish = "Domingo"
                Exit Select
            Case Else
                daySpanish = "N/A"
                Exit Select
        End Select

        Return daySpanish
    End Function

End Class