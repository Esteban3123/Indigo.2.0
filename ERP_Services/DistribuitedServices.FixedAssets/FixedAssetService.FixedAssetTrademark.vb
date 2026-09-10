'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Application.FixedAsset
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteTrademark(Trademark As Domain.Entities.FixedAssetTrademark, audit As AuditMessage) As ActionResult Implements IFixedAssetTrademarkService.DeleteTrademark
        Using service As IFixedAssetTrademarkAdminService = Container.Current.Resolve(Of IFixedAssetTrademarkAdminService)()
            Return service.DeleteTrademark(Trademark, audit)
        End Using
        'Return _fixedAssetTrademarkAdminService.DeleteTrademark(Trademark, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetTrademarkByCode(Code As String, session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.FixedAssetTrademark Implements IFixedAssetTrademarkService.GetTrademarkByCode
        Using service As IFixedAssetTrademarkAdminService = Container.Current.Resolve(Of IFixedAssetTrademarkAdminService)()
            Return service.GetTrademarkByCode(Code)
        End Using
        'Return _fixedAssetTrademarkAdminService.GetTrademarkByCode(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllTrademark(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.FixedAssetTrademark) Implements IFixedAssetTrademarkService.ListAllTrademark
        Using service As IFixedAssetTrademarkAdminService = Container.Current.Resolve(Of IFixedAssetTrademarkAdminService)()
            Return service.ListAllTrademark()
        End Using
        'Return _fixedAssetTrademarkAdminService.ListAllTrademark()
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveTrademark(Trademark As Domain.Entities.FixedAssetTrademark, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetTrademark) Implements IFixedAssetTrademarkService.SaveTrademark
        Using service As IFixedAssetTrademarkAdminService = Container.Current.Resolve(Of IFixedAssetTrademarkAdminService)()
            Return service.SaveTrademark(Trademark, audit, idSequense)
        End Using
        'Return _fixedAssetTrademarkAdminService.SaveTrademark(Trademark, audit, idSequense)
    End Function

    Public Function ChangeFixedAssetTrademarkStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetTrademark) Implements IFixedAssetTrademarkService.ChangeFixedAssetTrademarkStatus
        Using service As IFixedAssetTrademarkAdminService = Container.Current.Resolve(Of IFixedAssetTrademarkAdminService)()
            Return service.ChangeFixedAssetTrademarkStatus(Code, state, audit)
        End Using
        'Return _fixedAssetTrademarkAdminService.ChangeFixedAssetTrademarkStatus(Code, state, audit)
    End Function

End Class
