'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Carlos Ernesto Córdoba
' Created          : 20-06-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls.MVP
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Treasury.MVP
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Entities.Service
Imports Presentation.Accounting.MVP
Imports Presentation.Common.MVP
Imports Presentation.Common
Imports System.Windows.Forms
Imports System.Drawing
Imports Presentation.Treasury

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
    Public Event KeyDownAll(sender As Object, e As System.Windows.Forms.KeyEventArgs)
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

    Private _preLoadValue As Decimal = CDec(0)

    ''' <summary>
    ''' variable privada que toma el valor de la propiedad de escritura del mismo nombre
    ''' </summary>
    Private _currencyId As Integer

    ''' <summary>
    ''' Moneda
    ''' </summary>
    Public WriteOnly Property CurrencyId(Optional CurrencyAbbreviation As String = Nothing) As Integer
        Set(value As Integer)
            If String.IsNullOrEmpty(CurrencyAbbreviation) Then
                CurrencyAbbreviation = indigo.CurrencyISO4217
                _currencyId = Me.indigo.OfficialCurrencyId
            Else
                _currencyId = value
                setFormatsControls(CurrencyAbbreviation)
            End If
        End Set
    End Property
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

    Dim _productInvoice As Boolean
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
    ''' obtiene los metodos de pago para cartera
    ''' </summary>
    ''' <value>
    ''' The methods payments.
    ''' </value>
    Public ReadOnly Property MethodsPaymentsCash As List(Of Tuple(Of Byte, String))
        Get
            Return Utils.PaymentMethodTypes.FindAll(Function(x) {1, 2}.Contains(x.Item1))
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
            Return Utils.PaymentMethodTypes.FindAll(Function(x) {3, 4}.Contains(x.Item1))
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

    ''' <summary>
    ''' propiedad publica para establecer el valor a precargar
    ''' </summary>
    Public WriteOnly Property PreLoadValue As Decimal
        Set(value As Decimal)
            Me._preLoadValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece u obtiene el valor en cash
    ''' </summary>
    ''' <returns></returns>
    Private Property _valuePaymentCash As Decimal
        Get
            Return INDTxtValuePaymentCash.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValuePaymentCash.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece u obtiene el valor en cheque
    ''' </summary>
    ''' <returns></returns>
    Private Property _valuePaymentCheck As Decimal
        Get
            Return INDTxtValuePaymentCheck.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValuePaymentCheck.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece u obtiene el valor de tarjeta
    ''' </summary>
    ''' <returns></returns>
    Private Property _valuePaymentCard As Decimal
        Get
            Return INDTxtValuePaymentCard.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValuePaymentCard.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' establece u obtiene el valor de la consigancion
    ''' </summary>
    ''' <returns></returns>
    Private Property _valuePaymentDeposit As Decimal
        Get
            Return INDTxtValuePaymentDeposit.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValuePaymentDeposit.EditValue = value
        End Set
    End Property
#End Region

#Region "METHODS"

    ''' <summary>
    ''' Setea los controles a un formato predefinido
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Private Sub setFormatsControls(Abbreviation As String)
        If String.IsNullOrEmpty(Abbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo establecer el formato de la moneda"
            Exit Sub
        End If
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = Abbreviation.GetNumberFormat
        INDTxtValuePaymentCash.Properties.Mask.Culture = _culture
        INDTxtValuePaymentCheck.Properties.Mask.Culture = _culture
        INDTxtValuePaymentCard.Properties.Mask.Culture = _culture
        INDTxtBase.Properties.Mask.Culture = _culture
        INDTxtCommissionValue.Properties.Mask.Culture = _culture
        INDTxtRTFValue.Properties.Mask.Culture = _culture
        INDTxtICAValue.Properties.Mask.Culture = _culture
        INDTxtValuePaymentDeposit.Properties.Mask.Culture = _culture
    End Sub
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
                Me._valuePaymentCash = PaymentMethods.Value
            Case 2
                INDSleBank.EditValue = PaymentMethods.IdBank
                INDSleBank.Properties.NullText = PaymentMethods.CodeNameBank
                INDTxtCheckNumber.EditValue = PaymentMethods.CheckNumber
                INDDteDepositDate.EditValue = PaymentMethods.DepositDate
                Me._valuePaymentCheck = PaymentMethods.Value
            Case 3
                INDSleCard.EditValue = PaymentMethods.IdCard
                INDSleCard.Properties.NullText = PaymentMethods.CodeNameCard
                INDSleCard.Properties.ReadOnly = True
                INDSleCard.Properties.Buttons(0).Enabled = False
                INDTxtCardNumber.EditValue = PaymentMethods.CardNumber
                INDSePercentCommission.EditValue = PaymentMethods.PercentageCommission
                Me._valuePaymentCard = PaymentMethods.Value
                INDTxtBase.EditValue = PaymentMethods.BaseValue
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
                Me._valuePaymentDeposit = PaymentMethods.Value
        End Select
    End Sub
    ''' <summary>
    ''' metodo para agregar un metodo de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddPaymentMethod()
        If PaymentMethods Is Nothing Then
            PaymentMethods = New PaymentMethods
            PaymentMethods.PaymentMethodTypes = INDGlePaymentMethod.EditValue
        End If
        Select Case INDGlePaymentMethod.EditValue
            Case 1
                If ValidatePaymentMethodCash() = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                    PaymentMethods = Nothing
                    Exit Sub
                End If
                PaymentMethods.Value = Me._valuePaymentCash
            Case 2
                If ValidatePaymentMethodCheck() = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                    PaymentMethods = Nothing
                    Exit Sub
                End If
                PaymentMethods.IdBank = INDSleBank.EditValue
                PaymentMethods.CheckNumber = INDTxtCheckNumber.EditValue
                PaymentMethods.DepositDate = INDDteDepositDate.EditValue
                PaymentMethods.Value = Me._valuePaymentCheck
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
                PaymentMethods.Value = Me._valuePaymentCard
                PaymentMethods.BaseValue = CDec(INDTxtBase.EditValue)
                PaymentMethods.CommissionValue = Math.Round(CDec(INDTxtCommissionValue.EditValue), 2, MidpointRounding.AwayFromZero)
                PaymentMethods.PercentageCommission = INDSePercentCommission.EditValue
                PaymentMethods.RTFValue = Math.Round(CDec(INDTxtRTFValue.EditValue), 2, MidpointRounding.AwayFromZero)
                PaymentMethods.PercentageRTF = INDSeRTFPercent.EditValue
                PaymentMethods.ICAValue = Math.Round(CDec(INDTxtICAValue.EditValue), 2, MidpointRounding.AwayFromZero)
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
                PaymentMethods.Value = Me._valuePaymentDeposit
        End Select
        PaymentMethods.CurrencyId = Me._currencyId
        PaymentMethods.ValueInCurrencyHeader = PaymentMethods.Value
        Dim args As AddPaymentMethodsEventArgs = New AddPaymentMethodsEventArgs
        args.PaymentMethods = PaymentMethods        
        CleanControls()
        RaiseEvent AddPaymentMethods(Nothing, args)
        INDGlePaymentMethod.Focus()
    End Sub
    ''' <summary>
    ''' metodo para validar el metodo de pago efectivo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePaymentMethodCash() As Boolean
        If _valuePaymentCash <= 0 Then
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
        If Me._valuePaymentCheck <= 0 Then
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
        If _valuePaymentCard <= 0 Then
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
        If INDTxtDepositNumber.Text = String.Empty Then
            Return False
        End If
        If Me._valuePaymentDeposit <= 0 Then
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
        If _productInvoice = False Then
            INDGlePaymentMethod.Properties.ReadOnly = False
        End If
        Me._valuePaymentCash = 0
        INDSleBank.EditValue = Nothing
        INDSleBank.Properties.NullText = String.Empty
        INDTxtCheckNumber.Text = String.Empty
        INDDteDepositDate.EditValue = DepositDate
        Me._valuePaymentCheck = 0
        INDSleCard.EditValue = Nothing
        INDSleCard.Properties.NullText = String.Empty
        INDSePercentCommission.EditValue = 0
        INDTxtCardNumber.Text = String.Empty
        INDTxtValuePaymentCard.Enabled = False
        Me._valuePaymentCard = 0
        INDTxtBase.EditValue = 0
        INDTxtBase.Enabled = False
        INDTxtCommissionValue.EditValue = 0
        INDTxtRTFValue.EditValue = 0
        INDSeRTFPercent.EditValue = 0
        INDTxtICAValue.EditValue = 0
        INDSeICAPercent.EditValue = 0
        INDGleDepositType.EditValue = Nothing
        INDTxtDepositNumber.Text = String.Empty
        Me._valuePaymentDeposit = 0
        PaymentMethods = Nothing
        Me.CurrencyId(Me.indigo.CurrencyISO4217) = Me.indigo.OfficialCurrencyId
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
        'INDGlePaymentMethod.Properties.DataSource = MethodsPaymentsBank
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
                'formulario.ViewModeEditHold = True
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
#End Region

#Region "QueryPopUp"
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
                args.PaymentMethodType = 1
                Me._valuePaymentCash = Me._preLoadValue
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                EditedValueFromTheForm = False
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
                args.PaymentMethodType = 2
                Me._valuePaymentCheck = Me._preLoadValue
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                EditedValueFromTheForm = False
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
                args.PaymentMethodType = 3
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                Me._valuePaymentCard = _preLoadValue
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
                args.PaymentMethodType = 4
                If EditedValueFromTheForm = False Then
                    RaiseEvent ResizePopup(Nothing, args)
                End If
                Me._valuePaymentDeposit = Me._preLoadValue
                EditedValueFromTheForm = False
            Case Else
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
                    End Using
                End Using
            End If
        Else
            CleanControls()
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
        Dim value = Me._valuePaymentCard
        If TreasuryStaticServices.ValidateBaseValue(baseValue, value) Then
            INDTxtBase.EditValue = Me._valuePaymentCard
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
        INDTxtBase.EditValue = Me._valuePaymentCard
        'End If
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando pierde el foco y realiza el calculo del valor de la comision
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDTxtBase_LostFocus(sender As Object, e As EventArgs) Handles INDTxtBase.LostFocus
        If card IsNot Nothing Then
            Dim baseValue = CDec(INDTxtBase.EditValue)
            Dim value = Me._valuePaymentCard
            If TreasuryStaticServices.ValidateBaseValue(baseValue, value) = True Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("BaseValue", MODULE_NAME), INDTxtBase.Text, INDTxtValuePaymentCard.Text)
                INDTxtBase.EditValue = value
                Exit Sub
            End If
            INDTxtCommissionValue.EditValue = TreasuryStaticServices.PercentValue(baseValue, INDSePercentCommission.EditValue)
            INDTxtRTFValue.EditValue = TreasuryStaticServices.PercentValue(baseValue, INDSeRTFPercent.EditValue)
            INDTxtICAValue.EditValue = TreasuryStaticServices.PercentValue(baseValue, INDSeICAPercent.EditValue)
        End If
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
        INDGlePaymentMethod.EditValue = Nothing
        INDGlePaymentMethod.Enabled = True
        RaiseEvent ClosePopup(Nothing, EventArgs.Empty)
    End Sub
#End Region
#End Region


End Class
