'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Mariana Gonzalez Calderon
' Created          : 01/12/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class ContractAuditRepository
    Inherits GenericRepository(Of ContractAudit)
    Implements IContractAuditRepository

    ''' <summary>
    ''' Contexto de payroll
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payroll
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class

