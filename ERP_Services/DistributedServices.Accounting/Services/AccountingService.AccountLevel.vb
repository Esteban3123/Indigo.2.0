#Region "Imports"

Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' Deletes the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <returns></returns>
    Public Function DeleteAccountLevel(doc As Domain.Entities.MainAccountLevels) As Domain.Base.Entities.ActionMessageResult(Of Domain.Entities.MainAccountLevels) Implements IAccountingAccountLevel.DeleteAccountLevel
        Using service As IAccountLevelAdminService = Container.Current.Resolve(Of IAccountLevelAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeleteAccountLevel(doc, audit)
        End Using
        'Return Me._accountLevelAdminService.DeleteAccountLevel(doc, audit)
    End Function

    ''' <summary>
    ''' Gets the account Level by code.
    ''' </summary>
    ''' <param name="Code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountLevelByCode(Code As String, Optional tracking As Boolean = True) As Domain.Entities.MainAccountLevels Implements IAccountingAccountLevel.GetAccountLevelByCode
        Using service As IAccountLevelAdminService = Container.Current.Resolve(Of IAccountLevelAdminService)()
            Return service.GetAccountLevelByCode(Code, tracking)
        End Using
        'Return Me._accountLevelAdminService.GetAccountLevelByCode(Code, tracking)
    End Function

    ''' <summary>
    ''' Gets the account Level by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetAccountLevelById(id As Integer, Optional tracking As Boolean = True) As Domain.Entities.MainAccountLevels Implements IAccountingAccountLevel.GetAccountLevelById
        Using service As IAccountLevelAdminService = Container.Current.Resolve(Of IAccountLevelAdminService)()
            Return service.GetAccountLevelById(id, tracking)
        End Using
        'Return Me._accountLevelAdminService.GetAccountLevelById(id, tracking)
    End Function

    ''' <summary>
    ''' Saves the account level.
    ''' </summary>
    ''' <param name="doc">The document.</param>
    ''' <returns></returns>
    Public Function SaveAccountLevel(doc As Domain.Entities.MainAccountLevels) As Boolean Implements IAccountingAccountLevel.SaveAccountLevel
        Using service As IAccountLevelAdminService = Container.Current.Resolve(Of IAccountLevelAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveAccountLevel(doc, audit)
        End Using
        'Return Me._accountLevelAdminService.SaveAccountLevel(doc, audit)
    End Function

    ''' <summary>
    ''' Gets all account level.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountLevel() As List(Of Domain.Entities.MainAccountLevels) Implements IAccountingAccountLevel.GetAllAccountLevel
        Using service As IAccountLevelAdminService = Container.Current.Resolve(Of IAccountLevelAdminService)()
            Return service.GetAllAccountLevel()
        End Using
        'Return Me._accountLevelAdminService.GetAllAccountLevel()
    End Function

#End Region

End Class
