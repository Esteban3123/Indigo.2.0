#Region "Imports"
Imports Domain.Base
#End Region

Public Interface ICupsEntityContractDescriptionsRepository
    Inherits IRepository(Of CUPSEntityContractDescriptions)

    ''' <summary>
    ''' lista las Descripciones del contrato de entidad 
    ''' </summary>
    ''' <param name="CupsId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCUPSEntityContractDescriptionsByCupsId(CupsId As Integer) As List(Of CUPSEntityContractDescriptions)

End Interface
