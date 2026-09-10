#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ICostProductionCenterCategoryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Permite obtener una categoria de centro de produccion por el código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCostProductionCenterCategory(ByVal Code As String) As CostProductionCenterCategory

    ''' <summary>
    ''' Permite listar todas las categorias de centro de produccion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllCostProductionCenterCategory() As List(Of CostProductionCenterCategory)

    ''' <summary>
    ''' funcion que sirve para almacenar una categoria de centro de produccion
    ''' </summary>
    ''' <param name="CostProductionCenterCategory"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveCostProductionCenterCategory(ByVal CostProductionCenterCategory As CostProductionCenterCategory, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of CostProductionCenterCategory)

    ''' <summary>
    ''' Permite actualizar estado de una categoria de centro de produccion
    ''' </summary>
    ''' <param name="code">Code</param>
    ''' <param name="state">State</param>
    ''' <param name="audit">Audit</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function CostProductionCenterCategoryChangeState(code As String, state As Boolean, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of CostProductionCenterCategory)

    ''' <summary>
    ''' Permite eliminar una categoria de centro de produccion
    ''' </summary>
    ''' <param name="CostProductionCenterCategory"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteCostProductionCenterCategory(ByVal CostProductionCenterCategory As CostProductionCenterCategory, ByVal audit As AuditMessage) As ActionResult

End Interface
