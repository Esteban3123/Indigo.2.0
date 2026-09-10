Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IGlosaMedicalFeesConceptsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetGlosaMedicalFeesConceptsByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetGlosaMedicalFeesConceptsById(ByVal Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveGlosaMedicalFeesConcepts(ByVal GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStateGlosaMedicalFeesConcepts(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="GlosaMedicalFeesConcepts">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteGlosaMedicalFeesConcepts(ByVal GlosaMedicalFeesConcepts As GlosaMedicalFeesConcepts, ByVal audit As AuditMessage) As ActionResult(Of GlosaMedicalFeesConcepts)

End Interface
