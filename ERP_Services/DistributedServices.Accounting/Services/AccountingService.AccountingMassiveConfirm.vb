'***********************************************************************
' Assembly         : DistributedServices.Accounting
' Author           : Carlos Ernesto Cordoba
' Created          : 04-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Application.Accounting
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Class AccountingService

    Public Function ConfirmAccountingDocument(code As String) As ActionResult(Of Tuple(Of String, Integer)) Implements IAccountingMassiveConfirm.ConfirmAccountingDocument
        Using service As IAccountingMassiveConfirmAdminService = Container.Current.Resolve(Of IAccountingMassiveConfirmAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.ConfirmAccountingDocument(code, audit)
        End Using
    End Function

    Public Function ConfirmAccountingDocuments(listDocuments As List(Of String)) As ActionResult(Of List(Of Tuple(Of String, Integer))) Implements IAccountingMassiveConfirm.ConfirmAccountingDocuments
        Using service As IAccountingMassiveConfirmAdminService = Container.Current.Resolve(Of IAccountingMassiveConfirmAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.ConfirmAccountingDocuments(listDocuments, audit)
        End Using
    End Function

End Class