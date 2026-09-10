'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Andres Alarcon
' Created          : 10/06/2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base

#End Region

Public Interface IFixedAssetCatalogOfPropertyandServicesRepository
    Inherits IRepository(Of FixedAssetCatalogOfPropertyandServices)

    ''' <summary>
    ''' Obtiene un Catalogo de Bienes y Servicios
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCatalogOfPropertyandServicesByCode(Code As String) As FixedAssetCatalogOfPropertyandServices

End Interface
