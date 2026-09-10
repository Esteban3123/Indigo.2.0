
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
#End Region

<ServiceContract()>
Public Interface IPortfolioConciliationConcepts

#Region "Methods"

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPortfolioConciliationConceptsByCode(code As String, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPortfolioConciliationConceptsById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SavePortfolioConciliationConcepts(PortfolioConciliationConcepts As PortfolioConciliationConcepts, audit As AuditMessage, idSequense As Int64) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ChangeStatePortfolioConciliationConcepts(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="PortfolioConciliationConcepts">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeletePortfolioConciliationConcepts(PortfolioConciliationConcepts As PortfolioConciliationConcepts, audit As AuditMessage) As ActionResult(Of PortfolioConciliationConcepts)

#End Region

End Interface
