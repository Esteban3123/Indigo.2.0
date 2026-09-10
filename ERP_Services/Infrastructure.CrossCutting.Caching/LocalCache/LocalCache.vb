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

#Region "Imports"
Imports Microsoft.ApplicationServer.Caching
Imports System.Configuration
#End Region

''' <summary>
''' Clase con los Metodos Necesarios para la configuracion y mapeo del repositorio de cache local
''' </summary>
Public Class LocalCache
    Inherits Common

    ''' <summary>
    ''' inicializa una nueva instancia de la clase <see cref="LocalCache" />.
    ''' </summary>
    Sub New()
        MyBase.New(Connections.ConnectLocalCache())
    End Sub

End Class
