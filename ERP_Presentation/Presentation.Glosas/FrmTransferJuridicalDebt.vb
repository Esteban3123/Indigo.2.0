'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz
' Created          : 15-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
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
Imports Presentation.Glosas.MVP
Imports Presentation.Payments.MVP
Imports Presentation.Portfolio.MVP

#End Region
''' <summary>
''' Vista del frontal de traslado cobro jurídico
''' </summary>
Public Class FrmTransferJuridicalDebt
    Implements ITransferJuridicalDebt

#Region "Builder"

    ''' <summary>
    ''' Initialize
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _openFindSenser = "INDConsecutiveBte".ToUpper()
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PTransferJuridicalDebt

    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _modelJuridical As MTransferJuridicalDebt

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Parámetros de Cartera
    ''' </summary>
    Private _settingPortfolio As SettingPortfolio

    ''' <summary>
    ''' Encapsula el cliente consultado
    ''' </summary> 
    Private _customer As Domain.Entities.Customer

    ''' <summary>
    ''' Entidad del traslado cobro jurídico
    ''' </summary>
    Private _juridicalC As Domain.Entities.TransferJuridicalDebtCollectionC

    ''' <summary>
    ''' Entidad del traslado cobro jurídico detalle
    ''' </summary>
    Private _rowInvoice As Domain.Entities.TransferJuridicalDebtCollectionC

    ''' <summary>
    ''' Lista de detalles de traslado cobro jurídico
    ''' </summary>
    Private _detailJuridical As List(Of Domain.Entities.TransferJuridicalDebtCollectionD)

    ''' <summary>
    ''' Encapsula el estado actual en que se encuentra el proceso
    ''' </summary>
    Private _currentStatus As ActionOnControlsTypeTransferJuridical

    ''' <summary>
    ''' Bandera que me indica si el traslado cobro jurídico es nuevo
    ''' </summary>
    Private _isNew As Boolean = True
    ''' <summary>
    ''' Almacena el nombre del control que hizo el llamado al metodo buscar
    ''' </summary>
    Private _openFindSenser As String

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Bandera para search de unidad radicacion (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banFilingUnit As Boolean = True

#End Region

#Region "Properties"

    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    ''' <value>Valor que indica si se activan o inactivan controles</value>
    Public WriteOnly Property ActionsOnControls As ActionOnControlsTypeTransferJuridical Implements ITransferJuridicalDebt.ActionsOnControls
        Set(value As ActionOnControlsTypeTransferJuridical)
            INDlycRoot.BeginUpdate()
            INDEsbBills.Enabled = value
            INDsleFilingUnitSource.Enabled = value
            INDsleFilingUnitTarget.Enabled = value
            INDBtnImportFile.Enabled = value
            Me.INDSleLawyer.Enabled = True
            Me.INDSleDemandStatus.Enabled = True
            Select Case value
                Case ActionOnControlsTypeTransferJuridical.NewJuridical
                    Me._isNew = True
                    Me.INDConsecutiveBte.Enabled = False
                    Me.INDbteNit.Enabled = True
                    Me.INDDocumentNumberTxt.Enabled = True
                    Me.INDDocumentNumberTxt.Properties.ReadOnly = False
                    Me.INDCommentPce.Enabled = True
                    Me.INDCommentMem.Enabled = True
                    Me.INDCommentMem.Properties.ReadOnly = False
                    Me.INDDocumentDateDte.Enabled = True
                    Me.INDDocumentDateDte.Properties.ReadOnly = False
                    Me.INDDocumentDateDte.EditValue = Date.Today
                    Me.INDInvoicesDetailGc.Enabled = True
                    Me.INDpceAddInvoice.Enabled = True
                    Me.INDInvoicesSle.Enabled = True
                    Me.INDbtnAddInvoice.Enabled = True
                    Me.INDbteNit.Focus()
                    Me._currentStatus = value
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    ' Me.BarraBotones.PrepareToolbar(eAction.Save)
                    Me.BarraBotones.RibbonPageProcesos.Visible = False
                    Me.BarraBotones.StatusRecord = ActionOnControlsTypeTransferJuridical.UnconfirmedJuridical
                    Me.BarraBotones.StatusRecordVisible = True
                Case ActionOnControlsTypeTransferJuridical.UnconfirmedJuridical
                    Me._isNew = False
                    Me.INDConsecutiveBte.Enabled = False
                    Me.INDbteNit.Enabled = False
                    Me.INDDocumentNumberTxt.Enabled = True
                    Me.INDDocumentNumberTxt.Properties.ReadOnly = False
                    Me.INDCommentPce.Enabled = True
                    Me.INDCommentMem.Enabled = True
                    Me.INDCommentMem.Properties.ReadOnly = False
                    Me.INDDocumentDateDte.Enabled = True
                    Me.INDDocumentDateDte.Properties.ReadOnly = False
                    Me.INDDocumentDateDte.EditValue = Date.Today
                    Me.INDInvoicesDetailGc.Enabled = True
                    Me._currentStatus = value
                    Me.INDpceAddInvoice.Enabled = True
                    Me.INDInvoicesSle.Enabled = True
                    Me.INDbtnAddInvoice.Enabled = True
                    Me.INDInvoicesSle.Focus()
                Case ActionOnControlsTypeTransferJuridical.ConfirmedJuridical
                    Me._isNew = False
                    Me.INDConsecutiveBte.Enabled = True
                    Me.INDbteNit.Enabled = False
                    Me.INDDocumentDateDte.Enabled = False
                    Me.INDDocumentNumberTxt.Enabled = False
                    Me.INDRadicateDateLbl.Enabled = False
                    Me.INDCommentPce.Enabled = False
                    Me.INDpceAddInvoice.Enabled = False
                    Me.INDInvoicesDetailGc.Enabled = True
                    Me._currentStatus = value

                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                Case ActionOnControlsTypeTransferJuridical.InvalidateJuridical
                    Me._isNew = False
                    Me.INDConsecutiveBte.Enabled = True
                    Me.INDbteNit.Enabled = False
                    Me.INDDocumentDateDte.Enabled = False
                    Me.INDDocumentNumberTxt.Enabled = False
                    Me.INDRadicateDateLbl.Enabled = False
                    Me.INDCommentPce.Enabled = False
                    Me.INDpceAddInvoice.Enabled = False
                    Me.INDInvoicesDetailGc.Enabled = True
                    Me._currentStatus = value
                    Me.INDDocumentNumberTxt.Focus()
                Case Else 'Listo para consultar
                    Me._isNew = True
                    Me.INDConsecutiveBte.Enabled = True
                    Me.INDbteNit.Enabled = False
                    Me.INDDocumentDateDte.Enabled = False
                    Me.INDDocumentNumberTxt.Enabled = False
                    INDRadicateDateLbl.Enabled = False
                    Me.INDCommentPce.Enabled = False
                    Me.INDpceAddInvoice.Enabled = False
                    Me.INDInvoicesDetailGc.Enabled = False
                    Me.BarraBotones.StatusRecord = ActionOnControlsTypeTransferJuridical.UnconfirmedJuridical
                    Me.BarraBotones.StatusRecordVisible = False
                    Me._currentStatus = ActionOnControlsTypeTransferJuridical.WaitingQuery
                    Me.INDSleLawyer.Enabled = False
                    Me.INDSleDemandStatus.Enabled = False
                    If Me.FormSearchObjects IsNot Nothing AndAlso Me.FormSearchObjects.Visible = True Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
                    End If
                    Me.BarraBotones.RibbonPageProcesos.Visible = False
                    Me.INDConsecutiveBte.Focus()
                    Me._openFindSenser = Me.INDConsecutiveBte.Name.ToUpper
            End Select
            INDlycRoot.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property FilingUnitSourceId As Integer? Implements ITransferJuridicalDebt.FilingUnitSourceId
        Get
            Return INDsleFilingUnitSource.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnitSource.EditValue = value
        End Set
    End Property

    Public Property FilingUnitTargetId As Integer? Implements ITransferJuridicalDebt.FilingUnitTargetId
        Get
            Return INDsleFilingUnitTarget.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnitTarget.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property LawyerId As Integer? Implements ITransferJuridicalDebt.LawyerId
        Get
            Return INDSleLawyer.EditValue
        End Get
        Set(value As Integer?)
            INDSleLawyer.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property DemandStatusId As Integer? Implements ITransferJuridicalDebt.DemandStatusId
        Get
            Return INDSleDemandStatus.EditValue
        End Get
        Set(value As Integer?)
            INDSleDemandStatus.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceCustomers As XPInstantFeedbackSource

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomers()
        Using msearch As New MBusqueda
            DatasourceCustomers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Customers)
            INDbteNit.Datasource = DatasourceCustomers
        End Using
    End Sub

    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property FilingUnitSourceXpo As List(Of FilingUnit) Implements ITransferJuridicalDebt.FilingUnitSourceXpo
        Get
            Return INDsleFilingUnitSource.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDsleFilingUnitSource.Properties.DataSource = value
        End Set
    End Property

    Public Property FilingUnitTargetXpo As XPCollection Implements ITransferJuridicalDebt.FilingUnitTargetXpo
        Get
            Return INDsleFilingUnitTarget.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleFilingUnitTarget.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property Lawyers As XPInstantFeedbackSource Implements ITransferJuridicalDebt.Lawyers
        Get
            Return INDSleLawyer.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleLawyer.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property DemandStatusDataSource As XPInstantFeedbackSource Implements ITransferJuridicalDebt.DemandStatusDataSource
        Get
            Return INDSleDemandStatus.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleDemandStatus.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' retorna o asigna el datasource de factura aptas para traslado a cobro jurídico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property datasourceXPOInvoice As XPInstantFeedbackSource Implements ITransferJuridicalDebt.datasourceXPOInvoice
        Get
            Return Me.INDInvoicesSle.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            Me.INDInvoicesSle.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para asignar los detalles de traslado a cobro jurídico
    ''' </summary>
    ''' <value>Objeto</value>
    Public Property SelectedInvoices As Object Implements ITransferJuridicalDebt.SelectedInvoices
        Get
            Return Me.INDInvoicesDetailGc.DataSource
        End Get
        Set(value As Object)
            Me.INDInvoicesDetailGc.DataSource = value
            Me.INDInvoicesDetailGc.RefreshDataSource()
        End Set
    End Property

#End Region

#Region "ICrudBase"

    ''' <summary>
    ''' Metodo para abrir el control de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        If Not (FormSearchObjects IsNot Nothing AndAlso FormSearchObjects.Visible = True) Then
            FormSearchObjects = New FrmBusqueda()
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
            Select Case Me._openFindSenser.ToUpper()
                Case Me.INDConsecutiveBte.Name.ToUpper() 'Abre el frontal para buscar consecutivos
                    With FormSearchObjects
                        .ListaColumnas = {New ColumnInfo With {.Caption = "Consecutivo", .FieldName = "JuridicalTransferConsecutive"}, New ColumnInfo With {.Caption = "Factura", .FieldName = "InvoiceNumber"}, New ColumnInfo With {.Caption = "Entidad", .FieldName = "NitName"}}.ToList()
                        .ValorSolicitado = "JuridicalTransferConsecutive"
                        .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.TransferJuridicalDebt
                        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        .FormParent = Me
                        .ShowSearch(False)
                    End With
                Case Me.INDbteNit.Name.ToUpper() 'Abre el frontal para buscar terceros
                    With FormSearchObjects
                        .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
                        .ValorSolicitado = "Nit"
                        .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
                        .FormParent = Me
                        .ShowSearch(False)
                    End With
                Case Else 'Si esta sobre un control en donde no aplica el metodo buscar
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoAplicaBuscar, Eform.Comunes)
            End Select
            SearchMode = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Select Case Me._openFindSenser.ToUpper()
            Case Me.INDConsecutiveBte.Name.ToUpper()
                If Not String.IsNullOrEmpty(ReturnValue) Then
                    Me.INDConsecutiveBte.Text = ReturnValue
                    Me.UnblockeRecord()
                    LoadControls()
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Metodo base Buscar
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        Me.AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Metodo base Deshacer
    ''' </summary>gu
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Me.CleanControls()
        If SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo base Nuevo
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Me.CleanControls()
    End Sub

    ''' <summary>
    ''' Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If Not Me.ValidateControls() Then
                Exit Sub
            End If
            If Me._detailJuridical.Count = 0 OrElse Me._detailJuridical.TrueForAll(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted) Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
                Exit Sub
            End If

            Me.AssignValues()
            Me._juridicalC.State = "1"
			Await SaveTransferJuridicalDebtDetails()
			Deshacer()
		Catch ex As Exception
            Me.AsyncLoader(False)
            INDConsecutiveBte.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Logica actualizar barra botones
    ''' </summary>
    ''' <param name="existeDatos">Boolean</param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Metodo base Guardar
    ''' </summary>
    Public Async Sub SaveOrUpdateAndConfirm(action As Integer)
        Try
            If Not Me.ValidateControls() Then
                Exit Sub
            End If
            If Me._detailJuridical.Count = 0 OrElse Me._detailJuridical.TrueForAll(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted) Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
                Exit Sub
            End If

            Me.AssignValues()
            Me._juridicalC.State = "2"
            Await SaveTransferJuridicalDebtDetails()
        Catch ex As Exception
            Me.AsyncLoader(False)
            INDConsecutiveBte.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo base Eliminar
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' Propiedad para mostrar mensajes al usuario
    ''' </summary>
    ''' <param name="Icono">Icono según acción</param>
    ''' <value>Valor</value>
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

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Me.BarraBotones.RibbonPageProcesos.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True
    End Sub

    ''' <summary>
    ''' Evento guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Evento actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Evento anular
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Me.InvalidateTransferJuridical()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveOrUpdateAndConfirm(1) 'Insert
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de actualizar confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2) 'Update
    End Sub

    ''' <summary>
    ''' Evento confirmar
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Me.ConfirmTransferJuridical()
    End Sub

    ''' <summary>
    ''' Evento buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub

    ''' <summary>
    ''' Evento nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.NewJuridical()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _juridicalC.Id, 0, _juridicalC.Id)
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
    ''' Evento confirmar
    ''' </summary>
    Private Sub Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        Me.ReverseTransferJuridical()
    End Sub

    ''' <summary>
    ''' Evento customizar
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Evento reestablecer
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        Me.ResetLayout()
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub FrmTransferJuridicalDebt_Load(sender As Object, e As EventArgs) Handles Me.Load
        INDEsbBills.AddRangeColumns("Nº Factura")
        '******Inicializar variables******'
        Me._isNew = True
        Me._openFindSenser = String.Empty
        Me._modelJuridical = New MTransferJuridicalDebt(Me.Tag)
        Me.Funct = AddressOf GenerateDoc
        '*********************************'
        Me.INDbteNit.FuncQueryOnKeyEnterPressed = AddressOf Me.INDbteNit_KeyDown
        Me.INDbteNit.View.OptionsView.ShowGroupPanel = False
        Me._doc = Nothing
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecordEnabled = False
        'Me.IndigoGridControl1.SetHoldSize(Me.INDInvoicesDetailGc, True)
        Me.IndigoGridControl1.RefreshGrid(Me.INDInvoicesDetailGc)
        Me._presenter = New PTransferJuridicalDebt(Me)
        Me.INDDocumentDateDte.Properties.MaxValue = Date.Today
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Deshacer()
        SearchMode = False
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        banFilingUnit = Nothing
        _isNew = Nothing
        _openFindSenser = Nothing
        _customer = Nothing
        _juridicalC = Nothing
        _rowInvoice = Nothing
        _detailJuridical = Nothing
        _currentStatus = Nothing
        _modelJuridical = Nothing
        _presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        DatasourceCustomers = Nothing
        SearchMode = Nothing
        varImp = Nothing
    End Sub

#End Region

#Region "Activated"

    Private Async Sub FrmTransferJuridicalDebt_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDConsecutiveBte.Enabled Then
            INDConsecutiveBte.Focus()
        End If
        'Se carga el primer control de filingUnitSource
        Using model As New MFilingUnit(Tag)
            Dim x As ActionResult(Of List(Of FilingUnit)) = Await model.GetFilingUnitByUser(indigo.UserIndigo)
            Dim listFilingUnit As List(Of FilingUnit) = x.ObjectEmbbeded
            FilingUnitSourceXpo = listFilingUnit
        End Using
        'Se carga el segundo control de filingUnitTarget
        _presenter.InitializeFilingUnit()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._juridicalC IsNot Nothing AndAlso Me._juridicalC.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.record IsNot Nothing AndAlso Me.INDConsecutiveBte.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDConsecutiveBte.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDConsecutiveBte.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Evento al cerrar el frontal
    ''' </summary>
    Private Sub FrmTransferJuridicalDebt_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.UnblockeRecord()
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Permite la busqueda de una devolución por su numero de consecutivo
    ''' o la creación de una nueva si no se proporciona uno.
    ''' </summary>
    Private Sub INDConsecutiveBte_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDConsecutiveBte.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(Me.INDConsecutiveBte.Text.Trim()) AndAlso Not String.IsNullOrWhiteSpace(Me.INDConsecutiveBte.Text.Trim()) AndAlso Not Me.INDConsecutiveBte.Text.Trim().Equals("") AndAlso Not Me.INDConsecutiveBte.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoTienePermisos, Eform.Comunes)
                    Exit Sub
                End If
                Me.LoadControls() 'Consultamos el consecutivo
                Me.INDConsecutiveBte.Enabled = False
                Me.INDInvoicesSle.EditValue = Nothing
            Else
                Me.NewJuridical()
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Async Function INDbteNit_KeyDown(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Using model As New MTransferJuridicalDebt(Me.Tag)
            _customer = Await model.GetCustomerByNit(nit)
            Me.datasourceXPOInvoice = model.ListPortfolio_TransferJuridicalAccountReceivable(nit)
            Me.INDDocumentNumberTxt.Focus()
        End Using
        Return _customer
    End Function

    ''' <summary>
    ''' presion sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerEdit1_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddInvoice.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Or e.KeyCode = Keys.F4 Then
            INDpceAddInvoice.ShowPopup()
            INDInvoicesSle.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteNit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDbteNit.QueryPopUp
        If INDbteNit.Datasource Is Nothing Then
            LoadXpoCustomers()
        End If
    End Sub

    Private Sub INDsleFilingUnitTarget_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFilingUnitTarget.QueryPopUp
        If FilingUnitTargetXpo Is Nothing Then
            _presenter.InitializeFilingUnit()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleLawyer_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleLawyer.QueryPopUp
        If Me.Lawyers Is Nothing Then
            Me._presenter.ListLawyerActive()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleDemandStatus_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleDemandStatus.QueryPopUp
        If Me.DemandStatusDataSource Is Nothing Then
            Me._presenter.ListDemandStatusActive()
        End If
    End Sub

    Private Sub INDInvoicesSle_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDInvoicesSle.QueryPopUp
        If datasourceXPOInvoice Is Nothing Then
            If _customer IsNot Nothing Then
                Me.datasourceXPOInvoice = _modelJuridical.ListPortfolio_TransferJuridicalAccountReceivable(_customer.Nit)
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Async Sub INDbteNit_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDbteNit.EditValueChanged
        If Me.INDbteNit.EditValue IsNot Nothing AndAlso Me.INDbteNit.EditValue > 0 Then
            datasourceXPOInvoice = Nothing
            Me._customer = Await _modelJuridical.GetCustomerById(Me.INDbteNit.EditValue)
        End If
    End Sub

    Private Sub INDsleFilingUnitSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnitSource.EditValueChanged
        If FilingUnitSourceId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnitSource.ValidateFilingUnit()
        End If
        ValidateFilingUnitTarget()
    End Sub

    Private Sub INDsleFilingUnitTarget_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnitTarget.EditValueChanged
        If FilingUnitTargetId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnitTarget.ValidateFilingUnit()
        End If
        ValidateFilingUnitTarget()
    End Sub

    Private Sub INDSleLawyer_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleLawyer.EditValueChanged
        If INDSleLawyer.EditValue Is Nothing Then
            INDSleLawyer.Properties.NullText = String.Empty
        End If
    End Sub

    Private Sub INDSleDemandStatus_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleDemandStatus.EditValueChanged
        If INDSleDemandStatus.EditValue Is Nothing Then
            INDSleDemandStatus.Properties.NullText = String.Empty
        End If
    End Sub

#End Region

#Region "GotFocus"

    ''' <summary>
    ''' Asigna el nombre del sender a la variable _openFindSender para habilitar
    ''' la funcionalidad de buscar en el control que corresponda
    ''' </summary>
    Private Sub OpenFindSender_GotFocus(sender As Object, e As EventArgs) Handles INDConsecutiveBte.GotFocus
        Me._openFindSenser = CType(sender, System.Windows.Forms.Control).Name.ToUpper()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Realiza la busqueda de la factura seleccionada y la agrega a la rejilla
    ''' </summary>
    Private Async Sub INDbtnAddInvoice_Click(sender As Object, e As EventArgs) Handles INDbtnAddInvoice.Click
        Try
            AsyncLoader(True)
            INDbtnAddInvoice.Enabled = False
            If Me._customer IsNot Nothing AndAlso Me.INDInvoicesSle.EditValue IsNot Nothing Then
                If Me._detailJuridical.Any(Function(d) d.InvoiceNumber.Equals(Me.INDInvoicesSle.Text)) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.FacturaYaExisteTrasladoCobroJuridico, Eform.TrasladoCobroJurídico)
                    Me.INDInvoicesSle.EditValue = Nothing
                Else
                    Using model As New MTransferJuridicalDebt(Me.Tag)
                        Dim result = Await model.TransferJuridicalDebtCopyPaste(Me._idOperativeUnit, INDbteNit.EditValue, _juridicalC.Id, 3, Nothing, New List(Of List(Of String)) From {New List(Of String) From {Me.INDInvoicesSle.Text}})
                        If result.StateResult = False OrElse result.MessageResult.Count > 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = If(result.StateResult = False, result.Message, result.MessageResult.FirstOrDefault)
                        Else
                            For Each item In result.ObjectEmbbeded
                                _detailJuridical.Add(item)
                            Next
                            Me.SelectedInvoices = Me._detailJuridical
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
        Finally
            Me.AsyncLoader(False)
            INDbtnAddInvoice.Enabled = True
            Me.INDInvoicesSle.Focus()
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemButtonEdit1_Click(sender As Object, e As EventArgs) Handles INDRepositoryBtnDelete.Click
        DeleteInvoice()
    End Sub

    ''' <summary>
    ''' Evento Click sobre el boton eliminar de la rejilla de detalles de Devolución
    ''' </summary>
    Private Sub INDDeleteActionBtn_Click(sender As Object, e As EventArgs) Handles INDDeleteActionBtn.Click
        Me.DeleteInvoice()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el frontal de busqueda para filtrar las devoluciones
    ''' </summary>
    Private Sub INDConsecutiveBte_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDConsecutiveBte.Properties.ButtonClick
        Me.AbrirBusqueda()
    End Sub

#End Region

#Region "OpenFormButtonClick"

    Private Sub INDbteNit_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDbteNit.OpenFormButtonClick
        OpenForm(503, Nothing, True)
        LoadXpoCustomers()
    End Sub

#End Region

#Region "TxtFindControlEnter"

    ''' <summary>
    ''' Captura el evento enter en el control TextEdit de la caja de busqueda
    ''' en los controles SearchLookUpEdit que extienden su funcionalidad
    ''' </summary>
    Private Sub INDSearchLookUpControlExtended_TxtFindControlEnter(sender As Object, e As TxtFindControlEnterEventArgs) Handles INDSearchLookUpControlExtended.TxtFindControlEnter
        If Not e.TxtFind.Text.Trim().Equals(String.Empty) AndAlso e.BaseView.RowCount > 1 Then
            e.BaseView.Focus()
            e.BaseView.FocusedRowHandle = e.BaseView.GetRowHandle(0)
        ElseIf Not e.TxtFind.Text.Trim().Equals(String.Empty) AndAlso e.BaseView.RowCount = 1 AndAlso e.BaseView.GetRowCellValue(0, "InvoiceNumber") IsNot Nothing AndAlso e.BaseView.GetRowCellValue(0, "InvoiceNumber").GetType().FullName <> "DevExpress.Data.NotLoadedObject" Then
            Dim aux As New String(e.BaseView.GetRowCellValue(0, "InvoiceNumber"))
            Me.INDInvoicesSle.ClosePopup()
            Me.INDInvoicesSle.EditValue = aux
            Me.INDInvoicesSle.Text = aux
        End If
    End Sub

#End Region

#Region "PasteToGrid"

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If _juridicalC.State = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento esta confirmado"
            Exit Sub
        End If
        If INDbteNit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Cliente"
            Exit Sub
        End If
        AsyncLoader(True)
        Using model As New MTransferJuridicalDebt(Me.Tag)
            Dim result = Await model.TransferJuridicalDebtCopyPaste(Me._idOperativeUnit, INDbteNit.EditValue, _juridicalC.Id, 2, Nothing, e.Rows)

            'si ocurrio un error
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                AsyncLoader(False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If

            For Each item In result.ObjectEmbbeded
                Dim bill = _detailJuridical.Where(Function(x) x.InvoiceNumber = item.InvoiceNumber).FirstOrDefault()
                If bill Is Nothing Then
                    _detailJuridical.Add(item)
                Else
                    result.MessageResult.Add("La factura " & bill.InvoiceNumber & " ya esta agregada")
                End If
            Next

            Me.SelectedInvoices = Me._detailJuridical

            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Using
        AsyncLoader(False)
    End Sub

    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        If _juridicalC.State = 2 Then
            Mensaje(EeventViewerImages.Advertencia) = "El documento esta confirmado"
            Exit Sub
        End If
        If INDbteNit.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un Cliente"
            Exit Sub
        End If
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document

                    rows = workBook.Worksheets(0).Rows
                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If

                    Using model As New MTransferJuridicalDebt(Me.Tag)
                        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

                        SetRow(1, rows.LastUsedIndex + 1)

                        Dim result = Await model.TransferJuridicalDebtCopyPaste(Me._idOperativeUnit, INDbteNit.EditValue, _juridicalC.Id, 1, listRows.ToList(), Nothing)

                        'si ocurrio un error
                        If result.StateResult = False Then
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            AsyncLoader(False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If


                        For Each item In result.ObjectEmbbeded
                            Dim bill = _detailJuridical.Where(Function(x) x.InvoiceNumber = item.InvoiceNumber).FirstOrDefault()
                            If bill Is Nothing Then
                                _detailJuridical.Add(item)
                            Else
                                result.MessageResult.Add("La factura " & bill.InvoiceNumber & " ya esta agregada")
                            End If
                        Next

                        Me.SelectedInvoices = Me._detailJuridical

                        If result.MessageResult.Count > 0 Then
                            Using formulario As New FrmListErrors(result.MessageResult)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                transparent.ShowDialog(Me)
                            End Using
                        End If
                    End Using
                End If
                AsyncLoader(False)
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(1)})
                                              End SyncLock
                                          End Sub)
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = ActionOnControlsTypeTransferJuridical.ConfirmedJuridical, .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = ActionOnControlsTypeTransferJuridical.UnconfirmedJuridical, .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = ActionOnControlsTypeTransferJuridical.InvalidateJuridical, .StatusName = ResourceManager.GetString("StateInvalidate"), .StatusColor = Drawing.Color.OrangeRed}) 'System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()
        Me.INDConsecutiveBte.Text = String.Empty
        Me._customer = Nothing
        INDbteNit.EditValue = Nothing
        INDbteNit.DisplayNullText = String.Empty
        Me._detailJuridical = New List(Of TransferJuridicalDebtCollectionD)
        Me._juridicalC = New TransferJuridicalDebtCollectionC
        Me.INDInvoicesSle.Properties.DataSource = Nothing
        Me.INDInvoicesSle.EditValue = Nothing
        Dim d As DateTime = GetServerDate()
        Me.INDRadicateDateLbl.Tag = d
        Me.INDRadicateDateLbl.Text = d.ToString("D", indigo.Culture) 'DateTime.Now.ToLongDateString()
        Me.INDDocumentDateDte.EditValue = String.Empty
        Me.INDDocumentNumberTxt.Text = String.Empty
        Me.INDCommentMem.Text = String.Empty
        Me.INDInvoicesDetailGc.DataSource = Nothing
        Me.UnblockeRecord()
        Me._doc = Nothing
        FilingUnitSourceId = Nothing
        FilingUnitTargetId = Nothing
        Me.LawyerId = Nothing
        INDSleLawyer.Properties.NullText = String.Empty
        Me.DemandStatusId = Nothing
        INDSleDemandStatus.Properties.NullText = String.Empty
        Me._settingPortfolio = Nothing
        Me.BarraBotones.DisableBarDocument()
        Me.INDRepositoryBtnDelete.Buttons(0).Enabled = True
        Me.Actions.OptionsColumn.AllowEdit = True
        Me.BarraBotones.CleanAuditBasic()
        Me.ActionsOnControls = ActionOnControlsTypeTransferJuridical.WaitingQuery
        INDlycRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub UnblockeRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await _modelJuridical.DeleteBlockRecord(record)
            record = Nothing
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Prepara el formulario para un nuevo traslado a cobro jurídico
    ''' </summary>
    Private Sub NewJuridical()
        Me.CleanControls()
        Me.INDConsecutiveBte.Text = ResourceManager.GetString("LabelOrTextboxNew")
        Me.ActionsOnControls = ActionOnControlsTypeTransferJuridical.NewJuridical
        Me._juridicalC = New Domain.Entities.TransferJuridicalDebtCollectionC
    End Sub

    ''' <summary>
    ''' Cargar los controles con los datos del objeto traslado cobro jurídico
    ''' </summary>
    Private Async Sub LoadControls()
        INDlycRoot.BeginUpdate()
        Me.AsyncLoader(True)
        Using Model As New MTransferJuridicalDebt(Me.Tag)
            Me._juridicalC = Await Model.GetJuridicalByConsecutive(Me.INDConsecutiveBte.Text.Trim())
            If Not Me._juridicalC Is Nothing AndAlso Me._juridicalC.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, Me._juridicalC.Id)
                With Me._juridicalC
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    INDbteNit.EditValue = .CustomerId
                    INDbteNit.DisplayNullText = .Customer.Nit & " - " & .Customer.Name
                    Me.INDRadicateDateLbl.Text = .JuridicalTransferDate.ToString("D", indigo.Culture)
                    Me.INDDocumentNumberTxt.Text = .DocumentNumber
                    Me.INDDocumentDateDte.EditValue = .DocumentDate
                    banFilingUnit = False
                    FilingUnitSourceId = .FilingUnitSourceId
                    FilingUnitTargetId = .FilingUnitTargetId
                    Me.LawyerId = .LawyerId
                    If Me._juridicalC.Lawyer IsNot Nothing Then
                        Me.INDSleLawyer.Properties.NullText = String.Format("{0} - {1}", Me._juridicalC.Lawyer.Code, Me._juridicalC.Lawyer.Name)
                        If Me._juridicalC.Lawyer.ThirdParty IsNot Nothing Then
                            Me.INDSleLawyer.Properties.NullText = String.Format("{0} - {1} - {2}", Me.INDSleLawyer.Properties.NullText, Me._juridicalC.Lawyer.ThirdParty.Nit, Me._juridicalC.Lawyer.ThirdParty.Name)
                        End If
                    End If

                    Me.DemandStatusId = .DemandStatusId
                    If Me._juridicalC.DemandStatus IsNot Nothing Then
                        Me.INDSleDemandStatus.Properties.NullText = String.Format("{0} - {1}", Me._juridicalC.DemandStatus.Code, Me._juridicalC.DemandStatus.Description)
                    End If

                    banFilingUnit = True
                    Me.INDCommentMem.Text = .Comment
                    Me._detailJuridical = Await Model.ListDetailJuridicalById(.Id)
                    Me.SelectedInvoices = Me._detailJuridical
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._juridicalC.JuridicalTransferConsecutive)
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.AsyncLoader(False)
                    If .State.Equals("1") Then
                        Me.ActionsOnControls = ActionOnControlsTypeTransferJuridical.UnconfirmedJuridical
                        Me.BarraBotones.StatusRecord = ActionOnControlsTypeTransferJuridical.UnconfirmedJuridical
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    ElseIf .State.Equals("2") Then
                        Me.ActionsOnControls = ActionOnControlsTypeTransferJuridical.ConfirmedJuridical
                        Me.BarraBotones.StatusRecord = ActionOnControlsTypeTransferJuridical.ConfirmedJuridical
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                        Me.BarraBotones.DisableBarDocument()
                        Me.INDRepositoryBtnDelete.Buttons(0).Enabled = False
                        Me.Actions.OptionsColumn.AllowEdit = False
                        If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Actualizar) Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                            INDConsecutiveBte.Enabled = False
                        End If
                        If Not Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.EditDemandStatus) Then
                            INDSleDemandStatus.Enabled = False
                        End If
                        If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Reversar) Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Desconfirmar) = False
                            Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Desconfirmar, "Reversar")
                        End If
                    ElseIf .State.Equals("3") Then
                        Me.ActionsOnControls = ActionOnControlsTypeTransferJuridical.InvalidateJuridical
                        Me.BarraBotones.StatusRecord = ActionOnControlsTypeTransferJuridical.InvalidateJuridical
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                        Me.BarraBotones.DisableBarDocument()
                        Me.INDRepositoryBtnDelete.Buttons(0).Enabled = False
                        Me.Actions.OptionsColumn.AllowEdit = False
                    Else
                        Me.ActionsOnControls = ActionOnControlsTypeTransferJuridical.ConfirmedJuridical
                        Me.BarraBotones.StatusRecord = ActionOnControlsTypeTransferJuridical.ConfirmedJuridical
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                        Me.BarraBotones.DisableBarDocument()
                        Me.INDRepositoryBtnDelete.Buttons(0).Enabled = False
                        Me.Actions.OptionsColumn.AllowEdit = False
                        If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Actualizar) Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                            INDConsecutiveBte.Enabled = False
                        End If
                        If Not Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.EditDemandStatus) Then
                            INDSleDemandStatus.Enabled = False
                        End If
                    End If
                End With
                If result.Id = 0 Then
                    If Me.BarraBotones.StatusRecord <> ActionOnControlsTypeTransferJuridical.ConfirmedJuridical AndAlso Me.BarraBotones.StatusRecord <> ActionOnControlsTypeTransferJuridical.InvalidateJuridical Then
                        Me.BarraBotones.SetDocuments(Me._juridicalC.Id)
                    End If
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me._juridicalC.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If

                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

                Me.BarraBotones.PrintReport(PrintReportAction.None, _juridicalC.Id, 0, _juridicalC.Id)

            Else
                Me.AsyncLoader(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.NoExisteTrasladoCobroJuridico, Eform.TrasladoCobroJurídico)
                Me.INDConsecutiveBte.Focus()
                Me.INDConsecutiveBte.SelectAll()
            End If
        End Using
        INDlycRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        Dim detailString = " [ "
        For Each item In Me._detailJuridical
            If detailString.Length = 3 Then
                detailString = detailString & String.Format(obtenerRecurso(Eresources.FrmJuridicalDebtMetaDataDetail, Eform.InfoMetaData), item.InvoiceNumber)
            Else
                detailString = " - " & detailString & String.Format(obtenerRecurso(Eresources.FrmJuridicalDebtMetaDataDetail, Eform.InfoMetaData), item.InvoiceNumber)
            End If
        Next
        detailString = detailString & " ]"
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmJuridicalDebtMetaData, Eform.InfoMetaData), Me._juridicalC.Customer.Nit, Me._juridicalC.Customer.Name.Trim.ToLower, Me._juridicalC.DocumentNumber, detailString), .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me._juridicalC.JuridicalTransferConsecutive & "#$", .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(FrmJuridicalDebtMetaDataTitle, InfoMetaData), Me._juridicalC.JuridicalTransferConsecutive), .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmJuridicalDebtMetaData, Eform.InfoMetaData), Me._juridicalC.Customer.Nit, Me._juridicalC.Customer.Name.Trim.ToLower, Me._juridicalC.DocumentNumber, detailString)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmJuridicalDebtMetaDataTitle, Eform.InfoMetaData), Me._juridicalC.JuridicalTransferConsecutive)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Valida que la unidad de radicacion destino no sea igual a la de origen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateFilingUnitTarget()
        If FilingUnitTargetId IsNot Nothing AndAlso FilingUnitSourceId IsNot Nothing Then
            If FilingUnitTargetId = FilingUnitSourceId Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontSelectEqualsFilingUnit", NAME_MODULE)
                FilingUnitTargetId = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        If Me.INDbteNit.EditValue Is Nothing Then
			Me.INDbteNit.Focus()
			Mensaje(EeventViewerImages.Advertencia) = "El Nit no puede estar vacío"
			Return False
        End If
        If Me.INDDocumentNumberTxt.Text.Trim().Equals(String.Empty) Then
			Me.INDDocumentNumberTxt.Focus()
			Mensaje(EeventViewerImages.Advertencia) = "El Número de oficio no puede estar vacío"
			Return False
        End If
        If Me.INDDocumentDateDte.Text.Trim().Equals(String.Empty) Then
			Me.INDDocumentDateDte.Focus()
			Mensaje(EeventViewerImages.Advertencia) = "La fecha de oficio no puede estar vacío"
			Return False
        End If
        If Me.INDsleFilingUnitSource.Text.Trim().Equals(String.Empty) Then
			Me.INDsleFilingUnitSource.Focus()
			Mensaje(EeventViewerImages.Advertencia) = "La unidad de radicación de origen no puede estar vacío"
			Return False
        End If
        If Me.INDsleFilingUnitTarget.Text.Trim().Equals(String.Empty) Then
			Me.INDsleFilingUnitTarget.Focus()
			Mensaje(EeventViewerImages.Advertencia) = "La unidad de radicación de destino no puede estar vacío"
			Return False
        End If
        If Me.LawyerId Is Nothing OrElse Me.LawyerId.GetValueOrDefault = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_SelectLawyer", NAME_MODULE)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Asigna los valores de los controles al objeto Devoluciones
    ''' </summary>
    Private Sub AssignValues()
        'Me._juridicalC.OperatingUnitId = Me._idOperativeUnit
        Me._juridicalC.CustomerId = Me.INDbteNit.EditValue
        If Me._juridicalC.DocumentNumber Is Nothing OrElse Not Me._juridicalC.DocumentNumber.Trim().Equals(Me.INDDocumentNumberTxt.Text.Trim()) Then
            Me._juridicalC.DocumentNumber = Me.INDDocumentNumberTxt.Text.Trim()
        End If
        Me._juridicalC.JuridicalTransferDate = Date.Parse(Me.INDRadicateDateLbl.Text)
        If Object.Equals(Me._juridicalC.DocumentDate, Nothing) OrElse Not Me._juridicalC.DocumentDate.Equals(Date.Parse(Me.INDDocumentDateDte.EditValue)) Then
            Me._juridicalC.DocumentDate = Date.Parse(Me.INDDocumentDateDte.EditValue)
        End If
        If Me._juridicalC.Comment Is Nothing OrElse Not Me._juridicalC.Comment.Trim().Equals(Me.INDCommentMem.Text.Trim()) Then
            Me._juridicalC.Comment = Me.INDCommentMem.Text.Trim()
        End If
        Me._juridicalC.FilingUnitSourceId = FilingUnitSourceId
        Me._juridicalC.FilingUnitTargetId = FilingUnitTargetId
        Me._juridicalC.LawyerId = Me.LawyerId
        Me._juridicalC.DemandStatusId = Me.DemandStatusId
    End Sub

    ''' <summary>
    ''' Elimina una factura seleccionado
    ''' </summary>
    Private Sub DeleteInvoice()
        If Me.INDInvoicesDetailGv.SelectedRowsCount > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesEliminarRegistro, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim aux As Domain.Entities.TransferJuridicalDebtCollectionD = CType(Me.INDInvoicesDetailGv.GetRow(Me.INDInvoicesDetailGv.FocusedRowHandle), Domain.Entities.TransferJuridicalDebtCollectionD)
                If aux.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Me._detailJuridical.Remove(aux)
                    Me.SelectedInvoices = Me._detailJuridical.Where(Function(d) d.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList
                Else 'Eliminado
                    Me._detailJuridical.Remove(aux)
                    aux.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                    Me._detailJuridical.Add(aux)
                    Me.SelectedInvoices = Me._detailJuridical.Where(Function(d) d.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList
                    Me.INDInvoicesDetailGc.RefreshDataSource()
                End If
            End If
        End If
    End Sub

#Region "SaveProvisionAndDeterioration For Batches"

    ''' <summary>
    ''' control donde se muestra el progreso de la operacion de importar archivos
    ''' </summary>
    ''' <remarks></remarks>
    Dim progress As CtrProgress

    ''' <summary>
    ''' total de los items a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalItems As Integer

    ''' <summary>
    ''' posicion del item enviado
    ''' </summary>
    Dim indexSend As Integer = 0

    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer = 0

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Dim itemsSend As Integer = 100

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

    Private Async Function SaveTransferJuridicalDebtDetails() As Task
        Try
            AsyncLoader(True)

            progress = New CtrProgress
            progress.SetInfoFunction(AddressOf getInfo)
            progress.PrintInfo()
            progress.Dock = DockStyle.Fill
            AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
            AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))

            Dim errors As New StringBuilder()
            Dim State = Me._juridicalC.State
            Dim ListTransferJuridicalDebtCollectionDetailToSend = Me._detailJuridical.Select(Function(d) d.CloneEntity()).ToList()
            If ListTransferJuridicalDebtCollectionDetailToSend IsNot Nothing AndAlso ListTransferJuridicalDebtCollectionDetailToSend.Count > 0 Then
                indexSend = 0
                totalProcessedItems = 0
                totalItems = Me._detailJuridical.Count
                progress.SafeInvoke(Sub(x)
                                        x.SetTitle = "Registros Guardados"
                                        x.PrintInfo()
                                    End Sub)

                While ListTransferJuridicalDebtCollectionDetailToSend.Count > 0
                    Me._juridicalC.TransferJuridicalDebtCollectionD.Clear()
                    Dim listToSend = ListTransferJuridicalDebtCollectionDetailToSend.Take(itemsSend).ToList()
                    'agregamos los items a la cabecera
                    Dim objLock As New Object()
                    Parallel.ForEach(listToSend, Sub(x)
                                                     SyncLock objLock
                                                         Me._juridicalC.TransferJuridicalDebtCollectionD.Add(x)
                                                     End SyncLock
                                                 End Sub)

                    Using model As New MTransferJuridicalDebt(Me.Tag.ToString())
                        Dim Result = Await model.SaveJuridicalC(Me._juridicalC)
                        If Result.StateResult Then
                            Me.INDConsecutiveBte.Text = Result.ObjectEmbbeded.JuridicalTransferConsecutive
                            Me._juridicalC = Result.ObjectEmbbeded
                        Else
                            If ListTransferJuridicalDebtCollectionDetailToSend.Count < itemsSend Then
                                errors.AppendLine("Los items del " + (indexSend + 1).ToString() + " hasta " + (totalItems).ToString() + " no se pudieron guardar porque: " + Result.Message)
                            Else
                                errors.AppendLine("Los items del " + (indexSend + 1).ToString() + " hasta " + (indexSend + itemsSend).ToString() + " no se pudieron guardar porque: " + Result.Message)
                            End If
                        End If
                    End Using

                    If ListTransferJuridicalDebtCollectionDetailToSend.Count < itemsSend Then
                        indexSend += ListTransferJuridicalDebtCollectionDetailToSend.Count - 1
                        totalProcessedItems = totalItems
                        progress.PrintInfo()
                        ListTransferJuridicalDebtCollectionDetailToSend.RemoveRange(0, ListTransferJuridicalDebtCollectionDetailToSend.Count)
                    Else
                        indexSend += itemsSend
                        totalProcessedItems = indexSend
                        progress.PrintInfo()
                        ListTransferJuridicalDebtCollectionDetailToSend.RemoveRange(0, itemsSend)
                    End If
                End While
            End If

            If errors.Length = 0 Then
                If State = "2" AndAlso indigo.IndigoCompanyType = 1 Then

                    Using model As New MSettingPortfolio(Tag)
                        Dim Reclassified = Await model.GetSettingPortfolioByIdOperatingUnitAsync(_idOperativeUnit)

                        'si no realiza reclasificacion, preguntamos si desea confirmar
                        If Not Reclassified.LegalCollection AndAlso MessageIndigo.Show(ResourceManager.GetString("ReclassifyPortfolioWhenConfirmed", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                            AsyncLoader(False)
                            Exit Function
                        End If

                    End Using

                    Me._juridicalC.State = 2
                    Using model As New MTransferJuridicalDebt(Me.Tag.ToString())
                        Dim Result = Await model.ConfirmJuridical(Me._juridicalC)
                        If Result.StateResult = True Then
                            If Not String.IsNullOrEmpty(Result.Message) Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message.ToString
                            End If
                            Mensaje(EeventViewerImages.Informacion) = String.Format("Se confirmo correctamente el traslado a cobro juridico {0}", Me._juridicalC.JuridicalTransferConsecutive)
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _juridicalC.Id, 0, _juridicalC.Id)
                            SearchMode = False
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDConsecutiveBte.Enabled = False
                            If Result.Message IsNot Nothing Then
                                If Result.Message = "-999" Then
                                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                                Else
                                    Mensaje(EeventViewerImages.Advertencia) = Result.Message
                                End If
                            End If
                        End If
                    End Using
                ElseIf State = "1" Then
                    Using model As New MTransferJuridicalDebt(Me.Tag.ToString())
                        Me._juridicalC = Await model.GetJuridicalByConsecutive(Me._juridicalC.JuridicalTransferConsecutive) 'actualizamos objeto para cargar agregados customer
                        Me._detailJuridical = Await model.ListDetailJuridicalById(Me._juridicalC.Id)
                        Me.SelectedInvoices = Me._detailJuridical
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                        Me.BarraBotones.SetDocuments(Me._juridicalC.Id)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.ActionsOnControls = ActionOnControlsTypeTransferJuridical.UnconfirmedJuridical
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado)
                        If varImp = 1 Then
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _juridicalC.Id, 0, _juridicalC.Id)
                        Else
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _juridicalC.Id, 0, _juridicalC.Id)
                        End If
                    End Using
                    AsyncLoader(False)
                Else
                    Mensaje(EeventViewerImages.Informacion) = String.Format("Se confirmo correctamente el traslado a cobro juridico {0}", Me._juridicalC.JuridicalTransferConsecutive)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _juridicalC.Id, 0, _juridicalC.Id)
                    SearchMode = False
                    AsyncLoader(False)
                    Me.Deshacer()
                End If
            Else
                AsyncLoader(False)
                INDConsecutiveBte.Enabled = False
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If

            totalItems = 0
            totalProcessedItems = 0
            progress.PrintInfo()
            AdditionalControlPanel.Controls.Clear()
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
            AsyncLoader(False)
        End Try
    End Function

#End Region

    ''' <summary>
    ''' Anular el traslado cobro jurídico
    ''' </summary>
    Public Async Sub InvalidateTransferJuridical()
        Try
            If Me._detailJuridical.Count = 0 OrElse Me._detailJuridical.TrueForAll(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted) Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
                Exit Sub
            End If
            If Me._juridicalC.Id > 0 Then
                'If Me._juridicalC.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged Then
                Using model As New MTransferJuridicalDebt(Me.Tag)
                    Me.AsyncLoader(True)
                    Me._juridicalC.State = "3"
                    Dim result = Await model.SaveJuridicalC(Me._juridicalC)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesAnuladoCorrectamente)
                        Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _juridicalC.Id, 0, _juridicalC.Id)
                        SearchMode = False
                        Me.AsyncLoader(False)
                        Deshacer()
                    Else
                        Me.AsyncLoader(False)
                        INDConsecutiveBte.Enabled = False
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    End If
                End Using
                'End If
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            INDConsecutiveBte.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Confirmar el traslado cobro jurídico
    ''' </summary>
    Public Async Sub ConfirmTransferJuridical()
        Try
            If Me._detailJuridical.Count = 0 OrElse Me._detailJuridical.TrueForAll(Function(x) x.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted) Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
                Exit Sub
            End If
            Me._juridicalC.State = "2"
            'Me._juridicalC.OperatingUnitId = _idOperativeUnit
            Await Me.SaveTransferJuridicalDebtDetails()
        Catch ex As Exception
            Me.AsyncLoader(False)
            INDConsecutiveBte.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Reversa el traslado cobro jurídico
    ''' </summary>
    Public Async Sub ReverseTransferJuridical()
        Try
            If Me._juridicalC.Id > 0 Then
                With Me._juridicalC
                    .State = "3"
                End With
                If Me._juridicalC.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged Then
                    Using model As New MTransferJuridicalDebt(Me.Tag)
                        Me.AsyncLoader(True)
                        Dim result = Await model.ReverseJuridical(Me._juridicalC.Id, _idOperativeUnit)
                        If result IsNot Nothing Then
                            If result.StateResult = True Then
                                If result.Message IsNot Nothing Then
                                    Mensaje(EeventViewerImages.Informacion) = result.Message.ToString
                                End If
                                SearchMode = False
                                Me.AsyncLoader(False)
                                Me.Deshacer()
                            Else
                                Me.AsyncLoader(False)
                                INDConsecutiveBte.Enabled = False
                                If result.Message IsNot Nothing Then
                                    If result.Message = "-999" Then
                                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                                    Else
                                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                                    End If
                                End If
                            End If
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            INDConsecutiveBte.Enabled = False
            Throw ex
        End Try
    End Sub



#End Region

End Class