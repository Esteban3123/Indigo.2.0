'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollMassiveContract

    <OperationContract()>
    Function ValidateMassiveContract(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveContract_Result)

    <OperationContract()>
    Function GetMassiveContract(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_GetMassiveContract_Result)

    <OperationContract()>
    Sub SaveMassiveContract(pData As List(Of ImportFileRow), pSession As SessionValues)

    <OperationContract()>
    Function ValidateMassiveContractExtension(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveContractExtension_Result)

    <OperationContract()>
    Function GetMassiveContractExtension(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_GetMassiveContractExtension_Result)

    <OperationContract()>
    Sub SaveMassiveContractExtension(pData As List(Of ImportFileRow), pSession As SessionValues)
    
    <OperationContract()>
    Function ValidateMassiveDependentRelatives(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveDependentRelatives_Result)
    
    <OperationContract()>
    Function ValidateMassiveExternalEntities(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveExternalEntities_Result)

    <OperationContract()>
    Function ValidateMassiveNovelties(pData As GenericListNovelty, pSession As SessionValues) As List(Of SP_ValidateMassiveNovelties_Result)

    <OperationContract()>
    Function ValidateMassiveEmployeeSchedule(pData As GenericListEmployeeSchedule, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, pSession As SessionValues) As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    <OperationContract()>
    Function GetEmployeeScheduleC(Code As String, pSession As SessionValues) As EmployeeScheduleC

    <OperationContract()>
    Function DeleteEmployeeScheduleDetail(IdEmployeeSchedule As Integer, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, psession As SessionValues) As ActionResult(Of List(Of SP_ValidateMassiveEmployeeSchedule_Result))

End Interface
