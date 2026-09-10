'***********************************************************************
' Assembly         : Presentacion.Treasury
' Author           : Diego Andrés Roldán
' Created          : 17-03-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base.Extension
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

''' <summary>
''' Formulario de Entidades de Cuentas
''' </summary>
Public Class FrmEntityAccount
    Implements IEntityAccount, ICustomizableForm

#Region "Properties and Variables"

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' The lista usuarios
    ''' </summary>
    Dim ListaUsuarios As New List(Of User)

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Public Const NAME_MODULE As String = "Treasury"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    Public ReadOnly Property MyTag As Object Implements IEntityAccount.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IEntityAccount.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' variable que contiene la cabecera de la secuencia
    ''' </summary>
    Dim _sequence As TreasurySequence

    ''' <summary>
    ''' variable que contiene el id del detalle de la secuencia
    ''' </summary>
    Dim _idCurrentSequence As Int64

    ''' <summary>
    ''' Gets or sets the user code.
    ''' </summary>
    ''' <value>
    ''' The user code.
    ''' </value>
    Public Property UserCode As String
        Get
            Return CType(INDsleUser.EditValue, String)
        End Get
        Set(value As String)
            INDsleUser.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la cabecera de la secuencia
    ''' </summary>
    ''' <value>
    ''' The sequence.
    ''' </value>
    Public Property Sequence As TreasurySequence Implements IEntityAccount.Sequence
        Get
            Return _sequence
        End Get
        Set(value As TreasurySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de la entidad de la cuenta
    ''' </summary>
    ''' <value>
    ''' The code entity account.
    ''' </value>
    Public Property Code As String Implements IEntityAccount.Code
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
    ''' Gets or sets the account accounting.
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Public Property AccountAccounting As Integer Implements IEntityAccount.AccountAccounting
        Get
            Return INDsleAccountAccounting.EditValue
        End Get
        Set(value As Integer)
            INDsleAccountAccounting.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Prefijo
    ''' </summary>
    Public Property Prefix As String Implements IEntityAccount.Prefix
        Get
            Return INDtePrefix.Text
        End Get
        Set(value As String)
            INDtePrefix.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el banco
    ''' </summary>
    ''' <value>
    ''' The bank entity account.
    ''' </value>
    Public Property BankEntityAccount As Integer Implements IEntityAccount.BankEntityAccount
        Get
            Return INDsleBankEntityAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleBankEntityAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the cost center.
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Public Property CostCenter As Nullable(Of Integer) Implements IEntityAccount.CostCenter
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Nullable(Of Integer))
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the cost center.
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Public Property FMGCounterpartCostCenterId As Nullable(Of Integer) Implements IEntityAccount.FMGCounterpartCostCenterId
        Get
            Return INDSleCostCenterMainAccountCounterpart.EditValue
        End Get
        Set(value As Nullable(Of Integer))
            INDSleCostCenterMainAccountCounterpart.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Gets or sets the cost center.
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Public Property FMGExpenseCostCenterId As Nullable(Of Integer) Implements IEntityAccount.FMGExpenseCostCenterId
        Get
            Return INDSleCostCenterMainAccountExpenses.EditValue
        End Get
        Set(value As Nullable(Of Integer))
            INDSleCostCenterMainAccountExpenses.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Obtiene o establece el saldo inicial
    ''' </summary>
    ''' <value>
    ''' The initialize balance.
    ''' </value>
    Public Property InitialBalance As Decimal Implements IEntityAccount.InitialBalance
        Get
            Return INDtxtInitialBalance.EditValue
        End Get
        Set(value As Decimal)
            INDtxtInitialBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el saldo actual
    ''' </summary>
    ''' <value>
    ''' The current balance.
    ''' </value>
    Public Property CurrentBalance As Decimal Implements IEntityAccount.CurrentBalance
        Get
            Return CDec(INDtxtCurrentBalance.EditValue)
        End Get
        Set(value As Decimal)
            INDtxtCurrentBalance.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the datasource main account payment.
    ''' </summary>
    ''' <value>
    ''' The datasource main account payment.
    ''' </value>
    Public Property DatasourceMainAccountCounterpart As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.DatasourceMainAccountPayment
        Get
            Return CType(INDsleFMGMainAccountCounterpart.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFMGMainAccountCounterpart.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the datasource main account expense.
    ''' </summary>
    ''' <value>
    ''' The datasource main account expense.
    ''' </value>
    Public Property DatasourceMainAccountExpense As XPInstantFeedbackSource Implements IEntityAccount.DatasourceMainAccountExpense
        Get
            Return CType(INDsleFMGMainAccountExpenses.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleFMGMainAccountExpenses.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the FMG counterpart main account identifier.
    ''' </summary>
    ''' <value>
    ''' The FMG counterpart main account identifier.
    ''' </value>
    Public Property FMGCounterpartMainAccountId As Integer
        Get
            Return CType(INDsleFMGMainAccountCounterpart.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleFMGMainAccountCounterpart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the FMG expense main account identifier.
    ''' </summary>
    ''' <value>
    ''' The FMG expense main account identifier.
    ''' </value>
    Public Property FMGExpenseMainAccountId As Integer
        Get
            Return CType(INDsleFMGMainAccountExpenses.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleFMGMainAccountExpenses.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the initialize date.
    ''' </summary>
    ''' <value>
    ''' The initialize date.
    ''' </value>
    Public Property InitDate As Date Implements IEntityAccount.InitDate
        Get
            Return INDdteInitDate.EditValue
        End Get
        Set(value As Date)
            INDdteInitDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the number.
    ''' </summary>
    ''' <value>
    ''' The number.
    ''' </value>
    Public Property Number As String Implements IEntityAccount.Number
        Get
            Return INDtxtNumber.EditValue
        End Get
        Set(value As String)
            INDtxtNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the quota overdraft.
    ''' </summary>
    ''' <value>
    ''' The quota overdraft.
    ''' </value>
    Public Property Quota As Decimal Implements IEntityAccount.Quota
        Get
            Return INDtxtQuota.EditValue
        End Get
        Set(value As Decimal)
            INDtxtQuota.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la ciudad de radicacion
    ''' </summary>
    ''' <value>
    ''' The radication city.
    ''' </value>
    Public Property City As Integer Implements IEntityAccount.City
        Get
            Return INDsleCity.EditValue
        End Get
        Set(value As Integer)
            INDsleCity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la tasa x mil
    ''' </summary>
    ''' <value>
    ''' The rate.
    ''' </value>
    Public Property Rate As Decimal Implements IEntityAccount.Rate
        Get
            Return INDtxtRate.EditValue
        End Get
        Set(value As Decimal)
            INDtxtRate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de usuarios
    ''' </summary>
    ''' <value>
    ''' The data source user.
    ''' </value>
    Public Property DataSourceUser As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IEntityAccount.DataSourceUser
        Get
            Return CType(INDsleUser.Datasource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUser.Datasource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de bancos
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Public Property BankDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.BankDatasource
        Get
            Return CType(INDsleBankEntityAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBankEntityAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fuente de datos para ciudades
    ''' </summary>
    ''' <value>
    ''' The city datasource.
    ''' </value>
    Public Property CityDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.CityDatasource
        Get
            Return CType(INDsleCity.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Public Property AccountAccountingDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.AccountAccountingDatasource
        Get
            Return CType(INDsleAccountAccounting.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAccountAccounting.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.CostCenterDatasource
        Get
            Return CType(INDsleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasourceExpenses As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.CostCenterDatasourceExpenses
        Get
            Return CType(INDSleCostCenterMainAccountExpenses.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenterMainAccountExpenses.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Public Property CostCenterDatasourceCounterpart As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.CostCenterDatasourceCounterpart
        Get
            Return CType(INDSleCostCenterMainAccountCounterpart.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCostCenterMainAccountCounterpart.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Datasource de Terceros
    ''' </summary>
    Public Property ThirdPartyDatasourceExpenses As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.ThirdPartyDatasourceExpenses
        Get
            Return CType(INDSleThirdMainAccountExpenses.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleThirdMainAccountExpenses.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Datasource de Terceros
    ''' </summary>
    Public Property ThirdPartyDatasourceCounterpart As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.ThirdPartyDatasourceCounterpart
        Get
            Return CType(INDSleThirdMainAccountCounterpart.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleThirdMainAccountCounterpart.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de Terceros
    ''' </summary>
    Public Property ThirdPartyDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.ThirdPartyDatasource
        Get
            Return CType(INDsleThirdParty.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad permite obtener y establecer la fuente de datos asociada a un control XPInstantFeedbackSource
    ''' llamado INDSleCurrency dentro del contexto de una entidad de cuenta, según lo definido por la interfaz IEntityAccount.
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyDatasource As DevExpress.Xpo.XPInstantFeedbackSource Implements IEntityAccount.CurrencyDataSource
        Get
            Return CType(INDSleCurrency.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCurrency.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad permite obtener y establecer el identificador de moneda asociado a un control
    ''' INDSleCurrency dentro del contexto de una entidad de cuenta, según lo definido por la interfaz IEntityAccount.
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyId As Integer Implements IEntityAccount.CurrencyId
        Get
            Return INDSleCurrency.EditValue
        End Get
        Set(value As Integer)
            INDSleCurrency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value>
    ''' The third party identifier.
    ''' </value>
    Public Property ThirdPartyId As Integer? Implements IEntityAccount.ThirdPartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value>
    ''' The third party identifier.
    ''' </value>
    Public Property FMGCounterpartThirdPartyId As Integer? Implements IEntityAccount.FMGCounterpartThirdPartyId
        Get
            Return INDSleThirdMainAccountCounterpart.EditValue
        End Get
        Set(value As Integer?)
            INDSleThirdMainAccountCounterpart.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value>
    ''' The third party identifier.
    ''' </value>
    Public Property FMGExpenseThirdPartyId As Integer? Implements IEntityAccount.FMGExpenseThirdPartyId
        Get
            Return INDSleThirdMainAccountExpenses.EditValue
        End Get
        Set(value As Integer?)
            INDSleThirdMainAccountExpenses.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Public Property State As Boolean Implements IEntityAccount.State
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de la cuenta
    ''' </summary>
    ''' <value>
    ''' The type.
    ''' </value>
    Public Property Type As Integer Implements IEntityAccount.Type
        Get
            Return INDgleType.EditValue
        End Get
        Set(value As Integer)
            INDgleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the datasource grid user.
    ''' </summary>
    ''' <value>
    ''' The datasource grid user.
    ''' </value>
    Property DatasourceGridUser As List(Of User) Implements IEntityAccount.DatasourceGridUser
        Get
            Return CType(INDGcUser.DataSource, List(Of User))
        End Get
        Set(value As List(Of User))
            INDGcUser.DataSource = value
            If value IsNot Nothing Then
                ListaUsuarios = value
            Else
                ListaUsuarios.Clear()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Presentador de Entidades de Cuentas
    ''' </summary>
    Dim Presenter As PEntityAccount

    ''' <summary>
    ''' variable de tipo chequera
    ''' </summary>
    Dim checkBook As Checkbooks

    ''' <summary>
    ''' variable que almacena el registro bloqueado
    ''' </summary>
    Dim record As BlockRecordTreasury

    ''' <summary>
    ''' Variable de la entidad de cuentas bancarias de entidades
    ''' </summary>
    Dim EntityBankAccount As EntityBankAccounts

    ''' <summary>
    ''' contiene la lista a llenar en el tipo de cuenta
    ''' </summary>
    Dim TypeFile As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de chequeras
    ''' </summary>
    Dim ListVoucer As New List(Of Checkbooks)

    ''' <summary>
    ''' Listado con los estados de las chequeras
    ''' </summary>
    Dim StatusVoucher As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable para almacenar las monedas
    ''' </summary>
    Dim CurrencyAbbreviation As String

    ''' <summary>
    ''' bandera para saber cuando se abre por primera vez el popUp de terceros
    ''' </summary>
    Property _stateOpenPopUpThird As Boolean

    ''' <summary>
    '''  Ultimo valor de la revalorizacion
    ''' </summary>
    Dim _periodLastRevaluation As Integer

    ''' <summary>
    ''' Obtiene o establece el datasource para el campo moneda
    ''' </summary>
    Dim _balanceLastRevaluation As Decimal

    ''' <summary>
    ''' esta propiedad permite obtener y establecer el período de la última revaluación
    ''' asociado a una entidad de cuenta según lo definido por la interfaz IEntityAccount.
    ''' </summary>
    ''' <returns></returns>
    Property PeriodLastRevaluation As Integer Implements IEntityAccount.PeriodLastRevaluation
        Get
            Return _periodLastRevaluation
        End Get
        Set(value As Integer)
            _periodLastRevaluation = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad permite obtener y establecer el saldo de la última revaluación asociado
    ''' a una entidad de cuenta según lo definido por la interfaz IEntityAccount.
    ''' </summary>
    ''' <returns></returns>
    Property BalanceLastRevaluation As Decimal Implements IEntityAccount.BalanceLastRevaluation
        Get
            Return _balanceLastRevaluation
        End Get
        Set(value As Decimal)
            _balanceLastRevaluation = value
        End Set
    End Property

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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.EntityBankAccount IsNot Nothing AndAlso Me.EntityBankAccount.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEntityAccount(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEntityBankAccount(Me.EntityBankAccount)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
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
        If ValidateControls() = True Then
            'If INDlycgCheckbook.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            '    If INDgdvCheckbook.RowCount = 0 Then
            '        Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una chequera"
            '        Exit Sub
            '    End If
            'End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MEntityAccount(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of EntityBankAccounts) = Await Model.SaveEntityBankAccount(Me.EntityBankAccount, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If EntityBankAccount.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.EntityBankAccount = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await Me.NewEntityAccount()
        End If
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Carga las chequeras en la rejilla
    ''' </summary>
    Private Sub LoadVoucher()
        INDgcCheckbook.DataSource = EntityBankAccount.Checkbooks
    End Sub

    ''' <summary>
    ''' Creates the list status voucher.
    ''' </summary>
    Private Sub CreateListStatusVoucher()
        StatusVoucher.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("StatusVoucherActive", NAME_MODULE)))
        StatusVoucher.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("StatusVoucherBlocked", NAME_MODULE)))
        StatusVoucher.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("StatusVoucherClosed", NAME_MODULE)))
        INDgleStatusVoucher.Properties.DataSource = StatusVoucher
        RepositoryStatusVoucher.DataSource = StatusVoucher
    End Sub

    ''' <summary>
    ''' Crea la lista de tipo de cuenta
    ''' </summary>
    Public Sub CreateTypeFile()
        TypeFile.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("AccountTypeCurrent")))
        TypeFile.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("AccountTypeSaving")))

        INDgleType.Properties.DataSource = TypeFile
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
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
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Banco", .FieldName = "IdBank.Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3},
                              New ColumnInfo() With {.Caption = "Moneda", .FieldName = "CurrencyAbbreviation", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1},
                              New ColumnInfo() With {.Caption = "Número Cuenta", .FieldName = "Number", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Cuenta Contable", .FieldName = "IdMainAccount.NumberName", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllEntityBankAccount
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Retorna el valor obtenido por el formulario de busqueda
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEntityAccount.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDsleBankEntityAccount.Enabled = value
            INDsleCity.Enabled = value
            INDgleType.Enabled = value
            INDtxtInitialBalance.Enabled = value
            INDtxtRate.Enabled = value
            INDtxtNumber.Enabled = value
            'INDtxtQuota.Enabled = value
            INDdteInitDate.Enabled = value
            INDsleAccountAccounting.Enabled = value
            INDtxtCurrentBalance.Enabled = value
            INDtxtQuota.Enabled = value
            INDsleThirdParty.Enabled = value
            'INDsleCostCenter.Enabled = value
            'INDgdcGeneralConsecutive.Enabled = value
            'INDgdcDocumentsConsecutive.Enabled = value
            INDgcCheckbook.Enabled = value
            INDsleUser.Enabled = value
            INDGcUser.Enabled = value
            INDsleFMGMainAccountCounterpart.Enabled = value
            INDsleFMGMainAccountExpenses.Enabled = value
            INDtePrefix.Enabled = value

            INDSleThirdMainAccountCounterpart.Enabled = value
            INDsleCostCenter.Enabled = value
            INDSleCostCenterMainAccountCounterpart.Enabled = value
            INDSleThirdMainAccountExpenses.Enabled = value
            INDSleCostCenterMainAccountExpenses.Enabled = value
            INDsleFinancialSource.Enabled = value
            INDsbAddUser.Enabled = value

            'Voucher
            INDpopVoucher.Enabled = value
            INDtxtInitialNumber.Enabled = value
            INDtxtEndNumber.Enabled = value
            INDtxtCurrentNumber.Enabled = value
            INDgleStatusVoucher.Enabled = value
            INDSleCurrency.Enabled = value

            INDlycRoot.EndUpdate()
            If value Then
                INDsleBankEntityAccount.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Generates the document indexed.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.EntityBankAccount.Code, Me.EntityBankAccount.IdBank),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.EntityBankAccount.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.EntityBankAccount.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.EntityBankAccount.Code, Me.EntityBankAccount.IdBank)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.EntityBankAccount.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Loads the status.
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlycRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        INDbteCode.Text = Nothing
        INDsleBankEntityAccount.EditValue = Nothing
        INDsleCity.EditValue = Nothing
        INDgleType.EditValue = Nothing
        INDtxtInitialBalance.EditValue = 0
        INDtxtCurrentNumber.EditValue = 0
        INDtxtCurrentBalance.EditValue = 0
        INDtxtRate.EditValue = 0
        INDtxtNumber.Text = String.Empty
        INDtxtQuota.EditValue = 0
        INDdteInitDate.EditValue = Nothing
        INDsleAccountAccounting.EditValue = Nothing
        INDsleCostCenter.EditValue = Nothing
        INDsleFMGMainAccountCounterpart.EditValue = Nothing
        INDSleThirdMainAccountCounterpart.EditValue = Nothing
        INDSleCostCenterMainAccountCounterpart.EditValue = Nothing
        INDsleFMGMainAccountExpenses.EditValue = Nothing
        INDSleThirdMainAccountExpenses.EditValue = Nothing
        INDSleCostCenterMainAccountExpenses.EditValue = Nothing
        INDsleFinancialSource.EditValue = Nothing
        INDsleFinancialSource.Properties.NullText = String.Empty
        INDtePrefix.Text = String.Empty
        INDgcCheckbook.DataSource = Nothing
        Me.EntityBankAccount = Nothing
        Me.PeriodLastRevaluation = 0
        Me.BalanceLastRevaluation = 0
        _stateOpenPopUpThird = False
        INDlycgCheckbook.HideControl(True)
        INDliThirdParty.HideControl(True)
        INDlyciCostCenter.HideControl(True)
        INDLciThirdMainAccountCounterpart.HideControl(True)
        INDLciCostCenterMainAccountCounterpart.HideControl(True)
        INDLciThirdMainAccountExpenses.HideControl(True)
        INDLciCostCenterMainAccountExpenses.HideControl(True)
        INDSleCurrency.EditValue = Nothing
        INDSleCurrency.Properties.NullText = String.Empty
        Me.CurrencyAbbreviation = String.Empty
        Me.SetFormatsControls(Me.CurrencyAbbreviation)
        'Voucher
        CleanControlsVoucher()

        INDsleUser.EditValue = Nothing
        DatasourceGridUser = Nothing
        If ListaUsuarios IsNot Nothing Then
            ListaUsuarios.Clear()
        End If

        State = True

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Cleans the controls voucher.
    ''' </summary>
    Private Sub CleanControlsVoucher()
        INDtxtInitialNumber.Text = String.Empty
        INDtxtEndNumber.Text = String.Empty
        INDtxtCurrentNumber.EditValue = 0
        INDgleStatusVoucher.EditValue = Nothing
        INDtxtPrefix.Text = String.Empty
        checkBook = Nothing
        INDpopVoucher.Text = ResourceManager.GetString("AddCheckBook", NAME_MODULE)
        INDliAdd.HideControl(False)
        INDliEdit.HideControl(True)
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
                Using Model As New MEntityAccount(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetEntityBankAccount(INDbteCode.Text.Trim)
                    Dim _hasMovement As Boolean = False
                    EntityBankAccount = resultOperation.ObjectEmbbeded
                    INDlycRoot.BeginUpdate()
                    If EntityBankAccount IsNot Nothing AndAlso EntityBankAccount.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        record = Await Model.GetBlockRecordTreasury(CStr(Me.Tag), CStr(EntityBankAccount.Id))

                        Dim resultUser = Await Model.ListUsersByEntityBankAccountId(EntityBankAccount.Id)
                        If resultUser.StateResult Then
                            ListaUsuarios = resultUser.ObjectEmbbeded
                        End If

                        With EntityBankAccount
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Code = .Code
                            BankEntityAccount = .IdBank
                            City = .IdCity
                            Type = .Type
                            If .Type = ETypeAccount.Ahorro Then
                                INDgleType.Enabled = False
                            End If
                            Number = .Number
                            InitialBalance = .InitialBalance
                            Rate = .Rate
                            Quota = .Quota
                            InitDate = .InitialDate
                            AccountAccounting = .IdMainAccount
                            CurrentBalance = .CurrentBalance
                            ThirdPartyId = .ThirdPartyId
                            CostCenter = .IdCostCenter
                            FMGCounterpartMainAccountId = .FMGCounterpartMainAccountId
                            FMGCounterpartThirdPartyId = .FMGCounterpartThirdPartyId
                            FMGCounterpartCostCenterId = .FMGCounterpartCostCenterId
                            FMGExpenseMainAccountId = .FMGExpenseMainAccountId
                            FMGExpenseThirdPartyId = .FMGExpenseThirdPartyId
                            FMGExpenseCostCenterId = .FMGExpenseCostCenterId
                            If .CurrencyId IsNot Nothing Then
                                CurrencyId = .CurrencyId
                            Else
                                CurrencyId = indigo.OfficialCurrencyId
                            End If
                            _hasMovement = .hasMovements
                            If INDlyItemFinancialSource.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                                INDsleFinancialSource.EditValue = .FinancialSourceId
                                INDsleFinancialSource.Properties.NullText = .FinancialSourceDescription
                            End If
                            Prefix = .Prefix
                            State = .Status
                            Me.CurrencyAbbreviation = .CurrencyAbbreviation
                            Me.PeriodLastRevaluation = .PeriodLastRevaluation
                            Me.BalanceLastRevaluation = .BalanceLastRevaluation
                        End With
                        LoadVoucher()
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.EntityBankAccount.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecordTreasury(
                                New BlockRecordTreasury With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = EntityBankAccount.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(EntityBankAccount.Id, Me.Tag.ToString(), Nothing, GetType(EntityBankAccounts).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        If _hasMovement Then
                            INDSleCurrency.Enabled = False
                        End If
                        INDGcUser.DataSource = ListaUsuarios
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEntityAccount()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycRoot.EndUpdate()
                End Using
                Me.SetFormatsControls(Me.CurrencyAbbreviation)
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' esta función establece los formatos de visualización y máscaras de controles relacionados con monedas
    ''' en un formulario, utilizando abreviaturas de moneda y formatos numéricos adecuados para la cultura actual.
    ''' </summary>
    ''' <param name="Abbreviation"></param>
    Private Sub SetFormatsControls(Abbreviation As String)
        If String.IsNullOrEmpty(Abbreviation) Then
            Abbreviation = indigo.CurrencyISO4217
        End If
        Me.INDSleCurrency.Properties.NullText = $"{Abbreviation}"
        Dim _culture As Globalization.CultureInfo = Globalization.CultureInfo.CurrentCulture.Clone()
        _culture.NumberFormat = CurrencyAbbreviation.GetNumberFormat
        INDtxtInitialBalance.Properties.Mask.Culture = _culture
        INDtxtCurrentBalance.Properties.Mask.Culture = _culture
        INDtxtQuota.Properties.Mask.Culture = _culture
    End Sub
    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With EntityBankAccount
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .IdBank = BankEntityAccount
            .IdCity = City
            .Type = Type
            .Number = Number
            .InitialBalance = InitialBalance
            .CurrentBalance = CurrentBalance
            .Rate = Rate
            .Quota = Quota
            .InitialDate = InitDate
            .IdMainAccount = AccountAccounting
            .IdCostCenter = CostCenter
            .ThirdPartyId = ThirdPartyId
            .FMGCounterpartMainAccountId = FMGCounterpartMainAccountId
            .FMGCounterpartThirdPartyId = FMGCounterpartThirdPartyId
            .FMGCounterpartCostCenterId = FMGCounterpartCostCenterId
            .FMGExpenseMainAccountId = FMGExpenseMainAccountId
            .FMGExpenseThirdPartyId = FMGExpenseThirdPartyId
            .FMGExpenseCostCenterId = FMGExpenseCostCenterId
            .CurrencyId = CurrencyId
            .BalanceLastRevaluation = Me.BalanceLastRevaluation
            .PeriodLastRevaluation = Me.PeriodLastRevaluation
            If INDlyItemFinancialSource.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .FinancialSourceId = INDsleFinancialSource.EditValue
            Else
                .FinancialSourceId = Nothing
            End If
            .Prefix = Prefix
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using ModelCommonTreasury As New MEntityAccount(Me.Tag)
                Await ModelCommonTreasury.DeleteBlockRecordTreasury(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Crea una nueva entidad de cuentas
    ''' </summary>
    Private Async Function NewEntityAccount() As Task
        EntityBankAccount = New EntityBankAccounts() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
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
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MEntityAccount(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.EntityBankAccount.Code) Then
            Try
                Using model As New MEntityAccount(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.EntityBankAccount.Status
                    Dim result As ActionResult(Of EntityBankAccounts) = Await model.UpdateStateEntityBankAccount(Me.EntityBankAccount.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.EntityBankAccount = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        ListaUsuarios = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Presenter = Nothing
        checkBook = Nothing
        record = Nothing
        EntityBankAccount = Nothing
        TypeFile = Nothing
        ListVoucer = Nothing
        StatusVoucher = Nothing
        _stateOpenPopUpThird = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmEntityAccount control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmEntityAccount_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PEntityAccount(Me)
        Presenter.InitializeBank()
        Presenter.GetSequence()
        Presenter.InitializeAccountAccounting()
        Presenter.InitializeCostCenter()
        Presenter.InitializeCity()
        Presenter.LoadDefinitionLayout()
        Presenter.InitializeUser()
        Presenter.InitializeThirdParty()
        Presenter.InitializeMainAccountExpenses()
        Presenter.InitializeMainAccountPayment()
        Presenter.InitializeThirdPartyExpenses()
        Presenter.InitializeThirdPartyCounterpart()
        Presenter.InitializeCostCenterCounterpart()
        Presenter.InitializeCostCenterExpenses()

        Using model As New MEntityAccount(Me.Tag)
            Me.INDsleUser.FuncQueryOnKeyEnterPressed = AddressOf model.GetUserByCode
        End Using

        'Columna acciones
        Dim listAction As New List(Of eAcciones)
        listAction.Add(eAcciones.Edit)
        Dim listActionUser As New List(Of eAcciones)
        listActionUser.Add(eAcciones.Remove)

        IndigoGridView1.SetListAcction(INDgdvCheckbook, listAction)
        IndigoGridView2.SetListAcction(INDGvUser, listActionUser)
        IndigoGridControl1.RefreshGrid(INDgcCheckbook)
        IndigoGridControl1.RefreshGrid(INDGcUser)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgdvCheckbook.Columns
            If col.Name = "colActions" Then
                col.Width = "420"
            End If
        Next

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvUser.Columns
            If col.Name = "colActions" Then
                col.Width = "400"
            End If
        Next

        LoadStatus()
        Deshacer()
        CreateTypeFile()
        CreateListStatusVoucher()
        If indigo.IndigoCompanyType <> 1 Then 'Si el tipo de compañia es publica o de alcaldias
            INDlycgAccountingInfo.Text = "Información Contable y Presupuesto"
            INDlyItemFinancialSource.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemFinancialSource.AllowHide = False
        Else 'Si el tipo de compañia es privada
            INDlycgAccountingInfo.Text = "Información Contable"
            INDlyItemFinancialSource.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemFinancialSource.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se activa al cargar el formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmEntityAccount_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

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
                    Await Me.NewEntityAccount()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta al seleccionar un registro desde el Vituelf
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.EntityBankAccount IsNot Nothing AndAlso Me.EntityBankAccount.Id > 0 Then
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

    ''' <summary>
    ''' Evento que se ejecuta al cerrar el formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmEntityAccount_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Abre el formulario de bancos
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleBankEntityAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBankEntityAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPayrollBank
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeBank()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleCity control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleCity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCity
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCity()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleAccountAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleAccountAccounting_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAccountAccounting.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeAccountAccounting()
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
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCostCenter()
            End Using
        End If
    End Sub

#Region "EditValueChanged"
    ''' <summary>
    ''' Handles the EditValueChanged event of the INDgleType control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDgleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleType.EditValueChanged
        If INDgleType.EditValue = ETypeAccount.Ahorro Then
            CleanControlsVoucher()
            EntityBankAccount.Checkbooks.Clear()
            INDlycgCheckbook.HideControl()
            INDlyciQuotaOverdraft.HideControl()
            INDlyciQuotaOverdraft.AllowHide = True
            INDtxtQuota.EditValue = 0
        Else
            INDlycgCheckbook.HideControl(False)
            INDlyciQuotaOverdraft.HideControl(False)
            INDlyciQuotaOverdraft.AllowHide = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleAccountAccounting control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsleAccountAccounting_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAccountAccounting.EditValueChanged
        If INDsleAccountAccounting.EditValue IsNot Nothing Then
            Using Model As New MEntityAccount(Me.Tag)
                Dim puc As MainAccounts = Await Model.GetAccountById(INDsleAccountAccounting.EditValue)
                If puc.HandlesCostCenter Then
                    INDlyciCostCenter.HideControl(False)
                Else
                    INDlyciCostCenter.HideControl(True)
                    INDsleCostCenter.EditValue = Nothing
                End If
                If puc.HandlesThirdParty Then
                    INDliThirdParty.HideControl(False)
                Else
                    INDliThirdParty.HideControl(True)
                    INDsleThirdParty.EditValue = Nothing
                End If
            End Using
        Else
            INDlyciCostCenter.HideControl(True)
            INDsleCostCenter.EditValue = Nothing
            INDliThirdParty.HideControl(True)
            INDsleThirdParty.EditValue = Nothing
        End If
    End Sub

    Private Async Sub INDsleFMGMainAccountCounterpart_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFMGMainAccountCounterpart.EditValueChanged
        If INDsleFMGMainAccountCounterpart.EditValue IsNot Nothing Then
            Using Model As New MEntityAccount(Me.Tag)
                Dim puc As MainAccounts = Await Model.GetAccountById(INDsleFMGMainAccountCounterpart.EditValue)
                If puc.HandlesCostCenter Then
                    INDLciCostCenterMainAccountCounterpart.HideControl(False)
                Else
                    INDLciCostCenterMainAccountCounterpart.HideControl(True)
                    INDSleCostCenterMainAccountCounterpart.EditValue = Nothing
                End If
                If puc.HandlesThirdParty Then
                    INDLciThirdMainAccountCounterpart.HideControl(False)
                Else
                    INDLciThirdMainAccountCounterpart.HideControl(True)
                    INDSleThirdMainAccountCounterpart.EditValue = Nothing
                End If
            End Using
        Else
            INDLciCostCenterMainAccountCounterpart.HideControl(True)
            INDSleCostCenterMainAccountCounterpart.EditValue = Nothing
            INDLciThirdMainAccountCounterpart.HideControl(True)
            INDSleThirdMainAccountCounterpart.EditValue = Nothing
        End If
    End Sub

    Private Async Sub INDsleFMGMainAccountExpenses_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFMGMainAccountExpenses.EditValueChanged
        If INDsleFMGMainAccountExpenses.EditValue IsNot Nothing Then
            Using Model As New MEntityAccount(Me.Tag)
                Dim puc As MainAccounts = Await Model.GetAccountById(INDsleFMGMainAccountExpenses.EditValue)
                If puc.HandlesCostCenter Then
                    INDLciCostCenterMainAccountExpenses.HideControl(False)
                Else
                    INDLciCostCenterMainAccountExpenses.HideControl(True)
                    INDSleCostCenterMainAccountExpenses.EditValue = Nothing
                End If
                If puc.HandlesThirdParty Then
                    INDLciThirdMainAccountExpenses.HideControl(False)
                Else
                    INDLciThirdMainAccountExpenses.HideControl(True)
                    INDSleThirdMainAccountExpenses.EditValue = Nothing
                End If
            End Using
        Else
            INDLciCostCenterMainAccountExpenses.HideControl(True)
            INDSleCostCenterMainAccountExpenses.EditValue = Nothing
            INDLciThirdMainAccountExpenses.HideControl(True)
            INDSleThirdMainAccountExpenses.EditValue = Nothing
        End If
    End Sub

    Private Sub INDtxtQuota_EditValueChanged(sender As Object, e As EventArgs) Handles INDtxtQuota.EditValueChanged
        '3 de Enero de 2017, solicitado por Liliana Rojas

        CurrentBalance = CurrentBalance + Quota
    End Sub

#End Region

    ''' <summary>
    ''' Handles the Click event of the INDsbAddVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddVoucher_Click(sender As Object, e As EventArgs) Handles INDsbAddVoucher.Click
        SaveVoucher()
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the INDpopVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDpopVoucher_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpopVoucher.KeyDown
        If e.KeyCode = Keys.Enter Then
            INDpopVoucher.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Saves the voucher.
    ''' </summary>
    Private Sub SaveVoucher(Optional modified As Boolean = False)
        If ValidateCheckBook() Then
            If Not modified Then
                EntityBankAccount.Checkbooks.Add(New Checkbooks With {.InitialNumber = INDtxtInitialNumber.Text, .EndNumber = INDtxtEndNumber.Text, .CurrentNumber = INDtxtCurrentNumber.Text, .Prefix = INDtxtPrefix.Text, .Status = INDgleStatusVoucher.EditValue})
            Else
                For Each vouch As Checkbooks In EntityBankAccount.Checkbooks
                    If vouch.Equals(checkBook) Then
                        vouch.InitialNumber = INDtxtInitialNumber.Text
                        vouch.EndNumber = INDtxtEndNumber.Text
                        vouch.CurrentNumber = INDtxtCurrentNumber.Text
                        vouch.Status = INDgleStatusVoucher.EditValue
                        vouch.Prefix = INDtxtPrefix.Text
                        Exit For
                    End If
                Next
            End If
            If EntityBankAccount.ChangeTracker.State = ObjectState.Unchanged Then
                EntityBankAccount.ChangeTracker.State = ObjectState.Modified
            End If
            CleanControlsVoucher()
            LoadVoucher()
            SendKeys.Send("{F4}")
        End If
    End Sub

    ''' <summary>
    ''' valida los controles de chequera
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateCheckBook() As Boolean
        If CInt(INDtxtEndNumber.Text) < CInt(INDtxtInitialNumber.Text) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FinalNumberLessInitialNumber", NAME_MODULE)
            INDtxtEndNumber.Focus()
            Return False
        End If
        If CInt(INDtxtCurrentNumber.Text) > CInt(INDtxtEndNumber.Text) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FinalNumberLessCurrentNumber", NAME_MODULE)
            INDtxtCurrentNumber.Focus()
            Return False
        End If
        If CInt(INDtxtCurrentNumber.Text) < CInt(INDtxtInitialNumber.Text) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CurrentNumberLessInitialNumber", NAME_MODULE)
            INDtxtCurrentNumber.Focus()
            Return False
        End If
        If INDgleStatusVoucher.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("StatusVoucherNull", NAME_MODULE)
            INDgleStatusVoucher.Focus()
            Return False
        End If
        If INDgleStatusVoucher.EditValue = EStatusVoucher.Activa Then
            For Each vouch As Checkbooks In EntityBankAccount.Checkbooks
                If vouch.Status = EStatusVoucher.Activa Then
                    If checkBook IsNot Nothing Then   'si entra aca es porque es un registro de la rejilla que se está editando
                        If checkBook.Id > 0 Then
                            If Not vouch.Id.Equals(checkBook.Id) Then
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("VoucherStatusActiveExist", NAME_MODULE)
                                Return False
                            End If
                        Else
                            If Not checkBook.Equals(vouch) Then
                                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("VoucherStatusActiveExist", NAME_MODULE)
                                Return False
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("VoucherStatusActiveExist", NAME_MODULE)
                        Return False
                    End If
                End If
            Next
        End If
        Return True
    End Function

    ''' <summary>
    ''' Handles the Click event of the INDsbEdit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbEdit_Click(sender As Object, e As EventArgs) Handles INDsbEdit.Click
        SaveVoucher(True)
    End Sub

    ''' <summary>
    ''' Handles the CloseUp event of the INDpopVoucher control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.CloseUpEventArgs"/> instance containing the event data.</param>
    Private Sub INDpopVoucher_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpopVoucher.CloseUp
        CleanControlsVoucher()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAddUser control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsbAddUser_Click(sender As Object, e As EventArgs) Handles INDsbAddUser.Click
        If INDsleUser.EditValue IsNot Nothing Then
            If ListaUsuarios.FindAll(Function(x) x.UserCode.Equals(UserCode)).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserSelectExist", NAME_MODULE)
                Exit Sub
            End If
            Dim responsible As New EntityBankAccountUser()
            responsible.CodUser = UserCode
            Dim userXpo As Object = Nothing
            If INDgvUsers.GetFocusedRow() IsNot Nothing AndAlso Not INDgvUsers.GetFocusedRow().GetType().Name.Equals("NotLoadedObject") Then
                userXpo = DirectCast(DirectCast(INDgvUsers.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.UserXpo)
            Else
                Using model As New MEntityAccount(Me.Tag)
                    userXpo = Await model.GetUserByCode(UserCode)
                End Using
            End If
            If userXpo Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "El usuario seleccionado no existe"
                Exit Sub
            End If
            Dim user As New User()
            With user
                .Id = userXpo.Id
                .UserCode = userXpo.UserCode
                .Person = New Domain.Security.Entities.Person()
                .Person.Fullname = userXpo.PersonFullName
            End With
            ListaUsuarios.Add(user)

            EntityBankAccount.EntityBankAccountUser.Add(responsible)
            If EntityBankAccount.ChangeTracker.State = ObjectState.Unchanged Then
                EntityBankAccount.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
            End If
            INDsleUser.EditValue = Nothing
            INDGcUser.DataSource = Nothing
            INDGcUser.DataSource = ListaUsuarios
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserSelect", NAME_MODULE)
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
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeThirdParty()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleThirdParty control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If Not _stateOpenPopUpThird Then
            _stateOpenPopUpThird = True
            Presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleCurrency.QueryPopUp
        If INDSleCurrency.Properties.DataSource Is Nothing Then
            Presenter.InitializeCurrency()
        End If
    End Sub

    ''' <summary>
    ''' EditValueChanged
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCurrency.EditValueChanged
        Me.SetFormatsControls(INDSleCurrency.Properties.NullText)
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleFMGMainAccountPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountPayment_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFMGMainAccountCounterpart.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeMainAccountPayment()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleFMGMainAccountPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountPayment_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleFMGMainAccountCounterpart.QueryPopUp
        If INDsleFMGMainAccountCounterpart.Properties.DataSource Is Nothing Then
            Presenter.InitializeMainAccountPayment()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleFMGMainAccountExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountExpenses_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFMGMainAccountExpenses.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPopupPUC()
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeMainAccountExpenses()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleFMGMainAccountExpenses control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleFMGMainAccountExpenses_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleFMGMainAccountExpenses.QueryPopUp
        If INDsleFMGMainAccountExpenses.Properties.DataSource Is Nothing Then
            Presenter.InitializeMainAccountExpenses()
        End If
    End Sub
#End Region

#Region "EventClick"
    Private Sub INDSleThirdMainAccountCounterpart_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdMainAccountCounterpart.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeThirdPartyCounterpart()
            End Using
        End If
    End Sub

    Private Sub INDSleThirdMainAccountExpenses_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleThirdMainAccountExpenses.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeThirdPartyExpenses()
            End Using
        End If
    End Sub

    Private Sub INDSleCostCenterMainAccountCounterpart_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCostCenterMainAccountCounterpart.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCostCenter
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCostCenterCounterpart()
            End Using
        End If
    End Sub

    Private Sub INDSleCostCenterMainAccountExpenses_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleCostCenterMainAccountExpenses.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCostCenter
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 768)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.InitializeCostCenterExpenses()
            End Using
        End If
    End Sub

    Private Sub INDsleFinancialSource_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFinancialSource.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(201, Nothing, True)
            Presenter.InitializeFinancialSource()
        End If
    End Sub

    Private Sub INDsleFinancialSource_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleFinancialSource.QueryPopUp
        If INDsleFinancialSource.Properties.DataSource Is Nothing Then
            INDsleFinancialSource.Properties.DataSource = Presenter.InitializeFinancialSource()
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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
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

#End Region

#Region "Menu Contextual"

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        EditVoucher()
    End Sub

    ''' <summary>
    ''' Handles the ContexMenuActions event of the IndigoGridView1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim action As DXMenuItem = DirectCast(sender, DXMenuItem)
        Select Case (action.Tag)
            Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                EditVoucher()
            Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
            Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Add")
        End Select
    End Sub

    ''' <summary>
    ''' Edita una chequera
    ''' </summary>
    Private Sub EditVoucher()
        INDliAdd.HideControl()
        INDliEdit.HideControl(False)
        checkBook = CType(INDgdvCheckbook.GetFocusedRow, Checkbooks)
        INDtxtInitialNumber.EditValue = checkBook.InitialNumber
        INDtxtEndNumber.EditValue = checkBook.EndNumber
        INDtxtCurrentNumber.EditValue = checkBook.CurrentNumber
        INDgleStatusVoucher.EditValue = checkBook.Status
        INDtxtPrefix.Text = checkBook.Prefix
        INDpopVoucher.ShowPopup()
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView2_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        RemoveUser()
    End Sub

    ''' <summary>
    ''' Handles the ContexMenuActions event of the IndigoGridView2 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        'Dim action As DXMenuItem = DirectCast(sender, DXMenuItem)
        'Select Case (action.Tag)
        '    Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
        '        EditUser()
        '    Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
        '    Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Add")
        'End Select
        RemoveUser()
    End Sub

    ''' <summary>
    ''' Remover Usuario
    ''' </summary>
    Private Sub RemoveUser()
        Dim ObjUser = CType(INDGvUser.GetRow(INDGvUser.FocusedRowHandle), User)
        If ObjUser IsNot Nothing Then
            For Each usr As EntityBankAccountUser In EntityBankAccount.EntityBankAccountUser
                If usr.CodUser.ToLower.Equals(ObjUser.UserCode.ToLower) Then
                    usr.ChangeTracker.State = ObjectState.Deleted
                    EntityBankAccount.ChangeTracker.State = ObjectState.Modified
                    Exit For
                End If
            Next
            ListaUsuarios.Remove(ObjUser)
            INDGcUser.DataSource = Nothing
            INDGcUser.DataSource = ListaUsuarios
        End If
    End Sub

#End Region

#Region "Enum"

    ''' <summary>
    ''' Enumeración de tipo de cuenta
    ''' </summary>
    Public Enum ETypeAccount
        Ahorro = 1
        Corriente = 2
    End Enum

    Public Enum EStatusVoucher
        Activa = 1
        Bloqueada = 2
        Finalizada = 3
    End Enum

#End Region

End Class