'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 18-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class OrganizationalStructureRepository
    Inherits GenericRepository(Of OrganizationalStructureOfCosts)
    Implements IOrganizationalStructureRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una estructura organizacional por codigo
    ''' </summary>
    Public Function GetOrganizationalStructure(code As String) As OrganizationalStructureOfCosts Implements IOrganizationalStructureRepository.GetOrganizationalStructure
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From o In _context.OrganizationalStructureOfCosts Where o.Code.Equals(code) Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.OrganizationalStructureOfCosts.AsNoTracking() Where o.Code.Equals(code) Select o).FirstOrDefault()
            Return query
        Else
            Return New OrganizationalStructureOfCosts()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una estructura organizacional por id
    ''' </summary>
    Public Function GetOrganizationalStructureById(id As Integer) As OrganizationalStructureOfCosts Implements IOrganizationalStructureRepository.GetOrganizationalStructureById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From o In _context.OrganizationalStructureOfCosts Where o.Id = id Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.OrganizationalStructureOfCosts.AsNoTracking() Where o.Id = id Select o).FirstOrDefault()
            Return query
        Else
            Return New OrganizationalStructureOfCosts()
        End If
    End Function

End Class