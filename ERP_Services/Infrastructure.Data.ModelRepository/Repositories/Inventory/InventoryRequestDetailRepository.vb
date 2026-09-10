'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Juan Carlos Bermudez
' Created          : 14-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class InventoryRequestDetailRepository
    Inherits GenericRepository(Of InventoryRequestDetail)
    Implements IInventoryRequestDetailRepository

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
    ''' obtiene un listado de detalles de solicitudes de inventario por unidad funcional o almacen de destino
    ''' </summary>
    ''' <param name="idFilter"></param>
    ''' <param name="orderType"></param>
    ''' <param name="dispatchTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInventoryRequestDetailByTarget(idFilter As Integer, orderType As Integer, dispatchTo As Integer) As List(Of InventoryRequestDetail) Implements IInventoryRequestDetailRepository.ListInventoryRequestDetailByTarget
        Dim status As Integer = 2
        Dim res As List(Of Domain.Entities.InventoryRequestDetail) = New List(Of Domain.Entities.InventoryRequestDetail)
        If orderType = 1 Or orderType = 2 AndAlso dispatchTo = 1 Then
            res = (From ird In _context.InventoryRequestDetail
                Join ir In _context.InventoryRequest On ird.InventoryRequestId Equals ir.Id
                Where ir.TargetWarehouseId = idFilter And ir.Status = status And ird.OutstandingQuantity > 0 Select ird).ToList()
        ElseIf orderType = 2 AndAlso dispatchTo = 2 Then
            res = (From ird In _context.InventoryRequestDetail
                Join ir In _context.InventoryRequest On ird.InventoryRequestId Equals ir.Id
                Where ir.TargetFunctionalUnitId = idFilter And ir.Status = status And ird.OutstandingQuantity > 0 Select ird).ToList()
        End If

        If res.Count > 0 Then
            For Each item In res
                Dim inventoryRequest = (From re In _context.InventoryRequest.AsNoTracking() Where re.Id = item.InventoryRequestId Select re).FirstOrDefault()
                item.Code = inventoryRequest.Code
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.InventoryProductId Select p).FirstOrDefault()
                item.DescriptionProduct = product.Code + " - " + product.Name
                If product.ManufacturerId IsNot Nothing Then
                    item.ManufacturerName = (From p In _context.Manufacturer.AsNoTracking() Where product.ManufacturerId = p.Id Select p.Name).FirstOrDefault()
                End If
                item.HealthRegistration = product.HealthRegistration
                item.Presentation = product.Presentation
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' Obtiene un detalle de solicitud de inventario por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryRequestDetailById(id As Integer) As InventoryRequestDetail Implements IInventoryRequestDetailRepository.GetInventoryRequestDetailById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From ir In Me._context.InventoryRequestDetail Where ir.Id = id
                   Select ir).FirstOrDefault

        If res IsNot Nothing Then
            res.OriginalValue = (From ir In Me._context.InventoryRequestDetail.AsNoTracking Where ir.Id = id Select ir).FirstOrDefault
            Return res
        Else
            Return New InventoryRequestDetail
        End If

    End Function
End Class
