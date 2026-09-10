#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IPortfolioConciliationConceptsRepository
    Inherits IRepository(Of PortfolioConciliationConcepts)

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetPortfolioConciliationConceptsByCode(code As String) As PortfolioConciliationConcepts

    ''' <summary>
    '''  Obtiene un concepto por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioConciliationConceptsById(id As Integer) As PortfolioConciliationConcepts

End Interface
