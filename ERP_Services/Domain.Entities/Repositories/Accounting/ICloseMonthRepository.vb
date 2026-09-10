'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 2014-05-19
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region
Public Interface ICloseMonthRepository
    Inherits IRepository(Of ClosedMonth)

    Function ValidateBalanceCloseMonth(period As String, year As Integer) As Entity.Core.Objects.ObjectResult(Of SP_ValidateBalanceCloseMonth_Result)

    ''' <summary>
    ''' Gets the period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function GetPeriod(ByVal month As Integer, ByVal year As Integer) As Boolean

    ''' <summary>
    ''' Obtiene el registro del periodo abierto
    ''' </summary>
    ''' <returns></returns>
    Function GetOpenPeriod() As List(Of ClosedMonth)

    ''' <summary>
    ''' Validates the open month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function ValidateOpenMonth(ByVal month As Integer, ByVal year As Integer, ByVal status As Boolean) As Boolean

    ''' <summary>
    ''' Validates the period open.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function ValidatePeriodOpen(ByVal month As Integer, ByVal year As Integer) As Boolean

    ''' <summary>
    ''' Gets the month close.
    ''' </summary>
    ''' <param name="mont">The mont.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function GetMonthClose(ByVal mont As Integer, ByVal year As Integer) As ClosedMonth

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
    Function GetAllMonthByStatus(ByVal status As Boolean) As Integer

    ''' <summary>
    ''' Validates the open period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Function ValidateOpenPeriod(ByVal month As Integer, ByVal year As Integer) As Boolean

End Interface