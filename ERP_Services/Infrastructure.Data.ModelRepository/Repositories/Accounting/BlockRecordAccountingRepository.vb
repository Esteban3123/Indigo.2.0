'***********************************************************************
' Assembly         : Infrastructure.Data.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-03-17
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio de la entidad bloqueo de registro
''' </summary>
Public Class BlockRecordAccountingRepository
    Inherits GenericRepository(Of BlockRecordGeneralLedger)
    Implements IBlockRecordAccountingRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto de contabilidad
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="acountingContext">Contexto de cintabilidad</param>
    Public Sub New(ByVal acountingContext As IGlobalModelUnitOfWork)
        MyBase.New(acountingContext)
        Me._context = acountingContext
    End Sub

#End Region

#Region "IBlockRecordAccountingRepository"

    ''' <summary>
    ''' <see cref="IBlockRecordAccountingRepository.GetBlockRecordByIdformAndIdRecord" />
    ''' </summary>
    ''' <param name="IdForm"><see cref="IBlockRecordAccountingRepository.GetBlockRecordByIdformAndIdRecord" /></param>
    ''' <param name="IdRecord"><see cref="IBlockRecordAccountingRepository.GetBlockRecordByIdformAndIdRecord" /></param>
    ''' <returns><see cref="IBlockRecordAccountingRepository.GetBlockRecordByIdformAndIdRecord" /></returns>
    Public Function GetBlockRecordByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordGeneralLedger Implements IBlockRecordAccountingRepository.GetBlockRecordByIdformAndIdRecord
        If tracking Then
            Dim blockRecord = From e In _context.BlockRecordGeneralLedger
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecord.Count() > 0 Then
                Return blockRecord.FirstOrDefault
            Else
                Return New BlockRecordGeneralLedger()
            End If
        Else
            Dim blockRecord = (From e In _context.BlockRecordGeneralLedger.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecord IsNot Nothing > 0 Then
                Return blockRecord
            Else
                Return New BlockRecordGeneralLedger()
            End If
        End If
    End Function

#End Region

End Class
