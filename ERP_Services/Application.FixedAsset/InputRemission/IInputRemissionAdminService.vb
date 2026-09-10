#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetRemissionEntranceAdminService
    Inherits IDisposable

    '' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteFixedAssetRemissionEntrance(ByVal FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="FixedAssetRemissionEntrance">FixedAssetRemissionEntrance</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetRemissionEntrance(ByVal FixedAssetRemissionEntrance As FixedAssetRemissionEntrance, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetRemissionEntrance)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetRemissionEntranceByCode(ByVal code As String) As FixedAssetRemissionEntrance

End Interface
