#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.DocumentalSystem.Entities
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFileContainer

    ''' <summary>
    ''' Función que obtiene una lista de archivadores.
    ''' </summary>
    ''' <param name="session">objeto Session</param>
    ''' <returns>Lista de Archivadores</returns>
    <OperationContract>
    Function ListAllFileContainers(session As SessionValues) As List(Of FileContainer)
    ''' <summary>
    ''' Funcion para obtener un archivador
    ''' </summary>
    ''' <param name="session">objeto SessionInf</param>
    ''' <returns>Objeto Archivador</returns>
    <OperationContract>
    Function GetFileContainer(ByVal Id As String, ByVal session As SessionValues) As FileContainer
    ''' <summary>
    ''' Función para borrar un archivador
    ''' </summary>
    ''' <param name="fileContainer">Objeto Archivador</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function DeleteFileContainer(fileContainer As FileContainer, session As SessionValues) As ActionResult(Of FileContainer)
    ''' <summary>
    ''' Función para guardar un archivador
    ''' </summary>
    ''' <param name="fileContainer">Objeto Archivador</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>ActionResult</returns>
    <OperationContract>
    Function SaveFileContainer(fileContainer As FileContainer, session As SessionValues) As ActionResult(Of FileContainer)
End Interface



