'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity

Public Class ProductRateDetailRepository
    Inherits GenericRepository(Of ProductRateDetail)
    Implements IProductRateDetailRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene un detalle de la tarifa de productos por id del grupo de atención, id del producto y fecha de dispensación
    ''' </summary>
    Public Function GetProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId As Integer, ProductId As Integer, ServiceDate As Date) As ProductRateDetail Implements IProductRateDetailRepository.GetProductRateDetailByCareGroupIdProductIdServiceDate
        If CareGroupId = 0 Then
            Throw New ArgumentNullException("CareGroupId")
        End If

        Dim query = (From c As CareGroup In _context.CareGroup
                     Join p As ProductRate In _context.ProductRate On c.ProductRateId Equals p.Id
                     Join d As ProductRateDetail In _context.ProductRateDetail On d.ProductRateId Equals p.Id
                     Where c.Id = CareGroupId AndAlso d.ProductId = ProductId AndAlso d.Status = 1 AndAlso (d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate)
                     Select d)
        query = query.Include("InventoryProduct")
        Return query.FirstOrDefault
    End Function

    Public Function GetListProductRateDetailByCareGroupIdProductIdServiceDate(CareGroupId As Integer, ProductId As List(Of Integer), ServiceDate As Date) As List(Of ProductRateDetail) Implements IProductRateDetailRepository.GetListProductRateDetailByCareGroupIdProductIdServiceDate
        If CareGroupId = 0 Then
            Throw New ArgumentNullException("CareGroupId")
        End If

        Dim query = (From c As CareGroup In _context.CareGroup
                     Join p As ProductRate In _context.ProductRate On c.ProductRateId Equals p.Id
                     Join d As ProductRateDetail In _context.ProductRateDetail On d.ProductRateId Equals p.Id
                     Where c.Id = CareGroupId AndAlso ProductId.Contains(d.ProductId) AndAlso (d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate)
                     Select d)
        Return query.Include("InventoryProduct.GeneralLedgerIVA").ToList()

    End Function

    Public Function GetListProductRateDetailByCareGroupIdPackageServiceDate(CareGroupId As Integer, PackageIds As List(Of Integer), ServiceDate As Date) As List(Of ProductRateDetail) Implements IProductRateDetailRepository.GetListProductRateDetailByCareGroupIdPackageServiceDate
        If CareGroupId = 0 Then
            Throw New ArgumentNullException("CareGroupId")
        End If

        Dim query = (From c As CareGroup In _context.CareGroup
                     Join p As ProductRate In _context.ProductRate On c.ProductRateId Equals p.Id
                     Join d As ProductRateDetail In _context.ProductRateDetail On d.ProductRateId Equals p.Id
                     Where c.Id = CareGroupId AndAlso PackageIds.Contains(d.PackageId) AndAlso (d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate)
                     Select d)
        Return query.Include("ProductRateDetailPackage.PackageDetail.ATC").Include("ProductRateDetailPackage.InventoryProduct").ToList()

    End Function

    ''' <summary>
    ''' Obtiene el PackageId asociado a un producto terminado consultando RequestPackageDetailStatus
    ''' </summary>
    Public Function GetPackageIdByProductId(productId As Integer) As Integer? Implements IProductRateDetailRepository.GetPackageIdByProductId
        Return (From r In _context.RequestPackageDetailStatus.AsNoTracking()
                Where r.ProductId = productId AndAlso r.PackageId IsNot Nothing
                Select r.PackageId).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Consulta la definicion de tarifa en el contrato de centro de atencion externo
    ''' </summary>
    ''' <param name="ContractExternalClientId"></param>
    ''' <param name="ProductId"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    Public Function GetListProductRateDetailByContractExternalClientIdProductIdServiceDate(ContractExternalClientId As Integer, ProductId As Integer, ServiceDate As Date) As ProductRateDetail Implements IProductRateDetailRepository.GetListProductRateDetailByContractExternalClientIdProductIdServiceDate
        If ContractExternalClientId = 0 Then
            Throw New ArgumentNullException("ContractExternalClientId")
        End If

        Dim query = (From c As ContractExternalClients In _context.ContractExternalClients.AsNoTracking()
                     Join p As ProductRate In _context.ProductRate.AsNoTracking() On c.ProductRateId Equals p.Id
                     Join d As ProductRateDetail In _context.ProductRateDetail.AsNoTracking() On d.ProductRateId Equals p.Id
                     Where c.Id = ContractExternalClientId AndAlso d.ProductId = ProductId AndAlso (d.InitialDate <= ServiceDate AndAlso d.EndDate >= ServiceDate)
                     Select d)
        query = query.Include("InventoryProduct")
        Return query.FirstOrDefault()
    End Function
#End Region

End Class