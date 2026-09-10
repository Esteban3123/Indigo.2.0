Imports System.Runtime.CompilerServices
Imports Infrastructure.Data.Xpo.AuthorizationRepository

Module Extension
    <Extension()>
    Public Function ConvertToViewListRequests(_viewListRequestsTraceabilityXpo As List(Of ViewListRequestsTraceabilityXpo)) As List(Of ViewListRequestsXpo)
        If _viewListRequestsTraceabilityXpo Is Nothing Then
            Return New List(Of ViewListRequestsXpo)
        End If
        Dim ListViewListRequestsXpo = New List(Of ViewListRequestsXpo)
        For Each x In _viewListRequestsTraceabilityXpo
            Dim ViewListRequestsXpo = New ViewListRequestsXpo
            With ViewListRequestsXpo
                .Id = x.Id
                .EntityName = x.EntityName
                .EntityId = x.EntityId
                .CareCenterCode = x.CareCenterCode
                .CareCenterCodeName = x.CareCenterCodeName
                .FunctionalUnitCode = x.FunctionalUnitCode
                .FunctionalUnitName = x.FunctionalUnitName
                .FunctionalUnitCodeName = x.FunctionalUnitCodeName
                .AdmissionNumber = x.AdmissionNumber
                .Folio = x.Folio
                .TypeClinicalHistory = x.TypeClinicalHistory
                .CareGroupId = x.CareGroupId
                .CareGroupCode = x.CareGroupCode
                .CareGroupName = x.CareGroupName
                .CareGroupCodeName = x.CareGroupCodeName
                .HealthAdministratorId = x.HealthAdministratorId
                .HealthAdministratorCode = x.HealthAdministratorCode
                .HealthAdministratorName = x.HealthAdministratorName
                .HealthAdministratorCodeName = x.HealthAdministratorCodeName
                .PatientCode = x.PatientCode
                .PatientName = x.PatientName
                .PatientAddress = x.PatientAddress
                .PatientPhone = x.PatientPhone
                .PatientAge = x.PatientAge
                .RequestDate = x.RequestDate
                .ProfessionalCode = x.ProfessionalCode
                .Quantity = x.Quantity
                .Type = x.Type
                .ServiceId = x.ServiceId
                .ServiceCode = x.ServiceCode
                .ItemCodeOriginal = x.ItemCodeOriginal
                .ServiceDescription = x.ServiceDescription
                .ContractDescriptionId = x.ContractDescriptionId
                .ContractDescriptionCodeName = x.ContractDescriptionCodeName
                .IsCovered = x.IsCovered
                .Contracted = x.Contracted
                .Quoted = x.Quoted
                .AssignUserCode = x.AssignUserCode
                .AssignUser = x.AssignUser
                .RequestTime = x.RequestTime
                .RequestUnitTime = x.RequestUnitTime
                .RequestElapsedTime = x.RequestElapsedTime
                .ColorRequest = x.ColorRequest
                .TraceabilityPaperworkId = x.TraceabilityPaperworkId
                .TraceabilityPaperworkStatus = x.TraceabilityPaperworkStatus
                .TraceabilityPaperworkEventsId = x.TraceabilityPaperworkEventsId
                .TraceabilityPaperworkEventsStatus = x.TraceabilityPaperworkEventsStatus
                .AuthorizationSourceId = x.AuthorizationSourceId
                .IsManual = x.IsManual
                .Observations = x.Observations
                .Alert = x.Alert
                .PatientThirdPartyId = x.PatientThirdPartyId
                .AuthorizationGroupId = x.AuthorizationGroupId
                .AuthorizationGroupCodeName = x.AuthorizationGroupCodeName
                .ProfessionalCodeName = x.ProfessionalCodeName
                .CareCenterTargetCodeName = x.CareCenterTargetCodeName
                .FunctionalUnitTargetCodeName = x.FunctionalUnitTargetCodeName
                .DiagnosticCode = x.DiagnosticCode
                .DiagnosticDescription = x.DiagnosticDescription
                .TraceabilityPaperworkPostponementReasonsId = x.TraceabilityPaperworkPostponementReasonsId
                .PostponementReasonsId = x.PostponementReasonsId
                .PostponementDate = x.PostponementDate
                .PostponementCreationDate = x.PostponementCreationDate
                .PostponementCreationUser = x.PostponementCreationUser
                .PostponementCodeName = x.PostponementCodeName
                .PreviousStatus = x.PreviousStatus
                .SelectOption = x.SelectOption
            End With
            ListViewListRequestsXpo.Add(ViewListRequestsXpo)
        Next


        Return ListViewListRequestsXpo

    End Function

End Module
