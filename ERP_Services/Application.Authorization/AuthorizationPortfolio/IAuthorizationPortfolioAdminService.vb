'***********************************************************************
' Assembly         : Application.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface IAuthorizationPortfolioAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAuthorizationPortfolio(ByVal AuthorizationPortfolio As AuthorizationPortfolio, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of AuthorizationPortfolio)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAuthorizationPortfolio(ByVal AuthorizationPortfolio As AuthorizationPortfolio, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetAuthorizationPortfolio(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of AuthorizationPortfolio)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetAuthorizationPortfolioById(ByVal id As Integer) As AuthorizationPortfolio

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateAuthorizationPortfolio(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of AuthorizationPortfolio)

    ''' <summary>
    ''' CopyPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteAuthorizationPortfolioCareCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' CopyPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' CopyPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer)))

End Interface
