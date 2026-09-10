#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetReclassificationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetReclassificationById(Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetReclassification)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetReclassificationByCode(ByVal Code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetReclassification)

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetReclassification(ByVal FixedAssetReclassification As FixedAssetReclassification, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetReclassification)

    ''' <summary>
    ''' Confirma un registro
    ''' </summary>
    ''' <param name="FixedAssetReclassification"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmFixedAssetReclassification(ByVal FixedAssetReclassification As FixedAssetReclassification, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetReclassification)

End Interface
