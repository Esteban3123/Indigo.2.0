Imports Domain.Base

Public Interface IMaintenancePlanProgramatedRepository
    Inherits IRepository(Of MaintenancePlanProgramated)

    Function GetMaintenancePlanProgramatedById(id As Integer) As MaintenancePlanProgramated

End Interface
