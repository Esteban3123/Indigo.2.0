'***********************************************************************
' Assembly         : Application.Security
' Author           : Juan F. Tamayo
' Created          : 2014-08-19
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2014-08-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Security.Entities

#End Region

Public Interface IMachineAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los paquetes de actualización donde su versión
    ''' es mayor o igual a la especificada
    ''' </summary>
    ''' <param name="version">Versión a comparar</param>
    ''' <returns>Lista de paquetes</returns>
    Function ListUpdatePackagesGreaterOrEqualsThanVersion(version As Int32) As List(Of UpdatePackage)

    ''' <summary>
    ''' Lista todas las máquinas registradas en la base de seguridad
    ''' </summary>
    ''' <returns>Lista de máquinas</returns>
    Function ListAllMachines() As List(Of Machines)

    ''' <summary>
    ''' Lista máquinas por una lista de indentificaciones unicas
    ''' </summary>
    ''' <param name="list">Lista de identificaciones unicas de máquina</param>
    ''' <returns>Lista de máquinas</returns>
    Function ListMachinesByUId(list As List(Of String)) As List(Of Machines)

    ''' <summary>
    ''' Graba o actualiza una máquina
    ''' </summary>
    ''' <param name="m">Máquina a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    Function SaveMachine(ByVal m As Machines) As ActionResult(Of Machines)

    ''' <summary>
    ''' Cambia el alias de la máquina
    ''' </summary>
    ''' <param name="uid">Identificación unica de la máquina</param>
    ''' <param name="newNickName">Nuevo alias de la máquina</param>
    ''' <returns>Resultado de la acción</returns>
    Function ChangeNickName(ByVal uid As String, ByVal newNickName As String) As ActionResult

    ''' <summary>
    ''' Crea un paquete de actualización
    ''' </summary>
    ''' <param name="numberVersion">Numero de la version</param>
    ''' <param name="version">Versión del paquete de instalación</param>
    ''' <param name="buildDate">Fecha de compilación</param>
    ''' <param name="description">Descripción del paquete</param>
    ''' <returns>Resultado de la acción</returns>
    Function CreateUpdatePackage(ByVal numberVersion As Int32, ByVal version As String, ByVal buildDate As DateTime, ByVal description As String) As ActionResult

    ''' <summary>
    ''' Asigna fecha y estado de paquete actualizado en la máquina
    ''' </summary>
    ''' <param name="uidMachine">Identificacion unica de la maquina</param>
    ''' <param name="idVersionPackage">Id del registro del paquete</param>
    Sub SetDateOnUpdate(ByVal uidMachine As String, ByVal idVersionPackage As Int32)

    ''' <summary>
    ''' Crea la relación entre un paquete de actualización y una lista de máquinas
    ''' </summary>
    ''' <param name="idVersion">Id del paquete de actualización</param>
    ''' <param name="machines">Lista de identificaciones de las máquinas a relacionar</param>
    ''' <returns>Resultado de la acción</returns>
    Function UpgradeMachines(ByVal idVersion As Int32, ByVal machines As List(Of String)) As ActionResult

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
    ''' Obtiene un paquete por su id de registro
    ''' </summary>
    ''' <param name="id">Id del registro del paquete</param>
    ''' <returns>Páquete a consular</returns>
    Function GetById(id As Integer) As UpdatePackage

End Interface
