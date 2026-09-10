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

#End Region

Public Class CollectionRepository
    Inherits GenericRepository(Of Collection)
    Implements ICollectionRepository


    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' obtiene un recaudo del pac por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCollectionByCode(code As String, BudgetaryValidityId As Integer) As Collection Implements ICollectionRepository.GetCollectionByCode
        Dim res = (From c In _context.Collection Where c.Code = code AndAlso c.BudgetaryValidityId = BudgetaryValidityId Select c).FirstOrDefault()
        If res IsNot Nothing Then
            Dim validaty = (From v In _context.BudgetaryValidity.AsNoTracking() Where v.Id = res.BudgetaryValidityId Select v).FirstOrDefault()
            res.BudgetEntityId = validaty.BudgetaryEntityId
            res.NitNameThirdParty = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = res.ThirdPartyId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            res.OriginalValue = (From c In _context.Collection.AsNoTracking() Where c.Code = code AndAlso c.BudgetaryValidityId = BudgetaryValidityId Select c).FirstOrDefault()
            Return res
        Else
            Return New Collection
        End If
    End Function

    ''' <summary>
    ''' obtiene un recaudo del pac por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCollectionById(id As Integer) As Collection Implements ICollectionRepository.GetCollectionById
        Dim res = (From c In _context.Collection.Include("CollectionDetail") Where c.Id = id Select c).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From c In _context.Collection.AsNoTracking() Where c.Id = id Select c).FirstOrDefault()
            Return res
        End If
        Return New Collection
    End Function

    ''' <summary>
    ''' Crea un recaudo con un store procedure y enviando el objeto como Xml
    ''' </summary>
    ''' <param name="collectionlXml"></param>
    ''' <param name="collectionDetailForDeleteXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCollection(collectionlXml As String, collectionDetailForDeleteXml As String, codeUser As String) As SP_SaveCollection_Result Implements ICollectionRepository.SaveCollection
        Return _context.SP_SaveCollection(collectionlXml, collectionDetailForDeleteXml, codeUser).SingleOrDefault
    End Function

End Class
