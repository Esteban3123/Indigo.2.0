'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BlockRecordMedicalFeesRepository
    Inherits GenericRepository(Of BlockRecordMedicalFees)
    Implements IBlockRecordMedicalFeesRepository


    ''' <summary>
    ''' Contexto de honorarios medicos
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de honorarios medicos
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
    Public Function GetBlockRecordMedicalFeesByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordMedicalFees Implements IBlockRecordMedicalFeesRepository.GetBlockRecordMedicalFeesByIdformAndIdRecord
        If tracking Then
            Dim blockRecordMedicalFees = From e In _context.BlockRecordMedicalFees
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecordMedicalFees.Count > 0 Then
                Return blockRecordMedicalFees.FirstOrDefault
            Else
                Return New BlockRecordMedicalFees()
            End If
        Else
            Dim blockRecordMedicalFees = (From e In _context.BlockRecordMedicalFees.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecordMedicalFees IsNot Nothing > 0 Then
                Return blockRecordMedicalFees
            Else
                Return New BlockRecordMedicalFees()
            End If
        End If
    End Function

End Class
