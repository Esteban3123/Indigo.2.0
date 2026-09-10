'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-07-10
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-10
' Description      : Interface del frontal de parametros de glosas
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo

#End Region

''' <summary>
''' Interface que declara las propiedades y metodos que debe implementar el frontal de parametros de glosas
''' </summary>
Public Interface ITimeParameters
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Asigna un valor que indica que se esta realizacion una operacion asincrona
    ''' </summary>
    ''' <value>Valor</value>
    WriteOnly Property AsyncOperation As Boolean

    ''' <summary>
    ''' Obtiene o asigna el objeto que encapsula los parametros de tiempo en el modulo de glosas
    ''' </summary>
    ''' <value>Parametros de tiempo</value>
    ''' <returns>Los parametros de tiempo</returns>
    Property TimeParameters As Domain.Entities.TimeParameters

    ''' <summary>
    ''' Datasource del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceThird As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o Asigna el ID del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdThirdParty As Integer?

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una glosa
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeResponseObjection As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para enviar el oficio de respuesta a una glosa
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeSendResponseObjection As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una glosa extemporanea
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeResponseExObjection As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una reiteración
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeResponseReiteration As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para enviar el oficio de respuesta a una reiteración
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeSendResponseReiteration As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para responder una reiteración extemporanea
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeResponseExReiteration As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para realizar el proceso de conciliación
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeConciliation As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo maximo en días para realizar el proceso de cobro juridico
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property MaxTimeJuridicalDebit As Integer

    ''' <summary>
    ''' Obtiene o asigna el tiempo en días para realizar las notificaciones
    ''' </summary>
    ''' <value>Tiempo en días</value>
    ''' <returns>El tiempo en días</returns>
    Property NotificationPeriodicity As Integer

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el toma el sabado como día laboral
    ''' </summary>
    ''' <value>Valor</value>
    ''' <returns>El valor</returns>
    Property IsDaybusinessSaturday As Boolean

    ''' <summary>
    ''' Obtiene o asigna un valor que indica si el toma el domingo como día laboral
    ''' </summary>
    ''' <value>Valor</value>
    ''' <returns>El valor</returns>
    Property IsDaybusinessSunday As Boolean

    ''' <summary>
    ''' Obtiene o establece si afecta o no
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AffectedService As Boolean?

    ''' <summary>
    ''' Obtiene o establece el id del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GeneralGlossConceptNoteId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GeneralGlossConceptNoteIdXpo As XPInstantFeedbackSource

    ' ''' <summary>
    ' ''' Obtiene o establece el id del comprobante contable
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Property RadicationJournalVoucherTypeId As Integer?

    ' ''' <summary>
    ' ''' Establece el datasource del comprobante contable
    ' ''' </summary>
    ' ''' <value></value>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Property RadicationJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del comprobante contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReceptionObjectionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Establece el datasource del comprobante contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ReceptionObjectionJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del tipo comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConciliationJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConciliationJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DevolutionJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Establece el datasource del tipo de comprobante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DevolutionJournalVoucherTypeIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del comprobante contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TransferLegalJournalVoucherTypeId As Integer?

    ''' <summary>
    ''' Establece el datasource del comprobante contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TransferLegalJournalVoucherTypeIdXpo As XPInstantFeedbackSource


    ''' <summary>
    ''' Obtiene o establece el id del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DetailedGlossConceptNoteId As Integer?

    ''' <summary>
    ''' Establece el datasource del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DetailedGlossConceptNoteIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el id del concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PreviousLifetimesConceptNoteId As Integer?

    ''' <summary>
    ''' Establece el concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PreviousLifetimesConceptNoteIdXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece si determina si la factura sale del radicado inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DevolutionInjustificate As Integer?


    ''' <summary>
    ''' Obtiene o asigna el ID del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCostCenter As Integer?

    ''' <summary>
    ''' Obtiene o Asigna lista de centros de costos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DataSourceCostCenter As DevExpress.Xpo.XPInstantFeedbackSource

#End Region

End Interface
