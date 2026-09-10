'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 17-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Security.Entities

#End Region

Public Interface IEntityAccount
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene el tag del frontal
    ''' </summary>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o establece la cabecera de la secuencia
    ''' </summary>
    ''' <value>
    ''' The sequence.
    ''' </value>
    Property Sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo de la entidad de ahorro
    ''' </summary>
    ''' <value>
    ''' The code entity account.
    ''' </value>
    Property Code As String

    ''' <summary>
    ''' Obtiene o establece el banco
    ''' </summary>
    ''' <value>
    ''' The bank entity account.
    ''' </value>
    Property BankEntityAccount As Integer

    ''' <summary>
    ''' Prefijo
    ''' </summary>
    Property Prefix As String

    ''' <summary>
    ''' Obtiene o establece la ciudad de radicacion
    ''' </summary>
    ''' <value>
    ''' The radication city.
    ''' </value>
    Property City As Integer

    ''' <summary>
    ''' Obtiene o establece el tipo de la cuenta
    ''' </summary>
    ''' <value>
    ''' The type.
    ''' </value>
    Property Type As Integer

    ''' <summary>
    ''' Obtiene o establece la tasa x mil
    ''' </summary>
    ''' <value>
    ''' The rate.
    ''' </value>
    Property Number As String

    ''' <summary>
    ''' Obtiene o establece el prefijo
    ''' </summary>
    ''' <value>
    ''' The prefix.
    ''' </value>
    Property InitialBalance As Decimal

    ''' <summary>
    ''' Obtiene o establece el saldo actual
    ''' </summary>
    ''' <value>
    ''' The current balance.
    ''' </value>
    Property CurrentBalance As Decimal

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value>
    ''' The third party identifier.
    ''' </value>
    Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Obtiene o establece la tasa por mil
    ''' </summary>
    ''' <value>
    ''' The number.
    ''' </value>
    Property Rate As Decimal

    ''' <summary>
    ''' Gets or sets the quota overdraft.
    ''' </summary>
    ''' <value>
    ''' The quota overdraft.
    ''' </value>
    Property Quota As Decimal

    ''' <summary>
    ''' Gets or sets the initialize date.
    ''' </summary>
    ''' <value>
    ''' The initialize date.
    ''' </value>
    Property InitDate As Date

    ''' <summary>
    ''' Gets or sets the account accounting.
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Property AccountAccounting As Integer

    ''' <summary>
    ''' Gets or sets the cost center.
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Property CostCenter As Nullable(Of Integer)

    ''' <summary>
    ''' Obtiene o establece el datasource de usuarios
    ''' </summary>
    ''' <value>
    ''' The data source user.
    ''' </value>
    Property DataSourceUser As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de bancos
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Property BankDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece la fuente de datos para ciudades
    ''' </summary>
    ''' <value>
    ''' The city datasource.
    ''' </value>
    Property CityDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the datasource main account expense.
    ''' </summary>
    ''' <value>
    ''' The datasource main account expense.
    ''' </value>
    Property DatasourceMainAccountExpense As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Property AccountAccountingDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de Terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property ThirdPartyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Property CostCenterDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the datasource main account payment.
    ''' </summary>
    ''' <value>
    ''' The datasource main account payment.
    ''' </value>
    Property DatasourceMainAccountPayment As XPInstantFeedbackSource

    ''' <summary>
    ''' Carga las monedas
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDataSource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the datasource grid user.
    ''' </summary>
    ''' <value>
    ''' The datasource grid user.
    ''' </value>
    Property DatasourceGridUser As List(Of User)

    ''' <summary>
    ''' Gets or sets a value indicating whether [state].
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [state]; otherwise, <c>false</c>.
    ''' </value>
    Property State As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' ultimo periodo revalorizado
    ''' </summary>
    ''' <returns></returns>
    Property PeriodLastRevaluation As Integer

    ''' <summary>
    ''' Ultimo valor de la revalorizacion
    ''' </summary>
    ''' <returns></returns>
    Property BalanceLastRevaluation As Decimal
#End Region

    Property ThirdPartyDatasourceCounterpart As XPInstantFeedbackSource

    Property ThirdPartyDatasourceExpenses As XPInstantFeedbackSource

    Property CostCenterDatasourceCounterpart As XPInstantFeedbackSource

    Property CostCenterDatasourceExpenses As XPInstantFeedbackSource

    Property FMGCounterpartThirdPartyId As Integer?

    Property FMGExpenseThirdPartyId As Integer?

    Property FMGCounterpartCostCenterId As Integer?

    Property FMGExpenseCostCenterId As Integer?

    Property CurrencyId As Integer

End Interface
