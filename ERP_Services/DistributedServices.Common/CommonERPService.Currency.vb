#Region "Imports"

Imports Application.Common
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

#End Region

Partial Class CommonERPService

    Public Function DeleteCurrency(Currency As Domain.Entities.Currency, session As SessionValues) As ActionMessageResult(Of Domain.Entities.Currency) Implements ICommonERPService.DeleteCurrency
        Using CurrencyAdmin As ICurrencyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICurrencyAdminService)()
            Return CurrencyAdmin.DeleteCurrency(Currency, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetCurrency(code As String, session As SessionValues) As Domain.Entities.Currency Implements ICommonERPService.GetCurrency
        Using CurrencyAdmin As ICurrencyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICurrencyAdminService)()
            Return CurrencyAdmin.GetCurrency(code)
        End Using
    End Function

    Public Function ListAllCurrency(session As SessionValues) As List(Of Domain.Entities.Currency) Implements ICommonERPService.ListAllCurrency
        Using CurrencyAdmin As ICurrencyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICurrencyAdminService)()
            Return CurrencyAdmin.ListAllCurrency()
        End Using
    End Function

    Public Function SaveCurrency(Currency As Domain.Entities.Currency, session As SessionValues, idSequence As Long) As ActionResult(Of Currency) Implements ICommonERPService.SaveCurrency
        Using CurrencyAdmin As ICurrencyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICurrencyAdminService)()
            Return CurrencyAdmin.SaveCurrency(Currency, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCurrency">Id del Moneda</param>
    ''' <returns>El Moneda</returns>
    ''' <remarks></remarks>
    Public Function CurrencyById(ByVal idCurrency As Integer, session As SessionValues) As Domain.Entities.Currency Implements ICommonERPService.GetCurrencyById
        Using CurrencyAdmin As ICurrencyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICurrencyAdminService)()
            Return CurrencyAdmin.GetCurrencyById(idCurrency, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Actualiza el estado del Iva
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateCurrency(code As String, state As Boolean, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.Currency) Implements ICommonERPService.UpdateCurrency
        Using CurrencyAdmin As ICurrencyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICurrencyAdminService)()
            'Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return CurrencyAdmin.UpdateCurrency(code, state, session.AuditMessageWcf)
        End Using
        'Return Me._generalLedgerIVAAdminService.UpdateStateGeneralLedgerIVA(code, state, audit)
    End Function

    ''' <summary>
    ''' consulta la tasa de cambio con respecto a la moneda oficial
    ''' </summary>
    ''' <param name="ToCurrencyId"></param>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetTRMbyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, session As SessionValues, Optional DateTrm As Date? = Nothing, Optional entityName As String = Nothing) As ActionResult(Of TRM) Implements ICommonERPService.GetTRMbyCurrencyId
        Using CurrencyAdmin As ICurrencyAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ICurrencyAdminService)()
            Return CurrencyAdmin.GetTRMbyCurrencyId(ToCurrencyId, FromCurrencyId, session, DateTrm, entityName)
        End Using
    End Function
End Class
