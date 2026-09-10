'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordInventoryRepository
    Inherits GenericRepository(Of BlockRecordInventory)
    Implements IBlockRecordInventoryRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene el registro si esta bloqueado o no
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordInventoryByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordInventory Implements IBlockRecordInventoryRepository.GetBlockRecordInventoryByIdformAndIdRecord
        If tracking Then
            Dim blockRecordInventory = From e In _context.BlockRecordInventory
                    Where e.FormId = IdForm AndAlso e.RecordId = IdRecord
                    Select e
            If blockRecordInventory.Count > 0 Then
                Return blockRecordInventory.FirstOrDefault
            Else
                Return New BlockRecordInventory()
            End If
        Else
            Dim blockRecordInventory = (From e In _context.BlockRecordInventory.AsNoTracking
                                Where e.FormId = IdForm AndAlso e.RecordId = IdRecord
                                Select e).FirstOrDefault

            If blockRecordInventory IsNot Nothing > 0 Then
                Return blockRecordInventory
            Else
                Return New BlockRecordInventory()
            End If
        End If
    End Function
    
End Class
