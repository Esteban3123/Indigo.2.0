'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 21-08-2014
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
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Exceptions
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraGrid
Imports Presentation.Base.Eform
Imports Domain.Base.Entities
Imports System.Text
Imports System.Globalization
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class FrmSchedulePayment
    Implements ISchedulePayment, ICustomizableForm

#Region "Constructor"
    ''' <summary>
    ''' Initializes a new instance of the <see cref="FrmSchedulePayment"/> class.
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        _ctrValues = New CtrValues()
        _ctrValues.Dock = DockStyle.Top
        AdditionalControlPanel.Controls.Add(_ctrValues)
    End Sub

#End Region

#Region "Properties and Variables"

    ''' <summary>
    ''' variable que obtiene el valor de la celda (valor pagado) para luego calcular el porcentaje
    ''' </summary>
    Private _valueCell As Decimal
    ''' <summary>
    ''' variable utilizada para guardar y confirmar
    ''' </summary>
    Private _withConfirm As Boolean

    ''' <summary>
    ''' control de la parte superior que muestra la informacion totalizada de la progrmacion de pagos
    ''' </summary>
    Dim _ctrValues As CtrValues

    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"
    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence
    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequense As Int64
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    Private countAdd As Integer = 1

    ''' <summary>
    ''' Representa a la entidad de parámetros de cxp
    ''' </summary>
    Public Property PaymentsSettingPaymentsXpo As PaymentsSettingPaymentsXpo Implements ISchedulePayment.PaymentsSettingPaymentsXpo
    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    Public ReadOnly Property MyTag As Object Implements ISchedulePayment.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ISchedulePayment.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Public Property Sequense As TreasurySequence Implements ISchedulePayment.Sequense
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

    Public ReadOnly Property BudgetInterface As Boolean
        Get
            Return If(PaymentsSettingPaymentsXpo Is Nothing, False, PaymentsSettingPaymentsXpo.BudgetInterface)
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Public Property Code As String Implements ISchedulePayment.Code
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
    ''' Obtiene o establece la fecha de la programación de pagos
    ''' </summary>
    ''' <value>
    ''' The scheduled date.
    ''' </value>
    Public Property ScheduledDate As Date Implements ISchedulePayment.ScheduledDate
        Get
            Return CType(INDdeDate.EditValue, Date)
        End Get
        Set(value As Date)
            INDdeDate.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el estado de la programación de pagos
    ''' </summary>
    ''' <value>
    ''' The status.
    ''' </value>
    Public Property Status As String Implements ISchedulePayment.Status
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As String)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o establece el datasource de la programacion de pagos
    ''' </summary>
    ''' <value>
    ''' The schedule payment datasource.
    ''' </value>
    Public Property SchedulePaymentDatasource As List(Of Domain.Entities.SP_SchedulePayment_Result) Implements ISchedulePayment.SchedulePaymentDatasource
        Get
            Return CType(INDpgcSchedulePayment.DataSource, List(Of SP_SchedulePayment_Result))
        End Get
        Set(value As List(Of Domain.Entities.SP_SchedulePayment_Result))
            INDpgcSchedulePayment.DataSource = value
            If value IsNot Nothing Then
                createTotalControl(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' fecha de pago.
    ''' </summary>
    Public Property PaymentDate As DateTime
        Get
            Return INDdtePaymentDate.EditValue
        End Get
        Set(value As DateTime)
            INDdtePaymentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _schedulePayment As SchedulePayment
    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PSchedulePayment

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordTreasury

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' Variable que actualizala moneda del control que se está evaluando
    ''' </summary>
    ''' <remarks></remarks>
    Dim CurrencyForControl As Integer

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
        _valueCell = Nothing
        _withConfirm = Nothing
        _ctrValues = Nothing
        _sequence = Nothing
        _idCurrentSequense = Nothing
        _idOperativeUnit = Nothing
        _schedulePayment = Nothing
        _presenter = Nothing
        record = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmSchedulePayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmSchedulePayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        _presenter = New PSchedulePayment(Me)
        _presenter.GetSettingsPaymentsByOperatingUnitId(Me._idOperativeUnit)
        _presenter.GetSequence()
        _presenter.LoadDefinitionLayout()
        _withConfirm = False
        LoadStatus()
        Deshacer()
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmSchedulePayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmSchedulePayment_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
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
            Dim listColumns As New List(Of String)
            listColumns.Add("ThirdId")
            listColumns.Add("SupplierId")
            listColumns.Add("SupplierCode")
            listColumns.Add("SupplierNit")
            listColumns.Add("SupplierName")
            listColumns.Add("SupplierTypeId")
            listColumns.Add("DistributionLineId")
            listColumns.Add("DescriptionLine")
            listColumns.Add("ExpenseConceptIdDistributionLine")
            listColumns.Add("MainAccountIdDistributionLine")
            listColumns.Add("NatureExpenseConcept")

            listColumns.Add("SchedulePaymentDetailId")
            listColumns.Add("AccountPayableId")
            listColumns.Add("Invoice")
            listColumns.Add("ExpirationDate")
            listColumns.Add("AgePayment")
            listColumns.Add("CXPValue")
            listColumns.Add("InvoiceBalance")

            listColumns.Add("AccountPayableShareId")
            listColumns.Add("Share")
            listColumns.Add("ShareExpirationDate")
            listColumns.Add("BalanceShare")
            listColumns.Add("PayValue")
            listColumns.Add("PaymentPercent")
            listColumns.Add("PaymentConceptId")
            listColumns.Add("DiscountValue")
            listColumns.Add("BaseValue")
            listColumns.Add("DiscountRate")
            listColumns.Add("CxPRadicateDate")
            listColumns.Add("PaymentDate")
            listColumns.Add("ApplyDiscountValue")
            listColumns.Add("ValueNote")
            listColumns.Add("AdjusmentValueToAffectBase")
            listColumns.Add("CurrencyAbbreviation")
            listColumns.Add("SchedulePaymentDetailBudget")

            ShowDrilldown(e.CreateDrillDownDataSource(listColumns))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
        If Me._schedulePayment IsNot Nothing AndAlso Me._schedulePayment.Id > 0 Then
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
                    Await Me.NewSchedulePayment()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
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
#End Region

#Region "EditValueChanged"


    Public Property OriginalDatasource As List(Of SP_SchedulePayment_Result) Implements ISchedulePayment.OriginalDatasource

    Private Async Sub INDdtePaymentDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdtePaymentDate.EditValueChanged
        If _schedulePayment IsNot Nothing AndAlso _schedulePayment.Status <> 2 AndAlso SchedulePaymentDatasource IsNot Nothing Then
            Await CalculateDiscountByPaymentDate(SchedulePaymentDatasource, INDdtePaymentDate.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' funcion para calcular el descuento de una cuenta por pagar dependiendo de la fecha de pago
    ''' </summary>
    ''' <param name="ListSchedulePayment"></param>
    ''' <param name="PaymentDate"></param>
    ''' <returns></returns>
    Private Async Function CalculateDiscountByPaymentDate(ListSchedulePayment As List(Of SP_SchedulePayment_Result), PaymentDate As DateTime) As Task
        Try
            Using Model As New MSchedulePayment(Me.Tag)
                AsyncLoader(True)
                Dim Result = Await Model.CalculateDiscountByPaymentDate(ListSchedulePayment, PaymentDate)
                AsyncLoader(False)
                If Result?.StateResult Then
                    SchedulePaymentDatasource = Result.ObjectEmbbeded
                    INDpgcSchedulePayment.Refresh()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = Result?.Message
                End If
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Function
#End Region

#Region "CustomEditValue"
    ''' <summary>
    ''' evento que se utiliza para reevaluar el dato de una celda
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CustomEditValueEventArgs"/> instance containing the event data.</param>
    Private Sub INDpgcSchedulePayment_CustomEditValue(sender As Object, e As CustomEditValueEventArgs) Handles INDpgcSchedulePayment.CustomEditValue
        If Comparer.ReferenceEquals(e.DataField, INDcolPayValuePercent) Then
            If e.Value <> 0 Then
                _valueCell = e.Value
                If Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex - 1, e.RowIndex)) > 0 Then
                    e.Value = Math.Round(Convert.ToDecimal(((Convert.ToDecimal(e.Value) + (Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex + 1, e.RowIndex)))) / Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex - 1, e.RowIndex))) * 100.0F), 2)
                Else
                    e.Value = 0.0F
                End If
            End If
        Else
            e.Value = 0.0F
        End If
    End Sub
#End Region

#Region "CustomCellDisplayText"
    ''' <summary>
    ''' Handles the CustomCellDisplayText event of the INDpgcSchedulePayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraPivotGrid.PivotCellDisplayTextEventArgs"/> instance containing the event data.</param>
    Private Sub INDpgcSchedulePayment_CustomCellDisplayText(sender As Object, e As PivotCellDisplayTextEventArgs) Handles INDpgcSchedulePayment.CustomCellDisplayText
        If Comparer.ReferenceEquals(e.DataField, INDcolPayValuePercent) Then
            If e.Value <> 0 Then
                e.DisplayText = String.Format(RepositoryItemProgressBarPercent.DisplayFormat.FormatString, Math.Round((Convert.ToDecimal((Convert.ToDecimal(_valueCell) + Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex + 1, e.RowIndex))) / Convert.ToDecimal(INDpgcSchedulePayment.GetCellValue(e.ColumnIndex - 1, e.RowIndex))) * 100.0F), 2))
            End If
        End If
    End Sub
#End Region

#Region "UpdateDatasourceInvoice"
    ''' <summary>
    ''' controlador del evento que actualiza el datasource
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="UpdateDatasourceEventArgs"/> instance containing the event data.</param>
    Private Sub Handler_UpdateDatasourceInvoice(sender As Object, e As UpdateDatasourceEventArgs)
        If e.NewInvoiceShareDatasource IsNot Nothing Then

            For Each sp As SP_SchedulePayment_Result In e.NewInvoiceShareDatasource
                Dim alter As SP_SchedulePayment_Result = OriginalDatasource.Where(Function(x) x.SupplierId = sp.SupplierId _
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
                Dim alter2 As SP_SchedulePayment_Result = SchedulePaymentDatasource.Where(Function(x) x.SupplierId = sp.SupplierId _
                                                                                        And x.SupplierName.Equals(sp.SupplierName) _
                                                                                        And x.SupplierCode.Equals(sp.SupplierCode) _
                                                                                        And x.SupplierNit.Equals(sp.SupplierNit) _
                                                                                        And x.DescriptionLine.Equals(sp.DescriptionLine) _
                                                                                        And x.Invoice.Equals(sp.Invoice) _
                                                                                        And x.ExpirationDate.Equals(sp.ExpirationDate) _
                                                                                        And x.InvoiceBalance = (sp.InvoiceBalance) _
                                                                                        And x.Share = (sp.Share) _
                                                                                        And x.SupplierTypeId = sp.SupplierTypeId _
                                                                                        And x.ShareExpirationDate.Equals(sp.ShareExpirationDate)).FirstOrDefault()


                With alter
                    .PayValue = sp.PayValue
                    ''si en el datasource que se envia modificado el registro tiene un numero
                    '' mayor a 0 en positiondetailadd es porque este fue agregado 
                    If sp.PositionDetailsAdd > 0 Then
                        .PositionDetailsAdd = sp.PositionDetailsAdd
                    End If
                    .PaymentPercent = sp.PaymentPercent
                    .PaymentConceptId = sp.PaymentConceptId
                    .ApplyDiscountValue = sp.ApplyDiscountValue
                    .DiscountValue = sp.DiscountValue
                    .DiscountRate = sp.DiscountRate
                    .ValueNote = sp.ValueNote
                    .AdjusmentValueToAffectBase = sp.AdjusmentValueToAffectBase
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
                    If sp.PositionDetailsAdd > 0 Then
                        .PositionDetailsAdd = sp.PositionDetailsAdd
                        countAdd += 1
                    End If
                    .PaymentPercent = sp.PaymentPercent
                        .PaymentConceptId = sp.PaymentConceptId
                        .ApplyDiscountValue = sp.ApplyDiscountValue
                        .DiscountRate = sp.DiscountRate
                        .DiscountValue = sp.DiscountValue
                        .ValueNote = sp.ValueNote
                        .AdjusmentValueToAffectBase = sp.AdjusmentValueToAffectBase
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

        End If
        If _schedulePayment.ChangeTracker.State <> ObjectState.Added Then
            _schedulePayment.MarkAsModified()
        End If

        Dim paySupplier = OriginalDatasource?.FindAll(Function(s) s.PayValue <> 0)?. _
                            GroupBy(Function(d) d.SupplierId)?. _
                            Select(Function(r) r.Key)?.ToList()


        For Each item In ListSchedulePaymentBankAccount?.ToList()?.Where(Function(x) Not paySupplier?.Contains(x.SupplierId))
            item.MarkAsDeleted()
        Next

        createTotalControl(INDpgcSchedulePayment.DataSource)
        INDpgcSchedulePayment.RefreshData()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' nueva programacion de pagos
    ''' </summary>
    Private Async Function NewSchedulePayment() As Task
        Me._schedulePayment = New SchedulePayment()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MCommonTreasury(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
        _presenter.InitializeSchedulePaymentDatasource()
    End Function

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._schedulePayment.Code, Me._schedulePayment.ScheduledDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._schedulePayment.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._schedulePayment.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._schedulePayment.Code, Me._schedulePayment.ScheduledDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._schedulePayment.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Abre el formulario que muestra el detalle de las facturas
    ''' </summary>
    ''' <param name="ds">The ds.</param>
    Private Sub ShowDrilldown(ByVal ds As PivotDrillDownDataSource)
        If ds.RowCount > 0 Then
            Dim OnlyView As Boolean = (_schedulePayment.Status <> 1 AndAlso _schedulePayment.Status <> 0)
            Using Formulario As New InvoiceDetail
                Formulario.PaymentsSettingPaymentsXpo = Me.PaymentsSettingPaymentsXpo
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = True
                Formulario.ShowMenuContext = Not OnlyView
                Formulario.OnlyView = OnlyView
                Formulario.AllowModify = Not OnlyView
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Formulario.DetailInvoiceDatasource = ds
                Formulario.CountAddInvoice = countAdd
                If Me.ListSchedulePaymentBankAccount Is Nothing Then
                    Me.ListSchedulePaymentBankAccount = New Domain.Entities.TrackableCollection(Of SchedulePaymentBankAccount)
                End If
                Formulario.ListSchedulePaymentBankAccount = Me.ListSchedulePaymentBankAccount
                AddHandler Formulario.UpdateDatasourceInvoice, AddressOf Handler_UpdateDatasourceInvoice
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StatePartialPaid"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(244, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(0, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "5", .StatusName = ResourceManager.GetString("StateFullPaid"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(18, Byte), Integer), CType(CType(140, Byte), Integer), CType(CType(0, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
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
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListSchedulePayment
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
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MCommonTreasury(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISchedulePayment.ActionsOnControls
        Set(value As Boolean)
            INDlcRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDdeDate.Enabled = value
            ' INDTreeListSupplierType.Enabled = value
            INDpgcSchedulePayment.Enabled = value
            INDdtePaymentDate.Enabled = value
            'Me.BarraBotones.StatusRecordVisible = value

            If value Then
                INDdeDate.Focus()
                INDdeDate.Properties.ReadOnly = True
            Else
                INDbteCode.Focus()
            End If

            INDlcRoot.EndUpdate()
        End Set
    End Property

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
                Using Model As New MSchedulePayment(Me.Tag)
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetSchedulePayment(Me.Code, True)
                    INDlcRoot.BeginUpdate()
                    _schedulePayment = resultOperation.ObjectEmbbeded
                    If _schedulePayment IsNot Nothing AndAlso _schedulePayment.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MCommonTreasury(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecordTreasury(CStr(Me.Tag), CStr(_schedulePayment.Id))
                            With _schedulePayment
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Code = .Code
                                ScheduledDate = .ScheduledDate
                                Status = .Status.ToString()
                                PaymentDate = .PaymentDate
                                INDdtePaymentDate.Properties.MinValue = .ScheduledDate
                                INDdtePaymentDate.Properties.ReadOnly = IIf(Status = "2", True, False)
                                Me.ListSchedulePaymentBankAccount = .SchedulePaymentBankAccount
                            End With
                            Await LoadSchedulePayment()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._schedulePayment.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecordTreasury(
                                    New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _schedulePayment.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If

                            Me.BarraBotones.SetDocuments(_schedulePayment.Id, Me.Tag.ToString(), Nothing, GetType(SchedulePayment).Name)
                            If _schedulePayment.Status = 1 OrElse _schedulePayment.Status = 2 Then 'estado registrado
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                                'ReadOnlyControls(True)
                            End If
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, _schedulePayment.Id, 0, _schedulePayment.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewSchedulePayment()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                End Using
                INDlcRoot.EndUpdate()
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Loads the schedule payment.
    ''' </summary>
    Private Async Function LoadSchedulePayment() As Task
        Await _presenter.InitializeSchedulePaymentDatasourceWithCode(Code)
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _schedulePayment
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .ScheduledDate = ScheduledDate
            .PaymentDate = Me.PaymentDate
            .OperativeUnitId = _idOperativeUnit
            .Status = If(Status = String.Empty OrElse Status = "-1", 1, CByte(Status))
            .SchedulePaymentBankAccount = Me.ListSchedulePaymentBankAccount
        End With
        Dim schedulePaymentDetail As SchedulePaymentDetail
        For Each sp As SP_SchedulePayment_Result In OriginalDatasource _
                                                    .Where(Function(x) x.SchedulePaymentDetailId <> 0 OrElse x.PayValue <> 0) _
                                                    .OrderBy(Function(x) x.PositionDetailsAdd) _
                                                    .Cast(Of SP_SchedulePayment_Result)() _
                                                    .ToList()

            If sp.SchedulePaymentDetailId <> 0 Then
                schedulePaymentDetail = _schedulePayment.SchedulePaymentDetail.Where(Function(x) x.Id = sp.SchedulePaymentDetailId) _
                                                                                     .OrderBy(Function(x) x.PositionDetailsAdd) _
                                                                                    .Cast(Of SchedulePaymentDetail).FirstOrDefault()
                If sp.PayValue <> 0 Then
                    schedulePaymentDetail.MarkAsModified()
                Else
                    If schedulePaymentDetail IsNot Nothing Then
                        schedulePaymentDetail.MarkAsDeleted()
                    End If
                    Continue For
                End If
            Else
                schedulePaymentDetail = _schedulePayment.SchedulePaymentDetail.Where(Function(x) x.AccountPayableId = sp.AccountPayableId AndAlso x.AccountPayableShareId = sp.AccountPayableShareId) _
                                                                                    .OrderBy(Function(x) x.PositionDetailsAdd) _
                                                                                    .Cast(Of SchedulePaymentDetail).FirstOrDefault()
                If schedulePaymentDetail Is Nothing Then
                    schedulePaymentDetail = New SchedulePaymentDetail()
                    _schedulePayment.SchedulePaymentDetail.Add(schedulePaymentDetail)
                End If
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
                .Paid = False
                .Description = "Pago de Facturas por Dispersión de fondos"
                'Propiedades Extendidas para validación
                .InvoiceBalance = sp.InvoiceBalance
                .BalanceShare = sp.BalanceShare
                .Invoice = sp.Invoice
                .Share = sp.Share
                .SupplierCode = sp.SupplierCode
                .SupplierName = sp.SupplierName
                .AccountPayableCode = sp.AccountPayableCode
                .DiscountValue = sp.ApplyDiscountValue
                .PositionDetailsAdd = sp.PositionDetailsAdd

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
        Next
    End Sub

    ''' <summary>
    ''' Metodo para generar de manera dinamica loscontroles de moneda
    ''' </summary>
    Public Sub createTotalControl(value As List(Of Domain.Entities.SP_SchedulePayment_Result))
        If INDpgcSchedulePayment.DataSource IsNot Nothing Then
            Dim Currencies = From item In value
                             Group By item.CurrencyId, item.CurrencyAbbreviation Into Group
            Dim count = 0
            AdditionalControlPanel.Controls.Clear()
            Dim currencyLen = Currencies.Count()
            For Each item In Currencies
                Dim ctr = New CtrValues()
                If count <> currencyLen Then
                    ctr.INDLciLabelSchedule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    ctr.INDLciLabelDiscount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    ctr.INDLciLabelTotalValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    ctr.MaximumSize = New Size(97, 93)
                End If
                ctr.Dock = DockStyle.Left
                Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
                _culture.NumberFormat = item.CurrencyAbbreviation.GetNumberFormat
                ctr._culture = _culture
                CurrencyForControl = item.CurrencyId
                ctr.INDLciSubtitle.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ctr.INDLcSubtitle.Text = item.CurrencyAbbreviation

                AdditionalControlPanel.Controls.Add(ctr)
                ctr.SetTotalValues(AddressOf ReturnValues)
                ctr.RefreshTotalValues()
                count += 1
                '''se agrega la informacion de las facturas para el popup del controlador
                ctr.InvoiceShareDatasource = SchedulePaymentDatasource.Where(Function(x) x.PayValue <> 0 And x.CurrencyId = item.CurrencyId).Cast(Of SP_SchedulePayment_Result)().ToList()
            Next
        End If
    End Sub

    ''' <summary>
    ''' retorna los valores a mostrar en el control 
    ''' </summary>
    ''' <returns></returns>
    Private Function ReturnValues() As Tuple(Of Decimal, Decimal)
        If CurrencyForControl > 0 Then
            Dim SumBalanceShare = SchedulePaymentDatasource.Where(Function(x) x.PayValue <> 0 AndAlso x.CurrencyId = CurrencyForControl AndAlso (x.BalanceShare - x.ApplyDiscountValue) = x.PayValue).Cast(Of SP_SchedulePayment_Result)().ToList().Select(Function(j) j.BalanceShare).Sum()
            Dim SumPayValue = SchedulePaymentDatasource.Where(Function(x) x.PayValue <> 0 AndAlso x.CurrencyId = CurrencyForControl AndAlso (x.BalanceShare - x.ApplyDiscountValue) <> x.PayValue).Cast(Of SP_SchedulePayment_Result)().ToList().Select(Function(j) j.PayValue).Sum()
            Dim ValueSchedule = SumBalanceShare + SumPayValue
            Dim ValueDiscount = SchedulePaymentDatasource.Where(Function(x) x.ApplyDiscountValue <> 0 AndAlso x.CurrencyId = CurrencyForControl And (x.PayValue + x.ApplyDiscountValue + x.ValueNote) = (x.CXPValue)).Cast(Of SP_SchedulePayment_Result)().ToList().Select(Function(j) j.ApplyDiscountValue).Sum()
            Return New Tuple(Of Decimal, Decimal)(ValueSchedule, ValueDiscount)
        Else
            Dim SumBalanceShare = SchedulePaymentDatasource.Where(Function(x) x.PayValue <> 0 AndAlso (x.BalanceShare - x.ApplyDiscountValue) = x.PayValue).Cast(Of SP_SchedulePayment_Result)().ToList().Select(Function(j) j.BalanceShare).Sum()
            Dim SumPayValue = SchedulePaymentDatasource.Where(Function(x) x.PayValue <> 0 AndAlso (x.BalanceShare - x.ApplyDiscountValue) <> x.PayValue).Cast(Of SP_SchedulePayment_Result)().ToList().Select(Function(j) j.PayValue).Sum()
            Dim ValueSchedule = SumBalanceShare + SumPayValue
            Dim ValueDiscount = SchedulePaymentDatasource.Where(Function(x) x.ApplyDiscountValue <> 0 And (x.PayValue + x.ApplyDiscountValue + x.ValueNote) = (x.CXPValue)).Cast(Of SP_SchedulePayment_Result)().ToList().Select(Function(j) j.ApplyDiscountValue).Sum()
            Return New Tuple(Of Decimal, Decimal)(ValueSchedule, ValueDiscount)
        End If
    End Function

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

        countAdd = 1
        Code = String.Empty
        INDdeDate.EditValue = Nothing
        _ctrValues.ValueSchedule = 0
        _ctrValues.ValueDiscount = 0
        _ctrValues.TotalValue = 0
        _ctrValues.InvoiceShareDatasource = Nothing
        INDlcRoot.EndUpdate()
        ReadOnlyControls(False)
        _withConfirm = False
        Me._schedulePayment = Nothing
        INDpgcSchedulePayment.DataSource = Nothing
        INDpgcSchedulePayment.RefreshData()
        OriginalDatasource = Nothing
        ScheduledDate = Me.GetDateServer()
        Me.PaymentDate = Me.GetDateServer()
        INDdtePaymentDate.Properties.MinValue = Me.GetDateServer()
        Me.ListSchedulePaymentBankAccount = Nothing
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        DeleteBlockedRecord()
        AdditionalControlPanel.Controls.Clear()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements Base.ICrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' Anulars this instance.
    ''' </summary>
    Public Async Sub Anular()
        If Me._schedulePayment IsNot Nothing AndAlso Me._schedulePayment.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MSchedulePayment(Me.Tag.ToString())
                        _schedulePayment.Status = 3
                        AsyncLoader(True)
                        Dim result = Await Model.SaveSchedulePayment(Me._schedulePayment, False, Me._idCurrentSequense)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Me._schedulePayment = result.ObjectEmbbeded
                            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, _schedulePayment.Id, 0, _schedulePayment.Id)
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            _schedulePayment.SchedulePaymentDetail.Clear()
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
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        If OriginalDatasource.Where(Function(x) x.PayValue <> 0).Cast(Of SP_SchedulePayment_Result)().ToList().Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe pagar al menos una factura"
            Exit Sub
        End If
        If _idOperativeUnit = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una unidad operativa"
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MSchedulePayment(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveSchedulePayment(Me._schedulePayment, _withConfirm, Me._idCurrentSequense)
                If Result.StatusCode = eStatusResult.SUCCESS Then
                    If _schedulePayment.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Me._schedulePayment = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    ShowMessage(Result.StatusCode) = Result.Message
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, _schedulePayment.Id, 0, _schedulePayment.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, _schedulePayment.Id, 0, _schedulePayment.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _schedulePayment.Id, 0, _schedulePayment.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    If _schedulePayment.ChangeTracker.State = ObjectState.Added Then
                        _schedulePayment.SchedulePaymentDetail.Clear()
                        _schedulePayment.SchedulePaymentBankAccount.Clear()
                    End If
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
    ''' Confirmars this instance.
    ''' </summary>
    Private Async Sub Confirmar()
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessageWithUpdateLost", NAME_MODULE), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Try
                Using Model As New MSchedulePayment(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim Result = Await Model.ConfirmSchedulePayment(Me._schedulePayment.Id)
                    AsyncLoader(False)
                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SchedulePaymentConfirmComplete", NAME_MODULE)
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, _schedulePayment.Id, 0, _schedulePayment.Id)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        Me.Deshacer()
                    Else
                        _schedulePayment.SchedulePaymentDetail.Clear()
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
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewSchedulePayment()
        End If
    End Sub
#End Region

#Region "BarButton Events"
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
    ''' Barras the botones_ click_ actualizar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        _withConfirm = True
        varImp = 3
        Guardar()
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
    ''' Barras the botones_ click_ guardar confirmar.
    ''' </summary>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        _withConfirm = True
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular.
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        Anular()
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
    ''' Barras the botones_ click_ refresh grid.
    ''' </summary>
    Private Sub BarraBotones_Click_RefreshGrid() Handles BarraBotones.Click_RefreshGrid
        If MessageIndigo.Show(ResourceManager.GetString("RefreshScheduleDatasource"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _presenter.InitializeSchedulePaymentDatasource()
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

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, _schedulePayment.Id, 0, _schedulePayment.Id)
    End Sub


#End Region

End Class