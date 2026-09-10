Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class ElectronicPayrollNotificationRepository
    Inherits GenericRepository(Of ElectronicPayrollNotification)
    Implements IElectronicPayrollNotificationRepository

#Region "Builder"

    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

End Class
