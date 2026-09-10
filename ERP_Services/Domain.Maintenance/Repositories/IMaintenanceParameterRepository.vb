'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 05-03-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region

Public Interface IMaintenanceParameterRepository
    Inherits IRepository(Of MaintenanceParameter)

    ''' <summary>
    ''' Función que obtiene los Parámetros de Mantenimiento
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListMaintenanceParameter(Optional tracking As Boolean = True) As MaintenanceParameter

End Interface
