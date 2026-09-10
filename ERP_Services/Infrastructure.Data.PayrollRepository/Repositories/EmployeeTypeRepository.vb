'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 26-09-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class EmployeeTypeRepository
    Inherits GenericRepository(Of EmployeeType)
    Implements IEmployeeTypeRepository

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
    ''' Obtiene un tipo de empleado por codigo
    ''' </summary>
    ''' <returns>Tipo de empleado</returns>
    ''' <remarks></remarks>
    Public Function GetEmployeeType(code As String, Optional tracking As Boolean = True) As EmployeeType Implements IEmployeeTypeRepository.GetEmployeeType
        Dim employeeType As IQueryable(Of EmployeeType)
        If tracking = False Then
            employeeType = From e In _context.EmployeeType.AsNoTracking()
            Where e.Code = code
            Select e
        Else
            employeeType = From e In _context.EmployeeType
            Where e.Code = code
            Select e
        End If
        If employeeType.Count > 0 Then
            Return employeeType.SingleOrDefault()
        Else
            Return Nothing
        End If
    End Function

End Class
