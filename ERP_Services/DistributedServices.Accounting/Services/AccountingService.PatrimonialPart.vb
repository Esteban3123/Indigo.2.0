'***********************************************************************
' Assembly         : DistributedServices.Accounting
' Author           : Diego Andrés Roldán lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Class AccountingService

    ''' <summary>
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <returns></returns>
    Public Function DeletePatrimonialPart(patrimonialPart As Shareholding) As ActionResult Implements IAccountingPatrimonialPart.DeletePatrimonialPart
        Using service As IPatrimonialPartAdminService = Container.Current.Resolve(Of IPatrimonialPartAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeletePatrimonialPart(patrimonialPart, audit)
        End Using
        'Return Me._patrimonialPartAdminService.DeletePatrimonialPart(patrimonialPart, audit)
    End Function

    ''' <summary>
    ''' Gets the card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetPatrimonialPart(code As String) As Shareholding Implements IAccountingPatrimonialPart.GetPatrimonialPart
        Using service As IPatrimonialPartAdminService = Container.Current.Resolve(Of IPatrimonialPartAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetPatrimonialPart(code, audit)
        End Using
        'Return Me._patrimonialPartAdminService.GetPatrimonialPart(code, audit)
    End Function

    ''' <summary>
    ''' Saves the patrimonial part.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <returns></returns>
    Public Function SavePatrimonialPart(patrimonialPart As Shareholding) As ActionResult(Of Shareholding) Implements IAccountingPatrimonialPart.SavePatrimonialPart
        Using service As IPatrimonialPartAdminService = Container.Current.Resolve(Of IPatrimonialPartAdminService)()
            Dim idSequence As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SavePatrimonialPart(patrimonialPart, audit, idSequence)
        End Using
        'Return Me._patrimonialPartAdminService.SavePatrimonialPart(patrimonialPart, audit, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStatePatrimonialPart(code As String, state As Boolean) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Shareholding) Implements IAccountingPatrimonialPart.UpdateStatePatrimonialPart
        Using service As IPatrimonialPartAdminService = Container.Current.Resolve(Of IPatrimonialPartAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.UpdateStatePatrimonialPart(code, state, audit)
        End Using
        'Return Me._patrimonialPartAdminService.UpdateStatePatrimonialPart(code, state, audit)
    End Function

End Class
