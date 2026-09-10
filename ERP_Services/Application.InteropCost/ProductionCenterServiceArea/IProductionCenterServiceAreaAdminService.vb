'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IProductionCenterServiceAreaAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Function SaveProductionCenter(ByVal productionCenter As ProductionCenter, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ProductionCenter)

    ''' <summary>
    ''' Elimina un centro de produccion
    ''' </summary>
    Function DeleteProductionCenter(ByVal productionCenter As ProductionCenter, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un area de servicio por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetProductionCenterServiceAreaById(ByVal id As Integer) As ProductionCenterServiceArea

    ''' <summary>
    ''' Obtiene todos las areas de servicios relacionadas a un centro de produccion
    ''' </summary>
    ''' <param name="idProductionCenter">The identifier production center.</param>
    ''' <returns></returns>
    Function GetProductionCenterServiceAreaByProductionCenter(idProductionCenter As Integer) As List(Of ProductionCenterServiceArea)

End Interface
