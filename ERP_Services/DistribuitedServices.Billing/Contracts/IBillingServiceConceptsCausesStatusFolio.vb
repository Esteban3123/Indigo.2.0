
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IConceptsCausesStatusFolio

#Region "Methods"


    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetConceptsCausesStatusFolioByCode(code As String, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetConceptsCausesStatusFolioById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveConceptsCausesStatusFolio(ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, audit As AuditMessage, idSequense As Int64) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStateConceptsCausesStatusFolio(Id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="ConceptsCausesStatusFolio">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteConceptsCausesStatusFolio(ConceptsCausesStatusFolio As ConceptsCausesStatusFolio, audit As AuditMessage) As ActionResult(Of ConceptsCausesStatusFolio)

#End Region

End Interface
