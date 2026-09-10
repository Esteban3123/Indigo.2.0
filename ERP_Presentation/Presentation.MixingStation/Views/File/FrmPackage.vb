'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 06-06-2019

' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Presentation.Inventory
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmPackage
    Implements IPackage, ICustomizableForm

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddPackageArgs(sender As Object, e As SendPackageArgs)

#End Region

#Region "Fields"
    ''' <summary>
    ''' Is Loading
    ''' </summary>
    Private _isLoading As Boolean = False

    ''' <summary>
    ''' Representa a la entidad de paquete
    ''' </summary>
    Public MixinStationPackageXpo As Package

    ''' <summary>
    ''' Permite saber si es una preparación magistral personalizada
    ''' </summary>
    Public IsPersonalizedMasterPreparation As Boolean = False

    ''' <summary>
    ''' Permite saber si el formulario se esta abriendo desde la asignación de paquetes en la dashboard de confirmación de dosis unitarias
    ''' </summary>
    Public IsDashboardConfirmationUnitDose As Boolean = False

    ''' <summary>
    ''' Representa al registro al cual se le va asignar el paquete en la dashboard de confirmación de dosis unitarias
    ''' </summary>
    Public ViewListDashboardConfirmationUnitDoseXpo As ViewListDashboardConfirmationUnitDoseXpo

    ''' <summary>
    ''' Entidad UnitDoseType inicializada desde FrmAssignPackage para evitar consultas adicionales
    ''' </summary>
    Public UnitDoseTypeFromDashboard As UnitDoseType

    ''' <summary>
    ''' Obtiene la dosis solicitada desde el Dashboard de Confirmación de Dosis Unitarias
    ''' </summary>
    Public ReadOnly Property RequestedDosage As Decimal
        Get
            Return If(ViewListDashboardConfirmationUnitDoseXpo?.Dosage, 0D)
        End Get
    End Property

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Representa la entidad de Package
    ''' </summary>
    Private _packageEntity As Package

    ''' <summary>
    ''' Referencia la presentador (MixingStation.Package)
    ''' </summary>
    Private _presenter As PPackage

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As MixingStationSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' indica si el popup de RiskLevel se abre por primera vez para cargar el datasource
    ''' </summary>
    Private _openPopUpRiskLevel As Boolean

    ''' <summary>
    ''' indica si el popup del tipo de dosis unitaria por primera vez para cargar el datasource
    ''' </summary>
    Private _openPopUpUnitDoseType As Boolean

    ''' <summary>
    ''' flag para solo lectura
    ''' </summary>
    ''' <remarks></remarks>
    Private OnlyRead As Boolean = False

    ''' <summary>
    ''' Variable que contiene un ítem del detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private _itemPackageDetail As PackageDetail

    ''' <summary>
    ''' Listado de eliminados de los detalles del paquete
    ''' </summary>
    ''' <remarks></remarks>
    Private _listDeletePackageDetail As New List(Of PackageDetail)()

    ''' <summary>
    ''' Listado de los detalles del paquete
    ''' </summary>
    ''' <remarks></remarks>
    Private Property ListPackageDetail As List(Of PackageDetail)

    ''' <summary>
    ''' Listado de los detalles del paquete Antibioticoterapia
    ''' </summary>
    ''' <remarks></remarks>
    Property ListPackageDetailAntibiotic As List(Of PackageDetail)

    ''' <summary>
    ''' Listado de detalles de paquete enviado desde confirmacion de dosis unitaria
    ''' </summary>
    ''' <returns></returns>
    Property ListPackageDetailUnitDoseConfirm As List(Of PackageDetail)

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Private indexEditRecord As Integer

    ''' <summary>
    ''' Representa la entidad de Tipo de Dosis Unitaria
    ''' </summary>
    Private _unitDoseTypeEntity As UnitDoseType

    ''' <summary>
    ''' Diccionario para almacenar el listado de detalles de la tabla de estabilidad que tenga asociado el ATC
    ''' </summary>
    Private dictionaryATC As Dictionary(Of Integer, List(Of StabilityTableDetailXpo))

    ''' <summary>
    ''' Variable Permite editar el paquete
    ''' </summary>
    Private AllowsEditPackage As Boolean = True

    ''' <summary>
    ''' copia de la fila seleccionada para verificar si hubo un cambio en el medicamento principal para hacer o no de nuevo la consulta del producto terminado
    ''' </summary>
    Private _itemCopy As PackageDetail

    ''' <summary>
    ''' lista de detalle venida de dashboard de confirmacion unitaria cuando es una orden medica
    ''' </summary>
    Public ListPackageDetailXpo As List(Of MixinStationPackageDetailXpo)

    ''' <summary>
    ''' Datasource de readecuaciones
    ''' </summary>
    Private ListReadjustments As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Datasource de los tipos de etiqueta
    ''' </summary>
    Private ListFillingLabelType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Datasource de los tipos de estabilidad
    ''' </summary>
    Private ListStabilityType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Tipo de formulacion del medicamento principal
    ''' </summary>
    Private FormulationType As Integer


    ''' <summary>
    ''' Concentración del  medicamento de tipo peso - volumen
    ''' </summary>
    Private ConcentrationWeightVolumen As Decimal


#End Region

#Region "Propierties IPackage"

    ''' <summary>
    ''' Propiedad que contiene el código del registro
    ''' </summary>
    Public Property Code As String Implements IPackage.Code
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
    ''' Propiedad que contiene el nombre del registro
    ''' </summary>
    Private Property PackageName As String Implements IPackage.Name
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el estado del registro
    ''' </summary>
    Public Property State As Boolean Implements IPackage.State
        Get
            Return CBool(BarraBotones.StatusRecord)
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
    ''' Propiedad que contiene la descripción del registro
    ''' </summary>
    Public Property Description As String Implements IPackage.Description
        Get
            Return INDmeDescription.EditValue
        End Get
        Set(value As String)
            INDmeDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de paquete (producto)
    ''' </summary>
    Public Property ProductId As Integer? Implements IPackage.ProductId
        Get
            Return CType(INDsleProduct.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleProduct.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nivel de riesgo
    ''' </summary>
    Public Property InventoryRiskLevelId As Integer? Implements IPackage.InventoryRiskLevelId
        Get
            Return CType(INDsleRiskLevel.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleRiskLevel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de almacenamiento
    ''' </summary>
    Public Property Storage As Integer? Implements IPackage.Storage
        Get
            Return INDgleStorage.EditValue
        End Get
        Set(value As Integer?)
            INDgleStorage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las recaudaciones permitidas
    ''' </summary>
    Public Property Readjustments As Byte Implements IPackage.Readjustments
        Get
            Return INDsleReadjustments.EditValue
        End Get
        Set(value As Byte)
            INDsleReadjustments.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo alternativo
    ''' </summary>
    Public Property CodeAlternative As String Implements IPackage.CodeAlternative
        Get
            Return INDtxtCodeAlternative.EditValue
        End Get
        Set(value As String)
            INDtxtCodeAlternative.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo alternativo
    ''' </summary>
    Public Property ConcentrationAntibiotic As String
        Get
            Return INDtxtConcentration.EditValue
        End Get
        Set(value As String)
            INDtxtConcentration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el volumen total del preparado
    ''' </summary>
    Public Property VolumeTotalPrepared As Decimal?
        Get
            Return INDTotalVolumePrepared.EditValue
        End Get
        Set(value As Decimal?)
            INDTotalVolumePrepared.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del tipo de nutricion parenteral
    ''' </summary>
    Public Property NptId As Integer?
        Get
            Return CType(INDsleTypeNPT.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleTypeNPT.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del medicamento asociado al producto terminado (Para NPT)
    ''' </summary>
    Public Property MainDrugId As Integer?
        Get
            Return CType(INDSleMainDrug.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleMainDrug.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del preparado
    ''' </summary>
    Public Property MeasurementPreparedId As Integer?
        Get
            Return CType(INDsleMeasurementUnitPrepared.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleMeasurementUnitPrepared.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de preparación cuando el paquete es de tipo antibioticoterapia
    ''' </summary>
    Public Property PreparationTypeAntibiotic As Byte?
        Get
            Return INDslePreparationType.EditValue
        End Get
        Set(value As Byte?)
            INDslePreparationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la osmolaridad total del paquete (producto)
    ''' </summary>
    Public Property OsmolarityTotal As Decimal Implements IPackage.OsmolarityTotal
        Get
            Return INDspnTotalOsmolarity.EditValue
        End Get
        Set(value As Decimal)
            INDspnTotalOsmolarity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el volumen total del paquete (producto)
    ''' </summary>
    Public Property VolumeTotalOrder As Decimal? Implements IPackage.VolumeTotalOrder
        Get
            Return CType(INDspnTotalOrderVolume.EditValue, Decimal?)
        End Get
        Set(value As Decimal?)
            INDspnTotalOrderVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el volumen total con purga del paquete (producto)
    ''' </summary>
    Public Property VolumeTotalOrderPurga As Decimal Implements IPackage.VolumeTotalOrderPurga
        Get
            Return INDspnTotalOrderVolumePurge.EditValue
        End Get
        Set(value As Decimal)
            INDspnTotalOrderVolumePurge.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el peso total del paquete (producto)
    ''' </summary>
    Public Property WeightTotalSolution As Decimal Implements IPackage.WeightTotalSolution
        Get
            Return INDspnTotalSolutionWeight.EditValue
        End Get
        Set(value As Decimal)
            INDspnTotalSolutionWeight.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la concentración del paquete (producto)
    ''' </summary>
    Public Property Concentration As Decimal? Implements IPackage.Concentration
        Get
            Return CDec(INDspnConcentration.EditValue)
        End Get
        Set(value As Decimal?)
            INDspnConcentration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la concentración del paquete (producto)
    ''' </summary>
    Public Property ConcentrationMeasurementUnitId As Integer Implements IPackage.ConcentrationMeasurementUnitId
        Get
            Return CInt(INDsleConcentrationMeasurementUnit.EditValue)
        End Get
        Set(value As Integer)
            INDsleConcentrationMeasurementUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de estabilidad 
    ''' </summary>
    Public Property TypeStability As Byte? Implements IPackage.TypeStability
        Get
            Return IIf(INDSleTypeStability.EditValue Is Nothing, Nothing, CByte(INDSleTypeStability.EditValue))
        End Get
        Set(value As Byte?)
            INDSleTypeStability.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la estabilidad en Dias
    ''' </summary>
    Public Property StabilityDays As Integer? Implements IPackage.StabilityDays
        Get
            Return CInt(INDspnStabilityDays.EditValue)
        End Get
        Set(value As Integer?)
            INDspnStabilityDays.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la estabilidad en horas
    ''' </summary>
    Public Property StabilityHour As TimeSpan? Implements IPackage.StabilityHour
        Get
            If INDspnStabilityHours.EditValue IsNot Nothing AndAlso TypeOf INDspnStabilityHours.EditValue Is DateTime Then
                Return CType(INDspnStabilityHours.EditValue, DateTime).TimeOfDay
            Else
                Return Nothing
            End If
        End Get
        Set(value As TimeSpan?)
            If value.HasValue Then
                INDspnStabilityHours.EditValue = Date.Today.Add(value)
            Else
                INDspnStabilityHours.EditValue = Nothing
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las horas de vigencia a temperatura ambiente del paquete (producto)
    ''' </summary>
    Public Property EnvironmentalTemperatureTerm As Integer Implements IPackage.EnvironmentalTemperatureTerm
        Get
            Return IIf(INDspnEnvironmentalTemperatureTerm.EditValue Is Nothing, Nothing, CInt(INDspnEnvironmentalTemperatureTerm.EditValue))
        End Get
        Set(value As Integer)
            INDspnEnvironmentalTemperatureTerm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la Purga del paquete (producto)
    ''' </summary>
    Public Property Purge As Decimal Implements IPackage.Purge
        Get
            Return INDspnPurge.EditValue
        End Get
        Set(value As Decimal)
            INDspnPurge.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las instrucciones de preparación del paquete (producto)
    ''' </summary>
    Public Property PreparationInstructions As String Implements IPackage.PreparationInstructions
        Get
            Return IIf(INDmePreparationInstructions.EditValue Is Nothing, Nothing, INDmePreparationInstructions.EditValue)
        End Get
        Set(value As String)
            INDmePreparationInstructions.EditValue = IIf(value Is Nothing Or value = String.Empty, "", value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece las consideraciones especiales del paquete (producto)
    ''' </summary>
    Public Property SpecialConsiderations As String Implements IPackage.SpecialConsiderations
        Get
            Return IIf(INDmeSpecialConsiderations.EditValue Is Nothing, Nothing, INDmeSpecialConsiderations.EditValue)
        End Get
        Set(value As String)
            INDmeSpecialConsiderations.EditValue = IIf(value Is Nothing Or value = String.Empty, "", value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de dosis unitaria
    ''' </summary>
    Public Property UnitDoseTypeId As Integer? Implements IPackage.UnitDoseTypeId
        Get
            Return CType(INDsleUnitDoseType.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleUnitDoseType.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Public Property Sequence As MixingStationSequence Implements IPackage.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As MixingStationSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As MixingStationSequenceDetail In Me._sequence.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPackage.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IPackage.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPackage.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleUnitDoseType.Enabled = value
            INDsleRiskLevel.Enabled = value
            INDsleProduct.Enabled = value
            INDtxtCodeAlternative.Enabled = value
            INDgleStorage.Enabled = value
            INDsleReadjustments.Enabled = value
            INDsleTypeNPT.Enabled = value
            INDSleMainDrug.Enabled = value
            INDmeDescription.Enabled = value
            INDslePhotoProtection.Enabled = value
            INDBtnAddProducts.Enabled = value
            INDliStabilityHour.Enabled = value
            INDliStabilityDays.Enabled = value
            INDLciStabilityType.Enabled = value
            INDspnEnvironmentalTemperatureTerm.Enabled = value
            INDspnPurge.Enabled = value
            INDmePreparationInstructions.Enabled = value
            INDmeSpecialConsiderations.Enabled = value
            INDspnConcentration.Enabled = value
            INDsleConcentrationMeasurementUnit.Enabled = value
            INDliTotalVolumePrepared.Enabled = value
            INDTotalVolumePrepared.Enabled = value
            INDspnTotalOsmolarity.Enabled = value
            INDspnTotalOrderVolume.Enabled = value
            INDsleVolumeTotalOrderMeasurementUnit.Enabled = value
            INDspnTotalOrderVolumePurge.Enabled = value
            INDspnTotalSolutionWeight.Enabled = value
            INDGleType.Enabled = value
            INDGcComponents.Enabled = value
            INDGleVehicleOptimization.Enabled = value

            INDlycRoot.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Deja los controles en modo solo lectura (no editable) o habilita edición.
    ''' </summary>
    Public WriteOnly Property ControlsReadOnly As Boolean Implements IPackage.ControlsReadOnly
        Set(value As Boolean)
            ' Suspende actualizaciones visuales
            INDlycRoot.BeginUpdate()

            ' TextEdits / MemoEdits
            INDtxtName.Properties.ReadOnly = value
            INDmePreparationInstructions.Properties.ReadOnly = value
            INDmeSpecialConsiderations.Properties.ReadOnly = value
            INDtxtCodeAlternative.Properties.ReadOnly = value

            ' SpinEdits
            INDspnStabilityHours.Properties.ReadOnly = value
            INDspnStabilityDays.Properties.ReadOnly = value
            INDspnEnvironmentalTemperatureTerm.Properties.ReadOnly = value
            INDspnPurge.Properties.ReadOnly = value
            INDspnConcentration.Properties.ReadOnly = value
            INDspnTotalOsmolarity.Properties.ReadOnly = value
            INDspnTotalOrderVolume.Properties.ReadOnly = value
            INDspnTotalOrderVolumePurge.Properties.ReadOnly = value
            INDspnTotalSolutionWeight.Properties.ReadOnly = value
            INDTotalVolumePrepared.Properties.ReadOnly = value

            ' ComboBox / LookUpEdits / GridLookUpEdits
            INDsleUnitDoseType.Properties.ReadOnly = value
            INDsleRiskLevel.Properties.ReadOnly = value
            INDsleProduct.Properties.ReadOnly = value
            INDSleTypeStability.Properties.ReadOnly = value
            INDsleReadjustments.Properties.ReadOnly = value
            INDsleTypeNPT.Properties.ReadOnly = value
            INDslePhotoProtection.Properties.ReadOnly = value
            INDgleStorage.Properties.ReadOnly = value
            INDGleType.Properties.ReadOnly = value
            INDGleVehicleOptimization.Properties.ReadOnly = value
            INDsleConcentrationMeasurementUnit.Properties.ReadOnly = value
            INDsleVolumeTotalOrderMeasurementUnit.Properties.ReadOnly = value
            INDsleMeasurementUnitPrepared.Properties.ReadOnly = value
            INDslePreparationType.Properties.ReadOnly = value

            Dim ListActions As New List(Of eAcciones)
            IndigoGridView1.SetListAcction(INDGvComponents, ListActions)

            INDBtnAddProducts.Enabled = Not value
            INDGvComponents.Columns.ColumnByName("colActions").Visible = Not value
            INDGvComponents.Columns.ColumnByName("colActions").OptionsColumn.ShowInCustomizationForm = Not value

            ' Reactiva el pintado
            INDlycRoot.EndUpdate()
        End Set
    End Property

#End Region

#Region "DataSource"

    ''' <summary>
    ''' Propiedad que contiene los Productos 
    ''' </summary>
    Public Property ProductDatasource As XPInstantFeedbackSource Implements IPackage.ProductDatasource
        Get
            Return CType(INDsleProduct.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProduct.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de niveles de riesgo
    ''' </summary>
    Public Property RiskLevelDatasource As XPInstantFeedbackSource Implements IPackage.RiskLevelDatasource
        Get
            Return CType(INDsleRiskLevel.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRiskLevel.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de Readecuaciones
    ''' </summary>
    Public Property ReadjustmentsDatasource As XPInstantFeedbackSource Implements IPackage.ReadjustmentsDatasource
        Get
            Return CType(INDsleReadjustments.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleReadjustments.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del Tipo de Dosis Unitaria
    ''' </summary>
    Property UnitDoseTypeDatasource As XPInstantFeedbackSource Implements IPackage.UnitDoseTypeDatasource
        Get
            Return CType(INDsleUnitDoseType.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleUnitDoseType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del almacenamiento
    ''' </summary>
    Property StorageTemperatureDatasource As XPInstantFeedbackSource Implements IPackage.StorageTemperatureDatasource
        Get
            Return CType(INDgleStorage.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDgleStorage.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la Preferencia de Etiqueta del paquete
    ''' </summary>
    ''' <returns></returns>
    Public Property LabelType As Byte? Implements IPackage.LabelType
        Get
            Return CStr(INDGleType.EditValue)
        End Get
        Set(value As Byte?)
            INDGleType.EditValue = value
        End Set
    End Property

#End Region

#Region "Propierties ICrudBase"

    ''' <summary>
    ''' Propiedad que establece los mensajes (Advertencias)
    ''' </summary>
    ''' <param name="icono"></param>
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
    ''' Evento barra de botones Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina el turno seleccionado
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me._packageEntity IsNot Nothing AndAlso Me._packageEntity.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MPackage(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePackageAsync(Me._packageEntity)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Guarda el paquete
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar

        If Not AllowsEditPackage AndAlso INDsleProduct.Properties.ReadOnly Then
            Mensaje(EeventViewerImages.Advertencia) = "El paquete ya fue asociado a un proceso de producción"
            Exit Sub
        End If

        If Not _unitDoseTypeEntity?.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            If Not ValidateControls() Then Exit Sub

            If ListPackageDetail Is Nothing OrElse ListPackageDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Agregue un componente"
                Exit Sub
            End If

            If (From x In ListPackageDetail Where x.MainMedicine = True).Count = 0 Then
                Mensaje(EeventViewerImages.Informacion) = "Se debe agregar medicamento principal y vehículo"
                Exit Sub
            End If

            If PreparationTypeAntibiotic = 4 AndAlso Not ListPackageDetail.Exists(Function(x) CBool(x.ComponentType = 2)) Then 'Ninguno
                Mensaje(EeventViewerImages.Advertencia) = "Falta agregar insumos al paquete"
                Exit Sub
            End If

        ElseIf _unitDoseTypeEntity.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            If Not NptId.HasValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El campo Plantilla NPT es obligatorio para paquetes de tipo Nutrición Parenteral"
                Exit Sub
            End If

            If Not MainDrugId.HasValue Then
                Mensaje(EeventViewerImages.Advertencia) = "El medicamento de referencia no se encuentra parametrizado en la plantilla NPT"
                Exit Sub
            End If
        End If

        If _packageEntity.Id > 0 Then
            _packageEntity.MarkAsModified()

            If MessageIndigo.Show("Esta seguro que desea modificar el Paquete?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim showJustification As New ShowDialogJustification
                showJustification.Text = "Justificación Modificación del Paquete"
                Dim frmTransparent As New FrmTransparent(showJustification, False)
                Dim Justification As String = ""
                If frmTransparent.ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Justification = showJustification.Justification
                Else
                    Return
                End If
                _packageEntity.Justification = Justification
            Else
                Return
            End If
        End If

        AssigningValues()
        Try
            Using Model As New MPackage(Me.Tag.ToString())
                AsyncLoader(True)

                If IsDashboardConfirmationUnitDose AndAlso IsPersonalizedMasterPreparation Then 'Si el formulario se esta abriendo desde la dashboard
                    Dim args As New SendPackageArgs
                    args.Package = _packageEntity
                    args.IsPersonalizedMasterPreparation = IsPersonalizedMasterPreparation
                    RaiseEvent AddPackageArgs(Nothing, args)
                    Me.Close()
                    Return
                End If

                Dim result As ActionResult(Of Package) = Await Model.SavePackageAsync(Me._packageEntity, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _packageEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._packageEntity = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()

                    If IsDashboardConfirmationUnitDose Then 'Si el formulario se esta abriendo desde la dashboard
                        Dim args As New SendPackageArgs
                        args.Package = result.ObjectEmbbeded
                        args.IsPersonalizedMasterPreparation = IsPersonalizedMasterPreparation
                        RaiseEvent AddPackageArgs(Nothing, args)
                        Me.Close()
                    End If
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Limpia el formulario para iniciar
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewPackage()
        End If
    End Sub
#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones Activo - Inactivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
        Deshacer()
        If IsDashboardConfirmationUnitDose Then 'Si el formulario se esta abriendo desde la dashboard se cierra
            Me.Close()
        End If
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
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MixingStationSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MixingStationSequenceDetail.Any(Function(o) CBool(o.IdOperatingUnit = operatingUnit.Id)) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmPackage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'

        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        _presenter = New PPackage(Me)

        AsyncLoader(True)
        Await _presenter.GetSequence()
        If Not IsDashboardConfirmationUnitDose Then
            AsyncLoader(False)
        End If

        LoadStatus()
        IndigoGridView1.MoreInfoColunmns(INDGvComponents)
        AddActionsColumns()
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Deshacer()
        InitializeTuples()
        IndigoGridControl1.RefreshGrid(INDGcComponents)
        Await LoadControlsOfUnitDoseConfirm()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _packageEntity = Nothing
        _record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _openPopUpRiskLevel = Nothing
        _itemPackageDetail = Nothing
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmPackage_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedrecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewPackage()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Se ejecuta al desplegar el control de tabla estabilidad de la rejilla principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDrepPceStabilityTable_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPceStabilityTable.QueryPopUp
        Dim entityDetail = CType(INDGvComponents.GetFocusedRow(), PackageDetail)
        INDgcStabilityTable.DataSource = Nothing

        If entityDetail.MainMedicine Then
            If dictionaryATC Is Nothing Then
                dictionaryATC = New Dictionary(Of Integer, List(Of StabilityTableDetailXpo))
            End If

            If Not dictionaryATC.ContainsKey(entityDetail.AtcId) Then
                Dim list = _presenter.GetStabilityTableDetailByATCId(entityDetail.AtcId)
                If list IsNot Nothing AndAlso list.Count > 0 Then
                    dictionaryATC.Add(entityDetail.AtcId, list)
                End If
            End If

            If dictionaryATC.ContainsKey(entityDetail.AtcId) Then
                INDgcStabilityTable.DataSource = dictionaryATC(entityDetail.AtcId).ToList()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad de medida de la concentración
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConcentrationMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleConcentrationMeasurementUnit.QueryPopUp
        If INDsleConcentrationMeasurementUnit.Properties.DataSource Is Nothing Then
            INDsleConcentrationMeasurementUnit.Properties.DataSource = _presenter.InitializeMeasureUnitWeights()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo de nutricion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTypeNPT_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTypeNPT.QueryPopUp
        If INDsleTypeNPT.Properties.DataSource Is Nothing Then
            INDsleTypeNPT.Properties.DataSource = _presenter.InitializeNPT()
        End If
    End Sub

    ''' <summary>
    ''' Datasource unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleVolumeTotalOrderMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleVolumeTotalOrderMeasurementUnit.QueryPopUp
        If INDsleVolumeTotalOrderMeasurementUnit.Properties.DataSource Is Nothing Then
            INDsleVolumeTotalOrderMeasurementUnit.Properties.DataSource = _presenter.InitializeMeasureUnitVolumen()
        End If
    End Sub

    ''' <summary>
    ''' Datasource unidad de medida del preparado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMeasurementUnitPrepared_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasurementUnitPrepared.QueryPopUp
        If INDsleMeasurementUnitPrepared.Properties.DataSource Is Nothing And MeasurementPreparedId IsNot Nothing Then
            INDsleMeasurementUnitPrepared.Properties.DataSource = _presenter.InitializeMeasureUnitPrepared(MeasurementPreparedId)
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleATC control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.ReadOnly Then
            Exit Sub
        End If

        Dim FlagNPT As Boolean? = Nothing
        Dim ATCId As Integer? = Nothing

        'Se valida que hayan seleccionado un tipo de dosis unitaria
        If INDsleUnitDoseType.EditValue Is Nothing OrElse INDsleUnitDoseType.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de dosis unitaria"
            INDsleProduct.Properties.DataSource = Nothing
            Exit Sub
        End If

        If _unitDoseTypeEntity.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            If MainDrugId Is Nothing Then
                ProductDatasource = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "El medicamento de referencia en la plantilla NPT se encuentra vacío. Este dato es obligatorio en este tipo de dosis para asignar el código del producto terminado"
                Exit Sub
            End If

            ATCId = MainDrugId
            FlagNPT = True
        Else
            If ListPackageDetail Is Nothing OrElse ListPackageDetail.Count = 0 OrElse
                (From x In ListPackageDetail Where x.MainMedicine = True Select x).FirstOrDefault()?.AtcId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar componente medicamento principal al paquete"
                INDsleProduct.Properties.DataSource = Nothing
                Exit Sub
            End If

            ATCId = (From x In ListPackageDetail Where x.MainMedicine Select x).FirstOrDefault().AtcId
        End If

        _presenter.InitializeProduct(ATCId:=ATCId, ProductNPT:=FlagNPT)
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleRiskLevel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleRiskLevel_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRiskLevel.QueryPopUp
        If Not _openPopUpRiskLevel Then
            _presenter.InitializeRiskLevel()
            _openPopUpRiskLevel = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleUnitDoseType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleUnitDoseType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUnitDoseType.QueryPopUp
        If Not _openPopUpUnitDoseType Then
            INDsleUnitDoseType.Properties.DataSource = _presenter.InitializeUnitDoseTypeByStatusAndClass()
            _openPopUpUnitDoseType = True
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el formulario de unidad de medida de la concentración
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleConcentrationMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConcentrationMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(300, Nothing, True)
            If {EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile}.Contains(_unitDoseTypeEntity?.MSClass) Then
                INDsleConcentrationMeasurementUnit.Properties.DataSource = _presenter.InitializeMeasureUnitWeights()
            Else
                INDsleConcentrationMeasurementUnit.Properties.DataSource = _presenter.InitializeMeasureUnitWeight()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Abre el formulario de unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleVolumeTotalOrderMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVolumeTotalOrderMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(300, Nothing, True)
            INDsleVolumeTotalOrderMeasurementUnit.Properties.DataSource = _presenter.InitializeMeasureUnitVolumen()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDsleProduct.Properties.NullText = String.Empty
            INDsleProduct.Properties.ReadOnly = False
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
                Formulario.Size = New System.Drawing.Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeRiskLevel()
            End Using
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim tag = sender.Tag.ToString
        If String.IsNullOrEmpty(tag) Then
            Select Case sender.GetType
                Case GetType(DevExpress.XtraEditors.ButtonEdit)
                    tag = CType(sender, DevExpress.XtraEditors.ButtonEdit).Text
            End Select
        End If

        Select Case tag
            Case "Edit"
                EditDetail()
            Case "Remove", "Eliminar"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega productos al paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAddProducts_Click(sender As Object, e As EventArgs) Handles INDBtnAddProducts.Click
        If AllowsEditPackage = False Then
            Mensaje(EeventViewerImages.Advertencia) = "El paquete ya fue asociado a un proceso de producción y no se puede agregar más componentes"
            Exit Sub
        End If

        'Se valida que hayan seleccionado un tipo de dosis unitaria
        If INDsleUnitDoseType.EditValue Is Nothing OrElse INDsleUnitDoseType.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de dosis unitaria"
            Exit Sub
        End If

        ' Determinar automáticamente si el nuevo detalle será Principal o Complementario
        ' Solo aplica cuando viene del Dashboard de Confirmación de Dosis Unitarias (FrmAssignPackage)
        ' NOTA: Para Citostático con múltiples principales (intratecal), el flujo es directo desde FrmPackage,
        '       NO desde FrmAssignPackage, por lo que la lógica de complementario se mantiene aquí.
        Dim existeMainMedicine As Boolean = ListPackageDetail?.Any(Function(x) x.MainMedicine = True)
        Dim isNewDetailComplementary As Boolean = False
        Dim isNewDetailMainMedicine As Boolean = False

        If IsDashboardConfirmationUnitDose Then
            If existeMainMedicine Then
                ' Ya existe un medicamento principal → el nuevo será complementario
                isNewDetailComplementary = True
                isNewDetailMainMedicine = False
            Else
                ' No existe medicamento principal → el nuevo será principal (con tipo de preparación)
                isNewDetailComplementary = False
                isNewDetailMainMedicine = True
            End If
        End If

        Using formulario As New FrmPopupPackageDetail
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddPackageDetail, AddressOf ReturnAddPackageDetail
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
            formulario.ListPackageDetailValidation = ListPackageDetail
            formulario.unitDoseType = _unitDoseTypeEntity

            ' Pasar parámetros para determinar automáticamente el tipo de detalle
            formulario.IsDashboardConfirmationUnitDose = IsDashboardConfirmationUnitDose
            formulario.IsNewDetailComplementary = isNewDetailComplementary
            formulario.IsNewDetailMainMedicine = isNewDetailMainMedicine
            formulario.RequestedDosage = RequestedDosage

            ' Obtener el medicamento principal para filtrar por mismo ATC y forma farmacéutica (solo si es complementario)
            If isNewDetailComplementary Then
                Dim mainMedicine = ListPackageDetail?.FirstOrDefault(Function(x) x.MainMedicine = True)
                If mainMedicine IsNot Nothing Then
                    formulario.MainMedicineAtcId = mainMedicine.AtcId
                    formulario.MainMedicineFormulationType = mainMedicine.ATC?.FormulationType
                End If
            End If

            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPackage_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If IsDashboardConfirmationUnitDose = False Then 'Si se abre el form desde el menu principal
            If INDbteCode.Text Is String.Empty Then
                INDbteCode.Focus()
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara al cambiar el valor de tipo de dosis unitaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsleUnitDoseType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUnitDoseType.EditValueChanged
        Try
            AsyncLoader(True)
            If ListPackageDetail?.Any() And Not IsDashboardConfirmationUnitDose Then

                For Each item In ListPackageDetail
                    If item.Id > 0 Then _listDeletePackageDetail.Add(item)
                Next

                ListPackageDetail.Clear()
            End If

            If Not _isLoading Then
                TypeStability = Nothing
                StabilityHour = Nothing
                StabilityDays = Nothing
            End If

            RefrescarRejilla()
            Dim loading = _isLoading
            If IsNumeric(UnitDoseTypeId) And UnitDoseTypeId > 0 Then

                Await CargarUnitDoseType()

                If Not loading Then
                    CalculateValues()
                    Await CalculateValuesAntibiotic()
                End If
            End If

            If Not _isLoading Then
                EvaluateStabilityTypeVisibility()
            End If
        Catch ex As Exception
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Carga el tipo de dosis unitaria seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public Async Function CargarUnitDoseType() As Task
        Using Model As New MPackage(CStr(Me.Tag))
            Dim resultOperation = Await Model.GetUnitDoseTypeById(UnitDoseTypeId)

            _unitDoseTypeEntity = resultOperation.ObjectEmbbeded
            HideOrShowLabelsForUnitDoseType(_unitDoseTypeEntity?.MSClass)

            If Not _isLoading Then
                EvaluateStabilityTypeVisibility()
            End If
        End Using
    End Function

    ''' <summary>
    ''' Se habilitan o deshabilitan los campos dependiendo de la clase del tipo de dosis unitaria
    ''' </summary>
    Private Sub HideOrShowLabelsForUnitDoseType(ByVal _MsClass As Integer?)

        If _MsClass Is Nothing Then Exit Sub

        'Listas de objetos para identificar los campos para mostrar/ocultar
        Dim controlsToHide = New List(Of Object)()
        Dim controlsToShow = New List(Of Object)()
        ProductDatasource = Nothing

        Select Case _MsClass
            Case EUnitDoseTypeClass.ParenteralNutrition
                controlsToHide.AddRange({INDlcgChemicalPhysical, INDLciVehicleOptimization, INDliEnvironmentalTemperatureTerm, INDliPurge, INDliRiskLevel})
                controlsToShow.AddRange({INDTypeNPT, INDLyItemAddProducts, INDlcgMixingParameters, INDLciStabilityType, INDLciMainDrug})

                ConfigureGridColumns(False, "Tipo Componente", "ComponentTypeName")
                INDlcgParametersElaborations.Text = "Componentes de la nutrición parenteral"
                INDsleReadjustments.EditValue = 0
                INDsleReadjustments.ReadOnly = True
                INDGcComponents.AllowDrop = True

                If ListPackageDetail IsNot Nothing AndAlso ListPackageDetail.Any(Function(item) CBool(item.ComponentType = 5)) Then
                    INDGvComponents.Columns(2).Visible = True
                End If

                AddActionsColumns(EUnitDoseTypeClass.ParenteralNutrition)

            Case EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic
                AddActionsColumns(_MsClass.Value)

                controlsToHide.AddRange({
                INDTypeNPT, INDliConcentration, INDlyItemConcentrationMeasurementUnit,
                INDliTotalOsmolarity, INDliTotalOrderVolume, INDlyItemVolumeTotalOrderMeasurementUnit,
                INDliTotalSolutionWeight, INDliTotalOrderVolumePurge, INDLciStabilityType, INDLciMainDrug})

                controlsToShow.AddRange({
                INDLyItemAddProducts, INDlcgMixingParameters, INDlcgChemicalPhysical,
                INDliTotalVolumePrepared, INDlyItemPreparationType, INDliConcentrationAntibiotic,
                INDlyItemMeasurementUnitPrepared, INDliRiskLevel})

                CleanControlsNPT()

                If Not _isLoading Then
                    InventoryRiskLevelId = Nothing
                    INDsleRiskLevel.Properties.NullText = String.Empty
                End If

                ConfigureGridColumns(True, "Tipo", "PreparationTypeName")
                INDlcgParametersElaborations.Text = "Parametros para Elaboración"
                INDsleReadjustments.ReadOnly = False
                INDGcComponents.AllowDrop = False

            Case EUnitDoseTypeClass.Refilling
                AddActionsColumns(EUnitDoseTypeClass.Refilling)

                controlsToHide.AddRange({
                INDTypeNPT, INDliConcentration, INDlyItemConcentrationMeasurementUnit,
                INDliTotalOsmolarity, INDliTotalOrderVolume, INDlyItemVolumeTotalOrderMeasurementUnit,
                INDliTotalSolutionWeight, INDliTotalOrderVolumePurge, INDlyItemPreparationType, INDLciStabilityType})

                controlsToShow.AddRange({
                INDlcgMixingParameters, INDlcgChemicalPhysical, INDLyItemAddProducts,
                INDliConcentrationAntibiotic, INDliTotalVolumePrepared, INDlyItemMeasurementUnitPrepared, INDliRiskLevel})

                CleanControlsNPT()

                ConfigureGridColumns(True, "Tipo Componente", "ComponentTypeName")
                INDlcgParametersElaborations.Text = "Parametros para Elaboración"
                INDsleReadjustments.ReadOnly = False
                INDTotalVolumePrepared.Properties.ReadOnly = True
                INDGcComponents.AllowDrop = False

            Case Else
                controlsToHide.AddRange({
                INDlyItemMeasurementUnitPrepared, INDliTotalVolumePrepared,
                INDliConcentrationAntibiotic, INDlyItemPreparationType, INDTypeNPT, INDLciMainDrug})

                controlsToShow.AddRange({
                INDliConcentration, INDLyItemAddProducts, INDliTotalOrderVolume,
                INDlcgChemicalPhysical, INDlcgMixingParameters, INDlyItemConcentrationMeasurementUnit,
                INDlyItemVolumeTotalOrderMeasurementUnit, INDliRiskLevel})

                If _MsClass = EUnitDoseTypeClass.Magistral Then
                    controlsToShow.Add({INDLciStabilityType})
                Else
                    controlsToHide.Add({INDLciStabilityType})
                End If

                CleanControlsNPT()

                INDspnConcentration.ReadOnly = ({EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile}.Contains(_unitDoseTypeEntity?.MSClass))
                INDspnTotalOrderVolume.ReadOnly = ({EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile}.Contains(_unitDoseTypeEntity?.MSClass))
                ConfigureGridColumns(True, "Tipo Componente", "ComponentTypeName")
                INDlcgParametersElaborations.Text = "Parametros para Elaboración"
                INDsleReadjustments.ReadOnly = False
                INDGcComponents.AllowDrop = False
        End Select

        ApplyControlVisibility(controlsToHide, False)
        ApplyControlVisibility(controlsToShow, True)

        If INDliStabilityHour.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            StabilityHour = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Configura las columnas del grid para los diferentes tipos de dosis unitaria
    ''' </summary>
    Private Sub ConfigureGridColumns(showActions As Boolean, caption As String, fieldName As String)
        For i As Integer = 2 To 6
            INDGvComponents.Columns(i).Visible = IIf(i = 2 AndAlso showActions, True, False)
        Next

        INDGvComponents.Columns(1).Caption = caption
        INDGvComponents.Columns(1).FieldName = fieldName
        INDGvComponents.Columns.ColumnByName("colActions").Visible = showActions
        If INDGvComponents.Columns.ColumnByName("MoreInfo") IsNot Nothing Then
            INDGvComponents.Columns.ColumnByName("MoreInfo").Visible = showActions
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar el valor de la purga
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDspnPurge_EditValueChanged(sender As Object, e As EventArgs) Handles INDspnPurge.EditValueChanged
        INDspnTotalOrderVolumePurge.EditValue = If(INDspnTotalOrderVolume.EditValue, 0) + If(INDspnPurge.EditValue, 0)
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar el valor del tipo de estabilidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleTypeStability_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleTypeStability.EditValueChanged
        If TypeStability > 0 Then

            Dim controlsToHide = New List(Of Object)()
            Dim controlsToShow = New List(Of Object)()

            Select Case TypeStability
                Case 1
                    StabilityDays = Nothing
                    controlsToHide.AddRange({INDliStabilityDays})
                    controlsToShow.AddRange({INDliStabilityHour})
                Case 2
                    StabilityHour = Nothing
                    controlsToHide.AddRange({INDliStabilityHour})
                    controlsToShow.AddRange({INDliStabilityDays})
                Case 3
                    controlsToShow.AddRange({INDliStabilityHour, INDliStabilityDays})
            End Select

            ApplyControlVisibility(controlsToHide, False)
            ApplyControlVisibility(controlsToShow, True)
        End If
    End Sub

    ''' <summary>
    ''' Evalúa si se debe mostrar el tipo de estabilidad para medicamentos citostáticos con preparación "Ninguna"
    ''' </summary>
    Private Sub EvaluateStabilityTypeVisibility()
        If _unitDoseTypeEntity Is Nothing OrElse _unitDoseTypeEntity.MSClass <> EUnitDoseTypeClass.Cytostatic Then
            Return
        End If

        Dim allMedicinesHaveNoPreparation As Boolean = False
        If ListPackageDetail IsNot Nothing AndAlso ListPackageDetail.Any() Then
            Dim medicines = ListPackageDetail.Where(Function(x) x.ComponentType = 1).ToList()

            ' Verificar que existan medicamentos y que TODOS tengan preparación "Ninguna"
            If medicines.Any() Then
                allMedicinesHaveNoPreparation = medicines.All(Function(x) _
                    x.PreparationType.HasValue AndAlso
                    x.PreparationType.Value = 4)
            End If
        End If

        If allMedicinesHaveNoPreparation Then
            INDLciStabilityType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciStabilityType.Enabled = True
        Else
            INDLciStabilityType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciStabilityType.Enabled = False
            If Not _isLoading Then
                TypeStability = Nothing
                StabilityHour = Nothing
                StabilityDays = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se seleciona Diluyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDRICE_Click(sender As Object, e As EventArgs) Handles INDRICE.EditValueChanged
        Dim SelectRow = CType(INDGvComponents.GetFocusedRow(), PackageDetail)
        If SelectRow IsNot Nothing Then
            If Not Await ValidationsCheckEdit(SelectRow, 2, sender.Checked) Then
                SelectRow.Thinner = False
                sender.Checked = False
                INDGcComponents.RefreshDataSource()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se selecciona Vehiculo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDRICEVehicle_EditValueChanged(sender As Object, e As EventArgs) Handles INDRICEVehicle.EditValueChanged
        Dim SelectRow = CType(INDGvComponents.GetFocusedRow(), PackageDetail)
        If SelectRow IsNot Nothing Then
            If Not Await ValidationsCheckEdit(SelectRow, 3, sender.Checked) Then
                SelectRow.Vehicle = False
                sender.Checked = False
                INDGcComponents.RefreshDataSource()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento para cuando se selecciona en la rejilla medicamento principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDRICEMain_EditValueChanged(sender As Object, e As EventArgs) Handles INDRICEMain.EditValueChanged
        Dim selectRow = CType(INDGvComponents.GetFocusedRow(), PackageDetail)
        If selectRow IsNot Nothing Then
            If Not Await ValidationsCheckEdit(selectRow, 1, sender.Checked) Then
                selectRow.MainMedicine = False
                sender.Checked = False
                INDGcComponents.RefreshDataSource()
            End If
        End If
    End Sub

#End Region

#Region "Drag & Drop"

    ''' <summary>
    ''' Información del hit sobre la rejilla
    ''' </summary>
    Private _downHitInfo As GridHitInfo

    ''' <summary>
    ''' Aqui inicia el arrastrado
    ''' </summary>
    Private Sub INDGvComponents_MouseDown(sender As Object, e As MouseEventArgs) Handles INDGvComponents.MouseDown
        Dim view As GridView = CType(sender, GridView)

        Me._downHitInfo = Nothing
        Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))

        If Not System.Windows.Forms.Control.ModifierKeys = System.Windows.Forms.Keys.None Then Exit Sub

        If e.Button = System.Windows.Forms.MouseButtons.Left AndAlso hitInfo.InRow AndAlso hitInfo.HitTest <> GridHitTest.RowIndicator Then

            Dim item As PackageDetail = CType(INDGvComponents.GetRow(hitInfo.RowHandle), PackageDetail)
            If item IsNot Nothing AndAlso {1, 4}.Contains(item.ComponentType) Then
                Me._downHitInfo = hitInfo
            End If

        End If
    End Sub

    ''' <summary>
    ''' Aqui se selecciona los objetos a arrastrar
    ''' </summary>
    Private Sub INDGvComponents_MouseMove(sender As Object, e As MouseEventArgs) Handles INDGvComponents.MouseMove
        If e.Button = MouseButtons.Left AndAlso _downHitInfo IsNot Nothing AndAlso _downHitInfo.RowHandle >= 0 Then
            INDGcComponents.DoDragDrop(_downHitInfo.RowHandle, DragDropEffects.Move)
            Me._downHitInfo = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Aqui se verifica si el tipo de objeto a soltar corresponde al esperado
    ''' </summary>
    Private Sub INDGcComponents_DragOver(sender As Object, e As DragEventArgs) Handles INDGcComponents.DragOver
        If _downHitInfo IsNot Nothing Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ''' <summary>
    ''' Aqui se recupera los objetos arrastrados y se ordena segun la posicion en la que se suelta el objeto
    ''' </summary>
    Private Sub INDGcComponents_DragDrop(sender As Object, e As DragEventArgs) Handles INDGcComponents.DragDrop
        Dim punto As Point = INDGcComponents.PointToClient(New Point(e.X, e.Y))
        Dim hitInfo As GridHitInfo = INDGvComponents.CalcHitInfo(punto)
        Dim rowIndexTo As Integer = hitInfo.RowHandle

        If _downHitInfo.RowHandle >= 0 AndAlso rowIndexTo >= 0 AndAlso _downHitInfo.RowHandle <> rowIndexTo Then
            Dim dataSource As List(Of PackageDetail) = TryCast(INDGcComponents.DataSource, List(Of PackageDetail))
            If dataSource IsNot Nothing AndAlso dataSource(rowIndexTo).ComponentType <> 5 Then
                Dim ItemToDrop As PackageDetail = dataSource(_downHitInfo.RowHandle)
                ItemToDrop.NPTItemOrder = rowIndexTo
                dataSource.RemoveAt(_downHitInfo.RowHandle)
                dataSource.Insert(rowIndexTo, ItemToDrop)
            End If

            INDGcComponents.RefreshDataSource()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Async Function LoadControlsOfUnitDoseConfirm() As Task
        If IsDashboardConfirmationUnitDose Then 'Si se esta abriendo este formulario desde el dashboard
            Try
                If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                    Me.Close()
                End If
                If Me._sequence.IsManual Then
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por lo tanto no se puede realizar el paquete"
                    Me.Close()
                Else
                    AsyncLoader(False)
                    Await Me.NewPackage()
                    ' Crear copia de la lista para no modificar los originales si se cierra sin guardar
                    ListPackageDetail = ClonePackageDetailList(ListPackageDetailUnitDoseConfirm)
                    RefrescarRejilla()
                    ' Crear copia del paquete para no modificar el original si se cierra sin guardar
                    _packageEntity = ClonePackage(MixinStationPackageXpo)

                    If _packageEntity Is Nothing Then Await NewPackage()
                End If

                'Si es una preparación magistral se asigna el paquete asociado
                INDlyItemAssociatedPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                If IsPersonalizedMasterPreparation Then
                    INDlyItemAssociatedPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDtxtAssociatedPackage.EditValue = $"{MixinStationPackageXpo.Code} - {MixinStationPackageXpo.Name}"
                    INDtxtAssociatedPackage.Properties.ReadOnly = True

                    ' Inicializar _unitDoseTypeEntity ANTES de asignar EditValue para evitar problemas
                    ' con el evento asíncrono EditValueChanged que puede no completarse a tiempo
                    If UnitDoseTypeFromDashboard IsNot Nothing Then
                        _unitDoseTypeEntity = UnitDoseTypeFromDashboard
                        HideOrShowLabelsForUnitDoseType(_unitDoseTypeEntity?.MSClass)
                    End If

                    INDsleUnitDoseType.EditValue = MixinStationPackageXpo.UnitDoseTypeId
                    INDsleUnitDoseType.Properties.NullText = MixinStationPackageXpo.UnitDoseTypeCodeName
                    INDsleUnitDoseType.Properties.ReadOnly = True

                    If MixinStationPackageXpo.ProductId IsNot Nothing Then
                        INDsleProduct.EditValue = MixinStationPackageXpo.ProductId
                        INDsleProduct.Properties.NullText = MixinStationPackageXpo.Name
                        INDsleProduct.Properties.ReadOnly = True
                    End If

                    INDsleRiskLevel.EditValue = MixinStationPackageXpo.RiskLevelId
                    INDsleRiskLevel.Properties.NullText = MixinStationPackageXpo.RiskLevelCodeName

                    INDGleVehicleOptimization.EditValue = MixinStationPackageXpo.VehicleOptimization

                    'Se asignan los datos correspondientes al paquete principal
                    With MixinStationPackageXpo
                        PackageName = .Name
                        State = .State
                        CodeAlternative = .CodeAlternative
                        Description = .Description
                        StabilityHour = .StabilityHour
                        Storage = .Storage
                        EnvironmentalTemperatureTerm = .EnvironmentalTemperatureTerm
                        Purge = .Purge
                        PreparationInstructions = .PreparationInstructions
                        SpecialConsiderations = .SpecialConsiderations
                        INDslePhotoProtection.EditValue = .PhotoProtection
                        PreparationTypeAntibiotic = .PreparationType
                        LabelType = .LabelType

                        If .Concentration IsNot Nothing AndAlso .Concentration > 0 Then
                            Concentration = .Concentration
                            INDsleConcentrationMeasurementUnit.EditValue = .ConcentrationMeasurementUnitId
                            INDsleConcentrationMeasurementUnit.Properties.NullText = .ConcentrationMeasurementUnitCodeName
                        End If

                        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(CType((_unitDoseTypeEntity?.MSClass), EUnitDoseTypeClass)) Then
                            ConcentrationAntibiotic = .ConcentrationAntibiotic
                            VolumeTotalPrepared = .VolumeTotalPrepared
                            MeasurementPreparedId = .MeasurementPreparedId
                            PreparationTypeAntibiotic = .PreparationType
                        Else
                            OsmolarityTotal = CDec(IIf(CBool(.OsmolarityTotal IsNot Nothing AndAlso .OsmolarityTotal > 0), .OsmolarityTotal, Nothing))
                            VolumeTotalOrderPurga = CDec(IIf(CBool(.VolumeTotalOrderPurga IsNot Nothing AndAlso .VolumeTotalOrderPurga > 0), .VolumeTotalOrderPurga, Nothing))
                            WeightTotalSolution = CDec(IIf(CBool(.WeightTotalSolution IsNot Nothing AndAlso .WeightTotalSolution > 0), .WeightTotalSolution, Nothing))

                            VolumeTotalOrder = .VolumeTotalOrder
                            INDsleVolumeTotalOrderMeasurementUnit.EditValue = .VolumeTotalOrderMeasurementUnitId
                            INDsleVolumeTotalOrderMeasurementUnit.Properties.NullText = .VolumeTotalOrderMeasurementUnitCodeName
                        End If

                        Await RefreshDescription()

                    End With
                End If
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            End Try
        End If
    End Function

    ''' <summary>
    ''' Inicializa el datasource de los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim ListSiNo = New List(Of Tuple(Of Boolean, String))
        ListSiNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListSiNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDslePhotoProtection.Properties.DataSource = ListSiNo.ToList

        Dim listPreparationType = New List(Of Tuple(Of Integer, String))()
        listPreparationType.Add(New Tuple(Of Integer, String)(0, "No Aplica"))
        listPreparationType.Add(New Tuple(Of Integer, String)(1, "Reconstitución"))
        listPreparationType.Add(New Tuple(Of Integer, String)(2, "Dilución"))
        listPreparationType.Add(New Tuple(Of Integer, String)(3, "Reconstitución + Dilución"))
        listPreparationType.Add(New Tuple(Of Integer, String)(4, "Ninguna"))
        INDslePreparationType.Properties.DataSource = listPreparationType

        ListReadjustments = New List(Of Tuple(Of Integer, String))()
        ListReadjustments.Add(New Tuple(Of Integer, String)(0, "0"))
        ListReadjustments.Add(New Tuple(Of Integer, String)(1, "1"))
        ListReadjustments.Add(New Tuple(Of Integer, String)(2, "2"))
        ListReadjustments.Add(New Tuple(Of Integer, String)(3, "3"))
        ListReadjustments.Add(New Tuple(Of Integer, String)(4, "4"))
        ListReadjustments.Add(New Tuple(Of Integer, String)(5, "5"))
        INDsleReadjustments.Properties.DataSource = ListReadjustments

        ListFillingLabelType = New List(Of Tuple(Of Byte, String))()
        ListFillingLabelType.Add(New Tuple(Of Byte, String)(1, "Bolsa"))
        ListFillingLabelType.Add(New Tuple(Of Byte, String)(2, "Nutrición parenteral"))
        ListFillingLabelType.Add(New Tuple(Of Byte, String)(3, "Jeringa 10 CC"))
        ListFillingLabelType.Add(New Tuple(Of Byte, String)(4, "Tabletería 4x4 cm"))
        ListFillingLabelType.Add(New Tuple(Of Byte, String)(5, "Magistral"))
        INDGleType.Properties.DataSource = ListFillingLabelType

        ListStabilityType = New List(Of Tuple(Of Byte, String))()
        ListStabilityType.Add(New Tuple(Of Byte, String)(1, "Horas"))
        ListStabilityType.Add(New Tuple(Of Byte, String)(2, "Dias"))
        ListStabilityType.Add(New Tuple(Of Byte, String)(3, "Personalizado"))
        INDSleTypeStability.Properties.DataSource = ListStabilityType

        _presenter.InitializeUnitDoseType()
        _presenter.InitializeProduct()
        _presenter.InitializeRiskLevel()
        _presenter.InitializeStorageTemperature()
    End Sub

    ''' <summary>
    ''' Metodo que retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedrecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If Not INDbteCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedrecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Try
                Using Model As New MPackage(CStr(Me.Tag))
                    _isLoading = True
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetPackageAsync(INDbteCode.Text.Trim)
                    INDlycRoot.BeginUpdate()
                    Dim NTPEnabled = True

                    _packageEntity = resultOperation.ObjectEmbbeded
                    If _packageEntity IsNot Nothing AndAlso _packageEntity.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_packageEntity.Id))
                            With _packageEntity
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                PackageName = .Name
                                State = .State
                                Storage = .Storage
                                CodeAlternative = .CodeAlternative
                                Description = .Description
                                EnvironmentalTemperatureTerm = .EnvironmentalTemperatureTerm
                                Purge = .Purge
                                PreparationInstructions = .PreparationInstructions
                                SpecialConsiderations = .SpecialConsiderations
                                UnitDoseTypeId = .UnitDoseTypeId
                                InventoryRiskLevelId = .RiskLevelId
                                INDsleRiskLevel.Properties.NullText = .RiskLevelCodeName
                                Me.ProductId = .ProductId
                                INDsleProduct.Properties.NullText = .ProductName
                                INDGleType.EditValue = .LabelType
                                Readjustments = .Readjustments

                                If .TypeStability IsNot Nothing Then
                                    TypeStability = .TypeStability
                                    StabilityHour = .StabilityHour
                                    StabilityDays = .StabilityDays
                                End If

                                OsmolarityTotal = IIf(.OsmolarityTotal Is Nothing, Nothing, .OsmolarityTotal)
                                VolumeTotalOrderPurga = IIf(.VolumeTotalOrderPurga Is Nothing, Nothing, .VolumeTotalOrderPurga)
                                WeightTotalSolution = IIf(.WeightTotalSolution Is Nothing, Nothing, .WeightTotalSolution)

                                VolumeTotalOrder = IIf(.VolumeTotalOrder Is Nothing, Nothing, .VolumeTotalOrder)
                                INDsleVolumeTotalOrderMeasurementUnit.EditValue = .VolumeTotalOrderMeasurementUnitId
                                INDsleVolumeTotalOrderMeasurementUnit.Properties.NullText = .VolumeTotalOrderMeasurementUnitCodeName
                                INDGleVehicleOptimization.EditValue = .VehicleOptimization
                                INDslePhotoProtection.EditValue = .PhotoProtection

                                ListPackageDetail = .PackageDetail.ToList
                                INDGcComponents.DataSource = Nothing
                                INDGcComponents.DataSource = ListPackageDetail

                                If .Concentration IsNot Nothing Then
                                    Concentration = .Concentration
                                    INDsleConcentrationMeasurementUnit.EditValue = .ConcentrationMeasurementUnitId
                                    INDsleConcentrationMeasurementUnit.Properties.NullText = .ConcentrationMeasurementUnitCodeName
                                End If

                                ConcentrationAntibiotic = .ConcentrationAntibiotic
                                PreparationTypeAntibiotic = .PreparationType
                                VolumeTotalPrepared = .VolumeTotalPrepared

                                If .MeasurementPreparedId IsNot Nothing Then
                                    MeasurementPreparedId = .MeasurementPreparedId
                                    INDsleMeasurementUnitPrepared.Properties.NullText = .MeasurementPreparedUnitCodeName
                                End If

                                If _unitDoseTypeEntity Is Nothing Then
                                    Await CargarUnitDoseType()
                                End If

                                If _unitDoseTypeEntity.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                                    If .NptId IsNot Nothing Then
                                        NptId = .NptId
                                        Dim NPT = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of HCPARNUTCXpo)($"ID = {NptId}")
                                        INDsleTypeNPT.Properties.NullText = NPT.NAME

                                        If .MainDrugId IsNot Nothing Then
                                            MainDrugId = .MainDrugId
                                            INDSleMainDrug.Properties.NullText = .MainDrugCodeName
                                        Else
                                            Mensaje(EeventViewerImages.Advertencia) = "El medicamento de referencia en la plantilla NPT se encuentra vacío (Parametrice el código de producto terminado en la plantilla NPT). Este dato es obligatorio en este tipo de dosis para asignar el código del producto terminado"
                                        End If

                                        Dim EnableTypeNpt = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetCollection(Of RequestMixingStationDetailXpo)(Function(m) m.PackageId IsNot Nothing AndAlso m.PackageId = _packageEntity.Id).FirstOrDefault

                                        If EnableTypeNpt IsNot Nothing Then
                                            NTPEnabled = False
                                        End If
                                    End If

                                    RefreshDescriptionNPT()
                                    INDGcComponents.DataSource = ListPackageDetail.OrderBy(Function(x) If(x.NPTItemOrder.HasValue, 0, 1)). ' Los NULL (Nothing) van al final
                                                                    ThenBy(Function(x) x.NPTItemOrder.GetValueOrDefault(255)). ' Orden ascendente por NPTItemOrder
                                                                    ToList()
                                Else
                                    Await RefreshDescription()
                                End If

                            End With

                            AllowsEditPackage = Await Model.GetProductionPackageAsync(INDbteCode.Text.Trim)

                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._packageEntity.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _packageEntity.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_packageEntity.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConceptNotes).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True

                            If AllowsEditPackage Then
                                ControlsReadOnly = False
                            ElseIf ProductId IsNot Nothing OrElse ProductId <> 0 Then
                                ControlsReadOnly = True
                                INDsleProduct.Properties.ReadOnly = True
                            Else
                                ControlsReadOnly = True
                            End If

                            ' Evaluar visibilidad del tipo de estabilidad después de cargar el paquete
                            EvaluateStabilityTypeVisibility()
                        End Using
                    Else
                        INDBtnAddProducts.Enabled = True
                        AsyncLoader(False)

                        If Me._sequence.IsManual Then
                            Await Me.NewPackage()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If

                    INDsleTypeNPT.Enabled = NTPEnabled
                    _isLoading = False
                    INDlycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                _isLoading = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPackage() As Task
        _packageEntity = New Package() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequence))
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
    ''' Metodo que abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                              New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StateName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Tipo de Dosis Unitaria", .FieldName = "UnitDoseTypeId.Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Clase", .FieldName = "UnitDoseTypeId.ClassName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPackage
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo que cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me._packageEntity.Code) Then
            Try
                Using model As New MPackage(Me.Tag)
                    AsyncLoader(True)
                    Dim _state As Boolean = Not Me._packageEntity.State
                    Dim result As ActionResult(Of Package) = Await model.ChangeState(Me._packageEntity.Code, _state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._packageEntity = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Metodo que asigna los valores
    ''' </summary>
    Private Sub AssigningValues()
        With _packageEntity
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .IsPackagePersonalized = IsPersonalizedMasterPreparation
            .Code = Code
            .Name = PackageName
            .Description = Description
            .RiskLevelId = InventoryRiskLevelId
            .CodeAlternative = CodeAlternative
            .PhotoProtection = INDslePhotoProtection.EditValue
            .PreparationType = PreparationTypeAntibiotic
            .Storage = Storage
            .ConcentrationAntibiotic = ConcentrationAntibiotic
            .VolumeTotalPrepared = VolumeTotalPrepared
            .MeasurementPreparedId = MeasurementPreparedId
            .Readjustments = Readjustments

            If TypeStability IsNot Nothing Then
                .TypeStability = TypeStability
                .StabilityHour = StabilityHour
                .StabilityDays = StabilityDays
            End If

            .EnvironmentalTemperatureTerm = EnvironmentalTemperatureTerm
            .Purge = Purge
            .PreparationInstructions = PreparationInstructions
            .SpecialConsiderations = SpecialConsiderations
            .UnitDoseTypeId = UnitDoseTypeId
            .UnitDoseType = _unitDoseTypeEntity
            .NptId = NptId
            .MainDrugId = MainDrugId
            .VehicleOptimization = CBool(INDGleVehicleOptimization.EditValue)
            .PersonalizedMasterPreparation = IsPersonalizedMasterPreparation
            .OsmolarityTotal = OsmolarityTotal
            .VolumeTotalOrder = VolumeTotalOrder
            .VolumeTotalOrderMeasurementUnitId = INDsleVolumeTotalOrderMeasurementUnit.EditValue
            .VolumeTotalOrderPurga = VolumeTotalOrderPurga
            .WeightTotalSolution = WeightTotalSolution
            .StandardMix = True
            .LabelType = INDGleType.EditValue

            If IsPersonalizedMasterPreparation Then 'Si es una preparación magistral personalizada se asigna el id del paquete asociado
                .AssociatedPackageId = MixinStationPackageXpo.AssociatedPackageId
            Else
                .AssociatedPackageId = Nothing
            End If

            .Concentration = Nothing
            .ConcentrationMeasurementUnitId = Nothing
            If INDliConcentration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Concentration = Concentration
                .ConcentrationMeasurementUnitId = INDsleConcentrationMeasurementUnit.EditValue
            End If

            .ProductId = Nothing
            If INDliProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ProductId = Me.ProductId
            End If

            If _listDeletePackageDetail?.Any() Then
                For Each item In _listDeletePackageDetail
                    item.MarkAsDeleted()
                    .PackageDetail.Add(item)
                Next
            End If

            Dim ListView As List(Of PackageDetail) = TryCast(INDGcComponents.DataSource, List(Of PackageDetail)).ToList()
            For Each item In ListPackageDetail
                item.CreationUser = Me.indigo.UserIndigo
                item.CreationDate = DateTime.Now

                Dim measureUnitPart As String = String.Empty
                If Not String.IsNullOrEmpty(item.MeasureUnitDescription) AndAlso item.MeasureUnitDescription.Contains("-") Then
                    Dim parts = item.MeasureUnitDescription.Split("-"c)
                    If parts.Length > 1 Then
                        measureUnitPart = parts(1).Trim()
                    End If
                End If
                item.Dosis = $"{item.Quantity} {measureUnitPart}"
                item.QuantityMeasureunitname = $"{item.Quantity} {item.MeasurementUnitAbbreviation}"

                If _unitDoseTypeEntity.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                    item.NPTItemOrder = CType(ListView.FindIndex(Function(x) x.Id = item.Id), Byte?)
                End If


                .PackageDetail.Add(item)
            Next
        End With
    End Sub

    Private Sub CleanControlsAntibiotic()
        INDtxtConcentration.EditValue = Nothing
        PreparationTypeAntibiotic = Nothing
        INDslePreparationType.Properties.NullText = String.Empty
        INDtxtConcentration.Properties.NullText = String.Empty
        MeasurementPreparedId = Nothing
        INDTotalVolumePrepared.EditValue = Nothing
        INDsleMeasurementUnitPrepared.Properties.NullText = String.Empty
        INDmeDescription.EditValue = Nothing
    End Sub

    Private Sub CleanControlsNPT()
        NptId = Nothing
        INDsleTypeNPT.Properties.NullText = String.Empty
        MainDrugId = Nothing
        INDSleMainDrug.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()

        Me.SuspendLayout()
        Me.ActiveControl = Nothing
        Me.AutoScroll = False
        INDlycRoot.BeginUpdate()

        ActionsOnControls = False
        ControlsReadOnly = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        Me._doc = Nothing
        UnitDoseTypeId = Nothing
        ProductId = Nothing
        Storage = Nothing
        Readjustments = Nothing
        INDslePhotoProtection.EditValue = False
        PreparationTypeAntibiotic = Nothing
        TypeStability = Nothing
        StabilityHour = Nothing
        StabilityDays = Nothing
        EnvironmentalTemperatureTerm = Nothing
        Purge = Nothing
        PreparationInstructions = Nothing
        SpecialConsiderations = Nothing
        Concentration = Decimal.Zero
        INDsleConcentrationMeasurementUnit.EditValue = Nothing
        OsmolarityTotal = Nothing
        VolumeTotalOrder = Nothing
        INDsleVolumeTotalOrderMeasurementUnit.EditValue = Nothing
        VolumeTotalOrderPurga = Nothing
        WeightTotalSolution = Nothing
        VolumeTotalPrepared = Nothing
        MeasurementPreparedId = Nothing
        INDTotalVolumePrepared.EditValue = Nothing
        INDGleVehicleOptimization.EditValue = Nothing
        INDGleType.EditValue = Nothing
        ListPackageDetail = Nothing
        ListPackageDetailAntibiotic = Nothing
        _packageEntity = Nothing
        dictionaryATC = Nothing
        INDgcStabilityTable.DataSource = Nothing
        _itemCopy = Nothing
        InventoryRiskLevelId = Nothing

        State = True
        _openPopUpRiskLevel = False
        INDlyItemPreparationType.AllowHide = True
        INDliRiskLevel.ShowInCustomizationForm = True
        INDliConcentration.AllowHide = True
        INDsleUnitDoseType.Properties.ReadOnly = False
        INDsleProduct.Properties.ReadOnly = False
        INDlyItemConcentrationMeasurementUnit.AllowHide = True
        INDGleType.Properties.ReadOnly = False
        AllowsEditPackage = True

        Code = String.Empty
        PackageName = String.Empty
        CodeAlternative = String.Empty
        ConcentrationAntibiotic = String.Empty
        Description = String.Empty
        INDsleConcentrationMeasurementUnit.Properties.NullText = String.Empty
        INDsleVolumeTotalOrderMeasurementUnit.Properties.NullText = String.Empty
        INDsleMeasurementUnitPrepared.Properties.NullText = String.Empty
        INDsleProduct.Properties.NullText = String.Empty
        INDsleUnitDoseType.Properties.NullText = String.Empty
        INDGleVehicleOptimization.Properties.NullText = String.Empty
        INDgleStorage.Properties.NullText = String.Empty
        INDGleType.Properties.NullText = String.Empty
        INDsleRiskLevel.Properties.NullText = String.Empty

        INDliConcentration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemConcentrationMeasurementUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAssociatedPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPreparationType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliConcentrationAntibiotic.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTotalVolumePrepared.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemMeasurementUnitPrepared.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTotalOsmolarity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTotalOrderVolumePurge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTotalSolutionWeight.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliTotalVolumePrepared.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDliStabilityHour.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliStabilityDays.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        CleanControlsNPT()

        _listDeletePackageDetail.Clear()
        INDGcComponents.DataSource = ListPackageDetail

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        Await DeleteBlockedrecord()

        INDlycRoot.EndUpdate()
        Me.AutoScroll = True
        Me.ResumeLayout()
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnAddPackageDetail(sender As Object, e As AddProductPackageDetailEventArgs)
        Dim errors As New StringBuilder
        Try
            AsyncLoader(True)
            If ListPackageDetail Is Nothing Then
                ListPackageDetail = New List(Of PackageDetail)
            End If
            If e.FormulationType > 0 Then
                If e.FormulationType = 3 Then
                    INDTotalVolumePrepared.Value = e.VolumenTotalVehicle
                    INDTotalVolumePrepared.Properties.ReadOnly = True

                    ConcentrationWeightVolumen = e.ConcentrationWeightVolumen
                Else
                    INDTotalVolumePrepared.Properties.ReadOnly = False
                End If

                FormulationType = e.FormulationType
            End If

            If Not {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(CType((_unitDoseTypeEntity?.MSClass), EUnitDoseTypeClass)) Then
                If e.EditMode Then
                    If _itemCopy.MainMedicine AndAlso (_itemCopy.AtcId <> e.ItemPackageDetail.AtcId Or
                    _itemCopy.MainMedicine <> e.ItemPackageDetail.MainMedicine) Then
                        INDsleProduct.Properties.DataSource = Nothing
                    End If

                    ListPackageDetail.Remove(_itemPackageDetail)
                    ListPackageDetail.Insert(indexEditRecord, e.ItemPackageDetail)
                Else
                    If _unitDoseTypeEntity.MSClass = EUnitDoseTypeClass.ParenteralNutrition And Not e.ItemPackageDetail.ComponentType = 5 Then
                        If ListPackageDetail.Exists(Function(x) CBool(x.NPTItemOrder.HasValue And x.ComponentType <> 5)) Then

                            Dim maxOrder As Integer? = ListPackageDetail.Where(Function(x) CBool(x.NPTItemOrder.HasValue And x.ComponentType <> 5)).Max(Function(x) x.NPTItemOrder.GetValueOrDefault(0))
                            If maxOrder IsNot Nothing Then e.ItemPackageDetail.NPTItemOrder = CType(maxOrder + 1, Byte?)

                        End If
                    End If

                    ListPackageDetail.Add(e.ItemPackageDetail)
                End If

                CalculateValues()
            Else
                If e.ItemsPackageDetailAntibiotic.Any() Then
                    For Each ItemAntibiotic In e.ItemsPackageDetailAntibiotic
                        ListPackageDetail.Add(ItemAntibiotic)
                    Next
                Else
                    If e.EditMode Then
                        ListPackageDetail.Remove(_itemPackageDetail)
                        ListPackageDetail.Insert(indexEditRecord, e.ItemPackageDetail)
                    Else
                        ListPackageDetail.Add(e.ItemPackageDetail)
                    End If
                End If

                ' Procesar detalles modificados cuando se agregó un medicamento complementario o se editó un principal intratecal
                If e.ModifiedPackageDetails IsNot Nothing AndAlso e.ModifiedPackageDetails.Any() Then
                    ' Los detalles ya fueron modificados por referencia en la lista
                    ' Solo necesitamos refrescar la rejilla para mostrar los cambios
                    ' Opcional: Mostrar mensaje informando de los ajustes realizados
                    Dim mainMedicineModified = e.ModifiedPackageDetails.FirstOrDefault(Function(x) x.MainMedicine.GetValueOrDefault(False))
                    Dim vehicleModified = e.ModifiedPackageDetails.FirstOrDefault(Function(x) x.Vehicle)

                    Dim adjustmentMessage As New System.Text.StringBuilder()
                    adjustmentMessage.AppendLine("Se han realizado ajustes automáticos:")

                    If mainMedicineModified IsNot Nothing Then
                        adjustmentMessage.AppendLine($"• Medicamento principal ajustado a: {mainMedicineModified.QuantityMeasureunitname}")
                    End If

                    If vehicleModified IsNot Nothing Then
                        adjustmentMessage.AppendLine($"• Vehículo ajustado a: {vehicleModified.QuantityMeasureunitname}")
                    End If

                    ' Solo mostrar mensaje si hay algo que reportar
                    If mainMedicineModified IsNot Nothing OrElse vehicleModified IsNot Nothing Then
                        MessageIndigo.Show(adjustmentMessage.ToString(), MessageType.Information, Me.Text)
                    End If
                End If

                Await CalculateValuesAntibiotic()
            End If

            HideOrShowLabelsForUnitDoseType(_unitDoseTypeEntity?.MSClass)

            EvaluateStabilityTypeVisibility()

            Await RefreshDescription()
            RefrescarRejilla()

            Dim mainMedicineDetail = ListPackageDetail.FirstOrDefault(Function(a) a.MainMedicine.HasValue AndAlso a.MainMedicine.Value)
            If mainMedicineDetail IsNot Nothing Then
                Dim inventoryRisk = _presenter.GetInventoryRiskByATCId(mainMedicineDetail.AtcId.Value)
                InventoryRiskLevelId = inventoryRisk.Id
                INDsleRiskLevel.Properties.NullText = inventoryRisk.CodeName
            End If

            'Si esta en modo de edición y el formulario se abrió desde la dashboard y esta marcado como vahiculo el detalle
            If e.EditMode AndAlso IsDashboardConfirmationUnitDose AndAlso e.ItemPackageDetail IsNot Nothing AndAlso e.ItemPackageDetail.Vehicle AndAlso _unitDoseTypeEntity.MSClass <> EUnitDoseTypeClass.Magistral Then
                If e.ItemPackageDetail.ItemDilutionXpo IsNot Nothing Then
                    Dim info = CType(e.ItemPackageDetail.ItemDilutionXpo, StabilityTableDetailDilutionXpo)
                    INDslePhotoProtection.EditValue = info.PhotoProtection
                    INDspnStabilityHours.EditValue = info.HourStability
                    INDspnEnvironmentalTemperatureTerm.EditValue = info.HourStability
                    INDmePreparationInstructions.EditValue = info.StabilityTableDetailId.Observations
                    INDmeSpecialConsiderations.EditValue = info.StabilityTableDetailId.Observations
                ElseIf e.ItemPackageDetail.ItemReconstitutionXpo IsNot Nothing Then
                    Dim info = CType(e.ItemPackageDetail.ItemReconstitutionXpo, StabilityTableDetailReconstitutionXpo)
                    INDspnStabilityHours.EditValue = info.HourStability
                    INDspnEnvironmentalTemperatureTerm.EditValue = info.HourStability
                    INDmePreparationInstructions.EditValue = info.StabilityTableDetailId.Observations
                    INDmeSpecialConsiderations.EditValue = info.StabilityTableDetailId.Observations
                End If
            End If

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Private Sub RefreshDescriptionNPT()
        If ListPackageDetail IsNot Nothing AndAlso ListPackageDetail.Any() Then
            Dim description = String.Join(" " + " + ", ListPackageDetail() _
            .Select(Function(m) $"{m.DCIName}").ToArray())
            INDmeDescription.Text = description
        End If
    End Sub

    ''' <summary>
    ''' Se crea la descripcion del paquete teniendo en cuenta el orden
    ''' 1. MainMedicine, 2 vehicle y 3. Dilution
    ''' </summary>
    Private Async Function RefreshDescription() As Task
        Try
            If ListPackageDetail?.Any() Then
                INDmeDescription.Text = String.Empty

                Dim volumeMainMedicine = Task.Run(Function() _
                ListPackageDetail _
                .Where(Function(m) m.ComponentType = 1 AndAlso m.MainMedicine = True AndAlso m.Volume.HasValue) _
                .Sum(Function(m) m.Volume.Value))

                ' Obtener los componentes ordenados de forma asincrónica.
                ' Orden: 1-Principal, 2-Complementario, 3-Vehículo, 4-Diluyente, 5-Otros
                Dim orderedComponents = Task.Run(Function()
                                                     Dim getComponentOrder = Function(m As PackageDetail) As Integer
                                                                                 If m.MainMedicine = True Then
                                                                                     Return 1
                                                                                 ElseIf m.ComplementaryMedicine = True Then
                                                                                     Return 2
                                                                                 ElseIf m.Vehicle = True Then
                                                                                     Return 3
                                                                                 ElseIf m.Thinner = True Then
                                                                                     Return 4
                                                                                 Else
                                                                                     Return 5
                                                                                 End If
                                                                             End Function

                                                     Return ListPackageDetail _
                                                         .Where(Function(m) m.ComponentType = 1) _
                                                         .OrderBy(getComponentOrder) _
                                                         .Select(Function(m)
                                                                     Dim cantidadTotal As Decimal = If(Not m.Volume.HasValue And m.MainMedicine = False AndAlso m.ComplementaryMedicine <> True,
                                                                                                       m.Quantity.Value + Math.Round(volumeMainMedicine.Result, 2),
                                                                                                       If(m.Quantity, 0))
                                                                     Return $"{m.DCIName} {cantidadTotal} {m.MeasurementUnitAbbreviation}"
                                                                 End Function) _
                                                         .ToList()
                                                 End Function)

                Dim volumeMainMedicinetmp = Await volumeMainMedicine
                Dim orderedComponentsTmp = Await orderedComponents

                INDmeDescription.Text = String.Join(" + ", orderedComponentsTmp)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Refresca los datos de la rejilla
    ''' </summary>
    Private Sub RefrescarRejilla()
        If Me.ListPackageDetail Is Nothing Then
            Me.ListPackageDetail = New List(Of PackageDetail)
        End If

        INDGcComponents.DataSource = ListPackageDetail.OrderBy(Function(x) If(x.NPTItemOrder.HasValue, 0, 1)). ' Los NULL (Nothing) van al final
                                        ThenBy(Function(x) x.NPTItemOrder.GetValueOrDefault(255)). ' Orden ascendente por NPTItemOrder
                                        ToList()
        INDGvComponents.RefreshData()
        INDGcComponents.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Cálcula los valores de totalizados si el paquete es de tipo de dosis unitaria de naturaleza NPT
    ''' </summary>
    Private Sub CalculateValues()
        Dim totOsmolarity, totVolPurge, totWeigth, totVolumeThinner As Decimal?

        totOsmolarity = 0
        totVolPurge = 0
        totWeigth = 0
        totVolumeThinner = 0
        VolumeTotalOrder = 0

        INDsleVolumeTotalOrderMeasurementUnit.EditValue = Nothing
        INDsleVolumeTotalOrderMeasurementUnit.Properties.NullText = String.Empty
        If ListPackageDetail?.Any() Then
            For Each item In ListPackageDetail
                If item.ComponentType = 1 Then
                    totOsmolarity += item.Osmolarity
                    If item.Thinner Then
                        If item.Quantity IsNot Nothing Then
                            totVolumeThinner = item.Quantity
                        End If
                    End If
                End If

                If item.Quantity IsNot Nothing Then
                    If _unitDoseTypeEntity?.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then 'Si el tipo de dosis unitaria es NPT
                        If item.ComponentType = 1 Then 'Si es medicamento se suma
                            totVolPurge += item.Quantity
                        End If
                    End If
                    totWeigth += (item.Quantity + Purge) * item.Density
                End If
            Next

            Dim totVolume = (From x In ListPackageDetail Where (x.Thinner = True OrElse x.Vehicle = True) AndAlso x.ComponentType = 1 Select x.Quantity).Sum()
            Dim infoValidate = (From x In ListPackageDetail Where (x.Thinner = True OrElse x.Vehicle = True) AndAlso x.ComponentType = 1 Select x).FirstOrDefault()
            Dim MainMedicine = (From x In ListPackageDetail Where x.MainMedicine = True AndAlso x.ComponentType = 1 Select x).FirstOrDefault()
            ' Obtener la suma de cantidades de los medicamentos complementarios/adicionales
            ' Todos tienen MainMedicine = False y ComplementaryMedicine = True
            Dim complementarySum As Decimal = ListPackageDetail.Where(Function(x) _
                x.ComplementaryMedicine.GetValueOrDefault(False) AndAlso
                Not x.MainMedicine.GetValueOrDefault(False) AndAlso
                x.ComponentType = 1).Sum(Function(x) x.Quantity.GetValueOrDefault(0D))

            If infoValidate IsNot Nothing Then
                If (From x In ListPackageDetail Where (x.Thinner = True OrElse x.Vehicle = True) AndAlso x.ComponentType = 1).Count = (From x In ListPackageDetail Where (x.Thinner = True OrElse x.Vehicle = True) AndAlso x.ComponentType = 1 AndAlso x.MeasurementUnitId = infoValidate.MeasurementUnitId).Count Then
                    VolumeTotalOrder = totVolume
                    INDsleVolumeTotalOrderMeasurementUnit.EditValue = infoValidate.MeasurementUnitId
                    INDsleVolumeTotalOrderMeasurementUnit.Properties.NullText = infoValidate.MeasureUnitDescription
                End If
            End If

            If MainMedicine IsNot Nothing Then
                Select Case _unitDoseTypeEntity?.MSClass
                    Case EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile
                        ' Para Citostático/intratecal: sumar principal + complementarios/adicionales
                        Dim totalMainQuantity As Decimal = MainMedicine.Quantity.GetValueOrDefault(0D) + complementarySum
                        If totVolume > 0 AndAlso totalMainQuantity > 0 Then
                            Dim result As Decimal = totalMainQuantity / totVolume
                            Concentration = CDec(Utils.SetPartDecimalToValue(result))
                        End If

                    Case EUnitDoseTypeClass.Refilling
                        If MainMedicine.Volume.GetValueOrDefault(0D) > 0 Then
                            VolumeTotalPrepared = MainMedicine.Volume
                            MeasurementPreparedId = MainMedicine.VolumeMeasureUnit
                            INDsleMeasurementUnitPrepared.Properties.NullText = MainMedicine.VolumeMeasureUnitDescription

                            Dim TotalConcentration As Decimal = MainMedicine.Quantity.GetValueOrDefault(0D) / MainMedicine.Volume.Value
                            TotalConcentration = CDec(Utils.SetPartDecimalToValue(TotalConcentration))
                            ConcentrationAntibiotic = $"{TotalConcentration} {MainMedicine.MeasurementUnitAbbreviation} / { MainMedicine.VolumeMeasureUnitAbbreviation}"
                        End If
                End Select
            Else
                ConcentrationAntibiotic = String.Empty
                Concentration = Decimal.Zero
                INDsleConcentrationMeasurementUnit.EditValue = Nothing
                VolumeTotalPrepared = Decimal.Zero
                MeasurementPreparedId = Nothing
            End If
        End If

        If INDliTotalOsmolarity.Visible Then
            OsmolarityTotal = totOsmolarity
            VolumeTotalOrderPurga = totVolPurge + Purge
            WeightTotalSolution = totWeigth
        End If
    End Sub

    ''' <summary>
    ''' Cálcula los valores de totalizados si el paquete es de tipo de dosis unitaria antibioticoterapia
    ''' </summary>
    Private Async Function CalculateValuesAntibiotic() As Task
        If Not ListPackageDetail?.Any() Then
            Exit Function
        End If

        Dim Mainmedicine, Vehicle, Thinner As PackageDetail
        Dim TotalVolume As Decimal

        TotalVolume = Decimal.Zero
        Mainmedicine = Nothing
        Vehicle = Nothing
        Thinner = Nothing

        For Each item In ListPackageDetail
            If item.Thinner AndAlso Thinner Is Nothing Then
                Thinner = item
            ElseIf item.MainMedicine AndAlso Mainmedicine Is Nothing Then
                Mainmedicine = item
            ElseIf item.Vehicle AndAlso Vehicle Is Nothing Then
                Vehicle = item
            End If
        Next

        ' Calcular la suma de cantidades de medicamentos complementarios/adicionales
        ' Todos tienen MainMedicine = False y ComplementaryMedicine = True
        ' Esto incluye tanto los complementarios del Dashboard como los adicionales de intratecal
        Dim complementaryQuantitySum As Decimal = 0D
        Dim complementaryMedicines = ListPackageDetail.Where(Function(x) _
            x.ComplementaryMedicine.GetValueOrDefault(False) AndAlso Not x.MainMedicine.GetValueOrDefault(False))
        If complementaryMedicines.Any() Then
            complementaryQuantitySum = complementaryMedicines.Sum(Function(x) x.Quantity.GetValueOrDefault(0D))
        End If

        If ListPackageDetail.Any(Function(x) x.PreparationType.HasValue AndAlso x.PreparationType = 1) AndAlso Thinner IsNot Nothing Then
            TotalVolume = Thinner.Quantity.GetValueOrDefault(0D)
            PreparationTypeAntibiotic = Thinner.PreparationType
        End If

        If ListPackageDetail.Any(Function(x) x.PreparationType.HasValue AndAlso x.PreparationType = 2) AndAlso Vehicle IsNot Nothing Then
            ' Usar VolumeTotal del vehículo como volumen total del preparado si está disponible
            TotalVolume = If(Vehicle.VolumeTotal.GetValueOrDefault(0D) > 0, Vehicle.VolumeTotal.Value, Vehicle.Quantity.GetValueOrDefault(0D))
            PreparationTypeAntibiotic = Vehicle.PreparationType
        End If

        If ListPackageDetail.Any(Function(x) x.PreparationType.HasValue AndAlso x.PreparationType = 3) AndAlso Vehicle IsNot Nothing Then
            ' Usar VolumeTotal del vehículo como volumen total del preparado si está disponible
            Dim thinnerQty As Decimal = If(Thinner IsNot Nothing, Thinner.Quantity.GetValueOrDefault(0D), 0D)
            Dim vehicleVolume As Decimal = If(Vehicle.VolumeTotal.GetValueOrDefault(0D) > 0, Vehicle.VolumeTotal.Value, thinnerQty + Vehicle.Quantity.GetValueOrDefault(0D))
            TotalVolume = vehicleVolume
            If Thinner IsNot Nothing Then
                PreparationTypeAntibiotic = Thinner.PreparationType
            End If
        End If

        If ListPackageDetail.Exists(Function(y) y.PreparationType.HasValue AndAlso y.PreparationType = 4) AndAlso Mainmedicine IsNot Nothing AndAlso Mainmedicine.ATC IsNot Nothing Then
            PreparationTypeAntibiotic = Mainmedicine.PreparationType
            ConcentrationAntibiotic = Mainmedicine.ATC.Concentration
            VolumeTotalPrepared = Mainmedicine.ATC.Volume
            MeasurementPreparedId = Mainmedicine.ATC.VolumeMeasureUnit
            INDsleMeasurementUnitPrepared.Properties.NullText = Mainmedicine.ATC.NullTextUnitMeasureVolumen

            If _unitDoseTypeEntity.MSClass = EUnitDoseTypeClass.Cytostatic Then
                Dim medicinesWithPrepTypeNone = ListPackageDetail?.
                    Where(Function(x) x.ComponentType = 1 AndAlso x.PreparationType.HasValue AndAlso x.PreparationType = 4).ToList()

                If medicinesWithPrepTypeNone IsNot Nothing AndAlso medicinesWithPrepTypeNone.Count > 0 Then
                    ' Valor por defecto para el volumen
                    VolumeTotalPrepared = medicinesWithPrepTypeNone.
                        Where(Function(x) x.MainMedicine).
                        Sum(Function(x) x.Volume)

                    Dim allPesoVolumen As Boolean = medicinesWithPrepTypeNone.All(Function(x) x.ATC IsNot Nothing AndAlso x.ATC.FormulationType = 3)

                    If allPesoVolumen Then
                        ' Validar que todas las unidades de medida sean iguales
                        Dim firstMeasureUnitId As Integer? = medicinesWithPrepTypeNone.FirstOrDefault()?.MeasurementUnitId
                        Dim firstMeasureUnitAbbrev As String = medicinesWithPrepTypeNone.FirstOrDefault()?.MeasurementUnitAbbreviation
                        Dim allSameMeasureUnit As Boolean = medicinesWithPrepTypeNone.All(Function(x) x.MeasurementUnitId = firstMeasureUnitId)

                        ' Validar que todas las unidades de volumen sean iguales
                        Dim firstVolumeMeasureUnitId As Integer? = medicinesWithPrepTypeNone.FirstOrDefault()?.VolumeMeasureUnit
                        Dim firstVolumeMeasureUnitAbbrev As String = medicinesWithPrepTypeNone.FirstOrDefault()?.VolumeMeasureUnitAbbreviation
                        Dim allSameVolumeMeasureUnit As Boolean = medicinesWithPrepTypeNone.All(Function(x) x.VolumeMeasureUnit = firstVolumeMeasureUnitId)

                        If Not allSameMeasureUnit OrElse Not allSameVolumeMeasureUnit Then
                            Mensaje(EeventViewerImages.Advertencia) = "Los componentes del paquete intratecal deberían de ser Peso / Volumen"
                            Exit Function
                        End If

                        Dim totalQuantity As Decimal = Mainmedicine.Quantity + medicinesWithPrepTypeNone.Sum(Function(x) x.Quantity.GetValueOrDefault(0D))
                        VolumeTotalPrepared = Vehicle.VolumeTotal

                        If VolumeTotalPrepared > 0 AndAlso totalQuantity > 0 Then
                            Dim concentrationValue As Decimal = totalQuantity / VolumeTotalPrepared
                            concentrationValue = CDec(Utils.SetPartDecimalToValue(concentrationValue))

                            ConcentrationAntibiotic = $"{concentrationValue} {firstMeasureUnitAbbrev}/{firstVolumeMeasureUnitAbbrev}"
                            MeasurementPreparedId = firstVolumeMeasureUnitId
                            INDsleMeasurementUnitPrepared.Properties.NullText = medicinesWithPrepTypeNone.FirstOrDefault()?.VolumeMeasureUnitDescription
                        End If
                    End If
                End If
            End If

            Exit Function
        End If

        Dim measurementUnit = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MeasureUnitXpo)($"Abbreviation = 'ml'"))
        If measurementUnit Is Nothing Then Exit Function

        MeasurementPreparedId = measurementUnit.Id
        INDsleMeasurementUnitPrepared.Properties.NullText = measurementUnit.CodeName

        'Evaluacion cuanto el tipo de medicamento es Peso - Volumen
        If FormulationType <> 3 Then
            If TotalVolume > 0 AndAlso Mainmedicine IsNot Nothing Then
                ' Cantidad total = Principal + Complementarios/Adicionales
                Dim totalMedicineQuantity As Decimal = Mainmedicine.Quantity.GetValueOrDefault(0D) + complementaryQuantitySum
                Dim TotalConcentration As Decimal = totalMedicineQuantity / TotalVolume
                TotalConcentration = CDec(Utils.SetPartDecimalToValue(TotalConcentration))
                VolumeTotalPrepared = TotalVolume
                ConcentrationAntibiotic = $"{TotalConcentration} {Mainmedicine.MeasurementUnitAbbreviation} / {measurementUnit.Abbreviation}"
            End If
        Else
            ' FormulationType = 3 (Peso - Volumen): Recalcular la concentración
            ' Concentración = (Cantidad Principal + Cantidad Complementarios/Adicionales) / Volumen Total Preparado
            If Mainmedicine IsNot Nothing AndAlso Vehicle IsNot Nothing Then
                ' Obtener el volumen total del preparado (VolumeTotal del vehículo)
                Dim volumeTotalPreparado As Decimal = Vehicle.VolumeTotal.GetValueOrDefault(0D)
                If volumeTotalPreparado = 0 Then
                    ' Si VolumeTotal es 0, calcular como la suma del volumen del medicamento + cantidad del vehículo
                    Dim volumenMedicamento As Decimal = Mainmedicine.Volume.GetValueOrDefault(0D)
                    Dim volumenVehiculo As Decimal = Vehicle.Quantity.GetValueOrDefault(0D)
                    volumeTotalPreparado = volumenMedicamento + volumenVehiculo
                End If

                If volumeTotalPreparado > 0 Then
                    ' Cantidad total = Principal + Complementarios/Adicionales
                    Dim totalMedicineQuantity As Decimal = Mainmedicine.Quantity.GetValueOrDefault(0D) + complementaryQuantitySum
                    ' Concentración = Peso total / Volumen total
                    ConcentrationWeightVolumen = totalMedicineQuantity / volumeTotalPreparado
                    ConcentrationWeightVolumen = CDec(Utils.SetPartDecimalToValue(ConcentrationWeightVolumen))
                    VolumeTotalPrepared = volumeTotalPreparado
                    ConcentrationAntibiotic = $"{ConcentrationWeightVolumen} {Mainmedicine.MeasurementUnitAbbreviation} / {measurementUnit.Abbreviation}"
                End If
            ElseIf Mainmedicine IsNot Nothing Then
                ' Fallback: Si no hay vehículo, mantener el comportamiento anterior
                ConcentrationWeightVolumen = CDec(Utils.SetPartDecimalToValue(ConcentrationWeightVolumen))
                ConcentrationAntibiotic = $"{ConcentrationWeightVolumen} {Mainmedicine.MeasurementUnitAbbreviation} / {measurementUnit.Abbreviation}"
            End If
        End If
    End Function

    ''' <summary>
    ''' Método para agregar acciones a la rejilla de productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns(Optional _MSclass As Integer = 0)
        Dim ListActions As New List(Of eAcciones)

        If _MSclass <> EUnitDoseTypeClass.ParenteralNutrition Then
            ListActions.Add(eAcciones.Remove)
            ListActions.Add(eAcciones.Edit)
        Else
            If ListPackageDetail IsNot Nothing AndAlso ListPackageDetail.Any(Function(item) item.ComponentType = 5) Then
                ListActions.Add(eAcciones.Remove)
            End If
        End If

        IndigoGridView1.SetListAcction(INDGvComponents, ListActions)
        If Not ListActions.Any() Then
            INDGvComponents.Columns.ColumnByName("colActions").Visible = False
        End If

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvComponents.Columns
            If col.Name = "colActions" OrElse col.Name = "MoreInfo" Then
                col.Width = 75
            End If
        Next
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub EditDetail()
        Try
            AsyncLoader(True)

            If Not AllowsEditPackage Then
                Mensaje(EeventViewerImages.Advertencia) = "El paquete ya fue asociado a un proceso de producción y no se puede editar los componentes"
                Exit Sub
            End If

            'Se valida que hayan seleccionado un tipo de dosis unitaria
            If _unitDoseTypeEntity Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un tipo de dosis unitaria"
                Exit Sub
            End If

            _itemPackageDetail = DirectCast(INDGvComponents.GetFocusedRow(), PackageDetail)

            ' Verificar si es un medicamento complementario - se edita individualmente, no con la estructura del principal
            Dim isComplementaryMedicine As Boolean = _itemPackageDetail.ComplementaryMedicine.GetValueOrDefault(False)

            If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(_unitDoseTypeEntity.MSClass) _
               AndAlso _itemPackageDetail.ComponentType = 1 _
               AndAlso Not isComplementaryMedicine Then

                ' Verificar si es intratecal (múltiples medicamentos principales)
                Dim countMainMedicines = ListPackageDetail.Where(Function(x) x.MainMedicine.GetValueOrDefault(False) AndAlso x.ComponentType = 1).Count()
                Dim isIntrathecal As Boolean = (_unitDoseTypeEntity.MSClass = EUnitDoseTypeClass.Cytostatic AndAlso countMainMedicines > 1)

                ' Editar medicamento principal con su estructura (reconstituyente, vehículo)
                ' Excluir medicamentos complementarios de esta lista
                Dim tmplistDeletePackageDetail As New List(Of PackageDetail)
                Dim detailsToEdit As New List(Of PackageDetail)

                If isIntrathecal Then
                    ' Para intratecal: solo eliminar el principal que se está editando + reconstituyente + vehículo
                    ' Los demás principales permanecen intactos
                    tmplistDeletePackageDetail.Add(_itemPackageDetail) ' Solo el principal seleccionado

                    ' Agregar reconstituyente y vehículo (compartidos)
                    For Each item In ListPackageDetail.Where(Function(x) x.ComponentType = 1 AndAlso (x.Thinner OrElse x.Vehicle))
                        tmplistDeletePackageDetail.Add(item)
                    Next

                    ' Preparar detalles para editar: el principal seleccionado + reconstituyente + vehículo
                    detailsToEdit.Add(_itemPackageDetail)
                    For Each item In ListPackageDetail.Where(Function(x) x.ComponentType = 1 AndAlso (x.Thinner OrElse x.Vehicle))
                        detailsToEdit.Add(item)
                    Next
                Else
                    ' Flujo normal: eliminar y editar todos los detalles con ComponentType = 1 (excluyendo complementarios)
                    For Each item In ListPackageDetail.Where(Function(x) x.ComponentType = 1 AndAlso Not x.ComplementaryMedicine.GetValueOrDefault(False))
                        tmplistDeletePackageDetail.Add(item)
                        detailsToEdit.Add(item)
                    Next
                End If

                Using formulario As New FrmPopupPackageDetail
                    Me.Cursor = ChangeCursorIndigo()
                    AddHandler formulario.AddPackageDetail, AddressOf ReturnAddPackageDetail
                    formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
                    formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    ' Pasar solo los detalles específicos para editar
                    formulario.PackageDetaiAntibioticEdit = detailsToEdit
                    formulario.EditMode = True
                    formulario.ListPackageDetailValidation = ListPackageDetail.Where(Function(x) x.ComponentType = 1).ToList()
                    formulario.PackageId = _packageEntity.Id
                    formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
                    formulario.unitDoseType = _unitDoseTypeEntity
                    formulario.IsDashboardConfirmationUnitDose = IsDashboardConfirmationUnitDose
                    formulario.RequestedDosage = RequestedDosage
                    Dim transparent = New Base.FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default

                    If transparent.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                        Parallel.ForEach(tmplistDeletePackageDetail, Sub(item)
                                                                         If item.Id > 0 Then _listDeletePackageDetail.Add(item)
                                                                         ListPackageDetail.Remove(item)
                                                                     End Sub)

                        RefrescarRejilla()
                        Await RefreshDescription()
                        Await CalculateValuesAntibiotic()
                    End If
                End Using
            Else

                _itemCopy = New PackageDetail
                _itemCopy = Clone(_itemPackageDetail)
                If _itemPackageDetail IsNot Nothing Then
                    indexEditRecord = ListPackageDetail.IndexOf(_itemPackageDetail)
                    Using formulario As New FrmPopupPackageDetail
                        Me.Cursor = ChangeCursorIndigo()
                        AddHandler formulario.AddPackageDetail, AddressOf ReturnAddPackageDetail
                        formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
                        formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.9
                        formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        formulario.PackageDetailEdit = _itemPackageDetail
                        formulario.EditMode = True
                        formulario.ListPackageDetailValidation = ListPackageDetail
                        formulario.PackageId = _packageEntity.Id
                        formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
                        formulario.unitDoseType = _unitDoseTypeEntity
                        formulario.IsDashboardConfirmationUnitDose = IsDashboardConfirmationUnitDose
                        formulario.RequestedDosage = RequestedDosage
                        Dim transparent = New Base.FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default

                        If transparent.ShowDialog(Me) = Windows.Forms.DialogResult.OK Then
                            If _itemPackageDetail.Id > 0 Then _listDeletePackageDetail.Add(_itemPackageDetail)
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' funcion para clonar propiedades de una entidad para realizar comparacion
    ''' </summary>
    ''' <param name="Item"></param>
    ''' <returns></returns>
    Private Function Clone(Item As PackageDetail) As PackageDetail
        If Item Is Nothing Then
            Return New PackageDetail
        End If
        Dim NewItem = New PackageDetail
        With NewItem
            .Id = Item.Id
            .AtcId = Item.AtcId
            .MainMedicine = Item.MainMedicine
            .PackageId = Item.PackageId
            .Quantity = Item.Quantity
        End With
        Return NewItem
    End Function

    ''' <summary>
    ''' Crea una copia completa de un PackageDetail para evitar modificar el original
    ''' </summary>
    Private Function ClonePackageDetailFull(item As PackageDetail) As PackageDetail
        If item Is Nothing Then Return Nothing

        Dim newItem = New PackageDetail()
        ' Detener el tracking para evitar que se registren cambios durante la asignación
        ' Esto evita problemas de serialización con el ChangeTracker
        newItem.StopTracking()

        With newItem
            ' Propiedades básicas
            .Id = item.Id
            .PackageId = item.PackageId
            .AtcId = item.AtcId
            .SupplieId = item.SupplieId
            .ProductId = item.ProductId
            .ComponentType = item.ComponentType
            .ComponentTypeName = item.ComponentTypeName
            .PreparationType = item.PreparationType
            .PreparationTypeName = item.PreparationTypeName

            ' Propiedades del medicamento
            .MainMedicine = item.MainMedicine
            .Thinner = item.Thinner
            .Vehicle = item.Vehicle
            .ComplementaryMedicine = item.ComplementaryMedicine

            ' Cantidades y medidas
            .Quantity = item.Quantity
            .QuantityMeasureunitname = item.QuantityMeasureunitname
            .MeasurementUnitId = item.MeasurementUnitId
            .MeasurementUnitAbbreviation = item.MeasurementUnitAbbreviation
            .MeasureUnitDescription = item.MeasureUnitDescription

            ' Volumen
            .Volume = item.Volume
            .VolumeTotal = item.VolumeTotal
            .VolumeMeasureUnit = item.VolumeMeasureUnit
            .VolumeMeasureUnitDescription = item.VolumeMeasureUnitDescription
            .VolumeMeasureUnitAbbreviation = item.VolumeMeasureUnitAbbreviation

            ' Concentración y dilución
            .Concentration = item.Concentration
            .ConcentrationName = item.ConcentrationName
            .Dilution = item.Dilution

            ' Estabilidad
            .TimeUnit = item.TimeUnit
            .AmountTime = item.AmountTime

            ' Nombres y descripciones
            .SourceName = item.SourceName
            .SourceCodeName = item.SourceCodeName
            .SourceCodeNameSub = item.SourceCodeNameSub
            .DCIName = item.DCIName
            .Dosis = item.Dosis
            .ComponentName = item.ComponentName

            ' Propiedades adicionales
            .Osmolarity = item.Osmolarity
            .Density = item.Density
            .NPTItemOrder = item.NPTItemOrder

            ' Flags
            .IsDashboardConfirmationUnitDose = item.IsDashboardConfirmationUnitDose
            .SourceType = item.SourceType
            .UnitType = item.UnitType
            .IsPrescribed = item.IsPrescribed
            .WasOrdened = item.WasOrdened

            ' ATC si existe
            If item.ATC IsNot Nothing Then
                .ATC = New ATC With {
                    .Id = item.ATC.Id,
                    .Concentration = item.ATC.Concentration,
                    .Volume = item.ATC.Volume,
                    .Weight = item.ATC.Weight,
                    .VolumeMeasureUnit = item.ATC.VolumeMeasureUnit,
                    .NullTextUnitMeasureVolumen = item.ATC.NullTextUnitMeasureVolumen,
                    .FormulationType = item.ATC.FormulationType
                }
            End If
        End With

        Return newItem
    End Function

    ''' <summary>
    ''' Crea una copia completa de una lista de PackageDetail
    ''' </summary>
    Private Function ClonePackageDetailList(list As List(Of PackageDetail)) As List(Of PackageDetail)
        If list Is Nothing Then Return New List(Of PackageDetail)()
        Return list.Select(Function(item) ClonePackageDetailFull(item)).ToList()
    End Function

    ''' <summary>
    ''' Crea una copia del Package para evitar modificar el original si se cierra sin guardar
    ''' </summary>
    Private Function ClonePackage(original As Package) As Package
        If original Is Nothing Then Return New Package()

        Dim newPackage = New Package()
        ' Detener el tracking para evitar que se registren cambios durante la asignación
        ' Esto evita problemas de serialización con el ChangeTracker
        newPackage.StopTracking()

        With newPackage
            ' No copiar Id para que sea un nuevo registro
            .Id = 0
            .Code = original.Code
            .Name = original.Name
            .Description = original.Description
            .State = original.State
            .CodeAlternative = original.CodeAlternative
            .RiskLevelId = original.RiskLevelId
            .PhotoProtection = original.PhotoProtection
            .Storage = original.Storage
            .UnitDoseTypeId = original.UnitDoseTypeId
            .UnitDoseType = original.UnitDoseType
            .ProductId = original.ProductId
            .Concentration = original.Concentration
            .ConcentrationMeasurementUnitId = original.ConcentrationMeasurementUnitId
            .ConcentrationAntibiotic = original.ConcentrationAntibiotic
            .VolumeTotalPrepared = original.VolumeTotalPrepared
            .MeasurementPreparedId = original.MeasurementPreparedId
            .PreparationType = original.PreparationType
            .TypeStability = original.TypeStability
            .StabilityHour = original.StabilityHour
            .StabilityDays = original.StabilityDays
            .EnvironmentalTemperatureTerm = original.EnvironmentalTemperatureTerm
            .Purge = original.Purge
            .PreparationInstructions = original.PreparationInstructions
            .SpecialConsiderations = original.SpecialConsiderations
            .NptId = original.NptId
            .MainDrugId = original.MainDrugId
            .VehicleOptimization = original.VehicleOptimization
            .Readjustments = original.Readjustments
            .OsmolarityTotal = original.OsmolarityTotal
            .VolumeTotalOrder = original.VolumeTotalOrder
            .VolumeTotalOrderMeasurementUnitId = original.VolumeTotalOrderMeasurementUnitId
            .VolumeTotalOrderPurga = original.VolumeTotalOrderPurga
            .WeightTotalSolution = original.WeightTotalSolution
            .StandardMix = original.StandardMix
            .LabelType = original.LabelType
            .AssociatedPackageId = original.AssociatedPackageId
            .IsPackagePersonalized = original.IsPackagePersonalized
            .PersonalizedMasterPreparation = original.PersonalizedMasterPreparation

            ' No copiar PackageDetail aquí porque se manejará por separado con ClonePackageDetailList
        End With

        Return newPackage
    End Function

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteDetail()
        If Not AllowsEditPackage Then
            Mensaje(EeventViewerImages.Advertencia) = "El paquete ya fue asociado a un proceso de producción y no se puede eliminar los componentes"
            Exit Sub
        End If

        _itemPackageDetail = DirectCast(INDGvComponents.GetFocusedRow(), PackageDetail)

        If _unitDoseTypeEntity?.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            If Not {4, 5}.Contains(_itemPackageDetail.ComponentType) Then
                Exit Sub
            End If
        End If

        ' Verificar si es un medicamento complementario - se elimina individualmente
        Dim isComplementaryMedicine As Boolean = _itemPackageDetail.ComplementaryMedicine.GetValueOrDefault(False)

        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(_unitDoseTypeEntity.MSClass) _
           AndAlso _itemPackageDetail.ComponentType = 1 _
           AndAlso Not isComplementaryMedicine Then
            ' Eliminar medicamento principal con su estructura (reconstituyente, vehículo)
            ' NO eliminar los complementarios
            If MessageIndigo.Show("Al eliminar el registro se eliminaran los componentes relacionados (reconstituyente, vehículo) ¿Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If _listDeletePackageDetail Is Nothing Then
                    _listDeletePackageDetail = New List(Of PackageDetail)
                End If

                ' Solo eliminar detalles que NO son complementarios
                For Each item In ListPackageDetail.Where(Function(x) x.ComponentType = 1 AndAlso Not x.ComplementaryMedicine.GetValueOrDefault(False))
                    If item.Id > 0 Then _listDeletePackageDetail.Add(item)
                Next

                ListPackageDetail.RemoveAll(Function(x) x.ComponentType = 1 AndAlso Not x.ComplementaryMedicine.GetValueOrDefault(False))
                INDGcComponents.DataSource = Nothing
                INDGcComponents.DataSource = ListPackageDetail

                CleanControlsAntibiotic()
            End If
        Else

            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If _itemPackageDetail.Id > 0 Then
                    If _listDeletePackageDetail Is Nothing Then
                        _listDeletePackageDetail = New List(Of PackageDetail)
                    End If
                    _itemPackageDetail.MarkAsDeleted()
                    _listDeletePackageDetail.Add(_itemPackageDetail)
                End If

                ListPackageDetail.Remove(_itemPackageDetail)
                INDGcComponents.DataSource = Nothing
                INDGcComponents.DataSource = ListPackageDetail.OrderBy(Function(x) If(x.NPTItemOrder.HasValue, 0, 1)). ' Los NULL (Nothing) van al final
                                                ThenBy(Function(x) x.NPTItemOrder.GetValueOrDefault(255)). ' Orden ascendente por NPTItemOrder
                                                ToList()

                ' Si es un medicamento complementario y el tipo de dosis unitaria maneja preparación,
                ' usar CalculateValuesAntibiotic para recalcular concentración y volumen total
                If isComplementaryMedicine AndAlso _unitDoseTypeEntity IsNot Nothing AndAlso
                   {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(_unitDoseTypeEntity.MSClass) Then
                    Await CalculateValuesAntibiotic()
                Else
                    CalculateValues()
                End If

                If ListPackageDetail.Any() Then
                    Await RefreshDescription()
                Else
                    Description = String.Empty
                End If
            End If
        End If

        HideOrShowLabelsForUnitDoseType(_unitDoseTypeEntity?.MSClass)

        EvaluateStabilityTypeVisibility()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="_itemPackageDetail"></param>
    ''' <returns></returns>
    Private Function PaqueteValido(ByVal _itemPackageDetail As PackageDetail) As Boolean
        Dim errors As New System.Text.StringBuilder
        Dim components As New List(Of Tuple(Of Byte, Integer))
        components = ListPackageDetail.Select(Function(d) New Tuple(Of Byte, Integer)(d.ComponentType, IIf(d.ComponentType = 1, d.AtcId.GetValueOrDefault,
                                                                                                       IIf(d.ComponentType = 2, d.SupplieId.GetValueOrDefault,
                                                                                                       IIf(d.ComponentType = 3, d.ProductId.GetValueOrDefault, 0))))).ToList

        Dim _tuple = components.Where(Function(c) c.Item1 = _itemPackageDetail.ComponentType AndAlso
                                          c.Item2 = IIf(_itemPackageDetail.ComponentType = 1, _itemPackageDetail.AtcId.GetValueOrDefault,
                                                    IIf(_itemPackageDetail.ComponentType = 2, _itemPackageDetail.SupplieId.GetValueOrDefault,
                                                    IIf(_itemPackageDetail.ComponentType = 3, _itemPackageDetail.ProductId.GetValueOrDefault, 0)))).FirstOrDefault
        If _tuple IsNot Nothing Then
            components.Remove(_tuple)
        End If
        Using model As New MPackage(Me.Tag)
            Dim packages As List(Of PackageDto) = model.ListDuplicatePackage(_packageEntity.Id, components)
            If packages.Count > 0 Then
                PaqueteValido = False
                errors.AppendLine("Paquetes con los mismos componentes:")
                errors.AppendLine(String.Join(Environment.NewLine, packages.Select(Function(p) String.Format("{0} - {1}", p.Code, p.Name)).ToList))
                If MessageIndigo.Show("Existen paquetes con los mismos componentes. Desea revisarlos?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                    Using formulario As New FrmPopupPackageQuery
                        Me.Cursor = ChangeCursorIndigo()
                        formulario.Size = New System.Drawing.Size(1024, 780)
                        formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        formulario.operatingUnitId = Me.BarraBotones.OperatingUnitValue
                        formulario.ListPackage = packages
                        Dim transparent = New Base.FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
            Else
                PaqueteValido = True
            End If
        End Using
        Return PaqueteValido
    End Function

    Private Sub INDsleUnitDoseType_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDsleUnitDoseType.CloseUp
        Dim SelectDate As Integer = e.Value
        Dim ValueType As Integer = DirectCast(sender, DevExpress.XtraEditors.BaseEdit).EditValue
        If SelectDate <> ValueType Then
            If AllowsEditPackage = False Then
                Mensaje(EeventViewerImages.Advertencia) = "El paquete ya fue asociado a un proceso de producción y no se puede editar el tipo de dosis unitaria"
                e.Value = ValueType
            End If
        End If
    End Sub

    ''' <summary>
    ''' Funcion de validacion para cuando se cambia los valores de Medicamento principal o vehiculo o diluyente
    ''' </summary>
    ''' <param name="PackageDetail"></param>
    ''' <param name="checkType">1- main Medicine; 2 - Thinner, 3 - Vehicle</param>
    ''' <param name="CheckActions"> True or False </param>
    ''' <returns></returns>
    Private Async Function ValidationsCheckEdit(PackageDetail As PackageDetail, checkType As Byte, CheckActions As Boolean) As Task(Of Boolean)
        Dim errors As New StringBuilder
        Dim clonePackageDetail = PackageDetail.Clone

        Select Case checkType
            Case 1
                clonePackageDetail.MainMedicine = CheckActions

                If CheckActions Then
                    Dim _componentId As Integer
                    Select Case clonePackageDetail.ComponentType
                        Case 1
                            _componentId = clonePackageDetail.AtcId
                        Case 2
                            _componentId = clonePackageDetail.SupplieId
                        Case 3
                            _componentId = clonePackageDetail.ProductId
                    End Select

                    Dim _packageDetail = ListPackageDetail.FirstOrDefault(Function(d) d.MainMedicine)

                    ' Permitir múltiples medicamentos principales para Magistral y Citostático (intratecal)
                    If (_packageDetail IsNot Nothing AndAlso _componentId <> _packageDetail.AtcId) AndAlso
                        (_unitDoseTypeEntity?.MSClass <> EUnitDoseTypeClass.Magistral AndAlso
                         _unitDoseTypeEntity?.MSClass <> EUnitDoseTypeClass.Cytostatic) Then
                        errors.AppendLine(String.Format("El siguiente componente esta marcado como principal {0}", _packageDetail.SourceCodeName))
                        If MessageIndigo.Show(String.Format("{0}, desea modificarlo?", errors.ToString),
                                              MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            _packageDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                            _packageDetail.MainMedicine = False
                            INDGcComponents.RefreshDataSource()
                        Else
                            Return False
                        End If
                        errors.Clear()
                    End If
                End If
            Case 2
                clonePackageDetail.Thinner = CheckActions
            Case 3
                clonePackageDetail.Vehicle = CheckActions
        End Select

        Await Task.Factory.StartNew(Sub()
                                        Dim Query = _presenter.GetATC(PackageDetail.AtcId)
                                        If Query.FormulationType <> 2 AndAlso (clonePackageDetail.Thinner = True OrElse clonePackageDetail.Vehicle = True) Then
                                            errors.AppendLine("No se puede marcar un medicamento como diluyente o vehículo si el tipo de fórmula del medicamento es diferente a volumen")
                                        End If

                                        If clonePackageDetail.MainMedicine = True AndAlso (clonePackageDetail.Thinner = True OrElse clonePackageDetail.Vehicle = True) Then
                                            errors.AppendLine("Si un Medicamento es Principal, No puede ser al mismo tiempo Vehiculo y/o diluyente")
                                        End If
                                    End Sub)

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Return False
        End If

        Using Model As New MPackage(CStr(Me.Tag))
            If UnitDoseTypeId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Tipo de Dosis Unitaria"
                Return False
            End If

            Dim resultOperation = Await Model.GetUnitDoseTypeById(UnitDoseTypeId)

            If resultOperation.ObjectEmbbeded.MSClass = EUnitDoseTypeClass.ParenteralNutrition AndAlso CheckActions Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede Marcar porque el tipo de de Dosis es NPT"
                Return False
            End If
        End Using

        CalculateValues()
        Return True
    End Function

    Private Sub INDRICEMain_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRICEMain.EditValueChanging
        If IsDashboardConfirmationUnitDose Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar el medicamento principal"
            e.Cancel = True
        End If
    End Sub

    Private Sub INDsleTypeNPT_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTypeNPT.EditValueChanged
        If NptId IsNot Nothing And UnitDoseTypeId IsNot Nothing And Not _isLoading Then
            Dim Result = _presenter.GetComponentTypeNPT(NptId)

            If Result Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Plantilla NPT vacía"
                Exit Sub
            End If

            If ListPackageDetail Is Nothing Then
                ListPackageDetail = New List(Of PackageDetail)
            Else
                For Each item In ListPackageDetail
                    If item.Id > 0 Then
                        _listDeletePackageDetail.Add(item)
                    End If
                Next

                ListPackageDetail.RemoveAll(Function(x) x.ComponentType < 4)
            End If

            For Each item In Result
                Dim PackageDetail = New PackageDetail
                With PackageDetail
                    .AtcId = item.AtcId
                    .MeasurementUnitId = CInt(item.MeasurementUnitId)
                    .SourceCodeName = item.Name
                    .ComponentType = 1
                    .ComponentTypeName = "Medicamento"
                    .MainMedicine = True
                    .DCIName = item.AbbreviationName
                End With

                ListPackageDetail.Add(PackageDetail)
            Next

            RefreshDescriptionNPT()
            RefrescarRejilla()

            Dim NPT = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of HCPARNUTCXpo)($"ID = {NptId}")

            If NPT IsNot Nothing Then
                Dim Atc = _presenter.GetATCByCode(NPT.FinishedProductCode)
                MainDrugId = If(Atc?.Id, Nothing)
                INDSleMainDrug.Properties.NullText = If(Atc?.CodeName, String.Empty)
            End If

            If MainDrugId Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "El medicamento de referencia en la plantilla NPT se encuentra vacío (Parametrice el código de producto terminado en la plantilla NPT). Este dato es obligatorio en este tipo de dosis para asignar el código del producto terminado"
                Exit Sub
            End If
        End If
    End Sub

    Private Sub INDmeSpecialConsiderations_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDmeSpecialConsiderations.EditValueChanging
        If (e.NewValue IsNot Nothing) Then
            If (e.NewValue.ToString().Length > 500) Then
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub INDmePreparationInstructions_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDmePreparationInstructions.EditValueChanging
        If (e.NewValue IsNot Nothing) Then
            If (e.NewValue.ToString().Length > 500) Then
                e.Cancel = True
            End If
        End If
    End Sub
#End Region

#Region "QueryPopUpActionButtons"

    Private Sub IndigoGridView1_QueryPopUpActionButtons(sender As Object, e As QueryPopUpActionButtonsEventArgs) Handles IndigoGridView1.QueryPopUpActionButtons
        If _unitDoseTypeEntity?.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
            Dim popUp = CType(sender, DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit)
            Dim ItemSelected = (From x In INDGvComponents.GetSelectedRows() Where Not INDGvComponents.IsGroupRow(x) Select DirectCast(INDGvComponents.GetRow(x), PackageDetail)).FirstOrDefault()

            If ItemSelected.ComponentType <> 5 Then
                For Each button In e.Buttons
                    button.Visible = False
                Next
            End If
        End If
    End Sub

#End Region

#Region "PopMenuShowing"
    Private Sub INDviewMaster_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDGvComponents.PopupMenuShowing
        Try
            If _unitDoseTypeEntity?.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                Dim ItemSelected = (From x In INDGvComponents.GetSelectedRows() Where Not INDGvComponents.IsGroupRow(x) Select DirectCast(INDGvComponents.GetRow(x), PackageDetail)).FirstOrDefault()

                If ItemSelected.ComponentType <> 5 Then
                    For Each item In IndigoGridView1.BarManagerActions.Items
                        item.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                    Next
                End If
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub
#End Region

End Class

Public Class SendPackageArgs
    Inherits EventArgs

    Property Package As Package

    Property IsPersonalizedMasterPreparation As Boolean

End Class