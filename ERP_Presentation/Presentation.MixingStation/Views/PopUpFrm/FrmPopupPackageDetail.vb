'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 13/06/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports DevExpress.XtraEditors.Controls
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Presentation.MixingStation.MVP
Imports PPackage = Presentation.MixingStation.MVP.PPackage

#End Region

Public Class FrmPopupPackageDetail

#Region "Events"
    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddPackageDetail(sender As Object, e As AddProductPackageDetailEventArgs)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Permite consulta por medio de xpo la entidad de medicamentos
    ''' </summary>
    Private _atc As ATCXpo

    ''' <summary>
    ''' Permite consulta por medio de xpo la entidad de diluyente
    ''' </summary>
    Private _atcVehicle As ATCXpo

    ''' <summary>
    ''' Permite consulta por medio de xpo la entidad de factores de dilucion
    ''' </summary>
    Private _dilution As DilutionFactorsXpo

    ''' <summary>
    ''' Permite consulta por medio de xpo el detalle de factores de dilucion
    ''' </summary>
    Private _dilutionDetail As DilutionFactorsDetailXpo

    ''' <summary>
    ''' Permite consulta por medio de xpo a la unidad de medida del medicamento principal
    ''' </summary>
    Private _MeasureUnit As MeasureUnitXpo

    ''' <summary>
    ''' Permite consulta por medio de xpo a la unidad de medida del vehiculo
    ''' </summary>
    Private _MeasureUnitVehicle As MeasureUnitXpo

    ''' <summary>
    ''' Permite consulta por medio de xpo a la unidad de medida del reconstituyente
    ''' </summary>
    Private _MeasureUnitThinner As MixingStationMeasurementUnitXpo

    ''' <summary>
    ''' Presentador de package
    ''' </summary>
    Private Presenter As PPackage

    ''' <summary>
    ''' Define si el formulario esta editando o no
    ''' </summary>
    ''' <remarks></remarks>
    Private _editModeForm As Boolean

    ''' <summary>
    ''' Define si el form es solo para visualizar
    ''' </summary>
    ''' <remarks></remarks>
    Private _onlyRead As Boolean

    ''' <summary>
    ''' Datasource search
    ''' </summary>
    Private ListYesNo As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Datasource del tipo de preparacion
    ''' </summary>
    Private ListPreparationType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Datasource del tipo de unidad de tiempo
    ''' </summary>
    Private ListTimeUnit As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Datasource del tipo de detalle
    ''' </summary>
    Private ListComponentType As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' id de la unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Private _operatingUnitId As Integer

    ''' <summary>
    ''' listado del detalle del paquete para validar que los productos no se repitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPackageDetailValidation As List(Of PackageDetail)

    ''' <summary>
    ''' listado del detalle del paquete para validar que los productos no se repitan con la misma fuente
    ''' </summary>
    ''' <remarks></remarks>
    Private _listPackageDetailNuevos As List(Of PackageDetail)

    ''' <summary>
    ''' Obtiene el Id del paquete cabecera
    ''' </summary>
    Private _packageId As Integer

    ''' <summary>
    ''' Representa a la entidad de tipo de dosis unitaria que se selecciona en el form principal
    ''' </summary>
    Public unitDoseType As UnitDoseType

    ''' <summary>
    ''' Indica el tipo de formulacion del medicamento
    ''' '1-Peso,  2-Volumen,  3-Peso-Volumen,  4-Unidad de administración
    ''' </summary>
    Public FormulationType As Integer

    ''' <summary>
    ''' propiedad para para pasar el listado del detalle del paquete
    ''' </summary>
    Public Property ListPackageDetailValidation As List(Of PackageDetail)
        Get
            Return _listPackageDetailValidation
        End Get
        Set(value As List(Of PackageDetail))
            If value IsNot Nothing Then
                _listPackageDetailValidation = value
            Else
                _listPackageDetailValidation = New List(Of PackageDetail)
            End If
        End Set
    End Property
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el volumen del producto para el detalle del paquete
    ''' </summary>
    Public Property VolumenProduct As Decimal
        Get
            Return CType(INDseVolume.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDseVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cantidad del producto para el detalle del paquete
    ''' </summary>
    Public Property Quantity As Decimal
        Get
            Return CType(INDseQuantity.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDseQuantity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida de los medicamentos
    ''' </summary>
    Public Property MeasureUnitId As Integer?
        Get
            Return INDsleMeasurementUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleMeasurementUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del volumen de los medicamentos
    ''' </summary>
    Public Property VolumeMeasureUnit As Integer?
        Get
            Return INDsleVolumeMeasureUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleVolumeMeasureUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si es o no diluyente
    ''' </summary>
    Public Property Thinner As Boolean?
        Get
            Return INDsleThinner.EditValue
        End Get
        Set(value As Boolean?)
            INDsleThinner.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si es o no vehículo
    ''' </summary>
    Public Property Vehicle As Boolean?
        Get
            Return INDsleVehicle.EditValue
        End Get
        Set(value As Boolean?)
            INDsleVehicle.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para establecer el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property operatingUnitId As Integer
        Set(value As Integer)
            _operatingUnitId = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editModeForm = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad publica para pasar el registro que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property PackageDetailEdit As PackageDetail

    ''' <summary>
    ''' propiedad publica para pasar la lista de detalles que se va a editar
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property PackageDetaiAntibioticEdit As New List(Of PackageDetail)

    ''' <summary>
    ''' Establece el Id del paquete cabecera
    ''' </summary>
    Public WriteOnly Property PackageId As Integer
        Set(value As Integer)
            _packageId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de componente
    ''' </summary>
    Public Property ComponentType As Byte?
        Get
            Return CType(INDGleType.EditValue, Byte?)
        End Get
        Set(value As Byte?)
            INDGleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de preparación
    ''' </summary>
    Public Property PreparationType As Byte?
        Get
            Return CType(INDslePreparationType.EditValue, Byte?)
        End Get
        Set(value As Byte?)
            INDslePreparationType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el medicamento
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
    ''' Obtiene o establece el factor de dilucion
    ''' </summary>
    Public Property ReconstituyenteId As Integer?
        Get
            Return CType(INDsleReconstitution.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDsleReconstitution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el insumo
    ''' </summary>
    Public Property SupplieId As Integer?
        Get
            Return CType(INDSleSupplie.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleSupplie.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el producto
    ''' </summary>
    Public Property ProductId As Integer?
        Get
            Return CType(INDSleProduct.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleProduct.EditValue = value
        End Set
    End Property

    Private _dilutionFactor As Decimal
    ''' <summary>
    ''' Obtiene el factor de dilucion del reconstituyente
    ''' </summary>
    Public Property DilutionFactor As Decimal
        Get
            Return _dilutionFactor
        End Get
        Set(value As Decimal)
            _dilutionFactor = value
            INDtxtFactor.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la concentracion del reconstituyente
    ''' </summary>
    Public Property Concentration As Decimal

    ''' <summary>
    ''' Obtiene la concentracion del vehiculo
    ''' </summary>
    Public Property ConcentrationDilution As Decimal?

    ''' <summary>
    ''' volumen del reconstituyente
    ''' </summary>
    ''' <returns></returns>
    Public Property VolumenThinner As Decimal
        Get
            Return CType(INDtxtVolume.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDtxtVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la unidad de tiempo de la estabilidad
    ''' </summary>
    Public Property TimeUnitDilution As Byte?
        Get
            Return INDsleTimeUnit.EditValue
        End Get
        Set(value As Byte?)
            INDsleTimeUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la cantidad de tiempo de la estabilidad
    ''' </summary>
    ''' <returns></returns>
    Public Property AmountTimeDilution As Integer?
        Get
            Return CType(INDsleQuantityDilution.EditValue, Integer)
        End Get
        Set(value As Integer?)
            INDsleQuantityDilution.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la unidad de tiempo
    ''' </summary>
    Public Property TimeUnit As Byte

    ''' <summary>
    ''' Obtiene la cantidad de tiempo
    ''' </summary>
    Public Property AmountTime As Integer

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
    ''' volumen total del preparado
    ''' </summary>
    ''' <returns></returns>
    Public Property VolumenVehicleTotal As Decimal
        Get
            Return CType(INDVolumenTotalVehicle.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDVolumenTotalVehicle.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' volumen del vehiculo 
    ''' </summary>
    ''' <returns></returns>
    Public Property VolumenVehicle As Decimal

    ''' <summary>
    ''' Obtiene o establece la unidad de medida del preparado
    ''' </summary>
    Public Property MeasureunitPrepared As Integer?
        Get
            Return INDsleUnitMeasurementVehicle.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnitMeasurementVehicle.EditValue = value
        End Set
    End Property

    Private _sourceName As String
    Public Property SourceName() As String
        Get
            Return _sourceName
        End Get
        Set(ByVal value As String)
            _sourceName = value
        End Set
    End Property

    Private _sourceNameSub As String
    Public Property SourceNameSub() As String
        Get
            Return _sourceNameSub
        End Get
        Set(ByVal value As String)
            _sourceNameSub = value
        End Set
    End Property

    Private _osmolarity As Decimal
    Public Property Osmolarity() As Decimal
        Get
            Return _osmolarity
        End Get
        Set(ByVal value As Decimal)
            _osmolarity = value
        End Set
    End Property

    Private _density As Decimal
    Public Property Density() As Decimal
        Get
            Return _density
        End Get
        Set(ByVal value As Decimal)
            _density = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si el medicamento es o no principal
    ''' </summary>
    Public Property MainMedicine As Boolean?
        Get
            Return INDGleMainMedicine.EditValue
        End Get
        Set(value As Boolean?)
            INDGleMainMedicine.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indica si el formulario se abrió desde el Dashboard de Confirmación de Dosis Unitarias
    ''' </summary>
    Public Property IsDashboardConfirmationUnitDose As Boolean = False

    ''' <summary>
    ''' Indica si el nuevo detalle debe ser automáticamente un medicamento complementario
    ''' (Determinado automáticamente: True si ya existe un medicamento principal en la grilla)
    ''' </summary>
    Public Property IsNewDetailComplementary As Boolean = False

    ''' <summary>
    ''' Indica si el nuevo detalle debe ser automáticamente un medicamento principal
    ''' (Determinado automáticamente: True si NO existe un medicamento principal en la grilla)
    ''' </summary>
    Public Property IsNewDetailMainMedicine As Boolean = False

    ''' <summary>
    ''' Dosis solicitada desde el Dashboard de Confirmación de Dosis Unitarias
    ''' </summary>
    Public Property RequestedDosage As Decimal = 0D

    ''' <summary>
    ''' ATC del medicamento principal para filtrar medicamentos complementarios
    ''' </summary>
    Public Property MainMedicineAtcId As Integer?

    ''' <summary>
    ''' Tipo de formulación del medicamento principal para filtrar medicamentos complementarios
    ''' </summary>
    Public Property MainMedicineFormulationType As Integer?

    ''' <summary>
    ''' Indica si el detalle es un medicamento complementario (solo para uso interno)
    ''' </summary>
    Private _complementaryMedicine As Boolean? = Nothing
    Public Property ComplementaryMedicine As Boolean?
        Get
            Return _complementaryMedicine
        End Get
        Set(value As Boolean?)
            _complementaryMedicine = value
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Abre el formulario para adicion de productos
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

    Private Sub CleanControlsAll()
        ComponentType = If(unitDoseType.MSClass <> EUnitDoseTypeClass.ParenteralNutrition, CByte(1), CByte(4))
        CleanControls()
    End Sub

    ''' <summary>
    ''' Limpia los controles del grupo de dilucion
    ''' </summary>
    Private Sub CleanDilutionControls()
        VehicleId = Nothing
        VolumenVehicle = Nothing
        TimeUnitDilution = Nothing
        AmountTimeDilution = Nothing
        VolumenVehicleTotal = Nothing
        TimeUnitDilution = Nothing
        INDVolumeVehicle.EditValue = Nothing
        INDConcentrationVehicle.EditValue = Nothing

        INDSleVehicleDilution.Properties.NullText = String.Empty
        INDVolumenTotalVehicle.Properties.NullText = String.Empty
        INDConcentrationVehicle.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Limpia los controles al cambiar el tipo de preparación
    ''' </summary>
    Private Sub CleanReconstitutionControls()
        ReconstituyenteId = Nothing
        Concentration = Nothing
        VolumenThinner = Nothing
        DilutionFactor = Nothing
        INDtxtStability.EditValue = Nothing
        INDtxtConcentration.EditValue = Nothing
        AmountTimeDilution = Nothing

        INDtxtVolume.Properties.NullText = String.Empty
        INDtxtFactor.Properties.NullText = String.Empty
        INDsleReconstitution.Properties.NullText = String.Empty
    End Sub

    ''' <summary>
    ''' Limpia los controles al elegir medicamento
    ''' </summary>
    Private Sub HideControlsAddComponent()
        INDlygGeneralData.HideControl()
        INDlygReconstitution.HideControl()
        INDlygDilution.HideControl()
        INDlygDilutionStability.HideControl()
    End Sub

    ''' <summary>
    ''' Muestra los controles apropiados según el tipo de componente seleccionado
    ''' </summary>
    ''' <param name="componentTypeValue">Tipo de componente (1=Medicamento, 2=Insumo, 3=Producto, 4=Medicamento NPT, 5=Insumo NPT)</param>
    Private Sub ShowControlsForComponentType(componentTypeValue As Byte?)
        If componentTypeValue Is Nothing Then Return

        ' Ocultar todos los controles primero
        Dim groupControlsToHide = {INDlygGeneralData, INDLciMedicine, INDlyItemThinner, INDlyItemVehicle,
            INDLciMainMedicine, INDlyItemVolume, INDlyItemVolumeMeasureUnit, INDlyItemPreparationType, INDLciSupplie, INDLciProduct}

        ApplyControlVisibility(groupControlsToHide, False)

        ' Mostrar controles según el tipo de componente
        Select Case componentTypeValue
            Case 1, 4 ' Medicamento o Diluyente adicional para NPT
                If unitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                    INDlygGeneralData.HideControl()
                    INDlyItemPreparationType.HideControl()
                Else
                    INDlygGeneralData.HideControl(False)
                    INDLciMedicine.ShowLayout()
                    INDlyItemPreparationType.HideControl(False)
                End If

            Case 2 ' Insumo
                INDLciSupplie.HideControl(False)
                INDsleMeasurementUnit.Properties.DataSource = Nothing

            Case 3, 5 ' Producto tipo otro o insumo para NPT
                INDLciProduct.HideControl(False)
                INDLciProduct.Text = If(componentTypeValue = 5, "Insumo", "Producto")
        End Select
    End Sub

    ''' <summary>
    ''' Limpia los controles para agregar un producto nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        AtcId = Nothing
        SupplieId = Nothing
        ProductId = Nothing
        VehicleId = Nothing
        MeasureUnitId = Nothing
        ReconstituyenteId = Nothing
        _MeasureUnit = Nothing
        MeasureunitPrepared = Nothing
        _MeasureUnitVehicle = Nothing
        PreparationType = Nothing
        VolumenVehicle = Nothing
        VolumenVehicleTotal = Nothing
        Concentration = Nothing
        ConcentrationDilution = Nothing
        AmountTimeDilution = Nothing
        TimeUnitDilution = Nothing
        INDseVolume.EditValue = Nothing
        INDVolumenTotalVehicle.EditValue = Nothing
        INDtxtVolume.EditValue = Nothing
        VolumeMeasureUnit = Nothing
        INDsleMeasurementUnit.Properties.DataSource = Nothing
        INDsleUnitMeasurementVehicle.Properties.DataSource = Nothing

        Thinner = False
        Vehicle = False
        MainMedicine = False

        Quantity = 0
        Me.Osmolarity = 0
        Me.Density = 0
        TimeUnit = 1

        INDSleMedicine.Properties.NullText = String.Empty
        INDSleSupplie.Properties.NullText = String.Empty
        INDSleProduct.Properties.NullText = String.Empty
        INDsleReconstitution.Properties.NullText = String.Empty
        INDSleVehicleDilution.Properties.NullText = String.Empty
        INDsleMeasurementUnit.Properties.NullText = String.Empty
        INDsleThinner.Properties.NullText = String.Empty
        INDsleVehicle.Properties.NullText = String.Empty
        INDGleMainMedicine.Properties.NullText = String.Empty
        Me.SourceName = String.Empty
        INDsleVolumeMeasureUnit.Properties.NullText = String.Empty

        INDLciSupplie.HideControl()
        INDLciProduct.HideControl()
        INDlyItemVolume.HideControl()
        INDlyItemVolumeMeasureUnit.HideControl()
        INDLciMedicine.HideControl(False)
        INDlyItemThinner.HideControl(False)
        INDlyItemVehicle.HideControl(False)
        INDLciMainMedicine.HideControl(False)
        INDsleMeasurementUnit.Enabled = True
        INDslePreparationType.ReadOnly = True

        ReadOnlyControlsDashboardConfirmationUnitDose(False)
    End Sub

    ''' <summary>
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Async Function LoadControls() As Task
        If _editModeForm Then
            INDbtnAdd.Text = ResourceManager.GetString("Edit")
        End If

        ' Verificar si es un medicamento complementario (se edita solo, sin estructura de principal)
        If PackageDetailEdit IsNot Nothing AndAlso PackageDetailEdit.ComplementaryMedicine.GetValueOrDefault(False) Then
            ' Cargar solo el medicamento complementario
            LoadComplementaryMedicineForEdit()
            Return
        End If

        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(unitDoseType.MSClass) And PackageDetaiAntibioticEdit.Any() Then
            ' Verificar si el primer item es complementario
            Dim firstItem = PackageDetaiAntibioticEdit.FirstOrDefault()
            If firstItem IsNot Nothing AndAlso firstItem.ComplementaryMedicine.GetValueOrDefault(False) Then
                PackageDetailEdit = firstItem
                LoadComplementaryMedicineForEdit()
                Return
            End If

            PreparationType = PackageDetaiAntibioticEdit.FirstOrDefault().PreparationType
            For Each item In PackageDetaiAntibioticEdit

                PackageDetailEdit = item
                With PackageDetailEdit
                    If .MainMedicine Then
                        ComponentType = .ComponentType
                        INDGleType.ReadOnly = True
                        AtcId = .AtcId
                        INDSleMedicine.Properties.NullText = .SourceCodeName
                        INDSleMedicine.ReadOnly = True

                        MeasureUnitId = .MeasurementUnitId
                        INDsleMeasurementUnit.Properties.NullText = .MeasureUnitDescription
                        Quantity = .Quantity

                        If .Volume > 0 Then
                            VolumenProduct = .Volume
                            VolumeMeasureUnit = .VolumeMeasureUnit
                            INDsleVolumeMeasureUnit.Properties.NullText = .VolumeMeasureUnitDescription
                        End If

                    ElseIf .Thinner Then
                        Dim MeasureUnitATC = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MeasureUnitXpo)($"Id = { .MeasurementUnitId}"))
                        ReconstituyenteId = .AtcId
                        DilutionFactor = .Dilution
                        Concentration = .Concentration
                        VolumenThinner = .Quantity
                        INDtxtConcentration.EditValue = .ConcentrationName
                        INDsleReconstitution.Properties.NullText = .SourceCodeName
                        TimeUnit = .TimeUnit
                        AmountTime = .AmountTime
                        CalculateAmountTime(TimeUnit, AmountTime)
                        INDtxtUnitMeasurement.EditValue = MeasureUnitATC.Name

                        INDlygReconstitution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDlygReconstitution.AllowHide = False

                    ElseIf .Vehicle Then
                        VehicleId = .AtcId
                        MeasureunitPrepared = .MeasurementUnitId
                        VolumenVehicle = .Quantity
                        VolumenVehicleTotal = .VolumeTotal
                        ConcentrationDilution = .Concentration
                        INDConcentrationVehicle.EditValue = .ConcentrationName
                        AmountTimeDilution = .AmountTime
                        TimeUnitDilution = .TimeUnit
                        INDSleVehicleDilution.Properties.NullText = .SourceCodeName

                        INDlygDilution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDlygDilutionStability.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    End If

                    'Si el detalle viene creado desde el dashboard confirmación de dosis unitarias
                    If .IsDashboardConfirmationUnitDose Then
                        ReadOnlyControlsDashboardConfirmationUnitDose(True, .SourceType)
                    End If
                End With
            Next

            ' NOTA: Cuando se edita el medicamento principal, se deben mostrar TODOS los controles
            ' incluyendo tipo de preparación, reconstitución, dilución, etc.
            ' La lógica de ocultar controles solo aplica cuando se edita un medicamento adicional/complementario,
            ' pero eso ya se maneja arriba con LoadComplementaryMedicineForEdit()
        Else
            If PackageDetailEdit Is Nothing Then
                Exit Function
            End If

            With PackageDetailEdit

                INDlyItemPreparationType.HideControl()
                ComponentType = .ComponentType
                INDGleType.Properties.ReadOnly = True
                PreparationType = .PreparationType

                If .ComponentType = 1 OrElse .ComponentType = 4 Then 'Medicamento o Diluyente adicional para NPT
                    AtcId = .AtcId
                    INDSleMedicine.Properties.NullText = .SourceCodeName
                    INDSleMedicine.Properties.ReadOnly = True

                ElseIf .ComponentType = 2 Then 'Insumo
                    SupplieId = .SupplieId
                    INDSleSupplie.Properties.NullText = .SourceCodeName

                ElseIf .ComponentType = 3 OrElse .ComponentType = 5 Then 'Producto tipo otro o producto Tipo Insumo para NPT
                    ProductId = .ProductId
                    INDSleProduct.Properties.NullText = .SourceCodeName
                End If

                Quantity = .Quantity
                MeasureUnitId = .MeasurementUnitId
                INDsleMeasurementUnit.Properties.NullText = .MeasureUnitDescription

                If .Volume > 0 Then
                    VolumenProduct = .Volume
                    VolumeMeasureUnit = .VolumeMeasureUnit
                    INDsleVolumeMeasureUnit.Properties.NullText = .VolumeMeasureUnitDescription
                End If

                If INDlyItemThinner.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    Thinner = .Thinner
                End If

                If INDlyItemVehicle.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    Vehicle = .Vehicle
                End If

                If INDLciMainMedicine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    MainMedicine = .MainMedicine
                End If

                'Si el detalle viene creado desde el dashboard confirmación de dosis unitarias
                If .IsDashboardConfirmationUnitDose Then
                    ReadOnlyControlsDashboardConfirmationUnitDose(True, .SourceType)
                End If
            End With
        End If
    End Function

    ''' <summary>
    ''' Asigna el readOnly a los controles cuando el detalle es creado desde la confirmación de dosis unitaria
    ''' </summary>
    Private Sub ReadOnlyControlsDashboardConfirmationUnitDose(value As Boolean, Optional MedicalOrder As Boolean = False)
        INDGleType.Properties.ReadOnly = value
        INDSleMedicine.Properties.ReadOnly = value
        INDseQuantity.Properties.ReadOnly = value
        INDsleMeasurementUnit.Properties.ReadOnly = value
        INDseVolume.Properties.ReadOnly = value
        INDsleVolumeMeasureUnit.Properties.ReadOnly = value
        If Not MedicalOrder Then
            INDsleThinner.Properties.ReadOnly = value
            INDsleVehicle.Properties.ReadOnly = value
            INDGleMainMedicine.Properties.ReadOnly = value
        End If
    End Sub

    ''' <summary>
    ''' Agrega los datos al detalle de paquete
    ''' </summary>
    Private Async Function AddInventoryProduct() As Task

        If AtcId IsNot Nothing OrElse SupplieId IsNot Nothing OrElse ProductId IsNot Nothing Then
            PackageDetailEdit = New PackageDetail
            Dim atc As ATCXpo
            With PackageDetailEdit
                .ComponentType = ComponentType
                ' Asignar el nombre del tipo de componente según si es complementario o no
                If IsNewDetailComplementary AndAlso IsDashboardConfirmationUnitDose Then
                    .ComponentTypeName = "Medicamento Complementario"
                Else
                    .ComponentTypeName = INDGleType.Text
                End If
                .PreparationType = PreparationType
                .SourceName = Me.SourceName

                If ComponentType = 1 OrElse ComponentType = 4 Then
                    atc = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {AtcId}"))
                    .AtcId = AtcId
                    .SourceCodeName = INDSleMedicine.Text
                    .ComponentName = INDSleMedicine.Text  ' Nombre del componente para la rejilla
                    .PreparationTypeName = If(IsNewDetailComplementary AndAlso IsDashboardConfirmationUnitDose, "Medicamento Complementario", "Medicamento ppal")
                    .DCIName = atc.DCI.Name
                    ' Asignar siempre el objeto ATC con sus propiedades necesarias para cálculos posteriores
                    .ATC = New ATC With {
                        .Id = atc.Id,
                        .FormulationType = atc.FormulationType,
                        .Volume = atc.Volume,
                        .Weight = atc.Weight,
                        .Concentration = atc.Concentration,
                        .VolumeMeasureUnit = atc.VolumeMeasureUnit?.Id,
                        .NullTextUnitMeasureVolumen = atc.VolumeMeasureUnit?.CodeName
                    }
                ElseIf ComponentType = 2 Then
                    Dim supplie = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventorySupplieXpo)($"Id = {SupplieId}"))
                    .SupplieId = SupplieId
                    .SourceCodeName = INDSleSupplie.Text
                    .ComponentName = INDSleSupplie.Text  ' Nombre del componente para la rejilla
                    .PreparationTypeName = "Insumo"
                    .DCIName = supplie.SupplieName
                ElseIf ComponentType = 3 OrElse ComponentType = 5 Then
                    Dim product = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of InventoryProductXpo)($"Id = {ProductId}"))
                    .ProductId = ProductId
                    .SourceCodeName = INDSleProduct.Text
                    .ComponentName = INDSleProduct.Text  ' Nombre del componente para la rejilla
                    .DCIName = product.ATCId?.DCI?.Name
                End If

                .Quantity = Quantity
                .MeasureUnitDescription = INDsleMeasurementUnit.Text
                .MeasurementUnitId = MeasureUnitId

                ' Obtener la unidad de medida si no está cargada
                If _MeasureUnit Is Nothing AndAlso MeasureUnitId.HasValue Then
                    _MeasureUnit = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MeasureUnitXpo)($"Id = {MeasureUnitId.Value}")
                End If

                If _MeasureUnit IsNot Nothing Then
                    .MeasurementUnitAbbreviation = _MeasureUnit.Abbreviation
                    If unitDoseType.MSClass <> EUnitDoseTypeClass.ParenteralNutrition OrElse ComponentType = 5 Then
                        .QuantityMeasureunitname = $"{Quantity} {_MeasureUnit.Abbreviation}"
                        .Dosis = $"{Quantity} {_MeasureUnit.Abbreviation}"  ' Dosis para la rejilla
                    End If
                End If

                .Volume = Nothing
                .VolumeMeasureUnit = Nothing
                .VolumeMeasureUnitDescription = Nothing

                If INDlyItemVolume.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    Dim volumeMeasurementUnit = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MeasureUnitXpo)($"Id = {VolumeMeasureUnit}"))

                    .Volume = INDseVolume.EditValue
                    .VolumeMeasureUnit = VolumeMeasureUnit
                    .VolumeMeasureUnitDescription = INDsleVolumeMeasureUnit.Text

                    If atc?.FormulationType <> 1 AndAlso atc?.FormulationType <> 3 Then
                        .MeasurementUnitAbbreviation = volumeMeasurementUnit.Abbreviation
                    End If

                    If atc?.FormulationType = 3 Then
                        .VolumeMeasureUnitAbbreviation = volumeMeasurementUnit.Abbreviation
                    End If
                End If

                .Osmolarity = Me.Osmolarity
                .Density = Me.Density

                If {1, 4}.Contains(ComponentType) Then
                    .Thinner = If(Thinner Is Nothing, False, CType(Thinner, Boolean))
                    .Vehicle = If(Vehicle Is Nothing, False, CType(Vehicle, Boolean))
                    .MainMedicine = If(MainMedicine Is Nothing, False, CType(MainMedicine, Boolean))
                Else
                    .Thinner = False
                    .Vehicle = False
                    .MainMedicine = False
                End If

                ' Asignar ComplementaryMedicine si viene del Dashboard y es complementario
                If IsNewDetailComplementary AndAlso IsDashboardConfirmationUnitDose Then
                    .ComplementaryMedicine = True
                    .MainMedicine = False ' Asegurar que no sea principal
                    .Thinner = False
                    .Vehicle = False
                    .PreparationTypeName = "Medicamento Complementario"
                End If

                If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(unitDoseType.MSClass) And ComponentType = 1 Then
                    ' Solo marcar como MainMedicine si NO es complementario
                    If Not IsNewDetailComplementary Then
                        ' Para Citostático directo (no Dashboard): si ya existe vehículo, es un medicamento adicional
                        ' Marcarlo como ComplementaryMedicine (MainMedicine = False) para mantener consistencia en BD
                        If unitDoseType.MSClass = EUnitDoseTypeClass.Cytostatic AndAlso Not IsDashboardConfirmationUnitDose Then
                            Dim existeVehicleForComplementary = _listPackageDetailValidation?.Any(Function(x) x.Vehicle)
                            If existeVehicleForComplementary.GetValueOrDefault(False) Then
                                ' Es un medicamento adicional de intratecal
                                .MainMedicine = False
                                .ComplementaryMedicine = True
                                .Thinner = False
                                .Vehicle = False
                                .PreparationTypeName = "Medicamento Adicional"
                            Else
                                ' Es el primer medicamento principal
                                .MainMedicine = True
                                .Thinner = False
                                .Vehicle = False
                            End If
                        Else
                            ' Flujo normal (no Citostático directo)
                            .MainMedicine = True
                            .Thinner = False
                            .Vehicle = False
                        End If
                    End If
                    .Concentration = Nothing
                    .Dilution = Nothing
                    .AmountTime = Nothing
                    .TimeUnit = Nothing
                    .VolumeTotal = Nothing
                        PackageDetaiAntibioticEdit.Add(PackageDetailEdit)
                End If
            End With
        End If

        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(unitDoseType.MSClass) And ComponentType = 1 Then

            ' Para Citostático/intratecal: no agregar reconstituyente ni vehículo si ya existen o si estamos editando
            Dim existeThinner As Boolean = _listPackageDetailValidation?.Any(Function(x) x.Thinner)
            Dim existeVehicle As Boolean = _listPackageDetailValidation?.Any(Function(x) x.Vehicle)

            ' Para intratecal: verificar si hay múltiples medicamentos principales
            Dim countMainMedicinesForSkip = _listPackageDetailValidation?.Where(Function(x) x.MainMedicine.GetValueOrDefault(False) AndAlso x.ComponentType = 1).Count()
            Dim isIntrathecal As Boolean = (countMainMedicinesForSkip.GetValueOrDefault(0) > 1) OrElse (existeThinner OrElse existeVehicle)

            ' Saltar reconstituyente y vehículo si es Citostático intratecal o estamos editando un principal existente
            Dim skipReconstituyenteYVehicle As Boolean = (unitDoseType.MSClass = EUnitDoseTypeClass.Cytostatic AndAlso (isIntrathecal OrElse (_editModeForm AndAlso existeVehicle)))

            If ReconstituyenteId IsNot Nothing AndAlso Not skipReconstituyenteYVehicle Then
                PackageDetailEdit = New PackageDetail
                With PackageDetailEdit
                    Dim reconstitution = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {ReconstituyenteId}"))
                    .SourceCodeName = INDsleReconstitution.Text
                    .ComponentName = INDsleReconstitution.Text  ' Nombre del componente para la rejilla
                    .PreparationTypeName = "Reconstituyente"
                    .ComponentType = 1
                    .MainMedicine = False
                    .Vehicle = False
                    .Thinner = True
                    .PreparationType = PreparationType
                    .AtcId = ReconstituyenteId
                    .DCIName = reconstitution.DCI.Name
                    .Dilution = DilutionFactor
                    .Concentration = Concentration
                    .ConcentrationName = INDtxtConcentration.EditValue
                    .Quantity = Math.Round(VolumenThinner, 2)
                    .TimeUnit = TimeUnit
                    .AmountTime = AmountTime
                    .MeasurementUnitId = _MeasureUnitThinner.Id

                    .MeasurementUnitAbbreviation = _MeasureUnitThinner.Abbreviation
                    .QuantityMeasureunitname = $"{Math.Round(VolumenThinner, 2)} {_MeasureUnitThinner.Abbreviation}"
                    .Dosis = $"{Math.Round(VolumenThinner, 2)} {_MeasureUnitThinner.Abbreviation}"  ' Dosis para la rejilla

                    PackageDetaiAntibioticEdit.Add(PackageDetailEdit)
                End With
            End If

            If VehicleId IsNot Nothing AndAlso Not skipReconstituyenteYVehicle Then
                PackageDetailEdit = New PackageDetail
                With PackageDetailEdit
                    Dim vehicle = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {VehicleId}"))
                    .SourceCodeName = INDSleVehicleDilution.Text
                    .ComponentName = INDSleVehicleDilution.Text  ' Nombre del componente para la rejilla
                    .PreparationTypeName = "Vehiculo"
                    .ComponentType = 1
                    .MainMedicine = False
                    .Thinner = False
                    .Vehicle = True
                    .PreparationType = PreparationType
                    .AtcId = VehicleId
                    .DCIName = vehicle.DCI.Name
                    .Quantity = Math.Round(VolumenVehicle, 2)
                    .VolumeTotal = VolumenVehicleTotal
                    .Concentration = ConcentrationDilution
                    .ConcentrationName = INDConcentrationVehicle.EditValue
                    .AmountTime = AmountTimeDilution
                    .TimeUnit = TimeUnitDilution
                    .MeasurementUnitId = MeasureunitPrepared

                    .MeasurementUnitAbbreviation = _MeasureUnitVehicle.Abbreviation
                    .QuantityMeasureunitname = $"{Math.Round(VolumenVehicle, 2)} {_MeasureUnitVehicle.Abbreviation}"
                    .Dosis = $"{Math.Round(VolumenVehicle, 2)} {_MeasureUnitVehicle.Abbreviation}"  ' Dosis para la rejilla

                    PackageDetaiAntibioticEdit.Add(PackageDetailEdit)
                End With
            End If

            ' Para Citostático/intratecal: recalcular el vehículo existente cuando se agrega un nuevo medicamento principal
            If skipReconstituyenteYVehicle AndAlso unitDoseType.MSClass = EUnitDoseTypeClass.Cytostatic Then
                Await RecalculateVehicleForIntrathecalAsync(False, Nothing, Quantity)
            End If
        End If

        ' NOTA: Para modo edición de Citostático con múltiples principales (intratecal),
        ' el vehículo ya se calcula correctamente en CalculateVolumeVehicle considerando los otros principales.
        ' Por lo tanto, no es necesario recalcular aquí ya que el vehículo se guarda junto con el principal editado.
    End Function

    ''' <summary>
    ''' Recalcula el volumen del vehículo cuando se agregan medicamentos adicionales en Citostático/intratecal.
    ''' Considera el medicamento principal (MainMedicine = True) y los adicionales (ComplementaryMedicine = True).
    ''' Fórmula: Volumen Vehículo = Volumen Total Deseado - Σ(Volúmenes de medicamentos)
    ''' Volumen de cada medicamento = Dosis (mg) / Concentración (mg/ml)
    ''' </summary>
    ''' <param name="isEditMode">Indica si estamos en modo edición</param>
    ''' <param name="editingAtcId">AtcId del medicamento que se está editando (para excluirlo del cálculo)</param>
    ''' <param name="newQuantity">Nueva cantidad del medicamento que se está editando/agregando</param>
    Private Async Function RecalculateVehicleForIntrathecalAsync(Optional isEditMode As Boolean = False, Optional editingAtcId As Integer? = Nothing, Optional newQuantity As Decimal? = Nothing) As Task
        Try
            ' Obtener el vehículo existente
            Dim vehicleDetail = _listPackageDetailValidation?.FirstOrDefault(Function(x) x.Vehicle)
            If vehicleDetail Is Nothing Then Exit Function

            ' Obtener el volumen total deseado del vehículo
            Dim volumeTotalDeseado As Decimal = vehicleDetail.VolumeTotal.GetValueOrDefault(0D)
            If volumeTotalDeseado <= 0 Then Exit Function

            ' Calcular la suma de volúmenes de todos los medicamentos
            Dim sumaVolumenesMedicamentos As Decimal = 0D

            ' 1. Obtener el medicamento principal (MainMedicine = True)
            Dim mainMedicine = _listPackageDetailValidation?.FirstOrDefault(Function(x) x.MainMedicine.GetValueOrDefault(False) AndAlso x.ComponentType = 1)
            If mainMedicine IsNot Nothing Then
                ' Si estamos editando el principal, excluirlo del cálculo (usaremos el nuevo valor del formulario)
                If Not (isEditMode AndAlso editingAtcId.HasValue AndAlso mainMedicine.AtcId.HasValue AndAlso mainMedicine.AtcId.Value = editingAtcId.Value) Then
                    If mainMedicine.AtcId.HasValue Then
                        Dim atc = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {mainMedicine.AtcId.Value}"))
                        If atc IsNot Nothing AndAlso atc.Weight > 0 AndAlso atc.Volume > 0 Then
                            Dim concentracion As Decimal = atc.Weight / atc.Volume
                            Dim volumenMedicamento As Decimal = mainMedicine.Quantity.GetValueOrDefault(0D) / concentracion
                            sumaVolumenesMedicamentos += volumenMedicamento
                        ElseIf mainMedicine.Volume.GetValueOrDefault(0D) > 0 Then
                            sumaVolumenesMedicamentos += mainMedicine.Volume.Value
                        End If
                    End If
                End If
            End If

            ' 2. Obtener los medicamentos adicionales (ComplementaryMedicine = True, MainMedicine = False)
            Dim additionalMedicines = _listPackageDetailValidation?.Where(Function(x) _
                x.ComplementaryMedicine.GetValueOrDefault(False) AndAlso
                Not x.MainMedicine.GetValueOrDefault(False) AndAlso
                x.ComponentType = 1).ToList()

            If additionalMedicines IsNot Nothing AndAlso additionalMedicines.Any() Then
                For Each med In additionalMedicines
                    ' Si estamos editando este medicamento, excluirlo del cálculo
                    If isEditMode AndAlso editingAtcId.HasValue AndAlso med.AtcId.HasValue AndAlso med.AtcId.Value = editingAtcId.Value Then
                        Continue For
                    End If

                    If med.AtcId.HasValue Then
                        Dim atc = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {med.AtcId.Value}"))
                        If atc IsNot Nothing AndAlso atc.Weight > 0 AndAlso atc.Volume > 0 Then
                            Dim concentracion As Decimal = atc.Weight / atc.Volume
                            Dim volumenMedicamento As Decimal = med.Quantity.GetValueOrDefault(0D) / concentracion
                            sumaVolumenesMedicamentos += volumenMedicamento
                        ElseIf med.Volume.GetValueOrDefault(0D) > 0 Then
                            sumaVolumenesMedicamentos += med.Volume.Value
                        End If
                    End If
                Next
            End If

            ' 3. Agregar el volumen del medicamento que se está agregando o editando
            Dim cantidadActual As Decimal = If(newQuantity.HasValue, newQuantity.Value, Quantity)
            If _atc IsNot Nothing AndAlso _atc.Weight > 0 AndAlso _atc.Volume > 0 Then
                Dim concentracionNuevo As Decimal = _atc.Weight / _atc.Volume
                Dim volumenNuevoMedicamento As Decimal = cantidadActual / concentracionNuevo
                sumaVolumenesMedicamentos += volumenNuevoMedicamento
            ElseIf VolumenProduct > 0 Then
                sumaVolumenesMedicamentos += VolumenProduct
            End If

            ' Si la suma de volúmenes excede el volumen total, ajustar automáticamente
            If sumaVolumenesMedicamentos >= volumeTotalDeseado Then
                ' Agregar un margen del 20% al volumen total
                volumeTotalDeseado = Math.Ceiling(sumaVolumenesMedicamentos * 1.2D)
                vehicleDetail.VolumeTotal = volumeTotalDeseado
            End If

            ' Calcular el nuevo volumen del vehículo
            Dim nuevoVolumenVehiculo As Decimal = volumeTotalDeseado - sumaVolumenesMedicamentos
            If nuevoVolumenVehiculo < 0 Then nuevoVolumenVehiculo = 0

            ' Actualizar el vehículo (los cambios se reflejan automáticamente porque es una referencia)
            vehicleDetail.Quantity = Math.Round(nuevoVolumenVehiculo, 2)
            vehicleDetail.QuantityMeasureunitname = $"{Math.Round(nuevoVolumenVehiculo, 2)} {vehicleDetail.MeasurementUnitAbbreviation}"
            vehicleDetail.Dosis = $"{Math.Round(nuevoVolumenVehiculo, 2)} {vehicleDetail.MeasurementUnitAbbreviation}"
            vehicleDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified

            ' NOTA: No agregamos el vehículo a PackageDetaiAntibioticEdit porque ya existe en _listPackageDetailValidation
            ' y los cambios se reflejan automáticamente por ser una referencia al mismo objeto.
            ' FrmPackage actualizará la grilla con los valores modificados.

        Catch ex As Exception
            ' Log error pero no interrumpir el flujo
            Debug.WriteLine($"Error recalculando vehículo: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' Carga información de resumen para intratecal al editar un medicamento principal.
    ''' Muestra los valores de los demás medicamentos y el vehículo para referencia.
    ''' </summary>
    Private Async Function LoadIntrathecalSummaryInfoAsync() As Task
        Try
            ' Obtener el vehículo existente
            Dim vehicleDetail = _listPackageDetailValidation?.FirstOrDefault(Function(x) x.Vehicle)
            If vehicleDetail Is Nothing Then Exit Function

            ' Obtener todos los medicamentos principales (incluyendo el que estamos editando)
            Dim mainMedicines = _listPackageDetailValidation?.Where(Function(x) x.MainMedicine.GetValueOrDefault(False) AndAlso x.ComponentType = 1).ToList()
            If mainMedicines Is Nothing OrElse Not mainMedicines.Any() Then Exit Function

            ' Calcular la suma de volúmenes de los OTROS medicamentos principales (excluyendo el actual)
            Dim sumaVolumenesOtros As Decimal = 0D
            Dim sumaCantidadesOtros As Decimal = 0D
            Dim otrosMedicamentosInfo As New System.Text.StringBuilder()

            For Each med In mainMedicines
                ' Excluir el medicamento que estamos editando actualmente
                If med.AtcId.HasValue AndAlso AtcId.HasValue AndAlso med.AtcId.Value = AtcId.Value Then
                    Continue For
                End If

                ' Obtener el ATC para conocer la concentración
                If med.AtcId.HasValue Then
                    Dim atc = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {med.AtcId.Value}"))
                    If atc IsNot Nothing AndAlso atc.Weight > 0 AndAlso atc.Volume > 0 Then
                        Dim concentracion As Decimal = atc.Weight / atc.Volume
                        Dim volumenMedicamento As Decimal = med.Quantity.GetValueOrDefault(0D) / concentracion
                        sumaVolumenesOtros += volumenMedicamento
                        sumaCantidadesOtros += med.Quantity.GetValueOrDefault(0D)
                        otrosMedicamentosInfo.AppendLine($"• {med.SourceCodeName}: {med.Quantity} {med.MeasurementUnitAbbreviation} ({Math.Round(volumenMedicamento, 2)} ml)")
                    End If
                End If
            Next

            ' Mostrar información de referencia en el título del formulario o en algún control
            Dim volumeTotalDeseado As Decimal = vehicleDetail.VolumeTotal.GetValueOrDefault(0D)
            Dim volumenVehiculoActual As Decimal = vehicleDetail.Quantity.GetValueOrDefault(0D)

            ' Calcular volumen disponible para el medicamento actual
            Dim volumenDisponible As Decimal = volumeTotalDeseado - sumaVolumenesOtros - volumenVehiculoActual

            ' Actualizar el texto del formulario para mostrar información de referencia
            If otrosMedicamentosInfo.Length > 0 Then
                Me.Text = $"Editar Medicamento - Intratecal (Volumen Total: {volumeTotalDeseado} ml, Vehículo: {volumenVehiculoActual} ml)"
            End If

        Catch ex As Exception
            Debug.WriteLine($"Error cargando información de intratecal: {ex.Message}")
        End Try
    End Function

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
    ''' Metodo que inicializa las tuplas
    ''' </summary>
    Private Sub InitializationTuplas()

        ListYesNo = New List(Of Tuple(Of Boolean, String))()
        ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleThinner.Properties.DataSource = ListYesNo
        INDsleVehicle.Properties.DataSource = ListYesNo
        INDGleMainMedicine.Properties.DataSource = ListYesNo

        ListPreparationType = New List(Of Tuple(Of Byte, String))
        INDslePreparationType.Properties.DataSource = ListPreparationType

        ListTimeUnit = New List(Of Tuple(Of Byte, String))
        ListTimeUnit.Add(New Tuple(Of Byte, String)(1, "HORA(S)"))
        ListTimeUnit.Add(New Tuple(Of Byte, String)(2, "DIA(S)"))
        INDsleTimeUnit.Properties.DataSource = ListTimeUnit

        ListComponentType = New List(Of Tuple(Of Byte, String))()

        If Not _editModeForm And {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.Refilling}.Contains(unitDoseType.MSClass) Then
            If unitDoseType.MSClass = EUnitDoseTypeClass.Cytostatic Then
                Dim hasMedicineComponent As Boolean = ListPackageDetailValidation?.Any(Function(x) x.ComponentType = 1)

                If Not hasMedicineComponent Then
                    ListComponentType.Add(New Tuple(Of Byte, String)(1, "Medicamento"))

                Else
                    Dim hasAnyPreparationType As Boolean = ListPackageDetailValidation?.
                        Where(Function(x) x.ComponentType = 1).
                        Any(Function(x) x.PreparationType.HasValue AndAlso x.PreparationType <> 4)

                    If Not hasAnyPreparationType Then
                        ListComponentType.Add(New Tuple(Of Byte, String)(1, "Medicamento"))
                    End If
                End If
            ElseIf Not ListPackageDetailValidation.Any(Function(x) x.ComponentType = 1) Then
                ListComponentType.Add(New Tuple(Of Byte, String)(1, "Medicamento"))

            End If

        ElseIf unitDoseType.MSClass <> EUnitDoseTypeClass.ParenteralNutrition Then
            ListComponentType.Add(New Tuple(Of Byte, String)(1, "Medicamento"))
        End If

        If unitDoseType.MSClass <> EUnitDoseTypeClass.ParenteralNutrition Then
            ListComponentType.Add(New Tuple(Of Byte, String)(2, "Insumo"))

            If Not {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(unitDoseType.MSClass) Then
                ListComponentType.Add(New Tuple(Of Byte, String)(3, "Producto"))
            End If
        Else
            ListComponentType.Add(New Tuple(Of Byte, String)(4, "Medicamento")) 'Medicamento para NPT
            ListComponentType.Add(New Tuple(Of Byte, String)(5, "Insumo"))
        End If

        INDGleType.Properties.DataSource = ListComponentType
    End Sub

    ''' <summary>
    ''' Método que valida campos
    ''' </summary>
    Private Function ValidaCampos() As Boolean
        Dim errors As New StringBuilder
        _listPackageDetailNuevos = New List(Of PackageDetail)

        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(unitDoseType.MSClass) AndAlso _atc IsNot Nothing Then
            If PreparationType = 2 Then ' Dilución
                If VolumenVehicleTotal < VolumenProduct Then
                    errors.AppendLine("El valor del preparado no puede ser menor al valor del volumen del medicamento")
                End If
            End If

            If PreparationType = 3 Then 'Reconstitución + Dilución
                If VolumenVehicleTotal < VolumenThinner Then
                    errors.AppendLine("El valor del preparado no puede ser menor al valor del volumen del reconstituyente")
                End If

                If INDtxtConcentration.EditValue Is Nothing Or INDConcentrationVehicle.EditValue Is Nothing Then
                    errors.AppendLine("La Concentracion no puede estar vacia")
                End If
            End If

            If PreparationType = 1 Or PreparationType = 3 Then 'Reconstitución ó Reconstitución + Dilución
                If ReconstituyenteId Is Nothing Then
                    errors.AppendLine("Debe seleccionar un Reconstituyente")
                End If
            End If

            If PreparationType = 1 Then
                If INDtxtConcentration.EditValue Is Nothing Then
                    errors.AppendLine("La Concentracion no puede estar vacia")
                End If
            End If

            If PreparationType = 2 Or PreparationType = 3 Then 'Dilución ó Reconstitución + Dilución
                If INDSleVehicleDilution.EditValue Is Nothing Then
                    errors.AppendLine("Debe seleccionar un Vehiculo ")
                End If

                If TimeUnitDilution Is Nothing Then
                    errors.AppendLine("Se debe seleccionar unidad de tiempo para la preparación")
                End If

                If INDVolumeVehicle.EditValue Is Nothing OrElse INDVolumeVehicle.EditValue Is Nothing Then
                    errors.AppendLine("El Volumen no puede estar vacio")
                End If
            End If

            If unitDoseType?.MSClass = EUnitDoseTypeClass.Cytostatic AndAlso ComponentType = 1 AndAlso PreparationType Is Nothing Then
                errors.AppendLine("El Tipo de preparación no puede estar vacio")
            End If
        Else
                If Not ValidateControls() Then
                INDbtnAdd.Enabled = True
                Return False
            End If
            If INDseQuantity.EditValue = 0 Then
                errors.AppendLine("Campo " + INDlyItemQuantity.Text + ResourceManager.GetString("Empty"))
            End If
        End If

        'Se valida si el tipo de formula es diferente a Volumen y el usuario haya escogido vehiculo o diluyente
        If INDLciMedicine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If _atc.FormulationType <> 2 AndAlso (Thinner OrElse Vehicle) Then
                errors.AppendLine("No se puede marcar un medicamento como diluyente o vehículo si el tipo de fórmula del medicamento es diferente a volumen")
            End If
        End If

        If INDlygGeneralData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If Thinner AndAlso Vehicle AndAlso MainMedicine Then
                errors.AppendLine("No se puede marcar un medicamento como principal, diluyente y vehículo")
            End If

            If Not Thinner AndAlso Not Vehicle AndAlso Not MainMedicine AndAlso Not ComplementaryMedicine.GetValueOrDefault() Then
                errors.AppendLine("Debe seleccionar el tipo de componente")
            End If
        End If

        ' Validación para medicamento complementario
        If IsDashboardConfirmationUnitDose AndAlso ComplementaryMedicine.GetValueOrDefault(False) Then
            Dim mainMedicineDetail = ListPackageDetailValidation?.FirstOrDefault(Function(x) x.MainMedicine.GetValueOrDefault(False))

            If mainMedicineDetail IsNot Nothing Then
                ' Si estamos editando y la dosis solicitada es 0, saltamos validación de cantidad disponible
                ' ya que no se puede recalcular correctamente sin la dosis solicitada
                If _editModeForm AndAlso RequestedDosage = 0 Then
                    ' En modo edición sin dosis solicitada, solo validar que la cantidad sea positiva
                    If Quantity <= 0 Then
                        errors.AppendLine("La cantidad del medicamento complementario debe ser mayor a 0")
                    End If
                Else
                    ' Calcular la suma de cantidades de complementarios existentes
                    ' IMPORTANTE: Si estamos editando, excluir el complementario actual de la suma
                    Dim existingComplementarySum As Decimal = 0D
                    Dim existingComplementary = ListPackageDetailValidation?.Where(Function(x) x.ComplementaryMedicine.GetValueOrDefault(False))
                    If existingComplementary IsNot Nothing AndAlso existingComplementary.Any() Then
                        If _editModeForm AndAlso PackageDetailEdit IsNot Nothing Then
                            ' Excluir el complementario que estamos editando de la suma
                            ' Comparar por AtcId ya que pueden ser copias diferentes del mismo objeto
                            Dim currentAtcId = PackageDetailEdit.AtcId
                            existingComplementarySum = existingComplementary.
                                Where(Function(x) x.AtcId <> currentAtcId).
                                Sum(Function(x) x.Quantity.GetValueOrDefault(0D))
                        Else
                            existingComplementarySum = existingComplementary.Sum(Function(x) x.Quantity.GetValueOrDefault(0D))
                        End If
                    End If

                    ' La cantidad disponible se calcula basándose en la dosis solicitada, no en la cantidad actual del principal
                    ' Cantidad disponible = Dosis Solicitada - Suma de complementarios existentes (excluyendo el actual si es edición)
                    Dim availableQuantity As Decimal = RequestedDosage - existingComplementarySum

                    ' Validación: La cantidad del complementario no puede exceder la cantidad disponible
                    If Quantity > availableQuantity Then
                        errors.AppendLine($"La cantidad del medicamento complementario ({Quantity}) excede la cantidad disponible ({availableQuantity}). " &
                                         $"Dosis solicitada: {RequestedDosage}, Complementarios existentes: {existingComplementarySum}")
                    ElseIf Quantity > 0 AndAlso Not _editModeForm Then
                        ' Solo mostrar confirmación al crear nuevo, no al editar
                        ' Calcular el nuevo valor del principal después del ajuste
                        Dim newMainQuantity As Decimal = availableQuantity - Quantity

                        ' Preguntar al usuario si desea realizar el ajuste automático
                        Dim confirmMessage = $"Al agregar este medicamento complementario con cantidad {Quantity}, " & vbCrLf &
                                            $"el medicamento principal se ajustará automáticamente a {newMainQuantity}." & vbCrLf &
                                            $"El vehículo también será recalculado." & vbCrLf &
                                            $"¿Desea continuar?"

                        If MessageIndigo.Show(confirmMessage, MessageType.Question, Me.Text, Botones.SiNo) <> System.Windows.Forms.DialogResult.Yes Then
                            errors.AppendLine("Operación cancelada por el usuario")
                        End If
                    End If
                End If
            Else
                errors.AppendLine("No se encontró un medicamento principal para realizar el ajuste")
            End If
        End If

        If errors.Length = 0 Then
            'Se valida que si el tipo de dosis unitaria es NPT todos los medicamentos sean del mismo tipo de unidad de medida
            If ComponentType = 1 AndAlso unitDoseType?.MSClass = EUnitDoseTypeClass.ParenteralNutrition AndAlso ListPackageDetailValidation?.Any() Then
                If _editModeForm Then 'Si se esta editando
                    If (From x In ListPackageDetailValidation Where x.ComponentType = 1 AndAlso x.MeasurementUnitId IsNot Nothing _
                                                                  AndAlso x.MeasurementUnitId <> INDsleMeasurementUnit.EditValue _
                                                                  AndAlso Not x.Equals(PackageDetailEdit)).Count > 0 Then
                        errors.AppendLine("Las nutriciones parenterales no pueden tener componentes de medicamentos con diferentes unidades")
                    End If
                Else 'Si se esta agregando
                    If (From x In ListPackageDetailValidation Where x.ComponentType = 1 AndAlso x.MeasurementUnitId IsNot Nothing AndAlso x.MeasurementUnitId <> INDsleMeasurementUnit.EditValue).Count > 0 Then
                        errors.AppendLine("Las nutriciones parenterales no pueden tener componentes de medicamentos con diferentes unidades")
                    End If
                End If
            End If

            If (Not _editModeForm) AndAlso _listPackageDetailValidation.Any(Function(d) d.ComponentType = Me.ComponentType AndAlso
                                                                                        ((Me.ComponentType = 1 AndAlso d.AtcId = AtcId AndAlso (d.Thinner = Thinner OrElse d.Vehicle = Vehicle)) OrElse
                                                                                        (Me.ComponentType = 2 AndAlso d.SupplieId = SupplieId) OrElse
                                                                                        (Me.ComponentType = 3 AndAlso d.ProductId = ProductId))) Then
                errors.AppendLine("El paquete ya contiene este componente")
            Else
                Dim _componentId As Integer
                Select Case ComponentType
                    Case 1, 4
                        _componentId = AtcId
                    Case 2
                        _componentId = SupplieId
                    Case 3, 5
                        _componentId = ProductId
                End Select
                If errors.Length = 0 AndAlso Thinner Then
                    Dim _packageDetail = _listPackageDetailValidation.FirstOrDefault(Function(d) d.Thinner)
                    If (_packageDetail IsNot Nothing AndAlso _componentId <> _packageDetail.AtcId) Then
                        errors.AppendLine(String.Format("El siguiente componente esta marcado como diluyente {0}", _packageDetail.SourceCodeName))
                        If MessageIndigo.Show(String.Format("{0}, desea modificarlo?", errors.ToString),
                                                  MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            _packageDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                            _packageDetail.Thinner = False
                            _listPackageDetailNuevos.Add(_packageDetail)
                        Else
                            INDbtnAdd.Enabled = True
                            Return False
                        End If
                        errors.Clear()
                    End If
                End If
                If errors.Length = 0 AndAlso Vehicle Then
                    Dim _packageDetail = _listPackageDetailValidation.FirstOrDefault(Function(d) d.Vehicle)
                    If (_packageDetail IsNot Nothing AndAlso _componentId <> _packageDetail.AtcId) Then
                        errors.AppendLine(String.Format("El siguiente componente esta marcado como vehículo {0}", _packageDetail.SourceCodeName))
                        If MessageIndigo.Show(String.Format("{0}, desea modificarlo?", errors.ToString),
                                                  MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            _packageDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                            _packageDetail.Vehicle = False
                            _listPackageDetailNuevos.Add(_packageDetail)

                        Else
                            INDbtnAdd.Enabled = True
                            Return False
                        End If
                        errors.Clear()
                    End If
                End If
                If errors.Length = 0 AndAlso MainMedicine Then
                    ' Permitir múltiples medicamentos principales para Magistral y Citostático (intratecal)
                    If unitDoseType?.MSClass <> EUnitDoseTypeClass.Magistral AndAlso
                       unitDoseType?.MSClass <> EUnitDoseTypeClass.Cytostatic Then
                        Dim _packageDetail = _listPackageDetailValidation.FirstOrDefault(Function(d) d.MainMedicine)
                        If (_packageDetail IsNot Nothing AndAlso _componentId <> _packageDetail.AtcId) Then
                            errors.AppendLine(String.Format("El siguiente componente esta marcado como principal {0}", _packageDetail.SourceCodeName))
                            If MessageIndigo.Show(String.Format("{0}, desea modificarlo?", errors.ToString),
                                                  MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                                _packageDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                                _packageDetail.MainMedicine = False
                                _listPackageDetailNuevos.Add(_packageDetail)
                            Else
                                INDbtnAdd.Enabled = True
                                Return False
                            End If
                            errors.Clear()
                        End If
                    End If
                End If
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

    Private Sub LoadMeasurementUnit()
        If INDGleType.EditValue IsNot Nothing Then
            If INDLciMedicine.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If _atc IsNot Nothing Then
                    INDsleMeasurementUnit.Properties.DataSource = Presenter.InitializeMeasureUnitByType(_atc.FormulationType)
                End If
            Else
                If INDGleType.EditValue = 2 Then 'insumo
                    INDsleMeasurementUnit.Properties.DataSource = Presenter.InitializeMeasureUnitGeneric()
                Else
                    INDsleMeasurementUnit.Properties.DataSource = Presenter.InitializeMeasureUnitByType(Nothing)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que calcula la estabilidad del reconstituyente
    ''' </summary>
    Private Sub CalculateAmountTime(unitT As Byte, amountT As Integer)
        If unitT = 1 Then
            INDtxtStability.EditValue = $"{amountT} HORAS(S)"
        Else
            INDtxtStability.EditValue = $"{amountT} DIAS(S)"
        End If
    End Sub

    ''' <summary>
    ''' Método que calcula el volumen del vehiculo
    ''' Considera los medicamentos complementarios/adicionales existentes
    ''' </summary>
    Private Sub CalculateVolumeVehicle(volumeprepared As Decimal, volumeATC As Decimal)
        If VehicleId IsNot Nothing AndAlso volumeprepared > volumeATC And _MeasureUnitVehicle IsNot Nothing Then

            ' Calcular el volumen total de los medicamentos complementarios/adicionales existentes
            ' Esto aplica tanto para Dashboard como para intratecal directo
            ' Todos tienen MainMedicine = False y ComplementaryMedicine = True
            Dim complementaryVolumeSum As Decimal = 0D
            If _listPackageDetailValidation IsNot Nothing Then
                Dim complementaryMedicines = _listPackageDetailValidation.Where(Function(x) _
                    x.ComplementaryMedicine.GetValueOrDefault(False) AndAlso Not x.MainMedicine.GetValueOrDefault(False))
                If complementaryMedicines.Any() Then
                    complementaryVolumeSum = complementaryMedicines.Sum(Function(x) x.Volume.GetValueOrDefault(0D))
                End If
            End If

            ' Ya no necesitamos calcular "otros principales" porque los adicionales de intratecal
            ' ahora tienen MainMedicine = False y ComplementaryMedicine = True
            Dim otherMainMedicinesVolumeSum As Decimal = 0D

            If PreparationType = 2 Then
                VolumenVehicle = volumeprepared - volumeATC - complementaryVolumeSum - otherMainMedicinesVolumeSum
                If VolumenVehicle < 0 Then VolumenVehicle = 0
                INDVolumeVehicle.EditValue = $"{Math.Round(VolumenVehicle, 2)} {_MeasureUnitVehicle.Name}"
            End If

            If PreparationType = 3 AndAlso ReconstituyenteId IsNot Nothing Then
                VolumenVehicle = volumeprepared - volumeATC - complementaryVolumeSum - otherMainMedicinesVolumeSum
                If VolumenVehicle < 0 Then VolumenVehicle = 0
                INDVolumeVehicle.EditValue = $"{Math.Round(VolumenVehicle, 2)} {_MeasureUnitVehicle.Name}"
            End If
        Else
            INDVolumeVehicle.EditValue = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Método que calcula la concentracion
    ''' Considera los medicamentos complementarios/adicionales existentes
    ''' </summary>
    Private Sub CalculateConcentration(weightATC As Decimal, volumeTotal As Decimal)
        If volumeTotal <> 0 And _MeasureUnit IsNot Nothing And _MeasureUnitVehicle IsNot Nothing Then

            ' Calcular la suma de cantidades de los medicamentos complementarios/adicionales existentes
            ' Esto aplica tanto para Dashboard como para intratecal directo
            ' Todos tienen MainMedicine = False y ComplementaryMedicine = True
            Dim complementaryQuantitySum As Decimal = 0D
            If _listPackageDetailValidation IsNot Nothing Then
                Dim complementaryMedicines = _listPackageDetailValidation.Where(Function(x) _
                    x.ComplementaryMedicine.GetValueOrDefault(False) AndAlso Not x.MainMedicine.GetValueOrDefault(False))
                If complementaryMedicines.Any() Then
                    complementaryQuantitySum = complementaryMedicines.Sum(Function(x) x.Quantity.GetValueOrDefault(0D))
                End If
            End If

            ' Concentración = (Cantidad Principal + Cantidad Complementarios/Adicionales) / Volumen Total
            Dim totalWeight As Decimal = weightATC + complementaryQuantitySum
            ConcentrationDilution = totalWeight / volumeTotal
            ConcentrationDilution = CDec(Utils.SetPartDecimalToValue(ConcentrationDilution))

            INDConcentrationVehicle.EditValue = $"{ConcentrationDilution} {_MeasureUnit.Abbreviation} / {_MeasureUnitVehicle.Abbreviation}"
        Else
            INDConcentrationVehicle.EditValue = String.Empty
        End If
    End Sub

    ''' <summary>
    ''' Ajusta el medicamento principal y el vehículo cuando se agrega un medicamento complementario.
    ''' Reduce la cantidad del principal y recalcula el vehículo siguiendo la misma lógica del proceso normal.
    ''' </summary>
    ''' <param name="complementaryQuantity">Cantidad (peso) del medicamento complementario</param>
    ''' <param name="complementaryVolume">Volumen del medicamento complementario</param>
    ''' <returns>Lista de detalles modificados (principal, vehículo y reconstituyente si existe)</returns>
    Private Async Function AdjustMainMedicineForComplementaryAsync(complementaryQuantity As Decimal, complementaryVolume As Decimal) As Task(Of List(Of PackageDetail))
        Dim modifiedDetails As New List(Of PackageDetail)

        If _listPackageDetailValidation Is Nothing OrElse Not _listPackageDetailValidation.Any() Then
            Return modifiedDetails
        End If

        ' Buscar el medicamento principal
        Dim mainMedicine = _listPackageDetailValidation.FirstOrDefault(Function(x) x.MainMedicine.GetValueOrDefault(False))
        If mainMedicine Is Nothing Then
            Return modifiedDetails
        End If

        ' Calcular la suma de cantidades de complementarios existentes
        Dim existingComplementarySum As Decimal = 0D
        Dim existingComplementary = _listPackageDetailValidation.Where(Function(x) x.ComplementaryMedicine.GetValueOrDefault(False))
        If existingComplementary.Any() Then
            existingComplementarySum = existingComplementary.Sum(Function(x) x.Quantity.GetValueOrDefault(0D))
        End If

        ' Calcular la nueva cantidad del principal basándose en la dosis solicitada
        ' Nueva cantidad = Dosis solicitada - Complementarios existentes - Nuevo complementario
        Dim newMainQuantity As Decimal = RequestedDosage - existingComplementarySum - complementaryQuantity
        If newMainQuantity < 0 Then
            newMainQuantity = 0
        End If

        Dim ConcentrationATC = mainMedicine.ATC.Weight / mainMedicine.ATC.Volume
        Dim NewMainVolume = Decimal.Zero
        ' Ajustar el medicamento principal
        mainMedicine.Quantity = Math.Round(newMainQuantity, 2)
        NewMainVolume = newMainQuantity / ConcentrationATC
        mainMedicine.Volume = NewMainVolume

        mainMedicine.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified

        ' Actualizar la descripción de cantidad del principal
        If Not String.IsNullOrEmpty(mainMedicine.MeasurementUnitAbbreviation) Then
            mainMedicine.QuantityMeasureunitname = $"{Math.Round(newMainQuantity, 2)} {mainMedicine.MeasurementUnitAbbreviation}"
        End If

        modifiedDetails.Add(mainMedicine)

        ' Buscar el vehículo si existe
        Dim vehicleDetail = _listPackageDetailValidation.FirstOrDefault(Function(x) x.Vehicle)
        If vehicleDetail IsNot Nothing AndAlso vehicleDetail.AtcId.HasValue Then
            ' Obtener el ATC del vehículo
            Dim atcVehicle = Await Task.Run(Function() XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {vehicleDetail.AtcId.Value}"))

            ' VolumeTotal = Volumen del ATC del vehículo (volumen total del preparado)
            ' Si VolumeTotal es 0, usar el Volume del ATC del vehículo
            Dim volumeTotalPrepared As Decimal = vehicleDetail.VolumeTotal.GetValueOrDefault(0D)
            If volumeTotalPrepared = 0 AndAlso atcVehicle IsNot Nothing Then
                volumeTotalPrepared = atcVehicle.Volume
                ' Asignar VolumeTotal con el volumen del ATC del vehículo
                vehicleDetail.VolumeTotal = volumeTotalPrepared
            End If

            ' Calcular Quantity del vehículo usando Volume de los medicamentos:
            ' Quantity (Vehículo) = VolumeTotal - Volume(Principal) - Volume(Complementario)
            Dim totalMedicineVolume As Decimal = NewMainVolume + complementaryVolume
            Dim newVehicleQuantity As Decimal = volumeTotalPrepared - totalMedicineVolume

            If newVehicleQuantity < 0 Then
                newVehicleQuantity = 0
            End If

            ' Quantity = Volumen del vehículo que se usa
            vehicleDetail.Quantity = Math.Round(newVehicleQuantity, 2)
            vehicleDetail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified

            ' Completar campos de unidad de medida si están vacíos
            If atcVehicle IsNot Nothing Then
                If vehicleDetail.MeasurementUnitId Is Nothing AndAlso atcVehicle.VolumeMeasureUnit IsNot Nothing Then
                    vehicleDetail.MeasurementUnitId = atcVehicle.VolumeMeasureUnit.Id
                End If
                If String.IsNullOrEmpty(vehicleDetail.MeasurementUnitAbbreviation) AndAlso atcVehicle.VolumeMeasureUnit IsNot Nothing Then
                    vehicleDetail.MeasurementUnitAbbreviation = atcVehicle.VolumeMeasureUnit.Abbreviation
                End If
                If String.IsNullOrEmpty(vehicleDetail.MeasureUnitDescription) AndAlso atcVehicle.VolumeMeasureUnit IsNot Nothing Then
                    vehicleDetail.MeasureUnitDescription = atcVehicle.VolumeMeasureUnit.Name
                End If
            End If

            ' Actualizar la descripción de cantidad del vehículo
            If Not String.IsNullOrEmpty(vehicleDetail.MeasurementUnitAbbreviation) Then
                vehicleDetail.QuantityMeasureunitname = $"{Math.Round(newVehicleQuantity, 2)} {vehicleDetail.MeasurementUnitAbbreviation}"
            End If

            ' Recalcular la concentración
            ' Concentración = (CantidadPrincipal + CantidadComplementario) / VolumenTotalPreparado
            Dim totalQuantityMedicines As Decimal = newMainQuantity + complementaryQuantity
            If volumeTotalPrepared > 0 Then
                Dim newConcentration As Decimal = totalQuantityMedicines / volumeTotalPrepared
                newConcentration = CDec(Utils.SetPartDecimalToValue(newConcentration))
                vehicleDetail.Concentration = newConcentration

                ' Actualizar el nombre de concentración
                Dim mainMeasureUnit As String = mainMedicine.MeasurementUnitAbbreviation
                Dim vehicleMeasureUnit As String = vehicleDetail.MeasurementUnitAbbreviation
                If Not String.IsNullOrEmpty(mainMeasureUnit) AndAlso Not String.IsNullOrEmpty(vehicleMeasureUnit) Then
                    vehicleDetail.ConcentrationName = $"{Math.Round(newConcentration, 4)} {mainMeasureUnit} / {vehicleMeasureUnit}"
                End If
            End If

            modifiedDetails.Add(vehicleDetail)
        End If

        Return modifiedDetails
    End Function

    ''' <summary>
    ''' Método que Calcula el volumen a utilizar del atc
    ''' </summary>
    Private Sub CalculateVolumeATC()
        If _MeasureUnit IsNot Nothing Then
            Dim ConversionFactor As Decimal = Utils.MeasureUnitConvert(_atc.WeightMeasureUnit.Abbreviation, _MeasureUnit.Abbreviation)
            INDseVolume.EditValue = (Quantity * _atc.Volume) / (_atc.Weight * ConversionFactor)
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmPopupPackageDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.StatusRecordVisible = False

        Presenter = New PPackage()
        InitializationTuplas()
        CleanControlsAll()

        HideOrShowLayout(unitDoseType.MSClass)

        ' Inicializar control de medicamento complementario si viene del Dashboard
        InitializeComplementaryMedicineControl()

        If _editModeForm Then
            Await LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Inicializa el control de medicamento según el tipo determinado automáticamente
    ''' cuando viene del Dashboard de Confirmación de Dosis Unitarias
    ''' </summary>
    Private Sub InitializeComplementaryMedicineControl()
        If Not IsDashboardConfirmationUnitDose Then
            ' Si no viene del Dashboard, mantener texto original
            INDLciMedicine.Text = "Medicamento"
            Return
        End If

        If IsNewDetailComplementary Then
            ' CASO 1: Ya existe un medicamento principal → nuevo detalle es COMPLEMENTARIO

            ' Asegurar que "Medicamento" esté en el DataSource (para complementario siempre debe estar)
            Dim listComponentTypeTmp = CType(INDGleType.Properties.DataSource, List(Of Tuple(Of Byte, String)))
            If listComponentTypeTmp IsNot Nothing AndAlso Not listComponentTypeTmp.Any(Function(x) x.Item1 = 1) Then
                listComponentTypeTmp.Insert(0, New Tuple(Of Byte, String)(1, "Medicamento"))
                INDGleType.Properties.DataSource = listComponentTypeTmp
            End If

            ' Asignar el tipo de componente a Medicamento (1) - NO hacer ReadOnly para permitir cambiar a Insumo
            ComponentType = CByte(1)
            INDGleType.Refresh()

            ' Configurar automáticamente como medicamento complementario
            ComplementaryMedicine = True
            INDLciMedicine.Text = "Medicamento Complementario"

            ' Mostrar el campo de medicamento y los controles relacionados
            INDLciMedicine.ShowLayout()
            ShowControlsForComponentType(CByte(1))

            ' Ocultar opciones que no aplican para complementario
            INDlygGeneralData.HideControl()
            INDlyItemPreparationType?.HideControl() ' Complementario no tiene tipo de preparación

        ElseIf IsNewDetailMainMedicine Then
            ' CASO 2: No existe medicamento principal → nuevo detalle es PRINCIPAL
            ' Cambiar texto para medicamento principal
            INDLciMedicine.Text = "Medicamento Principal"
            INDLciMedicine.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0) ' Verde distintivo
            INDLciMedicine.AppearanceItemCaption.Options.UseForeColor = True

            ' Configurar automáticamente como medicamento principal
            If INDGleMainMedicine IsNot Nothing Then
                MainMedicine = True
                INDGleMainMedicine.Properties.ReadOnly = True ' Forzar como principal
                INDLciMainMedicine?.ShowLayout()
            End If

            ' Mostrar tipo de preparación para medicamento principal
            INDlyItemPreparationType?.ShowLayout()

        Else
            ' Flujo normal (sin determinación automática)
            INDLciMedicine.Text = "Medicamento"
        End If
    End Sub

    ''' <summary>
    ''' Carga los datos de un medicamento complementario/adicional para edición.
    ''' El complementario se edita solo, sin la estructura del medicamento principal.
    ''' Funciona tanto para Dashboard (complementario) como para intratecal directo (adicional).
    ''' Ambos casos tienen MainMedicine = False y ComplementaryMedicine = True.
    ''' </summary>
    Private Sub LoadComplementaryMedicineForEdit()
        If PackageDetailEdit Is Nothing Then Return

        With PackageDetailEdit
            ' Configurar tipo de componente
            ComponentType = .ComponentType
            INDGleType.EditValue = .ComponentType
            INDGleType.Properties.ReadOnly = True

            ' Cargar datos del medicamento
            AtcId = .AtcId
            INDSleMedicine.Properties.NullText = .SourceCodeName
            INDSleMedicine.Properties.ReadOnly = True

            ' Cargar cantidad y unidad de medida
            Quantity = .Quantity.GetValueOrDefault(0D)
            MeasureUnitId = .MeasurementUnitId
            INDsleMeasurementUnit.Properties.NullText = .MeasureUnitDescription

            ' Cargar volumen si existe
            If .Volume.GetValueOrDefault(0D) > 0 Then
                VolumenProduct = .Volume.GetValueOrDefault(0D)
                VolumeMeasureUnit = .VolumeMeasureUnit
                INDsleVolumeMeasureUnit.Properties.NullText = .VolumeMeasureUnitDescription
                ' Mostrar controles de volumen para Peso-Volumen
                INDlyItemVolume?.ShowLayout()
                INDlyItemVolumeMeasureUnit?.ShowLayout()
            End If

            PreparationType = .PreparationType

            ' Configurar como complementario (MainMedicine = False, ComplementaryMedicine = True)
            ComplementaryMedicine = True
            MainMedicine = False
            Thinner = False
            Vehicle = False

            ' Determinar el título según el contexto
            If IsDashboardConfirmationUnitDose Then
                INDLciMedicine.Text = "Medicamento Complementario"
            Else
                ' Intratecal directo o cualquier otro flujo
                INDLciMedicine.Text = "Medicamento Adicional"
            End If
            INDLciMedicine.ShowLayout()

            ' Ocultar TODOS los grupos y opciones que no aplican
            INDlygGeneralData?.HideControl()           ' Grupo de datos generales (diluyente, vehículo, principal)
            INDLciMainMedicine?.HideControl()          ' Opción medicamento principal
            INDlyItemThinner?.HideControl()            ' Opción diluyente
            INDlyItemVehicle?.HideControl()            ' Opción vehículo
            INDlyItemPreparationType?.HideControl()    ' Tipo de preparación

            ' Ocultar controles de reconstitución y dilución
            INDlygReconstitution?.HideControl()
            INDlygDilution?.HideControl()
            INDlygDilutionStability?.HideControl()

            'Si el detalle viene creado desde el dashboard confirmación de dosis unitarias
            If .IsDashboardConfirmationUnitDose Then
                ReadOnlyControlsDashboardConfirmationUnitDose(True, .SourceType)
            End If
        End With
    End Sub

    ''' <summary>
    ''' Muestra/oculta controles y cambia estado readonly según el tipo de clase de dosis.
    ''' </summary>
    Private Sub HideOrShowLayout(ByVal _MsClass As Integer)
        Select Case _MsClass
            Case EUnitDoseTypeClass.ParenteralNutrition

                INDlygGeneralData.HideControl()
                INDseQuantity.ReadOnly = True
                INDsleMeasurementUnit.ReadOnly = True
                INDseVolume.Properties.ReadOnly = True
                INDsleVolumeMeasureUnit.ReadOnly = True

            Case EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic

                INDlygGeneralData.HideControl()
                Dim PreparationTypeTmp = ListPackageDetailValidation?.FirstOrDefault(Function(x) x.PreparationType.HasValue And x.PreparationType = 4)
                If _MsClass <> EUnitDoseTypeClass.Cytostatic AndAlso PreparationTypeTmp?.PreparationType = 4 And Not _editModeForm Then
                    ' Determinar el tipo de componente apropiado
                    Dim targetComponentType As Byte = If(ListPackageDetailValidation?.Exists(Function(x) x.ComponentType = 1), CByte(2), CByte(1))
                    ComponentType = targetComponentType
                    ShowControlsForComponentType(targetComponentType)
                    INDGleType.ReadOnly = True
                End If

                ' Para Citostático/intratecal: si ya existe un medicamento principal con vehículo,
                ' ocultar tipo de preparación SOLO al agregar un nuevo medicamento (no al editar)
                If _MsClass = EUnitDoseTypeClass.Cytostatic AndAlso Not _editModeForm Then
                    Dim existeMainMedicine = ListPackageDetailValidation?.Any(Function(x) x.MainMedicine.GetValueOrDefault(False))
                    Dim existeVehicle = ListPackageDetailValidation?.Any(Function(x) x.Vehicle)

                    ' En modo agregar: ocultar si ya existe principal con vehículo
                    If existeMainMedicine.GetValueOrDefault(False) AndAlso existeVehicle.GetValueOrDefault(False) Then
                        ' Ocultar tipo de preparación
                        INDlyItemPreparationType?.HideControl()
                        ' Ocultar controles de reconstitución y dilución
                        INDlygReconstitution?.HideControl()
                        INDlygDilution?.HideControl()
                        INDlygDilutionStability?.HideControl()
                    End If
                End If

            Case EUnitDoseTypeClass.Refilling
                INDlyItemPreparationType.HideControl()
                If Not _editModeForm Then
                    If Not ListPackageDetailValidation?.Exists(Function(x) x.ComponentType = 1) Then

                        Thinner = False
                        INDsleThinner.Properties.ReadOnly = True

                        Vehicle = False
                        INDsleVehicle.Properties.ReadOnly = True

                        MainMedicine = True
                        INDGleMainMedicine.Properties.ReadOnly = True
                    Else
                        ComponentType = CByte(2)
                        ShowControlsForComponentType(CByte(2))
                    End If
                Else
                    INDGleMainMedicine.Properties.ReadOnly = True
                    INDsleThinner.Properties.ReadOnly = True
                    INDsleVehicle.Properties.ReadOnly = True
                End If
            Case Else
                INDlyItemPreparationType.HideControl()
                INDlygGeneralData.Enabled = True

                If _MsClass = EUnitDoseTypeClass.Magistral Then
                    INDLciMainMedicine.ShowLayout()
                    INDlyItemVehicle.ShowLayout()
                    Thinner = False
                    INDlyItemThinner.HideControl()
                End If
        End Select
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupPackageDetail_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega el producto al listado del paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        Try
            AsyncLoader(True)
            INDbtnAdd.Enabled = False
            If Not ValidaCampos() Then
                Exit Sub
            End If

            PackageDetaiAntibioticEdit.RemoveRange(0, PackageDetaiAntibioticEdit.Count)
            Await AddInventoryProduct()

            Dim args As New AddProductPackageDetailEventArgs

            ' Si es medicamento complementario, ajustar el medicamento principal y vehículo automáticamente
            Dim modifiedDetails As List(Of PackageDetail) = Nothing
            Dim calculatedConcentrationWeightVolumen As Decimal = 0D

            If IsDashboardConfirmationUnitDose AndAlso ComplementaryMedicine.GetValueOrDefault(False) Then
                Dim complementaryQuantity As Decimal = Quantity ' Cantidad (peso) del complementario
                Dim complementaryVolume As Decimal = VolumenProduct ' Volumen del complementario
                modifiedDetails = Await AdjustMainMedicineForComplementaryAsync(complementaryQuantity, complementaryVolume)
                args.ModifiedPackageDetails = modifiedDetails
                args.ComplementaryMedicineAdded = True

                ' Actualizar VolumenTotalVehicle y ConcentrationWeightVolumen desde los detalles modificados
                If modifiedDetails IsNot Nothing AndAlso modifiedDetails.Any() Then
                    Dim vehicleModified = modifiedDetails.FirstOrDefault(Function(x) x.Vehicle)
                    Dim mainMedicineModified = modifiedDetails.FirstOrDefault(Function(x) x.MainMedicine.GetValueOrDefault(False))

                    If vehicleModified IsNot Nothing Then
                        ' Volumen total del preparado = VolumeTotal del vehículo
                        INDVolumenTotalVehicle.EditValue = vehicleModified.VolumeTotal.GetValueOrDefault(0D)

                        ' Calcular la concentración para tipo Peso-Volumen
                        If mainMedicineModified IsNot Nothing AndAlso vehicleModified.VolumeTotal.GetValueOrDefault(0D) > 0 Then
                            ' Concentración = (Cantidad Principal + Cantidad Complementario) / VolumeTotal
                            Dim totalMedicineQuantity As Decimal = mainMedicineModified.Quantity.GetValueOrDefault(0D) + complementaryQuantity
                            Dim concentration As Decimal = totalMedicineQuantity / vehicleModified.VolumeTotal.Value
                            calculatedConcentrationWeightVolumen = CDec(Utils.SetPartDecimalToValue(concentration))
                        End If
                    End If
                End If

            ElseIf Not IsDashboardConfirmationUnitDose AndAlso _editModeForm AndAlso unitDoseType?.MSClass = EUnitDoseTypeClass.Cytostatic Then
                ' Para intratecal directo: verificar si hay medicamentos adicionales y recalcular vehículo
                ' Los adicionales tienen MainMedicine = False y ComplementaryMedicine = True
                Dim hasAdditionalMedicines = _listPackageDetailValidation?.Any(Function(x) _
                    x.ComplementaryMedicine.GetValueOrDefault(False) AndAlso
                    Not x.MainMedicine.GetValueOrDefault(False))

                If hasAdditionalMedicines.GetValueOrDefault(False) Then
                    ' Recalcular el vehículo considerando todos los medicamentos
                    Await RecalculateVehicleForIntrathecalAsync(True, AtcId, Quantity)

                    ' Obtener el vehículo actualizado para pasar los cambios a FrmPackage
                    Dim vehicleDetail = _listPackageDetailValidation?.FirstOrDefault(Function(x) x.Vehicle)
                    If vehicleDetail IsNot Nothing Then
                        modifiedDetails = New List(Of PackageDetail)()
                        modifiedDetails.Add(vehicleDetail)
                        args.ModifiedPackageDetails = modifiedDetails
                    End If
                End If
            End If

            If PackageDetaiAntibioticEdit.Any() Then
                args.ItemsPackageDetailAntibiotic = PackageDetaiAntibioticEdit
            Else
                args.ItemPackageDetail = PackageDetailEdit
            End If
            args.ListPackageDetail = _listPackageDetailNuevos
            args.EditMode = _editModeForm
            args.FormulationType = FormulationType
            args.VolumenTotalVehicle = INDVolumenTotalVehicle.EditValue

            ' Usar el valor calculado para complementario, o el método original para otros casos
            If calculatedConcentrationWeightVolumen > 0 Then
                args.ConcentrationWeightVolumen = calculatedConcentrationWeightVolumen
            Else
                args.ConcentrationWeightVolumen = GetConcentrationWeightVolumen()
            End If
            RaiseEvent AddPackageDetail(Nothing, args)
            INDbtnAdd.Enabled = True
            ComponentType = Nothing
            DialogResult = Windows.Forms.DialogResult.OK

        Catch ex As Exception
            INDbtnAdd.Enabled = True
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Calcula la concentracion del medicamento cuando sea de tipo Peso - volumen
    ''' </summary>
    ''' <returns></returns>
    Private Function GetConcentrationWeightVolumen()
        If FormulationType = 3 And PreparationType <> 4 Then
            If VolumenVehicle = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe indicar un vehículo para la dilución"
                Return Decimal.Zero
            End If

            Dim Quantity As Decimal = CDec(If(INDseQuantity.EditValue, 0)) ' Peso del medicamento
            Dim Volumen As Decimal = CDec(If(INDseVolume.EditValue, 0)) ' Volumen del medicamento
            Dim VolumenThinner As Decimal = VolumenVehicle '' Volumen del diluyente

            If (Volumen + VolumenThinner) = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La sumatoria del volumen del medicamento y del diluyente no puede ser cero"
                Return Decimal.Zero
            End If

            Dim Concentration As Decimal = Quantity / (Volumen + VolumenThinner)
            Return Concentration
        End If

        Return Decimal.Zero
    End Function

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmPopupPackageDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Datasource unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleVolumeMeasureUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleVolumeMeasureUnit.QueryPopUp
        If INDsleVolumeMeasureUnit.Properties.DataSource Is Nothing Then
            INDsleVolumeMeasureUnit.Properties.DataSource = Presenter.InitializeMeasureUnitVolumen()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleManufacturer control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    ''' INDSleMeasurementUnit
    Private Sub INDsleMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasurementUnit.QueryPopUp
        If INDsleMeasurementUnit.Properties.DataSource Is Nothing Then
            LoadMeasurementUnit()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the NDsleMeasurementVehicle control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    ''' INDsleUnitMeasurementVehicle
    Private Sub INDsleUnitMeasurementVehicle_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUnitMeasurementVehicle.QueryPopUp
        If INDsleUnitMeasurementVehicle.Properties.DataSource Is Nothing Then
            If _atcVehicle IsNot Nothing Then
                INDsleUnitMeasurementVehicle.Properties.DataSource = Presenter.InitializeMeasureUnitByType(2)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMedicine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleMedicine.QueryPopUp
        If INDSleMedicine.Properties.DataSource IsNot Nothing Then Return

        Dim filter As String = Nothing

        ' Si es medicamento complementario, filtrar por mismo ATC y forma farmacéutica del principal
        If IsNewDetailComplementary AndAlso MainMedicineAtcId.HasValue Then
            Dim mainAtc = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ATCXpo)($"Id = {MainMedicineAtcId.Value}")

            If mainAtc IsNot Nothing Then
                ' Filtrar por misma forma farmacéutica, misma entidad de medicamento y excluir el medicamento principal
                filter = $"PharmaceuticalFormId = {mainAtc.PharmaceuticalFormId} AND Id <> {MainMedicineAtcId.Value} AND ATCEntityId.Id = {mainAtc.ATCEntityId.Id}"

                ' Si tiene tipo de formulación, también filtrar por él
                If MainMedicineFormulationType.HasValue Then
                    filter &= $" AND FormulationType = {MainMedicineFormulationType.Value}"
                End If
            End If
        Else
            ' Comportamiento normal según el tipo de dosis unitaria
            Select Case unitDoseType?.MSClass
                Case EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic
                    filter = "FormulationType = 1 OR FormulationType = 3"

                    If unitDoseType?.MSClass = EUnitDoseTypeClass.Cytostatic AndAlso Not _editModeForm Then
                        Dim hasMedicineComponent As Boolean = ListPackageDetailValidation?.Any(Function(x) x.ComponentType = 1)

                        If hasMedicineComponent Then
                            Dim hasPreparationTypeNone As Boolean = ListPackageDetailValidation?.
                                Where(Function(x) x.ComponentType = 1).
                                Any(Function(x) x.PreparationType.HasValue AndAlso x.PreparationType = 4)

                            If hasPreparationTypeNone Then
                                filter = "FormulationType = 3"
                            End If
                        End If
                    End If

                Case EUnitDoseTypeClass.Refilling
                    filter = "FormulationType = 3"

                Case EUnitDoseTypeClass.ParenteralNutrition
                    filter = "DiluentProduct = 1"

            End Select
        End If

        INDSleMedicine.Properties.DataSource = If(filter IsNot Nothing,
                                                XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListATCbyFilter(filter),
                                                XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListATC())
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de reconstituyente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReconstitution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleReconstitution.QueryPopUp
        If INDsleReconstitution.Properties.DataSource Is Nothing AndAlso _atc IsNot Nothing Then
            INDsleReconstitution.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).MixingStationService.ListDilutionDetail(_atc.Id)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de Dilucion
    ''' Obtiene los diluyentes  de la tabla de estabilidad segun el medicamento  enviado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleVehicleDilution_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleVehicleDilution.QueryPopUp
        ' Evita cargar los datos si ya existen
        If INDSleVehicleDilution.Properties.DataSource IsNot Nothing Then
            Return
        End If

        If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.Cytostatic, EUnitDoseTypeClass.OtherSterile}.Contains(unitDoseType.MSClass) Then
            LoadStabilityTableData()
        Else
            INDSleVehicleDilution.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListATCbyFilter("DiluentProduct = 1")
        End If
    End Sub

    ''' <summary>
    ''' Evento que lee los vehiculos para la tabla de estabilidad del medicamento principal
    ''' </summary>
    Private Sub LoadStabilityTableData()
        Dim stabilityTable As StabilityTableDetailXpo = XpoServiceEx.Instance(indigo.TransactionalContainer) _
        .MixingStationService.ListStabilityTableDetailDilutionByAtcId(_atc.Id)

        If stabilityTable Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = $"El medicamento {_atc.CodeName} no posee tabla de estabilidad."
            Return
        End If

        Dim atcList As List(Of MixingStationATCXpo) = stabilityTable.StabilityTableDetailDilutionXpo _
            .Select(Function(d) d.ATCId) _
            .Distinct() _
            .ToList()

        If atcList IsNot Nothing AndAlso atcList.Any() Then
            INDSleVehicleDilution.Properties.DataSource = atcList
        Else
            Mensaje(EeventViewerImages.Informacion) = $"No se encontraron vehículos para la tabla de estabilidad de {_atc.CodeName}."
            Return
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de insumos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupplie_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleSupplie.QueryPopUp
        If INDSleSupplie.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDSleSupplie.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListInventorySupplie)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            Using model As New MPackage(Tag)
                If unitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition And ComponentType = 5 Then
                    INDSleProduct.Properties.DataSource = model.ListSuppliesForNpt()
                Else
                    INDSleProduct.Properties.DataSource = model.ListInventoryProductByProductType(4)
                End If
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de componente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
        ' Limpiar controles relacionados
        CleanControls()
        CleanReconstitutionControls()
        CleanDilutionControls()
        HideControlsAddComponent()

        ' Mostrar controles apropiados según el tipo de componente
        ShowControlsForComponentType(ComponentType)

        ' Aplicar configuraciones específicas según la clase de dosis unitaria
        HideOrShowLayout(unitDoseType.MSClass)

        ' Si viene del Dashboard y cambia a Medicamento (1), aplicar validaciones de complementario
        If IsDashboardConfirmationUnitDose AndAlso ComponentType = CByte(1) AndAlso IsNewDetailComplementary Then
            ' Configurar como medicamento complementario
            ComplementaryMedicine = True
            INDLciMedicine.Text = "Medicamento Complementario"

            ' Asegurar que el campo de medicamento esté visible
            INDLciMedicine.ShowLayout()

            ' Limpiar DataSource para que se recargue con filtro en QueryPopUp
            INDSleMedicine.Properties.DataSource = Nothing

            ' Ocultar opciones que no aplican para complementario
            INDLciMainMedicine?.HideControl()
            INDlyItemThinner?.HideControl()
            INDlyItemVehicle?.HideControl()
            INDlyItemPreparationType?.HideControl() ' Complementario no tiene tipo de preparación

        ElseIf IsDashboardConfirmationUnitDose AndAlso ComponentType = CByte(1) AndAlso IsNewDetailMainMedicine Then
            ' Si no hay medicamento principal, el nuevo es principal
            ComplementaryMedicine = False
            INDLciMedicine.Text = "Medicamento Principal"
            INDLciMedicine.AppearanceItemCaption.ForeColor = System.Drawing.Color.FromArgb(0, 128, 0)
            INDLciMedicine.AppearanceItemCaption.Options.UseForeColor = True

            ' Asegurar que el campo de medicamento esté visible
            INDLciMedicine.ShowLayout()

            ' Limpiar DataSource para que se recargue sin filtro en QueryPopUp
            INDSleMedicine.Properties.DataSource = Nothing

            ' Mostrar opciones de principal
            INDLciMainMedicine?.ShowLayout()
            MainMedicine = True
            If INDGleMainMedicine IsNot Nothing Then
                INDGleMainMedicine.Properties.ReadOnly = True
            End If

        ElseIf ComponentType <> CByte(1) Then
            ' Si cambia a otro tipo (Insumo), resetear configuración de complementario
            ComplementaryMedicine = False
            INDLciMedicine.Text = "Medicamento"
            INDLciMedicine.AppearanceItemCaption.Options.UseForeColor = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control Tipo de preparacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePreparationType_EditvalueChanged(sender As Object, e As EventArgs) Handles INDslePreparationType.EditValueChanged
        CleanReconstitutionControls()
        CleanDilutionControls()

        If PreparationType Is Nothing Then Return

        ' Oculta todos los controles por defecto
        INDlygGeneralData.HideControl()
        INDlygReconstitution.HideControl()
        INDlygDilution.HideControl()
        INDlygDilutionStability.HideControl()

        Select Case PreparationType
            Case 0 ' No aplica
                INDlygGeneralData.HideControl(False)
            Case 1 ' Reconstitución
                INDlygReconstitution.HideControl(False)
                SetReconstituyenteDefault()

            Case 2 ' Dilución
                INDlygDilution.HideControl(False)
                INDlygDilutionStability.HideControl(False)

            Case 3 ' Reconstitución - Dilución
                INDlygReconstitution.HideControl(False)
                INDlygDilution.HideControl(False)
                INDlygDilutionStability.HideControl(False)
                SetReconstituyenteDefault()

            Case 4 ' Ninguna
                'Ya están ocultos todos
        End Select
    End Sub

    ''' <summary>
    ''' Asigna el reconstituyente por defecto, si está disponible.
    ''' </summary>
    Private Sub SetReconstituyenteDefault()
        If _dilution?.DilutionFactorsDetailXpo IsNot Nothing Then
            Dim defaultFactor = _dilution.DilutionFactorsDetailXpo.FirstOrDefault(Function(x) x.ByDefault)
            If defaultFactor IsNot Nothing Then
                ReconstituyenteId = defaultFactor.AtcId
                INDsleReconstitution.Properties.NullText = defaultFactor.ATC?.CodeName
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de Reconstitucion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleReconstitution_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleReconstitution.EditValueChanged
        If ReconstituyenteId IsNot Nothing Then
            _dilutionDetail = _dilution?.DilutionFactorsDetailXpo?.FirstOrDefault(Function(x) x.AtcId = ReconstituyenteId)

            If _dilutionDetail IsNot Nothing Then
                _MeasureUnitThinner = _dilutionDetail.VolumeMeasureUnit
                Me.DilutionFactor = Utils.SetPartDecimalToValue(_dilutionDetail.Dilution)
                Me.TimeUnit = _dilutionDetail.TimeUnit
                Me.AmountTime = _dilutionDetail.AmountTime
                Me.Concentration = Utils.SetPartDecimalToValue(_dilutionDetail.Concentration)
                Me.VolumenThinner = _dilutionDetail.Volume

                CalculateAmountTime(TimeUnit, AmountTime)
                INDtxtUnitMeasurement.EditValue = _dilutionDetail.VolumeMeasureUnit.Name
                INDtxtConcentration.EditValue = $"{Concentration} {_MeasureUnit.Abbreviation} / {_MeasureUnitThinner.Abbreviation}"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de Vehiculo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleVehicleDilution_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleVehicleDilution.EditValueChanged
        If VehicleId IsNot Nothing Then
            Dim _presenter As New PInventoryProduct
            _atcVehicle = Await _presenter.GetMedicamentById(VehicleId)
            If _atcVehicle IsNot Nothing Then
                CalculateConcentration(Quantity, VolumenVehicleTotal)

                If PreparationType = 3 Then
                    CalculateVolumeVehicle(VolumenVehicleTotal, VolumenThinner)
                    MeasureunitPrepared = _atcVehicle.VolumeMeasureUnit?.Id
                    INDsleUnitMeasurementVehicle.Properties.NullText = _atcVehicle.VolumeMeasureUnit?.Name

                ElseIf PreparationType = 2 Then
                    CalculateVolumeVehicle(VolumenVehicleTotal, VolumenProduct)
                    MeasureunitPrepared = _atcVehicle.VolumeMeasureUnit?.Id
                    INDsleUnitMeasurementVehicle.Properties.NullText = _atcVehicle.VolumeMeasureUnit?.Name
                End If
            End If
            _presenter = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad de medida del preparado del vehiculo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitMeasurementVehicle_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUnitMeasurementVehicle.EditValueChanged
        If VehicleId IsNot Nothing And MeasureunitPrepared IsNot Nothing Then
            _MeasureUnitVehicle = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MeasureUnitXpo)($"Id = {MeasureunitPrepared}")

            If PreparationType = 3 Then
                CalculateVolumeVehicle(VolumenVehicleTotal, VolumenThinner)
                INDsleUnitMeasurementVehicle.Properties.NullText = _MeasureUnitVehicle.Name

            ElseIf PreparationType = 2 Then
                CalculateVolumeVehicle(VolumenVehicleTotal, VolumenProduct)
                INDsleUnitMeasurementVehicle.Properties.NullText = _MeasureUnitVehicle.Name
            End If

            If VolumenVehicleTotal > 0 And _MeasureUnitVehicle IsNot Nothing And _MeasureUnit IsNot Nothing Then
                CalculateConcentration(Quantity, VolumenVehicleTotal)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad de medida del medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMeasurementUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMeasurementUnit.EditValueChanged
        If MeasureUnitId IsNot Nothing Then
            _MeasureUnit = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MeasureUnitXpo)($"Id = {MeasureUnitId}")

            If PreparationType = 1 OrElse PreparationType = 3 Then
                If _MeasureUnitThinner IsNot Nothing AndAlso _MeasureUnit IsNot Nothing Then
                    INDtxtConcentration.EditValue = $"{Concentration} {_MeasureUnit.Abbreviation} / {_MeasureUnitThinner.Abbreviation}"
                End If
            End If

            If VolumenVehicleTotal > 0 Then
                CalculateConcentration(Quantity, VolumenVehicleTotal)
            End If

            If _atc IsNot Nothing AndAlso _atc.FormulationType = 3 Then
                CalculateVolumeATC()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de insumo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMedicine_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMedicine.EditValueChanged
        ProcessMedicament(AtcId, unitDoseType)
    End Sub

    ''' <summary>
    ''' Metodo que procesa lo necesario cuando se selecciona un medicamento
    ''' </summary>
    Private Async Sub ProcessMedicament(AtcId As Integer?, unitDoseType As UnitDoseType)
        Try
            AsyncLoader(True)
            If AtcId IsNot Nothing Then

                Dim _presenter As New PInventoryProduct
                _atc = Await _presenter.GetMedicamentById(AtcId.Value)
                If _atc Is Nothing Then
                    ShowWarning("Medicamento no encontrado")
                    Return
                End If

                Me.SourceName = _atc.Name
                Me.Osmolarity = _atc.Osmolarity
                Me.Density = _atc.Density

                If {EUnitDoseTypeClass.Antibiotics, EUnitDoseTypeClass.OtherSterile, EUnitDoseTypeClass.Cytostatic}.Contains(unitDoseType.MSClass) Then
                    If Not _atc.SuitableForReconstitution And _atc.FormulationType = 1 Then
                        ShowWarning($"El medicamento {_atc.CodeName} se encuentra parametrizado como no apto para reconstitución")
                        Return
                    End If

                    HandleAntibiotic(_atc)
                Else
                    HandleRegularMsClass(_atc)

                    If unitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                        INDseQuantity.ReadOnly = True
                        INDsleMeasurementUnit.ReadOnly = True
                        INDseVolume.Properties.ReadOnly = True
                        INDsleVolumeMeasureUnit.ReadOnly = True
                    End If
                End If
            Else
                CleanControls()

                CleanReconstitutionControls()
                CleanDilutionControls()
                HideControlsAddComponent()
            End If
        Catch ex As Exception
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que procesa la informacion del medicamento cuando el tipo de dosis unitaria es "Antibioticoterapia"
    ''' </summary>
    Private Sub HandleAntibiotic(_atc As ATCXpo)
        ListPreparationType = New List(Of Tuple(Of Byte, String))
        If Not _editModeForm Then
            HideControlsAddComponent()
            CleanReconstitutionControls()
            CleanDilutionControls()
        End If

        INDsleMeasurementUnit.Properties.DataSource = Nothing
        INDsleUnitMeasurementVehicle.Properties.DataSource = Nothing
        FormulationType = _atc.FormulationType

        If Not _editModeForm Then PreparationType = Nothing
        INDslePreparationType.Properties.DataSource = ListPreparationType
        INDslePreparationType.ReadOnly = False

        ' Si es un medicamento complementario, solo configurar valores básicos sin requerir dilución
        If IsNewDetailComplementary AndAlso IsDashboardConfirmationUnitDose Then
            ConfigureComplementaryMedicine(_atc)
            Return
        End If

        Select Case _atc.FormulationType
            Case 1 ' Peso
                Dim _presenterD As New PDilutionFactors
                _dilution = _presenterD.GetAtcDilutionById(_atc.Id)

                If _dilution IsNot Nothing Then
                    HideControlsForFormulationType(_atc.FormulationType)
                    ConfigureForWeight(_atc, _dilution)
                Else
                    ShowWarning($"Se debe parametrizar factor de dilución para el medicamento {_atc.CodeName}")
                End If

            Case 2 ' Volumen
                HideControlsForFormulationType(_atc.FormulationType)
                ConfigureForVolume(_atc)

            Case 3 ' Peso - Volumen

                HideControlsForFormulationType(_atc.FormulationType)
                ConfigureForWeightVolume(_atc, True)
                ListPreparationType.Add(New Tuple(Of Byte, String)(4, "Ninguna"))
                INDslePreparationType.Properties.DataSource = ListPreparationType

                If unitDoseType.MSClass = EUnitDoseTypeClass.Cytostatic AndAlso Not _editModeForm Then
                    Dim hasMedicineComponent As Boolean = ListPackageDetailValidation?.Any(Function(x) x.ComponentType = 1)

                    If hasMedicineComponent Then
                        Dim hasPreparationTypeNone As Boolean = ListPackageDetailValidation?.
                            Where(Function(x) x.ComponentType = 1).
                            Any(Function(x) x.PreparationType.HasValue AndAlso x.PreparationType = 4)

                        If hasPreparationTypeNone Then
                            PreparationType = CByte(4)
                            INDslePreparationType.ReadOnly = True
                        End If
                    End If
                End If

        End Select
    End Sub

    ''' <summary>
    ''' Configura los valores básicos para un medicamento complementario.
    ''' Los complementarios no requieren tipo de preparación ni dilución.
    ''' </summary>
    Private Sub ConfigureComplementaryMedicine(_atc As ATCXpo)
        ' Ocultar controles de volumen si no aplica
        HideControlsForFormulationType(_atc.FormulationType)

        ' Configurar unidad de medida según el tipo de formulación
        Select Case _atc.FormulationType
            Case 1 ' Peso
                If Not _editModeForm Then
                    Quantity = _atc.Weight
                    MeasureUnitId = _atc.WeightMeasureUnit?.Id
                    INDsleMeasurementUnit.Properties.NullText = _atc.WeightMeasureUnit?.CodeName
                End If

            Case 2 ' Volumen
                If Not _editModeForm Then
                    Quantity = _atc.Volume
                    MeasureUnitId = _atc.VolumeMeasureUnit?.Id
                    INDsleMeasurementUnit.Properties.NullText = _atc.VolumeMeasureUnit?.CodeName
                End If

            Case 3 ' Peso - Volumen
                If Not _editModeForm Then
                    Quantity = _atc.Weight
                    MeasureUnitId = _atc.WeightMeasureUnit?.Id
                    INDsleMeasurementUnit.Properties.NullText = _atc.WeightMeasureUnit?.CodeName

                    ' Para Peso-Volumen, también configurar el volumen
                    INDlyItemVolume.ShowLayout()
                    INDlyItemVolumeMeasureUnit.ShowLayout()
                    VolumenProduct = _atc.Volume
                    INDsleVolumeMeasureUnit.EditValue = _atc.VolumeMeasureUnit?.Id
                    INDsleVolumeMeasureUnit.Properties.NullText = _atc.VolumeMeasureUnit?.CodeName
                End If
        End Select

        ' Asegurar que el tipo de preparación sea "Ninguna" para complementarios
        PreparationType = CByte(4) ' Ninguna
        ListPreparationType.Add(New Tuple(Of Byte, String)(4, "Ninguna"))
        INDslePreparationType.Properties.DataSource = ListPreparationType
        INDslePreparationType.Properties.NullText = "Ninguna"
        INDslePreparationType.ReadOnly = True

        ' Ocultar controles de reconstitución y dilución
        INDlygReconstitution?.HideControl()
        INDlygDilution?.HideControl()
        INDlygDilutionStability?.HideControl()
    End Sub

    ''' <summary>
    ''' Metodo que procesa la informacion del medicamento para los demas tipos de dosis unitaria
    ''' </summary>
    Private Sub HandleRegularMsClass(_atc As ATCXpo)
        Select Case _atc.FormulationType
            Case 1 ' Peso
                ConfigureForWeight(_atc, Nothing)

            Case 2 ' Volumen
                ConfigureForVolume(_atc)

            Case 3 ' Peso - Volumen
                Select Case unitDoseType.MSClass
                    Case EUnitDoseTypeClass.Refilling
                        HideControlsForFormulationType(_atc.FormulationType)
                        ConfigureForWeightVolume(_atc, True)
                    Case Else
                        ConfigureForWeightVolume(_atc, False)
                End Select
        End Select
    End Sub

    ''' <summary>
    ''' Metodo que oculta campos dependiendo del tipo de unidad de medida
    ''' </summary>
    Private Sub HideControlsForFormulationType(FormulationType As Byte)
        If {1, 2}.Contains(FormulationType) Then
            INDlyItemVolume.HideControl()
            INDlyItemVolumeMeasureUnit.HideControl()
        Else
            INDlyItemVolume.ShowLayout()
            INDlyItemVolumeMeasureUnit.ShowLayout()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que setea los parametros cuando la unidad de medida es "Peso"
    ''' </summary>
    Private Sub ConfigureForWeight(_atc As ATCXpo, _dilution As DilutionFactorsXpo)

        If _dilution IsNot Nothing Then
            ListPreparationType.Add(New Tuple(Of Byte, String)(1, "Reconstitución"))
            ListPreparationType.Add(New Tuple(Of Byte, String)(3, "Reconstitucion - Dilución"))
        End If

        If Not _editModeForm Then

            Quantity = _atc.Weight
            MeasureUnitId = _atc.WeightMeasureUnit.Id
            INDsleMeasurementUnit.Properties.NullText = _atc.WeightMeasureUnit.CodeName

            If _dilution IsNot Nothing Then
                MeasureUnitId = _dilution.WeightMeasureUnit.Id
                INDsleMeasurementUnit.Properties.NullText = _dilution.WeightMeasureUnit.CodeName
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que setea los parametros cuando la unidad de medida es "Volumen"
    ''' </summary>
    Private Sub ConfigureForVolume(_atc As ATCXpo)
        If Not _editModeForm Then

            Quantity = _atc.Volume
            MeasureUnitId = _atc.VolumeMeasureUnit.Id
            INDsleMeasurementUnit.Properties.NullText = _atc.VolumeMeasureUnit.CodeName

        End If
    End Sub

    ''' <summary>
    ''' Metodo que setea los parametros cuando la unidad de medida es "Peso - Volumen"
    ''' </summary>
    Private Sub ConfigureForWeightVolume(_atc As ATCXpo, IsAntibiotic As Boolean)

        If IsAntibiotic Then
            ListPreparationType.Add(New Tuple(Of Byte, String)(2, "Dilución"))

            INDseVolume.Properties.ReadOnly = True
            INDsleMeasurementUnit.ReadOnly = True
            INDsleVolumeMeasureUnit.ReadOnly = True
            INDsleUnitMeasurementVehicle.ReadOnly = True
        End If

        If Not _editModeForm Then

            Quantity = _atc.Weight
            MeasureUnitId = _atc.WeightMeasureUnit.Id
            INDsleMeasurementUnit.Properties.NullText = _atc.WeightMeasureUnit.CodeName

            If IsAntibiotic Then
                VolumenProduct = _atc.Volume
                INDsleVolumeMeasureUnit.EditValue = _atc.VolumeMeasureUnit.Id
                INDsleVolumeMeasureUnit.Properties.NullText = _atc.VolumeMeasureUnit.CodeName
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de limpiar la informacion e indicar el mensaje de validacion
    ''' </summary>
    Private Sub ShowWarning(message As String)
        Mensaje(EeventViewerImages.Advertencia) = message
        CleanControls()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de insumo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleSupplie_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleSupplie.EditValueChanged
        If SupplieId IsNot Nothing Then

            Dim _presenter As New PInventoryProduct
            Dim _supplie As InventorySupplieXpo = Await _presenter.GetSupplieById(SupplieId)
            If _supplie IsNot Nothing Then
                Me.SourceName = _supplie.SupplieName
                Me.Osmolarity = 0
                Me.Density = 0
            End If

            _presenter = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleProduct.EditValueChanged
        If ProductId IsNot Nothing Then

            Using model As New MInventoryProduct(Me.Tag)
                Dim resultProduct = Await model.GetProductByIdXpo(ProductId)
                If resultProduct IsNot Nothing Then
                    Me.SourceName = resultProduct.Name
                    Me.Osmolarity = 0
                    Me.Density = 0

                    If unitDoseType.MSClass = EUnitDoseTypeClass.ParenteralNutrition Then
                        INDseQuantity.Properties.ReadOnly = True
                        INDsleMeasurementUnit.Properties.ReadOnly = True
                        Quantity = 1D
                    End If

                    If resultProduct.MeasurementUnitId IsNot Nothing Then
                        MeasureUnitId = resultProduct.MeasurementUnitId.Id
                        INDsleMeasurementUnit.Properties.NullText = String.Format("{0} - {1}", resultProduct.MeasurementUnitId.Code, resultProduct.MeasurementUnitId.Name)
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia el valor del volumen total del preparado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDVolumenTotalVehicle_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles INDVolumenTotalVehicle.EditValueChanged
        If VehicleId IsNot Nothing And MeasureunitPrepared IsNot Nothing And _MeasureUnit IsNot Nothing And _MeasureUnitVehicle IsNot Nothing Then

            CalculateConcentration(Quantity, VolumenVehicleTotal)
            If PreparationType = 2 Then
                CalculateVolumeVehicle(VolumenVehicleTotal, VolumenProduct)
            Else
                CalculateVolumeVehicle(VolumenVehicleTotal, VolumenThinner)
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia el valor del volumen del medicamento principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseVolume_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles INDseVolume.EditValueChanged
        CalculateVolumeVehicle(VolumenVehicleTotal, VolumenProduct)
    End Sub

    ''' <summary>
    ''' evento cuando se cambia la cantidad del medicamento principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseQuantity_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles INDseQuantity.EditValueChanged
        ''Si la unidad de medida del medicamento es (Peso - Volumen); el volumen a utilizar es calculado
        If _atc?.FormulationType = 3 Then
            CalculateVolumeATC()
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia el volumen de la reconstitución
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtVolume_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles INDtxtVolume.EditValueChanged
        If ReconstituyenteId IsNot Nothing And VehicleId IsNot Nothing Then
            CalculateVolumeVehicle(VolumenVehicleTotal, VolumenThinner)
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al dar click para cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupProductsPurchaseOrder_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim _componentId As Integer?
        Select Case ComponentType
            Case 1, 4
                _componentId = AtcId
            Case 2
                _componentId = SupplieId
            Case 3, 5
                _componentId = ProductId
        End Select
        If _componentId IsNot Nothing AndAlso _componentId > 0 Then
            If Not MessageIndigo.Show("Al cerrar el formulario se perderá la información que habia registrado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(300, Nothing, True)
            Using Model As New MBusqueda
                INDsleMeasurementUnit.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListMeasureUnit)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleVolumeMeasureUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVolumeMeasureUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(300, Nothing, True)
            INDsleVolumeMeasureUnit.Properties.DataSource = Presenter.InitializeMeasureUnitVolumen()
        End If
    End Sub

#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Async Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControlsAll()
        If _editModeForm Then Await LoadControls()
    End Sub
#End Region

End Class