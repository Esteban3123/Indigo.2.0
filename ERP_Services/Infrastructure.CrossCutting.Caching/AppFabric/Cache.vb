'***********************************************************************
' Assembly         : Infrastructure.CrossCutting.Caching
' Author           : WalterSierra
' Created          : 10-05-2011
'
' Last Modified By : WalterSierra
' Last Modified On : 10-05-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Microsoft.ApplicationServer.Caching
Imports System.Configuration
#End Region

''' <summary>
''' Clase con los Metodos Necesarios para la configuracion y mapeo del repositorio de cache
''' </summary>
Friend NotInheritable Class Cache
    Inherits Common

    ''' <summary>
    ''' inicializa una nueva instancia de la clase <see cref="Cache" />.
    ''' </summary>
    Sub New()
        MyBase.New(Connections.ConnectAppFabric())
    End Sub

End Class