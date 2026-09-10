'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 10-07-2014
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
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Infrastructure.CrossCutting.Xpo.Base.Extensions
Imports Presentation.Base.Eform
Imports Domain.Base.Entities
Imports System.Text
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Presentation.Accounting.MVP
Imports Presentation.Common
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Presentation.Common.MVP
Imports System.Globalization

#End Region

Public Class FrmConsignment
    Implements IConsignmentTransfer, ICustomizableForm

#Region "Builder"
    Public Sub New()
        InitializeComponent()
        ctrAdvance = New CtrAdvanceCxC()
        ctrAdvance.Title = "Valor Consignación"
        ctrAdvance.SetAdvance(AddressOf getRefundValue)
        ctrAdvance.PrintValue()
        ctrAdvance.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrAdvance)
        IndigoGridControl1.SetHideNoRecords(INDgcCash, True)
    End Sub

    Private Function getRefundValue() As Decimal
        Return Value
    End Function
#End Region

#Region "Properties and Variables"

#Region "Variables"
    Private Const MODULE_NAME As String = "Treasury"
    ''' <summary>
    ''' The CTR advance
    ''' </summary>
    Dim ctrAdvance As CtrAdvanceCxC
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
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _consignment As Consignment
    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PConsignmentTransfer
    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim _record As BlockRecordTreasury
    ''' <summary>
    ''' tupla que contiene el tipo del documento (1-consignacion,2-traslado)
    ''' </summary>
    Dim _tupleType As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' variable para conocer si es la primera vez que se abre el popup para cargar el datasource
    ''' </summary>
    Private _statePopUpEntityAccountOpen As Boolean
    ''' <summary>
    ''' The _state pop up cost center
    ''' </summary>
    Private _statePopUpCostCenterOpen As Boolean
    ''' <summary>
    ''' The _state pop up cash open
    ''' </summary>
    Private _statePopUpCashOpen As Boolean
    ''' <summary>
    ''' Bandera que me permite editar una caja
    ''' </summary>
    Private _bandEditCash As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "Properties"

    Public Event LoadControlsFinish(_consignment As Consignment)

    ''' <summary>
    ''' Obtiene o establece el id de las cajas
    ''' </summary>
    Public Property CashRegisterId As Integer
        Get
            Return INDsleCash.EditValue
        End Get
        Set(value As Integer)
            INDsleCash.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el valor a consignar de la caja
    ''' </summary>
    Public Property ValueCash As Decimal
        Get
            Return INDtxtConsignmentValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtConsignmentValue.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IConsignmentTransfer.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IConsignmentTransfer.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    Public Property Sequense As TreasurySequence Implements IConsignmentTransfer.Sequense
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
    ''' Consecutivo
    ''' </summary>
    Public Property Code As String Implements IConsignmentTransfer.Code
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
    ''' Obtiene o establece el id de centros de costo
    ''' </summary>
    Public Property CostCenterId As Integer? Implements IConsignmentTransfer.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    Public Property Description As String Implements IConsignmentTransfer.Description
        Get
            Return INDmeDetail.Text
        End Get
        Set(value As String)
            INDmeDetail.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    Public Property DocumentDate As Date Implements IConsignmentTransfer.DocumentDate
        Get
            Return INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            INDdeDocumentDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de cuentas bancarias
    ''' </summary>
    Public Property EntityBankAccountId As Integer Implements IConsignmentTransfer.EntityBankAccountId
        Get
            Return INDsleEntityAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleEntityAccount.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de cuentas contables
    ''' </summary>
    Public Property MainAccountId As Integer Implements IConsignmentTransfer.MainAccountId
    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As String Implements IConsignmentTransfer.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As String)
            BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el valor de la consignacion / traslado
    ''' </summary>
    Public Property Value As Decimal Implements IConsignmentTransfer.Value
    ''' <summary>
    ''' Obtiene o establece el datasource de los centros de costo
    ''' </summary>
    Public Property CostCenterDatasource As XPInstantFeedbackSource Implements IConsignmentTransfer.CostCenterDatasource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas bancarias
    ''' </summary>
    Public Property EntityBankAccountDatasource As LinqInstantFeedbackSource Implements IConsignmentTransfer.EntityBankAccountDatasource
        Get
            Return CType(INDsleEntityAccount.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEntityAccount.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de las cajas
    ''' </summary>
    ''' <value>
    ''' The cash datasource.
    ''' </value>
    Public Property CashDatasource As LinqInstantFeedbackSource Implements IConsignmentTransfer.CashDatasource
        Get
            Return CType(INDsleCash.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleCash.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource del detalle de las consignaciones
    ''' </summary>
    Public Property ConsignmentDetailDatasource As List(Of ConsignmentDetail)
        Get
            Return CType(INDgcCash.DataSource, List(Of ConsignmentDetail))
        End Get
        Set(value As List(Of ConsignmentDetail))
            INDgcCash.DataSource = value
        End Set
    End Property

    Private _currencyId As Integer?
    Public Property CurrencyId As Integer?
        Get
            Return _currencyId
        End Get
        Set(value As Integer?)
            _currencyId = value
            Me.INDsleCurrencyBankAccount.EditValue = value
        End Set
    End Property

    Private _currencyAbbreviation As String
    Public Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
            Me.INDsleCurrencyBankAccount.Properties.NullText = value
            SetCurrencyUI(value)
        End Set
    End Property

    Private _tRMValue As Decimal
    Public Property TRMValue As Decimal
        Get
            Return _tRMValue
        End Get
        Set(value As Decimal)
            _tRMValue = value
            Me.INDtxtTRM.EditValue = Utils.VisibleTRM(value)
        End Set
    End Property

    Public Property ValueInCurrencyHeader As Decimal
        Get
            Return Me.INDtxtValueInCurrencyHeader.EditValue
        End Get
        Set(value As Decimal)
            Me.INDtxtValueInCurrencyHeader.EditValue = value
        End Set
    End Property

    Private _currencyCash As Tuple(Of Integer?, String)
    Public Property CurrencyCash As Tuple(Of Integer?, String)
        Get
            Return _currencyCash
        End Get
        Set(value As Tuple(Of Integer?, String))
            _currencyCash = value
        End Set
    End Property

    ''' <summary>
    ''' retorna un clone de la cultura actual pero con el formato moneda especifico
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CultureNumbertFormat As CultureInfo
        Get
            Dim Culture As CultureInfo = CultureInfo.CurrentCulture.Clone()
            Culture.NumberFormat = Me.CurrencyAbbreviation.GetNumberFormat
            Return Culture
        End Get
    End Property
#End Region

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrAdvance = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _consignment = Nothing
        _presenter = Nothing
        _record = Nothing
        _record = Nothing
        _tupleType = Nothing
        _statePopUpEntityAccountOpen = Nothing
        _statePopUpCostCenterOpen = Nothing
        _statePopUpCashOpen = Nothing
        _bandEditCash = Nothing
        varImp = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmAppropriationTransfers control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmAppropriationTransfers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PConsignmentTransfer(Me)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()
        '_presenter.InitializeCash(2) '2 -Caja Mayor

        Dim listActions As New List(Of eAcciones)()
        listActions.Add(eAcciones.Remove)
        listActions.Add(eAcciones.Edit)
        listActions.Add(eAcciones.Add)
        IndigoGridView.SetListAcction(INDgvCash, listActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvCash.Columns
            If col.Name = "colActions" Then
                col.Width = 150
            End If
        Next

        IndigoGridControl1.RefreshGrid(INDgcCash)

        _statePopUpEntityAccountOpen = False
        _statePopUpCostCenterOpen = False
        _statePopUpCashOpen = False

        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the FormClosing event of the FrmAppropriationTransfers control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmAppropriationTransfers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
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
                    Await Me.NewConsignment()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpceAddCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddCash_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddCash.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpceAddCash.ShowPopup()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmEntityAccount
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeEntityBankAccount()
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
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeCostCenter()
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
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                _presenter.InitializeCash(2)
            End Using
        End If
    End Sub
#End Region

#Region "Actions"
    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView.Click_ButtonAction, IndigoGridView.ContexMenuActions
        Select Case (sender.Tag)
            Case "Add", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Add")
                INDpceAddCash.ShowPopup()
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                EditCash()
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteCashFromGrid()
        End Select
        BlockBankAccount()
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntityAccount.QueryPopUp
        If Not _statePopUpEntityAccountOpen Then
            _presenter.InitializeEntityBankAccount()
            _statePopUpEntityAccountOpen = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If Not _statePopUpCostCenterOpen Then
            _presenter.InitializeCostCenter()
            _statePopUpCostCenterOpen = True
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDpceAddCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddCash_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDpceAddCash.QueryPopUp
        Me.INDsleCash.Enabled = Not Me._bandEditCash
        INDsleCash.Focus()
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCash_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCash.QueryPopUp
        If Not _statePopUpCashOpen Then
            _presenter.InitializeCash(2) '2 -Caja Mayor
            _statePopUpCashOpen = True
        End If
    End Sub
#End Region

#Region "Identity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._consignment IsNot Nothing AndAlso _consignment.Id > 0 Then
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

#Region "Leave"
    ''' <summary>
    ''' Handles the Leave event of the INDtxtConsignmentValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtConsignmentValue_Leave(sender As Object, e As EventArgs) Handles INDtxtConsignmentValue.Leave

    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleCash_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCash.EditValueChanged
        If INDsleCash.EditValue Is Nothing Then
            Exit Sub
        End If

        If ConsignmentDetailDatasource?.Any(Function(x) x.CashRegisterId = CashRegisterId AndAlso Not _bandEditCash) Then
            INDsleCash.EditValue = Nothing
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CashRegisterExistInList", MODULE_NAME)
            Exit Sub
        End If

        Dim CashBalance As Decimal = 0

        If _bandEditCash Then
            Dim _cashRegisterLinq As ConsignmentDetail = TryCast(INDgvCash.GetFocusedRow(), ConsignmentDetail)
            CashBalance = _cashRegisterLinq.CurrentBalance
            INDlblMainAccount.Text = _cashRegisterLinq.FullNameMainAccount
            INDlblCostCenter.Text = _cashRegisterLinq.FullNameCostCenter
            Me.CurrencyCash = New Tuple(Of Integer?, String)(_cashRegisterLinq?.CashCurrencyId, _cashRegisterLinq?.CashCurrencyAbbreviation)
        Else
            Dim _cashRegister As CashRegisterXpo = TryCast(TryCast(INDgvCashXpo.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow, CashRegisterXpo)
            If _cashRegister Is Nothing Then Exit Sub

            CashBalance = _cashRegister.CurrentBalance
            INDlblMainAccount.Text = _cashRegister.IdMainAccount.NumberName
            INDlblCostCenter.Text = _cashRegister?.IdCostCenter?.CodeName
            Me.INDliCostCenterCash.HideControl(_cashRegister?.IdCostCenter Is Nothing)
            Me.CurrencyCash = New Tuple(Of Integer?, String)(_cashRegister?.CurrencyId, _cashRegister?.CurrencyAbbreviation)
        End If

        INDlblCashBalance.Text = Utils.GetMoneyWithISO4217(CashBalance, Me.CurrencyCash?.Item2)
        Me.INDLciTRM.HideControl(Me.CurrencyId = Me.CurrencyCash?.Item1)
        Me.INDLciValueInCurrencyHeader.HideControl(Me.CurrencyId = Me.CurrencyCash?.Item1)
        Me.INDLciValueInCurrencyHeader.Text = $"Valor ({Me.CurrencyAbbreviation})"
        INDtxtConsignmentValue.Properties.Mask.Culture = New Globalization.CultureInfo(CurrencyCash.Item2.GetCultureId())
        AsyncLoader(True)
        Await ValidateCurrencyEditValue(CurrencyCash?.Item1)
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityAccount.EditValueChanged
        INDliCostCenter.HideControl(True)
        If INDgvEntityAccountXpo.DataSource IsNot Nothing AndAlso INDgvEntityAccountXpo.GetFocusedRow() IsNot Nothing Then
            Dim _entityAccountXpo As EntityBankAccountXpo = DirectCast(DirectCast(INDgvEntityAccountXpo.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, EntityBankAccountXpo)
            If EntityBankAccountId <> 0 Then
                MainAccountId = _entityAccountXpo.IdMainAccount.Id
                Me.CurrencyId = _entityAccountXpo?.CurrencyId
                Me.CurrencyAbbreviation = _entityAccountXpo?.CurrencyAbbreviation
                If _entityAccountXpo.IdMainAccount.HandlesCostCenter Then
                    INDliCostCenter.HideControl(False)
                End If
            Else
                MainAccountId = 0
            End If
        End If
        CleanControlsCash()
    End Sub
#End Region

#Region "EditvalueChanging"
    ''' <summary>
    ''' bandera de control para los eventos editvaluechanging
    ''' </summary>
    Private _controlFlag As Boolean = True
    ''' <summary>
    ''' evento para cambiar ir calculando continuamente el valor del campo consignacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtValueInCurrencyHeader_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDtxtValueInCurrencyHeader.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) Then
            e.Cancel = True
            Exit Sub
        End If
        If Not _controlFlag Then
            Exit Sub
        End If
        _controlFlag = False
        Me.ValueCash = CDec(Convert.ToDecimal(e.NewValue, CultureInfo.InvariantCulture) * Me.TRMValue)
        _controlFlag = True
    End Sub

    ''' <summary>
    ''' evento para cambiar ir calculando continuamente el valor del campo valor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDtxtConsignmentValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDtxtConsignmentValue.EditValueChanging
        If String.IsNullOrEmpty(e.NewValue) Then
            e.Cancel = True
            Exit Sub
        End If
        If Not _controlFlag Then
            Exit Sub
        End If
        _controlFlag = False
        Me.ValueInCurrencyHeader = CDec(Convert.ToDecimal(e.NewValue, CultureInfo.InvariantCulture) / Me.TRMValue)
        _controlFlag = True
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddCash_Click(sender As Object, e As EventArgs) Handles INDsbAddCash.Click
        If ValidateCash() Then
            AddCashToList()
        End If
        BlockBankAccount()
    End Sub
#End Region

#Region "CloseUp"
    ''' <summary>
    ''' Handles the CloseUp event of the INDpceAddCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.CloseUpEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceAddCash_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceAddCash.CloseUp
        If _bandEditCash Then
            CleanControlsCash()
            _bandEditCash = False
        End If
    End Sub
#End Region

#End Region

#Region "IcrudBase"
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
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Guardars the specified with confirm.
    ''' </summary>
    Public Async Sub Guardar(withConfirm As Boolean)
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MConsignment(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result = Await Model.SaveConsignmentTransfer(Me._consignment, withConfirm, Me._idCurrentSequence)
                If result.StateResult = True Then
                    Me._consignment = result.ObjectEmbbeded
                    If withConfirm Then
                        If String.IsNullOrEmpty(result.Message) Then
                            Dim typeJournal As ActionResult(Of JournalVoucherTypes)
                            Using modelType As New MDocumentType(Me.Tag)
                                typeJournal = modelType.GetJournalVoucherById(CType(result.MessageResult(1), Integer))
                            End Using
                            If _consignment.Id <> 0 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("UpdateAndConfirmSatisfactory"), result.ObjectEmbbeded.Code, String.Concat(typeJournal.ObjectEmbbeded.Code, " - ", typeJournal.ObjectEmbbeded.Name), result.MessageResult(0))
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _consignment.Id, _consignment.Id)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmSatisfactory"), result.ObjectEmbbeded.Code, String.Concat(typeJournal.ObjectEmbbeded.Code, " - ", typeJournal.ObjectEmbbeded.Name), result.MessageResult(0))
                                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _consignment.Id, _consignment.Id)
                            End If
                        Else
                            If _consignment.Id <> 0 Then
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("UpdateButNotConfirmed"), result.ObjectEmbbeded.Code, result.Message)
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, _consignment.Id, _consignment.Id)
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SavedButNotConfirmed", MODULE_NAME), result.ObjectEmbbeded.Code, result.Message)
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, _consignment.Id, _consignment.Id)
                            End If
                        End If
                    Else
                        If _consignment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
                            End If
                            If Me._sequence.Sequential Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                            End If
                        ElseIf _consignment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _consignment.Id, _consignment.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _consignment.Id, _consignment.Id)
                    End Select
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
    Private Async Sub Confirmar()
        Dim resultOption As DialogResult
        Try
            Using Model As New MConsignment(Me.Tag.ToString())
                Dim auxConsignment As Consignment = (Await Model.GetConsignmentTransferById(_consignment.Id))
                If ValidateEntity(auxConsignment) Then
                    resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo)
                Else
                    resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessageWithUpdate"), MessageType.Question, Me.Text, Botones.SiNo)
                End If
                If resultOption = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    Dim result = Await Model.ConfirmConsignmentTransfer(Me._consignment.Id)
                    If result.StateResult = True Then
                        Dim consecutive = result.ObjectEmbbeded
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("DocumentConfirmWithConsecutive"), consecutive)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _consignment.Id, _consignment.Id)
                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.Message IsNot Nothing Then
                            generateListError(result.Message)
                        End If
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
    ''' Anulars this instance.
    ''' </summary>
    Public Async Sub Anular()
        If Me._consignment IsNot Nothing AndAlso Me._consignment.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                _consignment.Status = 3
                Try
                    Using Model As New MConsignment(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.SaveConsignmentTransfer(Me._consignment, False, Me._idCurrentSequence)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me._consignment = result.ObjectEmbbeded
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _consignment.Id, _consignment.Id)
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
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
    ''' valida la entidad para ver si hubu cambios
    ''' </summary>
    Private Function ValidateEntity(ByVal auxConsignment As Consignment) As Boolean
        If (auxConsignment.Id = _consignment.Id AndAlso auxConsignment.Code.Equals(_consignment.Code) AndAlso auxConsignment.EntityBankAccountId = _consignment.EntityBankAccountId AndAlso
            auxConsignment.Description.Equals(_consignment.Description) AndAlso auxConsignment.Status = _consignment.Status AndAlso auxConsignment.MainAccountId = _consignment.MainAccountId AndAlso
            auxConsignment.Description.Equals(_consignment.Description) AndAlso auxConsignment.Value = _consignment.Value) Then
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Me.NewConsignment()
        End If
    End Sub
#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' Edits the cash.
    ''' </summary>
    Private Sub EditCash()
        _bandEditCash = True
        Dim _detailConsignment As ConsignmentDetail = CType(INDgvCash.GetFocusedRow(), ConsignmentDetail)
        ValueCash = _detailConsignment.Value
        INDsleCash.Properties.NullText = _detailConsignment.FullNameCashRegister
        CashRegisterId = _detailConsignment.CashRegisterId
        INDpceAddCash.ShowPopup()
    End Sub

    ''' <summary>
    ''' Validates the cash.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateCash() As Boolean
        Dim errorList As New StringBuilder()
        If CashRegisterId = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Caja"))
        End If
        If ValueCash = 0 Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Valor a Consignar"))
        End If

        Dim _cashRegister = Nothing
        If _bandEditCash Then
            _cashRegister = CType(INDgvCash.GetFocusedRow(), ConsignmentDetail)
        Else
            _cashRegister = DirectCast(DirectCast(INDgvCashXpo.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CashRegisterXpo)
        End If
        If ValueCash > _cashRegister.CurrentBalance Then
            errorList.AppendLine("El valor a consignar no debe superar el saldo de la caja")
        End If

        If Me.INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso Me.ValueInCurrencyHeader = 0 Then
            errorList.AppendLine("El valor a consignar convertido en la moneda del banco no puede ser 0")
        End If

        If errorList.Length = 0 Then
            Return True
        Else
            Mensaje(EeventViewerImages.Advertencia) = errorList.ToString()
            Return False
        End If
    End Function

    ''' <summary>
    ''' Agrega la Nueva Caja al listado
    ''' </summary>
    Private Sub AddCashToList()
        Dim _consignmentDetail As ConsignmentDetail
        Dim _cashRegister = Nothing
        Dim MainAccountId As Integer = 0
        Dim CostCenterId As Integer? = Nothing
        Dim FullNameCostCenter As String = String.Empty
        Dim FullNameCashRegister As String = String.Empty
        Dim FullNameMainAccount As String = String.Empty
        Dim BalanceCash As Decimal = 0
        If _bandEditCash Then
            _consignmentDetail = CType(INDgvCash.GetFocusedRow(), ConsignmentDetail)
            MainAccountId = _consignmentDetail.MainAccountId
            CostCenterId = _consignmentDetail.CostCenterId
            FullNameCostCenter = _consignmentDetail.FullNameCostCenter
            FullNameCashRegister = _consignmentDetail.FullNameCashRegister
            FullNameMainAccount = _consignmentDetail.FullNameMainAccount
            BalanceCash = _consignmentDetail.CurrentBalance
        Else
            _cashRegister = DirectCast(INDsleCash.GetSelectedObject(), CashRegisterXpo)
            MainAccountId = _cashRegister.IdMainAccount.Id
            If _cashRegister.IdCostCenter IsNot Nothing Then
                CostCenterId = CType(_cashRegister.IdCostCenter.Id, Integer)
                FullNameCostCenter = _cashRegister.IdCostCenter.CodeName
            End If
            FullNameCashRegister = _cashRegister.CodeName
            FullNameMainAccount = _cashRegister.IdMainAccount.NumberName
            BalanceCash = _cashRegister.CurrentBalance
            _consignmentDetail = New ConsignmentDetail()
        End If

        With _consignmentDetail
            .CashRegisterId = CashRegisterId
            .CurrentBalance = BalanceCash
            .MainAccountId = MainAccountId
            If CostCenterId IsNot Nothing Then
                .CostCenterId = CostCenterId
                .FullNameCostCenter = FullNameCostCenter
            End If
            .Value = ValueCash
            .FullNameCashRegister = FullNameCashRegister
            .FullNameMainAccount = FullNameMainAccount
            .ValueInCurrencyHeader = Me.ValueInCurrencyHeader
            .CashCurrencyId = Me.CurrencyCash?.Item1
            .CashCurrencyAbbreviation = Me.CurrencyCash?.Item2
        End With
        If Not _bandEditCash Then
            _consignment.ConsignmentDetail.Add(_consignmentDetail)
        Else
            _consignment.MarkAsModified()
        End If
        ConsignmentDetailDatasource = _consignment.ConsignmentDetail.ToList()
        CalculateValue()
        INDgcCash.RefreshDataSource()
        CleanControlsCash()
    End Sub

    ''' <summary>
    ''' Limpia los controles de agregar cajas
    ''' </summary>
    Private Sub CleanControlsCash()
        INDsleCash.EditValue = Nothing
        INDsleCash.Properties.NullText = String.Empty
        INDlblCashBalance.Text = String.Empty
        INDlblCostCenter.Text = String.Empty
        INDlblMainAccount.Text = String.Empty
        ValueCash = 0D
        Me.ValueInCurrencyHeader = 0D
        Me.TRMValue = 1
        Me.CurrencyCash = Nothing
        Me.INDLciTRM.HideControl()
        Me.INDLciValueInCurrencyHeader.HideControl()
        INDsleCash.Focus()
    End Sub

    ''' <summary>
    ''' Calcula el valor de las cajas y lo pinta en el control.
    ''' </summary>
    Private Sub CalculateValue()
        If ConsignmentDetailDatasource IsNot Nothing Then
            Value = ConsignmentDetailDatasource.Sum(Function(x) x.ValueInCurrencyHeader)
        Else
            Value = 0
        End If
        ctrAdvance.PrintValue()
    End Sub

    ''' <summary>
    ''' News the consignment transfer.
    ''' </summary>
    Private Async Function NewConsignment() As Task
        DocumentDate = Me.GetDateServer()
        _consignment = New Consignment()
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
    End Function

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatusReverse"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Public Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Using Model As New MConsignment(Me.Tag)
                AsyncLoader(True)
                Dim resultOperation = Await Model.GetConsignmentTransfer(Me.Code, True)
                INDlycRoot.BeginUpdate()
                _consignment = resultOperation.ObjectEmbbeded
                If Not _consignment Is Nothing Then
                    If _consignment.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                            Dim result = Await ModelCommonTreasury.GetBlockRecordTreasury(Me.Tag, _consignment.Id)
                            With _consignment
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Code = .Code
                                DocumentDate = .DocumentDate
                                EntityBankAccountId = .EntityBankAccountId
                                MainAccountId = .MainAccountId
                                CostCenterId = .CostCenterId
                                Description = .Description
                                Value = .Value
                                Status = .Status.ToString()
                                Me.CurrencyId = .CurrencyId
                                Me.CurrencyAbbreviation = .CurrencyAbbreviation
                            End With
                            If _consignment.CostCenterId IsNot Nothing Then
                                INDliCostCenter.HideControl(False)
                            End If
                            LoadNullText()
                            Me.GetDocumentIndexed(Me.Tag & "_" & Me._consignment.Code)
                            If result.Id = 0 Then
                                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                                state.State = Domain.Base.Entities.ObjectState.Added
                                _record = New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _consignment.Id}
                                Dim operation = Await ModelCommonTreasury.SaveBlockRecordTreasury(_record)
                                _record = operation.ObjectEmbbeded
                            Else
                                _record = result
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_consignment.Id, Me.Tag.ToString(), Nothing, GetType(Consignment).Name)
                            Select Case Me._consignment.Status
                                Case 1
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
                                Case Else
                                    ReadOnlyControls(True)
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                            End Select
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _consignment.Id, 0, _consignment.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            ConsignmentDetailDatasource = _consignment.ConsignmentDetail.ToList()
                            INDgcCash.RefreshDataSource()
                            INDbteCode.Enabled = False
                            CalculateValue()
                            BlockBankAccount()
                            RaiseEvent LoadControlsFinish(_consignment)
                        End Using
                    Else
                        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        'Me.Code = String.Empty
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewConsignment()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Me.Code = String.Empty
                            Deshacer()
                            INDbteCode.Focus()
                        End If
                    End If
                Else
                    'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                    'Me.Code = String.Empty
                    'Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Me.NewConsignment()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                        Me.Code = String.Empty
                        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                        Deshacer()
                        INDbteCode.Focus()
                    End If
                End If
            End Using
            INDlycRoot.EndUpdate()
        End If

    End Function

    ''' <summary>
    ''' Loads the null text.
    ''' </summary>
    Private Sub LoadNullText()
        INDsleEntityAccount.Properties.NullText = _consignment.FullNameEntityBankAccount
        INDsleCostCenter.Properties.NullText = _consignment.FullNameCostCenter
    End Sub

    ''' <summary>
    ''' Deletes the cash from grid.
    ''' </summary>
    Private Sub DeleteCashFromGrid()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim _consignmentDetail As ConsignmentDetail = CType(INDgvCash.GetFocusedRow(), ConsignmentDetail)
            _consignmentDetail.MarkAsDeleted()
            ConsignmentDetailDatasource = _consignment.ConsignmentDetail.ToList()
            INDgcCash.RefreshDataSource()
            CalculateValue()
        End If
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._consignment.Code, Me._consignment.DocumentDate, Me.INDsleEntityAccount.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._consignment.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._consignment.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._consignment.Code, Me._consignment.DocumentDate, Me.INDsleEntityAccount.Text)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._consignment.Code)
            Return Me._doc
        End If
    End Function



    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _consignment
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .EntityBankAccountId = EntityBankAccountId
            .MainAccountId = MainAccountId
            .CostCenterId = CostCenterId
            .Description = Description
            .Value = Value
            .OperativeUnitId = _idOperativeUnit
            .Status = If(String.IsNullOrEmpty(Status) OrElse Status.Equals("0"), 1, CByte(Status))
            .CurrencyId = Me.CurrencyId
            .CurrencyAbbreviation = Me.CurrencyAbbreviation
        End With
        If _consignment.Id = 0 Then
            _consignment.MarkAsAdded()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "EntityBankAccountId.CurrencyAbbreviation", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "Value", .ColumnFormat = "N2", .FormatCulture = CultureNumbertFormat, .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListConsignment
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConsignmentTransfer.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDdeDocumentDate.Enabled = value
            INDsleEntityAccount.Enabled = value
            INDsleCostCenter.Enabled = value
            INDmeDetail.Enabled = value
            INDpceAddCash.Enabled = value
            INDgcCash.Enabled = value
            INDlycRoot.EndUpdate()

            Me.BarraBotones.StatusRecordVisible = value

            Using ModelBase As New MSettingsTreasury(MyTag)
                Dim setting = ModelBase.GetSettingsTreasuryByIdUnitOperativeSimple(BarraBotones.OperatingUnitValue)
                If setting IsNot Nothing Then
                    INDdeDocumentDate.Properties.MinValue = CDate(GetDateServer()).AddDays(-setting.ObjectEmbbeded.NumberDayBank)
                ElseIf value Then
                    INDdeDocumentDate.Focus()
                    INDdeDocumentDate.Properties.ReadOnly = True
                Else
                    INDbteCode.Focus()
                End If
            End Using

        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls() Implements IConsignmentTransfer.CleanControls
        INDlycRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing


        Code = String.Empty
        INDdeDocumentDate.EditValue = Nothing
        INDsleEntityAccount.EditValue = Nothing
        INDsleCostCenter.EditValue = Nothing
        INDmeDetail.Text = String.Empty
        INDlycRoot.EndUpdate()
        CleanControlsCash()

        ReadOnlyControls(False)

        INDsleEntityAccount.Properties.NullText = String.Empty
        INDsleEntityAccount.EditValue = Nothing
        INDliCostCenter.HideControl(True)
        INDgcCash.DataSource = Nothing
        MainAccountId = 0
        Me.CurrencyId = indigo.OfficialCurrencyId
        Me.CurrencyAbbreviation = indigo.CurrencyISO4217
        _bandEditCash = False
        Me.Value = 0F
        ctrAdvance.PrintValue()
        _consignment = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        DeleteBlockedRecord()
    End Sub

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
    ''' funcion Valida y establece el evento cuando la moneda cambia 
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ValidateCurrencyEditValue(_currencyId As Integer?) As Task
        If _currencyId Is Nothing OrElse _currencyId = 0 OrElse {2, 3, 4}.Contains(Me.Status) Then
            Return
        End If

        If Not Await GetTRM(_currencyId, Me.CurrencyId) Then
            Me.CleanControlsCash()
            Me.INDpceAddCash.ClosePopup()
            Return
        End If

        Me.ValueInCurrencyHeader = Me.ValueCash / Me.TRMValue
        Return
    End Function

    ''' <summary>
    ''' Funcion que se encarga de consultar el TRM
    ''' </summary>
    ''' <param name="_currencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Private Async Function GetTRM(_currencyId As Integer, ToCurrencyId As Integer) As Task(Of Boolean)
        If _currencyId = ToCurrencyId Then
            Me.TRMValue = 1
            Return True
        End If

        Using Model As New MConsignment(Me.Tag.ToString())
            Dim Result = Await Model.GetTRMbyCurrencyId(ToCurrencyId, _currencyId)
            If Result Is Nothing OrElse Not Result?.StateResult Then
                Me.Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                Return False
            End If
            Me.TRMValue = Result.ObjectEmbbeded.Value
            Return True
        End Using
    End Function

    ''' <summary>
    ''' funcion para bloquear el control de cuenta bancaria cuando exista agregado un detalle
    ''' </summary>
    Private Sub BlockBankAccount()
        Me.INDsleEntityAccount.Enabled = (Me.ConsignmentDetailDatasource Is Nothing OrElse Not Me.ConsignmentDetailDatasource?.Any())
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
        Me.BandedGridColumn6 = Window.Utils.FormatGrid(Me.BandedGridColumn6, _CurrencyAbbreviation)
        INDtxtValueInCurrencyHeader.Properties.Mask.Culture = New Globalization.CultureInfo(_CurrencyAbbreviation.GetCultureId())
        ctrAdvance.CurrencyCodeISO4217 = _CurrencyAbbreviation
        ctrAdvance.PrintValue()
    End Sub

#End Region

#Region "BarButtonEvents"
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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
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
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Guardar(False)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _consignment.Id, _consignment.Id)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub
#End Region

End Class