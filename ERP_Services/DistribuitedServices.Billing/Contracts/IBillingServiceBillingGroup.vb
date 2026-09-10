'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IBillingServiceBillingGroup
    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveBillingGroup(BillingGroup As Domain.Entities.BillingGroup, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingGroup)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteBillingGroup(BillingGroup As Domain.Entities.BillingGroup, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBillingGroup(code As String, audit As AuditMessage) As Domain.Entities.BillingGroup

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetBillingGroupById(id As Integer, audit As AuditMessage) As Domain.Entities.BillingGroup

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateBillingGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingGroup)
End Interface
