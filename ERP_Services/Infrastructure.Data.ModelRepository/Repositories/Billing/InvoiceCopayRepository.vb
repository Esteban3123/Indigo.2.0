Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class InvoiceCopayRepository
    Inherits GenericRepository(Of InvoiceCopay)
    Implements IInvoiceCopayRepository, Inject

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene la relacion entre la factura copago y la factura Salud ademas de incluir el tercero de la EAPB
    ''' </summary>
    ''' <param name="BasicBillingInvoiceId"></param>
    ''' <returns></returns>
    Public Function GetInvoiceCopayInfo(BasicBillingInvoiceId As Integer) As InvoiceCopay Implements IInvoiceCopayRepository.GetInvoiceCopayInfo
        Dim Query = (From x In _context.BasicBilling.AsNoTracking()
                     Join ic In _context.InvoiceCopay.AsNoTracking() On x.Id Equals ic.BasicBillingId
                     Join c In _context.Customer.AsNoTracking() On x.CustomerId Equals c.Id
                     Where x.InvoiceId = BasicBillingInvoiceId AndAlso x.ThirdPartyEntityCopayId IsNot Nothing AndAlso c.ThirdPartyId <> x.ThirdPartyEntityCopayId
                     Select New With {.Id = ic.Id, .InvoiceId = ic.InvoiceId, .BasicBillingId = ic.BasicBillingId, .ThirdPartyEAPBId = x.ThirdPartyEntityCopayId})?.FirstOrDefault
        If Query Is Nothing Then
            Return Nothing
        End If
        Return New InvoiceCopay With {.Id = Query.Id, .InvoiceId = Query.InvoiceId, .BasicBillingId = Query.BasicBillingId, .ThirdPartyEAPB = New ThirdParty With {.Id = Query.ThirdPartyEAPBId}}
    End Function

End Class
