#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

<ServiceContract()> _
Public Interface IMaintenancePlanService

    ''' <summary>
    ''' Función para Eliminar MaintenancePlan
    ''' </summary>
    ''' <param name="MaintenancePlan">Objeto MaintenancePlan</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteMaintenancePlan(MaintenancePlan As MaintenancePlan, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Función para Almacenar MaintenancePlan
    ''' </summary>
    ''' <param name="MaintenancePlan">Objeto MaintenancePlan</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveMaintenancePlan(MaintenancePlan As MaintenancePlan) As ActionResult(Of MaintenancePlan)

    ''' <summary>
    ''' Función que obtiene MaintenancePlan por Código
    ''' </summary>
    ''' <param name="CodeMaintenancePlan">Código de CodeMaintenancePlan</param>
    ''' <returns>MaintenancePlan</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetMaintenancePlan(CodeMaintenancePlan As String, audit As Infrastructure.CrossCutting.Base.AuditMessage, Optional tracking As Boolean = False) As MaintenancePlan

End Interface
