'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 20-06-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
' Copyright        : (c) . All rights reserved.'

'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Controls
Imports Presentation.Treasury.MVP
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Presentation.Accounting.MVP
Imports Presentation.Common.MVP
Imports Presentation.Common
Imports Infrastructure.Data.Xpo
Imports Presentation.Portfolio.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository

#End Region

Public Class CtrPopupPaymentMethod

#Region "PUBLIC EVENTS"
    ''' <summary>
    ''' evento publico para cambiar el tamaño del popup al elegir una forma de pago
    ''' </summary>
    Public Event ResizePopup(sender As Object, e As ResizePopupEventArgs)
    ''' <summary>
    ''' evento publico que sirve para abrir el popup cuando un formulario de agregar registros se cierre
    ''' </summary>
    Public Event OpenPopup(sender As Object, e As EventArgs)
    ''' <summary>
    ''' Evento publico para agregar un metodo de pago
    ''' </summary>    
    Public Event AddPaymentMethods(sender As Object, e As AddPaymentMethodsEventArgs)
    ''' <summary>
    ''' Evento publico para cerra el popup cuando se precione el boton cerrar del control
    ''' </summary>    
    Public Event ClosePopup(sender As Object, e As EventArgs)
    ''' <summary>
    ''' evento para disparar todos los key down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event KeyDownAll(sender As Object, e As KeyEventArgs)
#End Region

#Region "GLOBLAS"
    Dim card As Cards
    ''' <summary>
    ''' entidad de metodo de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim PaymentMethods As PaymentMethods
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Treasury"
    ''' <summary>
    ''' lista de los metodos de pago para caja
    ''' </summary>
    Dim listMethodsPaymentCash As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' lista de los metodos de pago para banco
    ''' </summary>
    Dim listMethodsPaymentBank As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' lista de los tipos de consignacion
    ''' </summary>
    Dim listConsignmentType As List(Of Tuple(Of Byte, String))

    Dim indigo As SessionValues = SessionValues.Instance

    Dim editMode As Boolean

    ''' <summary>
    ''' obtiene la consulta por id del convenio de redencion de puntos
    ''' </summary>
    Private AgreementsRedemptionPointsData As XPCollection

    ''' <summary>
    ''' obtiene la informacion del convenio de redencion de puntos
    ''' </summary>
    Private AgreementsRedemptionPoints As AgreementsRedemptionPoints

    ''' <summary>
    ''' obtiene los detalles del convenio de redencion de puntos por id
    ''' </summary>
    Private AgreementsRedemptionPointsDetail As XPCollection(Of AgreementsRedemptionPointsDetailXpo)

    ''' <summary>
    ''' valor que tiene cada punto segun el convenio
    ''' </summary>
    Dim PointsValue As Decimal

    ''' <summary>
    ''' cantidad de puntos del el convenio
    ''' </summary>
    Dim PointsAmount As Decimal

#End Region

#Region "PROPERTIES"
    Dim _closePopupChangePaymentMethod As Boolean
    Public Property ClosePopupChangePaymentMethod As Boolean
        Get
            Return _closePopupChangePaymentMethod
        End Get
        Set(value As Boolean)
            _closePopupChangePaymentMethod = value
        End Set
    End Property

    Dim _depositDate As Date?
    ''' <summary>
    ''' obtiene o establece la fecha habil para la consignacion dependiendo de la fecha del recibo de caja
    ''' </summary>
    Public Property DepositDate As Date?
        Get
            Return _depositDate
        End Get
        Set(value As Date?)
            _depositDate = value
        End Set
    End Property

    Property OperatingUnitId As Integer

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' tag del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim _tagForm As String
    ''' <summary>
    ''' propiedad para establecer el tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''' 
    Public Property TagForm As String
        Get
            Return _tagForm
        End Get
        Set(value As String)
            _tagForm = value
        End Set
    End Property

    ''' <summary>
    ''' variable para saber si un editvalue fue cambiado desde el formulario y no desde el control
    ''' </summary>
    Dim _editedValueFromTheForm As Boolean

    ''' <summary>
    ''' obtiene o establece si el editvalue fue cambiado desde el formulario o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property EditedValueFromTheForm As Boolean
        Get
            Return _editedValueFromTheForm
        End Get
        Set(value As Boolean)
            _editedValueFromTheForm = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene los metodos de pago para cartera
    ''' </summary>
    ''' <value>
    ''' The methods payments.
    ''' </value>
    Public ReadOnly Property MethodsPaymentsCash As List(Of Tuple(Of Byte, String))
        Get
            If listMethodsPaymentCash Is Nothing Then
                listMethodsPaymentCash = New List(Of Tuple(Of Byte, String))()
                listMethodsPaymentCash.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("EffectivePaymentMethod", MODULE_NAME)))
                listMethodsPaymentCash.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("PaymentMethodCheck", MODULE_NAME)))
                listMethodsPaymentCash.Add(New Tuple(Of Byte, String)(5, ResourceManager.GetString("PaymentMethodPoints", MODULE_NAME)))
                'listMethodsPaymentCash.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("CardPaymentMethod", MODULE_NAME)))
            End If
            Return listMethodsPaymentCash
        End Get
    End Property

    ''' <summary>
    ''' obtiene los metodos de pago para bvanco
    ''' </summary>
    ''' <value>
    ''' The methods payments.
    ''' </value>
    Public ReadOnly Property MethodsPaymentsBank As List(Of Tuple(Of Byte, String))
        Get
            If listMethodsPaymentBank Is Nothing Then
                listMethodsPaymentBank = New List(Of Tuple(Of Byte, String))()
                'listMethodsPaymentBank.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("EffectivePaymentMethod", MODULE_NAME)))
                'listMethodsPaymentBank.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("PaymentMethodCheck", MODULE_NAME)))
                listMethodsPaymentBank.Add(New Tuple(Of Byte, String)(3, ResourceManager.GetString("CardPaymentMethod", MODULE_NAME)))
                listMethodsPaymentBank.Add(New Tuple(Of Byte, String)(4, ResourceManager.GetString("ConsignmentPaymentMethod", MODULE_NAME)))
            End If
            Return listMethodsPaymentBank
        End Get
    End Property

    ''' <summary>
    ''' obtiene los tipos de consignacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ConsignmentType As List(Of Tuple(Of Byte, String))
        Get
            If listConsignmentType Is Nothing Then
                listConsignmentType = New List(Of Tuple(Of Byte, String))
                listConsignmentType.Add(New Tuple(Of Byte, String)(1, ResourceManager.GetString("LocalConsignment", MODULE_NAME)))
                listConsignmentType.Add(New Tuple(Of Byte, String)(2, ResourceManager.GetString("NationalConsignment", MODULE_NAME)))
            End If
            Return listConsignmentType
        End Get
    End Property

    ''' <summary>
    ''' obtiene o establce el datasource de los bancos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property BankXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleBank.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleBank.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource de las cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The bank account xpo.
    ''' </value>
    Property BankAccountXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource
        Get
            Return CType(INDSleBankAccount.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSleBankAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource de las tarjetas
    ''' </summary>
    ''' <value>
    ''' The card xpo.
    ''' </value>
    Property CardXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleCard.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCard.Properties.DataSource = value
        End Set
    End Property

    Private _listPaymentMethodsAdded As List(Of PaymentMethods)
    Public Property listPaymentMethodsAdded As List(Of PaymentMethods)
        Get
            Return _listPaymentMethodsAdded
        End Get
        Set(value As List(Of PaymentMethods))
            If value IsNot Nothing Then
                _listPaymentMethodsAdded = New List(Of PaymentMethods)
                For Each item In value
                    _listPaymentMethodsAdded.Add(item)
                Next
            End If
        End Set
    End Property

    Dim _bankAccountId As Integer
    Public WriteOnly Property BankAccountId As Integer
        Set(value As Integer)
            INDSleBankAccount.EditValue = value
            _bankAccountId = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource de los convenio de redencion de puntos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AgreementsRedemptionPointsXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSleAgreementsRedemptionPoints.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleAgreementsRedemptionPoints.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Id de la moneda de la caja o cuenta bancaria
    ''' </summary>
    Public Property cashReceiptsCurrencyId As Integer

    ''' <summary>
    ''' Abreviacion de la moneda de la caja o cuenta bancaria
    ''' </summary>
    Public Property cashReceiptsCurrencyAbrreviation As String

    ''' <summary>
    ''' En caso de venir como sub formulario, valor para el total del anticipio y detalles
    ''' </summary>
    Dim _productInvoice As Boolean

    ''' <summary>
    ''' Maneja o no el valor
    ''' </summary>
    WriteOnly Property HandleProductInvoice As Boolean
        Set(value As Boolean)
            _productInvoice = value
            If value Then
                INDGlePaymentMethod.Properties.DataSource = MethodsPaymentsCash
                INDGlePaymentMethod.EditValue = 1
                INDGlePaymentMethod.Properties.ReadOnly = True
            End If
        End Set
    End Property

    ''' <summary>
    ''' id  de la moneda del control
    ''' </summary>
    ''' <returns></returns>
    Private Property CtrCurrencyId As Integer
        Get
            Return INDTxtCurrency.EditValue
        End Get
        Set(value As Integer)
            INDTxtCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' retorna la abreviacion que esta dentro del control
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property CtrCurrencyAbbreviation As String
        Get
            Return INDTxtCurrency.Text
        End Get
    End Property

    ''' <summary>
    ''' tipo de metodo de pago
    ''' </summary>
    Public Property PaymentMethodTypes As Integer
        Get
            Return INDGlePaymentMethod.EditValue
        End Get
        Set(value As Integer)
            INDGlePaymentMethod.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de convenio de redencion de puntos
    ''' </summary>
    Public Property AgreementsRedemptionPointsId As Integer
        Get
            Return INDSleAgreementsRedemptionPoints.EditValue
        End Get
        Set(value As Integer)
            INDSleAgreementsRedemptionPoints.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de transaccion
    ''' </summary>
    Public Property TransactionNumber As String
        Get
            Return INDTxtTransactionNumber.EditValue
        End Get
        Set(value As String)
            INDTxtTransactionNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' fecha de la transaccion
    ''' </summary>
    Public Property TransactionDate As DateTime
        Get
            Return INDDteTransactionDate.EditValue
        End Get
        Set(value As DateTime)
            INDDteTransactionDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de puntos a redimir
    ''' </summary>
    Public Property RedemptionPoints As Integer
        Get
            Return INDTxtRedemptionPoints.EditValue
        End Get
        Set(value As Integer)
            INDTxtRedemptionPoints.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' valor del pago utilizando puntos de redencion
    ''' </summary>
    Public Property ValueRedemption As Decimal
        Get
            Return INDTxtValuePaymentCash.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValuePaymentCash.EditValue = value
        End Set
    End Property
#End Region

#Region "METHODS"
    ''' <summary>
    ''' inicializa el xpo de bancos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeBankXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                BankXPO = model.ConsultarEntidades(eDataSource.ListAllBank)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' inicializa el xpo de redencion de puntos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeAgreementsRedemptionPointsXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                AgreementsRedemptionPointsXPO = model.ConsultarEntidades(eDataSource.ListAgreementsRedemptionPoints)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' inicializa la busqueda de las cuentas bancarias
    ''' </summary>
    Private Sub InitializeBankAccountXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                Dim filter() As Object = {indigo.UserIndigo, True}
                BankAccountXPO = model.ConsultarEntidades(eDataSource.ListEntityBankAccountByUser, filter)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' inicializa la busqueda de las tarjetas
    ''' </summary>
    Private Sub InitializeCardXPO()
        If Not DesignMode Then
            Using model As New MBusqueda
                CardXPO = model.ConsultarEntidades(eDataSource.ListCard)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' cargar los datos en los controles para poder editarlos
    ''' </summary>
    ''' <param name="_paymentMethods"></param>
    ''' <remarks></remarks>
    Public Sub LoadControlsForEdit(_paymentMethods As PaymentMethods)
        editMode = True
        PaymentMethods = _paymentMethods
        INDGlePaymentMethod.EditValue = PaymentMethods.PaymentMethodTypes
        INDGlePaymentMethod.Enabled = False

        Select Case PaymentMethods.PaymentMethodTypes
            Case 1
                INDTxtValuePaymentCash.EditValue = PaymentMethods.Value
            Case 2
                INDSleBank.EditValue = PaymentMethods.IdBank
                INDSleBank.Properties.NullText = PaymentMethods.CodeNameBank
                INDTxtCheckNumber.EditValue = PaymentMethods.CheckNumber
                INDDteDepositDate.EditValue = PaymentMethods.DepositDate
                INDTxtValuePaymentCheck.EditValue = PaymentMethods.Value
            Case 3
                INDSleCard.EditValue = PaymentMethods.IdCard
                INDSleCard.Properties.NullText = PaymentMethods.CodeNameCard
                INDSleCard.Properties.ReadOnly = True
                INDSleCard.Properties.Buttons(0).Enabled = False
                INDTxtCardNumber.EditValue = PaymentMethods.CardNumber
                INDTxtCardNumber.Properties.ReadOnly = True
                INDSePercentCommission.EditValue = PaymentMethods.PercentageCommission
                INDTxtValuePaymentCard.EditValue = PaymentMethods.BaseValue
                INDTxtBase.EditValue = PaymentMethods.BaseValue
                INDTxtBase.Properties.ReadOnly = True
                INDTxtCommissionValue.EditValue = PaymentMethods.CommissionValue
                INDTxtRTFValue.EditValue = PaymentMethods.RTFValue
                INDSeRTFPercent.EditValue = PaymentMethods.PercentageRTF
                INDTxtICAValue.EditValue = PaymentMethods.ICAValue
                INDSeICAPercent.EditValue = PaymentMethods.PercentageICA
            Case 4
                INDSleBankAccount.EditValue = PaymentMethods.IdEntityBankAccount
                INDSleBankAccount.Properties.NullText = PaymentMethods.CodeNameBankAccount
                INDGleDepositType.EditValue = PaymentMethods.DepositType
                INDTxtDepositNumber.EditValue = PaymentMethods.DepositNumber
                INDTxtValuePaymentDeposit.EditValue = PaymentMethods.Value
            Case 5
                AgreementsRedemptionPointsId = PaymentMethods.IdAgreementsRedemptionPoints
                CtrCurrencyId = PaymentMethods.CurrencyId
                TransactionNumber = PaymentMethods.TransactionNumber
                TransactionDate = PaymentMethods.TransactionDate
                RedemptionPoints = PaymentMethods.RedemptionPoints
                ValueRedemption = PaymentMethods.Value

        End Select
        If PaymentMethods.CurrencyId <> cashReceiptsCurrencyId Then
            INDTxtTRM.EditValue = PaymentMethods.TRM
            INDTxtValueTRM.EditValue = PaymentMethods.ValueInCurrencyHeader
            INDTxtCurrency.Properties.NullText = PaymentMethods.CurrencyName
            CtrCurrencyId = PaymentMethods.CurrencyId
        End If
    End Sub
    ''' <summary>
    ''' metodo para agregar un metodo de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddPaymentMethod()

        'Valido que al menos exista un tipo de metodo de pago
        If PaymentMethodTypes > 0 Then
            If PaymentMethods Is Nothing Then
                PaymentMethods = New PaymentMethods
                PaymentMethods.PaymentMethodTypes = PaymentMethodTypes
            End If
            PaymentMethods.CurrencyId = CtrCurrencyId
            Select Case INDGlePaymentMethod.EditValue
                Case 1
                    If ValidatePaymentMethodCash() = False Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                        PaymentMethods = Nothing
                        Exit Sub
                    End If
                    PaymentMethods.Value = INDTxtValuePaymentCash.EditValue
                    If CtrCurrencyId <> cashReceiptsCurrencyId Then
                        PaymentMethods.TRM = INDTxtTRM.EditValue
                        PaymentMethods.ValueInCurrencyHeader = INDTxtValueTRM.EditValue
                        PaymentMethods.CurrencyName = Me.CtrCurrencyAbbreviation
                    Else
                        PaymentMethods.TRM = 1
                        PaymentMethods.ValueInCurrencyHeader = INDTxtValuePaymentCash.EditValue
                        PaymentMethods.CurrencyName = Me.CtrCurrencyAbbreviation
                    End If
                Case 2
                    If ValidatePaymentMethodCheck() = False Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                        PaymentMethods = Nothing
                        Exit Sub
                    End If
                    PaymentMethods.IdBank = INDSleBank.EditValue
                    PaymentMethods.CheckNumber = INDTxtCheckNumber.EditValue
                    PaymentMethods.DepositDate = INDDteDepositDate.EditValue
                    PaymentMethods.Value = INDTxtValuePaymentCheck.EditValue
                    If CtrCurrencyId <> cashReceiptsCurrencyId Then
                        PaymentMethods.TRM = INDTxtTRM.EditValue
                        PaymentMethods.ValueInCurrencyHeader = INDTxtValueTRM.EditValue
                        PaymentMethods.CurrencyName = CtrCurrencyAbbreviation
                    Else
                        PaymentMethods.TRM = INDTxtTRM.EditValue
                        PaymentMethods.ValueInCurrencyHeader = INDTxtValuePaymentCheck.EditValue
                        PaymentMethods.CurrencyName = CtrCurrencyAbbreviation
                    End If
                Case 3
                    If ValidatePaymentMethodCard() = False Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                        PaymentMethods = Nothing
                        Exit Sub
                    End If
                    If editMode = False Then
                        If _listPaymentMethodsAdded IsNot Nothing Then
                            Dim cardAdded = _listPaymentMethodsAdded.Find(Function(x) x.CardNumber = INDTxtCardNumber.EditValue)

                            If cardAdded IsNot Nothing Then
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CardNumberExist", MODULE_NAME)
                                PaymentMethods = Nothing
                                Exit Sub
                            End If
                        End If
                    End If
                    PaymentMethods.IdCard = INDSleCard.EditValue
                    PaymentMethods.CardNumber = INDTxtCardNumber.EditValue
                    PaymentMethods.Value = INDTxtValuePaymentCard.EditValue
                    PaymentMethods.ValueInCurrencyHeader = PaymentMethods.Value
                    PaymentMethods.BaseValue = CDec(INDTxtBase.EditValue)
                    PaymentMethods.CommissionValue = CDec(INDTxtCommissionValue.EditValue)
                    PaymentMethods.PercentageCommission = INDSePercentCommission.EditValue
                    PaymentMethods.RTFValue = CDec(INDTxtRTFValue.EditValue)
                    PaymentMethods.PercentageRTF = INDSeRTFPercent.EditValue
                    PaymentMethods.ICAValue = CDec(INDTxtICAValue.EditValue)
                    PaymentMethods.PercentageICA = INDSeICAPercent.EditValue

                    If PaymentMethods.ICAValue <> 0 OrElse PaymentMethods.RTFValue <> 0 OrElse PaymentMethods.CommissionValue Then
                        Using model As New MCashReceipts("")
                            Dim result As Domain.Base.Entities.ActionResult = model.ValidateCostcenterPaymentMethod(PaymentMethods, OperatingUnitId)
                            If Not result.StateResult Then
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                                Exit Sub
                            End If
                        End Using
                    End If
                Case 4
                    If ValidatePaymentMethodDeposit() = False Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                        PaymentMethods = Nothing
                        Exit Sub
                    End If
                    PaymentMethods.IdEntityBankAccount = INDSleBankAccount.EditValue
                    PaymentMethods.DepositType = INDGleDepositType.EditValue
                    PaymentMethods.DepositNumber = INDTxtDepositNumber.EditValue
                    PaymentMethods.Value = INDTxtValuePaymentDeposit.EditValue
                    If CtrCurrencyId <> cashReceiptsCurrencyId Then
                        PaymentMethods.TRM = INDTxtTRM.EditValue
                        PaymentMethods.ValueInCurrencyHeader = INDTxtValueTRM.EditValue
                        PaymentMethods.CurrencyName = CtrCurrencyAbbreviation
                    Else
                        PaymentMethods.TRM = 1
                        PaymentMethods.ValueInCurrencyHeader = INDTxtValuePaymentDeposit.EditValue
                        PaymentMethods.CurrencyName = CtrCurrencyAbbreviation
                    End If
                Case 5 'redencion de puntos
                    If ValidatePaymentMethodRedemptionPoints() = False Then
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                        PaymentMethods = Nothing
                        Exit Sub
                    End If

                    PaymentMethods.IdAgreementsRedemptionPoints = AgreementsRedemptionPointsId
                    PaymentMethods.TransactionNumber = TransactionNumber
                    PaymentMethods.TransactionDate = TransactionDate
                    PaymentMethods.RedemptionPoints = RedemptionPoints
                    PaymentMethods.Value = ValueRedemption

                    If CtrCurrencyId <> cashReceiptsCurrencyId Then
                        PaymentMethods.TRM = INDTxtTRM.EditValue

                        PaymentMethods.CurrencyName = cashReceiptsCurrencyAbrreviation
                        PaymentMethods.ValueInCurrencyHeader = INDTxtValueTRM.EditValue
                    Else
                        PaymentMethods.TRM = 1

                        PaymentMethods.CurrencyName = CtrCurrencyAbbreviation
                        PaymentMethods.ValueInCurrencyHeader = ValueRedemption
                    End If

            End Select
            Dim args As AddPaymentMethodsEventArgs = New AddPaymentMethodsEventArgs
            args.PaymentMethods = PaymentMethods
            If _listPaymentMethodsAdded Is Nothing Then
                _listPaymentMethodsAdded = New List(Of PaymentMethods)
            End If
            _listPaymentMethodsAdded.Add(PaymentMethods)
            CleanControls()
            RaiseEvent AddPaymentMethods(Nothing, args)
            INDGlePaymentMethod.Focus()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un tipo de forma de pago"
        End If

    End Sub
    ''' <summary>
    ''' metodo para validar el metodo de pago efectivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePaymentMethodCash() As Boolean
        If INDTxtValuePaymentCash.EditValue <= 0 Then
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' metodo para validar el metodo de pago cheque
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePaymentMethodCheck() As Boolean
        If INDSleBank.EditValue Is Nothing Then
            Return False
        End If
        If INDTxtCheckNumber.Text = String.Empty Then
            Return False
        End If
        If INDDteDepositDate.EditValue Is Nothing Then
            Return False
        End If
        If INDTxtValuePaymentCheck.EditValue <= 0 Then
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' metodo para validar el metodo de pago tarjeta
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePaymentMethodCard() As Boolean
        If INDSleCard.EditValue Is Nothing Then
            Return False
        End If
        If INDTxtCardNumber.Text = String.Empty Then
            Return False
        End If
        If INDTxtValuePaymentCard.EditValue <= 0 Then
            Return False
        End If
        If INDTxtBase.EditValue <= 0 Then
            Return False
        End If
        Return True
    End Function
    ''' <summary>
    ''' metodo para validar el metodo de pago consignacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePaymentMethodDeposit() As Boolean
        If INDLciAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDSleBankAccount.EditValue Is Nothing Then
                Return False
            End If
        End If
        If INDGleDepositType.EditValue Is Nothing Then
            Return False
        End If
        'If INDTxtDepositNumber.Text = String.Empty Then
        '    Return False
        'End If
        If INDTxtValuePaymentDeposit.EditValue <= 0 Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' metodo para validar el metodo de pago redencion de puntos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePaymentMethodRedemptionPoints() As Boolean
        If INDSleAgreementsRedemptionPoints.EditValue Is Nothing Then
            Return False
        End If
        If INDTxtCurrency.EditValue Is Nothing Then
            Return False
        End If
        If INDTxtTransactionNumber.EditValue Is Nothing Then
            Return False
        End If
        If INDDteTransactionDate.EditValue Is Nothing Then
            Return False
        End If
        If INDTxtRedemptionPoints.EditValue Is Nothing Then
            Return False
        End If
        If INDTxtValuePaymentCash.EditValue Is Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' metodo para limpiar los controles del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        editMode = False
        INDTxtValuePaymentCash.EditValue = 0
        INDSleBank.EditValue = Nothing
        INDSleBank.Properties.NullText = String.Empty
        INDTxtCheckNumber.Text = String.Empty
        INDDteDepositDate.EditValue = DepositDate
        INDTxtValuePaymentCheck.EditValue = 0
        INDSleCard.EditValue = Nothing
        INDSleCard.Properties.NullText = String.Empty
        INDSePercentCommission.EditValue = 0
        INDTxtCardNumber.Text = String.Empty
        INDTxtCardNumber.Properties.ReadOnly = False
        INDTxtValuePaymentCard.Enabled = False
        INDTxtValuePaymentCard.EditValue = 0F
        INDTxtBase.EditValue = 0F
        INDTxtBase.Enabled = False
        INDTxtCommissionValue.EditValue = 0
        INDTxtRTFValue.EditValue = 0F
        INDSeRTFPercent.EditValue = 0F
        INDTxtICAValue.EditValue = 0F
        INDSeICAPercent.EditValue = 0F
        INDSleBankAccount.EditValue = If(_bankAccountId = 0, Nothing, _bankAccountId)
        INDSleBankAccount.Properties.NullText = String.Empty
        INDGleDepositType.EditValue = Nothing
        INDTxtDepositNumber.Text = String.Empty
        INDTxtValuePaymentDeposit.EditValue = 0
        PaymentMethods = Nothing
        _listPaymentMethodsAdded = Nothing
        INDTxtValuePaymentCash.ReadOnly = False
        INDTxtCurrency.ReadOnly = False

        PaymentMethodTypes = Nothing
        AgreementsRedemptionPointsId = Nothing
        CtrCurrencyId = cashReceiptsCurrencyId
        TransactionNumber = Nothing
        TransactionDate = (Date.Now).ToString("d")
        RedemptionPoints = Nothing
        ValueRedemption = Nothing
    End Sub

    Public Sub HideAllLabels()
        INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciValueTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Async Sub CalculateTRMAndVisibility()
        Dim _cultureCurencyChange As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _cultureCurencyChange.NumberFormat = CtrCurrencyAbbreviation.GetNumberFormat
        If CtrCurrencyId <> cashReceiptsCurrencyId Then
            Dim TRM
            Using Model As New MPortfolioTransfers("")
                Dim Result = Await Model.GetTRMbyCurrencyId(cashReceiptsCurrencyId, CtrCurrencyId)
                ''En caso de que no tenga trm para la conversion no debe permitir la acción
                If Result Is Nothing OrElse Not Result?.StateResult Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "No existe un TRM a la fecha para esta moneda"
                    INDTxtTRM.EditValue = 0
                    CtrCurrencyId = INDTxtCurrency.OldEditValue
                    INDTxtValuePaymentCash.Properties.Mask.Culture = _cultureCurencyChange
                    INDTxtValuePaymentCheck.Properties.Mask.Culture = _cultureCurencyChange
                    HideAllLabels()
                    Exit Sub
                End If
                TRM = Result.ObjectEmbbeded.Value
            End Using
            INDLciTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciValueTRM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDTxtTRM.EditValue = TRM
            INDTxtValuePaymentCash.Properties.Mask.Culture = _cultureCurencyChange
            If TRM > 0 Then
                INDTxtValueTRM.EditValue = Math.Round(INDTxtValuePaymentCash.EditValue / TRM, 4)
            End If
        Else
            HideAllLabels()
        End If
    End Sub

#End Region

#Region "HANDLERS"

#Region "load"
    ''' <summary>
    ''' evento load del control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrPopupPaymentMethod_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDPcButtons.Visible = False
        InitializeBankAccountXPO()
        ''Moneda
        reloadFormatCurrency()
    End Sub

    Public Sub reloadFormatCurrency()
        CtrCurrencyId = cashReceiptsCurrencyId
        INDTxtCurrency.Properties.NullText = cashReceiptsCurrencyAbrreviation

        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = cashReceiptsCurrencyAbrreviation.GetNumberFormat

        INDTxtValuePaymentCash.Properties.Mask.Culture = _culture
        INDTxtValuePaymentCheck.Properties.Mask.Culture = _culture
        INDTxtValuePaymentDeposit.Properties.Mask.Culture = _culture
        INDTxtValuePaymentCard.Properties.Mask.Culture = _culture
        INDTxtBase.Properties.Mask.Culture = _culture
        INDTxtCommissionValue.Properties.Mask.Culture = _culture
        INDTxtRTFValue.Properties.Mask.Culture = _culture
        INDTxtICAValue.Properties.Mask.Culture = _culture
        INDLciValueTRM.Text = "Valor (" + cashReceiptsCurrencyAbrreviation + ")"
        INDTxtTRM.Properties.Mask.Culture = _culture
        INDTxtValueTRM.Properties.Mask.Culture = _culture
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' evento que se dispara al presionar el boton mas y abre el formulario requerido
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCard_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCard.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmCards
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeCardXPO()
                RaiseEvent OpenPopup(Nothing, EventArgs.Empty)
                INDSleCard.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el boton mas y abre el formulario requerido
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBankAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleBankAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmEntityAccount
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeBankAccountXPO()
                RaiseEvent OpenPopup(Nothing, EventArgs.Empty)
                INDSleBankAccount.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el boton mas y abre el formulario requerido
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBank_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleBank.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmPayrollBank
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeBankXPO()
                RaiseEvent OpenPopup(Nothing, EventArgs.Empty)
                INDSleBank.Focus()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al presionar el boton mas y abre el formulario requerido
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleAgreementsRedemptionPoints_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleAgreementsRedemptionPoints.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using formulario As New FrmAgreementsRedemptionPoints
                formulario.ViewModeEditHold = True
                formulario.Size = New Size(800, 700)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent = New FrmTransparent(formulario, False)
                transparent.ShowDialog()
                InitializeBankXPO()
                RaiseEvent OpenPopup(Nothing, EventArgs.Empty)
                INDSleAgreementsRedemptionPoints.Focus()
            End Using
        End If
    End Sub
#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleAgreementsRedemptionPoints_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleAgreementsRedemptionPoints.QueryPopUp
        If AgreementsRedemptionPointsXPO Is Nothing Then
            InitializeAgreementsRedemptionPointsXPO()
        End If
    End Sub

    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleBank_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBank.QueryPopUp
        If BankXPO Is Nothing Then
            InitializeBankXPO()
        End If
    End Sub

    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleCard_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCard.QueryPopUp
        If CardXPO Is Nothing Then
            InitializeCardXPO()
        End If
    End Sub

    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDSleBankAccount_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleBankAccount.QueryPopUp
        If BankAccountXPO Is Nothing Then
            InitializeBankAccountXPO()
        End If
    End Sub

    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDGleConsignmentType_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDGleDepositType.QueryPopUp
        If INDGleDepositType.Properties.DataSource Is Nothing Then
            INDGleDepositType.Properties.DataSource = ConsignmentType
        End If
    End Sub

    ''' <summary>
    ''' realiza la consulta cuando se despliega el popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDTxtCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDTxtCurrency.QueryPopUp
        If INDTxtCurrency.Properties.DataSource Is Nothing Then
            INDTxtCurrency.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).CommonService.GetCurrency()
        End If
    End Sub
#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' evento que se dispara al cambir el tipo de pago y visualiza los controles requeridos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDGleMethodPayment_EditValueChanged(sender As Object, e As EventArgs) Handles INDGlePaymentMethod.EditValueChanged
        Dim args As ResizePopupEventArgs = New ResizePopupEventArgs
        ClosePopupChangePaymentMethod = True
        INDTxtCurrency.Enabled = True
        Select Case INDGlePaymentMethod.EditValue
            Case 1
                INDLciValuePaymentCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciConsignmentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCardNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentageCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRTFValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentRTF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciICAValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentICA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDLciAgreementsRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                CtrCurrencyId = cashReceiptsCurrencyId

                args.PaymentMethodType = 1
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                INDTxtValuePaymentCash.ReadOnly = False
                EditedValueFromTheForm = False
                CalculateTRMAndVisibility()
            Case 2
                INDLciValuePaymentCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciNumberCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciConsignmentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciValuePaymentCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCardNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentageCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRTFValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentRTF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciICAValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentICA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDLciAgreementsRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                args.PaymentMethodType = 2
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                EditedValueFromTheForm = False
                CalculateTRMAndVisibility()
            Case 3
                INDLciValuePaymentCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciConsignmentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciCardNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciValuePaymentCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciPercentageCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciValueCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciRTFValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciPercentRTF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciICAValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciPercentICA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDLciAgreementsRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDTxtCurrency.Enabled = False
                HideAllLabels()
                args.PaymentMethodType = 3
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                EditedValueFromTheForm = False
            Case 4
                INDLciValuePaymentCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciConsignmentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCardNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentageCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRTFValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentRTF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciICAValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentICA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciNumberConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciValuePaymentConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                INDLciAgreementsRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDTxtCurrency.ReadOnly = True
                INDTxtValuePaymentCash.ReadOnly = True

                CalculateTRMAndVisibility()
                args.PaymentMethodType = 4
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                EditedValueFromTheForm = False
            Case 5
                INDLciAgreementsRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciAgreementsRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciTransactionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciValuePaymentCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                INDLciBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciConsignmentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCardNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentageCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRTFValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentRTF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciICAValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentICA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDTxtCurrency.ReadOnly = True
                INDTxtValuePaymentCash.ReadOnly = True

                CalculateTRMAndVisibility()
                args.PaymentMethodType = 5
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                EditedValueFromTheForm = False
            Case Else
                INDLciAgreementsRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciTransactionDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRedemptionPoints.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                INDLciValuePaymentCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBank.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberCash.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciConsignmentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCheck.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciCardNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentCard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciBase.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentageCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValueCommission.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciRTFValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentRTF.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciICAValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciPercentICA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciNumberConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciValuePaymentConsignment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                HideAllLabels()
                ClosePopupChangePaymentMethod = False
                args.PaymentMethodType = Nothing
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
        End Select

    End Sub

    ''' <summary>
    ''' evento que se dispara cuando se seleciona una tarjeta y consulta por id
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCard_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCard.EditValueChanged
        If INDSleCard.EditValue IsNot Nothing Then
            INDTxtValuePaymentCard.Enabled = True
            INDTxtBase.Enabled = True
            If PaymentMethods Is Nothing Then
                Using model As New MCards(TagForm)
                    card = model.GetCardById(INDSleCard.EditValue)
                    Using modelRetention As New MRetentionConcept(TagForm)
                        INDSePercentCommission.EditValue = modelRetention.GetRetentionByIdSimple(card.IdRetentionConceptCommision).Rate
                        INDSeRTFPercent.EditValue = modelRetention.GetRetentionByIdSimple(card.IdRetentionConceptRTF).Rate
                        INDSeICAPercent.EditValue = modelRetention.GetRetentionByIdSimple(card.IdRetentionConceptICA).Rate
                        If INDTxtValuePaymentCard.EditValue > 0 Then
                            ChangeBase()
                        End If
                    End Using
                End Using
            End If
        Else
            CleanControls()
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambia el valor de moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtCurrency.EditValueChanged
        CalculateTRMAndVisibility()
        ''se cambia el formato de la moneda
        'If cashReceiptsCurrencyId <> INDTxtCurrency.EditValue Then
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = cashReceiptsCurrencyAbrreviation.GetNumberFormat
        INDTxtValuePaymentCash.Properties.Mask.Culture = _culture
        INDTxtValuePaymentCheck.Properties.Mask.Culture = _culture
        INDTxtValuePaymentDeposit.Properties.Mask.Culture = _culture
        'INDTxtValuePaymentCheck.Properties.Mask.Culture = _culture

        INDTxtTRM.Properties.Mask.Culture = _culture
        INDLciValueTRM.Text = "Valor (" + cashReceiptsCurrencyAbrreviation + ")"
        'End If
    End Sub


    ''' <summary>
    ''' evento que se dispara al cambia el valor de efectivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtValuePaymentCash_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValuePaymentCash.EditValueChanged
        If INDTxtTRM.EditValue > 0 Then
            INDTxtValueTRM.EditValue = Math.Round(INDTxtValuePaymentCash.EditValue / INDTxtTRM.EditValue, 4)
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambia el valor de cheque
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtValuePaymentCheck_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValuePaymentCheck.EditValueChanged
        If INDTxtTRM.EditValue > 0 Then
            INDTxtValueTRM.EditValue = Math.Round(INDTxtValuePaymentCheck.EditValue / INDTxtTRM.EditValue, 4)
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambia el valor de deposito
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtValuePaymentDeposit_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValuePaymentDeposit.EditValueChanged
        If INDTxtTRM.EditValue > 0 Then
            INDTxtValueTRM.EditValue = Math.Round(INDTxtValuePaymentDeposit.EditValue / INDTxtTRM.EditValue, 4)
        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambiar el convenio de redencion de puntos
    ''' </summary>
    Private Sub INDSleAgreementsRedemptionPoints_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleAgreementsRedemptionPoints.EditValueChanged

        'valido que exita un convenio seleccionado
        If (AgreementsRedemptionPointsId > 0) Then
            If (listPaymentMethodsAdded?.Any(Function(x) x.IdAgreementsRedemptionPoints = AgreementsRedemptionPointsId)) AndAlso editMode = False Then
                AgreementsRedemptionPointsId = Nothing
                Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con el mismo convenio."
                Exit Sub
            End If
            Using model As New MAgreementsRedemptionPoints(TagForm)

                'obtenemos los datos del convenio por id
                AgreementsRedemptionPointsData = model.GetAgreementsRedemptionPointsById(AgreementsRedemptionPointsId)

                'guardamos el detalle del convenio
                For Each AgreementsRedemption In AgreementsRedemptionPointsData
                    AgreementsRedemptionPointsDetail = DirectCast(AgreementsRedemption, AgreementsRedemptionPointsXpo).AgreementsRedemptionPointsDetailXpo
                    CtrCurrencyId = DirectCast(AgreementsRedemption, AgreementsRedemptionPointsXpo).CurrencyId
                Next
            End Using

            'asignos el valor y la cantidad de los puntos del detalle
            Parallel.ForEach(AgreementsRedemptionPointsDetail, Sub(AgreementsRedemptionDetail)
                                                                   PointsValue = AgreementsRedemptionDetail.PointsValue
                                                                   PointsAmount = AgreementsRedemptionDetail.PointsAmount
                                                               End Sub)

        End If
    End Sub

    ''' <summary>
    ''' evento que se dispara al cambia el valor de los puntos redimidos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtRedemptionPoints_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtRedemptionPoints.EditValueChanged
        'valido que exita un convenio seleccionado
        If (AgreementsRedemptionPointsId > 0) And PointsValue > 0 Then
            ValueRedemption = (RedemptionPoints * PointsValue) / PointsAmount
        End If
    End Sub
#End Region

#Region "GotFocus"
    ''' <summary>
    ''' evento que se dispara cuando el control recibe el foco
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGlePaymentMethod_GotFocus(sender As Object, e As EventArgs) Handles INDGlePaymentMethod.GotFocus
        If PaymentMethods IsNot Nothing Then
            INDGlePaymentMethod.Enabled = False
            SendKeys.Send("{ENTER}")
        End If
    End Sub
    ''' <summary>
    ''' se dispara cuando recibe el foco y valida que la base no sea mayor que el valor pagado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtBase_GotFocus(sender As Object, e As EventArgs) Handles INDTxtBase.GotFocus
        Dim baseValue = CDec(INDTxtBase.EditValue)
        Dim value = CDec(INDTxtValuePaymentCard.EditValue)
        If editMode Then
            Exit Sub
        End If
        If TreasuryStaticServices.ValidateBaseValue(baseValue, value) Then
            INDTxtBase.EditValue = INDTxtValuePaymentCard.EditValue
        End If
    End Sub
#End Region

#Region "LostFocus"
    ''' <summary>
    ''' evento que se dispara cuando se pierde el foco
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtValuePaymentCard_LostFocus(sender As Object, e As EventArgs) Handles INDTxtValuePaymentCard.LostFocus
        'Dim baseValue = CDec(INDTxtBase.EditValue)
        'If baseValue = 0 Then
        INDTxtBase.EditValue = INDTxtValuePaymentCard.EditValue
        INDTxtBase.Focus()
        'End If
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando cambia el valor de la base y realiza el calculo del valor de la comisión
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtBase_TextChanged(sender As Object, e As EventArgs) Handles INDTxtBase.TextChanged
        If card IsNot Nothing Then
            ChangeBase()
        End If
    End Sub

    ''' <summary>
    ''' Al cambiar la franquicia de tarjeta se llama para calcular los valores, o cuando cambia la base calcula de new los valores
    ''' </summary>
    Private Sub ChangeBase()
        Dim baseValue = CDec(INDTxtBase.EditValue)
        Dim value = CDec(INDTxtValuePaymentCard.EditValue)
        'If editMode Then
        '    Exit Sub
        'End If
        If TreasuryStaticServices.ValidateBaseValue(baseValue, value) = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("BaseValue", MODULE_NAME), INDTxtBase.Text, INDTxtValuePaymentCard.Text)
            INDTxtBase.EditValue = value
            Exit Sub
        End If
        INDTxtCommissionValue.EditValue = TreasuryStaticServices.PercentValue(baseValue, INDSePercentCommission.EditValue)
        INDTxtRTFValue.EditValue = TreasuryStaticServices.PercentValue(baseValue, INDSeRTFPercent.EditValue)
        INDTxtICAValue.EditValue = TreasuryStaticServices.PercentValue(baseValue, INDSeICAPercent.EditValue)
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' evento que se dispara al dar click en agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDBtnAdd.Click
        AddPaymentMethod()
    End Sub
    ''' <summary>
    ''' evento que se dispara al presionar click en el boton de cerrar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnClose_Click(sender As Object, e As EventArgs) Handles INDBtnClose.Click
        CleanControls()
        Me.ClosePopupChangePaymentMethod = False
        INDGlePaymentMethod.EditValue = Nothing
        INDGlePaymentMethod.Enabled = True
        RaiseEvent ClosePopup(Nothing, EventArgs.Empty)
    End Sub

#End Region
#End Region


End Class
