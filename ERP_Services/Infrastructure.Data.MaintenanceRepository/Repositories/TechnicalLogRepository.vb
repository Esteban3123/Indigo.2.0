'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance
Imports Domain.Base.Entities
Imports Domain.Base

#End Region


''' <summary>
''' clase para hacer todas las operaciones de persistencia para la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Class TechnicalLogRepository
    Inherits GenericRepository(Of TechnicalLog)
    Implements ITechnicalLogRepository

    'Devuelve el contexto en este repositorio 
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IMaintenanceModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

   

    Public Function GetTechnicalLog(codeTechnicalLog As String, Optional tracking As Boolean = True) As TechnicalLog Implements ITechnicalLogRepository.GetTechnicalLog
        If tracking = True Then
            Dim Busqueda = From e In _context.TechnicalLog.Include("TechnicalLogMeasurementUnitDetail.MeasurementUnit")
                 Where e.Code = codeTechnicalLog
                 Select e
            If Busqueda.Count = 0 Then
                Return New TechnicalLog
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.TechnicalLog.AsNoTracking.Include("TechnicalLogMeasurementUnitDetail.MeasurementUnit").AsNoTracking
                 Where e.Code = codeTechnicalLog
                 Select e
            If Busqueda.Count = 0 Then
                Return New TechnicalLog
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllTechnicalLog() As List(Of TechnicalLog) Implements ITechnicalLogRepository.ListAllTechnicalLog
        Dim Busqueda = From e In _context.TechnicalLog.Include("TechnicalLogMeasurementUnitDetail").Include("TechnicalLogMeasurementUnitDetail.MeasurementUnit")
                 Select e

        Return Busqueda.ToList

    End Function

    Public Function SaveTechnicalLog(TechnicalLog As TechnicalLog) As Boolean Implements ITechnicalLogRepository.SaveTechnicalLog
        _context.TechnicalLog.ApplyChanges(TechnicalLog)
        Return True
    End Function
End Class
