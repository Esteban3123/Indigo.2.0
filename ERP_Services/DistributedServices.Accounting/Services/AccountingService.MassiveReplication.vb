#Region "Imports"
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' Replicación masiva
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="BookOriginId"></param>
    ''' <param name="BookDestinationId"></param>
    ''' <param name="CodeInitialJournalVoucherType"></param>
    ''' <param name="CodeEndJournalVoucherType"></param>
    ''' <returns></returns>
    Public Function SP_MassiveReplication(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, CodeInitialJournalVoucherType As String, CodeEndJournalVoucherType As String) As ActionResult(Of SP_MassiveReplication_Result) Implements IAccountingMassiveReplication.SP_MassiveReplication
        Using service As IMassiveReplicationAdminService = Container.Current.Resolve(Of IMassiveReplicationAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SP_MassiveReplication(InitialDate, EndDate, BookOriginId, BookDestinationId, CodeInitialJournalVoucherType, CodeEndJournalVoucherType, audit)
        End Using
        'Return Me._massiveReplicationAdminService.SP_MassiveReplication(InitialDate, EndDate, BookOriginId, BookDestinationId, CodeInitialJournalVoucherType, CodeEndJournalVoucherType, audit)
    End Function


    Public Function SP_MassiveReplication2(InitialDate As Date?, EndDate As Date?, BookOriginId As Integer, BookDestinationId As Integer, session As SessionValues) As ActionResult Implements IAccountingMassiveReplication.SP_MassiveReplication2
        Using service As IMassiveReplicationAdminService = Container.Current.Resolve(Of IMassiveReplicationAdminService)()
            Return service.SP_MassiveReplication2(InitialDate, EndDate, BookOriginId, BookDestinationId, session)
        End Using
        'Return Me._massiveReplicationAdminService.SP_MassiveReplication(InitialDate, EndDate, BookOriginId, BookDestinationId, CodeInitialJournalVoucherType, CodeEndJournalVoucherType, audit)
    End Function


#End Region

End Class
