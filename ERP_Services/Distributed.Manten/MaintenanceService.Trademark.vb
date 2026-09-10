'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.IOC
Imports Application.Maintenance
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports DistributedServices.Maintenance
Imports Domain.Maintenance.Entities

Partial Class MaintanceService
    Implements ITrademarkService

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteTrademark(Trademark As Domain.Maintenance.Entities.Trademark, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult Implements ITrademarkService.DeleteTrademark
        Using TrademarkAdmin As ITrademarkAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITrademarkAdminService)()
            Return TrademarkAdmin.DeleteTrademark(Trademark, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetTrademarkByCode(Code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Maintenance.Entities.Trademark Implements ITrademarkService.GetTrademarkByCode
        Using TrademarkAdmin As ITrademarkAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITrademarkAdminService)()
            Return TrademarkAdmin.GetTrademarkByCode(Code)
        End Using
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllTrademark(session As Infrastructure.CrossCutting.Base.SessionValues) As List(Of Domain.Maintenance.Entities.Trademark) Implements ITrademarkService.ListAllTrademark
        Using TrademarkAdmin As ITrademarkAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITrademarkAdminService)()
            Return TrademarkAdmin.ListAllTrademark()
        End Using
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveTrademark(Trademark As Domain.Maintenance.Entities.Trademark, session As Infrastructure.CrossCutting.Base.SessionValues, Optional idSequence As Long = 0) As ActionResult(Of Domain.Maintenance.Entities.Trademark) Implements ITrademarkService.SaveTrademark
        Using TrademarkAdmin As ITrademarkAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITrademarkAdminService)()
            'ServerSessionValues.Current.CurrentContainer = session.TransactionalContainer
            Return TrademarkAdmin.SaveTrademark(Trademark, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    Public Function UpdateStateTrademark(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Trademark) Implements ITrademarkService.UpdateStateTrademark
        Using TrademarkAdmin As ITrademarkAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ITrademarkAdminService)()
            'ServerSessionValues.Current.CurrentContainer = session.TransactionalContainer
            Return TrademarkAdmin.UpdateStateTrademark(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
