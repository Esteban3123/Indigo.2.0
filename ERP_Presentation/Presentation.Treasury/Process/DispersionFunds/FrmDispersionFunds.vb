'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 09-09-2014
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
Imports DevExpress.XtraPivotGrid
Imports Domain.Base.Entities
Imports System.Text
Imports DevExpress.Data.Linq
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports System.IO
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class FrmDispersionFunds
    Implements IDispersionFunds, ICustomizableForm

#Region "Builder"

    Public Sub New()
        InitializeComponent()
        ctrSchedulePayment = New CtrSchedulePayment()
        ctrSchedulePayment.PrintAdvance()
        ctrSchedulePayment.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrSchedulePayment)
    End Sub

#End Region

#Region "Properties and Variables"
    ''' <summary>
    ''' listado de parametros para la generacion de archivos planos
    ''' </summary>
    ''' <remarks></remarks>
    Dim optionalParameters As List(Of String)

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequense As Int64
    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String
    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence
    Public Property Sequense As TreasurySequence Implements IDispersionFunds.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets the cost center identifier.
    ''' </summary>
    ''' <value>
    ''' The cost center identifier.
    ''' </value>
    Private Property CostCenterId As Integer? Implements IDispersionFunds.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property



    ''' <summary>
    ''' propiedad que contiene el numero de la nota con que se generar los comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The note number.
    ''' </value>
    Private Property NoteNumber As String
        Get
            Return INDtxtNoteNumber.EditValue
        End Get
        Set(value As String)
            INDtxtNoteNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el Id de la chequera asociada a la cuenta bancaria
    ''' </summary>
    ''' <value>
    ''' The identifier check book.
    ''' </value>
    Private Property IdCheckBook As Integer?

    ''' <summary>
    ''' propiedad que contiene el numero de cheque inicial con que se van a generar los comprobantes de egreso
    ''' </summary>
    ''' <value>
    ''' The check number.
    ''' </value>
    Private Property CheckNumber As Long
        Get
            Return CType(INDspnCheck.EditValue, Long)
        End Get
        Set(value As Long)
            INDspnCheck.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' control de programacion de pagos
    ''' </summary>
    Dim ctrSchedulePayment As CtrSchedulePayment

    ''' <summary>
    ''' Constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IDispersionFunds.MyLayoutControl
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
    Public ReadOnly Property MyTag As String Implements IDispersionFunds.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Public Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo Implements IDispersionFunds.PaymentsSettingPaymentsXpo

    ''' <summary>
    ''' Obtiene o establece el codigo de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Public Property Code As String Implements IDispersionFunds.Code
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Obtiene o establece la cuenta bancaria de los comprobantes de egreso a generar
    ''' </summary>
    ''' <value>
    ''' The entity bank account identifier.
    ''' </value>
    Public Property EntityBankAccountId As Integer? Implements IDispersionFunds.EntityBankAccountId
        Get
            Return INDsleEntityBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el metodo de pago (cheque - Nota)
    ''' </summary>
    ''' <value>
    ''' The payment method.
    ''' </value>
    Public Property PaymentMethod As Byte? Implements IDispersionFunds.PaymentMethod
        Get
            Return If(INDglePaymentType.EditValue Is Nothing, Nothing, Convert.ToByte(INDglePaymentType.EditValue))
        End Get
        Set(value As Byte?)
            INDglePaymentType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The scheduled date.
    ''' </value>
    Public Property ScheduledDate As Date Implements IDispersionFunds.ScheduledDate
        Get
            Return INDdeDate.EditValue
        End Get
        Set(value As Date)
            INDdeDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Public Property Status As Byte Implements IDispersionFunds.Status
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Byte)
            Me.BarraBotones.StatusRecord = value.ToString()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la tasa por mil
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [tax by mil]; otherwise, <c>false</c>.
    ''' </value>
    Public Property TaxByMil As Boolean? Implements IDispersionFunds.TaxByMil
        Get
            Return INDrgTaxByMil.EditValue
        End Get
        Set(value As Boolean?)
            INDrgTaxByMil.EditValue = False
        End Set
    End Property

    ''' <summary>
    ''' Contiene la lista de tipos de pago
    ''' </summary>
    Public TuplePaymentType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Gets or sets the payment method datasource.
    ''' </summary>
    ''' <value>
    ''' The payment method datasource.
    ''' </value>
    Public Property PaymentMethodDatasource As List(Of Tuple(Of Integer, String)) Implements IDispersionFunds.PaymentMethodDatasource
        Get
            Return CType(INDglePaymentType.Properties.DataSource, List(Of Tuple(Of Integer, String)))
        End Get
        Set(value As List(Of Tuple(Of Integer, String)))
            INDglePaymentType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de las cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Public Property EntityBankAccountDatasource As LinqInstantFeedbackSource Implements IDispersionFunds.EntityBankAccountDatasource
        Get
            Return CType(INDsleEntityBankAccount.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleEntityBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IDispersionFunds.CostCenterDatasource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The schedule payment datasource.
    ''' </value>
    Public Property SchedulePaymentDatasource As List(Of SP_SchedulePayment_Result) Implements IDispersionFunds.SchedulePaymentDatasource
        Get
            Return CType(INDpgcSchedulePayment.DataSource, List(Of SP_SchedulePayment_Result))
        End Get
        Set(value As List(Of SP_SchedulePayment_Result))
            INDpgcSchedulePayment.DataSource = value
            If value IsNot Nothing Then
                ctrSchedulePayment.InvoiceShareDatasource = SchedulePaymentDatasource.Where(Function(x) x.PayValue <> 0).Cast(Of SP_SchedulePayment_Result)().ToList()
                ctrSchedulePayment.PrintAdvance()
            End If
        End Set
    End Property

    Public Property OriginalDatasource As List(Of SP_SchedulePayment_Result) Implements IDispersionFunds.OriginalDatasource

    ''' <summary>
    ''' presentador de archivos de dispersion
    ''' </summary>
    Private _presenter As PDispersionFunds

    ''' <summary>
    ''' entidad de programacion de pagos
    ''' </summary>
    Private _schedulePayment As SchedulePayment

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' lista de informacion de pago por proveedor
    ''' </summary>
    Private _listSchedulePaymentBankAccount As Domain.Entities.TrackableCollection(Of SchedulePaymentBankAccount)
    Public Property ListSchedulePaymentBankAccount As Domain.Entities.TrackableCollection(Of SchedulePaymentBankAccount)
        Get
            Return _listSchedulePaymentBankAccount
        End Get
        Set(value As Domain.Entities.TrackableCollection(Of SchedulePaymentBankAccount))
            _listSchedulePaymentBankAccount = value
        End Set
    End Property

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        optionalParameters = Nothing
        _idCurrentSequense = Nothing
        _prefixSelected = Nothing
        OriginalDatasource = Nothing
        _presenter = Nothing
        _schedulePayment = Nothing
        varImp = Nothing
    End Sub

    Private Sub FrmDispersionFunds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PDispersionFunds(Me)
        _presenter.GetSettingsPaymentsByOperatingUnitId(Me._idOperativeUnit)
        _presenter.GetSequence()
        _presenter.InitializeEntityBankAccount()
        _presenter.InitializeCostCenter()

        LoadStatus()
        Deshacer()
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
            If Not String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
                Await LoadControls()
            End If
        ElseIf e.KeyCode = Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Handles the ButtonClick event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph Then
            OpenSearch()
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
                transparent.ShowDialog(Me)
                _presenter.InitializeCostCenter()
            End Using
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
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog(Me)
                _presenter.InitializeEntityBankAccount()
            End Using
        End If
    End Sub
#End Region

#Region "CustomDisplayValue"
    ''' <summary>
    ''' variable que obtiene el valor de la celda (valor pagado) para luego calcular el porcentaje
    ''' </summary>
    Private _valueCell As Decimal

    ''' <summary>
    ''' evento que se utiliza para reevaluar el dato de una celda
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CustomEditValueEventArgs"/> instance containing the event data.</param>
    Private Sub INDpgcSchedulePayment_CustomEditValue(sender As Object, e As CustomEditValueEventArgs) Handles INDpgcSchedulePayment.CustomEditValue
        If Comparer.ReferenceEquals(e.DataField, INDcolPayValuePercent) Then
            If e.Value <> 0 Then
                _valueCell = e.Value
                If Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex - 1, e.RowIndex)) <> 0 Then
                    e.Value = Math.Round(Convert.ToDecimal(((Convert.ToDecimal(e.Value) + (Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex + 1, e.RowIndex)))) / Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex - 1, e.RowIndex))) * 100.0F), 2)
                End If
            End If
        Else
            e.Value = 0.0F
        End If
    End Sub

    ''' <summary>
    ''' Handles the CustomCellDisplayText event of the INDpgcSchedulePayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraPivotGrid.PivotCellDisplayTextEventArgs"/> instance containing the event data.</param>
    Private Sub INDpgcSchedulePayment_CustomCellDisplayText(sender As Object, e As PivotCellDisplayTextEventArgs) Handles INDpgcSchedulePayment.CustomCellDisplayText
        If Comparer.ReferenceEquals(e.DataField, INDcolPayValuePercent) Then
            If e.Value <> 0 AndAlso Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex - 1, e.RowIndex)) <> 0 Then
                e.DisplayText = String.Format(RepositoryItemProgressBarPercent.DisplayFormat.FormatString, Math.Round((Convert.ToDecimal((Convert.ToDecimal(_valueCell) + Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex + 1, e.RowIndex))) / Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex - 1, e.RowIndex))) * 100.0F), 2))
            End If
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDglePaymentType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDglePaymentType_EditValueChanged(sender As Object, e As EventArgs) Handles INDglePaymentType.EditValueChanged
        If PaymentMethod = 1 Then 'Cheque
            INDliCheck.HideControl(False)
            INDliDebitNote.HideControl(True)

            Await LoadCheck()


        Else 'Nota Debito
            INDliCheck.HideControl(True)
            INDliDebitNote.HideControl(False)

            DeleteCheckBlock()
            CheckNumber = Nothing
            IdCheckBook = Nothing

        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntityBankAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityBankAccount.EditValueChanged
        If EntityBankAccountId <> 0 Then
            INDglePaymentType.Enabled = True
            Dim entity = Nothing
            If _schedulePayment.Id <> 0 Then
                Using Model As New MEntityAccount(Me.Tag)
                    entity = Model.GetEntityBankAccountByIdSimple(EntityBankAccountId)
                    CostCenterId = entity.IdCostCenter
                End Using
            Else
                entity = DirectCast(DirectCast(INDgvEntityBankAccount.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.TreasuryRepository.EntityBankAccountXpo)
                CostCenterId = entity.IdCostCenter
            End If
            CreatePaymentMethod(entity.Type)
            If entity IsNot Nothing AndAlso entity.Id > 0 Then
                If Me._sequence.Scope.Equals("O") Then
                    Me._idCurrentSequense = Me.GetIdSequenceByPrefix(entity.Prefix)
                    Me._prefixSelected = entity.Prefix
                End If
                If entity.IdCostCenter IsNot Nothing AndAlso entity.IdCostCenter > 0 Then
                    INDliCostCenter.HideControl(False)
                    INDsleCostCenter.EditValue = entity.IdCostCenter
                Else
                    INDliCostCenter.HideControl(True)
                    INDsleCostCenter.EditValue = Nothing
                End If
            End If
        Else
            If Not String.IsNullOrEmpty(CheckNumber) Then
                DeleteCheckBlock()
            End If
            If Me._sequence.Scope.Equals("O") Then
                Me._idCurrentSequense = Me.GetIdSequenceByPrefix("-")
                Me._prefixSelected = String.Empty
            End If
            CheckNumber = Nothing
            NoteNumber = Nothing
            INDglePaymentType.EditValue = Nothing
            INDglePaymentType.Enabled = False
            INDliCheck.HideControl(True)
            INDliDebitNote.HideControl(True)
        End If
    End Sub
#End Region

#Region "DoubleClick"
    ''' <summary>
    ''' Evento doble click que abre el detalle de las facturas
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraPivotGrid.PivotCellEventArgs"/> instance containing the event data.</param>
    Private Sub INDpgcSchedulePayment_CellDoubleClick(sender As Object, e As DevExpress.XtraPivotGrid.PivotCellEventArgs) Handles INDpgcSchedulePayment.CellDoubleClick
        Try
            ShowDrilldown(e.CreateDrillDownDataSource())
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmDispersionFunds control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmDispersionFunds_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteCheckBlock()
    End Sub
#End Region

#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateUnGenetated"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatePartialPaid"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(0, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "5", .StatusName = ResourceManager.GetString("StateFullPaid"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(0, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Elimino un cheque bloqueado y lo envio a cheques pendientes
    ''' </summary>
    Private Async Sub DeleteCheckBlock()
        'If IdCheckBook <> 0 AndAlso CheckNumber <> 0 Then
        '    Using ModelOutstandingChecks As New MOutstandingChecks(Me.Tag)
        '        Dim outstandingChecks As New OutstandingChecks()
        '        With outstandingChecks
        '            .IdCheckBook = IdCheckBook
        '            .CheckNumber = CheckNumber
        '        End With
        '        Dim resultOutstandingChecks = Await ModelOutstandingChecks.SaveOutstandingChecks(outstandingChecks)
        '        IdCheckBook = outstandingChecks.IdCheckBook
        '        CheckNumber = outstandingChecks.CheckNumber
        '        If resultOutstandingChecks.StateResult = False Then
        '            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OutstandingChecksPutError", NAME_MODULE)
        '        End If
        '    End Using
        '    Using Model As New MCheck(Me.Tag)
        '        Dim resultOp = Await Model.GetCheckBlockByIdCheckBookNumber(IdCheckBook, CheckNumber)
        '        Dim checkBlock As CheckBlock = resultOp.ObjectEmbbeded
        '        If checkBlock IsNot Nothing AndAlso checkBlock.Id > 0 Then
        '            checkBlock.MarkAsDeleted()
        '            Dim regBlock = Await Model.DeleteCheckBlock(checkBlock)
        '            If regBlock.StateResult = False Then
        '                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteErrorCheckBlock", NAME_MODULE)
        '            End If
        '        End If
        '    End Using
        'End If
    End Sub

    ''' <summary>
    ''' Loads the check.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadCheck() As Task
        Dim setting As SettingsTreasury = Nothing
        Using ModelSetting As New MSettingsTreasury(Me.Tag)
            Dim resultSetting = ModelSetting.GetSettingsTreasuryByIdUnitOperativeSimple(Me._idOperativeUnit)
            If resultSetting.StateResult AndAlso resultSetting.ObjectEmbbeded IsNot Nothing AndAlso resultSetting.ObjectEmbbeded.Id > 0 Then
                setting = resultSetting.ObjectEmbbeded
                NoteNumber = Nothing
                INDspnCheck.Enabled = Not setting.CheckBookControl
                'obtener el primer cheque en la lista de pendientes
                Using ModelCheckBook As New MCheck(Me.Tag)
                    Dim _checkBook = ModelCheckBook.GetCheckByIdEntityBankAccountAndStatusSimple(EntityBankAccountId, 1) '1 - Estado activo
                    If _checkBook.StateResult = True AndAlso _checkBook.ObjectEmbbeded IsNot Nothing AndAlso _checkBook.ObjectEmbbeded.Id > 0 Then
                        Using ModelOutstandingCheck As New MOutstandingChecks(Me.Tag)
                            Dim outstandingCheck As OutstandingChecks = Await ModelOutstandingCheck.GetFirstOutstandingChecks(_checkBook.ObjectEmbbeded.Id)
                            If outstandingCheck IsNot Nothing AndAlso outstandingCheck.Id > 0 Then
                                IdCheckBook = outstandingCheck.IdCheckBook
                                CheckNumber = outstandingCheck.CheckNumber
                                Dim resultDelCheckPend = Await ModelOutstandingCheck.DeleteOutstandingChecks(outstandingCheck)
                                If resultDelCheckPend.StateResult = False Then
                                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("OutstandingChecksDeleteError", NAME_MODULE)
                                    Exit Function
                                End If
                                Using Model As New MCheck(Me.Tag)
                                    Dim checkBlock As New CheckBlock() With {.IdCheckbook = IdCheckBook, .CheckNumber = CheckNumber, .CodUser = indigo.UserIndigoId}
                                    Dim regBlock = Await Model.SaveCheckBlock(checkBlock)
                                    If regBlock.StateResult = False Then
                                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SaveErrorCheck", NAME_MODULE)
                                        Exit Function
                                    End If
                                End Using
                            Else
                                Dim checkBook As Checkbooks = _checkBook.ObjectEmbbeded
                                IdCheckBook = checkBook.Id
                                CheckNumber = checkBook.CurrentNumber
                                'Dim checkGood As Boolean = False
                                'While Not checkGood
                                '    'Dim checkBlock As CheckBlock = checkBook.CheckBlock.Where(Function(x) x.CheckNumber.Equals(Convert.ToInt64(CheckNumber))).Cast(Of CheckBlock).FirstOrDefault()
                                '    'If checkBlock Is Nothing OrElse checkBlock.Id = 0 Then
                                '    '    checkBlock = New CheckBlock() With {.IdCheckbook = checkBook.Id, .CheckNumber = CheckNumber, .CodUser = indigo.UserIndigoId}
                                '    '    Dim regBlock = Await ModelCheckBook.SaveCheckBlock(checkBlock)
                                '    '    If regBlock.StateResult = False Then
                                '    '        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SaveErrorCheck", NAME_MODULE)
                                '    '    Else
                                '    '        If Not setting.CheckBookControl Then
                                '    '            INDspnCheck.Properties.MinValue = checkBook.CurrentNumber
                                '    '            INDspnCheck.Properties.MaxValue = checkBook.EndNumber
                                '    '        End If
                                '    '        INDspnCheck.Enabled = Not setting.CheckBookControl
                                '    '    End If
                                '    '    checkGood = True
                                '    'Else
                                '    '    CheckNumber += 1
                                '    'End If
                                'End While
                            End If
                        End Using
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CheckActiveNoExist", NAME_MODULE)
                    End If
                    INDspnCheck.Enabled = Not setting.CheckBookControl
                End Using
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NotParameterizedOperationalUnit", NAME_MODULE)
                Exit Function
            End If
        End Using
    End Function

    ' ''' <summary>
    ' ''' Creates the type of the list payment.
    ' ''' </summary>
    'Private Sub CreateListPaymentType()
    '    TuplePaymentType = New List(Of Tuple(Of Integer, String))()
    '    TuplePaymentType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("PaymentMethodCheck", NAME_MODULE)))
    '    TuplePaymentType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("PaymentMethodDebitNote", NAME_MODULE)))
    '    PaymentMethodDatasource = TuplePaymentType
    'End Sub

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing AndAlso Me._sequence.TreasurySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequence.TreasurySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Crea el listado de las opciones de generado con
    ''' </summary>
    Private Sub CreatePaymentMethod(ByVal Type As Integer)
        TuplePaymentType = New List(Of Tuple(Of Integer, String))
        If Type = Presentation.Treasury.FrmVoucherTransaction.eEntityBankAccountType.Current Then
            TuplePaymentType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("PaymentMethodCheck", NAME_MODULE)))
        End If
        TuplePaymentType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("PaymentMethodDebitNote", NAME_MODULE)))
        PaymentMethodDatasource = TuplePaymentType
    End Sub

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
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "ScheduledDate", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Valor", .FieldName = "AmountPaidByCurrency", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusNameDispersionFund", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListSchedulePaymentConfirm
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
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    Private Sub NewVoucherTransaction()
        If Not Me._sequence.IsManual Then
            If Sequense IsNot Nothing AndAlso Sequense.Id > 0 Then
                If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                    Me._idCurrentSequense = 0
                ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                    Dim res = (From ou As TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail Where ou.OperatingUnit.Id = Me._idOperativeUnit Select ou).ToList()
                    If res IsNot Nothing AndAlso res.Count > 0 Then
                        Me._idCurrentSequense = res(0).Id
                    Else
                        Me._idCurrentSequense = Me._sequence.TreasurySequenceDetail(0).Id
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            End If
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

            Using Model As New MSchedulePayment(Me.Tag)
                AsyncLoader(True)
                Dim resultOperation = Await Model.GetSchedulePayment(Me.Code, True)
                INDlcRoot.BeginUpdate()
                _schedulePayment = resultOperation.ObjectEmbbeded
                If Not _schedulePayment Is Nothing Then
                    If _schedulePayment.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        'Validamos el estado de la planilla
                        If _schedulePayment.Status <> 2 And _schedulePayment.Status <> 4 And _schedulePayment.Status <> 5 Then
                            Me.Mensaje(EeventViewerImages.Advertencia) = "La planilla aún no ha sido Confirmada en Programación de Pagos."
                            AsyncLoader(False)
                            Deshacer()
                            Exit Function
                        End If
                        Me.NewVoucherTransaction()
                        Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                            With _schedulePayment
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Code = .Code
                                'ScheduledDate = .ScheduledDate
                                ScheduledDate = GetDateServer()
                                EntityBankAccountId = .EntityBankAccountId
                                CostCenterId = .CostCenterId
                                PaymentMethod = .PaymentMethod
                                NoteNumber = .NumberNote
                                TaxByMil = .TaxByMil
                                Status = .Status.ToString()
                                ListSchedulePaymentBankAccount = .SchedulePaymentBankAccount
                            End With
                            Await LoadSchedulePayment()
                            Me.BarraBotones.SetDocuments(_schedulePayment.Id, Me.Tag.ToString(), Nothing, GetType(SchedulePayment).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                            If _schedulePayment.Status = 4 Then
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                                'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                            ElseIf _schedulePayment.Status = 5 Then
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
                                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True
                                ReadOnlyControls(True)
                                'BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = False
                            End If
                            If Status = 2 Then
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = False
                            End If
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDbteCode.Enabled = False
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _schedulePayment.Id, 0, _schedulePayment.Id)
                        End Using
                    Else
                        AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Code = String.Empty
                        Deshacer()
                        INDbteCode.Focus()
                    End If
                Else
                    AsyncLoader(False)
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                    Deshacer()
                    INDbteCode.Focus()
                End If
            End Using
        End If
        INDlcRoot.EndUpdate()
    End Function

    ''' <summary>
    ''' Loads the schedule payment.
    ''' </summary>
    Private Async Function LoadSchedulePayment() As Task
        Await _presenter.InitializeSchedulePaymentDatasourceWithCode(Code)
    End Function

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlcRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = "-1"
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        Code = String.Empty
        INDdeDate.EditValue = Nothing
        INDsleEntityBankAccount.EditValue = Nothing
        INDglePaymentType.EditValue = Nothing
        INDrgTaxByMil.EditValue = False
        ctrSchedulePayment.InvoiceShareDatasource = Nothing
        ctrSchedulePayment.PrintAdvance()

        ReadOnlyControls(False)
        INDpgcSchedulePayment.DataSource = Nothing
        INDpgcSchedulePayment.RefreshData()
        INDliCheck.HideControl(True)
        INDliDebitNote.HideControl(True)
        INDliCostCenter.HideControl(True)

        Me._schedulePayment = Nothing
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GenerateFile) = True
        INDlcRoot.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IDispersionFunds.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDdeDate.Enabled = value
            INDsleEntityBankAccount.Enabled = value
            INDglePaymentType.Enabled = value
            INDrgTaxByMil.Enabled = False
            INDglePaymentType.Enabled = False

            INDtxtNoteNumber.Enabled = value
            INDpgcSchedulePayment.Enabled = value

            INDlcRoot.EndUpdate()

            Me.BarraBotones.StatusRecordVisible = value
            If value Then
                INDdeDate.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Abre el formulario que muestra el detalle de las facturas
    ''' </summary>
    ''' <param name="ds">The ds.</param>
    Private Sub ShowDrilldown(ByVal ds As PivotDrillDownDataSource)
        If ds.RowCount > 0 Then
            Using Formulario As New InvoiceDetail
                Formulario.PaymentsSettingPaymentsXpo = Me.PaymentsSettingPaymentsXpo
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.ShowMenuContext = False
                Formulario.OnlyView = True
                Formulario.FlagDispersion = True
                Formulario.AllowModify = Status = 2 'True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.DetailInvoiceDatasource = ds
                If ListSchedulePaymentBankAccount Is Nothing Then
                    ListSchedulePaymentBankAccount = New Domain.Entities.TrackableCollection(Of SchedulePaymentBankAccount)
                End If
                Formulario.ListSchedulePaymentBankAccount = ListSchedulePaymentBankAccount
                AddHandler Formulario.UpdateDatasourceInvoice, AddressOf Handler_UpdateDatasourceInvoice
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar

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
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        AssignValues()
        'Exit Sub
        With _schedulePayment
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .ScheduledDate = ScheduledDate
            .OperativeUnitId = _idOperativeUnit
            .Status = Status
        End With
        Dim schedulePaymentDetail As SchedulePaymentDetail
        For Each sp As SP_SchedulePayment_Result In OriginalDatasource.Where(Function(x) x.SchedulePaymentDetailId <> 0 OrElse x.PayValue <> 0).Cast(Of SP_SchedulePayment_Result)().ToList()
            If sp.SchedulePaymentDetailId <> 0 Then
                schedulePaymentDetail = _schedulePayment.SchedulePaymentDetail.Where(Function(x) x.Id = sp.SchedulePaymentDetailId).Cast(Of SchedulePaymentDetail).FirstOrDefault()
                If sp.PayValue <> 0 Then
                    schedulePaymentDetail.MarkAsModified()
                Else
                    schedulePaymentDetail.MarkAsDeleted()
                End If
                _schedulePayment.MarkAsModified()
            Else
                schedulePaymentDetail = New SchedulePaymentDetail()
            End If
            With schedulePaymentDetail
                '.Id = sp.SchedulePaymentDetailId
                .SupplierId = sp.SupplierId
                .ThirdPartyId = sp.ThirdId
                .DistributionLineId = sp.DistributionLineId
                .ExpenseConceptId = sp.ExpenseConceptIdDistributionLine
                .MainAccountId = sp.MainAccountIdDistributionLine
                .Nature = sp.NatureExpenseConcept
                .AccountPayableId = sp.AccountPayableId
                .AccountPayableShareId = sp.AccountPayableShareId
                .AmountPaid = sp.PayValue
                .AmountPercent = sp.PaymentPercent
                .PaymentConceptId = sp.PaymentConceptId
                .Invoice = sp.Invoice
                .Paid = False
                .Description = "Pago de Facturas por Dispersión de fondos"
                .AccountPayableCode = sp.AccountPayableCode

                If BudgetInterface Then
                    If sp.SchedulePaymentDetailBudget IsNot Nothing AndAlso sp.SchedulePaymentDetailBudget.Any() Then
                        For Each detail In sp.SchedulePaymentDetailBudget
                            Dim schedulePaymentDetailBudget = .SchedulePaymentDetailBudget.FirstOrDefault(Function(d) d.ObligationDetailId = detail.ObligationDetailId)
                            If Not (detail.Value > 0) Then
                                If schedulePaymentDetailBudget IsNot Nothing AndAlso schedulePaymentDetailBudget.Id <> 0 Then
                                    schedulePaymentDetailBudget.MarkAsDeleted()
                                End If
                                Continue For
                            End If

                            If schedulePaymentDetailBudget Is Nothing Then
                                schedulePaymentDetailBudget = New SchedulePaymentDetailBudget With {
                                    .Id = detail.Id,
                                    .ObligationDetailId = detail.ObligationDetailId
                                }
                                .SchedulePaymentDetailBudget.Add(schedulePaymentDetailBudget)
                            End If

                            schedulePaymentDetailBudget.Value = detail.Value
                        Next
                    End If
                End If
            End With
            _schedulePayment.SchedulePaymentDetail.Add(schedulePaymentDetail)
        Next
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.ICrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Guardars the specified with confirm.
    ''' </summary>
    ''' <param name="withConfirm">if set to <c>true</c> [with confirm].</param>
    Public Async Sub Guardar(withConfirm As Boolean)
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        If _idOperativeUnit = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una unidad operativa"
            Exit Sub
        End If
        AssigningValues()
        Try
            If withConfirm Then
                Try
                    Using model As New MDispersionFunds(Me.Tag)
                        AsyncLoader(True)
                        Dim Result = Await model.MakeSchedulePayment(Me._schedulePayment, Me._idCurrentSequense, Me._sequence)
                        If Result.StateResult Then
                            Using Formulario As New FrmListErrors(Result.ObjectEmbbeded)
                                Formulario.Title = ResourceManager.GetString("ResultOperationMessage")
                                Formulario.MinimizeBox = False
                                Formulario.MaximizeBox = False
                                Formulario.Size = New Size(780, 700)
                                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(Formulario, False)
                                transparent.ShowDialog(Me)
                            End Using
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _schedulePayment.Id, 0, _schedulePayment.Id)
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
            Else
                Using Model As New MSchedulePayment(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim Result = Await Model.SaveSchedulePayment(Me._schedulePayment, False, Me._idCurrentSequense)
                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        If _schedulePayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                            End If
                        End If
                        Me._schedulePayment = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Select Case varImp
                            Case 1
                                Me.BarraBotones.PrintReport(PrintReportAction.Create, _schedulePayment.Id, 0, _schedulePayment.Id)
                            Case 2
                                Me.BarraBotones.PrintReport(PrintReportAction.Update, _schedulePayment.Id, 0, _schedulePayment.Id)
                        End Select
                        AsyncLoader(False)
                        Deshacer()

                        'If _schedulePayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        '    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        '        Me.DicSequense(Me._sequence.TreasurySequenceDetail(0).Id).RemoveAt(0)
                        '    End If
                        '    If Me._sequence.Sequential Then
                        '        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        '    Else
                        '        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        '    End If
                        'ElseIf _schedulePayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        '    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        'End If
                        'Me._schedulePayment = Result.ObjectEmbbeded
                        'Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        'Select Case varImp
                        '    Case 1
                        '        Me.BarraBotones.PrintReport(PrintReportAction.Create, _schedulePayment.Id, 0, _schedulePayment.Id)
                        '    Case 2
                        '        Me.BarraBotones.PrintReport(PrintReportAction.Update, _schedulePayment.Id, 0, _schedulePayment.Id)
                        'End Select
                        'AsyncLoader(False)
                        'Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        _schedulePayment.SchedulePaymentDetail.Clear()
                        If Result.Message IsNot Nothing Then
                            generateListError(Result.Message)
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        'No implementa
    End Sub

    ''' <summary>
    ''' Confirmars this instance.
    ''' </summary>
    Private Async Sub Confirmar()
        'If Not ValidateControls() Then
        '    Exit Sub
        'End If
        'If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessageSchedulePayment"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '    AssignValues()
        '    Try
        '        Using Model As New MDispersionFunds(Me.Tag)
        '            AsyncLoader(True)
        '            Dim Result = Await Model.MakeSchedulePayment(Me._schedulePayment, Me._idCurrentSequense, Me._sequence)
        '            AsyncLoader(False)
        '            If Result.StateResult = True Then
        '                Using Formulario As New FrmListErrors(Result.ObjectEmbbeded)
        '                    Formulario.Title = ResourceManager.GetString("ResultOperationMessage")
        '                    Formulario.MinimizeBox = False
        '                    Formulario.MaximizeBox = False
        '                    Formulario.Size = New Size(780, 700)
        '                    Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        '                    Dim transparent As New FrmTransparent(Formulario, False)
        '                    transparent.ShowDialog(Me)
        '                End Using
        '                _searchMode = False
        '                Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _schedulePayment.Id, 0, _schedulePayment.Id)
        '                Me.Deshacer()

        '            Else
        '                'VoucherTransaction.VoucherTransactionDetails.Clear()
        '                If Result.Message IsNot Nothing Then
        '                    generateListError(Result.Message)
        '                End If
        '            End If
        '        End Using
        '    Catch ex As Exception
        '        Throw ex
        '        AsyncLoader(False)
        '    End Try
        'End If
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
    ''' Asigna los valores a la entidad
    ''' </summary>
    Private Sub AssignValues()
        With _schedulePayment
            .EntityBankAccountId = EntityBankAccountId
            .CostCenterId = CostCenterId
            .PaymentMethod = PaymentMethod
            .CheckId = IdCheckBook
            .NumberNote = NoteNumber
            .TaxByMil = TaxByMil
        End With
    End Sub

#End Region

#Region "BarButtonEvents"
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        'Confirmar()
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

    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        'No Implementa
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Me.NewVoucherTransaction()
        End If
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        varImp = 3
        Guardar(True)
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _schedulePayment.Id, 0, _schedulePayment.Id)
    End Sub

#End Region

    ''' <summary>
    ''' controlador del evento que actualiza el datasource
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="UpdateDatasourceEventArgs"/> instance containing the event data.</param>
    Private Sub Handler_UpdateDatasourceInvoice(sender As Object, e As UpdateDatasourceEventArgs)
        For Each sp As SP_SchedulePayment_Result In e.NewInvoiceShareDatasource
            Dim alter As SP_SchedulePayment_Result = OriginalDatasource.Where(Function(x) x.ThirdId = (sp.ThirdId) _
                                                                                        And x.SupplierId = sp.SupplierId _
                                                                                        And x.SupplierName.Equals(sp.SupplierName) _
                                                                                        And x.SupplierCode.Equals(sp.SupplierCode) _
                                                                                        And x.SupplierNit.Equals(sp.SupplierNit) _
                                                                                        And x.DescriptionLine.Equals(sp.DescriptionLine) _
                                                                                        And x.Invoice.Equals(sp.Invoice) _
                                                                                        And x.ExpirationDate.Equals(sp.ExpirationDate) _
                                                                                        And x.InvoiceBalance = (sp.InvoiceBalance) _
                                                                                        And x.Share = (sp.Share) _
                                                                                        And x.SupplierTypeId = sp.SupplierTypeId _
                                                                                        And x.ShareExpirationDate.Equals(sp.ShareExpirationDate) _
                                                                                        And x.BalanceShare = (sp.BalanceShare)).FirstOrDefault()
            Dim alter2 As SP_SchedulePayment_Result = SchedulePaymentDatasource.Where(Function(x) x.ThirdId = (sp.ThirdId) _
                                                                                        And x.SupplierId = sp.SupplierId _
                                                                                        And x.SupplierName.Equals(sp.SupplierName) _
                                                                                        And x.SupplierCode.Equals(sp.SupplierCode) _
                                                                                        And x.SupplierNit.Equals(sp.SupplierNit) _
                                                                                        And x.DescriptionLine.Equals(sp.DescriptionLine) _
                                                                                        And x.Invoice.Equals(sp.Invoice) _
                                                                                        And x.ExpirationDate.Equals(sp.ExpirationDate) _
                                                                                        And x.InvoiceBalance = (sp.InvoiceBalance) _
                                                                                        And x.Share = (sp.Share) _
                                                                                        And x.SupplierTypeId = sp.SupplierTypeId _
                                                                                        And x.ShareExpirationDate.Equals(sp.ShareExpirationDate) _
                                                                                        And x.BalanceShare = (sp.BalanceShare)).FirstOrDefault()
            With alter
                .PayValue = sp.PayValue
                .PaymentPercent = sp.PaymentPercent
                .PaymentConceptId = sp.PaymentConceptId
                If BudgetInterface Then
                    If sp.SchedulePaymentDetailBudget IsNot Nothing AndAlso sp.SchedulePaymentDetailBudget.Any() Then
                        For Each detail In sp.SchedulePaymentDetailBudget
                            Dim schedulePaymentDetailBudget = alter.SchedulePaymentDetailBudget.FirstOrDefault(Function(d) d.ObligationDetailId = detail.ObligationDetailId)
                            If schedulePaymentDetailBudget Is Nothing Then
                                If detail.Id = 0 AndAlso Not (detail.Value > 0) Then
                                    Continue For
                                End If

                                schedulePaymentDetailBudget = New SchedulePaymentDetailBudget With {
                                    .Id = detail.Id,
                                    .ObligationDetailId = detail.ObligationDetailId
                                }
                                .SchedulePaymentDetailBudget.Add(schedulePaymentDetailBudget)
                            End If
                            schedulePaymentDetailBudget.Value = detail.Value
                        Next
                    End If
                End If
            End With
            With alter2
                .PayValue = sp.PayValue
                .PaymentPercent = sp.PaymentPercent
                .PaymentConceptId = sp.PaymentConceptId
                If BudgetInterface Then
                    If sp.SchedulePaymentDetailBudget IsNot Nothing AndAlso sp.SchedulePaymentDetailBudget.Any() Then
                        For Each detail In sp.SchedulePaymentDetailBudget
                            Dim schedulePaymentDetailBudget = alter2.SchedulePaymentDetailBudget.FirstOrDefault(Function(d) d.ObligationDetailId = detail.ObligationDetailId)
                            If schedulePaymentDetailBudget Is Nothing Then
                                If detail.Id = 0 AndAlso Not (detail.Value > 0) Then
                                    Continue For
                                End If

                                schedulePaymentDetailBudget = New SchedulePaymentDetailBudget With {
                                    .Id = detail.Id,
                                    .ObligationDetailId = detail.ObligationDetailId
                                }
                                .SchedulePaymentDetailBudget.Add(schedulePaymentDetailBudget)
                            End If
                            schedulePaymentDetailBudget.Value = detail.Value
                        Next
                    End If
                End If
            End With
        Next

        If _schedulePayment.ChangeTracker.State <> ObjectState.Added Then
            _schedulePayment.MarkAsModified()
        End If

        ctrSchedulePayment.InvoiceShareDatasource = SchedulePaymentDatasource.Where(Function(x) x.PayValue <> 0).Cast(Of SP_SchedulePayment_Result)().ToList()
        ctrSchedulePayment.PrintAdvance()
        INDpgcSchedulePayment.RefreshData()
    End Sub

    Private Async Sub BarraBotones_Click_GenerateFile() Handles BarraBotones.Click_GenerateFile
        'Se valida que el usuario haya escogido una cuenta contable
        If EntityBankAccountId Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir una Cuenta Bancaria."
            INDsleEntityBankAccount.Focus()
            Exit Sub
        Else
            'se agrega Or debido aque cuando se cambiaba el banco este no se actualizaba por ya no era nothing, entonces se permite 
            'actualizar si la programacion no esta en estado anulada o pagada total.
            If _schedulePayment.EntityBankAccountId Is Nothing OrElse
                (Not {3, 5}.Contains(_schedulePayment.Status) AndAlso _schedulePayment.EntityBankAccountId <> EntityBankAccountId) Then
                _schedulePayment.EntityBankAccountId = EntityBankAccountId
            End If
        End If
        Try
            AsyncLoader(True)
            Using model As New MEntityAccount(MyTag)
                Dim bankAccount = model.GetEntityBankAccountByIdSimple(_schedulePayment.EntityBankAccountId)
                Using modelDispersionFunds As New MDispersionFunds(MyTag)
                    Using modelBank As New MBank(MyTag)
                        Dim bank = modelBank.GetBankById(bankAccount.IdBank)

                        If bank?.BankFileCode Is Nothing OrElse bank?.BankFileCode = -1 Then
                            Mensaje(EeventViewerImages.Advertencia) = "Debe elegir la Estructura Archivo Plano en el formulario de Bancos"
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        If bank.BankFileCode = "002" Then
                            Using formulario As New FrmOptionalParametersBankFile
                                'requiere pasar al formulario el numero del banco 
                                formulario.BankFileCode = bank.BankFileCode
                                formulario.StartPosition = FormStartPosition.CenterParent
                                formulario.Size = New Size With {.Width = 428, .Height = 167}
                                Dim transparent As New FrmTransparent(formulario, False)
                                transparent.ShowDialog(Me)
                                If transparent.DialogResult = System.Windows.Forms.DialogResult.OK Then
                                    optionalParameters = formulario.OptionalParameters
                                Else
                                    AsyncLoader(False)
                                    Exit Sub
                                End If
                            End Using
                        End If
                    End Using

                    If _schedulePayment IsNot Nothing AndAlso _schedulePayment.SchedulePaymentDetail.Any() AndAlso OriginalDatasource?.Any() Then
                        For Each schedulePaymentDetail In _schedulePayment.SchedulePaymentDetail
                            Dim sp = OriginalDatasource?.Where(Function(x) x.SchedulePaymentDetailId = schedulePaymentDetail.Id)?.FirstOrDefault()
                            If sp IsNot Nothing Then
                                schedulePaymentDetail.Invoice = sp.Invoice
                            End If
                        Next
                    End If

                    Dim result = Await modelDispersionFunds.GenerateBankFile(_schedulePayment, bankAccount.IdBank, optionalParameters)

                    If result.StateResult = True Then
                        DialogGenerateFile(result.ObjectEmbbeded, result.Message)
                    Else
                        If result?.MessageResult Is Nothing OrElse Not result?.MessageResult?.Any() Then
                            result.MessageResult = New List(Of String) From {result?.Message}
                        End If
                        Using formulario As New FrmListErrors(result?.MessageResult)
                            formulario.Size = New Size With {.Width = 620, .Height = 479}
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If

                End Using
            End Using
            AsyncLoader(False)
            INDbteCode.Enabled = False
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo que despliega show dialog para guardar un archivo plano
    ''' </summary>
    ''' <param name="content"></param>
    ''' <remarks></remarks>
    Public Sub DialogGenerateFile(content As String, nameBank As String)
        Dim save As SaveFileDialog = New SaveFileDialog()
        save.Filter = "Texto|*.txt"
        save.Title = "Archivo Banco"
        save.FileName = nameBank + ".txt"
        If save.ShowDialog(Me) = System.Windows.Forms.DialogResult.OK Then
            Dim file = save.OpenFile()
            Dim streamWrite As New StreamWriter(file)
            streamWrite.Write(content)
            streamWrite.Flush()
            streamWrite.Close()
            If MessageIndigo.Show(obtenerRecurso(GuardadoDeseaAbrir, Eform.ArchivoBanco), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Process.Start(save.FileName)
            End If
        End If
    End Sub

End Class