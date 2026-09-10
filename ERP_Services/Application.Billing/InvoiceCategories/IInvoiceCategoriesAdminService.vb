'***********************************************************************
' Assembly         : Application.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 16-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IInvoiceCategoriesAdminService
    Inherits IDisposable

    Function GetInvoiceCategory(code As String, audit As AuditMessage) As InvoiceCategories

    Function GetInvoiceCategoryById(id As Integer) As InvoiceCategories

    Function SaveInvoiceCategory(invoiceCategory As InvoiceCategories, audit As AuditMessage, Optional idSequence As Long = 0) As ActionResult(Of InvoiceCategories)

    Function DeleteInvoiceCategory(invoiceCategory As InvoiceCategories, audit As AuditMessage) As ActionResult

    Function UpdateStateInvoiceCategory(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of InvoiceCategories)

    ''' <summary>
    ''' metodo para pegar en la rejilla de plantilla de categorias
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CopyAndPasteCategories(data As List(Of List(Of String))) As ActionResult(Of List(Of InvoiceCategoriesUser), List(Of Tuple(Of String, Integer)))

End Interface
