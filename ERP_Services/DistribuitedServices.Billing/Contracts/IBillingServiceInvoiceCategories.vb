'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-10-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IBillingServiceInvoiceCategories

    <OperationContract()> _
    Function GetInvoiceCategory(code As String, audit As AuditMessage) As InvoiceCategories
    <OperationContract()> _
    Function GetInvoiceCategoryById(id As Integer) As InvoiceCategories
    <OperationContract()> _
    Function SaveInvoiceCategory(invoiceCategory As InvoiceCategories, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of InvoiceCategories)
    <OperationContract()> _
    Function DeleteInvoiceCategory(invoiceCategory As InvoiceCategories, audit As AuditMessage) As ActionResult
    <OperationContract()> _
    Function UpdateStateInvoiceCategory(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of InvoiceCategories)

    ''' <summary>
    ''' metodo para pegar en la rejilla del form de categorias
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CopyAndPasteCategories(data As List(Of List(Of String))) As ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer)))

End Interface
