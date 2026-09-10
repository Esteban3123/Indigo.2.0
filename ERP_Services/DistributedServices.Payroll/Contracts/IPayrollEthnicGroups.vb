Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollEthnicGroups

    <OperationContract()>
    Function DeleteEthnicGroups(ByVal pEthnicGroups As EthnicGroups, pSession As SessionValues) As ActionResult

    <OperationContract()>
    Function SaveEthnicGroups(ByVal pEthnicGroups As EthnicGroups, pSession As SessionValues, ByVal pIDSequence As Int64) As ActionResult(Of EthnicGroups)

    <OperationContract()>
    Function GetEthnicGroups(ByVal pCode As String, ByVal pTracking As Boolean, pSession As SessionValues) As ActionResult(Of EthnicGroups)

    <OperationContract()>
    Function GetEthnicGroupsById(ByVal ID As String, ByVal pTracking As Boolean, pSession As SessionValues) As ActionResult(Of EthnicGroups)

    <OperationContract()>
    Function ChangeStateEthnicGroups(ByVal pCode As String, ByVal pStatus As Boolean, pSession As SessionValues) As ActionResult(Of EthnicGroups)

End Interface
