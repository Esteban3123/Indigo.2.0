Imports Application.Portfolio
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class PortfolioService
#Region "Function"
    ''' <summary>
    ''' Obtiene todos los contratos
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllPortfolioConciliation(audit As AuditMessage) As List(Of PortfolioConciliation) Implements IPortfolioConciliationService.ListAllPortfolioConciliation
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.ListAllPortfolioConciliation()
        End Using
    End Function

    ''' <summary>
    ''' Obtiene contrato por codigo
    ''' </summary>
    ''' <param name="Consecutive"></param>
    ''' <returns></returns>
    Public Function GetConciliationByConsecutive(Consecutive As String) As PortfolioConciliation Implements IPortfolioConciliationService.GetConciliationByConsecutive
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.GetConciliationByConsecutive(Consecutive)
        End Using
    End Function

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="nameContainer"></param>
    ''' <param name="nit"></param>
    ''' <param name="InvoiceNumber"></param>
    ''' <param name="stringSQl"></param>
    ''' <param name="FlagNotConfirmInvoice"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function GetInvoice(ByVal nameContainer As String, nit As String, InvoiceNumber As String, ByVal stringSQl As String, ByVal FlagNotConfirmInvoice As String, session As SessionValues) As SP_invoiceList_Result Implements IPortfolioConciliationService.GetInvoice
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.GetInvoice(nameContainer, nit, InvoiceNumber, session.TransactionalContainer, stringSQl, FlagNotConfirmInvoice, session)
        End Using
    End Function

    ''' <summary>
    '''
    ''' </summary>
    ''' <param name="ListInvoices"></param>
    ''' <param name="Nit"></param>
    ''' <param name="containers"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function ValidateListInvoiceSp(ListInvoices As List(Of String), Nit As String, containers As String, Session As SessionValues) As ActionResult(Of List(Of PortfolioConciliationDetail)) Implements IPortfolioConciliationService.ValidateListInvoiceSp
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.ValidateListInvoiceSp(ListInvoices, Nit, containers, Session)
        End Using
    End Function

    Public Function ListAllInvoice(nameContainer As String, nit As String, InvoiceNumber As String, session As SessionValues, stringSQl As String, TopQuery As String, ByVal FlagNotConfirmInvoice As String) As ActionResult(Of List(Of SP_invoiceList_Result)) Implements IPortfolioConciliationService.ListAllInvoice
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.ListAllInvoice(nameContainer, nit, InvoiceNumber, session.TransactionalContainer, stringSQl, TopQuery, FlagNotConfirmInvoice, session)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una lista de detalles de una recepcion
    ''' </summary>
    ''' <param name="ConciliationId">codigo de la recpcion</param>
    ''' <returns>una lista detalle de oficio</returns>
    Public Function GetListConciliationDetail(ConciliationId As Integer, session As SessionValues) As List(Of PortfolioConciliationDetail) Implements IPortfolioConciliationService.GetListConciliationDetail
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.GetListConciliationDetail(ConciliationId)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [Portfolio].[SP_PortfolioConciliation] realizado para cargar los datos de conciliacion de cartera
    ''' </summary>
    ''' <param name="filters"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetSP_PortfolioConciliation(InvoiceNumber As String, ClosingDate As Date, Session As SessionValues) As SP_PortfolioConciliation_Result Implements IPortfolioConciliationService.GetSP_PortfolioConciliation
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.GetSP_PortfolioConciliation(InvoiceNumber, ClosingDate, Session)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza un registro
    ''' </summary>
    ''' <param name="PortfolioConciliation"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SavePortfolioConciliation(PortfolioConciliation As PortfolioConciliation, idSequense As Int64, audit As AuditMessage) As ActionResult(Of PortfolioConciliation) Implements IPortfolioConciliationService.SavePortfolioConciliation
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.SavePortfolioConciliation(PortfolioConciliation, audit, idSequense)
        End Using
    End Function
#End Region

#Region "import data excel"

    Public Function ValidateExcelData(dtSet As DataSet, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IPortfolioConciliationService.ValidateExcelData
        Using service As IPortfolioConciliationAdminService = Container.Current.Resolve(Of IPortfolioConciliationAdminService)()
            Return service.ValidateExcelData(dtSet, session)
        End Using
    End Function

#End Region
End Class