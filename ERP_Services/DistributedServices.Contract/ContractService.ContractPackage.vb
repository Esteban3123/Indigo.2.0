'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Giovanny Plazas
' Created          : 24/08/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function ChangeStateContractPackage(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage) Implements IContractContractPackage.ChangeStateContractPackage
        Using service As IContractPackageAdminService = Container.Current.Resolve(Of IContractPackageAdminService)()
            Return service.ChangeStateContractPackage(code, state, audit)
        End Using
    End Function

    Public Function DeleteContractPackage(ContractPackage As Domain.Entities.ContractPackage, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractContractPackage.DeleteContractPackage
        Using service As IContractPackageAdminService = Container.Current.Resolve(Of IContractPackageAdminService)()
            Return service.DeleteContractPackage(ContractPackage, audit)
        End Using
    End Function

    Public Function GetContractPackage(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage) Implements IContractContractPackage.GetContractPackage
        Using service As IContractPackageAdminService = Container.Current.Resolve(Of IContractPackageAdminService)()
            Return service.GetContractPackage(code, audit)
        End Using
    End Function

    Public Function GetContractPackageById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage) Implements IContractContractPackage.GetContractPackageById
        Using service As IContractPackageAdminService = Container.Current.Resolve(Of IContractPackageAdminService)()
            Return service.GetContractPackageById(id, audit)
        End Using
    End Function

    Public Function SaveContractPackage(ContractPackage As Domain.Entities.ContractPackage, audit As AuditMessage, Optional idSequense As Int64 = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage) Implements IContractContractPackage.SaveContractPackage
        Using service As IContractPackageAdminService = Container.Current.Resolve(Of IContractPackageAdminService)()
            Return service.SaveContractPackage(ContractPackage, audit, idSequense)
        End Using
    End Function

    Public Function SetCopyPasteOrImportFile(dataCopyPaste As List(Of List(Of String)), Name As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.ContractPackage) Implements IContractContractPackage.SetCopyPasteOrImportFile
        Using service As IContractPackageAdminService = Container.Current.Resolve(Of IContractPackageAdminService)()
            Return service.SetCopyPasteOrImportFile(dataCopyPaste, Name, audit)
        End Using
    End Function
End Class
