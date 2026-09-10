'************************************************************
' Assembly         : Infrastructure.Data.GlosasRepository
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Infrastructure.Data.Base
Imports Domain.Entities
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
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function GetTechnicalLog(codeTechnicalLog As String, Optional tracking As Boolean = True) As TechnicalLog Implements ITechnicalLogRepository.GetTechnicalLog
        Dim query As TechnicalLog = Nothing
        If tracking = True Then
            query = (From e In _context.TechnicalLog.Include("TechnicalLogMeasurementUnitDetail.MeasurementUnit")
                     Where e.Code = codeTechnicalLog
                     Select e).FirstOrDefault()

        Else
            query = (From e In _context.TechnicalLog.AsNoTracking.Include("TechnicalLogMeasurementUnitDetail.MeasurementUnit").AsNoTracking
                     Where e.Code = codeTechnicalLog
                     Select e).FirstOrDefault()
        End If
        If query Is Nothing Then
            Return New TechnicalLog
        Else
            If query.TechnicalLogMeasurementUnitDetail.Any() Then
                For Each item In query.TechnicalLogMeasurementUnitDetail
                    Dim um As MeasurementUnit = (From m In _context.MeasurementUnit.AsNoTracking() Where m.Id = item.IdMeasurementUnit Select m).FirstOrDefault()
                    item.MeasurementUnitCode = um.Code
                    item.MeasurementUnitName = um.Name
                Next
            End If
            Return query
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
