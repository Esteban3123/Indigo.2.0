#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetInventoryTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllFixedAssetInventoryType() As List(Of FixedAssetInventoryType)

    ''' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteFixedAssetInventoryType(ByVal FixedAssetInventoryType As FixedAssetInventoryType, ByVal audit As AuditMessage) As Domain.Base.Entities.ActionResult

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="FixedAssetInventoryType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetInventoryType(ByVal FixedAssetInventoryType As FixedAssetInventoryType, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInventoryType)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetInventoryType(ByVal codeInsurance As String) As FixedAssetInventoryType

    ''' <summary>
    ''' Cambiar el Estado del Estado
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function ChangeFixedAssetInventoryTypeStatus(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetInventoryType)


End Interface
