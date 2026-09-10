Imports Domain.Base

Public Interface IDrugInteractionRepository
    Inherits IRepository(Of DrugInteraction)

    ''' <summary>
    ''' Obtiene una interacción de medicamento por el Id del DCI padre
    ''' </summary>
    ''' <param name="ParentDCIid"></param>
    ''' <returns></returns>
    Function GetDrugInteractionByDCIParentId(ParentDCIid As Integer) As List(Of DrugInteraction)
End Interface
