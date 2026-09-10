Imports Domain.Base

Public Interface IConceptsCausesStatusFolioRepository
    Inherits IRepository(Of ConceptsCausesStatusFolio)

    ''' <summary>
    ''' obtiene el Conceptos Causas de Estado Folio
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConceptsCausesStatusFolioById(id As Integer) As ConceptsCausesStatusFolio

    ''' <summary>
    ''' obtiene el Conceptos Causas de Estado Folio
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConceptsCausesStatusFolioByCode(Code As String) As ConceptsCausesStatusFolio


End Interface
