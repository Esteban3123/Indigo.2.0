
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IMedicalGlosaMedicalFeesConcepts

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGlosaMedicalFeesConceptsByCode(code As String, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetGlosaMedicalFeesConceptsById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, audit As AuditMessage, idSequense As Int64) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateGlosaMedicalFeesConcepts(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteGlosaMedicalFeesConcepts(GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

#End Region

End Interface
