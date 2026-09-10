#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors.Controls
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CostRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Cost.MVP

#End Region

Public Class FrmCostActivity
    Implements ICostActivity

#Region "Builder"

    Public Sub New()
        InitializeComponent()
    End Sub

#End Region

#Region "Globals"

    Public Const MODULE_NAME As String = "Cost"

    Private _operativeUnitId As Int32

    Private _sequence As Domain.Entities.CostSecuence

    Private _currentSequenceId As Int64

    Private _presenter As PCostActivity

    Private _record As BlockRecordCost

    Private _costActivity As CostActivity

    Private _listCostActivityProductionCenter As List(Of CostActivityProductionCenter)

    Private _listCostActivityStep As List(Of CostActivityStep)

    Private _listCostActivityStepFixedAsset As List(Of CostActivityStepFixedAsset)

    Private _listCostActivityStepPayroll As List(Of CostActivityStepPayroll)

    Private _listCostActivityStepInventory As List(Of CostActivityStepInventory)

    Private _listCostActivityStepAddictionalCost As List(Of CostActivityStepAddictionalCost)

    ''' <summary>
    ''' Control de Conciliación Automática Bancaria
    ''' </summary>
    Private _ctrTmp As New CtrStandarCost()
    ''' <summary>
    ''' Almacenan el año y mes actual del módulo de costos
    ''' </summary>
    Private _currentYearCost As Integer
    Private _currentMonthCost As Integer

    ''' <summary>
    ''' Variable que contiene la abreviación de la moneda
    ''' </summary>
    Private _currencyAbbreviation As String

#End Region

#Region "Fields"

    Public ReadOnly Property MyTag As Object Implements ICostActivity.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICostActivity.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements ICostActivity.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()

            INDbeCode.Enabled = Not value
            INDteName.Enabled = value
            INDsleCUPSEntity.Enabled = value
            INDdeInitialDate.Enabled = value
            INDdeEndDate.Enabled = value
            INDmeDescription.Enabled = value

            INDpceAddProductionCenter.Enabled = value
            INDpceAddStep.Enabled = value
            INDpceAddFixedAsset.Enabled = value
            INDpceAddPayroll.Enabled = value
            INDpceAddInventory.Enabled = value
            INDpceAddAddictionalCost.Enabled = value

            INDgcProductionCenter.Enabled = value
            INDgcStep.Enabled = value
            INDgcFixedAsset.Enabled = value
            INDgcPayroll.Enabled = value
            INDgcInventory.Enabled = value
            INDgcAddictionalCost.Enabled = value

            Me.BarraBotones.StatusRecordVisible = value

            INDlcRoot.EndUpdate()
            If value Then
                INDteName.Focus()
            Else
                INDbeCode.Focus()
            End If
        End Set
    End Property

    Public Property Sequence As CostSecuence Implements ICostActivity.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As CostSecuence)
            _sequence = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.CostSecuenceDetail In Me._sequence.CostSecuenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String))
            Next
        End Set
    End Property

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

#End Region

#Region "Properties"

    Public Property Code As String Implements ICostActivity.Code
        Get
            If INDbeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbeCode.Text
            End If
        End Get
        Set(value As String)
            INDbeCode.Text = value
        End Set
    End Property

    Public Property ActivityName As String Implements ICostActivity.Name
        Get
            Return INDteName.Text
        End Get
        Set(value As String)
            INDteName.Text = value
        End Set
    End Property

    Public Property CUPSEntityId As Integer Implements ICostActivity.CUPSEntityId
        Get
            Return INDsleCUPSEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleCUPSEntity.EditValue = value
        End Set
    End Property

    Public Property InitialDate As Date? Implements ICostActivity.InitialDate
        Get
            Return INDdeInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdeInitialDate.EditValue = value
        End Set
    End Property

    Public Property EndDate As Date? Implements ICostActivity.EndDate
        Get
            Return INDdeEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdeEndDate.EditValue = value
        End Set
    End Property

    Public Property Description As String Implements ICostActivity.Description
        Get
            Return INDmeDescription.Text
        End Get
        Set(value As String)
            INDmeDescription.Text = value
        End Set
    End Property

    Public Property Status As Boolean Implements ICostActivity.Status
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
    ''' formato de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyNumbertFormat As Globalization.NumberFormatInfo

#End Region

#Region "XPO"

    Public Property CUPSEntityXpo As XPInstantFeedbackSource Implements ICostActivity.CUPSEntityXpo
        Get
            Return CType(INDsleCUPSEntity.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCUPSEntity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Crud"

    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7},
                              New ColumnInfo() With {.Caption = "Servicio", .FieldName = "CUPSEntityId.CodeDescription", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCostActivities
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
    End Sub

    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewCostActivity()
        End If
    End Sub

    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If Me._listCostActivityProductionCenter Is Nothing OrElse Me._listCostActivityProductionCenter.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un centro de producción"
                Exit Sub
            End If

            If Me._listCostActivityStep Is Nothing OrElse Me._listCostActivityStep.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar al menos un paso"
                Exit Sub
            End If

            Dim errorList As New StringBuilder()
            For Each item In _listCostActivityStep
                If Not _listCostActivityStepFixedAsset.Any(Function(d) d.ParentUUID = item.UUID AndAlso d.CostActivityStepId = item.Id) AndAlso
                        Not _listCostActivityStepPayroll.Any(Function(d) d.ParentUUID = item.UUID AndAlso d.CostActivityStepId = item.Id) AndAlso
                        Not _listCostActivityStepInventory.Any(Function(d) d.ParentUUID = item.UUID AndAlso d.CostActivityStepId = item.Id) AndAlso
                        Not _listCostActivityStepAddictionalCost.Any(Function(d) d.ParentUUID = item.UUID AndAlso d.CostActivityStepId = item.Id) Then
                    errorList.AppendLine(item.OrderDescription)
                End If
            Next

            If errorList.Length > 0 Then
                errorList.Insert(0, "Los siguientes pasos no tienen detalles relacionado: " & Environment.NewLine)
                Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
                Exit Sub
            End If
        End If

        AssigningValues()

        Try
            Using model As New MCostActivity(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveCostActivity(_costActivity, _listCostActivityProductionCenter, _listCostActivityStep, _listCostActivityStepFixedAsset, _listCostActivityStepPayroll, _listCostActivityStepInventory, _listCostActivityStepAddictionalCost)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me._costActivity = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    INDbeCode.Enabled = False
                    If _costActivity.ChangeTracker.State = ObjectState.Added Then
                        If Result.ObjectEmbbeded IsNot Nothing AndAlso Result.ObjectEmbbeded.Id > 0 Then
                            Me._costActivity = Result.ObjectEmbbeded
                            Me._costActivity.ChangeTracker.State = ObjectState.Modified
                            Code = Me._costActivity.Code
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
    End Sub

#End Region

#Region "BarButton Events"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._operativeUnitId = operatingUnit.Id
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbeCode.ButtonClick
        OpenSearch()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Evento que dispara el formulario de importar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportCostActivity
            AddHandler formulario.GetCostActivity, AddressOf ReturnGetCostActivity
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Async Sub FrmInvoiceEntityCapitatedDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._operativeUnitId = Me.BarraBotones.OperatingUnitValue

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PCostActivity(Me)
        _presenter.LoadDefinitionLayout()

        AdditionalControlPanel.Controls.Add(_ctrTmp)
        Using MCostSetting As New MCostSetting(Tag)
            Dim _costSetting = Await MCostSetting.GetCostSettingAsync()
            ShowStandarCostFields(_costSetting.AverageStandardCostActivity)
            _currentYearCost = _costSetting.Year
            _currentMonthCost = _costSetting.Month
        End Using

        AsyncLoader(True)
        Await _presenter.GetSequence()
        AsyncLoader(False)

        AddHandler CtrCostProductionCenter.AddCostActivityProductionCenter, AddressOf AddCostActivityProductionCenterPopup
        AddHandler CtrCostActivityStep.AddCostActivityStep, AddressOf AddCostActivityStepPopup
        AddHandler CtrCostActivityStepFixedAsset.AddCostActivityStepFixedAsset, AddressOf AddCostActivityStepFixedAssetPopup
        AddHandler CtrCostActivityStepPayroll.AddCostActivityStepPayroll, AddressOf AddCostActivityStepPayrollPopup
        AddHandler CtrCostActivityStepInventory.AddCostActivityStepInventory, AddressOf AddCostActivityStepInventoryPopup
        AddHandler CtrCostActivityStepAddictionalCost.AddCostActivityStepAddictionalCost, AddressOf AddCostActivityStepAddictionalCostPopup

        Deshacer()
        LoadStatus()
        SetActionsGrid()
        _currencyAbbreviation = _presenter.GetOfficialCurrencyFromCompanySettings().OfficialCurrency.Abbreviation
        SetCurrencyFormat(_currencyAbbreviation)
    End Sub


    ''' <summary>
    ''' Establece el formato de moneda en los controles del formulario.
    ''' </summary>
    ''' <param name="_currencyAbbreviation">Abreviación de la moneda.</param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        _ctrTmp.CurrencyAbbreviation = _currencyAbbreviation
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat, CtrCostActivityStepAddictionalCost.Controls)
        INDcolAddictionalCostCost = Window.Utils.FormatGrid(INDcolAddictionalCostCost, _currencyAbbreviation)
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _operativeUnitId = Nothing
        _sequence = Nothing
        _currentSequenceId = Nothing
        _presenter = Nothing
        _record = Nothing
        _costActivity = Nothing
        _listCostActivityProductionCenter = Nothing
        _listCostActivityStep = Nothing
        _listCostActivityStepFixedAsset = Nothing
        _listCostActivityStepPayroll = Nothing
        _listCostActivityStepInventory = Nothing
        _listCostActivityStepAddictionalCost = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbeCode.Enabled Then
            INDbeCode.Focus()
        End If
    End Sub

#End Region

#Region "FormClosing"

    Private Sub FrmInvoiceEntityCapitatedDistribution_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbeCode.KeyDown
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
                    Await Me.NewCostActivity()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleCUPSEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCUPSEntity.QueryPopUp
        If CUPSEntityXpo Is Nothing Then
            Me._presenter.InitializeCUPSEntityXpo()
        End If
    End Sub

    Private Sub INDpceAddProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddProductionCenter.QueryPopUp
        CtrCostProductionCenter.ListCostActivityProductionCenter = _listCostActivityProductionCenter
    End Sub

    Private Sub INDpceAddStep_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddStep.QueryPopUp
        CtrCostActivityStep.ListCostActivityStep = _listCostActivityStep
    End Sub

    Private Sub INDpceAddFixedAsset_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddFixedAsset.QueryPopUp
        CtrCostActivityStepFixedAsset.ListCostActivityStep = _listCostActivityStep
        CtrCostActivityStepFixedAsset.ListCostActivityStepFixedAsset = _listCostActivityStepFixedAsset
    End Sub

    Private Sub INDpceAddPayroll_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddPayroll.QueryPopUp
        CtrCostActivityStepPayroll.ListCostActivityStep = _listCostActivityStep
        CtrCostActivityStepPayroll.ListCostActivityStepPayroll = _listCostActivityStepPayroll
    End Sub

    Private Sub INDpceAddInventory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddInventory.QueryPopUp
        CtrCostActivityStepInventory.ListCostActivityStep = _listCostActivityStep
        CtrCostActivityStepInventory.ListCostActivityStepInventory = _listCostActivityStepInventory
    End Sub

    Private Sub INDpceAddAddictionalCost_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceAddAddictionalCost.QueryPopUp
        CtrCostActivityStepAddictionalCost.ListCostActivityStep = _listCostActivityStep
        CtrCostActivityStepAddictionalCost.ListCostActivityStepAddictionalCost = _listCostActivityStepAddictionalCost
    End Sub

#End Region

#Region "Closed"

    Private Sub INDpceAddProductionCenter_Closed(sender As Object, e As ClosedEventArgs) Handles INDpceAddProductionCenter.Closed
        CtrCostProductionCenter.CleanControls()
    End Sub

    Private Sub INDpceAddStep_Closed(sender As Object, e As ClosedEventArgs) Handles INDpceAddStep.Closed
        CtrCostActivityStep.CleanControls()
    End Sub

    Private Sub INDpceAddFixedAsset_Closed(sender As Object, e As ClosedEventArgs) Handles INDpceAddFixedAsset.Closed
        CtrCostActivityStepFixedAsset.CleanControls()
    End Sub

    Private Sub INDpceAddPayroll_Closed(sender As Object, e As ClosedEventArgs) Handles INDpceAddPayroll.Closed
        CtrCostActivityStepPayroll.CleanControls()
    End Sub

    Private Sub INDpceAddInventory_Closed(sender As Object, e As ClosedEventArgs) Handles INDpceAddInventory.Closed
        CtrCostActivityStepInventory.CleanControls()
    End Sub

    Private Sub INDpceAddAddictionalCost_Closed(sender As Object, e As ClosedEventArgs) Handles INDpceAddAddictionalCost.Closed
        CtrCostActivityStepAddictionalCost.CleanControls()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleCUPSEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCUPSEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("970", Nothing, True)
        End If
    End Sub

#End Region

#Region "ContexMenuActions"

    Private Sub IndigoGridViewProductionCenter_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewProductionCenter.ContexMenuActions, IndigoGridViewProductionCenter.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditCostActivityProductionCenter()
            Case "Remove"
                DeleteCostActivityProductionCenter()
        End Select
    End Sub

    Private Sub IndigoGridViewStep_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewStep.ContexMenuActions, IndigoGridViewStep.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditCostActivityStep()
            Case "Remove"
                DeleteCostActivityStep()
        End Select
    End Sub

    Private Sub IndigoGridViewFixedAsset_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewFixedAsset.ContexMenuActions, IndigoGridViewFixedAsset.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditCostActivityStepFixedAsset()
            Case "Remove"
                DeleteCostActivityStepFixedAsset()
        End Select
    End Sub

    Private Sub IndigoGridViewPayroll_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewPayroll.ContexMenuActions, IndigoGridViewPayroll.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditCostActivityStepPayroll()
            Case "Remove"
                DeleteCostActivityStepPayroll()
        End Select
    End Sub

    Private Sub IndigoGridViewInventory_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewInventory.ContexMenuActions, IndigoGridViewInventory.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditCostActivityStepInventory()
            Case "Remove"
                DeleteCostActivityStepInventory()
        End Select
    End Sub

    Private Sub IndigoGridViewAddictionalCost_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridViewAddictionalCost.ContexMenuActions, IndigoGridViewAddictionalCost.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditCostActivityStepAddictionalCost()
            Case "Remove"
                DeleteCostActivityStepAddictionalCost()
        End Select
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If Not Me._costActivity.Status Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento no puede ser modificado"
            Exit Sub
        End If
        If e.Rows.Count = 0 Then
            Exit Sub
        End If
        If e.Rows.Count > 300 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se puede procesar máximo 300 registros"
            Exit Sub
        End If

        Try
            If sender.Name = INDgcProductionCenter.Name Then
                Await Me.CopyAndPasteProductionCenter(e.Rows)
            ElseIf sender.Name = INDgcStep.Name Then
                Me.CopyAndPasteStep(e.Rows)
            ElseIf sender.Name = INDgcFixedAsset.Name Then
                Await Me.CopyAndPasteFixedAsset(e.Rows)
            ElseIf sender.Name = INDgcPayroll.Name Then
                Await Me.CopyAndPastePayroll(e.Rows)
            ElseIf sender.Name = INDgcInventory.Name Then
                Await Me.CopyAndPasteInventory(e.Rows)
            ElseIf sender.Name = INDgcAddictionalCost.Name Then
                Me.CopyAndPasteAddictionalCost(e.Rows)
            End If
        Catch ex As Exception
            'Throw ex
        End Try
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)() From {{eAcciones.Edit}, {eAcciones.Remove}}

        IndigoGridViewProductionCenter.SetListAcction(INDgvProductionCenter, _listActions)
        IndigoGridViewStep.SetListAcction(INDgvStep, _listActions)
        IndigoGridViewFixedAsset.SetListAcction(INDgvFixedAsset, _listActions)
        IndigoGridViewPayroll.SetListAcction(INDgvPayroll, _listActions)
        IndigoGridViewInventory.SetListAcction(INDgvInventory, _listActions)
        IndigoGridViewAddictionalCost.SetListAcction(INDgvAddictionalCost, _listActions)
    End Sub

    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbeCode.Text = ReturnValue
        If INDbeCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbeCode.Enabled = False
        End If
    End Sub

    Private Sub CleanControls()
        INDlcRoot.BeginUpdate()

        _doc = Nothing
        DeleteBlockedRecord()
        ReadOnlyControls(False)
        BarraBotones.EnableBarItems()
        BarraBotones.DisableBarDocument()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        Code = String.Empty
        ActivityName = String.Empty
        INDsleCUPSEntity.EditValue = Nothing
        INDsleCUPSEntity.Properties.NullText = String.Empty
        InitialDate = Nothing
        EndDate = Nothing
        INDmeDescription.Text = String.Empty
        Status = True

        _costActivity = Nothing
        _listCostActivityProductionCenter = Nothing
        _listCostActivityStep = Nothing
        _listCostActivityStepFixedAsset = Nothing
        _listCostActivityStepPayroll = Nothing
        _listCostActivityStepInventory = Nothing
        _listCostActivityStepAddictionalCost = Nothing

        INDgcProductionCenter.DataSource = Nothing
        INDgcStep.DataSource = Nothing
        INDgcFixedAsset.DataSource = Nothing
        INDgcPayroll.DataSource = Nothing
        INDgcInventory.DataSource = Nothing
        INDgcAddictionalCost.DataSource = Nothing

        CtrCostActivityStep.ListCostActivityStep = _listCostActivityStep
        CtrCostActivityStep.CleanControls()
        _ctrTmp.CleanControls()
        CleanStandardCostDetailsValues()

        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlcRoot.EndUpdate()
    End Sub

    Private Async Function NewCostActivity() As Task
        Me._costActivity = New CostActivity() With {.Status = True}
        Me._listCostActivityProductionCenter = New List(Of CostActivityProductionCenter)
        Me._listCostActivityStep = New List(Of CostActivityStep)
        Me._listCostActivityStepFixedAsset = New List(Of CostActivityStepFixedAsset)
        Me._listCostActivityStepPayroll = New List(Of CostActivityStepPayroll)
        Me._listCostActivityStepInventory = New List(Of CostActivityStepInventory)
        Me._listCostActivityStepAddictionalCost = New List(Of CostActivityStepAddictionalCost)

        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.CostSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequence.CostSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._currentSequenceId = Me._sequence.CostSecuenceDetail.SingleOrDefault(Function(s) s.IdOperatingUnit = Me._operativeUnitId).Id
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
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                    Else
                        Using model As New MCommonCost(Me.Tag)
                            Me.DicSequense(CInt(Me._currentSequenceId)) = Await model.GetNumericSequenseGroup(CInt(Me._currentSequenceId))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._currentSequenceId)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                            Exit Function
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                End If
            End If
        End If
    End Function

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MCostActivity(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim result = Await Model.GetInvoiceEntityCapitatedDistribution(INDbeCode.Text.Trim)
                    If Not result.StateResult Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        AsyncLoader(False)
                        Exit Function
                    End If
                    _costActivity = result.ObjectEmbbeded
                    INDlcRoot.BeginUpdate()
                End Using

                If _costActivity IsNot Nothing AndAlso _costActivity.Id > 0 Then
                    Using mCommonCost As New MCommonCost(Me.Tag)
                        Dim result = Await mCommonCost.GetBlockRecordCostByIdformAndIdRecord(Me.Tag, _costActivity.Id)

                        With _costActivity
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Code = .Code
                            ActivityName = .Name
                            INDsleCUPSEntity.EditValue = .CUPSEntityId
                            INDsleCUPSEntity.Properties.NullText = .CUPSEntityCodeName
                            InitialDate = .InitialDate
                            EndDate = .EndDate
                            Description = .Description
                            Status = .Status

                            _listCostActivityProductionCenter = New List(Of CostActivityProductionCenter)
                            If .CostActivityProductionCenter IsNot Nothing Then
                                For Each item In .CostActivityProductionCenter
                                    _listCostActivityProductionCenter.Add(item)
                                Next
                            End If

                            _listCostActivityStep = New List(Of CostActivityStep)
                            _listCostActivityStepFixedAsset = New List(Of CostActivityStepFixedAsset)
                            _listCostActivityStepPayroll = New List(Of CostActivityStepPayroll)
                            _listCostActivityStepInventory = New List(Of CostActivityStepInventory)
                            _listCostActivityStepAddictionalCost = New List(Of CostActivityStepAddictionalCost)
                            If .CostActivityStep IsNot Nothing Then
                                For Each item In .CostActivityStep.OrderBy(Function(d) d.Order)
                                    If item.CostActivityStepFixedAsset IsNot Nothing Then
                                        For Each detail In item.CostActivityStepFixedAsset
                                            _listCostActivityStepFixedAsset.Add(detail)
                                        Next
                                        item.CostActivityStepFixedAsset.Clear()
                                    End If

                                    If item.CostActivityStepPayroll IsNot Nothing Then
                                        For Each detail In item.CostActivityStepPayroll
                                            _listCostActivityStepPayroll.Add(detail)
                                        Next
                                        item.CostActivityStepPayroll.Clear()
                                    End If

                                    If item.CostActivityStepInventory IsNot Nothing Then
                                        For Each detail In item.CostActivityStepInventory
                                            _listCostActivityStepInventory.Add(detail)
                                        Next
                                        item.CostActivityStepInventory.Clear()
                                    End If

                                    If item.CostActivityStepAddictionalCost IsNot Nothing Then
                                        For Each detail In item.CostActivityStepAddictionalCost
                                            _listCostActivityStepAddictionalCost.Add(detail)
                                        Next
                                        item.CostActivityStepAddictionalCost.Clear()
                                    End If

                                    _listCostActivityStep.Add(item)
                                Next
                            End If

                            INDgcProductionCenter.DataSource = Nothing
                            INDgcProductionCenter.DataSource = _listCostActivityProductionCenter

                            INDgcStep.DataSource = Nothing
                            INDgcStep.DataSource = _listCostActivityStep
                            CtrCostActivityStep.ListCostActivityStep = _listCostActivityStep
                            CtrCostActivityStep.CleanControls()

                            INDgcFixedAsset.DataSource = Nothing
                            INDgcFixedAsset.DataSource = _listCostActivityStepFixedAsset

                            INDgcPayroll.DataSource = Nothing
                            INDgcPayroll.DataSource = _listCostActivityStepPayroll

                            INDgcInventory.DataSource = Nothing
                            INDgcInventory.DataSource = _listCostActivityStepInventory

                            INDgcAddictionalCost.DataSource = Nothing
                            INDgcAddictionalCost.DataSource = _listCostActivityStepAddictionalCost
                        End With
                        ShowStandardCostDetailsValues(_costActivity.Id)

                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._costActivity.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            _record = New BlockRecordCost With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _costActivity.Id}
                            Dim operation = Await mCommonCost.SaveBlockRecordCost(_record)
                            _record = operation.ObjectEmbbeded
                        Else
                            _record = result
                            Dim xtraMessage As String = String.Format(BaseClass.obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(_costActivity.Id, MyTag, Nothing, GetType(CostActivity).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewCostActivity()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Deshacer()
                    End If
                End If
                INDlcRoot.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDbeCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MCommonCost(CStr(Me.Tag))
                Await Model.DeleteBlockRecordCost(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._costActivity.Code, Me._costActivity.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._costActivity.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._costActivity.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._costActivity.Code, Me._costActivity.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._costActivity.Code)
        End If
        Return Me._doc
    End Function

    Private Sub AssigningValues()
        With _costActivity
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = BarraBotones.OperatingUnit.Id
            .Code = Code
            .Name = ActivityName
            .CUPSEntityId = CUPSEntityId
            .InitialDate = InitialDate
            .EndDate = EndDate
            .Description = Description

            .CostActivityProductionCenter.Clear()
            .CostActivityStep.Clear()

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using model As New MCostActivity(Me.Tag.ToString())
                    AsyncLoader(True)
                    _costActivity.Status = Not _costActivity.Status
                    Dim Result = Await model.ChangeStateCostActivity(_costActivity)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = "El estado se ha actualizado correctamente"
                        Me.Status = Result.ObjectEmbbeded.Status
                        Me._costActivity.Status = Me.Status
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        INDbeCode.Enabled = False
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbeCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    Private Async Sub ReturnGetCostActivity(sender As Object, costActivityId As Integer)
        Try
            AsyncLoader(True)

            Using Model As New MCostActivity(Me.Tag.ToString())
                Dim result = Await Model.GetInvoiceEntityCapitatedDistributionById(costActivityId)

                If result.StateResult Then
                    With result.ObjectEmbbeded
                        If .CostActivityStep IsNot Nothing Then
                            Dim maxOrder = _listCostActivityStep.Count
                            For Each item In .CostActivityStep.OrderBy(Function(d) d.Order)
                                Dim costActivityStep As New CostActivityStep With
                                {
                                    .UUID = Guid.NewGuid.ToString(),
                                    .Order = item.Order + maxOrder,
                                    .Description = item.Description,
                                    .OrderDescription = String.Format("{0} - {1}", .Order, .Description)
                                }

                                If item.CostActivityStepFixedAsset IsNot Nothing Then
                                    For Each detail In item.CostActivityStepFixedAsset
                                        If Not _listCostActivityStepFixedAsset.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.FixedAssetItemId = detail.FixedAssetItemId) Then
                                            _listCostActivityStepFixedAsset.Add(New CostActivityStepFixedAsset With
                                            {
                                                .ParentUUID = costActivityStep.UUID,
                                                .CostActivityStepOrderDescription = costActivityStep.OrderDescription,
                                                .FixedAssetItemId = detail.FixedAssetItemId,
                                                .FixedAssetItemCodeName = detail.FixedAssetItemCodeName,
                                                .Hours = detail.Hours
                                            })
                                        End If
                                    Next
                                End If

                                If item.CostActivityStepPayroll IsNot Nothing Then
                                    For Each detail In item.CostActivityStepPayroll
                                        If Not _listCostActivityStepPayroll.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.PayrollPositionId = detail.PayrollPositionId) Then
                                            _listCostActivityStepPayroll.Add(New CostActivityStepPayroll With
                                            {
                                                .ParentUUID = costActivityStep.UUID,
                                                .CostActivityStepOrderDescription = costActivityStep.OrderDescription,
                                                .PayrollPositionId = detail.PayrollPositionId,
                                                .PositionCodeName = detail.PositionCodeName,
                                                .Hours = detail.Hours
                                            })
                                        End If
                                    Next
                                End If

                                If item.CostActivityStepInventory IsNot Nothing Then
                                    For Each detail In item.CostActivityStepInventory
                                        If Not _listCostActivityStepInventory.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostInventoryGroupId = detail.CostInventoryGroupId) Then
                                            _listCostActivityStepInventory.Add(New CostActivityStepInventory With
                                            {
                                                .ParentUUID = costActivityStep.UUID,
                                                .CostActivityStepOrderDescription = costActivityStep.OrderDescription,
                                                .CostInventoryGroupId = detail.CostInventoryGroupId,
                                                .CostInventoryGroupCodeName = detail.CostInventoryGroupCodeName,
                                                .MeasurementUnitCodeName = detail.MeasurementUnitCodeName,
                                                .Quantity = detail.Quantity
                                            })
                                        End If
                                    Next
                                End If

                                If item.CostActivityStepAddictionalCost IsNot Nothing Then
                                    For Each detail In item.CostActivityStepAddictionalCost
                                        _listCostActivityStepAddictionalCost.Add(New CostActivityStepAddictionalCost With
                                        {
                                            .ParentUUID = costActivityStep.UUID,
                                            .CostActivityStepOrderDescription = costActivityStep.OrderDescription,
                                            .Description = detail.Description,
                                            .Value = detail.Value
                                        })
                                    Next
                                End If

                                _listCostActivityStep.Add(costActivityStep)
                            Next
                        End If

                        INDgcStep.DataSource = Nothing
                        INDgcStep.DataSource = _listCostActivityStep
                        CtrCostActivityStep.ListCostActivityStep = _listCostActivityStep
                        CtrCostActivityStep.CleanControls()

                        INDgcFixedAsset.DataSource = Nothing
                        INDgcFixedAsset.DataSource = _listCostActivityStepFixedAsset

                        INDgcPayroll.DataSource = Nothing
                        INDgcPayroll.DataSource = _listCostActivityStepPayroll

                        INDgcInventory.DataSource = Nothing
                        INDgcInventory.DataSource = _listCostActivityStepInventory

                        INDgcAddictionalCost.DataSource = Nothing
                        INDgcAddictionalCost.DataSource = _listCostActivityStepAddictionalCost
                    End With
                End If
            End Using

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Muestra u oculta los campos relacionados con el Costo Estándar Promedio de acuerdo al parámetro
    ''' </summary>
    ''' <param name="AverageStandardCostActivity"></param>
    Private Sub ShowStandarCostFields(ByVal AverageStandardCostActivity As Boolean)
        _ctrTmp.PrintInfo(AverageStandardCostActivity)
        INDLciFixedAsset.HideControl(Not AverageStandardCostActivity)
        INDLciPayroll.HideControl(Not AverageStandardCostActivity)
        INDLciInventory.HideControl(Not AverageStandardCostActivity)
        INDLciAdditionalCost.HideControl(Not AverageStandardCostActivity)
    End Sub

    ''' <summary>
    ''' Muestra los detalles del Costo Estándar Promedio de la actividad 
    ''' </summary>
    ''' <param name="ActivityId"></param>
    Private Async Sub ShowStandardCostDetailsValues(ByVal ActivityId As Integer)
        Dim listStandardCostDetails As List(Of StandardCostDetailsXpo)
        Using model As New MCostActivity(MyTag)
            listStandardCostDetails = Await model.GetStandardCostValuesByActivityId(ActivityId)
        End Using
        If listStandardCostDetails.Any() Then
            Dim standardCostDetails As StandardCostDetailsXpo = listStandardCostDetails.
                        FirstOrDefault(Function(item) item.StandarCostId.Validity.Month <= _currentMonthCost AndAlso item.StandarCostId.EndDate.Month >= _currentMonthCost AndAlso
                                                      item.StandarCostId.Validity.Year <= _currentYearCost AndAlso item.StandarCostId.EndDate.Year >= _currentYearCost)

            If standardCostDetails IsNot Nothing Then
                CurrencyNumbertFormat = If(CurrencyNumbertFormat, _currencyAbbreviation?.GetNumberFormat)
                _ctrTmp.StandarCostValue = standardCostDetails.StandarCostValue
                INDLFixedAsset.Text = String.Format("Costo Estándar Promedio {0}", Utils.GetMoneyWithISO4217(standardCostDetails.FixedAssetValue, _currencyAbbreviation, CurrencyNumbertFormat?.CurrencyDecimalDigits))
                INDLPayroll.Text = String.Format("Costo Estándar Promedio {0}", Utils.GetMoneyWithISO4217(standardCostDetails.PayrollValue, _currencyAbbreviation, CurrencyNumbertFormat?.CurrencyDecimalDigits))
                INDLInventory.Text = String.Format("Costo Estándar Promedio {0}", Utils.GetMoneyWithISO4217(standardCostDetails.InventoryValue, _currencyAbbreviation, CurrencyNumbertFormat?.CurrencyDecimalDigits))
                INDLAdditionalCost.Text = String.Format("Costo Estándar Promedio {0}", Utils.GetMoneyWithISO4217(standardCostDetails.AdditionalCost, _currencyAbbreviation, CurrencyNumbertFormat?.CurrencyDecimalDigits))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que límpia los valores de las actividades
    ''' </summary>
    Private Sub CleanStandardCostDetailsValues()
        INDLFixedAsset.Text = "Costo Estándar Promedio"
        INDLPayroll.Text = "Costo Estándar Promedio"
        INDLInventory.Text = "Costo Estándar Promedio"
        INDLAdditionalCost.Text = "Costo Estándar Promedio"
    End Sub

#Region "ProductionCenter"

    Private Sub AddCostActivityProductionCenterPopup(ByVal costActivityProductionCenter As CostActivityProductionCenter)
        If Not CtrCostProductionCenter.EditMode Then 'Si se esta guardando
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta editando
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        AddCostActivityProductionCenter(costActivityProductionCenter, CtrCostProductionCenter.EditMode)
    End Sub

    Private Sub EditCostActivityProductionCenter()
        Dim selected = CType(INDgvProductionCenter.GetFocusedRow, CostActivityProductionCenter)
        INDpceAddProductionCenter.ShowPopup()
        CtrCostProductionCenter.CostActivityProductionCenter = selected
        CtrCostProductionCenter.LoadControls()
    End Sub

    Private Sub DeleteCostActivityProductionCenter()
        Dim selected = CType(INDgvProductionCenter.GetFocusedRow, CostActivityProductionCenter)
        _listCostActivityProductionCenter.Remove(selected)

        AddCostActivityProductionCenter(Nothing, True)
    End Sub

    Private Sub AddCostActivityProductionCenter(ByVal costActivityProductionCenter As CostActivityProductionCenter, ByVal editMode As Boolean)
        If Not editMode Then
            _listCostActivityProductionCenter.Add(costActivityProductionCenter)
        End If

        INDgcProductionCenter.DataSource = Nothing
        INDgcProductionCenter.DataSource = _listCostActivityProductionCenter
    End Sub

    Private Async Function CopyAndPasteProductionCenter(listInfo As List(Of List(Of String))) As Task
        INDgvProductionCenter.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()

        Dim errors As New List(Of String)
        Dim data As New List(Of List(Of String))

        If listInfo.Count > 0 Then
            Dim line As Integer = 0
            For Each item In listInfo
                line = line + 1

                If item.Count <> 1 Then
                    errors.Add(String.Format("La linea {0} debe tener el formato: Codigo", line))
                    Continue For
                End If
                If String.IsNullOrEmpty(item(0)) Then
                    errors.Add(String.Format("La linea {0} no contiene un código", line))
                    Continue For
                End If

                data.Add(item)
            Next
        End If

        If data.Count > 0 Then
            Using model As New MCostActivity(MyTag)
                Dim result = Await model.CostActivityCopyAndPasteProductionCenter(data)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDgvProductionCenter.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    For Each costActivityProductionCenter In result.ObjectEmbbeded
                        If Not Me._listCostActivityProductionCenter.Any(Function(d) d.CostProductionCenterId = costActivityProductionCenter.CostProductionCenterId) Then
                            Me.AddCostActivityProductionCenter(costActivityProductionCenter, False)
                        End If
                    Next
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    errors.AddRange(result.MessageResult)
                End If
            End Using
        End If

        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgvProductionCenter.HideLoadingPanel()
    End Function

#End Region

#Region "Step"

    Private Sub AddCostActivityStepPopup(ByVal costActivityStep As CostActivityStep, ByVal previousOrder As Integer)
        If Not CtrCostActivityStep.EditMode Then 'Si se esta guardando            
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta editando
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        AddCostActivityStep(costActivityStep, previousOrder, CtrCostActivityStep.EditMode)
    End Sub

    Private Sub EditCostActivityStep()
        Dim selected = CType(INDgvStep.GetFocusedRow, CostActivityStep)
        INDpceAddStep.ShowPopup()
        CtrCostActivityStep.CostActivityStep = selected
        CtrCostActivityStep.LoadControls()
    End Sub

    Private Sub DeleteCostActivityStep()
        Dim selected = CType(INDgvStep.GetFocusedRow, CostActivityStep)

        If ValidateDeleteCostActivityStep(selected) Then
            _listCostActivityStep.Remove(selected)
            ReOrder(selected.Order, _listCostActivityStep.Count + 1, -1)
            ReLoadSteps(selected.Order, _listCostActivityStep.Count)

            INDgcStep.DataSource = Nothing
            INDgcStep.DataSource = _listCostActivityStep
            CtrCostActivityStep.ListCostActivityStep = _listCostActivityStep
            CtrCostActivityStep.CleanControls()
        End If
    End Sub

    Private Sub AddCostActivityStep(ByVal costActivityStep As CostActivityStep, ByVal previousOrder As Integer, ByVal editMode As Boolean)
        If Not editMode Then
            _listCostActivityStep.Add(costActivityStep)
        End If

        Me.ProcessReOrder(costActivityStep, previousOrder)

        INDgcStep.DataSource = Nothing
        INDgcStep.DataSource = _listCostActivityStep
    End Sub

    Private Sub CopyAndPasteStep(listInfo As List(Of List(Of String)))
        INDgvStep.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()
        If listInfo.Count > 0 Then
            Dim line As Integer = 0
            Dim errors As New List(Of String)

            For Each item In listInfo
                line = line + 1
                Dim order As Integer
                Dim lastOrder As Integer = Me._listCostActivityStep.Count + 1

                If item.Count <> 2 Then
                    errors.Add(String.Format("La linea {0} debe tener el formato: Orden - Descripcion", line))
                    Continue For
                End If
                If Not Integer.TryParse(item(0), order) Then
                    errors.Add(String.Format("La linea {0} no contiene un orden numerico", line))
                    Continue For
                End If
                If String.IsNullOrEmpty(item(1)) Then
                    errors.Add(String.Format("La linea {0} no contiene una descripcion", line))
                    Continue For
                End If
                If order <= 0 Then
                    errors.Add(String.Format("El orden de la linea {0} no puede ser menor o igual a 0", line))
                    Continue For
                End If
                If order > lastOrder Then
                    errors.Add(String.Format("El orden de la linea {0} es superior a los limites de los pasos", line))
                    Continue For
                End If
                If Me._listCostActivityStep.Any(Function(s) s.Description = item(1)) Then
                    errors.Add(String.Format("Ya existe un paso con la descripcion de la linea {0}", line))
                    Continue For
                End If

                AddCostActivityStep(New CostActivityStep With
                {
                    .UUID = Guid.NewGuid().ToString(),
                    .Order = order,
                    .Description = item(1)
                }, lastOrder, False)
            Next

            If errors.Count > 0 Then
                Using formulario As New FrmListErrors(errors)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgvStep.HideLoadingPanel()
    End Sub

    Private Function ValidateDeleteCostActivityStep(ByVal costActivityStep As CostActivityStep) As Boolean
        Dim errorList As New StringBuilder()

        If Me._listCostActivityStepFixedAsset.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id) Then
            errorList.AppendLine("Activos Fijos")
        End If

        If Me._listCostActivityStepPayroll.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id) Then
            errorList.AppendLine("Nomina")
        End If

        If Me._listCostActivityStepInventory.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id) Then
            errorList.AppendLine("Inventario")
        End If

        If Me._listCostActivityStepAddictionalCost.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id) Then
            errorList.AppendLine("Costos Adicionales")
        End If

        If errorList.Length > 0 Then
            errorList.Insert(0, "Existen elementos asociados al Paso: " & Environment.NewLine)
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        If _listCostActivityStep.Any(Function(s) s.Order <= 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "El orden de los pasos no puede ser menor o igual a 0"
            Return False
        End If

        Dim order As Integer = 0
        For Each activityStep In _listCostActivityStep
            order = order + 1
            If Not _listCostActivityStep.Any(Function(s) s.Order = order) Then
                errorList.AppendLine(String.Format("No existe el paso {0}", order))
            End If
        Next

        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If

        Return True
    End Function

    Private Sub ProcessReOrder(ByVal costActivityStep As CostActivityStep, ByVal previousOrder As Integer)
        Dim newOrder = costActivityStep.Order
        Dim minOrder = If(previousOrder > newOrder, newOrder, previousOrder)
        Dim maxOrder = If(previousOrder > newOrder, previousOrder, newOrder)
        Dim operatorOrder = If(previousOrder > newOrder, 1, -1)
        ReOrder(minOrder, maxOrder, operatorOrder)

        costActivityStep.Order = newOrder
        costActivityStep.OrderDescription = String.Format("{0} - {1}", costActivityStep.Order, costActivityStep.Description)

        ReLoadSteps(minOrder, maxOrder)
    End Sub

    Private Sub ReOrder(ByVal minOrder As Integer, ByVal maxOrder As Integer, ByVal operatorOrder As Integer)
        If minOrder = maxOrder Then
            Exit Sub
        End If

        For Each item In _listCostActivityStep.Where(Function(i) i.Order >= minOrder AndAlso i.Order <= maxOrder)
            item.Order = item.Order + operatorOrder
            item.OrderDescription = String.Format("{0} - {1}", item.Order, item.Description)
        Next
    End Sub

    Private Sub ReLoadSteps(ByVal minOrder As Integer, ByVal maxOrder As Integer)
        For Each costActivityStep In _listCostActivityStep.Where(Function(i) i.Order >= minOrder AndAlso i.Order <= maxOrder)
            For Each item In _listCostActivityStepFixedAsset.Where(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id)
                item.CostActivityStepOrderDescription = costActivityStep.OrderDescription
            Next
            INDgcFixedAsset.DataSource = Nothing
            INDgcFixedAsset.DataSource = _listCostActivityStepFixedAsset

            For Each item In _listCostActivityStepPayroll.Where(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id)
                item.CostActivityStepOrderDescription = costActivityStep.OrderDescription
            Next
            INDgcPayroll.DataSource = Nothing
            INDgcPayroll.DataSource = _listCostActivityStepPayroll

            For Each item In _listCostActivityStepInventory.Where(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id)
                item.CostActivityStepOrderDescription = costActivityStep.OrderDescription
            Next
            INDgcInventory.DataSource = Nothing
            INDgcInventory.DataSource = _listCostActivityStepInventory

            For Each item In _listCostActivityStepAddictionalCost.Where(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id)
                item.CostActivityStepOrderDescription = costActivityStep.OrderDescription
            Next
            INDgcAddictionalCost.DataSource = Nothing
            INDgcAddictionalCost.DataSource = _listCostActivityStepAddictionalCost
        Next
    End Sub

#End Region

#Region "FixedAsset"

    Private Sub AddCostActivityStepFixedAssetPopup(ByVal costActivityStepFixedAsset As CostActivityStepFixedAsset)
        If Not CtrCostActivityStepFixedAsset.EditMode Then 'Si se esta guardando
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta editando
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        AddCostActivityStepFixedAsset(costActivityStepFixedAsset, CtrCostActivityStepFixedAsset.EditMode)
    End Sub

    Private Sub EditCostActivityStepFixedAsset()
        Dim selected = CType(INDgvFixedAsset.GetFocusedRow, CostActivityStepFixedAsset)
        INDpceAddFixedAsset.ShowPopup()
        CtrCostActivityStepFixedAsset.CostActivityStepFixedAsset = selected
        CtrCostActivityStepFixedAsset.LoadControls()
    End Sub

    Private Sub DeleteCostActivityStepFixedAsset()
        Dim selected = CType(INDgvFixedAsset.GetFocusedRow, CostActivityStepFixedAsset)
        _listCostActivityStepFixedAsset.Remove(selected)

        AddCostActivityStepFixedAsset(Nothing, True)
    End Sub

    Private Sub AddCostActivityStepFixedAsset(ByVal costActivityStepFixedAsset As CostActivityStepFixedAsset, ByVal editMode As Boolean)
        If Not editMode Then
            _listCostActivityStepFixedAsset.Add(costActivityStepFixedAsset)
        End If

        INDgcFixedAsset.DataSource = Nothing
        INDgcFixedAsset.DataSource = _listCostActivityStepFixedAsset
    End Sub

    Private Async Function CopyAndPasteFixedAsset(listInfo As List(Of List(Of String))) As Task
        INDgvFixedAsset.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()

        Dim errors As New List(Of String)
        Dim data As New List(Of List(Of String))

        If listInfo.Count > 0 Then
            Dim line As Integer = 0
            For Each item In listInfo
                line = line + 1
                Dim order As Integer
                Dim value As Decimal

                If item.Count <> 3 Then
                    errors.Add(String.Format("La linea {0} debe tener el formato: Paso - Artículo - Horas", line))
                    Continue For
                End If
                If Not Integer.TryParse(item(0), order) Then
                    errors.Add(String.Format("La linea {0} no contiene un orden numerico", line))
                    Continue For
                End If
                If Not Me._listCostActivityStep.Any(Function(s) s.Order = order) Then
                    errors.Add(String.Format("No existe el paso asociado a la linea {0}", line))
                    Continue For
                End If
                If String.IsNullOrEmpty(item(1)) Then
                    errors.Add(String.Format("La linea {0} no contiene un codigo de Articulo", line))
                    Continue For
                End If
                If Not Decimal.TryParse(item(2), value) Then
                    errors.Add(String.Format("La linea {0} tiene un valor de horas no numérico", line))
                    Continue For
                End If
                If value <= 0 Then
                    errors.Add(String.Format("Las horas de la linea {0} no puede ser menor o igual a 0", line))
                    Continue For
                End If

                data.Add(item)
            Next
        End If

        If data.Count > 0 Then
            Using model As New MCostActivity(MyTag)
                Dim result = Await model.CostActivityCopyAndPasteFixedAsset(data)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDgvFixedAsset.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    For Each costActivityFixedAsset In result.ObjectEmbbeded
                        If Not Me._listCostActivityStepFixedAsset.Any(Function(d) d.FixedAssetItemId = costActivityFixedAsset.FixedAssetItemId) Then
                            Dim costActivityStep = Me._listCostActivityStep.FirstOrDefault(Function(s) s.Order = costActivityFixedAsset.CostActivityStepId)
                            costActivityFixedAsset.ParentUUID = costActivityStep.UUID
                            costActivityFixedAsset.CostActivityStepId = costActivityStep.Id
                            costActivityFixedAsset.CostActivityStepOrderDescription = costActivityStep.OrderDescription
                            Me.AddCostActivityStepFixedAsset(costActivityFixedAsset, False)
                        End If
                    Next
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    errors.AddRange(result.MessageResult)
                End If
            End Using
        End If

        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgvFixedAsset.HideLoadingPanel()
    End Function

#End Region

#Region "Payroll"

    Private Sub AddCostActivityStepPayrollPopup(ByVal costActivityStepPayroll As CostActivityStepPayroll)
        If Not CtrCostActivityStepPayroll.EditMode Then 'Si se esta guardando
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta editando
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        AddCostActivityStepPayroll(costActivityStepPayroll, CtrCostActivityStepPayroll.EditMode)
    End Sub

    Private Sub EditCostActivityStepPayroll()
        Dim selected = CType(INDgvPayroll.GetFocusedRow, CostActivityStepPayroll)
        INDpceAddPayroll.ShowPopup()
        CtrCostActivityStepPayroll.CostActivityStepPayroll = selected
        CtrCostActivityStepPayroll.LoadControls()
    End Sub

    Private Sub DeleteCostActivityStepPayroll()
        Dim selected = CType(INDgvPayroll.GetFocusedRow, CostActivityStepPayroll)
        _listCostActivityStepPayroll.Remove(selected)

        AddCostActivityStepPayroll(Nothing, True)
    End Sub

    Private Sub AddCostActivityStepPayroll(ByVal costActivityStepPayroll As CostActivityStepPayroll, ByVal editMode As Boolean)
        If Not editMode Then 'Si se esta guardando
            _listCostActivityStepPayroll.Add(costActivityStepPayroll)
        End If

        INDgcPayroll.DataSource = Nothing
        INDgcPayroll.DataSource = _listCostActivityStepPayroll
    End Sub

    Private Async Function CopyAndPastePayroll(listInfo As List(Of List(Of String))) As Task
        INDgvPayroll.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()

        Dim errors As New List(Of String)
        Dim data As New List(Of List(Of String))

        If listInfo.Count > 0 Then
            Dim line As Integer = 0
            For Each item In listInfo
                line = line + 1
                Dim order As Integer
                Dim value As Decimal

                If item.Count <> 3 Then
                    errors.Add(String.Format("La linea {0} debe tener el formato: Paso - Cargo - Horas", line))
                    Continue For
                End If
                If Not Integer.TryParse(item(0), order) Then
                    errors.Add(String.Format("La linea {0} no contiene un orden numerico", line))
                    Continue For
                End If
                If Not Me._listCostActivityStep.Any(Function(s) s.Order = order) Then
                    errors.Add(String.Format("No existe el paso asociado a la linea {0}", line))
                    Continue For
                End If
                If String.IsNullOrEmpty(item(1)) Then
                    errors.Add(String.Format("La linea {0} no contiene un codigo de Articulo", line))
                    Continue For
                End If
                If Not Decimal.TryParse(item(2), value) Then
                    errors.Add(String.Format("La linea {0} tiene un valor de horas no numérico", line))
                    Continue For
                End If
                If value <= 0 Then
                    errors.Add(String.Format("Las horas de la linea {0} no puede ser menor o igual a 0", line))
                    Continue For
                End If

                data.Add(item)
            Next
        End If

        If data.Count > 0 Then
            Using model As New MCostActivity(MyTag)
                Dim result = Await model.CostActivityCopyAndPastePayroll(data)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDgvPayroll.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    For Each costActivityPayroll In result.ObjectEmbbeded
                        Dim costActivityStep = Me._listCostActivityStep.FirstOrDefault(Function(s) s.Order = costActivityPayroll.CostActivityStepId)
                        If Not Me._listCostActivityStepPayroll.Any(Function(d) d.ParentUUID = costActivityStep.UUID AndAlso d.CostActivityStepId = costActivityStep.Id AndAlso d.PayrollPositionId = costActivityPayroll.PayrollPositionId) Then
                            costActivityPayroll.ParentUUID = costActivityStep.UUID
                            costActivityPayroll.CostActivityStepId = costActivityStep.Id
                            costActivityPayroll.CostActivityStepOrderDescription = costActivityStep.OrderDescription
                            Me.AddCostActivityStepPayroll(costActivityPayroll, False)
                        End If
                    Next
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    errors.AddRange(result.MessageResult)
                End If
            End Using
        End If

        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgvPayroll.HideLoadingPanel()
    End Function

#End Region

#Region "Inventory"

    Private Sub AddCostActivityStepInventoryPopup(ByVal costActivityStepInventory As CostActivityStepInventory)
        If Not CtrCostActivityStepInventory.EditMode Then 'Si se esta guardando
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta editando
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        AddCostActivityStepInventory(costActivityStepInventory, CtrCostActivityStepInventory.EditMode)
    End Sub

    Private Sub EditCostActivityStepInventory()
        Dim selected = CType(INDgvInventory.GetFocusedRow, CostActivityStepInventory)
        INDpceAddInventory.ShowPopup()
        CtrCostActivityStepInventory.CostActivityStepInventory = selected
        CtrCostActivityStepInventory.LoadControls()
    End Sub

    Private Sub DeleteCostActivityStepInventory()
        Dim selected = CType(INDgvInventory.GetFocusedRow, CostActivityStepInventory)
        _listCostActivityStepInventory.Remove(selected)

        AddCostActivityStepInventory(Nothing, True)
    End Sub

    Private Sub AddCostActivityStepInventory(ByVal costActivityStepInventory As CostActivityStepInventory, ByVal editMode As Boolean)
        If Not editMode Then
            _listCostActivityStepInventory.Add(costActivityStepInventory)
        End If

        INDgcInventory.DataSource = Nothing
        INDgcInventory.DataSource = _listCostActivityStepInventory
    End Sub

    Private Async Function CopyAndPasteInventory(listInfo As List(Of List(Of String))) As Task
        INDgvInventory.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()

        Dim errors As New List(Of String)
        Dim data As New List(Of List(Of String))

        If listInfo.Count > 0 Then
            Dim line As Integer = 0
            For Each item In listInfo
                line = line + 1
                Dim order As Integer
                Dim cantidad As Decimal

                If item.Count <> 3 Then
                    errors.Add(String.Format("La linea {0} debe tener el formato: Paso - Grupo de Producto - Cantidad", line))
                    Continue For
                End If
                If Not Integer.TryParse(item(0), order) Then
                    errors.Add(String.Format("La linea {0} no contiene un orden numerico", line))
                    Continue For
                End If
                If Not Me._listCostActivityStep.Any(Function(s) s.Order = order) Then
                    errors.Add(String.Format("No existe el paso asociado a la linea {0}", line))
                    Continue For
                End If
                If String.IsNullOrEmpty(item(1)) Then
                    errors.Add(String.Format("La linea {0} no contiene un codigo de Producto", line))
                    Continue For
                End If
                If Not Decimal.TryParse(item(2), cantidad) Then
                    errors.Add(String.Format("La linea {0} tiene una cantidad no numérica", line))
                    Continue For
                End If
                If cantidad <= 0 Then
                    errors.Add(String.Format("La cantidad de la linea {0} no puede ser menor o igual a 0", line))
                    Continue For
                End If

                data.Add(item)
            Next
        End If

        If data.Count > 0 Then
            Using model As New MCostActivity(MyTag)
                Dim result = Await model.CostActivityCopyAndPasteInventory(data)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDgvInventory.HideLoadingPanel()
                    Exit Function
                End If
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    For Each costActivityInventory In result.ObjectEmbbeded
                        If Not Me._listCostActivityStepInventory.Any(Function(d) d.CostInventoryGroupId = costActivityInventory.CostInventoryGroupId) Then
                            Dim costActivityStep = Me._listCostActivityStep.FirstOrDefault(Function(s) s.Order = costActivityInventory.CostActivityStepId)
                            costActivityInventory.ParentUUID = costActivityStep.UUID
                            costActivityInventory.CostActivityStepId = costActivityStep.Id
                            costActivityInventory.CostActivityStepOrderDescription = costActivityStep.OrderDescription
                            Me.AddCostActivityStepInventory(costActivityInventory, False)
                        End If
                    Next
                End If
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    errors.AddRange(result.MessageResult)
                End If
            End Using
        End If

        If errors.Count > 0 Then
            Using formulario As New FrmListErrors(errors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If

        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgvInventory.HideLoadingPanel()
    End Function

#End Region

#Region "AddictionalCost"

    Private Sub AddCostActivityStepAddictionalCostPopup(ByVal costActivityStepAddictionalCost As CostActivityStepAddictionalCost)
        If Not CtrCostActivityStepAddictionalCost.EditMode Then 'Si se esta guardando
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else 'Si se esta editando
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If

        AddCostActivityStepAddictionalCost(costActivityStepAddictionalCost, CtrCostActivityStepAddictionalCost.EditMode)
    End Sub

    Private Sub EditCostActivityStepAddictionalCost()
        Dim selected = CType(INDgvAddictionalCost.GetFocusedRow, CostActivityStepAddictionalCost)
        INDpceAddAddictionalCost.ShowPopup()
        CtrCostActivityStepAddictionalCost.CostActivityStepAddictionalCost = selected
        CtrCostActivityStepAddictionalCost.LoadControls()
    End Sub

    Private Sub DeleteCostActivityStepAddictionalCost()
        Dim selected = CType(INDgvAddictionalCost.GetFocusedRow, CostActivityStepAddictionalCost)
        _listCostActivityStepAddictionalCost.Remove(selected)

        AddCostActivityStepAddictionalCost(Nothing, True)
    End Sub

    Private Sub AddCostActivityStepAddictionalCost(ByVal costActivityStepAddictionalCost As CostActivityStepAddictionalCost, ByVal editMode As Boolean)
        If Not editMode Then
            _listCostActivityStepAddictionalCost.Add(costActivityStepAddictionalCost)
        End If

        INDgcAddictionalCost.DataSource = Nothing
        INDgcAddictionalCost.DataSource = _listCostActivityStepAddictionalCost
    End Sub

    Private Sub CopyAndPasteAddictionalCost(listInfo As List(Of List(Of String)))
        INDgvStep.ShowLoadingPanel()
        Me.Cursor = BaseClass.ChangeCursorIndigo()
        If listInfo.Count > 0 Then
            Dim line As Integer = 0
            Dim errors As New List(Of String)

            For Each item In listInfo
                line = line + 1
                Dim order As Integer
                Dim value As Decimal

                If item.Count <> 3 Then
                    errors.Add(String.Format("La linea {0} debe tener el formato: Orden - Descripcion - Valor", line))
                    Continue For
                End If
                If Not Integer.TryParse(item(0), order) Then
                    errors.Add(String.Format("La linea {0} no contiene un orden numerico", line))
                    Continue For
                End If
                If Not Me._listCostActivityStep.Any(Function(s) s.Order = order) Then
                    errors.Add(String.Format("No existe el paso asociado a la linea {0}", line))
                    Continue For
                End If
                If String.IsNullOrEmpty(item(1)) Then
                    errors.Add(String.Format("La linea {0} no contiene una descripcion", line))
                    Continue For
                End If
                If Me._listCostActivityStepAddictionalCost.Any(Function(s) s.Description = item(1)) Then
                    errors.Add(String.Format("Ya existe un costo adicional con la descripcion de la linea {0}", line))
                    Continue For
                End If
                If Not Decimal.TryParse(item(2), value) Then
                    errors.Add(String.Format("La linea {0} tiene un valor del costo no numérico", line))
                    Continue For
                End If
                If value <= 0 Then
                    errors.Add(String.Format("El valor del costo de la linea {0} no puede ser menor o igual a 0", line))
                    Continue For
                End If

                Dim costActivityStep = Me._listCostActivityStep.FirstOrDefault(Function(s) s.Order = order)
                AddCostActivityStepAddictionalCost(New CostActivityStepAddictionalCost With
                {
                    .ParentUUID = costActivityStep.UUID,
                    .CostActivityStepId = costActivityStep.Id,
                    .CostActivityStepOrderDescription = costActivityStep.OrderDescription,
                    .Description = item(1),
                    .Value = value
                }, False)
            Next

            If errors.Count > 0 Then
                Using formulario As New FrmListErrors(errors)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End If
        Me.Cursor = System.Windows.Forms.Cursors.Default
        INDgvStep.HideLoadingPanel()
    End Sub

#End Region

#End Region

End Class