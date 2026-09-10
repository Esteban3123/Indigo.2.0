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

    ''' <summary>
    ''' Obtiene los StandarCost cuyo rango de fechas se solapa con el rango dado, excluyendo el registro indicado
    ''' </summary>
    Function GetOverlappingStandarCosts(validity As Date, endDate As Date, excludeId As Integer) As List(Of StandarCost)

End Interface
