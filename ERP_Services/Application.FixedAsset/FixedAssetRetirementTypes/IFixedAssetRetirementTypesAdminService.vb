#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IFixedAssetRetirementTypesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos defectos
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    Function GetAllFixedAssetRetirementTypes(ByVal audit As AuditMessage) As List(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveFixedAssetRetirementTypes(ByVal FixedAssetRetirementTypes As FixedAssetRetirementTypes, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteFixedAssetRetirementTypes(ByVal FixedAssetRetirementTypes As FixedAssetRetirementTypes, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetFixedAssetRetirementTypesByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes)

    ''' <summary>
    ''' Obtiene un grupo por id
    ''' </summary>
    ''' <returns></returns>
    Function GetFixedAssetRetirementTypesById(ByVal id As Integer, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetRetirementTypes)

End Interface
