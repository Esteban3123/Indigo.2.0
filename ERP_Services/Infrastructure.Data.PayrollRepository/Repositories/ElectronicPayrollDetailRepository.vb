Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class ElectronicPayrollDetailRepository
    Inherits GenericRepository(Of ElectronicPayrollDetail)
    Implements IElectronicPayrollDetailRepository

#Region "Builder"

    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

End Class
