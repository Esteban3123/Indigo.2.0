'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 03-03-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************
#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region

Public Interface IEmailMaintenanceRepository
    Inherits IRepository(Of EmailMaintenance)
End Interface
