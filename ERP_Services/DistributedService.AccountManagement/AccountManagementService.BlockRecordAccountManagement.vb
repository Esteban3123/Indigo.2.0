'***********************************************************************
' Assembly         : DistributedServices.AccountManagement
' Author           : Felix Camilo Salazar Roldan
' Created          : 13-12-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.AccountManagement
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class AccountManagementService
    Implements IAccountManagementBlockRecordAccountManagement

    Public Function DeleteBlockRecordAccountManagement(BlockRecordAccountManagement As Domain.Entities.BlockRecordAccountManagement) As Domain.Base.Entities.ActionResult Implements IAccountManagementBlockRecordAccountManagement.DeleteBlockRecordAccountManagement
        Using service As IBlockRecordAccountManagementAdminService = Container.Current.Resolve(Of IBlockRecordAccountManagementAdminService)()
            Return service.DeleteBlockRecord(BlockRecordAccountManagement)
        End Using
        'Return Me._blockRecordAccountManagementAdminService.DeleteBlockRecord(BlockRecordAccountManagement)
    End Function

    Public Function GetBlockRecordAccountManagementByIdformAndIdRecord(IdForm As String, IdRecord As String) As Domain.Entities.BlockRecordAccountManagement Implements IAccountManagementBlockRecordAccountManagement.GetBlockRecordAccountManagementByIdformAndIdRecord
        Using service As IBlockRecordAccountManagementAdminService = Container.Current.Resolve(Of IBlockRecordAccountManagementAdminService)()
            Return service.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordAccountManagementAdminService.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    Public Function SaveBlockRecordAccountManagement(BlockRecordAccountManagement As Domain.Entities.BlockRecordAccountManagement) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordAccountManagement) Implements IAccountManagementBlockRecordAccountManagement.SaveBlockRecordAccountManagement
        Using service As IBlockRecordAccountManagementAdminService = Container.Current.Resolve(Of IBlockRecordAccountManagementAdminService)()
            Return service.SaveBlockRecord(BlockRecordAccountManagement)
        End Using
        'Return Me._blockRecordAccountManagementAdminService.SaveBlockRecord(BlockRecordAccountManagement)
    End Function
End Class

