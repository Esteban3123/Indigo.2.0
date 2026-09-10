'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 14-03-2014
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

#End Region

Public Interface ICash
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
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequence.
    ''' </value>
    Property Sequence As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' Obtiene o establece el codigo de la caja
    ''' </summary>
    ''' <value>
    ''' The code cash.
    ''' </value>
    Property CodeCash As String

    ''' <summary>
    ''' Obtiene el prefijo de la caja
    ''' </summary>
    Property Prefix As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la caja
    ''' </summary>
    ''' <value>
    ''' The name cash.
    ''' </value>
    Property NameCash As String

    ''' <summary>
    ''' Obtiene o establece el tipo de la caja
    ''' </summary>
    ''' <value>
    ''' The type.
    ''' </value>
    Property Type As Integer

    ''' <summary>
    ''' Obtiene o establece el saldo inicial
    ''' </summary>
    ''' <value>
    ''' The opening balance.
    ''' </value>
    Property InitialBalance As Decimal

    ''' <summary>
    ''' Gets or sets the third party identifier.
    ''' </summary>
    Property ThirdPartyId As Integer?

    ''' <summary>
    ''' Obtiene o establece la fecha inicial
    ''' </summary>
    ''' <value>
    ''' The starting date.
    ''' </value>
    Property InitialDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el reembolso
    ''' </summary>
    ''' <value>
    ''' The refound.
    ''' </value>
    Property RefoundDate As DateTime?

    ''' <summary>
    ''' Obtiene o establece la cuantia maxima
    ''' </summary>
    ''' <value>
    ''' The maximum amount.
    ''' </value>
    Property AmountMax As Decimal

    ''' <summary>
    ''' Obtiene o establece la cuantia minima
    ''' </summary>
    ''' <value>
    ''' The minimun amount.
    ''' </value>
    Property AmountMin As Decimal

    ''' <summary>
    ''' Obtiene o establece la cuantia disponible
    ''' </summary>
    ''' <value>
    ''' The amount available.
    ''' </value>
    Property CurrentBalance As Decimal

    ''' <summary>
    ''' Obtiene o establece la cuenta contable
    ''' </summary>
    ''' <value>
    ''' The account accounting.
    ''' </value>
    Property IdMainAccount As Integer

    ''' <summary>
    ''' Obtiene o establece el centro de costo
    ''' </summary>
    ''' <value>
    ''' The cost center.
    ''' </value>
    Property IdCostCenter As Nullable(Of Integer)

    '' <summary>
    '' Obtiene o establece el estado del registro
    '' </summary>
    '' <value>
    ''   <c>true</c> if [state]; otherwise, <c>false</c>.
    '' </value>
    Property State As Boolean

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas contables
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Property AccountAccountingDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasorce de centro costo  
    ''' </summary>
    ''' <value>
    ''' The cost center datasource.
    ''' </value>
    Property CostCenterDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the third party datasource.
    ''' </summary>
    Property ThirdPartyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de los usuarios
    ''' </summary>
    ''' <value>
    ''' The user datasource.
    ''' </value>
    Property UserDatasource As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean


    ''' <summary>
    ''' Obtiene o establece la moneda de la caja
    ''' </summary>
    ''' <value>
    ''' The type.
    ''' </value>
    Property CurrencyId As Integer

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

    ''' <summary>
    ''' Obtiene o establece el datasource para el campo moneda
    ''' </summary>
    ''' <value>
    ''' The account accounting datasource.
    ''' </value>
    Property CurrencyXpo As XPInstantFeedbackSource

#End Region

End Interface
