'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Juan Carlos Bermudez
' Created          : 16/07/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordPayrollRepository
    Inherits GenericRepository(Of BlockRecordPayroll)
    Implements IBlockRecordPayrollRepository

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
    Public Function GetBlockRecordPayrollByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordPayroll Implements IBlockRecordPayrollRepository.GetBlockRecordPayrollByIdformAndIdRecord
        If tracking Then
            Dim blockRecordPayroll = From e In _context.BlockRecordPayroll
                    Where e.FormId = IdForm AndAlso e.RecordId = IdRecord
                    Select e
            If blockRecordPayroll.Count > 0 Then
                Return blockRecordPayroll.FirstOrDefault
            Else
                Return New BlockRecordPayroll()
            End If
        Else
            Dim blockRecordPayroll = (From e In _context.BlockRecordPayroll.AsNoTracking
                                Where e.FormId = IdForm AndAlso e.RecordId = IdRecord
                                Select e).FirstOrDefault

            If blockRecordPayroll IsNot Nothing > 0 Then
                Return blockRecordPayroll
            Else
                Return New BlockRecordPayroll()
            End If
        End If
    End Function

End Class
