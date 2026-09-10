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
    ''' Obtiene los VieBot
    ''' </summary>
    ''' <param name="Form"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetVieBotByForm(Form As String) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.VieBot)) Implements IAccountingVieBot.GetVieBotByForm
        Using service As IVieBotAdminService = Container.Current.Resolve(Of IVieBotAdminService)()
            Return service.GetVieBotByForm(Form)
        End Using
        'Return Me._vieBotAdminService.GetVieBotByForm(Form)
    End Function

    ''' <summary>
    ''' Guarda los VieBot
    ''' </summary>
    ''' <param name="ListVieBot"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveVieBot(ListVieBot As List(Of Domain.Entities.VieBot)) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.VieBot)) Implements IAccountingVieBot.SaveVieBot
        Using service As IVieBotAdminService = Container.Current.Resolve(Of IVieBotAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveVieBot(ListVieBot, audit)
        End Using
        'Return Me._vieBotAdminService.SaveVieBot(ListVieBot, audit)
    End Function

#End Region

End Class
