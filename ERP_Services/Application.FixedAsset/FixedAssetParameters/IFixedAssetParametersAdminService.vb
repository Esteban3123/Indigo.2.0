#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ISettingFixedAssetAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingFixedAssetByOperatingUnitId(OperatingUnitId As Integer) As SettingFixedAsset

    ''' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="SettingFixedAsset"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteSettingFixedAsset(ByVal SettingFixedAsset As SettingFixedAsset, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="SettingFixedAsset"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSettingFixedAsset(ByVal SettingFixedAsset As SettingFixedAsset, ByVal audit As AuditMessage) As ActionResult(Of SettingFixedAsset)

End Interface
