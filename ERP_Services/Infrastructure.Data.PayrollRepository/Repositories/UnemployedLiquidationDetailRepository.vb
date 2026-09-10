'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 11-12-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class UnemployedLiquidationDetailRepository

    Inherits GenericRepository(Of UnemployedLiquidationDetail)
    Implements IUnemployedLiquidationDetailRepository


    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

End Class
