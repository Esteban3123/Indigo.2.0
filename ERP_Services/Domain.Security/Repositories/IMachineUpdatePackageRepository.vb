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

Public Interface IMachineUpdatePackageRepository
    Inherits IRepository(Of MachineUpdatePackage)

#Region "Methods"

    ''' <summary>
    ''' Obtiene la relación de paquete asignado a una máquina
    ''' </summary>
    ''' <param name="id">Id del registro de la relación</param>
    ''' <returns>Relación consultada</returns>
    Function GetById(ByVal id As Int32) As MachineUpdatePackage

    ''' <summary>
    ''' Obtiene la relación entre una máquina y un paquete
    ''' </summary>
    ''' <param name="idMachine">Id de la máquina</param>
    ''' <param name="idPackage">Id del paquete</param>
    ''' <returns>Relación consultada</returns>
    Function GetByIdMachineAndPackage(ByVal idMachine As Int32, ByVal idPackage As Int32) As MachineUpdatePackage

#End Region

End Interface
