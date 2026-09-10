'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 03-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Presentation.Base
Imports Presentation.Controls

#End Region

Public Interface IBankFile
    Inherits ICrudBase

#Region "Fields"

    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece la secuencia de cabecera
    ''' </summary>
    ''' <value>
    ''' The sequense.
    ''' </value>
    Property Sequence As Domain.Entities.PayrollSequence

#End Region

#Region "Properties"

    ''' <summary>
    ''' Unidad Operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OperatingUnitId As Integer

    ''' <summary>
    ''' Codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' Compañia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CompanyId As Integer

    ''' <summary>
    ''' Fecha Liquidacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LiquidationDate As Date?

    ''' <summary>
    ''' Entidad Bancaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property EntityBankAccountId As Integer

    ''' <summary>
    ''' Concepto de Egreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpenseConceptId As Integer

    ''' <summary>
    ''' Lista proceso de liquidacion (Nomina - Prima)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks
    Property listProcess_ As String

    ''' <summary>
    ''' Establece el datasource de los grupos dependiendo de la empresa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property datasourceGroups As List(Of Group)

    ''' <summary>
    ''' Propiedad que contiene el combo box de el periodo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property PeriodControl As DevExpress.XtraEditors.ComboBoxEdit

    ''' <summary>
    ''' Obtiene el periodo del año para la prima
    ''' </summary>
    ''' <returns></returns>
    Property PeriodIncentivePayment As Integer
#End Region

#Region "XPO"

    ''' <summary>
    ''' Datasource de las compañias
    ''' </summary>
    ''' <returns></returns>
    Property CompanyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de las fechas de liquidacion
    ''' </summary>
    ''' <returns></returns>
    Property LiquidationDateXpo As List(Of Date)

    ''' <summary>
    ''' Datasource de las entidades bancarias
    ''' </summary>
    ''' <returns></returns>
    Property EntityBankAccountXpo As LinqInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los conceptos de egreso
    ''' </summary>
    ''' <returns></returns>
    Property ExpenseConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource de los empleados que se encuentre de acuerdo a los filtros
    ''' </summary>
    ''' <returns></returns>
    Property LiquidationXpo As XPCollection(Of PayrollLiquidationXpo)
    ''' <summary>
    ''' Datasource de empleados generados que ya tengan confrimacion de pago en archivo plano para bancos
    ''' </summary>
    ''' <returns></returns>
    Property LiquidationXpo2 As XPCollection(Of PayrollLiquidationXpo)

    ''' <summary>
    ''' Datasource de listado de primas confirmadas
    ''' </summary>
    ''' <returns></returns>
    Property ListaPrimasLiquidadas As XPCollection(Of IncentivePayment)
    ''' <summary>
    ''' Propiedad para traer desde la vista las liquidaciones de primas confirmadas
    ''' </summary>
    ''' <returns></returns>
    Property BankFileIncentivePaymentXpo As XPCollection(Of PayrollBankFileIncentivePaymentXpo)
    ''' <summary>
    ''' Porpiedad para las primas ya confirmadas
    ''' </summary>
    ''' <returns></returns>
    Property BankFileIncentivePaymentConfirmXpo As XPCollection(Of BankFileIncentivePaymentConfirmXpo)


#End Region

End Interface
