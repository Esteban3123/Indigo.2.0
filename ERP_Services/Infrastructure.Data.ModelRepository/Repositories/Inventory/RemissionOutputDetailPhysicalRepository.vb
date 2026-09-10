'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class RemissionOutputDetailPhysicalRepository
    Inherits GenericRepository(Of RemissionOutputDetailPhysical)
    Implements IRemissionOutputDetailPhysicalRepository



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
    ''' lista los detalles del detalle de la remision de salida
    ''' </summary>
    ''' <param name="RemissionOutputId"></param>
    ''' <returns></returns>
    Public Function ListRemissionOutputDetailPhysicalByRemissionOutputId(RemissionOutputId As Integer) As List(Of RemissionOutputDetailPhysical) Implements IRemissionOutputDetailPhysicalRepository.ListRemissionOutputDetailPhysicalByRemissionOutputId
        Dim quantity As Integer = 0
        Dim status As Integer = 2
        Dim res = (From rodp In _context.RemissionOutputDetailPhysical
                   Join rod In _context.RemissionOutputDetail On rodp.RemissionOutputDetailId Equals rod.Id
                   Join ro In _context.RemissionOutput On rod.RemissionOutputId Equals ro.Id
                   Where ro.Status = status And ro.Id = RemissionOutputId And rodp.OutstandingQuantity > quantity Select rodp).ToList()
        For Each item In res
            Dim remissionDetail = (From rod In _context.RemissionOutputDetail.AsNoTracking() Where rod.Id = item.RemissionOutputDetailId Select rod).FirstOrDefault()
            Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = remissionDetail.ProductId Select p).FirstOrDefault()
            item.CodeNameProduct = product.Code + " - " + product.Name
            item.ProductId = product.Id
            item.QuantityDeliver = item.OutstandingQuantity
            Dim physicalInventory = (From pi In _context.PhysicalInventory.AsNoTracking() Where pi.Id = item.PhysicalInventoryId Select pi).FirstOrDefault()
            If physicalInventory.BatchSerialId IsNot Nothing Then
                item.CodeBatchSerial = (From bs In _context.BatchSerial.AsNoTracking() Where bs.Id = physicalInventory.BatchSerialId Select bs.BatchCode).FirstOrDefault()
            End If
        Next
        Return res
    End Function

    ''' <summary>
    ''' obtiene un detalle del detalle de la remision de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetRemissionOutputDetailPhysicalById(Id As Integer) As RemissionOutputDetailPhysical Implements IRemissionOutputDetailPhysicalRepository.GetRemissionOutputDetailPhysicalById
        Return (From rodp In _context.RemissionOutputDetailPhysical Where rodp.Id = Id Select rodp).FirstOrDefault()
    End Function
End Class
