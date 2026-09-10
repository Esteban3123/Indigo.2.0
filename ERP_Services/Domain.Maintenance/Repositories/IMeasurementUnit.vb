
'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************



#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region



''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface IMeasurementUnitRepository
    Inherits IRepository(Of MeasurementUnit)

    ''' <summary>
    ''' funcion que lista todas las unidades de medida
    ''' </summary>
    ''' <returns>Lista de unidad de medida</returns>
    Function ListAllMeasurementUnit() As List(Of MeasurementUnit)
    ''' <summary>
    ''' consulta para retornar una unidad medida teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeMeasurementUnit">el codigo de una unidad medida</param>
    ''' <returns>Objeto unidad medida</returns>
    Function GetMeasurementUnit(ByVal codeMeasurementUnit As String, Optional ByVal tracking As Boolean = True) As MeasurementUnit

    ''' <summary>
    ''' funcion para almacenar una unidad medida
    ''' </summary>
    ''' <param name="MeasurementUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveMeasurementUnit(MeasurementUnit As MeasurementUnit) As Boolean

End Interface
