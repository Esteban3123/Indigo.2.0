#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Portfolio.MVP

#End Region

''' <summary>
''' Vista del frontal de conciliación
''' </summary>
Public Class FrmPortfolioConciliation
    Implements IPortfolioConciliation

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

#End Region

#Region "Globals"

    ''' <summary>
    ''' presentador
    ''' </summary>
    Dim Presenter As PPortfolioConciliation

    ''' <summary>
    ''' Modelo
    ''' </summary>
    Dim Model As MPortfolioConciliation

    ''' <summary>
    ''' Entidad de la tabla PortfolioConciliation
    ''' </summary>
    Dim _portfolioConciliation As PortfolioConciliation

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPortfolio

    ''' <summary>
    ''' contiene el tercero
    ''' </summary>
    Dim _customer As Customer

    ''' <summary>
    ''' contiene el detalle
    ''' </summary>
    Dim PortfolioConciliationDetail As PortfolioConciliationDetail

    ''' <summary>
    ''' entidad de la tabla de participantes
    ''' </summary>
    Dim _portfolioParticipants As PortfolioConciliationParticipants

    ''' <summary>
    ''' Almacena el Id del customer
    ''' </summary>
    Dim CustomerId As Integer

    ''' <summary>
    ''' Almacena el Id del customer
    ''' </summary>
    Dim ThirdPartyId As Integer

    ''' <summary>
    ''' Almacena el listado de facturas de accountReceivablexpo
    ''' </summary>
    Dim _actionResult As List(Of PortfolioAccountReceivableXpo)

    ''' <summary>
    ''' Almacena un registro en especifico de AccountReceivablexpo
    ''' </summary>
    Dim _actionResultDetail As PortfolioAccountReceivableXpo

    ''' <summary>
    ''' Almacena el Id de la factura
    ''' </summary>
    Dim InvoiceId As Integer

    ''' <summary>
    ''' Almacena el numero de la factura
    ''' </summary>
    Dim InvoiceNumber As String

    ''' <summary>
    ''' bandera
    ''' </summary>
    Dim _isLoaded As Boolean

    ''' <summary>
    ''' bandera para solo lectura
    ''' </summary>
    Dim OnlyRead As Boolean = False

    ''' <summary>
    ''' Almacena el Id de la conciliacion
    ''' </summary>
    Dim _conciliationId As Integer

#End Region

#Region "Fields"

    ''' <summary>
    ''' Almacena el nombre del control que hizo el llamado al metodo buscar
    ''' </summary>
    Private _openFindSenser As String

    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceCustomers As XPInstantFeedbackSource

    ''' <summary>
    ''' Objeto openFileDialog
    ''' </summary>
    Private fileOpener As New OpenFileDialog

    ''' <summary>
    ''' ruta excel a cargar
    ''' </summary>
    ''' <remarks></remarks>
    Private rutaExcel As String

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Parametros de Portfolio
    ''' </summary>
    Private _parameterPortfolio As TimeParameters

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el numero consecutivo de la conciliacion
    ''' </summary>
    ''' <value>Numero de la conciliacion</value>
    ''' <returns>El numero de la conciliacion</returns>
    Public Property Consecutive As String Implements IPortfolioConciliation.Consecutive
        Get
            If (Me.INDbteConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteConsecutive.EditValue
            End If

        End Get
        Set(value As String)
            Me.INDbteConsecutive.EditValue = value
        End Set

    End Property

    ''' <summary>
    ''' Obtiene o asigna el nit de la entidad
    ''' </summary>
    ''' <value>Nit de la entidad</value>
    ''' <returns>Nit de la entidad</returns>
    Public Property ThirdPartyNit As Integer Implements IPortfolioConciliation.ThirdPartyNit
        Get
            Return Me.INDbteNit.EditValue
        End Get
        Set(value As Integer)
            Me.INDbteNit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nit de la entidad
    ''' </summary>
    ''' <value>Nit de la entidad</value>
    ''' <returns>Nit de la entidad</returns>
    Public Property ThirdPartyNitName As String Implements IPortfolioConciliation.ThirdPartyNitName
        Get
            If (Me.INDbteNit.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteNit.Text
            End If
        End Get
        Set(value As String)
            Me.INDbteNit.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el codigo o numero del oficio
    ''' </summary>
    ''' <value>Codigo o numero del oficio</value>
    ''' <returns>Codigo o numero del oficio</returns>
    Public Property Document As String Implements IPortfolioConciliation.DocumentNumber
        Get
            Return Me.INDtxtDocument.Text.Trim()
        End Get
        Set(value As String)
            Me.INDtxtDocument.Text = value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fecha de conciliacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property DatePortfolioConciliation As DateTime Implements IPortfolioConciliation.ConciliationDate
        Get
            Return Convert.ToDateTime(Me.INDtxtToday.Tag)
        End Get
        Set(value As Date)
            Me.INDtxtToday.Tag = value
            Me.INDtxtToday.Text = value.ToString("D", indigo.Culture)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fecha del oficio
    ''' </summary>
    ''' <value>Fecha del oficio</value>
    ''' <returns>Fecha del oficio</returns>
    Public Property DateDocument As DateTime Implements IPortfolioConciliation.DocumentDate
        Get
            Return Convert.ToDateTime(Me.INDdetDateDocument.EditValue)
        End Get
        Set(value As Date)
            Me.INDdetDateDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el comentario de observacion de la conciliacion
    ''' </summary>
    ''' <value>Comentario de observacion</value>
    ''' <returns>Comentario de observacion</returns>
    Public Property Comment As String Implements IPortfolioConciliation.Comment
        Get
            Return Me.INDmemComment.Text.Trim()
        End Get
        Set(value As String)
            Me.INDmemComment.Text = value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Byte Implements IPortfolioConciliation.State
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene la unidad operativa.
    ''' </summary>
    ''' <returns></returns>
    Public Property OperatingUnitId As Integer
        Get
            Return Me._idOperativeUnit
        End Get
        Set(value As Integer)
            Me._idOperativeUnit = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As PortfolioSequence

    Public Property Sequense As PortfolioSequence Implements IPortfolioConciliation.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fecha de corte
    ''' </summary>
    ''' <value>Fecha del oficio</value>
    ''' <returns>Fecha del oficio</returns>
    Public Property ClosingDate As DateTime Implements IPortfolioConciliation.ClosingDate
        Get
            Return Convert.ToDateTime(Me.INDClosingDate.EditValue)
        End Get
        Set(value As Date)
            Me.INDClosingDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IPortfolioConciliation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
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
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    ''' <value>Valor que indica si se activan o inactivan</value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPortfolioConciliation.ActionsOnControls
        Set(value As Boolean)
            'Datos Principales
            Me.INDbteConsecutive.Enabled = value
            If INDbteConsecutive.EditValue Is String.Empty Then
                Me.INDbteNit.Enabled = Not value
            Else
                Me.INDbteNit.Enabled = value
            End If
            Me.INDtxtDocument.Enabled = Not value
            Me.INDtxtDocument.Properties.ReadOnly = value
            Me.INDpceComment.Enabled = Not value
            Me.INDmemComment.Enabled = Not value
            Me.INDmemComment.Properties.ReadOnly = value
            Me.INDdetDateDocument.Enabled = Not value
            Me.INDdetDateDocument.Properties.ReadOnly = value
            Me.INDClosingDate.Enabled = Not value
            Me.INDClosingDate.ReadOnly = value
            Me.INDpceParticipants.Enabled = Not value
            Me.INDbtnAddParticipant.Enabled = Not value
            Me.INDtxtParticipantName.Enabled = Not value
            Me.INDtxtParticipantPosition.Enabled = Not value
            Me.INDrdgParticipantType.Enabled = Not value
            Me.INDsleSearchInvoice.Enabled = Not value
            Me.INDbtnAddInvoice.Enabled = Not value
            Me.INDbtnDeleteInvoice.Enabled = Not value
            Me.INDgdcInvoices.Enabled = Not value
            Me.INDbtnLoad.Enabled = Not value
            Me.INDlycgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDlycgHeader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End Set

    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de facturas seleccionadas
    ''' </summary>
    ''' <value>Lista de facturas seleccionadas</value>
    ''' <returns>Lista de facturas seleccionadas</returns>
    Public Property SelectedInvoices As Object Implements IPortfolioConciliation.SelectedInvoices
        Get
            Return Me.INDgdcInvoices.DataSource
        End Get
        Set(value As Object)
            Me.INDgdcInvoices.DataSource = (From p In CType(value, Domain.Entities.TrackableCollection(Of Domain.Entities.PortfolioConciliationDetail)) Where p.ChangeTracker.State <> ObjectState.Deleted Select p).ToList()
            Me.INDgdcInvoices.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna los participantes
    ''' </summary>
    ''' <value>Lista de participantes</value>
    ''' <returns>La lista de participantes</returns>
    Public Property Participants As Object Implements IPortfolioConciliation.Participants
        Get
            Return Me.INDgdcParticipants.DataSource
        End Get
        Set(value As Object)
            Me.INDgdcParticipants.DataSource = (From p In CType(value, Domain.Entities.TrackableCollection(Of Domain.Entities.PortfolioConciliationParticipants)) Where p.ChangeTracker.State <> ObjectState.Deleted Select p).ToList()
            Me.INDgdcParticipants.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas Objetadas
    ''' </summary>
    Public Property DataSourceInvoicesConciliation As List(Of PortfolioConciliationDetail) Implements IPortfolioConciliation.DataSourceInvoicesConciliation
        Get
            Return Me.INDgdcInvoices.DataSource
        End Get
        Set(value As List(Of PortfolioConciliationDetail))
            Me.INDgdcInvoices.DataSource = value
        End Set
    End Property

#End Region

#Region "Constructor"

    ''' <summary>
    ''' initialization
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _openFindSenser = "INDbteConsecutive"
    End Sub

#End Region

#Region "DataSource"

    ''' <summary>
    ''' Especifica
    ''' </summary>
    ''' <remarks></remarks>
    Private _source As List(Of Tuple(Of Byte, String))

    Private ReadOnly Property ListSource As List(Of Tuple(Of Byte, String))
        Get
            If _source Is Nothing Then
                _source = New List(Of Tuple(Of Byte, String))
                _source.Add(New Tuple(Of Byte, String)(1, "Sin Radicar"))
                _source.Add(New Tuple(Of Byte, String)(2, "Radicada sin Confirmar"))
                _source.Add(New Tuple(Of Byte, String)(3, "Radicada Entidad"))
                _source.Add(New Tuple(Of Byte, String)(7, "Certificada Parcial"))
                _source.Add(New Tuple(Of Byte, String)(8, "Certificada Total"))
                _source.Add(New Tuple(Of Byte, String)(14, "Devolución Factura"))
                _source.Add(New Tuple(Of Byte, String)(15, "Cuenta de Dificil Recaudo"))
                _source.Add(New Tuple(Of Byte, String)(16, "Cobro Jurídico"))

            End If
            Return _source
        End Get
    End Property

#End Region

#Region "Load"

    ''' <summary>
    ''' Inicializa la vista del frontal de conciliacion
    ''' </summary>
    Private Async Sub FrmPortfolioConciliation_Load(sender As Object, e As EventArgs) Handles Me.Load
        '******Inicializar variables******'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PPortfolioConciliation(Me)
        Presenter.GetSequense()
        Me._isLoaded = False
        Me.Model = New MPortfolioConciliation(Me.Tag)
        Me.LoadStatus()
        Me.Deshacer()
        Me._actionResult = New List(Of PortfolioAccountReceivableXpo)
        Me._portfolioConciliation = New PortfolioConciliation()
        Me.CustomerId = Nothing
        Me.InvoiceId = Nothing
        Me.ThirdPartyId = Nothing
        Me._conciliationId = Nothing
        Me.InvoiceNumber = Nothing
        INDGlePortfolioStatusNameC.Properties.DataSource = ListSource
        INDGlePortfolioStatusNameC.EditValue = 1
        '*********************************
        Me.INDbteNit.View.OptionsView.ShowGroupPanel = False
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecordEnabled = False
        Me.IndigoGridControl1.SetHoldSize(Me.INDgdcInvoices, True)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgdcInvoices, True)
        Me.INDdetDateDocument.EditValue = Date.Today
        Me.INDbtnAddParticipant.Text = obtenerRecurso(Eresources.TextoBotonAgregarParticipante, Eform.Conciliation)
        Me._isLoaded = True
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        SearchMode = False
        Me.OperatingUnitId = Me.BarraBotones.OperatingUnitValue
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _openFindSenser = Nothing
        _isLoaded = Nothing
        _portfolioConciliation = Nothing
        _portfolioParticipants = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        DatasourceCustomers = Nothing
        fileOpener = Nothing
        rutaExcel = Nothing
        PortfolioConciliationDetail = Nothing
        SearchMode = Nothing
        CustomerId = Nothing
        ThirdPartyId = Nothing
        Me.InvoiceId = Nothing
        Me._conciliationId = Nothing
        INDsleSearchInvoice.Properties.DataSource = Nothing
        INDGcMoreInfo.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmPortfolioConciliation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteConsecutive.Focus()
    End Sub

#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        Me.AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Deja el frontal listo para una nueva busqueda o insercion de datos
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        Await Me.NewPortfolioConciliation()
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Barras the botones_ click Eliminar.
    ''' </summary>
    Public Sub Anular() Implements ICrudBase.Eliminar
        If Status <> 2 Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("MaintenanceContract_ContractConfirm", NAME_MODULE)
            Exit Sub
        End If
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.Status = 3
            Guardar()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Guarda la conciliacion
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControl() Then
        Else
            Exit Sub
        End If

        Try
            Me.AssignValues()
            Using model As New MPortfolioConciliation(Me.Tag.ToString())
                'Bandera para saber si se guardo algo e indexar
                Dim flagSaved As Boolean = False
                Me.AsyncLoader(True)
                Dim Result = Await model.SavePortfolioConciliation(_portfolioConciliation, _idCurrentSequence)
                AsyncLoader(False)

                If Result.StateResult Then

                    If _portfolioConciliation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Mensaje(EeventViewerImages.Informacion) = Result.Message
                    Me._portfolioConciliation = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteConsecutive.Enabled = False
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult.Count > 0 AndAlso Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            INDbteConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Se dispara cuando el formulario se activa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPortfolioConciliation_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteConsecutive.Enabled Then
            INDbteConsecutive.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = String.Empty, .StatusColor = System.Drawing.Color.White})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub UnblockeRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Using Model As New MBlockRecordAndSequense(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
            Me.INDbtnDeleteInvoice.Enabled = True
            Me.INDbtnAddInvoice.Enabled = True
            Me.INDsleSearchInvoice.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Valida si una cadena de caracteres contiene caracteres validos
    ''' </summary>
    ''' <param name="str">Cadena a validar</param>
    ''' <returns>Valor que indica si la cadena es valida</returns>
    Private Function IsValidString(ByVal str As String) As Boolean
        Dim pattern As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_ "
        For Each c As Char In str
            If Not pattern.Contains(c) Then
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Prepara el formulario para una nueva conciliacion
    ''' </summary>
    Private Async Function NewPortfolioConciliation() As Task
        Me._portfolioConciliation = New PortfolioConciliation()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = False
            INDbteConsecutive.Enabled = True
            INDbteNit.Enabled = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = "Secuencia no Configurada"
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Exit Function

            End If
            If Me._sequence.Sequential Then
                Me.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = False
                INDbteNit.Enabled = True
                INDClosingDate.ReadOnly = False
                INDbteNit.Focus()

                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Consecutive = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = False
                        INDbteNit.Enabled = True
                        INDbteNit.Focus()
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Consecutive = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = False
                            INDbteNit.Enabled = True
                            INDbteNit.Focus()
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = False
                    INDbteNit.Enabled = True
                    INDbteNit.Focus()
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If

        End If
    End Function

    ''' <summary>
    ''' Abre el frontal del busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Consecutivo", .FieldName = "ConciliationConsecutive"},
                                      New ColumnInfo With {.Caption = "Nit-Entidad", .FieldName = "NitName"},
                                      New ColumnInfo With {.Caption = "Numero del oficio", .FieldName = "DocumentNumber"},
                                      New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName"}}.ToList()
            .ValorSolicitado = "ConciliationConsecutive"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPortfolioConciliation
            .FormParent = Me
            .ShowSearch()
        End With

    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        UnblockeRecord()
        Consecutive = ReturnValue
        If Consecutive <> String.Empty Then
            LoadControls()

            If INDbteConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Deshace los cambios realizados
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Me.CleanControls()
        If SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlycgRoot.BeginUpdate()

        Me.UnblockeRecord()
        Me.AdditionalControlPanel.Visible = False
        Me.INDbteConsecutive.Text = String.Empty
        Me.INDbteNit.DisplayNullText = String.Empty
        Me.INDbteNit.EditValue = Nothing
        Dim d As DateTime = GetServerDate()
        Me.INDtxtToday.Tag = d
        Me.INDtxtToday.Text = d.ToString("D", indigo.Culture)
        Me.INDdetDateDocument.Text = String.Empty
        Me.INDdetDateDocument.EditValue = Date.Today
        Me.ClosingDate = Date.Today
        Me.INDtxtDocument.Text = String.Empty
        Me.INDmemComment.Text = String.Empty
        Me.INDtxtParticipantName.Text = String.Empty
        Me.INDtxtParticipantPosition.Text = String.Empty
        Me.INDbtnAddParticipant.Tag = "ADD"
        Me.INDrdgParticipantType.EditValue = Nothing
        Me.INDgdcParticipants.DataSource = Nothing
        Me.INDGcMoreInfo.DataSource = Nothing
        Me.INDsleSearchInvoice.Text = String.Empty
        Me.INDsleSearchInvoice.Properties.DataSource = Nothing
        Me.INDgdcInvoices.DataSource = New List(Of Domain.Entities.PortfolioConciliationDetail)
        Me.BarraBotones.HideOptionReport()
        Me._portfolioConciliation = New Domain.Entities.PortfolioConciliation()
        Me._doc = Nothing
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        Me.ReadOnlyControls(False)
        OnlyRead = False
        Me.ActionsOnControls = True
        Status = 0
        Me.ThirdPartyId = 0

        INDlycgRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Limpia los controles del control popUp de participantes
    ''' dejandolo listo para agregar o modificar uno nuevo
    ''' </summary>
    Public Sub CleanParticipantControls()
        Me.INDtxtParticipantName.Text = String.Empty
        Me.INDtxtParticipantPosition.Text = String.Empty
        Me.INDrdgParticipantType.EditValue = Nothing
        Me.INDtxtParticipantName.Focus()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControl()

        If INDbteConsecutive.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.MensajeError) = ("Debe seleccionar un consecutivo")
            Me.INDbteConsecutive.Focus()
            Return False
            Exit Function
        End If

        If INDbteNit.DisplayNullText Is Nothing Then
            Mensaje(EeventViewerImages.MensajeError) = ("Debe Agregar un Nit")
            Return False
            Exit Function
        End If

        If INDtxtDocument.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.MensajeError) = ("Debe Agregar un Oficio")
            Return False
            Exit Function
        End If

        If INDgdcParticipants.DataSource Is Nothing Then
            Mensaje(EeventViewerImages.MensajeError) = ("Debe agregar por lo menos un participante")
            Return False
            Exit Function
        End If
        Return True
    End Function

    ''' <summary>
    ''' Asigna los valores de los controles al objeto
    ''' </summary>
    Private Sub AssignValues()
        With _portfolioConciliation
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .ConciliationConsecutive = Consecutive
            .ThirdPartyId = ThirdPartyId
            .DocumentNumber = Document
            .ConciliationDate = DatePortfolioConciliation
            .DocumentDate = DateDocument
            .Comment = Comment
            .State = Me.Status
            .ClosingDate = Me.ClosingDate
        End With

    End Sub

    ''' <summary>
    ''' Cargar los controles con los datos del objeto Company
    ''' </summary>
    Private Async Sub LoadControls()
        If Not String.IsNullOrEmpty(Consecutive) AndAlso Not String.IsNullOrWhiteSpace(Consecutive) Then

            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Sub
            End If

            Try
                Using Model As New MPortfolioConciliation(Me.Tag)
                    AsyncLoader(True)
                    _portfolioConciliation = Await Model.GetConciliationByConsecutive(Me.INDbteConsecutive.EditValue)
                    If _portfolioConciliation IsNot Nothing AndAlso _portfolioConciliation.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_portfolioConciliation.Id))

                            _isLoaded = True
                            With _portfolioConciliation
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                _conciliationId = .Id
                                Consecutive = .ConciliationConsecutive
                                INDbteNit.DisplayNullText = .ThirdPartyName
                                ThirdPartyId = .ThirdPartyId
                                Document = .DocumentNumber
                                DateDocument = .DocumentDate
                                DatePortfolioConciliation = .ConciliationDate
                                Status = .State
                                Participants = .PortfolioConciliationParticipants
                                CustomerId = .CustomerId
                                ClosingDate = .ClosingDate

                                For Each Detail In .PortfolioConciliationDetail
                                    InvoiceNumber = Detail.DescInvoiceNumber
                                    Dim entitySp = Await Model.GetSP_PortfolioConciliation(Detail.DescInvoiceNumber, ClosingDate)
                                    Detail.DescInvoiceStateEntity = entitySp.PortfolioStatusName
                                Next
                                Me.SelectedInvoices = .PortfolioConciliationDetail

                            End With

                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._portfolioConciliation.ConciliationConsecutive)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _portfolioConciliation.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            _isLoaded = False
                            AsyncLoader(False)

                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await NewPortfolioConciliation()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ConciliacionNoExiste, Eform.Conciliation)
                            Consecutive = String.Empty
                            INDbteConsecutive.Focus()
                        End If

                    End If
                End Using
                If Status > 0 Then
                    Me.ActionsOnControls = False
                    If Status = 1 Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                        Me.ReadOnlyControls(False)
                        OnlyRead = False
                        INDbteNit.Enabled = False
                        INDClosingDate.ReadOnly = True
                    Else

                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                        Me.ReadOnlyControls(True)
                        OnlyRead = True
                        INDbteConsecutive.Enabled = False
                        INDbteNit.Enabled = False
                        INDbtnAddInvoice.Enabled = False
                        If Status = 2 Then
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, Me._portfolioConciliation.Id, 0, {Me._portfolioConciliation.Id, False, Me.BarraBotones.OperatingUnit, False})

                        Else

                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True

                        End If
                    End If
                End If
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try

        End If
    End Sub

    ''' <summary>
    ''' Habilita la edicion en el participante seleccionado en la rejilla
    ''' </summary>
    Private Sub EditParticipant()
        Me.INDbtnAddParticipant.Tag = "EDIT"
        Me.INDbtnAddParticipant.Text = obtenerRecurso(Eresources.TextoBotonModificarParticipante, Eform.Conciliation)
        Me._portfolioParticipants = CType(Me.INDgdvParticipants.GetRow(Me.INDgdvParticipants.FocusedRowHandle), Domain.Entities.PortfolioConciliationParticipants)
        Me.INDtxtParticipantName.Text = Me._portfolioParticipants.FullName.Trim().ToUpper()
        Me.INDtxtParticipantPosition.Text = Me._portfolioParticipants.Position.Trim().ToUpper()
        Me.INDrdgParticipantType.EditValue = Me._portfolioParticipants.Type.ToString()
        Me.INDtxtParticipantName.Focus()
        Me.INDtxtParticipantName.SelectAll()
    End Sub

    ''' <summary>
    ''' Elimina un participante seleccionado
    ''' </summary>
    Private Sub DeleteParticipant()
        If Me._portfolioConciliation IsNot Nothing AndAlso Me._portfolioConciliation.State <> 2 Then
            If Me.INDgdvParticipants.SelectedRowsCount > 0 Then
                If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesEliminarRegistro, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim aux = CType(Me.INDgdvParticipants.GetRow(Me.INDgdvParticipants.FocusedRowHandle), Domain.Entities.PortfolioConciliationParticipants)
                    If aux.ChangeTracker.State = ObjectState.Added Then
                        Me._portfolioConciliation.PortfolioConciliationParticipants.Remove(aux)
                        Me.Participants = Me._portfolioConciliation.PortfolioConciliationParticipants
                    Else
                        Me._portfolioConciliation.PortfolioConciliationParticipants.Remove(aux)
                        aux.ChangeTracker.State = ObjectState.Deleted
                        Me._portfolioConciliation.PortfolioConciliationParticipants.Add(aux)
                        If Me._portfolioConciliation.ChangeTracker.State = ObjectState.Unchanged Then
                            Me._portfolioConciliation.ChangeTracker.State = ObjectState.Modified
                        End If
                        Me.Participants = Me._portfolioConciliation.PortfolioConciliationParticipants
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Elimina una factura seleccionado
    ''' </summary>
    Private Sub DeleteInvoice()
        If Me.INDgdvInvoices.SelectedRowsCount > 0 Then
            Dim aux As Domain.Entities.PortfolioConciliationDetail = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.PortfolioConciliationDetail)
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesEliminarRegistro, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If aux.ChangeTracker.State = ObjectState.Added Then
                    Me._portfolioConciliation.PortfolioConciliationDetail.Remove(aux)
                    SelectedInvoices = _portfolioConciliation.PortfolioConciliationDetail
                Else
                    Me._portfolioConciliation.PortfolioConciliationDetail.Remove(aux)
                    aux.ChangeTracker.State = ObjectState.Deleted
                    Me._portfolioConciliation.PortfolioConciliationDetail.Add(aux)
                    If Me._portfolioConciliation.ChangeTracker.State = ObjectState.Unchanged Then
                        Me._portfolioConciliation.ChangeTracker.State = ObjectState.Modified
                    End If
                    Me.SelectedInvoices = Me._portfolioConciliation.PortfolioConciliationDetail

                End If

            End If
        End If
    End Sub

    ''' <summary>
    '''
    ''' </summary>
    Private Async Sub AddConciliation()

        If Me.INDgdvInvoices.SelectedRowsCount > 0 Then
            Dim aux As Domain.Entities.PortfolioConciliationDetail = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.PortfolioConciliationDetail)
            If aux IsNot Nothing Then
                Using Model As New MPortfolioConciliation(Me.Tag)
                    Dim Details As SP_PortfolioConciliation_Result = Await Model.GetSP_PortfolioConciliation(aux.DescInvoiceNumber, ClosingDate)
                    INDGcMoreInfo.DataSource = Model.GetAllExtractAccountReceivableByDocumentNumber(aux.DescInvoiceNumber)
                    If aux.DescValueGlosado Is Nothing Then
                        INDlblValGlosa.Text = 0
                    Else
                        INDlblValGlosa.Text = ManageDecimalsFun(Details.ValueGlosado)
                    End If
                    INDlblValReite.Text = Details.ValueReiterated.MoneyFormat
                    INDlblPortfolioStatusName.Text = aux.DescInvoiceStateEntity
                    Dim TotalAcceptIPS = Details.ValueAcceptedFirstInstance + Details.ValueAcceptedSecondInstance
                    INDlblValAceptIPSGlosa.Text = TotalAcceptIPS.MoneyFormat
                    INDlblValAceptReite.Text = Details.ValueAcceptedSecondInstance.MoneyFormat
                    INDlblValPartialPayments.Text = Details.ValuePayments.MoneyFormat
                    INDtxtBillTotal.Text = Details.DocumentValue.MoneyFormat
                    INDtxtCurrentBalance.Text = Details.CurrentBalance.MoneyFormat
                    INDlblBalance.Text = Details.Balance.Value.MoneyFormat
                    INDtxtBalanceReconcile.Text = Details.BalanceReconcile
                End Using

                If aux.StateConciliation Then
                    INDlblValGlosaC.EditValue = aux.ValueGlosadoConciliation
                    INDlblBalanceC.EditValue = aux.BalanceConciliation
                    INDlblValDifference.Text = aux.PortfolioDifferenceConciliation.Value.MoneyFormat
                    INDsleSearchConConcep.EditValue = aux.PortfolioConciliationConceptsId

                    INDGlePortfolioStatusNameC.EditValue = aux.StatePortfolioConciliation
                    INDtxtCommentsConciliation.EditValue = aux.Comment
                    INDlblValGlosaC.ReadOnly = True
                    INDlblBalanceC.ReadOnly = True
                    INDsleSearchConConcep.ReadOnly = True
                    INDbtnAddConciliation.Enabled = False
                Else
                    INDlblValGlosaC.ReadOnly = False
                    INDlblBalanceC.ReadOnly = False
                    INDsleSearchConConcep.ReadOnly = False
                    INDlblValGlosaC.EditValue = 0
                    INDlblBalanceC.EditValue = 0
                    INDlblValDifference.Text = 0
                    INDlblValDifferenceGlosa.Text = 0
                    INDGlePortfolioStatusNameC.EditValue = 1
                    INDtxtCommentsConciliation.EditValue = ""
                    INDbtnAddConciliation.Enabled = True

                End If

            End If

        End If
    End Sub

    Async Sub ShowGeneralInvoiceMovements()
        Dim listaInvoice = New List(Of String)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing
        view = Me.INDgdcInvoices.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim ObjD As PortfolioConciliationDetail = TryCast(view.GetRow(item), PortfolioConciliationDetail)
                If ObjD IsNot Nothing And ObjD.StatePortfolioConciliation = 1 Then
                    listaInvoice.Add(ObjD.InvoiceId)
                End If
            End If
        Next
        If listaInvoice.Count > 0 Then
            Dim _option = 3 'general a nivel de factura
            Dim PermisoGuardar As Boolean
            'valido que tenga permiso para guardar
            If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Guardar) = True Then
                PermisoGuardar = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Función para crear el objeto openFileDialog
    ''' </summary>
    Private Sub GetOpenFileDialog()
        fileOpener.CheckPathExists = True
        fileOpener.CheckFileExists = True
        fileOpener.Filter = "Excel Files(.xlsx)|*.xlsx| Excel Files(.xls)|*.xls| " &
                             "Excel Files(*.xlsm)|*.xlsm"
        fileOpener.Multiselect = False
        fileOpener.AddExtension = True
        fileOpener.ValidateNames = True
        fileOpener.InitialDirectory = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder()
        If (fileOpener.ShowDialog(Me) = DialogResult.OK) Then
            If fileOpener.FileName <> String.Empty Then
                Dim fileInfo = New System.IO.FileInfo(fileOpener.FileName)
                rutaExcel = fileOpener.FileName
                Using FrmLoad As New FrmLoadexcel
                    FrmLoad.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    FrmLoad.PathFile = rutaExcel
                    FrmLoad.Size = New System.Drawing.Size(800, 600)
                    FrmLoad.ModuleName = "CON"
                    FrmLoad.ListInvoice = Me.DataSourceInvoicesConciliation
                    Dim transparent As New FrmTransparent(FrmLoad, False)
                    transparent.ShowDialog(Me)
                    LoadControls()
                End Using
            End If
        End If
    End Sub

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
    ''' Funcion para cada vez que asignen un valor a los label's (Manjo de decimales)
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Private Function ManageDecimalsFun(Value As Decimal)

        Return Value.MoneyFormat

    End Function

#End Region

#Region "Handlers"

#Region "FormClosing"

    ''' <summary>
    ''' Aqui se desbloquea el registro
    ''' </summary>
    Private Sub FrmPortfolioConciliation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.UnblockeRecord()
    End Sub

#End Region

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Cuando cambio de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Async Sub INDbteNit_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDbteNit.EditValueChanged
        Dim ctomer As Domain.Entities.Customer = Nothing

        If INDbteNit.EditValue IsNot Nothing Then
            AsyncLoader(True)
            Using Model As New MPortfolioConciliation(MyBase.Tag)
                ctomer = Await Model.GetCustomerByNit(Me.ThirdPartyNit)
            End Using

            If ctomer.Name IsNot Nothing AndAlso Not ctomer.Name.Trim().Equals(String.Empty) Then
                Me.ThirdPartyId = ctomer.ThirdPartyId
                Me.CustomerId = ctomer.Id
            End If
            AsyncLoader(False)

        End If

        INDbteNit.Enabled = False
        INDbteConsecutive.Enabled = False

    End Sub

    ''' <summary>
    ''' Muestra en tiempo real la diferencia del saldo de cartera.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDlblBalanceC_EditValueChanged(sender As Object, e As EventArgs) Handles INDlblBalanceC.EditValueChanged
        INDlblValDifference.Text = INDlblBalance.Text - INDlblBalanceC.EditValue
    End Sub

    Private Sub INDlblValGlosaC_EditValueChanged(sender As Object, e As EventArgs) Handles INDlblValGlosaC.EditValueChanged
        INDlblValDifferenceGlosa.Text = INDtxtBalanceReconcile.Text - INDlblValGlosaC.EditValue
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

    ''' <summary>
    ''' Ejecuta el evento de llenado del datasource de los conceptos de conciliacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSearchConConcep_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSearchConConcep.QueryPopUp
        If INDsleSearchConConcep.Properties.DataSource Is Nothing Then
            INDsleSearchConConcep.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PortfolioService.ListConciliationConcepts()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el evento de llenado del datasource de las facturas.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleSearchInvoice_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSearchInvoice.QueryPopUp
        If INDsleSearchInvoice.Properties.DataSource Is Nothing Then
            If Me.ThirdPartyId > 0 Then
                INDsleSearchInvoice.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).PortfolioService.PortfolioAccountReceivableXpo(ThirdPartyId)
            Else
                Exit Sub
            End If
        End If
    End Sub

#End Region

#Region "GotFocus"

    ''' <summary>
    ''' Asigna el nombre del sender a la variable _openFindSender para habilitar
    ''' la funcionalidad de buscar en el control que corresponda
    ''' </summary>
    Private Sub OpenFindSender_GotFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.GotFocus, INDsleSearchInvoice.GotFocus
        Me._openFindSenser = CType(sender, System.Windows.Forms.Control).Name.ToUpper()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Permite la busqueda de una conciliacion por su numero de consecutivo
    ''' o la creación de una nueva si no se proporciona uno.
    ''' </summary>
    Private Async Sub INDbteConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(Me.INDbteConsecutive.Text.Trim()) AndAlso Not String.IsNullOrWhiteSpace(Me.INDbteConsecutive.Text.Trim()) AndAlso Not Me.INDbteConsecutive.Text.Trim().Equals("") AndAlso Convert.ToInt64(Me.INDbteConsecutive.Text.Trim()) > 0 Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoTienePermisos, Eform.Comunes)
                    Exit Sub
                End If
                Me.LoadControls() 'Consultamos el consecutivo
            Else
                Await Me.NewPortfolioConciliation()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Abre el popUp de participantes al precionar enter o la tecla de espacio
    ''' </summary>
    Private Sub INDpceParticipants_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceParticipants.KeyDown
        If e.KeyCode.Equals(System.Windows.Forms.Keys.Enter) OrElse e.KeyCode.Equals(System.Windows.Forms.Keys.Space) Then
            Me.INDpceParticipants.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Abre el popUp del comentario al precionar enter o la tecla de espacio
    ''' </summary>
    Private Sub INDpceComment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceComment.KeyDown
        If e.KeyCode.Equals(System.Windows.Forms.Keys.Enter) OrElse e.KeyCode.Equals(System.Windows.Forms.Keys.Space) Then
            Me.INDpceComment.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Consulta un cliente por su nit
    ''' </summary>
    Private Async Sub INDbteNit_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteNit.KeyDown
        Dim ctomer As Domain.Entities.Customer = Nothing

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDbteNit.EditValue IsNot Nothing Then
                Using Model As New MPortfolioConciliation(MyBase.Tag)
                    ctomer = Await Model.GetCustomerByNit(Me.ThirdPartyNit)
                End Using

                If ctomer.Name IsNot Nothing AndAlso Not ctomer.Name.Trim().Equals(String.Empty) Then
                    Me.ThirdPartyId = ctomer.ThirdPartyId
                    Me.CustomerId = ctomer.Id
                    Me._customer = ctomer
                Else
                    'Mensaje, El cliente no existe
                    If Me._customer Is Nothing Then
                        Me.INDbteNit.DisplayNullText = String.Empty
                        Me.INDbteNit.EditValue = Nothing
                    Else
                        Me.INDbteNit.EditValue = Me._customer.Nit.Trim()
                    End If
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ClienteNoExite, Eform.Conciliation)
                End If

            End If
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Posiciona el foco en el primer campo del popUp
    ''' </summary>
    Private Sub INDpceParticipants_Popup(sender As Object, e As EventArgs) Handles INDpceParticipants.Popup
        Me.INDtxtParticipantName.Focus()
    End Sub

    ''' <summary>
    ''' Posiciona el foco en el primer campo del popUp
    ''' </summary>
    Private Sub INDpceComment_Popup(sender As Object, e As EventArgs) Handles INDpceComment.Popup
        Me.INDmemComment.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega o modifica un participante en la lista de la cabecera
    ''' </summary>
    Private Sub INDbtnAddParticipant_Click(sender As Object, e As EventArgs) Handles INDbtnAddParticipant.Click

        If Me.INDbtnAddParticipant.Tag.ToString().Trim().Equals("ADD") Then

            If Not Me.INDtxtParticipantName.Text.Trim().Equals(String.Empty) Then

                If Not Me.INDtxtParticipantPosition.Text.Trim().Equals(String.Empty) Then

                    If Me.INDrdgParticipantType.EditValue IsNot Nothing Then

                        If Not Me._portfolioConciliation.PortfolioConciliationParticipants.Any(Function(p As Domain.Entities.PortfolioConciliationParticipants) p.FullName.Replace(" ", "").ToUpper().Equals(Me.INDtxtParticipantName.Text.Replace(" ", "").ToUpper())) Then
                            Me._portfolioConciliation.PortfolioConciliationParticipants.Add(New Domain.Entities.PortfolioConciliationParticipants With {.PortfolioConciliationId = Me._portfolioConciliation.Id, .FullName = Me.INDtxtParticipantName.Text.Trim().ToUpper(), .Position = Me.INDtxtParticipantPosition.Text.Trim().ToUpper(), .Type = Me.INDrdgParticipantType.EditValue.ToString(), .TypeName = IIf(INDrdgParticipantType.EditValue = 1, "IPS", "EAPB")})
                            If Me._portfolioConciliation.ChangeTracker.State = ObjectState.Unchanged Then
                                Me._portfolioConciliation.ChangeTracker.State = ObjectState.Modified
                            End If
                            Me.Participants = Me._portfolioConciliation.PortfolioConciliationParticipants
                        End If
                        Me.CleanParticipantControls()
                    Else
                        Me.INDrdgParticipantType.Focus()
                    End If
                Else
                    Me.INDtxtParticipantPosition.Focus()
                End If
            Else
                Me.INDtxtParticipantName.Focus()
            End If
        Else 'Entonces es modificar
            Me.INDbtnAddParticipant.Tag = "ADD"
            If Not Me.INDtxtParticipantName.Text.Trim().Equals(String.Empty) Then

                If Not Me.INDtxtParticipantPosition.Text.Trim().Equals(String.Empty) Then
                    Me._portfolioConciliation.PortfolioConciliationParticipants.Remove(Me._portfolioParticipants)

                    If Not Me._portfolioParticipants.FullName.Trim().ToUpper().Equals(Me.INDtxtParticipantName.Text.Trim().ToUpper()) Then
                        Me._portfolioParticipants.FullName = Me.INDtxtParticipantName.Text.Trim()
                    End If
                    If Not Me._portfolioParticipants.Position.Trim().ToUpper().Equals(Me.INDtxtParticipantPosition.Text.Trim().ToUpper()) Then
                        Me._portfolioParticipants.Position = Me.INDtxtParticipantPosition.Text.Trim()
                    End If
                    If Not Me._portfolioParticipants.Type.Equals(Me.INDrdgParticipantType.EditValue.ToString()) Then
                        Me._portfolioParticipants.TypeName = IIf(INDrdgParticipantType.EditValue = 1, "IPS", "EAPB")
                    End If
                    Me._portfolioConciliation.PortfolioConciliationParticipants.Add(Me._portfolioParticipants)
                    If Me._portfolioConciliation.ChangeTracker.State = ObjectState.Unchanged Then
                        Me._portfolioConciliation.ChangeTracker.State = ObjectState.Modified
                    End If
                    Me.Participants = Me._portfolioConciliation.PortfolioConciliationParticipants
                    Me.INDbtnAddParticipant.Text = obtenerRecurso(Eresources.TextoBotonAgregarParticipante, Eform.Conciliation)
                    Me.CleanParticipantControls()
                Else
                    Me.INDtxtParticipantPosition.Text = Me._portfolioParticipants.Position.Trim()
                End If
            Else
                Me.INDtxtParticipantName.Text = Me._portfolioParticipants.FullName.Trim()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el modo de edicion del participante seleccionado
    ''' </summary>
    Private Sub INDbtnEditParticipant_Click(sender As Object, e As EventArgs) Handles INDbtnEditParticipant.Click
        Me.EditParticipant()
    End Sub

    ''' <summary>
    ''' Ejecuta la eliminacion del participante seleccionado
    ''' </summary>
    Private Sub INDbtnDeleteParticipant_Click(sender As Object, e As EventArgs) Handles INDbtnDeleteParticipant.Click
        Me.DeleteParticipant()
    End Sub

    ''' <summary>
    ''' Ejecuta el evento Click  para añadir o visualizar la consciliacion por factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PopupContainerEdit1_Click(sender As Object, e As EventArgs) Handles PopupContainerEdit1.Click
        Me.AddConciliation()
    End Sub

    ''' <summary>
    ''' Realiza la busqueda de la factura seleccionada y la agrega a la rejilla
    ''' </summary>
    Private Async Sub INDbtnAddInvoice_Click(sender As Object, e As EventArgs) Handles INDbtnAddInvoice.Click

        If INDsleSearchInvoice.EditValue IsNot String.Empty AndAlso INDsleSearchInvoice.EditValue IsNot Nothing Then
            If Me._portfolioConciliation.PortfolioConciliationDetail.Where(Function(x) x.DescInvoiceNumber = INDsleSearchInvoice.EditValue).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La factura ya se encuentra agregada"
                Return
            End If

            Dim entitySp = Await Model.GetSP_PortfolioConciliation(INDsleSearchInvoice.EditValue, ClosingDate)
            Dim PortfolioConciliationDetail As New PortfolioConciliationDetail()

            With PortfolioConciliationDetail

                _actionResult = Model.ListInvoicePortfolioFilter(INDsleSearchInvoice.EditValue)
                _actionResultDetail = (From h In Me._actionResult Where h.InvoiceNumber.Equals(INDsleSearchInvoice.EditValue) Select h).FirstOrDefault

                Me.InvoiceId = Me._actionResultDetail.InvoiceId.Id
                Dim PortfolioStatusName = Me._actionResultDetail.PortfolioStatusName

                .InvoiceId = Me.InvoiceId
                .DescInvoiceNumber = entitySp.DocumentCode
                .AccountReceivableDate = entitySp.AccountReceivableDate
                .RadicatedNumber = IIf(entitySp.RadicatedConsecutive Is Nothing, 0, entitySp.RadicatedConsecutive)
                .RadicatedDate = entitySp.RadicatedDate
                .Balance = entitySp.Balance
                .ValueGlosado = entitySp.ValueGlosado
                .ValueEntity = entitySp.DocumentValue
                .PortfolioStatus = entitySp.PortfolioStatus
                .DescInvoiceStateEntity = PortfolioStatusName
                .ValueAcceptedFirstInstance = entitySp.ValueAcceptedFirstInstance
                .ValueAcceptedSecondInstance = entitySp.ValueAcceptedSecondInstance

            End With

            _portfolioConciliation.PortfolioConciliationDetail.Add(PortfolioConciliationDetail)

            If _portfolioConciliation.ChangeTracker.State = ObjectState.Unchanged Then
                _portfolioConciliation.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            INDsleSearchInvoice.EditValue = Nothing
            Me.SelectedInvoices = _portfolioConciliation.PortfolioConciliationDetail
            'Me.INDgdcInvoices.RefreshDataSource()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Elija una factura"
        End If

    End Sub

    ''' <summary>
    ''' Elimina la factura seleccionada en la rejilla
    ''' </summary>
    Private Sub INDbtnDeleteInvoice_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDbtnDeleteInvoice.Click
        Me.DeleteInvoice()
    End Sub

    ''' <summary>
    ''' Abre formulario de Carga de Datos de excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnLoad_Click(sender As Object, e As EventArgs) Handles INDbtnLoad.Click
        If Status = 1 Then
            Me.GetOpenFileDialog()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ("La Conciliacion debe estar en estado Registrado : Guardela primero")

        End If

    End Sub

    Private Sub INDbtnAddConciliation_Click(sender As Object, e As EventArgs) Handles INDbtnAddConciliation.Click
        Dim aux As Domain.Entities.PortfolioConciliationDetail = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.PortfolioConciliationDetail)

        Dim Conciliation = Me._portfolioConciliation.PortfolioConciliationDetail.Where(Function(x) x.InvoiceId = aux.InvoiceId).FirstOrDefault
        INDlblValGlosaC.ReadOnly = False
        INDlblBalanceC.ReadOnly = False
        With Conciliation
            .ValueGlosadoConciliation = INDlblValGlosaC.EditValue
            .BalanceConciliation = INDlblBalanceC.EditValue
            .PortfolioDifferenceConciliation = INDlblValDifference.Text
            If INDsleSearchConConcep.EditValue Is String.Empty Then
                Mensaje(EeventViewerImages.Advertencia) = "El concepto de Conciliacion no puede estar vacio"
                Exit Sub
            Else
                .PortfolioConciliationConceptsId = INDsleSearchConConcep.EditValue
            End If
            .StatePortfolioConciliation = INDGlePortfolioStatusNameC.EditValue
            .Comment = INDtxtCommentsConciliation.EditValue
            .StateConciliation = True
        End With
        _portfolioConciliation.PortfolioConciliationDetail.Add(Conciliation)
        Mensaje(EeventViewerImages.Informacion) = ("Conciliacion Agregada")
        INDbtnAddConciliation.Enabled = False

    End Sub

    ''' <summary>
    ''' Abre el frontal de busqueda para filtrar las conciliaciones
    ''' </summary>
    Private Sub INDbteConsecutive_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteConsecutive.Properties.ButtonClick
        Me.AbrirBusqueda()
    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgdvParticipants_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgdvParticipants.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()
        e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuEditarParticipante, Eform.Conciliation), AddressOf EditParticipant, My.Resources.modificarLineaAzul))
        e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuEliminarParticipante, Eform.Conciliation), AddressOf DeleteParticipant, My.Resources.eliminarLineaAzul))
    End Sub

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgdvInvoices_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgdvInvoices.PopupMenuShowing
        If Me.INDgdvInvoices.SelectedRowsCount > 0 AndAlso Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle) IsNot Nothing AndAlso (CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.PortfolioConciliationDetail).ChangeTracker.State = ObjectState.Unchanged Or CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.PortfolioConciliationDetail).ChangeTracker.State = ObjectState.Modified) Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
            Dim lista = New List(Of String)
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

            view = Me.INDgdcInvoices.FocusedView
            For Each item As Integer In view.GetSelectedRows()
                If item > -1 Then
                    Dim ObjD As PortfolioConciliationDetail = TryCast(view.GetRow(item), PortfolioConciliationDetail)
                    lista.Add(ObjD.Invoice.InvoiceNumber)
                End If
            Next
            If lista.Count > 1 Then
                e.Menu.Items.Add(New DXMenuItem("Conciliación General", AddressOf ShowGeneralInvoiceMovements, My.Resources.modificarLineaAzul))
            Else
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuEliminarFacturaConciliacion, Eform.Conciliation), AddressOf DeleteInvoice, My.Resources.eliminarLineaAzul))
            End If
        Else
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
        End If
    End Sub

#End Region

#Region "DoubleClick"

    ''' <summary>
    ''' Inicia la edicion del participante seleccionado en la rejilla
    ''' </summary>
    Private Sub INDgdcParticipants_DoubleClick(sender As Object, e As EventArgs) Handles INDgdcParticipants.DoubleClick
        If Me.INDgdvParticipants.SelectedRowsCount > 0 Then
            Me.EditParticipant()
        End If
    End Sub

    ''' <summary>
    ''' Abre el PopUpContainer de los detalles de factura
    ''' </summary>
    Private Sub INDgcvObjetions_DoubleClick(sender As Object, e As EventArgs) Handles INDgdvInvoices.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._portfolioConciliation IsNot Nothing AndAlso Me._portfolioConciliation.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.record IsNot Nothing AndAlso Me.INDbteConsecutive.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
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
            Me.INDsleSearchInvoice.ClosePopup()
            Me.INDsleSearchInvoice.EditValue = aux
            Me.INDsleSearchInvoice.Text = aux
        End If
    End Sub

#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Se ejecuta la ccion de confirmar dependiendo del contexto en que se encuentre situado el proceso
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Me.Status = 2
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Me._portfolioConciliation.Id, 0, {Me._portfolioConciliation.Id, Me.BarraBotones.OperatingUnit})
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.Buscar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.Click_GuardarConfirmar
        Me.Status = 2
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Status = 1
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion nuevo
    ''' </summary>
    Private Async Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.CleanControls()
        Await Me.NewPortfolioConciliation()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de anular
    ''' </summary>
    Private Sub BarraBotones_Anular() Handles BarraBotones.ClickAnular
        Anular()
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



#End Region

End Class