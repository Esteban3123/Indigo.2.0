Imports  Domain.Entities
Imports Domain.Base.Entities

Public Interface IGeneralEvaluation

    ''' <summary>
    ''' Funcion para obtener una lista de movimientos
    ''' </summary>
    ''' <returns></returns>
    Function saveMov(ByVal listmov As List(Of GlosaMovementGlosa), Optional ByVal _IdUnitoperating As Integer = 0) As Task(Of ActionResult)

    ''' <summary>
    ''' Obtiene una lista de conceptos de evaluación
    ''' </summary>
    ''' <param name="Type">Tipo Concepto</param>
    ''' <returns>Lista de Conceptos</returns>
    Function ListConceptsGlosa(ByVal Type As String) As Task(Of List(Of Domain.Entities.ConceptGlosas))

    Function ListConceptsGlosaByTypes(ByVal Type As List(Of String)) As Task(Of List(Of Domain.Entities.ConceptGlosas))

    ''' <summary>
    ''' Funcion para cargar moviminetos de facturas
    ''' </summary>
    ''' <param name="ListInvoice"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllMovementGlosabymultipleInvoice(ByVal ListInvoice As List(Of String), Optional ByVal codeUser As String = "") As Task(Of List(Of GlosaMovementGlosa))

End Interface
