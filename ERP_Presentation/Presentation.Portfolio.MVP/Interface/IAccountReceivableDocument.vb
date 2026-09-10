'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Juan Carlos Bermudez
' Created          : 17-06-2015
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

#End Region

Public Interface IAccountReceivableDocument
    Inherits IcrudBase

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Property Sequense As Domain.Entities.PortfolioSequence
    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Obtiene o establece el codigo del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Code As String
    ''' <summary>
    ''' Obtiene o establece la fecha del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DocumentDate As Date
    ''' <summary>
    ''' Obtiene o establece el id del cliente del documento de cuenta x cobrar 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerId As Integer
    ''' <summary>
    ''' propiedad que contiene el listado de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CustomerXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountId As Integer
    ''' <summary>
    ''' Propiedad que contiene el listado de cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property MainAccountXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Obtiene o establece el id del centro de costo del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterId As Integer?
    ''' <summary>
    ''' Propiedad que contiene el listado de centros de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CostCenterXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' obtiene o establece el numero de la factura del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property InvoiceNumber As String
    ''' <summary>
    ''' Obtiene o establece el plazo del documento de cuentas x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Term As Integer
    ''' <summary>
    ''' Obtiene o establece la fecha de vencimiento del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ExpiredDate As Date
    ''' <summary>
    ''' Obtiene o establece la cantidad de cuotas del documento de cuenta x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Share As Integer
    ''' <summary>
    ''' obtiene o establece la observación del documento de cuotas x cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Observation As String
    ''' <summary>
    ''' Obtiene o establece el estado de la unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Status As Byte

    ''' <summary>
    ''' DataSource de la moneda 
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyDataSourceXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene el Id de la Moneda seleccionada o la que hay por defecto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CurrencyId(Optional _currencyAbbreviation As String = Nothing) As Integer

    ''' <summary>
    ''' Codigo de la abreviacion del standar ISO4217
    ''' </summary>
    ''' <returns></returns>
    ReadOnly Property CurrencyAbbreviation As String

End Interface
