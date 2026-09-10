'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 07-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ScheduleDetailConceptRepository
    Inherits GenericRepository(Of ScheduleDetailConcept)
    Implements IScheduleDetailConceptRepository

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

End Class
