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

Public Class ProductionScheduleRepository
    Inherits GenericRepository(Of ProductionSchedule)
    Implements IProductionScheduleRepository, Inject

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
    ''' Obtiene un registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetProductionSchedule(code As String, Optional tracking As Boolean = True) As ProductionSchedule Implements IProductionScheduleRepository.GetProductionSchedule
        Dim res = (From bg In _context.ProductionSchedule Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.ProductionSchedule.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New ProductionSchedule
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetProductionScheduleById(id As String, Optional tracking As Boolean = True) As ProductionSchedule Implements IProductionScheduleRepository.GetProductionScheduleById
        Dim res = (From bg In _context.ProductionSchedule Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.ProductionSchedule.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New ProductionSchedule
        End If
    End Function

    ''' <summary>
    ''' Guarda los cronogramas
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveProductionSchedule(xml As String, userCode As String) As SP_SaveProductionSchedule_Result Implements IProductionScheduleRepository.SP_SaveProductionSchedule
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveProductionSchedule(xml, userCode).SingleOrDefault
    End Function

    ''' <summary>
    ''' Consulta los paquetes por CampaignDetailId
    ''' </summary>
    ''' <param name="campaignDetailId">Id de la Campaña</param>
    ''' <returns></returns>
    Public Function SP_ListViewItemsCampaigns(campaignDetailId As Integer) As List(Of SP_ListViewItemsCampaigns_Result) Implements IProductionScheduleRepository.SP_ListViewItemsCampaigns
        Return (From ls In _context.SP_ListViewItemsCampaigns(campaignDetailId)).ToList()
    End Function

End Class