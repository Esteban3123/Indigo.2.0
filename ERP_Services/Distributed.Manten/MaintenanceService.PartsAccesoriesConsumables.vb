'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

Partial Class MaintanceService
    Implements IPartsAccesoriesConsumablesService

    ''' <summary>
    ''' Función para Eliminar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As Domain.Maintenance.Entities.PartsAccesoriesConsumables, session As Infrastructure.CrossCutting.Base.SessionValues) As Boolean Implements IPartsAccesoriesConsumablesService.DeletePartsAccesoriesConsumables
        Using PartsAccesoriesConsumablesAdmin As IPartsAccesoriesConsumablesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartsAccesoriesConsumablesAdminService)()
            Return PartsAccesoriesConsumablesAdmin.DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function GetPartsAccesoriesConsumablesByCode(Code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Maintenance.Entities.PartsAccesoriesConsumables Implements IPartsAccesoriesConsumablesService.GetPartsAccesoriesConsumablesByCode
        Using PartsAccesoriesConsumablesAdmin As IPartsAccesoriesConsumablesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartsAccesoriesConsumablesAdminService)()
            Return PartsAccesoriesConsumablesAdmin.GetPartsAccesoriesConsumablesByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Public Function ListAllPartsAccesoriesConsumables(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Maintenance.Entities.PartsAccesoriesConsumables) Implements IPartsAccesoriesConsumablesService.ListAllPartsAccesoriesConsumables
        Using PartsAccesoriesConsumablesAdmin As IPartsAccesoriesConsumablesAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IPartsAccesoriesConsumablesAdminService)()
            Return PartsAccesoriesConsumablesAdmin.ListAllPartsAccesoriesConsumables()
        End Using
    End Function


    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SavePartsAccesoriesConsumables(Empresa As String, PartsAccesoriesConsumables As Domain.Maintenance.Entities.PartsAccesoriesConsumables, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of Domain.Maintenance.Entities.PartsAccesoriesConsumables) Implements IPartsAccesoriesConsumablesService.SavePartsAccesoriesConsumables
        Using PartsAccesoriesConsumablesAdmin As IPartsAccesoriesConsumablesAdminService = IocFactory.Instance(Empresa).CurrentContainer.Resolve(Of IPartsAccesoriesConsumablesAdminService)()
            Dim idSequense As Int64 = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Int64)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Return PartsAccesoriesConsumablesAdmin.SavePartsAccesoriesConsumables(PartsAccesoriesConsumables, audit, idSequense)
        End Using
    End Function
End Class
