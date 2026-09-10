'************************************************************
' Assembly         : Domain.Entities.Service
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Maintenance.Entities
Imports Domain.Base
Imports Domain.Entities
#End Region

''' <summary>
''' Contrato de repositorio para la entidad secuencia numerica cabecera y detalle
''' </summary>
Public Interface IMaintenanceSequenceDetailRepository
    Inherits IRepository(Of MaintenanceSequenceDetail)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un detalle de secuencia numerica por su id
    ''' </summary>
    ''' <param name="id">Id del detalle</param>
    ''' <returns>Detalle de la secuencia numerica</returns>
    Function GetSequenseDById(ByVal id As Int32) As MaintenanceSequenceDetail
    Function GetSequenseDetailUpdatedById(idSequence As Int32) As MaintenanceSequenceDetail

#End Region

End Interface