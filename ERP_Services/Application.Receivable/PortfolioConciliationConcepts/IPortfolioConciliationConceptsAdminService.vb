Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPortfolioConciliationConceptsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetPortfolioConciliationConceptsByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetPortfolioConciliationConceptsById(ByVal Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SavePortfolioConciliationConcepts(ByVal PortfolioConciliationConcepts As PortfolioConciliationConcepts, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStatePortfolioConciliationConcepts(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeletePortfolioConciliationConcepts(ByVal PortfolioConciliationConcepts As PortfolioConciliationConcepts, ByVal audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

End Interface
