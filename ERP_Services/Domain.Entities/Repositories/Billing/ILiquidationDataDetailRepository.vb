Imports Domain.Base

Public Interface ILiquidationDataDetailRepository
    Inherits IRepository(Of LiquidationDataDetail)

    ''' <summary>
    ''' obtiene los datos de condiciones especificas 
    ''' </summary>
    ''' <param name="_liquidationDataId"></param>
    ''' <returns></returns>
    Function GetLiquidationDataDetailByLiquidationDataId(_liquidationDataId As List(Of Integer)) As List(Of LiquidationDataDetail)
End Interface
