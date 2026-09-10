'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 27/05/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : Control de usuario que permite agregar conceptos
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports Presentation.Payroll
Imports Presentation.Accounting
Imports Presentation.Accounting.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP
Imports Presentation.Treasury.MVP
Imports DevExpress.Data.Linq
Imports Domain.Entities.Service
Imports Presentation.Maintenance.MVP
Imports Presentation.Payments.MVP
Imports System.Text
Imports DevExpress.XtraLayout
Imports Presentation.Common.MVP

#End Region

Public Class CtrDisbursementVoucher

#Region "Variables"

    ''' <summary>
    ''' Constante con el nombre del módulo
    ''' </summary>
    Private Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Constante con el nombre del módulo de contabilidad
    ''' </summary>
    Private Const NAME_MODULE_ACCOUNTING As String = "Accounting"

    ''' <summary>
    ''' Constante con el nombre del modulo de pagos
    ''' </summary>
    Private Const NAME_MODULE_PAYMENT As String = "Payments"

    ''' <summary>
    ''' Contiene el tag del from principal
    ''' </summary>
    Private _myTag As String = "636"

    ''' <summary>
    ''' variable que contiene la lista de los campos obligatorios que no se han llenado
    ''' </summary>
    Private _listaValidaciones As StringBuilder

    ''' <summary>
    ''' bandera que indica si se está editando un registro o es nuevo
    ''' </summary>
    Public IsEditMode As Boolean = False

    ''' <summary>
    ''' variable que indica que se está editando por ende se cargan las facturas ya editadas
    ''' </summary>
    Private _datasourcePaymentConceptLoad As Boolean = False

    'variables obtenidas de la consulta del concepto de retencion
    Private _minBaseRetention As Decimal
    Private _retentiontype As Integer
    Private _listAccountingRetention As List(Of RetentionConceptRanges)
    Private _listAccountPayableComplex As List(Of AccountPayableComplex)
    'Public _accountPayableDatasource As List(Of AccountPayableComplex)

    Public Property AccounPayableDatasource As List(Of AccountPayableComplex)
        Get
            Return _listAccountPayableComplex
        End Get
        Set(value As List(Of AccountPayableComplex))
            _listAccountPayableComplex = value
        End Set
    End Property

    ''' <summary>
    ''' Listado de las facturas
    ''' </summary>
    Private _listAccountPayableDetail As List(Of AccountPayableDetailConcept)

    ''' <summary>
    ''' Contiene el id de la caja seleccionada en el frontal de comprobantes de egreso
    ''' </summary>
    Dim _idCashRegister As Integer

    ''' <summary>
    ''' Contiene el id de la cuenta bancaria en el frontal de comprobantes de egreso
    ''' </summary>
    Dim _idEntityBankAccount As Integer

    ''' <summary>
    ''' entidad detalle del comprobante de egreso
    ''' </summary>
    Private _voucherDetail As VoucherTransactionDetails

    ' ''' <summary>
    ' ''' Contiene el id del tercero (el mismo que esta en la cabecera)
    ' ''' </summary>
    'Dim _idThirdParty As Integer

    ''' <summary>
    ''' bandera que indica si se ha seleccionado un concepto
    ''' </summary>
    Dim _isEditValueConcept As Boolean

    ''' <summary>
    ''' Almacena el comportamiento del concepto
    ''' </summary>
    Private _behavior As Integer

    ''' <summary>
    ''' Lista la Naturaleza de la cuenta
    ''' </summary>
    Dim ListNature As List(Of Tuple(Of Integer, String))

#End Region

#Region "Properties"

    Public Event ClosePopup(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Obtiene o establece el concepto
    ''' </summary>
    ''' <value>
    ''' The concept.
    ''' </value>
    Public Property ExpenseConcept As Integer
        Get
            Return INDsleExpenseConcept.EditValue
        End Get
        Set(value As Integer)
            INDsleExpenseConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la caja en el comprobante de egreso
    ''' </summary>
    ''' <value>
    ''' The identifier cash.
    ''' </value>
    Public WriteOnly Property IdCash As Integer
        Set(value As Integer)
            _idCashRegister = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the account payable datasource.
    ''' </summary>
    ''' <value>
    ''' The account payable datasource.
    ''' </value>
    Public Property AccountPayableDatasource As List(Of AccountPayableComplex)
        Get
            Return CType(INDGcInvoices.DataSource, List(Of AccountPayableComplex))
        End Get
        Set(value As List(Of AccountPayableComplex))
            INDGcInvoices.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece una cuenta contable
    ''' </summary>
    Public Property AccountAccouting As Integer
        Get
            Return INDsleAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tercero
    ''' </summary>
    ''' <value>
    ''' The third parthy.
    ''' </value>
    Public Property ThirdParthy As Integer
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de Centro de Costo
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Public Property CostCenter As Integer?
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la naturaleza
    ''' </summary>
    ''' <value>
    ''' The nature.
    ''' </value>
    Public Property Nature As Integer
        Get
            Return INDgleNature.EditValue
        End Get
        Set(value As Integer)
            INDgleNature.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para los terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Public Property ThirdPartyDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account datasource.
    ''' </value>
    Public Property AccountDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleAccount.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Edit value de la cuenta bancaria
    ''' </summary>
    ''' <value>
    ''' The identifier entity bank account.
    ''' </value>
    Public Property EntityBankAccount As Integer?
        Get
            Return INDsleEntityAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleEntityAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion
    ''' </summary>
    ''' <value>
    ''' The retention concept.
    ''' </value>
    Public Property IdRetentionConcept As Integer?
        Get
            Return INDsleConceptRetention.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptRetention.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece los comentarios
    ''' </summary>
    ''' <value>
    ''' The observation.
    ''' </value>
    Public Property Observation As String
        Get
            Return INDmemoComments.EditValue
        End Get
        Set(value As String)
            INDmemoComments.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el porcentaje de retencion
    ''' </summary>
    ''' <value>
    ''' The percent retention.
    ''' </value>
    Public Property PercentRetention As Decimal
        Get
            Return INDsePercentage.EditValue
        End Get
        Set(value As Decimal)
            INDsePercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor facturado
    ''' </summary>
    ''' <value>
    ''' The invoiced value.
    ''' </value>
    Public Property BaseValueRetention As Decimal
        Get
            Return INDtxtBaseValue.EditValue
        End Get
        Set(value As Decimal)
            INDtxtBaseValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el id de la cuenta bancaria del form cabecera para validar que se elija una distinta en el concepto de egreso
    ''' </summary>
    ''' <value>
    ''' The identifier entity bank account.
    ''' </value>
    Public WriteOnly Property IdEntityBankAccount As Integer
        Set(value As Integer)
            _idEntityBankAccount = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la naturaleza del concepto
    ''' </summary>
    ''' <value>
    ''' The nature expense concept.
    ''' </value>
    Public ReadOnly Property NatureExpenseConcept As Integer
        Get
            Return INDgleNature.EditValue
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el valor del anticipo
    ''' </summary>
    ''' <value>
    ''' The value advance payment.
    ''' </value>
    Public Property ValueAdvancePayment As String
        Get
            Return INDtxtAdvancePayment.EditValue
        End Get
        Set(value As String)
            INDtxtAdvancePayment.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Devuelve el valor base
    ''' </summary>
    ''' <value>
    ''' The base value.
    ''' </value>
    Public Property ValueConcept As String
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As String)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el comportamiento del concepto
    ''' </summary>
    ''' <value>
    ''' The behavior.
    ''' </value>
    Public ReadOnly Property Behavior As Integer
        Get
            Return _behavior
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias
    ''' </summary>
    ''' <value>
    ''' The entity bank account datasource.
    ''' </value>
    Public Property EntityBankAccountDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleEntityAccount.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntityAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de conceptos de retencion
    ''' </summary>
    ''' <value>
    ''' The retention concept datasource.
    ''' </value>
    Public Property RetentionConceptDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDsleConceptRetention.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleConceptRetention.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para los conceptos
    ''' </summary>
    ''' <value>
    ''' The concept datasource.
    ''' </value>
    Public Property ConceptDatasource As LinqInstantFeedbackSource
        Get
            Return CType(INDsleExpenseConcept.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDsleExpenseConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para los mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' Obtiene una bandera que indica si se ha elegido concepto para no listar nuevamente el datasource
    ''' </summary>
    ''' <value>
    ''' The is edit value concept.
    ''' </value>
    Public ReadOnly Property IsEditValueConcept
        Get
            Return _isEditValueConcept
        End Get
    End Property

#End Region

#Region "Public Events"

    ''' <summary>
    ''' Ocurre cuando se cierra el popup de conceptos de egreso
    ''' </summary>
    Public Event AddExpenseConcept()

    ''' <summary>
    ''' Sale del popup
    ''' </summary>
    Public Event ExitPopUp()

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Focuses the First control.
    ''' </summary>
    Public Sub FocusFirstControl()
        INDsleExpenseConcept.Focus()
    End Sub

    ''' <summary>
    ''' Initializes los conceptos.
    ''' </summary>
    Public Sub InitializeConcept()
        If _idCashRegister > 0 Then
            Me.InitializeExpenseConceptByCash()
        Else
            Me.InitializeExpenseConceptNotCash()
        End If
    End Sub

    ''' <summary>
    ''' Inicia el datasource de conceptos
    ''' </summary>
    Public Sub InitializeExpenseConceptByCash()
        Using Model As New MBusqueda
            ConceptDatasource = Model.ConsultarEntidades(eDataSource.ListExpenseConceptByCash, CStr(_idCashRegister))
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the concept by entity account.
    ''' </summary>
    Public Sub InitializeExpenseConceptNotCash()
        Using Model As New MBusqueda
            ConceptDatasource = Model.ConsultarEntidades(eDataSource.ListExpenseConceptByNotCash)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de cuentas contables
    ''' </summary>
    Private Sub InitializeAccountAccounting()
        Using Model As New MBusqueda
            Dim filter() As Object = {5, True}
            AccountDatasource = Model.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de cuentas bancarias
    ''' </summary>
    Private Sub InitializeEntityBankAccount()
        Using Model As New MBusqueda
            EntityBankAccountDatasource = Model.ConsultarEntidades(eDataSource.ListEntityBankAccount)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia datasource de terceros
    ''' </summary>
    Private Sub InitializeThird()
        Using Model As New MBusqueda
            ThirdPartyDatasource = Model.ConsultarEntidades(eDataSource.ThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de centros de costo
    ''' </summary>
    Private Sub InitializeCostCenter()
        'Using Model As New MBusqueda
        CostCenterDatasource = Infrastructure.Data.Xpo.XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer).PayrollService.GetCostCenterByState(True) 'Model.ConsultarEntidades(eDataSource.CostCenter)
        'End Using
    End Sub

    ''' <summary>
    ''' Inicia el datasource de conceptos de retención
    ''' </summary>
    Private Sub InitializeRetentionConcept()
        Using Model As New MBusqueda
            RetentionConceptDatasource = Model.ConsultarEntidades(eDataSource.ListRetentionConceptByStatus, "True")
        End Using
    End Sub

    ''' <summary>
    ''' Limpia todos los controles
    ''' </summary>
    Public Sub CleanControls()
        INDsleExpenseConcept.EditValue = Nothing
        INDsleEntityAccount.EditValue = Nothing
        INDsleAccount.EditValue = Nothing
        INDsleCostCenter.EditValue = Nothing
        INDmemoComments.EditValue = Nothing
        INDgleNature.EditValue = 1
        INDtxtValue.EditValue = 0
        INDtxtAdvancePayment.EditValue = 0
        INDGcInvoices.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcInvoices)
        IndigoGridControl1.RefreshGrid(INDgcAdvancePayment)
        IndigoGridControl1.RefreshGrid(INDgcRefound)
        AccounPayableDatasource = Nothing

        IsEditMode = False
        CleanLayouts()

        'Layouts
        INDliEntityAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliAccountAccounting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDsleConceptRetention.EditValue = Nothing
        'INDtxtValue.Properties.ReadOnly = False
        INDtxtValue.Enabled = True
    End Sub

    ''' <summary>
    ''' Creates the list nature.
    ''' </summary>
    Private Sub CreateListNature()
        ListNature = New List(Of Tuple(Of Integer, String))
        ListNature.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountNatureDebit")))
        ListNature.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountNatureCredit")))
        INDgleNature.Properties.DataSource = ListNature
        INDgleNature.EditValue = 1
    End Sub

    ''' <summary>
    ''' Initializes the payments concept.
    ''' </summary>
    Private Sub InitializePaymentsConcept()
        Using Model As New MBusqueda
            Dim result As LinqInstantFeedbackSource = Model.ConsultarEntidades(eDataSource.ListAllPaymentConceptByState, True)
            RepositoryItemPaymentConcepts.DataSource = result
            'PaymentConceptDatasource = result
        End Using
    End Sub

    ''' <summary>
    ''' Iniciars the combos.
    ''' </summary>
    Public Sub IniciarCombos()
        If Not DesignMode Then
            Me._isEditValueConcept = False
            Me.InitializeAccountAccounting()
            Me.InitializeCostCenter()
            Me.InitializeThird()
            Me.InitializeEntityBankAccount()
            Me.InitializeRetentionConcept()
            Me.InitializePaymentsConcept()
            Me.CreateListNature()
        End If
    End Sub

    ''' <summary>
    ''' Habilita o inhabilita los controles de conceptos de retencion
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [actions on controls concept retention]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControlsConceptRetention As Boolean
        Set(value As Boolean)
            INDsleConceptRetention.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Limpia los controles de tipo de retencion
    ''' </summary>
    Public Sub CleanControlsRetention()
        _minBaseRetention = 0
        _retentiontype = 0
        'INDsePercentage.Properties.ReadOnly = True
        INDsePercentage.Enabled = False
        INDtxtBaseValue.EditValue = Nothing
        INDtxtRetentionValue.EditValue = Nothing
        INDsePercentage.EditValue = Nothing
        _retConcept = Nothing
    End Sub

    ''' <summary>
    ''' Carga los datos de la entidad en los controles
    ''' </summary>
    ''' <param name="exConcept">The ex concept.</param>
    Public Sub LoadDataExpenseConcept(ByVal exConcept As ExpenseConceptCplx)
        Me.IsEditMode = True
        _datasourcePaymentConceptLoad = True
        ThirdParthy = exConcept.IdThird
        ExpenseConcept = exConcept.IdExpenseConcept
        AccountAccouting = exConcept.IdAccountAccounting
        Nature = exConcept.IdNature
        ValueAdvancePayment = exConcept.ValueAdvance
        CostCenter = exConcept.IdCostCenter
        If exConcept.IdEntityBankAccount <> 0 Then
            EntityBankAccount = exConcept.IdEntityBankAccount
        End If
        ValueConcept = CStr(exConcept.ValueConcept - exConcept.ValueAdvance)
        If exConcept.IdRetentionConcept <> 0 Then
            IdRetentionConcept = exConcept.IdRetentionConcept
            Using Model As New MRetentionConcept(Me._myTag)
                _retConcept = Model.GetRetentionConceptByIdSimple(IdRetentionConcept)
                PercentRetention = exConcept.PercentRetention
                BaseValueRetention = TreasuryStaticServices.CalculateInvoiceValue(PercentRetention, ValueConcept)
                INDtxtRetentionValue.EditValue = AccountingServices.CalculateRetention(BaseValueRetention, _retConcept)
                'PercentRetention = _retConcept.Rate
                'InvoicedValue = TreasuryServices.CalculateInvoiceValue(PercentRetention, BaseValue)
                'INDtxtRetentionValue.EditValue = AccountingServices.CalculateRetention(InvoicedValue, _retConcept)
            End Using
        End If
        Observation = exConcept.Observation
        AccountPayableDatasource = Me.AccounPayableDatasource
        'calcular retencion
    End Sub

    ''' <summary>
    ''' Obtiene el listado de las facturas pendientes por pagar
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInvoices() As List(Of AccountPayableComplex)
        Return AccounPayableDatasource
    End Function

    Dim _listAccountPayableShares As List(Of AccountPayableShares)

    ''' <summary>
    ''' Carga las facturas pendientes por pagar del tercero seleccionado
    ''' </summary>
    Private Async Sub LoadPaymentInvoicesByThird()
        Using Model As New MAccountPayable(Me._myTag)
            Dim _listAccountPayableShares = Await Model.GetAccountPayableSharesByAccountPayableIdThirdIdAccountAndState(ThirdParthy, AccountAccouting, CInt(eStatusAccountPayable.Confirmado))
            If _listAccountPayableShares IsNot Nothing AndAlso _listAccountPayableShares.Count > 0 Then
                If Not _datasourcePaymentConceptLoad Then
                    AccounPayableDatasource = New List(Of AccountPayableComplex)
                    Dim apy As AccountPayableComplex
                    For Each ap As AccountPayableShares In _listAccountPayableShares
                        apy = New AccountPayableComplex()
                        With apy
                            .IdAccountPayable = ap.IdAccountPayable
                            .IdAccountPayableShare = ap.Id
                            .BillNumber = 0 'ap.AccountPayable.BillNumber
                            .BillDate = DateTime.Now 'ap.AccountPayable.BillDate
                            .ValueShare = ap.Balance
                            .Share = ap.Share
                        End With
                        AccounPayableDatasource.Add(apy)
                    Next
                    INDGcInvoices.DataSource = Nothing
                    INDGcInvoices.DataSource = AccounPayableDatasource
                Else
                    _datasourcePaymentConceptLoad = False
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("NoInvoiceThird", NAME_MODULE_PAYMENT), INDsleThirdParty.Text.Split("-").ElementAt(0), INDsleAccount.Text.Split("-").ElementAt(0))
            End If
        End Using
        'Using Model As New MAccountPayable(Me._myTag)
        '    Dim facturas As List(Of AccountPayable) = Await Model.GetAccountPayableByIdThirdIdAccountAndState(ThirdParthy, AccountAccouting, CInt(eStatusAccountPayable.Confirmado))
        '    If facturas IsNot Nothing AndAlso facturas.Count > 0 Then
        '        _listAccountPayableShares = Nothing
        '        _listAccountPayableShares = New List(Of AccountPayableShares)
        '        For Each Shares As AccountPayable In facturas
        '            _listAccountPayableShares.AddRange(Shares.AccountPayableShares)
        '        Next
        '        If Not _datasourcePaymentConceptLoad Then
        '            AccounPayableDatasource = New List(Of AccountPayableComplex)
        '            Dim apy As AccountPayableComplex
        '            For Each ap As AccountPayableShares In _listAccountPayableShares
        '                If ap.Balance > 0 Then
        '                    apy = New AccountPayableComplex()
        '                    With apy
        '                        .IdAccountPayable = ap.IdAccountPayable
        '                        .IdAccountPayableShare = ap.Id
        '                        .BillNumber = ap.AccountPayable.BillNumber
        '                        .BillDate = ap.AccountPayable.BillDate
        '                        .ValueShare = ap.Balance '.AccountPayable.Value
        '                        .Share = ap.Share
        '                    End With
        '                    AccounPayableDatasource.Add(apy)
        '                End If
        '            Next
        '            If AccounPayableDatasource.Count = 0 Then
        '                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("NoInvoiceThird", NAME_MODULE_PAYMENT), INDsleThirdParty.Text.Split("-").ElementAt(0), INDsleAccount.Text.Split("-").ElementAt(0))
        '            End If
        '            INDGcInvoices.DataSource = Nothing
        '            INDGcInvoices.DataSource = AccounPayableDatasource
        '        Else
        '            _datasourcePaymentConceptLoad = False
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("NoInvoiceThird", NAME_MODULE_PAYMENT), INDsleThirdParty.Text.Split("-").ElementAt(0), INDsleAccount.Text.Split("-").ElementAt(0))
        '    End If
        'End Using
    End Sub

    ''' <summary>
    ''' Validates the controls expense.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsExpense() As Boolean
        _listaValidaciones = New StringBuilder()
        Dim ValidationResult As Boolean = True
        'validaciones comunes
        If INDsleExpenseConcept.EditValue Is Nothing Then
            ValidationResult = False
            _listaValidaciones.AppendLine(INDliConcept.Text)
        End If
        If INDgleNature.EditValue Is Nothing Then
            ValidationResult = False
            _listaValidaciones.AppendLine(INDlyItemNature.Text)
        End If
        If INDsleThirdParty.EditValue Is Nothing Then
            ValidationResult = False
            _listaValidaciones.AppendLine(INDliThirdParty.Text)
        End If
        If INDsleAccount.EditValue Is Nothing Then
            ValidationResult = False
            _listaValidaciones.AppendLine(INDliAccountAccounting.Text)
        End If

        'validacion de la retención
        If INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            ValidationResult = ValidationResult And ValidateControlsRetentionConcept() 'Operacion AND
        End If
        'validaciones específicas de cada comportamiento
        Select Case Behavior
            Case CInt(eBehavior.TransferBetweenBanks)  'Traslado entre bancos
                If INDsleEntityAccount.EditValue Is Nothing Then
                    ValidationResult = False
                    _listaValidaciones.AppendLine(INDliEntityAccount.Text)
                End If
                If INDtxtValue.EditValue = 0 Then
                    ValidationResult = False
                    _listaValidaciones.AppendLine(INDlyItemValue.Text)
                End If
            Case CInt(eBehavior.PettyCash)  'Caja Menor
                If INDtxtValue.EditValue = 0 Then
                    ValidationResult = False
                    _listaValidaciones.AppendLine(INDlyItemValue.Text)
                End If
            Case CInt(eBehavior.PaymentAdvancePaymentInvoices)  'Pago/Anticipo Facturas CxP
                If AccounPayableDatasource IsNot Nothing Then
                    For Each acc As AccountPayableComplex In AccounPayableDatasource
                        If acc.IdPaymentConcept = 0 Then
                            If acc.PercentValue <> 0 Or acc.PayValue <> 0 Then
                                ValidationResult = False
                                '_listaValidaciones.AppendLine(acc.Text)
                                'Return False
                            End If
                        Else
                            If acc.PercentValue = 0 Or acc.PayValue = 0 Then
                                ValidationResult = False
                            End If
                        End If
                    Next
                End If
            Case CInt(eBehavior.ReturningImprestRC)  'Devolutivos de Anticipos RC
            Case CInt(eBehavior.PettyCashReimbursement)  'Reembolso de Caja Menor
        End Select
        Return ValidationResult
    End Function

    ''' <summary>
    ''' Valida los controles de conceptos de retencion
    ''' </summary>
    Private Function ValidateControlsRetentionConcept() As Boolean
        If INDsleConceptRetention.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemConceptRetention.Text)
        End If
        If INDsePercentage.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemPercentage.Text)
        End If
        If INDtxtRetentionValue.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemBaseValue.Text)
        End If
        If INDtxtBaseValue.EditValue Is Nothing Then
            _listaValidaciones.AppendLine(INDlyItemRetentionValue.Text)
        End If
        If INDsleConceptRetention.EditValue Is Nothing Or INDsePercentage.EditValue Is Nothing Or INDtxtRetentionValue.EditValue Is Nothing Or INDtxtBaseValue.EditValue Is Nothing Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Método que convierte valor a porcentaje y lo asigna al campo porcentaje de la rejilla
    ''' </summary>
    Private Sub ConvertValueToPercent()
        Dim accountPayableComplx As AccountPayableComplex = DirectCast(INDGvInvoices.GetFocusedRow, AccountPayableComplex)
        If accountPayableComplx.PayValue <> 0 Then
            If accountPayableComplx.PayValue > accountPayableComplx.ValueShare Then
                accountPayableComplx.PayValue = 0
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("PayValueMoreAccountPayableValue", NAME_MODULE)
            End If
            accountPayableComplx.PercentValue = TreasuryStaticServices.ConvertInvoiceValueToPercent(accountPayableComplx.PayValue, accountPayableComplx.ValueShare)
            UpdateBaseValue()
        End If
    End Sub

    ''' <summary>
    ''' Método que convierte de porcentaje a valor en pesos
    ''' </summary>
    Private Sub ConvertPercentToValue()
        Dim accountPayableComplx As AccountPayableComplex = DirectCast(INDGvInvoices.GetFocusedRow, AccountPayableComplex)
        If accountPayableComplx.PercentValue <> 0 Then
            accountPayableComplx.PayValue = TreasuryStaticServices.ConvertInvoicePercentToValue(accountPayableComplx.PercentValue, accountPayableComplx.ValueShare)
            UpdateBaseValue()
        End If
    End Sub

    Private Async Sub UpdateBaseValue()
        'Dim value As Decimal = 0
        'For Each apComplex As AccountPayableComplex In AccountPayableDatasource
        '    value = value + apComplex.Value
        'Next
    End Sub

    Public Function getVoucherTransactionD() As VoucherTransactionDetails
        Return _voucherDetail
    End Function

    ''' <summary>
    ''' Asigna los valores a la entidad detalle
    ''' </summary>
    Private Sub AssigningValues()
        _voucherDetail = New VoucherTransactionDetails() With { _
            .IdThirdParty = ThirdParthy,
            .IdExpenseConcept = ExpenseConcept,
            .IdEntityBankAccount = EntityBankAccount,
            .IdMainAccount = AccountAccouting,
            .Nature = Nature,
            .IdCostCenter = CostCenter,
            .Value = ValueConcept,
            .PercentRetention = PercentRetention,
            .IdRetentionConcept = IdRetentionConcept,
            .Detail = Observation
        }
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' Handles the Load event of the CtrDisbursementVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrDisbursementVoucher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'IndigoGridControl1.RefreshGrid(INDGcInvoices)
        IndigoGridControl1.RefreshGrid(INDgcAdvancePayment)
        IndigoGridControl1.RefreshGrid(INDgcRefound)
        'Me.IsEditMode = False
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleExpenseConcept.EditValueChanged
        If INDsleExpenseConcept.EditValue IsNot Nothing Then
            If INDsleExpenseConcept.EditValue > 0 Then
                _isEditValueConcept = True
                Using Model As New MExpenseConcepts(Me._myTag)
                    Dim Exp As ExpenseConcepts = Model.GetExpenseConceptByIdSimple(ExpenseConcept)
                    Me._behavior = Exp.Behavior
                    If Exp.IdMainAccount > 0 Then
                        INDliAccountAccounting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        INDsleAccount.EditValue = Exp.IdMainAccount
                    Else
                        INDliAccountAccounting.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        INDsleAccount.EditValue = Nothing
                    End If
                    If Exp IsNot Nothing Then
                        CleanLayouts()
                        Select Case Exp.Behavior
                            Case CInt(eBehavior.TransferBetweenBanks)  'Traslado entre bancos
                                INDliEntityAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                'INDtxtValue.Properties.ReadOnly = False
                                INDtxtValue.Enabled = True
                                'Preguntar por el banco al cual se le va a hacer la transferencia (diferente al seleccionado en la cabecera)
                            Case CInt(eBehavior.PettyCash)  'Caja Menor
                                'INDtxtValue.Properties.ReadOnly = False
                                INDlgrInvoicePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDtxtValue.Enabled = True
                                LoadPaymentInvoicesByThird()
                            Case CInt(eBehavior.PaymentAdvancePaymentInvoices)  'Pago/Anticipo Facturas CxP
                                INDlgrInvoicePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDtxtValue.EditValue = 0
                                'INDtxtValue.Properties.ReadOnly = True
                                INDtxtValue.Enabled = False
                                LoadPaymentInvoicesByThird()
                            Case CInt(eBehavior.ReturningImprestRC)  'Devolutivos de Anticipos RC
                                INDlgrAdvancePayments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDtxtValue.EditValue = 0
                                'INDtxtValue.Properties.ReadOnly = True
                                INDtxtValue.Enabled = False
                                'cargar facturas de recibos de caja que tengan anticipos del tercero seleccionado
                            Case CInt(eBehavior.PettyCashReimbursement)  'Reembolso de Caja Menor
                                INDlgrRefound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                                INDtxtValue.EditValue = 0
                                'INDtxtValue.Properties.ReadOnly = True
                                INDtxtValue.Enabled = False
                        End Select
                    End If
                End Using
            End If
        Else
            _isEditValueConcept = False
        End If
    End Sub

    ''' <summary>
    ''' Oculta los layouts por defecto
    ''' </summary>
    Private Sub CleanLayouts()
        INDlgrInvoicePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDliEntityAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlgrAdvancePayments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlgrRefound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleConcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleExpenseConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmExpenseConcepts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                If _idCashRegister > 0 Then
                    Me.InitializeExpenseConceptByCash()
                Else
                    Me.InitializeExpenseConceptNotCash()
                End If
            End Using
        End If
    End Sub

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
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeEntityBankAccount()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeAccountAccounting()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleConceptRetention control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConceptRetention_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleConceptRetention.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmRetention
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                'Me.initializeRetention()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, Formulario.Size.Height)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Me.InitializeThird()
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
                Me.InitializeCostCenter()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccount.EditValueChanged
        If INDsleAccount.EditValue IsNot Nothing Then
            Using Model As New MPUC(Me._myTag)
                Dim puc As MainAccounts = Await Model.GetAccountById(INDsleAccount.EditValue)
                If puc.HandlesCostCenter Then
                    INDliCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDliCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDsleCostCenter.EditValue = Nothing
                End If
                'reviso si la cuenta tiene retencion
                If puc.RetencionType <> 0 Then
                    INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    'INDtxtValue.Properties.ReadOnly = True
                    INDtxtValue.Enabled = False
                    ActionsOnControlsConceptRetention = True
                Else
                    ActionsOnControlsConceptRetention = False
                    INDsleConceptRetention.EditValue = Nothing
                    CleanControlsRetention()
                    INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the RepositoryItemPercentPay control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemPercentPay_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemPercentPay.KeyDown
        If e.KeyCode = Keys.Enter Then
            ConvertPercentToValue()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the RepositoryItemAmountPay control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemAmountPay_KeyDown(sender As Object, e As KeyEventArgs) Handles RepositoryItemAmountPay.KeyDown
        If e.KeyCode = Keys.Enter Then
            ConvertValueToPercent()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al perder el foco del control y que convierte de valor a pagar a porcentaje a pagar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemAmountPay_Leave(sender As Object, e As EventArgs) Handles RepositoryItemAmountPay.Leave
        ConvertValueToPercent()
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al perder el foco del control y que convierte de porcentaje a pagar a valor a pagar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub RepositoryItemPercentPay_Leave(sender As Object, e As EventArgs) Handles RepositoryItemPercentPay.Leave
        ConvertPercentToValue()
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntityAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityAccount.EditValueChanged
        If INDsleEntityAccount.EditValue IsNot Nothing AndAlso INDsleEntityAccount.EditValue > 0 Then
            If INDsleEntityAccount.EditValue = _idEntityBankAccount Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("EntityBankAccountRepeat", NAME_MODULE)
                INDsleEntityAccount.EditValue = Nothing
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If ValidateControlsExpense() Then
            If Behavior = CInt(eBehavior.PaymentAdvancePaymentInvoices) Then
                Dim payValueInvoice As Decimal = 0
                AccounPayableDatasource = CType(INDGcInvoices.DataSource, List(Of AccountPayableComplex))
                payValueInvoice = AccounPayableDatasource.Sum(Function(x) x.PayValue)
                ValueConcept = CStr(TreasuryStaticServices.CalculateExpenseValue(payValueInvoice, ValueAdvancePayment))
            End If
            AssigningValues()
            INDsleExpenseConcept.Focus()
            RaiseEvent AddExpenseConcept()
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("EmptyFields", NAME_MODULE), _listaValidaciones)
            _listaValidaciones = Nothing
        End If
    End Sub


    Private _retConcept As RetentionConcepts
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleConceptRetention control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleConceptRetention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptRetention.EditValueChanged
        If INDsleConceptRetention.EditValue IsNot Nothing AndAlso INDsleConceptRetention.EditValue > 0 Then
            Using Model As New MRetentionConcept(Me._myTag)
                CleanControlsRetention()
                _retConcept = Model.GetRetentionByIdSimple(INDsleConceptRetention.EditValue)
                _minBaseRetention = _retConcept.MinBase
                _retentiontype = _retConcept.Retention
                _listAccountingRetention = _retConcept.RetentionConceptRanges.ToList()
                If _retConcept.Id > 0 Then
                    Select Case _retConcept.Retention
                        Case CInt(eRetentionType.Base)
                            INDsePercentage.EditValue = _retConcept.Rate
                            INDtxtBaseValue.Focus()
                        Case CInt(eRetentionType.Rango)
                            INDtxtBaseValue.Focus()
                        Case CInt(eRetentionType.Variable)
                            'INDsePercentage.Properties.ReadOnly = False
                            INDsePercentage.Enabled = True
                            INDsePercentage.Focus()
                    End Select
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the Leave event of the INDtxtInvoicedValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtInvoicedValue_Leave(sender As Object, e As EventArgs) Handles INDtxtBaseValue.Leave
        If INDtxtBaseValue.EditValue IsNot Nothing AndAlso INDtxtBaseValue.EditValue > 0 Then
            If _minBaseRetention > INDtxtBaseValue.EditValue Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvoiceValueLessMinBase", NAME_MODULE)
                INDtxtBaseValue.EditValue = Nothing
                INDtxtRetentionValue.EditValue = Nothing
                Exit Sub
            End If
            Select Case _retentiontype
                Case CInt(eRetentionType.Rango), CInt(eRetentionType.Base)
                    Try
                        Dim retentionValue As Decimal = AccountingServices.CalculateRetention(CDec(INDtxtBaseValue.EditValue), _retConcept)
                        INDtxtRetentionValue.EditValue = retentionValue
                        INDtxtValue.EditValue = retentionValue
                        'Parcial
                        If _retentiontype = CInt(eRetentionType.Rango) Then
                            INDsePercentage.EditValue = CInt(INDtxtRetentionValue.EditValue) * 100 / CInt(INDtxtBaseValue.EditValue)
                        End If
                        'Esto debido a que es imposible calcular el porcentaje solo con el valor de la retencion y la retencion cuando el porcentaje se saca de otro lado
                    Catch ex As Exception
                        Mensaje(EeventViewerImages.Advertencia) = ex.Message
                    End Try
                Case CInt(eRetentionType.Variable)
                    If INDsePercentage.EditValue > 0 Or INDtxtBaseValue.EditValue > 0 Then
                        Try
                            Dim retentionValue As Decimal = AccountingServices.CalculateRetention(CDec(INDtxtBaseValue.EditValue), _retConcept, PercentRetention)
                            INDtxtRetentionValue.EditValue = retentionValue
                            INDtxtValue.EditValue = retentionValue
                        Catch ex As Exception
                            Mensaje(EeventViewerImages.Advertencia) = ex.Message
                        End Try
                    End If
            End Select
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDtxtRetentionValue control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtRetentionValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtRetentionValue.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlgrInvoicePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDsbAdd.Focus()
            Else
                INDtxtAdvancePayment.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDmemoComments control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDmemoComments_KeyDown(sender As Object, e As KeyEventArgs) Handles INDmemoComments.KeyDown
        If e.KeyCode = Keys.Enter Then
            If INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never And INDlgrInvoicePayment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                INDsbAdd.Focus()
            ElseIf INDlgrConceptRetention.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                INDsleConceptRetention.Focus()
            Else
                INDtxtAdvancePayment.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDtxtAdvancePayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtAdvancePayment_KeyDown(sender As Object, e As KeyEventArgs) Handles INDtxtAdvancePayment.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDsbAdd.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the LostFocus event of the INDtxtAdvancePayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDtxtAdvancePayment_LostFocus(sender As Object, e As EventArgs) Handles INDtxtAdvancePayment.LostFocus
        'Dim suma As Decimal = 0
        'For Each ap As AccountPayable In _listAccountPayable
        '    suma = suma + ap.Value
        'Next
        'BaseValue = suma
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbCancel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbCancel_Click(sender As Object, e As EventArgs) Handles INDsbCancel.Click
        IsEditMode = False
        CleanControls()
        CleanControlsRetention()
        RaiseEvent ExitPopUp()
    End Sub

#End Region

#Region "Enum"

    ''' <summary>
    ''' Enumeracion del comportamiento de los conceptos de caja menor
    ''' </summary>
    Public Enum eBehavior
        ''' <summary>
        ''' Traslado entre bancos
        ''' </summary>
        TransferBetweenBanks = 1
        ''' <summary>
        ''' Caja Menor
        ''' </summary>
        PettyCash = 2
        ''' <summary>
        ''' Pago/Anticipo de Facturas CxP
        ''' </summary>
        PaymentAdvancePaymentInvoices = 3
        ''' <summary>
        ''' Devolutivos de Anticipos RC
        ''' </summary>
        ReturningImprestRC = 4
        ''' <summary>
        '''  Reembolso de Caja Menor
        ''' </summary>
        PettyCashReimbursement = 5
    End Enum

    Public Enum eStatusAccountPayable
        Registrado = 1
        Confirmado = 2
        Anulado = 3
    End Enum

    ''' <summary>
    ''' Obtiene el tipo de retencion
    ''' </summary>
    Public Enum eRetentionType
        Base = 1
        Rango = 2
        Variable = 3
    End Enum

#End Region

End Class
