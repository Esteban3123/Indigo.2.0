'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 25/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Application.FixedAsset
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class FixedAssetService

    ''' <summary>
    ''' Elimina una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteFixedAssetLocation(Empresa As String, FixedAssetLocation As Domain.Entities.FixedAssetLocation, ByVal audit As AuditMessage) As Boolean Implements IFixedAssetFixedAssetLocation.DeleteLocation
        Using service As IFixedAssetLocationAdminService = Container.Current.Resolve(Of IFixedAssetLocationAdminService)()
            Return service.DeleteLocation(FixedAssetLocation, audit)
        End Using
        'Return Me._fixedAssetLocationAdminService.DeleteLocation(FixedAssetLocation, audit)
    End Function

    ''' <summary>
    ''' Obtiene una Ubicación
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFixedAssetLocation(Empresa As String, ByVal codeLocation As String, audit As AuditMessage) As Domain.Entities.FixedAssetLocation Implements IFixedAssetFixedAssetLocation.GetLocation
        Using service As IFixedAssetLocationAdminService = Container.Current.Resolve(Of IFixedAssetLocationAdminService)()
            Return service.GetLocation(codeLocation)
        End Using
        'Return Me._fixedAssetLocationAdminService.GetLocation(codeLocation)
    End Function

    ''' <summary>
    ''' Guarda o actualiza una Ubicación
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveFixedAssetLocation(Empresa As String, ByVal Location As List(Of FixedAssetLocation), ByVal audit As AuditMessage, idSequense As Int64) As Boolean Implements IFixedAssetFixedAssetLocation.SaveLocation
        Using service As IFixedAssetLocationAdminService = Container.Current.Resolve(Of IFixedAssetLocationAdminService)()
            Return service.SaveLocation(Location, audit)
        End Using
        'Return Me._fixedAssetLocationAdminService.SaveLocation(Location, audit)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllLocation(Empresa As String, audit As AuditMessage) As List(Of FixedAssetLocation) Implements IFixedAssetFixedAssetLocation.ListAllLocation
        Using service As IFixedAssetLocationAdminService = Container.Current.Resolve(Of IFixedAssetLocationAdminService)()
            Return service.ListAllLocation()
        End Using
        'Return Me._fixedAssetLocationAdminService.ListAllLocation()
    End Function


    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListLocation(Empresa As String, audit As AuditMessage) As List(Of FixedAssetLocation) Implements IFixedAssetFixedAssetLocation.ListLocation
        Using service As IFixedAssetLocationAdminService = Container.Current.Resolve(Of IFixedAssetLocationAdminService)()
            Return service.ListLocation()
        End Using
        'Return Me._fixedAssetLocationAdminService.ListLocation()
    End Function



End Class
