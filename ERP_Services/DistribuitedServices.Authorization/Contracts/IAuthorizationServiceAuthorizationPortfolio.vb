'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/03/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IAuthorizationServiceAuthorizationPortfolio
    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAuthorizationPortfolio(AuthorizationPortfolio As Domain.Entities.AuthorizationPortfolio, idSequense As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AuthorizationPortfolio)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAuthorizationPortfolio(AuthorizationPortfolio As Domain.Entities.AuthorizationPortfolio, audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAuthorizationPortfolio(code As String, audit As AuditMessage) As ActionResult(Of Domain.Entities.AuthorizationPortfolio)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAuthorizationPortfolioById(id As Integer, audit As AuditMessage) As Domain.Entities.AuthorizationPortfolio

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateAuthorizationPortfolio(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AuthorizationPortfolio)

    ''' <summary>
    ''' CopyPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SP_CopyAndPasteAuthorizationPortfolioCareCenter(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCareCenter), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' CopyPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioCUPSEntity), List(Of Tuple(Of String, Integer)))

    ''' <summary>
    ''' CopyPaste
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(data As List(Of List(Of String))) As ActionResult(Of List(Of AuthorizationPortfolioInventoryProduct), List(Of Tuple(Of String, Integer)))
End Interface
