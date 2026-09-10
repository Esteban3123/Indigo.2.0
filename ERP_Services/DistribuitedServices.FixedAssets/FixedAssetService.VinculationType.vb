'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-02-2016
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
    ''' <param name="VinculationType">Objeto VinculationType</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteVinculationType(VinculationType As Domain.Entities.FixedAssetVinculationType, audit As AuditMessage) As ActionResult Implements IFixedAssetVinculationTypeService.DeleteVinculationType
        Using service As IFixedAssetVinculationTypeAdminService = Container.Current.Resolve(Of IFixedAssetVinculationTypeAdminService)()
            Return service.DeleteVinculationType(VinculationType, audit)
        End Using
        'Return _fixedAssetVinculationTypeAdminService.DeleteVinculationType(VinculationType, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetVinculationTypeByCode(Code As String) As Domain.Entities.FixedAssetVinculationType Implements IFixedAssetVinculationTypeService.GetVinculationTypeByCode
        Using service As IFixedAssetVinculationTypeAdminService = Container.Current.Resolve(Of IFixedAssetVinculationTypeAdminService)()
            Return service.GetVinculationTypeByCode(Code)
        End Using
        'Return _fixedAssetVinculationTypeAdminService.GetVinculationTypeByCode(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllVinculationType(audit As AuditMessage) As List(Of Domain.Entities.FixedAssetVinculationType) Implements IFixedAssetVinculationTypeService.ListAllVinculationType
        Using service As IFixedAssetVinculationTypeAdminService = Container.Current.Resolve(Of IFixedAssetVinculationTypeAdminService)()
            Return service.ListAllVinculationType()
        End Using
        'Return _fixedAssetVinculationTypeAdminService.ListAllVinculationType()
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="VinculationType">Objeto VinculationType</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveVinculationType(VinculationType As Domain.Entities.FixedAssetVinculationType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetVinculationType) Implements IFixedAssetVinculationTypeService.SaveVinculationType
        Using service As IFixedAssetVinculationTypeAdminService = Container.Current.Resolve(Of IFixedAssetVinculationTypeAdminService)()
            Return service.SaveVinculationType(VinculationType, audit, idSequense)
        End Using
        'Return _fixedAssetVinculationTypeAdminService.SaveVinculationType(VinculationType, audit, idSequense)
    End Function

    Public Function ChangeFixedAssetVinculationTypeState(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetVinculationType) Implements IFixedAssetVinculationTypeService.ChangeFixedAssetVinculationTypeState
        Using service As IFixedAssetVinculationTypeAdminService = Container.Current.Resolve(Of IFixedAssetVinculationTypeAdminService)()
            Return service.ChangeFixedAssetVinculationTypeState(Code, State, audit)
        End Using
        'Return _fixedAssetVinculationTypeAdminService.ChangeFixedAssetVinculationTypeState(Code, State, audit)
    End Function

End Class
