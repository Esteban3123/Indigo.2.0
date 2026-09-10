#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IGlosaMedicalFeesConceptsRepository
    Inherits IRepository(Of GlosaMedicalFeesConcepts)

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetGlosaMedicalFeesConceptsByCode(code As String) As GlosaMedicalFeesConcepts

    ''' <summary>
    '''  Obtiene un concepto por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetGlosaMedicalFeesConceptsById(id As Integer) As GlosaMedicalFeesConcepts

End Interface
