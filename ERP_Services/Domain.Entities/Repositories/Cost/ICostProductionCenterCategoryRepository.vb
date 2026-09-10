#Region "Imports"

Imports Domain.Base

#End Region

Public Interface ICostProductionCenterCategoryRepository
    Inherits IRepository(Of CostProductionCenterCategory)

    ''' <summary>
    ''' Función que obtiene un indicio de deterioro por código
    ''' </summary>
    ''' <param name="Code">Código del indicio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCostProductionCenterCategoryByCode(Code As String) As CostProductionCenterCategory

    ''' <summary>
    ''' Función que obtiene todas los indicios de deterioro
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllCostProductionCenterCategory() As List(Of CostProductionCenterCategory)

End Interface
