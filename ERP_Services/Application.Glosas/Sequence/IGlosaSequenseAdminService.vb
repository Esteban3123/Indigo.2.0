#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

Public Interface IGlosaSequenseAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene la secuencia numerica para un formulario
    ''' </summary>
    ''' <param name="idForm">Id del formulario a consultar</param>
    ''' <returns>Secuencia numerica</returns>
    Function GetSequenseByIdForm(idForm As String) As GlosaSequence

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Function GetNumericSequenseGroupById(ByVal id As Int32) As List(Of String)

    ''' <summary>
    ''' Guarda la secuencia
    ''' </summary>
    ''' <param name="seq"></param>
    ''' <returns></returns>
    Function SaveSequence(ByVal seq As GlosaSequence) As ActionResult

#End Region

End Interface
