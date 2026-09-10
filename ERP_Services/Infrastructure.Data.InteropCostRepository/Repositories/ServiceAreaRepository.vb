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

Public Class ServiceAreaRepository
    Inherits GenericRepository(Of GENARESER)
    Implements IServiceAreaRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IInteropCostModelUnitOfWork

    Public Sub New(ByVal context As IInteropCostModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un area de servicio por codigo
    ''' </summary>
    Public Function GetServiceArea(code As String) As GENARESER Implements IServiceAreaRepository.GetServiceArea
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("Code")
        End If
        Dim query = (From sa In _context.GENARESER Where sa.GASCODIGO.Equals(code) Select sa).FirstOrDefault()
        If query IsNot Nothing AndAlso query.OID > 0 Then
            query.OriginalValue = (From sa In _context.GENARESER.AsNoTracking() Where sa.GASCODIGO.Equals(code) Select sa).FirstOrDefault()
            Return query
        Else
            Return New GENARESER()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un area de servicio por id
    ''' </summary>
    Public Function GetServiceAreaById(id As Integer) As GENARESER Implements IServiceAreaRepository.GetServiceAreaById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From sa In _context.GENARESER Where sa.OID = id Select sa).FirstOrDefault()
        If query IsNot Nothing AndAlso query.OID > 0 Then
            query.OriginalValue = (From sa In _context.GENARESER Where sa.OID = id Select sa).FirstOrDefault()
            Return query
        Else
            Return New GENARESER()
        End If
    End Function

End Class
