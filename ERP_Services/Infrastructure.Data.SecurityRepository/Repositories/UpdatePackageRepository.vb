'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Juan F. Tamayo
' Created          : 2014-08-19
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2014-08-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Security.Entities
Imports Domain.Security
Imports System.Globalization
Imports System.Data.Entity

#End Region

Public Class UpdatePackageRepository
    Inherits GenericRepository(Of UpdatePackage)
    Implements IUpdatePackageRepository

    ''' <summary>
    ''' Esta variable  contiene el contexto de nuestro modelo.
    ''' </summary>
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia <see cref="UpdatePackageRepository" /> class.	
    ''' </summary>
    ''' <param name="contex">nThe contex.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Lista todos los paquetes de actualización donde su versión
    ''' es mayor o igual a la especificada
    ''' </summary>
    ''' <param name="version">Versión a comparar</param>
    ''' <returns>Lista de paquetes</returns>
    Public Function ListUpdatePackagesGreaterOrEqualsThanVersion(version As Int32) As List(Of UpdatePackage) Implements IUpdatePackageRepository.ListUpdatePackagesGreaterOrEqualsThanVersion
        Dim packages = (From m As UpdatePackage In Me._context.UpdatePackage.AsNoTracking() Where (m.Status) AndAlso (version <= m.NumberVersion) Select m).ToList()
        If packages IsNot Nothing AndAlso packages.Count > 0 Then
            Return packages
        Else
            Return New List(Of UpdatePackage)()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el paquete de actualizacion existe por su version
    ''' </summary>
    ''' <param name="version">Versión a verificar</param>
    ''' <returns>Valor que indica si existe</returns>
    Public Function ExistsUpdatePackage(version As Int32) As Boolean Implements IUpdatePackageRepository.ExistsUpdatePackage
        Dim packages = (From m As UpdatePackage In Me._context.UpdatePackage.AsNoTracking() Where (m.Status) AndAlso (version = m.NumberVersion) Select m).ToList()
        If packages IsNot Nothing AndAlso packages.Count > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el paquete de actualizacion se puede registrar
    ''' porque es mayor a todos los que estan registrados
    ''' </summary>
    ''' <param name="version">Versión a verificar</param>
    ''' <returns>Valor que indica si se puede registrar</returns>
    Public Function CanRegister(ByVal version As Int32) As Boolean Implements IUpdatePackageRepository.CanRegister
        Dim packages = (From m As UpdatePackage In Me._context.UpdatePackage.AsNoTracking() Where (m.Status) AndAlso (m.NumberVersion >= version) Select m).ToList()
        If packages IsNot Nothing AndAlso packages.Count > 0 Then
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Obtiene un paquete por su id de registro
    ''' </summary>
    ''' <param name="id">Id del registro del paquete</param>
    ''' <returns>Páquete a consular</returns>
    Public Function GetById(id As Integer) As UpdatePackage Implements IUpdatePackageRepository.GetById
        Dim packages = (From m As UpdatePackage In Me._context.UpdatePackage.AsNoTracking().Include("MachineUpdatePackage.Machines") Where (m.Status) AndAlso (m.Id = id) Select m).ToList()
        If packages IsNot Nothing AndAlso packages.Count > 0 Then
            Return packages(0)
        Else
            Return New UpdatePackage()
        End If
    End Function

End Class
