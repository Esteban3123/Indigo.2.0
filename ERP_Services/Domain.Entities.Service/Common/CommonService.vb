Public Class CommonService

    ''' <summary>
    ''' Funcion que calcula el siguiente dia habil
    ''' </summary>
    ''' <param name="InitialDate">The initial date.</param>
    ''' <param name="holidays">The holidays.</param>
    ''' <param name="SaturdayBusiness">if set to <c>true</c> [saturday business].</param>
    ''' <param name="SundayBusiness">if set to <c>true</c> [sunday business].</param>
    ''' <returns></returns>
    Public Shared Function GetBusinessDay(ByVal InitialDate As Date, ByVal holidays As List(Of Domain.Entities.Holiday), ByVal SaturdayBusiness As Boolean, ByVal SundayBusiness As Boolean) As Date
        Dim dayBusiness As Boolean = False
        While Not dayBusiness
            Dim num As Integer = holidays.FindAll(Function(x) x.Holiday1 = InitialDate).Cast(Of Holiday).ToList().Count
            If num > 0 Then
                InitialDate = InitialDate.AddDays(1)
            Else
                If InitialDate.DayOfWeek = DayOfWeek.Saturday AndAlso Not SaturdayBusiness = True Then
                    InitialDate = InitialDate.AddDays(1)
                ElseIf InitialDate.DayOfWeek = DayOfWeek.Sunday AndAlso Not SundayBusiness = True Then
                    InitialDate = InitialDate.AddDays(1)
                Else
                    dayBusiness = True
                End If
            End If
        End While
        Return InitialDate
    End Function

    ''' <summary>
    ''' Funcion para obtener la las cuotas de un valor de manera exacta es decir sin decimales
    ''' </summary>
    ''' <param name="value">Valor</param>
    ''' <param name="numberShare">Cuotas</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GetValueShare(value As Decimal, numberShare As Integer) As List(Of Decimal)
        Dim valueShare As Decimal
        Dim listValue As List(Of Decimal) = New List(Of Decimal)()
        valueShare = Int(value / numberShare)
        If value Mod numberShare = 0 Then
            For x = 1 To numberShare
                listValue.Add(valueShare)
            Next
        Else
            For x = 1 To numberShare
                If x = numberShare Then
                    listValue.Add(valueShare + (value Mod numberShare))
                Else
                    listValue.Add(valueShare)
                End If
            Next
        End If
        Return listValue
    End Function

End Class
