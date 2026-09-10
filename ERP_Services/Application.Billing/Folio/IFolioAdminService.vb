Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IFolioAdminService
    Inherits IDisposable
    Function GetFolioDetails(ByVal revenueControlDetailId As Integer) As RequestResponse(Of Folio)

    Function GetAnnullateFolio(ByVal invoiceId As Integer) As RequestResponse(Of AnnullateFolio)

    ''' <summary>
    ''' funcion que se encarga de recalcular los valores del folio 
    ''' </summary>
    ''' <param name="_admissionNumber"></param>
    ''' <param name="_revenueControlDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function RecalculateFolio(ByVal _admissionNumber As String, ByVal _revenueControlDetailId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que se encarga de separar la cuenta madre , en folio paciente y aseguradora
    ''' </summary>
    ''' <param name="_admissionNumber"></param>
    ''' <param name="_revenueControlDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SeparateAccount(ByVal _admissionNumber As String, ByVal _revenueControlDetailId As Integer, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que se encarga de unificar las cuentas de los folios Paciente y aseguradora en el madre
    ''' </summary>
    ''' <param name="_admissionNumber"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function UnifyAccount(ByVal _admissionNumber As String, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que se encarga de validar los folios a liquidar
    ''' </summary>
    ''' <param name="listRevenueControlDetailId"></param>
    ''' <returns></returns>
    Function ValidateLiquidateFolio(ByVal listRevenueControlDetailId As String) As RequestResponse(Of String)

    ''' <summary>
    ''' Funcion que se encarga de cerrar la cuenta del paciente que este 0, para si todo esta facturado cambie el estado del ingreso
    ''' </summary>
    ''' <param name="revenueControlDetailId"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function CloseAccount(ByVal revenueControlDetailId As Integer?, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que obtiene los datos de la prefactura, con posiblidad de convertir a otra moneda
    ''' </summary>
    ''' <param name="revenueControlDetailId">Id del folio</param>
    ''' <param name="CurrencyId">Id de la moneda a convertir</param>
    ''' <param name="dateTRM">fecha del TRM a consultar</param>
    ''' <returns></returns>
    Function GetVReportInvoicePartial(ByVal revenueControlDetailId As Integer, CurrencyId As Integer?, dateTRM As Date?) As RequestResponse(Of InvoicePartialMasterAccount)

    ''' <summary>
    ''' Funcion que se encarga de consultar las devoluciones de IVA a la que una cuenta (folio) puede llegar a tener
    ''' esta funcion devuelve los valores segun la moneda y consulta el TRM segun la fecha, estos parametros son opcionales, ya que
    ''' si no se envia toma la moneda oficial 
    ''' </summary>
    ''' <param name="revenueControlDetailId"></param>
    ''' <param name="CurrencyId"></param>
    ''' <param name="dateTRM"></param>
    ''' <param name="invoicePartial"></param>
    ''' <returns></returns>
    Function GetTaxDevolutionByRevenueControlDetail(ByVal revenueControlDetailId As Integer,
                                                    Optional CurrencyId As Integer? = Nothing,
                                                    Optional dateTRM As Date? = Nothing,
                                                    Optional ByVal invoicePartial As InvoicePartialMasterAccount = Nothing,
                                                    Optional ByVal paymentCurrencyId As Integer? = Nothing) As RequestResponse(Of List(Of TaxDevolution))
End Interface