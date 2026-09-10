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

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmATC
    Implements IATC, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Properties and Variables"
    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Listado de eliminados de las patologías
    ''' </summary>
    Dim ListDeletePahotlogies As List(Of POSPathologies)

    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"
    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingInventory As SettingInventory

    Private _stateOpenPopUpDCI As Boolean
    Private _stateOpenPopUpAdministrationRoute As Boolean
    Private _stateOpenPopUpPharmacologicalGroup As Boolean
    Private _stateOpenPopUpRiskLevel As Boolean
    Private _stateOpenPopUpMeasureUnit As Boolean
    Private _stateOpenPopUpMeasureUnitVolumen As Boolean
    Private _stateOpenPopUpAdministrationUnit As Boolean
    ''' <summary>
    ''' Obtiene el layout del frontal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IATC.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IATC.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Public Property Sequence As InventorySequence Implements IATC.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si el producto es pos o no
    ''' </summary>
    ''' <returns></returns>
    Public Property POSProduct As Boolean? Implements IATC.POSProduct
        Get
            Return INDslePBSProduct.EditValue
        End Get
        Set(value As Boolean?)
            INDslePBSProduct.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _atc As ATC

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Dim _searchMode As Boolean

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PATC

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInventory

    ''' <summary>
    ''' Datasource de tipo de formulacion
    ''' </summary>
    Dim ListFormulationType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Datasource de Si y No
    ''' </summary>
    Dim ListYesNo As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Me permite saber si se puede cambiar el valor del control entrando al evento de editValueChanged
    ''' </summary>
    Private AllowChangeSearch As Boolean = True

    ''' <summary>
    ''' Variable que contiene la entidad de productos para el manejo de patologías
    ''' </summary>
    Dim ProductPathologies As InventoryProduct

    ''' <summary>
    ''' Variable que contiene la abreviacion de la unidad de medida
    ''' </summary>
    Dim MeasurementUnitAbbreviation As String

    ''' <summary>
    ''' Variable que contiene la suma de las concentraciones de sustancias
    ''' </summary>
    Dim TotalSubstanceConcentrationCombination As Double

    ''' <summary>
    ''' Variable que contiene la abreviación de unidad de medida para unidad de administración tipo peso
    ''' </summary>
    Dim SelectedWeightAbbreviation As String

    ''' <summary>
    ''' Variable que contiene la abreviación de unidad de medida para unidad de administración tipo volumen
    ''' </summary>
    Dim SelectedVolumeAbbreviation As String

    ''' <summary>
    ''' Variable que contiene la abreviación de unidad de medida para unidad de administración
    ''' </summary>
    Dim SelectedAdminUnitAbbreviation As String

    ''' <summary>
    ''' Variable que contiene los parámetros del popup de datos clínicos
    ''' </summary>
    Dim CurrentClinicalData As ATCClinicalData

    Dim _stateOpenPopUpContainerControl As Boolean = False

    ''' <summary>
    ''' Permite saber si es de consumo
    ''' </summary>
    ''' <returns></returns>
    Public Property Consumption As Boolean? Implements IATC.Consumption
        Get
            Return INDsleConsumption.EditValue
        End Get
        Set(value As Boolean?)
            INDsleConsumption.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Lista de ATCAdministrationRoute Eliminados
    ''' </summary>
    Dim ListDeleteATCAdministrationRoute As List(Of ATCAdministrationRoute)

    Dim ListATCAdministrationRoute As List(Of ATCAdministrationRoute)

    Dim listDeleteATCConcentrationByDCI As List(Of ATCConcentrationByDCI)

    ''' <summary>
    ''' Lista de ATCClinicalData eliminados
    ''' </summary>
    Dim ListDeleteClinicalData As List(Of ATCClinicalData)

#Region "Properties Entity"
    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    Public Property Code As String Implements IATC.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre de la abreviacion
    ''' </summary>
    Public Property AbbreviationName As String Implements IATC.AbbreviationName
        Get
            Return INDtxtAbbreviation.EditValue
        End Get
        Set(value As String)
            INDtxtAbbreviation.EditValue = value
        End Set
    End Property

    Public Property Presentations As String Implements IATC.Presentations
        Get
            Return INDTxtPresentation.EditValue
        End Get
        Set(value As String)
            INDTxtPresentation.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el id de la ruta de administracion
    ''' </summary>
    Public Property AdministrationRouteId As Integer Implements IATC.AdministrationRouteId
        Get
            Return INDsleAdministrationRoute.EditValue
        End Get
        Set(value As Integer)
            INDsleAdministrationRoute.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de administracion
    ''' </summary>
    Public Property AdministrationUnitId As Integer? Implements IATC.AdministrationUnitId
        Get
            Return INDsleAdministrationUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdministrationUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del ATC
    ''' </summary>
    Public Property ATCName As String Implements IATC.ATCName
        Get
            Return INDtxtATCName.Text
        End Get
        Set(value As String)
            INDtxtATCName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el calculo automatico
    ''' </summary>
    Public Property AutomaticCalculation As Boolean Implements IATC.AutomaticCalculation
        Get
            Return INDgleAutomaticCalc.EditValue
        End Get
        Set(value As Boolean)
            INDgleAutomaticCalc.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la concentracion
    ''' </summary>
    Public Property Concentration As String Implements IATC.Concentration
        Get
            Return INDtxtConcentration.EditValue
        End Get
        Set(value As String)
            INDtxtConcentration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad de la concentracion
    ''' </summary>
    Public Property ConcentrationQuantity As Decimal Implements IATC.ConcentrationQuantity
        Get
            Return INDSeConcentrationQuantity.EditValue
        End Get
        Set(value As Decimal)
            INDSeConcentrationQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida de la concentracion
    ''' </summary>
    Public Property MeasureUnitConcentrationId As Integer? Implements IATC.MeasureUnitConcentrationId
        Get
            Return INDSleMeasureUnitConcentration.EditValue
        End Get
        Set(value As Integer?)
            INDSleMeasureUnitConcentration.EditValue = value
        End Set
    End Property

    Public Property MeasureUnitConcentrationDatasource As XPInstantFeedbackSource Implements IATC.MeasureUnitConcentrationDatasource
        Get
            Return CType(INDSleMeasureUnitConcentration.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleMeasureUnitConcentration.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el DCI
    ''' </summary>
    Public Property DCIId As Integer Implements IATC.DCIId
        Get
            Return INDsleDCI.EditValue
        End Get
        Set(value As Integer)
            INDsleDCI.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el producto funciona como diluyente
    ''' </summary>
    Public Property DiluentProduct As Boolean Implements IATC.DiluentProduct
        Get
            Return INDgleDiluentWorks.EditValue
        End Get
        Set(value As Boolean)
            INDgleDiluentWorks.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de formulación
    ''' </summary>
    Public Property FormulationType As Byte Implements IATC.FormulationType
        Get
            Return INDgleFormulaType.EditValue
        End Get
        Set(value As Byte)
            INDgleFormulaType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si es medicamento trazador
    ''' </summary>
    Public Property IndicatorDrug As Boolean Implements IATC.IndicatorDrug
        Get
            Return INDgleMedicationLiner.EditValue
        End Get
        Set(value As Boolean)
            INDgleMedicationLiner.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nivel de riesgo
    ''' </summary>
    Public Property InventoryRiskLevelId As Integer Implements IATC.InventoryRiskLevelId
        Get
            Return INDsleRiskLevel.EditValue
        End Get
        Set(value As Integer)
            INDsleRiskLevel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si exige justificacion de medicamentos especiales
    ''' </summary>
    Public Property JustificationForSpecialDrugs As Boolean Implements IATC.JustificationForSpecialDrugs
        Get
            Return INDgleJustifiesSpecialMedications.EditValue
        End Get
        Set(value As Boolean)
            INDgleJustifiesSpecialMedications.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si exige justificacion de insumos/dispositivos
    ''' </summary>
    Public Property JustificationOfInputs As Boolean Implements IATC.JustificationOfInputs
        Get
            Return INDgleInputsJustifies.EditValue
        End Get
        Set(value As Boolean)
            INDgleInputsJustifies.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el grupo farmacologico
    ''' </summary>
    Public Property PharmacologicalGroupId As Integer Implements IATC.PharmacologicalGroupId
        Get
            Return INDslePharmacologicalGroup.EditValue
        End Get
        Set(value As Integer)
            INDslePharmacologicalGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las horas maximas de estabilidad
    ''' </summary>
    Public Property StabilityMaximumHours As Decimal Implements IATC.StabilityMaximumHours
        Get
            Return INDspMaximumHourStability.EditValue
        End Get
        Set(value As Decimal)
            INDspMaximumHourStability.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las horas minimas de estabilidad
    ''' </summary>
    Public Property StabilityMinimumHours As Integer Implements IATC.StabilityMinimumHours
        Get
            Return INDspMinimumHourStability.EditValue
        End Get
        Set(value As Integer)
            INDspMinimumHourStability.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IATC.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si realiza traslado sobrantes de productos
    ''' </summary>
    Public Property TransferSurplusProduct As Boolean Implements IATC.TransferSurplusProduct
        Get
            Return INDglePerformsTransfersSurplus.EditValue
        End Get
        Set(value As Boolean)
            INDglePerformsTransfersSurplus.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el volumen del ATC
    ''' </summary>
    Public Property Volume As Decimal? Implements IATC.Volume
        Get
            Return IIf(INDtxtVolumeAmountATC.EditValue Is Nothing, Nothing, CDec(INDtxtVolumeAmountATC.EditValue))
        End Get
        Set(value As Decimal?)
            INDtxtVolumeAmountATC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del volumen de ATC
    ''' </summary>
    Public Property VolumeMeasureUnit As Integer? Implements IATC.VolumeMeasureUnit
        Get
            Return INDsleMeasureUnitVolume.EditValue
        End Get
        Set(value As Integer?)
            INDsleMeasureUnitVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el peso del ATC
    ''' </summary>
    Public Property Weight As Decimal? Implements IATC.Weight
        Get
            Return IIf(INDtxtWeightATC.EditValue Is Nothing, Nothing, CDec(INDtxtWeightATC.EditValue))
        End Get
        Set(value As Decimal?)
            INDtxtWeightATC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del peso
    ''' </summary>
    Public Property WeightMeasureUnit As Integer? Implements IATC.WeightMeasureUnit
        Get
            Return INDsleMeasureUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleMeasureUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si el medicamento es NPT o no
    ''' </summary>
    Public Property ProductNPT As Boolean Implements IATC.ProductNPT
        Get
            Return CType(INDrgProductNPT.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDrgProductNPT.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Si el medicamento es NPT guarda la tipología de este como micro o macro nutriente 
    ''' </summary>
    Public Property ComponentType As Byte? Implements IATC.ComponentType
        Get
            Return IIf(INDSleComponentType.EditValue Is Nothing, Nothing, CByte(INDSleComponentType.EditValue))
        End Get
        Set(value As Byte?)
            INDSleComponentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si el medicamento es NPT o no
    ''' </summary>
    Public Property Multidosis As Boolean Implements IATC.Multidose
        Get
            Return CType(INDrgMultidosis.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDrgMultidosis.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si el medicamento es NPT o no
    ''' </summary>
    Public Property Stability As Boolean Implements IATC.Stability
        Get
            Return CType(INDrgStability.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDrgStability.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber si el medicamento es NPT o no
    ''' </summary>
    Public Property SuitableForReconstitution As Boolean? Implements IATC.SuitableForReconstitution
        Get
            Return CType(INDrgSuitableForReconstitution.EditValue, Boolean)
        End Get
        Set(value As Boolean?)
            INDrgSuitableForReconstitution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la osmolaridad del Medicamento
    ''' </summary>
    Public Property Osmolarity As Decimal? Implements IATC.Osmolarity
        Get
            Return IIf(INDtxtOsmolarityATC.EditValue Is Nothing, Nothing, CDec(INDtxtOsmolarityATC.EditValue))
        End Get
        Set(value As Decimal?)
            INDtxtOsmolarityATC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la densidad del Medicamento
    ''' </summary>
    Public Property Density As Decimal? Implements IATC.Density
        Get
            Return IIf(INDtxtDensityATC.EditValue Is Nothing, Nothing, CDec(INDtxtDensityATC.EditValue))
        End Get
        Set(value As Decimal?)
            INDtxtDensityATC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la forma farmaceutica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdPharmaceuticalForm As Integer? Implements IATC.IdPharmaceuticalForm
        Get
            Return INDSlePharmaceuticalForm.EditValue
        End Get
        Set(value As Integer?)
            INDSlePharmaceuticalForm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la forma farmaceutica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PharmaceuticalFormXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IATC.PharmaceuticalFormXpo
        Get
            Return CType(INDSlePharmaceuticalForm.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlePharmaceuticalForm.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece un valor que indica si asocia o no insumos y/o medicamentos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property HasSupplieMedicine As Boolean? Implements IATC.HasSupplieMedicine
        Get
            Return INDGleHasSupplieMedicine.EditValue
        End Get
        Set(value As Boolean?)
            INDGleHasSupplieMedicine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Antibiotico As Boolean? Implements IATC.Antibiotico
        Get
            Return INDGleAntibiotico.EditValue
        End Get
        Set(value As Boolean?)
            INDGleAntibiotico.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property RequiereJuntaMedica As Boolean? Implements IATC.RequiereJuntaMedica
        Get
            Return INDGleRequiereJuntaMedica.EditValue
        End Get
        Set(value As Boolean?)
            INDGleRequiereJuntaMedica.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property AltoCosto As Boolean? Implements IATC.AltoCosto
        Get
            Return INDGleAltoCosto.EditValue
        End Get
        Set(value As Boolean?)
            INDGleAltoCosto.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Conditioned As Boolean? Implements IATC.Conditioned
        Get
            Return INDSleConditioned.EditValue
        End Get
        Set(value As Boolean?)
            INDSleConditioned.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property UNIRS As Boolean? Implements IATC.UNIRS
        Get
            Return INDSleUNIRS.EditValue
        End Get
        Set(value As Boolean?)
            INDSleUNIRS.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si se validan las patologías pos
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ValidatePOSPathologies As Boolean
        Get
            Return If(_settingInventory Is Nothing, True, _settingInventory.ValidatePOSPathologies)
        End Get
    End Property

    ''' <summary>
    ''' Indica si el medicamento esta asociado a un DCI combinado
    ''' </summary>
    ''' <returns></returns>
    Public Property Combined As Boolean
        Get
            Return CType(INDrgCombinedMedicine.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDrgCombinedMedicine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' ID de la unidad UPR
    ''' </summary>
    ''' <returns></returns>
    Public Property UPRUnitId As Integer? Implements IATC.UPRUnitId
        Get
            Return INDsleUPRUnits.EditValue
        End Get
        Set(value As Integer?)
            INDsleUPRUnits.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Total de concentración de sustancias combinadas
    ''' </summary>
    ''' <returns></returns>
    Public Property TotalSubstanceConcentration As String Implements IATC.TotalSubstanceConcentration
        Get
            Return INDTxtTotalSubstanceConcentration.EditValue
        End Get
        Set(value As String)
            INDTxtTotalSubstanceConcentration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el tipo de dato clínico
    ''' </summary>
    ''' <returns></returns>
    Public Property DataType As Integer Implements IATC.DataType
        Get
            Return INDsleDataType.EditValue
        End Get
        Set(value As Integer)
            INDsleDataType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la descripción del dato clínico
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String Implements IATC.Description
        Get
            Return INDtxtDescription.EditValue
        End Get
        Set(value As String)
            INDtxtDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el tipo de dato clínico
    ''' </summary>
    ''' <returns></returns>
    Public Property ControlLaboratoryId As Integer? Implements IATC.ControlLaboratoryId
        Get
            Return INDseSelectLaboratory.EditValue
        End Get
        Set(value As Integer?)
            INDseSelectLaboratory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tiempo de la solicitud
    ''' </summary>
    ''' <returns></returns>
    Public Property TimeRequest As Integer? Implements IATC.TimeRequest
        Get
            Return CInt(INDseTimeRequest.EditValue)
        End Get
        Set(value As Integer?)
            INDseTimeRequest.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tiempo de la solicitud
    ''' </summary>
    ''' <returns></returns>
    Public Property Frequency As Integer? Implements IATC.Frequency
        Get
            Return INDseFrequency.EditValue
        End Get
        Set(value As Integer?)
            INDseFrequency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tiempo de la solicitud
    ''' </summary>
    ''' <returns></returns>
    Public Property DiagnosisId As Integer? Implements IATC.DiagnosisId
        Get
            Return INDsleDiagnostic.EditValue
        End Get
        Set(value As Integer?)
            INDsleDiagnostic.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' variable que indica si el popup de diagnosticos se abre por primera vez para llenar el datasource
    ''' </summary>
    Private _stateOpenPopUpDiagnostic As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource de diagnosticos
    ''' </summary>
    Private Property DiagnosticDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleDiagnostic.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDiagnostic.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Datasource"
    ''' <summary>
    ''' Obtiene o establece el datasource de unidad de administracion
    ''' </summary>
    Public Property AdministrationUnitDatasource As XPInstantFeedbackSource Implements IATC.AdministrationUnitDatasource
        Get
            Return CType(INDsleAdministrationUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAdministrationUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de rutas de administracion
    ''' </summary>
    Public Property AdmisnistrationRouteDatasource As XPInstantFeedbackSource Implements IATC.AdmisnistrationRouteDatasource
        Get
            Return CType(INDsleAdministrationRoute.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAdministrationRoute.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de DCI
    ''' </summary>
    Public Property DCIDatasource As XPInstantFeedbackSource Implements IATC.DCIDatasource
        Get
            Return CType(INDsleDCI.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleDCI.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de unidad de medida
    ''' </summary>
    Public Property MeasureUnitDatasource As XPInstantFeedbackSource Implements IATC.MeasureUnitDatasource
        Get
            Return CType(INDsleMeasureUnit.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMeasureUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de unidad de medida de volumen
    ''' </summary>
    Public Property MeasureUnitVolumenDatasource As XPInstantFeedbackSource Implements IATC.MeasureUnitVolumenDatasource
        Get
            Return CType(INDsleMeasureUnitVolume.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMeasureUnitVolume.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de grupo farmacologico
    ''' </summary>
    Public Property PharmacologicalGroupDatasource As XPInstantFeedbackSource Implements IATC.PharmacologicalGroupDatasource
        Get
            Return CType(INDslePharmacologicalGroup.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePharmacologicalGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de niveles de riesgo
    ''' </summary>
    Public Property RiskLevelDatasource As XPInstantFeedbackSource Implements IATC.RiskLevelDatasource
        Get
            Return CType(INDsleRiskLevel.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRiskLevel.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de unidades UPR
    ''' </summary>
    ''' <returns></returns>
    Public Property UPRUnitsDatasource As XPInstantFeedbackSource Implements IATC.UPRUnitsDatasource
        Get
            Return CType(INDsleUPRUnits.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleUPRUnits.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de laboratorios
    ''' </summary>
    ''' <returns></returns>
    Public Property LaboratoriesDatasource As XPInstantFeedbackSource Implements IATC.LaboratoriesDatasource
        Get
            Return CType(INDseSelectLaboratory.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDseSelectLaboratory.Properties.DataSource = value
        End Set
    End Property

#End Region

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _atc = Nothing
        _searchMode = Nothing
        _presenter = Nothing
        _record = Nothing
        ListFormulationType = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _settingInventory = Nothing
        ListDeleteATCAdministrationRoute = Nothing
        listDeleteATCConcentrationByDCI = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmATC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmATC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PATC(Me)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()

        ActionsOnControls = False
        Await LoadParameters()
        If FormSearchObjects Is Nothing Then
            _searchMode = False
        End If

        createListFormulationType()

        IndigoGridView1.SetListAcction(INDGvAdministrationRoute, {eAcciones.Remove}.ToList())
        IndigoGridControl1.RefreshGrid(INDGcAdministrationRoute)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvAdministrationRoute.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        IndigoGridView2.SetListAcction(INDGvOptions, {eAcciones.Edit, eAcciones.Remove}.ToList())
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvOptions.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        Deshacer()

        ShowPOSProduct()
        ShowUNIRS()
        ShowUPRUnits()
        LoadStatus()

        _stateOpenPopUpContainerControl = True
    End Sub

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._atc IsNot Nothing AndAlso Me._atc.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Await DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region
#End Region

#Region "Closing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmATC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmATC_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de atc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATCEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATCEntity.QueryPopUp
        If INDsleATCEntity.Properties.DataSource Is Nothing AndAlso INDsleDCI.EditValue IsNot Nothing Then
            INDsleATCEntity.Properties.DataSource = _presenter.LoadATC(INDsleDCI.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleDCI control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleDCI_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDCI.QueryPopUp
        If Not _stateOpenPopUpDCI Then
            _presenter.InitializeDCI()
            _stateOpenPopUpDCI = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleAdministrationRoute control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdministrationRoute_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdministrationRoute.QueryPopUp
        If Not _stateOpenPopUpAdministrationRoute Then
            _presenter.InitializeAdministrationRoute()
            _stateOpenPopUpAdministrationRoute = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDslePharmacologicalGroup control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDslePharmacologicalGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePharmacologicalGroup.QueryPopUp
        If Not _stateOpenPopUpPharmacologicalGroup Then
            _presenter.InitializePharmacologicalGroup()
            _stateOpenPopUpPharmacologicalGroup = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleRiskLevel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleRiskLevel_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRiskLevel.QueryPopUp
        If Not _stateOpenPopUpRiskLevel Then
            _presenter.InitializeRiskLevel()
            _stateOpenPopUpRiskLevel = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleMeasureUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleMeasureUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasureUnit.QueryPopUp
        If Not _stateOpenPopUpMeasureUnit Then
            _presenter.InitializeMeasureUnit()
            _stateOpenPopUpMeasureUnit = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleMeasureUnitVolume control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleMeasureUnitVolume_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasureUnitVolume.QueryPopUp
        If Not _stateOpenPopUpMeasureUnitVolumen Then
            _presenter.InitializeMeasureUnitVolumen()
            _stateOpenPopUpMeasureUnitVolumen = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleAdministrationUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdministrationUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdministrationUnit.QueryPopUp
        If Not _stateOpenPopUpAdministrationUnit Then
            _presenter.InitializeAdministrationUnit()
            _stateOpenPopUpAdministrationUnit = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de unidad de medida de la concentración
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleMeasureUnitConcentration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMeasureUnitConcentration.QueryPopUp
        If INDSleMeasureUnitConcentration.Properties.DataSource Is Nothing Then
            _presenter.InitializeMeasureUnitConcentration()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de forma farmaceutica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePharmaceuticalForm_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePharmaceuticalForm.QueryPopUp
        If INDSlePharmaceuticalForm.Properties.DataSource Is Nothing Then
            _presenter.InitializePharmaceuticalForm()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control para elegir laboratorio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseSelectLaboratory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDseSelectLaboratory.QueryPopUp
        If INDseSelectLaboratory.Properties.DataSource Is Nothing Then
            INDseSelectLaboratory.Properties.DataSource = _presenter.InitializeLaboratories()
        End If
    End Sub

    ''' <summary>
    ''' Método que carga el datasource para el combo de unidades de medida de sustancias combinadas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSeMeasurementUnit.QueryPopUp
        If INDSeMeasurementUnit.DataSource Is Nothing Then

            Dim selectedId As Integer

            INDSeMeasurementUnit.DataSource = _presenter.InitializeMeasureUnitSubstanceConcentration()

            If _atc IsNot Nothing AndAlso _atc.ATCConcentrationByDCI IsNot Nothing Then
                For Each atcConcentrationByDCI In _atc.ATCConcentrationByDCI
                    selectedId = atcConcentrationByDCI.ConcentrationMeasureUnitId
                Next
                For i As Integer = 0 To INDGvConcentrationOfSubstance.RowCount - 1
                    INDGvConcentrationOfSubstance.SetRowCellValue(i, "Name", selectedId)
                Next
                INDGcConcentrationOfSubstance.RefreshDataSource()
            End If
        End If
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewATC()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceOptions control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceOptions_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceOptions.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceOptions.ShowPopup()
        End If
    End Sub
#End Region

#Region "PopUpContainerControl"

#Region "Events"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleDiagnostic control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleDiagnostic_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDiagnostic.QueryPopUp
        If Not _stateOpenPopUpDiagnostic Then
            InitializeDiagnostic()
            _stateOpenPopUpDiagnostic = True
        End If
    End Sub

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        CurrentClinicalData = INDGvOptions.GetRow(INDGvOptions.FocusedRowHandle)
        Select Case sender.Tag
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                    Return
                End If

                If CurrentClinicalData IsNot Nothing Then
                    RemoveDetailClinicalData(CurrentClinicalData)
                End If

            Case "Edit"
                If CurrentClinicalData IsNot Nothing Then
                    DataType = CurrentClinicalData.DataType
                    DiagnosisId = CurrentClinicalData.DiagnosisId
                    Description = CurrentClinicalData.Description
                    ControlLaboratoryId = CurrentClinicalData.ControlLaboratoryId
                    TimeRequest = CurrentClinicalData.TimeRequest
                    Frequency = CurrentClinicalData.Frequency

                    Me.INDpceOptions.ShowPopup()
                End If
        End Select
    End Sub

#End Region

#Region "Methods and Variables"
    ''' <summary>
    ''' carga el datasource de diagnosticos
    ''' </summary>
    Private Sub InitializeDiagnostic()
        Using Model As New MBusqueda
            Me.DiagnosticDatasource = Model.ConsultarEntidades(eDataSource.ListDiagnostic)
        End Using
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al momento de dar click en el btn "Agregar" del popup de datos clínicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsbAddOption_Click(sender As Object, e As EventArgs) Handles INDsbAddOption.Click
        Dim isValid = ValidateClinicalDataFields()
        If Not isValid Then
            Exit Sub
        End If

        Dim clinicalData As ATCClinicalData

        'Si existe un CurrentClinicalData entonces se estaba editando el registro desde el evento ContexMenuActions y por lo tanto viene lleno con el contexto de la fila que se seleccionó.
        If CurrentClinicalData IsNot Nothing Then
            clinicalData = CurrentClinicalData
        Else
            clinicalData = New ATCClinicalData()
        End If

        'Se llena la entidad
        With clinicalData
            .DataType = DataType
            Select Case DataType
                Case 3
                    .Description = (INDsleDiagnostic.Text).ToUpper()
                    .DiagnosisId = DiagnosisId
                Case 9
                    .Description = ($"{INDseSelectLaboratory.Text} CADA {TimeRequest} {INDseFrequency.Text}").ToUpper()
                    .ControlLaboratoryId = ControlLaboratoryId
                    .TimeRequest = TimeRequest
                    .Frequency = Frequency
                Case Else
                    .Description = (INDtxtDescription.Text).ToUpper()
            End Select
        End With

        'Se guarda en la entidad ATC y se actualiza el datasource
        _atc.ATCClinicalData.Add(clinicalData)
        INDGcOptions.DataSource = _atc.ATCClinicalData.ToList()
        INDGcOptions.RefreshDataSource()
        INDpceOptions.ClosePopup()
        ClearPopUp()
    End Sub

    ''' <summary>
    ''' Métdoo que limpia el popup de datos clínicos
    ''' </summary>
    Private Sub ClearPopUp()
        INDsleDataType.EditValue = Nothing
        INDsleDiagnostic.EditValue = Nothing
        INDtxtDescription.EditValue = Nothing
        INDseSelectLaboratory.EditValue = Nothing
        INDseTimeRequest.EditValue = Nothing
        Frequency = Nothing
        CurrentClinicalData = Nothing
        INDpceOptions.ClosePopup()
    End Sub

    ''' <summary>
    ''' Método para validar campos obligatorios del popup datos clínicos
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateClinicalDataFields()
        Dim validate As Boolean = True
        If Not {9, 3}.Contains(DataType) And Description = "" Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar una descripción"
            validate = False
        End If
        Return validate
    End Function

    ''' <summary>
    ''' Elimina los detalles de la fila seleccionada para eliminar.
    ''' </summary>
    ''' <param name="CurrentClinicalData"></param>
    Private Sub RemoveDetailClinicalData(CurrentClinicalData As ATCClinicalData)
        CurrentClinicalData.MarkAsDeleted()
        If CurrentClinicalData.Id > 0 Then
            If ListDeleteClinicalData Is Nothing Then
                ListDeleteClinicalData = New List(Of ATCClinicalData)
            End If
            ListDeleteClinicalData.Add(CurrentClinicalData)
        End If
        INDGcOptions.DataSource = _atc.ATCClinicalData.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        INDGcOptions.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Método para ajustar de manera dinámica la altura del popupcontainercontrol
    ''' </summary>
    Private Sub AdjustPopupHeight()
        Dim totalHeight As Integer = 0

        ' Recorre todos los LayoutControlItems dentro del LayoutControlGroup y calcula la altura de los controles visibles
        For Each item As DevExpress.XtraLayout.LayoutControlItem In LayoutControlGroup2.Items
            If item.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                totalHeight += item.Height
            End If
        Next

        ' Ajusta la altura del PopupContainerControl según los controles visibles
        Dim newSize As New Size(PopupContainerControl2.Width, totalHeight + 25)
        PopupContainerControl2.MinimumSize = newSize
        PopupContainerControl2.MaximumSize = newSize

        'Se reasigna el popupcontainercontrol para que se actualice y refleje el cambio de altura
        INDpceOptions.Properties.PopupControl = Nothing
        INDpceOptions.Properties.PopupControl = PopupContainerControl2
        INDpceOptions.ShowPopup()
    End Sub

    ''' <summary>
    ''' Método para controlar visibilidad de controles del popup datos clínicos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDataType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDataType.EditValueChanged
        If _stateOpenPopUpContainerControl Then
            Select Case DataType
                Case 9
                    INDlciDescription.HideLayout()
                    INDlciDiagnostic.HideLayout()
                    INDlciSelectLaboratory.HideControl(False)
                    INDlciTimeRequest.HideControl(False)
                    INDlciFrequency.HideControl(False)
                    INDseFrequency.EditValue = 2
                Case 3
                    INDlciDescription.HideLayout()
                    INDlciSelectLaboratory.HideLayout()
                    INDlciTimeRequest.HideLayout()
                    INDlciFrequency.HideLayout()
                    INDlciDiagnostic.HideControl(False)
                Case Else
                    INDlciDescription.HideControl(False)
                    INDlciSelectLaboratory.HideLayout()
                    INDlciTimeRequest.HideLayout()
                    INDlciFrequency.HideLayout()
                    INDlciDiagnostic.HideLayout()
            End Select
            AdjustPopupHeight()
        End If
    End Sub
#End Region
#End Region

#Region "EditvalueChanged"
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si es pos o no
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePOSProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePBSProduct.EditValueChanged
        Conditioned = False
        INDLciConditioned.HideControl(True)

        'Si el producto es no pos se elimina el botón
        Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDslePBSProduct.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
        If info IsNot Nothing Then
            INDslePBSProduct.Properties.Buttons.RemoveAt(info.Index)
        End If

        If POSProduct IsNot Nothing Then
            If POSProduct Then 'Si el producto es pos se crea el botón en tiempo de ejcución
                If ValidatePOSPathologies Then
                    info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDslePBSProduct.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
                    If info Is Nothing Then 'Si no existe un boton de calculadora se crea uno nuevo
                        Dim editor As New DevExpress.XtraEditors.Controls.EditorButton
                        editor.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
                        editor.Visible = True
                        editor.Caption = "Aclaraciones"
                        editor.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
                        INDslePBSProduct.Properties.Buttons.Add(editor)
                    End If
                Else
                    INDLciConditioned.HideControl(False)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de si asocia o no elementos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleHasSupplieMedicine_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleHasSupplieMedicine.EditValueChanged
        If HasSupplieMedicine IsNot Nothing Then
            If HasSupplieMedicine Then 'Si asocia elementos se agrega el boton
                Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDGleHasSupplieMedicine.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
                If info Is Nothing Then 'Si no existe un boton  se crea uno nuevo
                    Dim editor As New DevExpress.XtraEditors.Controls.EditorButton
                    editor.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph
                    editor.Visible = True
                    editor.Caption = "Elementos"
                    editor.Appearance.Font = New System.Drawing.Font("Segoe UI Light", 9.0!)
                    INDGleHasSupplieMedicine.Properties.Buttons.Add(editor)
                End If
            Else 'Si no asocia elementos se quita el boton
                Dim info = (From item As DevExpress.XtraEditors.Controls.EditorButton In INDGleHasSupplieMedicine.Properties.Buttons Where item.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph).FirstOrDefault
                If info IsNot Nothing Then
                    INDGleHasSupplieMedicine.Properties.Buttons.RemoveAt(info.Index)
                End If
            End If
        End If
    End Sub



    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de atc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATCEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleATCEntity.EditValueChanged
        If INDsleATCEntity.EditValue IsNot Nothing AndAlso AllowChangeSearch Then
            Dim info = _presenter.GetATCEntityById(INDsleATCEntity.EditValue)
            If info IsNot Nothing Then
                INDslePharmacologicalGroup.EditValue = info.IdPharmacologicalGroup.Id
                INDslePharmacologicalGroup.Properties.NullText = info.IdPharmacologicalGroup.CodeName
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de forma farmaceutica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePharmaceuticalForm_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePharmaceuticalForm.EditValueChanged
        If INDSlePharmaceuticalForm.EditValue IsNot Nothing AndAlso AllowChangeSearch Then
            Dim info = _presenter.GetPharmaceuticalFormById(INDSlePharmaceuticalForm.EditValue)
            If info IsNot Nothing Then
                Presentations = info.CodeName
                'ShowStabilityHours(info.RequireStability.GetValueOrDefault)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del dci
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleDCI_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDCI.EditValueChanged
        If INDsleDCI.EditValue IsNot Nothing AndAlso AllowChangeSearch Then
            INDsleATCEntity.Properties.DataSource = Nothing
            INDsleATCEntity.EditValue = Nothing
            INDsleATCEntity.Properties.NullText = String.Empty

            INDslePharmacologicalGroup.EditValue = Nothing
            INDslePharmacologicalGroup.Properties.NullText = String.Empty

            ConcentrationQuantity = 0

            If _atc IsNot Nothing AndAlso _atc.ATCConcentrationByDCI IsNot Nothing AndAlso _atc.ATCConcentrationByDCI.Count > 0 Then
                For Each item In _atc.ATCConcentrationByDCI
                    If item.Id > 0 Then
                        If listDeleteATCConcentrationByDCI Is Nothing Then
                            listDeleteATCConcentrationByDCI = New List(Of ATCConcentrationByDCI)
                        End If
                        listDeleteATCConcentrationByDCI.Add(item)
                    End If
                Next

                _atc.ATCConcentrationByDCI.Clear()
            End If

            Dim info = _presenter.GetDCIById(INDsleDCI.EditValue)
            If info IsNot Nothing Then
                INDrgCombinedMedicine.EditValue = info.Combined
                INDrgCombinedMedicine_EditValueChanged(Nothing, Nothing)

                If _atc.ATCConcentrationByDCI Is Nothing Then
                    _atc.ATCConcentrationByDCI = New Domain.Entities.TrackableCollection(Of ATCConcentrationByDCI)
                End If

                For Each drugActiveXpo In info.DrugActivesParents
                    Dim item = New ATCConcentrationByDCI With {
                        .DCIId = drugActiveXpo.DCIId.Id,
                        .DCICodeName = drugActiveXpo.DCIId.CodeName
                    }
                    If Me.MeasureUnitConcentrationId IsNot Nothing Then
                        item.ConcentrationMeasureUnitId = Me.MeasureUnitConcentrationId
                        item.Name = INDSleMeasureUnitConcentration.Text
                    End If
                    _atc.ATCConcentrationByDCI.Add(item)
                Next

                INDGcConcentrationOfSubstance.DataSource = _atc.ATCConcentrationByDCI
                INDGcConcentrationOfSubstance.RefreshDataSource()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del campo unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMeasureUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMeasureUnit.EditValueChanged
        If WeightMeasureUnit IsNot Nothing Then
            Dim comboBox As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
            Dim rowHandle = comboBox.Properties.View.FocusedRowHandle
            SelectedWeightAbbreviation = CType(comboBox.Properties.View.GetRowCellValue(rowHandle, "Abbreviation"), String)

            If FormulationType = eFormuleType.Weight Then
                Concentration = $"{Weight} {SelectedWeightAbbreviation}"
            ElseIf FormulationType = eFormuleType.WeightVolumen Then
                changeMedicamentTotalConcentration()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del campo unidad de volumen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMeasureUnitVolume_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMeasureUnitVolume.EditValueChanged
        If VolumeMeasureUnit IsNot Nothing Then
            Dim comboBox As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
            Dim rowHandle = comboBox.Properties.View.FocusedRowHandle
            SelectedVolumeAbbreviation = CType(comboBox.Properties.View.GetRowCellValue(rowHandle, "Abbreviation"), String)
            If FormulationType = eFormuleType.Volumen Then
                Concentration = $"{Volume} {SelectedVolumeAbbreviation}"
            ElseIf FormulationType = eFormuleType.WeightVolumen Then
                changeMedicamentTotalConcentration()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento usado para actualizar el campo concetración total medicamento
    ''' </summary>
    Private Sub changeMedicamentTotalConcentration()
        Concentration = $"{Weight} {SelectedWeightAbbreviation} / {Volume} {SelectedVolumeAbbreviation}"
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del control unidad de medida y actualiza el valor del campo concentración total medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMeasureUnitConcentration_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMeasureUnitConcentration.EditValueChanged
        If FormulationType = eFormuleType.AdministrationUnit AndAlso (String.IsNullOrEmpty(Concentration) Or MeasureUnitConcentrationId IsNot Nothing) Then
            Dim comboBox As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
            Dim rowHandle = comboBox.Properties.View.FocusedRowHandle
            SelectedAdminUnitAbbreviation = CType(comboBox.Properties.View.GetRowCellValue(rowHandle, "Abbreviation"), String)
            Concentration = $"{ConcentrationQuantity} {SelectedAdminUnitAbbreviation}"
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del campo concentración y actualiza el valor de concentración total medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeConcentrationQuantity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeConcentrationQuantity.EditValueChanged
        If FormulationType = eFormuleType.AdministrationUnit AndAlso (String.IsNullOrEmpty(Concentration) Or ConcentrationQuantity > 0) Then
            Concentration = $"{ConcentrationQuantity} {SelectedAdminUnitAbbreviation}"
        End If
    End Sub

    ''' <summary>
    ''' Evento que maneja el cambio de tipo de formulación y ajusta visibilidad y valores según el tipo seleccionado.
    ''' </summary>
    Private Sub INDgleFormulaType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleFormulaType.EditValueChanged
        If FormulationType = 0 Then Exit Sub

        INDliWeightATC.HideLayout
        INDliMeasureUnit.HideLayout
        INDliVolumeAmountATC.HideLayout
        INDliMeasureUnitVolume.HideLayout
        INDliAdministrationUnit.HideLayout
        INDlySuitableForReconstitution.HideLayout
        INDliConcentrationQuantity.ShowLayout()
        INDliMeasureUnitConcentration.ShowLayout()

        Weight = Nothing
        WeightMeasureUnit = Nothing
        Volume = Nothing
        VolumeMeasureUnit = Nothing
        AdministrationUnitId = Nothing
        SelectedWeightAbbreviation = String.Empty
        SelectedVolumeAbbreviation = Nothing
        MeasureUnitConcentrationId = Nothing
        Concentration = String.Empty

        INDsleMeasureUnit.Properties.NullText = String.Empty
        INDsleMeasureUnitVolume.Properties.NullText = String.Empty
        INDSleMeasureUnitConcentration.Properties.NullText = String.Empty

        ' Mostrar controles específicos según el tipo de formulación
        Select Case FormulationType
            Case eFormuleType.Weight
                INDliWeightATC.ShowLayout
                INDliMeasureUnit.ShowLayout
                INDlySuitableForReconstitution.ShowLayout
                INDSleMeasureUnitConcentration.ReadOnly = True
                INDliConcentrationQuantity.HideControl()
                INDliMeasureUnitConcentration.HideControl()

            Case eFormuleType.Volumen
                INDliVolumeAmountATC.ShowLayout
                INDliMeasureUnitVolume.ShowLayout
                INDSleMeasureUnitConcentration.ReadOnly = True
                INDliConcentrationQuantity.HideControl()
                INDliMeasureUnitConcentration.HideControl()

            Case eFormuleType.WeightVolumen
                INDliWeightATC.ShowLayout
                INDliMeasureUnit.ShowLayout
                INDliVolumeAmountATC.ShowLayout
                INDliMeasureUnitVolume.ShowLayout
                INDSleMeasureUnitConcentration.ReadOnly = False
                INDliConcentrationQuantity.HideControl()
                INDliMeasureUnitConcentration.HideControl()

            Case eFormuleType.AdministrationUnit
                INDliAdministrationUnit.ShowLayout
                INDSleMeasureUnitConcentration.ReadOnly = False
                INDSeConcentrationQuantity.ReadOnly = False
        End Select
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDrgProductNPT control.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgProductNPT_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgProductNPT.EditValueChanged
        If ProductNPT Then
            INDliOsmolarityATC.ShowLayout()
            INDliDensityATC.ShowLayout()
            INDLciComponentType.ShowLayout()
        Else
            INDliOsmolarityATC.HideLayout()
            Osmolarity = Nothing
            INDliDensityATC.HideLayout()
            Density = Nothing
            INDLciComponentType.HideLayout()
            ComponentType = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de  Multidosis. Si se cambia a Si, el campo de requiere estabilidad solo puede ser Si
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgMultidosis_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgMultidosis.EditValueChanged
        If Multidosis Then
            INDrgStability.EditValue = True
            INDlyStability.Enabled = True
        Else
            INDrgStability.EditValue = False
            INDlyStability.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de  Estabilidad. Si se cambia a Si, el campo de estabilidad en horas se debe activar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgStability_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgStability.EditValueChanged
        ShowStabilityHours(Stability)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control de medicamento combinado, si cambia a Si, se calcula la concentración total de sustancias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrgCombinedMedicine_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgCombinedMedicine.EditValueChanged
        INDLcgConcentrationOfSubstance.HideControl(Not Combined)
        INDLciTotalSubstanceConcentration.HideControl(Not Combined)
        If Combined Then

            INDSeConcentrationQuantity.ReadOnly = False
            INDTxtTotalSubstanceConcentration.ReadOnly = True
            If _atc IsNot Nothing AndAlso _atc.ATCConcentrationByDCI IsNot Nothing Then
                For Each atcConcentrationByDCI In _atc.ATCConcentrationByDCI
                    TotalSubstanceConcentrationCombination += atcConcentrationByDCI.Concentration
                    MeasurementUnitAbbreviation = atcConcentrationByDCI.Abbreviation.FirstOrDefault
                Next
                TotalSubstanceConcentration = $"{TotalSubstanceConcentrationCombination} {MeasurementUnitAbbreviation}"
            End If
        Else
            INDSeConcentrationQuantity.ReadOnly = False
            INDSleMeasureUnitConcentration.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del control concentración y actualiza el campo concentración total medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeConcentration_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeConcentration.EditValueChanged
        If _atc.ATCConcentrationByDCI IsNot Nothing Then
            Dim row = INDGvConcentrationOfSubstance.GetFocusedObject(Of Domain.Entities.ATCConcentrationByDCI)()
            row.Concentration = CType(sender, DevExpress.XtraEditors.SpinEdit).EditValue
            TotalSubstanceConcentrationCombination = _atc.ATCConcentrationByDCI.Sum(Function(d) d.Concentration)

            TotalSubstanceConcentration = $"{TotalSubstanceConcentrationCombination} {MeasurementUnitAbbreviation}"
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor de la rejilla "Unidad de medida", actualiza todos los registros de la rejilla y el campo concentración total sustancias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeMeasurementUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeMeasurementUnit.EditValueChanged

        Dim edit As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim selectedObject As Infrastructure.Data.Xpo.InventoryRepository.MeasureUnitXpo = CType(edit.GetSelectedDataRow(), Infrastructure.Data.Xpo.InventoryRepository.MeasureUnitXpo)
        If selectedObject IsNot Nothing Then

            Dim selectedValueMeasure As String = selectedObject.Name
            Dim selectedId As Integer = selectedObject.Id
            MeasurementUnitAbbreviation = selectedObject.Abbreviation

            If _atc IsNot Nothing AndAlso _atc.ATCConcentrationByDCI IsNot Nothing Then
                For Each atcConcentrationByDCI In _atc.ATCConcentrationByDCI
                    atcConcentrationByDCI.ConcentrationMeasureUnitId = selectedId
                    atcConcentrationByDCI.Name = selectedValueMeasure
                Next
                For i As Integer = 0 To INDGvConcentrationOfSubstance.RowCount - 1
                    INDGvConcentrationOfSubstance.SetRowCellValue(i, "Name", selectedId)
                Next
                INDGcConcentrationOfSubstance.RefreshDataSource()

                TotalSubstanceConcentration = $"{TotalSubstanceConcentrationCombination} {MeasurementUnitAbbreviation}"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del control peso y actualiza el valor del campo concentracion total medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtWeightATC_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtWeightATC.EditValueChanged
        If FormulationType = eFormuleType.Weight Then
            Concentration = $"{Weight} {SelectedWeightAbbreviation}"
        ElseIf FormulationType = eFormuleType.WeightVolumen Then
            Concentration = $"{Weight} {SelectedWeightAbbreviation} / {Volume} {SelectedVolumeAbbreviation}"
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del control volumen y actualiza el valor del campo concentracion total medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtVolumeAmountATC_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtVolumeAmountATC.EditValueChanged
        If FormulationType = eFormuleType.Volumen Then
            Concentration = $"{Volume} {SelectedVolumeAbbreviation}"
        ElseIf FormulationType = eFormuleType.WeightVolumen Then
            Concentration = $"{Weight} {SelectedWeightAbbreviation} / {Volume} {SelectedVolumeAbbreviation}"
        End If
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de patologías del control de pos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePOSProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePBSProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            If ProductPathologies Is Nothing Then
                ProductPathologies = New InventoryProduct
            End If
            Dim frmPatologies As New FrmPopUpPosPatologies(ProductPathologies)
            AddHandler frmPatologies.ReturnToDeletes, AddressOf ReturnToDeletes
            Using Model As New MBusqueda
                frmPatologies.BillingGroupNoPOSDatasource = Model.ConsultarEntidades(eDataSource.ListBillingGroup)
                frmPatologies.Patologies = Model.ConsultarEntidades(eDataSource.ListDiagnostic)
            End Using
            Using tras As New FrmTransparent(frmPatologies, False)
                tras.ShowDialog(Me)
            End Using
            If ProductPathologies IsNot Nothing AndAlso ProductPathologies.POSPathologies IsNot Nothing AndAlso ProductPathologies.POSPathologies.Count > 0 Then
                ProductPathologies.AllPOSPathologies = False
                e.Button.Appearance.ForeColor = Color.FromArgb(50, 205, 50)
                e.Button.Appearance.Options.UseForeColor = True
            Else
                ProductPathologies.AllPOSPathologies = True
                e.Button.Appearance.ForeColor = Color.Empty
                e.Button.Appearance.Options.UseForeColor = False
            End If
        End If

    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el mas del control de atc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATCEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2059, Nothing, True)
            If INDsleDCI.EditValue IsNot Nothing Then
                INDsleATCEntity.Properties.DataSource = _presenter.LoadATC(INDsleDCI.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleDCI control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleDCI_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDCI.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDCI
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeDCI()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAdministrationUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdministrationUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAdministrationUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmMeasureUnit
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeAdministrationUnit()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAdministrationRoute control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAdministrationRoute_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAdministrationRoute.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmAdministrationRoute
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeAdministrationRoute()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDslePharmacologicalGroup control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDslePharmacologicalGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePharmacologicalGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmGroupsPharmacological
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializePharmacologicalGroup()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleRiskLevel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleRiskLevel_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRiskLevel.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmInventoryRiskLevel
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeRiskLevel()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleMeasureUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleMeasureUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasureUnit.ButtonClick, INDsleMeasureUnitVolume.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmMeasureUnit
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeMeasureUnit()
                _presenter.InitializeMeasureUnitVolumen()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de forma farmaceutica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePharmaceuticalForm_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlePharmaceuticalForm.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPharmaceuticalForm With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializePharmaceuticalForm()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddAdministrationRoute_Click(sender As Object, e As EventArgs) Handles INDSbAddAdministrationRoute.Click
        If String.IsNullOrEmpty(INDsleAdministrationRoute.EditValue) OrElse INDsleAdministrationRoute.EditValue = 0 Then
            MessageIndigo.Show("Seleccione vía de administración", MessageType.Information, Me.Text)
            Exit Sub
        ElseIf Me._atc.ATCAdministrationRoute Is Nothing Then
            Me._atc.ATCAdministrationRoute = New Domain.Entities.TrackableCollection(Of ATCAdministrationRoute)
        End If

        If Me._atc.ATCAdministrationRoute.Where(Function(d) d.AdministrationRouteId = INDsleAdministrationRoute.EditValue).FirstOrDefault Is Nothing Then
            Dim _ATCAdministrationRoute As New ATCAdministrationRoute
            Dim _AdministrationRoute = _presenter.GetAdministrationRouteById(INDsleAdministrationRoute.EditValue)
            Dim _AdministrationRouteAux As New AdministrationRoute
            _AdministrationRouteAux.Id = INDsleAdministrationRoute.EditValue
            _AdministrationRouteAux.Code = _AdministrationRoute.Code
            _AdministrationRouteAux.Name = _AdministrationRoute.Name
            _AdministrationRouteAux.StopTracking
            With _ATCAdministrationRoute
                .Id = 0
                .AdministrationRouteId = INDsleAdministrationRoute.EditValue
                .ATCId = Me._atc.Id
                .AdministrationRoute = _AdministrationRouteAux
            End With
            Me._atc.ATCAdministrationRoute.Add(_ATCAdministrationRoute)
            INDGvAdministrationRoute.BeginDataUpdate()
            INDGcAdministrationRoute.DataSource = Me._atc.ATCAdministrationRoute
            INDGvAdministrationRoute.EndDataUpdate()
        Else
            MessageIndigo.Show("Ya se agrego la vía de administración", MessageType.Warning, Me.Text)
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        Dim btnEdith As DevExpress.XtraEditors.ButtonEdit
        Dim _btnTag As String = String.Empty
        btn = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
        If btn Is Nothing Then
            btnEdith = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If btnEdith IsNot Nothing Then
                _btnTag = btnEdith.Text
            End If
        Else
            _btnTag = btn.Tag
        End If

        Select Case _btnTag
            Case "Remove", "Eliminar"

                RemoveDetailATCAdministrationRoute()

        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de asocia insumos/medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleHasSupplieMedicine_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleHasSupplieMedicine.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            If _atc Is Nothing Then
                _atc = New ATC With {.Status = True}
            End If
            Dim _frmPopupSupplieMedicine As New FrmPopupSupplieMedicine(_atc)
            Using tras As New FrmTransparent(_frmPopupSupplieMedicine, False)
                tras.ShowDialog(Me)
            End Using
        End If
    End Sub

    Private Sub INDsleUPRUnits_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUPRUnits.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmUPRUnits With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            _presenter.InitializeUPRUnits()
        End If
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove"
                RemoveDetailATCAdministrationRoute()
        End Select
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgleMedicationLiner_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleMedicationLiner.EditValueChanged
        If INDgleMedicationLiner.EditValue Is Nothing OrElse Not IndicatorDrug Then
            INDLciAntibiotico.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAntibiotico.AllowHide = True
            INDLciRequiereJuntaMedica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciRequiereJuntaMedica.AllowHide = True
            INDLciAltoCosto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAltoCosto.AllowHide = True
            Antibiotico = False
            RequiereJuntaMedica = False
            AltoCosto = False
        Else
            INDLciAntibiotico.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAntibiotico.AllowHide = False
            INDLciRequiereJuntaMedica.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciRequiereJuntaMedica.AllowHide = False
            INDLciAltoCosto.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAltoCosto.AllowHide = False
            If Antibiotico Is Nothing Then Antibiotico = False
            If RequiereJuntaMedica Is Nothing Then RequiereJuntaMedica = False
            If AltoCosto Is Nothing Then AltoCosto = False
        End If
    End Sub
#End Region

#End Region

#Region "Methods and Variables"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingInventory Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
                Deshacer()
                Exit Function
            End If
        End Using
    End Function

    ''' <summary>
    '''      
    ''' 
    ''' </summary>
    Private Sub RemoveDetailATCAdministrationRoute()
        Dim _ATCAdministrationRoute As ATCAdministrationRoute = INDGvAdministrationRoute.GetRow(INDGvAdministrationRoute.FocusedRowHandle)
        If _ATCAdministrationRoute IsNot Nothing Then
            _ATCAdministrationRoute.MarkAsDeleted()
            Me._atc.ATCAdministrationRoute.Remove(_ATCAdministrationRoute)

            If _ATCAdministrationRoute.Id > 0 Then
                If ListDeleteATCAdministrationRoute Is Nothing Then
                    ListDeleteATCAdministrationRoute = New List(Of ATCAdministrationRoute)
                End If
                ListDeleteATCAdministrationRoute.Add(_ATCAdministrationRoute)
            End If

            INDGcAdministrationRoute.DataSource = Nothing
            INDGcAdministrationRoute.DataSource = Me._atc.ATCAdministrationRoute
        End If
    End Sub

    ''' <summary>
    ''' Metodo que recibe la entidad eliminada de las patologias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnToDeletes(sender As Object, e As ReturnToDeletes)
        If ListDeletePahotlogies Is Nothing Then
            ListDeletePahotlogies = New List(Of POSPathologies)
        End If
        ListDeletePahotlogies.Add(e.POSPathologiesDelete)
    End Sub

    ''' <summary>
    ''' Creates the type of the list formulation.
    ''' </summary>
    Private Sub createListFormulationType()
        ListFormulationType = New List(Of Tuple(Of Integer, String))()
        ListFormulationType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("FormulationTypeWeight", MODULE_NAME)))
        ListFormulationType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("FormulationTypeVolume", MODULE_NAME)))
        ListFormulationType.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("FormulationTypeWeightVolume", MODULE_NAME)))
        ListFormulationType.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("FormulationTypeAdministrationUnit", MODULE_NAME)))
        INDgleFormulaType.Properties.DataSource = ListFormulationType

        ListYesNo = New List(Of Tuple(Of Boolean, String))()
        ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDslePBSProduct.Properties.DataSource = ListYesNo
        INDsleConsumption.Properties.DataSource = ListYesNo
        INDGleAntibiotico.Properties.DataSource = ListYesNo
        INDGleHasSupplieMedicine.Properties.DataSource = ListYesNo
        INDGleRequiereJuntaMedica.Properties.DataSource = ListYesNo
        INDGleAltoCosto.Properties.DataSource = ListYesNo
        INDSleConditioned.Properties.DataSource = ListYesNo
        INDSleUNIRS.Properties.DataSource = ListYesNo

        Dim ListComponentType = New List(Of Tuple(Of Byte, String))
        ListComponentType.Add(New Tuple(Of Byte, String)(1, "Macronutriente"))
        ListComponentType.Add(New Tuple(Of Byte, String)(2, "Micronutriente"))
        INDSleComponentType.Properties.DataSource = ListComponentType.ToList()

        Dim ListDataTypes = New List(Of Tuple(Of Integer, String))
        ListDataTypes.Add(New Tuple(Of Integer, String)(1, "Advertencias"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(2, "Posología"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(3, "Indicaciones"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(4, "Contraindicaciones"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(5, "Precauciones"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(6, "Reacciones Adversas"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(7, "Indicaciones no farmacológicas"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(8, "Información al paciente"))
        ListDataTypes.Add(New Tuple(Of Integer, String)(9, "Laboratorio(s) de control"))
        INDsleDataType.Properties.DataSource = ListDataTypes.ToList()
        INDrpiDataType.DataSource = ListDataTypes.ToList()

        Dim ListFrequency = New List(Of Tuple(Of Integer, String))
        ListFrequency.Add(New Tuple(Of Integer, String)(1, "Hora(s)"))
        ListFrequency.Add(New Tuple(Of Integer, String)(2, "Día(s)"))
        ListFrequency.Add(New Tuple(Of Integer, String)(3, "Semana(s)"))
        ListFrequency.Add(New Tuple(Of Integer, String)(4, "Mes"))
        INDseFrequency.Properties.DataSource = ListFrequency.ToList()

        'Se setea por defecto la opción "Día(s) como predeterminada"
        Frequency = 2
    End Sub

    ''' <summary>
    ''' News the atc.
    ''' </summary>
    Private Async Function NewATC() As Task

        If Me._settingInventory Is Nothing OrElse Me._settingInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", MODULE_NAME)
            Exit Function
        End If
        _atc = New ATC() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Abreviación", .FieldName = "AbbreviationName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Concentración", .FieldName = "Concentration", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListATC
            .FormParent = Me
            .ShowSearch()
        End With
        ' _searchMode = True
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IATC.ActionsOnControls
        Set(value As Boolean)
            Me.SuspendLayout()
            Me.ActiveControl = Nothing
            Me.AutoScroll = False
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtATCName.Enabled = value
            INDsleDCI.Enabled = value
            INDsleATCEntity.Enabled = value
            INDtxtAbbreviation.Enabled = value
            INDsleAdministrationRoute.Enabled = value
            INDslePharmacologicalGroup.Enabled = value
            INDsleRiskLevel.Enabled = value
            INDspMinimumHourStability.Enabled = value
            INDspMaximumHourStability.Enabled = value
            INDgleFormulaType.Enabled = value
            INDslePBSProduct.Enabled = value
            INDtxtWeightATC.Enabled = value
            INDsleMeasureUnit.Enabled = value
            INDtxtVolumeAmountATC.Enabled = value
            INDsleMeasureUnitVolume.Enabled = value
            INDsleAdministrationUnit.Enabled = value
            INDtxtConcentration.Enabled = value
            INDpceOptions.Enabled = value
            INDgleAutomaticCalc.Enabled = value
            INDglePerformsTransfersSurplus.Enabled = value
            INDgleDiluentWorks.Enabled = value
            INDsleSupplieProduct.Enabled = value
            INDgleJustifiesSpecialMedications.Enabled = value
            INDgleInputsJustifies.Enabled = value
            INDgleMedicationLiner.Enabled = value
            INDTxtPresentation.Enabled = value
            INDrgProductNPT.Enabled = value
            INDsleConsumption.Enabled = value
            INDSbAddAdministrationRoute.Enabled = value
            INDGvAdministrationRoute.OptionsBehavior.ReadOnly = Not value
            INDSlePharmaceuticalForm.Enabled = value
            INDGleHasSupplieMedicine.Enabled = value
            INDGleAntibiotico.Enabled = value
            INDGleRequiereJuntaMedica.Enabled = value
            INDGleAltoCosto.Enabled = value
            INDSleConditioned.Enabled = value
            INDSleUNIRS.Enabled = value
            INDrgMultidosis.Enabled = value
            INDsleUPRUnits.Enabled = value
            If Multidosis Then
                INDrgStability.Enabled = True
            Else
                INDrgStability.Enabled = False
            End If
            INDrgSuitableForReconstitution.Enabled = value
            INDtxtOsmolarityATC.Enabled = value
            INDtxtDensityATC.Enabled = value

            INDlcRoot.EndUpdate()
            Me.AutoScroll = True
            Me.ResumeLayout()

            If value Then
                INDtxtATCName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Limpia todos los controles y variables del formulario, evitando parpadeo visual.
    ''' </summary>
    Private Sub CleanControls()
        Me.SuspendLayout()
        Me.ActiveControl = Nothing
        Me.AutoScroll = False
        INDlcRoot.BeginUpdate()

        Try
            ' Bloquea acciones en controles y resetea la barra de botones
            ActionsOnControls = False
            Me.BarraBotones.StatusRecordVisible = False
            Me.BarraBotones.StatusRecord = Nothing
            Me.BarraBotones.EnableBarItems()
            Me.BarraBotones.DisableBarDocument()
            Me.BarraBotones.CleanAuditBasic()
            Me.BarraBotones.ReassignOperatingUnit()

            ' Reseteo de variables y propiedades del formulario
            _doc = Nothing
            _atc = Nothing
            ProductPathologies = Nothing
            POSProduct = Nothing
            Status = True
            Code = String.Empty
            DCIId = Nothing
            ATCName = Nothing
            AbbreviationName = Nothing
            AdministrationRouteId = Nothing
            PharmacologicalGroupId = Nothing
            Concentration = String.Empty
            InventoryRiskLevelId = Nothing
            StabilityMaximumHours = Nothing
            StabilityMinimumHours = Nothing
            FormulationType = Nothing
            Weight = Nothing
            WeightMeasureUnit = Nothing
            Volume = Nothing
            VolumeMeasureUnit = Nothing
            AdministrationUnitId = Nothing
            AutomaticCalculation = Nothing
            TransferSurplusProduct = Nothing
            DiluentProduct = Nothing
            JustificationForSpecialDrugs = Nothing
            JustificationOfInputs = Nothing
            IndicatorDrug = Nothing
            Presentations = Nothing
            ProductNPT = False
            ComponentType = Nothing
            Multidosis = False
            Combined = False
            Stability = False
            SuitableForReconstitution = False
            Consumption = Nothing
            Osmolarity = 0
            Density = 0
            ConcentrationQuantity = 0
            MeasureUnitConcentrationId = Nothing
            UPRUnitId = Nothing
            IdPharmaceuticalForm = Nothing
            HasSupplieMedicine = Nothing
            Antibiotico = Nothing
            RequiereJuntaMedica = Nothing
            AltoCosto = Nothing
            Conditioned = False
            UNIRS = False
            TotalSubstanceConcentration = Nothing
            TotalSubstanceConcentrationCombination = Nothing
            MeasurementUnitAbbreviation = String.Empty
            SelectedWeightAbbreviation = String.Empty
            SelectedVolumeAbbreviation = String.Empty
            DataType = Nothing
            Description = String.Empty
            ControlLaboratoryId = Nothing
            TimeRequest = Nothing
            Frequency = Nothing
            DiagnosisId = Nothing
            CurrentClinicalData = Nothing

            ' Limpiar estados de pop-ups
            _stateOpenPopUpDCI = False
            _stateOpenPopUpAdministrationRoute = False
            _stateOpenPopUpPharmacologicalGroup = False
            _stateOpenPopUpRiskLevel = False
            _stateOpenPopUpMeasureUnit = False
            _stateOpenPopUpMeasureUnitVolumen = False
            _stateOpenPopUpAdministrationUnit = False
            _stateOpenPopUpDiagnostic = False

            ' Limpiar controles visuales
            INDsleATCEntity.EditValue = Nothing
            INDsleSupplieProduct.EditValue = Nothing
            INDsleUPRUnits.EditValue = Nothing
            INDsleATCEntity.Properties.NullText = String.Empty
            INDslePBSProduct.Properties.NullText = String.Empty
            INDSleMeasureUnitConcentration.Properties.NullText = String.Empty
            INDSlePharmaceuticalForm.Properties.NullText = String.Empty
            INDsleAdministrationRoute.Properties.NullText = String.Empty
            INDslePharmacologicalGroup.Properties.NullText = String.Empty
            INDsleRiskLevel.Properties.NullText = String.Empty
            INDsleMeasureUnit.Properties.NullText = String.Empty
            INDsleMeasureUnitVolume.Properties.NullText = String.Empty
            INDsleAdministrationUnit.Properties.NullText = String.Empty
            INDsleDCI.Properties.NullText = String.Empty
            INDsleUPRUnits.Properties.NullText = String.Empty
            INDGleHasSupplieMedicine.Properties.NullText = String.Empty
            INDGleAntibiotico.Properties.NullText = String.Empty
            INDGleRequiereJuntaMedica.Properties.NullText = String.Empty
            INDGleAltoCosto.Properties.NullText = String.Empty

            ' Ocultar secciones visuales
            INDliWeightATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDliMeasureUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDliVolumeAmountATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDliMeasureUnitVolume.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDliAdministrationUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            ' Eliminar botones de patologías PBS si existen
            Dim info = INDslePBSProduct.Properties.Buttons _
            .OfType(Of DevExpress.XtraEditors.Controls.EditorButton)() _
            .FirstOrDefault(Function(btn) btn.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)

            If info IsNot Nothing Then
                INDslePBSProduct.Properties.Buttons.RemoveAt(info.Index)
            End If

            ' Limpiar datasources
            INDGcAdministrationRoute.DataSource = Nothing
            INDGcConcentrationOfSubstance.DataSource = Nothing
            INDSeMeasurementUnit.DataSource = Nothing
            INDGcOptions.DataSource = Nothing

            ' Inicializar listas de eliminación
            ListDeleteATCAdministrationRoute = Nothing
            listDeleteATCConcentrationByDCI = Nothing
            ListDeletePahotlogies = Nothing
            ListDeleteClinicalData = New List(Of ATCClinicalData)

            ' Reestablecer barra de botones
            If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            Else
                Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            End If

            ' Liberar bloqueo si existiera
            DeleteBlockedRecord()

        Finally
            INDlcRoot.EndUpdate()
            Me.AutoScroll = True
            Me.ResumeLayout()
        End Try
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _atc
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DCIId = DCIId
            .ATCEntityId = INDsleATCEntity.EditValue
            .Name = ATCName
            .AbbreviationName = AbbreviationName
            .PharmacologicalGroupId = PharmacologicalGroupId
            .Presentations = Presentations
            .Concentration = Concentration
            .InventoryRiskLevelId = InventoryRiskLevelId
            .StabilityMaximumHours = StabilityMaximumHours
            .StabilityMinimumHours = 0
            .FormulationType = FormulationType
            .Weight = Weight
            .WeightMeasureUnit = WeightMeasureUnit
            .Volume = Volume
            .VolumeMeasureUnit = VolumeMeasureUnit
            .AdministrationUnitId = AdministrationUnitId
            .Combined = Combined
            .AutomaticCalculation = AutomaticCalculation
            .TransferSurplusProduct = TransferSurplusProduct
            .DiluentProduct = DiluentProduct
            .SupplieProduct = INDsleSupplieProduct.EditValue
            .JustificationForSpecialDrugs = JustificationForSpecialDrugs
            .JustificationOfInputs = JustificationOfInputs
            .IndicatorDrug = IndicatorDrug
            .ProductNPT = ProductNPT
            .ComponentType = ComponentType
            If INDlyItemPBSProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                .POSProduct = True
            Else
                .POSProduct = POSProduct
            End If
            .Consumption = Consumption
            .PharmaceuticalFormId = IdPharmaceuticalForm
            .HasSupplieMedicine = HasSupplieMedicine
            .Antibiotic = Antibiotico
            .RequireMedicalBoard = RequiereJuntaMedica
            .HighCost = AltoCosto
            .Conditioned = Conditioned
            .UNIRS = UNIRS
            .Stability = Stability
            .Multidose = Multidosis
            .SuitableForReconstitution = SuitableForReconstitution
            .ConcentrationQuantity = ConcentrationQuantity
            .ConcentrationMeasureUnitId = MeasureUnitConcentrationId
            .UPRUnitsId = UPRUnitId
            .TotalSubstanceConcentration = TotalSubstanceConcentration

            If POSProduct IsNot Nothing AndAlso POSProduct Then 'Si el producto es pos
                If ProductPathologies Is Nothing Then
                    ProductPathologies = New InventoryProduct
                    ProductPathologies.AllPOSPathologies = True
                End If

                .AllPOSPathologies = ProductPathologies.AllPOSPathologies
                .BillingGroupNoPosId = ProductPathologies.BillingGroupNoPosId
                .DefineProfessional = ProductPathologies.DefineProfessional
                .ClinicalJustification = ProductPathologies.ClinicalJustification
                .ProductPathologies = ProductPathologies

                If ListDeletePahotlogies IsNot Nothing AndAlso ListDeletePahotlogies.Count > 0 Then
                    ListDeletePahotlogies.ForEach(Sub(item) ProductPathologies.POSPathologies.Add(item.MarkAsDeleted()))
                End If
            Else 'Si el producto es no pos
                .AllPOSPathologies = Nothing
                .BillingGroupNoPosId = Nothing
                .ProductPathologies = Nothing
                .DefineProfessional = False
                .ClinicalJustification = Nothing
            End If

            If ListDeleteATCAdministrationRoute IsNot Nothing AndAlso ListDeleteATCAdministrationRoute.Count > 0 Then
                For Each itemdetail As ATCAdministrationRoute In ListDeleteATCAdministrationRoute
                    .ATCAdministrationRoute.Add(itemdetail)
                Next
            End If

            If ListDeleteClinicalData IsNot Nothing AndAlso ListDeleteClinicalData.Count > 0 Then
                For Each item As ATCClinicalData In ListDeleteClinicalData
                    .ATCClinicalData.Add(item)
                Next
            End If

        End With

        If listDeleteATCConcentrationByDCI IsNot Nothing AndAlso listDeleteATCConcentrationByDCI.Count > 0 Then
            For Each item In listDeleteATCConcentrationByDCI
                _atc.ATCConcentrationByDCI.Add(item.MarkAsDeleted())
            Next
        End If
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._atc.Code, Me._atc.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._atc.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._atc.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._atc.Code, Me._atc.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._atc.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Carga los controles del formulario según el código actual y los datos obtenidos del modelo ATC.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If String.IsNullOrWhiteSpace(Code) Then Exit Function

        If Not Me.BarraBotones.PermiteConsultar Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Try
            Using model As New MATC(CStr(Me.Tag))
                AsyncLoader(True)

                ' Carga asincrónica de datos ATC
                Dim resultOperation = Await model.GetATC(Me.Code)
                _atc = resultOperation.ObjectEmbbeded

                If _atc Is Nothing OrElse _atc.Id = 0 Then
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewATC()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Code = String.Empty
                        INDbteCode.Focus()
                    End If
                    Return
                End If

                Me.SuspendLayout()
                Me.ActiveControl = Nothing
                Me.AutoScroll = False
                INDlcRoot.BeginUpdate()

                Me.BarraBotones.StatusRecordVisible = True

                ' Bloqueo de registro
                Using modelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                    _record = Await modelRecord.GetBlockRecord(Me.Tag.ToString(), _atc.Id)

                    If _record.Id = 0 Then
                        _record = (Await modelRecord.SaveBlockRecord(New BlockRecordInventory With {
                        .BlockDate = Date.Now,
                        .NameUser = Me.indigo.UserIndigoName,
                        .CodUser = Me.indigo.UserIndigo,
                        .FormId = Me.Tag,
                        .RecordId = _atc.Id,
                        .ChangeTracker = New ObjectChangeTracker With {.State = ObjectState.Added}
                    })).ObjectEmbbeded
                    Else
                        Dim xtraMsg = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMsg, ImagesXtraLabel.Warning, _record.CodUser)
                    End If
                End Using

                ' Auditoría
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), _atc.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), _atc.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), _atc.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), _atc.ModificationDate)

                LoadATCToControls(_atc)
                LoadPathologies()

                ' Documentos y acciones
                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._atc.Code)
                Me.BarraBotones.SetDocuments(_atc.Id, Me.Tag.ToString(), Nothing, GetType(ATC).Name)
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

                AsyncLoader(False)
                ActionsOnControls = True

            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw
        Finally
            INDlcRoot.EndUpdate()
            Me.AutoScroll = True
            Me.ResumeLayout()
        End Try
    End Function

    ''' <summary>
    ''' Asigna los valores del objeto ATC a los controles del formulario.
    ''' </summary>
    ''' <param name="atc">Entidad ATC cargada desde el modelo</param>
    Private Sub LoadATCToControls(atc As ATC)
        LayoutControls.SetCustomFieldsValue(atc.CustomProperties)

        SelectedAdminUnitAbbreviation = atc.ATCMeasureAbbreviation
        Code = atc.Code
        AllowChangeSearch = False
        DCIId = atc.DCIId
        INDsleATCEntity.EditValue = atc.ATCEntityId
        INDsleATCEntity.Properties.NullText = atc.NullTextATCEntity

        ATCName = atc.Name
        AbbreviationName = atc.AbbreviationName
        PharmacologicalGroupId = atc.PharmacologicalGroupId
        Presentations = atc.Presentations
        InventoryRiskLevelId = atc.InventoryRiskLevelId
        StabilityMaximumHours = atc.StabilityMaximumHours
        StabilityMinimumHours = atc.StabilityMinimumHours
        FormulationType = atc.FormulationType
        Weight = atc.Weight
        WeightMeasureUnit = atc.WeightMeasureUnit
        Volume = atc.Volume
        VolumeMeasureUnit = atc.VolumeMeasureUnit
        AdministrationUnitId = atc.AdministrationUnitId
        Combined = atc.Combined
        ConcentrationQuantity = atc.ConcentrationQuantity

        INDSleMeasureUnitConcentration.Properties.NullText = atc.NullTextUnitMeasureConcentration
        MeasureUnitConcentrationId = atc.ConcentrationMeasureUnitId
        Concentration = atc.Concentration
        AutomaticCalculation = atc.AutomaticCalculation
        TransferSurplusProduct = atc.TransferSurplusProduct
        DiluentProduct = atc.DiluentProduct

        INDsleSupplieProduct.EditValue = atc.SupplieProduct
        JustificationForSpecialDrugs = atc.JustificationForSpecialDrugs
        JustificationOfInputs = atc.JustificationOfInputs
        IndicatorDrug = atc.IndicatorDrug
        Status = atc.Status
        POSProduct = atc.POSProduct
        ProductNPT = atc.ProductNPT
        Consumption = atc.Consumption

        If atc.ProductNPT Then
            Osmolarity = atc.Osmolarity
            Density = atc.Density
            ComponentType = atc.ComponentType
        End If

        If atc.Combined Then
            TotalSubstanceConcentration = atc.TotalSubstanceConcentration
        End If

        IdPharmaceuticalForm = atc.PharmaceuticalFormId
        INDSlePharmaceuticalForm.Properties.NullText = atc.PharmaceuticalFormDescription

        HasSupplieMedicine = atc.HasSupplieMedicine.GetValueOrDefault()
        Antibiotico = atc.Antibiotic.GetValueOrDefault()
        RequiereJuntaMedica = atc.RequireMedicalBoard.GetValueOrDefault()
        AltoCosto = atc.HighCost.GetValueOrDefault()

        Conditioned = atc.Conditioned
        UNIRS = atc.UNIRS
        Multidosis = atc.Multidose
        Stability = atc.Stability
        SuitableForReconstitution = atc.SuitableForReconstitution

        INDlyStability.Enabled = atc.Stability

        ListATCAdministrationRoute = atc.ATCAdministrationRoute.ToList()
        UPRUnitId = atc.UPRUnitsId

        INDsleDCI.Properties.NullText = atc.NullTextDCI
        INDslePharmacologicalGroup.Properties.NullText = atc.NullTextPharmacologicalGroup
        INDsleRiskLevel.Properties.NullText = atc.NullTextRiskLevel
        INDsleMeasureUnit.Properties.NullText = atc.NullTextUnitMeasure
        INDsleMeasureUnitVolume.Properties.NullText = atc.NullTextUnitMeasureVolumen
        INDsleAdministrationUnit.Properties.NullText = atc.NullTextUnitAdministration
        INDSeMeasurementUnit.NullText = atc.ATCConcentrationByDCI.FirstOrDefault(Function(x) x.Name IsNot Nothing)?.Name

        INDGcAdministrationRoute.DataSource = atc.ATCAdministrationRoute
        INDGcConcentrationOfSubstance.DataSource = atc.ATCConcentrationByDCI
        INDGcOptions.DataSource = atc.ATCClinicalData

        AllowChangeSearch = True
    End Sub

    ''' <summary>
    ''' Oculta o muestra los controles de hora minima y maxima de estabilidad
    ''' </summary>
    ''' <param name="_RequireStability"></param>
    Private Sub ShowStabilityHours(ByVal _RequireStability As Boolean)
        INDliMinimumHourStability.HideLayout()
        If _RequireStability Then
            INDliMaximumHourStability.ShowLayout()
        Else
            INDliMaximumHourStability.HideLayout()
            INDspMinimumHourStability.EditValue = 0
            INDspMaximumHourStability.EditValue = 0
        End If
    End Sub

    '''' <summary>
    '''' Oculta o muestra el control de Producto PBS / Producto POS
    '''' </summary>
    Private Sub ShowPOSProduct()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDlyItemPBSProduct.HideLayout()
            POSProduct = True
        Else
            INDlyItemPBSProduct.ShowLayout()
        End If
    End Sub

    ''' <summary>
    ''' Oculta o muestra el control de UNIRS
    ''' </summary>
    Private Sub ShowUNIRS()
        If Me.indigo.Culture.Name <> "es-CO" Then
            INDLciUNIRS.HideLayout()
            UNIRS = False
        Else
            INDLciUNIRS.ShowLayout()
        End If
    End Sub

    Private Sub ShowUPRUnits()
        If indigo.LanguageCulture <> "es-CO" Then
            INDLciUPRUnits.HideControl()
            UPRUnitId = Nothing
        Else
            _presenter.InitializeUPRUnits()
        End If
    End Sub

    ''' <summary>
    ''' Carga las patologías
    ''' </summary>
    Private Sub LoadPathologies()
        ProductPathologies = New InventoryProduct
        ProductPathologies.POSProduct = _atc.POSProduct
        ProductPathologies.AllPOSPathologies = _atc.AllPOSPathologies
        ProductPathologies.BillingGroupNoPosId = _atc.BillingGroupNoPosId
        ProductPathologies.BillingGroupNoPOSDescription = _atc.BillingGroupNoPOSDescription
        ProductPathologies.DefineProfessional = _atc.DefineProfessional.GetValueOrDefault()
        ProductPathologies.ClinicalJustification = _atc.ClinicalJustification

        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim ListPathologies = _presenter.ListPathologiesByMedicamentId(_atc.Id)

                                  If Not tokenAsync.IsCancellationRequested Then
                                      INDslePBSProduct.SafeInvoke(Sub(o)
                                                                      If ListPathologies IsNot Nothing AndAlso ListPathologies.Count > 0 Then
                                                                          If o.Properties.Buttons IsNot Nothing AndAlso o.Properties.Buttons.Count > 1 _
                                                                              AndAlso o.Properties.Buttons(1).Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
                                                                              o.Properties.Buttons(1).Appearance.ForeColor = Color.FromArgb(50, 205, 50)
                                                                              o.Properties.Buttons(1).Appearance.Options.UseForeColor = True
                                                                          End If
                                                                          For Each item In ListPathologies
                                                                              Dim pathology As New POSPathologies
                                                                              With pathology
                                                                                  .Id = item.Id
                                                                                  .DiagnosticId = item.DiagnosticId.Id
                                                                                  .DiagnosticCode = item.DiagnosticId.Code
                                                                                  .DiagnosticName = item.DiagnosticId.Name
                                                                                  .MedicamentId = item.MedicamentId
                                                                                  .MinimumAge = item.MinimumAge
                                                                                  .MaximumAge = item.MaximumAge
                                                                                  .AgeMeasure = item.AgeMeasure
                                                                                  .AgeMeasureName = item.AgeMeasureName
                                                                              End With
                                                                              ProductPathologies.POSPathologies.Add(pathology.MarkAsUnchanged())
                                                                          Next
                                                                      Else
                                                                          If o.Properties.Buttons IsNot Nothing AndAlso o.Properties.Buttons.Count > 1 _
                                                                              AndAlso o.Properties.Buttons(1).Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
                                                                              o.Properties.Buttons(1).Appearance.ForeColor = Color.Empty
                                                                              o.Properties.Buttons(1).Appearance.Options.UseForeColor = False
                                                                          End If
                                                                      End If
                                                                  End Sub)
                                  End If
                              End Sub, tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Función que asigna los valores de los campos Osmolaridad y Densidad a la entidad ATC
    ''' </summary>
    Private Function ValidateOsmolarityDensity() As Integer

        Dim _Osmolarity As String = INDtxtOsmolarityATC.EditValue
        Dim _Density As String = INDtxtDensityATC.EditValue

        If _Osmolarity > 0 Then
            _atc.Osmolarity = _Osmolarity
        Else
            Return 1
        End If

        'Validación campo Densidad
        If ValidateCharactersAllowed(_Density) Then
            If (_Density.Contains(",")) Then 'Valida que tenga el separador
                If (InStr(1, Trim(_Density), ",", CompareMethod.Text) <= 3) Then
                    _atc.Density = _Density
                    Return 0
                Else
                    Return 1
                End If
            Else
                If (_Density.Length <= 2) Then
                    _atc.Density = _Density
                    Return 0
                Else
                    Return 1
                End If
            End If
        Else
            Return 2
        End If
    End Function

    ''' <summary>
    ''' Función que valida los valores ingresados en los campos de osmolaridad y densidad 
    ''' </summary>
    Private Function ValidateCharactersAllowed(cadena As String) As Boolean

        If (Trim(cadena).Count <= 7) Then 'Valida que los caracteres no sean mayor a 7 incluido el separador (,)

            Dim allowedCharacters As String = "1234567890,"
            Dim arrayAllowedCharacters = allowedCharacters.ToCharArray()
            Dim arraycadena = cadena.ToCharArray()
            Dim validate As Boolean = False

            For i As Integer = 0 To arraycadena.Count - 1
                For j As Integer = 0 To arrayAllowedCharacters.Count - 1
                    If (Not arraycadena(i).Equals(arrayAllowedCharacters(j))) Then
                        validate = False
                    Else
                        validate = True
                        Exit For
                    End If
                Next
                If (Not validate) Then
                    Return validate
                End If
            Next
            Return validate
        Else
            Return False
        End If
    End Function


#End Region

#Region "Icrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me._atc IsNot Nothing AndAlso Me._atc.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MATC(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteATC(Me._atc)
                        AsyncLoader(False)
                        If result.StateResult = True Then
                            'Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            ' _searchMode = False
                            Me.Deshacer()
                        Else
                            If result.Message IsNot Nothing Then
                                generateListError(result.Message)
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If

        If _atc.ATCAdministrationRoute Is Nothing OrElse _atc.ATCAdministrationRoute.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una vía de administración"
            Exit Sub
        End If

        'Se valida que hayan patologías si el medicamento aplica
        If INDslePBSProduct.EditValue = True AndAlso ProductPathologies IsNot Nothing AndAlso ProductPathologies.AllPOSPathologies = False AndAlso ProductPathologies.POSPathologies.Count = 0 Then
            If ValidatePOSPathologies Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una patología"
                Exit Sub
            End If
        End If

        If ProductNPT Then
            Dim mesagge As Integer = ValidateOsmolarityDensity()

            If mesagge <> 0 Then
                If mesagge = 1 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Para productos NPT la Osmolaridad debe ser superior a cero (0)"
                    Exit Sub
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Los siguientes campos son obligatorios: Osmolaridad (mOsm/L)"
                    Exit Sub
                End If
            End If
        End If

        AssigningValues()
        Try
            Using Model As New MATC(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveATC(Me._atc, Me._idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If _atc.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf _atc.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me._atc = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    '_searchMode = False
                    Me.Deshacer()
                Else
                    If Result.Message IsNot Nothing Then
                        If Result.StatusCode = eStatusResult.WARNING Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = Result.Message
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using Model As New MATC(Me.Tag)
                    AsyncLoader(True)
                    Dim _state As Boolean = Not _atc.Status
                    Dim Result = Await Model.UpdateStateATC(Me.Code, _state)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me._atc = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.MensajeError) = errors
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewATC()
        End If
    End Sub
#End Region

#Region "BarButton Events"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit

        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub
#End Region

#Region "Enums"
    Public Enum eTechnicalSheetType
        Indications = 1
        ContraIndications = 2
        Cautions = 3
        AdverseReactions = 4
    End Enum

    Public Enum eFormuleType
        Weight = 1
        Volumen = 2
        WeightVolumen = 3
        AdministrationUnit = 4
    End Enum
#End Region

End Class