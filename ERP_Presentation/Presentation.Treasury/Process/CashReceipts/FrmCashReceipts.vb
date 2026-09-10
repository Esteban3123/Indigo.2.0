'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 16-06-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraLayout.Utils
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Billing.MVP
Imports Presentation.Common
Imports Presentation.Common.MVP
Imports Presentation.Controls
Imports Presentation.Payments.MVP
Imports Presentation.Portfolio.MVP
Imports Presentation.Treasury.MVP

#End Region

Public Class FrmCashReceipts
    Implements ICashReceipts, ICustomizableForm

#Region "BUILDER"
    Public Sub New()
        InitializeComponent()
        _ctrDebitCredit = New CtrDebitCredit()
        _ctrDebitCredit.SetDebitAndCredit(AddressOf setDebitCredit)
        _ctrDebitCredit.RefreshDebitCredit()
        _ctrDebitCredit.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(_ctrDebitCredit)
    End Sub
#End Region

#Region "GLOBALS"
    Public Event LoadControlsFinish(_CashReceipts As CashReceipts)

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
    ''' bandera para saber si se esta agregando o editando en los conceptos de recibos de caja y asi limpiar o no los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private EditModeCashRegisterDetail As Boolean
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Treasury"
    ''' <summary>
    ''' control de usuario para manejar los debitos y los creditos
    ''' </summary>
    Public _ctrDebitCredit As CtrDebitCredit
    ''' <summary>
    ''' debitos
    ''' </summary>
    Public Property debit As Decimal
    ''' <summary>
    ''' creditos
    ''' </summary>
    Public Property credit As Decimal
    ''' <summary>
    ''' presentador de recibo de caja
    ''' </summary>
    Dim presenter As PCashReceipts
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordTreasury
    ''' <summary>
    ''' lista los tipos de caja 
    ''' </summary>
    Dim listTypeCollection As List(Of Tuple(Of Byte, String))
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
    ''' entidad del detalle del recibo de caja
    ''' </summary>
    ''' <remarks></remarks>
    Dim cashReceiptdetails As CashReceiptDetails
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
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
    ''' listado de las facturas para mas informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listAccountReceivable As List(Of AccountReceivable)
    ''' <summary>
    ''' listado de los anticipos para mas informacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listCashReceiptAdvancePayment As List(Of CashReceiptAdvancePayment)

    Dim listCashReceiptDetailAccountPayable As List(Of CashReceiptDetailAccountPayable)
    ''' <summary>
    ''' bandera para saber si se esta cargando controles
    ''' </summary>
    ''' <remarks></remarks>
    Dim _flagLoadControls As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

    ''' <summary>
    ''' entidad de parametros de factura
    ''' </summary>
    Dim settingBilling As SettingsBilling

    ''' <summary>
    ''' Tipo del folio que abre el frontal
    ''' </summary>
    Property FolioType As Byte

    ''' <summary>
    ''' Tipo del liquidacion que abre el frontal
    ''' </summary>
    Property LiquidationType As Byte

    ''' <summary>
    ''' Numero de admision que se pasa desde la cabecera del folio
    ''' </summary>
    Property AdmissionNumber As String

    ''' <summary>
    ''' Diccionario que se encarga de almacenar las cajas que se han seleccionado
    ''' </summary>
    ''' <returns></returns>
    Property DictionaryCashRegisters As New Dictionary(Of Integer, CashRegisters)()

    ''' <summary>
    ''' Obtiene la Id de la caja anteriormente seleccionada.
    ''' </summary>
    ''' <returns></returns>
    Private Property OldCashRegisterId As Integer?

    ''' <summary>
    ''' Bandera que identifica si entro al message de advertencia del metodo CashValidate
    ''' </summary>
    Dim _isFromMessageCashValidate As Boolean = False

#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequence As TreasurySequence Implements ICashReceipts.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
            If subCashReceipts Then
                NewCashReceipt()
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad para establecer los tipos de recaudo
    ''' </summary>
    ''' <value>
    ''' The type collection.
    ''' </value>
    Public ReadOnly Property TypeCollection As List(Of Tuple(Of Byte, String)) Implements ICashReceipts.TypeCash
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
    ''' obtiene o establce el Código del recibo
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Public Property Code As String Implements ICashReceipts.Code
        Get
            If INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
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
    ''' Tipo de recaudo: 1 = Caja, 2 = Bancos
    ''' </summary>
    Public Property CollectType As Integer Implements ICashReceipts.CollectType
        Get
            Return INDGleTypeCollection.EditValue
        End Get
        Set(value As Integer)
            INDGleTypeCollection.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece la fecha del documento
    ''' </summary>
    ''' <value>
    ''' The date document.
    ''' </value>
    Public Property DocumentDate As Date? Implements ICashReceipts.DocumentDate
        Get
            Return INDDteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDDteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' detalle
    ''' </summary>
    Public Property Detail As String Implements ICashReceipts.Detail
        Get
            Return INDMeDetail.Text
        End Get
        Set(value As String)
            INDMeDetail.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Id cuenta bancaria
    ''' </summary>
    Public Property IdBankAccount As Integer? Implements ICashReceipts.IdBankAccount
        Get
            Return INDSleBankAccount.EditValue
        End Get
        Set(value As Integer?)
            INDSleBankAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id de la caja
    ''' </summary>
    Public Property IdCashRegister As Integer? Implements ICashReceipts.IdCashRegister
        Get
            Return INDSleCash.EditValue
        End Get
        Set(value As Integer?)
            INDSleCash.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del centro de costo
    ''' </summary>
    Public Property IdCostCenter As Integer? Implements ICashReceipts.IdCostCenter
        Get
            Return INDSleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDSleCostCenter.EditValue = value
        End Set
    End Property

    Private _idMainAccount As Integer
    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    Public Property IdMainAccount As Integer Implements ICashReceipts.IdMainAccount
        Get
            Return _idMainAccount
        End Get
        Set(value As Integer)
            _idMainAccount = value
        End Set
    End Property

    Private _idThirdPartyMainAccount As Integer?
    ''' <summary>
    ''' Id de la cuenta contable del tercero, para establecer la cuenta de los detalles
    ''' nota: por el momento solo se usa desde basic Billing
    ''' </summary>
    Public WriteOnly Property IdThirdPartyMainAccount As Integer?
        Set(value As Integer?)
            _idThirdPartyMainAccount = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    Public Property IdThirdParty As Integer Implements ICashReceipts.IdThirdParty
        Get
            Return INDSleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDSleThirdParty.EditValue = value
        End Set
    End Property

    Public Property IdResponsiblePayment As Integer? Implements ICashReceipts.IdResponsiblePayment
        Get
            If BarraBotones.PermiteGuardarResponsablePagoTercero Then
                Return INDSleReponsiblePayment.EditValue
            Else
                Return Nothing
            End If
        End Get
        Set(value As Integer?)
            If BarraBotones.PermiteGuardarResponsablePagoTercero Then
                INDSleReponsiblePayment.EditValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Id de la moneda(se toma de la caja o banco)
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer Implements ICashReceipts.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDSleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que define el tercero responsable del pago.
    ''' </summary>
    ''' <returns></returns>
    Public Property PaymentResponsibles As String Implements ICashReceipts.PaymentResponsibles
        Get
            If BarraBotones.PermiteGuardarResponsablePagoTercero Then
                Return INDSleReponsiblePayment.Text
            Else
                Return INDTxtResponsiblePayment.Text
            End If

        End Get
        Set(value As String)
            If BarraBotones.PermiteGuardarResponsablePagoTercero Then
                INDSleReponsiblePayment.Properties.NullText = Nothing
                INDSleReponsiblePayment.Properties.NullText = value
            Else
                INDTxtResponsiblePayment.Text = value
            End If
        End Set
    End Property

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

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [actions on controls]; otherwise, 
    ''' <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICashReceipts.ActionsOnControls
        Set(value As Boolean)
            INDLcCashReceipt.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDSleThirdParty.Enabled = value
            INDSleReponsiblePayment.Enabled = value
            INDTxtResponsiblePayment.Enabled = value
            INDMeDetail.Enabled = value
            INDDteDocumentDate.Enabled = value
            INDGleTypeCollection.Enabled = value
            INDPceMethodPayment.Enabled = value
            INDGcMethodPayment.Enabled = value
            INDBtnAddCashReceiptConcept.Enabled = False
            INDGcCashReceiptDetail.Enabled = value
            INDSleBankAccount.Enabled = value
            INDSleCash.Enabled = value
            INDLcCashReceipt.EndUpdate()
            If INDBteCode.Enabled = True Then
                INDBteCode.Focus()
            Else
                INDSleThirdParty.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICashReceipts.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements ICashReceipts.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' propiedad que obtiene o establece las cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account.
    ''' </value>
    Public Property AccountXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ICashReceipts.AccountXPO
        Get
            Return CType(INDSleBankAccount.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSleBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece las cajas
    ''' </summary>
    ''' <value>
    ''' The cash xpo.
    ''' </value>
    Public Property CashXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements ICashReceipts.CashXPO
        Get
            Return CType(INDSleCash.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSleCash.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center xpo.
    ''' </value>
    Public Property CostCenterXPO As XPInstantFeedbackSource Implements ICashReceipts.CostCenterXPO
        Get
            Return CType(INDSleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los terceros
    ''' </summary>
    ''' <value>
    ''' The third party xpo.
    ''' </value>
    Public Property ThirdPartyXPO As XPInstantFeedbackSource Implements ICashReceipts.ThirdPartyXPO
        Get
            Return CType(INDSleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece los terceros
    ''' </summary>
    ''' <value>
    ''' The third party xpo.
    ''' </value>
    Public Property ResponsiblePaymentXPO As XPInstantFeedbackSource Implements ICashReceipts.ResponsiblePaymentXPO
        Get
            Return CType(INDSleReponsiblePayment.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleReponsiblePayment.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Variable para establecer el formato de la moneda
    ''' </summary>
    Public Property propertyCurrencyISO4217 As String

    ''' <summary>
    ''' Variable para identificar si se esta llamando desde otro formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Property subCashReceipts As Boolean = False

    ''' <summary>
    ''' bandera para saber el Documento Origen:
    ''' 1. Liquidación
    ''' 2. Factura de Productos
    ''' 3. Facturación Básica
    ''' </summary>
    ''' <remarks></remarks>
    Private _sourceDocument As eSourceDocument

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
    ''' Id del Centro de Costo que abre el frontal
    ''' </summary>
    Property CostCenterId As Integer

    ''' <summary>
    ''' Valor por defecto a crear del método de pago
    ''' </summary>
    ''' <value>
    ''' The cash default value.
    ''' </value>
    Property CashDefaultValue As Decimal

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
    ''' variable privada que toma el valor de la propieda del mismo nombre que establece
    ''' el Id de la moneda del formulario padre que abre el form de recibos de caja
    ''' </summary>
    Private _parentCurrencyId As Integer = SessionValues.Instance.OfficialCurrencyId
    Private _parentCurrencyAbbreviation As String = SessionValues.Instance.CurrencyISO4217

    ''' <summary>
    ''' propiedad de escritura que establece el valor a la variable de moneda del formulario padre
    ''' </summary>
    Public WriteOnly Property ParentCurrencyId(currencyAbbreviation As String) As Integer
        Set(value As Integer)
            If String.IsNullOrEmpty(currencyAbbreviation) Then
                Mensaje(EeventViewerImages.Advertencia) = "No se definió la abreviacion de la moneda del formulario padre"
                Exit Property
            End If
            _parentCurrencyAbbreviation = currencyAbbreviation
            _parentCurrencyId = value
        End Set
    End Property

#End Region

#Region "ICRUD BASE"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.cashReceipts IsNot Nothing AndAlso Me.cashReceipts.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using model As New MCashReceipts(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await model.DeleteCashReceipts(Me.cashReceipts)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If cashReceipts IsNot Nothing AndAlso cashReceipts.Status < 3 Then
            Dim errors As New StringBuilder
            ValidateFields(errors)
            ' Validaciones específicas de Guardar
            If INDGvMethodPayment.RowCount > 0 AndAlso subCashReceipts AndAlso _sourceDocument = eSourceDocument.ProductSales Then
                If listPaymentMethods.Sum(Function(x) x.Value) <> _valueProductInvoice Then
                    errors.AppendLine("El recibo de caja se debe hacer por el mismo valor (" + _valueProductInvoice.ToString("C0") + ") de la factura")
                End If
            End If
            If Not subCashReceipts Then
                If INDGvCashReceiptDetail.RowCount = 0 Then
                    errors.AppendLine("Se debe agregar mínimo un concepto de recibo de caja")
                End If
            End If
            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                AsyncLoader(False)
                Exit Sub
            End If
            AssigningValues()
        End If
        If subCashReceipts AndAlso (_sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling) Then
            cashReceipts.Status = 2
            Me.DialogResult = System.Windows.Forms.DialogResult.OK

            If Not Me.ValidateParentCurrency Then
                Me.DialogResult = DialogResult.Abort
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrencyDocumentDifferent")
            End If

            AsyncLoader(False)
            Me.Close()
            Exit Sub
        End If
        Try
            Using model As New MCashReceipts(MyTag)
                Dim result = Await model.SaveCashReceipts(cashReceipts, _idCurrentSequence, Me._sequence)
                'presenter.GetSequense()
                If result.StateResult = True Then
                    Dim actionMessage As String = ""
                    If cashReceipts.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numérica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If result.Message Is Nothing Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            actionMessage = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                            FailedMessage(result.Message, actionMessage)
                        End If
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
                    cashReceipts = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, cashReceipts.Id, 0, cashReceipts.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, cashReceipts.Id, 0, cashReceipts.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, cashReceipts.Id, 0, cashReceipts.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 AndAlso result.MessageResult(0) = "Periods" Then
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    End If
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' método para crear el mensaje de error cuando se guarda un recibo de caja 
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

                    Dim result = Await model.ConfirmCashReceipts(cashReceipts.Id)

                    If result.StateResult = True Then
                        Using modelSetting As New MSettingsTreasury(MyTag)
                            Dim setting = modelSetting.GetSettingsTreasuryByIdUnitOperativeSimple(BarraBotones.OperatingUnitValue)
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmSatisfactory"), cashReceipts.Code, setting.ObjectEmbbeded.FullNameJournaVoucherTypeCashReceipts, result.ObjectEmbbeded)
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, cashReceipts.Id, 0, cashReceipts.Id)
                            AsyncLoader(False)
                            Me.Deshacer()
                        End Using
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            Mensaje(EeventViewerImages.MensajeError) = result.MessageResult(0)
                        Else
                            Dim message As New StringBuilder
                            message.AppendLine(ResourceManager.GetString("NotConfirmedBy", MODULE_NAME))
                            message.AppendLine(result.ObjectEmbbeded)
                            'message.AppendLine(ResourceManager.GetString("ChangeCashReceipt", MODULE_NAME))
                            Mensaje(EeventViewerImages.Advertencia) = message.ToString()
                        End If
                        AsyncLoader(False)
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewCashReceipt()
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
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
                              New ColumnInfo With {.Caption = "Valor", .FieldName = "Value", .ColumnWidth = 200, .ColumnFormatType = DevExpress.Utils.FormatType.Custom, .ColumnFormat = "N2"},
                              New ColumnInfo With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = 200},
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
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' metodo para guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub SaveAndConfirm()

        Try
            Dim errors As New StringBuilder
            ValidateFields(errors)

            If errors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
                AsyncLoader(False)
                Exit Sub
            End If
            Using model As New MCashReceipts(MyTag)
                If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AssigningValues()

                    If INDGvCashReceiptDetail.RowCount = 0 Then
                        errors.AppendLine("Se debe agregar mínimo un concepto de recibo de caja")
                    End If

                    cashReceipts.Status = 2
                    Dim result = Await model.SaveAndConfirmCashReceipts(cashReceipts, _idCurrentSequence, Me._sequence)
                    presenter.GetSequense()

                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                        Me.Deshacer()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    If result.StateResult Then
                        Me.BarraBotones.PrintReport(PrintReportAction.Confirm, result.ObjectEmbbeded.Id, 0, result.ObjectEmbbeded.Id)
                    ElseIf result.ObjectEmbbeded IsNot Nothing Then
                        Me.BarraBotones.PrintReport(PrintReportAction.Create, cashReceipts.Id, 0, cashReceipts.Id)
                    End If
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try

    End Sub
#End Region

#Region "METHODS"
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
        If CtrPopupPaymentMethod1.ClosePopupChangePaymentMethod Then
            Dim size As System.Drawing.Size
            paymentMethodType = paymentType
            INDPceMethodPayment.Properties.PopupSizeable = True
            CtrPopupPaymentMethod1.INDBtnAdd.Width = widthButtonAdd
            CtrPopupPaymentMethod1.INDBtnClose.Width = widthButtonClose
            size.Width = widthPopup
            size.Height = heightPopup
            INDPccPaymentMethod.Size = size
            CtrPopupPaymentMethod1.INDBtnAdd.Visible = buttonAddVisible
            CtrPopupPaymentMethod1.INDBtnClose.Visible = buttonCloseVisible
            INDPceMethodPayment.Properties.PopupSizeable = False
            INDPceMethodPayment.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para agregar a las rejillas las acciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvMethodPayment, ListActions)
        IndigoGridView2.SetListAcction(INDGvCashReceiptDetail, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvMethodPayment.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvCashReceiptDetail.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    ''' <summary>
    ''' obtener debitos y creditos
    ''' </summary>
    Private Sub getDebitCredit()
        debit = 0
        credit = 0
        If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
            debit += Math.Round(listPaymentMethods.Sum(Function(x) x.ValueInCurrencyHeader), 2, MidpointRounding.AwayFromZero)
        End If

        If listCashReceiptdetails IsNot Nothing AndAlso listCashReceiptdetails.Count > 0 Then
            debit += listCashReceiptdetails.Where(Function(x) x.Nature = 1).Sum(Function(z) z.ValueInCurrencyHeader)
            credit += listCashReceiptdetails.Where(Function(x) x.Nature = 2).Sum(Function(z) z.ValueInCurrencyHeader)
        End If
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

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
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
    ''' metodo para limpiar los controles
    ''' </summary>
    Private Async Sub CleanControls()
        If taskSetMoreInfo IsNot Nothing AndAlso taskSetMoreInfo.IsBusy Then
            taskSetMoreInfo.WorkerSupportsCancellation = True
            taskSetMoreInfo.CancelAsync()
        End If

        If BarraBotones.PermiteGuardarResponsablePagoTercero Then
            INDLciResponsiblePaymentSle.Visibility = LayoutVisibility.Always
            INDLciResponsiblePaymentText.Visibility = LayoutVisibility.Never
        Else
            INDLciResponsiblePaymentSle.Visibility = LayoutVisibility.Never
            INDLciResponsiblePaymentText.Visibility = LayoutVisibility.Always
        End If

        INDLcCashReceipt.BeginUpdate()
        ReadOnlyControls(False)
        BarraBotones.CleanAuditBasic()
        ActionsOnControls = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.ReassignOperatingUnit()
        CtrPopupPaymentMethod1.INDGlePaymentMethod.EditValue = Nothing
        INDPceMethodPayment.ClosePopup()
        CtrPopupPaymentMethod1.CleanControls()
        INDBteCode.Text = String.Empty
        INDSleThirdParty.Properties.ReadOnly = False
        INDSleThirdParty.Properties.Buttons(0).Enabled = True
        INDSleThirdParty.Properties.NullText = String.Empty
        INDSleThirdParty.EditValue = Nothing
        INDSleReponsiblePayment.Properties.ReadOnly = False
        INDSleReponsiblePayment.Properties.Buttons(0).Enabled = True
        INDSleReponsiblePayment.Properties.NullText = String.Empty
        Me.IdResponsiblePayment = Nothing
        Me.PaymentResponsibles = String.Empty
        INDMeDetail.Text = String.Empty
        INDGleTypeCollection.EditValue = Nothing
        INDGleTypeCollection.Properties.ReadOnly = False
        INDSleBankAccount.Properties.NullText = String.Empty
        INDSleBankAccount.Properties.ReadOnly = False
        INDSleBankAccount.EditValue = Nothing
        INDSleCostCenter.Properties.NullText = String.Empty
        INDSleCostCenter.EditValue = Nothing
        INDGcMethodPayment.DataSource = Nothing
        listPaymentMethods = Nothing
        listPaymentMethodsDelete = Nothing
        INDGcCashReceiptDetail.DataSource = Nothing
        INDGvCashReceiptDetail.OptionsView.ShowFooter = False
        INDGvMethodPayment.OptionsView.ShowFooter = False
        IndigoGridControl1.RefreshGrid(INDGcCashReceiptDetail)
        IndigoGridControl1.RefreshGrid(INDGcMethodPayment)
        cashReceipts = Nothing
        listCashReceiptdetails = Nothing
        listCashReceiptdetailsDelete = Nothing
        INDSleCash.Properties.NullText = String.Empty
        IdCashRegister = Nothing
        INDSleCash.Properties.ReadOnly = False
        INDGcAdvancesMoreInfo.DataSource = Nothing
        INDGcBillsMoreInfo.DataSource = Nothing
        INDGcAccountPayable.DataSource = Nothing
        listAccountReceivable = Nothing
        listCashReceiptAdvancePayment = Nothing
        listCashReceiptDetailAccountPayable = Nothing
        OldCashRegisterId = Nothing
        getDebitCredit()
        If subCashReceipts Then
            _ctrDebitCredit.Visible = False
        Else
            _ctrDebitCredit.Visible = True
        End If
        _ctrDebitCredit.RefreshDebitCredit()
        INDLiAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLiCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        Dim dateServer = Me.GetDateServer()
        INDDteDocumentDate.Properties.ReadOnly = True
        INDDteDocumentDate.Properties.MinValue = dateServer
        INDDteDocumentDate.Properties.MaxValue = dateServer
        DocumentDate = dateServer

        Me.CleanEditValueBankAndCash()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLcCashReceipt.EndUpdate()
    End Sub


    Private Function SetDetail() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using model As New MCashReceipts(MyTag)
                                             listCashReceiptdetails = model.GetCashReceiptDetailsByIdCashReceipt(cashReceipts.Id)
                                         End Using
                                     End Sub)
    End Function

    ''' <summary>
    ''' metodo para cargar los datos a los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Dim taskSetMoreInfo As BackgroundWorker
    Public Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using model As New MCashReceipts(MyTag)
            INDLcCashReceipt.BeginUpdate()
            AsyncLoader(True)
            Dim result = Await model.GetCashRegisterByCode(Code)
            If result.StateResult = True AndAlso result.ObjectEmbbeded.Id > 0 Then
                cashReceipts = result.ObjectEmbbeded
                listPaymentMethods = model.GetPaymentMethodsByCashReceipts(cashReceipts.Id)
                Await SetDetail()
                If listCashReceiptdetails.Count > 0 Then
                    INDSleThirdParty.Properties.ReadOnly = True
                    INDSleThirdParty.Properties.Buttons(0).Enabled = False
                    INDSleReponsiblePayment.Properties.ReadOnly = True
                    INDSleReponsiblePayment.Properties.Buttons(0).Enabled = False
                End If
            Else
                cashReceipts = New CashReceipts
            End If
        End Using
        If cashReceipts.Id > 0 Then
            _flagLoadControls = True
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
                Code = .Code
                INDSleThirdParty.Properties.NullText = .NitNameThirdParty
                IdThirdParty = .IdThirdParty
                PaymentResponsibles = .PaymentResponsibles
                Detail = .Detail
                DocumentDate = .DocumentDate
                CollectType = .CollectType
                Dim dateServer = Me.GetDateServer()
                If .CollectType = 1 Then
                    INDLiAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLiAccount.AllowHide = True
                    INDLiCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLiCash.AllowHide = False
                    CtrPopupPaymentMethod1.INDGlePaymentMethod.Properties.DataSource = CtrPopupPaymentMethod1.MethodsPaymentsCash
                    INDDteDocumentDate.Properties.ReadOnly = True
                    If .Status = 1 Then
                        INDDteDocumentDate.Properties.MaxValue = dateServer
                        INDDteDocumentDate.Properties.MinValue = dateServer
                    End If
                Else
                    INDLiAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLiAccount.AllowHide = False
                    INDLiCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLiCash.AllowHide = True
                    CtrPopupPaymentMethod1.INDGlePaymentMethod.Properties.DataSource = CtrPopupPaymentMethod1.MethodsPaymentsBank
                    Using modelSetting As New MSettingsTreasury(MyTag)
                        INDDteDocumentDate.Properties.ReadOnly = True
                        INDDteDocumentDate.Properties.MinValue = Nothing
                        INDDteDocumentDate.Properties.MaxValue = Nothing
                        Dim setting = modelSetting.GetSettingsTreasuryByIdUnitOperativeSimple(BarraBotones.OperatingUnitValue)
                        If .Status = 1 Then
                            If setting.ObjectEmbbeded.AllowModifyDocumentsDateBank Then
                                INDDteDocumentDate.Properties.ReadOnly = False
                                INDDteDocumentDate.Properties.MaxValue = dateServer
                                INDDteDocumentDate.Properties.MinValue = CDate(dateServer).AddDays(-setting.ObjectEmbbeded.NumberDayBank)
                            Else
                                INDDteDocumentDate.Properties.ReadOnly = True
                                INDDteDocumentDate.Properties.MaxValue = dateServer
                                INDDteDocumentDate.Properties.MinValue = dateServer
                            End If
                        End If
                    End Using
                End If
                IdBankAccount = .IdBankAccount
                INDSleBankAccount.Properties.NullText = .CodeNameBankAccount
                IdCashRegister = .IdCashRegister
                INDSleCash.Properties.NullText = .CodeNameCashRegister
                IdCostCenter = .IdCostCenter
                INDSleCostCenter.Properties.NullText = .CodeNameCostCenter
                If .CurrencyId Is Nothing Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = String.Format("{0} No tiene una moneda definida", IIf(.CollectType = 1, "El recibo de caja", "La cuenta bancaria"))
                Else
                    Me.CurrencyId = .CurrencyId
                    Me.propertyCurrencyISO4217 = .currencyAbbreviation
                End If
                INDGleTypeCollection.Properties.ReadOnly = True
                INDSleBankAccount.Properties.ReadOnly = True
                INDSleCash.Properties.ReadOnly = True
                If .IdCostCenter IsNot Nothing Then
                    INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDLiCostCenter.AllowHide = False
                Else
                    INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLiCostCenter.AllowHide = True
                End If
                IdMainAccount = .IdMainAccount
                getDebitCredit()

                BarraBotones.StatusRecord = .Status.ToString()
                BarraBotones.OperatingUnitValue = .OperatingUnitId
                If .PortfolioAdvance.Count > 0 Then
                    INDTxtAdvanceValue.EditValue = .PortfolioAdvance(0).Value
                    INDTxtAdmission.EditValue = .PortfolioAdvance(0).AdmissionNumber
                    INDMeAdvanceDetail.Text = .PortfolioAdvance(0).Observations
                Else
                    INDTxtAdvanceValue.EditValue = Nothing
                    INDTxtAdmission.EditValue = Nothing
                    INDMeAdvanceDetail.Text = String.Empty
                End If
            End With
            Select Case cashReceipts.Status
                Case 1
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                Case Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    ReadOnlyControls(True)
                    INDColMoreInfo.OptionsColumn.AllowEdit = True
                    INDPccMoreInfo.Enabled = True
                    ' Habilitar grids y columnas de acciones para permitir visualización en modo confirmado/anulado
                    INDGcMethodPayment.Enabled = True
                    INDGcCashReceiptDetail.Enabled = True
                    ' Habilitar las vistas para que se puedan editar las columnas de acciones
                    INDGvMethodPayment.OptionsBehavior.Editable = True
                    INDGvCashReceiptDetail.OptionsBehavior.Editable = True
                    Dim colActionsMetodosPago = INDGvMethodPayment.Columns.ColumnByName("colActions")
                    If colActionsMetodosPago IsNot Nothing Then
                        colActionsMetodosPago.OptionsColumn.AllowEdit = True
                    End If
                    Dim colActionsConceptos = INDGvCashReceiptDetail.Columns.ColumnByName("colActions")
                    If colActionsConceptos IsNot Nothing Then
                        colActionsConceptos.OptionsColumn.AllowEdit = True
                    End If
            End Select

            Await SetDocuments()
            Me.GetDocumentIndexed(MyTag & "_" & cashReceipts.Code)
            GenerateBlockRecord()
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
            Await LoadReport()
            AsyncLoader(False)
            ActionsOnControls = True
            If cashReceipts.Status = 1 Then
                INDBtnAddCashReceiptConcept.Enabled = True
            End If

            'DataSource de Metodos/Formas de Pago
            INDGcMethodPayment.DataSource = Nothing
            INDGcMethodPayment.DataSource = listPaymentMethods
            If listPaymentMethods.Count > 0 Then
                INDGvMethodPayment.OptionsView.ShowFooter = True
            Else
                INDGvMethodPayment.OptionsView.ShowFooter = False
            End If
            'DataSource de Detalles de Recibos de Caja
            INDGcCashReceiptDetail.DataSource = Nothing
            INDGcCashReceiptDetail.DataSource = listCashReceiptdetails
            If listCashReceiptdetails.Count > 0 Then
                INDGvCashReceiptDetail.OptionsView.ShowFooter = True
            Else
                INDGvCashReceiptDetail.OptionsView.ShowFooter = False
            End If
            INDLcCashReceipt.EndUpdate()
            If listCashReceiptdetails.Count > 0 Then
                taskSetMoreInfo = New BackgroundWorker
                AddHandler taskSetMoreInfo.DoWork, AddressOf task_doWork
                AddHandler taskSetMoreInfo.RunWorkerCompleted, AddressOf task_RunWorkerCompleted

                If Not taskSetMoreInfo.IsBusy = True Then
                    taskSetMoreInfo.RunWorkerAsync()
                End If
            End If
            _ctrDebitCredit.RefreshDebitCredit()
            RaiseEvent LoadControlsFinish(cashReceipts)
            INDSleThirdParty.Focus()
            _flagLoadControls = False
        Else
            AsyncLoader(False)
            INDLcCashReceipt.EndUpdate()
            If Me._sequence.IsManual Then
                Me.NewCashReceipt()
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                Me.Code = String.Empty
                Deshacer()
                INDBteCode.Focus()
            End If
        End If
    End Function

    Private Function SetDocuments() As Task
        Return Task.Factory.StartNew(Sub()
                                         BarraBotones.SafeInvoke(Sub(x) x.SetDocuments(cashReceipts.Id, MyTag, Nothing, GetType(CashReceipts).Name))
                                     End Sub)
    End Function

    Private Function LoadReport() As Task
        Return Task.Factory.StartNew(Sub()
                                         Me.BarraBotones.SafeInvoke(Sub(x) x.PrintReport(PrintReportAction.None, cashReceipts.Id, 0, cashReceipts.Id))
                                     End Sub)
    End Function

    ''' <summary>
    ''' método para generar el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MCommonTreasury(MyTag)
            Dim result = Await model.GetBlockRecordTreasury(Me.Tag, Me.cashReceipts.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordTreasury With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = cashReceipts.Id}
                Dim operation = Await model.SaveBlockRecordTreasury(record)
                record = operation.ObjectEmbbeded
            Else
                record = result
                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para generar la indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content As String = String.Empty
        Dim detail As CashReceiptDetails = cashReceipts.CashReceiptDetails.Where(Function(x) x.CashReceiptConceptAffectation > 1).FirstOrDefault()
        If detail IsNot Nothing Then
            If detail.CashReceiptConceptAffectation = 2 Then
                If cashReceipts.PortfolioAdvance.Count > 0 And detail.CashReceiptAccountReceivable.Count > 0 Then
                    Dim bills = String.Join("-", (From b In detail.CashReceiptAccountReceivable Select b.InvoiceNumber).ToList.Distinct.ToList())
                    content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent_BillsAndAdvance", MODULE_NAME), cashReceipts.Code, INDSleThirdParty.Text, INDGleTypeCollection.Text & ": " & If(CollectType = 1, INDSleCash.Text, INDSleBankAccount.Text), bills, cashReceipts.PortfolioAdvance(0).Observations)
                ElseIf cashReceipts.PortfolioAdvance.Count > 0 Then
                    content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent_OnlyAdvance", MODULE_NAME), cashReceipts.Code, INDSleThirdParty.Text, INDGleTypeCollection.Text & ": " & If(CollectType = 1, INDSleCash.Text, INDSleBankAccount.Text), cashReceipts.PortfolioAdvance(0).Observations)
                Else
                    Dim bills = String.Join("-", (From b In detail.CashReceiptAccountReceivable Select b.InvoiceNumber).ToList.Distinct.ToList())
                    content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent_OnlyBills", MODULE_NAME), cashReceipts.Code, INDSleThirdParty.Text, INDGleTypeCollection.Text & ": " & If(CollectType = 1, INDSleCash.Text, INDSleBankAccount.Text), bills)
                End If
            Else
                Dim advances = String.Join("-", (From c In detail.CashReceiptAdvancePayment Select c.Code).ToList())
                content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent_RepaymentAdvances", MODULE_NAME), cashReceipts.Code, INDSleThirdParty.Text, INDGleTypeCollection.Text & ": " & If(CollectType = 1, INDSleCash.Text, INDSleBankAccount.Text), advances)
            End If
        Else
            content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), cashReceipts.Code, INDSleThirdParty.Text, INDGleTypeCollection.Text & ": " & If(CollectType = 1, INDSleCash.Text, INDSleBankAccount.Text))
        End If

        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & cashReceipts.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), cashReceipts.Code),
                .Update = dateServer,
                .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), cashReceipts.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' método para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        If subCashReceipts Then
            LoadCashReceiptDetail()
        End If

        With cashReceipts
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .IdThirdParty = IdThirdParty
            .CollectType = CollectType
            .IdMainAccount = IdMainAccount
            .IdCostCenter = IdCostCenter
            .CurrencyId = Me.CurrencyId
            .Detail = Detail
            .DocumentDate = DocumentDate
            If CollectType = 1 Then
                .IdCashRegister = IdCashRegister
            Else
                .IdBankAccount = IdBankAccount
            End If
            .Prefix = Me._prefixSelected

            .PaymentResponsibles = PaymentResponsibles
            .PaymentResponsiblesThirdPartyId = IdResponsiblePayment
            .Value = debit
            .OperatingUnitId = _idOperativeUnit
            .Status = 1
            If listPaymentMethodsDelete IsNot Nothing AndAlso listPaymentMethodsDelete.Count > 0 Then
                For Each item In listPaymentMethodsDelete
                    .PaymentMethods.Add(item.MarkAsDeleted())
                Next
            End If
            If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
                For Each item In listPaymentMethods
                    .PaymentMethods.Add(item)
                Next
            End If
            If listCashReceiptdetailsDelete IsNot Nothing AndAlso listCashReceiptdetailsDelete.Count > 0 Then
                For Each item In listCashReceiptdetailsDelete
                    .CashReceiptDetails.Add(item.MarkAsDeleted())
                Next
            End If
            If listCashReceiptdetails IsNot Nothing AndAlso listCashReceiptdetails.Count > 0 Then
                For Each item In listCashReceiptdetails
                    .CashReceiptDetails.Add(item)
                Next
            End If
        End With
        If cashReceipts.Id > 0 Then
            cashReceipts.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' método para asignar valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValuesDelete()
        cashReceipts.MarkAsDeleted()
        With cashReceipts

            If listPaymentMethodsDelete IsNot Nothing AndAlso listPaymentMethodsDelete.Count > 0 Then
                Parallel.ForEach(listPaymentMethodsDelete, Sub(item)
                                                               .PaymentMethods.Add(item.MarkAsDeleted())
                                                           End Sub)
            End If

            If listPaymentMethods.Count > 0 Then
                Parallel.ForEach(listPaymentMethods, Sub(item As PaymentMethods)
                                                         If item.Id > 0 Then
                                                             .PaymentMethods.Add(item.MarkAsDeleted)
                                                         End If
                                                     End Sub)
            End If

            If listCashReceiptdetailsDelete IsNot Nothing AndAlso listCashReceiptdetailsDelete.Count > 0 Then
                Parallel.ForEach(listCashReceiptdetailsDelete, Sub(item)
                                                                   .CashReceiptDetails.Add(item.MarkAsDeleted())
                                                               End Sub)
            End If
            If listCashReceiptdetails.Count > 0 Then
                Parallel.ForEach(listCashReceiptdetails, Sub(item As CashReceiptDetails)
                                                             If item.Id > 0 Then
                                                                 MarkAsDeleteCashReceiptDetail(item)
                                                                 .CashReceiptDetails.Add(item.MarkAsDeleted)
                                                             End If
                                                         End Sub)
            End If

        End With
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

            Dim portfolioAdvanceTmp As PortfolioAdvance = Nothing
            If cashReceiptdetails.Id > 0 Then
                portfolioAdvanceTmp = cashReceipts.PortfolioAdvance.Where(Function(x) x.CashReceiptDetailId = cashReceiptdetails.Id).FirstOrDefault()
            Else
                portfolioAdvanceTmp = cashReceipts.PortfolioAdvance.ToList().Find(Function(x) x.CashReceiptDetails IsNot Nothing AndAlso x.CashReceiptDetails.Equals(cashReceiptdetails))

            End If
        End If
        If _cashReceiptDetail.CashReceiptConceptAffectation = 3 Then
            While _cashReceiptDetail.CashReceiptAdvancePayment.Count > 0
                _cashReceiptDetail.CashReceiptAdvancePayment.Item(0).MarkAsDeleted()
            End While
        End If
        If _cashReceiptDetail.CashReceiptConceptAffectation = 4 Then
            While _cashReceiptDetail.CashReceiptDetailAccountPayable.Count > 0
                _cashReceiptDetail.CashReceiptDetailAccountPayable.Item(0).MarkAsDeleted()
            End While
        End If
    End Sub

    ''' <summary>
    ''' metodo para generar secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function NewCashReceipt() As Task
        cashReceipts = New CashReceipts()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            If subCashReceipts Then
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
            End If
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
                If subCashReceipts Then
                    Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Guardar, "Guardar y Confirmar")
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                End If
                'Validamos que tenga permisos de "guardar y confirmar" o "guardar", en el pop-up siemrpre se muestra guardar.
                If BarraBotones.PermissionsForm.ContainsKey(144) Then
                    If Not Me.subCashReceipts Then
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = False
                    Else
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
                    End If
                Else
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        If subCashReceipts Then
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                        End If
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
                            If subCashReceipts Then
                                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
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
                    If subCashReceipts Then
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GuardarConfirmar) = True
                    End If
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
    End Function

    ''' <summary>
    ''' metodo para agregar un concepto de recibo de caja a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnPopupCashReceiptConcept(sender As Object, e As AddCashReceiptConceptEventArgs)
        Select Case e.CashReceiptDetails.CashReceiptConceptAffectation
            Case 2
                listAccountReceivable = e.ListAccountReceivable
                INDGcBillsMoreInfo.DataSource = Nothing
                INDGcBillsMoreInfo.DataSource = listAccountReceivable
                IndigoGridControl1.RefreshGrid(INDGcBillsMoreInfo)
                IndigoGridControl1.RefreshGrid(INDGcAdvancesMoreInfo)
                IndigoGridControl1.RefreshGrid(INDGcAccountPayable)
                INDTcgBillAdvances.SelectedTabPageIndex = 0
            Case 3
                listCashReceiptAdvancePayment = e.ListAdvancePayment
                INDGcAdvancesMoreInfo.DataSource = Nothing
                INDGcAdvancesMoreInfo.DataSource = listCashReceiptAdvancePayment
                IndigoGridControl1.RefreshGrid(INDGcBillsMoreInfo)
                IndigoGridControl1.RefreshGrid(INDGcAdvancesMoreInfo)
                IndigoGridControl1.RefreshGrid(INDGcAccountPayable)
                INDTcgBillAdvances.SelectedTabPageIndex = 1
            Case 4
                listCashReceiptDetailAccountPayable = e.ListCashReceiptDetailAccountPayable
                INDGcAccountPayable.DataSource = Nothing
                INDGcAccountPayable.DataSource = listCashReceiptDetailAccountPayable
                IndigoGridControl1.RefreshGrid(INDGcBillsMoreInfo)
                IndigoGridControl1.RefreshGrid(INDGcAdvancesMoreInfo)
                IndigoGridControl1.RefreshGrid(INDGcAccountPayable)
                INDTcgBillAdvances.SelectedTabPageIndex = 2
        End Select

        If e.PortfolioAdvance IsNot Nothing Then
            If e.PortfolioAdvance.Id > 0 Then
                If e.PortfolioAdvance.Status = 0 Then
                    cashReceipts.PortfolioAdvance.Where(Function(x) x.CashReceiptDetailId = cashReceiptdetails.Id).FirstOrDefault().MarkAsDeleted()
                Else
                    Dim portfolioAdvanceTmp = cashReceipts.PortfolioAdvance.Where(Function(x) x.CashReceiptDetailId = cashReceiptdetails.Id).FirstOrDefault()
                    portfolioAdvanceTmp.Code = cashReceipts.Code
                    e.PortfolioAdvance.DocumentDate = cashReceipts.DocumentDate
                    portfolioAdvanceTmp.ModificationDate = Me.GetDateServer()
                End If
            Else
                If e.PortfolioAdvance.Status = 0 Then
                    cashReceipts.PortfolioAdvance.Remove(cashReceipts.PortfolioAdvance.ToList().Find(Function(x) x.CashReceiptDetails IsNot Nothing AndAlso x.CashReceiptDetails.Equals(cashReceiptdetails)))
                Else
                    e.PortfolioAdvance.CreationDate = Me.GetDateServer()
                    e.PortfolioAdvance.Code = cashReceipts.Code
                    e.PortfolioAdvance.CreationDate = DocumentDate
                    e.PortfolioAdvance.DocumentDate = DocumentDate
                    e.PortfolioAdvance.CashReceiptDetails = e.CashReceiptDetails
                    cashReceipts.PortfolioAdvance.Add(e.PortfolioAdvance)
                End If
            End If
        End If
        AddCashReceiptConcept(e.CashReceiptDetails)
    End Sub


    ''' <summary>
    ''' Loads the cash receipt detail(when its subform).
    ''' </summary>
    Private Async Sub LoadCashReceiptDetail()
        cashReceiptdetails = New CashReceiptDetails()
        cashReceiptdetails.IdThirdParty = INDSleThirdParty.EditValue
        Dim cashReceiptConcept As CashReceiptConcepts = Nothing 'Consultar de los parametros el concepto

        Using model As New MCashReceiptsConcepts(Me.Tag)
            If _sourceDocument = eSourceDocument.Liquidation Then
                If FolioType = 3 Then ' Particulares
                    cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling?.IndvidualAdvanceCashReceiptConceptId)
                Else
                    If LiquidationType = 1 Then
                        cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling?.PatientAdvanceCashReceiptConceptId)
                    Else
                        cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling?.CapitedPatientAdvanceCashReceiptConceptId)
                    End If
                End If
            ElseIf _sourceDocument = eSourceDocument.ProductSales Then
                cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling?.ProductSalesCashReceiptConceptId)
            ElseIf _sourceDocument = eSourceDocument.BasicBilling Then
                cashReceiptConcept = model.GetCashReceiptConceptById(settingBilling?.BasicBillingCashReceiptConceptId)
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
                cashReceiptdetails.IdMainAccount = settingBilling?.ProductSalesMainAccountId
                cashReceiptdetails.IdCostCenter = settingBilling?.ProductSalesCostCenterId
            ElseIf _sourceDocument = eSourceDocument.BasicBilling Then
                If Me._idThirdPartyMainAccount IsNot Nothing AndAlso cashReceiptConcept.Affectation <> 2 Then
                    cashReceiptConcept.IdMainAccount = Me._idThirdPartyMainAccount
                End If
                cashReceiptdetails.IdMainAccount = cashReceiptConcept.IdMainAccount
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

        ''condicion para ajustar el error de decimales para que tanto debitos como creditos queden balanceados
        Dim sumDebitCredit As Decimal = 0
        If listCashReceiptdetails?.Any() Then
            sumDebitCredit = listCashReceiptdetails.Sum(Function(x) x.Value * If(x.Nature = 2, 1, -1))
        End If

        If (cashReceiptdetails.Value + sumDebitCredit - PaymentValue) <> 0 Then
            Dim adjustmentValue As Decimal = PaymentValue - (cashReceiptdetails.Value + sumDebitCredit)
            cashReceiptdetails.Value += adjustmentValue
        End If

        cashReceiptdetails.ValueInCurrencyHeader = cashReceiptdetails.Value
        AddCashReceiptConcept(cashReceiptdetails)

        If (eSourceDocument.Liquidation = _sourceDocument) OrElse (eSourceDocument.BasicBilling = _sourceDocument AndAlso cashReceiptConcept?.Affectation = 2) Then
            Dim PortfolioAdvance = CreatePortfolioAdvance(cashReceiptConcept)
            PortfolioAdvance.CreationDate = Me.GetDateServer()
            PortfolioAdvance.DocumentDate = Me.DocumentDate
            PortfolioAdvance.Code = cashReceipts.Code
            PortfolioAdvance.CashReceiptDetails = listCashReceiptdetails.ElementAt(0)
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
        portfolioAdvance.CostCenterId = INDSleCostCenter.EditValue
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
        portfolioAdvance.Observations = "Cruce de cuentas factura {0}"
        portfolioAdvance.DebitValue = 0
        portfolioAdvance.CreditValue = 0
        portfolioAdvance.Balance = debitValue
        portfolioAdvance.ValueInCurrencyHeader = debitValue
        portfolioAdvance.CreationUser = indigo.UserIndigo
        portfolioAdvance.Status = 1
        Return portfolioAdvance
    End Function

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
            Dim detailTmp = listCashReceiptdetails.Find(Function(x) x.IdCashReceiptConcept = cashReceiptDetailsAdd.IdCashReceiptConcept And x.CardNumber = cashReceiptDetailsAdd.CardNumber)
            If detailTmp IsNot Nothing Then
                listCashReceiptdetails.Remove(detailTmp)
            End If
        End If
        listCashReceiptdetails.Add(cashReceiptDetailsAdd)
        INDGcCashReceiptDetail.DataSource = Nothing
        INDGcCashReceiptDetail.DataSource = listCashReceiptdetails
        INDGvCashReceiptDetail.OptionsView.ShowFooter = True
        INDSleThirdParty.Properties.ReadOnly = True
        INDSleThirdParty.Properties.Buttons(0).Enabled = False
        INDSleReponsiblePayment.Properties.ReadOnly = True
        INDSleReponsiblePayment.Properties.Buttons(0).Enabled = False
        If cashReceiptdetails Is Nothing Then
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("CashReceiptDetailAdd", MODULE_NAME)
        Else
            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("CashReceiptDetailEdit", MODULE_NAME)
        End If
        cashReceiptdetails = Nothing
        _ctrDebitCredit.CodeISO4217 = Me.propertyCurrencyISO4217
        getDebitCredit()
        _ctrDebitCredit.RefreshDebitCredit()
    End Sub

    ''' <summary>
    ''' metodo para instanciar el formulario de concepto de recibo de caja
    ''' </summary>
    ''' <param name="_cashReceiptDetail"></param>
    ''' <param name="ConfirmStatus">Indica si el recibo está confirmado/anulado para modo visualización</param>
    ''' <remarks></remarks>
    Private Sub InstantiatePopup(_cashReceiptDetail As CashReceiptDetails, listConcept As List(Of CashReceiptDetails), _portfolioAdvance As PortfolioAdvance, Optional ByVal ConfirmStatus As Boolean = False)
        Me.Cursor = ChangeCursorIndigo()

        Using formulario As New PopupCashReceiptConcept(_cashReceiptDetail, listConcept, _portfolioAdvance)
            AddHandler formulario.AddCashReceiptConcept, AddressOf ReturnPopupCashReceiptConcept
            AddHandler formulario.CashReceiptsDetailsNull, AddressOf CashReceiptsDetailNull
            If _cashReceiptDetail Is Nothing Then
                formulario.IdThirdParty = INDSleThirdParty.EditValue
                formulario.NameThirdParty = INDSleThirdParty.Text
            Else
                formulario.IdThirdPartyOriginalValue = INDSleThirdParty.EditValue
                formulario.NameThirdPartyOriginalValue = INDSleThirdParty.Text
            End If
            formulario.OperatingUnitId = _idOperativeUnit
            formulario.cashReceiptsCurrencyId = Me.CurrencyId
            formulario.cashReceiptsCurrencyAbrreviation = propertyCurrencyISO4217 'INDSleCurrency.Properties.NullText
            formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
            formulario.ViewModeEditHold = True
            formulario.ConfirmStatus = ConfirmStatus
            formulario.StartPosition = FormStartPosition.CenterParent
            formulario.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height * 0.8
            formulario.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width * 0.8
            Dim transparent = New FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener un dia habil
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetBusinessDay()
        Using Model As New MHoliday
            Dim holidays As List(Of Domain.Entities.Holiday) = Model.ListHolidayBetweenDate(DocumentDate.Value.AddDays(1), DocumentDate.Value.AddDays(1).AddMonths(1))
            Using ModelSettingTreasury As New MSettingsTreasury(MyTag)
                Dim setting As SettingsTreasury = (ModelSettingTreasury.GetSettingsTreasuryByIdUnitOperativeSimple(BarraBotones.OperatingUnitValue)).ObjectEmbbeded
                If setting IsNot Nothing AndAlso setting.Id > 0 Then
                    Dim depositDate = CommonService.GetBusinessDay(DocumentDate.Value.AddDays(1), holidays, setting.SaturdaySkillful, setting.SundaySkillful)
                    CtrPopupPaymentMethod1.DepositDate = depositDate
                    CtrPopupPaymentMethod1.INDDteDepositDate.EditValue = depositDate
                    CtrPopupPaymentMethod1.INDDteDepositDate.Properties.MinValue = depositDate
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "No se encontratron parámetros de tesorería para la unidad operativa seleccionada"
                End If
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
                Using modelPuc As New MPUC(MyTag)
                    Dim account = modelPuc.GetAccountByIdSimple(cashReceiptConceptICA.IdMainAccount)
                    If account.HandlesCostCenter Then
                        If card.GetCostCenter = 1 Then
                            cashReceiptDetailRetention.IdCostCenter = card.ICACostCenterId
                        ElseIf card.GetCostCenter = 2 Then
                            If card.CardCostCenter Is Nothing OrElse Not card.CardCostCenter.Any(Function(x) x.OperatingUnitId = _idOperativeUnit) Then
                                Exit Sub
                            End If
                            cashReceiptDetailRetention.IdCostCenter = card.CardCostCenter.Where(Function(x) x.OperatingUnitId = _idOperativeUnit).FirstOrDefault().CostCenterId
                        End If
                    End If
                    cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptICA.Code + " - " + cashReceiptConceptICA.Name
                    cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
                    cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptICA.IdMainAccount
                    cashReceiptDetailRetention.Nature = 1
                    cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptICA.Id
                    cashReceiptDetailRetention.Value = Decimal.Round(CDec(paymentMethodAdd.ICAValue), 2)
                    cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptICA
                    cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageICA
                    cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
                    cashReceiptDetailRetention.BillingValue = paymentMethodAdd.BaseValue
                    cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
                    cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptICA.Affectation
                    cashReceiptDetailRetention.ValueInCurrencyHeader = cashReceiptDetailRetention.Value
                    AddCashReceiptConcept(cashReceiptDetailRetention)
                End Using
            End Using
        End Using
    End Sub

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
                Using modelPuc As New MPUC(MyTag)
                    Dim account = modelPuc.GetAccountByIdSimple(cashReceiptConceptRTF.IdMainAccount)
                    If account.HandlesCostCenter Then
                        If card.GetCostCenter = 1 Then
                            cashReceiptDetailRetention.IdCostCenter = card.RTFCostCenterId
                        ElseIf card.GetCostCenter = 2 Then
                            If card.CardCostCenter Is Nothing OrElse Not card.CardCostCenter.Any(Function(x) x.OperatingUnitId = _idOperativeUnit) Then
                                Exit Sub
                            End If
                            cashReceiptDetailRetention.IdCostCenter = card.CardCostCenter.Where(Function(x) x.OperatingUnitId = _idOperativeUnit).FirstOrDefault().CostCenterId
                        End If
                    End If
                    cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptRTF.Code + " - " + cashReceiptConceptRTF.Name
                    cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
                    cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptRTF.IdMainAccount
                    cashReceiptDetailRetention.Nature = 1
                    cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptRTF.Id
                    cashReceiptDetailRetention.Value = Decimal.Round(CDec(paymentMethodAdd.RTFValue), 2)
                    cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptRTF
                    cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageRTF
                    cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
                    cashReceiptDetailRetention.BillingValue = paymentMethodAdd.BaseValue
                    cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
                    cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptRTF.Affectation
                    cashReceiptDetailRetention.ValueInCurrencyHeader = cashReceiptDetailRetention.Value
                    AddCashReceiptConcept(cashReceiptDetailRetention)
                End Using
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
                Using modelPuc As New MPUC(MyTag)
                    Dim account = modelPuc.GetAccountByIdSimple(cashReceiptConceptCommision.IdMainAccount)
                    If account.HandlesCostCenter Then
                        If card.GetCostCenter = 1 Then
                            cashReceiptDetailRetention.IdCostCenter = card.CommisionCostCenterId
                        ElseIf card.GetCostCenter = 2 Then
                            If card.CardCostCenter Is Nothing OrElse Not card.CardCostCenter.Any(Function(x) x.OperatingUnitId = _idOperativeUnit) Then
                                Exit Sub
                            End If
                            cashReceiptDetailRetention.IdCostCenter = card.CardCostCenter.Where(Function(x) x.OperatingUnitId = _idOperativeUnit).FirstOrDefault().CostCenterId
                        End If
                    End If
                    cashReceiptDetailRetention.CodeNameCashReceiptConcept = cashReceiptConceptCommision.Code + " - " + cashReceiptConceptCommision.Name
                    cashReceiptDetailRetention.IdThirdParty = card.IdThirdParty
                    cashReceiptDetailRetention.IdMainAccount = cashReceiptConceptCommision.IdMainAccount
                    cashReceiptDetailRetention.Nature = 1
                    cashReceiptDetailRetention.IdCashReceiptConcept = cashReceiptConceptCommision.Id
                    cashReceiptDetailRetention.Value = Decimal.Round(CDec(paymentMethodAdd.CommissionValue), 2)
                    cashReceiptDetailRetention.IdRetentionConcept = card.IdRetentionConceptCommision
                    cashReceiptDetailRetention.PercentageRetention = paymentMethodAdd.PercentageCommission
                    cashReceiptDetailRetention.BaseValue = paymentMethodAdd.BaseValue
                    cashReceiptDetailRetention.BillingValue = paymentMethodAdd.BaseValue
                    cashReceiptDetailRetention.CardNumber = paymentMethodAdd.CardNumber
                    cashReceiptDetailRetention.CashReceiptConceptAffectation = cashReceiptConceptCommision.Affectation
                    cashReceiptDetailRetention.ValueInCurrencyHeader = cashReceiptDetailRetention.Value
                    AddCashReceiptConcept(cashReceiptDetailRetention)
                End Using
            End Using
        End Using
    End Sub

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
    ''' metodo para establecer el color de la edad de cartera
    ''' </summary>
    ''' <param name="expiredDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetAgeBills(expiredDate As Date) As Integer
        Dim _agesPortfolio As List(Of AgesPortfolio)
        Dim colorAge As Integer = Convert.ToInt32(ePortfolioAge.ColorDefault)
        Dim _dateServer As Date
        Dim setting As SettingPortfolio
        Using model As New MSettingPortfolio(MyTag)
            setting = model.GetSettingPortfolioByIdOperatingUnit(_idOperativeUnit)
            If setting IsNot Nothing AndAlso setting.Id = 0 Then
                Return colorAge
            End If
        End Using
        Using Model As New MAgesPortfolio(Me.Tag)
            _agesPortfolio = (Model.ListAgesPortfolioByIdSettingPortfolio(setting.Id))
        End Using
        _dateServer = Me.GetDateServer
        Dim daysExpire As Integer = (CDate(expiredDate) - _dateServer).TotalDays
        If daysExpire > 0 Then
            colorAge = (From ap In _agesPortfolio Where ap.InitialRange <= daysExpire And ap.EndRange >= daysExpire Select ap.Color).FirstOrDefault()
        End If
        Return colorAge
    End Function

    ''' <summary>
    ''' Obtiene los parametros de facturacion
    ''' </summary>
    Private Async Sub GetSettingsBillingByIdUnitOperative()
        Dim errors As New StringBuilder
        Using model As New MBillingSetting(Me.Tag)
            settingBilling = Await model.GetSettingsBillingByIdUnitOperative(_idOperativeUnit, False)
        End Using
        If settingBilling Is Nothing OrElse settingBilling.Id = 0 Then
            AsyncLoader(False)
            errors.AppendLine("No se encontró un concepto definido en los parámetros de facturación")
        End If
    End Sub

    ''' <summary>
    ''' limpia la secuencia numerica por organizacion 
    ''' </summary>
    Private Sub CleanEditValueBankAndCash()
        Me.IdMainAccount = 0
        If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
            Me._prefixSelected = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Funcion para validar las cajas.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function CashValidate() As Task
        If listCashReceiptdetails?.Any() OrElse listPaymentMethods?.Any() Then
            If IdCashRegister IsNot Nothing AndAlso OldCashRegisterId IsNot Nothing AndAlso (IdCashRegister <> OldCashRegisterId) Then

                Dim NewCashRegister As CashRegisters = Await GetCashRegisterByDictionary(CInt(IdCashRegister))
                Dim OldCashRegister As CashRegisters = Await GetCashRegisterByDictionary(CInt(OldCashRegisterId))

                If If(NewCashRegister?.CurrencyId Is Nothing, indigo.OfficialCurrencyId, NewCashRegister.CurrencyId) <> If(OldCashRegister?.CurrencyId Is Nothing, indigo.OfficialCurrencyId, OldCashRegister.CurrencyId) Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar la caja debido a que ya exiten Métodos de pago con un tipo de moneda diferente."
                    _isFromMessageCashValidate = True
                    INDSleCash.EditValue = OldCashRegisterId
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Se encarga de obtener la caja por medio del diccionario, en caso de que exista
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetCashRegisterByDictionary(Value As Integer) As Task(Of CashRegisters)

        Dim CashRegisterTmp As CashRegisters = Nothing
        If DictionaryCashRegisters.ContainsKey(Value) Then
            CashRegisterTmp = DictionaryCashRegisters(Value)
        Else
            Using model As New MCashRegister(MyTag)
                CashRegisterTmp = Await model.GetcashRegisterById(Value)
            End Using

            If CashRegisterTmp.Id > 0 Then
                DictionaryCashRegisters.Add(Value, CashRegisterTmp)
            End If
        End If

        Return CashRegisterTmp
    End Function

#End Region

#Region "HANDLERS"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        paymentMehtodIsTarget = Nothing
        EditModePaymentMethod = Nothing
        EditModeCashRegisterDetail = Nothing
        _ctrDebitCredit = Nothing
        credit = Nothing
        debit = Nothing
        presenter = Nothing
        record = Nothing
        listTypeCollection = Nothing
        paymentMethodType = Nothing
        listPaymentMethods = Nothing
        listPaymentMethodsDelete = Nothing
        paymentMethods = Nothing
        listCashReceiptdetails = Nothing
        listCashReceiptdetailsDelete = Nothing
        cashReceiptdetails = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _prefixSelected = Nothing
        _idOperativeUnit = Nothing
        cashReceipts = Nothing
        listAccountReceivable = Nothing
        listCashReceiptAdvancePayment = Nothing
        listCashReceiptDetailAccountPayable = Nothing
        _flagLoadControls = Nothing
        subCashReceipts = False
        varImp = Nothing
        _parentCurrencyId = Nothing
        _parentCurrencyAbbreviation = Nothing
        Me._idThirdPartyMainAccount = Nothing
    End Sub

    ''' <summary>
    ''' evento load del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmCashReceipts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDLcCashReceipt, True)
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        _indigoSession = SessionValues.Instance
        INDGleTypeCollection.Properties.DataSource = TypeCollection
        IndigoGridControl1.RefreshGrid(INDGcCashReceiptDetail)
        IndigoGridControl1.RefreshGrid(INDGcMethodPayment)
        IndigoGridControl1.SetControlNextFocus(INDGcMethodPayment, INDBtnAddCashReceiptConcept)
        IndigoGridView21.MoreInfoColunmns(INDGvMethodPayment)
        AddActionsColumns()
        presenter = New PCashReceipts(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        CtrPopupPaymentMethod1.OperatingUnitId = _idOperativeUnit
        If Not subCashReceipts Then
            Me.CurrencyId = indigo.OfficialCurrencyId
            propertyCurrencyISO4217 = indigo.CurrencyISO4217
            Deshacer()
            _ctrDebitCredit.Visible = True
        Else
            Me.CurrencyId = _parentCurrencyId
            Me.propertyCurrencyISO4217 = _parentCurrencyAbbreviation
            _ctrDebitCredit.Visible = False
            AllowControlForSubForm()
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActualizarConfirmar) = True

            Await Task.Factory.StartNew(Sub()
                                            GetSettingsBillingByIdUnitOperative()
                                        End Sub)

        End If
        LoadStatus()
        Me.setFormatControls(propertyCurrencyISO4217)
    End Sub

    ''' <summary>
    ''' se ocultan grupos de formularios dependiendo de dondde se este llamando
    ''' </summary>
    Public Sub AllowControlForSubForm()
        If subCashReceipts Then
            INDLcgCashReceiptConcepts.Visibility = LayoutVisibility.Never
            INDLciResponsiblePaymentSle.Visibility = LayoutVisibility.Never
            INDLciResponsiblePaymentText.Visibility = LayoutVisibility.Never
            INDGleTypeCollection.EditValue = 1
            INDLciDate.Visibility = False
            LoadFirstCash()
            If Not IdCashRegister > 0 Then
                Mensaje(EeventViewerImages.Informacion) = "No se encontró una caja con la moneda para el recibo de caja. Digite de manera manual esta."
                Exit Sub
            End If
            If _sourceDocument = eSourceDocument.ProductSales Then
                INDBteCode.Properties.Buttons(0).Visible = False
                CtrPopupPaymentMethod1.HandleProductInvoice = True
            End If
            If CashDefaultValue > 0 Then
                SetPaymentMethodDatasource()
                InitializePaymentMethod()
            End If
            ActionsOnControls = True
            INDGleTypeCollection.Properties.ReadOnly = False
            INDSleBankAccount.Properties.ReadOnly = False
            INDSleCash.Properties.ReadOnly = False
        End If
    End Sub


    ''' <summary>
    ''' Sets the payment method datasource.
    ''' </summary>
    Private Sub SetPaymentMethodDatasource()
        INDLiCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDLiCash.AllowHide = False
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
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Properties.DataSource = CtrPopupPaymentMethod1.MethodsPaymentsCash
        If paymentMethodType = 4 Then
            CtrPopupPaymentMethod1.EditedValueFromTheForm = True
            CtrPopupPaymentMethod1.INDGlePaymentMethod.EditValue = Nothing
            CtrPopupPaymentMethod1.INDGlePaymentMethod.EditValue = 1
            INDPceMethodPayment.Properties.PopupSizeable = True
            CtrPopupPaymentMethod1.INDBtnAdd.Width = 185
            CtrPopupPaymentMethod1.INDBtnClose.Width = 185
            INDPccPaymentMethod.Size = New Size(384, 200)
            CtrPopupPaymentMethod1.INDBtnAdd.Visible = True
            CtrPopupPaymentMethod1.INDBtnClose.Visible = True
            INDPceMethodPayment.Properties.PopupSizeable = False
        End If
        INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLiCostCenter.AllowHide = True
        INDSleCostCenter.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Initializes the payment method.
    ''' </summary>
    Private Sub InitializePaymentMethod()
        Dim paymentMethods As New PaymentMethods()
        paymentMethods.PaymentMethodTypes = 1
        paymentMethods.Value = CashDefaultValue
        paymentMethods.CurrencyId = Me.CurrencyId
        paymentMethods.ValueInCurrencyHeader = CashDefaultValue
        Dim args As AddPaymentMethodsEventArgs = New AddPaymentMethodsEventArgs
        args.PaymentMethods = paymentMethods
        If CtrPopupPaymentMethod1.listPaymentMethodsAdded Is Nothing Then
            CtrPopupPaymentMethod1.listPaymentMethodsAdded = New List(Of PaymentMethods)
        End If
        CtrPopupPaymentMethod1.listPaymentMethodsAdded.Add(paymentMethods)
        CtrPopupPaymentMethod1_AddPaymentMethods(Nothing, args)
    End Sub


    Private Async Sub LoadFirstCash()
        Using model As New MCashRegister(MyTag)
            Dim cash As CashRegisters = Await model.GetFirstCash(_parentCurrencyId)
            If cash IsNot Nothing AndAlso cash.Id > 0 Then
                IdCashRegister = cash.Id
                INDSleCash.Properties.NullText = String.Concat(cash.Code, " - ", cash.Name)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' metodo que valida si el formulario de recibo de caja es abierto por otro formulario
    ''' tengan la misma moneda del documento padre, habilitado para facturacion basica
    ''' </summary>
    Private Function ValidateParentCurrency() As Boolean
        Return (Not subCashReceipts OrElse _sourceDocument <> eSourceDocument.BasicBilling OrElse Me.CurrencyId = Me._parentCurrencyId)
    End Function

    ''' <summary>
    ''' Valida que los detalles con concepto de retención tengan el BillingValue mayor o igual al BaseValue
    ''' </summary>
    ''' <param name="errors">StringBuilder donde se agregan los mensajes de error</param>
    Private Sub ValidateRetentionBillingValue(ByRef errors As StringBuilder)
        If listCashReceiptdetails IsNot Nothing Then
            For Each detail As CashReceiptDetails In listCashReceiptdetails.Where(Function(x) x.IdRetentionConcept IsNot Nothing AndAlso x.IdRetentionConcept > 0)
                If detail.BillingValue Is Nothing OrElse detail.BillingValue <= 0 OrElse detail.BillingValue < detail.BaseValue Then
                    errors.AppendLine("El valor facturado debe ser mayor o igual al valor base para los conceptos de retención")
                    Exit For
                End If
            Next
        End If
    End Sub

    ''' <summary>
    ''' Valida los campos obligatorios del recibo de caja
    ''' </summary>
    ''' <param name="errors">StringBuilder donde se agregan los mensajes de error</param>
    Private Sub ValidateFields(ByRef errors As StringBuilder)
        If Code Is Nothing Then
            errors.AppendLine("El campo Código es obligatorio")
        End If
        If Not IdThirdParty > 0 Then
            errors.AppendLine("El campo Tercero es obligatorio")
        End If
        If PaymentResponsibles Is Nothing Then
            errors.AppendLine("El campo Responsable de Pago es obligatorio")
        End If
        If Not CollectType > 0 Then
            errors.AppendLine("El campo Tipo de recaudo es obligatorio")
        End If
        If DocumentDate Is Nothing Then
            errors.AppendLine("El campo Fecha del documento es obligatorio")
        End If
        If String.IsNullOrEmpty(debit) Then
            errors.AppendLine("El campo Valor es obligatorio")
        End If
        If INDGvMethodPayment.RowCount = 0 Then
            errors.AppendLine("Se debe agregar mínimo un método de pago")
        End If
        ValidateRetentionBillingValue(errors)
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' evento que se dispara al hacer click en el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                presenter.InitializeThirdPartyXPO()
                INDSleThirdParty.Focus()
            End Using
        End If
    End Sub

    Private Sub INDSleReponsiblePayment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleReponsiblePayment.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmThirdParty
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                presenter.InitializeResponsiblePaymentXPO()
                INDSleReponsiblePayment.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmEntityAccount
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                presenter.InitializeAccountXPO()
                INDSleBankAccount.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el mas del control y abre el funcional solicitado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCash_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCash.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCash
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                presenter.InitializeCashXPO()
                INDSleCash.Focus()
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
                transparent.ShowDialog()
                presenter.InitializeCostCenterXPO()
                INDSleCostCenter.Focus()
            End Using
        End If
    End Sub
#End Region
#Region "EditValueChanging"

    ''' <summary>
    ''' evento que se dispara cuando se esta cambiando la cuenta bancaria
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleBankAccount_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDSleBankAccount.EditValueChanging
        If e.NewValue = 0 Then
            Return
        End If
        If INDSleBankAccount.EditValue IsNot Nothing AndAlso e.NewValue <> INDSleBankAccount.OldEditValue Then
            Using model As New MEntityAccount(MyTag)
                Dim bankAccount = model.GetEntityBankAccountByIdSimple(e.NewValue)
                Dim bankAccountOld = model.GetEntityBankAccountByIdSimple(e.OldValue)
                If bankAccount.CurrencyId <> bankAccountOld.CurrencyId AndAlso (listCashReceiptdetails?.Count > 0 Or listPaymentMethods?.Count > 0) Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se puede cambiar la cuenta bancaria debido a que ya exiten Métodos de pago con un tipo de moneda diferente."
                    e.Cancel = True
                    Exit Sub
                End If
            End Using

        End If
    End Sub
#End Region
#Region "EditValueChanged"
    ''' <summary>
    ''' evento que se dispara cuando cambia el valor del tipo de recaudo y oculta o muestra campos dependiendo del valor seleccionado
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDGleTypeCollection_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTypeCollection.EditValueChanged
        If _flagLoadControls Then
            Exit Sub
        End If
        Dim dateServer = Me.GetDateServer()
        If INDGleTypeCollection.EditValue = 1 Then 'caja
            INDDteDocumentDate.Properties.ReadOnly = True
            INDDteDocumentDate.Properties.MinValue = dateServer
            INDDteDocumentDate.Properties.MaxValue = dateServer
            DocumentDate = dateServer
            Me.IdBankAccount = 0
            INDLiCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLiCash.AllowHide = False

            INDLiAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLiAccount.AllowHide = True
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
                getDebitCredit()
                _ctrDebitCredit.RefreshDebitCredit()
            End If
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Properties.DataSource = CtrPopupPaymentMethod1.MethodsPaymentsCash
        Else 'banco
            INDGcMethodPayment.DataSource = Nothing
            listPaymentMethods = Nothing
            IdCashRegister = Nothing
            INDLiAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLiAccount.AllowHide = False
            INDLiCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLiCash.AllowHide = True
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Properties.DataSource = CtrPopupPaymentMethod1.MethodsPaymentsBank
            Using modelSetting As New MSettingsTreasury(MyTag)
                Dim setting = modelSetting.GetSettingsTreasuryByIdUnitOperativeSimple(BarraBotones.OperatingUnitValue)
                If setting.ObjectEmbbeded.AllowModifyDocumentsDateBank Then
                    INDDteDocumentDate.Properties.ReadOnly = False
                    INDDteDocumentDate.Properties.MinValue = CDate(dateServer).AddDays(-setting.ObjectEmbbeded.NumberDayBank)
                    INDDteDocumentDate.Properties.MaxValue = dateServer
                    DocumentDate = dateServer
                Else
                    INDDteDocumentDate.Properties.ReadOnly = True
                    INDDteDocumentDate.Properties.MinValue = dateServer
                    INDDteDocumentDate.Properties.MaxValue = dateServer
                    DocumentDate = dateServer
                End If
            End Using
        End If
        If paymentMethodType = 4 And INDGleTypeCollection.EditValue = 1 Then
            CtrPopupPaymentMethod1.EditedValueFromTheForm = True
            CtrPopupPaymentMethod1.INDGlePaymentMethod.EditValue = Nothing
            CtrPopupPaymentMethod1.INDGlePaymentMethod.EditValue = 1
            INDPceMethodPayment.Properties.PopupSizeable = True
            CtrPopupPaymentMethod1.INDBtnAdd.Width = 185
            CtrPopupPaymentMethod1.INDBtnClose.Width = 185
            INDPccPaymentMethod.Size = New Size(384, 200)
            CtrPopupPaymentMethod1.INDBtnAdd.Visible = True
            CtrPopupPaymentMethod1.INDBtnClose.Visible = True
            INDPceMethodPayment.Properties.PopupSizeable = False
        End If
        INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLiCostCenter.AllowHide = True
        INDSleBankAccount.EditValue = Nothing
        INDSleCostCenter.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambiar el valor del tercero y lo consulta para portularlo en el campo de responsable de pago
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDSleThirdParty_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleThirdParty.EditValueChanged

        If INDSleThirdParty.EditValue IsNot Nothing Then
            Dim third() As String = Nothing
            If subCashReceipts Then
                Using modelCashReceiptConcept As New MCashReceiptsConcepts(MyTag)
                    Dim thirdPartyInfo = modelCashReceiptConcept.GetThirdPartyByIdSimple(IdThirdParty)
                    If thirdPartyInfo IsNot Nothing Then
                        INDSleThirdParty.Properties.NullText = thirdPartyInfo.Nit + "-" + thirdPartyInfo.Name
                    End If
                End Using
            End If
            If INDSleThirdParty.Properties.NullText IsNot String.Empty Then
                third = INDSleThirdParty.Properties.NullText.Split("-")
            Else
                third = INDSleThirdParty.Text.Split("-")
            End If
            If _flagLoadControls = False Then
                INDBtnAddCashReceiptConcept.Enabled = True
                If third.Length > 1 Then
                    PaymentResponsibles = third(1).Trim()
                End If
                IdResponsiblePayment = INDSleThirdParty.EditValue
            End If
        Else
            PaymentResponsibles = Nothing
            INDBtnAddCashReceiptConcept.Enabled = False
        End If
    End Sub

    Private Async Sub INDSleBankAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBankAccount.EditValueChanged
        If Me.IdBankAccount = 0 Then
            Exit Sub
        End If
        Dim errors As New StringBuilder
        If _flagLoadControls Then
            AccountOrCashCurrency(0, 1)
            Exit Sub
        End If

        Me.CleanEditValueBankAndCash()

        If INDSleBankAccount.EditValue IsNot Nothing Then
            Using model As New MEntityAccount(MyTag)
                Dim bankAccount = model.GetEntityBankAccountByIdSimple(INDSleBankAccount.EditValue)
                If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                    Me._idCurrentSequence = Me.GetIdSequenceByPrefix(bankAccount.Prefix)
                    Me._prefixSelected = bankAccount.Prefix
                End If
                Using modelPuc As New MPUC(MyTag)
                    Dim account = Await modelPuc.GetAccountById(bankAccount.IdMainAccount, False)
                    IdMainAccount = account.Id
                    If account.HandlesCostCenter = True Then
                        INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLiCostCenter.AllowHide = False
                    Else
                        INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLiCostCenter.AllowHide = True
                    End If
                End Using
                AccountOrCashCurrency(0, 0)
                CtrPopupPaymentMethod1.BankAccountId = INDSleBankAccount.EditValue
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambiar el valor de la caja y consulta si la cuenta de la caja requiere centro de costo para ponerlo visible o invisible
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDSleCash_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCash.EditValueChanged

        If _isFromMessageCashValidate OrElse Me.INDGleTypeCollection.EditValue <> 1 Then
            _isFromMessageCashValidate = False
            Exit Sub
        End If

        Dim errors As New StringBuilder
        If _flagLoadControls Then
            AccountOrCashCurrency(1, 1)
            Exit Sub
        End If

        Me.CleanEditValueBankAndCash()

        If IdCashRegister IsNot Nothing Then
            Using model As New MCashRegister(MyTag)
                Await CashValidate()
                Dim cashRegister As CashRegisters = Await GetCashRegisterByDictionary(IdCashRegister)
                If Me._sequence.Scope.Equals("O") Then
                    Me._idCurrentSequence = Me.GetIdSequenceByPrefix(cashRegister.Prefix)
                    Me._prefixSelected = cashRegister.Prefix
                End If

                Using modelPUC As New MPUC(MyTag)
                    Dim account = Await modelPUC.GetAccountById(cashRegister.IdMainAccount, False)
                    IdMainAccount = account.Id
                    If account.HandlesCostCenter = True Then
                        INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDLiCostCenter.AllowHide = False
                    Else
                        INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDLiCostCenter.AllowHide = True
                    End If
                End Using
                AccountOrCashCurrency(1, 0)
            End Using
        End If
        OldCashRegisterId = IdCashRegister
    End Sub
    ''' <summary>
    ''' cuando cambia la fecha se consulta el dia habil para colocarla el metodo de pago consignacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDDteDocumentDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDteDocumentDate.EditValueChanged
        If _flagLoadControls Then
            Exit Sub
        End If
        If cashReceipts IsNot Nothing AndAlso cashReceipts.Status > 1 Then
            Exit Sub
        End If
        If DocumentDate IsNot Nothing Then
            Using Model As New MDocumentAccount(Me.Tag)
                If Not Await Model.ValidatePeriod(DocumentDate.Value.Month, DocumentDate.Value.Year) Then
                    If cashReceipts IsNot Nothing AndAlso cashReceipts.Id = 0 AndAlso cashReceipts.Status < 2 Then
                        Dim periods As List(Of ClosedMonth) = Model.GetOpenPeriod()
                        If periods IsNot Nothing AndAlso periods.Count > 0 Then
                            Dim openPeriod As String = AccountingServices.GetListOpenPeriods(periods)
                            Mensaje(EeventViewerImages.Advertencia) = openPeriod
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoOpenPeriods", MODULE_NAME)
                        End If
                        CtrPopupPaymentMethod1.DepositDate = Nothing
                        CtrPopupPaymentMethod1.INDDteDepositDate.EditValue = Nothing
                        CtrPopupPaymentMethod1.INDDteDepositDate.Properties.MinValue = Nothing
                        DocumentDate = Nothing
                    End If
                Else
                    GetBusinessDay()
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Setea los controles al formato preestablecido
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Private Sub setFormatControls(Abbreviation As String)
        INDSleCurrency.Properties.NullText = $"{Abbreviation}"
        Me.ColumnValuePaymentMethod = Window.Utils.FormatGrid(ColumnValuePaymentMethod, Abbreviation)
        Me.ColumnValueConcept = Window.Utils.FormatGrid(ColumnValueConcept, Abbreviation)
        _ctrDebitCredit.CodeISO4217 = Abbreviation
        _ctrDebitCredit.RefreshDebitCredit()
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' evento que se dispara al presionar click en el popup y asigna el tag del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDPceMethodPayment_Click(sender As Object, e As EventArgs) Handles INDPceMethodPayment.Click
        If INDGleTypeCollection.EditValue Is Nothing Then
            INDPceMethodPayment.ClosePopup()
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmCashReceipts_DontSelected_TypeCollection", MODULE_NAME)
            INDGleTypeCollection.Focus()
            Exit Sub
        End If
        If INDSleBankAccount.Visible = True And INDSleBankAccount.EditValue Is Nothing Then
            INDPceMethodPayment.ClosePopup()
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmCashReceipts_DontSelected_BankAccount", MODULE_NAME)
            INDSleBankAccount.Focus()
            Exit Sub
        End If

        If INDSleCash.Visible = True And IdCashRegister Is Nothing Then
            INDPceMethodPayment.ClosePopup()
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmCashReceipts_DontSelected_Cash", MODULE_NAME)
            INDSleCash.Focus()
            Exit Sub
        End If

        CtrPopupPaymentMethod1.listPaymentMethodsAdded = listPaymentMethods
        CtrPopupPaymentMethod1.TagForm = Me.MyTag
        If (CtrPopupPaymentMethod1.cashReceiptsCurrencyId <> Me.CurrencyId) Then
            CtrPopupPaymentMethod1.cashReceiptsCurrencyId = Me.CurrencyId
            CtrPopupPaymentMethod1.HideAllLabels()
        End If
        CtrPopupPaymentMethod1.cashReceiptsCurrencyAbrreviation = propertyCurrencyISO4217
        CtrPopupPaymentMethod1.reloadFormatCurrency()
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
    End Sub

    ''' <summary>
    ''' evento que instancia el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAddCashReceiptConcept.Click
        InstantiatePopup(Nothing, listCashReceiptdetails, Nothing)
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' evento que se dispara al presionar enter en el control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCostCenter_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSleCostCenter.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceMethodPayment.Focus()
            INDPceMethodPayment.ShowPopup()
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar enter en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAccount_INDSleCash_KeyDown(sender As Object, e As KeyEventArgs) Handles INDSleBankAccount.KeyDown, INDSleCash.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDLiCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDSleCostCenter.Focus()
            Else
                INDDteDocumentDate.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar enter en el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleTypeCash_KeyDown(sender As Object, e As KeyEventArgs) Handles INDGleTypeCollection.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDGleTypeCollection.EditValue IsNot Nothing Then
                If INDLiAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    INDSleBankAccount.Focus()
                Else
                    INDSleCash.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se dispara al presionar enter y da el foco al control requerido
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDPceMethodPayment_KeyDown(sender As Object, e As KeyEventArgs) Handles INDPceMethodPayment.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceMethodPayment.Focus()
            INDPceMethodPayment.ShowPopup()
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
        End If
    End Sub


    ''' <summary>
    ''' evento que se dispara cuando se presiona enter en el control y valida si el campo estavacio para dar paso a la secuencia numerica
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDBteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBteCode.KeyDown
        If BarraBotones.PermissionsForm.ContainsKey(2) Or BarraBotones.PermissionsForm.ContainsKey(144) Then

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
                        Await Me.NewCashReceipt()
                    Else
                        Await Me.LoadControls()
                    End If
                End If
            ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
                OpenSearch()
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = "El usuario no tiene permisos"
        End If
    End Sub
    ''' <summary>
    ''' evento que al presionar enter instancia el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnAddCashReceiptConcept.KeyDown
        If e.KeyCode = Keys.Enter Then
            InstantiatePopup(Nothing, listCashReceiptdetails, Nothing)
        End If
    End Sub
#End Region

#Region "CloseUp"

    ''' <summary>
    ''' evento que se dispara al cerrar el popup y si se esta editando un registro limpia los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDPceMethodPayment_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceMethodPayment.CloseUp
        If EditModePaymentMethod = True Then
            paymentMethods = Nothing
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Enabled = True
            CtrPopupPaymentMethod1.INDSleCard.Properties.ReadOnly = False
            CtrPopupPaymentMethod1.INDSleCard.Properties.Buttons(0).Enabled = True
            CtrPopupPaymentMethod1.CleanControls()
            EditModePaymentMethod = False
        End If

        CtrPopupPaymentMethod1.ClosePopupChangePaymentMethod = False
        If e.CloseMode = DevExpress.XtraEditors.PopupCloseMode.Cancel Then
            INDBtnAddCashReceiptConcept.Focus()
        End If
        If paymentMehtodIsTarget = True Then
            INDPceMethodPayment.Focus()
            INDPceMethodPayment.ShowPopup()
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
            paymentMehtodIsTarget = False
        End If

        ' Volver a deshabilitar el popup y restaurar los botones si está en modo confirmado/anulado
        If cashReceipts IsNot Nothing AndAlso Not {0, 1}.Contains(cashReceipts.Status) Then
            INDPceMethodPayment.Properties.ReadOnly = True
            CtrPopupPaymentMethod1.INDBtnAdd.Enabled = True
            CtrPopupPaymentMethod1.INDBtnClose.Enabled = True
        End If
    End Sub

#End Region

#Region "ResizePopup"
    ''' <summary>
    ''' evento que reorganiza el popup y lo dibuja de acuerdo a los parametros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrPopupPaymentMethod1_ResizePopup(sender As Object, e As ResizePopupEventArgs) Handles CtrPopupPaymentMethod1.ResizePopup
        Select Case e.PaymentMethodType
            Case 1, 3, 4
                SettingPopupPaymentMethods(e.PaymentMethodType, 320, 320, 840, 600, True, True)
            Case 2, 5
                SettingPopupPaymentMethods(e.PaymentMethodType, 320, 320, 840, 650, True, True)
            Case Else
                SettingPopupPaymentMethods(e.PaymentMethodType, 270, 270, 740, 540, True, True)
        End Select
        CtrPopupPaymentMethod1.INDPcButtons.Visible = True
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
    End Sub
#End Region

#Region "OpenPopup"
    ''' <summary>
    ''' evento del control de usuario para desplegar el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrPopupPaymentMethod1_OpenPopup(sender As Object, e As EventArgs) Handles CtrPopupPaymentMethod1.OpenPopup
        INDPceMethodPayment.ShowPopup()
    End Sub

#End Region

#Region "ClosePopup"
    ''' <summary>
    ''' evento del control de metodo de pago para cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CtrPopupPaymentMethod1_ClosePopup(sender As Object, e As EventArgs) Handles CtrPopupPaymentMethod1.ClosePopup
        INDPceMethodPayment.ClosePopup()
        INDBtnAddCashReceiptConcept.Focus()
    End Sub

#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' evento que dispara la consulta al desplegar el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleThirdParty.QueryPopUp
        If INDSleThirdParty.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ThirdPartyXPO Is Nothing Then
            presenter.InitializeThirdPartyXPO()
        End If
    End Sub

    Private Sub INDSleReponsiblePayment_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleReponsiblePayment.QueryPopUp
        If INDSleReponsiblePayment.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If ResponsiblePaymentXPO Is Nothing Then
            presenter.InitializeResponsiblePaymentXPO()
        End If
    End Sub

    ''' <summary>
    ''' evento que dispara la consulta al desplegar el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBankAccount.QueryPopUp
        If INDSleBankAccount.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If AccountXPO Is Nothing Then
            presenter.InitializeAccountXPO()
        End If
    End Sub

    ''' <summary>
    ''' evento que dispara la consulta al desplegar el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCash_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCash.QueryPopUp
        If INDSleCash.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CashXPO Is Nothing Then
            presenter.InitializeCashXPO()
        End If
    End Sub

    ''' <summary>
    ''' evento que dispara la consulta al desplegar el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If INDSleCostCenter.Properties.ReadOnly = True Then
            Exit Sub
        End If
        If CostCenterXPO Is Nothing Then
            presenter.InitializeCostCenterXPO()
        End If
    End Sub

#End Region

#Region "Click_ButtonAction"
    ''' <summary>
    ''' evento de la columna de acciones de métodos de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim isConfirmed As Boolean = (cashReceipts IsNot Nothing AndAlso (cashReceipts.Status <> 0 AndAlso cashReceipts.Status <> 1))
        Select Case sender.Tag.ToString
            Case "Edit"
                paymentMethods = DirectCast(INDGvMethodPayment.GetFocusedRow, PaymentMethods)
                CtrPopupPaymentMethod1.cashReceiptsCurrencyId = CurrencyId
                EditModePaymentMethod = True
                ' Habilitar temporalmente el popup si está en modo confirmado/anulado
                If isConfirmed Then
                    INDPceMethodPayment.Properties.ReadOnly = False
                    ' Bloquear los botones de agregar y limpiar en el popup
                    CtrPopupPaymentMethod1.INDBtnAdd.Enabled = False
                    CtrPopupPaymentMethod1.INDBtnClose.Enabled = False
                End If
                INDPceMethodPayment.Focus()
                CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
                CtrPopupPaymentMethod1.LoadControlsForEdit(paymentMethods)
                INDPceMethodPayment.ShowPopup()
            Case "Remove"
                ' No permitir eliminar cuando está confirmado o anulado
                If isConfirmed Then
                    Exit Sub
                End If
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    paymentMethods = DirectCast(INDGvMethodPayment.GetFocusedRow, PaymentMethods)
                    If paymentMethods.IdCard IsNot Nothing Then
                        If listCashReceiptdetailsDelete Is Nothing Then
                            listCashReceiptdetailsDelete = New List(Of CashReceiptDetails)
                        End If
                        If listCashReceiptdetails IsNot Nothing Then
                            listCashReceiptdetailsDelete.AddRange(listCashReceiptdetails.FindAll(Function(x) x.CardNumber = paymentMethods.CardNumber))
                            listCashReceiptdetails.RemoveAll(Function(x) x.CardNumber = paymentMethods.CardNumber)
                        End If
                        INDGcCashReceiptDetail.DataSource = Nothing
                        INDGcCashReceiptDetail.DataSource = listCashReceiptdetails
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
                    getDebitCredit()
                    _ctrDebitCredit.RefreshDebitCredit()
                    If listPaymentMethods.Count = 0 Then
                        INDGleTypeCollection.Properties.ReadOnly = False
                        INDSleBankAccount.Properties.ReadOnly = False
                        INDSleCash.Properties.ReadOnly = False
                        INDGvMethodPayment.OptionsView.ShowFooter = False
                        IndigoGridControl1.RefreshGrid(INDGcMethodPayment)
                        INDGvMethodPayment.OptionsFind.AlwaysVisible = False
                        IndigoGridControl1.SetExportButton(INDGcMethodPayment, False)
                    Else
                        'INDGvMethodPayment.OptionsFind.AlwaysVisible = True
                        'IndigoGridControl1.SetExportButton(INDGcMethodPayment, True)
                    End If
                End If
        End Select
    End Sub

    ''' <summary>
    ''' evento de la columna de acciones de conceptos de recibo de caja
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Dim isConfirmed As Boolean = (cashReceipts IsNot Nothing AndAlso (cashReceipts.Status <> 0 AndAlso cashReceipts.Status <> 1))
        Select Case sender.Tag.ToString
            Case "Edit"
                cashReceiptdetails = DirectCast(INDGvCashReceiptDetail.GetFocusedRow, CashReceiptDetails)
                If cashReceiptdetails.CashReceiptConceptAffectation = 2 Then

                    Dim portfolioAdvanceTmp As PortfolioAdvance = Nothing
                    If cashReceiptdetails.Id > 0 Then
                        portfolioAdvanceTmp = cashReceipts.PortfolioAdvance.Where(Function(x) x.CashReceiptDetailId = cashReceiptdetails.Id).FirstOrDefault()
                    Else
                        portfolioAdvanceTmp = cashReceipts.PortfolioAdvance.ToList().Find(Function(x) x.CashReceiptDetails IsNot Nothing AndAlso x.CashReceiptDetails.Equals(cashReceiptdetails))

                    End If


                    InstantiatePopup(cashReceiptdetails, listCashReceiptdetails, portfolioAdvanceTmp, isConfirmed)

                Else
                    InstantiatePopup(cashReceiptdetails, listCashReceiptdetails, Nothing, isConfirmed)
                End If
                cashReceiptdetails = Nothing

            Case "Remove"
                ' No permitir eliminar cuando está confirmado o anulado
                If isConfirmed Then
                    Exit Sub
                End If
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    cashReceiptdetails = DirectCast(INDGvCashReceiptDetail.GetFocusedRow, CashReceiptDetails)
                    If cashReceiptdetails.Id > 0 Then
                        MarkAsDeleteCashReceiptDetail(cashReceiptdetails)
                        If listCashReceiptdetailsDelete Is Nothing Then
                            listCashReceiptdetailsDelete = New List(Of CashReceiptDetails)
                        End If
                        listCashReceiptdetailsDelete.Add(cashReceiptdetails)
                    End If
                    listCashReceiptdetails.Remove(cashReceiptdetails)
                    INDGcCashReceiptDetail.DataSource = Nothing
                    INDGcCashReceiptDetail.DataSource = listCashReceiptdetails
                    cashReceiptdetails = Nothing
                    getDebitCredit()
                    _ctrDebitCredit.RefreshDebitCredit()
                    If listCashReceiptdetails.Count = 0 Then
                        INDGvCashReceiptDetail.OptionsView.ShowFooter = False
                        IndigoGridControl1.RefreshGrid(INDGcCashReceiptDetail)
                    End If
                End If
        End Select
    End Sub
#End Region

#Region "AddPaymentMethods"
    ''' <summary>
    ''' evento que se dispara cuando agrego un metodo de pago
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub CtrPopupPaymentMethod1_AddPaymentMethods(sender As Object, e As AddPaymentMethodsEventArgs) Handles CtrPopupPaymentMethod1.AddPaymentMethods

        If listPaymentMethods Is Nothing Then
            listPaymentMethods = New List(Of PaymentMethods)
        End If
        If subCashReceipts Then
            If _sourceDocument = eSourceDocument.ProductSales OrElse _sourceDocument = eSourceDocument.BasicBilling Then
                Dim valuePaymentsMethods As Decimal
                If listPaymentMethods IsNot Nothing AndAlso listPaymentMethods.Count > 0 Then
                    Dim previousValue = If(paymentMethods Is Nothing, 0, paymentMethods.Value)
                    valuePaymentsMethods = listPaymentMethods.Sum(Function(x) x.Value) + e.PaymentMethods.Value - previousValue
                Else
                    valuePaymentsMethods = e.PaymentMethods.Value
                End If

                If valuePaymentsMethods > _valueProductInvoice Then
                    Dim symbolCurrency As String = ""

                    Using model As New Presentation.Common.MVP.MCurrency(MyTag)
                        Dim currency = Await model.GetCurrencyById(CurrencyId)
                        If currency IsNot Nothing AndAlso currency.ISO4217 IsNot Nothing Then
                            symbolCurrency = currency.ISO4217.CodeAbbreviation.GetCurrencySimbol()
                        End If
                    End Using

                    Mensaje(EeventViewerImages.Advertencia) =
                        $"El metodo de pago no se puede agregar porque supera el valor {symbolCurrency} {_valueProductInvoice:N0} de la factura"

                    Exit Sub
                End If

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
                If listCashReceiptdetailsDelete Is Nothing Then
                    listCashReceiptdetailsDelete = New List(Of CashReceiptDetails)
                End If
                If paymentMethods IsNot Nothing Then
                    listCashReceiptdetailsDelete.AddRange(listCashReceiptdetails.FindAll(Function(x) x.CardNumber = paymentMethods.CardNumber))
                End If
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

        'si la forma de pago es efectivo solo permite una vez.
        If e.PaymentMethods.PaymentMethodTypes = 1 And listPaymentMethods.Where(Function(x) x.PaymentMethodTypes = 1).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmCashReceipts_SelectedAgain_Cash", MODULE_NAME)
            Exit Sub
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
        INDGleTypeCollection.Properties.ReadOnly = True
        INDSleBankAccount.Properties.ReadOnly = True
        INDSleCash.Properties.ReadOnly = True
        getDebitCredit()
        _ctrDebitCredit.RefreshDebitCredit()
        INDPceMethodPayment.Focus()
        CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        ReadOnlyControls(False)
        If cashReceipts IsNot Nothing AndAlso cashReceipts.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            LoadControls()
        End If
        Me.IdEntity = String.Empty
        If INDBteCode.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
    End Sub
#End Region

#Region "FormClosing"
    ''' <summary>
    ''' evento que se dispara al cerrar el formulario y elimina el registro bloqueado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCashReceipts_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "ShowingEditor"
    Private Sub INDGvCashReceiptDetail_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDGvCashReceiptDetail.ShowingEditor
        Dim detail As CashReceiptDetails = INDGvCashReceiptDetail.GetFocusedRow()
        If detail.CardNumber IsNot Nothing And detail.CashReceiptConceptAffectation = 1 Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Popup"
    Private Sub INDPceMethodPayment_Popup(sender As Object, e As EventArgs) Handles INDPceMethodPayment.Popup
        INDGvMethodPayment.OptionsFind.AlwaysVisible = False
        IndigoGridControl1.SetExportButton(INDGcMethodPayment, False)
    End Sub
#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al abrir el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Text Is String.Empty Then
            INDBteCode.Focus()
        End If
    End Sub

#End Region

#End Region

#Region "Enum"
    ''' <summary>
    ''' Enumeración Documento de Origen
    ''' </summary>
    Public Enum eSourceDocument
        Liquidation = 1
        ProductSales = 2
        BasicBilling = 3
    End Enum
#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        AsyncLoader(True)
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click confirmar.
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        AsyncLoader(True)
        If Me.subCashReceipts Then
            Confirmar()
        Else
            SaveAndConfirm()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        If Not subCashReceipts Then
            Deshacer()
        End If
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        AsyncLoader(True)
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
        '    If Me._sequence.TreasurySequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequence.TreasurySequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
        '        CtrPopupPaymentMethod1.OperatingUnitId = _idOperativeUnit
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If
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

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        AsyncLoader(True)
        SaveAndConfirm()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        AsyncLoader(True)
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

            cashReceipts.Status = 3
            varImp = 3
            Guardar()
        Else
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, cashReceipts.Id, 0, cashReceipts.Id)
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        AsyncLoader(True)
        SaveAndConfirm()
    End Sub

    Private Sub task_doWork(sender As Object, e As DoWorkEventArgs)
        INDGvBillsMoreInfo.ShowLoadingPanel()
        For Each item In listCashReceiptdetails
            If e.Cancel Then
                Exit Sub
            End If
            Select Case item.CashReceiptConceptAffectation
                Case 3
                    INDGvAdvanceMoreInfo.ShowLoadingPanel()
                    For Each advance In item.CashReceiptAdvancePayment
                        Using modelAdvance As New MAdvancePayments(MyTag)
                            Dim advancePayment = modelAdvance.GetAdvancePaymentsById(advance.AdvancePaymentId)
                            advance.Code = advancePayment.Code
                            advance.Value = advancePayment.Value
                            advance.Balance = advancePayment.Balance
                        End Using
                        If listCashReceiptAdvancePayment Is Nothing Then
                            listCashReceiptAdvancePayment = New List(Of CashReceiptAdvancePayment)
                        End If
                        If taskSetMoreInfo.CancellationPending Then
                            e.Cancel = True
                            Exit Sub
                        End If
                        listCashReceiptAdvancePayment.Add(advance)
                    Next
                Case 4
                    INDGvAccountPayable.ShowLoadingPanel()
                    For Each AccountPayable In item.CashReceiptDetailAccountPayable
                        Using modelAccountPayable As New MAccountPayable(MyTag)
                            Dim accountPayableTmp = modelAccountPayable.GetAccountPayableById(AccountPayable.AccountPayableId)
                            AccountPayable.BillNumber = accountPayableTmp.BillNumber
                            AccountPayable.MaximunRefundValue = accountPayableTmp.Value - accountPayableTmp.Balance
                            AccountPayable.Balance = accountPayableTmp.Balance
                        End Using
                        If listCashReceiptDetailAccountPayable Is Nothing Then
                            listCashReceiptDetailAccountPayable = New List(Of CashReceiptDetailAccountPayable)
                        End If
                        If taskSetMoreInfo.CancellationPending Then
                            e.Cancel = True
                            Exit Sub
                        End If

                        listCashReceiptDetailAccountPayable.Add(AccountPayable)


                    Next

            End Select
        Next
    End Sub

    Private Sub task_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        INDGvBillsMoreInfo.HideLoadingPanel()
        'INDTcgBillAdvances.SelectedTabPageIndex = 0
        IndigoGridControl1.RefreshGrid(INDGcBillsMoreInfo)
        IndigoGridControl1.RefreshGrid(INDGcAdvancesMoreInfo)
        INDGvBillsMoreInfo.HideLoadingPanel()

        INDGvAccountPayable.HideLoadingPanel()
        INDGcAccountPayable.DataSource = Nothing
        INDGcAccountPayable.DataSource = listCashReceiptDetailAccountPayable

        INDGcAdvancesMoreInfo.DataSource = Nothing
        INDGcAdvancesMoreInfo.DataSource = listCashReceiptAdvancePayment
        'INDTcgBillAdvances.SelectedTabPageIndex = 1
        IndigoGridControl1.RefreshGrid(INDGcBillsMoreInfo)
        INDGvAdvanceMoreInfo.HideLoadingPanel()
    End Sub

    Private Function LoadBillsMoreInfo() As Task
        Return Task.Factory.StartNew(Sub()
                                         For Each item In listCashReceiptdetails
                                             Select Case item.CashReceiptConceptAffectation
                                                 Case 2
                                                     For Each bill In item.CashReceiptAccountReceivable
                                                         If listAccountReceivable Is Nothing Then
                                                             listAccountReceivable = New List(Of AccountReceivable)
                                                         End If
                                                         Dim accountReceivable As AccountReceivable
                                                         Using modelAccountReceivable As New MAccountReceivable(MyTag)
                                                             accountReceivable = modelAccountReceivable.GetAccountReceivableById(bill.AccountReceivableId)
                                                             accountReceivable.PaymentValue = bill.Value
                                                             If accountReceivable.AccountReceivableAccounting.Count > 0 Then
                                                                 accountReceivable.Balance = accountReceivable.AccountReceivableAccounting.Where(Function(x) x.AccountReceivableId = accountReceivable.Id And x.MainAccountId = item.IdMainAccount).FirstOrDefault().Balance
                                                             Else
                                                                 accountReceivable.Balance = 0
                                                             End If
                                                             accountReceivable.Age = SetAgeBills(accountReceivable.ExpiredDate)
                                                         End Using
                                                         listAccountReceivable.Add(accountReceivable)
                                                     Next
                                                 Case 3
                                                     For Each advance In item.CashReceiptAdvancePayment
                                                         Using modelAdvance As New MAdvancePayments(MyTag)
                                                             Dim advancePayment = modelAdvance.GetAdvancePaymentsById(advance.AdvancePaymentId)
                                                             advance.Code = advancePayment.Code
                                                             advance.Value = advancePayment.Value
                                                             advance.Balance = advancePayment.Balance
                                                         End Using
                                                         If listCashReceiptAdvancePayment Is Nothing Then
                                                             listCashReceiptAdvancePayment = New List(Of CashReceiptAdvancePayment)
                                                         End If
                                                         listCashReceiptAdvancePayment.Add(advance)
                                                     Next
                                             End Select

                                         Next
                                     End Sub)

    End Function

    Private Sub INDRpPceMoreInfo_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRpPceMoreInfo.QueryPopUp
        INDGcBillsMoreInfo.DataSource = Nothing

        cashReceiptdetails = DirectCast(INDGvCashReceiptDetail.GetFocusedRow, CashReceiptDetails)
        If cashReceiptdetails.CashReceiptConceptAffectation = 2 Then
            Dim portfolioAdvanceTmp As PortfolioAdvance = Nothing
            If cashReceiptdetails.Id > 0 Then
                portfolioAdvanceTmp = cashReceipts.PortfolioAdvance.Where(Function(x) x.CashReceiptDetailId IsNot Nothing AndAlso x.CashReceiptDetailId = cashReceiptdetails.Id).FirstOrDefault()
            Else
                portfolioAdvanceTmp = cashReceipts.PortfolioAdvance.ToList().Find(Function(x) x.CashReceiptDetails IsNot Nothing AndAlso x.CashReceiptDetails.Equals(cashReceiptdetails))
            End If
            If portfolioAdvanceTmp IsNot Nothing Then
                INDTxtAdvanceValue.EditValue = portfolioAdvanceTmp.Value
                INDTxtAdmission.EditValue = portfolioAdvanceTmp.AdmissionNumber
                INDMeAdvanceDetail.Text = portfolioAdvanceTmp.Observations
            End If
        End If
    End Sub

    Private Sub INDDteDocumentDate_KeyDown(sender As Object, e As KeyEventArgs) Handles INDDteDocumentDate.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDPceMethodPayment.Focus()
            INDPceMethodPayment.ShowPopup()
            CtrPopupPaymentMethod1.INDGlePaymentMethod.Focus()
        End If
    End Sub

    Private Sub INDRpPceMoreInfo_Popup(sender As Object, e As EventArgs) Handles INDRpPceMoreInfo.Popup
        Dim detailTmp = DirectCast(INDGvCashReceiptDetail.GetFocusedRow, CashReceiptDetails)
        Select Case detailTmp.CashReceiptConceptAffectation
            Case 1
                INDTcgBillAdvances.SelectedTabPageIndex = 3
            Case 2
                INDTcgBillAdvances.SelectedTabPageIndex = 0
                If INDGcBillsMoreInfo.DataSource Is Nothing Then
                    Using model As New MCashReceipts(MyTag)

                        IndigoGridControl1.AcceptXPO = True
                        INDGcBillsMoreInfo.DataSource = model.GetCashReceiptAccountReceivableByCashReceiptDetailId(detailTmp.Id)
                    End Using
                End If
            Case 3
                INDTcgBillAdvances.SelectedTabPageIndex = 1
            Case 4
                INDTcgBillAdvances.SelectedTabPageIndex = 2
        End Select
    End Sub

    ''' <summary>
    ''' Se ejecuta para que valide si puede o no cambiar la caja o cuenta bancaria en caso de que ya tenga detalles o no
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub AccountOrCashCurrency(flagType As Boolean, flagEdit As Boolean)
        Me.CurrencyId = indigo.OfficialCurrencyId
        propertyCurrencyISO4217 = indigo.CurrencyISO4217
        Try
            If flagType Then
                Using model As New MCashRegister(MyTag)
                    Dim cashregister As CashRegisters = Await GetCashRegisterByDictionary(IdCashRegister)
                    If cashregister?.CurrencyId Is Nothing OrElse cashregister?.CurrencyId = 0 Then
                        Exit Sub
                    End If

                    Me.CurrencyId = cashregister.CurrencyId
                    propertyCurrencyISO4217 = cashregister.CurrencyName
                End Using
            Else
                Using model As New MEntityAccount(MyTag)

                    Dim bankAccount = model.GetEntityBankAccountByIdSimple(INDSleBankAccount.EditValue)
                    If bankAccount?.CurrencyId Is Nothing OrElse bankAccount?.CurrencyId = 0 Then
                        Exit Sub
                    End If

                    Me.CurrencyId = bankAccount.CurrencyId
                    propertyCurrencyISO4217 = bankAccount.CurrencyAbbreviation
                End Using
            End If
        Catch ex As Exception
        Finally
            setFormatControls(propertyCurrencyISO4217)
        End Try
    End Sub

    Private Sub INDSleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.EditValueChanged
        Me.setFormatControls(propertyCurrencyISO4217)
    End Sub

#End Region

End Class