'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Payroll
Imports Infrastructure.CrossCutting.IOC
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities

Partial Class PayrollService

    Public Function ValidateMassiveContract(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveContract_Result) Implements IPayrollMassiveContract.ValidateMassiveContract
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.ValidateMassiveContract(pData)
        End Using
    End Function

    Public Function GetMassiveContract(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_GetMassiveContract_Result) Implements IPayrollMassiveContract.GetMassiveContract
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.GetMassiveContract(pData)
        End Using
    End Function

    Public Sub SaveMassiveContract(pData As List(Of ImportFileRow), pSession As SessionValues) Implements IPayrollMassiveContract.SaveMassiveContract
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            mc.SaveMassiveContract(pData, pSession.UserIndigo, pSession.UserIndigoId)
        End Using
    End Sub

    Public Sub SaveMassiveContractExtension(pData As List(Of ImportFileRow), pSession As SessionValues) Implements IPayrollMassiveContract.SaveMassiveContractExtension
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            mc.SaveMassiveContractExtension(pData, pSession)
        End Using
    End Sub

    Public Function ValidateMassiveContractExtension(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveContractExtension_Result) Implements IPayrollMassiveContract.ValidateMassiveContractExtension
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.ValidateMassiveContractExtension(pData)
        End Using
    End Function

    Public Function GetMassiveContractExtension(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_GetMassiveContractExtension_Result) Implements IPayrollMassiveContract.GetMassiveContractExtension
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.GetMassiveContractExtension(pData)
        End Using
    End Function

    Public Function ValidateMassiveDependentRelatives(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveDependentRelatives_Result) Implements IPayrollMassiveContract.ValidateMassiveDependentRelatives
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.ValidateMassiveDependentRelatives(pData)
        End Using
    End Function

    Public Function ValidateMassiveExternalEntities(pData As List(Of ImportFileRow), pSession As SessionValues) As List(Of SP_ValidateMassiveExternalEntities_Result) Implements IPayrollMassiveContract.ValidateMassiveExternalEntities
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.ValidateMassiveExternalEntities(pData, pSession)
        End Using
    End Function

    Public Function ValidateMassiveNovelties(pData As GenericListNovelty, pSession As SessionValues) As List(Of SP_ValidateMassiveNovelties_Result) Implements IPayrollMassiveContract.ValidateMassiveNovelties
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.ValidateMassiveNovelties(pData, pSession)
        End Using
    End Function

    Public Function ValidateMassiveEmployeeSchedule(pData As GenericListEmployeeSchedule, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, pSession As SessionValues) As List(Of SP_ValidateMassiveEmployeeSchedule_Result) Implements IPayrollMassiveContract.ValidateMassiveEmployeeSchedule
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.ValidateMassiveEmployeeSchedule(pData, Action, Code, Description, Status, Id, Prefix, pSession)
        End Using
    End Function

    Public Function GetEmployeeScheduleC(Code As String, pSession As SessionValues) As EmployeeScheduleC Implements IPayrollMassiveContract.GetEmployeeScheduleC
        Using mc As IMassiveContractAdminService = IocFactory.Instance(pSession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.GetEmployeeScheduleC(Code)
        End Using
    End Function

    Public Function DeleteEmployeeScheduleDetail(IdEmployeeSchedule As Integer, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String, psession As SessionValues) As ActionResult(Of List(Of SP_ValidateMassiveEmployeeSchedule_Result)) Implements IPayrollMassiveContract.DeleteEmployeeScheduleDetail
        Using mc As IMassiveContractAdminService = IocFactory.Instance(psession.TransactionalContainer).CurrentContainer.Resolve(Of IMassiveContractAdminService)()
            Return mc.DeleteEmployeeScheduleDetail(IdEmployeeSchedule, Action, Code, Description, Status, Id, Prefix, psession)
        End Using
    End Function
End Class
