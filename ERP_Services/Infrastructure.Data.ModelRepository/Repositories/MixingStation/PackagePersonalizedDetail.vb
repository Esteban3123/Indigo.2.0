'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Duván Albeiro Mejia Cortes
' Created          : 16/09/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class PackagePersonalizedDetailRepository
    Inherits GenericRepository(Of PackagePersonalizedDetail)
    Implements IPackagePersonalizedDetailRepository, Inject
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
    Public Function ListAllPackageDetail() As List(Of PackagePersonalizedDetail) Implements IPackagePersonalizedDetailRepository.ListAllPackageDetail
        Dim packagePersonalizedDetail = From e In _context.PackagePersonalizedDetail
                                        Select e
        Return packagePersonalizedDetail.ToList()
    End Function

    ''' <summary>
    ''' Obtiene el detalle del paquete especifico
    ''' </summary>
    ''' <param name="packageId">Codigo del paquete</param>
    ''' <returns>Detalle del paquete</returns>
    ''' <remarks></remarks>
    Public Function GetPackageDetail(packageId As Integer, Optional tracking As Boolean = True) As PackagePersonalizedDetail Implements IPackagePersonalizedDetailRepository.GetPackageDetailByPackageId
        Dim packagePersonalizedDetail = From e In _context.PackagePersonalizedDetail
                                        Where e.PackagePersonalizedId = packageId
                                        Select e
        If packagePersonalizedDetail.Count > 0 Then
            Dim ObjPackage = Nothing
            If tracking = False Then
                ObjPackage = (From e In _context.PackagePersonalizedDetail.AsNoTracking
                              Where e.PackagePersonalizedId = packageId
                              Select e).SingleOrDefault
            Else
                ObjPackage = packagePersonalizedDetail.SingleOrDefault
            End If
            Return ObjPackage
        Else
            Return New PackagePersonalizedDetail()
        End If
    End Function

    ''' <summary>
    '''  Obienete la Lista de Detalles del Paquete
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <returns></returns>
    Public Function GetPackageDetailListByPackageId(packageId As Integer) As List(Of PackagePersonalizedDetail) Implements IPackagePersonalizedDetailRepository.GetPackageDetailListByPackageId
        Return (From e In _context.PackagePersonalizedDetail.Include("InventoryProduct").Include("ATC").Include("InventorySupplie")
                Where e.PackagePersonalizedId = packageId
                Select e).ToList()
    End Function

End Class