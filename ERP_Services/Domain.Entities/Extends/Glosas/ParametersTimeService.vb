Public Class ParametersTimeService

    Public Shared Function createListParametersTime(ByVal ObjetionDGlosa As Domain.Entities.TrazabilityParametersTime, ByVal objetionDReiteration As Domain.Entities.TrazabilityParametersTime, ConciliationD As Domain.Entities.TrazabilityParametersTime, userGlosa As String, userReiteration As String, userConciliation As String) As List(Of ControlParametersTime)

        Dim listControlTime As List(Of ControlParametersTime) = New List(Of ControlParametersTime)
        Dim control As ControlParametersTime

        If ObjetionDGlosa IsNot Nothing Then

            Dim spendGlosa = 0

            If ObjetionDGlosa.CompleteDate IsNot Nothing Then
                spendGlosa = (ObjetionDGlosa.CompleteDate - ObjetionDGlosa.DocumentDate).Value.TotalDays
            End If

            control = New ControlParametersTime With {.Code = "01", .OperationDate = ObjetionDGlosa.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, CDbl(ObjetionDGlosa.MaxTimeExtemporaneousGlosa), ObjetionDGlosa.DocumentDate), .CompleteDate = ObjetionDGlosa.CompleteDate, .ResponsibleNameCode = userGlosa, .TimeParameters = ObjetionDGlosa.MaxTimeExtemporaneousGlosa, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays), _
                                                      .SpendTime = spendGlosa}
            listControlTime.Add(control)

            Dim responsibleEvaluationGlosa = ""
            If ObjetionDGlosa.GlosaPortFolio.Responsible2 IsNot Nothing Then
                responsibleEvaluationGlosa = ObjetionDGlosa.GlosaPortFolio.Responsible2.CodeUser.Trim + " - " + ObjetionDGlosa.GlosaPortFolio.Responsible2.Name
            End If

            control = New ControlParametersTime With {.Code = "02", .OperationDate = ObjetionDGlosa.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, CDbl(ObjetionDGlosa.MaxTimeResponse), ObjetionDGlosa.DocumentDate), .CompleteDate = ObjetionDGlosa.GlosaPortFolio.EvaluationDateGlosa, .ResponsibleNameCode = responsibleEvaluationGlosa, .TimeParameters = ObjetionDGlosa.MaxTimeResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
            listControlTime.Add(control)

            Dim responsibleCoordinationGlosa = ""
            If ObjetionDGlosa.GlosaPortFolio.Responsible IsNot Nothing Then
                responsibleCoordinationGlosa = ObjetionDGlosa.GlosaPortFolio.Responsible.CodeUser.Trim + " - " + ObjetionDGlosa.GlosaPortFolio.Responsible.Name
            End If

            control = New ControlParametersTime With {.Code = "03", .OperationDate = ObjetionDGlosa.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, CDbl(ObjetionDGlosa.MaxTimeSendingDocumentResponse), ObjetionDGlosa.DocumentDate), .CompleteDate = ObjetionDGlosa.GlosaPortFolio.CoordinationDateGlosa, .ResponsibleNameCode = responsibleCoordinationGlosa, .TimeParameters = ObjetionDGlosa.MaxTimeSendingDocumentResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
            listControlTime.Add(control)
        End If

        If objetionDReiteration IsNot Nothing Then

            Dim spendReiteration = 0

            If objetionDReiteration.CompleteDate IsNot Nothing Then
                spendReiteration = (objetionDReiteration.CompleteDate - objetionDReiteration.DocumentDate).Value.TotalDays
            End If

3:          control = New ControlParametersTime With {.Code = "04", .OperationDate = objetionDReiteration.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, CDbl(objetionDReiteration.MaxTimeExtemporaneousGlosa), objetionDReiteration.DocumentDate), .CompleteDate = objetionDReiteration.CompleteDate, .ResponsibleNameCode = userReiteration, .TimeParameters = objetionDReiteration.MaxTimeExtemporaneousGlosa, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays), _
                                                      .SpendTime = spendReiteration}
            listControlTime.Add(control)

            Dim responsibleEvaluationReiteration = ""
            If objetionDReiteration.GlosaPortFolio.Responsible3 IsNot Nothing Then
                responsibleEvaluationReiteration = objetionDReiteration.GlosaPortFolio.Responsible3.CodeUser.Trim + " - " + objetionDReiteration.GlosaPortFolio.Responsible3.Name
            End If

            control = New ControlParametersTime With {.Code = "05", .OperationDate = objetionDReiteration.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, CDbl(objetionDReiteration.MaxTimeResponse), objetionDReiteration.DocumentDate), .CompleteDate = objetionDReiteration.GlosaPortFolio.EvaluationDateReiteration, .ResponsibleNameCode = responsibleEvaluationReiteration, .TimeParameters = objetionDReiteration.MaxTimeResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
            listControlTime.Add(control)

            Dim responsibleCoordinationReiteration = ""
            If objetionDReiteration.GlosaPortFolio.Responsible1 IsNot Nothing Then
                responsibleCoordinationReiteration = objetionDReiteration.GlosaPortFolio.Responsible1.CodeUser.Trim + " - " + objetionDReiteration.GlosaPortFolio.Responsible1.Name
            End If

            control = New ControlParametersTime With {.Code = "06", .OperationDate = objetionDReiteration.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, CDbl(objetionDReiteration.MaxTimeSendingDocumentResponse), objetionDReiteration.DocumentDate), .CompleteDate = objetionDReiteration.GlosaPortFolio.CoordinationDateReiteration, .ResponsibleNameCode = responsibleCoordinationReiteration, .TimeParameters = objetionDReiteration.MaxTimeSendingDocumentResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays)}
            listControlTime.Add(control)
        End If


        If ConciliationD IsNot Nothing Then

            Dim spendConciliation = 0

            If ConciliationD.CompleteDate IsNot Nothing Then
                spendConciliation = (ConciliationD.CompleteDate - ConciliationD.DocumentDate).Value.TotalDays
            End If

            control = New ControlParametersTime With {.Code = "07", .OperationDate = ConciliationD.DocumentDate, .LimitDate = DateAdd(DateInterval.DayOfYear, CDbl(ConciliationD.MaxTimeResponse), ConciliationD.DocumentDate), .CompleteDate = ConciliationD.CompleteDate, .ResponsibleNameCode = userConciliation, .TimeParameters = ConciliationD.MaxTimeResponse, .RemainingTime = IIf((.LimitDate - Date.Now).TotalDays < 0, 0, (.LimitDate - Date.Now).TotalDays), _
                                                      .SpendTime = spendConciliation}

            listControlTime.Add(control)
        End If

        Return listControlTime

    End Function

End Class
