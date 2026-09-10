#Region "Imports"

Imports Presentation.Treasury.MVP
Imports Presentation.Base
Imports Presentation.Common
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Accounting
Imports System.Text
Imports DevExpress.Xpo
Imports Presentation.Accounting.MVP
Imports Presentation.Payroll
Imports Presentation.Common.MVP
Imports Domain.Entities.Service
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Portfolio.MVP
Imports Presentation.Payments.MVP
Imports Presentation.Billing.MVP
Imports System.Drawing
Imports Presentation.Treasury
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports DevExpress.XtraSplashScreen
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Domain.Billing.POCO
#End Region

Public Class FrmCashReceivableLiquidation
    Implements ICashReceiptsLiquidation

#Region "Properties and Variables"

    Private _entityName As String = NameOf(Invoice)

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordTreasury
    ''' <summary>
    ''' debitos
    ''' </summary>
    Dim debit As Decimal
    ''' <summary>
    ''' creditos
    ''' </summary>
    Dim credit As Decimal

    ''' <summary>
    ''' bandera para saber si el metodo de pago es tarjeta
    ''' </summary>
    ''' <remarks></remarks>
    Private paymentMehtodIsTarget As Boolean

    ''' <summary>
    ''' bandera para saber si se esta agregando o editando en los metodos de pago y asi limpiar o no los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private EditModePaymentMethod As Boolean

    ''' <summary>
    ''' Valor por defecto a crear del método de pago
    ''' </summary>
    ''' <value>
    ''' The cash default value.
    ''' </value>
    Property CashDefaultValue As Decimal

    ''' <summary>
    ''' Moneda que venga de la factura
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyInvoiceId As Integer?

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Treasury"

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' variable para saber que metodo de pago se esta ejecutando y asi controlar si en el metodo de consignacion se muestra o no la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Dim paymentMethodType As Byte

    ''' <summary>
    ''' Listado que contiene los metodos de pago
    ''' </summary>
    Dim listPaymentMethods As List(Of PaymentMethods)

    ''' <summary>
    ''' Listado que contiene los metodos de pago para eliminar
    ''' </summary>
    Dim listPaymentMethodsDelete As List(Of PaymentMethods)

    ''' <summary>
    ''' entidad de metodos de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim paymentMethods As PaymentMethods

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' entidad de recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim cashReceipts As CashReceipts

    ''' <summary>
    ''' The presenter
    ''' </summary>
    Dim presenter As PCashReceiptsLiquidation

    ''' <summary>
    ''' entidad del detalle del recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim cashReceiptdetails As CashReceiptDetails

    ''' <summary>
    ''' listado detalle del recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim listCashReceiptdetails As List(Of CashReceiptDetails)
    ''' <summary>
    ''' listado detalle del recibo de caja para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listCashReceiptdetailsDelete As List(Of CashReceiptDetails)

    ''' <summary>
    ''' Lista las tazas
    ''' </summary>
    Private _listTRM As List(Of TRM)

    ''' <summary>
    ''' Numero de admision que se pasa desde la cabecera del folio
    ''' </summary>
    Property AdmissionNumber As String

    ''' <summary>
    ''' Identificación del paciente para trazabilidad en observaciones del anticipo
    ''' </summary>
    Property PatientIdentification As String

    ''' <summary>
    ''' Nombre del paciente para trazabilidad en observaciones del anticipo
    ''' </summary>
    Property PatientName As String

    ''' <summary>
    ''' Id del ultimo recibo de caja que fue confirmado
    ''' </summary>
    Private cashReceivableIdLast As Integer

    ''' <summary>
    ''' Tipo del folio que abre el frontal
    ''' </summary>
    Property FolioType As Byte

    ''' <summary>
    ''' Tipo del liquidacion que abre el frontal
    ''' </summary>
    Property LiquidationType As Byte

    ''' <summary>
    ''' Id del folio que abre el frontal si es desde liquidacion
    ''' </summary>
    Property RevenueControlDetailId As Integer

    ''' <summary>
    ''' Id del Centro de Costo que abre el frontal
    ''' </summary>
    Property CostCenterId As Integer

    ''' <summary>
    ''' decimales para la funcion de redondeo
    ''' </summary>
    Private _decimals As Integer = 2

    ''' <summary>
    ''' control superior en la barra botones que muestra el valor del folio en diferentes metodos de pagos
    ''' parametrizado en iva devuelto
    ''' </summary>
    Private _ctrTaxDevolution As CtrTaxDevolution

    ''' <summary>
    ''' bandera para saber el Documento Origen:
    ''' 1. Liquidación
    ''' 2. Factura de Productos
    ''' 3. Facturación Básica
    ''' </summary>
    ''' <remarks></remarks>
    Private _sourceDocument As eSourceDocument

    ''' <summary>
    ''' Id del folio del cual se calculará la devolucion del IVA
    ''' </summary>
    Private _reveneueControlDetailId As Integer?

    ''' <summary>
    ''' entidad que almacena la lista de iva devuelto en diferentes metodos de pago
    ''' </summary>
    Private _tupleTaxDevolution As Tuple(Of List(Of TaxDevolution), Integer)

    ''' <summary>
    ''' abbreviacion de la moneda
    ''' </summary>
    Private _currencyAbbreviation As String

    ''' <summary>
    ''' secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As TreasurySequence Implements ICashReceiptsLiquidation.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As TreasurySequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As TreasurySequenceDetail In Me._sequense.TreasurySequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
            NewCashReceipt()
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establce el Código del recibo
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Public Property Code As String Implements ICashReceiptsLiquidation.Code
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
    ''' Id de la caja
    ''' </summary>
    ''' <returns></returns>
    Public Property IdCashRegister As Integer? Implements ICashReceiptsLiquidation.IdCashRegister
        Get
            Return INDsleCash.EditValue
        End Get
        Set(value As Integer?)
            INDsleCash.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del centro de costo
    ''' </summary>
    ''' <returns></returns>
    Public Property IdCostCenter As Integer? Implements ICashReceiptsLiquidation.IdCostCenter
        Get
            Return INDSleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDSleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Moneda de la caja
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer Implements ICashReceiptsLiquidation.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDSleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Public Property IdMainAccount As Integer? Implements ICashReceiptsLiquidation.IdMainAccount

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICashReceiptsLiquidation.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDbteCode.Enabled = Not value

            INDPceMethodPayment.Enabled = value
            INDGcMethodPayment.Enabled = value
            INDsleCash.Enabled = value
            INDSleThirdParty.Enabled = value
            INDSleCostCenter.Enabled = value
            INDGleTypeFundRaising.Enabled = value
            INDSleThirdParty.Properties.ReadOnly = True

            If INDbteCode.Enabled = True Then
                INDbteCode.Focus()
            Else
                INDsleCash.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property IdThirdParty As Integer Implements ICashReceiptsLiquidation.IdThirdParty
        Get
            Return INDSleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDSleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICashReceiptsLiquidation.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements ICashReceiptsLiquidation.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' lista los tipos de caja 
    ''' </summary>
    Dim listTypeCollection As List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' obtiene o establece el listado de tipo caja
    ''' </summary>
    Public ReadOnly Property TypeCash As List(Of Tuple(Of Byte, String)) Implements ICashReceiptsLiquidation.TypeCash
        Get
            If listTypeCollection Is Nothing Then
                listTypeCollection = New List(Of Tuple(Of Byte, String))
                listTypeCollection.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("ExpenseTypeCash", MODULE_NAME)))
                listTypeCollection.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("ExpenseTypeBankAccount", MODULE_NAME)))
            End If
            Return listTypeCollection
        End Get
    End Property

    ''' <summary>
    ''' obtiene o establece las cajas
    ''' </summary>
    ''' <value>
    ''' The cash xpo.
    ''' </value>
    Public Property CashXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ICashReceiptsLiquidation.CashXPO
        Get
            Return CType(INDsleCash.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleCash.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece las cajas
    ''' </summary>
    ''' <value>
    ''' The cash xpo.
    ''' </value>
    Public Property BankAccountXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ICashReceiptsLiquidation.BankAccountXPO
        Get
            Return CType(INDsleBankAccount.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center xpo.
    ''' </value>
    Public Property CostCenterXPO As DevExpress.Xpo.XPInstantFeedbackSource Implements ICashReceiptsLiquidation.CostCenterXPO
        Get
            Return CType(INDSleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the third party xpo.
    ''' </summary>
    Public Property ThirdPartyXPO As XPInstantFeedbackSource Implements ICashReceiptsLiquidation.ThirdPartyXPO
        Get
            Return CType(INDSleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece el tipo de recaudo
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeFundRaising As Byte Implements ICashReceiptsLiquidation.TypeFundRaising
        Get
            Return CByte(INDGleTypeFundRaising.EditValue)
        End Get
        Set(value As Byte)
            INDGleTypeFundRaising.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id cuenta bancaria
    ''' </summary>
    Public Property IdBankAccount As Integer? Implements ICashReceiptsLiquidation.IdBankAccount
        Get
            Return INDsleBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' DataSource Tipos de recaudo
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TypeFundRaisingDataSource As List(Of Tuple(Of Byte, String))
        Get
            Dim _typeFundRaisingDataSource = New List(Of Tuple(Of Byte, String))
            _typeFundRaisingDataSource.Add(New Tuple(Of Byte, String)(1, "Caja"))
            _typeFundRaisingDataSource.Add(New Tuple(Of Byte, String)(2, "Cuenta Bancaria"))
            Return _typeFundRaisingDataSource
        End Get
    End Property

    Public Event ReturnCashReceivableId(cashReceivableId As Integer)

    ''' <summary>
    ''' Establece el Documento Origen:
    ''' 1. Liquidación
    ''' 2. Factura de Productos
    ''' 3. Facturación Básica
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property SourceDocument As eSourceDocument
        Set(value As eSourceDocument)
            _sourceDocument = value
        End Set
    End Property

    ''' <summary>
    ''' valor total de la factura de producto para hacer el recibo de caja por este 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _valueProductInvoice As Decimal

    ''' <summary>
    ''' Establece el valor de la factura de producto
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property ValueProductInvoice As Decimal
        Set(value As Decimal)
            _valueProductInvoice = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para obtener el recibo de caja que despues se guardara desde factura de producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property CashReceiptProductInvoice As CashReceipts
        Get
            Return cashReceipts
        End Get
    End Property

    ''' <summary>
    ''' abbreviacion de la moneda
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property CurrencyAbbreviation As String
        Get
            Return _currencyAbbreviation
        End Get
    End Property

    ''' <summary>
    ''' establece el id de la cuenta bancaria para que no permita ser modificada.
    ''' </summary>
    ''' <param name="NameCode"></param>
    Private WriteOnly Property _setBankAccountInControl(Optional NameCode As String = Nothing) As Integer
        Set(value As Integer)
            If CtrPopupPaymentMethod1 IsNot Nothing Then
                CtrPopupPaymentMethod1.INDSleBankAccount.EditValue = value
                CtrPopupPaymentMethod1.INDSleBankAccount.Properties.NullText = NameCode
                CtrPopupPaymentMethod1.INDSleBankAccount.ReadOnly = Not String.IsNullOrEmpty(NameCode)
            End If
        End Set
    End Property

#End Region

#Region "Events"

#Region "Builder"
    Public Sub New(Optional revenueControlDetailId As Integer? = Nothing)

        ' This call is required by the designer.
        InitializeComponent()

        If revenueControlDetailId IsNot Nothing Then
            _reveneueControlDetailId = revenueControlDetailId
            _ctrTaxDevolution = New CtrTaxDevolution()
            _ctrTaxDevolution.SetDataSourceTaxDevolution(AddressOf GetCtrTaxDevolutionInfo)
            _ctrTaxDevolution.LoadDataSourceTaxDevolution()
            _ctrTaxDevolution.Dock = DockStyle.Fill
            AdditionalControlPanel.Parent.MinimumSize = New System.Drawing.Size(250, AdditionalControlPanel.Height)
            AdditionalControlPanel.Parent.MaximumSize = New System.Drawing.Size(250, AdditionalControlPanel.Height)
            AdditionalControlPanel.MinimumSize = New System.Drawing.Size(250, AdditionalControlPanel.Height)
            AdditionalControlPanel.MaximumSize = New System.Drawing.Size(250, AdditionalControlPanel.Height)
            AdditionalControlPanel.Controls.Add(_ctrTaxDevolution)
        End If

    End Sub

#End Region

#Region "Load"
    ''' <summary>
    ''' Handles the Load event of the FrmCashReceivableLiquidation control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmCashReceivableLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        IndigoGridControl1.RefreshGrid(INDGcMethodPayment)
        AddActionsColumns()
        presenter = New PCashReceiptsLiquidation(Me)
        presenter.LoadDefinitionLayout()
        presenter.InitializeThirdPartyXPO()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        CtrPopupPaymentMethod1.OperatingUnitId = _idOperativeUnit
        Deshacer()
        Await presenter.GetSequense()
        LoadStatus()
        Me.INDGleTypeFundRaising.Properties.DataSource = Me.TypeFundRaisingDataSource


        If _sourceDocument = eSourceDocument.ProductSales Then
            INDbteCode.Properties.Buttons(0).Visible = False
            CtrPopupPaymentMethod1.HandleProductInvoice = True
        End If

        Await LoadFirstCash()
        If CashDefaultValue > 0 Then
            SetPaymentMethodDatasource()
            InitializePaymentMethod()
        End If
        _searchMode = False
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' evento que se dispara al hacer click en el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCash_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCash.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCash
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                presenter.InitializeCashXPO()
                INDsleCash.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCostCenter
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                presenter.InitializeCostCenterXPO()
                INDSleCostCenter.Focus()
            End Using
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' evento que se dispara al cambiar el valor de la caja y consulta si la cuenta de la caja requiere centro de costo para ponerlo visible o invisible
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDSleCash_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCash.EditValueChanged
        If INDsleCash.EditValue IsNot Nothing Then
            Using model As New MCashRegister(MyTag)
                AsyncLoader(True)
                Dim cash = Await model.GetcashRegisterById(IdCashRegister)
                If cash?.Id > 0 Then
                    If Me._sequense.Scope.Equals("O") Then
                        Me._idCurrentSequense = Me.GetIdSequenceByPrefix(cash.Prefix)
                        Me._prefixSelected = cash.Prefix
                    End If
                    Using modelPUC As New MPUC(MyTag)
                        Dim mainAccountId = If(_sourceDocument = eSourceDocument.BasicBilling, IdMainAccount, cash.IdMainAccount)
                        Dim account = Await modelPUC.GetAccountById(mainAccountId, False)
                        IdMainAccount = account.Id
                        If account.HandlesCostCenter = False Then
                            INDliCostCenter.HideControl(True)
                        Else
                            If CostCenterXPO Is Nothing Then
                                presenter.InitializeCostCenterXPO()
                            End If
                            IdCostCenter = cash.IdCostCenter
                            INDliCostCenter.HideControl(False)
                        End If
                    End Using
                    CurrencyId = cash.CurrencyId
                    setFormatsControls(cash.CurrencyName)
                    If listPaymentMethods?.Any Then
                        Dim GridCurrencyId = Me.listPaymentMethods?.FirstOrDefault?.CurrencyId
                        Await Me.loadListTRM(Me.CurrencyId, GridCurrencyId)

                        If _listTRM Is Nothing OrElse Not Me._listTRM?.Any(Function(a) a.CurrencyId = Me.CurrencyId _
                                                                           AndAlso a.OfficialCurrencyId = GridCurrencyId _
                                                                           AndAlso a.MeasurementDate = GetDateServer().Date) Then
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        Dim trmValue = Me._listTRM.FirstOrDefault(Function(a) a.CurrencyId = Me.CurrencyId _
                                                                           AndAlso a.OfficialCurrencyId = GridCurrencyId _
                                                                           AndAlso a.MeasurementDate = GetDateServer().Date)?.Value
                        For Each item In Me.listPaymentMethods
                            item.Value = Math.Round(CDec(item.Value / trmValue), Me._decimals)
                            item.CurrencyId = CurrencyId
                            item.ValueInCurrencyHeader = item.Value
                        Next
                        INDGcMethodPayment.RefreshDataSource()
                    End If
                    'Seteo el formato de los controles de metodos de pago
                    CtrPopupPaymentMethod1.CurrencyId(cash.CurrencyName) = If(cash.CurrencyId Is Nothing, indigo.OfficialCurrencyId, cash.CurrencyId)
                End If
                AsyncLoader(False)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambiar el valor de la cuenta bancaria y consulta si la cuenta de la cuenta requiere centro de costo para ponerlo visible o invisible
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBankAccount.EditValueChanged
        If INDsleBankAccount.EditValue Is Nothing Then
            Me._setBankAccountInControl = Nothing
            Exit Sub
        End If
        Using model As New MCashRegister(MyTag)
            AsyncLoader(True)
            Dim bankAccount = Await model.GetEntityBankAccountById(IdBankAccount)

            If bankAccount Is Nothing OrElse bankAccount?.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Error al consultar la cuenta bancaria"
                Exit Sub
            End If

            If Me._sequense?.Scope?.Equals("O") Then
                Me._idCurrentSequense = Me.GetIdSequenceByPrefix(bankAccount.Prefix)
                Me._prefixSelected = bankAccount.Prefix
            End If

            Using modelPUC As New MPUC(MyTag)
                Dim mainAccountId = If(_sourceDocument = eSourceDocument.BasicBilling, IdMainAccount, bankAccount.IdMainAccount)
                Dim account = Await modelPUC.GetAccountById(mainAccountId, False)
                IdMainAccount = account?.Id

                If account?.HandlesCostCenter = False Then
                    INDliCostCenter.HideControl(True)
                Else
                    If CostCenterXPO Is Nothing Then
                        presenter.InitializeCostCenterXPO()
                    End If
                    IdCostCenter = bankAccount?.IdCostCenter
                    INDliCostCenter.HideControl(False)
                End If
            End Using

            CurrencyId = If(bankAccount?.CurrencyId Is Nothing, Me.indigo.OfficialCurrencyId, bankAccount?.CurrencyId)
            setFormatsControls(bankAccount?.CurrencyAbbreviation)

            If listPaymentMethods?.Any Then
                Dim GridCurrencyId = Me.listPaymentMethods?.FirstOrDefault?.CurrencyId
                Await Me.loadListTRM(Me.CurrencyId, GridCurrencyId)

                If _listTRM Is Nothing OrElse Not Me._listTRM?.Any(Function(a) a.CurrencyId = Me.CurrencyId _
                                                    AndAlso a.OfficialCurrencyId = GridCurrencyId _
                                                    AndAlso a.MeasurementDate = GetDateServer().Date) Then
                    AsyncLoader(False)
                    Exit Sub
                End If

                Dim trmValue = Me._listTRM.FirstOrDefault(Function(a) a.CurrencyId = Me.CurrencyId _
                                                                       AndAlso a.OfficialCurrencyId = GridCurrencyId _
                                                                       AndAlso a.MeasurementDate = GetDateServer().Date)?.Value
                For Each item In Me.listPaymentMethods
                    item.Value = Math.Round(CDec(item.Value / trmValue), Me._decimals)
                    item.ValueInCurrencyHeader = item.Value
                    item.CurrencyId = CurrencyId
                Next
                INDGcMethodPayment.RefreshDataSource()
            End If
            'Seteo el formato de los controles de metodos de pago
            CtrPopupPaymentMethod1.CurrencyId(bankAccount?.CurrencyAbbreviation) = Me.CurrencyId
            Me._setBankAccountInControl($"{bankAccount?.Code} - {bankAccount?.Bank?.Name}") = IdBankAccount
            AsyncLoader(False)
        End Using
    End Sub

    ''' <summary>
    ''' evento que se ejecuta al cambiar el tipo de recaudo para mostra si es caja o cuenta bancaria depnediendo la seleccion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTypeFundRaising_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeFundRaising.EditValueChanged
        Me.IdCashRegister = Nothing
        Me.IdBankAccount = Nothing
        Me.INDsleCash.Properties.NullText = String.Empty
        Me.INDsleBankAccount.Properties.NullText = String.Empty
        Me._setBankAccountInControl = Nothing
        INDliCash.HideControl(INDGleTypeFundRaising.EditValue Is Nothing OrElse Me.TypeFundRaising <> eTypeFundRaising.Cashregister)
        INDLciBankAccount.HideControl(INDGleTypeFundRaising.EditValue Is Nothing OrElse Me.TypeFundRaising <> eTypeFundRaising.BankAccount)
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Properties.DataSource = If(Me.TypeFundRaising = eTypeFundRaising.Cashregister, CtrPopupPaymentMethod1.MethodsPaymentsCash, CtrPopupPaymentMethod1.MethodsPaymentsBank)
        If listPaymentMethodsDelete Is Nothing Then
            listPaymentMethodsDelete = New List(Of PaymentMethods)
        End If
        If listPaymentMethods IsNot Nothing Then
            Parallel.ForEach(listPaymentMethods, Sub(item As PaymentMethods)
                                                     If item.Id > 0 Then
                                                         listPaymentMethodsDelete.Add(item)
                                                     End If
                                                 End Sub)
            listPaymentMethods.RemoveAll(Function(x) True)

            INDGcMethodPayment.DataSource = Nothing
            INDGcMethodPayment.DataSource = listPaymentMethods
            INDGcMethodPayment.RefreshDataSource()
        End If
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' evento que se dipara al presionar click en el popup y asigna el tag del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDPceMethodPayment_Click(sender As Object, e As EventArgs) Handles INDPceMethodPayment.Click
        CtrPopupPaymentMethod1.listPaymentMethodsAdded = listPaymentMethods
        CtrPopupPaymentMethod1.TagForm = Me.MyTag
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
        CtrPopupPaymentMethod1.PreLoadValue = CDec(0)

        If listPaymentMethods Is Nothing OrElse Not listPaymentMethods?.Any() Then
            Await Me.loadListTRM(Me.CurrencyId, Me.CurrencyInvoiceId)
            If Not Me._listTRM?.Any(Function(a) a.CurrencyId = Me.CurrencyId _
                                                                               AndAlso a.OfficialCurrencyId = Me.CurrencyInvoiceId _
                                                                               AndAlso a.MeasurementDate = GetDateServer().Date) Then
                Exit Sub
            End If
            Dim trmValue = Me._listTRM.FirstOrDefault(Function(a) a.CurrencyId = Me.CurrencyId _
                                                                               AndAlso a.OfficialCurrencyId = Me.CurrencyInvoiceId _
                                                                               AndAlso a.MeasurementDate = GetDateServer().Date)?.Value

            CtrPopupPaymentMethod1.PreLoadValue = Math.Round(CDbl(CashDefaultValue / trmValue), Me._decimals)
        End If
    End Sub

    ''' <summary>
    ''' evnto que instancia el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs)
        InstantiatePopup(Nothing, listCashReceiptdetails, Nothing)
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCash_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCash.QueryPopUp
        If CashXPO Is Nothing Then
            presenter.InitializeCashXPO()
        End If
    End Sub

    ''' <summary>
    ''' evento de consulta cuentas bancarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBankAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBankAccount.QueryPopUp
        If Me.BankAccountXPO Is Nothing Then
            presenter.InitializeBankAccountXPO()
        End If
    End Sub
#End Region

#Region "Actions"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        debit = Nothing
        credit = Nothing
        paymentMehtodIsTarget = Nothing
        EditModePaymentMethod = Nothing
        CashDefaultValue = Nothing
        _searchMode = Nothing
        paymentMethodType = Nothing
        listPaymentMethods = Nothing
        listPaymentMethodsDelete = Nothing
        paymentMethods = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _prefixSelected = Nothing
        _idOperativeUnit = Nothing
        cashReceipts = Nothing
        presenter = Nothing
        cashReceiptdetails = Nothing
        listCashReceiptdetails = Nothing
        listCashReceiptdetailsDelete = Nothing
        AdmissionNumber = Nothing
        cashReceivableIdLast = Nothing
        FolioType = Nothing
        LiquidationType = Nothing
        _sourceDocument = Nothing
        _valueProductInvoice = Nothing
        _tupleTaxDevolution = Nothing
    End Sub


    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case sender.Tag.ToString
            Case "Edit"
                paymentMethods = DirectCast(INDGvMethodPayment.GetFocusedRow, PaymentMethods)
                EditModePaymentMethod = True
                INDPceMethodPayment.Focus()
                CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
                CtrPopupPaymentMethod1.LoadControlsForEdit(paymentMethods)
                INDPceMethodPayment.ShowPopup()
            Case "Remove"
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    paymentMethods = DirectCast(INDGvMethodPayment.GetFocusedRow, PaymentMethods)
                    If paymentMethods.IdCard IsNot Nothing AndAlso listCashReceiptdetails?.Any() Then
                        listCashReceiptdetails.RemoveAll(Function(x) x.CardNumber = paymentMethods.CardNumber)
                    End If
                    If paymentMethods.Id > 0 Then
                        'paymentMethods.MarkAsDeleted()
                        If listPaymentMethodsDelete Is Nothing Then
                            listPaymentMethodsDelete = New List(Of PaymentMethods)
                        End If
                        listPaymentMethodsDelete.Add(paymentMethods)
                    End If
                    listPaymentMethods.Remove(paymentMethods)
                    INDGcMethodPayment.DataSource = Nothing
                    INDGcMethodPayment.DataSource = listPaymentMethods
                    paymentMethods = Nothing
                    If listPaymentMethods.Count = 0 Then
                        INDGvMethodPayment.OptionsView.ShowFooter = False
                        IndigoGridControl1.RefreshGrid(INDGcMethodPayment)
                        IndigoGridControl1.SetExportButton(INDGcMethodPayment, False)
                    Else
                        IndigoGridControl1.SetExportButton(INDGcMethodPayment, True)
                    End If
                End If
        End Select
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = Keys.Enter Then
            If String.IsNullOrEmpty(Code) Then
                NewCashReceipt()
            Else
                Await LoadControls()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDsleCash control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCash_KeyDown(sender As Object, e As KeyEventArgs) Handles INDsleCash.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDliCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleCostCenter.Focus()
            Else
                INDPceMethodPayment.Focus()
                INDPceMethodPayment.ShowPopup()
                CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDPceMethodPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDPceMethodPayment_KeyDown(sender As Object, e As KeyEventArgs) Handles INDPceMethodPayment.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceMethodPayment.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the FrmCashReceivableLiquidation control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCashReceivableLiquidation_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
#End Region

#Region "ClosePopUp"
    Private Sub INDPceMethodPayment_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceMethodPayment.CloseUp
        If EditModePaymentMethod = True Then
            paymentMethods = Nothing
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Enabled = True
            CtrPopupPaymentMethod1.INDSleCard.Properties.ReadOnly = False
            CtrPopupPaymentMethod1.INDSleCard.Properties.Buttons(0).Enabled = True
            CtrPopupPaymentMethod1.CleanControls()
            EditModePaymentMethod = False
        End If
        If CtrPopupPaymentMethod1.ClosePopupChangePaymentMethod = False Then
            If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
                IndigoGridControl1.SetExportButton(INDGcMethodPayment, True)
            End If
        End If
        CtrPopupPaymentMethod1.ClosePopupChangePaymentMethod = False
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            If listPaymentMethods IsNot Nothing Then
                IndigoGridControl1.ControlNextFocus = True
            End If
        End If
        If paymentMehtodIsTarget = True Then
            INDPceMethodPayment.Focus()
            INDPceMethodPayment.ShowPopup()
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
            paymentMehtodIsTarget = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the ClosePopup event of the CtrPopupPaymentMethod1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrPopupPaymentMethod1_ClosePopup(sender As Object, e As EventArgs) Handles CtrPopupPaymentMethod1.ClosePopup
        INDPceMethodPayment.ClosePopup()
        If listPaymentMethods IsNot Nothing Then
            IndigoGridControl1.ControlNextFocus = True
        End If
    End Sub
#End Region

#Region "OpenPoup"
    ''' <summary>
    ''' Handles the OpenPopup event of the CtrPopupPaymentMethod1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrPopupPaymentMethod1_OpenPopup(sender As Object, e As EventArgs) Handles CtrPopupPaymentMethod1.OpenPopup
        INDPceMethodPayment.ShowPopup()
    End Sub

    ''' <summary>
    ''' Handles the ResizePopup event of the CtrPopupPaymentMethod1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ResizePopupEventArgs"/> instance containing the event data.</param>
    Private Sub CtrPopupPaymentMethod1_ResizePopup(sender As Object, e As ResizePopupEventArgs) Handles CtrPopupPaymentMethod1.ResizePopup
        Select Case e.PaymentMethodType
            Case 1, 4
                SettingPopupPaymentMethods(e.PaymentMethodType, 320, 320, 540, 300, True, True)
            Case 2
                SettingPopupPaymentMethods(e.PaymentMethodType, 270, 270, 540, 550, True, True)
            Case 3
                SettingPopupPaymentMethods(e.PaymentMethodType, 420, 420, 840, 850, True, True)
            Case Else
                SettingPopupPaymentMethods(e.PaymentMethodType, 270, 270, 540, 300, True, True)
        End Select
        CtrPopupPaymentMethod1.INDPcButtons.Visible = True
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
    End Sub

    ''' <summary>
    ''' Handles the Popup event of the INDPceMethodPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDPceMethodPayment_Popup(sender As Object, e As EventArgs) Handles INDPceMethodPayment.Popup
        IndigoGridControl1.SetExportButton(INDGcMethodPayment, False)
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmCashReceivableLiquidation control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmCashReceivableLiquidation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not Me.BarraBotones.Enabled Then
            e.Cancel = True
            Exit Sub
        End If

        RaiseEvent ReturnCashReceivableId(cashReceivableIdLast)
    End Sub
#End Region

#End Region

#Region "Methods"
    Private Sub CtrPopupPaymentMethod1_AddPaymentMethods(sender As Object, e As AddPaymentMethodsEventArgs) Handles CtrPopupPaymentMethod1.AddPaymentMethods
        If listPaymentMethods Is Nothing Then
            listPaymentMethods = New List(Of PaymentMethods)

            INDGcMethodPayment.DataSource = Nothing
            INDGcMethodPayment.DataSource = listPaymentMethods
        End If

        If _sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling Then
            Dim valuePaymentsMethods As Decimal
            If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
                Dim previousValue = If(paymentMethods Is Nothing, 0, paymentMethods.Value)
                valuePaymentsMethods = listPaymentMethods.Sum(Function(x) x.Value) + e.PaymentMethods.Value - previousValue
            Else
                valuePaymentsMethods = e.PaymentMethods.Value
            End If

            If valuePaymentsMethods > _valueProductInvoice Then
                Mensaje(EeventViewerImages.Advertencia) = "El metodo de pago no se puede agregar porque supera el valor " + _valueProductInvoice.ToString("C2") + " de la factura"
                Exit Sub
            End If
        End If

        If paymentMethods IsNot Nothing Then
            listPaymentMethods.Remove(paymentMethods)
        End If
        If e.PaymentMethods.Id > 0 Then
            e.PaymentMethods.MarkAsModified()
        End If

        If e.PaymentMethods.IdCard IsNot Nothing Then
            paymentMehtodIsTarget = True
            If listCashReceiptdetails IsNot Nothing Then
                listCashReceiptdetails.RemoveAll(Function(x) x.CardNumber = e.PaymentMethods.CardNumber)
            End If
            AddCashReceiptDetailICA(e.PaymentMethods)
            AddCashReceiptDetailRTF(e.PaymentMethods)
            AddCashReceiptDetailCommision(e.PaymentMethods)
            If listCashReceiptdetails IsNot Nothing AndAlso listCashReceiptdetails.Count > 0 Then
                e.PaymentMethods.Value -= listCashReceiptdetails.FindAll(Function(x) x.CardNumber = e.PaymentMethods.CardNumber).Sum(Function(y) y.Value)
                e.PaymentMethods.ValueInCurrencyHeader = e.PaymentMethods.Value
            End If
        End If

        listPaymentMethods.Add(e.PaymentMethods)
        INDGcMethodPayment.DataSource = Nothing
        INDGcMethodPayment.DataSource = listPaymentMethods
        INDGvMethodPayment.OptionsView.ShowFooter = True
        If paymentMethods Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("PaymentMethodAdd", MODULE_NAME)
        Else
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("PaymentMethodEdit", MODULE_NAME)
            paymentMethods = Nothing
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Enabled = True
            INDPceMethodPayment.ClosePopup()
        End If
        INDPceMethodPayment.Focus()
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
    End Sub

    ''' <summary>
    ''' metodo para configurar como se vera el popup de metodos de pago
    ''' </summary>
    ''' <param name="paymentType"></param>
    ''' <param name="widthButtonAdd"></param>
    ''' <param name="widthButtonClose"></param>
    ''' <param name="widthPopup"></param>
    ''' <param name="heightPopup"></param>
    ''' <remarks></remarks>
    Private Sub SettingPopupPaymentMethods(paymentType As Byte, widthButtonAdd As Integer, widthButtonClose As Integer, widthPopup As Integer, heightPopup As Integer, buttonAddVisible As Boolean, buttonCloseVisible As Boolean)
        Dim size As System.Drawing.Size
        paymentMethodType = paymentType
        INDPceMethodPayment.Properties.PopupSizeable = True
        CtrPopupPaymentMethod1.INDBtnAdd.Width = widthButtonAdd
        CtrPopupPaymentMethod1.INDBtnClose.Width = widthButtonClose
        size.Width = widthPopup
        size.Height = heightPopup
        INDpccPaymentMethod.Size = size
        CtrPopupPaymentMethod1.INDBtnAdd.Visible = buttonAddVisible
        CtrPopupPaymentMethod1.INDBtnClose.Visible = buttonCloseVisible
        INDPceMethodPayment.Properties.PopupSizeable = False
        INDPceMethodPayment.ShowPopup()
    End Sub

    Private Sub setFormatsControls(Optional Abbreviation As String = Nothing)
        If String.IsNullOrEmpty(Abbreviation) Then
            Abbreviation = indigo.CurrencyISO4217
        End If

        Me._currencyAbbreviation = Abbreviation
        INDSleCurrency.Properties.NullText = $"{Abbreviation}"
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat

        Me.GridColumnValueAdvance = Window.Utils.FormatGrid(GridColumnValueAdvance, Abbreviation)
        Me.CurrencyConvertInListTaxDevolution(Me.CurrencyId)
    End Sub

    ''' <summary>
    ''' metodo para establecer los debitos y creditos del documento
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Private Function setDebitCredit() As Tuple(Of Decimal, Decimal)
        Return New Tuple(Of Decimal, Decimal)(debit, credit)
    End Function

    Private Function GenerateDoc() As IndexedDocument2
        Return Nothing
    End Function

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvMethodPayment, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvMethodPayment.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
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
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequense IsNot Nothing AndAlso Me._sequense.TreasurySequenceDetail IsNot Nothing AndAlso Me._sequense.TreasurySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequense.TreasurySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' metodo para agregar el registro al detalle del recibo de caja por el valor de lña retefuente
    ''' </summary>
    ''' <param name="paymentMethodAdd"></param>
    ''' <remarks></remarks>
    Private Sub AddCashReceiptDetailRTF(paymentMethodAdd As PaymentMethods)
        If paymentMethodAdd.RTFValue = 0 Then
            Exit Sub
        End If
        Using model As New MCards(MyTag)
            Dim card = model.GetCardById(paymentMethodAdd.IdCard)
            Using modelCashReceiptConcept As New MCashReceiptsConcepts(MyTag)
                Dim cashReceiptDetailRetention As CashReceiptDetails = New CashReceiptDetails
                Dim cashReceiptConceptRTF = modelCashReceiptConcept.GetCashReceiptConceptById(card.IdCashReceiptConceptRTF)
                cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptRTF.Code + " - " + cashReceiptConceptRTF.Name
                cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
                cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptRTF.IdMainAccount
                cashReceiptDetailRetention.Nature = 1
                cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptRTF.Id
                cashReceiptDetailRetention.Value = paymentMethodAdd.RTFValue
                cashReceiptDetailRetention.ValueInCurrencyHeader = paymentMethodAdd.RTFValue
                cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptRTF
                cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageRTF
                cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
                cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
                cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptRTF.Affectation
                AddCashReceiptConcept(cashReceiptDetailRetention)
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' metodo para agregar el registro al detalle del recibo de caja por el valor de la comision
    ''' </summary>
    ''' <param name="paymentMethodAdd"></param>
    ''' <remarks></remarks>
    Private Sub AddCashReceiptDetailCommision(paymentMethodAdd As PaymentMethods)
        If paymentMethodAdd.CommissionValue = 0 Then
            Exit Sub
        End If
        Using model As New MCards(MyTag)
            Dim card = model.GetCardById(paymentMethodAdd.IdCard)
            Using modelCashReceiptConcept As New MCashReceiptsConcepts(MyTag)
                Dim cashReceiptDetailRetention As CashReceiptDetails = New CashReceiptDetails
                Dim cashReceiptConceptCommision = modelCashReceiptConcept.GetCashReceiptConceptById(card.IdCashReceiptConceptCommision)
                cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptCommision.Code + " - " + cashReceiptConceptCommision.Name
                cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
                cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptCommision.IdMainAccount
                cashReceiptDetailRetention.Nature = 1
                cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptCommision.Id
                cashReceiptDetailRetention.Value = paymentMethodAdd.CommissionValue
                cashReceiptDetailRetention.ValueInCurrencyHeader = paymentMethodAdd.CommissionValue
                cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptCommision
                cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageCommission
                cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
                cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
                cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptCommision.Affectation
                AddCashReceiptConcept(cashReceiptDetailRetention)
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' metodo para agregar el registro al detalle del recibo de caja por el valor del ica
    ''' </summary>
    ''' <param name="paymentMethodAdd"></param>
    ''' <remarks></remarks>
    Private Sub AddCashReceiptDetailICA(paymentMethodAdd As PaymentMethods)
        If paymentMethodAdd.ICAValue = 0 Then
            Exit Sub
        End If
        Using model As New MCards(MyTag)
            Dim card = model.GetCardById(paymentMethodAdd.IdCard)
            Using modelCashReceiptConcept As New MCashReceiptsConcepts(MyTag)
                Dim cashReceiptDetailRetention As CashReceiptDetails = New CashReceiptDetails
                Dim cashReceiptConceptICA = modelCashReceiptConcept.GetCashReceiptConceptById(card.IdCashReceiptConceptICA)
                cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptICA.Code + " - " + cashReceiptConceptICA.Name
                cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
                cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptICA.IdMainAccount
                cashReceiptDetailRetention.Nature = 1
                cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptICA.Id
                cashReceiptDetailRetention.Value = paymentMethodAdd.ICAValue
                cashReceiptDetailRetention.ValueInCurrencyHeader = paymentMethodAdd.ICAValue
                cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptICA
                cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageICA
                cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
                cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
                cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptICA.Affectation
                AddCashReceiptConcept(cashReceiptDetailRetention)
            End Using
        End Using
    End Sub

    ''' <summary>
    ''' Sets the payment method datasource.
    ''' </summary>
    Private Sub SetPaymentMethodDatasource()
        If listPaymentMethods IsNot Nothing Then
            If listPaymentMethodsDelete Is Nothing Then
                listPaymentMethodsDelete = New List(Of PaymentMethods)
            End If
            Dim listPaymentsMethodsTmp = listPaymentMethods.FindAll(Function(x) x.PaymentMethodTypes = 4)
            Parallel.ForEach(listPaymentsMethodsTmp, Sub(item As PaymentMethods)
                                                         If item.Id > 0 Then
                                                             listPaymentMethodsDelete.Add(item)
                                                         End If
                                                     End Sub)
            Parallel.ForEach(listPaymentsMethodsTmp, Sub(item As PaymentMethods)
                                                         listPaymentMethods.Remove(item)
                                                     End Sub)
            INDGcMethodPayment.DataSource = Nothing
            INDGcMethodPayment.DataSource = listPaymentMethods
        End If

        If paymentMethodType = 4 Then
            CtrPopupPaymentMethod1.EditedValueFromTheForm = True
            CtrPopupPaymentMethod1.INDGlePaymentMethod.EditValue = Nothing
            CtrPopupPaymentMethod1.INDGlePaymentMethod.EditValue = 1
            INDPceMethodPayment.Properties.PopupSizeable = True
            CtrPopupPaymentMethod1.INDBtnAdd.Width = 185
            CtrPopupPaymentMethod1.INDBtnClose.Width = 185
            INDpccPaymentMethod.Size = New Size(384, 200)
            CtrPopupPaymentMethod1.INDBtnAdd.Visible = True
            CtrPopupPaymentMethod1.INDBtnClose.Visible = True
            INDPceMethodPayment.Properties.PopupSizeable = False
        End If

        INDliCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliCostCenter.AllowHide = True
        INDSleCostCenter.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' metodo para limpiar los controles
    ''' </summary>
    Private Sub CleanControls()
        ReadOnlyControls(False)
        BarraBotones.CleanAuditBasic()
        ActionsOnControls = False
        'DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        INDPceMethodPayment.ClosePopup()
        CtrPopupPaymentMethod1.CleanControls()
        INDbteCode.Text = String.Empty
        INDMeDetail.Text = String.Empty
        INDsleCash.Properties.NullText = String.Empty
        INDsleCash.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = String.Empty
        INDSleCostCenter.EditValue = Nothing
        INDGcMethodPayment.DataSource = Nothing
        listPaymentMethods = Nothing
        listPaymentMethodsDelete = Nothing
        INDGvMethodPayment.OptionsView.ShowFooter = False
        IndigoGridControl1.RefreshGrid(INDGcMethodPayment)
        cashReceipts = Nothing
        listCashReceiptdetails = Nothing
        listCashReceiptdetailsDelete = Nothing
        INDsleCash.Properties.NullText = String.Empty
        Me.IdCashRegister = Nothing
        Me.IdBankAccount = Nothing
        Me.TypeFundRaising = 1
        Me._currencyAbbreviation = Me.indigo?.CurrencyISO4217
        INDliCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
    End Sub

    ''' <summary>
    ''' metodo para marcar todos los agregados del detalle como elimados
    ''' </summary>
    ''' <param name="_cashReceiptDetail"></param>
    ''' <remarks></remarks>
    Private Sub MarkAsDeleteCashReceiptDetail(_cashReceiptDetail As CashReceiptDetails)
        If _cashReceiptDetail.CashReceiptConceptAffectation = 2 Then
            If _cashReceiptDetail.CashReceiptAccountReceivable.Count > 0 Then
                While _cashReceiptDetail.CashReceiptAccountReceivable.Count > 0
                    _cashReceiptDetail.CashReceiptAccountReceivable.Item(0).MarkAsDeleted()
                End While
            End If
            If cashReceipts.PortfolioAdvance.Count > 0 Then
                cashReceipts.PortfolioAdvance.Item(0).MarkAsDeleted()
            End If
        End If
        If _cashReceiptDetail.CashReceiptConceptAffectation = 3 Then
            While _cashReceiptDetail.CashReceiptAdvancePayment.Count > 0
                _cashReceiptDetail.CashReceiptAdvancePayment.Item(0).MarkAsDeleted()
            End While
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub NewCashReceipt()
        Me.cashReceipts = New CashReceipts()
        If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
            Me._idCurrentSequense = 0
        ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
            If Me._sequense.TreasurySequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                Me._idCurrentSequense = Me._sequense.TreasurySequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                Exit Sub
            End If
        End If
        If Not Me._sequense.Sequential Then
            If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    If _sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling Then
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                    End If
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Else
                    Using model As New MCommonTreasury(CStr(Me.Tag))
                        Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                    End Using
                    If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        If _sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                        End If
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
                If _sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling Then
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
        Else
            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            If _sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling Then
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            End If
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
        If _sourceDocument = eSourceDocument.Liquidation Then
            'Validamos que tenga permisos de "guardar y confirmar" o "guardar", en el pop-up siemrpre se muestra guardar.
            If BarraBotones.PermissionsForm.ContainsKey(144) Then
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
            Else
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' obtener debitos y creditos
    ''' </summary>
    Private Sub getDebitCredit()
        debit = 0
        credit = 0
        If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
            debit += listPaymentMethods.Sum(Function(x) x.Value)
        End If

        If listCashReceiptdetails IsNot Nothing AndAlso listCashReceiptdetails.Count > 0 Then
            debit += listCashReceiptdetails.Where(Function(x) x.Nature = 1).Sum(Function(z) z.Value)
            credit += listCashReceiptdetails.Where(Function(x) x.Nature = 2).Sum(Function(z) z.Value)
        End If
    End Sub

    ''' <summary>
    ''' metodo para cargar los datos a los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using model As New MCashReceipts(MyTag)
            AsyncLoader(True)
            Dim result = Await model.GetCashRegisterByCode(Code)
            If result.StateResult = True AndAlso result.ObjectEmbbeded.Id > 0 Then
                cashReceipts = result.ObjectEmbbeded
                listPaymentMethods = model.GetPaymentMethodsByCashReceipts(cashReceipts.Id)
            Else
                cashReceipts = New CashReceipts
            End If
            AsyncLoader(False)
        End Using

        If cashReceipts.Id > 0 Then
            With cashReceipts
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                Me.BarraBotones.StatusRecordVisible = True
                Me.TypeFundRaising = .CollectType
                IdThirdParty = .IdThirdParty
                IdCashRegister = .IdCashRegister
                IdBankAccount = .IdBankAccount
                INDsleCash.Properties.NullText = .CodeNameCashRegister
                IdCostCenter = .IdCostCenter
                INDSleCostCenter.Properties.NullText = .CodeNameCostCenter
                INDGcMethodPayment.DataSource = Nothing
                INDGcMethodPayment.DataSource = listPaymentMethods
                If listPaymentMethods.Count > 0 Then
                    INDGvMethodPayment.OptionsView.ShowFooter = True
                Else
                    INDGvMethodPayment.OptionsView.ShowFooter = False
                End If
                BarraBotones.StatusRecord = .Status
                BarraBotones.StatusRecord = .Status.ToString()
                BarraBotones.OperatingUnitValue = .OperatingUnitId
            End With
            ActionsOnControls = True
            Select Case cashReceipts.Status
                Case 1
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                Case Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
            End Select
            BarraBotones.SetDocuments(cashReceipts.Id, MyTag, Nothing, GetType(CashReceipts).Name)
            INDsleCash.Focus()
            Me.GetDocumentIndexed(MyTag & "_" & cashReceipts.Code)
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmCashReceipts_DontExists", MODULE_NAME)
            Code = String.Empty
            INDbteCode.Focus()
        End If
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False

        Me.BarraBotones.PrintReport(PrintReportAction.None, cashReceipts.Id, 0, cashReceipts.Id)

    End Function

    ''' <summary>
    ''' metodo para instanciar el formulario de concepto de recibo de caja
    ''' </summary>
    ''' <param name="_cashReceiptDetail"></param>
    ''' <remarks></remarks>
    Private Sub InstantiatePopup(_cashReceiptDetail As CashReceiptDetails, listConcept As List(Of CashReceiptDetails), _portfolioAdvance As PortfolioAdvance)
        Me.Cursor = ChangeCursorIndigo()
        Using formulario As New PopupCashReceiptConceptLiquidation(_cashReceiptDetail, listConcept, _portfolioAdvance)
            AddHandler formulario.AddCashReceiptConcept, AddressOf ReturnPopupCashReceiptConcept
            AddHandler formulario.CashReceiptsDetailsNull, AddressOf CashReceiptsDetailNull
            formulario.IdThirdParty = IdThirdParty
            formulario.AdmissionNumber = AdmissionNumber
            formulario.OperatingUnitId = _idOperativeUnit
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            'formulario.ViewModeEditHold = True
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Size = New Size(870, 700)
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Handles the Closed event of the INDPceMethodPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ClosedEventArgs"/> instance containing the event data.</param>
    Private Sub INDPceMethodPayment_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceMethodPayment.Closed
    End Sub

    ''' <summary>
    ''' metodo para poner la entidad del detalle en nothing desde el formulario PopupCashReceiptConcept
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CashReceiptsDetailNull(sender As Object, e As EventArgs)
        cashReceiptdetails = Nothing
    End Sub

    ''' <summary>
    ''' metodo para agregar un concepto de recibo de caja a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupCashReceiptConcept(sender As Object, e As Presentation.Billing.AddCashReceiptConceptEventArgs)
        AddCashReceiptConcept(e.CashReceiptDetails)
        If e.PortfolioAdvance IsNot Nothing Then
            If e.PortfolioAdvance.Id > 0 Then
                If e.PortfolioAdvance.Status = 0 Then
                    cashReceipts.PortfolioAdvance.Item(0).MarkAsDeleted()
                Else
                    cashReceipts.PortfolioAdvance.Item(0).Code = cashReceipts.Code
                    cashReceipts.PortfolioAdvance.Item(0).ModificationDate = Me.GetDateServer()
                End If
            Else
                If e.PortfolioAdvance.Status = 0 Then
                    cashReceipts.PortfolioAdvance.Clear()
                Else
                    e.PortfolioAdvance.CreationDate = Me.GetDateServer()
                    cashReceipts.PortfolioAdvance.Clear()
                    e.PortfolioAdvance.Code = cashReceipts.Code
                    e.PortfolioAdvance.CashReceiptDetails = e.CashReceiptDetails
                    cashReceipts.PortfolioAdvance.Add(e.PortfolioAdvance)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo para agregar o editar un detalle del recibo de caja
    ''' </summary>
    ''' <param name="cashReceiptDetailsAdd"></param>
    ''' <remarks></remarks>
    Private Sub AddCashReceiptConcept(cashReceiptDetailsAdd As CashReceiptDetails)
        If listCashReceiptdetails Is Nothing Then
            listCashReceiptdetails = New List(Of CashReceiptDetails)
        End If
        If cashReceiptdetails IsNot Nothing Then
            listCashReceiptdetails.Remove(cashReceiptdetails)
        End If
        If cashReceiptDetailsAdd.Id > 0 Then
            cashReceiptDetailsAdd.MarkAsModified()
        End If
        If cashReceiptDetailsAdd.CardNumber IsNot Nothing Then
            Dim detailTmp = listCashReceiptdetails.Find(Function(x) x.IdCashReceiptConcept = cashReceiptDetailsAdd.IdCashReceiptConcept)
            If detailTmp IsNot Nothing Then
                listCashReceiptdetails.Remove(detailTmp)
            End If
        End If
        listCashReceiptdetails.Add(cashReceiptDetailsAdd)
        If _sourceDocument = eSourceDocument.Liquidation Then
            If cashReceiptdetails Is Nothing Then
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("CashReceiptDetailAdd", MODULE_NAME)
            Else
                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("CashReceiptDetailEdit", MODULE_NAME)
            End If
        End If
        cashReceiptdetails = Nothing
    End Sub

    Dim settingBilling As SettingsBilling

    ''' <summary>
    ''' Loads the cash receipt detail.
    ''' </summary>
    Private Sub LoadCashReceiptDetail()
        cashReceiptdetails = New CashReceiptDetails()
        cashReceiptdetails.IdThirdParty = INDSleThirdParty.EditValue
        cashReceiptdetails.CurrencyId = Me.CurrencyId
        Dim cashReceiptConcept As CashReceiptConcepts = Nothing 'Consultar de los parametros el concepto
        Using model As New MCashReceiptsConcepts(Me.Tag)
            If _sourceDocument = eSourceDocument.Liquidation Then
                If FolioType = 3 Then ' Particulares
                    cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling.IndvidualAdvanceCashReceiptConceptId)
                Else
                    If LiquidationType = 1 Then
                        cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling.PatientAdvanceCashReceiptConceptId)
                    Else
                        cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling.CapitedPatientAdvanceCashReceiptConceptId)
                    End If
                End If
            ElseIf _sourceDocument = eSourceDocument.ProductSales Then
                cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling.ProductSalesCashReceiptConceptId)
            ElseIf _sourceDocument = eSourceDocument.BasicBilling Then
                cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling.BasicBillingCashReceiptConceptId)
            End If
        End Using

        If cashReceiptConcept IsNot Nothing Then
            If _sourceDocument = eSourceDocument.Liquidation Then
                cashReceiptdetails.IdMainAccount = cashReceiptConcept.IdMainAccount
                If LiquidationType = 1 Then
                    cashReceiptdetails.IdCostCenter = INDSleCostCenter.EditValue
                Else
                    cashReceiptdetails.IdCostCenter = CostCenterId
                End If
            ElseIf _sourceDocument = eSourceDocument.ProductSales Then
                cashReceiptdetails.IdMainAccount = settingBilling.ProductSalesMainAccountId
                cashReceiptdetails.IdCostCenter = settingBilling.ProductSalesCostCenterId
            ElseIf _sourceDocument = eSourceDocument.BasicBilling Then
                cashReceiptdetails.IdMainAccount = Me.IdMainAccount
                cashReceiptdetails.IdCostCenter = CostCenterId
            End If

            cashReceiptdetails.IdCashReceiptConcept = cashReceiptConcept.Id
            cashReceiptdetails.CashReceiptConceptAffectation = CByte(cashReceiptConcept.Affectation)
            cashReceiptdetails.Nature = cashReceiptConcept.Nature
        End If

        Dim PaymentValue As Decimal = 0
        Dim retention As Decimal = 0
        If listPaymentMethods?.Any() Then
            PaymentValue = listPaymentMethods.Sum(Function(x) x.Value)
        End If

        If listPaymentMethods?.Any(Function(s) s.IdCard IsNot Nothing AndAlso s.IdCard > 0 AndAlso (s.CommissionValue + s.RTFValue + s.ICAValue) > 0) Then
            retention = listPaymentMethods?.Where(Function(s) s.IdCard > 0)?.Sum(Function(x) x.CommissionValue + x.RTFValue + x.ICAValue)
        End If

        cashReceiptdetails.Value = PaymentValue + retention

        If listCashReceiptdetails?.Any(Function(s) s.Nature = 2) Then
            cashReceiptdetails.Value -= listCashReceiptdetails?.Where(Function(a) a.Nature = 2)?.Sum(Function(x) x.Value)
        End If

        Dim sumDebitCredit As Decimal = 0
        If listCashReceiptdetails?.Any() Then
            sumDebitCredit = listCashReceiptdetails.Sum(Function(x) x.Value * If(x.Nature = 2, 1, -1))
        End If
        'validacion para ajustar decimales 
        If (cashReceiptdetails.Value + sumDebitCredit - PaymentValue) <> 0 Then
            Dim adjustmentValue As Decimal = PaymentValue - (cashReceiptdetails.Value + sumDebitCredit)
            cashReceiptdetails.Value += adjustmentValue
        End If

        cashReceiptdetails.ValueInCurrencyHeader = cashReceiptdetails.Value
        AddCashReceiptConcept(cashReceiptdetails)

        If _sourceDocument = eSourceDocument.Liquidation Then
            Dim PortfolioAdvance = CreatePortfolioAdvance(cashReceiptConcept)
            PortfolioAdvance.CreationDate = Me.GetDateServer()
            PortfolioAdvance.Code = cashReceipts.Code
            PortfolioAdvance.CashReceiptDetails = listCashReceiptdetails.LastOrDefault
            cashReceipts.PortfolioAdvance.Add(PortfolioAdvance)
        End If
    End Sub

    ''' <summary>
    ''' Creates the portfolio advance.
    ''' </summary>
    ''' <returns></returns>
    Private Function CreatePortfolioAdvance(cashReceiptConcepts As CashReceiptConcepts) As PortfolioAdvance
        Dim portfolioAdvance = New PortfolioAdvance()
        portfolioAdvance.ThirdPartyId = IdThirdParty
        portfolioAdvance.MainAccountId = cashReceiptConcepts.IdMainAccount
        portfolioAdvance.CostCenterId = CostCenterId
        portfolioAdvance.CurrencyId = Me.CurrencyId
        Dim debitValue As Decimal = 0

        If listPaymentMethods?.Any() Then
            debitValue += listPaymentMethods.Sum(Function(x) x.Value)
        End If

        If listPaymentMethods?.Any(Function(s) s.IdCard IsNot Nothing AndAlso s.IdCard > 0 AndAlso (s.CommissionValue + s.RTFValue + s.ICAValue) > 0) Then
            debitValue += listPaymentMethods?.Where(Function(s) s.IdCard > 0)?.Sum(Function(x) x.CommissionValue + x.RTFValue + x.ICAValue)
        End If
        portfolioAdvance.Value = debitValue 'CDec(INDTxtPortfolioAdvance.EditValue)
        portfolioAdvance.AdmissionNumber = AdmissionNumber
        portfolioAdvance.Observations = $"Anticipo registrado desde liquidación para la atención No. {AdmissionNumber}, correspondiente al paciente {PatientIdentification} – {PatientName}."
        portfolioAdvance.DebitValue = 0
        portfolioAdvance.CreditValue = 0
        portfolioAdvance.Balance = debitValue
        portfolioAdvance.ValueInCurrencyHeader = debitValue
        portfolioAdvance.CreationUser = indigo.UserIndigo
        portfolioAdvance.Status = 1
        Return portfolioAdvance
    End Function

    ''' <summary>
    ''' Carga una lista global del TRM
    ''' </summary>
    ''' <param name="fromCurrency">Moneda de la transacción</param>
    ''' <param name="toCurrency">Moneda a convertir</param>
    Private Async Function loadListTRM(fromCurrency As Integer?, toCurrency As Integer?) As Task
        If fromCurrency Is Nothing AndAlso toCurrency Is Nothing Then
            Return
        End If
        Me._listTRM = If(Me._listTRM Is Nothing, New List(Of TRM), Me._listTRM)
        'Se busca que el TRM de la moneda a convertir no este en la lista
        If Not Me._listTRM?.Any(Function(a) a.CurrencyId = fromCurrency AndAlso a.OfficialCurrencyId = toCurrency AndAlso a.MeasurementDate = GetDateServer().Date) Then
            Using model As New MPortfolioTransfers(Me.Tag)
                Dim result = Await model.GetTRMbyCurrencyId(fromCurrency, toCurrency, Nothing, NameOf(Invoice))
                If Not result?.StateResult Then
                    ShowMessage(EeventViewerImages.Advertencia) = result?.Message
                    Exit Function
                End If
                Me._listTRM.Add(result?.ObjectEmbbeded)
            End Using
        End If
    End Function

    ''' <summary>
    ''' metodo para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        Dim third As Domain.Entities.ThirdParty = Nothing
        Using model As New MThirdParty(Me.Tag)
            third = model.GetThirdPartyByIdSimple(IdThirdParty)
        End Using
        LoadCashReceiptDetail()
        With cashReceipts
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .IdThirdParty = IdThirdParty
            .CollectType = Me.TypeFundRaising
            .IdMainAccount = IdMainAccount
            .IdCostCenter = IdCostCenter
            .Detail = INDMeDetail.Text
            .DocumentDate = GetDateServer()
            .EntityName = _entityName

            If .PortfolioAdvance.Count > 0 Then
                If .PortfolioAdvance.Item(0).Id = 0 Then
                    .PortfolioAdvance.Item(0).CreationDate = .DocumentDate
                    .PortfolioAdvance.Item(0).DocumentDate = .DocumentDate
                End If
            End If

            .IdCashRegister = IdCashRegister
            .IdBankAccount = IdBankAccount
            .Prefix = Me._prefixSelected
            .PaymentResponsibles = String.Concat(third.Nit, " - ", third.Name)

            Dim debitValue As Decimal = CDec(0)
            If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
                debitValue = listPaymentMethods.Sum(Function(x) x.Value)
            End If

            If listCashReceiptdetails IsNot Nothing AndAlso listCashReceiptdetails.Count > 0 Then
                debitValue += listCashReceiptdetails.Where(Function(x) x.Nature = 1).Sum(Function(z) z.Value)
                credit = listCashReceiptdetails.Where(Function(x) x.Nature = 2).Sum(Function(z) z.Value)
            End If

            .Value = debitValue
            .OperatingUnitId = _idOperativeUnit
            .Status = 1
            .CurrencyId = CurrencyId
            If listPaymentMethodsDelete IsNot Nothing AndAlso listPaymentMethodsDelete.Count > 0 Then
                Parallel.ForEach(listPaymentMethodsDelete, Sub(item As PaymentMethods)
                                                               .PaymentMethods.Add(item.MarkAsDeleted())
                                                           End Sub)
            End If
            If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
                Parallel.ForEach(listPaymentMethods, Sub(item)
                                                         .PaymentMethods.Add(item)
                                                     End Sub)
            End If

            If listCashReceiptdetailsDelete IsNot Nothing AndAlso listCashReceiptdetailsDelete.Count > 0 Then
                Parallel.ForEach(listCashReceiptdetailsDelete, Sub(item As CashReceiptDetails)
                                                                   .CashReceiptDetails.Add(item.MarkAsDeleted())
                                                               End Sub)
            End If
            If listCashReceiptdetails IsNot Nothing AndAlso listCashReceiptdetails.Count > 0 Then
                Parallel.ForEach(listCashReceiptdetails, Sub(item)
                                                             .CashReceiptDetails.Add(item)
                                                         End Sub)
            End If
        End With
        If cashReceipts.Id > 0 Then
            cashReceipts.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' funcion que devuelve la tupla con los datos a la funcion delegada
    ''' </summary>
    ''' <returns></returns>
    Private Function GetCtrTaxDevolutionInfo() As Tuple(Of List(Of TaxDevolution), String)
        If _reveneueControlDetailId Is Nothing OrElse _tupleTaxDevolution Is Nothing OrElse _tupleTaxDevolution?.Item2 Is Nothing Then
            Return Nothing
        End If
        Return New Tuple(Of List(Of TaxDevolution), String)(_tupleTaxDevolution?.Item1, _currencyAbbreviation)
    End Function

    ''' <summary>
    ''' consulta los datos de iva devuelto
    ''' </summary>
    ''' <returns></returns>
    Private Async Function TaxDevolutionDataSource(reveneueControlDetailId As Integer, Optional currencyId As Integer? = Nothing) As Task(Of Tuple(Of List(Of TaxDevolution), Integer))
        Using Model As New MCashRegister("")
            Dim result = Await Task.Factory.StartNew(Function() Model.GetTaxDevolutionByRevenueControlDetail(reveneueControlDetailId, CurrencyId:=Me.CurrencyInvoiceId, paymentCurrencyId:=currencyId))

            If result?.Any() Then
                currencyId = result.FirstOrDefault.CurrencyId
            ElseIf currencyId Is Nothing Then
                currencyId = Me.indigo.OfficialCurrencyId
            End If

            Return New Tuple(Of List(Of TaxDevolution), Integer)(result, currencyId)
        End Using
    End Function

    ''' <summary>
    ''' funcion que recibe el listado de iva devuelto por metodo de pago y lo convierte dependiendo de la moneda de la caja o banco
    ''' </summary>
    ''' <param name="currencyId">id de la moneda</param>
    ''' <returns></returns>
    Private Async Function CurrencyConvertInListTaxDevolution(currencyId As Integer?) As Task

        If currencyId Is Nothing Then
            currencyId = Me.indigo?.OfficialCurrencyId
        End If

        If _reveneueControlDetailId IsNot Nothing Then
            _tupleTaxDevolution = Await TaxDevolutionDataSource(_reveneueControlDetailId, currencyId)
            _ctrTaxDevolution.LoadDataSourceTaxDevolution()
        End If

        Return
    End Function

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
        If _searchMode = False Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            End If
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        'If cashReceipts.Id > 0 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        AssigningValuesDelete()
        '        Using model As New MCashReceipts(MyTag)
        '            AsyncLoader(True)
        '            Dim result = Await model.DeleteCashReceipts(cashReceipts)
        '            AsyncLoader(False)
        '            If result.StateResult = True Then
        '                Await Me.DeleteDocumentIndexed()
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                AsyncLoader(False)
        '                _searchMode = False
        '                Deshacer()
        '            Else
        '                If result.MessageResult(0) = "-999" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                    AsyncLoader(False)
        '                ElseIf result.MessageResult(0) = "-000" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                    AsyncLoader(False)
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                    AsyncLoader(False)
        '                End If
        '            End If

        '        End Using
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' Guardars this instance.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If cashReceipts IsNot Nothing AndAlso cashReceipts.Status < 3 Then
            AsyncLoader(True)
            If ValidateControls() = True Then
                Dim errors As New StringBuilder
                Using model As New MBillingSetting(Me.Tag)
                    settingBilling = Await model.GetSettingsBillingByIdUnitOperative(_idOperativeUnit, False)
                End Using
                If settingBilling Is Nothing OrElse settingBilling.Id = 0 Then
                    AsyncLoader(False)
                    errors.AppendLine("No se encontró un concepto definido en los parámetros de facturación")
                End If

                If (Me.TypeFundRaising = eTypeFundRaising.Cashregister AndAlso Me.IdCashRegister Is Nothing) OrElse (Me.TypeFundRaising = eTypeFundRaising.BankAccount AndAlso Me.IdBankAccount Is Nothing) Then
                    errors.AppendLine($"El método de recaudo es {If(Me.TypeFundRaising = eTypeFundRaising.Cashregister, "Caja", "Cuenta bancaria")} pero no ha selecionado ninguna")
                End If

                If INDGvMethodPayment.RowCount = 0 Then
                    AsyncLoader(False)
                    errors.AppendLine("Se debe agregar mínimo un metodo de pago")
                Else
                    If _sourceDocument = eSourceDocument.ProductSales Then
                        If listPaymentMethods.Sum(Function(x) x.Value) <> _valueProductInvoice Then
                            AsyncLoader(False)
                            errors.AppendLine("El recibo de caja se debe hacer por el mismo valor (" + _valueProductInvoice.ToString("C0") + ") de la factura")
                        End If
                    End If
                End If
                If errors.Length > 0 Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                    Exit Sub
                End If
            Else
                AsyncLoader(False)
                Exit Sub
            End If
            AssigningValues()
        End If

        If _sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling Then
            cashReceipts.Status = 2
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            AsyncLoader(False)
            Me.Close()
            Exit Sub
        End If

        Try
            Using model As New MCashReceipts(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveCashReceipts(cashReceipts, _idCurrentSequense, Me._sequense)
                If result.StateResult = True Then
                    cashReceipts = result.ObjectEmbbeded
                    Dim actionMessage As String = ""
                    If cashReceipts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Or cashReceipts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.TreasurySequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If result.Message Is Nothing Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), cashReceipts.Code)
                        Else
                            actionMessage = String.Format(ResourceManager.GetString("SavedWithCode"), cashReceipts.Code)
                            FailedMessage(result.Message, actionMessage)
                        End If
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        _searchMode = False
                        Me.Deshacer()
                    Else
                        If result.Message Is Nothing Then
                            If cashReceipts.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        Else
                            actionMessage = ResourceManager.GetString("UpdateMessage")
                            FailedMessage(result.Message, actionMessage)
                        End If
                    End If
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult(0) = "Periods" Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try

    End Sub

    ''' <summary>
    ''' metodo para crear el mensaje de error cuando se guarda un recibo de caja 
    ''' </summary>
    ''' <param name="errorMessage"></param>
    ''' <param name="actionMessage"></param>
    ''' <remarks></remarks>
    Private Sub FailedMessage(errorMessage As String, actionMessage As String)
        Dim message As New StringBuilder
        message.AppendLine(actionMessage)
        message.AppendLine(ResourceManager.GetString("NotConfirmedBecause", MODULE_NAME))
        message.AppendLine(errorMessage)
        Mensaje(EeventViewerImages.Advertencia) = message.ToString()
    End Sub

    ''' <summary>
    ''' confirmar el recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub Confirmar()
        Dim resultOption As DialogResult
        If cashReceipts.ChangeTracker.State = ObjectState.Unchanged Then
            resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo)
        Else
            resultOption = MessageIndigo.Show(ResourceManager.GetString("ConfirmMessageWithUpdate"), MessageType.Question, Me.Text, Botones.SiNo)
        End If
        If resultOption = System.Windows.Forms.DialogResult.Yes Then
            Try
                Using model As New MCashReceipts(MyTag)
                    AsyncLoader(True)
                    Dim result = Await model.ConfirmCashReceipts(cashReceipts.Id)
                    Await presenter.GetSequense()
                    If result.StateResult = True Then
                        Using modelSetting As New MSettingsTreasury(MyTag)
                            Dim setting = modelSetting.GetSettingsTreasuryByIdUnitOperativeSimple(BarraBotones.OperatingUnitValue)
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmSatisfactory"), cashReceipts.Code, setting.ObjectEmbbeded.FullNameJournaVoucherTypeCashReceipts, result.ObjectEmbbeded)
                            AsyncLoader(False)
                            _searchMode = False
                            Me.Deshacer()
                        End Using
                    Else
                        AsyncLoader(False)
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            Mensaje(EeventViewerImages.MensajeError) = result.MessageResult(0)
                        Else
                            Dim message As New StringBuilder
                            message.AppendLine(ResourceManager.GetString("NotConfirmedBy", MODULE_NAME))
                            message.AppendLine(result.ObjectEmbbeded)
                            Mensaje(EeventViewerImages.Advertencia) = message.ToString()
                        End If

                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        NewCashReceipt()
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MCommonTreasury(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        _searchMode = True
        DeleteBlockedRecord()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 100},
                              New ColumnInfo With {.Caption = "Tercero", .FieldName = "NitName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Cuenta", .FieldName = "NumberName", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Tipo de Recaudo", .FieldName = "CollectType", .ColumnWidth = 200},
                              New ColumnInfo With {.Caption = "Fecha del Documento", .FieldName = "DocumentDate", .ColumnWidth = 200},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = 200}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCashReceipts
            .FiltroBusqueda = BarraBotones.OperatingUnitValue.ToString()
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
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)
    ''' <summary>
    ''' Saves the and confirm.
    ''' </summary>
    Public Async Sub SaveAndConfirm()
        If ValidateControls() = True Then
            Dim errors As New StringBuilder
            If INDGvMethodPayment.RowCount = 0 Then
                errors.AppendLine("Se debe agregar mínimo un metodo de pago")
            End If

            Using model As New MBillingSetting(Me.Tag)
                AsyncLoader(True)
                settingBilling = Await model.GetSettingsBillingByIdUnitOperative(_idOperativeUnit, False)
                AsyncLoader(False)
            End Using
            If settingBilling Is Nothing OrElse settingBilling.Id = 0 Then
                errors.AppendLine("No se encontró un concepto definido en los parámetros de facturación")
            End If

            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        Dim cashReceiptId As Integer
        Try
            Me.CloseBox = False
            Using model As New MCashReceipts(MyTag)
                If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    AssigningValues()
                    cashReceipts.Status = 2
                    cashReceipts.RevenueControlDetailId = Me.RevenueControlDetailId
                    Dim result = Await model.SaveAndConfirmCashReceipts(cashReceipts, _idCurrentSequense, Me._sequense)

                    If waitForm.IsSplashFormVisible Then
                        waitForm.CloseWaitForm()
                    End If
                    waitForm.ShowWaitForm()

                    Await presenter.GetSequense()

                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        cashReceiptId = result.ObjectEmbbeded.Id
                        If result.ObjectEmbbeded.PortfolioAdvance IsNot Nothing AndAlso result.ObjectEmbbeded.PortfolioAdvance.Count > 0 Then
                            cashReceivableIdLast = result.ObjectEmbbeded.PortfolioAdvance.First().Id
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                        waitForm.CloseWaitForm()
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    _searchMode = False
                    Me.Deshacer()
                    Dim reportDef As New Presentation.Reporter.rptCashReceipt()
                    AddHandler reportDef.AfterPrint, Sub()
                                                         waitForm.CloseWaitForm()
                                                         Me.CloseBox = True
                                                     End Sub
                    ReportHelper.ExecuteReport(reportDef, Me, Me.BarraBotones.PermissionsForm, cashReceiptId, cashReceiptId)
                End If

            End Using

            AsyncLoader(False)
            Me.Close()
        Catch ex As Exception
            AsyncLoader(False)
            Me.CloseBox = True
            Throw ex
        End Try
    End Sub

#End Region

#Region "Bar Button"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
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
        BarraBotones.Focus()
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Confirmar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit

    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveAndConfirm()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            cashReceipts.Status = 3
            Guardar()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Initializes the payment method.
    ''' </summary>
    Private Sub InitializePaymentMethod()
        Dim paymentMethods As New PaymentMethods()
        paymentMethods.PaymentMethodTypes = Me.TypeFundRaising
        paymentMethods.Value = Math.Round(CashDefaultValue, Me._decimals)
        paymentMethods.ValueInCurrencyHeader = paymentMethods.Value
        paymentMethods.CurrencyId = Me.CurrencyInvoiceId
        Dim args As AddPaymentMethodsEventArgs = New AddPaymentMethodsEventArgs
        args.PaymentMethods = paymentMethods
        If CtrPopupPaymentMethod1.listPaymentMethodsAdded Is Nothing Then
            CtrPopupPaymentMethod1.listPaymentMethodsAdded = New List(Of PaymentMethods)
        End If
        CtrPopupPaymentMethod1.listPaymentMethodsAdded.Add(paymentMethods)
        CtrPopupPaymentMethod1_AddPaymentMethods(Nothing, args)
    End Sub

    Private Async Function LoadFirstCash() As Task
        Using model As New MCashRegister(MyTag)
            Dim cash As CashRegisters = Await model.GetFirstCash()
            If cash IsNot Nothing AndAlso cash.Id > 0 Then
                IdCashRegister = cash.Id
                CurrencyId = If(cash.CurrencyId Is Nothing, indigo.OfficialCurrencyId, cash.CurrencyId)
                INDsleCash.Properties.NullText = String.Concat(cash.Code, " - ", cash.Name)
            ElseIf _reveneueControlDetailId IsNot Nothing Then
                _tupleTaxDevolution = Await TaxDevolutionDataSource(_reveneueControlDetailId)
            End If
        End Using
    End Function
End Class

#Region "Enums"

''' <summary>
''' Enumeración Documento de Origen
''' </summary>
Public Enum eSourceDocument
    Liquidation = 1
    ProductSales = 2
    BasicBilling = 3
End Enum

Public Enum eTypeFundRaising
    Cashregister = 1
    BankAccount = 2
End Enum

#End Region