Imports System.ServiceModel

<ServiceContract()>
Public Interface IXpoDefaultGate

    ''' <summary>
    ''' Obtiene el numero de Endpoint disponible para usar por una
    ''' nueva instancia de cliente
    ''' </summary>
    ''' <returns>Numero de Endpoint disponible</returns>
    <OperationContract()>
    Function GetEndpointNumber() As Integer

End Interface
