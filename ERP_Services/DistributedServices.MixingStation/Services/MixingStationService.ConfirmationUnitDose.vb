'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceConfirmationUnitDose

    Public Function SaveConfirmationUnitDose(ConfirmationUnitDose As ConfirmationUnitDose, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose) Implements IMixingStationServiceConfirmationUnitDose.SaveConfirmationUnitDose
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.SaveConfirmationUnitDose(ConfirmationUnitDose, audit)
        End Using
    End Function

    Public Function DeleteConfirmationUnitDose(listIds As List(Of Integer), audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IMixingStationServiceConfirmationUnitDose.DeleteConfirmationUnitDose
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.DeleteConfirmationUnitDose(listIds, audit, TransactionalContainer)
        End Using
    End Function

    Public Function GetConfirmationUnitDoseById(id As Integer) As ActionResult(Of ConfirmationUnitDose) Implements IMixingStationServiceConfirmationUnitDose.GetConfirmationUnitDoseById
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.GetConfirmationUnitDoseById(id)
        End Using
    End Function

    Public Function UpdateMedicalOrderCM(objParams As String, audit As AuditMessage) As ActionResult(Of SP_UpdateMedicalOrder_Result) Implements IMixingStationServiceConfirmationUnitDose.UpdateMedicalOrderCM
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.UpdateMedicalOrderCM(objParams, audit)
        End Using
    End Function

    Public Function SaveConfirmationUnitDoseAndPackage(ConfirmationUnitDose As ConfirmationUnitDose, package As Package, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose) Implements IMixingStationServiceConfirmationUnitDose.SaveConfirmationUnitDoseAndPackage
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.SaveConfirmationUnitDoseAndPackage(ConfirmationUnitDose, package, audit)
        End Using
    End Function

    ''' <summary>
    ''' Verifica una solicitud de tipo NPT
    ''' </summary>
    Public Function VerifyRequestNPT(_ItemTmpt As ConfirmationUnitDoseValidations) As ActionResult Implements IMixingStationServiceConfirmationUnitDose.VerifyRequestNPT
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.VerifyRequestNPT(_ItemTmpt)
        End Using
    End Function

    Public Function SaveConfirmationUnitDoseAndPackageList(confirmationUnitDoses As List(Of ConfirmationUnitDose), package As Package, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose) Implements IMixingStationServiceConfirmationUnitDose.SaveConfirmationUnitDoseAndPackageList
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.SaveConfirmationUnitDoseAndPackageList(confirmationUnitDoses, package, audit)
        End Using
    End Function

    Public Function AnnulateUnitDoses(items As List(Of AnnulateUnitDoseModel), audit As AuditMessage) As ActionResult Implements IMixingStationServiceConfirmationUnitDose.AnnulateUnitDoses
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.AnnulateUnitDoses(items, audit)
        End Using
    End Function

    ''' <summary>
    ''' Establece si la mezcla es segura o no
    ''' </summary>
    ''' Function SetSafeStatus(items As List(Of ConfirmationUnitDoseValidations), audit As AuditMessage) As ActionResult
    Public Function SetSafeStatus(items As List(Of ConfirmationUnitDoseValidations), audit As AuditMessage) As ActionResult Implements IMixingStationServiceConfirmationUnitDose.SetSafeStatus
        Using service As IConfirmationUnitDoseAdminService = Container.Current.Resolve(Of IConfirmationUnitDoseAdminService)()
            Return service.SetSafeStatus(items, audit)
        End Using
    End Function

End Class
