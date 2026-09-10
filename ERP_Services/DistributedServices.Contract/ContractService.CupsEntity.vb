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

    Public Function ChangeStateCupsEntity(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity) Implements IContractCupsEntity.ChangeStateCupsEntity
        Using service As ICupsEntityAdminService = Container.Current.Resolve(Of ICupsEntityAdminService)()
            Return service.ChangeStateCupsEntity(code, state, audit)
        End Using
        'Return Me._cupsEntityAdminService.ChangeStateCupsEntity(code, state, audit)
    End Function

    Public Function DeleteCupsEntity(CupsEntity As Domain.Entities.CupsEntity, audit As AuditMessage) As Domain.Base.Entities.ActionResult Implements IContractCupsEntity.DeleteCupsEntity
        Using service As ICupsEntityAdminService = Container.Current.Resolve(Of ICupsEntityAdminService)()
            Return service.DeleteCupsEntity(CupsEntity, audit)
        End Using
        'Return Me._cupsEntityAdminService.DeleteCupsEntity(CupsEntity, audit)
    End Function

    Public Function GetCupsEntity(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity) Implements IContractCupsEntity.GetCupsEntity
        Using service As ICupsEntityAdminService = Container.Current.Resolve(Of ICupsEntityAdminService)()
            Return service.GetCupsEntity(code, audit)
        End Using
        'Return Me._cupsEntityAdminService.GetCupsEntity(code, audit)
    End Function

    Public Function GetCupsEntityById(id As Integer, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity) Implements IContractCupsEntity.GetCupsEntityById
        Using service As ICupsEntityAdminService = Container.Current.Resolve(Of ICupsEntityAdminService)()
            Return service.GetCupsEntityById(id, audit)
        End Using
        'Return Me._cupsEntityAdminService.GetCupsEntityById(id, audit)
    End Function

    Public Function SaveCupsEntity(CupsEntity As Domain.Entities.CupsEntity, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CupsEntity) Implements IContractCupsEntity.SaveCupsEntity
        Using service As ICupsEntityAdminService = Container.Current.Resolve(Of ICupsEntityAdminService)()
            Return service.SaveCupsEntity(CupsEntity, audit)
        End Using
        'Return Me._cupsEntityAdminService.SaveCupsEntity(CupsEntity, audit)
    End Function

    Public Function SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId As Integer) As ActionResult(Of SP_ValidateDescriptionsInCrystal_Result) Implements IContractCupsEntity.SP_ValidateDescriptionsInCrystal
        Using service As ICupsEntityAdminService = Container.Current.Resolve(Of ICupsEntityAdminService)()
            Return service.SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId)
        End Using
    End Function

    Public Function SP_ValidateCUPSInCrystal(CUPSEntityCode As String) As ActionResult(Of SP_ValidateCUPSInCrystal_Result) Implements IContractCupsEntity.SP_ValidateCUPSInCrystal
        Using service As ICupsEntityAdminService = Container.Current.Resolve(Of ICupsEntityAdminService)()
            Return service.SP_ValidateCUPSInCrystal(CUPSEntityCode)
        End Using
    End Function

End Class
