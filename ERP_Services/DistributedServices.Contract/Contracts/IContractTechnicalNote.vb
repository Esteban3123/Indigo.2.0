#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface IContractTechnicalNote

    ''' <summary>
    ''' Obtiene una nota técnica por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTechnicalNoteById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote)

    ''' <summary>
    ''' Obtiene una nota técnica por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetTechnicalNote(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote)

    ''' <summary>
    ''' Guarda o Actualiza una nota técnica
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveTechnicalNote(ByVal TechnicalNote As TechnicalNote, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of TechnicalNote)

    ''' <summary>
    ''' Cambia el estado de la nota técnica
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateTechnicalNote(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TechnicalNote)

End Interface
