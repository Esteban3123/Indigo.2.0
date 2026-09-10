'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordContractRepository
    Inherits GenericRepository(Of BlockRecordContract)
    Implements IBlockRecordContractRepository

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
    Public Function GetBlockRecordContractByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordContract Implements IBlockRecordContractRepository.GetBlockRecordContractByIdformAndIdRecord
        If tracking Then
            Dim blockRecordContract = From e In _context.BlockRecordContract
                    Where e.FormId = IdForm AndAlso e.RecordId = IdRecord
                    Select e
            If blockRecordContract.Count > 0 Then
                Return blockRecordContract.FirstOrDefault
            Else
                Return New BlockRecordContract()
            End If
        Else
            Dim blockRecordContract = (From e In _context.BlockRecordContract.AsNoTracking
                                Where e.FormId = IdForm AndAlso e.RecordId = IdRecord
                                Select e).FirstOrDefault

            If blockRecordContract IsNot Nothing > 0 Then
                Return blockRecordContract
            Else
                Return New BlockRecordContract()
            End If
        End If
    End Function
    
End Class
