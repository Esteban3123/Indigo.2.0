Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IEthnicGroupsRepository
    Inherits IRepository(Of EthnicGroups)

    Function GetEthnicGroups(pCode As String, pTracking As Boolean) As EthnicGroups

    Function GetEthnicGroupsById(ID As Integer, pTracking As Boolean) As EthnicGroups

End Interface
