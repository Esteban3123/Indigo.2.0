'***********************************************************************
' Assembly         : Infrastructure.Data.InteropCostRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordCostRepository
    Inherits GenericRepository(Of BlockRecordCost)
    Implements IBlockRecordCostRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro bloqueado por id del registro y formulario
    ''' </summary>
    Public Function GetBlockRecordCostByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordCost Implements IBlockRecordCostRepository.GetBlockRecordCostByIdformAndIdRecord
        If tracking Then
            Dim blockRecordCost = From e In _context.BlockRecordCost
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecordCost.Count > 0 Then
                Return blockRecordCost.FirstOrDefault
            Else
                Return New BlockRecordCost()
            End If
        Else
            Dim blockRecordCost = (From e In _context.BlockRecordCost.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecordCost IsNot Nothing > 0 Then
                Return blockRecordCost
            Else
                Return New BlockRecordCost()
            End If
        End If
    End Function
End Class