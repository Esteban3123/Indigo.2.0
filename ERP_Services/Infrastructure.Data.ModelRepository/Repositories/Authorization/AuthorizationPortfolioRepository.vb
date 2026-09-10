'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class AuthorizationPortfolioRepository
    Inherits GenericRepository(Of AuthorizationPortfolio)
    Implements IAuthorizationPortfolioRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationPortfolioByCode(code As String) As AuthorizationPortfolio Implements IAuthorizationPortfolioRepository.GetAuthorizationPortfolioByCode
        Dim res = (From bg In _context.AuthorizationPortfolio Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.AuthorizationPortfolio.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New AuthorizationPortfolio
        End If
    End Function

    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetAuthorizationPortfolioById(id As Integer) As AuthorizationPortfolio Implements IAuthorizationPortfolioRepository.GetAuthorizationPortfolioById
        Dim res = (From bg In _context.AuthorizationPortfolio.Include("AuthorizationPortfolioCUPSEntity").Include("AuthorizationPortfolioInventoryProduct").Include("AuthorizationPortfolioCareCenter") Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            Return res
        Else
            Return New AuthorizationPortfolio
        End If
    End Function

    ''' <summary>
    ''' Sp que se encarga de guardar un portafolio
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveAuthorizationPortfolio(xml As String, userCode As String) As SP_SaveAuthorizationPortfolio_Result Implements IAuthorizationPortfolioRepository.SP_SaveAuthorizationPortfolio
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAuthorizationPortfolio(xml, userCode).SingleOrDefault
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioCareCenter(xmlObject As String) As List(Of SP_CopyAndPasteAuthorizationPortfolioCareCenter_Result) Implements IAuthorizationPortfolioRepository.SP_CopyAndPasteAuthorizationPortfolioCareCenter
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteAuthorizationPortfolioCareCenter(xmlObject).ToList()
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(xmlObject As String) As List(Of SP_CopyAndPasteAuthorizationPortfolioCUPSEntity_Result) Implements IAuthorizationPortfolioRepository.SP_CopyAndPasteAuthorizationPortfolioCUPSEntity
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteAuthorizationPortfolioCUPSEntity(xmlObject).ToList()
    End Function

    Public Function SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(xmlObject As String) As List(Of SP_CopyAndPasteAuthorizationPortfolioInventoryProduct_Result) Implements IAuthorizationPortfolioRepository.SP_CopyAndPasteAuthorizationPortfolioInventoryProduct
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPasteAuthorizationPortfolioInventoryProduct(xmlObject).ToList()
    End Function

End Class
