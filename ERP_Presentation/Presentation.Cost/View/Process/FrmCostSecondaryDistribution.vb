'***********************************************************************
' Assembly         : Presentacion.Cost
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/12/2016
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CostRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Cost.MVP
Imports Presentation.InteropCost

#End Region

Public Class FrmCostSecondaryDistribution
    Implements ICostDirectDistributionSecondary

#Region "Builder"

    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmGeneralExpenses"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrMainAccountValueDate()
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
    End Sub

#End Region

#Region "Variables and Properties"

#Region "Variables"

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Cost"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements ICostDirectDistributionSecondary.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el layout Control
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ICostDirectDistributionSecondary.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Control de Fecha
    ''' </summary>
    Private _tmpCurrentPeriod As CtrMainAccountValueDate

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PCostDirectDistributionSecondary

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.CostSecuence

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As Domain.Entities.CostSecuence Implements ICostDirectDistributionSecondary.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.CostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.CostSecuenceDetail In Me._sequence.CostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordCost

    ''' <summary>
    ''' Columnas del gridview
    ''' </summary>
    Private _colActions As DevExpress.XtraGrid.Columns.GridColumn

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Parámetros de costos
    ''' </summary>
    Private _settingsCost As CostSetting

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As CostSetting Implements ICostDirectDistributionSecondary.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As CostSetting)
            _tmpCurrentPeriod.Year = value.Year
            _tmpCurrentPeriod.Month = value.Month
            _tmpCurrentPeriod.LoadDate()
            _settingsCost = value
        End Set
    End Property

    ''' <summary>
    ''' entidad de distribución secundaria
    ''' </summary>
    Dim DirectDistributionSecondary As CostDirectDistributionSecondary

    ''' <summary>
    ''' Listado del detalle de base de distribución
    ''' </summary>
    Property ListDirectDistributionSecondaryDetail As List(Of CostDirectDistributionSecondaryDetail)
        Get
            Return CType(INDgcDetail.DataSource, List(Of CostDirectDistributionSecondaryDetail))
        End Get
        Set(value As List(Of CostDirectDistributionSecondaryDetail))
            INDgcDetail.DataSource = value
            INDgcDetail.Refresh()
            INDgvDetail.OptionsView.ShowFooter = True

            _tmpCurrentPeriod.Value = Me.Value
            _tmpCurrentPeriod.Balance = Me.Balance
        End Set
    End Property

    ''' <summary>
    ''' listado con los detalles para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteDirectDistributionSecondaryDetail As List(Of Integer)

    ''' <summary>
    ''' Identifica si se esta cargando un registro
    ''' </summary>
    Private _isLoadingControls As Boolean

    ''' <summary>
    ''' Listado de ids para actualizar el campo import en la tabla LogisticProductionCenterRecordDetail
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListCostLogisticsProductionCenterDetail As List(Of Integer)

    ''' <summary>
    ''' Centro de produccion asociado al elemento de distribucion secundaria
    ''' </summary>
    ''' <remarks></remarks>
    Dim ProductionCenterId As Integer = 0

#End Region

#Region "Properties Entity"

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostDirectDistributionSecondary.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleDistributionSecondary.Enabled = value
            INDpceAddDetail.Enabled = False
            INDebtnDistribution.Enabled = False
            INDgcDetail.Enabled = value
            BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDsleDistributionSecondary.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Año de la distribución del gasto Directo
    ''' </summary>
    Public Property Year As Integer Implements ICostDirectDistributionSecondary.Year
        Get
            Return _tmpCurrentPeriod.Year
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Year = value
        End Set
    End Property

    ''' <summary>
    ''' Mes de la distribución del gasto Directo
    ''' </summary>
    Public Property Month As Integer Implements ICostDirectDistributionSecondary.Month
        Get
            Return _tmpCurrentPeriod.Month
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Month = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto directo
    ''' </summary>
    Public Property Code As String Implements ICostDirectDistributionSecondary.Code
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
    ''' Elemento de distribución secundaria
    ''' </summary>
    ''' <returns></returns>
    Public Property DistributionSecondaryId As Integer? Implements ICostDirectDistributionSecondary.DistributionSecondaryId
        Get
            Return INDsleDistributionSecondary.EditValue
        End Get
        Set(value As Integer?)
            INDsleDistributionSecondary.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre
    ''' </summary>
    Public Property Description As String Implements ICostDirectDistributionSecondary.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Valor de la distribución secundaria
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property Value As Decimal
        Get
            Dim _value As Decimal = 0

            If DirectDistributionSecondary Is Nothing OrElse DirectDistributionSecondary.Id = 0 OrElse DirectDistributionSecondary.Status = 1 Then
                If costStimation IsNot Nothing Then
                    _value = costStimation.InitialDistribution
                End If
            Else
                _value = DirectDistributionSecondary.Value
            End If

            Return _value
        End Get
    End Property

    ''' <summary>
    ''' Valor pendiente de la distribución secundaria
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property Balance As Decimal
        Get
            Dim _valueDistribuited As Decimal = 0

            If DirectDistributionSecondary IsNot Nothing Then
                _valueDistribuited = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Sum(Function(d) d.Value)
            End If

            Return Me.Value - _valueDistribuited
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As String
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

#End Region

#Region "Datasource"

    Dim DistributionSecondaryXpo As CostDistributionSecondaryXpo

    Dim costStimation As CostEstimationNativeXpo = Nothing

#End Region

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmSecondaryDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me.indigo = SessionValues.Instance
        _presenter = New PCostDirectDistributionSecondary(Me)
        SetCurrencyFormat(_presenter.GetOfficialCurrencyFromCompanySettings().OfficialCurrency.Abbreviation)
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        _presenter.LoadDefinitionLayout()
        AsyncLoader(True)
        _presenter.GetSequence()
        _presenter.LoadSettingCost()
        AsyncLoader(False)

        INDebtnDistribution.AddRangeColumns("Centro Produccion", "Unidad Medida", "Cantidad", "Valor Unidad Medida")
        Deshacer()
        LoadStatus()
        SetActionsGrid()
    End Sub

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        changeNumericFormatByCurrency(numberFormat, INDpccAddProductionCenter.Controls)
        _tmpCurrentPeriod.CurrencyAbbreviation = _currencyAbbreviation
        INDColValue = Window.Utils.FormatGrid(INDColValue, _currencyAbbreviation)
    End Sub

    Private Sub FrmSecondaryDistribution_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListCostLogisticsProductionCenterDetail = Nothing
        ProductionCenterId = Nothing
        _settingsCost = Nothing
        _tmpCurrentPeriod = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        _colActions = Nothing
        _isLoadingControls = Nothing
        ListDeleteDirectDistributionSecondaryDetail = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmCostSecondaryDistribution_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteCode.Focus()
    End Sub

#End Region

#Region "KeyDown"

    Private Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(INDbteCode.Text) Then
                    LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(INDbteCode.Text) Then
                    Me.NewDirectDistributionSecondary()
                Else
                    LoadControls()
                End If
            End If
        End If
    End Sub

    Private Sub INDpceAddDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceAddDetail.ShowPopup()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleDistributionSecondary_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleDistributionSecondary.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1737, Nothing, True)
            Using model As New MCostDirectDistributionSecondary(MyTag)
                INDsleDistributionSecondary.Properties.DataSource = model.GetDistributionSecondary()
            End Using
        End If
    End Sub

    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1729, Nothing, True)
            Using model As New MCostDirectDistributionSecondary(MyTag)
                INDsleProductionCenter.Properties.DataSource = model.GetDistributionSecondaryProductionCenterBySecundaryId(INDsleDistributionSecondary.EditValue)
            End Using
        End If
    End Sub

    Private Sub INDsleMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(300, Nothing, True)
            Using model As New MCostDirectDistributionSecondary(MyTag)
                INDsleMeasurementUnit.Properties.DataSource = model.GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(INDsleDistributionSecondary.EditValue)
            End Using
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleDistributionSecondary_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleDistributionSecondary.QueryPopUp
        If INDsleDistributionSecondary.Properties.DataSource Is Nothing Then
            Using model As New MCostDirectDistributionSecondary(MyTag)
                INDsleDistributionSecondary.Properties.DataSource = model.GetDistributionSecondary()
            End Using
        End If
    End Sub

    Private Sub INDsleProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionCenter.QueryPopUp
        If INDsleProductionCenter.Properties.DataSource Is Nothing Then
            Using model As New MCostDirectDistributionSecondary(MyTag)
                INDsleProductionCenter.Properties.DataSource = model.GetDistributionSecondaryProductionCenterBySecundaryId(INDsleDistributionSecondary.EditValue)
            End Using
        End If
    End Sub

    Private Sub INDsleMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasurementUnit.QueryPopUp
        If INDsleMeasurementUnit.Properties.DataSource Is Nothing Then
            Using model As New MCostDirectDistributionSecondary(MyTag)
                INDsleMeasurementUnit.Properties.DataSource = model.GetDistributionSecondaryMeasurementUnitByDistributionSecondaryId(INDsleDistributionSecondary.EditValue)
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleDistributionSecondary_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDistributionSecondary.EditValueChanged
        DistributionSecondaryXpo = Nothing
        ProductionCenterId = 0
        costStimation = Nothing
        ListCostLogisticsProductionCenterDetail = Nothing

        INDpceAddDetail.Enabled = False
        INDebtnDistribution.Enabled = False
        _tmpCurrentPeriod.Value = 0
        _tmpCurrentPeriod.Balance = 0
        GridColumn7.Visible = False
        _colActions.Visible = False

        If INDsleDistributionSecondary.EditValue IsNot Nothing Then
            DistributionSecondaryXpo = _presenter.GetDistributionSecondary(INDsleDistributionSecondary.EditValue)

            ProductionCenterId = DistributionSecondaryXpo.ProductionCenterId.Id
            INDsleDistributionSecondary.Properties.NullText = DistributionSecondaryXpo.CodeName
            INDtxtDescription.EditValue = DistributionSecondaryXpo.CodeName

            Using model As New MCostDirectDistributionSecondary(MyTag)
                costStimation = model.GetCostEstimationXpo(ProductionCenterId, Year, Month)
                If costStimation Is Nothing Then
                    INDsleDistributionSecondary.EditValue = Nothing
                    Mensaje(EeventViewerImages.Advertencia) = "El centro de producción no tiene una estimación de costo de tipo primaria"
                    Exit Sub
                End If

                _tmpCurrentPeriod.Balance = Me.Balance
                _tmpCurrentPeriod.LoadDate()
            End Using

            If Not _isLoadingControls OrElse (DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Status = 1 AndAlso Me.Balance <> 0) Then
                If Not _isLoadingControls OrElse Not IsDirectDistribution(DistributionSecondaryXpo) Then
                    If DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Id > 0 AndAlso DirectDistributionSecondary.CostDirectDistributionSecondaryDetail IsNot Nothing Then
                        If ListDeleteDirectDistributionSecondaryDetail Is Nothing Then
                            ListDeleteDirectDistributionSecondaryDetail = New List(Of Integer)
                        End If
                        For Each detail In DirectDistributionSecondary.CostDirectDistributionSecondaryDetail
                            If detail.Id > 0 Then
                                ListDeleteDirectDistributionSecondaryDetail.Add(detail.Id)
                            End If
                        Next
                    End If
                    DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Clear()
                    ListDirectDistributionSecondaryDetail = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.ToList()
                    CleanControlsPopup()
                End If

                If Not IsDirectDistribution(DistributionSecondaryXpo) Then
                    RaiseCalcDistribution()
                End If
            End If

            If Not _isLoadingControls OrElse (DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Status = 1) Then
                If IsDirectDistribution(DistributionSecondaryXpo) Then
                    INDpceAddDetail.Enabled = True
                    INDebtnDistribution.Enabled = True
                    GridColumn1.VisibleIndex = 0
                    GridColumn7.VisibleIndex = 1
                    GridColumn10.VisibleIndex = 2
                    INDColValue.VisibleIndex = 3
                    _colActions.VisibleIndex = 4
                    If DistributionSecondaryXpo.ProductionCenterId.CenterType = 3 Then
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub INDsleMeasurementUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMeasurementUnit.EditValueChanged
        If INDsleMeasurementUnit.EditValue IsNot Nothing Then
            Using model As New MCostDirectDistributionSecondary(MyTag)
                Dim measureUnit As Infrastructure.Data.Xpo.CostRepository.InventoryInventoryMeasurementUnitReportXpo = model.GetInventoryMeasurementUnitById(INDsleMeasurementUnit.EditValue)
                INDTxtMeasureUnitValue.EditValue = measureUnit.CostValue
                INDTxtMeasureUnitValue.Properties.ReadOnly = Not measureUnit.AllowEditCostValue
            End Using
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        Dim errors As New StringBuilder

        If INDsleProductionCenter.EditValue Is Nothing Then
            errors.AppendLine("Seleccione un centro de producción")
        End If
        If INDsleMeasurementUnit.EditValue Is Nothing Then
            errors.AppendLine("Seleccione una unidad de medida")
        End If
        If INDtxtMaximumAmount.EditValue <= 0 Then
            errors.AppendLine("El valor debe ser mayor a 0")
        End If
        If DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Where(Function(x) x.ProductionCenterId = INDsleProductionCenter.EditValue).FirstOrDefault() IsNot Nothing Then
            errors.AppendLine("Ya se encuentar agregado el centro de producción")
        End If
        If (DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Sum(Function(x) x.Value) + (INDtxtMaximumAmount.EditValue * INDTxtMeasureUnitValue.EditValue)) > Me.Value Then
            errors.AppendLine("No se puede agregar el item porque superaria el valor de la estimación de costo")
        End If

        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString
            Exit Sub
        End If

        Dim DirectDistributionSecondaryDetail As New CostDirectDistributionSecondaryDetail
        With DirectDistributionSecondaryDetail
            .ProductionCenterId = INDsleProductionCenter.EditValue
            .CodeNameProductionCenter = INDsleProductionCenter.Text
            .MeasurementUnitId = INDsleMeasurementUnit.EditValue
            .CodeNameMeasureUnit = INDsleMeasurementUnit.Text
            .Value = INDtxtMaximumAmount.EditValue * INDTxtMeasureUnitValue.EditValue
            .Percentage = (.Value / costStimation.InitialDistribution * 100)
        End With

        DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Add(DirectDistributionSecondaryDetail)
        ListDirectDistributionSecondaryDetail = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.ToList()

        CleanControlsPopup()
        INDsleProductionCenter.Focus()
    End Sub

#End Region

#Region "MenuContext"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If DirectDistributionSecondary.Status > 1 OrElse Not IsDirectDistribution(DistributionSecondaryXpo) Then
            Exit Sub
        End If
        If MessageIndigo.Show("Seguro de eliminar el registro", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        Dim detail = DirectCast(INDgvDetail.GetFocusedRow, CostDirectDistributionSecondaryDetail)
        If detail.Id > 0 Then
            If ListDeleteDirectDistributionSecondaryDetail Is Nothing Then
                ListDeleteDirectDistributionSecondaryDetail = New List(Of Integer)
            End If
            detail.MarkAsDeleted()
            ListDeleteDirectDistributionSecondaryDetail.Add(detail.Id)
        End If
        DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Remove(detail)
        ListDirectDistributionSecondaryDetail = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.ToList()
    End Sub

#End Region

#Region "DatasourceChanged"

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDirectCostDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDetail.DataSourceChanged
        Dim status = ListDirectDistributionSecondaryDetail IsNot Nothing AndAlso ListDirectDistributionSecondaryDetail.Count > 0
        INDsleDistributionSecondary.Properties.ReadOnly = status
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(sender, e.Rows)
    End Sub

    ''' <summary>
    ''' Metodo que copia y pega los items a la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If DistributionSecondaryId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un elemento de distribución secundaria"
            Exit Function
        End If

        INDgvDetail.ShowLoadingPanel()
        Me.Cursor = ChangeCursorIndigo()
        Using model As New MCostDistributionSecondaryElements(MyTag)
            Dim result = Await model.SP_CopyPasteCostSecondaryDistribution(ListInfo, DistributionSecondaryId, Me.Value)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.MensajeError) = result.Message
                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDgvDetail.HideLoadingPanel()
                Exit Function
            End If
            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                If DirectDistributionSecondary.CostDirectDistributionSecondaryDetail IsNot Nothing AndAlso DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Count > 0 Then
                    If result.ObjectEmbbededAux Is Nothing Then
                        result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                    End If
                    For Each x In result.ObjectEmbbeded
                        If (From l In DirectDistributionSecondary.CostDirectDistributionSecondaryDetail Where l.ProductionCenterId = x.ProductionCenterId Select l).Count > 0 Then
                            result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El centro de producción " + x.CodeNameProductionCenter + " ya existe en la lista con la unidad de medida " + x.CodeNameMeasureUnit, 2))
                            Continue For
                        End If
                        If (DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Sum(Function(y) y.Value) + x.Value) > costStimation.InitialDistribution Then
                            result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("El centro de producción " + x.CodeNameProductionCenter + " con la unidad de medida " + x.CodeNameMeasureUnit + " no se puede agregar porque superaria el valor de la estimación de costo", 2))
                            Continue For
                        End If
                        DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Add(x)
                    Next
                Else
                    If result.ObjectEmbbeded.Sum(Function(y) y.Value) > costStimation.InitialDistribution Then
                        result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("No se pueden agregar los items porque la sumatoria de sus valores superaria el valor de la estimación de costo", 2))
                    Else
                        result.ObjectEmbbeded.ForEach(Sub(x) DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Add(x))
                    End If
                End If
            End If
            If result.ObjectEmbbededAux IsNot Nothing AndAlso result.ObjectEmbbededAux.Count > 0 Then
                Using formulario As New FrmListErrors(result.ObjectEmbbededAux)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using
        INDgvDetail.HideLoadingPanel()
        Me.Cursor = System.Windows.Forms.Cursors.Default
        ListDirectDistributionSecondaryDetail = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.ToList()
    End Function

#End Region

#Region "Popup"

    Private Sub INDpceAddDetail_Popup(sender As Object, e As EventArgs) Handles INDpceAddDetail.Popup
        INDsleProductionCenter.Focus()
    End Sub

#End Region

#End Region

#Region "IdEntity"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDetail, _listActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDetail.Columns
            If col.Name = "colActions" Then
                _colActions = col
                col.Width = 130
                col.OptionsColumn.FixedWidth = True
            End If
        Next
    End Sub

    Public Sub CleanControls() Implements ICostDirectDistributionSecondary.CleanControls
        ReadOnlyControls(False, INDlcRoot)
        ActionsOnControls = False
        INDbteCode.EditValue = String.Empty
        INDtxtDescription.EditValue = String.Empty
        INDsleDistributionSecondary.Properties.ReadOnly = False
        INDsleDistributionSecondary.EditValue = Nothing
        INDsleDistributionSecondary.Properties.NullText = String.Empty
        INDgcDetail.DataSource = Nothing
        DirectDistributionSecondary = Nothing
        Me.BarraBotones.StatusRecord = Nothing
        DeleteBlockedRecord()
        _tmpCurrentPeriod.Value = 0
        _tmpCurrentPeriod.Balance = 0
        If _settingsCost IsNot Nothing Then
            _tmpCurrentPeriod.Year = _settingsCost.Year
            _tmpCurrentPeriod.Month = _settingsCost.Month
        End If
        _tmpCurrentPeriod.LoadDate()
        Me._doc = Nothing
        ListCostLogisticsProductionCenterDetail = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarTodos) = False
    End Sub

    Private Sub CleanControlsPopup()
        INDsleProductionCenter.EditValue = Nothing
        INDsleMeasurementUnit.EditValue = Nothing
        INDTxtMeasureUnitValue.EditValue = 0
        INDtxtMaximumAmount.EditValue = 0
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.DirectDistributionSecondary.Code, Me.DirectDistributionSecondary.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.DirectDistributionSecondary.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DirectDistributionSecondary.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me.DirectDistributionSecondary.Code, Me.DirectDistributionSecondary.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me.DirectDistributionSecondary.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonInteropCost As New MCommonCost(Me.Tag)
                Await ModelCommonInteropCost.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Private Async Sub NewDirectDistributionSecondary()
        If Me._settingsCost Is Nothing OrElse Me._settingsCost.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Costos"
            Exit Sub
        End If

        Me.DirectDistributionSecondary = New CostDirectDistributionSecondary() With {.Status = 1}
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarTodos) = True
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.CostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Sub
                End If
            End If
            If Not Me._sequence.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense.ContainsKey(CInt(Me._idCurrentSequence)) = True AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MCommonCost(Me.Tag)
                            Me.DicSequense(Me._idCurrentSequence) = Await model.GetNumericSequenseGroup(Me._idCurrentSequence)
                        End Using
                        If Me.DicSequense(Me._idCurrentSequence) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequence).Count > 0 Then
                            Me.Code = Me.DicSequense(Me._idCurrentSequence)(0)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Sub
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        End If
        Me.ActionsOnControls = True
        Status = "1"
    End Sub

    Public Async Sub LoadControls() Implements ICostDirectDistributionSecondary.LoadControls
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarTodos) = False
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            Using model As New MCostDirectDistributionSecondary(MyTag)
                AsyncLoader(True)
                DirectDistributionSecondary = Await model.GetCostDirectDistributionSecondary(Me.Code)
                If DirectDistributionSecondary IsNot Nothing AndAlso DirectDistributionSecondary.Id > 0 Then
                    Using ModelCommonTreasury As New MCommonCost(Me.Tag)
                        Dim result = Await ModelCommonTreasury.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, DirectDistributionSecondary.Id)
                        With DirectDistributionSecondary
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            _isLoadingControls = True
                            Year = .Year
                            Month = .Month
                            Code = .Code
                            DistributionSecondaryId = .DistributionSecondaryId
                            Description = .Description
                            Status = .Status.ToString()
                            ListDirectDistributionSecondaryDetail = .CostDirectDistributionSecondaryDetail.ToList()
                            _isLoadingControls = False
                        End With

                        Me.GetDocumentIndexed(Me.Tag & "_" & Me.DirectDistributionSecondary.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = DirectDistributionSecondary.Id}
                            Dim operation = Await ModelCommonTreasury.SaveBlockRecordCost(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            _record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(DirectDistributionSecondary.Id, MyTag, Nothing, GetType(DistributionSecondary).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)

                        AsyncLoader(False)
                        ActionsOnControls = True
                        If DirectDistributionSecondary.Status > 1 Then
                            ReadOnlyControls(True)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        End If
                        INDbteCode.Enabled = False
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Me.NewDirectDistributionSecondary()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = "No existe el registro"
                        Deshacer()
                    End If
                End If
            End Using
        End If
    End Sub

    Public Sub AssigningValues() Implements ICostDirectDistributionSecondary.AssigningValues
        With DirectDistributionSecondary
            .Code = Code
            .DistributionSecondaryId = DistributionSecondaryId
            .Description = Description
            .Year = Year
            .Month = Month
            .CostEstimationId = costStimation.Id
            .Value = Me.Value

            Dim TotalValue As Decimal = 0
            If IsDirectDistribution(DistributionSecondaryXpo) Then
                If Me.Balance <> 0 Then
                    Dim detail = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Where(Function(d) d.Value > 0).FirstOrDefault
                    detail.Value = detail.Value + Me.Balance
                    TotalValue = detail.Value
                End If
            End If

            If .CostDirectDistributionSecondaryDetail IsNot Nothing AndAlso .CostDirectDistributionSecondaryDetail.Count > 0 Then
                .Value = .CostDirectDistributionSecondaryDetail.ToList.Sum(Function(x) x.Value)
            Else
                .Value = TotalValue
            End If

        End With
    End Sub

    ''' <summary>
    ''' Lanza el calculo de distribución
    ''' </summary>
    Private Async Sub RaiseCalcDistribution()
        If DistributionSecondaryXpo IsNot Nothing Then
            Try
                Using model As New MCostDirectDistributionSecondary(Me.MyTag)
                    AsyncLoader(True)
                    Dim res = Await model.CalculateDistributionSecondary(DistributionSecondaryId, Me.Value, Year, Month)
                    If Not res.StateResult Then 'Error
                        AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = res.Message
                        Exit Sub
                    Else
                        For Each item As CostDirectDistributionSecondaryDetail In res.ObjectEmbbeded
                            DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Add(item)
                        Next
                        ListDirectDistributionSecondaryDetail = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.ToList()
                    End If
                    AsyncLoader(False)
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Mensaje(EeventViewerImages.Advertencia) = ex.Message
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Valida si el elemento del costo de distribucion secundaria es de tipo distribución directa
    ''' </summary>
    ''' <param name="distributionSecondary">Elemento del costo</param>
    ''' <returns>Valor que indica si es de tipo directo</returns>
    Private Function IsDirectDistribution(ByVal distributionSecondary As CostDistributionSecondaryXpo) As Boolean
        If distributionSecondary IsNot Nothing AndAlso distributionSecondary.CostDistributionSecondaryBaseXpo.Count = 1 AndAlso distributionSecondary.CostDistributionSecondaryBaseXpo.Any(Function(b) b.DistributionType = 1) Then
            Return True
        End If
        Return False
    End Function

#End Region

#Region "ICrud"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        CleanControlsPopup()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Me.BarraBotones.Focus()
        If DirectDistributionSecondary.Status <> 3 Then
            If ValidateControls() = True Then
                If INDgvDetail.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo un detalle"
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            AsyncLoader(True)
            Using model As New MCostDirectDistributionSecondary(MyTag)
                Dim result = Await model.SaveCostDirectDistributionSecondary(DirectDistributionSecondary, ListDeleteDirectDistributionSecondaryDetail, ListCostLogisticsProductionCenterDetail)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Me.DirectDistributionSecondary = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.Deshacer()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
            INDbteCode.Enabled = False
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewDirectDistributionSecondary()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim listItemsColumnEditStatus As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Registrado", 1))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        listItemsColumnEditStatus.Add(New Tuple(Of String, Byte)("Anulado", 3))

        Dim listMonths As New List(Of Tuple(Of String, Integer))
        listMonths.Add(New Tuple(Of String, Integer)("Enero", 1))
        listMonths.Add(New Tuple(Of String, Integer)("Febrero", 2))
        listMonths.Add(New Tuple(Of String, Integer)("Marzo", 3))
        listMonths.Add(New Tuple(Of String, Integer)("Abril", 4))
        listMonths.Add(New Tuple(Of String, Integer)("Mayo", 5))
        listMonths.Add(New Tuple(Of String, Integer)("Junio", 6))
        listMonths.Add(New Tuple(Of String, Integer)("Julio", 7))
        listMonths.Add(New Tuple(Of String, Integer)("Agosto", 8))
        listMonths.Add(New Tuple(Of String, Integer)("Septiembre", 9))
        listMonths.Add(New Tuple(Of String, Integer)("Octubre", 10))
        listMonths.Add(New Tuple(Of String, Integer)("Noviembre", 11))
        listMonths.Add(New Tuple(Of String, Integer)("Diciembre", 12))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5},
                              New ColumnInfo() With {.Caption = "Elemento", .FieldName = "DistributionSecondaryId.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.5},
                              New ColumnInfo() With {.Caption = "Mes", .FieldName = "Month", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1, .ColumnEdit = True, .ListItemsDatasourceColumEdit = listMonths},
                              New ColumnInfo() With {.Caption = "Año", .FieldName = "Year", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditStatus, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostDirectDistributionSecondary
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            INDbteCode.Enabled = False
        End If
    End Sub

#End Region

#Region "BarButton Events"

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        DirectDistributionSecondary.Status = 2
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        DirectDistributionSecondary.Status = 2
        Guardar()
    End Sub

    Private Async Sub BarraBotones_ClickConfirmarTodos() Handles BarraBotones.ClickConfirmarTodos
        If MessageIndigo.Show(String.Format("¿Está seguro que desea generar todas las distribuciones secundarias del periodo {0}-{1}?", Year, Month), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.BarraBotones.Focus()
            Try
                AsyncLoader(True)
                Using model As New MCostDirectDistributionSecondary(MyTag)
                    Dim result = Await model.GenerateDistributionSecondary(Year, Month, _idOperativeUnit)
                    AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        Me.Deshacer()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                End Using
            Catch ex As Exception
                Throw ex
                AsyncLoader(False)
                INDbteCode.Enabled = False
            End Try
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        DirectDistributionSecondary.Status = 3
        Guardar()
    End Sub

    Private Async Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        'Se valida que hayan seleccionado un elemento de distribucion secundaria
        If INDsleDistributionSecondary.EditValue Is Nothing OrElse INDsleDistributionSecondary.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un elemento de distribución secundaria"
            Exit Sub
        End If

        If costStimation Is Nothing OrElse costStimation.InitialDistribution = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El centro de producción seleccionado en el elemento de distribución secundaria no tiene estimación de costo de tipo primaria"
            Exit Sub
        End If

        'Se consultan los detalles de la tabla LogisticProductionCenterRecordDetail
        AsyncLoader(True)
        Dim result As DataTable = Await _presenter.GetDataImport(Year, Month, ProductionCenterId)
        AsyncLoader(False)

        If result Is Nothing OrElse result.Rows.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros para importar"
            Exit Sub
        End If

        For Each item In result.Rows
            Dim DirectDistributionSecondaryDetail As New CostDirectDistributionSecondaryDetail
            With DirectDistributionSecondaryDetail
                .ProductionCenterId = item("ProductionCenterId")
                .CodeNameProductionCenter = item("ProductionCenterCodeName")
                .MeasurementUnitId = item("MeasurementUnitId")
                .CodeNameMeasureUnit = item("MeasurementUniCodeName")
                .Percentage = item("Percentage")
                .Value = item("Value")
            End With
            DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.Add(DirectDistributionSecondaryDetail)
        Next

        'Se asignan los ids para enviarlos a guardar
        ListCostLogisticsProductionCenterDetail = (From x In DirectDistributionSecondary.CostDirectDistributionSecondaryDetail Select x.ProductionCenterId).ToList()
        ListDirectDistributionSecondaryDetail = DirectDistributionSecondary.CostDirectDistributionSecondaryDetail.ToList()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        DirectDistributionSecondary.Status = 1
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
        DirectDistributionSecondary.Status = 1
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

End Class