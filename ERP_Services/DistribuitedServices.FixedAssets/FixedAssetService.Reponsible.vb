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
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteResponsible(Responsible As Domain.Entities.FixedAssetResponsible, audit As AuditMessage) As ActionResult Implements IFixedAssetResponsibleService.DeleteResponsible
        Using service As IFixedAssetResponsibleAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleAdminService)()
            Return service.DeleteResponsible(Responsible, audit)
        End Using
        'Return _fixedAssetResponsibleAdminService.DeleteResponsible(Responsible, audit)
    End Function


    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Public Function GetResponsibleByCode(Code As String) As Domain.Entities.FixedAssetResponsible Implements IFixedAssetResponsibleService.GetResponsibleByCode
        Using service As IFixedAssetResponsibleAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleAdminService)()
            Return service.GetResponsibleByCode(Code)
        End Using
        'Return _fixedAssetResponsibleAdminService.GetResponsibleByCode(Code)
    End Function

    Public Function GetFunctionalUnitByCode(Code As String) As FunctionalUnit Implements IFixedAssetResponsibleService.GetFunctionalUnitByCode
        Using service As IFixedAssetResponsibleAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleAdminService)()
            Return service.GetFunctionalUnitByCode(Code)
        End Using
        'Return _fixedAssetResponsibleAdminService.GetFunctionalUnitByCode(Code)
    End Function

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Public Function ListAllResponsible(audit As AuditMessage) As List(Of Domain.Entities.FixedAssetResponsible) Implements IFixedAssetResponsibleService.ListAllResponsible
        Using service As IFixedAssetResponsibleAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleAdminService)()
            Return service.ListAllResponsible()
        End Using
        'Return _fixedAssetResponsibleAdminService.ListAllResponsible()
    End Function

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ResponsibleType">Objeto ResponsibleType</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveResponsible(Responsible As Domain.Entities.FixedAssetResponsible, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetResponsible) Implements IFixedAssetResponsibleService.SaveResponsible
        Using service As IFixedAssetResponsibleAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleAdminService)()
            Return service.SaveResponsible(Responsible, audit, idSequense)
        End Using
        'Return _fixedAssetResponsibleAdminService.SaveResponsible(Responsible, audit, idSequense)
    End Function

    Public Function GetFuncionalUnitByIdUser(IdResponsible As Integer) As List(Of ResponsibleFunctionalUnit) Implements IFixedAssetResponsibleService.GetFuncionalUnitByIdUser
        Using service As IFixedAssetResponsibleAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleAdminService)()
            Return service.GetFuncionalUnitByIdUser(IdResponsible)
        End Using
        'Return _fixedAssetResponsibleAdminService.GetFuncionalUnitByIdUser(IdResponsible)
    End Function

    Public Function ChangeStateFixedAssetResponsible(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetResponsible) Implements IFixedAssetResponsibleService.ChangeStateFixedAssetResponsible
        Using service As IFixedAssetResponsibleAdminService = Container.Current.Resolve(Of IFixedAssetResponsibleAdminService)()
            Return service.ChangeStateFixedAssetResponsible(Code, State, audit)
        End Using
        'Return _fixedAssetResponsibleAdminService.ChangeStateFixedAssetResponsible(Code, State, audit)
    End Function
End Class
