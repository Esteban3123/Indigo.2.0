'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 18-07-2014
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
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Payments.MVP
Imports DevExpress.Data.Linq
Imports DevExpress.Data.Async.Helpers

#End Region

Public Class FrmPettyCashReimbursements
    Implements IRefund

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        ctrAdvance = New CtrAdvanceCxC()
        ctrAdvance.Title = "Valor Reembolso"
        ctrAdvance.SetAdvance(AddressOf getRefundValue)
        ctrAdvance.PrintValue()
        ctrAdvance.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrAdvance)
    End Sub

    Private Function getRefundValue() As Decimal
        Return Value
    End Function
#End Region

#Region "Variables and Properties"

    ''' <summary>
    ''' Control para mostrar el valor del reembolso
    ''' </summary>
    Dim ctrAdvance As CtrAdvanceCxC
    ''' <summary>
    ''' constante que contiene el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

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
    ''' The list voucher transaction
    ''' </summary>
    Private _listVoucherTransaction As List(Of VoucherTransaction)

    ''' <summary>
    ''' variable bandera utilizada para saber si se desea confirmar al momento de guardar
    ''' </summary>
    Private _withConfirm As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRefund.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements IRefund.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As TreasurySequence Implements IRefund.Sequense
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
    ''' Obtiene o establece el codigo del reembolso
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Public Property Code As String Implements IRefund.Code
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
    ''' Obtiene o establece la caja del reembolso
    ''' </summary>
    ''' <value>
    ''' The identifier cash register.
    ''' </value>
    Public Property IdCashRegister As Integer Implements IRefund.IdCashRegister
        Get
            Return INDsleCashRegister.EditValue
        End Get
        Set(value As Integer)
            INDsleCashRegister.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el objeto seleccionado del recibo de caja
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property selectedCash As Infrastructure.Data.Xpo.TreasuryRepository.CashRegisterXpo
        Get
            Return TryCast(TryCast(INDGvCashRegister.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.TreasuryRepository.CashRegisterXpo)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el detalle del reembolso
    ''' </summary>
    ''' <value>
    ''' The detail.
    ''' </value>
    Public Property Detail As String Implements IRefund.Detail
        Get
            Return INDmeDetail.Text
        End Get
        Set(value As String)
            INDmeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha final del reembolso
    ''' </summary>
    ''' <value>
    ''' The final date.
    ''' </value>
    Public Property FinalDate As DateTime Implements IRefund.FinalDate
        Get
            Return CType(INDdeFinalDate.EditValue, DateTime)
        End Get
        Set(value As DateTime)
            INDdeFinalDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha inicial del reembolso
    ''' </summary>
    ''' <value>
    ''' The initial date.
    ''' </value>
    Public Property InitialDate As DateTime? Implements IRefund.InitialDate
        Get
            Return CType(INDdeInitialDate.EditValue, DateTime?)
        End Get
        Set(value As DateTime?)
            INDdeInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor del reembolso
    ''' </summary>
    ''' <value>
    ''' The value.
    ''' </value>
    Public Property Value As Decimal Implements IRefund.Value
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property CashDatasource As LinqInstantFeedbackSource Implements IRefund.CashDatasource
        Get
            Return CType(INDsleCashRegister.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleCashRegister.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del documento
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Public Property Status As Byte Implements IRefund.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    Public Property DateServer As Date Implements IRefund.DateServer

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Private _presenter As PRefund

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordTreasury

    ''' <summary>
    ''' variable que contiene la entidad de reembolsos
    ''' </summary>
    Private _refund As Refunds

    Private _statePopUpCash As Boolean

    Dim _flagLoadControls As Boolean
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrAdvance = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _listVoucherTransaction = Nothing
        _withConfirm = Nothing
        varImp = Nothing
        DateServer = Nothing
        _presenter = Nothing
        _record = Nothing
        _refund = Nothing
        _statePopUpCash = Nothing
        _flagLoadControls = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmPettyCashReimbursements control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPettyCashReimbursements_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PRefund(Me)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()
        _presenter.LoadDateServer()
        IndigoGridControl1.RefreshGrid(INDGcVoucherTransaction)

        _withConfirm = False
        _statePopUpCash = True

        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmPettyCashReimbursements control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPettyCashReimbursements_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCashRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCashRegister_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCashRegister.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCash
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                'Inicializar
            End Using
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsbAddExpenses_Click(sender As Object, e As EventArgs) Handles INDsbAddExpenses.Click
        If IdCashRegister <> 0 Then
            Using Model As New MVoucherTransaction(Me.Tag)
                Dim resultVoucherT = Nothing
                If InitialDate Is Nothing Then
                    resultVoucherT = Await Model.ListVoucherTransactionFinalDateNotRefundExpenseType(IdCashRegister, FinalDate, 2) '2-caja menor
                Else
                    resultVoucherT = Await Model.ListVoucherTransactionBetweenDateNotRefundExpenseType(IdCashRegister, InitialDate, FinalDate, 2) '2-caja menor
                End If
                If resultVoucherT.StateResult = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("VoucherTransactionRefundEmpty", NAME_MODULE)
                Else
                    _listVoucherTransaction = resultVoucherT.ObjectEmbbeded
                    If _listVoucherTransaction IsNot Nothing AndAlso _listVoucherTransaction.Count > 0 Then
                        If _listVoucherTransaction.Any(Function(a) a.CurrencyId IsNot Nothing) Then
                            Dim currencies = _listVoucherTransaction.Where(Function(w) w.CurrencyId IsNot Nothing).Select(Function(s) s.CurrencyId).Distinct.ToList()
                            If currencies.Count > 1 Then
                                Mensaje(EeventViewerImages.Advertencia) = "Se encontraron más de una moneda en el listado de egresos"
                                Exit Sub
                            End If
                        End If

                        INDGcVoucherTransaction.DataSource = Nothing
                        INDGcVoucherTransaction.DataSource = _listVoucherTransaction
                        Value = _listVoucherTransaction.Sum(Function(x) x.Value)
                        ctrAdvance.PrintValue()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("VoucherTransactionRefundEmpty", NAME_MODULE)
                    End If
                End If
            End Using
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CashRefundNotFound", NAME_MODULE)
        End If
    End Sub

    Private Sub setFormatsControls(Optional Abbreviation As String = Nothing)
        If String.IsNullOrEmpty(Abbreviation) Then
            Abbreviation = indigo.CurrencyISO4217
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = New Globalization.CultureInfo(Abbreviation.GetCultureId()).NumberFormat
        Me.GridColumnValue = Window.Utils.FormatGrid(GridColumnValue, Abbreviation)
        ctrAdvance.CurrencyCodeISO4217 = Abbreviation
        ctrAdvance.PrintValue()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewRefund()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._refund IsNot Nothing AndAlso Me._refund.Id > 0 Then
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
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCashRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCashRegister_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCashRegister.QueryPopUp
        If _statePopUpCash Then
            _presenter.InitializeCash()
            _statePopUpCash = False
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCashRegister control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleCashRegister_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCashRegister.EditValueChanged
        If _flagLoadControls Then
            Exit Sub
        End If
        If IdCashRegister > 0 Then
            Dim cash = Nothing
            If _refund.Id <> 0 Then
                Using Model As New MCashRegister(Me.Tag)
                    cash = TryCast(Await Model.GetcashRegisterById(IdCashRegister), CashRegisters)
                End Using
            Else
                cash = TryCast(TryCast(INDGvCashRegister.GetFocusedRow(), ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, Infrastructure.Data.Xpo.TreasuryRepository.CashRegisterXpo)
            End If
            InitialDate = IIf(cash.RefundDate Is Nothing, New DateTime(1900, 1, 1), CType(cash.RefundDate, DateTime).AddSeconds(1))
            If cash.RefundDate IsNot Nothing Then
                INDdeFinalDate.Properties.MinValue = InitialDate
            End If
            Me.setFormatsControls(Me.selectedCash?.CurrencyAbbreviation)
        End If
        If _refund.Id = 0 Then
            'consultar reembolso por caja
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDdeFinalDate control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDdeFinalDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdeFinalDate.EditValueChanged
        If INDdeFinalDate.EditValue IsNot Nothing Then
            INDdeFinalDate.Properties.MaxValue = GetDateServer()
        End If
    End Sub
#End Region

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusRefunded"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer), CType(CType(0, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "0", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._refund.Code, Me._refund.IdCashRegister),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._refund.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._refund.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._refund.Code, Me._refund.IdCashRegister)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._refund.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()
        INDliAddExpenses.HideControl(False)
        INDlyRoot.BeginUpdate()
        ReadOnlyControls(False)

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = "0"
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        _flagLoadControls = False
        INDbteCode.Text = String.Empty
        INDsleCashRegister.EditValue = Nothing
        INDSleFilingUnit.EditValue = Nothing
        INDdeInitialDate.EditValue = Nothing
        INDdeFinalDate.EditValue = Nothing
        INDtxtValue.EditValue = 0
        INDmeDetail.Text = String.Empty

        INDlyRoot.EndUpdate()
        INDGcVoucherTransaction.DataSource = Nothing
        INDsleCashRegister.Properties.NullText = String.Empty
        INDmeDetail.Properties.ReadOnly = False
        Me.setFormatsControls()
        ctrAdvance.PrintValue()
        _refund = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        DeleteBlockedRecord()
        CashDatasource = Nothing
        _statePopUpCash = True
        INDlycRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _refund
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .IdCashRegister = IdCashRegister
            .FilingUnitId = INDSleFilingUnit.EditValue
            .InitialDate = InitialDate
            .FinalDate = FinalDate
            .Value = Value
            .Detail = Detail
            .Refunded = False
            .Status = If(Status = 0, 1, Status)
        End With
        If _listVoucherTransaction IsNot Nothing Then
            For Each vt As VoucherTransaction In (From l In _listVoucherTransaction Where l.Status = 2 Select l)
                _refund.VoucherTransaction.Add(vt)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Nuevo reembolso
    ''' </summary>
    Private Async Function NewRefund() As Task
        Me._refund = New Refunds()
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
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Consecutivo", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "InitialDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "Value", .ColumnFormat = "N2", .ColumnFormatType = 3, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Estado Actual", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRefund
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRefund.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDsleCashRegister.Enabled = value
            INDSleFilingUnit.Enabled = value
            INDdeFinalDate.Enabled = value
            INDmeDetail.Enabled = value
            INDsbAddExpenses.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value
            INDlycRoot.EndUpdate()
            If value Then
                INDsleCashRegister.Focus()
            Else
                INDbteCode.Focus()
            End If

        End Set
    End Property

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
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MRefund(Me.Tag)
                    'INDlycRoot.BeginUpdate()
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetRefund(Me.Code)
                    _refund = resultOperation.ObjectEmbbeded
                    If Not _refund Is Nothing Then
                        If _refund.Id > 0 Then
                            Me.BarraBotones.StatusRecordVisible = True
                            _flagLoadControls = True
                            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                                _record = Await ModelCommonTreasury.GetBlockRecordTreasury(Me.Tag, _refund.Id)
                                With _refund
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                    Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                    Code = .Code
                                    IdCashRegister = .IdCashRegister
                                    INDSleFilingUnit.EditValue = .FilingUnitId
                                    InitialDate = .InitialDate
                                    FinalDate = .FinalDate
                                    Value = .Value
                                    Me.setFormatsControls(.CurrencyAbbreviation)
                                    ctrAdvance.PrintValue()
                                    Detail = .Detail
                                    If .Refunded Then
                                        Status = 4 'estado reembolsado
                                    Else
                                        Status = .Status
                                    End If
                                End With
                                INDsleCashRegister.Properties.NullText = _refund.FullNameCashRegister
                                Me.GetDocumentIndexed(Me.Tag & "_" & Me._refund.Code)
                                If _record.Id = 0 Then
                                    _record = (Await ModelCommonTreasury.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _refund.Id})
                                    ).ObjectEmbbeded
                                Else
                                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                                End If
                                Me.BarraBotones.SetDocuments(_refund.Id, Me.Tag.ToString(), Nothing, GetType(Refunds).Name)
                                INDliAddExpenses.HideControl(False)
                                INDmeDetail.Properties.ReadOnly = False
                                If _refund.Status = 2 OrElse _refund.Status = 3 OrElse Status = 4 Then 'estado confirmado
                                    ReadOnlyControls(True)
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                    INDliAddExpenses.HideControl(True)
                                    INDmeDetail.Properties.ReadOnly = True
                                    If _refund.Status = 2 AndAlso Not _refund.Refunded Then
                                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = False
                                    End If
                                Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                End If
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                                Me.BarraBotones.PrintReport(PrintReportAction.None, Me._refund.Id, 0, Me._refund.Id)
                                AsyncLoader(False)
                                ActionsOnControls = True
                                INDGcVoucherTransaction.DataSource = _refund.VoucherTransaction.ToList()
                                INDbteCode.Enabled = False
                            End Using
                        Else
                            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            'Me.Code = String.Empty
                            AsyncLoader(False)
                            If Me._sequence.IsManual Then
                                Await Me.NewRefund()
                            Else
                                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                                Me.Code = String.Empty
                                Deshacer()
                                INDbteCode.Focus()
                            End If
                        End If
                    Else
                        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        'Me.Code = String.Empty
                        'Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRefund()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Me.Code = String.Empty
                            Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                            Deshacer()
                            INDbteCode.Focus()
                        End If
                    End If
                End Using
                'INDlycRoot.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function
#End Region

#Region "ICrud"
    Public Sub Buscar() Implements ICrudBase.Buscar

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
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me._refund IsNot Nothing AndAlso Me._refund.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MRefund(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteRefund(Me._refund)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
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
    ''' Anula el documento
    ''' </summary>
    Public Async Sub Anular()
        If Me._refund IsNot Nothing AndAlso Me._refund.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                _refund.Status = 3
                Try
                    Using Model As New MRefund(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.SaveRefund(Me._refund, False, Me._idCurrentSequence)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me._refund = result.ObjectEmbbeded
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            '_voucherTransaction.VoucherTransactionDetails.Clear()
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
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        If _listVoucherTransaction IsNot Nothing AndAlso _listVoucherTransaction.Count > 0 Then 'Se valida que en el listado no venga comprobantes registrados
            If (From l In _listVoucherTransaction Where l.Status = 1 Select l).Count > 0 Then
                Dim list = (From l In _listVoucherTransaction Where l.Status = 1 Select l).ToList
                Dim listErrors As New StringBuilder
                listErrors.AppendLine("No se puede guardar porque: ")
                list.ForEach(Sub(item) listErrors.AppendLine("El comprobante " + item.Code + " tiene estado registrado."))
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
                Exit Sub
            End If
        End If
        AssigningValues()
        Try
            Using Model As New MRefund(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveRefund(Me._refund, Me._withConfirm, Me._idCurrentSequence)
                If Result.StateResult = True Then
                    Me._refund = Result.ObjectEmbbeded
                    If _withConfirm Then
                        If String.IsNullOrEmpty(Result.Message) Then
                            If _refund.Id <> 0 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("RefundUpdateConfirmComplete", NAME_MODULE), Result.ObjectEmbbeded.Code)
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("RefundSaveConfirmComplete", NAME_MODULE), Result.ObjectEmbbeded.Code)
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
                            End If
                        Else
                            If _refund.Id <> 0 Then
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RefundSaveButNotConfirm", NAME_MODULE), Result.ObjectEmbbeded.Code, Result.Message)
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RefundUpdateButNotConfirm", NAME_MODULE), Result.ObjectEmbbeded.Code, Result.Message)
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
                            End If
                        End If
                    Else
                        If _refund.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequence.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf _refund.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
                    End Select
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If Result.Message IsNot Nothing Then
                        generateListError(Result.Message)
                    End If
                End If
                _withConfirm = False
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
    ''' Confirmars this instance.
    ''' </summary>
    Private Sub Confirmar()
        'Using Model As New MRefund(Me.Tag.ToString())
        '    If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Dim Result = Await RunAsyncOperation(Model.ConfirmRefund(Me._refund.Id, Me._idCurrentSequense))
        '        If Result.StateResult = True Then
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RefundConfirmComplete", NAME_MODULE)
        '            Me._refund = Result.ObjectEmbbeded
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            _searchMode = False
        '            Me.Deshacer()
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '        _withConfirm = False
        '    End If
        'End Using
    End Sub


    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewRefund()
        End If
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
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
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
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        _withConfirm = True
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        _withConfirm = True
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        _withConfirm = False
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirmar()
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
        _withConfirm = False
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        _withConfirm = False
        Anular()
    End Sub

    ''' <summary>
    ''' Clcik Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir

        'Dim reportDef As New Reporter.rptPettyCashReimbursements()
        'reportDef.INDIdRefund = Me._refund.Id
        'reportDef.INDDateStart = INDdeInitialDate.EditValue
        'reportDef.INDDateEnd = INDdeFinalDate.EditValue
        'Me.BarraBotones.PrintReport(reportDef, Me._refund.Id, True, Me.Tag, Nothing, "FrmPettyCashReimbursements")
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _refund.Id, 0, _refund.Id, CDate(INDdeInitialDate.EditValue), CDate(INDdeFinalDate.EditValue))
    End Sub

#End Region
    Private Async Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
        Await Task.Factory.StartNew(
            Sub()
                INDSleFilingUnit.BeginInvoke(
                                  Async Sub()
                                      Using model As New MFilingUnit(Me.Tag)
                                          Dim x As ActionResult(Of List(Of FilingUnit)) = Await model.GetFilingUnitByUser(indigo.UserIndigo)
                                          Dim listFilingUnit As List(Of FilingUnit) = x.ObjectEmbbeded
                                          FilingUnitXpo = listFilingUnit
                                      End Using
                                  End Sub)
            End Sub)
    End Sub

    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitXpo As List(Of FilingUnit)
        Get
            Return INDSleFilingUnit.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDSleFilingUnit.Properties.DataSource = value
        End Set
    End Property
End Class