#Region "Imports"

Imports DevExpress.Xpo.DB
Imports System.ServiceModel

#End Region

Public Class WCFServiceDataStoreDefault
    Inherits ClientBase(Of IXpoDefaultGate)

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="endpointConfigurationName">Configuración del punto de entrada del servicio</param>
    ''' <param name="remoteAddress">Dirección remota del servicio</param>
    Public Sub New(ByVal endpointConfigurationName As String, ByVal remoteAddress As String)
        MyBase.New(endpointConfigurationName, remoteAddress)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el numero de Endpoint disponible para usar por una
    ''' nueva instancia de cliente
    ''' </summary>
    ''' <returns>Numero de Endpoint disponible</returns>
    Public Function GetEndpointNumber() As Integer
        Return Channel.GetEndpointNumber()
    End Function

#End Region

End Class
