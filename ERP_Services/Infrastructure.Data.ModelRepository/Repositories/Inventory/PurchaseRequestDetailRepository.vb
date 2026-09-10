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

Public Class PurchaseRequestDetailRepository
    Inherits GenericRepository(Of PurchaseRequestDetail)
    Implements IPurchaseRequestDetailRepository

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
    ''' obtiene un listado de detalles de solicitudes de compra de inventario por unidad funcional
    ''' </summary>
    ''' <param name="idFunctionalUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPurchaseRequestDetailByFunctionalUnit(idFunctionalUnit As Integer) As List(Of PurchaseRequestDetail) Implements IPurchaseRequestDetailRepository.ListPurchaseRequestDetailByFunctionalUnit
        Dim status As Integer = 2
        Dim res As List(Of Domain.Entities.PurchaseRequestDetail) = New List(Of Domain.Entities.PurchaseRequestDetail)
        
            res = (From ird In _context.PurchaseRequestDetail
                Join ir In _context.PurchaseRequest On ird.PurchaseRequestId Equals ir.Id
                Where ir.FunctionalUnitId = idFunctionalUnit And ir.Status = status And ird.OutstandingQuantity > 0 Select ird).ToList()
        

        If res.Count > 0 Then
            For Each item In res
                Dim PurchaseRequest = (From re In _context.PurchaseRequest.AsNoTracking() Where re.Id = item.PurchaseRequestId Select re).FirstOrDefault()
                item.Code = PurchaseRequest.Code
                If item.InventoryProductId IsNot Nothing Then
                    Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.InventoryProductId Select p).FirstOrDefault()
                    item.DescriptionProduct = product.Code + " - " + product.Name
                    If product.ManufacturerId IsNot Nothing Then
                        item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where product.ManufacturerId = p.Id Select p.Name).FirstOrDefault()
                    End If
                    item.HealthRegistration = product.HealthRegistration
                    item.Presentation = product.Presentation
                ElseIf item.FixedAssetItemId IsNot Nothing Then
                    Dim fixedAsset = (From fa In _context.FixedAssetItem.AsNoTracking() Where fa.Id = item.FixedAssetItemId Select fa).FirstOrDefault()
                    item.DescriptionProduct = fixedAsset.Code + " - " + fixedAsset.Description
                Else
                    item.DescriptionProduct = item.OtherRequest
                End If
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un detalle de solicitud de compra de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPurchaseRequestDetailById(id As Integer) As PurchaseRequestDetail Implements IPurchaseRequestDetailRepository.GetPurchaseRequestDetailById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.PurchaseRequestDetail Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From ir In Me._context.PurchaseRequestDetail.AsNoTracking Where ir.Id = id Select ir).FirstOrDefault
            Return res
        Else
            Return New PurchaseRequestDetail
        End If

    End Function
End Class
