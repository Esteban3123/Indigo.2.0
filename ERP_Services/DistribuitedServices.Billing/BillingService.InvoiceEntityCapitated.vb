#Region "Imports"

Imports System.ServiceModel
Imports Application.Billing
Imports Domain.Billing.POCO
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity


#End Region

Partial Public Class BillingService
    Implements IBillingServiceInvoiceEntityCapitated

    ''' <summary>
    ''' Obtiene una factura por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetInvoiceEntityCapitatedById(Id As Integer) As Domain.Entities.InvoiceEntityCapitated Implements IBillingServiceInvoiceEntityCapitated.GetInvoiceEntityCapitatedById
        Using service As IInvoiceEntityCapitatedAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedAdminService)()
            Return service.GetInvoiceEntityCapitatedById(Id)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene una factura de entidad capitada por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetInvoiceEntityCapitated(code As String, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceEntityCapitated) Implements IBillingServiceInvoiceEntityCapitated.GetInvoiceEntityCapitated
        Using service As IInvoiceEntityCapitatedAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedAdminService)()
            Return service.GetInvoiceEntityCapitated(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una factura a entidad capitada
    ''' </summary>
    ''' <param name="invoice"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Async Function SaveInvoiceEntityCapitated(invoice As Domain.Entities.InvoiceEntityCapitated, session As SessionValues) As Task(Of Domain.Base.Entities.ActionResult(Of Domain.Entities.InvoiceEntityCapitated)) Implements IBillingServiceInvoiceEntityCapitated.SaveInvoiceEntityCapitated
        Using service As IInvoiceEntityCapitatedAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedAdminService)()
            Return Await service.SaveInvoiceEntityCapitatedAsync(invoice, session)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene los valores totales por concepto de recaudo para un periodo de tiempo y grupo de atención establecido.
    ''' </summary>
    ''' <param name="initialDate">Fecha inicial</param>
    ''' <param name="finalDate">Fecha final</param>
    ''' <param name="careGroupId">Grupo de atención</param>
    ''' <returns></returns>
    Public Async Function GetCollectionValuesAsync(initialDate As DateTime, finalDate As DateTime, careGroupId As Integer, invoiceCategoryId As Integer) As Task(Of CollectionValues) Implements IBillingServiceInvoiceEntityCapitated.GetCollectionValuesAsync
        Using service As IInvoiceEntityCapitatedAdminService = Container.Current.Resolve(Of IInvoiceEntityCapitatedAdminService)()
            Return Await service.GetCollectionValuesAsync(initialDate, finalDate, careGroupId, invoiceCategoryId)
        End Using
    End Function
End Class
