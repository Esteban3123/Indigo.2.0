Imports Domain.Base.Entities

Public Partial Class ScheduleDetail

    Public ReadOnly Property HourDescription As String
        Get
            Dim concat As String = ""
            Dim indice As Integer = 0
            Dim StrFinal As String = " Y "
            Me.ScheduleDetailHour.ToList().ForEach(Sub(x)
                                                       indice += 1
                                                       If indice = Me.ScheduleDetailHour.Count Then
                                                           StrFinal = ""
                                                       End If
                                                       concat &= String.Concat(x.DateTimeInitial.ToString("hh:mm") & " A " & x.DateTimeEnding.ToString("hh:mm") & StrFinal)
                                                   End Sub)
            Return concat
        End Get
    End Property

    Public Overloads Shared Widening Operator CType(ByVal value As ScheduleDetail) As NoveltyScheduleDetail
        Dim deduccionDetalle As New NoveltyScheduleDetail()
        deduccionDetalle.GroupId = value.GroupId
        deduccionDetalle.EmployeeId = value.EmployeeId
        deduccionDetalle.CompanyId = value.CompanyId
        deduccionDetalle.BranchOfficeId = value.BranchOfficeId
        deduccionDetalle.FunctionalUnitId = value.FunctionalUnitId
        deduccionDetalle.CostCenterId = value.CenterCostId
        deduccionDetalle.ScheduleFunctionalUnitId = value.ScheduleFunctionalUnitId
        deduccionDetalle.Letter = value.Letter
        deduccionDetalle.DateDetail = value.DateDetail
        deduccionDetalle.Period = value.DateDetail.ToString("MM/yyyy")
        deduccionDetalle.TotalNumberHours = value.TotalNumberHours
        deduccionDetalle.ScheduleTemplateId = value.ScheduleTemplateId
        deduccionDetalle.Status = value.Status
        deduccionDetalle.State = value.State
        deduccionDetalle.NoveltyScheduleDetailHour = New TrackableCollection(Of NoveltyScheduleDetailHour)()
        For Each detalleHora As ScheduleDetailHour In value.ScheduleDetailHour
            deduccionDetalle.NoveltyScheduleDetailHour.Add(CType(detalleHora, NoveltyScheduleDetailHour))
        Next
        Return deduccionDetalle
    End Operator

End Class
