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
Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class BillingService
    Implements IBillingBlockRecordBilling

    Public Function DeleteBlockRecordBilling(BlockRecordBilling As Domain.Entities.BlockRecordBilling) As Domain.Base.Entities.ActionResult Implements IBillingBlockRecordBilling.DeleteBlockRecordBilling
        Using service As IBlockRecordBillingAdminService = Container.Current.Resolve(Of IBlockRecordBillingAdminService)()
            Return service.DeleteBlockRecord(BlockRecordBilling)
        End Using
        'Return Me._blockRecordBillingAdminService.DeleteBlockRecord(BlockRecordBilling)
    End Function

    Public Function GetBlockRecordBillingByIdformAndIdRecord(IdForm As String, IdRecord As String) As Domain.Entities.BlockRecordBilling Implements IBillingBlockRecordBilling.GetBlockRecordBillingByIdformAndIdRecord
        Using service As IBlockRecordBillingAdminService = Container.Current.Resolve(Of IBlockRecordBillingAdminService)()
            Return service.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
        End Using
        'Return Me._blockRecordBillingAdminService.GetBlockRecordByIdformAndIdRecord(IdForm, IdRecord)
    End Function

    Public Function SaveBlockRecordBilling(BlockRecordBilling As Domain.Entities.BlockRecordBilling) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordBilling) Implements IBillingBlockRecordBilling.SaveBlockRecordBilling
        Using service As IBlockRecordBillingAdminService = Container.Current.Resolve(Of IBlockRecordBillingAdminService)()
            Return service.SaveBlockRecord(BlockRecordBilling)
        End Using
        'Return Me._blockRecordBillingAdminService.SaveBlockRecord(BlockRecordBilling)
    End Function
End Class
