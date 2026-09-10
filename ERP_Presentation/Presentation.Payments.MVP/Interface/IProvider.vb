'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/03/2014
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

#End Region

''' <summary>
''' esta interfaz contiene las propiedades y metodos que va implementar nuestra vista y va a controlar nuestro presenter
''' </summary>
''' <remarks></remarks>
Public Interface IProvider
    Inherits IcrudBase

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo de los bancos
    ''' </summary>
    Property CodeProvider As String

    ''' <summary>
    ''' Esta propiedad contiene el nombre de los bancos
    ''' </summary>
    Property NameProvider As String

    ''' <summary>
    ''' Id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property IdAccount As Integer

    ''' <summary>
    ''' Propiedad que contiene el listado de ciudades xpo
    ''' </summary>
    Property AccountsXpo As XPInstantFeedbackSource

    ''' <summary>
    ''' Esta propiedad contiene el numero de dias de plazo
    ''' </summary>
    Property DaysLater As Integer

    ''' <summary>
    ''' Esta propiedad contiene el concepto de iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ConceptIVA As Boolean

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    Property Status As Boolean

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    WriteOnly Property ActionsOnControls As Boolean

    ''' <summary>
    ''' Esta propiedad contiene el nombre del contacto
    ''' </summary>
    Property NameContact As String

    ''' <summary>
    ''' Esta propiedad contiene el codigo del contacto
    ''' </summary>
    Property Contact As String

    ''' <summary>
    ''' Esta propiedad contiene la ciudad del contacto
    ''' </summary>
    Property CityContact As String

    ''' <summary>
    ''' Esta propiedad contiene el valor de la hora
    ''' </summary>
    Property TimeValue As Decimal

    ''' <summary>
    ''' Esta propiedad contiene el numero de horas contratadas
    ''' </summary>
    Property ContractedHours As Integer

    ''' <summary>
    ''' Esta propiedad contiene el cargo del contacto
    ''' </summary>
    Property Charge As String

    ''' <summary>
    ''' Esta propiedad contiene el valor mensual
    ''' </summary>
    Property MonthlyValue As Decimal

    ''' <summary>
    ''' Esta propiedad contiene la comision
    ''' </summary>
    Property Commission As Decimal

    ''' <summary>
    ''' Obtiene o establece el datasource de terceros
    ''' </summary>
    ''' <value>
    ''' The third party datasource.
    ''' </value>
    Property ThirdPartyDatasource As XPInstantFeedbackSource

    ''' <summary>
    ''' Gets or sets the third party cards.
    ''' </summary>
    ''' <value>
    ''' The third party cards.
    ''' </value>
    Property ThirdPartyProvider As Integer

#End Region

End Interface
