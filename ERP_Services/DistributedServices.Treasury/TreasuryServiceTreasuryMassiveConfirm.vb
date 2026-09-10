'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Treasury
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class TreasuryService

    Public Function ConfirmTreasuryDocument(processId As Integer, code As String, session As SessionValues, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Tuple(Of String, Integer)) Implements ITreasuryServiceTreasuryMassiveConfirm.ConfirmTreasuryDocument
        Using service As ITreasuryMassiveConfirmAdminService = Container.Current.Resolve(Of ITreasuryMassiveConfirmAdminService)()
            Return service.ConfirmTreasuryDocument(processId, code, audit, session)
        End Using
    End Function

    Public Function ConfirmTreasuryDocuments(processId As Integer, listDocuments As List(Of String), IndigoSessionValues As SessionValues, audit As AuditMessage) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements ITreasuryServiceTreasuryMassiveConfirm.ConfirmTreasuryDocuments
        Using service As ITreasuryMassiveConfirmAdminService = Container.Current.Resolve(Of ITreasuryMassiveConfirmAdminService)()
            Return service.ConfirmTreasuryDocuments(processId, listDocuments, audit, IndigoSessionValues)
        End Using
    End Function

End Class