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

Public Class CollectionDetailRepository
    Inherits GenericRepository(Of CollectionDetail)
    Implements ICollectionDetailRepository



    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region


    ''' <summary>
    ''' obtiene le detalle del recaudo por id de la cabecera
    ''' </summary>
    ''' <param name="CollectionId"></param>
    ''' <returns></returns>
    Public Function GetCollectionDetailByCollectionId(CollectionId As Integer) As List(Of CollectionDetail) Implements ICollectionDetailRepository.GetCollectionDetailByCollectionId
        Dim res = (From cd In _context.CollectionDetail Where cd.CollectionId = CollectionId Select cd).ToList()
        For Each item In res
            Dim recognitionDetail = (From rd In _context.RecognitionDetail.AsNoTracking() Where rd.Id = item.RecognitionDetailId Select rd).FirstOrDefault()
            Dim recognition = (From r In _context.Recognition.AsNoTracking() Where r.Id = recognitionDetail.RecognitionId Select r).FirstOrDefault()
            Dim budget = (From b In _context.Budget.AsNoTracking() Where b.CategoryId = recognitionDetail.CategoryId And b.RevenueTypeId = recognitionDetail.RevenueTypeId Select b).FirstOrDefault()
            Dim category = (From c In _context.Category.AsNoTracking() Where c.Id = budget.CategoryId Select c).FirstOrDefault()
            If category.FinancialSourceId IsNot Nothing Then
                item.CodeNameFinancialSource = (From fs In _context.FinancialSource.AsNoTracking() Where fs.Id = category.FinancialSourceId Select String.Concat(fs.Code, " - ", fs.Name)).FirstOrDefault()
            End If
            item.CodeNameCategory = String.Concat(category.Code, " - ", category.Name)
            item.CodeRecognition = recognition.Code
            item.RecognitionBalance = recognitionDetail.Balance
        Next
        Return res
    End Function

   

    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCollectionDetailById(id As Integer) As CollectionDetail Implements ICollectionDetailRepository.GetCollectionDetailById
        Return (From cd In _context.CollectionDetail Where cd.Id = id Select cd).FirstOrDefault()
    End Function
End Class
