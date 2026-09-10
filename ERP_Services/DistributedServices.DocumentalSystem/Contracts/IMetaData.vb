#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.DocumentalSystem.Entities
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IMetaData

    ''' <summary>
    ''' Función que obtiene una lista de metadata.
    ''' </summary>
    ''' <returns>Lista de Metadata</returns>
    <OperationContract>
    Function ListAllMetadata(sessionInf As SessionInfo) As List(Of Metadata)
    ''' <summary>
    ''' Funcion para obtener metadata según archivador
    ''' </summary>
    ''' <param name="sessionInf">Objeto sessionInfo</param>
    ''' <returns>Lista de Metadata</returns>
    <OperationContract>
    Function GetMetadataByFileContainer(sessionInf As SessionInfo) As List(Of Metadata)
    ''' <summary>
    ''' Funcion Lista de Metadata 
    ''' </summary>
    ''' <param name="IdFileContainer">Id del contenedor</param>
    ''' <param name="Indigo">Variable de sesion</param>
    ''' <returns>Lista de Metada</returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function ListMetadataByFileContainer(ByVal IdFileContainer As String, ByVal Indigo As SessionValues) As List(Of Domain.DocumentalSystem.Entities.Metadata)
    ''' <summary>
    ''' Funcion para guardar metadata
    ''' </summary>
    ''' <param name="_listMetadata"></param>
    <OperationContract>
    Function saveListMetata(ByVal _listMetadata As List(Of Metadata), ByVal indigo As SessionValues) As ActionResult
    ''' <summary>
    ''' Funcion para eliminar metadata
    ''' </summary>
    ''' <param name="_objMetadata"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract>
    Function DeleteMetadata(ByVal _objMetadata As Metadata, ByVal indigo As SessionValues) As ActionResult
End Interface



