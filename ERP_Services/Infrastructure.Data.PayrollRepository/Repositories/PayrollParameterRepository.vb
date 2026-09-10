'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 31-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class PayrollParameterRepository

    Inherits GenericRepository(Of PayrollParameter)
    Implements IPayrollParameterRepository

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
    ''' Obtiene una Parametrización de Nómina
    ''' </summary>
    ''' <param name="groupId">Group ID</param>
    ''' <returns>Parametrización de Nómina</returns>
    ''' <remarks></remarks>
    Public Function GetPayrollParameter(groupId As String) As PayrollParameter Implements IPayrollParameterRepository.GetPayrollParameter
        Dim groupQuery = From e In _context.Group Where e.Id = groupId Select e.PayrollParameterId
        Dim intPayrollParameterId As Integer
        If groupQuery.Count > 0 Then
            intPayrollParameterId = groupQuery.SingleOrDefault()
        Else
            Return New PayrollParameter()
        End If
        Dim payrollParameter = From e In _context.PayrollParameter
                                 Where e.Id = intPayrollParameterId
                                 Select e
        If payrollParameter.Count > 0 Then
            Return payrollParameter.SingleOrDefault
        Else
            Return New PayrollParameter()
        End If
    End Function

    ''' <summary>
    ''' Lista todas las Parametrizaciones de Nómina
    ''' </summary>
    ''' <returns>Parametrizaciones de Nómina</returns>
    ''' <remarks></remarks>
    Public Function ListAllPayrollParameter() As List(Of PayrollParameter) Implements IPayrollParameterRepository.ListAllPayrollParameter
        Dim payrollParameter = From e In _context.PayrollParameter
                         Select e
        Return payrollParameter.ToList()
    End Function

   
End Class
