'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Diego Andres Roldan Lozano
' Created          : 2018-08-31
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IMaintenanceProtocolRepository
    Inherits IRepository(Of MaintenanceProtocol)
    ''' <summary>
    ''' obtiene un protocolo de mantenimiento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMaintenanceProtocolByCode(code As String) As MaintenanceProtocol
    ''' <summary>
    ''' obtiene un protocolo de mantenimiento por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMaintenanceProtocolById(id As Integer) As MaintenanceProtocol
End Interface