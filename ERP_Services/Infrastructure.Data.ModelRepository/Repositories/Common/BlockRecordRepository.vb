'***********************************************************************
' Assembly         : Infrastructure.Data.CommonRepository
' Author           : Juan Diego Diaz
' Created          : 03-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class BlockRecordRepository
    Inherits GenericRepository(Of BlockRecord)

    Implements IBlockRecordRepository



    ' contexto del repositorio de ciudades
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''  Contructor del repositorio coidades el cual instancia una nueva clase
    ''' </summary>
    ''' <param name="context">contexto del repositorio</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Public Function GetBlockRecordByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecord Implements IBlockRecordRepository.GetBlockRecordByIdformAndIdRecord
        If tracking Then
            Dim blockRecord = From e In _context.BlockRecord
                    Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                    Select e
            If blockRecord.Count > 0 Then
                Return blockRecord.FirstOrDefault
            Else
                Return New BlockRecord()
            End If
        Else
            Dim blockRecord = (From e In _context.BlockRecord.AsNoTracking
                                Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                                Select e).FirstOrDefault

            If blockRecord IsNot Nothing > 0 Then
                Return blockRecord
            Else
                Return New BlockRecord()
            End If
        End If
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListAllBlockRecord() As List(Of BlockRecord) Implements IBlockRecordRepository.ListAllBlockRecord
        Dim blockRecord = From e In _context.BlockRecord
               Select e
        Return blockRecord.ToList()
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados por Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListBlockRecordByIdForm(IdForm As String) As List(Of BlockRecord) Implements IBlockRecordRepository.ListBlockRecordByIdForm
        Dim blockRecord = From e In _context.BlockRecord
                          Where e.IdForm = IdForm
                          Select e
        Return blockRecord.ToList()
    End Function

    ''' <summary>
    ''' Limpiar regsitro de bloqueo
    ''' </summary>
    ''' <param name="CodUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_UnlockBlockRecord(CodUser As String) As SP_UnlockBlockRecord_Result Implements IBlockRecordRepository.SP_UnlockBlockRecord
        Dim obj = (From c In _context.SP_UnlockBlockRecord(CodUser)).ToList()
        If obj.Count > 0 Then
            Return obj.SingleOrDefault
        Else
            Return New SP_UnlockBlockRecord_Result
        End If
    End Function
End Class
