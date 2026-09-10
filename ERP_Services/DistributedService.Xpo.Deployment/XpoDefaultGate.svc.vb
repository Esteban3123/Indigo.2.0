
''' <summary>
''' Provee servicios de inicialización para
''' los proxies Xpo
''' </summary>
Public Class XpoDefaultGate
    Implements IXpoDefaultGate

    ''' <summary>
    ''' Cantidad de servicios XpoGateEx disponibles para el balanceo
    ''' de peticiones
    ''' </summary>
    Public Const TOTAL_GATES As Integer = 49

    ''' <summary>
    ''' Objeto para sincronizar el acceso de hilos
    ''' </summary>
    Public Shared ObjSysRoot As Object

    ''' <summary>
    ''' Número de Endpoint actual, disponible para la proxima
    ''' instancia de cliente a conectar
    ''' </summary>
    Public Shared CurrentEndpointNumber As Integer

    ''' <summary>
    ''' Inicializa las variables estaticas del servicio
    ''' </summary>
    Shared Sub New()
        ObjSysRoot = New Object()
        CurrentEndpointNumber = -1
    End Sub

    ''' <summary>
    ''' Obtiene el numero de Endpoint disponible para usar por una
    ''' nueva instancia de cliente
    ''' </summary>
    ''' <returns>Numero de Endpoint disponible</returns>
    Public Function GetEndpointNumber() As Integer Implements IXpoDefaultGate.GetEndpointNumber
        SyncLock ObjSysRoot
            If CurrentEndpointNumber = TOTAL_GATES Then
                CurrentEndpointNumber = 0
            Else
                CurrentEndpointNumber += 1
            End If
            Return CurrentEndpointNumber
        End SyncLock
    End Function

End Class
