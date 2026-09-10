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

Public Class RequestUnitDoseExternalCareCenterRepository
    Inherits GenericRepository(Of RequestUnitDoseExternalCareCenter)
    Implements IRequestUnitDoseExternalCareCenterRepository, Inject

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
    Public Function GetRequestUnitDoseExternalCareCenter(code As String, Optional tracking As Boolean = True) As RequestUnitDoseExternalCareCenter Implements IRequestUnitDoseExternalCareCenterRepository.GetRequestUnitDoseExternalCareCenter
        Dim res = (From bg In _context.RequestUnitDoseExternalCareCenter Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.CMConfigurationCodeName = (From x In _context.CMConfiguration.AsNoTracking Where x.Id = res.CMConfigurationId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault
            res.ExternalCareCenterCodeName = (From x In _context.ExternalCareCenter.AsNoTracking Where x.Id = res.ExternalCareCenterId Select String.Concat(x.Code, " - ", x.Description)).FirstOrDefault
            res.NumberContractExternalClients = (From x In _context.ContractExternalClients.AsNoTracking() Where x.Id = res.ContractExternalClientsId Select x.ContractNumber).FirstOrDefault()

            res.OriginalValue = (From bg In _context.RequestUnitDoseExternalCareCenter.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New RequestUnitDoseExternalCareCenter
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetRequestUnitDoseExternalCareCenterById(id As String, Optional tracking As Boolean = True) As RequestUnitDoseExternalCareCenter Implements IRequestUnitDoseExternalCareCenterRepository.GetRequestUnitDoseExternalCareCenterById
        Dim res = (From bg In _context.RequestUnitDoseExternalCareCenter Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.RequestUnitDoseExternalCareCenter.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New RequestUnitDoseExternalCareCenter
        End If
    End Function

    ''' <summary>
    ''' Guarda las solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveRequestUnitDoseExternalCareCenter(xml As String, userCode As String) As SP_SaveRequestUnitDoseExternalCareCenter_Result Implements IRequestUnitDoseExternalCareCenterRepository.SP_SaveRequestUnitDoseExternalCareCenter
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveRequestUnitDoseExternalCareCenter(xml, userCode).SingleOrDefault
    End Function

End Class