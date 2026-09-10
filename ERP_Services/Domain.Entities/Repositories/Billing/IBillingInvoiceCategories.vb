'************************************************************
' Assembly         : Domain.Contract
' Author           : Diego Andres Roldan
' Created          : 26-10-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IBillingInvoiceCategories
    Inherits IRepository(Of InvoiceCategories)

    Function GetInvoiceCategory(code As String) As InvoiceCategories

    Function GetInvoiceCategoryById(id As Integer) As InvoiceCategories

    ''' <summary>
    ''' Valida el CopyPaste del form de categorias
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteCategories(xmlObject As String) As List(Of SP_CopyAndPasteCategories_Result)

    Function GetInvoiceCategoryPOCO(code As String) As InvoiceCategories

    Function GetListInvoiceCategoryPOCO(listCode As List(Of String)) As List(Of InvoiceCategories)
End Interface
