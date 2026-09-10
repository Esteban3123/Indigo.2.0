#Region "Imports"

Imports DevExpress.Xpo
Imports Presentation.Base

#End Region

Public Interface ICustomers
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del Tercero
    ''' </summary>
    Property NitCustomers As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre del Tercero
    ''' </summary>
    Property NameCustomers As String

    ''' <summary>
    ''' Esta propiedad contiene el código EPS del Tercero
    ''' </summary>
    Property EPSCodeCustomers As String

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta por cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountReceivableId As Integer

    ''' <summary>
    ''' obtiene o establece el plazo en dias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Term As Integer

    ''' <summary>
    ''' propiedad que contiene el estado del Tercero
    ''' </summary>
    Property StatusCustomers As Boolean

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Obtiene o establece el datasource de los conceptos de nota de cartera
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Property PortfolioNoteConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de los conceptos de retencion
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Property RetentionConceptXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la clasificación de deterioro para la factura básica
    ''' </summary>
    ''' <returns></returns>
    Property BasicBillingClassificationId As Integer?

    ''' <summary>
    ''' DataSource del campo Clasificación Factura Básica
    ''' </summary>
    ''' <returns></returns>
    Property BasicBillingClassificationXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Id de la clasificación de deterioro para la factura salud
    ''' </summary>
    ''' <returns></returns>
    Property HealthInvoiceClassificationId As Integer?

    ''' <summary>
    ''' DataSource del campo Clasificación Factura Salud
    ''' </summary>
    ''' <returns></returns>
    Property HealthInvoiceClassificationXpo As XPInstantFeedbackSource

#End Region

End Interface
