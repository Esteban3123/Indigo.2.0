'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class ContractService

    Public Function SaveSettingsContract(SettingsContract As SettingsContract, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of SettingsContract) Implements IContractSettingsContract.SaveSettingsContract
        Using service As ISettingsContractAdminService = Container.Current.Resolve(Of ISettingsContractAdminService)()
            Return service.SaveSettingsContract(SettingsContract, audit, idSequense)
        End Using
    End Function

    Public Function DeleteSettingsContract(SettingsContract As SettingsContract, audit As AuditMessage) As ActionResult Implements IContractSettingsContract.DeleteSettingsContract
        Using service As ISettingsContractAdminService = Container.Current.Resolve(Of ISettingsContractAdminService)()
            Return service.DeleteSettingsContract(SettingsContract, audit)
        End Using
    End Function

    Public Function GetSettingsContractByOperatingUnitId(operatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of SettingsContract) Implements IContractSettingsContract.GetSettingsContractByOperatingUnitId
        Using service As ISettingsContractAdminService = Container.Current.Resolve(Of ISettingsContractAdminService)()
            Return service.GetSettingsContractByOperatingUnitId(operatingUnitId, audit)
        End Using
    End Function

End Class
