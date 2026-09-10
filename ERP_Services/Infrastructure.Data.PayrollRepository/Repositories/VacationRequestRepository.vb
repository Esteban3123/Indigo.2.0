'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Faiber Mora
' Created          : 07-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class VacationRequestRepository
    Inherits GenericRepository(Of VacationRequest)
    Implements IVacationRequestRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Funcion para obtener las solicitudes de vacaciones de un empleado
    ''' </summary>
    ''' <param name="employeeId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVacationRequestByEmployee(employeeId As Integer) As List(Of VacationRequest) Implements IVacationRequestRepository.GetVacationRequestByEmployee
        Dim query = (From e In _context.VacationRequest
            Where e.EmployeeId = employeeId Select e)
        Return query.ToList()
    End Function
End Class
