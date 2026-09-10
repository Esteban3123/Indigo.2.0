'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IMaintenancePlanAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables por Código
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Function GetMaintenancePlan(Code As String, Optional tracking As Boolean = False) As MaintenancePlan

    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="MaintenancePlan">Objeto MaintenancePlan</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveMaintenancePlan(MaintenancePlan As MaintenancePlan, idSequense As Long, audit As AuditMessage) As ActionResult(Of MaintenancePlan)

    ''' <summary>
    ''' Función para Eliminar MaintenancePlan
    ''' </summary>
    ''' <param name="MaintenancePlan">Objeto MaintenancePlan</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteMaintenancePlan(MaintenancePlan As MaintenancePlan, audit As AuditMessage) As Boolean

End Interface
