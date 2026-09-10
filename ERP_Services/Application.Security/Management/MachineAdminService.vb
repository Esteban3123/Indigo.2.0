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
Imports Domain.Security
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class MachineAdminService
    Implements IMachineAdminService

#Region "Fields"

    ''' <summary>
    ''' Repositorio de maquinas
    ''' </summary>
    Private _machineRepository As IMachineRepository
    ''' <summary>
    ''' Repositorio de paquetes de actualización
    ''' </summary>
    Private _updatePackageRepository As IUpdatePackageRepository
    ''' <summary>
    ''' Repositorio de relaciones entre máquinas y paquetes
    ''' </summary>
    Private _machineUpdatePackageRepository As IMachineUpdatePackageRepository

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="im">Repositorio de máquinas</param>
    ''' <param name="up">Repositorio de paquetes de actualización</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal im As IMachineRepository, ByVal up As IUpdatePackageRepository, ByVal mup As IMachineUpdatePackageRepository)
        If im Is Nothing Then
            Throw New ArgumentNullException("im")
        End If
        If up Is Nothing Then
            Throw New ArgumentNullException("up")
        End If
        If mup Is Nothing Then
            Throw New ArgumentNullException("mup")
        End If
        Me._machineRepository = im
        Me._updatePackageRepository = up
        Me._machineUpdatePackageRepository = mup
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lista todas las máquinas registradas en la base de seguridad
    ''' </summary>
    ''' <returns>Lista de máquinas</returns>
    Public Function ListAllMachines() As List(Of Machines) Implements IMachineAdminService.ListAllMachines
        Try
            Return Me._machineRepository.ListAllMachines()
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Machines)()
        End Try
    End Function

    ''' <summary>
    ''' Graba o actualiza una máquina
    ''' </summary>
    ''' <param name="m">Máquina a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveMachine(m As Machines) As ActionResult(Of Machines) Implements IMachineAdminService.SaveMachine
        If m Is Nothing Then
            Throw New ArgumentNullException("m")
        End If
        Dim unitOfWork = Me._machineRepository.UnitWork
        Try
            Dim res = New ActionResult(Of Machines)()
            Dim ma As Machines = Me._machineRepository.GetMachine(m.UID)
            If ma.Id <= 0 Then
                m.RegDate = DateTime.Now
                Me._machineRepository.SaveEntity(m)
                res.StateResult = True
                res.ObjectEmbbeded = m
                res.Message = "I" 'Nuevo
            Else
                ma.Architecture = m.Architecture
                ma.IPs = m.IPs
                ma.LastUpDate = m.LastUpDate
                ma.MACs = m.MACs
                ma.Name = m.Name
                ma.ClientVersion = m.ClientVersion
                ma.OSName = m.OSName
                ma.MarkAsModified()
                Me._machineRepository.SaveEntity(ma)
                res.StateResult = True
                res.ObjectEmbbeded = ma
                res.Message = "U" 'Actualización
            End If
            unitOfWork.Commit()
            Return res
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Machines)() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Cambia el alias de la máquina
    ''' </summary>
    ''' <param name="uid">Identificación unica de la máquina</param>
    ''' <param name="newNickName">Nuevo alias de la máquina</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function ChangeNickName(uid As String, newNickName As String) As ActionResult Implements IMachineAdminService.ChangeNickName
        If uid Is Nothing Then
            Throw New ArgumentNullException("uid")
        End If
        If newNickName Is Nothing Then
            Throw New ArgumentNullException("newNickName")
        End If
        Dim unitOfWork = Me._machineRepository.UnitWork
        Try
            Dim ma As Machines = Me._machineRepository.GetMachine(uid)
            If ma.Id > 0 Then
                ma.NickName = newNickName
                ma.MarkAsModified()
                Me._machineRepository.SaveEntity(ma)
                unitOfWork.Commit()
            End If
            Return New ActionResult() With {.StateResult = True}
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Lista todos los paquetes de actualización donde su versión
    ''' es mayor o igual a la especificada
    ''' </summary>
    ''' <param name="version">Versión a comparar</param>
    ''' <returns>Lista de paquetes</returns>
    Public Function ListUpdatePackagesGreaterOrEqualsThanVersion(version As Int32) As List(Of UpdatePackage) Implements IMachineAdminService.ListUpdatePackagesGreaterOrEqualsThanVersion
        Try
            Return Me._updatePackageRepository.ListUpdatePackagesGreaterOrEqualsThanVersion(version)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of UpdatePackage)()
        End Try
    End Function

    ''' <summary>
    ''' Lista máquinas por una lista de indentificaciones unicas
    ''' </summary>
    ''' <param name="list">Lista de identificaciones unicas de máquina</param>
    ''' <returns>Lista de máquinas</returns>
    Public Function ListMachinesByUId(list As List(Of String)) As List(Of Machines) Implements IMachineAdminService.ListMachinesByUId
        Try
            Return Me._machineRepository.ListMachinesByUId(list)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New List(Of Machines)()
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el paquete de actualizacion existe por su version
    ''' </summary>
    ''' <param name="version">Versión a verificar</param>
    ''' <returns>Valor que indica si existe</returns>
    Public Function ExistsUpdatePackage(ByVal version As Int32) As Boolean Implements IMachineAdminService.ExistsUpdatePackage
        Try
            Return Me._updatePackageRepository.ExistsUpdatePackage(version)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Obtiene un valor que indica si el paquete de actualizacion se puede registrar
    ''' porque es mayor a todos los que estan registrados
    ''' </summary>
    ''' <param name="version">Versión a verificar</param>
    ''' <returns>Valor que indica si se puede registrar</returns>
    Public Function CanRegister(ByVal version As Int32) As Boolean Implements IMachineAdminService.CanRegister
        Try
            Return Me._updatePackageRepository.CanRegister(version)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Crea la relación entre un paquete de actualización y una lista de máquinas
    ''' </summary>
    ''' <param name="idVersion">Id del paquete de actualización</param>
    ''' <param name="machines">Lista de identificaciones de las máquinas a relacionar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function UpgradeMachines(idVersion As Integer, machines As List(Of String)) As ActionResult Implements IMachineAdminService.UpgradeMachines
        If machines Is Nothing Then
            Throw New ArgumentNullException("machines")
        End If
        Dim unitOfWork = Me._updatePackageRepository.UnitWork
        Try
            Dim res = New ActionResult()
            Dim pack As UpdatePackage = Me._updatePackageRepository.GetById(idVersion)
            If pack IsNot Nothing AndAlso pack.Id > 0 Then
                Dim listMachines As List(Of Machines) = Me._machineRepository.ListMachinesByUId(machines)
                If listMachines IsNot Nothing AndAlso listMachines.Count > 0 Then
                    If pack.MachineUpdatePackage Is Nothing OrElse pack.MachineUpdatePackage.Count = 0 Then
                        pack.MachineUpdatePackage = New TrackableCollection(Of MachineUpdatePackage)()
                        For Each m As Machines In listMachines
                            pack.MachineUpdatePackage.Add(New MachineUpdatePackage())
                            pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).Machines = m
                            pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).UpdatePackage = pack
                            pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).Upgraded = False
                            pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).UpgradeDate = DateTime.Now
                        Next
                    Else
                        For Each m As Machines In listMachines
                            If Not pack.MachineUpdatePackage.Any(Function(ma) ma.IdMachine = m.Id) Then
                                pack.MachineUpdatePackage.Add(New MachineUpdatePackage())
                                pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).Machines = m
                                pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).UpdatePackage = pack
                                pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).Upgraded = False
                                pack.MachineUpdatePackage(pack.MachineUpdatePackage.Count - 1).UpgradeDate = DateTime.Now
                            End If
                        Next
                    End If
                    pack.MarkAsModified()
                    Me._updatePackageRepository.SaveEntity(pack)
                    unitOfWork.Commit()
                    Return New ActionResult() With {.StateResult = True}
                Else
                    Return New ActionResult() With {.StateResult = False, .Message = "{ERR2}"} 'Las máquinas en la lista no existen
                End If
            Else
                Return New ActionResult() With {.StateResult = False, .Message = "{ERR1}"} 'El paquete no existe
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Crea un paquete de actualización
    ''' </summary>
    ''' <param name="numberVersion">Numero de la version</param>
    ''' <param name="version">Versión del paquete de instalación</param>
    ''' <param name="buildDate">Fecha de compilación</param>
    ''' <param name="description">Descripción del paquete</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function CreateUpdatePackage(ByVal numberVersion As Int32, ByVal version As String, ByVal buildDate As DateTime, ByVal description As String) As ActionResult Implements IMachineAdminService.CreateUpdatePackage
        If version Is Nothing Then
            Throw New ArgumentNullException("version")
        End If
        If description Is Nothing Then
            Throw New ArgumentNullException("description")
        End If
        Dim unitOfWork = Me._updatePackageRepository.UnitWork
        Try
            Dim res = New ActionResult()
            Dim ma As New UpdatePackage()
            ma.BuildDate = buildDate
            ma.CreationDate = DateTime.Now
            ma.Description = description
            ma.PackageVersion = version
            ma.NumberVersion = numberVersion
            ma.PackageFileName = (version & ".update")
            ma.Status = True
            Me._updatePackageRepository.SaveEntity(ma)
            unitOfWork.Commit()
            res.StateResult = True
            Return res
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult() With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

    ''' <summary>
    ''' Asigna fecha y estado de paquete actualizado en la máquina
    ''' </summary>
    ''' <param name="uidMachine">Identificacion unica de la maquina</param>
    ''' <param name="idVersionPackage">Id del registro del paquete</param>
    Public Sub SetDateOnUpdate(uidMachine As String, idVersionPackage As Integer) Implements IMachineAdminService.SetDateOnUpdate
        If uidMachine Is Nothing Then
            Throw New ArgumentNullException("uidMachine")
        End If
        Try
            Dim unitOfWork = Me._machineUpdatePackageRepository.UnitWork
            Dim m = Me._machineRepository.GetMachine(uidMachine)
            If m IsNot Nothing AndAlso m.Id > 0 Then
                Dim up = Me._machineUpdatePackageRepository.GetByIdMachineAndPackage(m.Id, idVersionPackage)
                If up IsNot Nothing AndAlso up.Id > 0 Then
                    up.UpgradedDate = DateTime.Now
                    up.Upgraded = True
                    up.MarkAsModified()
                    Me._machineUpdatePackageRepository.SaveEntity(up)
                    unitOfWork.Commit()
                End If
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
        End Try
    End Sub

    ''' <summary>
    ''' Obtiene un paquete por su id de registro
    ''' </summary>
    ''' <param name="id">Id del registro del paquete</param>
    ''' <returns>Páquete a consular</returns>
    Public Function GetById(id As Integer) As UpdatePackage Implements IMachineAdminService.GetById
        Try
            Return Me._updatePackageRepository.GetById(id)
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New UpdatePackage()
        End Try
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If
            _machineRepository = Nothing
            _updatePackageRepository = Nothing
            _machineUpdatePackageRepository = Nothing
            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
