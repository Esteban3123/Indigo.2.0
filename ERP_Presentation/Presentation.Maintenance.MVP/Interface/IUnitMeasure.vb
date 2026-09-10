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
Imports Domain.Maintenance.Entities

#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presentador
''' </summary>
Public Interface IUnitMeasure
    Inherits IcrudBase

#Region "properties"
    ''' <summary>
    ''' Esta propiedad contiene el codigo de la unidad de medida
    ''' </summary>
    Property CodeUnitMeasure As String
    ''' <summary>
    ''' Esta propiedad contiene el nombre de la unidad de medida
    ''' </summary>
    Property NameUnitMeasure As String
    ''' <summary>
    ''' Esta propiedad contiene la abreviatura de la unidad de medida
    ''' </summary>
    Property AbbreviationUnitMeasure As String
    ''' <summary>
    ''' Esta propiedad contiene  el tipo de unidad de medida
    ''' </summary>
    Property TypeUnitMeasure As String
    ''' <summary>
    ''' Esta propiedad contiene el estado del tipo de inventario
    ''' </summary>
    Property StateUnitMeasure As Boolean
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
