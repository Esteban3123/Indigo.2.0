Imports Domain.Base
Imports Domain.Base.Entities

Public Interface IAverageStandardCostRepository
    Inherits IRepository(Of StandarCost)

    ''' <summary>
    ''' Obtiene al entidad por código
    ''' </summary>
    ''' <param name="standarCostCode"></param>
    ''' <returns></returns>
    Function GetAverageStandardCostByCode(standarCostCode As String) As StandarCost

    ''' <summary>
    ''' Obtiene al entidad por Id
    ''' </summary>
    ''' <param name="standarCostId"></param>
    ''' <returns></returns>
    Function GetAverageStandardCostById(standarCostId As Integer) As StandarCost


End Interface
