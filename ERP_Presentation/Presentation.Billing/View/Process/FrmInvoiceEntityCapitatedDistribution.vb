'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Miguel Angel Fonseca Castro
' Created          : 2019-03-13
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports DevExpress.Xpo
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Xpo.Base

#End Region

Public Class FrmInvoiceEntityCapitatedDistribution
    Implements IInvoiceEntityCapitatedDistribution, ICustomizableForm

#Region "Builder"

    Public Sub New()
        _ctrAdjustment = New CtrAdjustmentInformation()
        _ctrAdjustment.DebitValueText = "FACTURADO:"
        _ctrAdjustment.CreditValueText = "CONTROLES:"
        _ctrAdjustment.PositiveValue = "GANANCIA: "
        _ctrAdjustment.NegativeValue = "PERDIDA: "

        InitializeComponent()

        _ctrAdjustment.SetInfoFunction(AddressOf getAdjustmentInformation)
        _ctrAdjustment.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_ctrAdjustment)
        _ctrAdjustment.PrintInfo()

        IndigoGridControl1.SetHideNoRecords(INDgcBill, True)
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Billing"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Secuencia numerica del fomulario
    ''' </summary>
    Private Property _sequence As BillingSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _currentSequenceId As Int64

    ''' <summary>
    ''' Presentador del formulario
    ''' </summary>
    Dim _presenter As PInvoiceEntityCapitatedDistribution

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

    ''' <summary>
    ''' bandera para identificar cuando se consulta un registro
    ''' </summary>
    ''' <remarks></remarks>
    Private _isLoad As Boolean

    ''' <summary>
    ''' Control para establecer la informacion de la distribución
    ''' </summary>
    Private _ctrAdjustment As CtrAdjustmentInformation

    ''' <summary>
    ''' representa la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _invoiceEntityCapitatedDistribution As InvoiceEntityCapitatedDistribution

    ''' <summary>
    ''' listado de detalle de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listInvoiceEntityCapitatedDistributionDetail As List(Of InvoiceEntityCapitatedDistributionDetail)

    ''' <summary>
    ''' espera mientras abre el formulario de liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private waitForm As New DevExpress.XtraSplashScreen.SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True)

#End Region

#Region "Properties"

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInvoiceEntityCapitatedDistribution.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IInvoiceEntityCapitatedDistribution.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequense As BillingSequence Implements IInvoiceEntityCapitatedDistribution.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public Property Code As String Implements IInvoiceEntityCapitatedDistribution.Code
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

    Public Property DocumentDate As Date? Implements IInvoiceEntityCapitatedDistribution.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property

    Public Property InvoiceEntityCapitatedId As Integer Implements IInvoiceEntityCapitatedDistribution.InvoiceEntityCapitatedId
        Get
            Return INDsleInvoiceEntityCapitatedId.EditValue
        End Get
        Set(value As Integer)
            INDsleInvoiceEntityCapitatedId.EditValue = value
        End Set
    End Property

    Public Property CareGroupId As Integer Implements IInvoiceEntityCapitatedDistribution.CareGroupId
        Get
            Return INDsleCareGroupId.EditValue
        End Get
        Set(value As Integer)
            INDsleCareGroupId.EditValue = value
        End Set
    End Property

    Public Property InitialDate As Date? Implements IInvoiceEntityCapitatedDistribution.InitialDate
        Get
            Return INDdeInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdeInitialDate.EditValue = value
        End Set
    End Property

    Public Property EndDate As Date? Implements IInvoiceEntityCapitatedDistribution.EndDate
        Get
            Return INDdeEndDate.EditValue
        End Get
        Set(value As Date?)
            INDdeEndDate.EditValue = value
        End Set
    End Property

    Public Property Observation As String Implements IInvoiceEntityCapitatedDistribution.Observation
        Get
            Return INDmeObservation.EditValue
        End Get
        Set(value As String)
            INDmeObservation.EditValue = value
        End Set
    End Property

    Public Property InvoiceValue As Decimal Implements IInvoiceEntityCapitatedDistribution.InvoiceValue
        Get
            Return If(Me._invoiceEntityCapitatedDistribution Is Nothing, 0, Me._invoiceEntityCapitatedDistribution.InvoiceValue)
        End Get
        Set(value As Decimal)
            Me._invoiceEntityCapitatedDistribution.InvoiceValue = value
        End Set
    End Property

    Public ReadOnly Property TotalControlValue As Decimal Implements IInvoiceEntityCapitatedDistribution.TotalControlValue
        Get
            Return If(Me._listInvoiceEntityCapitatedDistributionDetail Is Nothing, 0, Me._listInvoiceEntityCapitatedDistributionDetail.Sum(Function(d) d.InvoiceValue))
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyInvoiceEntityCapitatedDistribution.BeginUpdate()

            'Datos Principales
            INDbeCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDsleInvoiceEntityCapitatedId.Enabled = value
            INDmeObservation.Enabled = value

            BarraBotones.StatusRecordVisible = value

            INDlyInvoiceEntityCapitatedDistribution.EndUpdate()

            If value Then
                INDdeDocumentDate.Focus()
            Else
                INDbeCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

#End Region

#Region "XPO"

    Public Property InvoiceEntityCapitatedXpo As XPInstantFeedbackSource Implements IInvoiceEntityCapitatedDistribution.InvoiceEntityCapitatedXpo
        Get
            Return CType(INDsleInvoiceEntityCapitatedId.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleInvoiceEntityCapitatedId.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Factura Monto Fijo", .FieldName = "InvoiceEntityCapitatedId.Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListInvoiceEntityCapitatedDistributions
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewInvoiceEntityCapitatedDistribution()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If _invoiceEntityCapitatedDistribution.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            Else
                If Me._isLoad Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede guardar porque no ha terminado de cargar las facturas"
                    Exit Sub
                End If
                If _listInvoiceEntityCapitatedDistributionDetail Is Nothing OrElse _listInvoiceEntityCapitatedDistributionDetail.Count = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un detalle"
                    Exit Sub
                End If
            End If
        End If

        AssigningValues()

        Try
            Using model As New MInvoiceEntityCapitatedDistribution(Me.Tag.ToString())
                AsyncLoader(True)
                Dim listInvoiceId = _listInvoiceEntityCapitatedDistributionDetail.Select(Function(d) d.InvoiceId).ToList()
                Dim Result = Await model.SaveInvoiceEntityCapitatedDistribution(_invoiceEntityCapitatedDistribution, listInvoiceId, _currentSequenceId)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me._invoiceEntityCapitatedDistribution = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    INDbeCode.Enabled = False
                    If _invoiceEntityCapitatedDistribution.ChangeTracker.State = ObjectState.Added Then
                        If Result.ObjectEmbbeded IsNot Nothing AndAlso Result.ObjectEmbbeded.Id > 0 Then
                            _invoiceEntityCapitatedDistribution.Id = Result.ObjectEmbbeded.Id
                            _invoiceEntityCapitatedDistribution.Code = Result.ObjectEmbbeded.Code
                            _invoiceEntityCapitatedDistribution.ChangeTracker.State = ObjectState.Modified
                            Code = _invoiceEntityCapitatedDistribution.Code
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

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar
    End Sub

#End Region

#Region "Bar Buttons"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._operativeUnitId Then
            Me._operativeUnitId = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
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
        Buscar()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _invoiceEntityCapitatedDistribution.Status = 2
            Me.Guardar()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _invoiceEntityCapitatedDistribution.Status = 2
            Me.Guardar()
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If _invoiceEntityCapitatedDistribution.Status = 2 Then
            If MessageIndigo.Show("¿Está seguro que desea reversar el documento?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Reversar()
            End If
        Else
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                _invoiceEntityCapitatedDistribution.Status = 3
                Guardar()
            End If
        End If
    End Sub

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        _invoiceEntityCapitatedDistribution.Status = 2
        Me.Guardar()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _invoiceEntityCapitatedDistribution.Id, 0, _invoiceEntityCapitatedDistribution.Code)
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Async Sub FrmInvoiceEntityCapitatedDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyInvoiceEntityCapitatedDistribution, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PInvoiceEntityCapitatedDistribution(Me)
        _presenter.LoadDefinitionLayout()

        AsyncLoader(True)
        Await _presenter.GetSequense()
        AsyncLoader(False)

        Deshacer()
        LoadStatus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _operativeUnitId = Nothing
        _sequence = Nothing
        _currentSequenceId = Nothing
        _presenter = Nothing
        _record = Nothing
        _isLoad = Nothing
        _ctrAdjustment = Nothing
        _invoiceEntityCapitatedDistribution = Nothing
        _listInvoiceEntityCapitatedDistributionDetail = Nothing
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
                    Await Me.NewInvoiceEntityCapitatedDistribution()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    Private Sub INDgvDetails_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgvDetails.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            Dim view = CType(sender, GridView)
            Dim hitInfo As GridHitInfo = view.CalcHitInfo(e.Point)
            view.FocusedRowHandle = hitInfo.RowHandle

            Dim groupRow = view.GetParentRowHandle(view.FocusedRowHandle)
            Dim childRows As Integer = GetChildRowsHandles(view, groupRow)

            PopupMenu1.Manager = BarManager1
            PopupMenu1.ShowPopup(INDgvDetails.GridControl.PointToScreen(e.Point))
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDsleEntityCapitatedId_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleInvoiceEntityCapitatedId.QueryPopUp
        If InvoiceEntityCapitatedXpo Is Nothing Then
            Me._presenter.InitializeInvoiceEntityCapitatedXPO()
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Async Sub INDsleEntityCapitatedId_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleInvoiceEntityCapitatedId.EditValueChanging
        If Not Me._isLoad Then
            If INDsleInvoiceEntityCapitatedId.Properties.ReadOnly Then
                e.Cancel = True
            Else
                If e.NewValue Is Nothing Then
                    INDsleCareGroupId.EditValue = Nothing
                    INDsleCareGroupId.Properties.NullText = String.Empty
                    InitialDate = Nothing
                    EndDate = Nothing
                Else
                    Dim invoiceEntityCapitated = If(INDgvInvoiceEntityCapitatedId.DataSource IsNot Nothing, DirectCast(DirectCast(INDgvInvoiceEntityCapitatedId.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BillingRepository.InvoiceEntityCapitatedXpo), Nothing)
                    If invoiceEntityCapitated Is Nothing Then
                        invoiceEntityCapitated = _presenter.GetinvoiceEntityCapitatedById(e.NewValue)
                    End If
                    'Obtengo el valor del XPO y lo asigno
                    INDsleCareGroupId.EditValue = invoiceEntityCapitated.CareGroupId.Id
                    INDsleCareGroupId.Properties.NullText = invoiceEntityCapitated.CareGroupId.CodeName
                    InitialDate = invoiceEntityCapitated.InitialDate
                    EndDate = invoiceEntityCapitated.EndDate
                    InvoiceValue = invoiceEntityCapitated.TotalValue
                End If
            End If
        End If

        INDlyItemCareGroupId.Visibility = If(e.NewValue Is Nothing, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDlyItemInitialDate.Visibility = If(e.NewValue Is Nothing, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDlyItemEndDate.Visibility = If(e.NewValue Is Nothing, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)

        INDgcBill.DataSource = Nothing
        _listInvoiceEntityCapitatedDistributionDetail = New List(Of InvoiceEntityCapitatedDistributionDetail)
        If e.NewValue IsNot Nothing
            Await LoadDetails(e.NewValue)
        End If

        _ctrAdjustment.PrintInfo()
    End Sub

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim row As InvoiceEntityCapitatedDistributionDetail = INDgvDetails.GetFocusedRow()
            If row IsNot Nothing Then
                If e.NewValue Then
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Add)
                Else
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Remove)
                End If
            End If
        End If
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDbbSelection_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbSelection.ItemClick
        SelectOptions(1)
    End Sub

    Private Sub INDbbUnselection_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbUnselection.ItemClick
        SelectOptions(0)
    End Sub

    Private Sub INDbbSeeLiquidation_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbSeeLiquidation.ItemClick
        OpenLiquidateForm()
    End Sub

#End Region

#Region "MouseDown"

    Private Sub INDgvDetails_MouseDown(sender As Object, e As MouseEventArgs) Handles INDgvDetails.MouseDown
        Dim view As GridView = TryCast(sender, GridView)
        Dim hi As GridHitInfo = view.CalcHitInfo(e.Location)
        If hi.Column IsNot Nothing Then
            If hi.InColumn AndAlso hi.Column.FieldName = "SelectOption" Then
                If Not hi.InRow Then
                    Dim listFilterXpCollection = view.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                    Dim action As CollectionChangeAction = IIf((listFilterXpCollection.Count = cont), CollectionChangeAction.Remove, CollectionChangeAction.Add)
                    For Each row In listFilterXpCollection
                        Me.SelectedOrUnSelected(row, action)
                    Next
                End If
            End If
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Function getAdjustmentInformation() As Tuple(Of Decimal, Decimal)
        Return New Tuple(Of Decimal, Decimal)(InvoiceValue, TotalControlValue)
    End Function

    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    Private Sub CleanControls()
        INDlyInvoiceEntityCapitatedDistribution.BeginUpdate()

        _doc = Nothing
        DeleteBlockedRecord()
        ReadOnlyControls(False)
        BarraBotones.EnableBarItems()
        BarraBotones.DisableBarDocument()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        Code = String.Empty
        DocumentDate = Nothing
        INDsleInvoiceEntityCapitatedId.EditValue = Nothing
        INDsleInvoiceEntityCapitatedId.Properties.NullText = String.Empty
        INDmeObservation.Text = String.Empty

        _invoiceEntityCapitatedDistribution = Nothing
        _listInvoiceEntityCapitatedDistributionDetail = Nothing
        INDgcBill.DataSource = Nothing
        _ctrAdjustment.PrintInfo()

        INDsleCareGroupId.Properties.ReadOnly = True
        INDdeInitialDate.Properties.ReadOnly = True
        INDdeEndDate.Properties.ReadOnly = True
        ActionsOnControls = False

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyInvoiceEntityCapitatedDistribution.EndUpdate()
    End Sub

    Private Async Function NewInvoiceEntityCapitatedDistribution() As Task
        _invoiceEntityCapitatedDistribution = New InvoiceEntityCapitatedDistribution() With {.Status = 1}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.BillingSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._currentSequenceId = Me._sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._operativeUnitId).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._currentSequenceId)) = Await model.GetNumericSequenseGroup(CInt(Me._currentSequenceId))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._currentSequenceId)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._currentSequenceId)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._currentSequenceId))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
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
                Using Model As New MInvoiceEntityCapitatedDistribution(CStr(Me.Tag))
                    AsyncLoader(True)
                    _invoiceEntityCapitatedDistribution = (Await Model.GetInvoiceEntityCapitatedDistribution(INDbeCode.Text.Trim)).ObjectEmbbeded
                    INDlyInvoiceEntityCapitatedDistribution.BeginUpdate()
                    If _invoiceEntityCapitatedDistribution IsNot Nothing AndAlso _invoiceEntityCapitatedDistribution.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_invoiceEntityCapitatedDistribution.Id))
                            With _invoiceEntityCapitatedDistribution
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.BarraBotones.StatusRecordVisible = True

                                _isLoad = True
                                Code = .Code
                                DocumentDate = .DocumentDate
                                InvoiceEntityCapitatedId = .InvoiceEntityCapitatedId
                                INDsleInvoiceEntityCapitatedId.Properties.NullText = .InvoiceEntityCapitatedCode
                                CareGroupId = .CareGroupId
                                INDsleCareGroupId.Properties.NullText = .CareGroupName
                                InitialDate = .InitialDate
                                EndDate = .EndDate
                                Observation = .Observation
                                Me.BarraBotones.StatusRecord = .Status.ToString
                                _isLoad = False
                            End With
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _invoiceEntityCapitatedDistribution.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            If _invoiceEntityCapitatedDistribution.Status = 1 Then
                                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Anular, ResourceManager.GetString("CaptionAnnulBarButton"))
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                            ElseIf _invoiceEntityCapitatedDistribution.Status = 2 Then
                                Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Anular, ResourceManager.GetString("CaptionReverseBarButton"))
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAnnular)
                                Me.ReadOnlyControls(True)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                            End If
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._invoiceEntityCapitatedDistribution.Code)
                            Me.BarraBotones.SetDocuments(_invoiceEntityCapitatedDistribution.Id, Me.Tag.ToString(), Nothing, GetType(InvoiceEntityCapitatedDistribution).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            'Para la impresion
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _invoiceEntityCapitatedDistribution.Id, 0, _invoiceEntityCapitatedDistribution.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewInvoiceEntityCapitatedDistribution()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbeCode.Focus()
                        End If
                    End If
                    INDlyInvoiceEntityCapitatedDistribution.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbeCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Private Async Function LoadDetails(ByVal invoiceEntityCapitatedId As Integer) As Task
        Using Model As New MInvoiceEntityCapitatedDistribution(CStr(Me.Tag))
            AsyncLoader(True)

            Dim resultDetails = Await Model.GetInvoiceEntityCapitatedDistributionDetailsByInvoiceEntityCapitatedId(invoiceEntityCapitatedId, _invoiceEntityCapitatedDistribution.Id)
            If resultDetails.StateResult Then
                INDgcBill.DataSource = resultDetails.ObjectEmbbeded
                _listInvoiceEntityCapitatedDistributionDetail = resultDetails.ObjectEmbbeded.Where(Function(d) d.SelectOption).ToList()
            Else
                Mensaje(EeventViewerImages.Advertencia) = resultDetails.Message
            End If

            AsyncLoader(False)
        End Using
    End Function

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

    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    Private Sub AssigningValues()
        With _invoiceEntityCapitatedDistribution
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = BarraBotones.OperatingUnit.Id
            .Code = Code
            .DocumentDate = DocumentDate
            .InvoiceEntityCapitatedId = InvoiceEntityCapitatedId
            .Observation = Observation

            .InvoiceEntityCapitatedDistributionDetail.Clear()

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._invoiceEntityCapitatedDistribution.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._invoiceEntityCapitatedDistribution.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._invoiceEntityCapitatedDistribution.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._invoiceEntityCapitatedDistribution.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._invoiceEntityCapitatedDistribution.Code)
            Return Me._doc
        End If
    End Function

    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._invoiceEntityCapitatedDistribution IsNot Nothing AndAlso Me._invoiceEntityCapitatedDistribution.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbeCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbeCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    Public Function GetChildRowsHandles(view As GridView, groupRowHandle As Integer) As Integer
        Dim childRows As Integer = 0
        If Not view.IsGroupRow(groupRowHandle) Then
            childRows = 1
            Return childRows
        End If
        Return childRows
    End Function

    Private Sub SelectOptions(optionCheck As Integer)
        If _invoiceEntityCapitatedDistribution Is Nothing OrElse _invoiceEntityCapitatedDistribution.Status <> 1 Then
            Exit Sub
        End If

        Dim view As GridView = INDgvDetails
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), optionCheck)
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    If optionCheck = 0 Then
                        Me.SelectedOrUnSelected(row, CollectionChangeAction.Remove)
                    Else
                        Me.SelectedOrUnSelected(row, CollectionChangeAction.Add)
                    End If
                End If
            Next
            INDgcBill.RefreshDataSource()
        End If
    End Sub

    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, optionCheck As Integer)
        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, optionCheck)
            Else
                Dim row As Object = view.GetRow(childHandle)
                If optionCheck = 0 Then
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Remove)
                Else
                    Me.SelectedOrUnSelected(row, CollectionChangeAction.Add)
                End If
            End If
        Next
    End Sub

    Private Sub SelectedOrUnSelected(row As InvoiceEntityCapitatedDistributionDetail, action As CollectionChangeAction)
        If _invoiceEntityCapitatedDistribution.Status <> 1 Then
            Exit Sub
        End If

        If row IsNot Nothing
            If action = CollectionChangeAction.Add And Not row.SelectOption Then
                row.SelectOption = True
                If Not Me._listInvoiceEntityCapitatedDistributionDetail.Any(Function(d) d.InvoiceId = row.Id) Then
                    Me._listInvoiceEntityCapitatedDistributionDetail.Add(row)
                End If
            ElseIf action = CollectionChangeAction.Remove And row.SelectOption Then
                row.SelectOption = False
                Me._listInvoiceEntityCapitatedDistributionDetail.Remove(row)
            End If

            If Me._listInvoiceEntityCapitatedDistributionDetail.Any()
                INDsleInvoiceEntityCapitatedId.Properties.ReadOnly = True
            Else
                INDsleInvoiceEntityCapitatedId.Properties.ReadOnly = False
            End If

            _ctrAdjustment.PrintInfo()
            INDgcBill.RefreshDataSource()
        End If
    End Sub

    Private Sub OpenLiquidateForm()
        If INDgvDetails.GetFocusedRow() Is Nothing Then
            Exit Sub
        End If
        If Not waitForm.IsSplashFormVisible Then
            waitForm.ShowWaitForm()
        End If
        Dim row As InvoiceEntityCapitatedDistributionDetail = INDgvDetails.GetFocusedRow()
        Using FrmLiquidation As New FrmLiquidation()
            FrmLiquidation.Size = New System.Drawing.Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height)
            FrmLiquidation.MinimizeBox = False
            FrmLiquidation.MaximizeBox = False
            Using m As New MControlOutpatientServices(Me.Tag)
                Dim admissionCollection = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.HisContainer).CrystalService.Liquidation_GetAdmission(row.AdmissionNumber)
                FrmLiquidation.AuxAdmissionToReload = admissionCollection
            End Using
            FrmLiquidation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            AddHandler FrmLiquidation.Shown, Sub()
                                                 waitForm.CloseWaitForm()
                                             End Sub
            Dim transparent As New FrmTransparent(FrmLiquidation, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Public Async Sub Reversar()
        Try 
            Using model As New MInvoiceEntityCapitatedDistribution(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result = Await model.ReverseInvoiceEntityCapitatedDistribution(_invoiceEntityCapitatedDistribution)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _invoiceEntityCapitatedDistribution.Id, 0, _invoiceEntityCapitatedDistribution.Id)

                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

End Class