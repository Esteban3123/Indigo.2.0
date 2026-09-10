
'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class VerifyAutoliquidationRepository

    Inherits GenericRepository(Of VerifyAutoliquidationFile)
    Implements IVerifyAutoliquidationRepository

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
