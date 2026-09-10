'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class RemissionOutputDetailRepository
    Inherits GenericRepository(Of RemissionOutputDetail)
    Implements IRemissionOutputDetailRepository


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
    ''' <param name="RemissionOutputId"></param>
    ''' <returns></returns>
    Public Function ListRemissionOutputDetailByIdRemissionOutput(RemissionOutputId As Integer) As List(Of RemissionOutputDetail) Implements IRemissionOutputDetailRepository.ListRemissionOutputDetailByIdRemissionOutput
        Dim res = (From rod In _context.RemissionOutputDetail.Include("RemissionOutputDetailPhysical") Where rod.RemissionOutputId = RemissionOutputId Select rod).ToList()
        For Each item In res
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            Dim packingUnit = (From pu In _context.PackagingUnit.AsNoTracking() Where pu.Id = product.PackagingUnitId Select pu).FirstOrDefault()
            item.consumptionUnit = packingUnit.Code + " - " + packingUnit.Name
        Next
        Return res
    End Function

    
    ''' <summary>
    ''' obtiene un detalle de la remision por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetRemissionOutputDetailById(Id As Integer) As RemissionOutputDetail Implements IRemissionOutputDetailRepository.GetRemissionOutputDetailById
        Return (From rod In _context.RemissionOutputDetail Where rod.Id = Id Select rod).FirstOrDefault()
    End Function
End Class
