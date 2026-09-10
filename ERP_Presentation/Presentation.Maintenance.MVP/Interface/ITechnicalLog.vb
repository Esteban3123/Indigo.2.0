'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Andres Cardozo
' Created          : 04-09-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Presentation.Base
Imports Presentation.CloudAgent.IndigoReference.Payroll
Imports Domain.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface ITechnicalLog
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo del registro tecnico
    ''' </summary>
    Property CodeTechnicalLog As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre del registro tecnico
    ''' </summary>
    Property NameTechnicalLog As String
    ''' <summary>
    ''' Esta propiedad contiene el id de la unidad de medida
    ''' </summary>
    Property IdUnitMeasure As Integer
    ''' <summary>
    ''' Esta propiedad contiene el listado de unidades de media
    ''' </summary>
    WriteOnly Property UnitMeasureDatasource As List(Of MeasurementUnit)
    ''' <summary>
    ''' Esta propiedad contiene el estado del registro tecnico
    ''' </summary>
    Property StateTechnicalLog As Boolean
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.MaintenanceSequence
#End Region

End Interface
