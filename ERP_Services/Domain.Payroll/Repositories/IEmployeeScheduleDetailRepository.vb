Imports Domain.Base
Imports Domain.Payroll.Entities


Public Interface IEmployeeScheduleDetailRepository
    Inherits IRepository(Of EmployeeScheduleDetail)

    Function GetEmployeeScheduleDetail(ByVal Id As Integer) As EmployeeScheduleDetail
End Interface
