'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService

    Public Function GetDirectDistributionSecondary(code As String, audit As AuditMessage) As Domain.Entities.DirectDistributionSecondary Implements IInteropCostServiceDirectDistributionSecondary.GetDirectDistributionSecondary
        Using service As IDirectDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDirectDistributionSecondaryAdminService)()
            Return service.GetDirectDistributionSecondary(code, audit)
        End Using
        'Return _directDistributionSecondaryAdminService.GetDirectDistributionSecondary(code, audit)
    End Function

    Public Function GetDirectDistributionSecondaryById(id As Integer) As Domain.Entities.DirectDistributionSecondary Implements IInteropCostServiceDirectDistributionSecondary.GetDirectDistributionSecondaryById
        Using service As IDirectDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDirectDistributionSecondaryAdminService)()
            Return service.GetDirectDistributionSecondaryById(id)
        End Using
        'Return _directDistributionSecondaryAdminService.GetDirectDistributionSecondaryById(id)
    End Function

    Public Function SaveDirectDistributionSecondary(DirectDistributionSecondary As Domain.Entities.DirectDistributionSecondary, ListUpdateIds As List(Of Integer), idSequence As Int64, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.DirectDistributionSecondary) Implements IInteropCostServiceDirectDistributionSecondary.SaveDirectDistributionSecondary
        Using service As IDirectDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDirectDistributionSecondaryAdminService)()
            Return service.SaveDirectDistributionSecondary(DirectDistributionSecondary, ListUpdateIds, audit, idSequence)
        End Using
        'Return _directDistributionSecondaryAdminService.SaveDirectDistributionSecondary(DirectDistributionSecondary, ListUpdateIds, audit, idSequence)
    End Function

    Public Function GetDataImportLogisticProductionCenterById(ByVal ProductionCenterId As Integer) As ActionResult(Of List(Of LogisticsProductionCenterRecordDetail)) Implements IInteropCostServiceDirectDistributionSecondary.GetDataImportLogisticProductionCenterById
        Using service As IDirectDistributionSecondaryAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IDirectDistributionSecondaryAdminService)()
            'Dim idSequence As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetDataImportLogisticProductionCenterById(ProductionCenterId)
        End Using
        'Return _directDistributionSecondaryAdminService.GetDataImportLogisticProductionCenterById(ProductionCenterId)
    End Function

End Class
