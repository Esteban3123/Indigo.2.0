'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 09-06-2016
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
    Public Function DeleteFixedAssetStatus(FixedAssetStatusAsset As Domain.Entities.FixedAssetStatusAsset, audit As AuditMessage) As ActionResult Implements IFixedAssetStatusService.DeleteFixedAssetStatusAsset
        Using service As IFixedAssetStatusAdminService = Container.Current.Resolve(Of IFixedAssetStatusAdminService)()
            Return service.DeleteFixedAssetStatus(FixedAssetStatusAsset, audit)
        End Using
        'Return _fixedAssetStatusAdminService.DeleteFixedAssetStatus(FixedAssetStatusAsset, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetStatusAssetByCode(Code As String) As Domain.Entities.FixedAssetStatusAsset Implements IFixedAssetStatusService.GetFixedAssetStatusAssetByCode
        Using service As IFixedAssetStatusAdminService = Container.Current.Resolve(Of IFixedAssetStatusAdminService)()
            Return service.GetFixedAssetStatusAsset(Code)
        End Using
        'Return _fixedAssetStatusAdminService.GetFixedAssetStatusAsset(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllFixedAssetStatusAsset(audit As AuditMessage) As List(Of Domain.Entities.FixedAssetStatusAsset) Implements IFixedAssetStatusService.ListAllFixedAssetStatusAsset
        Using service As IFixedAssetStatusAdminService = Container.Current.Resolve(Of IFixedAssetStatusAdminService)()
            Return service.ListAllFixedAssetStatus()
        End Using
        'Return _fixedAssetStatusAdminService.ListAllFixedAssetStatus()
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetStatusAsset(FixedAssetStatusAsset As Domain.Entities.FixedAssetStatusAsset, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetStatusAsset) Implements IFixedAssetStatusService.SaveFixedAssetStatusAsset
        Using service As IFixedAssetStatusAdminService = Container.Current.Resolve(Of IFixedAssetStatusAdminService)()
            Return service.SaveFixedAssetStatusAsset(FixedAssetStatusAsset, audit, idSequense)
        End Using
        'Return _fixedAssetStatusAdminService.SaveFixedAssetStatusAsset(FixedAssetStatusAsset, audit, idSequense)
    End Function

    Public Function ChangeFixedAssetStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetStatusAsset) Implements IFixedAssetStatusService.ChangeFixedAssetStatus
        Using service As IFixedAssetStatusAdminService = Container.Current.Resolve(Of IFixedAssetStatusAdminService)()
            Return service.FixedAssetStatusChangeState(Code, State, audit)
        End Using
        'Return _fixedAssetStatusAdminService.FixedAssetStatusChangeState(Code, State, audit)
    End Function

End Class
