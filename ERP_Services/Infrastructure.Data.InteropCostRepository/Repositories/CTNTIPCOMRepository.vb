'***********************************************************************
' Assembly         : Infrastructure.Data.CrystalRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 09-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.InteropCost
Imports Domain.InteropCost.Entities

Public Class CTNTIPCOMRepository
    Inherits GenericRepository(Of CTNTIPCOM)
    Implements ICTNTIPCOMRepository

    ' contexto del repositorio de ciudades
    Private _context As IInteropCostModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IInteropCostModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCTNTIPCOMById(oid As Integer) As CTNTIPCOM Implements ICTNTIPCOMRepository.GetCTNTIPCOMById
        Dim query = From e In _context.CTNTIPCOM.AsNoTracking()
                   Where e.OID = oid
                   Select e
        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return New CTNTIPCOM()
        End If
    End Function
End Class