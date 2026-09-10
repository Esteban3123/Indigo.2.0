'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 15-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.InteropCost.Entities
Imports Domain.InteropCost

Public Class CostCenterRepository
    Inherits GenericRepository(Of CTNCENCOS)
    Implements ICostCenterRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IInteropCostModelUnitOfWork

    Public Sub New(ByVal context As IInteropCostModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un centro de costo por codigo
    ''' </summary>
    Public Function GetCostCenter(code As String) As CTNCENCOS Implements ICostCenterRepository.GetCostCenter
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From sa In _context.CTNCENCOS Where sa.CCCODIGO.Equals(code) Select sa).FirstOrDefault()
        If query IsNot Nothing AndAlso query.OID > 0 Then
            query.OriginalValue = (From sa In _context.CTNCENCOS.AsNoTracking() Where sa.CCCODIGO.Equals(code) Select sa).FirstOrDefault()
            Return query
        Else
            Return New CTNCENCOS()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un centro de costo por id
    ''' </summary>
    Public Function GetCostCenterById(id As Integer) As CTNCENCOS Implements ICostCenterRepository.GetCostCenterById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From sa In _context.CTNCENCOS Where sa.OID = id Select sa).FirstOrDefault()
        If query IsNot Nothing AndAlso query.OID > 0 Then
            query.OriginalValue = (From sa In _context.CTNCENCOS Where sa.OID = id Select sa).FirstOrDefault()
            Return query
        Else
            Return New CTNCENCOS()
        End If
    End Function

End Class