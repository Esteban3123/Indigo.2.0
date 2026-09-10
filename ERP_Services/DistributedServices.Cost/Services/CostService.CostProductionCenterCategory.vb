#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.Cost
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class CostService
    Implements ICostServiceCostProductionCenterCategory

    ''' <summary>
    ''' Función que obtiene una categoria de centro de produccion por el código
    ''' </summary>
    ''' <param name="Code">Código del indicio de deterioro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCostProductionCenterCategoryByCode(Code As String) As Domain.Entities.CostProductionCenterCategory Implements ICostServiceCostProductionCenterCategory.GetCostProductionCenterCategoryByCode
        Using service As ICostProductionCenterCategoryAdminService = Container.Current.Resolve(Of ICostProductionCenterCategoryAdminService)()
            Return service.GetCostProductionCenterCategory(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas las categorias de centro de produccion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllCostProductionCenterCategory(audit As AuditMessage) As List(Of Domain.Entities.CostProductionCenterCategory) Implements ICostServiceCostProductionCenterCategory.ListAllCostProductionCenterCategory
        Using service As ICostProductionCenterCategoryAdminService = Container.Current.Resolve(Of ICostProductionCenterCategoryAdminService)()
            Return service.ListAllCostProductionCenterCategory()
        End Using
    End Function

    ''' <summary>
    ''' Función para almacenar una categoria de centro de produccion
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveCostProductionCenterCategory(CostProductionCenterCategory As Domain.Entities.CostProductionCenterCategory, idSequense As Int64, audit As AuditMessage) As ActionResult(Of CostProductionCenterCategory) Implements ICostServiceCostProductionCenterCategory.SaveCostProductionCenterCategory
        Using service As ICostProductionCenterCategoryAdminService = Container.Current.Resolve(Of ICostProductionCenterCategoryAdminService)()
            Return service.SaveCostProductionCenterCategory(CostProductionCenterCategory, audit, idSequense)
        End Using
    End Function

    ''' <summary>
    ''' Función para actualizar estado de una categoria de centro de produccion
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function CostProductionCenterCategoryChangeState(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of CostProductionCenterCategory) Implements ICostServiceCostProductionCenterCategory.CostProductionCenterCategoryChangeState
        Using service As ICostProductionCenterCategoryAdminService = Container.Current.Resolve(Of ICostProductionCenterCategoryAdminService)()
            Return service.CostProductionCenterCategoryChangeState(Code, State, audit)
        End Using
    End Function

    ''' <summary>
    ''' Función para eliminar una categoria de centro de produccion
    ''' </summary>
    ''' <param name="CostProductionCenterCategory"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteCostProductionCenterCategory(CostProductionCenterCategory As Domain.Entities.CostProductionCenterCategory, audit As AuditMessage) As ActionResult Implements ICostServiceCostProductionCenterCategory.DeleteCostProductionCenterCategory
        Using service As ICostProductionCenterCategoryAdminService = Container.Current.Resolve(Of ICostProductionCenterCategoryAdminService)()
            Return service.DeleteCostProductionCenterCategory(CostProductionCenterCategory, audit)
        End Using
    End Function

End Class
