'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Carlos Ernesto Cordoba
' Created          : 22/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IContractServiceIPSServiceGroup
    ''' <summary>
    ''' Guarda o Actualiza una IPSServiceGroup
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveIPSServiceGroup(IPSServiceGroup As Domain.Entities.BillingConcept, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingConcept)
    ''' <summary>
    ''' Elimina una IPSServiceGroup
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteIPSServiceGroup(IPSServiceGroup As Domain.Entities.BillingConcept, audit As AuditMessage) As Domain.Base.Entities.ActionResult
    ''' <summary>
    ''' Obtiene una IPSServiceGroup por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetIPSServiceGroup(code As String, audit As AuditMessage) As Domain.Entities.BillingConcept
    ''' <summary>
    ''' Obtiene una IPSServiceGroup por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetIPSServiceGroupById(ByVal id As Integer) As BillingConcept
    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateIPSServiceGroup(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BillingConcept)
    ''' <summary>
    ''' Copia y pega los centros de costo por sucursal y unidad funcional
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function CopyAndPasteBillingConceptCostCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of BillingConceptCostCenter))
End Interface
