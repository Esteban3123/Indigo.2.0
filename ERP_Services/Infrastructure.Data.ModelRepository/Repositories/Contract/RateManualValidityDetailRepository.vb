#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class RateManualValidityDetailRepository
    Inherits GenericRepository(Of RateManualValidityDetail)
    Implements IRateManualValidityDetailRepository

#Region "Builder"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicializa el contexto
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    ''' <summary>
    ''' funcion para consultar el manual tarifario definido en la definicion de tarifa, por Id de vigencia y fecha de servicio
    ''' </summary>
    ''' <param name="RateManualValidityId"></param>
    ''' <param name="ServiceDate"></param>
    ''' <returns></returns>
    Public Function GetRateManualTypeAndRateManualId(RateManualValidityId As Integer, ServiceDate As Date) As Object Implements IRateManualValidityDetailRepository.GetRateManualTypeAndRateManualId

        Dim query = (From x In _context.RateManualValidityDetail
                     Where x.RateManualValidityId = RateManualValidityId AndAlso x.InitialDate <= ServiceDate AndAlso x.EndDate >= ServiceDate
                     Select New With {Key x.RateManualId, Key x.RateManual.Type})?.FirstOrDefault
        Return query
    End Function

#End Region

End Class
