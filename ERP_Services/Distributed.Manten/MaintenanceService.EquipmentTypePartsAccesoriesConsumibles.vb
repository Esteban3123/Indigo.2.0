'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 25-03-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Partial Class MaintanceService
    Implements IEquipmentTypePartsAccesoriesConsumablesService

    ''' <summary>
    ''' Función para Almacenar los Tipo de Equipo x Partes, Accesorios y Consumibles
    ''' </summary>
    ''' <param name="ListEquipmentTypePartsAccesoriesConsumablesService"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEquipmentTypePartsAccesoriesConsumablesService(ListEquipmentTypePartsAccesoriesConsumablesService As List(Of Domain.Maintenance.Entities.EquipmentTypePartsAccesoriesConsumibles), session As SessionValues) As Boolean Implements IEquipmentTypePartsAccesoriesConsumablesService.DeleteEquipmentTypePartsAccesoriesConsumablesService
        Using EquipmentTypePartsAccesoriesConsumablesAdmin As IEquipmentTypePartsAccesoriesConsumablesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEquipmentTypePartsAccesoriesConsumablesAdminService)()
            Return EquipmentTypePartsAccesoriesConsumablesAdmin.DeleteEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumablesService, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar los Tipo de Equipo x Partes, Accesorios y Consumibles
    ''' </summary>
    ''' <param name="ListEquipmentTypePartsAccesoriesConsumablesService">ListEquipmentTypePartsAccesoriesConsumablesService</param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentTypePartsAccesoriesConsumablesService(ListEquipmentTypePartsAccesoriesConsumablesService As List(Of Domain.Maintenance.Entities.EquipmentTypePartsAccesoriesConsumibles), session As SessionValues) As Boolean Implements IEquipmentTypePartsAccesoriesConsumablesService.SaveEquipmentTypePartsAccesoriesConsumablesService
        Using EquipmentTypePartsAccesoriesConsumablesAdmin As IEquipmentTypePartsAccesoriesConsumablesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEquipmentTypePartsAccesoriesConsumablesAdminService)()
            Return EquipmentTypePartsAccesoriesConsumablesAdmin.SaveEquipmentTypePartsAccesoriesConsumables(ListEquipmentTypePartsAccesoriesConsumablesService, session.AuditMessageWcf)
        End Using
    End Function
End Class
