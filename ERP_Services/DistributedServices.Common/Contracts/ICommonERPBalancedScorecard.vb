Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface ICommonERPBalancedScorecard

    <OperationContract()>
    Function SaveBalancedScorecard(Id As Integer, name As String, data As String, session As SessionValues) As ActionResult

End Interface
