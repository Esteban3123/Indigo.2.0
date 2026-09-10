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

Public Class MachineUpdatePackageRepository
    Inherits GenericRepository(Of MachineUpdatePackage)
    Implements IMachineUpdatePackageRepository

    ''' <summary>
    ''' Esta variable  contiene el contexto de nuestro modelo.
    ''' </summary>
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    ''' Inicializa una nueva instancia <see cref="MachineUpdatePackageRepository" /> class.	
    ''' </summary>
    ''' <param name="contex">nThe contex.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Obtiene la relación de paquete asignado a una máquina
    ''' </summary>
    ''' <param name="id">Id del registro de la relación</param>
    ''' <returns>Relación consultada</returns>
    Public Function GetById(id As Integer) As MachineUpdatePackage Implements IMachineUpdatePackageRepository.GetById
        Dim res = (From p As MachineUpdatePackage In Me._context.MachineUpdatePackage.Include("Machines").Include("UpdatePackage") Where p.Id = id Select p).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res(0)
        Else
            Return New MachineUpdatePackage()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la relación entre una máquina y un paquete
    ''' </summary>
    ''' <param name="idMachine">Id de la máquina</param>
    ''' <param name="idPackage">Id del paquete</param>
    ''' <returns>Relación consultada</returns>
    Public Function GetByIdMachineAndPackage(idMachine As Integer, idPackage As Integer) As MachineUpdatePackage Implements IMachineUpdatePackageRepository.GetByIdMachineAndPackage
        Dim res = (From p As MachineUpdatePackage In Me._context.MachineUpdatePackage.Include("Machines").Include("UpdatePackage") Where p.IdMachine = idMachine And p.IdUpdatePackage = idPackage Select p).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res(0)
        Else
            Return New MachineUpdatePackage()
        End If
    End Function

End Class
