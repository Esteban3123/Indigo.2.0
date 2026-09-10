'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Yoe Andres Cardenas
' Created          : 06-06-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
#End Region

Partial Class MixingStationService
    Implements IMixingStationServicePackage

    ''' <summary>
    ''' Actualiza un paquete
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllPackage(audit As AuditMessage) As List(Of Package) Implements IMixingStationServicePackage.ListAllPackage
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.ListAllPackage(audit)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los paquetes duplicados
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <param name="packageDetailTmp"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListDuplicatePackage(packageId As Integer, packageDetailTmp As List(Of Tuple(Of Byte, Integer)), audit As AuditMessage) As List(Of PackageDto) Implements IMixingStationServicePackage.ListDuplicatePackage
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.ListDuplicatePackage(packageId, packageDetailTmp, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un paquete
    ''' </summary>
    ''' <param name="package"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SavePackage(package As Package, idSequence As Int64, audit As AuditMessage) As ActionResult(Of Package) Implements IMixingStationServicePackage.SavePackage
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.SavePackage(package, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un paquete
    ''' </summary>
    ''' <param name="package"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeletePackage(package As Package, audit As AuditMessage) As ActionResult Implements IMixingStationServicePackage.DeletePackage
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.DeletePackage(package, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de un paquete
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateStatePackage(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Package) Implements IMixingStationServicePackage.UpdateStatePackage
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.UpdateStatePackage(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPackage(code As String, audit As AuditMessage) As ActionResult(Of Package) Implements IMixingStationServicePackage.GetPackage
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.GetPackage(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetPackageById(id As Integer, audit As AuditMessage) As ActionResult(Of Package) Implements IMixingStationServicePackage.GetPackageById
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.GetPackageById(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un paquete asociado a un proceso de producción
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetProductionPackage(code As String, audit As AuditMessage) As Boolean Implements IMixingStationServicePackage.GetProductionPackage
        Using service As IPackageAdminService = Container.Current.Resolve(Of IPackageAdminService)()
            Return service.GetProductionPackage(code, audit)
        End Using
    End Function
End Class
