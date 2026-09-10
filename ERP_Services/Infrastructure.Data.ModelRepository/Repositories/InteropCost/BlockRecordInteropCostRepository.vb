'***********************************************************************
' Assembly         : Infrastructure.Data.InteropCostRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 11-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordInteropCostRepository
    Inherits GenericRepository(Of BlockRecordInteropCost)
    Implements IBlockRecordInteropCostRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro bloqueado por id del registro y formulario
    ''' </summary>
    Public Function GetBlockRecordInteropCostByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordInteropCost Implements IBlockRecordInteropCostRepository.GetBlockRecordInteropCostByIdformAndIdRecord
        If tracking Then
            Dim blockRecordInteropCost = From e In _context.BlockRecordInteropCost
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecordInteropCost.Count > 0 Then
                Return blockRecordInteropCost.FirstOrDefault
            Else
                Return New BlockRecordInteropCost()
            End If
        Else
            Dim blockRecordInteropCost = (From e In _context.BlockRecordInteropCost.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecordInteropCost IsNot Nothing > 0 Then
                Return blockRecordInteropCost
            Else
                Return New BlockRecordInteropCost()
            End If
        End If
    End Function

End Class