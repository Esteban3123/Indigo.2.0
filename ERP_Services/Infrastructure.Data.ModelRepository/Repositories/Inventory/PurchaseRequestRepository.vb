'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Hector Rodriguez R
' Created          : 11-04-2019
'
' Copyright        : (c) . All rights reserved.
' About            : PBI3499
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class PurchaseRequestRepository
    Inherits GenericRepository(Of PurchaseRequest)
    Implements IPurchaseRequestRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obitene una solicitud de compra de inventario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPurchaseRequestByCode(code As String) As PurchaseRequest Implements IPurchaseRequestRepository.GetPurchaseRequestByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From ir In Me._context.PurchaseRequest.Include("PurchaseRequestDetail") Where ir.Code.Equals(code.Trim())
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            If res.PurchaseRequestDetail IsNot Nothing AndAlso res.PurchaseRequestDetail.Count > 0 Then
                For Each item As PurchaseRequestDetail In res.PurchaseRequestDetail.Where(Function(d) d.InventoryProductId IsNot Nothing AndAlso d.InventoryProductId.GetValueOrDefault() > 0)
                    Dim product = (From p In Me._context.InventoryProduct.AsNoTracking() Where p.Id = item.InventoryProductId Select p).FirstOrDefault
                    item.DescriptionProduct = product.Code + " - " + product.Name
                    If product.MeasurementUnitId IsNot Nothing AndAlso product.MeasurementUnitId.GetValueOrDefault() > 0 Then
                        Dim measurementUnit = (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = product.MeasurementUnitId Select mu).FirstOrDefault()
                        item.consumptionUnit = measurementUnit.Code + " - " + measurementUnit.Name
                    End If

                    If product.ManufacturerId IsNot Nothing Then
                        item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where product.ManufacturerId = p.Id Select p.Name).FirstOrDefault()
                    End If
                    item.HealthRegistration = product.HealthRegistration
                    item.Presentation = product.Presentation

                    item.InventoryProduct = product
                    item.StatusName = IIf(item.Status = 0, "Registrado", IIf(item.Status = 1, "Aprobado", "Rechazado"))
                Next
                For Each item As PurchaseRequestDetail In res.PurchaseRequestDetail.Where(Function(d) d.FixedAssetItemId IsNot Nothing AndAlso d.FixedAssetItemId.GetValueOrDefault() > 0)
                    Dim fixedAsset = (From fa In Me._context.FixedAssetItem.AsNoTracking() Where fa.Id = item.FixedAssetItemId Select fa).FirstOrDefault
                    item.Code = fixedAsset.Code
                    item.Observation = fixedAsset.Description
                    item.DescriptionProduct = fixedAsset.Code + " - " + fixedAsset.Description
                    Dim trademark = (From fat In _context.FixedAssetTrademark.AsNoTracking() Where fat.Id = item.TrademarkId Select fat).FirstOrDefault()
                    item.consumptionUnit = trademark.Code + " - " + trademark.Name + " - " + item.Model
                    'item.FixedAssetItem = fixedAsset
                    item.StatusName = IIf(item.Status = 0, "Registrado", IIf(item.Status = 1, "Aprobado", "Rechazado"))
                Next
                For Each item As PurchaseRequestDetail In res.PurchaseRequestDetail.Where(Function(d) d.InventoryProductId Is Nothing AndAlso d.FixedAssetItemId Is Nothing)
                    item.DescriptionProduct = item.OtherRequest
                    Dim measurementUnit = (From mu In _context.InventoryMeasurementUnit.AsNoTracking() Where mu.Id = item.MeasurementUnitId Select mu).FirstOrDefault()
                    item.consumptionUnit = measurementUnit.Code + " - " + measurementUnit.Name
                    item.StatusName = IIf(item.Status = 0, "Registrado", IIf(item.Status = 1, "Aprobado", "Rechazado"))
                Next
            End If

            If res.FunctionalUnitId > 0 Then
                Dim FunctionalUnit = (From uf In Me._context.FunctionalUnit.AsNoTracking() Where uf.Id = res.FunctionalUnitId Select uf).FirstOrDefault
                res.DescriptionFunctionalUnit = FunctionalUnit.Code + " - " + FunctionalUnit.Name
            End If
            res.OriginalValue = (From ir In Me._context.PurchaseRequest.AsNoTracking Where ir.Code.Equals(code.Trim()) Select ir).FirstOrDefault

            Return res
        Else
            Return New PurchaseRequest
        End If
    End Function

    ''' <summary>
    ''' Obtiene una solicitud de compra de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPurchaseRequestById(id As Integer) As PurchaseRequest Implements IPurchaseRequestRepository.GetPurchaseRequestById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.PurchaseRequest.Include("PurchaseRequestDetail") Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From ir In Me._context.PurchaseRequest.AsNoTracking Where ir.Id = id Select ir).FirstOrDefault
            Return res
        Else
            Return New PurchaseRequest
        End If

    End Function
End Class
