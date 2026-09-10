Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CUPSEntityContractDescriptionsRepository
    Inherits GenericRepository(Of CUPSEntityContractDescriptions)
    Implements ICupsEntityContractDescriptionsRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork
    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' lista Descripciones del contrato de entidad 
    ''' </summary>
    ''' <param name="CupsId"></param>
    ''' <returns></returns>
    Public Function ListCUPSEntityContractDescriptionsByCupsId(CupsId As Integer) As List(Of CUPSEntityContractDescriptions) Implements ICupsEntityContractDescriptionsRepository.ListCUPSEntityContractDescriptionsByCupsId
        Dim res = (From ch In _context.CUPSEntityContractDescriptions.AsNoTracking() Where ch.CUPSEntityId = CupsId Select ch).ToList()
        Return res
    End Function

End Class
