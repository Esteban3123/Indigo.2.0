'***********************************************************************
' Assembly         : ServicesDistribuited.Accounting
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Application.Accounting
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class AccountingService

#Region "Functions"
    ''' <summary>
    ''' Deletes the statement folio.
    ''' </summary>
    ''' <param name="statementfolio"></param>
    ''' <returns></returns>
    Public Function DeleteStatementFolio(statementfolio As Domain.Entities.AttachedDeclarations) As Domain.Base.Entities.ActionResult Implements IAccountingStatementFolio.DeleteStatementFolio
        Using service As IStatementFolioAdminService = Container.Current.Resolve(Of IStatementFolioAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeleteStatementFolio(statementfolio, audit)
        End Using
        'Return Me._statementFolioAdminService.DeleteStatementFolio(statementfolio, audit)
    End Function

    ''' <summary>
    ''' Gets the statement folio by code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetStatementFolioByCode(code As String, Optional tracking As Boolean = True) As Domain.Entities.AttachedDeclarations Implements IAccountingStatementFolio.GetStatementFolioByCode
        Using service As IStatementFolioAdminService = Container.Current.Resolve(Of IStatementFolioAdminService)()
            Return service.GetStatementFolioByCode(code, tracking)
        End Using
        'Return Me._statementFolioAdminService.GetStatementFolioByCode(code, tracking)
    End Function

    ''' <summary>
    ''' Gets the statement folio by identifier.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetStatementFolioById(id As Integer, Optional tracking As Boolean = True) As Domain.Entities.AttachedDeclarations Implements IAccountingStatementFolio.GetStatementFolioById
        Using service As IStatementFolioAdminService = Container.Current.Resolve(Of IStatementFolioAdminService)()
            Return service.GetStatementFolioById(id, tracking)
        End Using
        'Return Me._statementFolioAdminService.GetStatementFolioById(id, tracking)
    End Function

    ''' <summary>
    ''' Saves the statement folio.
    ''' </summary>
    ''' <param name="statementfolio">The statementfolio.</param>
    ''' <returns></returns>
    Public Function SaveStatementFolio(statementfolio As Domain.Entities.AttachedDeclarations) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AttachedDeclarations) Implements IAccountingStatementFolio.SaveStatementFolio
        Using service As IStatementFolioAdminService = Container.Current.Resolve(Of IStatementFolioAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveStatementFolio(statementfolio, audit, idSequense)
        End Using
        'Return Me._statementFolioAdminService.SaveStatementFolio(statementfolio, audit, idSequense)
    End Function

    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function ChangeStateStatementfolio(code As String, state As Boolean) As Domain.Base.Entities.ActionResult(Of Domain.Entities.AttachedDeclarations) Implements IAccountingStatementFolio.ChangeStateStatementfolio
        Using service As IStatementFolioAdminService = Container.Current.Resolve(Of IStatementFolioAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.ChangeStateStatementFolio(code, state, audit)
        End Using
        'Return Me._statementFolioAdminService.ChangeStateStatementFolio(code, state, audit)
    End Function
#End Region

End Class
