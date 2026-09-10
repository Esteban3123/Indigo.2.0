'***********************************************************************
' Assembly         : Presentacion.Maintenance.MVP
' Author           : Julian Cardozo
' Created          : 04-08-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Domain.Entities

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface ISupplier
    Inherits IcrudBase

#Region "Propiedades"
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    ReadOnly Property MyTag As Object
    ''' <summary>
    ''' establece la secuencia del formulario
    ''' </summary>
    ''' <returns></returns>
    Property Sequence As PaymentsSecuence

    ''' <summary>
    ''' Esta propiedad contiene el codigo del fabricante
    ''' </summary>
    Property Code As String
    ''' <summary>
    ''' Gets or sets the third party cards.
    ''' </summary>
    ''' <value>
    ''' The third party cards.
    ''' </value>
    Property IdThirdParty As Integer?
    ''' <summary>
    ''' Esta propiedad contiene el nombre del fabricante
    ''' </summary>
    Property NameSupplier As String
    ''' <summary>
    ''' Propiedad que obtiene o establece los dias de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property TimeLimitDays As Integer
    ''' <summary>
    ''' Esta propiedad contiene el codigo CMMS
    ''' </summary>
    Property CodeCMMS As String
    ''' <summary>
    ''' Obtiene o establece si el proveedor es declarante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Declarant As Boolean
    ''' <summary>
    ''' propiedad que contiene el sitio web del fabricante
    ''' </summary>
    Property WebSiteSupplier As String
    ''' <summary>
    ''' Obtiene o establece si el proveedor maneja retencion permanente
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PermanentRetention As Boolean
    ''' <summary>
    ''' Obtiene o establece si el proveedor es autorretenedor a título de renta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SelfWithholding As Boolean
    ''' <summary>
    ''' Obtiene o establece si el proveedor es autorretenedor a título de ICA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SelfWithholdingICA As Boolean
    ''' <summary>
    ''' Obtiene o establece si el proveedor no maneja IVA
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property NotIva As Boolean
    ''' <summary>
    ''' Obtiene o establece si el proveedor es fabricante
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Manufacturer As Boolean
    ''' <summary>
    ''' Obtiene o establece si el proveedor es vendedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Seller As Boolean    
    
    ''' <summary>
    ''' Esta propiedad contiene la ciudad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdCity As Integer?    
    ''' <summary>
    ''' Obtiene o establece el id del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierTypeId As Integer

    ''' <summary>
    ''' propiedad que contiene el estado
    ''' </summary>
    Property Status As Boolean
    ''' <summary>
    ''' Propiedad que tiene la lista de pronto pago
    ''' </summary>
    ''' <returns></returns>
    Property ListPromptPaymentDiscount As List(Of PromptPaymentDiscount)

    ''' <summary>
    ''' propiedad que contiene el Estado cuenta bancaria del Proveedor 
    ''' </summary>
    Property State As Byte


#End Region

#Region "Xpo"

    ''' <summary>
    ''' Obtiene o establece el datasource de terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property ThirdPartyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Obtiene o establece el datasource de bancos
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Property BankDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property CitiesXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Establece el datasource del tipo de proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SupplierTypeXpo As XPCollection

    ''' <summary>
    ''' Propiedad que contiene el listado de las lineas de distribucion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DistributionLineXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el listado de los cargos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property PositionXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' propiedad que obtiene o establece el datasource que lista las monedas
    ''' </summary>
    ''' <returns></returns>
    Property CurrencyXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el Costeo inventario en consignación
    '''0 - Costo promedio
    '''1 - Lista de costos
    ''' </summary>
    ''' <value></value>
    Property ConsignmentInventoryCosting As Integer
#End Region

End Interface