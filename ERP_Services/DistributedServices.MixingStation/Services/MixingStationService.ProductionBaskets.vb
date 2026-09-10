'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports DistributedServices.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceProductionBaskets

    Public Function SaveProductionBaskets(ProductionBaskets As ProductionBaskets, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of ProductionBaskets) Implements IMixingStationServiceProductionBaskets.SaveProductionBaskets
        Using service As IProductionBasketsAdminService = Container.Current.Resolve(Of IProductionBasketsAdminService)()
            Return service.SaveProductionBaskets(ProductionBaskets, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function DeleteProductionBaskets(ProductionBaskets As ProductionBaskets, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IMixingStationServiceProductionBaskets.DeleteProductionBaskets
        Using service As IProductionBasketsAdminService = Container.Current.Resolve(Of IProductionBasketsAdminService)()
            Return service.DeleteProductionBaskets(ProductionBaskets, audit, TransactionalContainer)
        End Using
    End Function

    Public Function GetProductionBasketse(code As String, audit As AuditMessage) As ActionResult(Of ProductionBaskets) Implements IMixingStationServiceProductionBaskets.GetProductionBasketse
        Using service As IProductionBasketsAdminService = Container.Current.Resolve(Of IProductionBasketsAdminService)()
            Return service.GetProductionBasketse(code, audit)
        End Using
    End Function

    Public Function GetProductionBasketsById(id As Integer) As ActionResult(Of ProductionBaskets) Implements IMixingStationServiceProductionBaskets.GetProductionBasketsById
        Using service As IProductionBasketsAdminService = Container.Current.Resolve(Of IProductionBasketsAdminService)()
            Return service.GetProductionBasketsById(id)
        End Using
    End Function

    Public Function ChangeStateProductionBaskets(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of ProductionBaskets) Implements IMixingStationServiceProductionBaskets.ChangeStateProductionBaskets
        Using service As IProductionBasketsAdminService = Container.Current.Resolve(Of IProductionBasketsAdminService)()
            Return service.ChangeStateProductionBaskets(code, state, audit, operatingUnitId)
        End Using
    End Function

End Class
