'***********************************************************************
' Assembly         : Domain.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 28/01/2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface IFixedAssetItemCatalogRepository

    Inherits IRepository(Of FixedAssetItemCatalog)

    ''' <summary>
    ''' Obtiene una Ubicación por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetItemCatalogByCode(code As String, Optional tracking As Boolean = True) As FixedAssetItemCatalog

    ''' <summary>
    ''' Obtiene un Catálogo de Items por el Id del Catálogo
    ''' </summary>
    ''' <param name="IdfixedAssetCalag">IdfixedAssetCalag</param>
    ''' <returns>FixedAssetItemCatalog</returns>
    ''' <remarks></remarks>
    Function GetFixedAssetItemCatalogById(IdfixedAssetCalag As Integer) As FixedAssetItemCatalog

    ''' <summary>
    ''' Obtiene un Catálogo de Items por el Id del Item
    ''' </summary>
    ''' <param name="IdItem">IdItem</param>
    ''' <returns>FixedAssetItemCatalog</returns>
    ''' <remarks></remarks>
    Function GetFixedAssetItemCatalogByItemId(IdItem As Integer) As FixedAssetItemCatalog
    ''' <summary>
    ''' Setea valores de los detalles de Articúlos desde archivo Excel
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetFixedAssetItemCatalogDetailFromFile(XmlObject As String) As List(Of SP_SetFixedAssetItemCatalogDetailFromFile_Result)

End Interface
