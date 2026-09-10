'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Yoe Andres Cardenas
' Created          : 06/06/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServicePackage
    ''' <summary>
    ''' Lista todos los paquetes
    ''' </summary>
    ''' <returns>Lista de paquetes</returns>
    <OperationContract()>
    Function ListAllPackage(audit As AuditMessage) As List(Of Package)

    ''' <summary>
    ''' Lista todos los paquetes duplicados
    ''' </summary>
    ''' <param name="packageId"></param>
    ''' <param name="packageDetailTmp"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function ListDuplicatePackage(ByVal packageId As Integer, ByVal packageDetailTmp As List(Of Tuple(Of Byte, Integer)), audit As AuditMessage) As List(Of PackageDto)

    ''' <summary>
    ''' Guarda un paquete
    ''' </summary>
    ''' <param name="package">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SavePackage(ByVal package As Package, idSequence As Int64, audit As AuditMessage) As ActionResult(Of Package)

    ''' <summary>
    ''' Elimina un paquete
    ''' </summary>
    ''' <param name="package">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function DeletePackage(ByVal package As Package, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza un paquete
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function UpdateStatePackage(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of Package)

    ''' <summary>
    ''' Obtiene un paquete por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetPackage(ByVal code As String, audit As AuditMessage) As ActionResult(Of Package)

    ''' <summary>
    ''' Obtiene un paquete por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    <OperationContract()>
    Function GetPackageById(id As Integer, audit As AuditMessage) As ActionResult(Of Package)

    ''' <summary>
    ''' Obtiene un paquete asociado a un proceso de producción
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductionPackage(ByVal code As String, audit As AuditMessage) As Boolean

End Interface
