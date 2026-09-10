#Region "Importar"
Imports Domain.Base
#End Region

Public Interface IAgreementsRedemptionPointsRepository

    Inherits IRepository(Of AgreementsRedemptionPoints)

    ''' <summary>
    ''' Función que obtiene todos los convenios de redención de puntos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllAgreementsRedemptionPointsRepository() As List(Of AgreementsRedemptionPoints)

    ''' <summary>
    ''' Función que obtiene por codigo un convenio de redención de puntos
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>AgreementsRedemptionPointsRepository</returns>
    ''' <remarks></remarks>
    Function GetAgreementsRedemptionPointsRepositoryByCode(Code As String, Optional desatach As Boolean = True) As AgreementsRedemptionPoints

    ''' <summary>
    ''' Función que obtiene todos los convenios de redención de puntos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function GetAgreementsRedemptionPointsDetails(Id As Integer) As List(Of AgreementsRedemptionPointsDetail)

End Interface
