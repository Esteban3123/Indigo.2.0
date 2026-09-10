#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IRateManualValidityRepository
    Inherits IRepository(Of RateManualValidity), Inject

    ''' <summary>
    ''' Obtiene una vigencia de manual tarifario por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualValidityById(id As Integer) As RateManualValidity

    ''' <summary>
    ''' Obtiene una vigencia de manual tarifario con los agregados
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualValidityByIdWithAggregates(id As Integer) As RateManualValidity

    ''' <summary>
    ''' Obtiene una vigencia de manual tarifario por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRateManualValidity(code As String) As RateManualValidity

End Interface
