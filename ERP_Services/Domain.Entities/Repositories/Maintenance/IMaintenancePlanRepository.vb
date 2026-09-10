'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 04-09-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region

Public Interface IMaintenancePlanRepository
    Inherits IRepository(Of MaintenancePlan)

    ''' <summary>
    ''' consulta para retornar un Plan de Mantenimiento por Código
    ''' </summary>
    ''' <param name="codeMaintenancePlan">el codigo del Plan de Mantenimiento</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetMaintenancePlan(ByVal codeMaintenancePlan As String, Optional Tracking As Boolean = False) As MaintenancePlan

End Interface
