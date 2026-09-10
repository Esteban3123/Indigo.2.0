'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IPaymentsDeferredCausationShare

    ''' <summary>
    ''' Guarda o Actualiza una cuota de causacion diferida
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function SaveDeferredCausationShare(ByVal deferredCausationShare As DeferredCausationShare) As ActionResult(Of DeferredCausationShare)

    ''' <summary>
    ''' Actualiza los valores de las cuotas de la causacion diferida
    ''' </summary>
    ''' <param name="ListDeferredCausationShare"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveListDeferredCausationShare(ListDeferredCausationShare As List(Of Domain.Entities.DeferredCausationShare), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.DeferredCausationShare))

    ''' <summary>
    ''' Elimina una cuota de causacion diferida
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function DeleteDeferredCausationShare(ByVal deferredCausationShare As DeferredCausationShare) As ActionResult

    ''' <summary>
    ''' Obtiene una cuota de la causacion diferida por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()> _
    Function GetDeferredCausationShareById(ByVal id As Integer) As ActionResult(Of DeferredCausationShare)

    ''' <summary>
    ''' Obtiene el listado de cuotas de causacion que tiene asociado la cxp
    ''' </summary>
    ''' <param name="accountPayableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetDeferredCausationShareByAccountPayableId(accountPayableId As Integer) As ActionResult(Of List(Of DeferredCausationShare))

End Interface
