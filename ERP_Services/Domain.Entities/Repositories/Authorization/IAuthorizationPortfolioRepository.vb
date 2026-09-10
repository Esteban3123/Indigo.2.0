'************************************************************
' Assembly         : Domain.Authorization
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IAuthorizationPortfolioRepository
    Inherits IRepository(Of AuthorizationPortfolio)
    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationPortfolioByCode(code As String) As AuthorizationPortfolio
    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAuthorizationPortfolioById(id As Integer) As AuthorizationPortfolio

    ''' <summary>
    ''' Sp que se encarga de guardar el portafolio
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Function SP_SaveAuthorizationPortfolio(xml As String, userCode As String) As SP_SaveAuthorizationPortfolio_Result

    ''' <summary>
    ''' Valida el CopyPaste
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteAuthorizationPortfolioCareCenter(xmlObject As String) As List(Of SP_CopyAndPasteAuthorizationPortfolioCareCenter_Result)

    ''' <summary>
    ''' Valida el CopyPaste
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(xmlObject As String) As List(Of SP_CopyAndPasteAuthorizationPortfolioCUPSEntity_Result)

    ''' <summary>
    ''' Valida el CopyPaste
    ''' </summary>
    ''' <param name="xmlObject">Objeto xml armado con el listado que se envia desde presentation</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(xmlObject As String) As List(Of SP_CopyAndPasteAuthorizationPortfolioInventoryProduct_Result)
End Interface
