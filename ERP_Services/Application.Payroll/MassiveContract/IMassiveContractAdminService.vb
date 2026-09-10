

Imports Domain.Base.Entities
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IMassiveContractAdminService
    Inherits IDisposable

    Function ValidateMassiveContract(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveContract_Result)

    Function GetMassiveContract(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveContract_Result)

    Function SaveMassiveContract(pData As List(Of ImportFileRow), pCodeUser As String, pIdUser As Integer) As SP_SaveMassiveContract_DTO

    Function ValidateMassiveContractExtension(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveContractExtension_Result)

    Function GetMassiveContractExtension(pData As List(Of ImportFileRow)) As List(Of SP_GetMassiveContractExtension_Result)

    Sub SaveMassiveContractExtension(pData As List(Of ImportFileRow), psession As SessionValues)

    Function ValidateMassiveDependentRelatives(pData As List(Of ImportFileRow)) As List(Of SP_ValidateMassiveDependentRelatives_Result)

    Function ValidateMassiveExternalEntities(pData As List(Of ImportFileRow), psession As SessionValues) As List(Of SP_ValidateMassiveExternalEntities_Result)

    Function ValidateMassiveNovelties(pData As GenericListNovelty, psession As SessionValues) As List(Of SP_ValidateMassiveNovelties_Result)

    Function ValidateMassiveEmployeeSchedule(pData As GenericListEmployeeSchedule, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, psession As SessionValues) As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    Function GetEmployeeScheduleC(Code As String) As EmployeeScheduleC

    Function DeleteEmployeeScheduleDetail(IdEmployeeSchedule As Integer, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, psession As SessionValues) As ActionResult(Of List(Of SP_ValidateMassiveEmployeeSchedule_Result))

End Interface
