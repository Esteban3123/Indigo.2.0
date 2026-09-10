#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetStatusAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllFixedAssetStatus() As List(Of FixedAssetStatusAsset)

    ''' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteFixedAssetStatus(ByVal FixedAssetStatusAsset As FixedAssetStatusAsset, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="FixedAssetStatusAsset"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetStatusAsset(ByVal FixedAssetStatusAsset As FixedAssetStatusAsset, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetStatusAsset)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetStatusAsset(ByVal Code As String) As FixedAssetStatusAsset

    ''' <summary>
    ''' Cambiar el Estado del Estado de Activos Fijos
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function FixedAssetStatusChangeState(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetStatusAsset)

End Interface
