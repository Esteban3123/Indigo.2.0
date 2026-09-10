'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls

#End Region

Public Interface IATC
    Inherits IcrudBase

    ''' <summary>
    ''' Obtiene el layout del frontal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el nombre del ATC
    ''' </summary>
    Property ATCName As String

    ''' <summary>
    ''' Obtiene o establece el DCI
    ''' </summary>
    Property DCIId As Integer

    ''' <summary>
    ''' Obtiene o establece el nombre de la abreviacion
    ''' </summary>
    Property AbbreviationName As String

    ''' <summary>
    ''' Obtiene o Establece la Presentacion
    ''' </summary>
    ''' <returns></returns>
    Property Presentations As String

    ''' <summary>
    ''' Obtiene o establece el id de la ruta de administracion
    ''' </summary>
    Property AdministrationRouteId As Integer

    ''' <summary>
    ''' Obtiene o establece el grupo farmacologico
    ''' </summary>
    Property PharmacologicalGroupId As Integer

    ''' <summary>
    ''' Obtiene o establece la concentracion
    ''' </summary>
    Property Concentration As String

    ''' <summary>
    ''' Obtiene o establece la concentración para el medicamento
    ''' </summary>
    Property ConcentrationQuantity As Decimal

    ''' <summary>
    ''' Obtiene o establece la Unidad de medida de la concentración
    ''' </summary>
    Property MeasureUnitConcentrationId As Integer?

    ''' <summary>
    ''' Obtiene o establece el datasource de unidad de medida
    ''' </summary>
    Property MeasureUnitConcentrationDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el nivel de riesgo
    ''' </summary>
    Property InventoryRiskLevelId As Integer

    ''' <summary>
    ''' Obtiene o establece las horas minimas de estabilidad
    ''' </summary>
    Property StabilityMinimumHours As Integer

    ''' <summary>
    ''' Obtiene o establece las horas maximas de estabilidad
    ''' </summary>
    Property StabilityMaximumHours As Decimal

    ''' <summary>
    ''' Obtiene o establece el tipo de formulación
    ''' </summary>
    Property FormulationType As Byte

    ''' <summary>
    ''' Obtiene o establece el peso del ATC
    ''' </summary>
    Property Weight As Decimal?

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del peso
    ''' </summary>
    Property WeightMeasureUnit As Integer?

    ''' <summary>
    ''' Obtiene o establece el volumen del ATC
    ''' </summary>
    Property Volume As Decimal?

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del volumen de ATC
    ''' </summary>
    Property VolumeMeasureUnit As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de administracion
    ''' </summary>
    Property AdministrationUnitId As Integer?

    ''' <summary>
    ''' Obtiene o establece el calculo automatico
    ''' </summary>
    Property AutomaticCalculation As Boolean

    ''' <summary>
    ''' Obtiene o establece si realiza traslado sobrantes de productos
    ''' </summary>
    Property TransferSurplusProduct As Boolean

    ''' <summary>
    ''' Obtiene o establece si el producto funciona como diluyente
    ''' </summary>
    Property DiluentProduct As Boolean

    ''' <summary>
    ''' Obtiene o establece si exige justificacion de medicamentos especiales
    ''' </summary>
    Property JustificationForSpecialDrugs As Boolean

    ''' <summary>
    ''' Obtiene o establece si exige justificacion de insumos/dispositivos
    ''' </summary>
    Property JustificationOfInputs As Boolean

    ''' <summary>
    ''' Obtiene o establece si es medicamento trazador
    ''' </summary>
    Property IndicatorDrug As Boolean

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de DCI
    ''' </summary>
    Property DCIDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de rutas de administracion
    ''' </summary>
    Property AdmisnistrationRouteDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de grupo farmacologico
    ''' </summary>
    Property PharmacologicalGroupDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de niveles de riesgo
    ''' </summary>
    Property RiskLevelDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de unidad de medida
    ''' </summary>
    Property MeasureUnitDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de unidad de medida de volumen
    ''' </summary>
    Property MeasureUnitVolumenDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de unidad de administracion
    ''' </summary>
    Property AdministrationUnitDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de las unidades de presentación UPR
    ''' </summary>
    ''' <returns></returns>
    Property UPRUnitsDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de los laboratorios
    ''' </summary>
    ''' <returns></returns>
    Property LaboratoriesDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Property Sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Permite saber si el producto es pos o no
    ''' </summary>
    ''' <returns></returns>
    Property POSProduct As Boolean?

    ''' <summary>
    ''' Permite saber si es de consumo
    ''' </summary>
    ''' <returns></returns>
    Property Consumption As Boolean?

    ''' <summary>
    ''' Permite saber si el medicamento es NPT o no
    ''' </summary>
    Property ProductNPT As Boolean

    ''' <summary>
    ''' Si el medicamento es NPT identifica la tipología de este como micro o macro nutriente 
    ''' </summary>
    Property ComponentType As Byte?

    ''' <summary>
    ''' Obtiene o establece la osmolaridad del Medicamento
    ''' </summary>
    Property Osmolarity As Decimal?

    ''' <summary>
    ''' Obtiene o establece la densidad del Medicamento
    ''' </summary>
    Property Density As Decimal?

    ''' <summary>
    ''' Establece el datasource de forma farmacologica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PharmaceuticalFormXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id de la forma farmacologica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdPharmaceuticalForm As Integer?

    ''' <summary>
    ''' Obtiene o establece un valor que indica si asocia o no insumos y/o medicamentos
    ''' </summary>
    ''' <returns></returns>
    Property HasSupplieMedicine As Boolean?

    ''' <summary>
    ''' Antibiotico
    ''' </summary>
    ''' <returns></returns>
    Property Antibiotico As Boolean?

    ''' <summary>
    ''' RequiereJuntaMedica
    ''' </summary>
    ''' <returns></returns>
    Property RequiereJuntaMedica As Boolean?

    ''' <summary>
    ''' AltoCosto
    ''' </summary>
    ''' <returns></returns>
    Property AltoCosto As Boolean?

    ''' <summary>
    ''' Condicionado
    ''' </summary>
    ''' <returns></returns>
    Property Conditioned As Boolean?

    ''' <summary>
    ''' UNIRS
    ''' </summary>
    ''' <returns></returns>
    Property UNIRS As Boolean?

    ''' <summary>
    ''' Multidose
    ''' </summary>
    ''' <returns></returns>
    Property Multidose As Boolean

    ''' <summary>
    ''' Stability
    ''' </summary>
    ''' <returns></returns>
    Property Stability As Boolean

    ''' <summary>
    ''' SuitableForReconstitution
    ''' </summary>
    ''' <returns></returns>
    Property SuitableForReconstitution As Boolean?

    ''' <summary>
    ''' UPRUnitsId
    ''' </summary>
    ''' <returns></returns>
    Property UPRUnitId As Integer?

    ''' <summary>
    ''' TotalSubstanceConcentration
    ''' </summary>
    ''' <returns></returns>
    Property TotalSubstanceConcentration As String

    ''' <summary>
    ''' Tipo de dato
    ''' </summary>
    ''' <returns></returns>
    Property DataType As Integer

    ''' <summary>
    ''' Descripción del dato clínico
    ''' </summary>
    ''' <returns></returns>
    Property Description As String

    ''' <summary>
    ''' Id del cups de tipo laboratorio
    ''' </summary>
    ''' <returns></returns>
    Property ControlLaboratoryId As Integer?

    ''' <summary>
    ''' Tiempo de la solicitud para laboratorio
    ''' </summary>
    ''' <returns></returns>
    Property TimeRequest As Integer?

    ''' <summary>
    ''' Frecuencia del laboratorio (horas,dias,semanas,meses)
    ''' </summary>
    ''' <returns></returns>
    Property Frequency As Integer?

    ''' <summary>
    '''Id del tipo de diagnóstico
    ''' </summary>
    ''' <returns></returns>
    Property DiagnosisId As Integer?
End Interface
