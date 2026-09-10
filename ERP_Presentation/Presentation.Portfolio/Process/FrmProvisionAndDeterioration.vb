'***********************************************************************
' Assembly         : Presentacion.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/10/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Portfolio.Model
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Portfolio.MVP

#End Region

Public Class FrmProvisionAndDeterioration
    Implements IProvisionAndDeterioration, ICustomizableForm

#Region "Builder"

    Private ctrDeterioro As CtrDeterioro

    Public Sub New()
        InitializeComponent()
        ctrDeterioro = New CtrDeterioro()
        ctrDeterioro.Dock = System.Windows.Forms.DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrDeterioro)
        IndigoGridControl1.SetHideNoRecords(INDgcBill, True)

        AddHandler bwLoadBills.DoWork, AddressOf bwLoadBills_DoWork
        AddHandler bwLoadBills.RunWorkerCompleted, AddressOf bwLoadBills_RunWorkerCompleted

        'Inicializar el diccionario de libros contables
        _dictionaryLegalBook = New Dictionary(Of Integer, BookXpo)
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Private _presenter As PPortfolioProvision

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _currentSequenceId As Int64

    ''' <summary>
    ''' Secuencia numerica del fomulario
    ''' </summary>
    Private _sequence As PortfolioSequence

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordPortfolio

    ''' <summary>
    ''' Registro actual
    ''' </summary>
    Private _portfolioProvision As PortfolioProvision

    ''' <summary>
    ''' Listado de provisión y deterioro
    ''' </summary>
    ''' <remarks></remarks>
    Private ListPortfolioProvisionDetail As List(Of PortfolioProvisionDetail)

    ''' <summary>
    ''' Listado temporal de provisión y deterioro a borrar
    ''' </summary>
    ''' <remarks></remarks>
    Private ListTmpPortfolioProvisionDetailDelete As List(Of PortfolioProvisionDetail)

    ''' <summary>
    ''' Listado de provisión y deterioro a borrar
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeletePortfolioProvisionDetail As List(Of Integer)

    ''' <summary>
    ''' Lista ligera para deterioro por clasificación (evita cálculos pesados)
    ''' </summary>
    Private _lightweightDetailsList As List(Of PortfolioProvisionDetail)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _varImp As Integer

    ''' <summary>
    ''' Variable que almacena los parámetros de CxC
    ''' </summary>
    Private _settingPortfolio As SettingPortfolio

    ''' <summary>
    ''' Diccionario para libros contables
    ''' </summary>
    ''' <remarks></remarks>
    Private _dictionaryLegalBook As Dictionary(Of Integer, BookXpo)

    ''' <summary>
    ''' Almacena el padre original de la rejilla modificada por el tab
    ''' </summary>
    Private _originalParent As DevExpress.XtraLayout.LayoutControlGroup = Nothing

    ''' <summary>
    ''' Guarda la posición original de la rejilla antes de modificarla
    ''' </summary>
    Private _originalLocation As System.Drawing.Point

    ''' <summary>
    ''' Guarda el tamaño original de la rejilla para poder restaurarla
    ''' </summary>
    Private _originalSize As System.Drawing.Size

    ''' <summary>
    ''' Almacena los items dentro del layout para mantener su orden
    ''' </summary>
    Private _originalNextItem As DevExpress.XtraLayout.BaseLayoutItem = Nothing

    ''' <summary>
    ''' Lista para los datos del deterioro de cartera perteneciente al libro Tipo 1: Colgap/Fiscal
    ''' </summary>
    Private _fiscalData As List(Of PortfolioDeteriorationByClassificationDTO)

    ''' <summary>
    ''' Lista para los datos del deterioro de cartera perteneciente al libro Tipo 2: Niif
    ''' </summary>
    Private _niffData As List(Of PortfolioDeteriorationByClassificationDTO)

    ''' <summary>
    ''' Referencia al GridControl del tab Fiscal creado dinámicamente
    ''' </summary>
    Private _fiscalGridControl As DevExpress.XtraGrid.GridControl = Nothing

    ''' <summary>
    ''' Referencia al GridControl del tab NIIF creado dinámicamente
    ''' </summary>
    Private _niffGridControl As DevExpress.XtraGrid.GridControl = Nothing

    ''' <summary>
    ''' Botón exportar para el GridControl Fiscal
    ''' </summary>
    Private _fiscalExportButton As Presentation.Controls.ExportDataButton = Nothing

    ''' <summary>
    ''' Botón exportar para el GridControl NIIF
    ''' </summary>
    Private _niffExportButton As Presentation.Controls.ExportDataButton = Nothing

#End Region

#Region "Tuples"

    ''' <summary>
    ''' Listado para mostrar en el form de errores
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListMessage As List(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim ListProcess As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de opciones para el deterioro
    ''' </summary>
    Dim ListApplyDeterioration As New List(Of Tuple(Of Integer, String))

#End Region

#Region "Properties"

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IProvisionAndDeterioration.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IProvisionAndDeterioration.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyProvisionAndDeterioration.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDdteDocuementDate.Enabled = value
            INDdteCourtDate.Enabled = value
            INDsleProcess.Enabled = value
            INDmemoDescription.Enabled = value
            INDsleApplyDeterioration.Enabled = value
            INDlyProvisionAndDeterioration.EndUpdate()
            If value Then
                INDdteDocuementDate.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As PortfolioSequence Implements IProvisionAndDeterioration.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IProvisionAndDeterioration.Code
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
    ''' Fecha de documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IProvisionAndDeterioration.DocumentDate
        Get
            Return INDdteDocuementDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocuementDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha de corte
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CourtDate As Date? Implements IProvisionAndDeterioration.CourtDate
        Get
            Return INDdteCourtDate.EditValue
        End Get
        Set(value As Date?)
            INDdteCourtDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Proceso (DocumentType)
    '''     1 - Provisión
    '''     2 - Deterioro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Process As Integer? Implements IProvisionAndDeterioration.Process
        Get
            Return INDsleProcess.EditValue
        End Get
        Set(value As Integer?)
            INDsleProcess.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Especifica si se aplica el deterioro (Cuando DocumentType = 2) 
    '''     1 - Facturacion en Cartera
    '''     2 - Facturación en Glosa
    '''     3 - Deterioro por Clasificación
    ''' </summary>
    ''' <returns></returns>
    Public Property ApplyDeterioration As Byte? Implements IProvisionAndDeterioration.ApplyDeterioration
        Get
            Return IIf(INDsleApplyDeterioration.EditValue Is Nothing, Nothing, CByte(INDsleApplyDeterioration.EditValue))
        End Get
        Set(value As Byte?)
            INDsleApplyDeterioration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Descripción
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements IProvisionAndDeterioration.Description
        Get
            Return INDmemoDescription.EditValue
        End Get
        Set(value As String)
            INDmemoDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la edad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AgesId As Integer? Implements IProvisionAndDeterioration.AgesId
        Get
            Return INDsleAges.EditValue
        End Get
        Set(value As Integer?)
            INDsleAges.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' No. de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceCode As String Implements IProvisionAndDeterioration.InvoiceCode
        Get
            Return INDSleInvoiceNumber.EditValue
        End Get
        Set(value As String)
            INDSleInvoiceNumber.EditValue = value
        End Set
    End Property

#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource edad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListAgesPorfolio As List(Of Domain.Entities.AgesPortfolio) Implements IProvisionAndDeterioration.ListAgesPorfolio
        Get
            Return INDsleAges.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.AgesPortfolio))
            INDsleAges.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceNumberXpo As XPInstantFeedbackSource Implements IProvisionAndDeterioration.InvoiceNumberXpo
        Get
            Return INDSleInvoiceNumber.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleInvoiceNumber.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Entidad xpo para las edades
    ''' </summary>
    ''' <remarks></remarks>
    Dim PortfolioAgesPortfolio As Domain.Entities.AgesPortfolio

    ''' <summary>
    ''' Listado de facturas que se consulta por el rango seleccionado por el usuario
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListViewAccountReceivableByPortfolioProvisionXpo As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha Corte", .FieldName = "CourtDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Proceso", .FieldName = "DocumentTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioProvision
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewPortfolioProvision()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If _portfolioProvision.Status <> 3 Then
            If ValidateControls() = False Then
                Exit Sub
            Else
                If bwLoadBills.IsBusy = True Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede guardar porque se están cargando las facturas. Por favor espere."
                    Exit Sub
                End If
                If ListPortfolioProvisionDetail Is Nothing OrElse Not ListPortfolioProvisionDetail.Any(Function(d) d.SelectOption) Then
                    Mensaje(EeventViewerImages.Advertencia) = "No hay registros seleccionados para guardar"
                    Exit Sub
                End If
            End If
        End If
        Try
            AssigningValues()
            Await SavePortfolioProvisionDetail()
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Metodo que confirma la provision y deterioro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function Confirm() As Task
        If bwLoadBills.IsBusy = True Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede confirmar porque se están cargando las facturas. Por favor espere."
            Exit Function
        End If
        If ValidateControls() = False Then
            Exit Function
        End If
        If ListPortfolioProvisionDetail Is Nothing OrElse Not ListPortfolioProvisionDetail.Any(Function(d) d.SelectOption) Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay registros seleccionados para poder confirmar"
            Exit Function
        End If
        If (From l In _portfolioProvision.PortfolioProvisionDetail Where l.Percentage = 0 Select l).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Hay detalles en la rejilla que tienen porcentaje en cero"
            Exit Function
        End If
        AssigningValues()

        Try
            Await SavePortfolioProvisionDetail()
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Function

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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

#Region "Bar Button Events"

    ''' <summary>
    ''' LOAD: Carga los permisos de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' CLICK_GUARDAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _varImp = 1
        _portfolioProvision.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ACTUALIZAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _varImp = 2
        _portfolioProvision.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' CLICK_ANULAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _varImp = 3
            _portfolioProvision.Status = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' CLICK_GUARDAR_CONFIRMAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _varImp = 4
            _portfolioProvision.Status = 2
            Await Confirm()
        End If
    End Sub

    ''' <summary>
    ''' CLICK_ACTUALIZAR_COFIRMAR
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _varImp = 4
            _portfolioProvision.Status = 2
            Await Confirm()
        End If
    End Sub

    ''' <summary>
    ''' Barra botones: Confirma
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _varImp = 4
            _portfolioProvision.Status = 2
            Await Confirm()
        End If
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
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _portfolioProvision.Id, 0, _portfolioProvision.Code)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._operativeUnitId = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmProvisionAndDeterioration_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyProvisionAndDeterioration, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PPortfolioProvision(Me)
        _presenter.LoadDefinitionLayout()
        _presenter.GetSequense()
        Deshacer()
        InitializeTuples()
        _settingPortfolio = Await GetSettingPortfolio()

        TabbedControlGroup1.SelectedTabPageIndex = 0
        INDpceBill.Enabled = False
        INDgcBill.Enabled = False
        INDEsbBills.Enabled = False

        'Se agrega el summaryGroup por código porque no se encontro por diseño
        INDviewBill.OptionsView.GroupFooterShowMode = GroupFooterShowMode.VisibleIfExpanded
        Dim item1 As GridGroupSummaryItem = New GridGroupSummaryItem()
        item1.FieldName = "BalanceAccountReceivable"
        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        item1.DisplayFormat = Convert.ToChar(Keys.Tab) + "Total: {0:c2}"
        item1.ShowInGroupColumnFooter = INDviewBill.Columns("BalanceAccountReceivable")
        INDviewBill.GroupSummary.Add(item1)

        item1 = New GridGroupSummaryItem()
        item1.FieldName = "BalanceWithGlosa"
        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        item1.DisplayFormat = Convert.ToChar(Keys.Tab) + "Total: {0:c2}"
        item1.ShowInGroupColumnFooter = INDviewBill.Columns("BalanceWithGlosa")
        INDviewBill.GroupSummary.Add(item1)

        item1 = New GridGroupSummaryItem()
        item1.FieldName = "NetPresentValue"
        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        item1.DisplayFormat = Convert.ToChar(Keys.Tab) + "Total: {0:c2}"
        item1.ShowInGroupColumnFooter = INDviewBill.Columns("NetPresentValue")
        INDviewBill.GroupSummary.Add(item1)

        item1 = New GridGroupSummaryItem()
        item1.FieldName = "Value"
        item1.SummaryType = DevExpress.Data.SummaryItemType.Sum
        item1.DisplayFormat = Convert.ToChar(Keys.Tab) + "Total: {0:c2}"
        item1.ShowInGroupColumnFooter = INDviewBill.Columns("Value")
        INDviewBill.GroupSummary.Add(item1)

        'IndigoGridControl1.RefreshGrid(INDgcBill)
        INDEsbBills.AddRangeColumns("
            No. Factura",
            "Expectativa",
            "Porcentaje"
        )

        LoadStatus()
    End Sub

    ''' <summary>
    ''' Función que obtiene los parámetros de CxC
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetSettingPortfolio() As Task(Of SettingPortfolio)
        If _operativeUnitId = 0 Then
            _operativeUnitId = BarraBotones.OperatingUnitValue
        End If
        Using model As New MSettingPortfolio(Me.Tag)
            Return Await model.GetSettingPortfolioByIdOperatingUnitAsync(_operativeUnitId)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los libros contables activos asociados al VieBot de manera optimizada.
    ''' </summary>
    Private Sub GetLegalBooks()
        Using model As New MPortfolioProvision(CStr(MyTag))
            Dim listVieBot As List(Of VieBotXpo) = model.GetActivesLegalBooks()
            If listVieBot IsNot Nothing Then
                ' Agrupar por LegalBookId.Id y tomar el primero de cada grupo
                Dim uniqueLegalBooks = listVieBot.
                GroupBy(Function(x) x.LegalBookId.Id).
                Select(Function(g) g.First().LegalBookId)

                For Each legalBook In uniqueLegalBooks
                    If Not _dictionaryLegalBook.ContainsKey(legalBook.Id) Then
                        _dictionaryLegalBook.Add(legalBook.Id, legalBook)
                    End If
                Next
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Muestra u oculta el loading panel en todas las vistas disponibles (tabs)
    ''' </summary>
    ''' <param name="show">True para mostrar, False para ocultar</param>
    Private Sub ShowHideLoadingPanelsForAllTabs(show As Boolean)
        ' Mostrar/ocultar en la vista principal si está activa
        If INDgcBill.MainView Is INDviewDeteriorationByClassification Then
            If show Then
                INDviewDeteriorationByClassification.ShowLoadingPanel()
            Else
                INDviewDeteriorationByClassification.HideLoadingPanel()
            End If
        End If

        ' Mostrar/ocultar en el grid dinámico Fiscal si existe
        If _fiscalGridControl IsNot Nothing AndAlso _fiscalGridControl.MainView IsNot Nothing Then
            Dim fiscalView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(_fiscalGridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            If fiscalView IsNot Nothing Then
                If show Then
                    fiscalView.ShowLoadingPanel()
                Else
                    fiscalView.HideLoadingPanel()
                End If
            End If
        End If

        ' Mostrar/ocultar en el grid dinámico NIIF si existe
        If _niffGridControl IsNot Nothing AndAlso _niffGridControl.MainView IsNot Nothing Then
            Dim niffView As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(_niffGridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
            If niffView IsNot Nothing Then
                If show Then
                    niffView.ShowLoadingPanel()
                Else
                    niffView.HideLoadingPanel()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la información para el deterioro de cartera por Clasificación
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetPortfolioDeterioratioByClassification() As Task
        If CourtDate Is Nothing OrElse _operativeUnitId <= 0 Then Return

        ' Si es un registro guardado, transformar los detalles existentes
        If _portfolioProvision IsNot Nothing AndAlso _portfolioProvision.Id > 0 Then
            ShowHideLoadingPanelsForAllTabs(True)
            Try
                Dim dataFromDetails = TransformDetailsToClassificationDTO()

                If dataFromDetails IsNot Nothing AndAlso dataFromDetails.Count > 0 Then
                    _fiscalData = dataFromDetails.Where(Function(x) x.TypeBook = 1).ToList()
                    _niffData = dataFromDetails.Where(Function(x) x.TypeBook = 2).ToList()
                Else
                    ' Inicializar listas vacías para crear tabs sin datos
                    _fiscalData = New List(Of PortfolioDeteriorationByClassificationDTO)
                    _niffData = New List(Of PortfolioDeteriorationByClassificationDTO)
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoDataPortfolioDeteriorationByClassification", NAME_MODULE)
                End If
                ' Actualizar tabs con los datos (ya deberían estar creados)
                CreateTabsForByClassification()
            Catch ex As Exception
                Throw
            Finally
                ShowHideLoadingPanelsForAllTabs(False)
            End Try
            Return
        End If

        ' Si es un registro nuevo, consultar la base de datos
        ShowHideLoadingPanelsForAllTabs(True)
        Try
            Using model As New MPortfolioProvision(CStr(MyTag))
                Dim res = Await model.GetPortfolioDeteriorationByClassification(CourtDate, _operativeUnitId)

                If Not res.StateResult Then
                    Mensaje(EeventViewerImages.MensajeError) = res.Message
                    Return
                End If

                Dim embeddedData = res.ObjectEmbbeded.ToList()
                Dim hasErrors = embeddedData.Any(Function(x) x.Code <> 0)
                Dim hasValidData = embeddedData.Any(Function(x) x.Code = 0)

                If hasErrors Then
                    ShowErrors(embeddedData.Where(Function(x) x.Code <> 0).Select(Function(x) x.Message).ToList())
                End If

                If hasValidData Then
                    Dim dataWithoutErrors As List(Of PortfolioDeteriorationByClassificationDTO) = embeddedData.Where(Function(x) x.Code = 0).ToList()
                    _fiscalData = dataWithoutErrors.Where(Function(x) x.TypeBook = 1).ToList()
                    _niffData = dataWithoutErrors.Where(Function(x) x.TypeBook = 2).ToList()
                    CreateTabsForByClassification()
                    CreateDetailWithPortfolioDeteriorationByClassificationData(dataWithoutErrors)
                Else
                    ' Inicializar listas vacías para crear tabs sin datos
                    _fiscalData = New List(Of PortfolioDeteriorationByClassificationDTO)
                    _niffData = New List(Of PortfolioDeteriorationByClassificationDTO)
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoDataPortfolioDeteriorationByClassification", NAME_MODULE)
                    ' Crear tabs siempre, incluso sin datos
                    CreateTabsForByClassification()
                End If
            End Using
        Catch ex As Exception
            Throw
        Finally
            ShowHideLoadingPanelsForAllTabs(False)
        End Try
    End Function

    ''' <summary>
    ''' Función para mostrar los errores de los datos obtenidos
    ''' </summary>
    ''' <param name="errors"></param>
    Private Sub ShowErrors(errors As List(Of String))
        Using formulario As New FrmListErrors(errors)
            formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(formulario, False)
            Me.Cursor = Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub



    ''' <summary>
    ''' Limpia los tabs de clasificación si existen
    ''' </summary>
    Private Sub ClearTabsForByClassification()
        Try
            ' Obtener el LayoutControl desde INDlygCxC
            Dim layoutControl As DevExpress.XtraLayout.LayoutControl = TryCast(INDlygCxC.Owner, DevExpress.XtraLayout.LayoutControl)

            If layoutControl Is Nothing OrElse _originalParent Is Nothing Then
                Exit Sub
            End If

            ' Buscar el TabbedControlGroup en el grupo padre original
            Dim existingTabGroup As DevExpress.XtraLayout.TabbedControlGroup = Nothing
            existingTabGroup = _originalParent.Items.OfType(Of DevExpress.XtraLayout.TabbedControlGroup)() _
                          .FirstOrDefault(Function(item) item.Name = "TabbedControlGroupBooks")

            If existingTabGroup IsNot Nothing Then
                layoutControl.BeginUpdate()

                Try
                    ' **PASO 1: Mover INDlyItemBill de vuelta al padre original**
                    If _originalNextItem IsNot Nothing AndAlso _originalNextItem.Parent Is _originalParent Then
                        ' Mover antes del item siguiente usando InsertType.Left
                        INDlyItemBill.Move(_originalNextItem, DevExpress.XtraLayout.Utils.InsertType.Left)
                    Else
                        ' Si no hay item siguiente válido, mover al final del grupo usando AddItem
                        Dim currentParent = TryCast(INDlyItemBill.Parent, DevExpress.XtraLayout.LayoutControlGroup)
                        If currentParent IsNot Nothing AndAlso currentParent IsNot _originalParent Then
                            currentParent.Remove(INDlyItemBill)
                        End If
                        _originalParent.AddItem(INDlyItemBill)
                    End If

                    ' **PASO 2: Restaurar propiedades originales**
                    INDlyItemBill.Location = _originalLocation
                    INDlyItemBill.Size = _originalSize
                    INDlyItemBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    ' **PASO 3: Remover el TabbedControlGroup**
                    _originalParent.Remove(existingTabGroup)

                    ' **PASO 4: Dispose del TabbedControlGroup**
                    existingTabGroup.Dispose()

                    ' Mostrar los controles que se habían ocultado
                    INDlyItemPopupBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Catch ex As Exception
                    Mensaje(EeventViewerImages.MensajeError) = "Error al restaurar layout: " & ex.Message
                Finally
                    layoutControl.EndUpdate()
                End Try

                ' Limpiar referencias DESPUÉS del Finally
                _originalParent = Nothing
                _originalNextItem = Nothing

                ' Limpiar referencias a GridControls dinámicos
                _fiscalData = Nothing
                _niffData = Nothing

                ' Remover handler del grid principal si existe
                RemoveHandler INDviewDeteriorationByClassification.CustomDrawFooter, AddressOf DynamicGridView_CustomDrawFooter

                ' Limpiar botones exportar dinámicos
                If _fiscalExportButton IsNot Nothing AndAlso _fiscalGridControl IsNot Nothing AndAlso _fiscalGridControl.MainView IsNot Nothing Then
                    RemoveHandler CType(_fiscalGridControl.MainView, GridView).CustomDrawFooter, AddressOf DynamicGridView_CustomDrawFooter
                    _fiscalExportButton.Dispose()
                    _fiscalExportButton = Nothing
                End If
                If _niffExportButton IsNot Nothing AndAlso _niffGridControl IsNot Nothing AndAlso _niffGridControl.MainView IsNot Nothing Then
                    RemoveHandler CType(_niffGridControl.MainView, GridView).CustomDrawFooter, AddressOf DynamicGridView_CustomDrawFooter
                    _niffExportButton.Dispose()
                    _niffExportButton = Nothing
                End If

                _fiscalGridControl = Nothing
                _niffGridControl = Nothing
                INDgcBill.DataSource = Nothing
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message
        End Try
    End Sub

    ''' <summary>
    ''' Crea tabs dinámicos por cada libro contable
    ''' </summary>
    Private Sub CreateTabsForByClassification()
        ' Si no hay diccionario de libros contables, inicializarlo vacío para permitir crear tabs por defecto
        If _dictionaryLegalBook Is Nothing Then
            _dictionaryLegalBook = New Dictionary(Of Integer, BookXpo)
        End If

        ' Verificar si los tabs ya existen para actualizar DataSources en lugar de recrear
        Dim layoutControl As DevExpress.XtraLayout.LayoutControl = TryCast(INDlygCxC.Owner, DevExpress.XtraLayout.LayoutControl)
        Dim existingTabGroup As DevExpress.XtraLayout.TabbedControlGroup = Nothing
        If layoutControl IsNot Nothing AndAlso _originalParent IsNot Nothing Then
            existingTabGroup = _originalParent.Items.OfType(Of DevExpress.XtraLayout.TabbedControlGroup)() _
                          .FirstOrDefault(Function(item) item.Name = "TabbedControlGroupBooks")
        End If

        ' Si los tabs ya existen, actualizar DataSources y salir
        If existingTabGroup IsNot Nothing Then
            ' Asegurar que las listas de datos existan (inicializar vacías si no existen)
            If _fiscalData Is Nothing Then _fiscalData = New List(Of PortfolioDeteriorationByClassificationDTO)
            If _niffData Is Nothing Then _niffData = New List(Of PortfolioDeteriorationByClassificationDTO)

            ' Determinar qué tab es el primero basado en los tipos de libros disponibles
            Dim bookTypes As New List(Of Integer)
            If _dictionaryLegalBook IsNot Nothing AndAlso _dictionaryLegalBook.Count > 0 Then
                Dim fiscalBookExists = _dictionaryLegalBook.Values.Any(Function(b) b.TypeBook = 1)
                Dim niffBookExists = _dictionaryLegalBook.Values.Any(Function(b) b.TypeBook = 2)
                If fiscalBookExists Then bookTypes.Add(1)
                If niffBookExists Then bookTypes.Add(2)
            Else
                If _fiscalData.Count > 0 OrElse _niffData.Count = 0 Then bookTypes.Add(1)
                If _niffData.Count > 0 OrElse _fiscalData.Count = 0 Then bookTypes.Add(2)
                If bookTypes.Count = 0 Then
                    bookTypes.Add(1)
                    bookTypes.Add(2)
                End If
            End If

            ' Actualizar DataSource del primer tab (INDgcBill) basado en el primer tipo de libro
            If bookTypes.Count > 0 Then
                Dim firstBookType = bookTypes(0)
                INDgcBill.DataSource = If(firstBookType = 1, _fiscalData, _niffData)
                INDgcBill.RefreshDataSource()
            End If

            ' Actualizar DataSources de los GridControls dinámicos si existen
            If _fiscalGridControl IsNot Nothing Then
                _fiscalGridControl.DataSource = _fiscalData
                _fiscalGridControl.RefreshDataSource()
            End If
            If _niffGridControl IsNot Nothing Then
                _niffGridControl.DataSource = _niffData
                _niffGridControl.RefreshDataSource()
            End If
            Return
        End If

        ' Clear existing tabs
        ClearTabsForByClassification()

        ' Hide specified controls
        INDlyItemPopupBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ' Reutilizar layoutControl ya obtenido anteriormente, o obtenerlo si no existe
        If layoutControl Is Nothing Then
            layoutControl = TryCast(INDlygCxC.Owner, DevExpress.XtraLayout.LayoutControl)
        End If

        If layoutControl Is Nothing Then
            Mensaje(EeventViewerImages.MensajeError) = "Could not get LayoutControl reference"
            Exit Sub
        End If

        ' Begin layout update
        layoutControl.BeginUpdate()

        Try
            ' SAVE original parent, location, size and next item
            _originalParent = TryCast(INDlyItemBill.Parent, DevExpress.XtraLayout.LayoutControlGroup)
            _originalLocation = INDlyItemBill.Location
            _originalSize = INDlyItemBill.Size

            ' Save the item after INDlyItemBill to restore position
            If _originalParent IsNot Nothing Then
                Dim currentIndex As Integer = _originalParent.Items.IndexOf(INDlyItemBill)
                If currentIndex >= 0 AndAlso currentIndex < _originalParent.Items.Count - 1 Then
                    _originalNextItem = _originalParent.Items(currentIndex + 1)
                Else
                    _originalNextItem = Nothing
                End If
            End If

            ' Create TabbedControlGroup
            Dim TabbedControlGroup As New DevExpress.XtraLayout.TabbedControlGroup()
            CType(TabbedControlGroup, System.ComponentModel.ISupportInitialize).BeginInit()

            TabbedControlGroup.Location = _originalLocation
            TabbedControlGroup.Name = "TabbedControlGroupBooks"
            TabbedControlGroup.SelectedTabPageIndex = 0
            TabbedControlGroup.Size = _originalSize

            ' Add TabbedControlGroup to layout in correct position
            If _originalParent IsNot Nothing Then
                If _originalNextItem IsNot Nothing Then
                    _originalParent.AddItem(TabbedControlGroup, _originalNextItem, DevExpress.XtraLayout.Utils.InsertType.Left)
                Else
                    _originalParent.AddItem(TabbedControlGroup)
                End If
            Else
                INDlygCxC.AddItem(TabbedControlGroup)
            End If

            CType(TabbedControlGroup, System.ComponentModel.ISupportInitialize).EndInit()

            ' Determine which book types exist based on dictionary, not data
            Dim bookTypes As New List(Of Integer)
            ' Siempre crear tabs para los tipos de libros que existen en el diccionario
            If _dictionaryLegalBook IsNot Nothing AndAlso _dictionaryLegalBook.Count > 0 Then
                Dim fiscalBookExists = _dictionaryLegalBook.Values.Any(Function(b) b.TypeBook = 1)
                Dim niffBookExists = _dictionaryLegalBook.Values.Any(Function(b) b.TypeBook = 2)
                If fiscalBookExists Then bookTypes.Add(1)
                If niffBookExists Then bookTypes.Add(2)
            Else
                ' Si no hay diccionario, crear tabs basados en datos disponibles o ambos por defecto
                If (_fiscalData IsNot Nothing AndAlso _fiscalData.Count > 0) OrElse (_niffData Is Nothing OrElse _niffData.Count = 0) Then
                    bookTypes.Add(1)
                End If
                If (_niffData IsNot Nothing AndAlso _niffData.Count > 0) OrElse (_fiscalData Is Nothing OrElse _fiscalData.Count = 0) Then
                    bookTypes.Add(2)
                End If
                ' Si no hay datos ni diccionario, crear ambos tabs por defecto
                If bookTypes.Count = 0 Then
                    bookTypes.Add(1)
                    bookTypes.Add(2)
                End If
            End If

            ' Asegurar que las listas de datos existan (inicializar vacías si no existen)
            If _fiscalData Is Nothing Then _fiscalData = New List(Of PortfolioDeteriorationByClassificationDTO)
            If _niffData Is Nothing Then _niffData = New List(Of PortfolioDeteriorationByClassificationDTO)

            ' Create a tab for each book type
            Dim isFirstTab As Boolean = True
            For Each bookType In bookTypes
                Dim tabGroup As New DevExpress.XtraLayout.LayoutControlGroup()
                CType(tabGroup, System.ComponentModel.ISupportInitialize).BeginInit()

                ' Get book name from dictionary
                Dim nametext As String = _dictionaryLegalBook?.Values.FirstOrDefault(Function(b) b.TypeBook = bookType)?.Name
                If String.IsNullOrEmpty(nametext) Then nametext = If(bookType = 1, "Fiscal", "Niif")

                tabGroup.Text = nametext
                tabGroup.Name = "TabBook_" & bookType.ToString()

                ' Add tabGroup to TabbedControlGroup FIRST
                TabbedControlGroup.AddTabPage(tabGroup)
                CType(tabGroup, System.ComponentModel.ISupportInitialize).EndInit()

                If isFirstTab Then
                    ' First tab: use main gridControl and gridView
                    INDgcBill.DataSource = If(bookType = 1, _fiscalData, _niffData)
                    INDgcBill.MainView = INDviewDeteriorationByClassification

                    ' Suscribir el handler personalizado para posicionar el botón exportar arriba
                    If IndigoGridControl1.GetExportButton(INDgcBill) Then
                        AddHandler INDviewDeteriorationByClassification.CustomDrawFooter, AddressOf DynamicGridView_CustomDrawFooter

                        ' Obtener el botón exportar existente del grid principal
                        Dim existingButton As Presentation.Controls.ExportDataButton = Nothing
                        For Each ctrl As Control In INDgcBill.Controls
                            If TypeOf ctrl Is Presentation.Controls.ExportDataButton Then
                                existingButton = CType(ctrl, Presentation.Controls.ExportDataButton)
                                Exit For
                            End If
                        Next

                        ' Guardar referencia al botón según el tipo de libro
                        If bookType = 1 Then
                            _fiscalExportButton = existingButton
                        ElseIf bookType = 2 Then
                            _niffExportButton = existingButton
                        End If
                    End If

                    ' NOW move the original LayoutControlItem (tabGroup is already in layout)
                    INDlyItemBill.Move(tabGroup, DevExpress.XtraLayout.Utils.InsertType.Top)
                    INDlyItemBill.Location = New System.Drawing.Point(0, 0)
                    INDlyItemBill.Size = New System.Drawing.Size(_originalSize.Width, _originalSize.Height)
                    INDlyItemBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                    isFirstTab = False
                Else
                    ' Second tab: create new cloned GridControl/GridView
                    Dim newGridControl As New DevExpress.XtraGrid.GridControl()
                    Dim newView As New DevExpress.XtraGrid.Views.Grid.GridView(newGridControl)
                    newGridControl.MainView = newView
                    newGridControl.ViewCollection.Add(newView)
                    newGridControl.Dock = DockStyle.Fill

                    ' Clonar todas las propiedades del GridView
                    CloneGridViewProperties(INDviewDeteriorationByClassification, newView)

                    ' Clone columns from base view (con RepositoryItems)
                    CloneColumnsFrom(INDviewDeteriorationByClassification, newView, newGridControl)

                    ' Assign filtered data (incluso si está vacío)
                    newGridControl.DataSource = If(bookType = 1, _fiscalData, _niffData)

                    ' Crear e inicializar el botón exportar para el grid dinámico
                    If IndigoGridControl1.GetExportButton(INDgcBill) Then
                        ' Crear instancia del botón exportar
                        Dim exportButton As New Presentation.Controls.ExportDataButton(newGridControl)
                        exportButton.Visible = newView.OptionsView.ShowFooter

                        ' Suscribir el handler personalizado que posiciona el botón arriba
                        AddHandler newView.CustomDrawFooter, AddressOf DynamicGridView_CustomDrawFooter

                        ' Guardar referencia al botón según el tipo de libro
                        If bookType = 1 Then
                            _fiscalExportButton = exportButton
                        ElseIf bookType = 2 Then
                            _niffExportButton = exportButton
                        End If
                    End If

                    ' Guardar referencia al GridControl para poder actualizar su DataSource después
                    If bookType = 1 Then
                        _fiscalGridControl = newGridControl
                    ElseIf bookType = 2 Then
                        _niffGridControl = newGridControl
                    End If

                    ' Create and add LayoutControlItem for new grid
                    Dim gridItem As New DevExpress.XtraLayout.LayoutControlItem()
                    gridItem.Control = newGridControl
                    gridItem.TextVisible = False
                    tabGroup.AddItem(gridItem)
                End If
            Next

        Finally
            layoutControl.EndUpdate()
        End Try

        ' Refresh grids
        INDgcBill.RefreshDataSource()

        ' Refrescar también los GridControls dinámicos si existen
        If _fiscalGridControl IsNot Nothing Then
            _fiscalGridControl.RefreshDataSource()
        End If
        If _niffGridControl IsNot Nothing Then
            _niffGridControl.RefreshDataSource()
        End If
    End Sub


    ''' <summary>
    ''' Método encargado de copiar las columnas para los diferentes tabs
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="target"></param>
    ''' <param name="gridControl">GridControl destino que necesita los RepositoryItems</param>
    Private Sub CloneColumnsFrom(source As DevExpress.XtraGrid.Views.Grid.GridView, target As DevExpress.XtraGrid.Views.Grid.GridView, gridControl As DevExpress.XtraGrid.GridControl)
        target.Columns.Clear()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In source.Columns
            Dim newCol As New DevExpress.XtraGrid.Columns.GridColumn()
            newCol.FieldName = col.FieldName
            newCol.Caption = col.Caption
            newCol.Visible = col.Visible
            newCol.VisibleIndex = col.VisibleIndex
            newCol.Width = col.Width

            ' Copiar propiedades adicionales de la columna
            newCol.DisplayFormat.FormatString = col.DisplayFormat.FormatString
            newCol.DisplayFormat.FormatType = col.DisplayFormat.FormatType
            newCol.UnboundType = col.UnboundType
            If Not String.IsNullOrEmpty(col.UnboundExpression) Then
                newCol.UnboundExpression = col.UnboundExpression
            End If
            newCol.ImageOptions.Alignment = col.ImageOptions.Alignment

            ' Copiar OptionsColumn
            newCol.OptionsColumn.AllowEdit = col.OptionsColumn.AllowEdit
            newCol.OptionsColumn.AllowFocus = col.OptionsColumn.AllowFocus
            newCol.OptionsColumn.AllowGroup = col.OptionsColumn.AllowGroup
            newCol.OptionsColumn.AllowMerge = col.OptionsColumn.AllowMerge
            newCol.OptionsColumn.AllowMove = col.OptionsColumn.AllowMove
            newCol.OptionsColumn.AllowShowHide = col.OptionsColumn.AllowShowHide
            newCol.OptionsColumn.AllowSize = col.OptionsColumn.AllowSize
            newCol.OptionsColumn.AllowSort = col.OptionsColumn.AllowSort
            newCol.OptionsColumn.FixedWidth = col.OptionsColumn.FixedWidth
            newCol.OptionsFilter.AllowAutoFilter = col.OptionsFilter.AllowAutoFilter
            newCol.OptionsFilter.AllowFilter = col.OptionsFilter.AllowFilter

            ' Copiar ColumnEdit (especialmente importante para INDrepCheckSelectOption)
            If col.ColumnEdit IsNot Nothing Then
                ' Verificar si el RepositoryItem ya existe en el GridControl
                Dim repoItemName As String = col.ColumnEdit.Name
                Dim existingRepoItem As DevExpress.XtraEditors.Repository.RepositoryItem = Nothing

                For Each repoItem As DevExpress.XtraEditors.Repository.RepositoryItem In gridControl.RepositoryItems
                    If repoItem.Name = repoItemName Then
                        existingRepoItem = repoItem
                        Exit For
                    End If
                Next

                ' Si no existe, agregarlo al GridControl
                If existingRepoItem Is Nothing Then
                    ' Clonar el RepositoryItem
                    If TypeOf col.ColumnEdit Is DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit Then
                        Dim sourceCheckEdit As DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit = CType(col.ColumnEdit, DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit)
                        Dim newCheckEdit As New DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit()
                        newCheckEdit.AutoHeight = sourceCheckEdit.AutoHeight
                        newCheckEdit.Name = repoItemName
                        gridControl.RepositoryItems.Add(newCheckEdit)
                        newCol.ColumnEdit = newCheckEdit
                    Else
                        ' Para otros tipos de RepositoryItems, usar el mismo del GridControl principal si existe
                        Dim mainRepoItem As DevExpress.XtraEditors.Repository.RepositoryItem = INDgcBill.RepositoryItems(repoItemName)
                        If mainRepoItem IsNot Nothing Then
                            gridControl.RepositoryItems.Add(mainRepoItem)
                            newCol.ColumnEdit = mainRepoItem
                        End If
                    End If
                Else
                    newCol.ColumnEdit = existingRepoItem
                End If
            End If

            target.Columns.Add(newCol)
        Next
    End Sub

    ''' <summary>
    ''' Método auxiliar que copia todas las propiedades de estilo y configuración del GridView fuente al destino
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="target"></param>
    Private Sub CloneGridViewProperties(source As DevExpress.XtraGrid.Views.Grid.GridView, target As DevExpress.XtraGrid.Views.Grid.GridView)
        ' Copiar Appearance settings
        target.Appearance.FocusedRow.BorderColor = source.Appearance.FocusedRow.BorderColor
        target.Appearance.FocusedRow.Font = source.Appearance.FocusedRow.Font
        target.Appearance.FocusedRow.Options.UseBorderColor = source.Appearance.FocusedRow.Options.UseBorderColor
        target.Appearance.FocusedRow.Options.UseFont = source.Appearance.FocusedRow.Options.UseFont
        target.Appearance.FocusedRow.Options.UseForeColor = source.Appearance.FocusedRow.Options.UseForeColor

        target.Appearance.GroupRow.Font = source.Appearance.GroupRow.Font
        target.Appearance.GroupRow.Options.UseFont = source.Appearance.GroupRow.Options.UseFont

        target.Appearance.HeaderPanel.Font = source.Appearance.HeaderPanel.Font
        target.Appearance.HeaderPanel.Options.UseFont = source.Appearance.HeaderPanel.Options.UseFont

        target.Appearance.Row.Font = source.Appearance.Row.Font
        target.Appearance.Row.Options.UseFont = source.Appearance.Row.Options.UseFont

        target.Appearance.ViewCaption.Font = source.Appearance.ViewCaption.Font
        target.Appearance.ViewCaption.Options.UseFont = source.Appearance.ViewCaption.Options.UseFont

        ' Copiar OptionsView
        target.OptionsView.EnableAppearanceEvenRow = source.OptionsView.EnableAppearanceEvenRow
        target.OptionsView.EnableAppearanceOddRow = source.OptionsView.EnableAppearanceOddRow
        target.OptionsView.ShowAutoFilterRow = source.OptionsView.ShowAutoFilterRow
        target.OptionsView.ShowDetailButtons = source.OptionsView.ShowDetailButtons
        target.OptionsView.ShowFooter = source.OptionsView.ShowFooter
        target.OptionsView.ShowGroupPanel = source.OptionsView.ShowGroupPanel

        ' Copiar OptionsSelection
        target.OptionsSelection.MultiSelect = source.OptionsSelection.MultiSelect
        target.OptionsSelection.EnableAppearanceFocusedCell = source.OptionsSelection.EnableAppearanceFocusedCell

        ' Copiar FocusRectStyle
        target.FocusRectStyle = source.FocusRectStyle

        ' Aplicar propiedades personalizadas de IndigoGridView
        If IndigoGridView1 IsNot Nothing Then
            Dim temaIndigoMetro As Boolean = IndigoGridView1.GetTemaIndigoMetro(source)
            IndigoGridView1.SetTemaIndigoMetro(target, temaIndigoMetro)
        End If
    End Sub



    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._portfolioProvision IsNot Nothing AndAlso Me._portfolioProvision.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
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

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _operativeUnitId = Nothing
        _currentSequenceId = Nothing
        _record = Nothing
        _sequence = Nothing
        _presenter = Nothing
        _portfolioProvision = Nothing
        ListProcess = Nothing
        ListApplyDeterioration = Nothing
        ListPortfolioProvisionDetail = Nothing
        bwLoadBills = Nothing
        ListViewAccountReceivableByPortfolioProvisionXpo = Nothing
        ListMessage = Nothing
        _varImp = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub

#End Region

#Region "KeyDown"

    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewPortfolioProvision()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub INDpceBill_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceBill.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceBill.ShowPopup()
        End If
    End Sub

#End Region

#Region "Popup"

    Private Sub INDpceBill_Popup(sender As Object, e As EventArgs) Handles INDpceBill.Popup
        If TabbedControlGroup1.SelectedTabPageIndex = 0 Then 'Si esta el primer tab activo
            INDSleInvoiceNumber.Focus()
        Else 'SI esta el segundo tab activo
            INDsleAges.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleInvoiceNumber_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleInvoiceNumber.QueryPopUp
        If InvoiceNumberXpo Is Nothing Then
            If Me.BarraBotones.OperatingUnit Is Nothing OrElse Me.BarraBotones.OperatingUnit.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una unidad operativa"
                Exit Sub
            End If
            If CourtDate Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar una fecha de corte"
                Exit Sub
            End If
            If INDsleProcess.EditValue = 2 AndAlso ApplyDeterioration Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe indicar a que se aplicará el deterioro (Cartera o Glosa)"
                Exit Sub
            End If

            _presenter.InititalizeAges(BarraBotones.OperatingUnit.Id)
            If INDsleProcess.EditValue = 2 AndAlso INDsleApplyDeterioration.EditValue = 2 Then 'Si el proceso es deterioro y aplica a glosa
                _presenter.ListInvoiceProvisionAndDeteriorationGlosa(CourtDate)
            ElseIf INDsleProcess.EditValue = 2 AndAlso INDsleApplyDeterioration.EditValue = 1 Then 'Si el proceso es deterioro y aplica a cxc
                _presenter.ListInvoiceProvisionAndDeteriorationNotGlosa(CourtDate)
            Else 'Si es provision
                _presenter.ListInvoiceProvisionAndDeterioration()
            End If
        End If
    End Sub

    Private Sub INDGvSelector_CustomUnboundColumnData(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs) Handles INDGvInvoiceNumber.CustomUnboundColumnData
        If e.IsGetData Then
            Dim view = TryCast(sender, GridView)
            Dim row = e.Row
            If row IsNot Nothing AndAlso row.GetType <> GetType(DevExpress.Data.NotLoadedObject) Then
                Dim entity = CType(row, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                Dim diff = DateDiff(DateInterval.Day, entity.DocumentDate, CourtDate.Value)
                If e.Column.Name = "INDColInvoiceNumber_Diff" Then
                    e.Value = diff
                ElseIf e.Column.Name = "INDColInvoiceNumber_Age" Then
                    If ListAgesPorfolio IsNot Nothing Then
                        Dim range = String.Empty
                        Dim age = (From a In ListAgesPorfolio Where a.InitialRange <= diff AndAlso diff <= a.EndRange Select a).FirstOrDefault
                        If age IsNot Nothing Then
                            range = age.Name
                        End If
                        e.Value = range
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub INDsleAges_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAges.QueryPopUp
        If BarraBotones.OperatingUnit Is Nothing OrElse BarraBotones.OperatingUnit.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una unidad operativa"
            Exit Sub
        End If

        _presenter.InititalizeAges(BarraBotones.OperatingUnit.Id)
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDdteCourtDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteCourtDate.EditValueChanged
        ValidateEnabled()
        InvoiceNumberXpo = Nothing
        If ApplyDeterioration = 3 Then
            ctrDeterioro.ChangeToDeteriorationByClassification(CourtDate)
        End If
    End Sub

    Private Sub INDsleProcess_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProcess.EditValueChanged
        ValidateEnabled()
        If Process IsNot Nothing Then
            If Process = 1 Then 'Provisión
                colPercentage.Caption = "% Provisión"
                colValue.Caption = "Valor Provisión"

                'Se oculta el campo aplica deterioro a
                INDlyItemApplyDeterioration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemApplyDeterioration.AllowHide = True
                ApplyDeterioration = Nothing

                ' Limpiar los tabs si existen y restaurar el estado normal
                ClearTabsForByClassification()
                INDlyItemPopupBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDgcBill.MainView = INDviewBill
            Else 'Deterioro
                colPercentage.Caption = "Porcentaje VPN"
                colValue.Caption = "Valor Deterioro"

                'Se habilita el campo aplica deterioro a
                INDlyItemApplyDeterioration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemApplyDeterioration.AllowHide = False
                'Se habilita nueva opción en el campo Aplica Deterioro
                If _settingPortfolio.ApplyDeteriorationByClassification Then
                    ' Verificar que NO exista el item antes de agregarlo
                    If Not ListApplyDeterioration.Any(Function(x) x.Item1 = 3) Then
                        ListApplyDeterioration.Add(New Tuple(Of Integer, String)(3, "Deterioro por Clasificación"))
                        INDsleApplyDeterioration.Properties.DataSource = ListApplyDeterioration.ToList()
                        GetLegalBooks() 'Obtenemos los Libros Contables para crear los Tabs
                    End If
                Else
                    ' Verificar si existe el item antes de eliminarlo
                    If ListApplyDeterioration.Any(Function(x) x.Item1 = 3) Then
                        ListApplyDeterioration.RemoveAll(Function(x) x.Item1 = 3)
                        INDsleApplyDeterioration.Properties.DataSource = ListApplyDeterioration.ToList()
                    End If
                End If

            End If
        End If
        EnableFilterInvoicesParameter()
    End Sub

    Private Async Sub INDsleApplyDeterioration_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleApplyDeterioration.EditValueChanged
        EnableFilterInvoicesParameter()
        If ApplyDeterioration = 3 Then
            ' Obtener libros contables SIEMPRE antes de crear los tabs (solo si no están cargados)
            If _dictionaryLegalBook Is Nothing OrElse _dictionaryLegalBook.Count = 0 Then
                GetLegalBooks()
            End If

            ' Inicializar listas vacías si no existen
            If _fiscalData Is Nothing Then _fiscalData = New List(Of PortfolioDeteriorationByClassificationDTO)
            If _niffData Is Nothing Then _niffData = New List(Of PortfolioDeteriorationByClassificationDTO)

            ' Crear tabs inmediatamente, antes de cargar datos (incluso si no hay libros contables)
            CreateTabsForByClassification()

            ' Ahora cargar los datos (si están disponibles los parámetros necesarios)
            Await GetPortfolioDeterioratioByClassification()
        Else
            ' Limpiar los tabs si existen y restaurar el estado normal
            ClearTabsForByClassification()
            INDlyItemPopupBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDgcBill.MainView = INDviewBill
        End If
    End Sub

    Private Sub INDsleAges_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAges.EditValueChanged
        If AgesId IsNot Nothing Then
            PortfolioAgesPortfolio = DirectCast(GridView2.GetFocusedRow, Domain.Entities.AgesPortfolio)
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepSePercentage_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepSePercentage.EditValueChanging
        If e IsNot Nothing Then
            Dim row As PortfolioProvisionDetail = INDviewBill.GetFocusedRow()
            If row IsNot Nothing Then
                row.Percentage = e.NewValue.ToString.Replace(".", ",")
                row.Value = Math.Round(row.BalanceAccountReceivable * row.Percentage / 100, 0)
                INDgcBill.RefreshDataSource()
            End If
        End If
    End Sub

    Private Sub INDrepCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim row As PortfolioProvisionDetail = INDviewBill.GetFocusedRow()
            If row IsNot Nothing Then
                row.SelectOption = e.NewValue

                If row.Id > 0 Then
                    If ListDeletePortfolioProvisionDetail Is Nothing Then
                        ListDeletePortfolioProvisionDetail = New List(Of Integer)
                    End If

                    If ListDeletePortfolioProvisionDetail.Any(Function(detailId) detailId = row.Id) Then
                        ListDeletePortfolioProvisionDetail.Remove(row.Id)
                    ElseIf Not row.SelectOption Then
                        ListDeletePortfolioProvisionDetail.Add(row.Id)
                    End If
                End If

                INDgcBill.RefreshDataSource()
                VisibleCheck()
            End If
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddInvoices()
    End Sub

#End Region

#Region "DataSourceChanged"

    Private Sub INDviewBill_DataSourceChanged(sender As Object, e As EventArgs) Handles INDviewBill.DataSourceChanged
        If INDviewBill.RowCount > 0 Then 'Se bloquean los controles
            INDdteCourtDate.Properties.ReadOnly = True
            INDsleProcess.Properties.ReadOnly = True
            INDsleApplyDeterioration.Properties.ReadOnly = True
        Else 'Se desbloquean los controles
            INDdteCourtDate.Properties.ReadOnly = False
            INDsleProcess.Properties.ReadOnly = False
            INDsleApplyDeterioration.Properties.ReadOnly = False
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"

    Private Sub INDgcBill_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcBill.MouseDoubleClick
        If ListPortfolioProvisionDetail IsNot Nothing AndAlso ListPortfolioProvisionDetail.Count > 0 Then
            Dim hitPoint = Me.INDviewBill.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDcolSelect") Then

                    Dim listFilterXpCollection = INDviewBill.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDcolSelect.Image = Global.Presentation.Portfolio.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        cont = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If cont = ListPortfolioProvisionDetail.Count Then
                            Me.INDcolSelect.Image = Global.Presentation.Portfolio.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDgcBill.RefreshDataSource()
                    Me.INDgcBill.Invalidate()
                End If
            End If
        End If
    End Sub

#End Region

#Region "ItemClick"

    Private Sub BarButtonItem3_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbDelete.ItemClick
        DeleteDetail()
    End Sub

    Private Sub BarButtonItem1_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbSelection.ItemClick
        SelectOptions(1)
    End Sub

    Private Sub BarButtonItem2_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbUnselection.ItemClick
        SelectOptions(0)
    End Sub

    Private Sub INDbbModificarVPN_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbModificarVPN.ItemClick
        Using frm As New FrmChangeRateVPN()
            frm.VPNRate = ctrDeterioro.TasaVPN
            AddHandler frm.OnChangeVPNRate, AddressOf OnChangeVPNRate
            Dim tr As New FrmTransparent(frm, False)
            tr.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDbbExpectative_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbExpectative.ItemClick
        Using frm As New FrmChangeExpectative()
            frm.Expectative = ctrDeterioro.Expectative 'CType(INDviewBill.GetFocusedRow(), PortfolioProvisionDetail).Percentage
            AddHandler frm.OnChangeExpectative, AddressOf OnChangeExpectative
            Dim tr As New FrmTransparent(frm, False)
            tr.ShowDialog(Me)
        End Using
    End Sub

    Private Sub INDbbActualizarValores_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles INDbbActualizarValores.ItemClick
        Dim childRows As Integer = INDviewBill.GetChildRowCount(INDviewBill.FocusedRowHandle)
        If childRows > 0 Then
            For i As Integer = 0 To childRows - 1 Step 1
                Dim childGroup As Integer = INDviewBill.GetChildRowCount(INDviewBill.GetChildRowHandle(INDviewBill.FocusedRowHandle, i))
                If INDviewBill.IsGroupRow(INDviewBill.GetChildRowHandle(INDviewBill.FocusedRowHandle, i)) AndAlso childGroup > 0 Then
                    For j As Integer = 0 To childGroup - 1 Step 1
                        Dim rowData As PortfolioProvisionDetail = INDviewBill.GetRow(INDviewBill.GetChildRowHandle(INDviewBill.GetChildRowHandle(INDviewBill.FocusedRowHandle, i), j))
                        If rowData IsNot Nothing Then
                            Dim entity = _presenter.GeViewAccountReceivableByPortfolioProvisionById(rowData.Id)
                            If entity IsNot Nothing Then
                                With rowData
                                    .FacturerValue = entity.Value
                                    If INDsleApplyDeterioration.EditValue = 2 Then 'Si aplica a glosa
                                        .ValueGlosado = entity.BalanceGlosa
                                    End If
                                    .BalanceAccountReceivable = entity.Balance
                                    .AccumulatedDeterioration = entity.DeteriorationBalance
                                    .CalculateDeterioration()
                                End With
                            End If
                        End If
                    Next
                Else
                    Dim rowData As PortfolioProvisionDetail = INDviewBill.GetRow(INDviewBill.GetChildRowHandle(INDviewBill.FocusedRowHandle, i))
                    If rowData IsNot Nothing Then
                        Dim entity = _presenter.GeViewAccountReceivableByPortfolioProvisionById(rowData.Id)
                        If entity IsNot Nothing Then
                            With rowData
                                .FacturerValue = entity.Value
                                If INDsleApplyDeterioration.EditValue = 2 Then
                                    .ValueGlosado = entity.BalanceGlosa
                                End If
                                .BalanceAccountReceivable = entity.Balance
                                .AccumulatedDeterioration = entity.DeteriorationBalance
                                .CalculateDeterioration()
                            End With
                        End If
                    End If
                End If
            Next
        Else
            Dim rowData As PortfolioProvisionDetail = INDviewBill.GetFocusedRow()
            If rowData IsNot Nothing Then
                Dim entity = _presenter.GeViewAccountReceivableByPortfolioProvisionById(rowData.Id)
                If entity IsNot Nothing Then
                    With rowData
                        .FacturerValue = entity.Value
                        If INDsleApplyDeterioration.EditValue = 2 Then
                            .ValueGlosado = entity.BalanceGlosa
                        End If
                        .BalanceAccountReceivable = entity.Balance
                        .AccumulatedDeterioration = entity.DeteriorationBalance
                        .CalculateDeterioration()
                    End With
                End If
            End If
        End If
        INDgcBill.RefreshDataSource()
    End Sub

#End Region

#Region "PopupMenuShowing"

    Private Sub INDviewBill_PopupMenuShowing(sender As Object, e As Views.Grid.PopupMenuShowingEventArgs) Handles INDviewBill.PopupMenuShowing
        If e.HitInfo IsNot Nothing Then
            INDbbDelete.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            If INDsleProcess.EditValue IsNot Nothing AndAlso INDsleProcess.EditValue = 2 AndAlso (INDsleApplyDeterioration.EditValue = 1 OrElse INDsleApplyDeterioration.EditValue = 2) Then
                INDbbModificarVPN.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDbbExpectative.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
                INDbbActualizarValores.Visibility = DevExpress.XtraBars.BarItemVisibility.Always
            Else
                INDbbModificarVPN.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                INDbbExpectative.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
                INDbbActualizarValores.Visibility = DevExpress.XtraBars.BarItemVisibility.Never
            End If
            PopupMenu1.Manager = BarManager1
            PopupMenu1.ShowPopup(INDviewBill.GridControl.PointToScreen(e.Point))
        End If
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Await PasteToGrid(sender, e.Rows)
    End Sub

    Private Async Function PasteToGrid(sender As DevExpress.XtraGrid.GridControl, ListInfo As List(Of List(Of String))) As Task
        If sender.Name = INDgcBill.Name Then
            INDviewBill.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MPortfolioProvision(MyTag)
                Dim result = Await model.CopyAndPastePortfolioProvision(ListInfo, CourtDate, Process, BarraBotones.OperatingUnit.Id, INDsleApplyDeterioration.EditValue, ctrDeterioro.TasaVPN, ctrDeterioro.Expectative)
                If result.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    INDviewBill.HideLoadingPanel()
                    Exit Function
                End If

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    If ListPortfolioProvisionDetail IsNot Nothing AndAlso ListPortfolioProvisionDetail.Count > 0 Then
                        For Each item In result.ObjectEmbbeded
                            If ListPortfolioProvisionDetail.Any(Function(x) x.AccountReceivableId = item.AccountReceivableId) Then
                                If result.ObjectEmbbededAux Is Nothing Then
                                    result.ObjectEmbbededAux = New List(Of Tuple(Of String, Integer))
                                End If
                                result.ObjectEmbbededAux.Add(New Tuple(Of String, Integer)("La factura " + item.InvoiceNumber + " ya existe en la lista", 2))
                            Else
                                ListPortfolioProvisionDetail.Add(item)
                            End If
                        Next
                    Else
                        ListPortfolioProvisionDetail = result.ObjectEmbbeded
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
            INDgcBill.DataSource = Nothing
            INDgcBill.DataSource = ListPortfolioProvisionDetail
            Me.Cursor = System.Windows.Forms.Cursors.Default
            INDviewBill.HideLoadingPanel()
            VisibleCheck()
            INDgcBill.RefreshDataSource()
            INDviewBill.ExpandAllGroups()
        End If
    End Function

#End Region

#Region "FormClosing"

    Private Sub FrmProvisionAndDeterioration_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Handler personalizado para CustomDrawFooter que posiciona el botón exportar en la parte superior del footer
    ''' </summary>
    Private Sub DynamicGridView_CustomDrawFooter(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs)
        Dim gridView As GridView = TryCast(sender, GridView)
        If gridView Is Nothing Then Return

        Dim gridControl As GridControl = TryCast(gridView.GridControl, GridControl)
        If gridControl Is Nothing Then Return

        ' Determinar qué botón usar según el grid
        Dim exportButton As Presentation.Controls.ExportDataButton = Nothing
        If gridControl Is INDgcBill Then
            ' El primer tab puede usar cualquiera de los dos botones según qué libro sea el primero
            exportButton = If(_fiscalExportButton, _niffExportButton)
        ElseIf gridControl Is _fiscalGridControl Then
            exportButton = _fiscalExportButton
        ElseIf gridControl Is _niffGridControl Then
            exportButton = _niffExportButton
        End If

        If exportButton Is Nothing Then Return

        ' Agregar el botón al grid si no está agregado
        If Not gridControl.Controls.Contains(exportButton) Then
            gridControl.Controls.Add(exportButton)
        End If

        ' Obtener el ancho del indicador (columna de selección)
        Dim pInfo = gridView.GetType().GetField("fViewInfo", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
        Dim xWidth As Integer = 0
        If pInfo IsNot Nothing Then
            Dim col = DirectCast(pInfo.GetValue(gridView), DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)
            xWidth = col.ViewRects.IndicatorWidth
        End If

        ' Posicionar el botón en la parte SUPERIOR del footer (sin centrado)
        exportButton.Anchor = AnchorStyles.Top Or AnchorStyles.Left
        exportButton.Location = New Point(e.Bounds.Location.X + 2, e.Bounds.Location.Y + 1)
        exportButton.Size = New System.Drawing.Size(34, e.Bounds.Height - 2)
        exportButton.BringToFront()
    End Sub

    ''' <summary>
    ''' Carga los estados de la devolucion de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Llena el control de comportamiento con el listado
    ''' </summary>
    Private Sub InitializeTuples()
        ListProcess = New List(Of Tuple(Of Integer, String))
        ListProcess.Add(New Tuple(Of Integer, String)(1, "Provisión"))
        ListProcess.Add(New Tuple(Of Integer, String)(2, "Deterioro"))
        INDsleProcess.Properties.DataSource = ListProcess.ToList()

        ListApplyDeterioration = New List(Of Tuple(Of Integer, String))
        ListApplyDeterioration.Add(New Tuple(Of Integer, String)(1, "Deterioro a facturación en cartera"))
        ListApplyDeterioration.Add(New Tuple(Of Integer, String)(2, "Deterioro a facturación en glosa"))
        INDsleApplyDeterioration.Properties.DataSource = ListApplyDeterioration.ToList()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyProvisionAndDeterioration.BeginUpdate()
        DeleteBlockedRecord()

        ' Limpiar tabs de clasificación si existen
        ClearTabsForByClassification()

        ctrDeterioro.CleanControls()
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Nothing
        CourtDate = Nothing
        Process = Nothing
        INDsleApplyDeterioration.EditValue = Nothing
        INDlyItemApplyDeterioration.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemApplyDeterioration.AllowHide = True
        Description = String.Empty
        InvoiceCode = Nothing
        AgesId = Nothing
        INDcheckSelect.CheckState = CheckState.Unchecked
        INDgcBill.DataSource = Nothing
        ListPortfolioProvisionDetail = Nothing
        ListViewAccountReceivableByPortfolioProvisionXpo = Nothing
        ListMessage = Nothing
        PortfolioAgesPortfolio = Nothing
        _portfolioProvision = Nothing
        ReadOnlyControls(False)
        INDdteCourtDate.Properties.ReadOnly = False
        INDsleProcess.Properties.ReadOnly = False

        ' Restaurar la vista original de la rejilla
        INDgcBill.MainView = INDviewBill

        ' Asegurar que los controles estén visibles
        INDlyItemPopupBill.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        INDlyProvisionAndDeterioration.EndUpdate()
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
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
    ''' Habilita algunos controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateEnabled()
        If CourtDate IsNot Nothing AndAlso Process IsNot Nothing Then
            INDpceBill.Enabled = True
            INDgcBill.Enabled = True
            INDEsbBills.Enabled = True
        Else
            INDpceBill.Enabled = False
            INDgcBill.Enabled = False
            INDEsbBills.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPortfolioProvision() As Task
        _portfolioProvision = New PortfolioProvision()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._currentSequenceId = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._operativeUnitId) Then
                    Me._currentSequenceId = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._operativeUnitId).Id
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

    ''' <summary>
    ''' Metodo que carga los controles de la devolucion de solicitudes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MPortfolioProvision(CStr(Me.Tag))
                    AsyncLoader(True)
                    _portfolioProvision = (Await Model.GetPortfolioProvision(INDbteCode.Text.Trim)).ObjectEmbbeded
                    INDlyProvisionAndDeterioration.BeginUpdate()
                    If _portfolioProvision IsNot Nothing AndAlso _portfolioProvision.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_portfolioProvision.Id))
                            With _portfolioProvision
                                Me.BarraBotones.StatusRecordVisible = True
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                CourtDate = .CourtDate
                                Process = .DocumentType
                                Description = .Description
                                ApplyDeterioration = .ApplyDeterioration
                                Me.BarraBotones.StatusRecord = .Status.ToString

                                LoadDetails(.Id)

                                ' Si es deterioro por clasificación, cargar las tabs desde los detalles guardados
                                If .ApplyDeterioration = 3 Then
                                    Await GetPortfolioDeterioratioByClassification()
                                End If
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._portfolioProvision.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _portfolioProvision.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            If _portfolioProvision.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.ReadOnlyControls(False)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                Me.ReadOnlyControls(True)
                                INDEsbBills.Enabled = False
                            End If
                            Me.BarraBotones.SetDocuments(_portfolioProvision.Id, Me.Tag.ToString(), Nothing, GetType(PortfolioProvision).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDdteCourtDate.Properties.ReadOnly = True
                            INDsleProcess.Properties.ReadOnly = True
                            INDsleApplyDeterioration.Properties.ReadOnly = True
                            'Para la impresion
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _portfolioProvision.Id, 0, _portfolioProvision.Code)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewPortfolioProvision()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlyProvisionAndDeterioration.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Carga los detalles a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadDetails(PortfolioProvisionId)
        ListPortfolioProvisionDetail = New List(Of PortfolioProvisionDetail)
        Dim ListDetail = _presenter.ListVPortfolioProvisionDetailByProvisionId(PortfolioProvisionId)
        If ListDetail IsNot Nothing AndAlso ListDetail.Count > 0 Then
            For Each itemXpo As VPortfolioProvisionDetailXpo In ListDetail
                Dim PortfolioProvisionDetail = New PortfolioProvisionDetail
                With PortfolioProvisionDetail
                    .Id = itemXpo.Id
                    .SelectOption = True
                    .ConfirmDateAccountReceivable = itemXpo.ConfirmDateAccountReceivable
                    .AccountReceivableId = itemXpo.AccountReceivableId
                    .InvoiceNumber = itemXpo.InvoiceNumber
                    .Days = itemXpo.Days
                    .AgesId = itemXpo.AgesId
                    .AgesDescription = itemXpo.Range
                    .FacturerValue = itemXpo.FacturerValue
                    .BalanceAccountReceivable = itemXpo.BalanceAccountReceivable
                    .ValueGlosado = itemXpo.ValueGlosado
                    .BalanceWithGlosa = .BalanceAccountReceivable - .ValueGlosado
                    .Expectative = itemXpo.Expectative
                    .Percentage = itemXpo.Percentage
                    .NetPresentValue = itemXpo.NetPresentValue
                    .Value = itemXpo.Value
                    .AccumulatedDeterioration = itemXpo.AccumulatedDeterioration
                    If Process = 2 Then
                        .DeteriorationCalculated = .Value - .AccumulatedDeterioration
                    End If

                    .RegimenName = itemXpo.RegimenName
                    .ThirdPartyNitName = itemXpo.ThirdPartyNitName
                    .LegalBookId = itemXpo.LegalBookId
                    .TypeBook = itemXpo.TypeBook
                    .PortfolioDeteriorationClassificationId = itemXpo.PortfolioDeteriorationClassificationId
                    .PortfolioClassification = itemXpo.PortfolioClassification
                End With
                ListPortfolioProvisionDetail.Add(PortfolioProvisionDetail)
            Next
            INDgcBill.DataSource = Nothing
            INDgcBill.DataSource = ListPortfolioProvisionDetail
            VisibleCheck()
            INDviewBill.ExpandAllGroups()
        End If
    End Sub

    ''' <summary>
    ''' Transforma los detalles guardados de PortfolioProvisionDetail a PortfolioDeteriorationByClassificationDTO
    ''' </summary>
    ''' <returns>Lista de PortfolioDeteriorationByClassificationDTO</returns>
    Private Function TransformDetailsToClassificationDTO() As List(Of PortfolioDeteriorationByClassificationDTO)
        Dim result As New List(Of PortfolioDeteriorationByClassificationDTO)

        If ListPortfolioProvisionDetail IsNot Nothing AndAlso ListPortfolioProvisionDetail.Count > 0 Then
            For Each detail As PortfolioProvisionDetail In ListPortfolioProvisionDetail
                Dim dto As New PortfolioDeteriorationByClassificationDTO With {
                    .Code = 0,
                    .Message = Nothing,
                    .AccountReceivableId = detail.AccountReceivableId,
                    .InvoiceNumber = detail.InvoiceNumber,
                    .AccountReceivableDate = detail.ConfirmDateAccountReceivable,
                    .RadicatedDate = detail.ConfirmDateAccountReceivable,
                    .Days = detail.Days,
                    .AgesDescription = detail.AgesDescription,
                    .DocumentValue = detail.FacturerValue,
                    .Balance = detail.BalanceAccountReceivable,
                    .ValueGlosado = detail.ValueGlosado,
                    .Percentage = detail.Percentage,
                    .Value = detail.Value,
                    .AccumulatedDeterioration = detail.AccumulatedDeterioration,
                    .RegimenName = detail.RegimenName,
                    .ThirdPartyNitName = detail.ThirdPartyNitName,
                    .TypeBook = If(detail.TypeBook, 1),
                    .PortfolioClassification = detail.PortfolioClassification,
                    .LegalBookId = detail.LegalBookId,
                    .PortfolioClassificationId = detail.PortfolioDeteriorationClassificationId,
                    .AgesId = detail.AgesId
                }
                result.Add(dto)
            Next
        End If

        Return result
    End Function

    ''' <summary>
    ''' Crea los detalles de acuerdo a la información del deterioro por clasificación
    ''' </summary>
    ''' <param name="data"></param>
    Private Sub CreateDetailWithPortfolioDeteriorationByClassificationData(data As List(Of PortfolioDeteriorationByClassificationDTO))
        If data Is Nothing OrElse data.Count = 0 Then Return

        ' Inicializar lista ligera con capacidad pre-asignada (performance)
        _lightweightDetailsList = New List(Of PortfolioProvisionDetail)(data.Count)

        ' Crear items SIN disparar eventos, cálculos o validaciones
        For Each item In data
            Dim detail As New PortfolioProvisionDetail With {
                .SelectOption = True,
                .ConfirmDateAccountReceivable = If(item.RadicatedDate, item.DocumentDate),
                .AccountReceivableId = item.AccountReceivableId,
                .InvoiceNumber = item.InvoiceNumber,
                .Days = If(item.Days, 0),
                .AgesId = item.AgesId,
                .AgesDescription = item.AgesDescription,
                .FacturerValue = item.DocumentValue,
                .BalanceAccountReceivable = item.Balance,
                .ValueGlosado = item.ValueGlosado,
                .BalanceWithGlosa = item.Balance - item.ValueGlosado,
                .Percentage = item.Percentage,
                .Value = item.Value,
                .AccumulatedDeterioration = item.AccumulatedDeterioration,
                .DeteriorationCalculated = If(Process = 2, item.Value - item.AccumulatedDeterioration, 0),
                .RegimenName = item.RegimenName,
                .ThirdPartyNitName = item.ThirdPartyNitName,
                .LegalBookId = item.LegalBookId,
                .PortfolioDeteriorationClassificationId = item.PortfolioClassificationId
            }
            _lightweightDetailsList.Add(detail)
        Next

        ' Asignación directa sin copia (referenciar la misma lista)
        ListPortfolioProvisionDetail = _lightweightDetailsList
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _portfolioProvision
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .OperatingUnitId = BarraBotones.OperatingUnit.Id
            .Code = Code
            .DocumentDate = DocumentDate
            .CourtDate = CourtDate
            .DocumentType = Process
            .ApplyDeterioration = IIf(Process = 1, Nothing, ApplyDeterioration)
            .Description = Description

            ' OPTIMIZACIÓN AGRESIVA: NO tocar colección para ApplyDeterioration = 3
            ' Los detalles se procesarán directamente en SavePortfolioProvisionDetail
            If ApplyDeterioration <> 3 Then
                ' Comportamiento original solo para ApplyDeterioration = 1 y 2
                .PortfolioProvisionDetail.Clear()
                If ListPortfolioProvisionDetail IsNot Nothing AndAlso ListPortfolioProvisionDetail.Count > 0 Then
                    ListPortfolioProvisionDetail.FindAll(Function(item) item.SelectOption = True).ForEach(Sub(item)
                                                                                                              .PortfolioProvisionDetail.Add(item)
                                                                                                          End Sub)
                End If
            End If
            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._portfolioProvision.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me._portfolioProvision.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._portfolioProvision.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._portfolioProvision.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._portfolioProvision.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Obtiene los childsRowHandles
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="groupRowHandle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetChildRowsHandles(view As GridView, groupRowHandle As Integer) As Integer
        Dim childRows As Integer = 0
        If Not view.IsGroupRow(groupRowHandle) Then
            childRows = 1
            Return childRows
        End If
        Return childRows
    End Function

    Private Sub EnableFilterInvoicesParameter()
        If Process = 2 AndAlso (ApplyDeterioration = 1 OrElse ApplyDeterioration = 2) Then
            INDgcBill.BeginUpdate()

            colRegimen.Visible = True
            colThirdParty.Visible = True

            INDcolSelect.VisibleIndex = 0
            colVigence.VisibleIndex = 1
            colDate.VisibleIndex = 2
            colInvoice.VisibleIndex = 3
            colAge.VisibleIndex = 4
            colValorFacturado.VisibleIndex = 5
            colBalance.VisibleIndex = 6
            colValorGlosado.VisibleIndex = IIf(INDsleApplyDeterioration.EditValue = 2, 7, -1)
            colBalanceWithGlosa.VisibleIndex = IIf(INDsleApplyDeterioration.EditValue = 2, 8, -1)
            colExpectative.VisibleIndex = 9
            colPercentage.VisibleIndex = 10
            colPresentValue.VisibleIndex = 11
            colValue.VisibleIndex = 12
            colAccumulatedDeterioration.VisibleIndex = 13
            colDeteriorationCalculated.VisibleIndex = 14

            colAge.GroupIndex = -1
            colRegimen.GroupIndex = 0
            colThirdParty.GroupIndex = 1

            INDgcBill.EndUpdate()

            If ListPortfolioProvisionDetail IsNot Nothing Then
                ListPortfolioProvisionDetail.ForEach(Sub(o)
                                                         o.Percentage = ctrDeterioro.TasaVPN
                                                         o.Expectative = ctrDeterioro.Expectative
                                                         o.CalculateDeterioration()
                                                     End Sub)
            End If
            INDgcBill.RefreshDataSource()
            ctrDeterioro.RestoreToDefaultState()
            ctrDeterioro.Show()
            Me.BarraBotones.StatusRecordVisible = True
        Else
            If ApplyDeterioration = 3 Then
                ctrDeterioro.ChangeToDeteriorationByClassification(CourtDate)
                ctrDeterioro.Show()
            Else
                ctrDeterioro.RestoreToDefaultState()
                ctrDeterioro.Hide()
            End If
            INDgcBill.BeginUpdate()

            colRegimen.Visible = False
            colThirdParty.Visible = False

            colVigence.Visible = False
            colDate.Visible = True
            colInvoice.Visible = True
            colAge.Visible = True
            colValorFacturado.Visible = False
            colBalance.Visible = True
            colValorGlosado.Visible = False
            colBalanceWithGlosa.Visible = False
            colExpectative.Visible = False
            colPercentage.Visible = True
            colPresentValue.Visible = False
            colValue.Visible = True
            colAccumulatedDeterioration.Visible = False
            colDeteriorationCalculated.Visible = False

            colAge.GroupIndex = 0
            colRegimen.GroupIndex = -1
            colThirdParty.GroupIndex = -1

            INDgcBill.EndUpdate()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VisibleCheck()
        If ListPortfolioProvisionDetail IsNot Nothing AndAlso ListPortfolioProvisionDetail.Count > 0 Then
            If ListPortfolioProvisionDetail.Where(Function(item) item.SelectOption = True).Count = ListPortfolioProvisionDetail.Count Then
                Me.INDcolSelect.Image = Global.Presentation.Portfolio.My.Resources.Resources.check
            Else
                Me.INDcolSelect.Image = Global.Presentation.Portfolio.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

#Region "Methods Grid"

    ''' <summary>
    ''' Agrega una factura a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInvoices()
        If bwLoadBills.IsBusy = True Then
            Mensaje(EeventViewerImages.Advertencia) = "Existe un proceso que no ha terminado por favor espere"
            Exit Sub
        End If
        If INDsleProcess.EditValue = 2 AndAlso ApplyDeterioration Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe indicar a que se aplicará el deterioro (Cartera o Glosa)"
            Exit Sub
        End If
        If TabbedControlGroup1.SelectedTabPageIndex = 0 Then 'Si el tab esta en el de facturas
            'Se valida que existan edades de cartera
            If ListAgesPorfolio Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No existen edades de cartera para la unidad operativa escogida"
                Exit Sub
            End If
            'Se valida que hayan escogido una factura
            If InvoiceCode Is Nothing OrElse InvoiceCode = String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una factura"
                INDSleInvoiceNumber.Focus()
                Exit Sub
            End If

            'Obtenemos la factura seleccionada
            Dim entity = _presenter.GeViewAccountReceivableByPortfolioProvisionById(INDSleInvoiceNumber.EditValue)
            If entity Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No existen la factura seleccionada"
                Exit Sub
            End If

            If ListPortfolioProvisionDetail Is Nothing Then 'Se verifica si el listado de la rejilla esta vacío
                ListPortfolioProvisionDetail = New List(Of PortfolioProvisionDetail)
            Else 'Si no esta vacío se valida que la factura escogida no este ya en el listado
                If (From x In ListPortfolioProvisionDetail Where x.AccountReceivableId = entity.Id Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "La factura seleccionada " + entity.InvoiceNumber + " ya existe en la lista"
                    Exit Sub
                End If
            End If

            Dim diff = DateDiff(DateInterval.Day, entity.DocumentDate, CourtDate.Value)
            Dim age = (From a In ListAgesPorfolio Where a.InitialRange <= diff AndAlso diff <= a.EndRange Select a).FirstOrDefault
            If age Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "La diferencia en días de la factura seleccionada no existe en el rango de edades de cartera"
                Exit Sub
            Else
                If age.EndRange = Integer.MaxValue Then
                    entity.AgesId = Nothing
                Else
                    entity.AgesId = age.Id
                End If
                entity.AgesDescription = age.Name
                entity.PercentageProvision = age.ProvisionPercentage
                entity.PercentageDeterioration = age.DeteriorationPercentage
            End If

            'Se crea el nuevo objeto para agregarlo al listado
            Dim portfolioProvisionDetail As New PortfolioProvisionDetail
            With portfolioProvisionDetail
                .ConfirmDateAccountReceivable = entity.DocumentDate
                .AccountReceivableId = entity.Id
                .InvoiceNumber = entity.InvoiceNumber
                .AgesId = entity.AgesId
                .AgesDescription = entity.AgesDescription
                .FacturerValue = entity.Value
                .BalanceAccountReceivable = entity.Balance

                If INDsleProcess.EditValue = 2 AndAlso (INDsleApplyDeterioration.EditValue = 1 OrElse INDsleApplyDeterioration.EditValue = 2) Then 'Deterioro
                    .ValueGlosado = IIf(INDsleApplyDeterioration.EditValue = 2, entity.BalanceGlosa, 0)
                    .BalanceWithGlosa = .BalanceAccountReceivable - .ValueGlosado
                    .Expectative = IIf(ctrDeterioro.Expectative = 0, entity.Expectative, ctrDeterioro.Expectative)
                    .Percentage = IIf(ctrDeterioro.TasaVPN = 0, entity.PercentageDeterioration, ctrDeterioro.TasaVPN)
                    .AccumulatedDeterioration = entity.DeteriorationBalance
                    .CalculateDeterioration()
                Else 'Provision
                    .Percentage = entity.PercentageProvision
                    .NetPresentValue = 0
                    .Expectative = 0
                    .Value = Math.Round(.BalanceAccountReceivable * .Percentage / 100, 2)
                End If

                .RegimenName = entity.RegimenName
                .ThirdPartyNitName = entity.ThirdPartyDescription
            End With

            'Se agrega el objeto al listado
            ListPortfolioProvisionDetail.Add(portfolioProvisionDetail)
            INDgcBill.DataSource = Nothing
            INDgcBill.DataSource = ListPortfolioProvisionDetail
            VisibleCheck()
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            InvoiceCode = Nothing
            INDviewBill.ExpandAllGroups()
            INDSleInvoiceNumber.Focus()
        Else 'Si el tab está en el de edades
            'Se valida que haya escogido una edad
            If AgesId Is Nothing OrElse PortfolioAgesPortfolio Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una edad"
                INDsleAges.Focus()
                Exit Sub
            End If

            Try
                INDpceBill.ClosePopup()
                AsyncLoader(True)
                'Inicia el backGroundWorker
                bwLoadBills.RunWorkerAsync()
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try

        End If
    End Sub

    ''' <summary>
    ''' Metodo que selecciona todo el grupo o todo el subGrupo
    ''' optionCheck = 0 quitar seleccion,
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptions(optionCheck As Integer)
        Dim view As GridView = INDviewBill
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), 1, optionCheck)
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    row.SelectOption = optionCheck

                    If optionCheck = 0 AndAlso row.Id > 0 Then
                        If ListDeletePortfolioProvisionDetail Is Nothing Then
                            ListDeletePortfolioProvisionDetail = New List(Of Integer)
                        End If

                        If Not ListDeletePortfolioProvisionDetail.Any(Function(detailId) detailId = row.Id) Then
                            ListDeletePortfolioProvisionDetail.Add(row.Id)
                        End If
                    End If
                End If
            Next
            VisibleCheck()
            INDgcBill.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Elimina un detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If MessageIndigo.Show("Desea eliminar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        ListTmpPortfolioProvisionDetailDelete = New List(Of PortfolioProvisionDetail)
        If ListDeletePortfolioProvisionDetail Is Nothing Then
            ListDeletePortfolioProvisionDetail = New List(Of Integer)
        End If

        Dim view As GridView = INDviewBill
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), 2, 0)

                    For Each detail In ListTmpPortfolioProvisionDetailDelete
                        ListPortfolioProvisionDetail.Remove(detail)
                    Next
                Else
                    Dim row = view.GetRow(listHandlesSelected(i))
                    If row.Id > 0 Then
                        ListDeletePortfolioProvisionDetail.Add(row.Id)
                    End If
                    ListPortfolioProvisionDetail.Remove(row)
                End If
            Next
        End If

        INDgcBill.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Cambiar la expectativa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnChangeExpectative(value As Integer)
        Dim view As GridView = INDviewBill
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), 3, value)
                Else
                    Dim row As PortfolioProvisionDetail = view.GetRow(listHandlesSelected(i))
                    With row
                        .Expectative = value
                        .CalculateDeterioration()
                    End With
                End If
            Next
        End If

        INDgcBill.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Cambia el porcentaje
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnChangeVPNRate(value As Decimal)
        Dim view As GridView = INDviewBill
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                If view.IsGroupRow(listHandlesSelected(i)) Then
                    GetChildsRows(view, listHandlesSelected(i), 4, value)
                Else
                    Dim row As PortfolioProvisionDetail = view.GetRow(listHandlesSelected(i))
                    With row
                        .Percentage = value
                        .CalculateDeterioration()
                    End With
                End If
            Next
        End If

        INDgcBill.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Obtiene la informacion de las filas de la rejilla
    ''' Dependiendo de la opcion actualiza estados de la rejilla o las adiciona en la lista para la eliminacion
    ''' options:
    '''     1 - Seleccionar / Deseleccionar
    '''     2 - Eliminar
    '''     3 - Actualizar VPN
    '''     4 - Actualizar Rate
    ''' optionCheck = 1 poner seleccion
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetChildsRows(view As GridView, groupRowHandle As Integer, options As Integer, value As Decimal)
        Dim childCount As Integer = view.GetChildRowCount(groupRowHandle)
        For i As Integer = 0 To childCount - 1
            Dim childHandle As Integer = view.GetChildRowHandle(groupRowHandle, i)
            If view.IsGroupRow(childHandle) Then
                GetChildsRows(view, childHandle, options, value)
            Else
                Dim row As PortfolioProvisionDetail = view.GetRow(childHandle)
                If row IsNot Nothing Then
                    If options = 1 Then 'Check or Uncheck
                        If value = 0 Then
                            row.SelectOption = False
                            If row.Id > 0 Then
                                ListDeletePortfolioProvisionDetail.Add(row.Id)
                            End If
                        Else
                            If ListDeletePortfolioProvisionDetail IsNot Nothing AndAlso ListDeletePortfolioProvisionDetail.Any(Function(Id) Id = row.Id) Then
                                ListDeletePortfolioProvisionDetail.Remove(row.Id)
                            End If
                            row.SelectOption = True
                        End If
                    ElseIf options = 2 Then 'Delete
                        If row.Id > 0 Then
                            ListDeletePortfolioProvisionDetail.Add(row.Id)
                        End If
                        ListTmpPortfolioProvisionDetailDelete.Add(row)
                    ElseIf options = 3 OrElse options = 4 Then 'Update calculated values
                        With row
                            .Expectative = If(options = 3, value, .Expectative)
                            .Percentage = If(options = 4, value, .Percentage)
                            .CalculateDeterioration()
                        End With
                    End If
                End If
            End If
        Next
    End Sub

#End Region

#End Region

#Region "Workers"

#Region "Add Bills"

    ''' <summary>
    ''' Asyncrono para consultar las facturas que estan en el rango de edades escogido por el usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private bwLoadBills As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Inicia el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwLoadBills_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        INDviewBill.ShowLoadingPanel()
        ListViewAccountReceivableByPortfolioProvisionXpo = Nothing
        If INDsleProcess.EditValue IsNot Nothing AndAlso INDsleProcess.EditValue = 2 AndAlso INDsleApplyDeterioration.EditValue = 2 Then 'Si es deterioro y aplica a glosa
            ListViewAccountReceivableByPortfolioProvisionXpo = _presenter.GetInvoiceByInitialRangeAndEndRangeGlosa(CourtDate, PortfolioAgesPortfolio.InitialRange, PortfolioAgesPortfolio.EndRange)
        ElseIf INDsleProcess.EditValue IsNot Nothing AndAlso INDsleProcess.EditValue = 2 AndAlso INDsleApplyDeterioration.EditValue = 1 Then 'Si es deterioro y aplica a cxc
            ListViewAccountReceivableByPortfolioProvisionXpo = _presenter.GetInvoiceByInitialRangeAndEndRangeNotGlosa(CourtDate, PortfolioAgesPortfolio.InitialRange, PortfolioAgesPortfolio.EndRange)
        Else 'Si es provision
            ListViewAccountReceivableByPortfolioProvisionXpo = _presenter.GetInvoiceByInitialRangeAndEndRange(CourtDate, PortfolioAgesPortfolio.InitialRange, PortfolioAgesPortfolio.EndRange)
        End If
        LoadItemsRanges()
    End Sub

    ''' <summary>
    ''' Convierte los items que vienen desde el xpo a entidad para asignarlos a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadItemsRanges()
        If ListViewAccountReceivableByPortfolioProvisionXpo IsNot Nothing AndAlso ListViewAccountReceivableByPortfolioProvisionXpo.Count > 0 Then 'Si se encontró registros
            'Si el listado de la rejilla es nulo 
            If ListPortfolioProvisionDetail Is Nothing Then
                ListPortfolioProvisionDetail = New List(Of PortfolioProvisionDetail)
            End If

            'Se instancia el listado de mensajes
            ListMessage = New List(Of Tuple(Of String, Integer))

            'Se recorre el listado consultado anteriormente para armar los objetos que van en la rejilla
            For Each item In ListViewAccountReceivableByPortfolioProvisionXpo
                'Se valida que la factura que se va recorriendo no esté en la rejilla
                If (From x In ListPortfolioProvisionDetail Where x.AccountReceivableId = item.Id Select x).Count > 0 Then
                    ListMessage.Add(New Tuple(Of String, Integer)("La factura " + item.InvoiceNumber + " ya existe en la lista", 2))
                    Continue For
                End If

                'Se crea el nuevo objeto para agregarlo al listado
                Dim portfolioProvisionDetail As New PortfolioProvisionDetail
                With portfolioProvisionDetail
                    .SelectOption = INDcheckSelect.Checked
                    .ConfirmDateAccountReceivable = item.DocumentDate
                    .AccountReceivableId = item.Id
                    .InvoiceNumber = item.InvoiceNumber
                    .Days = DateDiff(DateInterval.Day, item.DocumentDate, CourtDate.Value)
                    If PortfolioAgesPortfolio.EndRange = Integer.MaxValue Then
                        .AgesId = Nothing
                    Else
                        .AgesId = PortfolioAgesPortfolio.Id
                    End If
                    .AgesDescription = PortfolioAgesPortfolio.Name

                    If Process = 2 Then 'Deterioro
                        .FacturerValue = item.Value
                        .BalanceAccountReceivable = item.Balance
                        .ValueGlosado = IIf(ApplyDeterioration = 2, item.BalanceGlosa, 0)
                        .BalanceWithGlosa = .BalanceAccountReceivable - .ValueGlosado
                        .Expectative = IIf(ctrDeterioro.Expectative = 0, item.Expectative, ctrDeterioro.Expectative)
                        .Percentage = ctrDeterioro.TasaVPN
                        .AccumulatedDeterioration = item.DeteriorationBalance
                        .CalculateDeterioration()
                    Else 'Provisión
                        .BalanceAccountReceivable = item.Balance
                        .Expectative = 0
                        .Percentage = PortfolioAgesPortfolio.ProvisionPercentage
                        .NetPresentValue = 0
                        .Value = Math.Round(.BalanceAccountReceivable * .Percentage / 100, 0, MidpointRounding.AwayFromZero)
                    End If

                    .RegimenName = item.RegimenName
                    .ThirdPartyNitName = item.ThirdPartyDescription
                End With

                ListPortfolioProvisionDetail.Add(portfolioProvisionDetail)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwLoadBills_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If ListViewAccountReceivableByPortfolioProvisionXpo Is Nothing OrElse ListViewAccountReceivableByPortfolioProvisionXpo.Count = 0 Then 'Se valida que haya regsitros
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró información con el rango seleccionado"
            INDviewBill.HideLoadingPanel()
            AsyncLoader(False)
            Exit Sub
        End If

        AsyncLoader(False)
        'Se asigna el listado conformado para pegar a la rejilla
        INDgcBill.DataSource = Nothing
        INDgcBill.DataSource = ListPortfolioProvisionDetail
        INDviewBill.HideLoadingPanel()
        AgesId = Nothing
        INDcheckSelect.CheckState = CheckState.Unchecked
        INDviewBill.ExpandAllGroups()
        VisibleCheck()
        INDsleAges.Focus()

        'Se valida si vienen errores para sacar el form de errores
        If ListMessage IsNot Nothing AndAlso ListMessage.Count > 0 Then
            Using Formulario As New FrmListErrors(ListMessage)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.Width = 920
                Formulario.Height = 600
                Dim frm As New FrmTransparent(Formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                frm.ShowDialog(Me)
            End Using
        End If

    End Sub

#End Region

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

    Private Async Function SavePortfolioProvisionDetail() As Task
        Try
            AsyncLoader(True)

            Dim errors As New StringBuilder()
            Dim Status = Me._portfolioProvision.Status

            If {1, 2}.Contains(Status) Then
                progress = New CtrProgress
                progress.SetInfoFunction(AddressOf getInfo)
                progress.PrintInfo()
                progress.Dock = DockStyle.Fill
                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Clear())
                AdditionalControlPanel.SafeInvoke(Sub(x) x.Controls.Add(progress))

                Me._portfolioProvision.Status = 1

                ' OPTIMIZACIÓN: para ApplyDeterioration = 3, preparar items con FK directamente
                Dim ListPortfolioProvisionDetailToSend As List(Of PortfolioProvisionDetail)

                If ApplyDeterioration = 3 Then
                    ' Bypass: trabajar con items seleccionados SIN manipular colección padre
                    ListPortfolioProvisionDetailToSend = ListPortfolioProvisionDetail?.Where(Function(x) x.SelectOption).ToList()
                Else
                    ' Comportamiento original para ApplyDeterioration 1 y 2
                    ListPortfolioProvisionDetailToSend = _portfolioProvision.PortfolioProvisionDetail.ToList()
                End If

                totalProcessedItems = 0
                totalItems = If(ListPortfolioProvisionDetailToSend?.Count, 0)
                If (totalItems > 0) Then
                    indexSend = 0
                    progress.SafeInvoke(Sub(x)
                                            x.SetTitle = "Registros Guardados"
                                            x.PrintInfo()
                                        End Sub)

                    While ListPortfolioProvisionDetailToSend.Count > 0
                        ' OPTIMIZACIÓN: Calcular tamaño de bloque dinámico según ApplyDeterioration
                        ' Para ApplyDeterioration = 3: usar bloques más grandes (500) para mejor rendimiento
                        ' Para ApplyDeterioration = 1 y 2: mantener tamaño conservador (100)
                        Dim currentBatchSize As Integer = If(ApplyDeterioration = 3, 500, itemsSend)

                        Dim objLock As New Object()
                        'agregamos los items a la cabecera
                        Dim listToSend = ListPortfolioProvisionDetailToSend.Take(currentBatchSize).ToList()
                        _portfolioProvision.PortfolioProvisionDetail.Clear()

                        ' Para ApplyDeterioration = 3: asignar FK directamente sin eventos
                        If ApplyDeterioration = 3 Then
                            For Each item In listToSend
                                ' Asignar FK directamente (evita eventos de colección)
                                item.PortfolioProvisionId = If(_portfolioProvision.Id > 0, _portfolioProvision.Id, 0)
                                _portfolioProvision.PortfolioProvisionDetail.Add(item)
                            Next
                        Else
                            ' Comportamiento original
                            Parallel.ForEach(listToSend, Sub(x)
                                                             SyncLock objLock
                                                                 _portfolioProvision.PortfolioProvisionDetail.Add(x)
                                                             End SyncLock
                                                         End Sub)
                        End If

                        Dim quantityDetailsToProcess = If(ListPortfolioProvisionDetailToSend.Count < currentBatchSize, ListPortfolioProvisionDetailToSend.Count, currentBatchSize)
                        indexSend = totalProcessedItems + 1
                        totalProcessedItems += quantityDetailsToProcess

                        Using model As New MPortfolioProvision(Me.Tag.ToString())
                            Dim Result = Await model.SavePortfolioProvision(_portfolioProvision, ListDeletePortfolioProvisionDetail)
                            If Result.StateResult Then
                                Me.Code = Result.ObjectEmbbeded.Code
                                Me._portfolioProvision = Result.ObjectEmbbeded
                            Else
                                errors.AppendLine("Los items del " + (indexSend).ToString() + " hasta " + (totalProcessedItems).ToString() + " no se pudieron guardar porque:" + vbNewLine + Result.Message)
                            End If
                        End Using

                        If quantityDetailsToProcess > 0 Then
                            ListPortfolioProvisionDetailToSend.RemoveRange(0, quantityDetailsToProcess)
                        End If

                        progress.PrintInfo()
                    End While
                End If

                AdditionalControlPanel.Controls.Clear()
                AdditionalControlPanel.Controls.Add(ctrDeterioro)
            End If

            If errors.Length = 0 Then
                _portfolioProvision.Status = Status
                _portfolioProvision.PortfolioProvisionDetail.Clear()

                If Status = 2 Then
                    Using model As New MPortfolioProvision(Me.Tag.ToString())
                        Dim Result = Await model.ConfirmPortfolioProvision(_portfolioProvision, ListDeletePortfolioProvisionDetail, _operativeUnitId)
                        AsyncLoader(False)
                        If Result.StateResult = True Then
                            If Result.StateResultAux Then
                                Mensaje(EeventViewerImages.Informacion) = Result.Message
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = Result.Message
                            End If
                        Else
                            INDbteCode.Enabled = False
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                            Exit Function
                        End If
                    End Using
                ElseIf Status = 3 Then
                    Using model As New MPortfolioProvision(Me.Tag.ToString())
                        Dim Result = Await model.SavePortfolioProvision(_portfolioProvision, ListDeletePortfolioProvisionDetail)
                        AsyncLoader(False)
                        If Result.StateResult = True Then
                            _portfolioProvision = Result.ObjectEmbbeded
                            Mensaje(EeventViewerImages.Informacion) = String.Format("Se anuló correctamente el registro con código {0}", _portfolioProvision.Code)
                        Else
                            INDbteCode.Enabled = False
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                            Exit Function
                        End If
                    End Using
                Else
                    Mensaje(EeventViewerImages.Informacion) = String.Format("Se guardó correctamente el registro con código {0}", _portfolioProvision.Code)
                    AsyncLoader(False)
                End If

                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                Select Case _varImp
                    Case 1
                        Me.BarraBotones.PrintReport(PrintReportAction.Create, _portfolioProvision.Id, 0, _portfolioProvision.Id)
                    Case 2
                        Me.BarraBotones.PrintReport(PrintReportAction.Update, _portfolioProvision.Id, 0, _portfolioProvision.Id)
                    Case 3
                        Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _portfolioProvision.Id, 0, _portfolioProvision.Id)
                    Case 4
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _portfolioProvision.Id, 0, _portfolioProvision.Id)
                End Select

                Me.Deshacer()
            Else
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            End If
        Catch ex As Exception
            AsyncLoader(False)
        End Try
    End Function

#End Region

#End Region

End Class