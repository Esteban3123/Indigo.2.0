'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IDocumentInvoiceProductSalesDevolutionRepository
    Inherits IRepository(Of DocumentInvoiceProductSalesDevolution)

    ''' <summary>
    ''' obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentInvoiceProductSalesDevolutionByCode(code As String) As DocumentInvoiceProductSalesDevolution

    ''' <summary>
    ''' obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentInvoiceProductSalesDevolutionById(id As Integer) As DocumentInvoiceProductSalesDevolution

    ''' <summary>
    ''' Proceso de devolución parcial de venta
    ''' </summary>
    ''' <param name="xmlData"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_SaveDocumentInvoiceProductSalesDevolution(xmlData As String, codeUser As String) As SP_SaveDocumentInvoiceProductSalesDevolution_Result

End Interface
