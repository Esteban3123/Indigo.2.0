'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Caching
' Author           : WalterSierra
' Created          : 10-05-2011
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-02-27
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


''' <summary>
''' clase para la Factoria de la cache local
''' utiliza el patron singleton
''' </summary>
Public Class LocalCacheFactory

#Region "Singleton"

    Shared ReadOnly m_instance As New LocalCacheFactory()

    ''' <summary>
    ''' Obtiene la Instancia singleton de LocalCacheFactory
    ''' </summary>
    Public Shared ReadOnly Property Instance() As LocalCacheFactory
        Get
            Return m_instance
        End Get
    End Property

#End Region

#Region "Members"

    Private _CurrentCache As ICache

    ''' <summary>
    ''' Obtiene el ICache actual de <see cref="ICache" />
    ''' </summary>
    Public ReadOnly Property CurrentCache() As ICache
        Get
            Return _CurrentCache
        End Get
    End Property

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Exclusivo del Patron singleton
    ''' </summary>
    Shared Sub New()
    End Sub
    Private Sub New()
        Try
            _CurrentCache = New LocalCache()
        Catch
            LogEvent.LogEvent()
        End Try
    End Sub

#End Region

End Class
