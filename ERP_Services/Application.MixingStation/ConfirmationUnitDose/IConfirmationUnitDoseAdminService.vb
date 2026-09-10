'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/01/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IConfirmationUnitDoseAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveConfirmationUnitDose(ByVal ConfirmationUnitDose As ConfirmationUnitDose, ByVal audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Guarda una confirmacion de dosis
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveConfirmationUnitDoseAndPackage(ConfirmationUnitDose As ConfirmationUnitDose, package As Package, ByVal audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteConfirmationUnitDose(listIds As List(Of Integer), ByVal audit As AuditMessage, TransactionalContainer As String) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetConfirmationUnitDoseById(ByVal id As Integer) As ActionResult(Of ConfirmationUnitDose)

    ''' <summary>
    ''' Verifica una solicitud de tipo NPT
    ''' </summary>
    Function VerifyRequestNPT(_ItemTmpt As ConfirmationUnitDoseValidations) As ActionResult

    ''' <summary>
    ''' Actualiza la orden medica
    ''' </summary>
    ''' <returns></returns>
    Function UpdateMedicalOrderCM(objParams As String, audit As AuditMessage) As ActionResult(Of SP_UpdateMedicalOrder_Result)
    Function SaveConfirmationUnitDoseAndPackageList(confirmationUnitDoses As List(Of ConfirmationUnitDose), package As Package, audit As AuditMessage) As ActionResult(Of ConfirmationUnitDose)
    Function AnnulateUnitDoses(items As List(Of AnnulateUnitDoseModel), audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Establece si la mezcla es segura o no
    ''' </summary>
    Function SetSafeStatus(items As List(Of ConfirmationUnitDoseValidations), audit As AuditMessage) As ActionResult

End Interface