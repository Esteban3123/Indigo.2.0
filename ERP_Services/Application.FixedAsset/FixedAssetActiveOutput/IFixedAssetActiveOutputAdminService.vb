#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetActiveOutputAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveFixedAssetActiveOutput(ByVal FixedAssetActiveOutput As FixedAssetActiveOutput, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetActiveOutput)

    ''' <summary>
    ''' Confirma el ingreso
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <param name="idSequense"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmFixedAssetActiveOutput(ByVal FixedAssetActiveOutput As FixedAssetActiveOutput, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetActiveOutput)

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetActiveOutput(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetActiveOutput)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetActiveOutputById(Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetActiveOutput)

End Interface
