'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IDocumentInvoiceProductSalesDevolutionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza el registro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveDocumentInvoiceProductSalesDevolution(ByVal DocumentInvoiceProductSalesDevolution As DocumentInvoiceProductSalesDevolution, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DocumentInvoiceProductSalesDevolution)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetDocumentInvoiceProductSalesDevolution(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of DocumentInvoiceProductSalesDevolution)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <returns></returns>
    Function GetDocumentInvoiceProductSalesDevolutionById(ByVal id As Integer) As ActionResult(Of DocumentInvoiceProductSalesDevolution)

End Interface
