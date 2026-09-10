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


Public Interface IMaintenancePlanDetailRepository
    Inherits IRepository(Of MaintenancePlanDetail)
End Interface
