Imports Domain.Base.Entities

Partial Public Class ScheduleDetailHour

    Public Overloads Shared Widening Operator CType(ByVal value As ScheduleDetailHour) As NoveltyScheduleDetailHour
        Dim detalleHora As New NoveltyScheduleDetailHour()
        detalleHora.DateTimeInitial = value.DateTimeInitial
        detalleHora.DateTimeEnding = value.DateTimeEnding
        detalleHora.TotalNumberHours = value.TotalNumberHours
        detalleHora.NextDay = value.NextDay
        detalleHora.AppliedLiquidationConcept = value.AppliedLiquidationConcept
        detalleHora.State = value.State
        detalleHora.NoveltyScheduleDetailConcept = New TrackableCollection(Of NoveltyScheduleDetailConcept)()
        For Each detalleConcepto As ScheduleDetailConcept In value.ScheduleDetailConcept
            detalleHora.NoveltyScheduleDetailConcept.Add(CType(detalleConcepto, NoveltyScheduleDetailConcept))
        Next
        Return detalleHora
    End Operator

End Class
