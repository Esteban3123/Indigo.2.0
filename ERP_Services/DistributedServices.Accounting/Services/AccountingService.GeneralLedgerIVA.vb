'***********************************************************************
' Assembly         : DistributedServices.Accounting
' Author           : Diego Andrés Roldán lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' obtiene el iva por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetGeneralLedgerIVAByCode(code As String) As Domain.Entities.GeneralLedgerIVA Implements IAccountingGeneralLedgerIVA.GetGeneralLedgerIVAByCode
        Using service As IGeneralLedgerIVAAdminService = Container.Current.Resolve(Of IGeneralLedgerIVAAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetGeneralLedgerIVAByCode(code.Trim(), audit)
        End Using
        'Return Me._generalLedgerIVAAdminService.GetGeneralLedgerIVAByCode(code.Trim(), audit)
    End Function

    ''' <summary>
    ''' obtiene el iva por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetGeneralLedgerIVAById(id As Integer, Optional auditParam As AuditMessage = Nothing) As Domain.Base.Entities.ActionResult(Of GeneralLedgerIVA) Implements IAccountingGeneralLedgerIVA.GetGeneralLedgerIVAById
        Using service As IGeneralLedgerIVAAdminService = Container.Current.Resolve(Of IGeneralLedgerIVAAdminService)()
            Dim audit As AuditMessage = If(auditParam IsNot Nothing, auditParam, OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE))
            Return service.GetGeneralLedgerIVAById(id, audit)
        End Using
        'Return Me._generalLedgerIVAAdminService.GetGeneralLedgerIVAById(id, audit)
    End Function

    ''' <summary>
    ''' Elimina un IVA
    ''' </summary>
    ''' <param name="doc">Iva a eliminar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function DeleteGeneralLedgerIVA(doc As Domain.Entities.GeneralLedgerIVA) As Domain.Base.Entities.ActionResult Implements IAccountingGeneralLedgerIVA.DeleteGeneralLedgerIVA
        Using service As IGeneralLedgerIVAAdminService = Container.Current.Resolve(Of IGeneralLedgerIVAAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeleteGeneralLedgerIVA(doc, audit)
        End Using
        'Return Me._generalLedgerIVAAdminService.DeleteGeneralLedgerIVA(doc, audit)
    End Function

    ''' <summary>
    ''' Graba un IVA
    ''' </summary>
    ''' <param name="doc">IVA a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveGeneralLedgerIVA(doc As Domain.Entities.GeneralLedgerIVA) As Domain.Base.Entities.ActionResult(Of Domain.Entities.GeneralLedgerIVA) Implements IAccountingGeneralLedgerIVA.SaveGeneralLedgerIVA
        Using service As IGeneralLedgerIVAAdminService = Container.Current.Resolve(Of IGeneralLedgerIVAAdminService)()
            Dim idSequence As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveGeneralLedgerIVA(doc, audit, idSequence)
        End Using
        'Return Me._generalLedgerIVAAdminService.SaveGeneralLedgerIVA(doc, audit, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza el estado del Iva
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateGeneralLedgerIVA(code As String, state As Boolean) As Domain.Base.Entities.ActionResult(Of Domain.Entities.GeneralLedgerIVA) Implements IAccountingGeneralLedgerIVA.UpdateStateGeneralLedgerIva
        Using service As IGeneralLedgerIVAAdminService = Container.Current.Resolve(Of IGeneralLedgerIVAAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.UpdateStateGeneralLedgerIVA(code, state, audit)
        End Using
        'Return Me._generalLedgerIVAAdminService.UpdateStateGeneralLedgerIVA(code, state, audit)
    End Function

#End Region

End Class
