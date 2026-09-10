#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
#End Region
<ServiceModel.ServiceContract()>
Public Interface IAccountingCloseMonth

    ''' <summary>
    ''' Saves the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveCloseMonth(ByVal month As ClosedMonth, ByVal Status As Nullable(Of Integer), ListIdJournalVouchers As String, ByVal idMonth As Nullable(Of Integer)) As ActionResult(Of ClosedMonth)

    ''' <summary>
    ''' Gets the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetCloseMonth(ByVal month As Integer, ByVal year As Integer) As Boolean

    ''' <summary>
    ''' Funcion para validar si el periodo se encuentra abierto
    ''' </summary>
    ''' <param name="month">true si esta abierto.</param>
    ''' <param name="year">false si esta cerrado.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateOpenMonth(ByVal month As Integer, ByVal year As Integer, ByVal status As Boolean) As Boolean

    ''' <summary>
    ''' Metodo para validar si el periodo esta abierto
    ''' </summary>
    ''' <param name="month">el mes.</param>
    ''' <param name="year">el año.</param>
    ''' <returns>true = si esta abierto . false = si esta cerrado</returns>
    <OperationContract()>
    Function ValidatePeriodOpen(ByVal month As Integer, ByVal year As Integer) As Boolean

    ''' <summary>
    ''' Gets the month close.
    ''' </summary>
    ''' <param name="mont">The mont.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMonthClose(ByVal month As Integer, ByVal year As Integer) As ClosedMonth

    ''' <summary>
    ''' Obtiene el registro del periodo abierto
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetOpenPeriod() As List(Of ClosedMonth)

    ''' <summary>
    ''' funcion para obtener los meses por año
    ''' </summary>
    ''' <param name="year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAllMontbyYear(year As Integer, ByVal Status As Boolean) As List(Of CloseMonthComplex)

    ''' <summary>
    ''' Funcion para ordener el ultimo mes abierto
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLastMonthOpen(ByVal status As Boolean) As Integer

    ''' <summary>
    ''' Validates the open period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function ValidateOpenPeriod(ByVal month As Integer, ByVal year As Integer) As Boolean

    ''' <summary>
    ''' metodo para recalcular los saldos de contabilidad
    ''' </summary>
    ''' <param name="period"></param>
    ''' <param name="year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ValidateBalanceCloseMonth(period As String, year As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))


End Interface
