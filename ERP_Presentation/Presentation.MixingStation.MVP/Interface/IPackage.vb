'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Yoe Andres Cardenas
' Created          : 06-09-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports DevExpress.Xpo
#End Region

Public Interface IPackage
    Inherits IcrudBase
#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad que contiene el nombre del registro
    ''' </summary>
    Property Name As String

    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    Property State As Boolean

    ''' <summary>
    ''' Propiedad que contiene la descripción del registro
    ''' </summary>
    Property Description As String

	''' <summary>
	''' Obtiene o establece el nivel de riesgo
	''' </summary>
	Property InventoryRiskLevelId As Integer?

    ''' <summary>
    ''' Obtiene o establece el codigo alternativo
    ''' </summary>
    Property CodeAlternative As String

    ''' <summary>
    ''' Obtiene o establece la osmolaridad total del paquete (producto)
    ''' </summary>
    Property OsmolarityTotal As Decimal

    ''' <summary>
    ''' Obtiene o establece el volumen total del paquete (producto)
    ''' </summary>
    Property VolumeTotalOrder As Decimal?

    ''' <summary>
    ''' Obtiene o establece el volumen total con purga del paquete (producto)
    ''' </summary>
    Property VolumeTotalOrderPurga As Decimal

	''' <summary>
	''' Obtiene o establece el peso total del paquete (producto)
	''' </summary>
	Property WeightTotalSolution As Decimal

    ''' <summary>
    ''' Obtiene o establece la concentración del paquete
    ''' </summary>
    Property Concentration As Decimal?

    ''' <summary>
    ''' Obtiene o establece la unidad de medida concentración del paquete
    ''' </summary>
    Property ConcentrationMeasurementUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece la estabilidad en horas
    ''' </summary>
    Property StabilityHour As TimeSpan?

    ''' <summary>
    ''' Obtiene o establece la estabilidad en dias
    ''' </summary>
    Property StabilityDays As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de estabilidad
    ''' </summary>
    Property TypeStability As Byte?

    ''' <summary>
    ''' Obtiene o establece las horas de vigencia a temperatura ambiente del paquete
    ''' </summary>
    Property EnvironmentalTemperatureTerm As Integer

	''' <summary>
	''' Obtiene o establece la purga del paquete (producto)
	''' </summary>
	Property Purge As Decimal

    ''' <summary>
    ''' Obtiene o establece las instrucciones de preaparación del paquete
    ''' </summary>
    Property PreparationInstructions As String

	''' <summary>
	''' Obtiene o establece las consideraciones especiales del paquete
	''' </summary>
	Property SpecialConsiderations As String

    ''' <summary>
    ''' Obtiene o establece el tipo de dosis unitaria del paquete
    ''' </summary>
    Property UnitDoseTypeId As Integer?

    ''' <summary>
    ''' Propiedad para habilitar o desabilitar controles
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Deja los controles en modo solo lectura (no editable) o habilita edición.
    ''' </summary>
    WriteOnly Property ControlsReadOnly As Boolean

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
    ''' Obtiene o establece el datasource de tipo de productos
    ''' </summary>
    Property ProductDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de niveles de riesgo
    ''' </summary>
    Property RiskLevelDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de tipo de dosis unitaria
    ''' </summary>
    Property UnitDoseTypeDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource del almacenamiento
    ''' </summary>
    Property StorageTemperatureDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el tipo de etiqueta del paquete
    ''' </summary>
    Property LabelType As Byte?

    ''' <summary>
    ''' Obtiene o establece el Id producto terminado
    ''' </summary>
    Property ProductId As Integer?

    ''' <summary>
    ''' Obtiene o establece el tipo de almacenamiento
    ''' </summary>
    Property Storage As Integer?

    ''' <summary>
    ''' Obtiene o establece el numero de readecuaciones permitidas
    ''' </summary>
    Property Readjustments As Byte

    ''' <summary>
    ''' Obtiene o establece el datasource de Readecuaciones permitidas
    ''' </summary>
    Property ReadjustmentsDatasource As XPInstantFeedbackSource
#End Region
End Interface
