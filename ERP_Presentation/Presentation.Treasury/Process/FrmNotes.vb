'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 16-06-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Treasury.MVP
Imports Presentation.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports System.Text
Imports Presentation.Accounting.MVP
Imports DevExpress.Xpo
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports DevExpress.Data.Linq
Imports DevExpress.XtraSpreadsheet
Imports DevExpress.Spreadsheet
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

''' <summary>
''' Formulario de Notas de Tesoreria
''' </summary>
Public Class FrmNotes
    Implements INotes, ICustomizableForm

#Region "Constructor"

    ''' <summary>
    ''' Inicia el control de debitos y creditos
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        ctrTmp = New CtrAdvanceCxC()
        Dim toolTip = New DevExpress.Utils.SuperToolTip()
        Dim toolTipItemBody As New DevExpress.Utils.ToolTipItem()
        toolTipItemBody.Text = "Valor de la Nota"
        toolTip.Items.Add(toolTipItemBody)
        ctrTmp.LabelValue.SuperTip = toolTip
        ctrTmp.SetAdvance(AddressOf getValueNote)
        ctrTmp.PrintValue()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
        IndigoGridControl1.SetHideNoRecords(INDgcNoteConcept, True)
        CalendarDaysAllowed = False
    End Sub

#End Region

#Region "Properties and Variables"

    Private _flagEntityAccount As Boolean
    Private _flagCashRegister As Boolean
    Private _flagCostCenter As Boolean

    ''' <summary>
    ''' The path control nature
    ''' </summary>
    Dim pathControlNature As String = "Naturaleza: {0}"

    ''' <summary>
    ''' formulario de comprobante de egreso
    ''' </summary>
    Dim frmVoucherTransaction As FrmVoucherTransaction

    Private frmCashReceipt As FrmCashReceipts

    Private frmConsignment As FrmConsignment

    Private frmCrossAccount As FrmVoucherTransactionCrossing

    Private fechaMinima As DateTime?


    Private _CalendarDaysAllowed As Boolean
    Public Property CalendarDaysAllowed() As Boolean
        Get
            Return _CalendarDaysAllowed
        End Get
        Set(ByVal value As Boolean)
            _CalendarDaysAllowed = value
            If Not value Then
                fechaMinima = Nothing
            End If
            LoadCalendarControl()
        End Set
    End Property

    ''' <summary>
    ''' Control para manejar los debitos y créditos
    ''' </summary>
    Public ctrTmp As CtrAdvanceCxC

    ''' <summary>
    ''' bandera que verifica si el popup se abre
    ''' </summary>
    Private _stateOpenPopUpVoucherTransaction As Boolean

    Private _stateOpenPopUpCashReceipt As Boolean

    Private _stateOpenPopUpConsignment As Boolean

    Private _stateOpenPopUpCrossAccount As Boolean

    ''' <summary>
    ''' Constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Constante que contiene el nombre del modulo de contabilidad
    ''' </summary>
    Public Const NAME_MODULE_ACCOUNTING As String = "Accounting"

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Contiene el listado del tipo de nota
    ''' </summary>
    Dim ListNoteType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de las naturalezas
    ''' </summary>
    Dim ListNature As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    Public Property MainAccountId As Integer? Implements INotes.MainAccountId

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private myStream As String = Nothing
    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private listErrosImportFile As List(Of String())
    ''' <summary>
    ''' coleccion de filas que se van a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Private rows As RowCollection
    ''' <summary>
    ''' Lista de errores desde el import
    ''' </summary>
    Private _errorListImport As List(Of String)

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 300
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Private listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    Private _NoteConceptListImport As List(Of TreasuryNoteDetail)
    ''' <summary>
    ''' Obtiene o establece el listado de conceptos de nota
    ''' </summary>
    ''' <value>
    ''' The note concept list.
    ''' </value>
    Public Property NoteConceptList As List(Of TreasuryNoteDetail)
        Get
            Return CType(INDgcNoteConcept.DataSource, List(Of TreasuryNoteDetail))
        End Get
        Set(value As List(Of TreasuryNoteDetail))
            INDgcNoteConcept.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the value.
    ''' </summary>
    ''' <value>
    ''' The value.
    ''' </value>
    Public Property Value As Decimal? Implements INotes.Value

    ''' <summary>
    ''' Obtiene o establece el número del documento
    ''' </summary>
    ''' <value>
    ''' The code notes.
    ''' </value>
    Public Property Code As String Implements INotes.Code
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
    ''' Obtiene o establece el tipo de la nota
    ''' </summary>
    ''' <value>
    ''' The type of the note.
    ''' </value>
    Public Property NoteType As Short Implements INotes.NoteType
        Get
            Return INDgleNoteType.EditValue
        End Get
        Set(value As Short)
            INDgleNoteType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la nota
    ''' </summary>
    Public Property NoteDate As Date Implements INotes.NoteDate
        Get
            Return INDdeDate.EditValue
        End Get
        Set(value As Date)
            INDdeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta bancaria
    ''' </summary>
    ''' <value>
    ''' The entity bank account.
    ''' </value>
    Public Property EntityBankAccountId As Integer? Implements INotes.EntityBankAccountId
        Get
            Return INDsleEntityBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la caja
    ''' </summary>
    ''' <value>
    ''' The cash.
    ''' </value>
    Public Property CashRegisterId As Integer? Implements INotes.CashRegisterId
        Get
            Return INDsleCash.EditValue
        End Get
        Set(value As Integer?)
            INDsleCash.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Property CrossAccountId As Integer? Implements INotes.CrossAccountId
        Get
            Return INDSleCrossAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleCrossAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la nota
    ''' </summary>
    ''' <value>
    ''' The date time.
    ''' </value>
    Public Property DateNote As Date? Implements INotes.DateNote
        Get
            Return INDdeDate.EditValue
        End Get
        Set(value As Date?)
            INDdeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Public Property CostCenterId As Integer? Implements INotes.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el detalle
    ''' </summary>
    ''' <value>
    ''' The detail.
    ''' </value>
    Public Property Detail As String Implements INotes.Description
        Get
            Return INDmeDetail.Text
        End Get
        Set(value As String)
            INDmeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza de la nota
    ''' </summary>
    ''' <value>
    ''' The nature.
    ''' </value>
    Public Property Nature As Short? Implements INotes.Nature

    ''' <summary>
    ''' Obtiene o establece el datasource de comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The voucher transaction datasource.
    ''' </value>
    Public Property VoucherTransactionDatasource As XPInstantFeedbackSource Implements INotes.VoucherTransactionDatasource
        Get
            Return CType(INDsleVoucherTransaction.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleVoucherTransaction.Properties.DataSource = value
        End Set
    End Property

    Public Property CashReceiptDatasource As XPInstantFeedbackSource Implements INotes.CashReceiptDatasource
        Get
            Return INDsleCashReceipt.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCashReceipt.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource para el control múltiple de recibos de caja (tipo nota 8)
    ''' </summary>
    Public Property CashReceiptsDatasource As XPBaseCollection Implements INotes.CashReceiptsDatasource
        Get
            Return TryCast(INDsleCashReceipts.Properties.DataSource, XPBaseCollection)
        End Get
        Set(value As XPBaseCollection)
            INDsleCashReceipts.Properties.DataSource = value
        End Set
    End Property

    Public Property ConsignmentDatasource As XPInstantFeedbackSource Implements INotes.ConsignmentDatasource
        Get
            Return INDsleConsignment.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleConsignment.Properties.DataSource = value
        End Set
    End Property

    Public Property CrossAccountDatasource As XPInstantFeedbackSource Implements INotes.CrossAccountDatasource
        Get
            Return INDSleCrossAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCrossAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de comprobante de egreso
    ''' </summary>
    Public Property VoucherTransactionId As Integer? Implements INotes.VoucherTransactionId
        Get
            Return INDsleVoucherTransaction.EditValue
        End Get
        Set(value As Integer?)
            INDsleVoucherTransaction.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de comprobante de egreso
    ''' </summary>
    Public Property CashReceiptId As Integer? Implements INotes.CashReceiptId
        Get
            Return INDsleCashReceipt.EditValue
        End Get
        Set(value As Integer?)
            INDsleCashReceipt.EditValue = value
        End Set
    End Property

    Public Property ConsignmentId As Integer? Implements INotes.ConsignmentId
        Get
            Return INDsleConsignment.EditValue
        End Get
        Set(value As Integer?)
            INDsleConsignment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [state notes].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state notes]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Status As String Implements INotes.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property CashDatasource As LinqInstantFeedbackSource Implements INotes.CashDatasource
        Get
            Return CType(INDsleCash.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleCash.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements INotes.CostCenterDatasource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Public Property EntityAccountDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements INotes.EntityAccountDatasource
        Get
            Return CType(INDsleEntityBankAccount.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntityBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements INotes.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As String Implements INotes.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequense As TreasurySequence Implements INotes.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            _sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' The presenter
    ''' </summary>
    Dim _presenter As PNotes

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordTreasury

    ''' <summary>
    ''' entidad de notas de tesoreria
    ''' </summary>
    Private _treasuryNote As TreasuryNote

    Private _currencyId As Integer?
    Public Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer?
        Get
            Return _currencyId
        End Get
        Set(value As Integer?)
            _currencyId = value
            SetCurrencyUI(_currencyAbbreviation)
        End Set
    End Property

    ''' <summary>
    ''' Lista recibos de caja seleccionados para tipo de nota 8 
    ''' </summary>
    Dim ListTreasuryNoteCashReceiptsDetail As New List(Of TreasuryNoteCashReceiptsDetail)

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Anula el documento
    ''' </summary>
    Public Async Sub Anular()
        If Me._treasuryNote IsNot Nothing AndAlso Me._treasuryNote.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                _treasuryNote.Status = 3
                Try
                    Using Model As New MTreasuryNote(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.SaveTreasuryNote(Me._treasuryNote, False, Me._idCurrentSequence)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me._treasuryNote = result.ObjectEmbbeded
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _treasuryNote.Id, 0, _treasuryNote.Id)
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            If result.Message IsNot Nothing Then
                                generateListError(result.Message)
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' Confirma el comprobante de egreso
    ''' </summary>
    ''' <exception cref="System.NotImplementedException"></exception>
    Private Async Sub Confirmar()
        Dim resultOption As DialogResult
        resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessageIfUpdate"), MessageType.Question, Me.Text, Botones.SiNo)
        If resultOption = System.Windows.Forms.DialogResult.Yes Then
            Try
                Using Model As New MTreasuryNote(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim Result = Await Model.ConfirmTreasuryNote(Me._treasuryNote.Id, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If NoteType = 3 AndAlso Result.MessageResult IsNot Nothing AndAlso Result.MessageResultAux.Count > 0 Then
                            Mensaje(EeventViewerImages.Informacion) = Result.MessageResult(0)
                        Else
                            Dim consecutive = Result.ObjectEmbbeded
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("DocumentConfirmWithConsecutive"), consecutive)
                        End If
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _treasuryNote.Id, 0, _treasuryNote.Id)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If Result.Message IsNot Nothing Then
                            generateListError(Result.Message)
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Guardars the specified with confirm.
    ''' </summary>
    ''' <param name="withConfirm">if set to <c>true</c> [with confirm].</param>
    Public Async Sub Guardar(ByVal withConfirm As Boolean)
        If ValidateFields() = False Then
            Exit Sub
        End If
        If _idOperativeUnit = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una Unidad Operativa"
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MTreasuryNote(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveTreasuryNote(Me._treasuryNote, withConfirm, Me._idCurrentSequence)
                If Result.StateResult = True Then
                    Me._treasuryNote = Result.ObjectEmbbeded
                    If withConfirm Then
                        If String.IsNullOrEmpty(Result.Message) Then
                            If Not {3, 4, 5, 6, 7, 8}.Contains(NoteType) Then
                                Dim typeJournal As ActionResult(Of JournalVoucherTypes)
                                Using modelType As New MDocumentType(Me.Tag)
                                    typeJournal = modelType.GetJournalVoucherById(CType(Result.MessageResult(1), Integer))
                                End Using
                                If _treasuryNote.Id <> 0 Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("UpdateAndConfirmSatisfactory"), Result.ObjectEmbbeded.Code, String.Concat(typeJournal.ObjectEmbbeded.Code, " - ", typeJournal.ObjectEmbbeded.Name), Result.MessageResult(0))
                                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _treasuryNote.Id, 0, _treasuryNote.Id)
                                Else
                                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmSatisfactory"), Result.ObjectEmbbeded.Code, String.Concat(typeJournal.ObjectEmbbeded.Code, " - ", typeJournal.ObjectEmbbeded.Name), Result.MessageResult(0))
                                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _treasuryNote.Id, 0, _treasuryNote.Id)
                                End If
                            Else
                                'Desconfirmacion y otros tipos especiales
                                If NoteType = 3 Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format("El Comprobante de Egreso se ha reversado con éxito Generando:" + vbCrLf + "Nota {0} " + vbCrLf + "{1}", Result.ObjectEmbbeded.Code, Result.MessageResult(1))
                                ElseIf NoteType = 4 Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format("El Recibo de Caja se ha reversado con éxito Generando" + vbCrLf + "Nota {0} " + vbCrLf + "{1}", Result.ObjectEmbbeded.Code, Result.MessageResult(1))
                                ElseIf NoteType = 5 Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format("La Consignación se ha reversado con éxito Generando" + vbCrLf + "Nota {0} " + vbCrLf + "{1}", Result.ObjectEmbbeded.Code, Result.MessageResult(1))
                                ElseIf NoteType = 6 Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format("El Cruce CxC vs CxP se ha reversado con éxito Generando" + vbCrLf + "Nota {0} " + vbCrLf + "{1}", Result.ObjectEmbbeded.Code, Result.MessageResult(1))
                                ElseIf NoteType = 7 Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format("El Recibo de Caja se ha Devuelto con éxito Generando" + vbCrLf + "Nota {0} " + vbCrLf + "{1}", Result.ObjectEmbbeded.Code, Result.MessageResult(1))
                                ElseIf NoteType = 8 Then
                                    Mensaje(EeventViewerImages.Informacion) = String.Format("Los Gastos de Conciliación de Tarjetas se han registrado con éxito" + vbCrLf + "Nota {0}", Result.ObjectEmbbeded.Code)
                                End If
                            End If
                        Else
                            If _treasuryNote.Id <> 0 Then
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("UpdateButNotConfirmed"), Result.ObjectEmbbeded.Code, Result.Message)
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, _treasuryNote.Id, 0, _treasuryNote.Id)
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SavedButNotConfirmed", NAME_MODULE), Result.ObjectEmbbeded.Code, Result.Message)
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, _treasuryNote.Id, 0, _treasuryNote.Id)
                            End If
                        End If
                    Else
                        If _treasuryNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequence.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf _treasuryNote.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _treasuryNote.Id, 0, _treasuryNote.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _treasuryNote.Id, 0, _treasuryNote.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If Result.Message IsNot Nothing Then
                        generateListError(Result.Message)
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Genera el mensaje de error
    ''' </summary>
    ''' <param name="errors">The errors.</param>
    Private Sub generateListError(errors As String)
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewNote()
        End If
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadCalendarControl()
        Using ModelBase As New MSettingsTreasury(MyTag)
            If CalendarDaysAllowed Then
                Dim setting = ModelBase.GetSettingsTreasuryByIdUnitOperativeSimple(BarraBotones.OperatingUnitValue)
                If setting IsNot Nothing Then
                    INDdeDate.Properties.MinValue = CDate(GetDateServer()).AddDays(-setting.ObjectEmbbeded.NumberDayBank)
                    If fechaMinima IsNot Nothing Then
                        If DateDiff(DateInterval.Day, INDdeDate.Properties.MinValue, fechaMinima.GetValueOrDefault()) > 0 Then
                            INDdeDate.Properties.MinValue = fechaMinima.GetValueOrDefault()
                        End If
                    End If
                    INDdeDate.Properties.ReadOnly = False
                End If
            Else
                INDdeDate.Properties.MinValue = Date.MinValue
                INDdeDate.EditValue = CDate(GetDateServer())
                INDdeDate.Focus()
                INDdeDate.Properties.ReadOnly = True
            End If
        End Using
    End Function

    ''' <summary>
    ''' Validates the fields.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        If ValidateControls() Then
            If Not {4, 5, 7}.Contains(NoteType) AndAlso Value IsNot Nothing Then
                If Value = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidNoteValue", NAME_MODULE))
                    Return False
                End If
            End If
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Edits the note concept.
    ''' </summary>
    Private Sub EditNoteConcept()
        ctrNoteConcept.CleanControls()
        ctrNoteConcept.IsEdit = True
        ctrNoteConcept.NoteConcept = CType(INDgvNoteConcept.GetFocusedRow(), TreasuryNoteDetail)
        ctrNoteConcept.LoadControls()
        INDpeNoteConcept.ShowPopup()
    End Sub

    ''' <summary>
    ''' Deletes the note concept.
    ''' </summary>
    Private Sub DeleteNoteConcept()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim noteConcept As TreasuryNoteDetail = CType(INDgvNoteConcept.GetFocusedRow(), TreasuryNoteDetail)
            If noteConcept.Id > 0 Then
                noteConcept.MarkAsDeleted()
                NoteConceptList = _treasuryNote.TreasuryNoteDetail.ToList()
            Else
                NoteConceptList.Remove(noteConcept)
            End If

            Dim resultValidateConcept = TreasuryStaticServices.ValidateValueNoteConcept(NoteConceptList)
            Value = CType(resultValidateConcept.ObjectEmbbeded, Object()).ElementAt(1)
            If resultValidateConcept.StateResult Then
                Nature = CShort(CType(resultValidateConcept.ObjectEmbbeded, Object()).ElementAt(0))
            Else
                Mensaje(EeventViewerImages.Advertencia) = resultValidateConcept.Message
            End If
            INDgcNoteConcept.RefreshDataSource()
            If _treasuryNote.ChangeTracker.State <> ObjectState.Added Then
                _treasuryNote.MarkAsModified()
            End If
            INDgleNoteType.Properties.ReadOnly = INDgcNoteConcept.DataSource IsNot Nothing AndAlso CType(INDgcNoteConcept.DataSource, List(Of TreasuryNoteDetail)).Count > 0
            ctrTmp.Title = If(Nature = 1, String.Format(pathControlNature, ResourceManager.GetString("AccountNatureDebit")), String.Format(pathControlNature, ResourceManager.GetString("AccountNatureCredit")))
            ctrTmp.PrintValue()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Consulta el total de los debitos y creditos
    ''' </summary>
    ''' <returns></returns>
    Private Function getValueNote() As Decimal
        If NoteType = 3 AndAlso frmVoucherTransaction IsNot Nothing Then
            Value = frmVoucherTransaction.Value
            If frmVoucherTransaction.TaxByMilValue IsNot Nothing AndAlso frmVoucherTransaction.TaxByMilValue <> 0 Then
                Value += frmVoucherTransaction.TaxByMilValue
            End If
        End If
        If NoteType = 4 AndAlso frmCashReceipt IsNot Nothing Then
            Value = frmCashReceipt.debit
        End If
        If NoteType = 5 AndAlso frmConsignment IsNot Nothing Then
            Value = frmConsignment.Value
        End If
        If NoteType = 6 AndAlso frmCrossAccount IsNot Nothing Then
            Dim values As Tuple(Of Decimal, Decimal) = frmCrossAccount.getCrossingValue()
            If values IsNot Nothing Then Value = values.Item1
        End If
        If Value Is Nothing Then
            Return 0
        End If
        Return Value
    End Function

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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Tipo Nota", .FieldName = "NoteTypeName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Comprobante de Egreso", .FieldName = "VoucherTransactionId.Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Recibo de Caja", .FieldName = "CashReceiptId.Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Consignación", .FieldName = "ConsignmentId.Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Cruce CxC vs CxP", .FieldName = "CrossingAccountId.Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "NoteDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "TotalValueDetail", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3, .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .ColumnFormat = "n2"},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListTreasuryNote
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor obtenido por el formulario de busqueda
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements INotes.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDgleNoteType.Enabled = value
            INDsleEntityBankAccount.Enabled = value
            INDsleCash.Enabled = value
            INDdeDate.Enabled = value
            INDsleCostCenter.Enabled = value
            INDmeDetail.Enabled = value
            INDgcNoteConcept.Enabled = value
            INDsleVoucherTransaction.Enabled = value
            INDEsbConceptNote.Enabled = value
            INDSleCrossAccount.Enabled = value
            INDpeNoteConcept.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value
            INDlycRoot.EndUpdate()
        End Set
    End Property

    ''' <summary>
    ''' Generates the document indexed.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        Dim typeNote As String = String.Empty
        Select Case NoteType
            Case 1
                typeNote = ResourceManager.GetString("ExpenseTypeBankAccount", "Treasury")
            Case 2
                typeNote = ResourceManager.GetString("ExpenseTypeCash", "Treasury")
            Case 3
                typeNote = ResourceManager.GetString("NoteTypeUnConfirmVoucherTransaction", "Treasury")
            Case 4
                typeNote = ResourceManager.GetString("NoteTypeUnConfirmCashReceipt", "Treasury")
            Case 5
                typeNote = ResourceManager.GetString("NoteTypeUnConfirmConsignment", "Treasury")
            Case 6
                typeNote = ResourceManager.GetString("ReversionCrossAccount", "Treasury")
            Case 7
                typeNote = ResourceManager.GetString("NoteTypeDevolutionCashReceipt", "Treasury")
            Case 8
                typeNote = ResourceManager.GetString("CardReconciliationExpenses", "Treasury")
            Case Else
                typeNote = String.Empty
        End Select
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._treasuryNote.Code, typeNote, Me._treasuryNote.NoteDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._treasuryNote.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._treasuryNote.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._treasuryNote.Code, typeNote, Me._treasuryNote.NoteDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._treasuryNote.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Loads the status.
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
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()
        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        INDbteCode.Text = String.Empty
        INDgleNoteType.EditValue = Nothing
        INDsleEntityBankAccount.EditValue = Nothing
        INDsleCash.EditValue = Nothing
        INDdeDate.EditValue = Nothing
        INDsleCostCenter.EditValue = Nothing
        MainAccountId = Nothing
        INDmeDetail.Text = String.Empty
        Nature = Nothing
        INDsleVoucherTransaction.EditValue = Nothing
        INDsleCashReceipt.EditValue = Nothing
        INDsleConsignment.EditValue = Nothing
        NoteConceptList = Nothing
        ListTreasuryNoteCashReceiptsDetail = New List(Of TreasuryNoteCashReceiptsDetail)
        Value = 0
        ctrTmp.Title = String.Format(pathControlNature, String.Empty)
        ctrTmp.PrintValue()
        _treasuryNote = Nothing
        INDpcNoteData.Visible = True
        INDpcDocumentDetail.Visible = False
        INDlycRoot.EndUpdate()
        _stateOpenPopUpVoucherTransaction = False
        CleanLayouts()
        ReadOnlyControls(False, INDlycRoot)
        ReadOnlyControls(False, LayoutControl1)
        INDsleVoucherTransaction.Properties.NullText = String.Empty
        INDsleCashReceipt.Properties.NullText = String.Empty
        INDsleConsignment.Properties.NullText = String.Empty
        _NoteConceptListImport = Nothing
        INDpcDocuments.Controls.Clear()
        frmCashReceipt = Nothing
        frmVoucherTransaction = Nothing
        frmConsignment = Nothing

        INDsleCash.Properties.NullText = String.Empty
        INDsleEntityBankAccount.Properties.NullText = String.Empty
        INDsleCostCenter.Properties.NullText = String.Empty

        INDSleCrossAccount.EditValue = Nothing
        frmCrossAccount = Nothing
        _stateOpenPopUpCrossAccount = False

        ClearCashReceiptsControls()

        Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MTreasuryNote(Me.Tag)
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetTreasuryNote(Me.Code)
                    INDlycRoot.BeginUpdate()
                    _treasuryNote = resultOperation.ObjectEmbbeded
                    If Not _treasuryNote Is Nothing AndAlso _treasuryNote.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                            _record = Await ModelCommonTreasury.GetBlockRecordTreasury(Me.Tag, _treasuryNote.Id)
                            With _treasuryNote
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Code = .Code
                                NoteType = .NoteType
                                CashRegisterId = .CashRegisterId
                                EntityBankAccountId = .EntityBankAccountId
                                VoucherTransactionId = .VoucherTransactionId
                                CashReceiptId = .CashReceiptId
                                ConsignmentId = .ConsignmentId
                                CrossAccountId = .CrossingAccountId
                                NoteDate = .NoteDate
                                MainAccountId = .MainAccountId
                                CostCenterId = .CostCenterId
                                Detail = .Description
                                Nature = .Nature
                                Value = .Value
                                Status = .Status.ToString()
                                Me.CurrencyId(.Currency?.Abbreviation) = .CurrencyId
                            End With

                            INDsleCash.Properties.NullText = _treasuryNote.FullNameCashRegister
                            INDsleEntityBankAccount.Properties.NullText = _treasuryNote.FullNameEntityAccount
                            INDsleCostCenter.Properties.NullText = _treasuryNote.FullNameCostCenter
                            INDsleCashReceipt.Properties.NullText = _treasuryNote.FullNameCashReceipt
                            INDsleConsignment.Properties.NullText = _treasuryNote.ConsignmentCode
                            INDSleCrossAccount.Properties.NullText = _treasuryNote.CrossAccountCode

                            ctrTmp.Title = If(_treasuryNote.Nature = 1, String.Format(pathControlNature, ResourceManager.GetString("AccountNatureDebit")), String.Format(pathControlNature, ResourceManager.GetString("AccountNatureCredit")))
                            NoteConceptList = _treasuryNote.TreasuryNoteDetail.ToList()
                            ListTreasuryNoteCashReceiptsDetail = _treasuryNote.TreasuryNoteCashReceiptsDetail.ToList()
                            INDgcCashReceipts.DataSource = ListTreasuryNoteCashReceiptsDetail
                            ctrTmp.PrintValue()
                            LoadNullText()
                            Me.GetDocumentIndexed(Me.Tag & "_" & Me._treasuryNote.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelCommonTreasury.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _treasuryNote.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_treasuryNote.Id, MyTag, Nothing, GetType(TreasuryNote).Name)
                            If _treasuryNote.Status = 2 OrElse _treasuryNote.Status = 3 Then 'estado confirmado
                                ReadOnlyControls(True, INDlycRoot)
                                ReadOnlyControls(True, LayoutControl1)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                            End If
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

                            Me.BarraBotones.PrintReport(PrintReportAction.None, _treasuryNote.Id, 0, _treasuryNote.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDbteCode.Enabled = False
                            INDsbView.Enabled = True
                        End Using
                    Else

                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewNote()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Me.Code = String.Empty
                            Deshacer()
                            INDbteCode.Focus()
                        End If
                    End If
                End Using
                INDlycRoot.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Loads the null text.
    ''' </summary>
    Private Sub LoadNullText()
        With _treasuryNote
            INDsleVoucherTransaction.Properties.NullText = .FullNameVoucherTransaction
        End With
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _treasuryNote
            .Code = Code
            .NoteType = NoteType
            .CashRegisterId = CashRegisterId
            .EntityBankAccountId = EntityBankAccountId
            .VoucherTransactionId = VoucherTransactionId
            .CashReceiptId = CashReceiptId
            .ConsignmentId = ConsignmentId
            .MainAccountId = MainAccountId
            .CostCenterId = CostCenterId
            .NoteDate = NoteDate
            .Description = Detail
            .Nature = Nature
            .Value = Value
            .OperatingUnitId = _idOperativeUnit
            .Status = If(Status Is Nothing, 1, CByte(Status))
            .CrossingAccountId = CrossAccountId
            .CurrencyId = Me.CurrencyId
        End With
        If NoteConceptList IsNot Nothing Then
            For Each _concept As TreasuryNoteDetail In NoteConceptList
                If _concept.Id <> 0 Then
                    _concept.MarkAsModified()
                End If
                _treasuryNote.TreasuryNoteDetail.Add(_concept)
            Next
        End If

        _treasuryNote.TreasuryNoteCashReceiptsDetail.Clear()
        ''' <summary>
        ''' CashReceipts
        ''' </summary>
        If ListTreasuryNoteCashReceiptsDetail IsNot Nothing Then
            For Each obj As TreasuryNoteCashReceiptsDetail In ListTreasuryNoteCashReceiptsDetail
                If obj.Id <> 0 Then
                    obj.MarkAsModified()
                End If
                obj.CashReceipts = Nothing
                _treasuryNote.TreasuryNoteCashReceiptsDetail.Add(obj)
            Next
        End If
    End Sub

    ''' <summary>
    ''' News the note.
    ''' </summary>
    Private Async Function NewNote() As Task
        _treasuryNote = New TreasuryNote()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MCommonTreasury(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
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
        _presenter.LoadDateServer()
    End Function

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Crea el listado del tipo de nota
    ''' </summary>
    Private Sub CreateListNoteType()
        ListNoteType = New List(Of Tuple(Of Integer, String))
        ListNoteType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("ExpenseTypeBankAccount", NAME_MODULE)))
        ListNoteType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("ExpenseTypeCash", NAME_MODULE)))
        ListNoteType.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("NoteTypeUnConfirmVoucherTransaction", NAME_MODULE)))
        ListNoteType.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("NoteTypeUnConfirmCashReceipt", NAME_MODULE)))
        ListNoteType.Add(New Tuple(Of Integer, String)(5, ResourceManager.GetString("NoteTypeUnConfirmConsignment", NAME_MODULE)))
        ListNoteType.Add(New Tuple(Of Integer, String)(6, ResourceManager.GetString("ReversionCrossAccount", NAME_MODULE)))
        ListNoteType.Add(New Tuple(Of Integer, String)(7, ResourceManager.GetString("NoteTypeDevolutionCashReceipt", NAME_MODULE)))
        ListNoteType.Add(New Tuple(Of Integer, String)(8, ResourceManager.GetString("CardReconciliationExpenses", NAME_MODULE)))
        INDgleNoteType.Properties.DataSource = ListNoteType
    End Sub

    ''' <summary>
    ''' establece el formato moneda segun la que reciba el metodo
    ''' </summary>
    ''' <param name="_CurrencyAbbreviation"></param>
    Private Sub SetCurrencyUI(_CurrencyAbbreviation As String)
        If String.IsNullOrEmpty(_CurrencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "Está llegando vacia la abreviación de la moneda"
            Exit Sub
        End If

        Me.changeNumericFormatByCurrency(New Globalization.CultureInfo(_CurrencyAbbreviation.GetCultureId()).NumberFormat)
        Me.BandedGridColumn3 = Window.Utils.FormatGrid(Me.BandedGridColumn3, _CurrencyAbbreviation)
        ctrNoteConcept.SetCurrencyUI(_CurrencyAbbreviation)
        ctrTmp.CurrencyCodeISO4217 = _CurrencyAbbreviation
        ctrTmp.PrintValue()
    End Sub
    ''' <summary>
    ''' Creamso la extructura del excel 
    ''' </summary>
    Private Sub CreateStructureExcel()
        INDEsbConceptNote.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Concepto"},'0
                            New ExcelColumn With {.Name = "Tercero"},'1
                            New ExcelColumn With {.Name = "Centro de costo"},'2
                            New ExcelColumn With {.Name = "Naturaleza", .Comment = "Opción 1 Débitos 2 Créditos"},'3
                            New ExcelColumn With {.Name = "Valor"}
                        }
                    })
    End Sub
    ''' <summary>
    ''' Funcion para importar 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ImportFile() As Task
        If NoteConceptList IsNot Nothing AndAlso NoteConceptList.Count > 0 Then
            If MessageIndigo.Show("Se perderan los datos que estan en la rejilla, desea continuar", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                Exit Function
            Else
                NoteConceptList = Nothing
                _NoteConceptListImport = Nothing
                INDgcNoteConcept.DataSource = Nothing
            End If
        End If

        'Configuramos el cuadro de dialogo para importar el archivo
        Dim noteOpenFileDialog As New OpenFileDialog()
        noteOpenFileDialog.InitialDirectory = "c:\"
        noteOpenFileDialog.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        noteOpenFileDialog.FilterIndex = 2
        noteOpenFileDialog.RestoreDirectory = True
        noteOpenFileDialog.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If noteOpenFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Function
        End If
        Try
            myStream = noteOpenFileDialog.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            AsyncLoader(True)
            Await LoadImportFile()
            If _NoteConceptListImport IsNot Nothing Then
                INDgcNoteConcept.DataSource = _NoteConceptListImport
                For Each item In _NoteConceptListImport
                    AddNoteConceptMasive(item, EventArgs.Empty)
                Next
            Else
                Using formulario As New FrmListErrors(_errorListImport)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        Catch ex As Exception
            Throw
        End Try
    End Function
    ''' <summary>
    ''' Evento para copiar y pegar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If sender.Name = INDgcNoteConcept.Name Then
            If e.Rows.Count = 0 Then
                Exit Sub
            End If
            If e.Rows.Count > 1000 Then
                Mensaje(EeventViewerImages.Advertencia) = "No es posible postular mas de 1000 registros"
                Exit Sub
            End If
            INDgvNoteConcept.ShowLoadingPanel()
            Me.Cursor = ChangeCursorIndigo()
            Using model As New MNoteConcepts(MyBase.Tag)
                Dim result = Await model.CopyPasteNoteConceptDetail(e.Rows)

                Me.Cursor = System.Windows.Forms.Cursors.Default
                INDgvNoteConcept.HideLoadingPanel()
                INDgcNoteConcept.DataSource = Nothing
                INDgcNoteConcept.DataSource = result.ObjectEmbbeded
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    INDgvNoteConcept.OptionsFind.AlwaysVisible = True
                    IndigoGridControl1.SetExportButton(INDgcNoteConcept, True)
                    For Each item In result.ObjectEmbbeded
                        AddNoteConceptMasive(item, EventArgs.Empty)
                    Next
                Else
                    INDgvNoteConcept.OptionsFind.AlwaysVisible = False
                    IndigoGridControl1.SetExportButton(INDgcNoteConcept, False)

                End If
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If

            End Using
        End If

    End Sub
    ''' <summary>
    ''' Controla las filas del archivo Excel
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              If rows.Item(x).SpreadsheetRowToList(5).All(Function(cell) String.IsNullOrEmpty(cell)) Then
                                                  Return
                                              End If
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(5)})

                                              End SyncLock

                                          End Sub)
    End Sub
    ''' <summary>
    ''' Funcion para cargar la importacion
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadImportFile() As Task
        Return Task.Factory.StartNew(Sub()
                                         Dim totalProcessedItems As Integer = 0
                                         listErrosImportFile = New List(Of String())
                                         Dim ssc = New SpreadsheetControl()
                                         ssc.AllowDrop = False
                                         ssc.LoadDocument(myStream)

                                         Dim workBook As IWorkbook = ssc.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Exit Sub
                                         End If

                                         Dim indexSend = 0
                                         Dim totalItems = rows.LastUsedIndex
                                         ' Verificar si excede los 1000 registros
                                         If (totalItems + 1) > 1000 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "El archivo supera los 1000 registros permitidos."
                                             Exit Sub
                                         End If
                                         While (totalItems + 1) > totalProcessedItems
                                             Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                             indexSend = totalProcessedItems
                                             totalProcessedItems += quantityDetailsToProcess
                                             listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                             SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)

                                             Try
                                                 Using model As New MNoteConcepts(MyBase.Tag)
                                                     AsyncLoader(True)
                                                     Dim ObjResult = model.ValidateNoteConcept(listRows.ToList())
                                                     Dim Result = ObjResult.ObjectEmbbeded

                                                     If ObjResult.ObjectEmbbeded IsNot Nothing AndAlso ObjResult.MessageResult.Count = 0 Then
                                                         _NoteConceptListImport = Result
                                                     Else
                                                         _errorListImport = ObjResult.MessageResult

                                                     End If

                                                 End Using
                                             Catch ex As Exception
                                                 Throw ex
                                             End Try

                                         End While
                                     End Sub)
    End Function
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _flagEntityAccount = Nothing
        _flagCostCenter = Nothing
        _flagCashRegister = Nothing
        pathControlNature = Nothing
        frmVoucherTransaction = Nothing
        frmCashReceipt = Nothing
        frmConsignment = Nothing
        ctrTmp = Nothing
        _stateOpenPopUpVoucherTransaction = Nothing
        _stateOpenPopUpCashReceipt = Nothing
        _stateOpenPopUpConsignment = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        frmCrossAccount = Nothing
        _stateOpenPopUpCrossAccount = Nothing
        ListNoteType = Nothing
        ListNature = Nothing
        MainAccountId = Nothing
        varImp = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmThirdPartyAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PNotes(Me)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()
        _presenter.LoadDateServer()

        _presenter.InitializeCostCenter()
        IndigoGridControl1.RefreshGrid(INDgcNoteConcept)
        ctrNoteConcept.UploadSearchLookUp()

        'Acciones para el grid de recibos de caja
        Dim listActionReceipts As New List(Of eAcciones)
        listActionReceipts.Add(eAcciones.Remove)
        IndigoGridView11.SetListAcction(INDgvCashReceipts, listActionReceipts)

        'Acciones para el grid de conceptos de nota
        Dim listActionConcepts As New List(Of eAcciones)
        listActionConcepts.Add(eAcciones.Remove)
        listActionConcepts.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvNoteConcept, listActionConcepts)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvNoteConcept.Columns
            If col.Name = "colActions" Then
                col.Width = 200
            End If
        Next
        ctrNoteConcept.CleanControls()
        CreateListNoteType()
        CreateStructureExcel()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the Activated event of the FrmNotes control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub


    ''' <summary>
    ''' Carga el formulario de comprobantes de egreso
    ''' </summary>
    Private Sub LoadControlsVoucher()
        INDpcDocuments.Controls.Clear()
        INDlblNameVoucher.Text = "Comprobante de Egreso"
        AsyncLoader(True)
        If frmVoucherTransaction Is Nothing Then
            frmVoucherTransaction = New FrmVoucherTransaction()
            frmVoucherTransaction.TopLevel = False
            frmVoucherTransaction.Parent = INDpcDocuments
            frmVoucherTransaction.ToolBar.Dock = DockStyle.None
            frmVoucherTransaction.ViewModeEditHold = False
            frmVoucherTransaction.Dock = DockStyle.Fill
            frmVoucherTransaction.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            AddHandler frmVoucherTransaction.LoadControlsFinish, AddressOf LoadControlsFinishVoucherTransaction
            AddHandler frmVoucherTransaction.Shown, AddressOf VoucherTransaction_Shown
            frmVoucherTransaction.Show()
        Else
            frmVoucherTransaction.Parent = INDpcDocuments
            frmVoucherTransaction.Deshacer()
            VoucherTransaction_Shown(Nothing, Nothing)
        End If
        INDpcNoteData.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

    Private Sub LoadControlsCashReceiptForm()
        INDpcDocuments.Controls.Clear()
        INDlblNameVoucher.Text = "Recibo de Caja"
        AsyncLoader(True)
        If frmCashReceipt Is Nothing Then
            frmCashReceipt = New FrmCashReceipts()
            frmCashReceipt.TopLevel = False
            frmCashReceipt.Parent = INDpcDocuments
            frmCashReceipt.ToolBar.Dock = DockStyle.None
            frmCashReceipt.ViewModeEditHold = False
            frmCashReceipt.Dock = DockStyle.Fill
            frmCashReceipt.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            AddHandler frmCashReceipt.LoadControlsFinish, AddressOf LoadControlsFinishCashReceipt
            AddHandler frmCashReceipt.Shown, AddressOf CashReceipt_Shown
            frmCashReceipt.Show()
        Else
            frmCashReceipt.Parent = INDpcDocuments
            frmCashReceipt.Deshacer()
            CashReceipt_Shown(Nothing, Nothing)
        End If
        INDpcNoteData.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

    Private Sub LoadControlsConsignmentForm()
        INDpcDocuments.Controls.Clear()
        INDlblNameVoucher.Text = "Consignación"
        AsyncLoader(True)
        If frmConsignment Is Nothing Then
            frmConsignment = New FrmConsignment()
            frmConsignment.TopLevel = False
            frmConsignment.Parent = INDpcDocuments
            frmConsignment.ToolBar.Dock = DockStyle.None
            frmConsignment.ViewModeEditHold = False
            frmConsignment.Dock = DockStyle.Fill
            frmConsignment.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            AddHandler frmConsignment.LoadControlsFinish, AddressOf LoadControlsFinishConsignment
            AddHandler frmConsignment.Shown, AddressOf Consignment_Shown
            frmConsignment.Show()
        Else
            frmConsignment.Parent = INDpcDocuments
            frmConsignment.Deshacer()
            Consignment_Shown(Nothing, Nothing)
        End If
        INDpcNoteData.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

    Private Sub LoadControlsCrossAccountForm()
        INDpcDocuments.Controls.Clear()
        INDlblNameVoucher.Text = "Cruce CxC vs CxP"
        AsyncLoader(True)
        If frmCrossAccount Is Nothing Then
            frmCrossAccount = New FrmVoucherTransactionCrossing()
            frmCrossAccount.CallNote = True
            frmCrossAccount.TopLevel = False
            frmCrossAccount.Parent = INDpcDocuments
            frmCrossAccount.ToolBar.Dock = DockStyle.None
            frmCrossAccount.ViewModeEditHold = False
            frmCrossAccount.Dock = DockStyle.Fill
            frmCrossAccount.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
            AddHandler frmCrossAccount.LoadControlsFinish, AddressOf LoadControlsFinishCrossAccount
            AddHandler frmCrossAccount.Shown, AddressOf CrossAccount_Shown
            frmCrossAccount.Show()
        Else
            frmCrossAccount.Parent = INDpcDocuments
            frmCrossAccount.Deshacer()
            CrossAccount_Shown(Nothing, Nothing)
        End If
        INDpcNoteData.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub VoucherTransaction_Shown(sender As Object, e As EventArgs)
        If _treasuryNote.Status = 2 OrElse _treasuryNote.Status = 3 Then
            frmVoucherTransaction.Code = _treasuryNote.FullNameVoucherTransaction
        Else
            frmVoucherTransaction.Code = INDsleVoucherTransaction.Text.Trim()
        End If
        frmVoucherTransaction.LoadControls()
        INDliView.HideControl(False)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub CashReceipt_Shown(sender As Object, e As EventArgs)
        If _treasuryNote.Status = 2 OrElse _treasuryNote.Status = 3 Then
            frmCashReceipt.Code = _treasuryNote.FullNameCashReceipt
        Else
            If INDsleCashReceipt.Text.Trim().Equals("") Then
                frmCashReceipt.Code = _treasuryNote.FullNameCashReceipt
            Else
                frmCashReceipt.Code = INDsleCashReceipt.Text.Trim()
            End If
        End If
        Await frmCashReceipt.LoadControls()
        Me.CurrencyId(frmCashReceipt.propertyCurrencyISO4217) = frmCashReceipt.CurrencyId
        INDliView.HideControl(False)
        AsyncLoader(False)
    End Sub

    Private Sub Consignment_Shown(sender As Object, e As EventArgs)
        If _treasuryNote.Status = 2 OrElse _treasuryNote.Status = 3 Then
            frmConsignment.Code = _treasuryNote.ConsignmentCode
        Else
            If INDsleConsignment.Text.Trim().Equals("") Then
                frmConsignment.Code = _treasuryNote.ConsignmentCode
            Else
                frmConsignment.Code = INDsleConsignment.Text.Trim()
            End If
        End If
        frmConsignment.LoadControls()
        INDliView.HideControl(False)
        AsyncLoader(False)
    End Sub

    Private Async Sub CrossAccount_Shown(sender As Object, e As EventArgs)
        If _treasuryNote.Status = 2 OrElse _treasuryNote.Status = 3 Then
            frmCrossAccount.Code = _treasuryNote.CrossAccountCode
        Else
            If INDSleCrossAccount.Text.Trim().Equals("") Then
                frmCrossAccount.Code = _treasuryNote.CrossAccountCode
            Else
                frmCrossAccount.Code = INDSleCrossAccount.Text.Trim()
            End If
        End If
        Await frmCrossAccount.LoadControls()
        INDliView.HideControl(False)
        AsyncLoader(False)
    End Sub

    Public Sub LoadControlsFinishCrossAccount()
        ctrTmp.Title = "Valor Total"
        ctrTmp.PrintValue()
    End Sub

    Public Sub LoadControlsFinishVoucherTransaction(_voucherTransaction As VoucherTransaction)
        ctrTmp.Title = "Valor Total"
        ctrTmp.PrintValue()
        If _voucherTransaction.ExpenseType = 1 Then
            fechaMinima = _voucherTransaction.DocumentDate
            CalendarDaysAllowed = True
        Else
            CalendarDaysAllowed = False
        End If
    End Sub

    Public Sub LoadControlsFinishCashReceipt(_CashReceipts As CashReceipts)
        ctrTmp.Title = "Valor Total"
        ctrTmp.PrintValue()
        If _CashReceipts.CollectType = 2 Then
            fechaMinima = _CashReceipts.DocumentDate
            CalendarDaysAllowed = True
        Else
            CalendarDaysAllowed = False
        End If
        ShowOrHideCashBank(_CashReceipts.CollectType)
    End Sub

    ''' <summary>
    ''' muestra u oculta el control de caja o de cuenta bancaria dependiendo del collectype del recibo de caja
    ''' </summary>
    ''' <param name="value"></param>
    Private Sub ShowOrHideCashBank(value As Byte)
        INDliCash.HideControl(IIf(value = 1 And NoteType = 7, False, True))
        INDliBackAccount.HideControl(IIf(value = 2 And NoteType = 7, False, True))
    End Sub

    Public Sub LoadControlsFinishConsignment(_consignment As Consignment)
        ctrTmp.Title = "Valor Total"
        ctrTmp.CurrencyCodeISO4217 = _consignment.CurrencyAbbreviation
        Me.CurrencyId(_consignment.CurrencyAbbreviation) = _consignment.CurrencyId
        ctrTmp.PrintValue()
        fechaMinima = _consignment.DocumentDate
        CalendarDaysAllowed = True
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDtxtDocumentNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewNote()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituelf
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._treasuryNote IsNot Nothing AndAlso Me._treasuryNote.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
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

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleNoteType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleNoteType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleNoteType.EditValueChanged
        ActionsOnControlsNoteType()
        Me.CurrencyId(indigo.CurrencyISO4217) = indigo.OfficialCurrencyId
    End Sub

    ''' <summary>
    ''' Limpia ocultando los layouts
    ''' </summary>
    Private Sub CleanLayouts()
        INDliView.HideControl(True)
        INDliVoucherTransaction.HideControl(True)
        INDliCashReceipt.HideControl(True)
        INDliConsignment.HideControl(True)
        INDLciCrossAccount.HideControl(True)
        INDliBackAccount.HideControl(True)
        INDliCash.HideControl(True)
        INDliDate.HideControl(False)
        INDliCostCenter.HideControl(True)
        INDliDetail.HideControl(True)
        INDlcgNoteConcept.HideControl(True)
        INDlcgCardReconciliationExpenses.HideControl(True)
    End Sub

    ''' <summary>
    ''' Sets a value indicating whether [actions on controls note type].
    ''' </summary>
    Private Sub ActionsOnControlsNoteType()
        INDlycRoot.BeginUpdate()
        'CleanLayouts()
        INDliDate.HideControl(False)
        CalendarDaysAllowed = False
        Select Case NoteType
            Case eNoteType.EntityAccount
                INDliView.HideControl()
                INDliVoucherTransaction.HideControl()
                INDsleVoucherTransaction.EditValue = Nothing
                INDliCashReceipt.HideControl()
                INDsleCashReceipt.EditValue = Nothing
                INDliConsignment.HideControl()
                INDsleConsignment.EditValue = Nothing
                INDliCash.HideControl()
                INDsleCash.EditValue = Nothing
                INDliCostCenter.HideControl()
                INDsleCostCenter.EditValue = Nothing
                INDSleCrossAccount.EditValue = Nothing
                INDLciCrossAccount.HideControl()
                INDlcgCardReconciliationExpenses.HideControl()

                INDliBackAccount.HideControl(False)
                INDliDetail.HideControl(False)
                INDlcgNoteConcept.HideControl(False)
                INDsbImport.Enabled = True
                INDEsbConceptNote.Enabled = True
                CalendarDaysAllowed = True

            Case eNoteType.CashRegister
                INDliView.HideControl()
                INDliVoucherTransaction.HideControl()
                INDsleVoucherTransaction.EditValue = Nothing
                INDliCashReceipt.HideControl()
                INDsleCashReceipt.EditValue = Nothing
                INDliConsignment.HideControl()
                INDsleConsignment.EditValue = Nothing
                INDliBackAccount.HideControl()
                INDsleEntityBankAccount.EditValue = Nothing
                INDliCash.HideControl(False)
                INDsleCash.EditValue = Nothing
                INDliCostCenter.HideControl()
                INDsleCostCenter.EditValue = Nothing
                INDSleCrossAccount.EditValue = Nothing
                INDLciCrossAccount.HideControl()
                INDlcgCardReconciliationExpenses.HideControl()
                INDliDetail.HideControl(False)
                INDlcgNoteConcept.HideControl(False)
            Case eNoteType.UnConfirmVoucherTransaction
                INDliView.HideControl()
                INDliCashReceipt.HideControl()
                INDsleCashReceipt.EditValue = Nothing
                INDliConsignment.HideControl()
                INDsleConsignment.EditValue = Nothing
                INDliBackAccount.HideControl()
                INDsleEntityBankAccount.EditValue = Nothing
                INDliCash.HideControl()
                INDsleCash.EditValue = Nothing
                INDliCostCenter.HideControl()
                INDsleCostCenter.EditValue = Nothing
                INDlcgNoteConcept.HideControl()
                INDSleCrossAccount.EditValue = Nothing
                INDLciCrossAccount.HideControl()
                INDlcgCardReconciliationExpenses.HideControl()

                INDliVoucherTransaction.HideControl(False)
                INDliDetail.HideControl(False)
            Case eNoteType.UnConfirmCashReceipt, eNoteType.DevolutionCashReceipt
                INDliView.HideControl()
                INDliVoucherTransaction.HideControl()
                INDsleVoucherTransaction.EditValue = Nothing
                INDliConsignment.HideControl()
                INDsleConsignment.EditValue = Nothing
                INDliBackAccount.HideControl()
                INDsleEntityBankAccount.EditValue = Nothing
                INDliCash.HideControl()
                INDsleCash.EditValue = Nothing
                INDliCostCenter.HideControl()
                INDsleCostCenter.EditValue = Nothing
                INDlcgNoteConcept.HideControl()
                INDSleCrossAccount.EditValue = Nothing
                INDLciCrossAccount.HideControl()
                INDlcgCardReconciliationExpenses.HideControl()
                INDliCashReceipt.HideControl(False)
                INDliDetail.HideControl(False)

            Case eNoteType.UnConfirmConsignment
                INDliView.HideControl()
                INDliVoucherTransaction.HideControl()
                INDsleVoucherTransaction.EditValue = Nothing
                INDliCashReceipt.HideControl()
                INDsleCashReceipt.EditValue = Nothing
                INDliBackAccount.HideControl()
                INDsleEntityBankAccount.EditValue = Nothing
                INDliCash.HideControl()
                INDsleCash.EditValue = Nothing
                INDliCostCenter.HideControl()
                INDsleCostCenter.EditValue = Nothing
                INDlcgNoteConcept.HideControl()
                INDSleCrossAccount.EditValue = Nothing
                INDLciCrossAccount.HideControl()
                INDlcgCardReconciliationExpenses.HideControl()

                INDliConsignment.HideControl(False)
                INDliDetail.HideControl(False)
            Case eNoteType.ReversionCrossAccount
                INDliView.HideControl()
                INDliVoucherTransaction.HideControl()
                INDsleVoucherTransaction.EditValue = Nothing
                INDliCashReceipt.HideControl()
                INDsleCashReceipt.EditValue = Nothing
                INDliBackAccount.HideControl()
                INDsleEntityBankAccount.EditValue = Nothing
                INDliCash.HideControl()
                INDsleCash.EditValue = Nothing
                INDliCostCenter.HideControl()
                INDsleCostCenter.EditValue = Nothing
                INDlcgNoteConcept.HideControl()
                INDliConsignment.HideControl()
                INDsleConsignment.EditValue = Nothing
                INDlcgCardReconciliationExpenses.HideControl()
                INDLciCrossAccount.HideControl(False)
                INDliDetail.HideControl(False)
            Case eNoteType.CardReconciliationExpenses
                INDliView.HideControl()
                INDliVoucherTransaction.HideControl()
                INDsleVoucherTransaction.EditValue = Nothing
                INDliCashReceipt.HideControl()
                INDsleCashReceipt.EditValue = Nothing
                INDliConsignment.HideControl()
                INDsleConsignment.EditValue = Nothing
                INDliCash.HideControl()
                INDsleCash.EditValue = Nothing
                INDliCostCenter.HideControl()
                INDsleCostCenter.EditValue = Nothing
                INDSleCrossAccount.EditValue = Nothing
                INDLciCrossAccount.HideControl()

                ' Mostrar ambos segmentos: conceptos de nota Y recibos de caja
                INDliBackAccount.HideControl(False)
                INDliDetail.HideControl(False)
                INDlcgNoteConcept.HideControl(False)
                INDlcgCardReconciliationExpenses.HideControl(False)
                INDsbImport.Enabled = True
                INDEsbConceptNote.Enabled = True
                CalendarDaysAllowed = True
        End Select
        INDlycRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDdeDate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDdeDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeDate.EditValueChanged
        If DateNote IsNot Nothing Then
            _stateOpenPopUpCashReceipt = False

            If NoteType = eNoteType.CardReconciliationExpenses Then
                ClearCashReceiptsControls()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntityBankAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleEntityBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityBankAccount.EditValueChanged
        If INDsleEntityBankAccount.EditValue IsNot Nothing AndAlso INDsleEntityBankAccount.EditValue > 0 Then
            Using Model As New MEntityAccount(Me.MyTag)
                Dim _entityAccount As EntityBankAccounts = Await Model.GetEntityBankAccountById(EntityBankAccountId)
                If _entityAccount IsNot Nothing AndAlso _entityAccount.Id > 0 Then
                    MainAccountId = _entityAccount.IdMainAccount
                    Me.CurrencyId(_entityAccount.CurrencyAbbreviation) = _entityAccount.CurrencyId
                    If _entityAccount.MainAccounts.HandlesCostCenter Then
                        INDliCostCenter.HideControl(False)
                        CostCenterId = _entityAccount.IdCostCenter
                        INDsleCostCenter.Properties.ReadOnly = True
                    Else
                        INDliCostCenter.HideControl(True)
                    End If
                End If
            End Using

            ' Recargar recibos de caja cuando cambia la cuenta bancaria
            _stateOpenPopUpCashReceipt = False

            If {eNoteType.CardReconciliationExpenses}.Contains(NoteType) Then
                ClearCashReceiptsControls()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleCash_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCash.EditValueChanged
        If INDsleCash.EditValue IsNot Nothing AndAlso INDsleCash.EditValue > 0 Then
            Using Model As New MCashRegister(Me.MyTag)
                Dim _cashRegister As CashRegisters = Await Model.GetcashRegisterById(CashRegisterId)
                If _cashRegister IsNot Nothing AndAlso _cashRegister.Id > 0 Then
                    MainAccountId = _cashRegister.IdMainAccount
                    Me.CurrencyId(_cashRegister?.CurrencyName) = _cashRegister?.CurrencyId
                    If _cashRegister.MainAccountHandlesCostCenter Then
                        INDliCostCenter.HideControl(False)
                        CostCenterId = _cashRegister.IdCostCenter
                        INDsleCostCenter.Properties.ReadOnly = True
                    Else
                        INDliCostCenter.HideControl(True)
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpeNoteConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpeNoteConcept_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpeNoteConcept.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpeNoteConcept.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleEntityBankAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityBankAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEntityAccount
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeEntityBankAccount()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleVoucherTransaction_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVoucherTransaction.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmVoucherTransaction
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeVoucerTransaction()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCash_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCash.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCash
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeCash()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCostCenter
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeCostCenter()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCrossAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCrossAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCrossAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmVoucherTransactionCrossing
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeCrossAccount()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDpeNoteConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDpeNoteConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDpeNoteConcept.QueryPopUp
        'ctrNoteConcept.CleanControls()
        ctrNoteConcept.FirstControlFocus()
    End Sub

    ''' <summary>
    ''' Handles the CloseUp event of the INDpeNoteConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.CloseUpEventArgs"/> instance containing the event data.</param>
    Private Sub INDpeNoteConcept_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpeNoteConcept.CloseUp
        'ctrNoteConcept.CleanControls()
    End Sub

    ''' <summary>
    ''' Handles the Closed event of the INDpeNoteConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDpeNoteConcept_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpeNoteConcept.Closed
        ctrNoteConcept.CleanControls()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmThirdPartyAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmNotes_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleVoucherTransaction_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleVoucherTransaction.QueryPopUp
        If Not _stateOpenPopUpVoucherTransaction AndAlso INDsleVoucherTransaction.Properties.ReadOnly = False Then
            _presenter.InitializeVoucerTransaction()
            _stateOpenPopUpVoucherTransaction = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCashReceipt control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCashReceipt_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCashReceipt.QueryPopUp
        If Not INDsleCashReceipt.Properties.ReadOnly AndAlso CashReceiptDatasource Is Nothing Then
            ' Cargar todos los recibos confirmados
            _presenter.InitializeCashReceipts()
            _stateOpenPopUpCashReceipt = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCashReceipts control (tipo nota 8).
    ''' </summary>
    Private Sub INDsleCashReceipts_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCashReceipts.QueryPopUp
        If Not INDsleCashReceipts.Properties.ReadOnly AndAlso CashReceiptsDatasource Is Nothing Then
            ' Validar condiciones necesarias antes de cargar recibos (cuenta bancaria siempre requerida)
            If Not ValidateCanLoadCashReceipts() Then
                e.Cancel = True
                Exit Sub
            End If

            ' Cargar recibos filtrados por cuenta bancaria y mes
            _presenter.InitializeCashReceiptsForCardReconciliation(EntityBankAccountId.Value, DateNote.Value)
        End If
    End Sub

    ''' <summary>
    ''' Valida que se cumplan las condiciones necesarias para cargar recibos de caja
    ''' </summary>
    ''' <returns>True si todas las validaciones pasan, False en caso contrario</returns>
    Private Function ValidateCanLoadCashReceipts() As Boolean
        If INDliBackAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If EntityBankAccountId.GetValueOrDefault(0) = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una cuenta bancaria antes de seleccionar un recibo de caja"
                Return False
            End If
        End If

        ' Validar fecha (siempre requerida para filtrar recibos)
        If DateNote Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una fecha antes de seleccionar un recibo de caja"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Limpia los controles de datasource de recibos de caja para el la nota tipo 8
    ''' </summary>
    Private Sub ClearCashReceiptsControls()
        INDsleCashReceipts.Properties.DataSource = Nothing
        INDsleCashReceipts.EditValue = Nothing
        INDgcCashReceipts.DataSource = Nothing
        INDsleCashReceipts.Properties.NullText = String.Empty
    End Sub

    Private Sub INDsleConsignment_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleConsignment.QueryPopUp
        If Not _stateOpenPopUpConsignment AndAlso INDsleConsignment.Properties.ReadOnly = False Then
            _presenter.InitializeConsignment()
            _stateOpenPopUpConsignment = True
        End If
    End Sub

    Private Sub INDSleCrossAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCrossAccount.QueryPopUp
        If Not _stateOpenPopUpCrossAccount AndAlso INDSleCrossAccount.Properties.ReadOnly = False Then
            _presenter.InitializeCrossAccount()
            _stateOpenPopUpCrossAccount = True
        End If
    End Sub

    Private Sub INDsleCashReceipt_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCashReceipt.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("635", Nothing, True)
        End If
    End Sub

    Private Sub INDsleConsignment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConsignment.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("638", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleVoucherTransaction control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleVoucherTransaction_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleVoucherTransaction.EditValueChanged
        If VoucherTransactionId IsNot Nothing AndAlso VoucherTransactionId > 0 Then
            Dim voucherTransaction = _presenter.GetVoucherTransactionById(VoucherTransactionId)
            Me.CurrencyId(voucherTransaction?.CurrencyAbbreviation) = voucherTransaction?.CurrencyId
            If voucherTransaction.VoucherClass = 2 Then
                VoucherTransactionId = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "No se puede reversar un comprobante de egreso de tipo reembolso"
                Exit Sub
            End If
            LoadControlsVoucher()
        End If
    End Sub

    Private Sub INDsleCashReceipt_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCashReceipt.EditValueChanged
        If CashReceiptId <> 0 Then
            LoadControlsCashReceiptForm()
        End If
    End Sub

    Private Sub INDsleConsignment_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConsignment.EditValueChanged
        If ConsignmentId IsNot Nothing AndAlso ConsignmentId > 0 Then
            LoadControlsConsignmentForm()
        End If
    End Sub

    Private Sub INDsleCrossAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCrossAccount.EditValueChanged
        If CrossAccountId IsNot Nothing AndAlso CrossAccountId > 0 Then
            LoadControlsCrossAccountForm()
        End If
    End Sub

    ''' <summary>
    ''' CTRs the navigation1_ click back.
    ''' </summary>
    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        INDpcNoteData.Visible = True
        INDpcDocumentDetail.Visible = False
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbView control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbView_Click(sender As Object, e As EventArgs) Handles INDsbView.Click
        'voucher_Load(Nothing, Nothing)
        INDpcNoteData.Visible = False
        INDpcDocumentDetail.Visible = True
    End Sub

    ''' <summary>
    ''' Método que agrega el concepto de nota
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub AddNoteConcept(sender As Object, e As EventArgs) Handles ctrNoteConcept.AddNoteConcept
        If NoteConceptList Is Nothing Then
            NoteConceptList = New List(Of TreasuryNoteDetail)()
        End If
        Dim noteConcept As TreasuryNoteDetail = CType(sender, TreasuryNoteDetail)
        If ctrNoteConcept.IsEdit Then
            Dim noteEdit As TreasuryNoteDetail = CType(INDgvNoteConcept.GetFocusedRow(), TreasuryNoteDetail)
            noteEdit = noteConcept
        Else
            NoteConceptList.Add(noteConcept)
        End If
        Dim resultValidateConcept = TreasuryStaticServices.ValidateValueNoteConcept(NoteConceptList)
        Value = CType(resultValidateConcept.ObjectEmbbeded, Object()).ElementAt(1)
        If resultValidateConcept.StateResult Then
            Nature = CShort(CType(resultValidateConcept.ObjectEmbbeded, Object()).ElementAt(0))
        Else
            Mensaje(EeventViewerImages.Advertencia) = resultValidateConcept.Message
        End If
        INDgcNoteConcept.RefreshDataSource()
        ctrNoteConcept.IsEdit = False
        If _treasuryNote.ChangeTracker.State <> ObjectState.Added Then
            _treasuryNote.MarkAsModified()
        End If
        INDgleNoteType.Properties.ReadOnly = True
        ctrTmp.Title = If(Nature = 1, String.Format(pathControlNature, ResourceManager.GetString("AccountNatureDebit")), String.Format(pathControlNature, ResourceManager.GetString("AccountNatureCredit")))
        ctrTmp.PrintValue()
    End Sub
    ''' <summary>
    ''' Funcion para controlar los valores del CTR desde la importacion o CopyPaste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AddNoteConceptMasive(sender As Object, e As EventArgs) Handles ctrNoteConcept.AddNoteConcept
        If NoteConceptList Is Nothing Then
            NoteConceptList = New List(Of TreasuryNoteDetail)()
        End If
        Dim noteConcept As TreasuryNoteDetail = CType(sender, TreasuryNoteDetail)
        Dim resultValidateConcept = TreasuryStaticServices.ValidateValueNoteConcept(NoteConceptList)
        Value = CType(resultValidateConcept.ObjectEmbbeded, Object()).ElementAt(1)
        If resultValidateConcept.StateResult Then
            Nature = CShort(CType(resultValidateConcept.ObjectEmbbeded, Object()).ElementAt(0))
        Else
            Mensaje(EeventViewerImages.Advertencia) = resultValidateConcept.Message
        End If
        INDgcNoteConcept.RefreshDataSource()
        ctrNoteConcept.IsEdit = False
        If _treasuryNote.ChangeTracker.State <> ObjectState.Added Then
            _treasuryNote.MarkAsModified()
        End If
        INDgleNoteType.Properties.ReadOnly = True
        ctrTmp.Title = If(Nature = 1, String.Format(pathControlNature, ResourceManager.GetString("AccountNatureDebit")), String.Format(pathControlNature, ResourceManager.GetString("AccountNatureCredit")))
        ctrTmp.PrintValue()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control to add cash receipts
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If INDsleCashReceipts.EditValue IsNot Nothing Then
            Try
                ' Obtener el ID del recibo seleccionado
                Dim receiptId As Integer = CInt(INDsleCashReceipts.EditValue)

                ' Verificar si el recibo ya fue agregado
                If ListTreasuryNoteCashReceiptsDetail.Any(Function(x) x.CashReceiptsId = receiptId) Then
                    Mensaje(EeventViewerImages.Advertencia) = "El recibo de caja ya fue agregado"
                    Exit Sub
                End If

                ' Buscar el recibo en la colección datasource
                Dim collection = TryCast(INDsleCashReceipts.Properties.DataSource, XPCollection(Of TreasuryCashReceiptsXpo))
                If collection IsNot Nothing Then
                    Dim selectedReceipt = collection.FirstOrDefault(Function(x) x.Id = receiptId)

                    If selectedReceipt IsNot Nothing Then
                        ' Crear objeto CashReceipts para la lista
                        Dim newReceipt As New CashReceipts() With {
                            .Id = selectedReceipt.Id,
                            .Code = selectedReceipt.Code,
                            .DocumentDate = selectedReceipt.DocumentDate,
                            .Value = selectedReceipt.Value
                        }
                        ' Agregar a la lista
                        Dim newObj As New TreasuryNoteCashReceiptsDetail() With {
                            .CashReceiptsId = selectedReceipt.Id,
                            .CashReceipts = newReceipt
                        }
                        ListTreasuryNoteCashReceiptsDetail.Add(newObj)
                        ' Limpiar selección
                        INDsleCashReceipts.EditValue = Nothing
                        ' Actualizar grid
                        INDgcCashReceipts.DataSource = Nothing
                        INDgcCashReceipts.DataSource = ListTreasuryNoteCashReceiptsDetail
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontró el recibo seleccionado"
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "Error al acceder a los datos de recibos"
                End If
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "Error al agregar el recibo: " & ex.Message
            End Try
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un recibo de caja"
        End If
    End Sub

    ''' <summary>
    ''' Remueve un recibo de caja del grid
    ''' </summary>
    Private Sub RemoveCashReceipt()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim cashReceipts As TreasuryNoteCashReceiptsDetail = CType(INDgvCashReceipts.GetFocusedRow(), TreasuryNoteCashReceiptsDetail)
            If cashReceipts.Id > 0 Then
                cashReceipts.MarkAsDeleted()
            End If
            ListTreasuryNoteCashReceiptsDetail.Remove(cashReceipts)
            INDgcCashReceipts.DataSource = Nothing
            INDgcCashReceipts.DataSource = ListTreasuryNoteCashReceiptsDetail
            If _treasuryNote.ChangeTracker.State <> ObjectState.Added Then
                _treasuryNote.MarkAsModified()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handler para acciones del grid de recibos de caja
    ''' </summary>
    Private Sub IndigoGridView11_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView11.Click_ButtonAction, IndigoGridView11.ContexMenuActions
        RemoveCashReceipt()
    End Sub

    ''' <summary>
    ''' Handler para acciones del grid de conceptos de nota
    ''' </summary>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        ' Obtener el tag correctamente
        Dim senderTag As String = String.Empty
        If sender.Tag IsNot Nothing Then
            senderTag = sender.Tag.ToString()
        End If

        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString()
        End If

        ' Grid de conceptos de nota
        If senderTag = "Edit" OrElse senderTag = ResourceManager.GetString("Edit") Then
            EditNoteConcept()
        ElseIf senderTag = "Remove" OrElse senderTag = ResourceManager.GetString("Remove") Then
            DeleteNoteConcept()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleEntityBankAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityBankAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityBankAccount.QueryPopUp
        If Not _flagEntityAccount Then
            _flagEntityAccount = True
            _presenter.InitializeEntityBankAccount()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCash_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCash.QueryPopUp
        If Not _flagCashRegister Then
            _flagCashRegister = True
            _presenter.InitializeCash()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If Not _flagCostCenter Then
            _flagCostCenter = True
            _presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Handles the DataSourceChanged event of the INDgcNoteConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgcNoteConcept_DataSourceChanged(sender As Object, e As EventArgs) Handles INDgcNoteConcept.DataSourceChanged
        'INDgleNoteType.Properties.ReadOnly = INDgcNoteConcept.DataSource IsNot Nothing AndAlso CType(INDgcNoteConcept.DataSource, List(Of TreasuryNoteDetail)).Count > 0
    End Sub
    ''' <summary>
    ''' Metodo para importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDsbImport_Click(sender As Object, e As EventArgs) Handles INDsbImport.Click

        Await ImportFile()
        AsyncLoader(False)
    End Sub




#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
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
        varImp = 1
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Clcik Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir

        'Dim reportDef As New Reporter.rptNotes()
        'Me.BarraBotones.PrintReport(reportDef, Me._treasuryNote.Id, True, Me.Tag, Nothing, "FrmNotes")
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _treasuryNote.Id, 0, _treasuryNote.Id)
    End Sub

    Private Sub BarraBotones_QueryLoadReportsAndDefinitions(sender As Object, e As LoadReportsAndDefinitionsEventArgs) Handles BarraBotones.QueryLoadReportsAndDefinitions
        e.ListReportsAndDefinitions = e.ListReportsAndDefinitions.Where(Function(r) r.CodeEntity IsNot Nothing AndAlso r.CodeEntity.Trim().Equals(If(NoteType.ToString() = "7", "4", NoteType.ToString()))).ToList()
    End Sub

#End Region

#Region "Enums"

    ''' <summary>
    ''' Enumeración de tipo de nota
    ''' </summary>
    Public Enum eNoteType
        EntityAccount = 1
        CashRegister = 2
        UnConfirmVoucherTransaction = 3
        UnConfirmCashReceipt = 4
        UnConfirmConsignment = 5
        ReversionCrossAccount = 6
        DevolutionCashReceipt = 7
        CardReconciliationExpenses = 8
    End Enum

    Private Sub INDSleCrossAccount_EditValueChanged_1(sender As Object, e As EventArgs) Handles INDSleCrossAccount.EditValueChanged

    End Sub







#End Region

End Class