'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base
Imports System.Linq
Imports System.Linq.Dynamic.Core
Imports System.Data.Entity

Public Class RequestMixingStationDetailRepository
    Inherits GenericRepository(Of RequestMixingStationDetail)
    Implements IRequestMixingStationDetailRepository, Inject

    ''' <summary>
    ''' Contexto de Tipo de dosis unitaria
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Tipo de Dosis Unitaria
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetRequestMixingStationDetailById(id As String, Optional tracking As Boolean = True) As RequestMixingStationDetail Implements IRequestMixingStationDetailRepository.GetRequestMixingStationDetailById
        Dim res = (From bg In _context.RequestMixingStationDetail _
                       .Include("PackagePersonalized.PackagePersonalizedDetail.ATC") _
                       .Include("PackagePersonalized.PackagePersonalizedDetail.InventorySupplie") _
                       .Include("PackagePersonalized.PackagePersonalizedDetail.InventoryProduct") _
                       .Include("Package.PackageDetail.InventoryProduct") _
                       .Include("Package.PackageDetail.ATC") _
                       .Include("Package.PackageDetail.InventorySupplie") _
                       .Include("UnitDoseType")
                   Where bg.Id = id Select bg).FirstOrDefault()

        Return res
    End Function


    Public Function GetRequestMixingStationDetailByIdAsNoTracking(id As Integer) As RequestMixingStationDetail Implements IRequestMixingStationDetailRepository.GetRequestMixingStationDetailByIdAsNoTracking
        Dim res = (From bg In _context.RequestMixingStationDetail.AsNoTracking() _
                       .Include("PackagePersonalized.PackagePersonalizedDetail.ATC").AsNoTracking() _
                       .Include("PackagePersonalized.PackagePersonalizedDetail.InventorySupplie").AsNoTracking() _
                       .Include("PackagePersonalized.PackagePersonalizedDetail.InventoryProduct").AsNoTracking() _
                       .Include("Package.PackageDetail.InventoryProduct").AsNoTracking() _
                       .Include("Package.PackageDetail.ATC").AsNoTracking() _
                       .Include("Package.PackageDetail.InventorySupplie").AsNoTracking() _
                       .Include("UnitDoseType").AsNoTracking().Include("ATC").AsNoTracking()
                   Where bg.Id = id Select bg).FirstOrDefault()

        Return res
    End Function

    ''' <summary>
    ''' obtiene las solicitudes por el id del detalle de la campaña
    ''' </summary>
    ''' <param name="CampaignDetailId"></param>
    ''' <returns></returns>
    Public Function GetRequestMixingStationDetailByCampaignDetailId(CampaignDetailId As List(Of Integer)) As List(Of RequestMixingStationDetail) Implements IRequestMixingStationDetailRepository.GetRequestMixingStationDetailByCampaignDetailId
        Dim res = (From bg In _context.RequestMixingStationDetail.AsNoTracking().Include("UnitDoseType").Include("RequestMixingStation").AsNoTracking()
                   Where CampaignDetailId.Contains(bg.CampaignDetailId) AndAlso bg.Status <> 3
                   Select bg).ToList()
        If res IsNot Nothing Then
            Return res
        Else
            Return New List(Of RequestMixingStationDetail)
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de las Solicitudes por Ids
    ''' </summary>
    ''' <param name="ListIds"></param>
    ''' <returns></returns>
    Public Function GetRequestMixingStationDetailByListIds(ListIds As List(Of Integer)) As List(Of RequestMixingStationDetail) Implements IRequestMixingStationDetailRepository.GetRequestMixingStationDetailByListIds
        Dim res = (From bg In _context.RequestMixingStationDetail.AsNoTracking() Where ListIds.Contains(bg.Id) Select bg).ToList()
        If res IsNot Nothing Then
            Return res
        Else
            Return New List(Of RequestMixingStationDetail)
        End If
    End Function


End Class