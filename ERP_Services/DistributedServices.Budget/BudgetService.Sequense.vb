#Region "Imports"

Imports Application.Budget
Imports Microsoft.Practices.Unity

#End Region

Partial Class BudgetService

    ''' <summary>
    ''' Obtiene la configuración de secuencia numerica asignada al frontal
    ''' </summary>
    ''' <param name="idForm">Id del frontal a consultar</param>
    ''' <returns>Secuencia numerica asignada al frontal</returns>
    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.BudgetSequence Implements IBudgetSequense.GetSequenseByIdForm
        Using service As IBudgetSequenseAdminService = Container.Current.Resolve(Of IBudgetSequenseAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un grupo de secuencias numericas por su id de configuración
    ''' </summary>
    ''' <param name="id">Id de la configuración de la secuencia</param>
    ''' <returns>Grupo de secuencias numericas</returns>
    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements IBudgetSequense.GetNumericSequenseGroupById
        Using service As IBudgetSequenseAdminService = Container.Current.Resolve(Of IBudgetSequenseAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
    End Function

    Public Function SaveSequence(seq As Domain.Entities.BudgetSequence) As Domain.Base.Entities.ActionResult Implements IBudgetService.SaveSequence
        Using service As IBudgetSequenseAdminService = Container.Current.Resolve(Of IBudgetSequenseAdminService)()
            Return service.SaveSequence(seq)
        End Using
    End Function

End Class