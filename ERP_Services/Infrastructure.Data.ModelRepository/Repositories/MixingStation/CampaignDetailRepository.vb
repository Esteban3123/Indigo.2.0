'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Duvan Mejia
' Created          : 23/07/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class CampaignDetailRepository
    Inherits GenericRepository(Of CampaignDetail)
    Implements ICampaignDetailRepository, Inject

    ''' <summary>
    ''' Contexto de Configuración de Central de Mezclas
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Configuración de Central de Mezclas
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene el detalle de la campaña por Id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCampaignDetailById(id As Integer) As CampaignDetail Implements ICampaignDetailRepository.GetCampaignDetailById
        Dim res = (From bg In _context.CampaignDetail.Include("RequestMixingStationDetail") Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.CampaignDetail.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New CampaignDetail
        End If
    End Function

    ''' <summary>
    ''' Obtiene los detalle de la campaña por Ids
    ''' </summary>
    ''' <param name="ids"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetCampaignsDetailByIds(ids As List(Of Integer), Optional tracking As Boolean = True) As List(Of CampaignDetail) Implements ICampaignDetailRepository.GetCampaignsDetailByIds
        Dim res = (From bg In _context.CampaignDetail.AsNoTracking().Include("Campaign.CMConfiguration").AsNoTracking() Where ids.Contains(bg.Id) Select bg).ToList()
        If res IsNot Nothing Then
            Return res
        Else
            Return New List(Of CampaignDetail)
        End If
    End Function
End Class
