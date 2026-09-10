#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IGlosaSequenceDetailRepository
    Inherits IRepository(Of GlosaSequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As GlosaSequenceDetail

    Function GetSequenseDetailUpdatedById(idSequence As Int32) As GlosaSequenceDetail

#End Region

End Interface
