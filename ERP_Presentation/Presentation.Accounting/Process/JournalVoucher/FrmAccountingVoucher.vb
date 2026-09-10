'***********************************************************************
' Assembly         : Presentacion.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 16-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Spreadsheet
Imports DevExpress.Utils
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Controls.MVP

#End Region

Public Class FrmAccountingVoucher
    Implements IAccountBalance, ICustomizableForm

#Region "Builder"

    ''' <summary>
    ''' Método constructor
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        ctrDebitCredit = New CtrDebitCredit()
        ctrDebitCredit.Dock = DockStyle.Fill
        ctrDebitCredit.SetDebitAndCredit(AddressOf getDebitAndCredit)
        ctrDebitCredit.CodeISO4217 = Me.CodeCurrency
        AdditionalControlPanel.Controls.Add(ctrDebitCredit)
        ctrDebitCredit.RefreshDebitCredit()

        AddHandler journalVourcherHologationBs.DoWork, AddressOf journalVourcherHologationBs_DoWork
        AddHandler journalVourcherHologationBs.RunWorkerCompleted, AddressOf journalVourcherHologationBs_RunWorkerCompleted
    End Sub

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Accounting"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PAccountingVoucher

    ''' <summary>
    ''' Control para establecer el balance del comprobante
    ''' </summary>
    Public ctrDebitCredit As CtrDebitCredit

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _operativeUnitId As Int32

    ''' <summary>
    ''' variable para registrar el bloqueo de los registros
    ''' </summary>
    Private _blockRecord As BlockRecordGeneralLedger

    ''' <summary>
    ''' Filtro aplicado en el control de busqueda
    ''' </summary>
    Private _activeFilterString As String

    ''' <summary>
    ''' Id Journal Voucher
    ''' </summary>
    Private _journalVoucherId As Integer = 0

    ''' <summary>
    ''' The document accounting
    ''' </summary>
    Private _journalVoucher As Domain.Entities.JournalVouchers

    ''' <summary>
    ''' Detalle a editar
    ''' </summary>
    Private _journalVoucherDetail As JournalVoucherDetails

    ''' <summary>
    ''' Listado que contiene el detalle del documento contable
    ''' </summary>
    Private _listJournalVoucherDetails As List(Of JournalVoucherDetails)

    ''' <summary>
    ''' Listado que contiene el detalle del documento contable a eliminar
    ''' </summary>
    Private _listDeleteJournalVoucherDetails As List(Of JournalVoucherDetails)

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _varImp As Integer

    ''' <summary>
    '''  Codigo de 3 caracteres de la moneda  ISO4217
    ''' </summary>
    Private _codeCurrency As String

    ''' <summary>
    ''' Id de la cultura con el que se abre el formulario
    ''' </summary>
    Private CultureInfoId As Integer

    ''' <summary>
    ''' Id de la Moneda del Libro
    ''' </summary>
    Private _currencyBookId As Integer?

    ''' <summary>
    ''' Parrametros de Facturacion
    ''' </summary>
    Private SettingBilling As SettingsBilling

#Region "Worker"

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
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Dim totalProcessedItems As Integer

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Dim itemsSend As Integer = 600

    ''' <summary>
    ''' metodo para mostrar en el control cuantos items se han procesado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getInfo() As Tuple(Of String, String)
        Return New Tuple(Of String, String)(totalProcessedItems.ToString(), totalItems.ToString())
    End Function

#End Region

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IAccountBalance.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDlyJournalVoucher.BeginUpdate()

            indGlConsecutive.Enabled = Not value
            indglTypeDocument.Enabled = value
            indDtDateDocument.Enabled = value
            indMemoObservation.Enabled = value
            btnAdd.Enabled = value
            INDEsbDetails.Enabled = value
            INDBtnImportFile.Enabled = False
            indGcDetail.Enabled = value

            INDlyJournalVoucher.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the consecutive.
    ''' </summary>
    ''' <value>
    ''' The consecutive.
    ''' </value>
    Public Property Consecutive As String Implements IAccountBalance.Consecutive
        Get
            If indGlConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return indGlConsecutive.Text
            End If
        End Get
        Set(value As String)
            indGlConsecutive.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the i documente.
    ''' </summary>
    ''' <value>
    ''' The i documente.
    ''' </value>
    Public Property IDocumente As Integer Implements IAccountBalance.IDocumente
        Get
            Return indglTypeDocument.EditValue
        End Get
        Set(value As Integer)
            indglTypeDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the date document.
    ''' </summary>
    ''' <value>
    ''' The date document.
    ''' </value>
    Public Property DateDocument As Date Implements IAccountBalance.DateDocument
        Get
            Return indDtDateDocument.EditValue
        End Get
        Set(value As Date)
            indDtDateDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the number document.
    ''' </summary>
    ''' <value>
    ''' The number document.
    ''' </value>
    Public Property NumberDocument As Integer Implements IAccountBalance.NumberDocument
        Get
            Return indMemoObservation.Text
        End Get
        Set(value As Integer)
            indMemoObservation.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the observation.
    ''' </summary>
    ''' <value>
    ''' The observation.
    ''' </value>
    Public Property Observation As String Implements IAccountBalance.Observation
        Get
            Return indMemoObservation.Text
        End Get
        Set(value As String)
            indMemoObservation.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the state.
    ''' </summary>
    ''' <value>
    ''' The state.
    ''' </value>
    Public Property State As Byte Implements IAccountBalance.State
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Sumatoria Debito del comprobante
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetDebit As Decimal
        Get
            Dim _debitValue = 0D
            If Me._listJournalVoucherDetails IsNot Nothing AndAlso Me._listJournalVoucherDetails.Any Then
                _debitValue = Me._listJournalVoucherDetails.Sum(Function(d) d.DebitValue)
            End If
            Return _debitValue
        End Get
    End Property

    ''' <summary>
    ''' Sumatoria Credito del comprobante
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property GetCredit As Decimal
        Get
            Dim _creditValue = 0D
            If Me._listJournalVoucherDetails IsNot Nothing AndAlso Me._listJournalVoucherDetails.Any Then
                _creditValue = Me._listJournalVoucherDetails.Sum(Function(d) d.CreditValue)
            End If
            Return _creditValue
        End Get
    End Property

    ''' <summary>
    ''' ISO 4217 Code
    ''' </summary>
    ''' <returns></returns>
    Property CodeCurrency As String Implements IAccountBalance.CodeCurrency
        Get
            Return _codeCurrency
        End Get
        Set(value As String)
            _codeCurrency = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the debit.
    ''' </summary>
    ''' <returns></returns>
    Private Function getDebitAndCredit() As Tuple(Of Decimal, Decimal)
        Return New Tuple(Of Decimal, Decimal)(GetDebit, GetCredit)
    End Function

    ''' <summary>
    ''' retorna un clone de la cultura actual pero con el formato moneda especifico del libro
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CultureNumbertFormat As CultureInfo
        Get
            Dim Culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Culture.NumberFormat = Me.CodeCurrency.GetNumberFormat
            Return Culture
        End Get
    End Property

    ''' <summary>
    ''' Id de la moneda del libro seleccionado
    ''' </summary>
    ''' <returns></returns>
    Property BookCurrencyId As Integer? Implements IAccountBalance.BookCurrencyId
        Get
            Return _currencyBookId
        End Get
        Set(value As Integer?)
            _currencyBookId = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Gets or sets the document type xpo.
    ''' </summary>
    ''' <value>
    ''' The document type xpo.
    ''' </value>
    Public Property DocumentTypeXpo As XPInstantFeedbackSource Implements IAccountBalance.DocumentTypeXpo
        Get
            Return CType(indglTypeDocument.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            indglTypeDocument.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        AddHandler FormSearchObjects.ReturnActiveFilter, AddressOf ReturnActiveFilter
        With FormSearchObjects
            .ListaColumnas = {
                              New ColumnInfo With {.ColumnAligment = HorzAlignment.Center, .Caption = "Consecutivo", .FieldName = "Consecutive", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14},
                              New ColumnInfo With {.Caption = "Tipo de Documento", .FieldName = "JournalVoucherTypeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.16},
                              New ColumnInfo With {.Caption = "Origen", .FieldName = "EntityName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14},
                              New ColumnInfo With {.Caption = "Documento", .FieldName = "EntityCode", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14},
                              New ColumnInfo With {.Caption = "Fecha de Documento", .FieldName = "VoucherDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14},
                              New ColumnInfo With {.Caption = "Observaciones", .FieldName = "Detail", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14},
                              New ColumnInfo With {.Caption = "Valor", .FieldName = "Value", .ColumnFormat = "C2", .FormatCulture = CultureNumbertFormat, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.14}}.ToList()
            .ValorSolicitado = "Id"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.GetAllJournalVourchers
            .FiltroBusqueda = INDSleLegalBook.EditValue
            .ActiveFilterString = _activeFilterString
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        If ReturnValue = "" Then
            Return
        End If
        indGlConsecutive.Text = ""
        _journalVoucherId = ReturnValue
        If _journalVoucherId <> 0 Then
            Await LoadControls()
            If indGlConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            indGlConsecutive.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo para mantener el filtro de busqueda
    ''' </summary>
    ''' <param name="ActiveFilterString"></param>
    Private Sub ReturnActiveFilter(ByVal ActiveFilterString As String)
        _activeFilterString = ActiveFilterString
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Dim withLegalBook = If(String.IsNullOrEmpty(INDSleLegalBook.EditValue), True, False)
        Deshacer(withLegalBook)
    End Sub

    ''' <summary>
    ''' Método que se encarga de hacer el llamado al método de deshacer cambios en los controles.
    ''' </summary>
    ''' <param name="withLegalBook"></param>
    Private Sub Deshacer(Optional withLegalBook As Boolean = False)
        CleanControls(withLegalBook)
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        NewJournalVoucher()
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Me._journalVoucher.Status <> 3 AndAlso Me._journalVoucher.Status <> 4 Then
            If ValidateHeader() = False Then
                Exit Sub
            End If
            If Me._listJournalVoucherDetails Is Nothing OrElse Me._listJournalVoucherDetails.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El comprobante contable no tiene detalles."
                Exit Sub
            End If
            If Me.GetDebit <> Me.GetCredit Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format("El comprobante contable se encuentra desbalanceado. Débitos: {0} - Créditos: {1}", String.Format("{0:c2}", GetDebit), String.Format("{0:c2}", GetCredit))
                Exit Sub
            End If
        End If
        Try
            AssigningValues()
            Await SaveJournalVoucher()
        Catch ex As Exception
            AsyncLoader(False)
            indGlConsecutive.Enabled = False
            Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' metodo para abrir el form de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, indGlConsecutive.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer todo. 
    ''' </summary>
    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        Deshacer(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Metodo para guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _journalVoucher.Status = 1
        _varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _journalVoucher.Status = 1
        _varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _journalVoucher.Status = 3
            _varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' metodo para guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _journalVoucher.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' metodo para actualizar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _journalVoucher.Status = 2
            _varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' desconfirma el documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_Desconfirmar() Handles BarraBotones.Click_Desconfirmar
        Dim unConfirmationMessageDefault = Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Disconfirm", "Accounting")

        If _journalVoucher?.Id > 0 Then
            Dim journalVouchersAssociated = _presenter.GetJournalVouchersAssociated(_journalVoucher.AccountingMovementId)
            If journalVouchersAssociated.Count > 1 Then
                If Not journalVouchersAssociated.Where(Function(jv) jv.Id = _journalVoucher.Id AndAlso jv.LegalBookId.OfficialBook).Any() Then
                    Mensaje(EeventViewerImages.Advertencia) = "El comprobante contable tiene homólogos. Desconfirme el comprobante contable del libro oficial"
                    Exit Sub
                End If

                unConfirmationMessageDefault = $"El comprobante contable tiene homólogos, {unConfirmationMessageDefault}"
            End If
        End If

        If (MessageIndigo.Show(unConfirmationMessageDefault, MessageType.Question, "Desconfirmar", Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
            _journalVoucher.Status = 4
            _varImp = 2
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Clcik Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _journalVoucher.Id, 0, _journalVoucher.Id, BarraBotones.OperatingUnit)
    End Sub

    ''' <summary>
    ''' Generar homologacion
    ''' </summary>
    ''' <param name="legalBookId"></param>
    Private Sub BarraBotones_HomologationJournalVoucher(legalBookId As Integer) Handles BarraBotones.HomologationJournalVoucher
        Dim listJournalTmp = New List(Of JournalVouchers)
        Using Model As New MDocumentAccount(MyTag)
            Dim result = Model.GenerateHomologationsJournalVoucher(_journalVoucher, legalBookId)
            If result IsNot Nothing AndAlso result.StateResult = True Then
                Dim journal As New JournalVouchers
                With journal
                    .LegalBookId = legalBookId
                End With
                For Each detail In result.ObjectEmbbeded
                    journal.JournalVoucherDetails.Add(detail)
                Next
                listJournalTmp.Add(journal)

                BarraBotones.SetDataSourceJuornalVoucher(listJournalTmp, officialBooks)
                BarraBotones.HomologationsCount += listJournalTmp.Count
            Else
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                BarraBotones.LySaveHomologationButton = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Guardar la homologacion
    ''' </summary>
    ''' <param name="journalVoucherHomologation"></param>
    Private Async Sub BarraBotones_SaveHomologationJournalVoucher(journalVoucherHomologation As JournalVouchers) Handles BarraBotones.SaveHomologationJournalVoucher
        Try
            AsyncLoader(True)
            ''Probar si se puede llamar el metodo guardar, reemplazando el journalvoucher
            With journalVoucherHomologation
                .AccountingMovementId = _journalVoucher.AccountingMovementId
                .IdJournalVoucher = _journalVoucher.IdJournalVoucher
                .VoucherDate = _journalVoucher.VoucherDate
                .Status = _journalVoucher.Status
                .Detail = _journalVoucher.Detail
                .EntityCode = _journalVoucher.EntityCode
                .EntityId = _journalVoucher.EntityId
                .EntityName = _journalVoucher.GetType.Name
                .IsClosedYear = _journalVoucher.IsClosedYear
            End With
            Using Model As New MDocumentAccount(Me.Tag.ToString())
                Dim Result = Await Model.SaveDocumentAccounting(journalVoucherHomologation)
                If Result.StateResult Then
                    Me.BarraBotones.LegalBookId = 0
                    Me.BarraBotones.HomologationsCount = 0
                    Await LoadControls()
                    Mensaje(EeventViewerImages.Informacion) = "Se creo correctamente la homologación"
                Else
                    AsyncLoader(False)
                    Me.BarraBotones.LyHomologationButton = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.BarraBotones.LySaveHomologationButton = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult.Count > 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString
                    ElseIf Not String.IsNullOrEmpty(Result.Message) Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "Ocurrió un error al guardar el comprobante de homologación"
                    End If

                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que dispara el formulario de importar productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ImportarInformacion() Handles BarraBotones.Click_ImportarInformacion
        Using formulario As New FrmImportJournalVoucher
            AddHandler formulario.GetJournalVoucher, AddressOf ReturnGetJournalVoucher
            Me.Cursor = ChangeCursorIndigo()
            formulario.Size = New System.Drawing.Size(800, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.LegalBookId = INDSleLegalBook.EditValue
            formulario.JournalVoucherTypeId = IDocumente
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Método que retorna el comprobante contable seleccionado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="journalVoucherId"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnGetJournalVoucher(sender As Object, journalVoucherId As Integer)
        Try
            AsyncLoader(True)

            Using Model As New MDocumentAccount(Me.Tag.ToString())
                Dim result = Await Model.GetJournalVoucherDetailsbyJournalVoucherId(journalVoucherId)
                If result.StateResult Then
                    Dim journalVoucherDetails = result.ObjectEmbbeded
                    If journalVoucherDetails.Count > 0 Then
                        _listJournalVoucherDetails = If(_listJournalVoucherDetails Is Nothing, New List(Of JournalVoucherDetails), _listJournalVoucherDetails)
                        For Each journalVoucherDetail In journalVoucherDetails
                            _listJournalVoucherDetails.Add(New JournalVoucherDetails With
                            {
                                .IdMainAccount = journalVoucherDetail.IdMainAccount,
                                .CodeNameMainAccount = journalVoucherDetail.CodeNameMainAccount,
                                .IdThirdParty = journalVoucherDetail.IdThirdParty,
                                .CodeNameThirdParty = journalVoucherDetail.CodeNameThirdParty,
                                .IdCostCenter = journalVoucherDetail.IdCostCenter,
                                .CodeNameCostCenter = journalVoucherDetail.CodeNameCostCenter,
                                .DebitValue = journalVoucherDetail.DebitValue,
                                .CreditValue = journalVoucherDetail.CreditValue,
                                .Detail = journalVoucherDetail.Detail,
                                .IdRetention = journalVoucherDetail.IdRetention,
                                .CodeNameRetention = journalVoucherDetail.CodeNameRetention,
                                .RetentionRate = journalVoucherDetail.RetentionRate,
                                .BaseValue = journalVoucherDetail.BaseValue,
                                .BillingValue = journalVoucherDetail.BillingValue
                            })
                        Next

                        indGcDetail.DataSource = Nothing
                        indGcDetail.DataSource = _listJournalVoucherDetails
                        ctrDebitCredit.RefreshDebitCredit()
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Handles the Load event of the FrmAccountingVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAccountingVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyJournalVoucher, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        _operativeUnitId = BarraBotones.OperatingUnitValue
        CultureInfoId = CultureInfo.CurrentCulture.LCID
        '******************************
        Me._presenter = New PAccountingVoucher(Me)
        Using Model As New MDocumentAccount(MyTag)
            INDSleLegalBook.Properties.DataSource = Model.ListBook()
        End Using

        AddActionsColumns()
        Deshacer(True)
        LoadStatus()

        INDEsbDetails.AddExcelSheets(New ExcelSheet With {
            .Columns = New List(Of ExcelColumn) From {
                New ExcelColumn With {.Name = "Cuenta Contable"},
                New ExcelColumn With {.Name = "Tercero"},
                New ExcelColumn With {.Name = "Centro de Costo", .Type = ExcelColumnType.Text},
                New ExcelColumn With {.Name = "Naturaleza", .Comment = "1 = Debito" & vbCrLf & "2 = Credito"},
                New ExcelColumn With {.Name = "Observación"},
                New ExcelColumn With {.Name = "Valor"},
                New ExcelColumn With {.Name = "Retencion"},
                New ExcelColumn With {.Name = "Valor Facturado", .Comment = "Aplica Solo Para Retención"},
                New ExcelColumn With {.Name = "Valor Base", .Comment = "Aplica Solo Para Retención"}
            }
        })
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando el formulario está siendo eliminado, reinicia las variables y demás objetos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrDebitCredit = Nothing
        _operativeUnitId = Nothing
        _blockRecord = Nothing
        _activeFilterString = Nothing
        _journalVoucherId = Nothing
        _journalVoucher = Nothing
        _journalVoucherDetail = Nothing
        _listJournalVoucherDetails = Nothing
        _listDeleteJournalVoucherDetails = Nothing
        _varImp = Nothing

        progress = Nothing
        totalItems = Nothing
        totalProcessedItems = Nothing
        itemsSend = Nothing

        journalHomologations = Nothing
        officialBooks = Nothing
        journalVourcherHologationBs = Nothing
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se ejecuta cuando el frontal se activa
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDSleLegalBook.EditValue Is Nothing Then
            INDSleLegalBook.Focus()
        End If
    End Sub

#End Region

#Region "FormClosed"

    ''' <summary>
    ''' cierra y elimina el block record
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAccountingVoucher_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the indGlConsecutive control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Sub indGlConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indGlConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If indGlConsecutive.Text = "" Then
                NewJournalVoucher()
            Else
                indGlConsecutive.Enabled = False
                indglTypeDocument.Enabled = True
                indglTypeDocument.Focus()
                BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the indglTypeDocument control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Async Sub indglTypeDocument_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indglTypeDocument.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDSleLegalBook.EditValue) AndAlso Not String.IsNullOrEmpty(indglTypeDocument.EditValue) AndAlso Not String.IsNullOrEmpty(indGlConsecutive.Text) Then
                Await LoadControls()
            Else
                indDtDateDocument.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the indDtDateDocument control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Sub indDtDateDocument_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indDtDateDocument.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            indMemoObservation.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the indTxtDocument control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Sub indTxtDocument_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            indMemoObservation.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the indMemoObservation control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Sub indMemoObservation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles indMemoObservation.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            btnAdd.Focus()
        End If
    End Sub

#End Region

#Region "LostFocus"

    ''' <summary>
    ''' evento que valida el periodo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub indDtDateDocument_LostFocus(sender As Object, e As EventArgs) Handles indDtDateDocument.LostFocus
        Using Model As New MDocumentAccount(Me.Tag)
            If indDtDateDocument.EditValue IsNot Nothing Then
                If Not Await Model.ValidatePeriod(CDate(indDtDateDocument.EditValue).Month, CDate(indDtDateDocument.EditValue).Year) Then
                    Dim periods As List(Of ClosedMonth) = Model.GetOpenPeriod()
                    If periods IsNot Nothing AndAlso periods.Count > 0 Then
                        Dim perOpen As String = AccountingServices.GetListOpenPeriods(periods)
                        Mensaje(EeventViewerImages.Advertencia) = perOpen
                    End If

                End If
            End If
        End Using
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Handles the QueryPopUp event of the indglTypeDocument control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub indglTypeDocument_QueryPopUp(sender As Object, e As CancelEventArgs) Handles indglTypeDocument.QueryPopUp
        If DocumentTypeXpo Is Nothing Then
            Using msearch As New MBusqueda()
                DocumentTypeXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListJournalVoucherByState, "True")
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Este evento se ejecuta cuando se da click sobre el control de libro oficial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleLegalBook_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleLegalBook.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("1682", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Este evento se ejecuta cuando se da click sobre el control de "tipo de comprobante contable"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub indglTypeDocument_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles indglTypeDocument.ButtonClick, indglTypeDocument.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmDocumentType
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Using msearch As New MBusqueda()
                    DocumentTypeXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListJournalVoucherByState, "True")
                End Using
            End Using
        End If
    End Sub

#End Region

#Region "OpenLink"

    ''' <summary>
    ''' Evento que controla la acción al dar click sobre el hipervinculo de "Tipo documento"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDHleDocument_OpenLink(sender As Object, e As DevExpress.XtraEditors.Controls.OpenLinkEventArgs) Handles INDHleDocument.OpenLink
        If _journalVoucher Is Nothing Then
            Exit Sub
        End If

        If SettingBilling Is Nothing Then
            Using model As New MBillingSetting(Me.Tag)
                SettingBilling = Await model.GetSettingsBillingByIdUnitOperative(BarraBotones.OperatingUnit.Id.ToString(), False)
            End Using
        End If

        If _journalVoucher.EntityName = "Invoice" Then
            If SettingBilling?.LiquidateMasterAccount Then 'si liquida cuenta madre envia el reporte correspondiente
                Dim reportCima As New Reporter.rptSaleInvoiceMotherAccount
                ReportHelper.ExecuteReport(reportCima, Me, Me.BarraBotones.PermissionsForm, _journalVoucher.EntityId)
                reportCima.ParametrosReporte = New Object() {_journalVoucher.EntityId}
            Else
                Dim reportDef As New Reporter.rptSaleInvoice
                ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, _journalVoucher.EntityId)
                reportDef.ParametrosReporte = New Object() {_journalVoucher.EntityId}
            End If
        ElseIf _journalVoucher.EntityName = "InvoiceEntityCapitated" Then
            Dim reportDef As New Reporter.rptSaleInvoiceCapitated
            ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, _journalVoucher.EntityId)
            reportDef.ParametrosReporte = New Object() {_journalVoucher.EntityId}
        ElseIf _journalVoucher.EntityName = "GlosaObjectionsReceptionD" Then
            Dim _tagForm = ResourceManager.GetString(_journalVoucher.EntityName + "_TAG", NAME_MODULE)
            If _tagForm IsNot Nothing Then
                Using Model As New MDocumentAccount(MyTag)
                    Dim detail As GlosasObjectionDXpo = Model.GetGlosasObjectionDByInvoiceNumber(_journalVoucher.EntityCode)
                    OpenForm(_tagForm, detail.GlosaObjectionsReceptionCId.RadicatedConsecutive, False)
                End Using
            End If
        Else
            Dim _tagForm = ResourceManager.GetString(_journalVoucher.EntityName + "_TAG", NAME_MODULE)
            If _tagForm IsNot Nothing Then
                OpenForm(_tagForm, _journalVoucher.EntityCode, False)
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Libro Oficial"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleLegalBook_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleLegalBook.EditValueChanged
        indGlConsecutive.Enabled = True
        INDSleLegalBook.Enabled = False
        indGlConsecutive.Focus()
        If INDSleLegalBook.Properties.DataSource IsNot Nothing AndAlso INDSleLegalBook.EditValue IsNot Nothing Then
            Dim Currency = TryCast(TryCast(SearchLookUpEdit1View.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, BookXpo).CommonCurrency
            Me.CodeCurrency = Currency?.Abbreviation
            Me.BookCurrencyId = Currency?.Id
        End If
        Dim CultureId = Me.CodeCurrency.GetCultureId
        INDColCreditValue = Window.Utils.FormatGrid(INDColCreditValue, Me.CodeCurrency)
        INDColDebits = Window.Utils.FormatGrid(INDColDebits, Me.CodeCurrency)
        ctrDebitCredit.CodeISO4217 = Me.CodeCurrency
        ctrDebitCredit.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Tipo de comprobante contable" 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indglTypeDocument_EditValueChanged(sender As Object, e As EventArgs) Handles indglTypeDocument.EditValueChanged
        INDBtnImportFile.Enabled = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        If _journalVoucher Is Nothing OrElse _journalVoucher.Id > 0 Then
            Exit Sub
        End If
        If Not String.IsNullOrEmpty(indglTypeDocument.EditValue) Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
            If indDtDateDocument.EditValue IsNot Nothing Then
                INDBtnImportFile.Enabled = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando cambia el valor del control "Fecha" 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub indDtDateDocument_EditValueChanged(sender As Object, e As EventArgs) Handles indDtDateDocument.EditValueChanged
        INDBtnImportFile.Enabled = False
        If _journalVoucher IsNot Nothing AndAlso _journalVoucher.Id > 0 Then
            Exit Sub
        End If
        If indDtDateDocument.EditValue IsNot Nothing Then
            If indglTypeDocument.EditValue IsNot String.Empty Then
                INDBtnImportFile.Enabled = True
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Handles the Click event of the SimpleButton1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        If ValidateHeader() = True AndAlso Me._journalVoucher.Status = 1 Then
            LoadFormAddItem(Nothing)
        End If
    End Sub

    ''' <summary>
    ''' Evento que llama la función ImportFile 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Await ImportFile()
    End Sub

#End Region

#Region "DoubleClick"

    ''' <summary>
    ''' Evento para abrir el registro cuando no esta la columna de acciones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub viewDetail_DoubleClick(sender As Object, e As EventArgs) Handles viewDetail.DoubleClick
        LoadFormAddItem(DirectCast(viewDetail.GetFocusedRow(), JournalVoucherDetails))
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Establece las acciones de la columna de mas info
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                LoadFormAddItem(DirectCast(viewDetail.GetFocusedRow(), JournalVoucherDetails))
            Case "Remove"
                Dim rowItem As JournalVoucherDetails
                rowItem = DirectCast(viewDetail.GetFocusedRow(), JournalVoucherDetails)

                If rowItem.Id > 0 Then
                    rowItem.MarkAsDeleted()
                    If _listDeleteJournalVoucherDetails Is Nothing Then
                        _listDeleteJournalVoucherDetails = New List(Of JournalVoucherDetails)
                    End If
                    _listDeleteJournalVoucherDetails.Add(rowItem)
                End If

                _listJournalVoucherDetails.Remove(rowItem)
                indGcDetail.DataSource = Nothing
                indGcDetail.DataSource = _listJournalVoucherDetails
                ctrDebitCredit.RefreshDebitCredit()
        End Select
    End Sub

#End Region

#Region "PasteToGrid"

    ''' <summary>
    ''' Evento que se ejecuta cuando se realiza un pegado de datos en una rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If e.Rows.Count > itemsSend Then
            Mensaje(EeventViewerImages.Advertencia) = "Para procesar esta cantidad de información se debe hacer por medio de importación de archivos"
            Exit Sub
        End If

        AsyncLoader(True)
        If e.Rows.Count = 0 Then
            AsyncLoader(False)
            Exit Sub
        End If
        If e.Rows(0).Item(0).Contains("Cuenta Contable") Then
            e.Rows.Remove(e.Rows.ElementAt(0))
        End If
        Using model As New MDocumentAccount(MyTag)
            Dim result = Await model.SetJournalVoucherDetailsCopyPaste(INDSleLegalBook.EditValue, e.Rows)
            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    transparent.ShowDialog(Me)
                End Using
            End If
            If _listJournalVoucherDetails Is Nothing Then
                _listJournalVoucherDetails = New List(Of JournalVoucherDetails)
            End If
            _listJournalVoucherDetails.AddRange(result.ObjectEmbbeded)

            indGcDetail.DataSource = Nothing
            indGcDetail.DataSource = _listJournalVoucherDetails
            ctrDebitCredit.RefreshDebitCredit()
        End Using

        AsyncLoader(False)
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        IndigoGridControl1.RefreshGrid(indGcDetail)
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Limpiar los controles
    ''' </summary>
    ''' <param name="withLegalBook"></param>
    Public Async Sub CleanControls(withLegalBook As Boolean)
        INDlyJournalVoucher.BeginUpdate()
        Await DeleteBlockedRecord()

        indGlConsecutive.Text = ""
        indglTypeDocument.EditValue = Nothing
        indglTypeDocument.Properties.NullText = ""
        indDtDateDocument.EditValue = Nothing
        INDtxtDocument.Text = ""
        INDlyItemDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDHleDocument.Text = ""
        INDlyItemDocumentLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        indMemoObservation.Text = ""
        'INDlyItemObservation.HideControl()
        indGcDetail.DataSource = Nothing

        _journalVoucherId = 0
        _journalVoucher = Nothing
        _journalVoucherDetail = Nothing
        _listJournalVoucherDetails = Nothing
        _listDeleteJournalVoucherDetails = Nothing

        ReadOnlyControls(False)
        ActionsOnControls = False

        If withLegalBook Then
            Me.CodeCurrency = String.Empty
            INDSleLegalBook.EditValue = Nothing
            INDSleLegalBook.Properties.NullText = ""
            INDSleLegalBook.Enabled = True
            indGlConsecutive.Enabled = False
            Me.BookCurrencyId = Nothing
            INDSleLegalBook.Focus()
        Else
            INDSleLegalBook.Enabled = False
            indGlConsecutive.Enabled = True
            indGlConsecutive.Focus()
        End If
        ctrDebitCredit.RefreshDebitCredit()

        Me.BarraBotones.LegalBookId = 0
        Me.BarraBotones.HomologationsCount = 0
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False

        INDlyJournalVoucher.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.JournalVourcherHomologation) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para  crear un nuevo comprobante contable
    ''' </summary>
    Private Sub NewJournalVoucher()
        Me.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecord = "1" 'Sin confirmar
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True

        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        Me._journalVoucher = New Domain.Entities.JournalVouchers With {.Status = 1}
    End Sub

    ''' <summary>
    ''' Carga los controles.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If _journalVoucherId > 0 OrElse (Not String.IsNullOrEmpty(INDSleLegalBook.EditValue) AndAlso Not String.IsNullOrEmpty(indglTypeDocument.EditValue) AndAlso Not String.IsNullOrEmpty(Consecutive)) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MDocumentAccount(Me.Tag)
                    AsyncLoader(True)
                    INDlyJournalVoucher.BeginUpdate()

                    If _journalVoucherId > 0 Then
                        _journalVoucher = Await Model.GetJournalVouchersbyIdAsync(_journalVoucherId)
                    Else
                        _journalVoucher = Await Model.GetAccountingDocumentByConsecutiveAndJournalVoucherTypeIdAndLegalBookId(INDSleLegalBook.EditValue, indglTypeDocument.EditValue, Consecutive)
                    End If

                    If _journalVoucher IsNot Nothing AndAlso _journalVoucher.Id > 0 Then
                        _blockRecord = Await Model.GetBlockRecord(Me.Tag, _journalVoucher.Id)
                        With _journalVoucher
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                            _journalVoucherId = .Id
                            INDSleLegalBook.EditValue = .LegalBookId
                            indGlConsecutive.EditValue = .Consecutive
                            indglTypeDocument.EditValue = .IdJournalVoucher
                            indglTypeDocument.Properties.NullText = .CodeNameJournalVoucherType
                            indDtDateDocument.EditValue = .VoucherDate
                            indMemoObservation.EditValue = .Detail
                            Me.BarraBotones.StatusRecord = .Status.ToString()
                            Me.BarraBotones.StatusRecordVisible = True
                            INDBtnImportFile.Enabled = False

                            'Se coloca en el campo de documento el nombre del tipo el cual genero el comprobante
                            'cuando exista el origen del frm ConsignmentCostList eliminar el And _journalVoucher.EntityName <> "ConsignmentCostList"
                            If _journalVoucher.EntityCode IsNot Nothing AndAlso _journalVoucher.EntityCode.Count > 0 Then
                                INDHleDocument.Text = String.Format(ResourceManager.GetString(_journalVoucher.EntityName), _journalVoucher.EntityCode)
                                INDlyItemDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                                INDlyItemDocumentLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                            Else
                                INDtxtDocument.Text = String.Format(ResourceManager.GetString("JournalVoucher"), _journalVoucher.Consecutive)
                                INDlyItemDocument.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDlyItemDocumentLink.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                            End If

                            'Consulto los detalles
                            Await LoadDetails()
                            ctrDebitCredit.RefreshDebitCredit()

                            'si el comprobante esta confirmado se habilita el boton para homologar 
                        End With
                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._journalVoucher.Consecutive)
                        If _blockRecord.Id = 0 Then
                            _blockRecord = (Await Model.SaveBlockRecord(
                                New BlockRecordGeneralLedger With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _journalVoucher.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(Eresources.RegistroBloqueado, Eform.Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                        End If

                        If _journalVoucher.Status = 1 Then
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        Else
                            If _journalVoucher.Status = 2 Then
                                BarraBotones.PrepareToolbar(eAction.OnlyDisconfirm)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                            End If
                            ReadOnlyControls(True)
                        End If

                        Me.BarraBotones.SetDocuments(_journalVoucher.Id, Me.Tag.ToString(), Nothing, GetType(JournalVouchers).Name)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
                        Me.BarraBotones.PrintReport(PrintReportAction.None, _journalVoucher.Id, 0, _journalVoucher.Id, BarraBotones.OperatingUnitValue)

                        ActionsOnControls = True
                        If _journalVoucher?.Status = 2 Then
                            journalVourcherHologationBs.RunWorkerAsync()
                        End If
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)

                        If (MessageIndigo.Show(ResourceManager.GetString("NoExist", "Accounting"), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                            NewJournalVoucher()
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ImportarInformacion) = False
                            indDtDateDocument.Focus()
                        Else
                            indGlConsecutive.EditValue = Nothing
                            Deshacer()
                            indGlConsecutive.Focus()
                        End If
                    End If

                    INDlyJournalVoucher.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Deshacer()
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Carga los detalles del comprobante
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadDetails() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using Model As New MDocumentAccount(Me.Tag)
                                             Dim objLock As New Object()
                                             _listJournalVoucherDetails = New List(Of JournalVoucherDetails)
                                             Dim JournalVoucherDetailXPO = Model.ListJournalVourcherDetailsByJournalVoucherId(_journalVoucher.Id)
                                             Parallel.ForEach(JournalVoucherDetailXPO, Sub(item)
                                                                                           Dim journalVoucherDetail As New JournalVoucherDetails
                                                                                           With journalVoucherDetail
                                                                                               .Id = item.Id
                                                                                               .IdAccounting = item.IdAccounting.Id
                                                                                               .IdMainAccount = item.IdMainAccount.Id
                                                                                               .CodeNameMainAccount = item.IdMainAccount.NumberName
                                                                                               If item.IdThirdParty IsNot Nothing Then
                                                                                                   .IdThirdParty = item.IdThirdParty.Id
                                                                                                   .CodeNameThirdParty = item.IdThirdParty.NitName
                                                                                               End If
                                                                                               If item.IdCostCenter IsNot Nothing Then
                                                                                                   .IdCostCenter = item.IdCostCenter.Id
                                                                                                   .CodeNameCostCenter = item.IdCostCenter.Code + " - " + item.IdCostCenter.Name
                                                                                               End If
                                                                                               .Nature = If(item.DebitValue > 0, 1, 2)
                                                                                               .DebitValue = item.DebitValue
                                                                                               .CreditValue = item.CreditValue
                                                                                               .Detail = item.Detail
                                                                                               .BaseValue = item.BaseValue
                                                                                               If item.IdRetention IsNot Nothing Then
                                                                                                   .IdRetention = item.IdRetention.Id
                                                                                                   .CodeNameRetention = item.IdRetention.CodeName
                                                                                                   .RetentionRate = item.RetentionRate
                                                                                                   .BaseValue = item.BaseValue
                                                                                                   .BillingValue = item.BillingValue
                                                                                               End If
                                                                                           End With

                                                                                           SyncLock objLock
                                                                                               journalVoucherDetail.MarkAsUnchanged()
                                                                                               _listJournalVoucherDetails.Add(journalVoucherDetail)
                                                                                           End SyncLock
                                                                                       End Sub)

                                             indGcDetail.SafeInvoke(Sub(x) x.DataSource = _listJournalVoucherDetails)
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' funcion que retorna el documento a indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then     
            _doc = New IndexedDocument2()
            With _doc
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._journalVoucher.Consecutive)
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._journalVoucher.Consecutive, Me.indglTypeDocument.Text, "Contabilidad", indDtDateDocument.Text)
                .IdEntity = "$#" & Me.Tag & "_" & Me._journalVoucher.Consecutive & "#$"
                .IdForm = Me.Tag
                .CreationDate = dateServer
                .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
                .Update = dateServer
                .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
                .DocumentType = IndexedDocumentType.File
                .Extension = ""
            End With
        Else
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._journalVoucher.Consecutive)
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._journalVoucher.Consecutive, Me.indglTypeDocument.Text, "Contabilidad", indDtDateDocument.Text)
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
        End If

        Return Me._doc
    End Function

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 AndAlso _blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MAccountClass(Me.Tag)
                Await Model.DeleteBlockRecord(_blockRecord)
            End Using
            _blockRecord = Nothing
        End If
    End Function

    ''' <summary>
    ''' Metodo para validar la cabecera del documento
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateHeader() As Boolean
        If String.IsNullOrEmpty(INDSleLegalBook.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un libro contable"
            Return False
        End If
        If String.IsNullOrEmpty(indglTypeDocument.EditValue) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectTypeDocument", "Accounting")
            Return False
        End If
        If indDtDateDocument.EditValue Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectDateDocument", "Accounting")
            Return False
        End If
        If String.IsNullOrEmpty(indMemoObservation.Text) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe escribir una observación"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' metodo para crear la cabecera del documento contable
    ''' </summary>
    Private Sub AssigningValues()
        With _journalVoucher
            .LegalBookId = INDSleLegalBook.EditValue
            .IdJournalVoucher = IDocumente
            .VoucherDate = DateDocument
            .Detail = indMemoObservation.Text
            .BookCurrencyId = Me.BookCurrencyId
            .EntityName = If(.EntityName Is Nothing, _journalVoucher.GetType().Name, .EntityName)

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Loads the form add item.
    ''' </summary>
    Private Sub LoadFormAddItem(item As JournalVoucherDetails)
        _journalVoucherDetail = item

        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmPopupVoucherAccount(_journalVoucher)
            AddHandler Formulario.AddDocumentAccounting, AddressOf AddDocumentAccounting
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Formulario.listDetail = _listJournalVoucherDetails
            Formulario.RowItem = _journalVoucherDetail
            Formulario.LegalBookId = INDSleLegalBook.EditValue
            Formulario.CultureId = Me.CodeCurrency.GetCultureId
            Dim transparent As New FrmTransparent(Formulario, False)
            Formulario.NameDocument = indglTypeDocument.Text
            Me.Cursor = System.Windows.Forms.Cursors.Default
            Formulario.formParent = Me
            Formulario.Size = New System.Drawing.Size(800, 700)
            transparent.ShowDialog(Me.MdiParent)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para agregar un detalle del documeto contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AddDocumentAccounting(sender As Object, e As AddDocumentAccountingEventArgs)
        If _journalVoucherDetail Is Nothing Then
            _listJournalVoucherDetails = If(_listJournalVoucherDetails Is Nothing, New List(Of JournalVoucherDetails), _listJournalVoucherDetails)
            _listJournalVoucherDetails.Add(e.DocumentAccounting)
        End If
        indGcDetail.DataSource = Nothing
        indGcDetail.DataSource = _listJournalVoucherDetails
        ctrDebitCredit.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' metodo para setear el formato de la moneda en las columnas que se le indiquen
    ''' </summary>
    ''' <param name="CultureId"></param>
    ''' <param name="Column"></param>
    Private Sub FormatGrid(CultureId As Integer, ByRef Column As GridColumn)
        Dim fInfo As FormatInfo = Column.DisplayFormat
        fInfo.FormatType = FormatType.Custom
        fInfo.FormatString = "c2"
        fInfo.Format = CultureNumbertFormat
        Column.SummaryItem.Format = CultureNumbertFormat
    End Sub

#End Region

#Region "Import File & Copy - Paste"

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim _myStream As String = Nothing

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
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Dim listErrosImportFile As List(Of String())

    ''' <summary>
    ''' Método de importación de archivo excel
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ImportFile() As Task
        If ValidateHeader() = False Then
            Exit Function
        End If
        If _journalVoucherId > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se puede importar información en un registro previamente creado"
            Exit Function
        End If
        If _listJournalVoucherDetails IsNot Nothing AndAlso _listJournalVoucherDetails.Count > 0 Then
            If MessageIndigo.Show("Se perderan los datos que estan en la rejilla, desea continuar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            End If
        End If

        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Function
        End If

        Try
            'obtengo la rura del archivo
            _myStream = openFileDialog1.FileName
            If (_myStream Is Nothing OrElse _myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            AsyncLoader(True)

            'Validamos los datos de excel y armamos el listado que se envia para poder pegar en la rejilla de facturas
            If MessageIndigo.Show("Desea continuar con el proceso de importación", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                _listJournalVoucherDetails = New List(Of JournalVoucherDetails)
                indGcDetail.DataSource = Nothing
                indGcDetail.DataSource = _listJournalVoucherDetails
                ctrDebitCredit.RefreshDebitCredit()

                Await LoadImportFile()

                If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = $"El archivo presento error en {listErrosImportFile.Count} registros y no se podra confirmar el documento"

                    _listJournalVoucherDetails = New List(Of JournalVoucherDetails)
                    indGcDetail.DataSource = Nothing
                    indGcDetail.DataSource = _listJournalVoucherDetails
                    ctrDebitCredit.RefreshDebitCredit()

                    Dim ErrorsExcel As New SpreadsheetControl
                    ErrorsExcel.CreateNewDocument()
                    ErrorsExcel.Document.Worksheets.ActiveWorksheet = ErrorsExcel.Document.Worksheets(0)
                    Dim worksheet As Worksheet = ErrorsExcel.Document.Worksheets.ActiveWorksheet

                    worksheet.Cells(0, 0).Value = "Cuenta Contable"
                    worksheet.Cells(0, 1).Value = "Tercero"
                    worksheet.Cells(0, 2).Value = "Centro de Costo"
                    worksheet.Cells(0, 3).Value = "Naturaleza"
                    worksheet.Cells(0, 4).Value = "Observación"
                    worksheet.Cells(0, 5).Value = "Valor"
                    worksheet.Cells(0, 6).Value = "Retencion"
                    worksheet.Cells(0, 7).Value = "Valor Facturado"
                    worksheet.Cells(0, 8).Value = "Valor Base"
                    worksheet.DefaultColumnWidth = 300

                    Dim rows = 1
                    For Each dato As String() In listErrosImportFile
                        Dim Columns = 0
                        For Each item In dato
                            worksheet.Cells(rows, Columns).Value = item
                            Columns += 1
                        Next
                        rows += 1
                    Next

                    Dim fileName As String = System.IO.Path.GetTempPath() & indMemoObservation.EditValue & ".xlsx"
                    ErrorsExcel.SaveDocument(fileName)
                    System.Diagnostics.Process.Start(fileName)
                Else
                    indGcDetail.DataSource = Nothing
                    indGcDetail.DataSource = _listJournalVoucherDetails
                    ctrDebitCredit.RefreshDebitCredit()

                    _journalVoucher.Status = 2
                    _varImp = 4
                    Guardar()
                End If
            End If

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Metodo que se encarga de validar el archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadImportFile() As Task
        listErrosImportFile = New List(Of String())()

        Dim rowsLocal As RowCollection = Await Task.Run(Function()
                                                            Dim wb As New Workbook()
                                                            wb.LoadDocument(_myStream)
                                                            Return wb.Worksheets(0).Rows
                                                        End Function)

        rows = rowsLocal
        If rows.LastUsedIndex <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
            Return
        End If

        If progress Is Nothing OrElse progress.IsDisposed Then
            progress = New CtrProgress()
            progress.SetInfoFunction(AddressOf getInfo)
            progress.Dock = DockStyle.Fill
        End If

        progress.PrintInfo()

        AdditionalControlPanel.SafeInvoke(Sub(x)
                                              x.Controls.Clear()
                                              x.Controls.Add(progress)
                                          End Sub)

        UpdateProgressBarTitle("Registros Validados")

        totalProcessedItems = 0
        totalItems = rows.LastUsedIndex
        Dim indexSend = 0

        Using trasparent = New FrmTransparent(Nothing, False)
            trasparent.SafeInvoke(Sub(f) f.Show(Me))
            Try
                While (totalItems + 1) > totalProcessedItems
                    Dim quantityDetailsToProcess =
                    If((totalItems + 1) < (totalProcessedItems + itemsSend),
                       (totalItems + 1) - totalProcessedItems,
                       itemsSend)

                    indexSend = totalProcessedItems
                    totalProcessedItems += quantityDetailsToProcess

                    listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                    SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)

                    Dim result = Await Task.Run(Function()
                                                    Using model As New MDocumentAccount(Me.Tag.ToString())
                                                        Return model.ValidateFileJournalVoucherDetails(INDSleLegalBook.EditValue, listRows.ToList())
                                                    End Using
                                                End Function)

                    If result.ListMessageResult IsNot Nothing AndAlso result.ListMessageResult.Any() Then
                        listErrosImportFile.AddRange(result.ListMessageResult)
                    End If
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                        _listJournalVoucherDetails.AddRange(result.ObjectEmbbeded)
                    End If

                    progress.SafeInvoke(Sub(x) x.PrintInfo())
                End While
            Finally
                trasparent.SafeInvoke(Sub(f) f.Close())
            End Try
        End Using

        RestoreControlPanel()
    End Function

    ''' <summary>
    ''' metodo para establecer las filas que se van a enviar a procesar
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    ''' <remarks></remarks>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(9)})
                                              End SyncLock
                                          End Sub)
    End Sub

#End Region

#Region "SaveJournalVoucher"

    ''' <summary>
    ''' Método que gestiona el guardado de comprobantes contables
    ''' </summary>
    ''' <returns></returns>
    Private Async Function SaveJournalVoucher() As Task
        Try
            AsyncLoader(True)

            Dim status = Me._journalVoucher.Status

            ' Inicializa el control de progreso en la UI
            Await ShowProgressBarAsync()

            If {1, 2}.Contains(Me._journalVoucher.Status) Then
                ' Filtra los detalles válidos
                Dim detailsToSend = PrepareJournalVoucherDetails()
                If detailsToSend.Any() Then
                    UpdateProgressBarTitle("Registros Guardados")
                    ' Asigna la colección filtrada a la entidad y guarda
                    AssignDetailsToVoucher(detailsToSend)
                End If
            End If
            Dim result As Object = Nothing
            Using model As New MDocumentAccount(Me.Tag.ToString())
                result = Await model.SaveDocumentAccounting(Me._journalVoucher)
            End Using

            HandleSaveResult(result)
            UpdateProgressBar()
            RestoreControlPanel()
            UpdateIndexedDocument(Me.BarraBotones._listDocuments)
            PrintAfterSave()
            PostSaveActions(status)
        Catch ex As Exception
            ShowExceptionMessage(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Function

    ' Métodos auxiliares

    ' 1. Mostrar el progress bar
    Private Async Function ShowProgressBarAsync() As Task
        Await Task.Run(Sub()
                           progress = New CtrProgress
                           progress.SetInfoFunction(AddressOf getInfo)
                           progress.PrintInfo()
                           progress.Dock = DockStyle.Fill
                       End Sub)
        AdditionalControlPanel.SafeInvoke(Sub(x)
                                              x.Controls.Clear()
                                              x.Controls.Add(progress)
                                          End Sub)
    End Function

    ' 2. Filtrar y preparar detalles válidos
    Private Function PrepareJournalVoucherDetails() As List(Of JournalVoucherDetails)
        Return If(_listJournalVoucherDetails IsNot Nothing,
            _listJournalVoucherDetails.Where(Function(o) o.ChangeTracker.State <> ObjectState.Deleted).ToList(),
            New List(Of JournalVoucherDetails)())
    End Function

    ' 3. Asignar detalles al comprobante
    Private Sub AssignDetailsToVoucher(details As List(Of JournalVoucherDetails))
        _journalVoucher.JournalVoucherDetails.Clear()
        For Each item In details
            _journalVoucher.JournalVoucherDetails.Add(item)
        Next
    End Sub

    ' 4. Actualizar el título del progress bar
    Private Sub UpdateProgressBarTitle(title As String)
        progress.SafeInvoke(Sub(x)
                                x.SetTitle = title
                                x.PrintInfo()
                            End Sub)
    End Sub

    ' 5. Manejar el resultado del guardado
    Private Sub HandleSaveResult(result As Object)
        If result IsNot Nothing AndAlso result.StateResult Then
            Me.Consecutive = result.ObjectEmbbeded.Consecutive
            Me._journalVoucher = result.ObjectEmbbeded
            Mensaje(EeventViewerImages.Informacion) = result.Message
            _journalVoucher = result.ObjectEmbbeded
        Else
            Mensaje(EeventViewerImages.Advertencia) = result?.Message
        End If
    End Sub

    ' 6. Actualizar el progress bar
    Private Sub UpdateProgressBar()
        progress.SafeInvoke(Sub(x) x.PrintInfo())
    End Sub

    ' 7. Restaurar el panel de controles adicional
    Private Sub RestoreControlPanel()
        AdditionalControlPanel.SafeInvoke(Sub(x)
                                              x.Controls.Clear()
                                              x.Controls.Add(ctrDebitCredit)
                                          End Sub)
    End Sub

    ' 8. Imprimir después de guardar
    Private Sub PrintAfterSave()
        Select Case _varImp
            Case 1
                Me.BarraBotones.PrintReport(PrintReportAction.Create, _journalVoucher.Id, 0, _journalVoucher.Id, BarraBotones.OperatingUnitValue)
            Case 2
                Me.BarraBotones.PrintReport(PrintReportAction.Update, _journalVoucher.Id, 0, _journalVoucher.Id, BarraBotones.OperatingUnitValue)
            Case 3
                Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _journalVoucher.Id, 0, _journalVoucher.Id, BarraBotones.OperatingUnitValue)
            Case 4
                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _journalVoucher.Id, 0, _journalVoucher.Id, BarraBotones.OperatingUnitValue)
        End Select
    End Sub

    ' 9. Acciones posteriores al guardado
    Private Async Sub PostSaveActions(Status As Integer)
        If Status = 4 Then
            Await Me.LoadControls()
        Else
            Me.Deshacer()
        End If
    End Sub

    ' 10. Mostrar mensaje de excepción
    Private Sub ShowExceptionMessage(ex As Exception)
        Mensaje(EeventViewerImages.Advertencia) = Infrastructure.CrossCutting.Base.Utils.GetInnerExceptionMessageToString(ex)
    End Sub

#End Region

#Region "Homologation"

    ''' <summary>
    ''' Listado de comprobante posibles a homologar
    ''' </summary>
    Dim journalHomologations As XPCollection(Of JournalVouchersXpo)

    ''' <summary>
    ''' Listado de libros contables
    ''' </summary>
    Dim officialBooks As XPCollection

    ''' <summary>
    ''' Asyncrono para lanzar el proceso de homologación
    ''' </summary>
    ''' <remarks></remarks>
    Dim journalVourcherHologationBs As New BackgroundWorker

    ''' <summary>
    ''' Consulta información sobre las homologaciones de comprobantes contables y libros contables relacionados a un comprobante
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub journalVourcherHologationBs_DoWork(sender As Object, e As DoWorkEventArgs)
        Using Model As New MDocumentAccount(MyTag)
            journalHomologations = Model.ListJournalVourcherHomologations(_journalVoucher.AccountingMovementId, _journalVoucher.Id)
            officialBooks = Model.ListBookJournalVoucherHomologations(_journalVoucher.LegalBookId)
        End Using
    End Sub

    ''' <summary>
    ''' Procesa la información obtenida de las homologaciones de comprobantes contables y libros contables realizada en el evento anterior 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub journalVourcherHologationBs_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        Dim listJournalTmp = New List(Of JournalVouchers)
        For Each item As JournalVouchersXpo In journalHomologations
            Dim journal As New JournalVouchers
            With journal
                .AccountingMovementId = item.AccountingMovementId
                .Consecutive = item.Consecutive
                .LegalBookId = item.LegalBookId.Id
                .IdJournalVoucher = item.IdJournalVoucher.Id
                .VoucherDate = item.VoucherDate
            End With
            For Each detail As JournalVoucherDetailsXpo In item.GeneralLedger_JournalVoucherDetailsCollection
                Dim journalDetail As New JournalVoucherDetails
                With journalDetail
                    If detail.IdMainAccount IsNot Nothing Then
                        .CodeNameMainAccount = detail.IdMainAccount.NumberName
                    End If
                    If detail.IdThirdParty IsNot Nothing Then
                        .CodeNameThirdParty = detail.IdThirdParty.NitName
                    End If
                    If detail.IdCostCenter IsNot Nothing Then
                        .CodeNameCostCenter = detail.IdCostCenter.CodeName
                    End If
                    .DebitValue = detail.DebitValue
                    .CreditValue = detail.CreditValue
                    .BaseValue = detail.BaseValue
                End With
                journal.JournalVoucherDetails.Add(journalDetail)
            Next
            listJournalTmp.Add(journal)
        Next

        If officialBooks.Count > 0 Then
            BarraBotones.SetDataSourceJuornalVoucher(listJournalTmp, officialBooks)
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.JournalVourcherHomologation) = False
            BarraBotones.HomologationsCount = journalHomologations.Count
        Else
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.JournalVourcherHomologation) = True
        End If
    End Sub

#End Region

End Class