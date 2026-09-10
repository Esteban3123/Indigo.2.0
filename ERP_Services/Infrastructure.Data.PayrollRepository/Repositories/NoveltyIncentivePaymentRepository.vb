'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 07-03-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class NoveltyIncentivePaymentRepository


    Inherits GenericRepository(Of NoveltyIncentivePayment)
    Implements INoveltyIncentivePaymentRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    
    Public Function GetNoveltyIncentivePaymentByEmployeeIdStatus(EmployeeId As Integer, Status As Byte) As List(Of NoveltyIncentivePayment) Implements INoveltyIncentivePaymentRepository.GetNoveltyIncentivePaymentByEmployeeIdStatus
        Dim NoveltyIncentivePayment = From e In _context.NoveltyIncentivePayment.Include("Concept")
                          Where e.EmployeeId = EmployeeId And e.Status = Status
                          Select e

        If NoveltyIncentivePayment.Count > 0 Then
            Return NoveltyIncentivePayment.ToList()
        Else
            Return Nothing
        End If
    End Function
End Class
