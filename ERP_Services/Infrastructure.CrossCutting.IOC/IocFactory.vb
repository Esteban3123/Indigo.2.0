'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.IOC
' Author           : WalterSierra
' Created          : 11-03-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-25
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
''' <summary>
''' clase para la Factoria del patron IoC
''' utiliza el patron singleton
''' </summary>
Public NotInheritable Class IocFactory

#Region "Singleton"

    ''' <summary>
    ''' Obtiene la Instancia singleton de IoCFactory
    ''' </summary>
    Public Shared ReadOnly Property Instance(Optional ByVal Empresa As String = "") As IocFactory
        Get
            ServerSessionValues.Current.CurrentContainer = Empresa
            If ServerSessionValues.Current.CurrentHISContainer Is Nothing Then
                ServerSessionValues.Current.CurrentHISContainer = String.Empty
            End If
            Return New IocFactory(Empresa)
        End Get
    End Property

#End Region

#Region "Members"

    Private _CurrentContainer As IContainer

    ''' <summary>
    ''' Obtiene el IContainer actual de <see cref="IContainer" />
    ''' <remarks>
    ''' en este momento solo existe un Contendedor IoC
    ''' </remarks>
    ''' </summary>
    Public ReadOnly Property CurrentContainer() As IContainer
        Get
            Return _CurrentContainer
        End Get
    End Property

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Exclusivo del Patron singleton
    ''' </summary>
    Shared Sub New()
    End Sub
    Private Sub New(Empresa As String)
        _CurrentContainer = New IocUnityContainer(Empresa)
    End Sub

#End Region

End Class
