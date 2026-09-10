'***********************************************************************
' Assembly         : Domain.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 2014-05-29
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region
Public Interface IAccountingBalanceAdminService
    Inherits IDisposable

#Region "Functions"
    ''' <summary>
    ''' Funcion para obtener el balance por los parametros correspondientes
    ''' </summary>
    ''' <param name="mont">mes.</param>
    ''' <param name="idAccount">id cuenta contable.</param>
    ''' <param name="idThird">id tercero.</param>
    ''' <param name="idCostCenter">id centro de costo.</param>
    ''' <returns></returns>
    Function GetBalanceByMonthAccountThirdCostCenter(ByVal mont As Integer, ByVal year As Integer, ByVal idAccount As Integer, ByVal idThird As Int32?, idCostCenter As Int32?) As GeneralLedgerBalance

    ''' <summary>
    ''' funcion para guardar las cuentas de balance
    ''' </summary>
    ''' <param name="accounting">The accounting balance.</param>
    ''' <returns></returns>
    Function SaveAccountingBalance(ByVal accounting As Domain.Entities.JournalVouchers, audit As AuditMessage, Optional ByVal withCommit As Boolean = True) As Boolean

    ''' <summary>
    ''' metodo para recalcular los saldos de contabilidad
    ''' </summary>
    ''' <param name="periodId"></param>
    ''' <param name="legalBookId"></param>
    ''' <param name="mainAccountId"></param>
    ''' <param name="validateMovement"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RecalculateBalance(periodId As Integer, legalBookId As Integer, mainAccountId As Integer, validateMovement As Boolean, year As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))

#End Region

End Interface
