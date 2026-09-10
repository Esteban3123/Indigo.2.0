'***********************************************************************
' Assembly         : Application.Glosas
' Author           : RafaelPatiño
' Created          : 11-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IInterfaceNativeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Funcion para la creacion del documento de reclasificacion de cartera
    ''' </summary>
    ''' <param name="_idSequence">Id secuencia a crear</param>
    ''' <param name="_AccountReceivableId">Obj  de la cartera</param>
    ''' <param name="_BalanceInvoice">valor del documento</param>
    ''' <param name="audit">Objeto info. de auditoria</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CreatePortfolioReclassification(ByVal TypePortfolioReclassification As TypePortfolioReclassification, ByVal _idSequence As Integer, ByVal _AccountReceivableId As AccountReceivable,
                                                     ByVal _BalanceInvoice As Decimal, ByVal _ThirdPartyId As Integer, ByVal GlossParameter As TimeParameters, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion para la creacion del documento de reclasificacion de cartera de una transferencia juridica
    ''' </summary>
    ''' <param name="_idSequence">Id secuencia a crear</param>
    ''' <param name="_AccountReceivableId">Obj  de la cartera</param>
    ''' <param name="listAccount">lista de cuentas contables de la factura con saldo</param>
    ''' <param name="_ThirdPartyId">tercero</param>
    ''' <param name="GlossParameter">parametros de glosa</param>
    ''' <param name="audit">Objeto info. de auditoria</param>
    ''' <returns></returns>
    Function CreatePortfolioReclassificationLegalTransfer(ByVal TypePortfolioReclassification As TypePortfolioReclassification, ByVal _idSequence As Integer, ByVal _AccountReceivableId As AccountReceivable,
                                                     ByVal listAccount As List(Of Tuple(Of Integer, Decimal)), ByVal _ThirdPartyId As Integer, ByVal GlossParameter As TimeParameters, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Crea comprobante contable movimiento cuentas de orden para empresas del sector publico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function createJournalVouchersCompanyPublic(ByVal First As Integer, ByVal itemD As GlosaObjectionsReceptionD, _AccountReceivable As AccountReceivable, ByVal GlossParameter As TimeParameters, valueGlosa As Decimal, IndigoSessionValues As SessionValues) As ActionResult

    ''' <summary>
    ''' Funcion de creacion de Nota Creditos
    ''' </summary>
    Function createCreditNote(ByVal typeAcceptedIPS As ETypeAcceptedIPSModule, ByVal _GlosaPortfolioGlosada As GlosaPortfolioGlosada, ByVal _AccountReceivable As AccountReceivable, ByVal GlossParameter As TimeParameters, ByVal _idSequence As Integer, ByVal _idOperativeUnit As Integer, ByVal Session As SessionValues, Optional ConciliationCId As Integer? = Nothing) As ActionResult

    ''' <summary>
    ''' metodo para crear la reclasificacion
    ''' </summary>
    ''' <param name="radicateInvoiceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GeneratePortfolioReclasification(operatingUnitId As Integer, radicateInvoiceId As Integer, codeUser As String) As ActionResult


End Interface
