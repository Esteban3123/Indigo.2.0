'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/04/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Presentation.Controls
Imports DevExpress.Data.Linq

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface INotesDebitCreditPortfolio
    Inherits IcrudBase

#Region "Fields"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' obtiene o establce el codigo de la nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String

    ''' <summary>
    ''' fecha de la nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NoteDate As Date?

    ''' <summary>
    ''' tipo de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NoteType As Integer

    ''' <summary>
    ''' naturaleza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Nature As Integer

    ''' <summary>
    ''' id del cliente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerId As Integer?

    ''' <summary>
    ''' Id del anticipo
    ''' </summary>
    ''' <returns></returns>
    Property AdvanceDistributionId As Integer?

    ''' <summary>
    ''' Id del cruce de anticipo vs CxC
    ''' </summary>
    ''' <returns></returns>
    Property PortfolioTransferId As Integer?

    ''' <summary>
    ''' observaciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observations As String

    ''' <summary>
    ''' id de la moneda
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyId As Integer

    ''' <summary>
    ''' Codigo de la abreviacion del standar ISO4217
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyAbbreviation As String

#End Region

#Region "XPO"

    ''' <summary>
    ''' obtiene o establece los clientes
    ''' </summary>
    Property CustomerXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de anticipos a distribuir
    ''' </summary>
    Property AdvanceDistributionXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de cruces de anticipos
    ''' </summary>
    Property PorfolioTransferXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o estable las facturas
    ''' </summary>
    Property BillsXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece los anticipos
    ''' </summary>
    Property AdvanceXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece los conceptos de notas
    ''' </summary>
    Property NoteConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas xpo
    ''' </summary>
    Property AccountsXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establce los terceros
    ''' </summary>
    Property ThirdPartyXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    Property CostCenterConceptXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece los conceptos de retencion
    ''' </summary>
    Property RetentionConceptXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de clientes a distribuir
    ''' </summary>
    ''' <returns></returns>
    Property CustomerDistributionXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas xpo
    ''' </summary>
    Property AccountsDistributionXPO As XPInstantFeedbackSource

    ''' <summary>
    ''' obtiene o establece los centros de costo
    ''' </summary>
    Property CostCenterConceptDistributionXPO As DevExpress.Xpo.XPInstantFeedbackSource

    ''' <summary>
    ''' DataSoruce de la moenda 
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDataSourceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' datasource de la tarifa de iva
    ''' </summary>
    ''' <returns></returns>
    Property TaxRateDataSourceXpo As XPInstantFeedbackSource

#End Region

End Interface
