'************************************************************
' Assembly         : Infrastructure.Data.Contract
' Author           : Anthony Smith Cuellar Ocampo
' Created          : 2023-12-04
'
' Copyright        : (c) . All rights reserved.
'************************************************************

Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class RIPSServiceGroupsRepository
    Inherits GenericRepository(Of RIPSServiceGroups)
    Implements IRIPSServiceGroupsRepository

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todos los grupos de servicios RIPS
    ''' </summary>
    ''' <returns>Lista con grupos de servicios RIPS</returns>
    Public Function ListAllRIPSServiceGroups() As List(Of RIPSServiceGroups) Implements IRIPSServiceGroupsRepository.ListAllRIPSServiceGroups
        Dim Busqueda = From e In _context.RIPSServiceGroups
                       Select e
        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por ID.
    ''' </summary>
    ''' <param name="id">ID del grupo de servicio</param>
    ''' <returns>Grupo de servicio RIPS</returns>
    Public Function GetRIPSServiceGroupById(Id As Integer, Optional tracking As Boolean = True) As RIPSServiceGroups Implements IRIPSServiceGroupsRepository.GetRIPSServiceGroupById
        Dim ServiceGroup = (From e In _context.RIPSServiceGroups
                            Where e.Id = Id
                            Select e).FirstOrDefault()
        Return ServiceGroup
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio RIPS por su código.
    ''' </summary>
    ''' <param name="code">Código del grupo de servicio</param>
    ''' <param name="audit"></param>
    ''' <returns>Grupo de servicios RIPS</returns>
    Public Function GetRIPSServiceGroupByCode(code As String) As RIPSServiceGroups Implements IRIPSServiceGroupsRepository.GetRIPSServiceGroupByCode
        Dim ServiceGroup = (From e In _context.RIPSServiceGroups
                            Where e.Code = code
                            Select e).FirstOrDefault
        Return ServiceGroup
    End Function
End Class
