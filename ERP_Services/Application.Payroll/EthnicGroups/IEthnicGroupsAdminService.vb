Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IEthnicGroupsAdminService
    Inherits IDisposable

    Function DeleteEthnicGroups(ByVal pEthnicGroups As EthnicGroups, ByVal pAudit As AuditMessage) As ActionResult

    Function SaveEthnicGroups(ByVal pEthnicGroups As EthnicGroups, ByVal pAudit As AuditMessage, ByVal Optional pIDSequence As Long = 0) As ActionResult(Of EthnicGroups)

    Function GetEthnicGroups(ByVal pCode As String, ByVal pTracking As Boolean, ByVal pAudit As AuditMessage) As ActionResult(Of EthnicGroups)

    Function GetEthnicGroupsById(ByVal pID As Integer, ByVal pTracking As Boolean, ByVal pAudit As AuditMessage) As ActionResult(Of EthnicGroups)

    Function ChangeState(ByVal pCode As String, ByVal pStatus As Boolean, ByVal pAudit As AuditMessage) As ActionResult(Of EthnicGroups)

End Interface
