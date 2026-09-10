Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IReligiousBeliefsAdminService
    Inherits IDisposable

    Function DeleteReligiousBeliefs(ByVal pReligiousBeliefs As ReligiousBeliefs, ByVal pAudit As AuditMessage) As ActionResult

    Function SaveReligiousBeliefs(ByVal pReligiousBeliefs As ReligiousBeliefs, ByVal pAudit As AuditMessage, ByVal Optional pIDSequence As Long = 0) As ActionResult(Of ReligiousBeliefs)

    Function GetReligiousBeliefs(ByVal pCode As String, ByVal pTracking As Boolean, ByVal pAudit As AuditMessage) As ActionResult(Of ReligiousBeliefs)

    Function GetReligiousBeliefsByID(ByVal pID As Integer, ByVal pTracking As Boolean, ByVal pAudit As AuditMessage) As ActionResult(Of ReligiousBeliefs)

    Function ChangeState(ByVal pCode As String, ByVal state As Boolean, ByVal pAudit As AuditMessage) As ActionResult(Of ReligiousBeliefs)

End Interface
