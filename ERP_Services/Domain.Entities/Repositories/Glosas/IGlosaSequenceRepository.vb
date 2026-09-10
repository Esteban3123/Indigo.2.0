#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IGlosaSequenceRepository
    Inherits IRepository(Of GlosaSequence)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Function GetSequenseByIdForm(ByVal idForm As String) As GlosaSequence

#End Region

End Interface
