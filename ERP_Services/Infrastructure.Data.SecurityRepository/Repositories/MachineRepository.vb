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

Public Class MachineRepository
    Inherits GenericRepository(Of Machines)
    Implements IMachineRepository

    ''' <summary>
    ''' Esta variable  contiene el contexto de nuestro modelo.
    ''' </summary>
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia <see cref="MachineRepository" /> class.	
    ''' </summary>
    ''' <param name="contex">nThe contex.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Lista todas las máquinas registradas en la base de seguridad
    ''' </summary>
    ''' <returns>Lista de máquinas</returns>
    Public Function ListAllMachines() As List(Of Machines) Implements IMachineRepository.ListAllMachines
        Dim machines = (From m As Machines In Me._context.Machines.AsNoTracking() Select m).ToList()
        If machines IsNot Nothing AndAlso machines.Count > 0 Then
            Return machines
        Else
            Return New List(Of Machines)()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una máquina por su identificación unica
    ''' </summary>
    ''' <param name="uid">Identificación unica</param>
    ''' <returns>Maquina consultada</returns>
    Public Function GetMachine(uid As String) As Machines Implements IMachineRepository.GetMachine
        Dim machines = (From m As Machines In Me._context.Machines.AsNoTracking() Where m.UID.Equals(uid) Select m).ToList()
        If machines IsNot Nothing AndAlso machines.Count > 0 Then
            Return machines(0)
        Else
            Return New Machines()
        End If
    End Function

    ''' <summary>
    ''' Lista máquinas por una lista de indentificaciones unicas
    ''' </summary>
    ''' <param name="list">Lista de identificaciones unicas de máquina</param>
    ''' <returns>Lista de máquinas</returns>
    Public Function ListMachinesByUId(list As List(Of String)) As List(Of Machines) Implements IMachineRepository.ListMachinesByUId
        Dim machines = (From m As Machines In Me._context.Machines.AsNoTracking() Where list.Contains(m.UID) Select m).ToList()
        If machines IsNot Nothing AndAlso machines.Count > 0 Then
            Return machines
        Else
            Return New List(Of Machines)()
        End If
    End Function

End Class
