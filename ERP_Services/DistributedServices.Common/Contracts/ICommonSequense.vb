#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface ICommonSequense

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    <OperationContract()>
    Function GetSequenseByIdForm(idForm As String, session As SessionValues) As CommonSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    <OperationContract()>
    Function GetNumericSequenseGroupById(id As Integer, session As SessionValues) As List(Of String)

    <OperationContract()>
    Function ListSequences(session As SessionValues) As List(Of Domain.Entities.Sequense)

    <OperationContract()>
    Function SaveSequence(ByVal seq As CommonSequence) As ActionResult

    <OperationContract()>
    Function SavePatternSequence(ByVal seq As Domain.Entities.Sequense, session As SessionValues) As ActionResult

    <OperationContract()>
    Function GetPatternSequence(ByVal idSeq As Integer, session As SessionValues) As Domain.Entities.Sequense

    <OperationContract()>
    Function GetPatternSequenceByName(ByVal nameSeq As String, session As SessionValues) As Domain.Entities.Sequense

#End Region

End Interface
