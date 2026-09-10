#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IBranchService

    <OperationContract()> _
    Function ListAllBranch(Empresa As String) As List(Of Branch)

    <OperationContract()> _
    Function DeleteBranch(Empresa As String, ByVal Branch As Branch, ByVal audit As AuditMessage) As Boolean

    <OperationContract()> _
    Function SaveBranch(ByVal Branch As Branch, ByVal session As SessionValues) As ActionResult(Of Domain.Maintenance.Entities.Branch)

    <OperationContract()> _
    Function GetBranch(Empresa As String, ByVal codeBranch As String) As Branch

    '<OperationContract()> _
    'Function ListAllCostCenter(Empresa As String) As List(Of CostCenter)

    <OperationContract()> _
    Function Change_StateBranch(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Domain.Maintenance.Entities.Branch)
End Interface
