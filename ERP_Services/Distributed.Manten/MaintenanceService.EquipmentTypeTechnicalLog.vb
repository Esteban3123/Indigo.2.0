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

    Implements IEquipmentTypeTechnicalLogService

    ''' <summary>
    ''' Función para Almacenar los Registros Técnicos x Tipo de Equipo
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of Domain.Maintenance.Entities.EquipmentTypeTechnicalLog), session As SessionValues) As Boolean Implements IEquipmentTypeTechnicalLogService.SaveEquipmentTypeTechnicalLog
        Using EquipmentTypeTechnicalLogAdmin As IEquipmentTypeTechnicalLogAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEquipmentTypeTechnicalLogAdminService)()
            Return EquipmentTypeTechnicalLogAdmin.SaveEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Función para Eliminar los Registros Técnicos x Tipo de Equipo
    ''' </summary>
    ''' <param name="ListEquipmentTypeTechnicalLog"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog As List(Of Domain.Maintenance.Entities.EquipmentTypeTechnicalLog), session As SessionValues) As Boolean Implements IEquipmentTypeTechnicalLogService.DeleteEquipmentTypeTechnicalLog
        Using EquipmentTypeTechnicalLogAdmin As IEquipmentTypeTechnicalLogAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IEquipmentTypeTechnicalLogAdminService)()
            Return EquipmentTypeTechnicalLogAdmin.DeleteEquipmentTypeTechnicalLog(ListEquipmentTypeTechnicalLog, session.AuditMessageWcf)
        End Using
    End Function
End Class
