Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollReligiousBeliefs

    <OperationContract()>
    Function DeleteReligiousBeliefs(ByVal pReligiousBeliefs As ReligiousBeliefs, pSession As SessionValues) As ActionResult

    <OperationContract()>
    Function SaveReligiousBeliefs(ByVal pReligiousBeliefs As ReligiousBeliefs, pSession As SessionValues, ByVal IDSequence As Int64) As ActionResult(Of ReligiousBeliefs)

    <OperationContract()>
    Function GetReligiousBeliefs(ByVal pCode As String, ByVal pTracking As Boolean, pSession As SessionValues) As ActionResult(Of ReligiousBeliefs)

    <OperationContract()>
    Function GetReligiousBeliefsById(ByVal ID As String, ByVal pTracking As Boolean, pSession As SessionValues) As ActionResult(Of ReligiousBeliefs)

    <OperationContract()>
    Function ChangeStateReligiousBeliefs(ByVal pCode As String, ByVal pStatus As Boolean, pSession As SessionValues) As ActionResult(Of ReligiousBeliefs)

End Interface
