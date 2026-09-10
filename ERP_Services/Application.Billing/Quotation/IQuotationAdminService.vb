'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 27/01/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IQuotationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveQuotation(ByVal Quotation As Quotation, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Quotation)

    ''' <summary>
    ''' Obtiene por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetQuotation(ByVal code As String) As ActionResult(Of Quotation)

    ''' <summary>
    ''' Obtiene por id
    ''' </summary>
    ''' <returns></returns>
    Function GetQuotationById(ByVal id As Integer) As ActionResult(Of Quotation)

End Interface
