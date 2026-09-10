'***********************************************************************
' Assembly         : Presentacion.InteropCost
' Author           : Diego Andrés Roldán
' Created          : 26-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.InteropCost.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Controls.MVP
Imports DevExpress.Utils.Menu
Imports System.ComponentModel
Imports DevExpress.Data.PLinq
Imports DevExpress.XtraGrid.Columns
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports Infrastructure.Data.Xpo.InteropCostRepository

#End Region

Public Class FrmDistributionDirectExpenses
    Implements IDistributionDirectCost

#Region "Builder"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmGeneralExpenses"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _tmpCurrentPeriod = New CtrMainAccountValueDate()
        _tmpCurrentPeriod.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_tmpCurrentPeriod)
        model = New MDistributionDirectCost(Me.Tag)
        _presenter = New PDirectCost(Me)
    End Sub
#End Region

#Region "Properties"

    ''' <summary>
    ''' Valor que me sirve para poder validar al momento de realizar el lostFocus del control de valor
    ''' y calcular las distribuciones para ponerlas en la rejilla
    ''' </summary>
    Dim ValueTemp As Decimal = 0

    ''' <summary>
    ''' Listado de eliminados de los detalles
    ''' </summary>
    Dim ListDelete As List(Of DistributionDirectCostDetail)

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRoundService As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Constante que contiene  el nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "InteropCost"

    Private _settingsCost As InteropCostSetting

    Private _isLoadingControls As Boolean

    ''' <summary>
    ''' Parámetros de costos
    ''' </summary>
    Public Property SettingsCost As InteropCostSetting Implements IDistributionDirectCost.SettingsCost
        Get
            Return _settingsCost
        End Get
        Set(value As InteropCostSetting)
            _tmpCurrentPeriod.Year = value.Year
            _tmpCurrentPeriod.Month = value.Month
            _tmpCurrentPeriod.LoadDate()
            _settingsCost = value
        End Set
    End Property

    ''' <summary>
    ''' valor a distribuir previo por si la validacion sobre el valor contable sale falso
    ''' </summary>
    Private _previusValue As Decimal

    ''' <summary>
    ''' modelo
    ''' </summary>
    Private model As MDistributionDirectCost

    ''' <summary>
    ''' Control de Fecha
    ''' </summary>
    Private _tmpCurrentPeriod As CtrMainAccountValueDate

    ''' <summary>
    ''' Estado de abierto del popup de gastos generales
    ''' </summary>
    Private _stateOpenPopUpGeneralExpense As Boolean

    ''' <summary>
    ''' Estado de abierto del popup de centros de producción
    ''' </summary>
    Private _stateOpenPopUpProductionCenter As Boolean

    ''' <summary>
    ''' Obtiene el layout Control
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IDistributionDirectCost.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IDistributionDirectCost.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.InteropCostSecuence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PDirectCost

    Private _colActions As GridColumn

    ''' <summary>
    ''' entidad de gastos generales
    ''' </summary>
    Private _directCost As DistributionDirectCost

    Private _isLoaded As Boolean

    ''' <summary>
    ''' Listado de detalle de gastos directos
    ''' </summary>
    Property ListDistributionDirectCostDetail As List(Of DistributionDirectCostDetail)
        Get
            Return CType(INDgcDirectCostDetail.DataSource, List(Of DistributionDirectCostDetail))
        End Get
        Set(value As List(Of DistributionDirectCostDetail))
            INDgcDirectCostDetail.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInteropCost

#Region "Properties Entity"

    ''' <summary>
    ''' Observaciones
    ''' </summary>
    ''' <returns></returns>
    Public Property Observation As String Implements IDistributionDirectCost.Observation
        Get
            Return INDmemoObservation.EditValue
        End Get
        Set(value As String)
            INDmemoObservation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource unidades de medida
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MeasurementUnitXpo As XPInstantFeedbackSource Implements IDistributionDirectCost.MeasurementUnitXpo
        Get
            Return INDsleMeasurementUnit.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMeasurementUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del gasto directo
    ''' </summary>
    Public Property Code As String Implements IDistributionDirectCost.Code
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
    ''' Obtiene o establece el nombre del gasto directo
    ''' </summary>
    Public Property Description As String Implements IDistributionDirectCost.Description
        Get
            Return INDtxtDescription.Text
        End Get
        Set(value As String)
            INDtxtDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del gasto directo
    ''' </summary>
    Public Property GeneralExpenseId As Integer Implements IDistributionDirectCost.GeneralExpenseId
        Get
            Return INDsleGeneralExpense.EditValue
        End Get
        Set(value As Integer)
            INDsleGeneralExpense.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Mes de la distribución del gasto Directo
    ''' </summary>
    Public Property Month As Integer Implements IDistributionDirectCost.Month
        Get
            Return _tmpCurrentPeriod.Month
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Month = value
        End Set
    End Property

    ''' <summary>
    ''' Año de la distribución del gasto Directo
    ''' </summary>
    Public Property Year As Integer Implements IDistributionDirectCost.Year
        Get
            Return _tmpCurrentPeriod.Year
        End Get
        Set(value As Integer)
            _tmpCurrentPeriod.Year = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the sequence.
    ''' </summary>
    Public Property Sequence As Domain.Entities.InteropCostSecuence Implements IDistributionDirectCost.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.InteropCostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.InteropCostSecuenceDetail In Me._sequence.InteropCostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    Public Property Status As String Implements IDistributionDirectCost.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value
        End Set
    End Property

    Private Property Value As Decimal
        Get
            Return CType(INDTxtValue.EditValue, Decimal)
        End Get
        Set(value As Decimal)
            INDTxtValue.EditValue = value
        End Set
    End Property
#End Region

#Region "Datasource"

    ''' <summary>
    ''' Obtiene los gastos generales
    ''' </summary>
    Public Property GeneralExpenseXpo As XPInstantFeedbackSource Implements IDistributionDirectCost.GeneralExpenseXpo
        Get
            Return INDsleGeneralExpense.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleGeneralExpense.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the production center data source.
    ''' </summary>
    Public Property ProductionCenterXpo As XPInstantFeedbackSource Implements IDistributionDirectCost.ProductionCenterXpo
        Get
            Return INDsleProductionCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleProductionCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de centros de produccion para el repositorio
    ''' </summary>
    Public Property ProductionCenterDataSourceRerpository As XPInstantFeedbackSource Implements IDistributionDirectCost.ProductionCenterDataSourceRerpository
        Get
            Return CType(INDrpsleProductionCenter.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDrpsleProductionCenter.DataSource = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(status As eStatusResult) As String
        Set(value As String)
            If status = eStatusResult.SUCCESS Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf status = eStatusResult.WARNING Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf status = eStatusResult.EXCEPTION Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDistributionDirectCost.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleGeneralExpense.Enabled = value
            BarraBotones.StatusRecordVisible = value
            INDgcDirectCostDetail.Enabled = value
            INDTxtValue.Enabled = value
            INDmemoObservation.Enabled = value
            INDlcRoot.EndUpdate()
            If value Then
                INDsleGeneralExpense.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

#End Region

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ValueTemp = Nothing
        ListDelete = Nothing
        ListRoundService = Nothing
        _settingsCost = Nothing
        _isLoadingControls = Nothing
        _previusValue = Nothing
        model = Nothing
        _tmpCurrentPeriod = Nothing
        _stateOpenPopUpGeneralExpense = Nothing
        _stateOpenPopUpProductionCenter = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _presenter = Nothing
        _colActions = Nothing
        _directCost = Nothing
        _isLoaded = Nothing
        _record = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmDistributionDirectExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionDirectExpenses_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        Dim _listActions As New List(Of eAcciones)()
        _listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvDirectCostDetail, _listActions)
        For Each col As GridColumn In INDgvDirectCostDetail.Columns
            If col.Name = "colActions" Then
                _colActions = col
                col.Width = 130
                col.OptionsColumn.FixedWidth = True
            End If
        Next

        _presenter.LoadDefinitionLayout()
        _presenter.GetSequence()
        INDebtDistribution.AddRangeColumns("Centro Produccion", "Unidad Medida", "Cantidad", "Puntos de Valor")
        INDtxtMaximumAmount.Properties.MaxValue = Decimal.MaxValue
        InitializeTuples()
        LoadStatus()
        Deshacer()
        _isLoaded = True
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Handles the FormClosing event of the FrmDistributionDirectExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionDirectExpenses_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "Shown"
    Private Sub FrmDistributionDirectExpense_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteCode.Focus()
        _presenter.LoadSettingCost()
    End Sub

#End Region

#Region "Disposed"
    ''' <summary>
    ''' Handles the Disposed event of the FrmCategories control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmDistributionDirectExpense_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        model.Dispose()
    End Sub
#End Region

#Region "KeyDown"

    Private Sub INDTxtValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDTxtValue.KeyDown
        'If e.KeyCode = Keys.Enter AndAlso _directCost IsNot Nothing AndAlso _directCost.DistributionDirectCostDetail.Count = 0 Then
        '    RaiseCalcDistribution()
        'End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
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
                    Me.NewDirectCost()
                Else
                    LoadControls()
                End If
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddDetail_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceAddDetail.ShowPopup()
        End If
    End Sub


#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _directCostDetail As DistributionDirectCostDetail = CType(INDgvDirectCostDetail.GetFocusedRow(), DistributionDirectCostDetail)
            _directCostDetail.MarkAsDeleted()
            If _directCost.ChangeTracker.State <> ObjectState.Added Then
                _directCost.MarkAsModified()
            End If
            ListDistributionDirectCostDetail = _directCost.DistributionDirectCostDetail.ToList()
            INDgcDirectCostDetail.RefreshDataSource()
            CalculateBalance()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleGeneralExpense control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleGeneralExpense_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleGeneralExpense.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm(1210, Nothing, True)
            _presenter.InitializeXpoGeneralExpense()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Me.OpenForm("1201", Nothing, True)
            _presenter.InitializeProductionCenter(INDsleGeneralExpense.EditValue)
        End If
    End Sub

    Private Sub INDsleMeasurementUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMeasurementUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(IndigoSearchLookUpControl1.GetTagForm(INDsleMeasurementUnit), Nothing, True)
            _presenter.InitializeMeasurimentUnit(INDsleGeneralExpense.EditValue)
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionCenter.QueryPopUp
        If ProductionCenterXpo Is Nothing Then
            _presenter.InitializeProductionCenter(INDsleGeneralExpense.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de elementos del costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleGeneralExpense_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleGeneralExpense.QueryPopUp
        If GeneralExpenseXpo Is Nothing Then
            _presenter.InitializeXpoGeneralExpense()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasurementUnit.QueryPopUp
        If MeasurementUnitXpo Is Nothing Then
            _presenter.InitializeMeasurimentUnit(INDsleGeneralExpense.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDpceAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddDetail_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddDetail.QueryPopUp
        INDsleProductionCenter.Focus()
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddProductionCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddProductionCenter_Click(sender As Object, e As EventArgs) Handles INDsbAddProductionCenter.Click
        If ValidateControlsProductionCenter() Then ' AndAlso ValidateMainAccountValue() Then
            If Not ValidateProductionCenterInList(INDsleProductionCenter.EditValue, INDsleMeasurementUnit.EditValue, INDtxtCostValue.EditValue) Then
                Mensaje(eStatusResult.WARNING) = "El centro de producción " + INDsleProductionCenter.Text + " con la unidad de medida " + INDsleMeasurementUnit.Text + " tiene el mismo punto de valor en la lista"
                Exit Sub
            End If
            AddDirectExpensesDetail(CType(INDsleProductionCenter.EditValue, Integer), CType(INDsleMeasurementUnit.EditValue, Integer), CType(INDtxtMaximumAmount.EditValue, Decimal), CType(INDtxtCostValue.EditValue, Decimal), CType(INDSleRound.EditValue, Integer))
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleGeneralExpense control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleGeneralExpense_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleGeneralExpense.EditValueChanged
        If _directCost IsNot Nothing AndAlso Not _isLoadingControls Then
            _directCost.DistributionDirectCostDetail.Clear()
            ListDistributionDirectCostDetail = _directCost.DistributionDirectCostDetail.ToList()
            INDgcDirectCostDetail.RefreshDataSource()
            CleanControlsDetail()
        End If
        If GeneralExpenseId = 0 Then
            INDpceAddDetail.Enabled = False
            _tmpCurrentPeriod.Value = 0
            _tmpCurrentPeriod.Balance = 0
        Else
            ProductionCenterXpo = Nothing
            MeasurementUnitXpo = Nothing
            RaiseCalcDistribution()
        End If
        INDtxtDescription.EditValue = INDsleGeneralExpense.Text
    End Sub

    Private Sub INDTxtValue_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValue.EditValueChanged
        If INDTxtValue.EditValue IsNot Nothing AndAlso _isLoaded Then
            _tmpCurrentPeriod.Value = Me.Value
            CalculateBalance()
            If INDpceAddDetail.Enabled Then
                CalculatePercentage()
            End If
        End If
    End Sub

    Private Sub INDsleMeasurementUnit_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMeasurementUnit.EditValueChanged
        If INDsleMeasurementUnit.Properties.DataSource IsNot Nothing AndAlso INDsleMeasurementUnit.EditValue IsNot Nothing Then
            Dim obj = DirectCast(DirectCast(viewSearchMeasurementUnit.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.MeasureUnitXpo)
            'Dim obj = CType(INDsleMeasurementUnit.Properties.DataSource, List(Of InventoryMeasurementUnit)).Where(Function(o) o.Id = CInt(INDsleMeasurementUnit.EditValue)).FirstOrDefault()
            Me.INDtxtCostValue.EditValue = obj.CostValue
            Me.INDtxtCostValue.Properties.ReadOnly = Not obj.AllowEditCostValue
        End If
    End Sub

#End Region

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._directCost IsNot Nothing AndAlso Me._directCost.Id > 0 Then
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

#Region "ItemClick"

    Private Async Sub MBtnAddCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnAddCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("LoadProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Using Model As New MProductionCenter(Me.Tag)
                INDlcRoot.BeginUpdate()
                AsyncLoader(True)
                Dim listProductionCenter As List(Of ProductionCenter) = Await Model.ListProductionCenter()
                If listProductionCenter IsNot Nothing AndAlso listProductionCenter.Count > 0 Then
                    If ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0 Then
                        For Each item As DistributionDirectCostDetail In ListDistributionDirectCostDetail
                            listProductionCenter.Remove(listProductionCenter.Where(Function(x) x.Id = item.ProductionCenterId).FirstOrDefault())
                        Next
                    End If
                    For Each item As ProductionCenter In listProductionCenter
                        Dim _directExpenseDetail As New DistributionDirectCostDetail()
                        With _directExpenseDetail
                            .ProductionCenterId = item.Id
                            .Value = 0
                        End With
                        _directCost.DistributionDirectCostDetail.Add(_directExpenseDetail)
                    Next
                    ListDistributionDirectCostDetail = _directCost.DistributionDirectCostDetail.ToList()
                    INDgcDirectCostDetail.RefreshDataSource()
                End If
                AsyncLoader(False)
                INDlcRoot.EndUpdate()
            End Using
        End If
    End Sub

    Private Sub MBtnRemoveCenter_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles MBtnRemoveCenter.ItemClick
        If MessageIndigo.Show(ResourceManager.GetString("DeleteProductionCenter", MODULE_NAME), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _directCost.DistributionDirectCostDetail IsNot Nothing AndAlso _directCost.DistributionDirectCostDetail.Count > 0 Then
                While _directCost.DistributionDirectCostDetail.Count > 0
                    _directCost.DistributionDirectCostDetail(_directCost.DistributionDirectCostDetail.Count - 1).MarkAsDeleted()
                End While
                ListDistributionDirectCostDetail = _directCost.DistributionDirectCostDetail.ToList()
                INDgcDirectCostDetail.RefreshDataSource()
            End If
        End If
    End Sub

#End Region

#Region "DatasourceChanged"

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcDirectCostDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcDirectCostDetail_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcDirectCostDetail.DataSourceChanged
        INDsleGeneralExpense.Properties.ReadOnly = ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0
    End Sub

#End Region

#Region "PasteToGrid"

    Private Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        'TODO: Cambiar esta consulta, porque ya no se trae toda las entidades sino que se realiza paso a paso con xpo Att Carlos Mario
        'Dim generalExpense As GeneralExpense = Me.GeneralExpenseDatasource.Where(Function(o) o.Id = GeneralExpenseId).FirstOrDefault()
        'Dim generalExpense As GeneralExpense = Nothing

        'Se valida que se hayan seleccionado un id del elemento del costo
        If INDsleGeneralExpense.EditValue Is Nothing OrElse INDsleGeneralExpense.EditValue = 0 Then
            Mensaje(eStatusResult.WARNING) = "Debe seleccionar un elemento del costo"
            Exit Sub
        End If

        'Se obtiene el elemento del costo
        Dim generalExpense As GeneralExpenseXpo = _presenter.GetGeneralExpenseById(INDsleGeneralExpense.EditValue)
        If generalExpense IsNot Nothing Then
            If IsDirectDistribution(generalExpense) Then
                If e.Rows IsNot Nothing AndAlso e.Rows.Count > 0 AndAlso e.Rows(0).Count >= 3 Then

                    Dim ListProductionCenterXpo = (From x In generalExpense.DistributionBaseXpo(0).DistributionBaseDetailXpo Select x.ProductionCenterId).ToList
                    Dim ListMeasurementUnitXpo = (From x In generalExpense.DistributionBaseXpo(0).DistributionBaseMeasurementUnitXpo Select x.MeasurementUnitId).ToList

                    Dim line As Integer = 0
                    Dim listMessage As New List(Of Tuple(Of Int32, String))()
                    For Each row As List(Of String) In e.Rows
                        line += 1
                        Dim idProductionCenter As Integer = 0
                        Dim idMUnit As Integer = 0
                        Dim count As Decimal = 0
                        Dim costValue As Decimal = 0
                        If row(0).Trim() <> String.Empty AndAlso ListProductionCenterXpo IsNot Nothing AndAlso ListProductionCenterXpo.Any(Function(o) o.Code.Equals(row(0).Trim())) Then
                            Dim codeNameProductionCenter = ListProductionCenterXpo.Where(Function(o) o.Code.Equals(row(0).Trim())).FirstOrDefault().CodeName
                            idProductionCenter = ListProductionCenterXpo.Where(Function(o) o.Code.Equals(row(0).Trim())).FirstOrDefault().Id
                            If row(1).Trim() <> String.Empty AndAlso ListMeasurementUnitXpo IsNot Nothing AndAlso ListMeasurementUnitXpo.Any(Function(o) o.Code.Equals(row(1).Trim())) Then
                                Dim mUnit As InventoryInventoryMeasurementUnitReportXpo = ListMeasurementUnitXpo.Where(Function(o) o.Code.Equals(row(1).Trim())).FirstOrDefault()
                                idMUnit = mUnit.Id
                                Dim auxDec As Decimal = 0
                                If row(2).Trim() <> String.Empty AndAlso Decimal.TryParse(row(2).Trim(), Globalization.NumberStyles.AllowDecimalPoint, indigo.Culture, auxDec) Then
                                    count = auxDec
                                Else
                                    'Se documenta el error
                                    listMessage.Add(New Tuple(Of Int32, String)(line, "La cantidad especificada no tiene un formato numérico válido"))
                                    Continue For
                                End If
                                If mUnit.AllowEditCostValue Then
                                    If row.Count = 4 AndAlso row(3).Trim() <> String.Empty AndAlso Decimal.TryParse(row(3).Trim(), Globalization.NumberStyles.AllowDecimalPoint, indigo.Culture, auxDec) Then
                                        costValue = auxDec
                                    Else
                                        'Se documenta el error
                                        listMessage.Add(New Tuple(Of Int32, String)(line, "Los puntos de valor especificados no tiene un formato numérico válido"))
                                        Continue For
                                    End If
                                Else
                                    costValue = mUnit.CostValue
                                    If row.Count = 4 AndAlso row(3).Trim() <> String.Empty Then
                                        'Se documenta la advertencia
                                        listMessage.Add(New Tuple(Of Int32, String)(line, "La unidad de medida no permite modificar sus puntos de valor"))
                                    End If
                                End If
                                If Not ValidateProductionCenterInList(idProductionCenter, idMUnit, costValue) Then

                                    'Cuando el centro de produccion con la unidad de medida y los puntos de valor ya existe en el listado lo que se hace es sumar sus valores
                                    If ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0 Then

                                        'Se obtiene el registro para actualizarlo
                                        Dim info = (From x In ListDistributionDirectCostDetail Where x.ProductionCenterId = idProductionCenter And x.MeasurementUnitId = idMUnit And x.CostValue = costValue Select x).FirstOrDefault

                                        'Se actualizan los campos
                                        info.Count += count
                                        info.Value = Infrastructure.CrossCutting.Base.Utils.RoundValue((info.Count * info.CostValue), 1)
                                        INDgcDirectCostDetail.RefreshDataSource()
                                        CalculateBalance()
                                        CalculatePercentage()
                                        CleanControlsDetail()
                                        Continue For
                                    End If

                                    'Se comentan estas lineas a petición de Christian Salazar porque en el hospital de neiva se lo piedieron att: Carlos Mario
                                    'listMessage.Add(New Tuple(Of Int32, String)(line, "El centro de producción " + codeNameProductionCenter + " con la unidad de medida " + mUnit.Code + " - " + mUnit.Name + " tiene el mismo punto de valor en la lista"))
                                    'Continue For
                                End If
                                AddDirectExpensesDetail(idProductionCenter, idMUnit, count, costValue, 0, codeNameProductionCenter, mUnit.Code + " - " + mUnit.Name)
                            Else
                                'Se documenta el error
                                listMessage.Add(New Tuple(Of Int32, String)(line, "La unidad de medida no existe o no esta asignada al elemento del costo"))
                                Continue For
                            End If
                        Else
                            'Se documenta el error
                            listMessage.Add(New Tuple(Of Int32, String)(line, "El centro de producción no existe o no esta asignado al elemento del costo"))
                            Continue For
                        End If
                    Next
                    If listMessage.Count > 0 Then
                        Dim frmMessage As New FrmMessagesImport()
                        frmMessage.INDgcMessages.DataSource = listMessage
                        Using tras As New FrmTransparent(frmMessage, False)
                            tras.ShowDialog(Me)
                        End Using
                    End If
                Else
                    Me.Mensaje(eStatusResult.WARNING) = "El formato de la estructura usada para importar los datos NO ES CORRECTO!!"
                End If
            Else
                Me.Mensaje(eStatusResult.WARNING) = "El elemento del costo seleccionado no tiene una distribución directa"
            End If
        Else
            Me.Mensaje(eStatusResult.WARNING) = "Debe seleccionar un elemento del costo"
        End If
    End Sub

#End Region

#Region "LostFocus"

    ''' <summary>
    ''' Evento que se dispara al perder el foco el control de valor
    ''' </summary>
    Private Sub INDTxtValue_LostFocus() Handles INDTxtValue.LostFocus
        If Value <> ValueTemp AndAlso INDpceAddDetail.Enabled = False Then
            If _directCost.Id > 0 AndAlso _directCost.DistributionDirectCostDetail IsNot Nothing AndAlso _directCost.DistributionDirectCostDetail.Count > 0 Then
                If ListDelete Is Nothing Then
                    ListDelete = New List(Of DistributionDirectCostDetail)
                End If
                For Each item In (From x In _directCost.DistributionDirectCostDetail Where x.Id > 0).ToList()
                    ListDelete.Add(item)
                Next
            End If
            _directCost.DistributionDirectCostDetail.Clear()
            RaiseCalcDistribution()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub InitializeTuples()
        'Redondeo
        ListRoundService = New List(Of Tuple(Of Integer, String))
        ListRoundService.Add(New Tuple(Of Integer, String)(0, "Ninguno"))
        ListRoundService.Add(New Tuple(Of Integer, String)(1, "A la Unidad"))
        ListRoundService.Add(New Tuple(Of Integer, String)(10, "A la Décima"))
        ListRoundService.Add(New Tuple(Of Integer, String)(100, "A la Centésima"))
        ListRoundService.Add(New Tuple(Of Integer, String)(1000, "A la Milésima"))
        INDSleRound.Properties.DataSource = ListRoundService.ToList
    End Sub

    ''' <summary>
    ''' Lanza el calculo de distribución
    ''' </summary>
    Private Async Sub RaiseCalcDistribution()
        'TODO: Cambiar esta consulta, porque ya no se trae toda las entidades sino que se realiza paso a paso con xpo Att Carlos Mario
        'Dim generalExpense As GeneralExpense = Me.GeneralExpenseDatasource.Where(Function(o) o.Id = GeneralExpenseId).FirstOrDefault()

        'Se obtiene la entidad xpo de elementos del costo
        Dim generalExpense As GeneralExpenseXpo = _presenter.GetGeneralExpenseById(INDsleGeneralExpense.EditValue)
        If generalExpense IsNot Nothing Then
            If IsDirectDistribution(generalExpense) Then
                INDpceAddDetail.Enabled = True
                IndigoGridView1.RaiseMenuPopUp = True
                'Me.ProductionCenterDataSource = ListProductionCentersByGeneralExpenseId(GeneralExpenseId)
                'Me.INDsleMeasurementUnit.Properties.DataSource = ListMeasurmentUnitByGeneralExpenseId(GeneralExpenseId)
                Me.ColMeasurementUnit.Visible = True
                Me.ColCount.Visible = True
                Me.ColCostValue.Visible = True
                Me.ColTotal.Visible = True
                Me.ColPercentage.Visible = True
                Me._colActions.Visible = True
                Me.ColProductionCenter.VisibleIndex = 0
                Me.ColMeasurementUnit.VisibleIndex = 1
                Me.ColCount.VisibleIndex = 2
                Me.ColCostValue.VisibleIndex = 3
                Me.ColTotal.VisibleIndex = 4
                Me.ColPercentage.VisibleIndex = 5
                Me._colActions.VisibleIndex = 6
            Else
                INDpceAddDetail.Enabled = False
                Me._colActions.Visible = False
                Me.ColCostValue.Visible = False
                Me.ColMeasurementUnit.Visible = False
                Me.ColCount.Visible = False
                IndigoGridView1.RaiseMenuPopUp = False
                'Aqui realizo la distribución por centro de producción
                If Me.Value > 0 Then
                    Using model As New MDistributionDirectCost(Me.MyTag)
                        AsyncLoader(True)
                        Dim res = Await model.CalcDistribution(INDsleGeneralExpense.EditValue, Me.Value, Year, Month, indigo.InteropCostContainer)
                        If Not res.StateResult Then 'Error
                            AsyncLoader(False)
                            Me.Mensaje(eStatusResult.WARNING) = res.Message
                            Exit Sub
                        Else
                            For Each item As DistributionDirectCostDetail In res.ObjectEmbbeded
                                _directCost.DistributionDirectCostDetail.Add(item)
                                Dim obj As DistributionDirectCostValues = IIf(_directCost.DistributionDirectCostValues.Any(Function(o) o.ProductionCenterId = item.ProductionCenterId), _directCost.DistributionDirectCostValues.Where(Function(o) o.ProductionCenterId = item.ProductionCenterId).FirstOrDefault(), New DistributionDirectCostValues())
                                obj.ByArea = item.ByArea
                                obj.BySupplyValue = item.BySupplyValue
                                obj.ByWorkmanship = item.ByWorkmanship
                                obj.ByAssetValue = item.ByAssetValue
                                obj.BySales = item.BySales
                                obj.ByOfficialHours = item.ByOfficialHours
                                If Not _directCost.DistributionDirectCostValues.Any(Function(o) o.ProductionCenterId = item.ProductionCenterId) Then
                                    obj.ProductionCenterId = item.ProductionCenterId
                                    _directCost.DistributionDirectCostValues.Add(obj)
                                End If
                                'Se coge la cuenta contable y centro de costo de la primera distribución, porque se supone que para todas las distribuciones se agregan los mismos centros de producción
                                Dim detail = generalExpense.DistributionBaseXpo(0).DistributionBaseDetailXpo.Where(Function(o) o.ProductionCenterId.Id = item.ProductionCenterId).FirstOrDefault()
                                If detail IsNot Nothing Then
                                    If detail.CostCenterId <> 0 Then
                                        item.CostCenterId = detail.CostCenterId
                                    End If
                                    item.MainAccountId = detail.MainAccountId
                                End If
                            Next
                            ListDistributionDirectCostDetail = _directCost.DistributionDirectCostDetail.ToList()
                            INDgcDirectCostDetail.RefreshDataSource()
                            _tmpCurrentPeriod.Value = Me.Value
                            CalculateBalance()
                            CalculatePercentage()
                            ValueTemp = Value
                        End If
                        AsyncLoader(False)
                    End Using
                End If
            End If
        End If
    End Sub

    Private Sub CalculateBalance()
        Dim sum As Decimal = 0
        If _directCost IsNot Nothing AndAlso _directCost.DistributionDirectCostDetail IsNot Nothing Then
            sum = _directCost.DistributionDirectCostDetail.Sum(Function(o) o.Value)
        End If
        Dim sumBalance As Decimal = 0
        If _tmpCurrentPeriod.Value > 0 And (sum <= _tmpCurrentPeriod.Value) Then
            sumBalance = (_tmpCurrentPeriod.Value - sum)
        End If
        '_tmpCurrentPeriod.Balance = IIf(_tmpCurrentPeriod.Value > 0 And (sum <= _tmpCurrentPeriod.Value), (_tmpCurrentPeriod.Value - sum), 0)
        _tmpCurrentPeriod.Balance = sumBalance
    End Sub

    ''' <summary>
    ''' Valida si el elemento del costo es de tipo distribución directa
    ''' </summary>
    ''' <param name="generalExpense">Elemento del costo</param>
    ''' <returns>Valor que indica si es de tipo directo</returns>
    Private Function IsDirectDistribution(ByVal generalExpense As GeneralExpenseXpo) As Boolean
        If generalExpense IsNot Nothing AndAlso generalExpense.DistributionBaseXpo.Count = 1 AndAlso generalExpense.DistributionBaseXpo(0).DistributionType = 1 Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' Nuevo Distribucion de gastos directos
    ''' </summary>
    Private Async Sub NewDirectCost()
        If Sequence Is Nothing OrElse Sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If _settingsCost Is Nothing OrElse _settingsCost.Id = 0 Then
            Exit Sub
        End If
        Me._directCost = New DistributionDirectCost()
        Status = "1" 'Sin confirmar
        If Me._sequence.IsManual Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.InteropCostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequence = Me._sequence.InteropCostSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).FirstOrDefault().Id
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
                        Using model As New MCommonInteropCost(Me.Tag)
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
    End Sub

    ''' <summary>
    ''' Imports the previus data.
    ''' </summary>
    Private Async Sub ImportPreviusData()
        Dim _settingsCostQ As InteropCostSetting
        Using ModelSetting As New MInteropCostSetting(Me.Tag)
            _settingsCostQ = ModelSetting.GetInteropCostSetting()
        End Using
        If _settingsCostQ IsNot Nothing Then
            Dim _previusMonth As Integer = _settingsCostQ.Month - 1
            Dim _previusYear As Integer = _settingsCostQ.Year
            If _previusMonth = 0 Then
                _previusMonth = 12
                _previusYear -= 1
            End If
            Dim errorList As New StringBuilder()

            Dim _listDistributionImport As List(Of DistributionDirectCost) = Await model.ListDistributionDirectCostByYearMonth(_previusYear, _previusMonth)
            If _listDistributionImport IsNot Nothing Then
                For Each item As DistributionDirectCost In _listDistributionImport
                    Dim _distrib As New DistributionDirectCost()
                    _distrib = item.Clone()
                    _distrib.Id = 0
                    _distrib.MarkAsAdded()
                    For Each itemDetail As DistributionDirectCostDetail In _distrib.DistributionDirectCostDetail
                        itemDetail.Id = 0
                        itemDetail.MarkAsAdded()
                    Next
                    Dim Result = Await model.SaveDistributionDirectCost(item, Me._idCurrentSequence)
                    If Result.StateResult = False Then
                        errorList.AppendLine(Result.Message)
                    End If
                Next

                If errorList.Length > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
                Else
                    Mensaje(EeventViewerImages.Informacion) = "Se importaron los datos Correctamente"
                End If

            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron parámetros de costos"
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonInteropCost As New MCommonInteropCost(Me.Tag)
                Await ModelCommonInteropCost.DeleteBlockRecordInteropCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' valida los controles de centros de producción
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsProductionCenter() As Boolean
        Dim errorList As New StringBuilder()
        If CType(INDsleMeasurementUnit.EditValue, Decimal) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Unidad de Medida"))
        End If
        If INDtxtMaximumAmount.EditValue Is Nothing Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Cantidad"))
        End If
        If CType(INDsleProductionCenter.EditValue, Integer) = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), ResourceManager.GetString("ProductionCenter", MODULE_NAME)))
        End If
        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Adds the direct expenses detail.
    ''' </summary>
    Private Sub AddDirectExpensesDetail(ByVal idProductionCenter As Integer, ByVal idMeasurementUnit As Integer, ByVal count As Decimal, ByVal costValue As Decimal, roundType As Integer,
                                        Optional ProductionCenterCodeName As String = "", Optional MeasurementUnitCodeName As String = "")
        Dim roundTypeFinal As Infrastructure.CrossCutting.Base.Utils.RoundLevel = Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit
        If roundType = 1 Then
            roundTypeFinal = Infrastructure.CrossCutting.Base.Utils.RoundLevel.Unit
        ElseIf roundType = 10 Then
            roundTypeFinal = Infrastructure.CrossCutting.Base.Utils.RoundLevel.Ten
        ElseIf roundType = 100 Then
            roundTypeFinal = Infrastructure.CrossCutting.Base.Utils.RoundLevel.Hundred
        ElseIf roundType = 1000 Then
            roundTypeFinal = Infrastructure.CrossCutting.Base.Utils.RoundLevel.Thousands
        End If
        'TODO: Cambiar esta consulta, porque ya no se trae toda las entidades sino que se realiza paso a paso con xpo Att Carlos Mario
        'Dim generalExpense As GeneralExpense = Me.GeneralExpenseDatasource.Where(Function(o) o.Id = GeneralExpenseId).FirstOrDefault()
        Dim generalExpense = _presenter.GetGeneralExpenseById(INDsleGeneralExpense.EditValue)
        Dim _directExpenseDetail As New DistributionDirectCostDetail()
        With _directExpenseDetail
            .ProductionCenterId = idProductionCenter
            If ProductionCenterCodeName = "" Then
                .ProductionCenterCodeName = INDsleProductionCenter.Text
            Else
                .ProductionCenterCodeName = ProductionCenterCodeName
            End If
            .MeasurementUnitId = idMeasurementUnit
            If MeasurementUnitCodeName = "" Then
                .MeasurementUnitCodeName = INDsleMeasurementUnit.Text
            Else
                .MeasurementUnitCodeName = MeasurementUnitCodeName
            End If
            '.InventoryMeasurementUnit = CType(INDsleMeasurementUnit.Properties.DataSource, List(Of InventoryMeasurementUnit)).Where(Function(o) o.Id = .MeasurementUnitId).FirstOrDefault()
            .Count = count
            .CostValue = costValue
            .Value = Infrastructure.CrossCutting.Base.Utils.RoundValue((.Count * .CostValue), roundTypeFinal)
            Dim detail = generalExpense.DistributionBaseXpo(0).DistributionBaseDetailXpo.Where(Function(o) o.ProductionCenterId.Id = .ProductionCenterId).FirstOrDefault()
            If detail IsNot Nothing Then
                If detail.CostCenterId <> 0 Then
                    .CostCenterId = detail.CostCenterId
                End If
                .MainAccountId = detail.MainAccountId
            End If
        End With
        _directCost.DistributionDirectCostDetail.Add(_directExpenseDetail)
        ListDistributionDirectCostDetail = _directCost.DistributionDirectCostDetail.ToList()
        INDgcDirectCostDetail.RefreshDataSource()
        CalculateBalance()
        CalculatePercentage()
        CleanControlsDetail()
    End Sub

    ''' <summary>
    ''' Valida que el centro de produccion a agregar no esté ya agregado
    ''' </summary>
    Private Function ValidateProductionCenterInList(idProductionCenter As Integer, idMUnit As Integer, costVal As Decimal) As Boolean
        If ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0 Then
            Return Not ListDistributionDirectCostDetail.Any(Function(x) x.ProductionCenterId = idProductionCenter And x.MeasurementUnitId = idMUnit And x.CostValue = costVal)
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Limpia los controles del detalle del gasto directo
    ''' </summary>
    Private Sub CleanControlsDetail()
        INDsleProductionCenter.EditValue = Nothing
        INDtxtMaximumAmount.EditValue = 0
        INDsleMeasurementUnit.EditValue = Nothing
        Me.INDtxtCostValue.EditValue = Nothing
        INDsleProductionCenter.Focus()
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._directCost.Code, Me._directCost.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._directCost.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._directCost.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._directCost.Code, Me._directCost.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._directCost.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Asigna los valores a los campos de la entidad
    ''' </summary>
    Public Sub AssigningValues() Implements IDistributionDirectCost.AssigningValues
        With _directCost
            .Code = Code
            .GeneralExpenseId = GeneralExpenseId
            .Description = Description
            .Year = Year
            .Month = Month
            .Value = Math.Round(Value, 0)
            .Observation = Observation

            If ListDelete IsNot Nothing AndAlso ListDelete.Count > 0 Then
                ListDelete.ForEach(Sub(x) .DistributionDirectCostDetail.Add(x.MarkAsDeleted()))
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IDistributionDirectCost.CleanControls
        INDlcRoot.BeginUpdate()
        ReadOnlyControls(False, INDlcRoot)
        ActionsOnControls = False
        Code = String.Empty
        GeneralExpenseId = Nothing
        Description = Nothing
        Year = Nothing
        Month = Nothing
        Value = CDec(0)
        Status = Nothing
        Observation = String.Empty
        ProductionCenterXpo = Nothing
        MeasurementUnitXpo = Nothing
        INDpceAddDetail.Enabled = False

        INDsleGeneralExpense.Properties.ReadOnly = False

        IndigoGridView1.RaiseMenuPopUp = True

        _stateOpenPopUpGeneralExpense = False
        _stateOpenPopUpProductionCenter = False
        ListDistributionDirectCostDetail = Nothing
        ListDelete = Nothing
        ValueTemp = 0
        _tmpCurrentPeriod.Value = 0
        _tmpCurrentPeriod.Balance = 0
        INDsleGeneralExpense.Properties.NullText = String.Empty

        _directCost = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If FormSearchObjects Is Nothing OrElse FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If

        INDlcRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Carga los datos en los controles
    ''' </summary>
    Public Async Sub LoadControls() Implements IDistributionDirectCost.LoadControls
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If
            INDlcRoot.BeginUpdate()
            AsyncLoader(True)
            Await _presenter.InitializeGeneralExpenses()
            Dim res = Await model.GetDistributionDirectCost(Me.Code)
            If res.StatusCode <> eStatusResult.SUCCESS Then
                Mensaje(res.StatusCode) = res.Message
                AsyncLoader(False)
                Exit Sub
            End If
            _directCost = res.ObjectEmbbeded
            If _directCost IsNot Nothing AndAlso _directCost.Id > 0 Then
                Using ModelCommonTreasury As New MCommonInteropCost(Me.Tag)
                    Dim result = Await ModelCommonTreasury.GetBlockRecordInteropCostByIdformAndIdRecord(Me.Tag, _directCost.Id)

                    With _directCost
                        _isLoadingControls = True
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Code = .Code
                        Year = .Year
                        Month = .Month
                        GeneralExpenseId = .GeneralExpenseId
                        INDsleGeneralExpense.Properties.NullText = _directCost.FullNameGeneralExpense
                        ListDistributionDirectCostDetail = _directCost.DistributionDirectCostDetail.ToList()
                        Me.Value = .Value
                        ValueTemp = .Value
                        Observation = .Observation
                        Status = .Status.ToString()
                        _isLoadingControls = False
                        Description = .Description
                    End With
                    _tmpCurrentPeriod.LoadDate()
                    CalculateBalance()

                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._directCost.Code)
                    If result.Id = 0 Then
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        _record = New BlockRecordInteropCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _directCost.Id}
                        Dim operation = Await ModelCommonTreasury.SaveBlockRecordInteropCost(_record)
                        _record = operation.ObjectEmbbeded
                    Else
                        _record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(_directCost.Id, MyTag, Nothing, GetType(DistributionDirectCost).Name)
                    If _directCost.Status = 1 Then
                        IndigoGridView1.RaiseMenuPopUp = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    Else
                        IndigoGridView1.RaiseMenuPopUp = False
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        ReadOnlyControls(True)
                    End If
                    Dim _settingsCostQ As InteropCostSetting
                    Using ModelSetting As New MInteropCostSetting(Me.Tag)
                        _settingsCostQ = ModelSetting.GetInteropCostSetting()
                    End Using
                    If Year <> _settingsCostQ.Year OrElse Month <> _settingsCostQ.Month Then
                        'Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Dim dtfi = indigo.Culture.DateTimeFormat
                        Dim xtraMessage As String = String.Format(ResourceManager.GetString("RegisterNotInPeriod", MODULE_NAME), Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(_settingsCostQ.Month), Microsoft.VisualBasic.VbStrConv.ProperCase), _settingsCostQ.Year)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, "")
                        ReadOnlyControls(True, INDlcRoot)
                    End If

                    AsyncLoader(False)
                    ActionsOnControls = True
                    INDbteCode.Enabled = False
                End Using
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, _directCost.Id, 0, _directCost.Id, _idOperativeUnit)
            Else
                AsyncLoader(False)
                If Me._sequence.IsManual Then
                    Me.NewDirectCost()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    Deshacer()
                End If
            End If
            INDlcRoot.EndUpdate()
        End If
    End Sub

    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, _directCost.Id, 0, _directCost.Id, _idOperativeUnit))
                                     End Sub)
    End Function

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _directCost.Id, 0, _directCost.Id, _idOperativeUnit)
    End Sub

    ''' <summary>
    ''' Calcula los porcentajes de cada item
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalculatePercentage()
        If ListDistributionDirectCostDetail IsNot Nothing AndAlso ListDistributionDirectCostDetail.Count > 0 Then
            Dim totalValue As Decimal = Value
            Dim sumTotalValue As Decimal = (From x In ListDistributionDirectCostDetail Select x.Value).Sum()
            Dim percentageTmp As Decimal = 0.0000
            Dim valueTmp As Decimal = 0.00
            ListDistributionDirectCostDetail.ForEach(Sub(item)
                                                         If Value > 0 Then
                                                             'item.Percentage = Math.Round(((item.Value * 100) / totalValue), 2)
                                                             item.Percentage = BaseClass.TruncateDecimal(((item.Value * 100) / totalValue), 2)
                                                             percentageTmp += item.Percentage
                                                             valueTmp += item.Value
                                                         Else
                                                             item.Percentage = 0
                                                         End If
                                                     End Sub)
            If percentageTmp > 100 And sumTotalValue >= totalValue Then
                ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage - (percentageTmp - 100)
                percentageTmp -= (percentageTmp - 100)
            ElseIf percentageTmp < 100 And sumTotalValue >= totalValue Then
                ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage + (100 - percentageTmp)
                percentageTmp += (100 - percentageTmp)
            End If
            If percentageTmp <> 100 AndAlso sumTotalValue > totalValue Then 'Se debe corregir también el valor asi como el porcentaje por petición de Christian Salazar
                'Se organiza el listado de mayor a menor según el valor
                ListDistributionDirectCostDetail = (From x In ListDistributionDirectCostDetail Order By x.Value Descending Select x).ToList()
                'Se saca el primer valor y se le resta la diferencia
                ListDistributionDirectCostDetail(0).Value -= (sumTotalValue - totalValue)
            End If

            If Math.Round(_tmpCurrentPeriod.Balance, 0) = 0 AndAlso valueTmp <> Value AndAlso percentageTmp <> 100 AndAlso valueTmp < Value Then
                ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Value = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Value + (Value - valueTmp)
                ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage = ListDistributionDirectCostDetail.Item(ListDistributionDirectCostDetail.Count - 1).Percentage + (100 - percentageTmp)
            End If

            INDgcDirectCostDetail.RefreshDataSource()
        End If
    End Sub

    Private Function ListProductionCentersByGeneralExpenseId(p1 As Integer) As List(Of ProductionCenter)
        Dim list As New List(Of ProductionCenter)()
        'TODO: Cambiar esta consulta, porque ya no se trae toda las entidades sino que se realiza paso a paso con xpo Att Carlos Mario
        'For Each disbase As DistributionBaseDetail In Me.GeneralExpenseDatasource.Where(Function(o) o.Id = p1).FirstOrDefault().DistributionBase(0).DistributionBaseDetail
        '    list.Add(disbase.ProductionCenter)
        'Next
        Return list
    End Function

    Private Function ListMeasurmentUnitByGeneralExpenseId(p1 As Integer) As List(Of InventoryMeasurementUnit)
        Dim list As New List(Of InventoryMeasurementUnit)()
        'TODO: Cambiar esta consulta, porque ya no se trae toda las entidades sino que se realiza paso a paso con xpo Att Carlos Mario
        'For Each disbase As DistributionBaseMeasurementUnit In Me.GeneralExpenseDatasource.Where(Function(o) o.Id = p1).FirstOrDefault().DistributionBase(0).DistributionBaseMeasurementUnit
        '    list.Add(disbase.InventoryMeasurementUnit)
        'Next
        Return list
    End Function

#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Eliminars this instance.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Me._directCost IsNot Nothing AndAlso Me._directCost.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Dim result = Await model.DeleteDistributionDirectCost(Me._directCost)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbteCode.Enabled = False
                    End If
                    Mensaje(result.StatusCode) = result.Message
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar
        SaveAndConfirm(1)
    End Sub

    ''' <summary>
    ''' Guarda o actualiza el registro
    ''' </summary>
    ''' <param name="statusRecord">Estado del registro</param>
    Private Async Sub SaveAndConfirm(ByVal statusRecord As Byte)
        Me.BarraBotones.Focus()
        If ValidateControls() = True Then
            If INDgvDirectCostDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una información detallada"
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        _directCost.Status = statusRecord
        Try
            AsyncLoader(True)
            Dim result = Await model.SaveDistributionDirectCost(Me._directCost, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If _directCost.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._sequence.InteropCostSecuenceDetail(0).Id).RemoveAt(0)
                    End If
                End If
                Me._directCost = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                AsyncLoader(False)
                INDbteCode.Enabled = False
            End If
            Mensaje(result.StatusCode) = result.Message
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewDirectCost()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue

        Dim ListItems As New List(Of Tuple(Of String, Byte))
        ListItems.Add(New Tuple(Of String, Byte)("Sin Confirmar", 1))
        ListItems.Add(New Tuple(Of String, Byte)("Confirmado", 2))
        ListItems.Add(New Tuple(Of String, Byte)("Anulado", 3))

        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Elemento Costo", .FieldName = "GeneralExpenseId.CodeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Año", .FieldName = "Year", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Mes", .FieldName = "Month", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListItems}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListDistributionDirectCost
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
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub
#End Region

#Region "BarButton Events"
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
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveAndConfirm(2)
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveAndConfirm(2)
    End Sub

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        SaveAndConfirm(2)
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        SaveAndConfirm(3)
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