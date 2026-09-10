Imports Domain.Base
Imports Domain.Crystal.Entities

Public Interface IHCPLAOTRPROCUPSRepository
    Inherits IRepository(Of HCPLAOTRPROCUPS)

    Function GetHCPLAOTRPROCUPSByID(id As Integer) As HCPLAOTRPROCUPS

End Interface
