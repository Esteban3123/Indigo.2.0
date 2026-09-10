'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Sergio Fernandez
' Created          : 2014-10-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Base
#End Region
Public Class CloseMonthRepository
    Inherits GenericRepository(Of ClosedMonth)
    Implements ICloseMonthRepository, Inject

    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

#Region "Constructor"
    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub
#End Region

#Region "functions"
    ''' <summary>
    ''' Gets the period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function GetPeriod(month As Integer, year As Integer) As Boolean Implements ICloseMonthRepository.GetPeriod
        Dim query = From e In _context.ClosedMonth
                    Where e.Month = month And e.Year = year
                    Select e

        If query.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Validates the open month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function ValidateOpenMonth(month As Integer, year As Integer, ByVal status As Boolean) As Boolean Implements ICloseMonthRepository.ValidateOpenMonth
        Dim query = From e In _context.ClosedMonth
                    Where e.Month = month And e.Year = year
                    Select e
        If query.Count > 0 Then

            If status = True Then
                If query.FirstOrDefault().Status = False Or query.FirstOrDefault().Status Is Nothing Then
                    Return False
                Else
                    Return True
                End If
            Else
                If query.FirstOrDefault().Status = False Then
                    Return False
                Else
                    Return True
                End If
            End If
        Else
            Return False
        End If
    End Function


    ''' <summary>
    ''' Validates the period open.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function ValidatePeriodOpen(month As Integer, year As Integer) As Boolean Implements ICloseMonthRepository.ValidatePeriodOpen
        Dim query = From e In _context.ClosedMonth
                    Where e.Month = month And e.Year = year
                    Select e
        If query.Count > 0 Then
            If query.FirstOrDefault().Status = True Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' funcion para consultar el mes
    ''' </summary>
    ''' <param name="mont"></param>
    ''' <param name="year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMonthClose(mont As Integer, year As Integer) As ClosedMonth Implements ICloseMonthRepository.GetMonthClose
        Dim query = From e In _context.ClosedMonth.AsNoTracking()
                    Where e.Month = mont And e.Year = year
                    Select e
        If query.Count > 0 Then
            query.FirstOrDefault().OriginalValue = (From d In _context.ClosedMonth.AsNoTracking() Where d.Month = mont And d.Year = year Select d).SingleOrDefault()
            Return query.FirstOrDefault()
        Else
            Return New ClosedMonth With
            {
                .Year = year,
                .Month = mont,
                .Status = False
            }
        End If
    End Function

    ''' <summary>
    ''' Obtiene el registro del periodo abierto
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOpenPeriod() As List(Of ClosedMonth) Implements ICloseMonthRepository.GetOpenPeriod
        'Dim query = From e In _context.ClosedMonth.AsNoTracking()
        '              Where e.Status = True
        '              Select e
        'If query.Count > 0 Then
        '    query.FirstOrDefault().OriginalValue = (From d In _context.ClosedMonth.AsNoTracking() Where d.Status = True Select d).SingleOrDefault()
        '    Return query.FirstOrDefault()
        'Else
        '    Return Nothing
        'End If
        Dim res = (From d As ClosedMonth In Me._context.ClosedMonth Where d.Status = True Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Gets all montby year.
    ''' </summary>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function GetAllMontbyYear(year As Integer, ByVal Status As Boolean) As List(Of CloseMonthComplex) Implements ICloseMonthRepository.GetAllMontbyYear
        Dim query = (From m In _context.ClosedMonth
                     Where m.Year = year
                     Select m).ToList()

        Dim ListMonth As New List(Of CloseMonthComplex)
        Dim closeMonth As CloseMonthComplex

        For idMonth = 1 To 12
            If query.Any(Function(d) d.Month = idMonth AndAlso d.Status = Status) Then
                Continue For
            End If

            closeMonth = New CloseMonthComplex() With {.idMonth = idMonth, .Status = IIf(Status, "Cerrado", "Abierto")}
            Select Case idMonth
                Case 1
                    closeMonth.Month = "Enero"
                Case 2
                    closeMonth.Month = "Febrero"
                Case 3
                    closeMonth.Month = "Marzo"
                Case 4
                    closeMonth.Month = "Abril"
                Case 5
                    closeMonth.Month = "Mayo"
                Case 6
                    closeMonth.Month = "Junio"
                Case 7
                    closeMonth.Month = "Julio"
                Case 8
                    closeMonth.Month = "Agosto"
                Case 9
                    closeMonth.Month = "Septiembre"
                Case 10
                    closeMonth.Month = "Octubre"
                Case 11
                    closeMonth.Month = "Noviembre"
                Case 12
                    closeMonth.Month = "Diciembre"
            End Select
            ListMonth.Add(closeMonth)
        Next

        Return ListMonth
    End Function


    ''' <summary>
    ''' Funcion para ordener todos los meses por estado
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function GetAllMonthByStatus(status As Boolean) As Integer Implements ICloseMonthRepository.GetAllMonthByStatus
        Dim query = (From e In _context.ClosedMonth
                     Where e.Status = status
                     Select e).ToList()

        If query.Count() > 0 Then
            query = query.OrderByDescending(Function(x) x.Year).ThenByDescending(Function(x) x.Month).ToList()
            Return query.FirstOrDefault().Month
        End If

        Return 0
    End Function


    ''' <summary>
    ''' Validates the open period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function ValidateOpenPeriod(month As Integer, year As Integer) As Boolean Implements ICloseMonthRepository.ValidateOpenPeriod

    End Function

#End Region


    Public Function ValidateBalanceCloseMonth(period As String, year As Integer) As Entity.Core.Objects.ObjectResult(Of SP_ValidateBalanceCloseMonth_Result) Implements ICloseMonthRepository.ValidateBalanceCloseMonth
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ValidateBalanceCloseMonth(period, year)
    End Function

End Class
