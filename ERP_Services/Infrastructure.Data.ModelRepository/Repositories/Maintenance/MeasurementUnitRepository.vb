
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

Public Class MeasurementUnitRepository
    Inherits GenericRepository(Of MeasurementUnit)
    Implements IMeasurementUnitRepository


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


    Public Function GetMeasurementUnit(codeMeasurementUnit As String, Optional ByVal tracking As Boolean = True) As MeasurementUnit Implements IMeasurementUnitRepository.GetMeasurementUnit
        If tracking = True Then
            Dim Busqueda = From e In _context.MeasurementUnit
                 Where e.Code = codeMeasurementUnit
                 Select e
            If Busqueda.Count = 0 Then
                Return New MeasurementUnit
            Else
                Return Busqueda.Single
            End If
        Else
            Dim Busqueda = From e In _context.MeasurementUnit.AsNoTracking
                 Where e.Code = codeMeasurementUnit
                 Select e
            If Busqueda.Count = 0 Then
                Return New MeasurementUnit
            Else
                Return Busqueda.Single
            End If
        End If
    End Function

    Public Function ListAllMeasurementUnit() As List(Of MeasurementUnit) Implements IMeasurementUnitRepository.ListAllMeasurementUnit
        Dim Busqueda = From e In _context.MeasurementUnit
                Select e

        Return Busqueda.ToList
    End Function

    Public Function SaveMeasurementUnit(MeasurementUnit As MeasurementUnit) As Boolean Implements IMeasurementUnitRepository.SaveMeasurementUnit
        _context.MeasurementUnit.ApplyChanges(MeasurementUnit)
        Return True
    End Function
End Class



