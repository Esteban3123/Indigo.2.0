'***********************************************************************
' Assembly         : Infrastructure.Data.MixingStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 26-04-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class BlockRecordMixingStationRepository
    Inherits GenericRepository(Of BlockRecordMixingStation)
    Implements IBlockRecordMixingStationRepository, Inject

    ''' <summary>
    ''' Contexto de MixingStation
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de MixingStation
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
    Public Function GetBlockRecordMixingStationByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordMixingStation Implements IBlockRecordMixingStationRepository.GetBlockRecordMixingStationByIdformAndIdRecord
        If tracking Then
            Dim blockRecordMixingStation = From e In _context.BlockRecordMixingStation
                                           Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                           Select e
            If blockRecordMixingStation.Count > 0 Then
                Return blockRecordMixingStation.FirstOrDefault
            Else
                Return New BlockRecordMixingStation()
            End If
        Else
            Dim blockRecordMixingStation = (From e In _context.BlockRecordMixingStation.AsNoTracking
                                            Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                            Select e).FirstOrDefault

            If blockRecordMixingStation IsNot Nothing > 0 Then
                Return blockRecordMixingStation
            Else
                Return New BlockRecordMixingStation()
            End If
        End If
    End Function

End Class
