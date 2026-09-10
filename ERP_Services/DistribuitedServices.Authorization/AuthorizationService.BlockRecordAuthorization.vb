'***********************************************************************
' Assembly         : DistributedServices.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Authorization
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class AuthorizationService
    Implements IAuthorizationBlockRecordAuthorization

    Public Function DeleteBlockRecordBilling(BlockRecordAuthorization As Domain.Entities.BlockRecordAuthorization) As Domain.Base.Entities.ActionResult Implements IAuthorizationBlockRecordAuthorization.DeleteBlockRecordAuthorization
        Using service As IBlockRecordAuthorizationAdminService = Container.Current.Resolve(Of IBlockRecordAuthorizationAdminService)()
            Return service.DeleteBlockRecord(BlockRecordAuthorization)
        End Using
    End Function

    Public Function GetBlockRecordBillingByIdformAndIdRecord(IdForm As String, IdRecord As String) As Domain.Entities.BlockRecordAuthorization Implements IAuthorizationBlockRecordAuthorization.GetBlockRecordAuthorizationByIdformAndIdRecord
        Using service As IBlockRecordAuthorizationAdminService = Container.Current.Resolve(Of IBlockRecordAuthorizationAdminService)()
            Return service.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
        End Using
    End Function

    Public Function SaveBlockRecordBilling(BlockRecordAuthorization As Domain.Entities.BlockRecordAuthorization) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordAuthorization) Implements IAuthorizationBlockRecordAuthorization.SaveBlockRecordAuthorization
        Using service As IBlockRecordAuthorizationAdminService = Container.Current.Resolve(Of IBlockRecordAuthorizationAdminService)()
            Return service.SaveBlockRecord(BlockRecordAuthorization)
        End Using
    End Function

End Class
