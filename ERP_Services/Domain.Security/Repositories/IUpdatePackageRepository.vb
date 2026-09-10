'***********************************************************************
' Assembly         : Domain.Security
' Author           : Juan F. Tamayo
' Created          : 2014-08-19
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2014-08-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Security.Entities
Imports Domain.Base

#End Region

Public Interface IUpdatePackageRepository
    Inherits IRepository(Of UpdatePackage)

#Region "Methods"

    ''' <summary>
    ''' Obtiene un paquete por su id de registro
    ''' </summary>
    ''' <param name="id">Id del registro del paquete</param>
    ''' <returns>Páquete a consular</returns>
    Function GetById(ByVal id As Int32) As UpdatePackage

    ''' <summary>
    ''' Obtiene un valor que indica si el paquete de actualizacion existe por su version
    ''' </summary>
    ''' <param name="version">Versión a verificar</param>
    ''' <returns>Valor que indica si existe</returns>
    Function ExistsUpdatePackage(ByVal version As Int32) As Boolean

    ''' <summary>
    ''' Obtiene un valor que indica si el paquete de actualizacion se puede registrar
    ''' porque es mayor a todos los que estan registrados
    ''' </summary>
    ''' <param name="version">Versión a verificar</param>
    ''' <returns>Valor que indica si se puede registrar</returns>
    Function CanRegister(ByVal version As Int32) As Boolean

    ''' <summary>
    ''' Lista todos los paquetes de actualización donde su versión
    ''' es mayor o igual a la especificada
    ''' </summary>
    ''' <param name="version">Versión a comparar</param>
    ''' <returns>Lista de paquetes</returns>
    Function ListUpdatePackagesGreaterOrEqualsThanVersion(ByVal version As Int32) As List(Of UpdatePackage)

#End Region

End Interface
