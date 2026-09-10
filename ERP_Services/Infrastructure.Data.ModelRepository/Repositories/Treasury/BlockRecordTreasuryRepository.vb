'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordTreasuryRepository
    Inherits GenericRepository(Of BlockRecordTreasury)
    Implements IBlockRecordTreasuryRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBlockRecordTreasuryByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordTreasury Implements IBlockRecordTreasuryRepository.GetBlockRecordTreasuryByIdformAndIdRecord
        If tracking Then
            Dim blockRecordTreasury = From e In _context.BlockRecordTreasury
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecordTreasury.Count > 0 Then
                Return blockRecordTreasury.FirstOrDefault
            Else
                Return New BlockRecordTreasury()
            End If
        Else
            Dim blockRecordTreasury = (From e In _context.BlockRecordTreasury.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecordTreasury IsNot Nothing > 0 Then
                Return blockRecordTreasury
            Else
                Return New BlockRecordTreasury()
            End If
        End If
    End Function

End Class
