'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 23-05-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
#End Region

Public Interface ICashReceiptsLiquidation
    Inherits IcrudBase

#Region "Properties"
    ''' <summary>
    ''' obtiene o establce el Código del recibo
    ''' </summary>
    ''' <value>
    ''' The code.
    ''' </value>
    Property Code As String
    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdThirdParty As Integer
    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdMainAccount As Integer?
    ''' <summary>
    ''' Id del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenter As Integer?
    ''' <summary>
    ''' Id de la caja
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCashRegister As Integer?

    ''' <summary>
    ''' Moneda de la caja
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId As Integer
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
    ''' obtiene o establece las cajas
    ''' </summary>
    ''' <value>
    ''' The cash xpo.
    ''' </value>
    Property CashXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    ''' <value>
    ''' The cost center xpo.
    ''' </value>
    Property CostCenterXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the third party xpo.
    ''' </summary>
    ''' <value>
    ''' The third party xpo.
    ''' </value>
    Property ThirdPartyXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece el listado de tipo caja
    ''' </summary>
    ''' <value>
    ''' 
    ''' </value>
    ReadOnly Property TypeCash As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.TreasurySequence

    ''' <summary>
    ''' propiedad que obtiene o establece el tipo de recaudo
    ''' </summary>
    ''' <returns>1- caja, 2- cuenta bancaria</returns>
    Property TypeFundRaising As Byte

    ''' <summary>
    ''' Id cuenta bancaria
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdBankAccount As Integer?

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource de combo de cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Property BankAccountXPO As DevExpress.Data.Linq.LinqInstantFeedbackSource
#End Region

End Interface