#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetDepreciationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene el registro por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetDepreciation(code As String, audit As AuditMessage) As ActionResult(Of FixedAssetDepreciation)

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetFixedAssetDepreciationById(Id As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetDepreciation)

    ''' <summary>
    ''' Genera y guarda la depreciación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, audit As AuditMessage, ModeConfirm As Boolean) As ActionResult(Of FixedAssetDepreciation)

    ''' <summary>
    ''' Confirma la depreciación
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConfirmDepreciation(DepreciationMonth As Integer, DepreciationYear As Integer, OperatingUnitId As Integer, audit As AuditMessage) As ActionResult(Of FixedAssetDepreciation)

End Interface
