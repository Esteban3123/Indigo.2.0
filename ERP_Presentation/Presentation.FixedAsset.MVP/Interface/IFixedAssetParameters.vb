'***********************************************************************
' Assembly         : Presentacion.FixedAsset.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 19-01-2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports Domain.Entities

#End Region

Public Interface ISettingFixedAsset
    Inherits IcrudBase

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' fecha de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ProcessDate As DateTime?
    ''' <summary>
    ''' id de la linea de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdIngressAccountingVoucher As Integer?
    ''' <summary>
    ''' id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdDepreciationAccountingVoucher As Integer?
    ''' <summary>
    ''' numero de la remision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAditionAccountingVoucher As Integer?

    Property IdOutputAccountingVoucher As Integer?
    Property IdIntangibleAmortizationVoucher As Integer?

    Property IdOtherIngressAccountingAccount As Integer?

    Property IdDonationAccountingAccount As Integer?

    Property IdTransferPropertyAccountingAccount As Integer?

    Property IdOtherConceptsAccountingAccount As Integer?

    Property IdRecuperationAccountingAccount As Integer?

    Property LowBidAmount As Double

    Property TopMinorValue As Double

    Property IvaCost As Boolean

    Property FixedAssetChief As Integer?
    ''' <summary>
    ''' Propiedad que contendra el id de la moneda escogida
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId As Integer?
    ''' <summary>
    ''' Obtiene una lista de monedas de una collección
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListCurrency As XPCollection


    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IngressListAccountingVoucher As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DepreciationListAccountingVoucher As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AditionListAccountingVoucher As XPInstantFeedbackSource

    ''' <summary>
    ''' Datasource del comprobante de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TransferJournalVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del comprobante de traslado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdTransferJournalVoucherId As Integer?

    ''' <summary>
    ''' Datasource del comprobante de reclasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReclassificationJournalVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del comprobante de reclasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdReclassificationJournalVoucherId As Integer?

    ''' <summary>
    ''' Datasource del comprobante de devolución de ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DevolutionJournalVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id del comprobante de devolución de ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DevolutionJournalVoucherId As Integer?

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IntangibleAmortizationListAccountingVoucher As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OutputListAccountingVoucher As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OtherIngressAccountingAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DonationIngressAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TransferOfPropertyIngressAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property OtherConceptIngressAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RecuperationIngressAccount As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene y asigna el objeto de tipo xpo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ThirdParty As XPInstantFeedbackSource


    ''' <summary>
    ''' Obteniene el tag del frontal
    ''' </summary>
    ''' <value>
    ''' My tag.
    ''' </value>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante contable de Valorización/Desvalorización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValorizationDevaluationAccountingVoucherXpo As XPInstantFeedbackSource

    ''' <summary>
    '''Obtiene o Establece el id del tipo de comprobante contable de Valorización/Desvalorización
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ValorizationDevaluationAccountingVoucherId As Integer?

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitId As Integer?

    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FilingUnitXpo As List(Of FilingUnit)

    ''' <summary>
    ''' Cuenta contable de servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServiceMainAccountId As Integer?

    ''' <summary>
    ''' Datasource de la cuenta contable de servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ServiceMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' COncepto de pago para el iva flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAFreightAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Datasource del concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAFreightAccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Concepto de pago para el flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FreightAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Datasource concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property FreightAccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Concepto de pago para el flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Datasource concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVAAccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Especifica de donde sacar la retencion del iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVARetention As Integer?

    ''' <summary>
    ''' Concepto de pago para el flete
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVARetentionAccountPayableConceptId As Integer?

    ''' <summary>
    ''' Datasource concepto de pago
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IVARetentionAccountPayableConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Cuenta de venta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesMainAccountId As Integer?

    ''' <summary>
    ''' Datasource cuenta de venta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SalesMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Cuenta de reposicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReplacementMainAccountId As Integer?

#Region "Settings by Legal Book"
    ''' <summary>
    ''' Obtiene o establece el id del libro oficial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookId As Integer?

    ''' <summary>
    ''' Establece el datasource del libro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property LegalBookXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el valor establecido de iva al costo por el libro indicado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IvaCostByLegalBook As Boolean?

    ''' <summary>
    ''' Obtiene o establece el valor establecido de manejo de cuantia mínima por el libro indicado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HandlesMinimumAmountByLegalBook As Boolean?

    ''' <summary>
    ''' Obtiene o establece el valor establecido de Aplica Catalogo de bienes y servicios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AppliesCatalogPropertyandServices As Boolean?
#End Region

    ''' <summary>
    ''' Datasource cuenta reposicion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReplacementMainAccountXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene la lista de los concepto de nota de pagos que se va usar cuando se haga una devolucion del comprobante de entrada
    ''' </summary>
    ''' <value></value>
    Property RefundAccountPayableConceptNoteXpo As XPInstantFeedbackSource

End Interface
