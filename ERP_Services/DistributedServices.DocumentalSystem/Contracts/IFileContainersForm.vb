#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.DocumentalSystem.Entities
#End Region

<ServiceContract()> _
Public Interface IFileContainersForm

    ''' <summary>
    ''' Función que obtiene una lista de archivadores formularios.
    ''' </summary>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de FileContainersForm</returns>
    <OperationContract>
    Function ListAllFileContainersForm(session As SessionValues) As List(Of FileContainersForm)
    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Archivador
    ''' </summary>
    ''' <param name="Id">Id del contenedor de archivos</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de FileContainersForm</returns>
    <OperationContract>
    Function ListFileContainersFormByIdFileContainer(Id As String, session As SessionValues) As List(Of FileContainersForm)
    ''' <summary>
    ''' Funcion para obtener una lista de archivadores por formulario segun Id del Formulario
    ''' </summary>
    ''' <param name="Id">Id del formulario</param>
    ''' <param name="session">Objeto session</param>
    ''' <returns>Lista de FileContainersForm</returns>
    <OperationContract>
    Function ListFileContainersFormByIdForm(Id As String, session As SessionValues) As List(Of FileContainersForm)
End Interface



