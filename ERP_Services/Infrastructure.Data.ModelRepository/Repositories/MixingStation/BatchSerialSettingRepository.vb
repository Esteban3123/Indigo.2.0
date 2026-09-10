'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Diego A. Roldan
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class BatchSerialSettingRepository
    Inherits GenericRepository(Of BatchSerialSetting)
    Implements IBatchSerialSettingRepository, Inject

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
    ''' obtiene el registro de parametro de lote
    ''' </summary>
    ''' <returns></returns>
    Public Function GetBatchSerialSetting() As BatchSerialSetting Implements IBatchSerialSettingRepository.GetBatchSerialSetting
        Dim Result = (From x In _context.BatchSerialSetting.Include("Sequense") Select x).FirstOrDefault
        If Result IsNot Nothing Then
            Result.PatternName = $"{Result.Sequense?.Name} - {Result.Sequense?.Pattern}"
        End If
        Return Result
    End Function


End Class
