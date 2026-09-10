'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class CollectionModificationRepository
    Inherits GenericRepository(Of CollectionModification)
    Implements ICollectionModificationRepository


    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' obtiene una modificacion del recaudo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCollectionModificationByCode(code As String, BudgetaryValidityId As Integer) As CollectionModification Implements ICollectionModificationRepository.GetCollectionModificationByCode
        Dim res = (From c In _context.CollectionModification Where c.Code = code AndAlso c.BudgetaryValidityId = BudgetaryValidityId Select c).FirstOrDefault()
        If res IsNot Nothing Then
            Dim validaty = (From v In _context.BudgetaryValidity.AsNoTracking() Where v.Id = res.BudgetaryValidityId Select v).FirstOrDefault()
            res.BudgetEntityId = validaty.BudgetaryEntityId
            res.CodeCollection = (From c In _context.Collection.AsNoTracking() Where c.Id = res.CollectionId Select c).FirstOrDefault().Code
            res.OriginalValue = (From c In _context.CollectionModification.AsNoTracking() Where c.Code = code AndAlso c.BudgetaryValidityId = BudgetaryValidityId Select c).FirstOrDefault()
            Return res
        Else
            Return New CollectionModification
        End If
    End Function

    ''' <summary>
    ''' obtiene una modificacion del recaudo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCollectionModificationById(id As Integer) As CollectionModification Implements ICollectionModificationRepository.GetCollectionModificationById
        Dim res = (From c In _context.CollectionModification.Include("CollectionModificationDetail") Where c.Id = id Select c).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From c In _context.CollectionModification.AsNoTracking() Where c.Id = id Select c).FirstOrDefault()
            Return res
        End If
        Return New CollectionModification
    End Function

    Public Function SP_SaveCollectionModification(CollectionModificationXml As String, CollectionModificationDetailForDeleteXml As String, CodeUser As String) As SP_SaveCollectionModification_Result Implements ICollectionModificationRepository.SP_SaveCollectionModification
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCollectionModification(CollectionModificationXml, CollectionModificationDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

End Class
