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

Public Class CampaignRepository
    Inherits GenericRepository(Of Campaign)
    Implements ICampaignRepository, Inject

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
    Public Function GetCampaignById(id As String, Optional tracking As Boolean = True) As Campaign Implements ICampaignRepository.GetCampaignById
        Dim res = (From bg In _context.Campaign.Include("CampaignDetail") Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.Campaign.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New Campaign
        End If
    End Function

    ''' <summary>
    ''' Guarda los cronogramas
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveCampaign(xml As String, userCode As String) As SP_SaveCampaign_Result Implements ICampaignRepository.SP_SaveCampaign
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveCampaign(xml, userCode).SingleOrDefault
    End Function

    ''' <summary>
    ''' Anula los pacientes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_ProcessMixingStation(xml As String, userCode As String) As SP_ProcessMixingStation_Result Implements ICampaignRepository.SP_ProcessMixingStation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ProcessMixingStation(xml, userCode).SingleOrDefault
    End Function

End Class