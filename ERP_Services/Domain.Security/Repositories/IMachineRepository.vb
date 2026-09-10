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

Public Interface IMachineRepository
    Inherits IRepository(Of Machines)

#Region "Methods"

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
    Function ListMachinesByUId(ByVal list As List(Of String)) As List(Of Machines)

    ''' <summary>
    ''' Obtiene una máquina por su identificación unica
    ''' </summary>
    ''' <param name="uid">Identificación unica</param>
    ''' <returns>Ma´quina consultada</returns>
    Function GetMachine(ByVal uid As String) As Machines

#End Region

End Interface
