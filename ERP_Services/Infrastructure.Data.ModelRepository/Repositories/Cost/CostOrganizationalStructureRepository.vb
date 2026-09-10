'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CostOrganizationalStructureRepository
    Inherits GenericRepository(Of CostOrganizationalStructureOfCosts)
    Implements ICostOrganizationalStructureRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCostOrganizationalStructure(code As String) As CostOrganizationalStructureOfCosts Implements ICostOrganizationalStructureRepository.GetCostOrganizationalStructure
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From o In _context.CostOrganizationalStructureOfCosts Where o.Code.Equals(code) Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.CostOrganizationalStructureOfCosts.AsNoTracking() Where o.Code.Equals(code) Select o).FirstOrDefault()
            Return query
        Else
            Return New CostOrganizationalStructureOfCosts()
        End If
    End Function

    Public Function GetCostOrganizationalStructureById(id As Integer) As CostOrganizationalStructureOfCosts Implements ICostOrganizationalStructureRepository.GetCostOrganizationalStructureById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From o In _context.CostOrganizationalStructureOfCosts Where o.Id = id Select o).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From o In _context.CostOrganizationalStructureOfCosts.AsNoTracking() Where o.Id = id Select o).FirstOrDefault()
            Return query
        Else
            Return New CostOrganizationalStructureOfCosts()
        End If
    End Function
End Class