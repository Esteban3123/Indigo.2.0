'***********************************************************************
' Assembly         : DistributedServices.Contract
' Author           : Hector Rodriguez Rubiano
' Created          : 14/03/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Contract
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports DistributedServices.Contract
Imports Domain.Crystal.Entities

Partial Class ContractService
    
    Public Function GetImagingGroupActive() As ActionResult(Of List(Of RISGRIMAGE)) Implements IContractServiceImagingGroup.GetImagingGroupActive
        Using service As IImagingGroupAdminService = Container.Current.Resolve(Of IImagingGroupAdminService)()
            Return service.GetImagingGroupActive()
        End Using
    End Function

    Public Function GetImagingGroupById(id As Integer, audit As AuditMessage) As ActionResult(Of RISGRIMAGE) Implements IContractServiceImagingGroup.GetImagingGroupById
        Using service As IImagingGroupAdminService = Container.Current.Resolve(Of IImagingGroupAdminService)()
            Return service.GetImagingGroupById(id, audit)
        End Using
    End Function
End Class
