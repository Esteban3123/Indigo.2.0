'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 05-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region
Public Interface IProductionLine
    Inherits IcrudBase
#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad que contiene la descripción del registro
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    Property State As Boolean

    ''' <summary>
    ''' Propiedad que contiene las unidades unitarios
    ''' </summary>
    Property UnitDoseTypeId As Integer
    ''' <summary>
    ''' Propiedad que contiene las unidades funcionales
    ''' </summary>
    Property FuctionalUnitId As Integer

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property Work24Hours As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property StartTime As DateTime?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkLunes As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkMartes As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkMiercoles As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkJueves As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkViernes As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkSabado As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkDomingo As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property WorkFestivo As Boolean?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property EndTime As DateTime?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property StopDate As DateTime?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ReasonForStop As String

    ''' <summary>
    ''' Propiedad para habilitar o desabilitar controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Propiedad que contiene la configuración de secuencia asignada al formulario
    ''' </summary>
    Property Sequence As MixingStationSequence

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece el datasource de tipo de dosis unitaria
    ''' </summary>
    Property UnitDoseTypeDatasource As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o establece el datasource de unidades unitarias
    ''' </summary>
    Property FuctionalUnitDatasource As XPInstantFeedbackSource

#End Region
End Interface
