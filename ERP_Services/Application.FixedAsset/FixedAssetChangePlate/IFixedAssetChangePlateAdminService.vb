#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetChangePlateAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetChangePlate(ByVal FixedAssetChangePlate As FixedAssetChangePlate, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetChangePlate)

    ''' <summary>
    ''' Confirma el ingreso
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmFixedAssetChangePlate(ByVal FixedAssetChangePlate As FixedAssetChangePlate, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetChangePlate)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetChangePlate(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetChangePlate)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetChangePlateById(Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetChangePlate)

End Interface
