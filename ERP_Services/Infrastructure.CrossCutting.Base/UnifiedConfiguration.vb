'***********************************************************************
' Assembly         : Infraestructure.CrossCutting.Base
' Author           : Hector Rodriguez Rubiano
' Created          : 2021-01-26
'
' Description      : Encapsula y administra los datos de configuración de la aplicacion en la base de datos
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Runtime.Serialization
Imports Domain.Security.Entities
#End Region

<DataContract()>
Public Class UnifiedConfiguration

#Region "Builders"

    Private Shared _instance As UnifiedConfiguration
    ''' <summary>
    ''' Esta Propiedad instancia la clase IndigoSingleton por una unica vez.	
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Shared ReadOnly Property Instance As UnifiedConfiguration
        Get
            If _instance Is Nothing Then
                _instance = New UnifiedConfiguration()
            End If
            Return _instance
        End Get
    End Property

#End Region

#Region "Propiedades"
    Private _UserConfig As UserConfiguration
    Public Property UserConfig() As UserConfiguration
        Get
            Return _UserConfig
        End Get
        Set(ByVal value As UserConfiguration)
            _UserConfig = value
        End Set
    End Property
    Private _CompanySelected As Company
    Public Property CompanySelected() As Company
        Get
            Return _CompanySelected
        End Get
        Set(ByVal value As Company)
            _CompanySelected = value
        End Set
    End Property
    Private _UsuarioEHR As Object
    Public Property UsuarioEHR() As Object
        Get
            Return _UsuarioEHR
        End Get
        Set(ByVal value As Object)
            _UsuarioEHR = value
        End Set
    End Property
    Private _Profesional As Object
    Public Property Profesional() As Object
        Get
            Return _Profesional
        End Get
        Set(ByVal value As Object)
            _Profesional = value
        End Set
    End Property
    Private _ServiceConfiguration As ServiceConfiguration
    Public Property PServiceConfiguration() As ServiceConfiguration
        Get
            Return _ServiceConfiguration
        End Get
        Set(ByVal value As ServiceConfiguration)
            _ServiceConfiguration = value
        End Set
    End Property
    Private _ListCompanies As List(Of Company)
    Public Property ListCompanies() As List(Of Company)
        Get
            Return _ListCompanies
        End Get
        Set(ByVal value As List(Of Company))
            _ListCompanies = value
        End Set
    End Property

#End Region
End Class
