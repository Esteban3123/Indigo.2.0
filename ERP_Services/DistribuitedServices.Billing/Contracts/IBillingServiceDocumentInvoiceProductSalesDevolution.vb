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

<ServiceContract()>
Public Interface IBillingServiceDocumentInvoiceProductSalesDevolution

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveDocumentInvoiceProductSalesDevolution(DocumentInvoiceProductSalesDevolution As DocumentInvoiceProductSalesDevolution, idSequense As Int64, audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution)

    ''' <summary>
    ''' Obtiene un registro por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDocumentInvoiceProductSalesDevolution(code As String, audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution)

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetDocumentInvoiceProductSalesDevolutionById(id As Integer, audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution)

End Interface
