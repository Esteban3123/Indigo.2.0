#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetEntryDevolutionAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetEntryDevolution(ByVal FixedAssetEntryDevolution As FixedAssetEntryDevolution, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetEntryDevolution)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryDevolution(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetEntryDevolution)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetEntryDevolutionById(Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetEntryDevolution)

    ''' <summary>
    ''' Confirma un registro
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmFixedAssetEntryDevolution(ByVal FixedAssetEntryDevolution As FixedAssetEntryDevolution, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetEntryDevolution)

End Interface
