'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 08-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class InventoryContractDetailRepository
    Inherits GenericRepository(Of InventoryContractDetail)
    Implements IInventoryContractDetailRepository


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
    ''' obtiene los detalles del contrato por el id del proveedor  y la linea de distribucion
    ''' </summary>
    ''' <param name="supplierId"></param>
    ''' <param name="supplierDistributionLineId"></param>
    ''' <returns></returns>
    Public Function GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId(supplierId As Integer, supplierDistributionLineId As Integer, contractType As Integer) As List(Of InventoryContractDetail) Implements IInventoryContractDetailRepository.GetInventoryContractDetailBySupplierIdAndSupplierDistributionLineId
        Dim status As Integer = 2
        Dim outstandingQuantity = 0
        Dim res = (From icd In _context.InventoryContractDetail
                Join ic In _context.InventoryContract On icd.InventoryContractId Equals ic.Id
                Join ct In _context.InventoryContractType On ic.ContractTypeId Equals ct.Id
                Where ic.SupplierId = supplierId And ic.SupplierDistributionLineId = supplierDistributionLineId And ic.Status = status And ct.Type = contractType And icd.OutstandingQuantity > outstandingQuantity Select icd).ToList()
        If res.Count > 0 Then
            For Each item In res
                Dim inventoryContract = (From ic In _context.InventoryContract.AsNoTracking() Where ic.Id = item.InventoryContractId Select ic).FirstOrDefault()
                item.Code = inventoryContract.Code
                Dim product = (From p In _context.InventoryProduct.AsNoTracking() Where p.Id = item.ProductId Select p).FirstOrDefault()
                item.CodeNameProduct = product.Code + " - " + product.Name
            Next
        End If
        Return res
    End Function


    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetInventoryContractDetailById(id As Integer) As InventoryContractDetail Implements IInventoryContractDetailRepository.GetInventoryContractDetailById
        Return (From icd In _context.InventoryContractDetail Where icd.Id = id Select icd).FirstOrDefault()
    End Function
End Class
