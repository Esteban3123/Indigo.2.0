'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Angi Camila Duran Vargas
'Created          : 27-07-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class ProductInTransitDetailRepository
    Inherits GenericRepository(Of ProductInTransitDetail)
    Implements IProductInTransitDetailRepository



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
    ''' lista el detalla de la remision
    ''' </summary>
    ''' <param name="idProductInTransit"></param>
    ''' <returns></returns>
    Public Function ListProductInTransitDetailByIdProductInTransit(idProductInTransit As Integer) As List(Of ProductInTransitDetail) Implements IProductInTransitDetailRepository.ListProductInTransitDetailByIdProductInTransit
        Dim res = (From red In _context.ProductInTransitDetail Where red.ProductInTransitId = idProductInTransit Select red).ToList()
        Return res
    End Function

    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetProductInTransitDetailById(Id As Integer) As ProductInTransitDetail Implements IProductInTransitDetailRepository.GetProductInTransitDetailById
        Dim remissionEntranceDetail = (From red In _context.ProductInTransitDetail Where red.Id = Id Select red).FirstOrDefault()

        If remissionEntranceDetail IsNot Nothing Then
            Dim kardex = (From k In _context.Kardex Where k.EntityId = remissionEntranceDetail.ProductInTransitId And k.EntityName = GetType(ProductInTransit).Name And k.ProductId = remissionEntranceDetail.ProductId Select k).ToList()
            If kardex Is Nothing OrElse kardex.Count = 0 Then
                remissionEntranceDetail.ValueInKardex = remissionEntranceDetail.UnitValue
            Else
                remissionEntranceDetail.ValueInKardex = kardex.Sum(Function(k) k.Quantity * k.Value) / kardex.Sum(Function(k) k.Quantity)
            End If
        End If

        Return remissionEntranceDetail
    End Function
End Class
