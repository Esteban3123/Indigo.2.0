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
    Implements IMixingStationServiceTransportation

    Public Function SaveTransportation(Transportation As Transportation, audit As AuditMessage, operatingUnitId As Integer, Optional idSequense As Long = 0) As ActionResult(Of Transportation) Implements IMixingStationServiceTransportation.SaveTransportation
        Using service As ITransportationAdminService = Container.Current.Resolve(Of ITransportationAdminService)()
            Return service.SaveTransportation(Transportation, audit, operatingUnitId, idSequense)
        End Using
    End Function

    Public Function DeleteTransportation(Transportation As Transportation, audit As AuditMessage, TransactionalContainer As String) As ActionResult Implements IMixingStationServiceTransportation.DeleteTransportation
        Using service As ITransportationAdminService = Container.Current.Resolve(Of ITransportationAdminService)()
            Return service.DeleteTransportation(Transportation, audit, TransactionalContainer)
        End Using
    End Function

    Public Function GetTransportation(code As String, audit As AuditMessage) As ActionResult(Of Transportation) Implements IMixingStationServiceTransportation.GetTransportation
        Using service As ITransportationAdminService = Container.Current.Resolve(Of ITransportationAdminService)()
            Return service.GetTransportation(code, audit)
        End Using
    End Function

    Public Function GetTransportationById(id As Integer) As ActionResult(Of Transportation) Implements IMixingStationServiceTransportation.GetTransportationById
        Using service As ITransportationAdminService = Container.Current.Resolve(Of ITransportationAdminService)()
            Return service.GetTransportationById(id)
        End Using
    End Function

    Public Function ChangeStateTransportation(code As String, state As Boolean, audit As AuditMessage, operatingUnitId As Integer) As ActionResult(Of Transportation) Implements IMixingStationServiceTransportation.ChangeStateTransportation
        Using service As ITransportationAdminService = Container.Current.Resolve(Of ITransportationAdminService)()
            Return service.ChangeStateTransportation(code, state, audit, operatingUnitId)
        End Using
    End Function

End Class
