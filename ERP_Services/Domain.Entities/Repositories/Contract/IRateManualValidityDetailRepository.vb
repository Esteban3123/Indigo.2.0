#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IRateManualValidityDetailRepository
    Inherits IRepository(Of RateManualValidityDetail)
    ''' <summary>
    ''' funcion para obtener la informacion del manual por vigencia y fecha de servicio
    ''' </summary>
    ''' <param name="RateManualValidityId"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    Function GetRateManualTypeAndRateManualId(RateManualValidityId As Integer, ServiceDate As Date) As Object
End Interface
