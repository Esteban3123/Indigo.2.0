'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 07-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Libraries imported"
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls
#End Region

''' <summary>
''' Esta interfaz contiene las propiedades y metodos que va implemenmtar nuestra vista y va a controlar nuestro presenter
''' </summary>
Public Interface ICurrency
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Propiedad que contiene el codigo del pais
    ''' </summary>
    Property Code As String

    ''' <summary>
    ''' Propiedad que contiene la lista de abreviacion con nombre
    ''' </summary>
    Property CurrencyISO4217 As XPInstantFeedbackSource
    ''' <summary>
    ''' Propiedad que contiene la lista de abreviacion con nombre
    ''' </summary>
    Property CurrencyISO4217filter As XPInstantFeedbackSource

    ''' <summary>
    ''' Propiedad que contiene el nombre de la moneda
    ''' </summary>
    Property CurrencyName As String

    ''' <summary>
    ''' Propiedad que contiene la abreviatura de la moneda
    ''' </summary>
    Property Abbreviation As String

    ''' <summary>
    ''' Propiedad que contiene la tasa de cambio automatica
    ''' </summary>
    Property CurrencyExchange As Boolean

    ''' <summary>
    ''' Propiedad que contiene la variación de la tasa de cambio automatica
    ''' </summary>
    Property RateVariation As Boolean

    ''' <summary>
    ''' Propiedad que contiene el tipo de redondeo
    ''' </summary>
    Property RoundingType As String

    ''' <summary>
    ''' Propiedad que contiene el gentilicio del pais
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Propiedad que contiene la fecha de medicion para la tasa representativa del mercado
    ''' </summary>
    Property MeasurementDate As Date

    ''' <summary>
    ''' Propiedad que contiene el valor de la tasa representativa del mercado
    ''' </summary>
    Property Value As Double

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean
    Property Sequence As GeneralLedgerSequence

    ReadOnly Property MyTag As String

    ''' <summary>
    ''' Obtiene o establece el layout para customizacion
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    ReadOnly Property MyLayoutControl As IndigoLayoutControl

    ''' <summary>
    ''' Propiedad que tiene el id de la abreviatura y nombre segun iso 4217
    ''' </summary>
    ''' <returns></returns>
    Property ISO4217Id As Integer?

#End Region
End Interface
