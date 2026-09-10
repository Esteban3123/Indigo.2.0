Public Class DateTimeHelper

#Region "Constants"

    Public Const COLOMBIA_TIMEZONE_ID As String = "SA Pacific Standard Time"

#End Region

#Region "Methods"

    Public Shared Function GetColombianDate() As DateTime
        Dim timezone = TimeZoneInfo.FindSystemTimeZoneById(COLOMBIA_TIMEZONE_ID)
        Return TimeZoneInfo.ConvertTime(DateTime.Now, timezone)
    End Function

#End Region

End Class
