Imports Application.Cost
Imports Microsoft.Practices.Unity

Partial Public Class CostService
    Implements ICostServiceCostSequence

    Public Function GetNumericSequenseGroupById(id As Integer) As List(Of String) Implements ICostServiceCostSequence.GetNumericSequenseGroupById
        Using service As ICostSequenceAdminService = Container.Current.Resolve(Of ICostSequenceAdminService)()
            Return service.GetNumericSequenseGroupById(id)
        End Using
        'Return _costSequenceAdminService.GetNumericSequenseGroupById(id)
    End Function

    Public Function GetSequenseByIdForm(idForm As String) As Domain.Entities.CostSecuence Implements ICostServiceCostSequence.GetSequenseByIdForm
        Using service As ICostSequenceAdminService = Container.Current.Resolve(Of ICostSequenceAdminService)()
            Return service.GetSequenseByIdForm(idForm)
        End Using
        'Return _costSequenceAdminService.GetSequenseByIdForm(idForm)
    End Function

    Public Function SaveSequence(seq As Domain.Entities.CostSecuence) As Domain.Base.Entities.ActionResult Implements ICostServiceCostSequence.SaveSequence
        Using service As ICostSequenceAdminService = Container.Current.Resolve(Of ICostSequenceAdminService)()
            Return service.SaveSequence(seq)
        End Using
        'Return _costSequenceAdminService.SaveSequence(seq)
    End Function
End Class
