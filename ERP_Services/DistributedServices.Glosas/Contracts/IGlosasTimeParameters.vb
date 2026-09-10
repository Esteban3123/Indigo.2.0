Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IGlosasTimeParameters

#Region "Methods"

    ''' <summary>
    ''' Obtiene un conjunto de parametros de tiempo por su numero de Id
    ''' </summary>
    ''' <param name="Id">Id del conjunto de parametros</param>
    ''' <returns>El conjunto de parametros</returns>
    <OperationContract>
    Function GetTimeParameters(Id As String, session As SessionValues) As TimeParameters

    ''' <summary>
    ''' Obtiene el primero o por defecto conjunto de parametros de tiempo
    ''' </summary>
    ''' <returns>El conjunto de parametros</returns>
    <OperationContract>
    Function GetTimeParametersSingleOrDefault(ByVal Entity As String, session As SessionValues, _IdIOperatingUnit As Integer) As TimeParameters

    ''' <summary>
    ''' Lista todos los conjuntos de parametros de tiempo
    ''' </summary>
    ''' <returns>Lista de conjuntos de parametros</returns>
    <OperationContract>
    Function ListAllTimeParameters(session As SessionValues, _IdIOperatingUnit As Integer, Optional Control As String = "") As List(Of TimeParameters)

    ''' <summary>
    ''' Graba o actualiza un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Conjunto de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    <OperationContract>
    Function SaveTimeParameters(ByVal obj As TimeParameters, session As SessionValues) As ActionResult(Of TimeParameters)
    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    <OperationContract>
    Function ListControlParametersTime(Invoice As String, Entity As String, session As SessionValues, ByVal _IdIOperatingUnit As Integer) As List(Of ControlParametersTime)

    ''' <summary>
    ''' Graba o actualiza una lista de parametros de tiempo
    ''' </summary>
    ''' <param name="ListParameters">Lista de parametros de tiempo</param>
    ''' <returns>Un objeto con el resultado de la accion</returns>
    <OperationContract>
    Function SaveListTimeParameters(ListParameters As List(Of TimeParameters), ByVal listConceptGloss As List(Of Domain.Entities.ConceptGlosas), session As SessionValues) As Domain.Base.Entities.ActionResult(Of List(Of TimeParameters))

    ''' <summary>
    ''' Eliminar un conjunto de parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Parametros de Tiempo</param>
    ''' <param name="session">Objeto Session</param>
    ''' <returns>Action Result</returns>
    <OperationContract>
    Function DeleteTimeParameters(obj As TimeParameters, session As SessionValues) As ActionResult

#End Region

End Interface
