'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceConfirmationUnitDose

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveConfirmationUnitDose(ByVal ConfirmationUnitDose As ConfirmationUnitDose, ByVal audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Guarda una confirmacion de dosis
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveConfirmationUnitDoseAndPackage(ConfirmationUnitDose As ConfirmationUnitDose, package As Package, ByVal audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Verifica una solicitud de tipo NPT
    ''' </summary>
    <OperationContract>
    Function VerifyRequestNPT(_ItemTmpt As ConfirmationUnitDoseValidations) As ActionResult

    <OperationContract()>
    Function SaveConfirmationUnitDoseAndPackageList(confirmationUnitDoses As List(Of ConfirmationUnitDose), package As Package, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteConfirmationUnitDose(listIds As List(Of Integer), ByVal audit As AuditMessage, TransactionalContainer As String) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetConfirmationUnitDoseById(ByVal id As Integer) As ActionResult(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Actualiza la orden medica
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateMedicalOrderCM(objParams As String, audit As AuditMessage) As ActionResult(Of SP_UpdateMedicalOrder_Result)

    <OperationContract()>
    Function AnnulateUnitDoses(items As List(Of AnnulateUnitDoseModel), audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' EStablece si la mezcla es segura o no
    ''' </summary>
    <OperationContract>
    Function SetSafeStatus(items As List(Of ConfirmationUnitDoseValidations), audit As AuditMessage) As ActionResult

End Interface
