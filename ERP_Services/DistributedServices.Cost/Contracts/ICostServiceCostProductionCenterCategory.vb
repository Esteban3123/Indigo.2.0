#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

<ServiceContract()>
Public Interface ICostServiceCostProductionCenterCategory

    ''' <summary>
    ''' Función que obtiene una categoria de centro de produccion por Código
    ''' </summary>
    ''' <param name="Code">Código del indicio de deterioro</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCostProductionCenterCategoryByCode(Code As String) As CostProductionCenterCategory

    ''' <summary>
    ''' Función que obtiene todas las categorias de centro de produccion
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllCostProductionCenterCategory(audit As AuditMessage) As List(Of CostProductionCenterCategory)

    ''' <summary>
    ''' Función para almacenar una categoria de centro de produccion
    ''' </summary>
    ''' <param name="CostProductionCenterCategory"></param>
    ''' <param name="idSequense"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveCostProductionCenterCategory(CostProductionCenterCategory As CostProductionCenterCategory, idSequense As Int64, audit As AuditMessage) As ActionResult(Of CostProductionCenterCategory)

    ''' <summary>
    ''' Función para actualizar estado de una categoria de centro de produccion
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function CostProductionCenterCategoryChangeState(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of CostProductionCenterCategory)

    ''' <summary>
    ''' Función para eliminar una categoria de centro de produccion
    ''' </summary>
    ''' <param name="CostProductionCenterCategory"></param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteCostProductionCenterCategory(CostProductionCenterCategory As CostProductionCenterCategory, audit As AuditMessage) As ActionResult

End Interface
