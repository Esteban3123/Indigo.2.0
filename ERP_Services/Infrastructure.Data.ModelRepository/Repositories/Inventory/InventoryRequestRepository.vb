'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 30-04-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Infrastructure.CrossCutting.Base

#End Region

Public Class InventoryRequestRepository
    Inherits GenericRepository(Of InventoryRequest)
    Implements IInventoryRequestRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obitene una solicitud de inventario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryRequestByCode(code As String) As InventoryRequest Implements IInventoryRequestRepository.GetInventoryRequestByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From ir In Me._context.InventoryRequest.Include("InventoryRequestDetail").Include("InventoryRequestDetailOther") Where ir.Code.Equals(code.Trim())
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            If res.InventoryRequestDetail IsNot Nothing AndAlso res.InventoryRequestDetail.Count > 0 Then
                For Each item As InventoryRequestDetail In res.InventoryRequestDetail

                    Dim product = (From p In Me._context.InventoryProduct.AsNoTracking() Where p.Id = item.InventoryProductId Select p).FirstOrDefault
                    item.DescriptionProduct = product.Code + " - " + product.Name

                    Dim packingUnit = (From pu In _context.PackagingUnit.AsNoTracking() Where pu.Id = product.PackagingUnitId Select pu).FirstOrDefault()
                    item.consumptionUnit = packingUnit.Code + " - " + packingUnit.Name

                    item.HealthRegistration = product.HealthRegistration
                    item.Presentation = product.Presentation

                    If product.ManufacturerId IsNot Nothing Then
                        item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where product.ManufacturerId = p.Id Select p.Name).FirstOrDefault()
                    End If

                Next
            End If

            If res.InventoryRequestDetailOther IsNot Nothing AndAlso res.InventoryRequestDetailOther.Count > 0 Then
                For Each item As InventoryRequestDetailOther In res.InventoryRequestDetailOther

                    item.ApproveQuantity = item.Quantity
                    item.Quantity = IIf(item.OriginalQuantity IsNot Nothing, item.OriginalQuantity, item.Quantity)

                    If item.ComponentType = 1 Then 'Medicines
                        Dim ATC = (From p In Me._context.ATC.AsNoTracking() Where p.Id = item.ATCId Select p).FirstOrDefault
                        item.SourceCodeName = ATC.Code + " - " + ATC.Name

                        Dim PackagingUnit = (From p In Me._context.InventoryProduct.AsNoTracking()
                                             Join pu In Me._context.PackagingUnit.AsNoTracking() On p.PackagingUnitId Equals pu.Id
                                             Where p.ATCId = item.ATCId
                                             Select pu).FirstOrDefault()

                        item.consumptionUnit = If(PackagingUnit IsNot Nothing, PackagingUnit.Code + " - " + PackagingUnit.Name, "")

                    ElseIf item.ComponentType = 2 Then 'Supplies
                        Dim Supplies = (From p In Me._context.InventorySupplie.AsNoTracking() Where p.Id = item.SupplieId Select p).FirstOrDefault
                        item.SourceCodeName = Supplies.Code + " - " + Supplies.SupplieName

                        Dim PackagingUnit = (From p In Me._context.InventoryProduct.AsNoTracking()
                                             Join pu In Me._context.PackagingUnit.AsNoTracking() On p.PackagingUnitId Equals pu.Id
                                             Where p.SupplieId = item.SupplieId
                                             Select pu).FirstOrDefault()

                        item.consumptionUnit = If(PackagingUnit IsNot Nothing, PackagingUnit.Code + " - " + PackagingUnit.Name, "")

                    Else 'Products
                        Dim product = (From p In Me._context.InventoryProduct.AsNoTracking() Where p.Id = item.InventoryProductId Select p).FirstOrDefault
                        item.SourceCodeName = product.Code + " - " + product.Name

                        Dim packingUnit = (From pu In _context.PackagingUnit.AsNoTracking() Where pu.Id = product.PackagingUnitId Select pu).FirstOrDefault()
                        item.consumptionUnit = packingUnit.Code + " - " + packingUnit.Name


                    End If

                    Select Case item.ComponentType
                        Case 1
                            item.ComponentTypeName = "Medicamento"
                        Case 2
                            item.ComponentTypeName = "Insumo"
                        Case 3
                            item.ComponentTypeName = "Producto"
                    End Select

                Next
            End If

            Dim warehouse As Warehouse

            If res.TargetFunctionalUnitId > 0 Then
                Dim FunctionalUnit = (From uf In Me._context.FunctionalUnit.AsNoTracking() Where uf.Id = res.TargetFunctionalUnitId Select uf).FirstOrDefault
                res.DescriptionFunctionalUnit = FunctionalUnit.Code + " - " + FunctionalUnit.Name
            End If
            If res.SourceWarehouseId > 0 Then
                warehouse = (From w In Me._context.Warehouse.AsNoTracking() Where w.Id = res.SourceWarehouseId Select w).FirstOrDefault
                res.DescriptionSourceWarehouse = warehouse.Code + " - " + warehouse.Name
                res.Prefix = warehouse.Prefix
            End If
            If res.TargetWarehouseId > 0 Then
                warehouse = (From w In Me._context.Warehouse.AsNoTracking() Where w.Id = res.TargetWarehouseId Select w).FirstOrDefault
                res.DescriptionTargetWarehouse = warehouse.Code + " - " + warehouse.Name
            End If
            res.OriginalValue = (From ir In Me._context.InventoryRequest.AsNoTracking Where ir.Code.Equals(code.Trim()) Select ir).FirstOrDefault

            Return res
        Else
            Return New InventoryRequest
        End If
    End Function

    ''' <summary>
    ''' Obtiene una solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryRequestById(id As Integer) As InventoryRequest Implements IInventoryRequestRepository.GetInventoryRequestById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.InventoryRequest.Include("InventoryRequestDetail").Include("InventoryRequestDetailOther") Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From ir In Me._context.InventoryRequest.AsNoTracking Where ir.Id = id Select ir).FirstOrDefault
            Return res
        Else
            Return New InventoryRequest
        End If

    End Function

    ''' <summary>
    ''' CopyPaste/Import solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Public Function SP_CopyPasteAndImportRequests(xml As String) As List(Of SP_CopyPasteAndImportRequests_Result) Implements IInventoryRequestRepository.SP_CopyPasteAndImportRequests
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyPasteAndImportRequests(xml).ToList()
    End Function


    ''' <summary>
    ''' CopyPaste/Import solicitudes medicamentos,insumos,otros
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Public Function SP_CopyPasteAndImportRequestsOtherDetail(xml As String) As List(Of SP_CopyPasteAndImportRequestsOtherDetail_Result) Implements IInventoryRequestRepository.SP_CopyPasteAndImportRequestsOtherDetail
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyPasteAndImportRequestsOtherDetail(xml).ToList()
    End Function


End Class
