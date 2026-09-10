'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IPaymentsServicePaymentsMassiveConfirm

    ''' <summary>
    ''' Metodo para confirmar un documento del módulo de cuentas por pagar
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmPaymentDocument(processId As Integer, code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' metodo para confirmar documentos de cuentas por pagar masivamente
    ''' </summary>
    ''' <param name="processId"></param>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ConfirmPaymentsDocuments(processId As Integer, listDocuments As List(Of String), audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface