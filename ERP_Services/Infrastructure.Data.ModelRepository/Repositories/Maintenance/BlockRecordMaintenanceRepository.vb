'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.Data.ModelRepository

Public Class BlockRecordMaintenanceRepository
    Inherits GenericRepository(Of BlockRecordMaintenance)
    Implements IBlockRecordMaintenanceRepository


    ''' <summary>
    ''' Contexto de mantenimiento
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de mantenimiento
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene el registro bloqueado
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetBlockRecordMaintenanceByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordMaintenance Implements IBlockRecordMaintenanceRepository.GetBlockRecordMaintenanceByIdformAndIdRecord
        If tracking Then
            Dim blockRecord = From e In _context.BlockRecordMaintenance
                    Where e.IdForm = CType(IdForm, Integer) AndAlso e.IdRecord = CType(IdRecord, Integer)
                    Select e
            If blockRecord.Count > 0 Then
                Return blockRecord.FirstOrDefault
            Else
                Return New BlockRecordMaintenance()
            End If
        Else
            Dim blockRecord = (From e In _context.BlockRecordMaintenance.AsNoTracking
                                Where e.IdForm = CType(IdForm, Integer) AndAlso e.IdRecord = CType(IdRecord, Integer)
                                Select e).FirstOrDefault

            If CInt(blockRecord IsNot Nothing) > 0 Then
                Return blockRecord
            Else
                Return New BlockRecordMaintenance()
            End If
        End If
    End Function
End Class
