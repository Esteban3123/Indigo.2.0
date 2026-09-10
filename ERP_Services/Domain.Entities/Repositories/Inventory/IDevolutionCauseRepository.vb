#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IDevolutionCauseRepository
    Inherits IRepository(Of DevolutionCause)

    ''' <summary>
    ''' Obtiene una causa de devolución por codigo
    ''' </summary>
    ''' <param name="code">codigo de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDevolutionCauseByCode(code As String) As DevolutionCause

    ''' <summary>
    ''' obtiene una causa de devolución por id
    ''' </summary>
    ''' <param name="id">id de la unidad de paquete</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDevolutionCauseById(id As Integer) As DevolutionCause

End Interface
