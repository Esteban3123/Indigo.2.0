
#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IAuthorizationSequence

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    <OperationContract()>
    Function GetSequenseByIdForm(idForm As String) As AuthorizationSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    <OperationContract()>
    Function GetNumericSequenseGroupById(id As Integer) As List(Of String)

    ''' <summary>
    ''' Guarda una secuencia
    ''' </summary>
    ''' <param name="seq"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSequence(ByVal seq As AuthorizationSequence) As ActionResult

#End Region

End Interface
