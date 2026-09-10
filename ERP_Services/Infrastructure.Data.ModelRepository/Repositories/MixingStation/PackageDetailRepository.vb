'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 12/06/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class PackageDetailRepository
    Inherits GenericRepository(Of PackageDetail)
    Implements IPackageDetailRepository, Inject
    ''' <summary>
    ''' Contexto de PackageDetail
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de PackageDetail
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
    ''' <summary>
    ''' Lista todos los detalles del paquete
    ''' </summary>
    Public Function ListAllPackageDetail() As List(Of PackageDetail) Implements IPackageDetailRepository.ListAllPackageDetail
        Dim packageDetail = From e In _context.PackageDetail
                            Select e
        Return packageDetail.ToList()
    End Function

    ''' <summary>
    ''' Obtiene el detalle del paquete especifico
    ''' </summary>
    ''' <param name="packageId">Codigo del paquete</param>
    ''' <returns>Detalle del paquete</returns>
    ''' <remarks></remarks>
    Public Function GetPackageDetail(packageId As Integer, Optional tracking As Boolean = True) As PackageDetail Implements IPackageDetailRepository.GetPackageDetailByPackageId
        Dim packageDetail = From e In _context.PackageDetail.Include("InventoryProduct").Include("ATC").Include("InventorySupplie")
                            Where e.PackageId = packageId
                            Select e
        If packageDetail.Count > 0 Then
            Dim ObjPackage = Nothing
            If tracking = False Then
                ObjPackage = (From e In _context.PackageDetail.Include("InventoryProduct").Include("ATC").Include("InventorySupplie").AsNoTracking
                              Where e.PackageId = packageId
                              Select e).SingleOrDefault
            Else
                ObjPackage = packageDetail.SingleOrDefault
            End If
            Return ObjPackage
        Else
            Return New PackageDetail()
        End If
    End Function
End Class