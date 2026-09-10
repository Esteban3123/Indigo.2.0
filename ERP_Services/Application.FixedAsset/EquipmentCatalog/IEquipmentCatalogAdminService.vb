#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IEquipmentCatalogAdminService
    Inherits IDisposable

    '' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteEquipmentCatalog(ByVal FixedAssetItemCatalog As FixedAssetItemCatalog, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="FixedAssetItemCatalog"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEquipmentCatalog(ByVal FixedAssetItemCatalog As FixedAssetItemCatalog, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetItemCatalog)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEquipmentCatalogByCode(ByVal codeInsurance As String) As FixedAssetItemCatalog

    ''' <summary>
    ''' Cambia el estado  del artículo según el código
    ''' </summary>
    ''' <param name="code">Código del artículo</param>
    ''' <returns>Cargo</returns> 
    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetItemCatalog)

End Interface
