'***********************************************************************
' Assembly         : Infrastructure.Data.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity
Imports System.Linq.Dynamic.Core
#End Region

Public Class ServiceOrderDetailDistributionRepository
    Inherits GenericRepository(Of ServiceOrderDetailDistribution)
    Implements IServiceOrderDetailDistributionRepository

    'Contexto de payrollGetServiceOrderDetailDistributionByServideOrderDetailId
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    ''' <summary>
    ''' lista el total de items por el id del detalle de la tabla de control
    ''' </summary>
    ''' <param name="RevenueControlDetailId"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailDistributionByRevenueControlDetailId(RevenueControlDetailId As Integer) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByRevenueControlDetailId
        Return (From sodd In _context.ServiceOrderDetailDistribution.AsNoTracking() Where sodd.RevenueControlDetailId = RevenueControlDetailId Select sodd).ToList()
    End Function

    ''' <summary>
    ''' metodo para obtener un detalle de la distribucion
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId As Integer) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByServideOrderDetailId
        Dim result = (From sodd In _context.ServiceOrderDetailDistribution Where sodd.ServiceOrderDetailId = ServiceOrderDetailId Select sodd).ToList()
        If result IsNot Nothing AndAlso result.Count > 0 Then
            Return result
        Else
            Return New List(Of ServiceOrderDetailDistribution)()
        End If
    End Function

    Public Function GetServiceOrderDetailDistributionByIdWithIncludes(id As Integer, Optional includes() As String = Nothing) As ServiceOrderDetailDistribution Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByIdWithIncludes
        Dim query As IQueryable(Of ServiceOrderDetailDistribution) = _context.ServiceOrderDetailDistribution.Where(Function(rcd) rcd.Id = id).AsQueryable()
        If includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(include)
                                          query = query.Include(include)
                                      End Sub)
        End If
        Return query.FirstOrDefault()
    End Function

    Public Sub RemoveRange(servorderDetailDistributionList As List(Of ServiceOrderDetailDistribution)) Implements IServiceOrderDetailDistributionRepository.RemoveRange
        _context.ServiceOrderDetailDistribution.RemoveRange(servorderDetailDistributionList)
    End Sub

    Public Function GetServiceOrderDetailDistributionByRevenueControlDetailIdAndCodeAssociateService(folioId As Integer, codeassociate As String) As ServiceOrderDetailDistribution Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByRevenueControlDetailIdAndCodeAssociateService
        Return _context.ServiceOrderDetailDistribution.Include("ServiceOrderDetail").Where(Function(x) x.RevenueControlDetailId = folioId AndAlso x.ServiceOrderDetail.CodeAssociateService = codeassociate).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Gets the service order detail distribution query.
    ''' </summary>
    ''' <param name="where">The where.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Public Function GetServiceOrderDetailDistributionQuery(where As String, Optional includes() As String = Nothing) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionQuery
        Dim query As IQueryable(Of ServiceOrderDetailDistribution) = _context.ServiceOrderDetailDistribution.AsQueryable()
        If includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(include)
                                          query = query.Include(include)
                                      End Sub)
        End If
        If Not where.Equals(String.Empty) Then
            query = query.Where(where)
        End If
        Return query.ToList()
    End Function

    Public Function GetServiceOrderDetailDistributionListByIds(listId As List(Of Integer), Optional includes() As String = Nothing) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionListByIds
        Dim query As IQueryable(Of ServiceOrderDetailDistribution) = _context.ServiceOrderDetailDistribution.AsQueryable()
        If includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(include)
                                          query = query.Include(include)
                                      End Sub)
        End If
        Return query.Where(Function(o) listId.Contains(o.Id)).ToList()
    End Function

    Public Function GetServiceOrderDetailDistributionByServiceOrderDetailListByIds(serviceOrderDetailListId As List(Of Integer), Optional includes() As String = Nothing) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionByServiceOrderDetailListByIds
        Dim query As IQueryable(Of ServiceOrderDetailDistribution) = _context.ServiceOrderDetailDistribution.Where(Function(o) serviceOrderDetailListId.Contains(o.ServiceOrderDetailId)).AsQueryable()
        If includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(include)
                                          query = query.Include(include)
                                      End Sub)
        End If
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Obtiene una distribución de servicio por su id
    ''' </summary>
    ''' <param name="ServiceOrderDetailId">Id de la distribución</param>
    ''' <returns>La distribución consultada</returns>
    Public Function GetServiceOrderDetailDistributionById(ServiceOrderDetailId As Integer, Optional tracking As Boolean = True) As ServiceOrderDetailDistribution Implements IServiceOrderDetailDistributionRepository.GetServiceOrderDetailDistributionById
        If tracking Then
            Return (From serv As ServiceOrderDetailDistribution In Me._context.ServiceOrderDetailDistribution.Include("ServiceOrderDetail").Include("ServiceOrderDetail.IPSService") Where serv.Id = ServiceOrderDetailId Select serv).FirstOrDefault()
        Else
            Return (From serv As ServiceOrderDetailDistribution In Me._context.ServiceOrderDetailDistribution Where serv.Id = ServiceOrderDetailId Select serv).FirstOrDefault()
        End If
    End Function

    ''' <summary>
    ''' Counts the service order detail distribution by revenue control detail identifier.
    ''' </summary>
    ''' <param name="revenueControlDetailId">The revenue control detail identifier.</param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailDistributionIdByRevenueControlDetailId(revenueControlDetailId As Integer) As List(Of Integer) Implements IServiceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionIdByRevenueControlDetailId
        Return _context.ServiceOrderDetailDistribution.Where(Function(o) o.RevenueControlDetailId = revenueControlDetailId AndAlso o.ApplyRecoveryFee >= 1).OrderBy(Function(x) x.GrandTotalSalesPrice).Select(Function(x) x.Id).ToList()
    End Function

    ''' <summary>
    ''' Lists the service order detail distribution by revenue control detail and last care group distinct.
    ''' </summary>
    ''' <param name="revenueControlDetail">The revenue control detail.</param>
    ''' <param name="lastCaregroup">The last caregroup.</param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailDistributionByRevenueControlDetailAndLastCareGroupDistinct(revenueControlDetail As Integer, lastCaregroup As Integer) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionByRevenueControlDetailAndLastCareGroupDistinct
        Dim distributions = (From sodd In _context.ServiceOrderDetailDistribution.Include("ServiceOrderDetail").
                             Include("ServiceOrderDetail.ServiceOrderDetailSurgical")
                             Where sodd.RevenueControlDetailId = revenueControlDetail AndAlso sodd.LastCaregroupId <> lastCaregroup AndAlso sodd.ServiceOrderDetail.IsDelete = False AndAlso sodd.DistributionType <> 5).ToList()
        If distributions IsNot Nothing Then
            For Each d In distributions
                If d.ServiceOrderDetail.CUPSEntityId IsNot Nothing Then
                    d.ServiceOrderDetail.CodeNameCups = (From c In _context.CUPSEntity.AsNoTracking() Where c.Id = d.ServiceOrderDetail.CUPSEntityId Select String.Concat(c.Code, " - ", c.Description)).FirstOrDefault()
                End If
                If d.ServiceOrderDetail.IPSServiceId IsNot Nothing Then
                    d.ServiceOrderDetail.CodeNameIpsService = (From c In _context.IPSService.AsNoTracking() Where c.Id = d.ServiceOrderDetail.IPSServiceId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
                End If
                If d.ServiceOrderDetail.PerformsFunctionalUnitId <> 0 Then
                    d.ServiceOrderDetail.CodeNameFunctionalUnit = (From c In _context.FunctionalUnit.AsNoTracking() Where c.Id = d.ServiceOrderDetail.PerformsFunctionalUnitId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
                End If
                If d.ServiceOrderDetail.ThirdPartyId IsNot Nothing Then
                    d.ServiceOrderDetail.CodeNameHealthAdministrator = (From c In _context.ThirdParty.AsNoTracking() Where c.Id = d.ServiceOrderDetail.ThirdPartyId Select String.Concat(c.Nit, " - ", c.Name)).FirstOrDefault()
                End If
            Next
        End If
        Return distributions
    End Function

    Public Function ListServiceOrderDetailDistributionByRevenueControlDetailAndLastCareGroupWithIncludes(revenueControlDetail As Integer, lastCaregroup As Integer, includes() As String) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionByRevenueControlDetailAndLastCareGroupWithIncludes
        Dim query As IQueryable(Of ServiceOrderDetailDistribution) = (From sodd In _context.ServiceOrderDetailDistribution
                                                                      Where sodd.RevenueControlDetailId = revenueControlDetail AndAlso sodd.LastCaregroupId <> lastCaregroup _
                                                                      AndAlso sodd.ServiceOrderDetail.IsDelete = False AndAlso sodd.DistributionType <> 5).AsQueryable()
        If query IsNot Nothing AndAlso includes IsNot Nothing Then
            includes.ToList().ForEach(Sub(i)
                                          query = query.Include(i)
                                      End Sub)
        End If
        Return query.ToList()
    End Function

    ''' <summary>
    ''' Lists the service order detail distribution by service order detail list identifier.
    ''' </summary>
    ''' <param name="serviceOrderDetailToExclude">The service order detail to exclude.</param>
    ''' <returns></returns>
    Public Function ListServiceOrderDetailDistributionByServiceOrderDetailListId(serviceOrderDetailToExclude As List(Of Integer)) As List(Of ServiceOrderDetailDistribution) Implements IServiceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionByServiceOrderDetailListId
        Return (From sodd In _context.ServiceOrderDetailDistribution.Include("ServiceOrderDetail").Include("ServiceOrderDetail.ServiceOrderDetailSurgical") Where serviceOrderDetailToExclude.Contains(sodd.ServiceOrderDetailId) Select sodd).ToList()
    End Function

    Public Function ListServiceOrderDetailDistributionIdByRevenueControlDetailIdToRemove(revenueControlDetailId As Integer) As List(Of Integer) Implements IServiceOrderDetailDistributionRepository.ListServiceOrderDetailDistributionIdByRevenueControlDetailIdToRemove
        Return _context.ServiceOrderDetailDistribution.Where(Function(o) o.RevenueControlDetailId = revenueControlDetailId AndAlso o.ApplyRecoveryFee = 2).OrderBy(Function(x) x.GrandTotalSalesPrice).Select(Function(x) x.Id).ToList()
    End Function
End Class
