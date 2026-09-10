'***********************************************************************
' Assembly         : Application.Accounting.PUCAdminService
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 02-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region
Public Interface ICloseMonthAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <returns></returns>
    Function SaveCloseMonth(ByVal month As ClosedMonth, ByVal audit As AuditMessage, ByVal Status As Nullable(Of Integer), ListIdJournalVouchers As String, ByVal idMonth As Nullable(Of Integer)) As ActionResult(Of ClosedMonth)

    ''' <summary>
    ''' Gets the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function GetCloseMonth(ByVal month As Integer, ByVal year As Integer) As Boolean

    ''' <summary>
    ''' Funcion para validar si el periodo se encuentra abierto
    ''' </summary>
    ''' <param name="month">true si esta abierto.</param>
    ''' <param name="year">false si esta cerrado.</param>
    ''' <returns></returns>
    Function ValidateOpenMonth(ByVal month As Integer, ByVal year As Integer, ByVal status As Boolean) As Boolean

    ''' <summary>
    ''' Metodo para validar si el periodo esta abierto
    ''' </summary>
    ''' <param name="month">el mes.</param>
    ''' <param name="year">el año.</param>
    ''' <returns>true = si esta abierto . false = si esta cerrado</returns>
    Function ValidatePeriodOpen(ByVal month As Integer, ByVal year As Integer) As Boolean

    ''' <summary>
    ''' Gets the month close.
    ''' </summary>
    ''' <param name="mont">The mont.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function GetMonthClose(ByVal mont As Integer, ByVal year As Integer) As ClosedMonth

    ''' <summary>
    ''' Obtiene el registro del periodo abierto
    ''' </summary>
    ''' <returns></returns>
    Function GetOpenPeriod() As List(Of ClosedMonth)

    ''' <summary>
    ''' Gets all montby year.
    ''' </summary>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function GetAllMontbyYear(ByVal year As Integer, ByVal Status As Boolean) As List(Of CloseMonthComplex)

    ''' <summary>
    ''' Funcion para ordener el ultimo mes abierto
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Function GetLastMonthOpen(ByVal status As Boolean) As Integer

    ''' <summary>
    ''' Validates the open period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function ValidateOpenPeriod(ByVal month As Integer, ByVal year As Integer) As Boolean
    ''' <summary>
    ''' metodo para validar que los saldos esten balanceados antes de cerrar el mes
    ''' </summary>
    ''' <param name="period"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateBalanceCloseMonth(period As String, year As Integer) As ActionResult(Of List(Of Tuple(Of String, Integer)))

End Interface
