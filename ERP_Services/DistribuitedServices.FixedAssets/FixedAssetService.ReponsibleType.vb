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
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteResponsibleType(ResponsibleType As Domain.Entities.ResponsibleType, audit As AuditMessage) As ActionResult Implements IFixedAssetResponsibleTypeService.DeleteResponsibleType
        Using service As IFixedAssetResponsibleTypeAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleTypeAdminService)()
            Return service.DeleteResponsibleType(ResponsibleType, audit)
        End Using
        'Return _fixedAssetResponsibleTypeAdminService.DeleteResponsibleType(ResponsibleType, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetResponsibleTypeByCode(Code As String) As Domain.Entities.ResponsibleType Implements IFixedAssetResponsibleTypeService.GetResponsibleTypeByCode
        Using service As IFixedAssetResponsibleTypeAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleTypeAdminService)()
            Return service.GetResponsibleTypeByCode(Code)
        End Using
        'Return _fixedAssetResponsibleTypeAdminService.GetResponsibleTypeByCode(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllResponsibleType(audit As AuditMessage) As List(Of Domain.Entities.ResponsibleType) Implements IFixedAssetResponsibleTypeService.ListAllResponsibleType
        Using service As IFixedAssetResponsibleTypeAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleTypeAdminService)()
            Return service.ListAllResponsibleType()
        End Using
        'Return _fixedAssetResponsibleTypeAdminService.ListAllResponsibleType()
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ResponsibleType">Objeto ResponsibleType</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveResponsibleType(ResponsibleType As Domain.Entities.ResponsibleType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ResponsibleType) Implements IFixedAssetResponsibleTypeService.SaveResponsibleType
        Using service As IFixedAssetResponsibleTypeAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleTypeAdminService)()
            Return service.SaveResponsibleType(ResponsibleType, audit, idSequense)
        End Using
        'Return _fixedAssetResponsibleTypeAdminService.SaveResponsibleType(ResponsibleType, audit, idSequense)
    End Function


    Public Function ChangeFixedAssetResponsibleTypeStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of ResponsibleType) Implements IFixedAssetResponsibleTypeService.ChangeFixedAssetResponsibleTypeStatus
        Using service As IFixedAssetResponsibleTypeAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleTypeAdminService)()
            Return service.ChangeFixedAssetResponsibleTypeStatus(Code, State, audit)
        End Using
        'Return _fixedAssetResponsibleTypeAdminService.ChangeFixedAssetResponsibleTypeStatus(Code, State, audit)
    End Function

End Class
