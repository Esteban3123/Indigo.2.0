'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 08-04-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Domain.Payroll.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Domain.Base.Entities
Imports Presentation.Base

''' <summary>
''' Modelo de incapacidades el cual se comunica con los sevicios distribuidos
''' </summary>
''' <remarks></remarks>
Public Class MNovelty
    Inherits ModelBase
    Implements IDisposable


    Sub New(tag As String)
        MyBase.New(tag)
    End Sub

    ''' <summary>
    ''' Elimina una Incapacidad
    ''' </summary>
    ''' <param name="novelty">Incapacidad</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function DeleteNoveltyAsync(novelty As Novelty) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteNoveltyAsync(novelty, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene las Incapacidades por Empleado
    ''' </summary>
    ''' <param name="employeeId">Id del Empleado</param>
    ''' <returns>Incapacidades por Empleado</returns>
    ''' <remarks></remarks>
    Public Async Function GetEmployeeInabilityAsync(employeeId As String) As Task(Of List(Of Novelty))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEmployeeNoveltyAsync(employeeId, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene una Incapacidad
    ''' </summary>
    ''' <param name="code">Código de la Incapacidad</param>
    ''' <returns>Incapacidad</returns>
    ''' <remarks></remarks>
    Public Async Function GetInabilityAsync(code As String) As Task(Of Novelty)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetNoveltyAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Almacena o Actualiza una Incapacidad
    ''' </summary>
    ''' <param name="inability">Incapacidad</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Async Function SaveInabilityAsync(inability As Novelty) As Task(Of ActionMessageResult(Of Novelty))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveNoveltyAsync(inability, Indigo)
    End Function
    ' ''' <summary>
    ' ''' Obtiene los campos nulos de una tabla
    ' ''' </summary>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Public Async Function GetNullFieldsAsync() As Task(Of DataSet)
    '    Return Await IndigoConecta.Instancia.CurrentCloud.IndigoComunes.GetFieldsNULLAsync("Payroll", "Inability")
    'End Function

    ''' <summary>
    ''' Obtiene las incapacidades de un empleado por tipo de novedad
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="typeNovelty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInabilityByTypeNoveltyAsync(employeeId As Integer, typeNovelty As Byte) As Task(Of List(Of Novelty))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetNoveltyByTypeNoveltyAsync(employeeId, typeNovelty, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene las incapacidades de un empleado por el campo Liquidate
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="liquidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetInabilityLiquidateAsync(employeeId As Integer, liquidate As Boolean) As Task(Of List(Of Novelty))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetNoveltyLiquidateAsync(employeeId, liquidate, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene las incapacidades de un empleado y filtra los datos por liquidate
    ''' </summary>
    ''' <param name="employeeId">id del empleado</param>
    ''' <param name="status">estado de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInabilityLiquidate(employeeId As Integer, status As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetInabilityLiquidate(employeeId, status)
    End Function

    ''' <summary>
    ''' Obtiene las novedades de un empleado y filtra por el tipo
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="typeNovelty"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListNoveltyByType(employeeId As Integer, typeNovelty As Byte) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetNoveltyLiquidateByType(employeeId, typeNovelty)
    End Function

    ''' <summary>
    ''' Retorna las incapacidades en un rango de fechas
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <param name="StartDate"></param>
    ''' <param name="EndDate"></param>
    ''' <returns></returns>
    Public Function GetInabilitiesByDate(employeeId As Integer, StartDate As Date, EndDate As Date) As XPCollection(Of PayrollNoveltyXpo)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetInabilitiesByDate(employeeId, StartDate, EndDate)
    End Function

    ''' <summary>
    ''' Obtengo la ultima fecha inicial del ciclo para postular descuento.
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    Public Function GetLastCycleStartDate(employeeId As Integer, initialDate As Date) As DateTime?
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetLastCycleStartDate(employeeId, initialDate)
    End Function

    ''' <summary>
    ''' Busca una novedad por el id
    ''' </summary>
    ''' <param name="id">ID de la novedad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetNoveltyById(id As Integer) As Task(Of Novelty)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetNoveltyByIdAsync(id, Indigo)
    End Function

    ''' <summary>
    ''' obtiene un listado de los detalles que estan dentro de un rango de fechas
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <param name="dateInitial">Fecha Inicial</param>
    ''' <param name="dateEnd">Fecha Final</param>
    ''' <returns>Lista de detalles de un calendario</returns>
    ''' <remarks></remarks>
    Public Async Function GetScheduleDetailByEmployeeBetweenDateFromNovelty(employeeId As Integer, dateInitial As Date, dateEnd As Date) As Task(Of List(Of ScheduleDetail))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetScheduleDetailByEmployeeBetweenDateFromNoveltyAsync(employeeId, dateInitial, dateEnd, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la lista de novedades que estan dentro de un rango de fechas a excepcion de la que se envia como parametro
    ''' </summary>
    ''' <param name="noveltyId">id de la novedad que se va a exonerar</param>
    ''' <param name="initialDate">Fecha de inicio</param>
    ''' <param name="endDate">Fecha Fin</param>
    ''' <returns>Lista de novedades</returns>
    ''' <remarks></remarks>
    Public Async Function GetNoveltyDistinctNoveltyBetweenDate(noveltyId As Integer, employeeId As Integer, initialDate As Date, endDate As Date) As Task(Of List(Of Novelty))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetNoveltyDistinctNoveltyBetweenDateAsync(noveltyId, employeeId, initialDate, endDate, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la novedad que tenga la fecha mas alta del mismo codigo consecutivo
    ''' </summary>
    ''' <param name="consecutive">Consecutivo a buscar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetUltimateNoveltyConsecutiveAsync(consecutive As Integer) As Task(Of Novelty)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetUltimateNoveltyConsecutiveAsync(consecutive, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene un promedio del ibc del empleado en los ultimos meses
    ''' </summary>
    ''' <param name="contractId">Id del contrato</param>
    ''' <param name="numberLastMonth">Meses que se quiere calcular</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetAverageIBCLiquidationLastMonth(contractId As Integer, numberLastMonth As Integer) As Task(Of Decimal)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetAverageIBCLiquidationLastMonthAsync(contractId, numberLastMonth, Indigo)
    End Function

    Public Async Function GetAverageIBCLiquidationRealDateMonth(EmployeeId As Integer, year As Integer, month As Integer) As Task(Of Liquidation)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetLiquidationByEmployeeIdYearMonthAsync(EmployeeId, year, month, Indigo)
    End Function

    Public Async Function IsValidDay(listHoliday As List(Of Domain.Entities.Holiday), saturdayBusinessDay As Boolean, sundayBusinessDay As Boolean, dateValidation As Date) As Task(Of Decimal)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.IsValidDayAsync(listHoliday, saturdayBusinessDay, sundayBusinessDay, dateValidation, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene los festivos que estan dentro de un rango de fecha
    ''' </summary>
    ''' <param name="initialDate">Fecha Inicial</param>
    ''' <param name="endDate">Fecha Final</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ListHolidayBetweenDate(ByVal initialDate As Date, ByVal endDate As Date) As Task(Of List(Of Domain.Entities.Holiday))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListHolidayBetweenDateAsync(initialDate, endDate, Indigo)
    End Function

    ''' <summary>
    ''' Función para Obtener un listado de Novedades por Consecutivo
    ''' </summary>
    ''' <param name="Consecutive">Consecutive</param>
    ''' <param name="session">session</param>
    ''' <returns>List(Of Novelty)</returns>
    ''' <remarks></remarks>
    Public Async Function GetListNoveltyByConsecutive(ByVal Consecutive As Integer) As Task(Of List(Of Novelty))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetListNoveltyByConsecutiveAsync(Consecutive, Indigo)
    End Function

    Public Async Function CalculateTwoFirstDaysAsync(employeeId As Integer) As Task(Of ActionResult(Of Decimal))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CalculateTwoFirstDaysAsync(employeeId, Indigo)
    End Function


    Public Async Function GetVacactionInitialEndDate(EmployeeId As Integer, InitialDate As Date, EndDate As Date) As Task(Of List(Of Vacation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetVacationBetweenDateAsync(EmployeeId, InitialDate, EndDate, Indigo)
    End Function

    Public Async Function Days360(InitialDate As Date, EndDate As Date) As Task(Of Integer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.Days360Async(InitialDate, EndDate, Indigo)
    End Function
    ''' <summary>
    ''' Funcion para calcular el valor del concepto de incapacidades para salarios integrales
    ''' </summary>
    ''' <param name="contract"></param>
    ''' <param name="noveltyDays"></param>
    ''' <returns></returns>
    Public Async Function CalculateNoveltyConcept(contract As Contract, noveltyDays As Integer) As Task(Of ActionResult(Of Decimal))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.CalculateNoveltyConceptAsync(contract, noveltyDays, Indigo)
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
