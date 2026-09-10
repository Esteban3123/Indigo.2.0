'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Judy Andrea Díaz Reyes
' Created          : 21/05/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base

Public Class StabilityTableRepository
    Inherits GenericRepository(Of StabilityTable)
    Implements IStabilityTableRepository, Inject

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
    Public Function GetStabilityTable(code As String, Optional tracking As Boolean = True) As StabilityTable Implements IStabilityTableRepository.GetStabilityTable
        Dim res = (From bg In _context.StabilityTable Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.StabilityTable.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New StabilityTable
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetStabilityTableById(id As String, Optional tracking As Boolean = True) As StabilityTable Implements IStabilityTableRepository.GetStabilityTableById
        Dim res = (From bg In _context.StabilityTable Where bg.Id = id Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.StabilityTable.AsNoTracking() Where bg.Id = id Select bg).FirstOrDefault()
            Return res
        Else
            Return New StabilityTable
        End If
    End Function

    ''' <summary>
    ''' Sp que guarda la tabla de estabilidad
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function SP_SaveStabilityTable(xml As String, userCode As String) As SP_SaveStabilityTable_Result Implements IStabilityTableRepository.SP_SaveStabilityTable
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveStabilityTable(xml, userCode).SingleOrDefault
    End Function

End Class