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

Public Class RequestUnitDoseInventoryRepository
    Inherits GenericRepository(Of RequestUnitDoseInventory)
    Implements IRequestUnitDoseInventoryRepository, Inject

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
    Public Function GetRequestUnitDoseInventory(code As String, Optional tracking As Boolean = True) As RequestUnitDoseInventory Implements IRequestUnitDoseInventoryRepository.GetRequestUnitDoseInventory
        Dim res = (From bg In _context.RequestUnitDoseInventory Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.CMConfigurationCodeName = (From x In _context.CMConfiguration.AsNoTracking Where x.Id = res.CMConfigurationId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault
            res.WarehouseCodeName = (From x In _context.Warehouse.AsNoTracking Where x.Id = res.WarehouseId Select String.Concat(x.Code, " - ", x.Name)).FirstOrDefault

            res.OriginalValue = (From bg In _context.RequestUnitDoseInventory.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New RequestUnitDoseInventory
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetRequestUnitDoseInventoryById(id As String, Optional tracking As Boolean = True) As RequestUnitDoseInventory Implements IRequestUnitDoseInventoryRepository.GetRequestUnitDoseInventoryById
        Dim res = (From bg In _context.RequestUnitDoseInventory Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.RequestUnitDoseInventory.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New RequestUnitDoseInventory
        End If
    End Function

    ''' <summary>
    ''' Guarda las solicitudes
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveRequestUnitDoseInventory(xml As String, userCode As String) As SP_SaveRequestUnitDoseInventory_Result Implements IRequestUnitDoseInventoryRepository.SP_SaveRequestUnitDoseInventory
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveRequestUnitDoseInventory(xml, userCode).SingleOrDefault
    End Function

End Class