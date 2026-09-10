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

Public Class AFNDEPRECIRepository
    Inherits GenericRepository(Of AFNDEPRECI)
    Implements IAFNDEPRECIRepository

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

    Public Function GetAFNDEPRECIByYearMonth(year As Integer, month As Integer) As AFNDEPRECI Implements IAFNDEPRECIRepository.GetAFNDEPRECIByYearMonth
        Dim query = From e In _context.AFNDEPRECI.AsNoTracking().Include("AFNCALDEP").AsNoTracking()
                   Where e.ACAFECCHCI IsNot Nothing AndAlso e.ACAFECCHCI.Value.Year = year AndAlso e.ACAFECCHCI.Value.Month = month
                   Select e
        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return New AFNDEPRECI()
        End If
    End Function
End Class