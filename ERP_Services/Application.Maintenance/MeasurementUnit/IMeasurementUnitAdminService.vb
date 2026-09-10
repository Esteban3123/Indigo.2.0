
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad unidad de medida
''' </summary>
''' <remarks></remarks>
Public Interface IMeasurementUnitAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas las unidades de medida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllMeasurementUnit() As List(Of MeasurementUnit)


    ''' <summary>
    ''' funcion que sirve para eliminar una unidad de medida.
    ''' </summary>
    ''' <param name="MeasurementUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteMeasurementUnit(ByVal MeasurementUnit As MeasurementUnit, ByVal audit As AuditMessage) As actionresult


    ''' <summary>
    ''' funcion que sirve para guardar una unidad de medida.
    ''' </summary>
    ''' <param name="MeasurementUnit"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveMeasurementUnit(ByVal MeasurementUnit As MeasurementUnit, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of MeasurementUnit)

    ''' <summary>
    ''' funciona que sirve para listar una unidad de medida.
    ''' </summary>
    ''' <param name="codeMeasurementUnit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMeasurementUnit(ByVal codeMeasurementUnit As String, ByVal audit As AuditMessage) As MeasurementUnit
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateMeasurementUnit(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of MeasurementUnit)
End Interface
