'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Andres Alarcon
' Created          : 10-06-2024
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IFixedAssetCatalogOfPropertyandServicesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="FixedAssetCatalogOfPropertyandServices">FixedAssetCatalogOfPropertyandServices</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetCatalogOfPropertyandServices(ByVal FixedAssetCatalogOfPropertyandServices As FixedAssetCatalogOfPropertyandServices, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetCatalogOfPropertyandServices)

    ''' <summary>
    ''' Obtiene por medio de codigo un catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetCatalogOfPropertyandServicesByCode(ByVal Code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices)

    ''' <summary>
    ''' Función para Eliminar un catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteCatalogOfPropertyandServices(Trademark As FixedAssetCatalogOfPropertyandServices, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambiar el Estado del catalogo de bienes y servicios
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function ChangeCatalogOfPropertyandServicesStatus(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetCatalogOfPropertyandServices)

End Interface
