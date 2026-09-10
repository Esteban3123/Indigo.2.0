#Region "Imports"

Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class AccountingService

    ''' <summary>
    ''' Gets the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function GetCloseMonth(month As Integer, year As Integer) As Boolean Implements IAccountingCloseMonth.GetCloseMonth
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.GetCloseMonth(month, year)
        End Using
        'Return _CloseMonthAdminService.GetCloseMonth(month, year)
    End Function

    ''' <summary>
    ''' Saves the close month.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveCloseMonth(ByVal month As ClosedMonth, ByVal Status As Nullable(Of Integer), ListIdJournalVouchers As String, ByVal idMonth As Nullable(Of Integer)) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ClosedMonth) Implements IAccountingCloseMonth.SaveCloseMonth
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveCloseMonth(month, audit, Status, ListIdJournalVouchers, idMonth)
        End Using
        'Return Me._CloseMonthAdminService.SaveCloseMonth(month, audit, Status, ListIdJournalVouchers, idMonth)
    End Function


    ''' <summary>
    ''' Funcion para validar si el periodo se encuentra abierto
    ''' </summary>
    ''' <param name="month">true si esta abierto.</param>
    ''' <param name="year">false si esta cerrado.</param>
    ''' <param name="status"></param>
    ''' <returns></returns>
    Public Function ValidateOpenMonth(month As Integer, year As Integer, status As Boolean) As Boolean Implements IAccountingCloseMonth.ValidateOpenMonth
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.ValidateOpenMonth(month, year, status)
        End Using
        'Return _CloseMonthAdminService.ValidateOpenMonth(month, year, status)
    End Function

    ''' <summary>
    ''' Metodo para validar si el periodo esta abierto
    ''' </summary>
    ''' <param name="month">el mes.</param>
    ''' <param name="year">el año.</param>
    ''' <returns>
    ''' true = si esta abierto . false = si esta cerrado
    ''' </returns>
    Public Function ValidatePeriodOpen(month As Integer, year As Integer) As Boolean Implements IAccountingCloseMonth.ValidatePeriodOpen
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.ValidatePeriodOpen(month, year)
        End Using
        'Return _CloseMonthAdminService.ValidatePeriodOpen(month, year)
    End Function

    ''' <summary>
    ''' Gets the month close.
    ''' </summary>
    ''' <param name="month"></param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function GetMonthClose(month As Integer, year As Integer) As Domain.Entities.ClosedMonth Implements IAccountingCloseMonth.GetMonthClose
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.GetMonthClose(month, year)
        End Using
        'Return _CloseMonthAdminService.GetMonthClose(month, year)
    End Function

    ''' <summary>
    ''' Gets the open period.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOpenPeriod() As List(Of Domain.Entities.ClosedMonth) Implements IAccountingCloseMonth.GetOpenPeriod
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.GetOpenPeriod()
        End Using
        'Return _CloseMonthAdminService.GetOpenPeriod()
    End Function

    ''' <summary>
    ''' funcion para obtener los meses por año
    ''' </summary>
    ''' <param name="year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAllMontbyYear(year As Integer, ByVal Status As Boolean) As List(Of Domain.Entities.CloseMonthComplex) Implements IAccountingCloseMonth.GetAllMontbyYear
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.GetAllMontbyYear(year, Status)
        End Using
        'Return _CloseMonthAdminService.GetAllMontbyYear(year, Status)
    End Function

    ''' <summary>
    ''' Funcion para ordener el ultimo mes abierto
    ''' </summary>
    ''' <param name="status">if set to <c>true</c> [status].</param>
    ''' <returns></returns>
    Public Function GetLastMonthOpen(status As Boolean) As Integer Implements IAccountingCloseMonth.GetLastMonthOpen
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.GetLastMonthOpen(status)
        End Using
        'Return _CloseMonthAdminService.GetLastMonthOpen(status)
    End Function

    ''' <summary>
    ''' Validates the open period.
    ''' </summary>
    ''' <param name="month">The month.</param>
    ''' <param name="year">The year.</param>
    ''' <returns></returns>
    Public Function ValidateOpenPeriod(month As Integer, year As Integer) As Boolean Implements IAccountingCloseMonth.ValidateOpenPeriod
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.ValidateOpenPeriod(month, year)
        End Using
        'Return _CloseMonthAdminService.ValidateOpenPeriod(month, year)
    End Function


    Public Function ValidateBalanceCloseMonth(period As String, year As Integer) As Domain.Base.Entities.ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAccountingCloseMonth.ValidateBalanceCloseMonth
        Using service As ICloseMonthAdminService = Container.Current.Resolve(Of ICloseMonthAdminService)()
            Return service.ValidateBalanceCloseMonth(period, year)
        End Using
        'Return _CloseMonthAdminService.ValidateBalanceCloseMonth(period, year)
    End Function

End Class
