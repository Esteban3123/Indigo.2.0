'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Public Class PortfolioService
    Public Function GetHardCollectionByCode(code As String, audit As AuditMessage) As Domain.Entities.HardCollection Implements IPortfolioServiceHardCollection.GetHardCollectionByCode
        Using service As IHardCollectionAdminService = Container.Current.Resolve(Of IHardCollectionAdminService)()
            Return service.GetHardCollectionByCode(code, audit)
        End Using
        'Return _hardCollectionAdminService.GetHardCollectionByCode(code, audit)
    End Function

    Public Function SaveHardCollection(HardCollection As Domain.Entities.HardCollection, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HardCollection) Implements IPortfolioServiceHardCollection.SaveHardCollection
        Using service As IHardCollectionAdminService = Container.Current.Resolve(Of IHardCollectionAdminService)()
            Return service.SaveHardCollection(HardCollection, audit)
        End Using
        'Return _hardCollectionAdminService.SaveHardCollection(HardCollection, audit)
    End Function


    Public Function GetHardCollectionDetailByHardCollectionId(hardCollectionId As Integer) As List(Of Domain.Entities.HardCollectionDetail) Implements IPortfolioServiceHardCollection.GetHardCollectionDetailByHardCollectionId
        Using service As IHardCollectionAdminService = Container.Current.Resolve(Of IHardCollectionAdminService)()
            Return service.GetHardCollectionDetailByHardCollectionId(hardCollectionId)
        End Using
        'Return _hardCollectionAdminService.GetHardCollectionDetailByHardCollectionId(hardCollectionId)
    End Function

    Public Function SetCopyPasteOrImportFileHardCollection(dataImportFile As List(Of Domain.Base.Entities.ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.HardCollectionDetail)) Implements IPortfolioServiceHardCollection.SetCopyPasteOrImportFileHardCollection
        Using service As IHardCollectionAdminService = Container.Current.Resolve(Of IHardCollectionAdminService)()
            Return service.SetCopyPasteOrImportFileHardCollection(dataImportFile, dataCopyPaste)
        End Using
        'Return _hardCollectionAdminService.SetCopyPasteOrImportFileHardCollection(dataImportFile, dataCopyPaste)
    End Function
End Class
