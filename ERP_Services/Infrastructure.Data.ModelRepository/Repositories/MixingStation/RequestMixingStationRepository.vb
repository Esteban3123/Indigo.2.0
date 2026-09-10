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

Public Class RequestMixingStationRepository
    Inherits GenericRepository(Of RequestMixingStation)
    Implements IRequestMixingStationRepository, Inject

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
    Public Function GetRequestMixingStation(code As String, Optional tracking As Boolean = True) As RequestMixingStation Implements IRequestMixingStationRepository.GetRequestMixingStation
        Dim res = (From bg In _context.RequestMixingStation.Include("RequestMixingStationDetail") Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.RequestMixingStation.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New RequestMixingStation
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetRequestMixingStationById(id As String, Optional tracking As Boolean = True) As RequestMixingStation Implements IRequestMixingStationRepository.GetRequestMixingStationById
        Dim res = (From bg In _context.RequestMixingStation Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.RequestMixingStation.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New RequestMixingStation
        End If
    End Function

    ''' <summary>
    ''' Guarda las solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_ProcessMixingStation(xml As String, userCode As String) As SP_ProcessMixingStation_Result Implements IRequestMixingStationRepository.SP_ProcessMixingStation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ProcessMixingStation(xml, userCode).SingleOrDefault
    End Function

End Class