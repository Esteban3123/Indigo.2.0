'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Base
#End Region


Public Interface IServiceOrderDetailDistributionRepository
    Inherits IRepository(Of ServiceOrderDetailDistribution)

    Function ListServiceOrderDetailDistributionByRevenueControlDetailAndLastCareGroupWithIncludes(revenueControlDetail As Integer, lastCaregroup As Integer, includes() As String) As List(Of ServiceOrderDetailDistribution)

    Function GetServiceOrderDetailDistributionByServiceOrderDetailListByIds(serviceOrderDetailListId As List(Of Integer), Optional includes() As String = Nothing) As List(Of ServiceOrderDetailDistribution)

    Function GetServiceOrderDetailDistributionByRevenueControlDetailIdAndCodeAssociateService(folioId As Integer, codeassociate As String) As ServiceOrderDetailDistribution

    ''' <summary>
    ''' Removes the range.
    ''' </summary>
    ''' <param name="servorderDetailDistributionList">The servorder detail distribution list.</param>
    Sub RemoveRange(servorderDetailDistributionList As List(Of ServiceOrderDetailDistribution))

    ''' <summary>
    ''' Gets the service order detail distribution query.
    ''' </summary>
    ''' <param name="where">The where.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailDistributionQuery(where As String, Optional includes() As String = Nothing) As List(Of ServiceOrderDetailDistribution)

    ''' <summary>
    ''' Gets the service order detail distribution by identifier with includes.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailDistributionByIdWithIncludes(id As Integer, Optional includes() As String = Nothing) As ServiceOrderDetailDistribution

    ''' <summary>
    ''' Gets the service order detail distribution list by ids.
    ''' </summary>
    ''' <param name="listId">The list identifier.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Function GetServiceOrderDetailDistributionListByIds(listId As List(Of Integer), Optional includes() As String = Nothing) As List(Of ServiceOrderDetailDistribution)

    ''' <summary>
    ''' metodo para obtener un detalle de la distribucion
    ''' </summary>
    ''' <param name="ServiceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderDetailDistributionByServideOrderDetailId(ServiceOrderDetailId As Integer) As List(Of ServiceOrderDetailDistribution)
    ''' <summary>
    ''' lista el total de items por el id del detalle de la tabla de control
    ''' </summary>
    ''' <param name="RevenueControlDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServiceOrderDetailDistributionByRevenueControlDetailId(RevenueControlDetailId As Integer) As List(Of ServiceOrderDetailDistribution)

    ''' <summary>
    ''' Obtiene una distribución de servicio por su id
    ''' </summary>
    ''' <param name="ServiceOrderDetailId">Id de la distribución</param>
    ''' <returns>La distribución consultada</returns>
    Function GetServiceOrderDetailDistributionById(ServiceOrderDetailId As Integer, Optional tracking As Boolean = True) As ServiceOrderDetailDistribution

    ''' <summary>
    ''' Counts the service order detail distribution by revenue control detail identifier.
    ''' </summary>
    ''' <param name="revenueControlDetailId">The revenue control detail identifier.</param>
    ''' <returns></returns>
    Function ListServiceOrderDetailDistributionIdByRevenueControlDetailId(revenueControlDetailId As Integer) As List(Of Integer)

    ''' <summary>
    ''' Lists the service order detail distribution by revenue control detail and last care group distinct.
    ''' </summary>
    ''' <param name="revenueControlDetail">The revenue control detail.</param>
    ''' <param name="lastCaregroup">The last caregroup.</param>
    ''' <returns></returns>
    Function ListServiceOrderDetailDistributionByRevenueControlDetailAndLastCareGroupDistinct(revenueControlDetail As Integer, lastCaregroup As Integer) As List(Of ServiceOrderDetailDistribution)

    ''' <summary>
    ''' Lists the service order detail distribution by service order detail list identifier.
    ''' </summary>
    ''' <param name="serviceOrderDetailToExclude">The service order detail to exclude.</param>
    ''' <returns></returns>
    Function ListServiceOrderDetailDistributionByServiceOrderDetailListId(serviceOrderDetailToExclude As List(Of Integer)) As List(Of ServiceOrderDetailDistribution)

    Function ListServiceOrderDetailDistributionIdByRevenueControlDetailIdToRemove(revenueControlDetailId As Integer) As List(Of Integer)


End Interface
