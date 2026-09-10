'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Giovanny Plazas L
' Created          : 05/09/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Controls
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Xpo.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmPopupDilutionFactorsDetail

#Region "Events"

    ''' <summary>
    ''' evento para agregar un producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddDilutionFactorsDetail(sender As Object, e As GetDilutionFactorsDetailEventArgs)

#End Region

#Region "Builder"
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        _CtrDilutionFactor = New CtrDilutionFactor()
        _CtrDilutionFactor.Dock = DockStyle.Top
        AdditionalControlPanel.Controls.Add(_CtrDilutionFactor)

    End Sub
#End Region

#Region "Variables"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "MixingStation"

    ''' <summary>
    ''' Define si el formulario esta editando o no
    ''' </summary>
    ''' <remarks></remarks>
    Private _editModeForm As Boolean

    ''' <summary>
    ''' control de la parte superior 
    ''' </summary>
    Private _CtrDilutionFactor As CtrDilutionFactor

    ''' <summary>
    ''' Id del  Medicamento seleccionado en la cabecera
    ''' </summary>
    Public AtcIdHeader As Integer?

    ''' <summary>
    ''' Referencia la presentador (MixingStation.DilutionFactors)
    ''' </summary>
    Private _presenter As PDilutionFactors

    ''' <summary>
    ''' Volumen existente
    ''' </summary>
    Private RequiredVolume As Decimal

#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene o establece el medicamento diluyente
    ''' </summary>
    Public Property AtcId As Integer?
        Get
            Return CType(INDSleATC.EditValue, Integer?)
        End Get
        Set(value As Integer?)
            INDSleATC.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' volumen de la dilucion
    ''' </summary>
    ''' <returns></returns>
    Public Property Volumen As Decimal
        Get
            Return CType(INDseVolume.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDseVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' volumen de desplazamiento
    ''' </summary>
    ''' <returns></returns>
    Public Property DisplacementVolume As Decimal
        Get
            Return CType(INDseDisplacementVolume.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDseDisplacementVolume.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad de medida
    ''' </summary>
    ''' <returns></returns>
    Public Property MeasureUnitId As Integer?
        Get
            Return INDsleMeasurementUnit.EditValue
        End Get
        Set(value As Integer?)
            INDsleMeasurementUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' factor de dilucion
    ''' </summary>
    Private _dilutionFactor As Decimal
    Public Property DilutionFactor As Decimal
        Get
            Return _dilutionFactor
        End Get
        Set(value As Decimal)
            _dilutionFactor = value
            INDTeDilutionFactor.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' concentracion del medicamento
    ''' </summary>
    Private _concentration As Decimal
    Public Property Concentration As Decimal
        Get
            Return _concentration
        End Get
        Set(value As Decimal)
            _concentration = value
            INDTeConcentration.EditValue = $"{value} {_weightStandarAbbreviation}/{MeasureUnitDefault.Abbreviation}"
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la unidad de tiempo
    ''' </summary>
    Public Property TimeUnit As Byte
        Get
            Return INDsleTimeUnit.EditValue
        End Get
        Set(value As Byte)
            INDsleTimeUnit.EditValue = value
        End Set
    End Property

    Public Property TimeUnitDatasource As List(Of Tuple(Of Byte, String))
        Get
            Return TryCast(INDsleTimeUnit.Properties.DataSource, List(Of Tuple(Of Byte, String)))
        End Get
        Set(value As List(Of Tuple(Of Byte, String)))
            INDsleTimeUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Cantidad de tiempo
    ''' </summary>
    ''' <returns></returns>
    Public Property TimeQuantity As Integer
        Get
            Return CType(INDseQuantity.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDseQuantity.EditValue = value
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

    Private _measureUnitDefault
    Public Property MeasureUnitDefault As MeasureUnitXpo
        Get
            Return _measureUnitDefault
        End Get
        Set(value As MeasureUnitXpo)
            _measureUnitDefault = value
        End Set
    End Property

    Private _weightStandar As Decimal
    Public WriteOnly Property WeightStandar As Decimal
        Set(value As Decimal)
            _weightStandar = value
        End Set
    End Property

    Private _weightStandarAbbreviation As String
    Public WriteOnly Property WeightStandarAbbreviation As String
        Set(value As String)
            _weightStandarAbbreviation = value
        End Set
    End Property

    Private _dilutionFactorDetail As DilutionFactorsDetail
    Public WriteOnly Property DilutionFactorDetail As DilutionFactorsDetail
        Set(value As DilutionFactorsDetail)
            _dilutionFactorDetail = value
        End Set
    End Property

    Private _ATCName As String
    Public WriteOnly Property ATCName As String
        Set(value As String)
            _ATCName = value
        End Set
    End Property

#End Region

#Region "DataSource"

    Private _listTimeUnit As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Propiedad para capturar el tipo de etiqueta
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property listTimeUnit As List(Of Tuple(Of Byte, String))
        Get
            If _listTimeUnit Is Nothing Then
                _listTimeUnit = New List(Of Tuple(Of Byte, String))
                _listTimeUnit.Add(New Tuple(Of Byte, String)(1, "HORA(S)"))
                _listTimeUnit.Add(New Tuple(Of Byte, String)(2, "DIA(S)"))
            End If
            Return _listTimeUnit
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia los controles para agregar un producto nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        Me.AtcId = Nothing
        Me.Volumen = 0.00F
        Me.DilutionFactor = 0.0F
        Concentration = 0.00F
        TimeQuantity = 0
        TimeUnit = 1
    End Sub

    ''' <summary>
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Sub LoadControls()
        INDbtnAdd.Text = ResourceManager.GetString("Edit")
        If _dilutionFactorDetail Is Nothing Then
            CleanControls()
            Exit Sub
        End If
        With _dilutionFactorDetail
            Me.AtcId = .AtcId
            Me.INDSleATC.Properties.NullText = .ATCCodeName
            Me.Volumen = .Volume
            Me.DisplacementVolume = .DisplacementVolume
            Me.RequiredVolume = .RequiredVolume
            Me.TimeQuantity = .AmountTime
            Me.TimeUnit = .TimeUnit
        End With
        INDSleATC.Properties.ReadOnly = True
    End Sub

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
    ''' Método que valida campos
    ''' </summary>
    Private Function ValidaCampos() As Boolean
        Dim errors As New StringBuilder

        If Not ValidateControls() Then
            INDbtnAdd.Enabled = True
            Return False
        End If

        If Me.Volumen <= 0 Then
            errors.AppendLine("El volumen del medicamento debe ser superior a 0")
        End If

        If Me.RequiredVolume <= 0 Then
            errors.AppendLine("El volumen requerido(Vol. Total - Vol. Desplazamiento) debe ser superior a 0")
        End If

        If errors.Length > 0 Then
            INDbtnAdd.Enabled = True
            Mensaje(EeventViewerImages.Advertencia) = $"Se presentaron las siguientes validaciones : {errors.ToString}"
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Metodo que se encarga de calcular los valores de dilucion y concentracion del detalle de factor de dilucion
    ''' </summary>
    Private Sub DivisionCalculated(_atcId As Integer?, _volumen As Decimal, weightStandar As Decimal)
        If _atcId IsNot Nothing And _volumen > 0 AndAlso weightStandar >= 0 Then

            Dim ResultDilution = _volumen / weightStandar
            Me.DilutionFactor = Utils.SetPartDecimalToValue(ResultDilution)

            Dim ResultConcentration As Decimal = weightStandar / _volumen
            Me.Concentration = Utils.SetPartDecimalToValue(ResultConcentration)

            If _volumen > DisplacementVolume Then RequiredVolume = _volumen - DisplacementVolume
        End If
    End Sub

    Public Function AssignEntity() As DilutionFactorsDetail
        Dim _dilutionFactorsDetail = New DilutionFactorsDetail
        With _dilutionFactorsDetail
            .AtcId = AtcId
            .Volume = Volumen
            .DisplacementVolume = DisplacementVolume
            .RequiredVolume = RequiredVolume
            .Dilution = DilutionFactor
            .Concentration = Me.Concentration
            .TimeUnit = TimeUnit
            .AmountTime = Me.TimeQuantity
            .VolumeMeasureUnit = MeasureUnitDefault.Id
            .RequiredVolumeCodeName = $"{RequiredVolume}{MeasureUnitDefault.Abbreviation}"
            .DisplacementVolumeCodeName = $"{DisplacementVolume}{MeasureUnitDefault.Abbreviation}"
            .VolumenCodeName = $"{Volumen} {MeasureUnitDefault.Abbreviation}"
            .TimeUnitCodeName = $"{TimeQuantity} {INDsleTimeUnit.Properties.GetDisplayTextByKeyValue(TimeUnit)}"
            .ConcentrationCodeName = INDTeConcentration.EditValue
            .ATCCodeName = $"{INDSleATC.Properties.GetDisplayTextByKeyValue(AtcId)}"
            .ByDefault = False
        End With
        Return _dilutionFactorsDetail
    End Function

#End Region

#Region "Handlers"

#Region "EditValueChanged"

    ''' <summary>
    ''' evento para cargar los controles derivados del medicamento principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleATC_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleATC.EditValueChanged
        If AtcId IsNot Nothing Then
            Await GetStabilityDetailReconstitution()
        End If
    End Sub

    ''' <summary>
    ''' evento para cargar el detalle tipo reconstituyente de la estabilidad
    ''' </summary>
    Private Async Function GetStabilityDetailReconstitution() As Task
        Try
            AsyncLoader(True)

            Dim StabilityDetailTmp = Await Task.Run(Function() As MixingStationRepository.StabilityTableDetailReconstitutionXpo
                                                        Return TryCast(INDSleATC?.GetSelectedObject(), MixingStationRepository.StabilityTableDetailReconstitutionXpo)
                                                    End Function)

            If StabilityDetailTmp Is Nothing Then
                StabilityDetailTmp = Await _presenter.GetStabilityTableDetailReconstitutionByAtcId(AtcId, AtcIdHeader)
            End If

            If StabilityDetailTmp IsNot Nothing Then
                DisplacementVolume = StabilityDetailTmp.Volume
                If Volumen > DisplacementVolume Then RequiredVolume = Volumen - DisplacementVolume
            End If

        Catch ex As Exception
            Throw
        Finally
            AsyncLoader(False)
        End Try
    End Function

#End Region

#Region "Load"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmPopupDilutionFactorsDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.StatusRecordVisible = False

        'Establece por defecto la unidad de medida Mililitros
        AsyncLoader(True)
        Using model As New MDilutionFactors("2821")
            MeasureUnitDefault = Await Task.Factory.StartNew(Function() As MeasureUnitXpo
                                                                 Return model.DefaultMeasureUnit("Code = 019")
                                                             End Function)
            MeasureUnitId = MeasureUnitDefault.Id
            INDsleMeasurementUnit.Properties.NullText = MeasureUnitDefault.CodeName
        End Using

        _presenter = New PDilutionFactors()
        TimeUnitDatasource = listTimeUnit
        TimeUnit = 0
        _CtrDilutionFactor.MedicineName = _ATCName
        _CtrDilutionFactor.WeightStandart = $"{_weightStandar} {_weightStandarAbbreviation}"
        Await InitializeDataSource()

        AsyncLoader(False)
        If _editModeForm Then
            LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Inicializa los Datasources de los campos 
    ''' </summary>
    Private Async Function InitializeDataSource() As Task
        INDSleATC.Properties.DataSource = Await _presenter.GetDilutionStabilityTableDetailByATCIdHeader(AtcIdHeader)

        If Not CType(INDSleATC.Properties.DataSource, List(Of MixingStationRepository.StabilityTableDetailReconstitutionXpo))?.Any() Then
            Mensaje(EeventViewerImages.Advertencia) = $"No existen medicamentos tipo 'Diluyente' asociados al medicamento {_ATCName}"
        End If
    End Function

#End Region

#Region "Activated"

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupDilutionFactorsDetail_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated

    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega el producto al listado del paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        Try
            INDbtnAdd.Enabled = False
            If Not ValidaCampos() Then
                Exit Sub
            End If

            Dim _dilutionFactorsDetail = AssignEntity()
            Dim args = New GetDilutionFactorsDetailEventArgs
            args.ItemDetails = _dilutionFactorsDetail
            args.EditModeForm = _editModeForm
            RaiseEvent AddDilutionFactorsDetail(Nothing, args)

            INDbtnAdd.Enabled = True
            Me.Close()
        Catch ex As Exception
            INDbtnAdd.Enabled = True
            Throw ex
        End Try
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmPopupDilutionFactorsDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleMedicine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleATC.QueryPopUp
        If INDSleATC.Properties.DataSource Is Nothing And AtcIdHeader IsNot Nothing Then
            INDSleATC.Properties.DataSource = Await _presenter.GetDilutionStabilityTableDetailByATCIdHeader(AtcIdHeader)
        End If
    End Sub


#End Region

#Region "EditValueChanged"


    ''' <summary>
    ''' evento cuando se cambia el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseVolume_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDseVolume.EditValueChanged
        DivisionCalculated(AtcId, Volumen, _weightStandar)
    End Sub

#End Region
#Region "EditValueChanging"

    ''' <summary>
    ''' evento cuando se cambia el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseQuantity_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDseQuantity.EditValueChanging
        If e.NewValue <= 0 Then
            e.Cancel = True
            Exit Sub
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


#End Region

#End Region

#Region "OtherClass"
    ''' <summary>
    ''' clase para retornar en el evento 
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GetDilutionFactorsDetailEventArgs
        Inherits EventArgs

        Property ItemDetails As DilutionFactorsDetail
        Property EditModeForm As Boolean = False
    End Class
#End Region

End Class