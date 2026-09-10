'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Andrea Pahola Coqueco Cuellar
' Created          : 25/09/2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmDetailRequestAntibiotic
    Implements IDetailRequestAntibiotic

#Region "Enum"

    ''' <summary>
    ''' Enumerable para identificar el tipo de preparación a realizar: Reconstitución, Dilución o ambas
    ''' </summary>
    Public Enum ePreparationType
        Reconstitution = 1
        ReconstitutionDilution = 2
        Dilution = 3
    End Enum

    ''' <summary>
    ''' Enumerable para identificar el tipo de formulación del Atc: Peso, volumen, peso-volumen o unidad de administración
    ''' </summary>
    Public Enum eFormulationType
        Weight = 1
        Volume = 2
        WeightVolume = 3
        AdministrationUnit = 4
    End Enum

    ''' <summary>
    ''' Enumerable para identificar el tipo de item: Medicamento, insumo o producto
    ''' </summary>
    Public Enum eItemType
        Atc = 1
    End Enum

    ''' <summary>
    ''' Enumerable para identificar el tipo de componente: Medicamento principal, Reconstituyente o Diluyente
    ''' </summary>
    Public Enum eComponentType
        MainMedicine = 1
        Reconstituent = 2
        Vehicle = 3
    End Enum

#End Region

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddExternalPatientPreparation(sender As Object, e As AddExternalPatientPreparationEventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Representa a la entidad de tipo de dosis unitaria que se selecciona en el form principal
    ''' </summary>
    Public UnitDoseType As UnitDoseType

    ''' <summary>
    ''' Establece el estado de la solicitud
    ''' </summary>
    Public AllowAdd As Boolean

    ''' <summary>
    ''' Propiedad que establecen los detalles a editar
    ''' </summary>
    Public WriteOnly Property ExternalPatientPreparationEdit As ExternalPatientPreparation
        Set(value As ExternalPatientPreparation)
            NewExternalPatientPreparation = value.Clone
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' Activa los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDetailRequestAntibiotic.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()

            INDSeQuantity.Enabled = value
            INDSleMeasurementUnit.Enabled = value
            INDSeVolume.Enabled = value
            INDSleVolumeMeasureUnit.Enabled = value
            INDSlePreparationType.Enabled = value
            INDSleAdministrationRoute.Enabled = value
            INDSePreparationsRequested.Enabled = value

            INDSleReconstituent.Enabled = value
            INDSeReconstituentVolume.Enabled = value
            INDSleReconstituentUnitMeasurement.Enabled = value

            INDSeTotalPreparedVolume.Enabled = value
            INDSleVehicleDilution.Enabled = value
            INDSeVolumeVehicle.Enabled = value
            INDsleVehicleUnitMeasurement.Enabled = value
            INDsleTotalPreparedUnitMeasurement.Enabled = value

            INDlyRoot.EndUpdate()
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador de package - 
    ''' </summary>
    Private Presenter As PDetailRequestAntibiotic

    ''' <summary>
    ''' Permite consulta del medicamento principal por medio de xpo la entidad de medicamentos
    ''' </summary>
    Private Atc As ATCXpo

    ''' <summary>
    ''' Permite consulta del reconstituyente por medio de xpo la entidad de medicamentos
    ''' </summary>
    Private Reconstituent As ATCXpo

    ''' <summary>
    ''' Permite consulta del objeto vehiculo por medio de xpo la entidad de medicamentos
    ''' </summary>
    Private Vehicle As ATCXpo

    ''' <summary>
    ''' Propiedad que establecen los detalles a editar
    ''' </summary>
    Public NewExternalPatientPreparation As ExternalPatientPreparation

    ''' <summary>
    ''' Propiedad que establece si el formulario se encuentra cargando la data
    ''' </summary>
    Dim isLoading As Boolean

    ''' <summary>
    ''' Obtiene la concentración del preparado.
    ''' </summary>
    Dim concentration As String

    ''' <summary>
    ''' Obtiene la descripción del preparado.
    ''' </summary>
    Dim Description As String

#End Region

#Region "Controls"

    ''' <summary>
    ''' Obtiene o establece el tipo de componente
    ''' </summary>
    Public Property ComponentType As Byte
        Get
            Return CType(INDGleType.EditValue, Byte)
        End Get
        Set(value As Byte)
            INDGleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id medicamento principal
    ''' </summary>
    Public Property AtcId As Integer?
        Get
            Return CType(INDSleMedicine.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleMedicine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto del medicamento principal
    ''' </summary>
    Public Property MedicineCodeName As String
        Get
            If INDSleMedicine.Properties.DataSource Is Nothing Then
                Return INDSleMedicine.Properties.NullText
            End If
            Return INDSleMedicine.Text
        End Get
        Set(value As String)
            INDSleMedicine.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad en peso del medicamento principal
    ''' </summary>
    Public Property Quantity As Decimal?
        Get
            Return CType(INDSeQuantity.EditValue, Decimal?)
        End Get
        Set(value As Decimal?)
            INDSeQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida en peso del medicamento
    ''' </summary>
    Public Property MeasureUnitId As Integer?
        Get
            Return INDSleMeasurementUnit.EditValue
        End Get
        Set(value As Integer?)
            INDSleMeasurementUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto de la unidad de medida en peso del medicamento
    ''' </summary>
    Public Property MeasureUnitCodeName As String
        Get
            If INDSleMeasurementUnit.Properties.DataSource Is Nothing Then
                Return INDSleMeasurementUnit.Properties.NullText
            End If
            Return INDSleMeasurementUnit.Text
        End Get
        Set(value As String)
            INDSleMeasurementUnit.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad en volumen del medicamento principal
    ''' </summary>
    Public Property VolumeMedicine As Decimal?
        Get
            Return CType(INDSeVolume.EditValue, Decimal?)
        End Get
        Set(value As Decimal?)
            INDSeVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida en el volumen del medicamento principal
    ''' </summary>
    Public Property MeasureUnitVolumeId As Integer?
        Get
            Return INDSleVolumeMeasureUnit.EditValue
        End Get
        Set(value As Integer?)
            INDSleVolumeMeasureUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto de la unidad de medida en volumen del medicamento
    ''' </summary>
    Public Property MeasureUnitVolumeCodeName As String
        Get
            If INDSleVolumeMeasureUnit.Properties.DataSource Is Nothing Then
                Return INDSleVolumeMeasureUnit.Properties.NullText
            End If
            Return INDSleVolumeMeasureUnit.Text
        End Get
        Set(value As String)
            INDSleVolumeMeasureUnit.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de preparación
    ''' </summary>
    Public Property PreparationType As Byte?
        Get
            Return CType(INDSlePreparationType.EditValue, Byte?)
        End Get
        Set(value As Byte?)
            INDSlePreparationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la vía de administración
    ''' </summary>
    Public Property AdministrationRouteId As Integer? Implements IDetailRequestAntibiotic.AdministrationRouteId
        Get
            Return INDSleAdministrationRoute.EditValue
        End Get
        Set(value As Integer?)
            INDSleAdministrationRoute.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto de la vía de administración
    ''' </summary>
    Public Property AdministrationRouteCodeName As String
        Get
            If INDSleAdministrationRoute.Properties.DataSource Is Nothing Then
                Return INDSleAdministrationRoute.Properties.NullText
            End If
            Return INDSleAdministrationRoute.Text
        End Get
        Set(value As String)
            INDSleAdministrationRoute.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad en peso del medicamento principal
    ''' </summary>
    Public Property PreparationsRequested As Integer
        Get
            Return CType(INDSePreparationsRequested.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDSePreparationsRequested.EditValue = value
        End Set
    End Property

    '==============================================================

    ''' <summary>
    ''' Obtiene o establece el Id del reconstituyente
    ''' </summary>
    Public Property ReconstituentId As Integer?
        Get
            Return CType(INDSleReconstituent.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleReconstituent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto del Reconstituyente
    ''' </summary>
    Public Property ReconstituentCodeName As String
        Get
            If INDSleReconstituent.Properties.DataSource Is Nothing Then
                Return INDSleReconstituent.Properties.NullText
            End If
            Return INDSleReconstituent.Text
        End Get
        Set(value As String)
            INDSleReconstituent.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el volumen del reconstituyente
    ''' </summary>
    Public Property ReconstituentVolume As Decimal?
        Get
            Return CType(INDSeReconstituentVolume.EditValue, Decimal?)
        End Get
        Set(value As Decimal?)
            INDSeReconstituentVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del volumen del reconstituyente
    ''' </summary>
    Public Property ReconstituentUnitMeasurement As Integer?
        Get
            Return INDSleReconstituentUnitMeasurement.EditValue
        End Get
        Set(value As Integer?)
            INDSleReconstituentUnitMeasurement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto de la unidad de medida del volumen del reconstituyente
    ''' </summary>
    Public Property ReconstituentUnitMeasurementCodeName As String
        Get
            If INDSleReconstituentUnitMeasurement.Properties.DataSource Is Nothing Then
                Return INDSleReconstituentUnitMeasurement.Properties.NullText
            End If
            Return INDSleReconstituentUnitMeasurement.Text
        End Get
        Set(value As String)
            INDSleReconstituentUnitMeasurement.Properties.NullText = value
        End Set
    End Property

    '==============================================================

    ''' <summary>
    ''' Obtiene o establece el volumen total del preparado
    ''' </summary>
    Public Property TotalPreparedVolume As Decimal?
        Get
            Return CType(INDSeTotalPreparedVolume.EditValue, Decimal?)
        End Get
        Set(value As Decimal?)
            INDSeTotalPreparedVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del preparado
    ''' </summary>
    Public Property TotalPreparedUnitMeasurement As Integer?
        Get
            Return INDsleTotalPreparedUnitMeasurement.EditValue
        End Get
        Set(value As Integer?)
            INDsleTotalPreparedUnitMeasurement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto de la unidad de medida del volumen del preparado
    ''' </summary>
    Public Property TotalPreparedUnitMeasurementCodeName As String
        Get
            If INDsleTotalPreparedUnitMeasurement.Properties.DataSource Is Nothing Then
                Return INDsleTotalPreparedUnitMeasurement.Properties.NullText
            End If
            Return INDsleTotalPreparedUnitMeasurement.Text
        End Get
        Set(value As String)
            INDsleTotalPreparedUnitMeasurement.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el vehiculo de la dilucion
    ''' </summary>
    Public Property VehicleId As Integer?
        Get
            Return CType(INDSleVehicleDilution.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleVehicleDilution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto del Vehículo
    ''' </summary>
    Public Property VehicleCodeName As String
        Get
            If INDSleVehicleDilution.Properties.DataSource Is Nothing Then
                Return INDSleVehicleDilution.Properties.NullText
            End If
            Return INDSleVehicleDilution.Text
        End Get
        Set(value As String)
            INDSleVehicleDilution.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el volumen del reconstituyente
    ''' </summary>
    Public Property VolumeVehicle As Decimal?
        Get
            Return INDSeVolumeVehicle.EditValue
        End Get
        Set(value As Decimal?)
            INDSeVolumeVehicle.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del vehículo
    ''' </summary>
    Public Property VehicleUnitMeasurement As Integer?
        Get
            Return INDsleVehicleUnitMeasurement.EditValue
        End Get
        Set(value As Integer?)
            INDsleVehicleUnitMeasurement.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el texto de la unidad de medida del vehículo
    ''' </summary>
    Public Property VehicleUnitMeasurementCodeName As String
        Get
            If INDsleVehicleUnitMeasurement.Properties.DataSource Is Nothing Then
                Return INDsleVehicleUnitMeasurement.Properties.NullText
            End If
            Return INDsleVehicleUnitMeasurement.Text
        End Get
        Set(value As String)
            INDsleVehicleUnitMeasurement.Properties.NullText = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Datasource del tipo de item a solicitar
    ''' </summary>
    Public Property ComponentTypeDatasource As List(Of Tuple(Of Byte, String))
        Get
            Return INDGleType.Properties.DataSource
        End Get
        Set(value As List(Of Tuple(Of Byte, String)))
            INDGleType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource del medicamento principal
    ''' </summary>
    ''' <returns></returns>
    Public Property AtcDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.ATCDatasource
        Get
            Return INDSleMedicine.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleMedicine.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece la unidad de medida de la cantidad a utilizar del medicamento principal
    ''' </summary>
    ''' <returns></returns>
    Public Property MeasurementUnitDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.MeasurementUnitDatasource
        Get
            Return INDSleMeasurementUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleMeasurementUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource de la unidad de medida del volumen del medicamento principal
    ''' </summary>
    ''' <returns></returns>
    Public Property MeasureUnitVolumeDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.MeasureUnitVolumeDatasource
        Get
            Return INDSleVolumeMeasureUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleVolumeMeasureUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del tipo de preparacion
    ''' </summary>
    Public Property PreparationTypeDatasource As List(Of Tuple(Of Byte, String))
        Get
            Return INDSlePreparationType.Properties.DataSource
        End Get
        Set(value As List(Of Tuple(Of Byte, String)))
            INDSlePreparationType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para la via de administración
    ''' </summary>
    ''' <returns></returns>
    Public Property AdministrationRouteDatasource As XPCollection(Of ViewATCAdministrationRouteXpo) Implements IDetailRequestAntibiotic.AdministrationRouteDatasource
        Get
            Return INDSleAdministrationRoute.Properties.DataSource
        End Get
        Set(value As XPCollection(Of ViewATCAdministrationRouteXpo))
            INDSleAdministrationRoute.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasource para el reconstituyente
    ''' </summary>
    ''' <returns></returns>
    Public Property ReconstituentDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.ReconstituentDatasource
        Get
            Return INDSleReconstituent.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleReconstituent.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasoruce para el volumen del reconstituyente
    ''' </summary>
    ''' <returns></returns>
    Public Property ReconstituentUnitMeasurementDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.ReconstituentUnitMeasurementDatasource
        Get
            Return INDSleReconstituentUnitMeasurement.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleReconstituentUnitMeasurement.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasoruce para el volumen del preparado
    ''' </summary>
    ''' <returns></returns>
    Public Property TotalPreparedUnitMeasurementDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.TotalPreparedUnitMeasurementDatasource
        Get
            Return INDsleTotalPreparedUnitMeasurement.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleTotalPreparedUnitMeasurement.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasoruce para el vehículo
    ''' </summary>
    ''' <returns></returns>
    Public Property VehicleDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.VehicleDatasource
        Get
            Return INDSleVehicleDilution.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleVehicleDilution.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene o establece el datasoruce para el volumen del preparado
    ''' </summary>
    ''' <returns></returns>
    Public Property VehicleUnitMeasurementDatasource As XPInstantFeedbackSource Implements IDetailRequestAntibiotic.VehicleUnitMeasurementDatasource
        Get
            Return INDsleVehicleUnitMeasurement.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVehicleUnitMeasurement.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

#Region "ControlsManagment"

    ''' <summary>
    ''' Limpia los controles del formulario
    ''' </summary>
    Private Sub CleanAllControls()
        INDlyRoot.BeginUpdate()
        ActionsOnControls = False
        ComponentType = 1
        AtcId = Nothing
        MedicineCodeName = String.Empty
        INDSleMedicine.Properties.ReadOnly = False
        '===================================
        CleanMedicineControls()
        INDbtnAdd.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Add")
        INDlyRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Limpia los controles de relacionadas al medicamento principal
    ''' </summary>
    Private Sub CleanMedicineControls()
        ActionsOnControls = False
        CleanDatasource()
        '===================================
        Atc = Nothing
        Quantity = Nothing
        MeasureUnitId = Nothing
        MeasureUnitCodeName = String.Empty
        VolumeMedicine = Nothing
        INDSeVolume.ReadOnly = False
        MeasureUnitVolumeId = Nothing
        MeasureUnitVolumeCodeName = String.Empty
        PreparationType = Nothing
        AdministrationRouteId = Nothing
        AdministrationRouteCodeName = String.Empty
        PreparationsRequested = Nothing
        Description = String.Empty
        '===================================
        CleanReconstitutionControls()
        CleanDilutionControls()
        HideControlsAddComponent()
        ShowControlsMedicine()
    End Sub

    ''' <summary>
    ''' Limpia los controles relacionados al reconstituyente
    ''' </summary>
    Private Sub CleanReconstitutionControls()
        Reconstituent = Nothing
        ReconstituentId = Nothing
        ReconstituentCodeName = String.Empty
        ReconstituentVolume = Nothing
        ReconstituentUnitMeasurement = Nothing
        ReconstituentUnitMeasurementCodeName = String.Empty
    End Sub

    ''' <summary>
    ''' Limpia los controles relacionados al vehiculo
    ''' </summary>
    Private Sub CleanDilutionControls()
        TotalPreparedVolume = Nothing
        TotalPreparedUnitMeasurement = Nothing
        TotalPreparedUnitMeasurementCodeName = String.Empty
        concentration = String.Empty
        Vehicle = Nothing
        VehicleId = Nothing
        VehicleCodeName = String.Empty
        VolumeVehicle = Nothing
        VehicleUnitMeasurement = Nothing
        VehicleUnitMeasurementCodeName = String.Empty
    End Sub

    ''' <summary>
    ''' Limpia los datasource
    ''' </summary>
    Private Sub CleanDatasource()
        MeasurementUnitDatasource = Nothing
        MeasureUnitVolumeDatasource = Nothing
        AdministrationRouteDatasource = Nothing
        ReconstituentDatasource = Nothing
        ReconstituentUnitMeasurementDatasource = Nothing
        TotalPreparedUnitMeasurementDatasource = Nothing
        VehicleDatasource = Nothing
    End Sub

    ''' <summary>
    ''' Limpia el objeto ExternalPatientPreparation y los controles al agregar una nueva preparación
    ''' </summary>
    Private Sub CleanExternalPreparation()
        NewExternalPatientPreparation = New ExternalPatientPreparation
        CleanAllControls()
    End Sub

    ''' <summary>
    ''' Oculta los controles al elegir medicamento
    ''' </summary>
    Private Sub HideControlsAddComponent()
        INDLcgReconstitution.HideControl()
        INDLcgDilution.HideControl()
    End Sub

    ''' <summary>
    ''' Muestra todos los controles asociados al medicamento principal
    ''' </summary>
    Private Sub ShowControlsMedicine()
        INDLciQuantity.ShowLayout()
        INDLciUnitMeasurement.ShowLayout()
        INDLciVolume.ShowLayout()
        INDLciVolumeMeasureUnit.ShowLayout()
    End Sub
#End Region

#Region "Assignments"

    ''' <summary>
    ''' Método que establece la unidad de volumen para medicamentos con tipo de formulación peso volumen
    ''' </summary>
    Private Sub SetVolumeUnit(volumeUnitId As Integer, volumeUnitCodeName As String)
        If isLoading Then
            Exit Sub
        End If

        TotalPreparedUnitMeasurement = volumeUnitId
        TotalPreparedUnitMeasurementCodeName = volumeUnitCodeName
        INDsleTotalPreparedUnitMeasurement.Enabled = False

        VehicleUnitMeasurement = volumeUnitId
        VehicleUnitMeasurementCodeName = volumeUnitCodeName
        INDsleVehicleUnitMeasurement.Enabled = False
    End Sub

    ''' <summary>
    ''' Método que establece la unidad de volumen si el preparado tiene un reconstituyente
    ''' </summary>
    Private Sub SetVolumeUnitMeasurementFromReconstituent()
        If Atc IsNot Nothing And PreparationType = ePreparationType.Reconstitution Then
            TotalPreparedUnitMeasurement = ReconstituentUnitMeasurement
        End If

        If PreparationType = ePreparationType.ReconstitutionDilution AndAlso ReconstituentUnitMeasurement IsNot Nothing Then
            SetVolumeUnit(ReconstituentUnitMeasurement, ReconstituentUnitMeasurementCodeName)
        End If
    End Sub

    ''' <summary>
    ''' Método que cálcula el volumen del vehiculo
    ''' </summary>
    Private Sub VehicleVolumeCalculation()
        If Atc Is Nothing Then
            Exit Sub
        End If

        If Atc.FormulationType = eFormulationType.Weight And PreparationType = ePreparationType.Reconstitution Then
            Exit Sub
        End If

        If PreparationType = ePreparationType.ReconstitutionDilution Then
            If Not ValidateReconstitutionQuantity() Then
                Exit Sub
            End If

            VolumeVehicle = TotalPreparedVolume - If(ReconstituentVolume, 0) - If(VolumeMedicine, 0)

        ElseIf PreparationType = ePreparationType.Dilution Then
            If Not ValidateVolumeQuantity() Then
                Exit Sub
            End If

            VolumeVehicle = TotalPreparedVolume - If(VolumeMedicine, 0)
        End If
    End Sub

    ''' <summary>
    ''' Método que agrega los datos al objeto de la preparación
    ''' </summary>
    Private Sub AddPreparation()
        With NewExternalPatientPreparation
            '----------- Remove previous details
            Dim listDeleteExternalPatientPreparationDetail = New List(Of ExternalPatientPreparationDetail)
            For Each preparationDetail In .ExternalPatientPreparationDetail
                If preparationDetail.Id > 0 Then
                    listDeleteExternalPatientPreparationDetail.Add(preparationDetail)
                End If
            Next
            .ExternalPatientPreparationDetail.Clear()
            listDeleteExternalPatientPreparationDetail.ForEach(Sub(x) .ExternalPatientPreparationDetail.Add(x.MarkAsDeleted()))
            '-----------
            .PreparationsRequested = PreparationsRequested
            .PreparationTypeId = PreparationType
            .AdministrationRouteId = AdministrationRouteId
            .AdministrationRouteCodeName = INDSleAdministrationRoute.Text
            .VolumeTotalOrder = TotalPreparedVolume
            .TotalPreparedUnitMeasurementId = TotalPreparedUnitMeasurement
            .TotalPreparedUnitMeasurementCodeName = INDsleTotalPreparedUnitMeasurement.Text
            .TotalPreparedUnitMeasurementAbreviation = GetUnitMeasurementAbbreviation(TotalPreparedUnitMeasurement)
            .Concentration = concentration
            .MainMedicineCodeName = MedicineCodeName
            .ReconstituentCodeName = ReconstituentCodeName
            .VehicleCodeName = VehicleCodeName

            Dim numberOfItems As Integer = If(PreparationType = ePreparationType.ReconstitutionDilution, 3, 2)
            For counter As Integer = 1 To numberOfItems
                Dim preparationDetail = New ExternalPatientPreparationDetail
                With preparationDetail
                    .itemType = eItemType.Atc
                    Select Case counter
                        Case 1
                            AssignMainMedicine(preparationDetail)
                            Description += $"{BuildDescription(Atc.AbbreviationName, If(.Quantity, .Volume), If(.MeasurementUnitAbreviation, .VolumeMeasureUnitAbreviation))}"

                            If .Quantity.HasValue AndAlso .Volume.HasValue Then
                                Description += $" / { .Volume} { .VolumeMeasureUnitAbreviation}"
                            End If

                        Case 2
                            If PreparationType = ePreparationType.Reconstitution OrElse PreparationType = ePreparationType.ReconstitutionDilution Then
                                AssignReconstituent(preparationDetail)
                                Description += $" + {BuildDescription(Reconstituent.AbbreviationName, .Volume, .VolumeMeasureUnitAbreviation)}"

                            ElseIf PreparationType = ePreparationType.Dilution Then
                                AssignVehicle(preparationDetail)
                                Description += $" + {BuildDescription(Vehicle.AbbreviationName, .Volume, .VolumeMeasureUnitAbreviation)}"
                            End If
                        Case 3
                            AssignVehicle(preparationDetail)
                            Description += $" + {BuildDescription(Vehicle.AbbreviationName, .Volume, .VolumeMeasureUnitAbreviation)}"
                    End Select
                End With
                preparationDetail.LoadDescriptions()

                NewExternalPatientPreparation.ExternalPatientPreparationDetail.Add(preparationDetail)
            Next

            .Description = Description
            .LoadDescriptions()
        End With
    End Sub

    ''' <summary>
    ''' Método que construye la descripción de cada item del preparado
    ''' Se separa de las otras descripciones porque no obtiene el nombre del medicamento
    ''' sino su abreviación
    ''' </summary>
    Private Function BuildDescription(abbreviation As String, value As Double, unitAbbreviation As String) As String
        Return $"{abbreviation} {value} {unitAbbreviation}"
    End Function

    ''' <summary>
    ''' Método que obtiene la concentración para la adecuación
    ''' </summary>
    Private Sub CalculateConcentration()
        Dim TotalConcentration As Decimal = 0D

        If Quantity > 0 AndAlso TotalPreparedVolume > 0 Then
            TotalConcentration = Quantity / TotalPreparedVolume
        End If

        ' Ajuste de precisión según el valor
        If TotalConcentration > 1 Then
            TotalConcentration = Math.Round(CType(TotalConcentration, Decimal), 2)
        Else
            TotalConcentration = Math.Round(CType(TotalConcentration, Decimal), 4)
        End If

        concentration = $"{TotalConcentration} {GetUnitMeasurementAbbreviation(MeasureUnitId)} / {GetUnitMeasurementAbbreviation(TotalPreparedUnitMeasurement)}"
    End Sub

    ''' <summary>
    ''' Método que realiza el cálculo del volumen para medicamentos peso-volumen
    ''' </summary>
    Private Sub CalculateVolumeFromWeight()
        VolumeMedicine = Math.Round(CType(Quantity * Atc.Volume / Atc.Weight, Decimal), 2)
    End Sub

    ''' <summary>
    ''' Método que obtiene la abreviación de la unidad de medida
    ''' </summary>
    Private Function GetUnitMeasurementAbbreviation(unitMeasurementId As Integer) As String
        Return Presenter.GetMeasurementUnitById(unitMeasurementId).Abbreviation
    End Function

    ''' <summary>
    ''' Método que asigna la información del medicamento principal
    ''' </summary>
    Private Sub AssignMainMedicine(ExternalPreparationDetail As ExternalPatientPreparationDetail)
        With ExternalPreparationDetail
            .ComponentType = eComponentType.MainMedicine
            .AtcId = AtcId
            .AtcCodeName = MedicineCodeName

            If Atc.FormulationType = eFormulationType.Weight OrElse Atc.FormulationType = eFormulationType.WeightVolume Then
                .Quantity = Quantity
                .MeasurementUnitId = MeasureUnitId
                .MeasurementUnitCodeName = MeasureUnitCodeName
                .MeasurementUnitAbreviation = GetUnitMeasurementAbbreviation(MeasureUnitId)
            End If

            If Atc.FormulationType = eFormulationType.Volume OrElse Atc.FormulationType = eFormulationType.WeightVolume Then
                .Volume = VolumeMedicine
                .VolumeMeasureUnitId = MeasureUnitVolumeId
                .VolumeMeasureUnitCodeName = MeasureUnitVolumeCodeName
                .VolumeMeasureUnitAbreviation = GetUnitMeasurementAbbreviation(MeasureUnitVolumeId)
            End If
        End With
    End Sub

    ''' <summary>
    ''' Método que asigna la información del reconstituyente
    ''' </summary>
    Private Sub AssignReconstituent(ExternalPreparationDetail As ExternalPatientPreparationDetail)
        With ExternalPreparationDetail
            .ComponentType = eComponentType.Reconstituent
            .AtcId = ReconstituentId
            .AtcCodeName = ReconstituentCodeName
            .Volume = ReconstituentVolume
            .VolumeMeasureUnitId = ReconstituentUnitMeasurement
            .VolumeMeasureUnitCodeName = ReconstituentUnitMeasurementCodeName
            .VolumeMeasureUnitAbreviation = GetUnitMeasurementAbbreviation(ReconstituentUnitMeasurement)
        End With
    End Sub

    ''' <summary>
    ''' Método que asigna la información del vehicle
    ''' </summary>
    Private Sub AssignVehicle(ExternalPreparationDetail As ExternalPatientPreparationDetail)
        With ExternalPreparationDetail
            .ComponentType = eComponentType.Vehicle
            .AtcId = VehicleId
            .AtcCodeName = VehicleCodeName
            .Volume = VolumeVehicle
            .VolumeMeasureUnitId = VehicleUnitMeasurement
            .VolumeMeasureUnitCodeName = VehicleUnitMeasurementCodeName
            .VolumeMeasureUnitAbreviation = GetUnitMeasurementAbbreviation(VehicleUnitMeasurement)
        End With
    End Sub

#End Region

#Region "Validations"

    ''' <summary>
    ''' Método que valida la cantidad del volumen para el cálculo del volumen del vehiculo
    ''' </summary>
    Private Function ValidateVolumeQuantity()
        If VolumeMedicine Is Nothing OrElse VolumeMedicine = 0 Then
            VolumeVehicle = Nothing
            Mensaje(EeventViewerImages.Advertencia) = $"Para el cálculo del vehículo registre el volumen del medicamento"
            Return False
        End If

        If TotalPreparedVolume <= VolumeMedicine Then
            VolumeVehicle = Nothing
            Mensaje(EeventViewerImages.Advertencia) = $"El total del preparado debe ser mayor al volumen del medicamento"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Método que valida la cantidad del reconstituyente para el cálculo del volumen del vehiculo
    ''' </summary>
    Private Function ValidateReconstitutionQuantity()
        If ReconstituentVolume Is Nothing OrElse ReconstituentVolume = 0 Then
            VolumeVehicle = Nothing
            Mensaje(EeventViewerImages.Advertencia) = $"Para el cálculo del vehículo registre el volumen del reconstituyente"
            Return False
        End If

        If TotalPreparedVolume <= (ReconstituentVolume + If(VolumeMedicine, 0)) Then
            VolumeVehicle = Nothing
            Mensaje(EeventViewerImages.Advertencia) = $"El total del preparado debe ser mayor al volumen de la reconstitución"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Método que valida todos los campos del popUp
    ''' </summary>
    Private Function ValidateAllFields() As Boolean
        Dim errors As New StringBuilder

        If AtcId Is Nothing OrElse Atc Is Nothing Then
            errors.AppendLine("Debe seleccionar un medicamento")
        End If

        If Atc IsNot Nothing Then
            If Atc.FormulationType = eFormulationType.WeightVolume OrElse Atc.FormulationType = eFormulationType.Weight Then
                If Quantity Is Nothing OrElse Quantity = 0 Then
                    errors.AppendLine("Ingrese la cantidad para el medicamento")
                End If
            End If

            If Atc.FormulationType = eFormulationType.WeightVolume OrElse Atc.FormulationType = eFormulationType.Volume Then
                If VolumeMedicine Is Nothing OrElse VolumeMedicine = 0 Then
                    errors.AppendLine("Ingrese el volumen del medicamento")
                End If
            End If
        End If

        If PreparationType Is Nothing OrElse PreparationType = 0 Then
            errors.AppendLine("Debe seleccionar un tipo de preparación")
        End If

        If AdministrationRouteId Is Nothing OrElse AdministrationRouteId = 0 Then
            errors.AppendLine("Debe seleccionar una vía de administración")
        End If

        If PreparationsRequested = 0 Then
            errors.AppendLine("Ingrese la cantidad de preparaciones a solicitar")
        End If

        If PreparationType = ePreparationType.Reconstitution OrElse PreparationType = ePreparationType.ReconstitutionDilution Then
            If ReconstituentId Is Nothing Then
                errors.AppendLine("Debe seleccionar un Reconstituyente")
            End If

            If ReconstituentUnitMeasurement Is Nothing Then
                errors.AppendLine("Debe seleccionar una unidad de medida para el Reconstituyente")
            End If
        End If

        If PreparationType = ePreparationType.Dilution OrElse PreparationType = ePreparationType.ReconstitutionDilution Then
            If TotalPreparedVolume Is Nothing OrElse TotalPreparedVolume = 0 Then
                errors.AppendLine("Ingrese la cantidad para el volumen total de la adecuación")
            End If

            If TotalPreparedUnitMeasurement Is Nothing Then
                errors.AppendLine("Debe seleccionar una unidad de medida para el total del preparado")
            End If

            If VehicleId Is Nothing OrElse VehicleId = 0 Then
                errors.AppendLine("Debe seleccionar un vehículo")
            End If

            If VolumeVehicle Is Nothing OrElse VolumeVehicle = 0 Then
                errors.AppendLine("El volumen del vehículo debe ser superior a 0, ajuste el volumen del total del preparado")
            End If

            If VehicleUnitMeasurement Is Nothing Then
                errors.AppendLine("Debe seleccionar una unidad de medida para el vehículo")
            End If

        End If

        If errors.Length > 0 Then
            INDbtnAdd.Enabled = True
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Return False
        Else
            Return True
        End If
    End Function

#End Region

#Region "OthersMethods"

    ''' <summary>
    ''' Abre el formulario para agregar los detalles de la preparación solicitada
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 730)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Método que inicializa las tuplas
    ''' </summary>
    Private Sub InitializationTuplas()
        PreparationTypeDatasource = New List(Of Tuple(Of Byte, String))

        Dim ListComponentType = New List(Of Tuple(Of Byte, String))()
        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(UnitDoseType.MSClass) Then
            ListComponentType.Add(New Tuple(Of Byte, String)(1, "Medicamento"))
        End If
        ComponentTypeDatasource = ListComponentType
    End Sub

    ''' <summary>
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Sub LoadControls()
        If NewExternalPatientPreparation Is Nothing Then
            NewExternalPatientPreparation = New ExternalPatientPreparation
            Exit Sub
        End If

        isLoading = True
        With NewExternalPatientPreparation
            For Each item In NewExternalPatientPreparation.ExternalPatientPreparationDetail.OrderBy(Function(d) d.ComponentType)
                With item
                    Select Case .ComponentType
                        Case eComponentType.MainMedicine
                            ComponentType = .ComponentType
                            AtcId = .AtcId
                            MedicineCodeName = .CodeName
                            INDSleMedicine.ReadOnly = True
                            PreparationType = NewExternalPatientPreparation.PreparationTypeId

                            If INDLciQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                Quantity = .Quantity
                                MeasureUnitId = .MeasurementUnitId
                                MeasureUnitCodeName = .MeasurementUnitCodeName
                            End If

                            If INDLciVolume.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                VolumeMedicine = .Volume
                                MeasureUnitVolumeId = .VolumeMeasureUnitId
                                MeasureUnitVolumeCodeName = .VolumeMeasureUnitCodeName
                            End If

                        Case eComponentType.Reconstituent
                            ReconstituentId = .AtcId
                            ReconstituentCodeName = .CodeName
                            ReconstituentVolume = .Volume
                            ReconstituentUnitMeasurement = .VolumeMeasureUnitId
                            ReconstituentUnitMeasurementCodeName = .VolumeMeasureUnitCodeName

                        Case eComponentType.Vehicle
                            VehicleId = .AtcId
                            VehicleCodeName = .CodeName
                            VolumeVehicle = .Volume
                            VehicleUnitMeasurement = .VolumeMeasureUnitId
                            VehicleUnitMeasurementCodeName = .VolumeMeasureUnitCodeName
                    End Select
                End With
            Next

            PreparationsRequested = .PreparationsRequested
            AdministrationRouteId = .AdministrationRouteId
            AdministrationRouteCodeName = .AdministrationRouteCodeName
            TotalPreparedVolume = .VolumeTotalOrder
            TotalPreparedUnitMeasurement = .TotalPreparedUnitMeasurementId
            TotalPreparedUnitMeasurementCodeName = .TotalPreparedUnitMeasurementCodeName
            concentration = .Concentration
        End With

        isLoading = False

        If AllowAdd Then
            ActionsOnControls = True

            If PreparationType = ePreparationType.ReconstitutionDilution OrElse PreparationType = ePreparationType.Dilution Then
                INDsleVehicleUnitMeasurement.Enabled = False
                INDsleTotalPreparedUnitMeasurement.Enabled = False
            End If
        Else
            ActionsOnControls = False
        End If

        INDbtnAdd.Text = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
    End Sub

    ''' <summary>
    ''' Método que obtiene los atc por Id
    ''' </summary>
    Private Async Function GetAtcById(id As Integer?, ComponentType As Integer) As Task(Of ATCXpo)
        Dim inventoryProductPresenter As New PInventoryProduct
        Dim item As ATCXpo = Await inventoryProductPresenter.GetMedicamentById(id)

        ' Si no se encuentra, mostrar el mensaje y limpiar controles
        If item Is Nothing Then
            Select Case ComponentType
                Case eComponentType.MainMedicine
                    Mensaje(EeventViewerImages.Advertencia) = "El medicamento no fue encontrado"
                    CleanAllControls()
                Case eComponentType.Reconstituent
                    Mensaje(EeventViewerImages.Advertencia) = "El Reconstituyente no fue encontrado"
                    CleanReconstitutionControls()
                Case eComponentType.Vehicle
                    Mensaje(EeventViewerImages.Advertencia) = "El vehículo no fue encontrado"
                    CleanDilutionControls()
            End Select
            Return Nothing
        End If

        If (item.FormulationType = eFormulationType.WeightVolume OrElse item.FormulationType = eFormulationType.Weight) AndAlso item.Weight <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El medicamento seleccionado debe tener un valor parametrizado como peso en su maestro correspondiente."
            Return Nothing
        End If

        If (item.FormulationType = eFormulationType.WeightVolume OrElse item.FormulationType = eFormulationType.Volume) AndAlso item.Volume <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El medicamento seleccionado debe tener un valor parametrizado como volumen en su maestro correspondiente."
            Return Nothing
        End If

        Return item
    End Function

#End Region

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupPackageDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.StatusRecordVisible = False

        Presenter = New PDetailRequestAntibiotic(Me)
        InitializationTuplas()
        CleanAllControls()
        LoadControls()

        If Not AllowAdd Then
            INDbtnAdd.Enabled = False
        Else
            INDbtnAdd.Enabled = True
        End If

    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup
    ''' </summary>
    Private Sub FrmDetailRequestAntibiotic_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control del medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMedicine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMedicine.QueryPopUp
        If AtcDatasource Is Nothing Then
            If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(UnitDoseType.MSClass) Then
                Presenter.InitializeAtcByFormulationTypeWeigth(True, String.Format("{0}, {1}", Convert.ToInt32(eFormulationType.Weight), Convert.ToInt32(eFormulationType.WeightVolume)))
            Else
                Presenter.InitializeAtcByFormulationTypeWeigth(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMeasurementUnit.QueryPopUp
        If MeasurementUnitDatasource Is Nothing Then
            Presenter.InitializeMeasureUnit(Atc.FormulationType)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control dela unidad de medida de volumen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleVolumeMeasureUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleVolumeMeasureUnit.QueryPopUp
        If MeasureUnitVolumeDatasource Is Nothing Then
            Presenter.InitializeMeasureUnitVolume()
        End If
    End Sub

    '================================================================================

    ''' <summary>
    ''' Evento que se dispara al desplegar el control del Reconstituyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleReconstituent_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleReconstituent.QueryPopUp
        If ReconstituentDatasource Is Nothing Then
            Presenter.InitializeReconstituent()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la unidad de medida del Reconstituyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleReconstituentUnitMeasurement_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleReconstituentUnitMeasurement.QueryPopUp
        If ReconstituentUnitMeasurementDatasource Is Nothing Then
            Presenter.InitializeReconstituentUnitMeasurement()
        End If
    End Sub

    '================================================================================
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la unidad de medida del total del preparado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleTotalPreparedUnitMeasurement_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleTotalPreparedUnitMeasurement.QueryPopUp
        If TotalPreparedUnitMeasurementDatasource Is Nothing Then
            Presenter.InitializeTotalPreparedUnitMeasurement()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control del vehículo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleVehicleDilution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleVehicleDilution.QueryPopUp
        If VehicleDatasource Is Nothing Then
            Presenter.InitializeVehicle()
        End If
    End Sub
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de la unidad de medida del vehículo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleVolumeUnitMeasurement_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleVehicleUnitMeasurement.QueryPopUp
        If VehicleUnitMeasurementDatasource Is Nothing Then
            Presenter.InitializeVehicleUnitMeasurement()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleMedicine_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMedicine.EditValueChanged
        CleanMedicineControls()

        If AtcId Is Nothing Then
            Exit Sub
        End If

        Atc = Await GetAtcById(AtcId, eComponentType.MainMedicine)

        Dim ListPreparationType = New List(Of Tuple(Of Byte, String))

        Select Case Atc.FormulationType
            Case eFormulationType.Weight
                INDLciVolume.HideControl()
                INDLciVolumeMeasureUnit.HideControl()

                Quantity = Atc.Weight
                MeasureUnitId = Atc.WeightMeasureUnit.Id
                MeasureUnitCodeName = Atc.WeightMeasureUnit.CodeName

                ListPreparationType.Add(New Tuple(Of Byte, String)(ePreparationType.Reconstitution, "Reconstitución"))
                ListPreparationType.Add(New Tuple(Of Byte, String)(ePreparationType.ReconstitutionDilution, "Reconstitucion - Dilución"))

            Case eFormulationType.Volume
                INDLciQuantity.HideControl()
                INDLciUnitMeasurement.HideControl()

                VolumeMedicine = Atc.Volume
                MeasureUnitVolumeId = Atc.VolumeMeasureUnit.Id
                MeasureUnitVolumeCodeName = Atc.VolumeMeasureUnit.CodeName

                ListPreparationType.Add(New Tuple(Of Byte, String)(ePreparationType.Dilution, "Dilución"))

            Case eFormulationType.WeightVolume
                Quantity = Atc.Weight
                MeasureUnitId = Atc.WeightMeasureUnit.Id
                MeasureUnitCodeName = Atc.WeightMeasureUnit.CodeName

                VolumeMedicine = Atc.Volume
                MeasureUnitVolumeId = Atc.VolumeMeasureUnit.Id
                MeasureUnitVolumeCodeName = Atc.VolumeMeasureUnit.CodeName
                INDSeVolume.ReadOnly = True

                ListPreparationType.Add(New Tuple(Of Byte, String)(ePreparationType.Dilution, "Dilución"))

            Case Else
                Mensaje(EeventViewerImages.Advertencia) = $"El medicamento {Atc.CodeName} posee una unidad de medida que no corresponde a peso o volumen"
                CleanAllControls()
                Exit Sub
        End Select

        ActionsOnControls = True
        PreparationTypeDatasource = ListPreparationType
        Presenter.InitializeAdministrationRoute(AtcId)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el tipo de preparación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSlePreparationType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlePreparationType.EditValueChanged
        CleanReconstitutionControls()
        CleanDilutionControls()
        HideControlsAddComponent()

        If PreparationType Is Nothing Then
            Exit Sub
        End If

        If PreparationType = ePreparationType.Reconstitution Then
            INDLcgReconstitution.HideControl(False)

        ElseIf PreparationType = ePreparationType.Dilution Then
            If Atc IsNot Nothing AndAlso Atc.FormulationType = eFormulationType.WeightVolume Then
                SetVolumeUnit(MeasureUnitVolumeId, MeasureUnitVolumeCodeName)
            End If

            INDLcgDilution.HideControl(False)

        ElseIf PreparationType = ePreparationType.ReconstitutionDilution Then
            INDLcgReconstitution.HideControl(False)
            INDLcgDilution.HideControl(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar la unidad de medida de volumen del componente/medicamento principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleVolumeMeasureUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleVolumeMeasureUnit.EditValueChanged
        If Atc IsNot Nothing AndAlso Atc.FormulationType = eFormulationType.WeightVolume AndAlso PreparationType = ePreparationType.Dilution Then
            SetVolumeUnit(MeasureUnitVolumeId, MeasureUnitVolumeCodeName)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el reconstituyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleReconstituent_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleReconstituent.EditValueChanged
        If ReconstituentId Is Nothing Then
            Exit Sub
        End If

        Reconstituent = Await GetAtcById(ReconstituentId, eComponentType.Reconstituent)

        ReconstituentUnitMeasurement = Reconstituent.VolumeMeasureUnit.Id
        ReconstituentUnitMeasurementCodeName = Reconstituent.VolumeMeasureUnit.CodeName
        SetVolumeUnitMeasurementFromReconstituent()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar la unidad de medida de volumen del reconstituyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleReconstituentUnitMeasurement_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleReconstituentUnitMeasurement.EditValueChanged
        SetVolumeUnitMeasurementFromReconstituent()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar la unidad de medida de volumen del reconstituyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleVehicleDilution_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleVehicleDilution.EditValueChanged
        If VehicleId IsNot Nothing AndAlso (PreparationType = ePreparationType.Dilution OrElse PreparationType = ePreparationType.ReconstitutionDilution) Then
            Vehicle = Await GetAtcById(VehicleId, eComponentType.Vehicle)
        End If
    End Sub

#End Region

#Region "Validating"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la cantidad del medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeQuantity_Validating(sender As Object, e As CancelEventArgs) Handles INDSeQuantity.Validating
        If Atc.FormulationType = eFormulationType.WeightVolume Then
            CalculateVolumeFromWeight()
        End If

        VehicleVolumeCalculation()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del volumen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeVolume_Validating(sender As Object, e As CancelEventArgs) Handles INDSeVolume.Validating
        VehicleVolumeCalculation()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del reconstituyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeReconstituentVolume_Validating(sender As Object, e As CancelEventArgs) Handles INDSeReconstituentVolume.Validating
        If PreparationType = ePreparationType.Reconstitution Then
            TotalPreparedVolume = ReconstituentVolume
        End If

        VehicleVolumeCalculation()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del volumen del total del preparado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeTotalPreparedVolume_Validating(sender As Object, e As CancelEventArgs) Handles INDSeTotalPreparedVolume.Validating
        VehicleVolumeCalculation()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al ejecutar el clic del botón agregar del popUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        Try
            INDbtnAdd.Enabled = False
            If Not ValidateAllFields() Then
                Exit Sub
            End If

            CalculateConcentration()
            AddPreparation()

            Dim args As New AddExternalPatientPreparationEventArgs
            args.ExternalPatientPreparation = NewExternalPatientPreparation
            RaiseEvent AddExternalPatientPreparation(Nothing, args)
            CleanExternalPreparation()

        Catch ex As Exception
            Throw ex
        Finally
            INDbtnAdd.Enabled = True
        End Try
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al dar click para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmDetailRequestAntibiotic_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If AtcId IsNot Nothing AndAlso AtcId > 0 Then
            If Not MessageIndigo.Show("Al cerrar el formulario se perderá la información que habia registrado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddExternalPatientPreparationEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Obtiene o establece el objeto para guardar la preparación
    ''' </summary>
    Property ExternalPatientPreparation As ExternalPatientPreparation

End Class