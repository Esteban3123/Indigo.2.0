'***********************************************************************
' Assembly         : Presentation.Reporter.Net8
' Original Author  : OscarSierra
' Created          : 03-08-2011
' Adapted for .NET 8
'***********************************************************************

Imports System.Globalization
Imports System.Runtime.Serialization

Namespace Infrastructure

    ''' <summary>
    ''' Esta clase hace uso del patron singleton y contiene cada una de las 
    ''' propiedades de valores de Session a las cuales pueden acceder en cualquier momento de la aplicacion.
    ''' </summary>
    <DataContract()>
    Public Class SessionValues

#Region "Builders"

        Private Shared _instance As SessionValues
        ''' <summary>
        ''' Esta Propiedad instancia la clase IndigoSingleton por una unica vez.	
        ''' </summary>
        Public Shared ReadOnly Property Instance As SessionValues
            Get
                If _instance Is Nothing Then
                    _instance = New SessionValues()
                End If
                Return _instance
            End Get
        End Property

#End Region

#Region "Globalization"

        Private _culture As CultureInfo
        ''' <summary>
        ''' Obtiene o asigna la cultura configurada para la aplicación
        ''' </summary>
        Public Property Culture As CultureInfo
            Get
                Return Me._culture
            End Get
            Set(value As CultureInfo)
                Me._culture = value
            End Set
        End Property

        Private _City As String
        ''' <summary>
        ''' Esta propiedad contiene el WOIED de la ciudad.
        ''' </summary>
        <DataMember>
        Property City As String
            Get
                Return _City
            End Get
            Set(ByVal value As String)
                _City = value
            End Set
        End Property
#End Region

#Region "Containers"

        Private _securityContainer As String
        ''' <summary>
        ''' Propiedad para almacenar el valor del contenedor de seguridad
        ''' </summary>
        <DataMember()>
        Public Property SecurityContainer As String
            Get
                If Me._securityContainer Is Nothing Then
                    Me._securityContainer = String.Empty
                End If
                Return Me._securityContainer
            End Get
            Set(value As String)
                Me._securityContainer = value
            End Set
        End Property

        Private _transactionalContainer As String
        ''' <summary>
        ''' Propiedad para almacenar el valor del contenedor transaccional
        ''' </summary>
        <DataMember()>
        Public Property TransactionalContainer As String
            Get
                If Me._transactionalContainer Is Nothing Then
                    Me._transactionalContainer = String.Empty
                End If
                Return Me._transactionalContainer
            End Get
            Set(value As String)
                Me._transactionalContainer = value
            End Set
        End Property

#End Region

#Region "Company Properties"

        Private _indigoCompany As String
        ''' <summary>
        ''' Esta propiedad contiene el Codigo de la empresa Indigo a la cual esta conectado el usuario.
        ''' </summary>
        <DataMember>
        Property IndigoCompany As String
            Get
                Return _indigoCompany
            End Get
            Set(ByVal value As String)
                _indigoCompany = value
            End Set
        End Property

        Private _indigoCompanyName As String
        ''' <summary>
        ''' Esta propiedad contiene el nombre de la empresa Indigo a la cual esta conectado el usuario.
        ''' </summary>
        <DataMember>
        Property IndigoCompanyName As String
            Get
                Return _indigoCompanyName
            End Get
            Set(ByVal value As String)
                _indigoCompanyName = value
            End Set
        End Property

        Private _indigoCompanyNit As String
        ''' <summary>
        ''' Esta propiedad contiene el nit de la empresa a la cual esta conectado el usuario.
        ''' </summary>
        <DataMember>
        Property IndigoCompanyNit As String
            Get
                Return _indigoCompanyNit
            End Get
            Set(ByVal value As String)
                _indigoCompanyNit = value
            End Set
        End Property

        Private _indigoCompanyType As Integer
        ''' <summary>
        ''' Esta propiedad Especifica el tipo de compañia (1 - Privada 2 - Publica)
        ''' </summary>
        <DataMember>
        Property IndigoCompanyType As Integer
            Get
                Return _indigoCompanyType
            End Get
            Set(ByVal value As Integer)
                _indigoCompanyType = value
            End Set
        End Property

#End Region

#Region "User Properties"

        Dim _userIndigo As String
        ''' <summary>
        ''' Esta propiedad contiene el usuario indigo que esta conectado.
        ''' </summary>
        <DataMember>
        Property UserIndigo As String
            Get
                Return If(_userIndigo, String.Empty)
            End Get
            Set(ByVal value As String)
                _userIndigo = value
            End Set
        End Property

        Dim _userIndigoName As String
        ''' <summary>
        ''' Esta propiedad contiene el nombre del usuario indigo que esta conectado.
        ''' </summary>
        <DataMember>
        Property UserIndigoName As String
            Get
                Return If(_userIndigoName, String.Empty)
            End Get
            Set(ByVal value As String)
                _userIndigoName = value
            End Set
        End Property

#End Region

#Region "Currency Properties"

        Private _currencyISO4217 As String = "COP"
        ''' <summary>
        ''' Código ISO 4217 de la moneda
        ''' </summary>
        <DataMember>
        Public Property CurrencyISO4217 As String
            Get
                Return _currencyISO4217
            End Get
            Set(value As String)
                _currencyISO4217 = value
            End Set
        End Property

        Private _currencyName As String = "PESOS"
        ''' <summary>
        ''' Nombre de la moneda
        ''' </summary>
        <DataMember>
        Public Property CurrencyName As String
            Get
                Return _currencyName
            End Get
            Set(value As String)
                _currencyName = value
            End Set
        End Property

#End Region

    End Class

End Namespace
