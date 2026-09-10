#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


<ServiceContract()> _
Public Interface IFixedAssetInsuranceService


    <OperationContract()> _
    Function ListAllInsurance(Empresa As String) As List(Of FixedAssetInsurance)


    <OperationContract()>
    Function DeleteInsurance(Insurance As FixedAssetInsurance, audit As AuditMessage) As ActionResult
    'Function DeleteInsurance(Empresa As String, ByVal Insurance As FixedAssetInsurance, ByVal audit As AuditMessage) As Boolean


    <OperationContract()>
    Function SaveInsurance(Insurance As FixedAssetInsurance, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetInsurance)
    ' Function SaveInsurance(Empresa As String, ByVal Insurance As FixedAssetInsurance, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetInsurance)

    <OperationContract()> _
    Function GetInsurance(Empresa As String, ByVal codeInsurance As String) As FixedAssetInsurance

    <OperationContract()>
    Function Change_StateInsurance(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetInsurance)
    'Function Change_StateInsurance(code As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of FixedAssetInsurance)

End Interface
