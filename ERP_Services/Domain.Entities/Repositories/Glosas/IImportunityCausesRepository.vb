#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IImportunityCausesRepository
    Inherits IRepository(Of ImportunityCauses)

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetImportunityCausesByCode(code As String) As ImportunityCauses

    ''' <summary>
    '''  Obtiene un concepto por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetImportunityCausesById(id As Integer) As ImportunityCauses

End Interface
