'***********************************************************************
' Assembly         : Presentacion.Payments.Utils
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/07/2014
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Presentation.Payments.MVP

#End Region

Public Class FrmTransfers
    Implements ITransfers, ICustomizableForm

#Region "Constructor"

    Private _advanceValue As Decimal
    Dim ctrAdvance As CtrAdvanceTreasury

    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        ctrAdvance = New CtrAdvanceTreasury()
        ctrAdvance.Title = "Saldo Anticipo"
        ctrAdvance.WithEvent = False
        ctrAdvance.SetAdvance(AddressOf getAdvance)
        ctrAdvance.PrintAdvance()
        ctrAdvance.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrAdvance)
    End Sub

    ''' <summary>
    ''' Obtiene el valor del anticipo
    ''' </summary>
    ''' <returns></returns>
    Private Function getAdvance() As Decimal
        Return AdvanceBalance
    End Function

#End Region

#Region "Properties"

    Public ReadOnly Property AdvanceBalance As Decimal
        Get
            Dim _advanceBalance As Decimal = _advanceValue
            If ListPaymentTranferDetail IsNot Nothing AndAlso ListPaymentTranferDetail.Count > 0 Then
                _advanceBalance = _advanceBalance - ListPaymentTranferDetail.Sum(Function(d) d.Value)
            End If
            If listPaymentsTranfersOtherConcept IsNot Nothing AndAlso listPaymentsTranfersOtherConcept.Count > 0 Then
                _advanceBalance = _advanceBalance - listPaymentsTranfersOtherConcept.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
                _advanceBalance = _advanceBalance + listPaymentsTranfersOtherConcept.Where(Function(d) d.Nature <> 1).Sum(Function(d) d.Value)
            End If

            If paymentTransfer IsNot Nothing AndAlso paymentTransfer.Status = 2 Then
                _advanceBalance = _advanceValue
            End If

            Return _advanceBalance
        End Get
    End Property

    Public ReadOnly Property AdvanceValue As Decimal
        Get
            Dim _value As Decimal = 0
            If ListPaymentTranferDetail IsNot Nothing AndAlso ListPaymentTranferDetail.Count > 0 Then
                _value = _value + ListPaymentTranferDetail.Sum(Function(d) d.Value)
            End If
            If listPaymentsTranfersOtherConcept IsNot Nothing AndAlso listPaymentsTranfersOtherConcept.Count > 0 Then
                _value = _value - listPaymentsTranfersOtherConcept.Where(Function(d) d.Nature = 1).Sum(Function(d) d.Value)
                _value = _value + listPaymentsTranfersOtherConcept.Where(Function(d) d.Nature <> 1).Sum(Function(d) d.Value)
            End If
            Return _value
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterOtherConceptId As Integer? Implements ITransfers.CostCenterOtherConceptId
        Get
            Return INDsleCostCenterOtherConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenterOtherConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del centro costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterOtherConceptXpo As XPInstantFeedbackSource Implements ITransfers.CostCenterOtherConceptXpo
        Get
            Return INDsleCostCenterOtherConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenterOtherConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountOtherConceptId As Integer? Implements ITransfers.MainAccountOtherConceptId
        Get
            Return INDsleMainAccountOtherConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccountOtherConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountOtherConceptXpo As XPInstantFeedbackSource Implements ITransfers.MainAccountOtherConceptXpo
        Get
            Return INDsleMainAccountOtherConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMainAccountOtherConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Nature As Integer? Implements ITransfers.Nature
        Get
            Return INDsleNatureOtherConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleNatureOtherConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyOtherConceptId As Integer? Implements ITransfers.ThirdPartyOtherConceptId
        Get
            Return INDsleThirdPartyOtherConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdPartyOtherConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ThirdPartyOtherConceptXpo As XPInstantFeedbackSource Implements ITransfers.ThirdPartyOtherConceptXpo
        Get
            Return INDsleThirdPartyOtherConcept.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdPartyOtherConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de los otros conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueOtherConcept As Decimal Implements ITransfers.ValueOtherConcept
        Get
            Return INDtxtValueOtherConcept.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValueOtherConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptNoteId As Integer? Implements ITransfers.AccountPayableConceptNoteId
        Get
            Return INDsleAccountPayableConceptNote.EditValue
        End Get
        Set(value As Integer?)
            INDsleAccountPayableConceptNote.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptNoteXpo As XPInstantFeedbackSource Implements ITransfers.AccountPayableConceptNoteXpo
        Get
            Return INDsleAccountPayableConceptNote.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccountPayableConceptNote.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TransferType As Integer? Implements ITransfers.TransferType
        Get
            Return INDsleTransferType.EditValue
        End Get
        Set(value As Integer?)
            INDsleTransferType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements ITransfers.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el saldo de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BalanceShare As Decimal Implements ITransfers.BalanceShare
        Get
            Return INDtxtBalance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillDate As Date Implements ITransfers.BillDate
        Get
            Return INDdteBillDate.EditValue
        End Get
        Set(value As Date)
            INDdteBillDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor a cruzar de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CrossValueShare As Decimal Implements ITransfers.CrossValueShare
        Get
            Return INDtxtCrossValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtCrossValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de expiracion de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExpiratedDateShare As Date Implements ITransfers.ExpiratedDateShare
        Get
            Return INDdteExpirationDate.EditValue
        End Get
        Set(value As Date)
            INDdteExpirationDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValueShare As Decimal Implements ITransfers.ValueShare
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccountPayableShare As Integer Implements ITransfers.IdAccountPayableShare
        Get
            Return INDsleBill.EditValue
        End Get
        Set(value As Integer)
            INDsleBill.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Carga el datasource del control de facturas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableSharesDatasource As XPInstantFeedbackSource Implements ITransfers.AccountPayableSharesDatasource
        Get
            Return CType(INDsleBill.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBill.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount As Integer? Implements ITransfers.IdAccount
        Get
            Return _idAccount
        End Get
        Set(value As Integer?)
            _idAccount = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplier As Integer? Implements ITransfers.IdSupplier
        Get
            Return INDsleSupplier.EditValue
        End Get
        Set(value As Integer?)
            INDsleSupplier.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el listado de proveedores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierXpo As XPInstantFeedbackSource Implements ITransfers.SupplierXpo
        Get
            Return INDsleSupplier.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleSupplier.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de anticipos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdvancePaymentsXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ITransfers.AdvancePaymentsXpo
        Get
            Return CType(INDsleAdvance.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleAdvance.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del anticipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAdvancePayments As Integer? Implements ITransfers.IdAdvancePayments
        Get
            Return INDsleAdvance.EditValue
        End Get
        Set(value As Integer?)
            INDsleAdvance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ITransfers.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia numerica del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As PaymentsSecuence Implements ITransfers.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PaymentsSecuence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequence.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o estalece los comentarios del traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Comments As String Implements ITransfers.Comments
        Get
            Return INDmemoComments.Text
        End Get
        Set(value As String)
            INDmemoComments.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo del traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Consecutive As String Implements ITransfers.Consecutive
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As XPInstantFeedbackSource Implements ITransfers.CostCenterXpo
        Get
            Return INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCostCenter As Integer? Implements ITransfers.IdCostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de los registros de los conceptos de pagos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Integer Implements ITransfers.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Integer)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' optiene la moneda del anticipo seleccionado
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer? Implements ITransfers.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDSleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' codigo standart de la moneda ISO4217
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyAbbreviation As String Implements ITransfers.CurrencyAbbreviation
        Get
            Return _currencyAbbreviation
        End Get
        Set(value As String)
            _currencyAbbreviation = value
        End Set
    End Property

#End Region

#Region "Constant"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene la informacion sacada del control de anticipo (Xpo)
    ''' </summary>
    ''' <remarks></remarks>
    Dim advancePaymentsSearchXpo As Infrastructure.Data.Xpo.PaymentsRepository.AdvancePaymentsXpo

    ''' <summary>
    ''' Variable que contiene la informacion sacada del control de proveedor (Xpo)
    ''' </summary>
    ''' <remarks></remarks>
    Dim suppplierMainAccount As Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_Supplier

    ''' <summary>
    ''' Listado de eliminados de los detalles del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeletePaymentTransferDetail As List(Of PaymentTransferDetail)

    ''' <summary>
    ''' Listado de detalles del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListPaymentTranferDetail As List(Of PaymentTransferDetail)


    ''' <summary>
    ''' Representa la entidad de traslados
    ''' </summary>
    ''' <remarks></remarks>
    Dim paymentTransfer As PaymentTransfer

    ''' <summary>
    ''' Contiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Dim dateServerVariable As DateTime

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Variable que representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PTransfers

    ''' <summary>
    ''' Representa el id del proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _idSupplier As Integer

    ''' <summary>
    ''' Variable que retorna la propiedad de cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _idAccount As Integer?

    ''' <summary>
    ''' Variable que contiene la informacion sacada del control de facturas para las cuotas (Xpo)
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountPayableShareXpo As Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Variable bandera que me dice cuando puedo realizar el getFocusedRow de los search
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSearch As Boolean = False

    ''' <summary>
    ''' Variable para el id del tercero
    ''' </summary>
    ''' <remarks></remarks>
    Private _idThirdParty As Integer

    ''' <summary>
    ''' Dictionario de datos para las cuotas de las facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private _dictionaryAccountPayable As Dictionary(Of String, List(Of AccountPayableSharesXpo))

    ''' <summary>
    ''' Representa el valor anterior del valor a cruzar de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private _oldValueGrid As Decimal

    Private _codeNameSupplierDocumentIndexed As String = String.Empty
    Private _nitThitrdPartyDocumentIndexed As String = String.Empty
    Private _nameThirdPartyDocumentIndexed As String = String.Empty

    Dim codeDocumentIndexed As String

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListTransferType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' listado de las naturalezas de las cuentas
    ''' </summary>
    Dim listNature As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Permite saber si se ejecuta el EditValueChanged
    ''' True=Permite, False=No permite
    ''' </summary>
    ''' <remarks></remarks>
    Dim FlagChangeSearch As Boolean = True

    ''' <summary>
    ''' standart ISO4217
    ''' </summary>
    Private _currencyAbbreviation As String

    ''' <summary>
    ''' id de la moneda que viene en la factura
    ''' </summary>
    Private _currencyInvoiceId As Integer?

    ''' <summary>
    ''' Lista de la tasa de cambio
    ''' </summary>
    Private _listTRM As List(Of TRM)

    ''' <summary>
    ''' Redondeo
    ''' </summary>
    Private _decimals As Integer = 2

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        advancePaymentsSearchXpo = Nothing
        suppplierMainAccount = Nothing
        ListDeletePaymentTransferDetail = Nothing
        ListPaymentTranferDetail = Nothing
        paymentTransfer = Nothing
        dateServerVariable = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Presenter = Nothing
        _idSupplier = Nothing
        _idAccount = Nothing
        accountPayableShareXpo = Nothing
        record = Nothing
        banSearch = Nothing
        _idThirdParty = Nothing
        _dictionaryAccountPayable = Nothing
        _oldValueGrid = Nothing
        _codeNameSupplierDocumentIndexed = Nothing
        _nitThitrdPartyDocumentIndexed = Nothing
        _nameThirdPartyDocumentIndexed = Nothing
        codeDocumentIndexed = Nothing
        ListTransferType = Nothing
        varImp = Nothing
        listNature = Nothing
        FlagChangeSearch = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmTransfers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyTransfers, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PTransfers(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        InitializeTuple()
        'viewGridBill.OptionsFind.AlwaysVisible = True
        'IndigoGridControl1.SetExportButton(INDgcBills, True)
        IndigoGridControl1.RefreshGrid(INDgcBills)
        IndigoGridControl1.RefreshGrid(INDgcOtherConcept)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewGridBill, ListActions)
        IndigoGridView2.SetListAcction(INDGvOtherConcept, ListActions)
        IndigoGridView1.MoreInfoColunmns(viewGridBill)
        _dictionaryAccountPayable = New Dictionary(Of String, List(Of AccountPayableSharesXpo))

        INDdteDocumentDate.Properties.MaxValue = GetDateServer()

        INDEsbBill.AddRangeColumns("Factura", "Valor Cruce")
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleSupplier.QueryPopUp
        If INDsleSupplier.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If INDsleCostCenter.Properties.DataSource Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdvance_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleAdvance.QueryPopUp
        If INDsleAdvance.Properties.DataSource Is Nothing Then
            If INDsleSupplier.EditValue IsNot Nothing Then
                Presenter.InitializeAdvancePayments(IdSupplier)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBill_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleBill.QueryPopUp
        If INDsleBill.Properties.DataSource Is Nothing Then
            If IdSupplier > 0 Then
                InitializeBills()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de nota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayableConceptNote_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleAccountPayableConceptNote.QueryPopUp
        If AccountPayableConceptNoteXpo Is Nothing Then
            Presenter.InitializeAccountPayableConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountOtherConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleMainAccountOtherConcept.QueryPopUp
        If INDsleMainAccountOtherConcept.Properties.ReadOnly Then
            Exit Sub
        End If
        If MainAccountOtherConceptXpo Is Nothing Then
            Presenter.InitializeMainAccountOtherConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdPartyOtherConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleThirdPartyOtherConcept.QueryPopUp
        If ThirdPartyOtherConceptXpo Is Nothing Then
            Presenter.InitializeThirdPartyOtherConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterOtherConcept_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleCostCenterOtherConcept.QueryPopUp
        If CostCenterOtherConceptXpo Is Nothing Then
            Presenter.InitializeCostCenterOtherConcept()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSupplier.EditValueChanged
        If INDsleSupplier.EditValue IsNot Nothing Then
            If banSearch = False Then
                LoadInformationSupplier()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de anticipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdvance_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdvance.EditValueChanged
        If INDsleAdvance.EditValue IsNot Nothing AndAlso INDsleAdvance.EditValue > 0 Then
            If banSearch = False Then
                LoadInformationAdvancePayments()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBill_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBill.EditValueChanged
        If INDsleBill.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(INDsleBill.EditValue) AndAlso INDsleBill.EditValue > 0 Then
            If banSearch = False Then
                LoadInformationShares()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de traslado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleTransferType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleTransferType.EditValueChanged
        If TransferType IsNot Nothing Then
            InitializeBills()

            If TransferType = 1 Then
                INDEsbBill.AddRangeColumns("Factura", "Valor Cruce")
            Else
                INDEsbBill.AddRangeColumns("Proveedor", "Factura", "Valor Cruce")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de concepto de nota
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayableConceptNote_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountPayableConceptNote.EditValueChanged
        If AccountPayableConceptNoteId IsNot Nothing Then
            Dim accountPayableConceptNoteXpo = DirectCast(DirectCast(ViewSearchConceptNotes.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.PaymentsAccountPayableConceptNotesXpo)
            If accountPayableConceptNoteXpo IsNot Nothing Then
                If accountPayableConceptNoteXpo.ConceptType = 1 Then 'Si el tipo de concepto de nota es General
                    INDsleMainAccountOtherConcept.Properties.ReadOnly = False
                    MainAccountOtherConceptId = Nothing
                    INDsleMainAccountOtherConcept.Properties.NullText = String.Empty
                    VisibilityThirdPartyAndCostCenter(False, False)
                Else 'Si el tipo de concepto de nota es Especifico
                    INDsleMainAccountOtherConcept.Properties.ReadOnly = True
                    FlagChangeSearch = False
                    MainAccountOtherConceptId = accountPayableConceptNoteXpo.IdAccount.Id
                    INDsleMainAccountOtherConcept.Properties.NullText = accountPayableConceptNoteXpo.IdAccount.NumberName
                    FlagChangeSearch = True
                    VisibilityThirdPartyAndCostCenter(accountPayableConceptNoteXpo.IdAccount.HandlesThirdParty, accountPayableConceptNoteXpo.IdAccount.HandlesCostCenter)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que oculta o se hace visible el layout del centro de costo y tercero
    ''' </summary>
    ''' <param name="HandlesThirdParty"></param>
    ''' <param name="HandlesCostCenter"></param>
    ''' <remarks></remarks>
    Private Sub VisibilityThirdPartyAndCostCenter(HandlesThirdParty As Boolean, HandlesCostCenter As Boolean)
        If HandlesThirdParty Then 'Si la cuenta maneja tercero se habilita el tercero
            INDlyItemThirdPartyOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemThirdPartyOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        If HandlesCostCenter Then 'Si la cuenta maneja centro costo se habilita el centro costo
            INDlyItemCostCenterOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDlyItemCostCenterOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        RezizablePopup()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para redimensionar el popup dependiendo de los layout
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RezizablePopup()
        Dim size As System.Drawing.Size
        size.Width = 500

        If INDlyItemThirdPartyOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyItemCostCenterOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            size.Height = 360
        ElseIf INDlyItemThirdPartyOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always OrElse INDlyItemCostCenterOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            size.Height = 330
        Else
            size.Height = 300
        End If

        INDpceOtherConcepts.Properties.PopupSizeable = True
        INDpopupOtherConcept.Size = size
        INDpceOtherConcepts.Properties.PopupSizeable = False
        INDpceOtherConcepts.ShowPopup()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountOtherConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMainAccountOtherConcept.EditValueChanged
        If MainAccountOtherConceptXpo IsNot Nothing Then
            Dim mainAccountXpo = DirectCast(DirectCast(INDGvMainAccount.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.AccountingRepository.PUCServiceXpo)
            VisibilityThirdPartyAndCostCenter(mainAccountXpo.HandlesThirdParty, mainAccountXpo.HandlesCostCenter)
        End If

    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Consecutive.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Consecutive) Then
                    Await Me.NewTransfer()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de popup de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceBill.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            Dim listError = ValidateControlsSupplierAndAdvance()
            If listError.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listError
            Else
                INDpceBill.ShowPopup()
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnAddBill_Click(sender As Object, e As EventArgs) Handles INDbtnAddBill.Click
        Await AddBill()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_Click(sender As Object, e As EventArgs) Handles INDpceBill.Click
        Dim listError = ValidateControlsSupplierAndAdvance()
        If listError.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listError
            INDpceBill.ClosePopup()
        End If
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmTransfers_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Await DeleteBlockedRecord()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#Region "Leave"

    ''' <summary>
    ''' Evento que se dispara al perder el foco del repositorio de valur cruce
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTextCrossValue_Leave(sender As Object, e As EventArgs) Handles INDrepTextCrossValue.Leave
        ValidateShare(sender)
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            DeleteDetail()
        End If
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            DeleteDetail()
        End If
    End Sub

#End Region

#Region "Enter"

    ''' <summary>
    ''' Evento que se dispara al colocar el foco en el repositorio de texto del valor a cruzar de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTextCrossValue_Enter(sender As Object, e As EventArgs) Handles INDrepTextCrossValue.Enter
        Dim crossValueTmp = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
        _oldValueGrid = crossValueTmp.EditValue
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_Popup(sender As Object, e As EventArgs) Handles INDpceBill.Popup
        'viewGridBill.OptionsFind.AlwaysVisible = False
        'IndigoGridControl1.SetExportButton(INDgcBills, False)
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceBill.CloseUp
        If ListPaymentTranferDetail Is Nothing Then
            'viewGridBill.OptionsFind.AlwaysVisible = False
            'IndigoGridControl1.SetExportButton(INDgcBills, False)
        ElseIf ListPaymentTranferDetail.Count > 0 Then
            'viewGridBill.OptionsFind.AlwaysVisible = True
            'IndigoGridControl1.SetExportButton(INDgcBills, True)
        Else
            'viewGridBill.OptionsFind.AlwaysVisible = False
            'IndigoGridControl1.SetExportButton(INDgcBills, False)
        End If
        If e.CloseMode = PopupCloseMode.Cancel Then
            IndigoGridControl1.ControlNextFocus = True
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Maintenance.FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCostCenter With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se abre para abrir el form de concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccountPayableConceptNote_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountPayableConceptNote.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(724, Nothing, True)
            Presenter.InitializeAccountPayableConceptNote()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMainAccountOtherConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccountOtherConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(602, Nothing, True)
            Presenter.InitializeMainAccountOtherConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form del tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdPartyOtherConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdPartyOtherConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.InitializeThirdPartyOtherConcept()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterOtherConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenterOtherConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(517, Nothing, True)
            Presenter.InitializeCostCenterOtherConcept()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que inicializa el datasource de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeBills()
        If IdSupplier IsNot Nothing AndAlso TransferType IsNot Nothing Then
            Presenter.InitializeAccountPayable(IdSupplier, TransferType)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que inicializa el search
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListTransferType = New List(Of Tuple(Of Integer, String))
        ListTransferType.Add(New Tuple(Of Integer, String)(1, "Mismo Proveedor"))
        ListTransferType.Add(New Tuple(Of Integer, String)(2, "Diferente Proveedor"))
        INDsleTransferType.Properties.DataSource = ListTransferType.ToList

        listNature = New List(Of Tuple(Of Byte, String))
        listNature.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        listNature.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDsleNatureOtherConcept.Properties.DataSource = listNature.ToList

    End Sub

    ''' <summary>
    ''' Valida el pago de las cuotas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateShareds() As String
        Dim listError As New StringBuilder()
        If ListPaymentTranferDetail IsNot Nothing AndAlso ListPaymentTranferDetail.Count > 0 Then
            For Each accountSharedTmp In ListPaymentTranferDetail
                If _dictionaryAccountPayable.ContainsKey(accountSharedTmp.NumberBill) Then
                    Dim listShared = _dictionaryAccountPayable(accountSharedTmp.NumberBill)
                    Dim index As Integer
                    For index = accountSharedTmp.Share - 1 To 1 Step -1
                        Dim query = ListPaymentTranferDetail.Where(Function(y) y.Share = index And y.NumberBill = accountSharedTmp.NumberBill)
                        If query.Count > 0 Then
                            Dim sharedComplexTmp = query.ToList.Item(0)
                            If sharedComplexTmp.Value <> sharedComplexTmp.BalanceShare Then
                                listError.AppendLine(String.Format(ResourceManager.GetString("ShareWithBalance", "Treasury"), accountSharedTmp.Share.ToString(), accountSharedTmp.NumberBill, sharedComplexTmp.Share.ToString()))
                                Continue For
                            End If
                        Else
                            Dim sharedXpoTmp = listShared.Where(Function(x) x.Share = index).ToList.Item(0)
                            If sharedXpoTmp.Balance <> 0 Then
                                listError.AppendLine(String.Format(ResourceManager.GetString("ShareWithBalance", "Treasury"), accountSharedTmp.Share.ToString(), accountSharedTmp.NumberBill, sharedXpoTmp.Share.ToString()))
                                Continue For
                            End If
                        End If
                    Next
                End If
            Next
        End If
        Return listError.ToString()
    End Function

    ''' <summary>
    ''' Valida los controles de proveedor y anticipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsSupplierAndAdvance() As String
        Dim listError As New StringBuilder
        If IdSupplier Is Nothing Then
            listError.AppendLine("-Falta elegir un Proveedor.")
        End If
        If IdAdvancePayments Is Nothing Then
            listError.AppendLine("-Falta elegir un Anticipo.")
        End If
        Return listError.ToString
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        ' Me.ViewModeEditHold = True
        If Me.paymentTransfer IsNot Nothing AndAlso Me.paymentTransfer.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await DeleteBlockedRecord()
                Consecutive = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Consecutive = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub CleanControls()
        INDlyTransfers.BeginUpdate()
        AccountPayableSharesDatasource = Nothing
        ReadOnlyControls(False)
        INDsleAccount.Properties.ReadOnly = True
        ActionsOnControlsPoppup = False
        ActionsOnControls = False
        Consecutive = String.Empty
        DocumentDate = Nothing

        banSearch = True
        IdSupplier = Nothing
        IdAccountPayableShare = Nothing
        IdAdvancePayments = Nothing
        banSearch = False

        IdAccount = Nothing
        INDsleAccount.EditValue = Nothing
        IdCostCenter = Nothing
        Comments = String.Empty
        BillDate = Nothing
        ExpiratedDateShare = Nothing
        ValueShare = 0
        BalanceShare = 0
        CrossValueShare = 0
        _advanceValue = 0
        INDgcBills.DataSource = Nothing
        INDgcOtherConcept.DataSource = Nothing
        ListPaymentTranferDetail = Nothing
        ListDeletePaymentTransferDetail = Nothing
        listPaymentsTranfersOtherConcept = Nothing
        listPaymentsTranfersOtherConceptDelete = Nothing
        paymentTransfer = Nothing
        TransferType = Nothing
        Me._currencyInvoiceId = Nothing
        Me._listTRM = Nothing
        Me.CurrencyExchangeActions(False)
        Me.LoadDefaultCurrency()

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        ctrAdvance.PrintAdvance()

        INDsleSupplier.Properties.NullText = String.Empty
        INDsleAdvance.Properties.NullText = String.Empty
        INDsleAccount.Properties.NullText = String.Empty
        INDsleCostCenter.Properties.NullText = String.Empty

        INDlyTransfers.EndUpdate()
        Me.BarraBotones.StatusRecord = "1"
        Me.BarraBotones.StatusRecordVisible = False
        Await DeleteBlockedRecord()
        CleanControlsOtherConcept()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.codeDocumentIndexed, Me._codeNameSupplierDocumentIndexed, Me._nitThitrdPartyDocumentIndexed, Me._nameThirdPartyDocumentIndexed),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.codeDocumentIndexed & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.codeDocumentIndexed),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.codeDocumentIndexed, Me._codeNameSupplierDocumentIndexed, Me._nitThitrdPartyDocumentIndexed, Me._nameThirdPartyDocumentIndexed)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.codeDocumentIndexed)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDBteCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDsleSupplier.Enabled = value
            INDsleAdvance.Enabled = value
            INDsleTransferType.Enabled = value
            INDsleAccount.Enabled = value
            INDsleCostCenter.Enabled = value
            INDmemoComments.Enabled = value
            INDpceBill.Enabled = value
            INDgcBills.Enabled = value
            INDpceOtherConcepts.Enabled = value
            INDgcOtherConcept.Enabled = value
            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Prepara los controles y realiza la logica para
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewTransfer() As Task
        paymentTransfer = New PaymentTransfer()
        TransferType = 1
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Consecutive = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Consecutive = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Consecutive = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        GetDate(True, True)
    End Function

    ''' <summary>
    ''' Carga la informacion de las cuotas y de la factura que viene en el control de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub LoadInformationShares()
        accountPayableShareXpo = DirectCast(DirectCast(viewAccountPayableShare.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableSharesXpo)
        If accountPayableShareXpo IsNot Nothing Then
            Dim TRMValue As Decimal = 1
            Dim account = accountPayableShareXpo.IdAccountPayable
            Me.BillDate = accountPayableShareXpo.IdAccountPayable.DocumentDate
            Me.ExpiratedDateShare = accountPayableShareXpo.DateExpires
            SetDictionary(account)
            If Not Await Me.CurrencyExchangeActions(True,
                                                    Me.CurrencyId,
                                                    Me.CurrencyAbbreviation,
                                                    If(account?.CurrencyId > 0, account?.CurrencyId, Me.indigo.OfficialCurrencyId),
                                                    Me.DocumentDate) Then
                CleanControlsPopup()
                Exit Sub
            End If
            If Me.CurrencyId <> account?.CurrencyId Then
                TRMValue = _listTRM.FirstOrDefault(Function(x) x.CurrencyId = Me.CurrencyId AndAlso x.OfficialCurrencyId = account?.CurrencyId AndAlso x.MeasurementDate.Date = Me.DocumentDate.Value.Date)?.Value
            End If
            Me.ValueShare = Math.Round(accountPayableShareXpo.InitialValue / TRMValue, Me._decimals)
            Me.BalanceShare = Math.Round(accountPayableShareXpo.Balance / TRMValue, Me._decimals)
            ActionsOnControlsPoppup = True
            INDtxtCrossValue.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Carga la informacion que viene en el control de anticipo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInformationAdvancePayments()
        advancePaymentsSearchXpo = DirectCast(DirectCast(viewAdvance.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.AdvancePaymentsXpo)
        If advancePaymentsSearchXpo IsNot Nothing Then
            Me.CurrencyId = IIf(advancePaymentsSearchXpo.Currency?.Id Is Nothing, indigo.OfficialCurrencyId, advancePaymentsSearchXpo.Currency?.Id)
            Me.CurrencyAbbreviation = IIf(advancePaymentsSearchXpo?.Currency Is Nothing, indigo.CurrencyISO4217, advancePaymentsSearchXpo?.Currency?.Abbreviation)
            IdAccount = advancePaymentsSearchXpo.IdAccount.Id
            INDsleAccount.EditValue = IdAccount
            INDsleAccount.Properties.NullText = advancePaymentsSearchXpo.IdAccount.Number + " - " + advancePaymentsSearchXpo.IdAccount.Name
            setFormatGeneralControls(Me.CurrencyAbbreviation)
            If advancePaymentsSearchXpo.IdAccount.HandlesCostCenter = True Then
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemCostCenter.AllowHide = False
            Else
                INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                IdCostCenter = Nothing
                INDlyItemCostCenter.AllowHide = True
            End If
            BarraBotones.StatusRecordVisible = True
            _advanceValue = advancePaymentsSearchXpo.Balance
            ctrAdvance.PrintAdvance()
        End If
    End Sub

    ''' <summary>
    ''' Carga la informacion que viene en el control de proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInformationSupplier()
        suppplierMainAccount = DirectCast(DirectCast(viewSupplier.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.MaintenanceRepository.Maintenance_Supplier)
        _idThirdParty = suppplierMainAccount.IdThirdParty.Id

        _codeNameSupplierDocumentIndexed = suppplierMainAccount.CodeName
        _nameThirdPartyDocumentIndexed = suppplierMainAccount.IdThirdParty.Name
        _nitThitrdPartyDocumentIndexed = suppplierMainAccount.IdThirdParty.Nit

        Presenter.InitializeAdvancePayments(IdSupplier)
        IdAdvancePayments = Nothing
        IdAccount = Nothing
        _advanceValue = 0
        ctrAdvance.PrintAdvance()
        INDsleAccount.Properties.NullText = String.Empty
        InitializeBills()
    End Sub

    ''' <summary>
    ''' Setea los controles al formato preestablecido
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Private Sub setFormatGeneralControls(Abbreviation As String)
        Me.INDSleCurrency.Properties.NullText = $"{Abbreviation}"
        Me.GridColumnValueTransfer = Window.Utils.FormatGrid(GridColumnValueTransfer, Abbreviation)
        Me.GridColumnValueOtherConcept = Window.Utils.FormatGrid(GridColumnValueOtherConcept, Abbreviation)

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat
        INDtxtValue.Properties.Mask.Culture = _culture
        INDtxtBalance.Properties.Mask.Culture = _culture
        INDtxtCrossValue.Properties.Mask.Culture = _culture
        INDtxtValueOtherConcept.Properties.Mask.Culture = _culture
        ctrAdvance.CodeISO4217 = Abbreviation
    End Sub


    ''' <summary>
    ''' Establece la mondeda de cambio
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CurrencyExchangeActions(value As Boolean,
                                                   Optional _currencyId As Integer? = Nothing,
                                                   Optional _currencyAbbreviation As String = Nothing,
                                                   Optional ToCurrencyId As Integer? = Nothing,
                                                   Optional ByVal dateDocument As Date? = Nothing) As Task(Of Boolean)
        'Si el valor es esta en False se oculta o si no pasa alguna de las otras condiciones
        If Not value OrElse CurrencyId Is Nothing OrElse String.IsNullOrEmpty(_currencyAbbreviation) OrElse ToCurrencyId Is Nothing OrElse (_currencyId = ToCurrencyId) Then
            Me.ShowOrHideExchangeControl(False)
            Return (Not value OrElse _currencyId = ToCurrencyId)
        End If

        'si pasa la validacion se muestra el control
        Me.ShowOrHideExchangeControl(True)

        If dateDocument Is Nothing Then
            dateDocument = GetDateServer().Date
        End If

        'si ya existe el TRM se consulta el guardado
        If _listTRM IsNot Nothing AndAlso _listTRM.Any(Function(x) x.CurrencyId = _currencyId AndAlso x.OfficialCurrencyId = ToCurrencyId AndAlso x.MeasurementDate.Date = dateDocument.Value.Date) Then
            INDTxtExchange.Text = $"{_currencyAbbreviation} - TRM:{String.Format("{0:n2}", Utils.VisibleTRM(_listTRM.FirstOrDefault(Function(x) x.CurrencyId = _currencyId AndAlso
                                                                                                          x.OfficialCurrencyId = ToCurrencyId AndAlso
                                                                                                          x.MeasurementDate.Date = dateDocument.Value.Date).Value))}"
            Return True
        End If

        Using model As New MTransfers(Me.Tag.ToString())
            Dim result = Await model.GetTRMbyCurrencyId(_currencyId, ToCurrencyId, dateDocument.Value.Date)
            If result Is Nothing OrElse Not result?.StateResult Then
                Me.ShowOrHideExchangeControl(False)
                Return False
            End If

            _listTRM = If(_listTRM Is Nothing, New List(Of TRM), _listTRM)
            _listTRM.Add(result.ObjectEmbbeded)
            Me.Mensaje(EeventViewerImages.Informacion) = result?.Message
            Dim textTRM = String.Format("{0:n2}", Utils.VisibleTRM(_listTRM.FirstOrDefault(Function(x) x.CurrencyId = _currencyId AndAlso
                                                                                                          x.OfficialCurrencyId = ToCurrencyId AndAlso
                                                                                                          x.MeasurementDate.Date = dateDocument.Value.Date).Value))
            INDTxtExchange.Text = $"{_currencyAbbreviation} - TRM: {textTRM}"
            Return True
        End Using
    End Function

    ''' <summary>
    ''' oculta o muestra el control de tasa de cambio
    ''' </summary>
    ''' <param name="Value"></param>
    Private Sub ShowOrHideExchangeControl(Value As Boolean)
        INDLciExchange.Visibility = If(Not Value, DevExpress.XtraLayout.Utils.LayoutVisibility.Never, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
        INDTxtExchange.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Mete los valores del diccionario de datos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDictionary(account As AccountPayableXpo)
        Dim listShares As List(Of AccountPayableSharesXpo) = Presenter.GetListAccountPayableSharesById(account.Id)
        If Not _dictionaryAccountPayable.ContainsKey(account.BillNumber) Then
            _dictionaryAccountPayable.Add(account.BillNumber, listShares)
        Else
            _dictionaryAccountPayable(account.BillNumber) = listShares
        End If
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
    ''' Mensajes que se le muestran al usuario
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControlsPoppup As Boolean
        Set(value As Boolean)
            INDtxtCrossValue.Enabled = value
            INDbtnAddBill.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControlsConsultBill As Boolean
        Set(value As Boolean)
            INDsleAdvance.Properties.ReadOnly = value
            INDsleSupplier.Properties.ReadOnly = value
            INDsleTransferType.Properties.ReadOnly = value
        End Set
    End Property

    ''' <summary>
    ''' Crea la entidad de detalle del traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function CreatePaymentTransferDetail() As Task(Of PaymentTransferDetail)
        Dim paymentTransferDetail = New PaymentTransferDetail

        Dim valueInCurrency As Decimal = 0
        Dim TRM As Decimal? = 0

        If Me.CurrencyId <> Me.accountPayableShareXpo.IdAccountPayable?.CurrencyId Then

            If Not _listTRM?.Any(Function(x) x.CurrencyId = Me.accountPayableShareXpo.IdAccountPayable?.CurrencyId AndAlso x.OfficialCurrencyId = Me.CurrencyId AndAlso x.MeasurementDate = Me.DocumentDate.Value.Date) Then

                Using model As New MTransfers(Me.Tag.ToString())
                    Dim TRMInvoice = Await model.GetTRMbyCurrencyId(Me.accountPayableShareXpo.IdAccountPayable?.CurrencyId, Me.CurrencyId, Me.DocumentDate.Value.Date)
                    If TRMInvoice.StateResult Then
                        _listTRM.Add(TRMInvoice.ObjectEmbbeded)
                    End If
                End Using

            End If

            TRM = _listTRM?.Where(Function(w) w.CurrencyId = Me.accountPayableShareXpo.IdAccountPayable?.CurrencyId AndAlso w.OfficialCurrencyId = Me.CurrencyId AndAlso w.MeasurementDate = Me.DocumentDate.Value.Date)?.FirstOrDefault()?.Value

            If TRM Is Nothing OrElse TRM = 0 Then
                Return Nothing
            End If

            valueInCurrency = Math.Round(Me.CrossValueShare / TRM.Value, Me._decimals)
        Else
            valueInCurrency = Me.CrossValueShare
        End If
        With paymentTransferDetail
            .NumberBill = accountPayableShareXpo.NumberInvoice
            .NumberNameMainAccount = accountPayableShareXpo.IdAccountPayable.IdAccount.NumberName
            .BalanceBill = accountPayableShareXpo.IdAccountPayable.Balance
            .Share = accountPayableShareXpo.Share
            .BalanceShare = accountPayableShareXpo.Balance
            .Value = Me.CrossValueShare
            .TRMValue = TRM
            .CurrencyAbbreviationInvoice = accountPayableShareXpo?.IdAccountPayable?.Abbreviation
            .ValueInCurrencyInvoice = valueInCurrency
            .SupplierId = accountPayableShareXpo.IdAccountPayable.IdSupplier.Id
            .SupplierDescription = accountPayableShareXpo.IdAccountPayable.IdSupplier.CodeName
            If accountPayableShareXpo.IdAccountPayable.IdThirdParty IsNot Nothing Then 'Si viene con tercero se le asigna
                .ThirdPartyId = accountPayableShareXpo.IdAccountPayable.IdThirdParty.Id
            Else 'Sino se envia nothing
                .ThirdPartyId = Nothing
            End If
            .AccountPayableId = accountPayableShareXpo.IdAccountPayable.Id
            .AccountPayableShareId = accountPayableShareXpo.Id
            .MainAccountId = accountPayableShareXpo.IdAccountPayable.IdAccount.Id
            If accountPayableShareXpo.IdAccountPayable.IdCostCenter = 0 Then
                .CostCenterId = Nothing
            Else
                .CostCenterId = accountPayableShareXpo.IdAccountPayable.IdCostCenter
            End If
        End With
        Return paymentTransferDetail
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        IdAccountPayableShare = Nothing
        BillDate = Nothing
        ExpiratedDateShare = Nothing
        ValueShare = 0
        BalanceShare = 0
        CrossValueShare = 0
        GetDate(False, True)
        ActionsOnControlsPoppup = False
        Me.CurrencyExchangeActions(False)
    End Sub

    ''' <summary>
    ''' Valida el valor total de los detalles con el saldo del anticipo
    ''' </summary>
    ''' <param name="mode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateValue(mode As Boolean) As Boolean
        If CrossValueShare > AdvanceBalance Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Agrega una factura a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AddBill() As Task
        If ValidateControlsPopupBill() Then
            If ListPaymentTranferDetail Is Nothing Then
                ListPaymentTranferDetail = New List(Of PaymentTransferDetail)
            Else
                Dim flag As Boolean = ListPaymentTranferDetail.Exists(Function(item) item.AccountPayableShareId = IdAccountPayableShare AndAlso item.Share = accountPayableShareXpo.Share)
                If flag Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ShareExist", NAME_MODULE)
                    INDsleBill.Focus()
                    Return
                End If
            End If
            Dim paymentTransferDetail = Await CreatePaymentTransferDetail()

            If paymentTransferDetail Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "No se logró añadir la factura"
                INDsleBill.Focus()
                Return
            End If

            ListPaymentTranferDetail.Add(paymentTransferDetail)
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ShareAgregateSatisfactory", NAME_MODULE)
            INDgcBills.DataSource = Nothing
            INDgcBills.DataSource = ListPaymentTranferDetail
            CleanControlsPopup()
            ActionsOnControlsConsultBill = True
            ctrAdvance.PrintAdvance()

            If paymentTransfer.Id > 0 Then
                paymentTransfer.MarkAsModified()
            End If
        End If
    End Function

    ''' <summary>
    ''' Valida que los controles del popup esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopupBill() As Boolean
        If IdAdvancePayments Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un anticipo."
            INDsleAdvance.Focus()
            Return False
        End If
        If Me.CrossValueShare = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ingrese un valor a cruzar de la cuota."
            INDsleAdvance.Focus()
            Return False
        End If
        If Me.CrossValueShare > Me.BalanceShare Then
            CrossValueShare = 0
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CrossValueExceededShareBalance", NAME_MODULE)
            INDtxtCrossValue.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function SetBills(result As List(Of PaymentTransferDetail)) As List(Of String)
        Dim listErrors As New List(Of String)

        If ListPaymentTranferDetail IsNot Nothing AndAlso ListPaymentTranferDetail.Count > 0 Then
            For Each item In result
                If ListPaymentTranferDetail.Any(Function(ptd) ptd.AccountPayableId = item.AccountPayableId) Then
                    listErrors.Add("La factura " & item.NumberBill & " ya esta agregada.")
                    Continue For
                End If

                ListPaymentTranferDetail.Add(item)
            Next
        Else
            ListPaymentTranferDetail = result
        End If

        INDgcBills.DataSource = Nothing
        INDgcBills.DataSource = ListPaymentTranferDetail

        ActionsOnControlsConsultBill = True
        ctrAdvance.PrintAdvance()

        Return listErrors
    End Function

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With paymentTransfer
            .Code = Consecutive
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .DocumentDate = DocumentDate
            .SupplierId = IdSupplier
            .ThirdPartyId = _idThirdParty
            .OperatingUnitId = BarraBotones.OperatingUnit.Id
            .TransferType = TransferType
            .AdvancePaymentId = IdAdvancePayments
            .MainAccountId = IdAccount
            If IdCostCenter IsNot Nothing AndAlso IdCostCenter > 0 Then
                .CostCenterId = IdCostCenter
            Else
                .CostCenterId = Nothing
            End If
            .Observations = Comments
            .Status = 1

            If ListDeletePaymentTransferDetail IsNot Nothing Then
                For Each item As PaymentTransferDetail In ListDeletePaymentTransferDetail
                    ListPaymentTranferDetail.Add(item)
                Next
                .MarkAsModified()
            End If
            If listPaymentsTranfersOtherConceptDelete IsNot Nothing Then
                For Each item In listPaymentsTranfersOtherConceptDelete
                    .PaymentTransferOtherConcept.Add(item)
                Next
                .MarkAsModified()
            End If
            If ListPaymentTranferDetail IsNot Nothing Then
                For Each item As PaymentTransferDetail In ListPaymentTranferDetail
                    .PaymentTransferDetail.Add(item)
                Next
            End If
            If listPaymentsTranfersOtherConcept IsNot Nothing Then
                For Each item In listPaymentsTranfersOtherConcept
                    .PaymentTransferOtherConcept.Add(item)
                Next
            End If
        End With
    End Sub

    ''' <summary>
    ''' carga por defecto la moneda oficial en los controles
    ''' </summary>
    Private Sub LoadDefaultCurrency()
        Me.CurrencyId = indigo.OfficialCurrencyId
        Me.CurrencyAbbreviation = indigo.CurrencyISO4217
        Me.setFormatGeneralControls(Me.CurrencyAbbreviation)
        Me.CurrencyExchangeActions(False)
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Consecutive) AndAlso Not String.IsNullOrWhiteSpace(Consecutive) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using model As New MTransfers(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await model.GetPaymentTransfer(INDBteCode.Text.Trim)
                    paymentTransfer = resultOperation.ObjectEmbbeded
                    INDLycRoot.BeginUpdate()
                    If paymentTransfer IsNot Nothing AndAlso paymentTransfer.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(paymentTransfer.Id))
                            With paymentTransfer
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                                Consecutive = .Code
                                DocumentDate = .DocumentDate
                                banSearch = True
                                IdSupplier = .SupplierId
                                _idThirdParty = .ThirdPartyId
                                INDsleSupplier.Properties.NullText = .CodeNameSupplier
                                IdAdvancePayments = .AdvancePaymentId

                                If .AdvancePayments?.CurrencyId Is Nothing Then
                                    Me.CurrencyId = indigo.OfficialCurrencyId
                                    Me.CurrencyAbbreviation = indigo.CurrencyISO4217
                                Else
                                    Me.CurrencyId = .AdvancePayments?.CurrencyId
                                    Me.CurrencyAbbreviation = .AdvancePayments?.Currency?.Abbreviation
                                End If

                                Me.setFormatGeneralControls(CurrencyAbbreviation)
                                INDsleAdvance.Properties.NullText = .CodeAdvancePayments
                                TransferType = .TransferType
                                InitializeBills()

                                IdAccount = .MainAccountId
                                INDsleAccount.Properties.NullText = .NumberNameAccount
                                INDsleAccount.EditValue = IdAccount
                                If .CostCenterId IsNot Nothing Then
                                    IdCostCenter = .CostCenterId
                                    INDsleCostCenter.Properties.NullText = .CodeNameCostCenter
                                End If
                                banSearch = False
                                Comments = .Observations

                                ListPaymentTranferDetail = .PaymentTransferDetail.ToList
                                INDgcBills.DataSource = Nothing
                                INDgcBills.DataSource = ListPaymentTranferDetail

                                listPaymentsTranfersOtherConcept = .PaymentTransferOtherConcept.ToList()
                                INDgcOtherConcept.DataSource = listPaymentsTranfersOtherConcept

                                'Se obtiene el anticipo para sacar el saldo
                                Dim advancePaymentsXpoTemp = Presenter.GetAdvancePaymentsById(.AdvancePaymentId)
                                If advancePaymentsXpoTemp IsNot Nothing Then
                                    _advanceValue = advancePaymentsXpoTemp.Balance
                                End If

                                ctrAdvance.PrintAdvance()

                                BarraBotones.StatusRecord = .Status.ToString

                                If .Status = 1 Then
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                                Else
                                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                End If
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.paymentTransfer.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = paymentTransfer.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(paymentTransfer.Id, Me.Tag.ToString(), Nothing, GetType(PaymentTransfer).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True

                            If ListPaymentTranferDetail IsNot Nothing Then
                                ActionsOnControlsConsultBill = True
                            End If
                            If paymentTransfer.Status <> 1 Then
                                ReadOnlyControls(True)
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewTransfer()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Consecutive = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDLycRoot.EndUpdate()
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    Me.BarraBotones.PrintReport(PrintReportAction.None, paymentTransfer.Id, 0, paymentTransfer.Id)
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetDate(ByVal mode As Boolean, ByVal modePopup As Boolean)
        Using model As New MAccountPayable(CStr(Tag))
            dateServerVariable = Await model.GetServerDate()
            If mode Then
                INDdteDocumentDate.EditValue = dateServerVariable
            End If
            If modePopup Then
                INDdteBillDate.EditValue = dateServerVariable
                INDdteExpirationDate.EditValue = dateServerVariable
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Valida el valor de la cuota de la rejilla de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateShare(sender As Object)
        Dim ptd As PaymentTransferDetail = CType(viewGridBill.GetFocusedRow, PaymentTransferDetail)
        Dim crossValueTmp = DirectCast(sender, DevExpress.XtraEditors.TextEdit)
        If crossValueTmp.EditValue <= ptd.BalanceShare Then
            ptd.Value = crossValueTmp.EditValue
            If ValidateValue(False) = False Then
                ptd.Value = _oldValueGrid
                INDgcBills.RefreshDataSource()
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValueExceededAdvancePayments", NAME_MODULE)
                Exit Sub
            End If
            If ptd.Id > 0 Then
                paymentTransfer.MarkAsModified()
            End If
        Else
            ptd.Value = _oldValueGrid
            INDgcBills.RefreshDataSource()
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CrossValueExceededShareBalance", NAME_MODULE)
        End If
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        Dim ptd As PaymentTransferDetail = CType(viewGridBill.GetFocusedRow, PaymentTransferDetail)
        If ptd.Id <> 0 Then
            If ListDeletePaymentTransferDetail Is Nothing Then
                ListDeletePaymentTransferDetail = New List(Of PaymentTransferDetail)
            End If
            ptd.MarkAsDeleted()
            ListDeletePaymentTransferDetail.Add(ptd)
        End If
        ListPaymentTranferDetail.Remove(ptd)
        INDgcBills.DataSource = Nothing
        INDgcBills.DataSource = ListPaymentTranferDetail
        ctrAdvance.PrintAdvance()
        If ListPaymentTranferDetail.Count = 0 Then
            ActionsOnControlsConsultBill = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el detalle de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetailOtherConcept()
        Dim otherConcept As PaymentTransferOtherConcept = CType(INDGvOtherConcept.GetFocusedRow, PaymentTransferOtherConcept)
        If otherConcept.Id <> 0 Then
            If listPaymentsTranfersOtherConceptDelete Is Nothing Then
                listPaymentsTranfersOtherConceptDelete = New List(Of PaymentTransferOtherConcept)
            End If
            otherConcept.MarkAsDeleted()
            listPaymentsTranfersOtherConceptDelete.Add(otherConcept)
        End If

        listPaymentsTranfersOtherConcept.Remove(otherConcept)
        INDgcOtherConcept.DataSource = Nothing
        INDgcOtherConcept.DataSource = listPaymentsTranfersOtherConcept
        ctrAdvance.PrintAdvance()
    End Sub

    ''' <summary>
    ''' Genera los mensajes de error.
    ''' </summary>
    ''' <param name="errors"></param>
    ''' <remarks></remarks>
    Private Sub generateListError(errors As String)
        If errors = "_Seq01_" Then
            errors = "No tiene secuencia numérica parametrizada."
        ElseIf errors = "_Seq02_" Then
            errors = "La secuencia numérica ya excedió el limite permitido."
        End If
        Dim listError As New StringBuilder()
        listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
        listError.AppendLine(errors)
        Mensaje(EeventViewerImages.Advertencia) = listError.ToString()
    End Sub

#End Region

#Region "ICrud"

    ''' <summary>
    ''' Metodo que abre el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If

        Dim ListItems As New List(Of Tuple(Of String, Integer))
        ListItems.Add(New Tuple(Of String, Integer)("Registrado", 1))
        ListItems.Add(New Tuple(Of String, Integer)("Confirmado", 2))
        ListItems.Add(New Tuple(Of String, Integer)("Anulado", 3))

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Proveedor", .FieldName = "SupplierId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "AdvancePaymentId.CurrencyAbbreviation", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Nit", .FieldName = "SupplierId.IdThirdParty.Nit", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "Status", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1), .ColumnEdit = True, .ListItemsDatasourceColumEdit = ListItems}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPaymentTransfer
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        Try
            If paymentTransfer IsNot Nothing AndAlso paymentTransfer.Id > -1 Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MTransfers(Me.Tag.ToString())
                        AsyncLoader(True)
                        'paymentTransfer.MarkAsDeleted()
                        Dim result = Await Model.DeletePaymentTransferAsync(paymentTransfer)
                        If result.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                            End If
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If paymentTransfer.Status <> 3 Then
            'Se valida que el saldo no sea inferior a cero
            If AdvanceBalance < 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El saldo no puede ser menor a cero."
                Exit Sub
            End If
            If ValidateControls() = False Then
                Exit Sub
            End If
            Dim validateSharesErrors = ValidateShareds()
            If validateSharesErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = validateSharesErrors
                Exit Sub
            End If
            If listPaymentsTranfersOtherConcept IsNot Nothing AndAlso listPaymentsTranfersOtherConcept.Count > 0 AndAlso ListPaymentTranferDetail IsNot Nothing AndAlso ListPaymentTranferDetail.Count > 0 Then
                Dim debit = listPaymentsTranfersOtherConcept.Where(Function(z) z.Nature = 1).Sum(Function(x) x.Value)
                Dim credit = listPaymentsTranfersOtherConcept.Where(Function(z) z.Nature = 2).Sum(Function(x) x.Value)
                Dim totalValueBills = ListPaymentTranferDetail.Sum(Function(x) x.Value)
                Dim debitCreditResult = debit - credit
                If debitCreditResult < 0 Then
                    debitCreditResult *= -1
                End If
                If debitCreditResult > totalValueBills Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede guardar porque el valor de otros conceptos es mayor al total de las facturas"
                    Exit Sub
                End If
            End If
            AssigningValues()
        End If
        Try
            Using model As New MTransfers(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SavePaymentTransferAsync(paymentTransfer, _idCurrentSequence)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    codeDocumentIndexed = Result.ObjectEmbbeded.Code
                    If paymentTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf paymentTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If paymentTransfer.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.paymentTransfer = Result.ObjectEmbbeded
                    'Ajuste para que el reporte salga con las acciones de guardar y actualizar
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, Result.ObjectEmbbeded.Id, 0, Result.ObjectEmbbeded.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, Result.ObjectEmbbeded.Id, 0, Result.ObjectEmbbeded.Id)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, Result.ObjectEmbbeded.Id, 0, Result.ObjectEmbbeded.Id)
                    End Select
                    '--------------
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If Result.MessageResult IsNot Nothing Then
                        generateListError(Result.MessageResult(0))
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
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
            Await NewTransfer()
        End If
    End Sub

    ''' <summary>
    ''' Confirma el traslado
    ''' </summary>
    ''' <exception cref="System.NotImplementedException"></exception>
    Private Async Sub Confirmar()
        'Se valida que el saldo no sea inferior a cero
        If AdvanceBalance < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El saldo no puede ser menor a cero."
            Exit Sub
        End If
        If ValidateControls() = False Then
            Exit Sub
        End If
        Dim validateSharesErrors = ValidateShareds()
        If validateSharesErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = validateSharesErrors
            Exit Sub
        End If
        If listPaymentsTranfersOtherConcept IsNot Nothing AndAlso listPaymentsTranfersOtherConcept.Count > 0 AndAlso ListPaymentTranferDetail IsNot Nothing AndAlso ListPaymentTranferDetail.Count > 0 Then
            Dim debit = listPaymentsTranfersOtherConcept.Where(Function(z) z.Nature = 1).Sum(Function(x) x.Value)
            Dim credit = listPaymentsTranfersOtherConcept.Where(Function(z) z.Nature = 2).Sum(Function(x) x.Value)
            Dim totalValueBills = ListPaymentTranferDetail.Sum(Function(x) x.Value)
            Dim debitCreditResult = debit - credit
            If debitCreditResult < 0 Then
                debitCreditResult *= -1
            End If
            If debitCreditResult > totalValueBills Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede guardar porque el valor de otros conceptos es mayor al total de las facturas"
                Exit Sub
            End If
        End If
        AssigningValues()
        Try
            Using Model As New MTransfers(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await Model.SaveAndConfirmTransfer(paymentTransfer, _idCurrentSequence)
                If Result.StateResult Then
                    codeDocumentIndexed = Result.ObjectEmbbeded.Code
                    If paymentTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    ElseIf paymentTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    End If
                    Me.paymentTransfer = Result.ObjectEmbbeded
                    If Result.StateResultAux Then
                        Dim stringBuilder = New StringBuilder

                        'Consulto el tipo de comprobante
                        Dim nameTypeVoucher As String = ""
                        Dim idType As Integer
                        Dim parameters As ActionResult(Of SettingPayments)
                        Dim typeJournal As ActionResult(Of JournalVoucherTypes)
                        Using modelParameters As New MParameters("")
                            parameters = Await modelParameters.GetSettingPaymentsByIdOperatingUnit(BarraBotones.OperatingUnitValue)
                        End Using
                        idType = parameters.ObjectEmbbeded.IdJournalVoucherTranslation
                        Using modelType As New MDocumentType("")
                            typeJournal = modelType.GetJournalVoucherById(idType)
                        End Using
                        '------------------------------------
                        stringBuilder.AppendLine(String.Format(ResourceManager.GetString("DocumentSaveWithConsecutive", NAME_MODULE), Result.ObjectEmbbeded.Code, typeJournal.ObjectEmbbeded.Name, Result.MessageResult(0)))
                        If Result.MessageResult?.Count > 1 Then
                            stringBuilder.AppendLine(Result.MessageResult(1))
                        End If
                        Mensaje(EeventViewerImages.Informacion) = stringBuilder.ToString()
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, Result.ObjectEmbbeded.Id, 0, Result.ObjectEmbbeded.Id)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format("Se guardo el Cruce de Anticipo vs CxP con código {0} pero no se confirmo por {1}", Result.ObjectEmbbeded.Code, vbNewLine + Result.MessageResult(0))
                    End If
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    If Result.MessageResult IsNot Nothing Then
                        generateListError(Result.MessageResult(0))
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Barra botones: Confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirmar()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBteCode.ButtonClick
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
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de la barra botones de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barra botones: Click Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        paymentTransfer.Status = 3
        varImp = 4
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, paymentTransfer.Id, 0, paymentTransfer.Id)
    End Sub

#End Region

    Private Function ValidatePopupOtherConcept() As ActionResult
        Dim errors As New StringBuilder
        If AccountPayableConceptNoteId Is Nothing Then
            errors.AppendLine("Concepto de Nota vacio")
        End If
        If MainAccountOtherConceptId Is Nothing Then
            errors.AppendLine("Cuenta Contable vacia")
        End If
        If INDlyItemThirdPartyOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ThirdPartyOtherConceptId Is Nothing Then
                errors.AppendLine("Tercero Vacio")
            End If
        End If
        If INDlyItemCostCenterOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If CostCenterOtherConceptId Is Nothing Then
                errors.AppendLine("Centro de Costo Vacio")
            End If
        End If
        If INDsleNatureOtherConcept.EditValue Is Nothing Then
            errors.AppendLine("Naturaleza Vacia")
        End If
        If ValueOtherConcept = 0 Then
            errors.AppendLine("Valor Vacio")
        End If
        If errors.Length = 0 Then
            Return New ActionResult With {.StateResult = True}
        Else
            Return New ActionResult With {.StateResult = False, .Message = errors.ToString()}
        End If
    End Function

    Private Sub CleanControlsOtherConcept()
        INDsleAccountPayableConceptNote.EditValue = Nothing
        INDsleMainAccountOtherConcept.EditValue = Nothing
        INDsleMainAccountOtherConcept.Properties.ReadOnly = True
        INDsleMainAccountOtherConcept.Properties.NullText = String.Empty
        INDsleThirdPartyOtherConcept.EditValue = Nothing
        INDlyItemThirdPartyOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleCostCenterOtherConcept.EditValue = Nothing
        INDlyItemCostCenterOtherConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleNatureOtherConcept.EditValue = Nothing
        ValueOtherConcept = 0
    End Sub

    Private Sub INDbtnAddOtherConcept_Click(sender As Object, e As EventArgs) Handles INDbtnAddOtherConcept.Click
        Dim resultValidatePopup = ValidatePopupOtherConcept()
        If resultValidatePopup.StateResult = False Then
            Mensaje(EeventViewerImages.Advertencia) = resultValidatePopup.Message
            Exit Sub
        End If

        Dim otherConcept As New PaymentTransferOtherConcept
        With otherConcept
            .AccountPayableConceptNoteId = AccountPayableConceptNoteId
            .CodeNameConceptNote = INDsleAccountPayableConceptNote.Text
            .MainAccountId = MainAccountOtherConceptId
            .NumberNameMainAccount = INDsleMainAccountOtherConcept.Text
            .ThirdPartyId = ThirdPartyOtherConceptId
            .CostCenterId = CostCenterOtherConceptId
            .Nature = INDsleNatureOtherConcept.EditValue
            .Value = ValueOtherConcept
        End With
        If listPaymentsTranfersOtherConcept Is Nothing Then
            listPaymentsTranfersOtherConcept = New List(Of PaymentTransferOtherConcept)
        End If
        listPaymentsTranfersOtherConcept.Add(otherConcept)
        INDgcOtherConcept.DataSource = listPaymentsTranfersOtherConcept
        INDgcOtherConcept.RefreshDataSource()

        ctrAdvance.PrintAdvance()
        CleanControlsOtherConcept()
        RezizablePopup()
        INDsleAccountPayableConceptNote.Focus()
    End Sub

    Dim listPaymentsTranfersOtherConcept As List(Of PaymentTransferOtherConcept)

    ''' <summary>
    ''' Listado de eliminados de los detalles de otros conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPaymentsTranfersOtherConceptDelete As List(Of PaymentTransferOtherConcept)

    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            DeleteDetailOtherConcept()
        End If
    End Sub

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If paymentTransfer.Id > 0 AndAlso paymentTransfer.Status > 1 Then
            Exit Sub
        End If

        Try
            If _idThirdParty = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un proveedor."
                Exit Sub
            End If

            If IdAdvancePayments Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un anticipo."
                Exit Sub
            End If

            If TransferType = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar tipo de traslado."
                Exit Sub
            End If

            AsyncLoader(True)

            Dim listErrors As New List(Of String)

            If sender.Name = INDgcBills.Name Then
                Using model As New MTransfers(MyTag)
                    Dim result = Await model.ImportBillsToPaymentTransfer(e.Rows, New List(Of Object) From {TransferType, IdSupplier, Me.CurrencyId})

                    AsyncLoader(False)
                    listErrors = result.MessageResult
                    If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                        result.MessageResult.AddRange(SetBills(result.ObjectEmbbeded))
                    End If
                End Using
            End If

            If listErrors.Count > 0 Then
                Using formulario As New FrmListErrors(listErrors)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

#End Region

End Class