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
Public Interface IPaymentsDeferredCausation

    ''' <summary>
    ''' Guarda o Actualiza una causacion diferida
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveDeferredCausation(deferredCausation As Domain.Entities.DeferredCausation, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DeferredCausation)

    ''' <summary>
    ''' Elimina una causacion diferida
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteDeferredCausation(deferredCausation As Domain.Entities.DeferredCausation, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDeferredCausationByIdAccountPayable(idAccountPayable As Integer, audit As AuditMessage) As List(Of Domain.Entities.DeferredCausation)

    ''' <summary>
    ''' Obtiene un listado de causaciones diferidas por el codigo de la cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDeferredCausationByAccountPayableCode(Code As String, audit As AuditMessage) As List(Of Domain.Entities.DeferredCausation)

End Interface
