Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IConceptsCausesStatusFolioAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetConceptsCausesStatusFolioById(ByVal Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetConceptsCausesStatusFolioByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveConceptsCausesStatusFolio(ByVal ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStateConceptsCausesStatusFolio(ByVal Id As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteConceptsCausesStatusFolio(ByVal ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, ByVal audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

End Interface
