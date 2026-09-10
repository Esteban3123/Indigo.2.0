Imports Domain.Base
Imports Domain.Entities

Public Interface IRevenueRecognitionDetailRepository
    Inherits IRepository(Of RevenueRecognitionDetail)

    ''' <summary>
    ''' odtiene el listado de detalles de reconocimientos de ingresos asociados a un folio
    ''' </summary>
    ''' <param name="RevenueControlDetailId"></param>
    ''' <returns></returns>
    Function GetRevenueRecognitionDetailByRevenueControlDetailId(RevenueControlDetailId As Integer) As List(Of RevenueRecognitionDetail)

End Interface
