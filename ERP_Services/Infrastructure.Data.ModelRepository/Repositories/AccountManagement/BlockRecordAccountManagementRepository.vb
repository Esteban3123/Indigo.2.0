
'************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Felix Camilo Salazar Roldan
' Created          : 13-12-2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

Public Class BlockRecordAccountManagementRepository
    Inherits GenericRepository(Of BlockRecordAccountManagement)

    Implements IBlockRecordAccountManagementRepository


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
    Public Function GetBlockRecordByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordAccountManagement Implements IBlockRecordAccountManagementRepository.GetBlockRecordByIdformAndIdRecord
        If tracking Then
            Dim blockRecord = From e In _context.BlockRecordAccountManagement
                              Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                              Select e
            If blockRecord.Count > 0 Then
                Return blockRecord.FirstOrDefault
            Else
                Return New BlockRecordAccountManagement()
            End If
        Else
            Dim blockRecord = (From e In _context.BlockRecordAccountManagement.AsNoTracking
                               Where e.IdForm = IdForm AndAlso e.IdRecord = IdRecord
                               Select e).FirstOrDefault

            If blockRecord IsNot Nothing > 0 Then
                Return blockRecord
            Else
                Return New BlockRecordAccountManagement()
            End If
        End If
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListAllBlockRecord() As List(Of BlockRecordAccountManagement) Implements IBlockRecordAccountManagementRepository.ListAllBlockRecord
        Dim blockRecord = From e In _context.BlockRecordAccountManagement
                          Select e
        Return blockRecord.ToList()
    End Function

    ''' <summary>
    ''' Lista Todos los registros bloqueados por Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <returns>Registros bloqueados</returns>
    Public Function ListBlockRecordByIdForm(IdForm As String) As List(Of BlockRecordAccountManagement) Implements IBlockRecordAccountManagementRepository.ListBlockRecordByIdForm
        Dim blockRecord = From e In _context.BlockRecordAccountManagement
                          Where e.IdForm = IdForm
                          Select e
        Return blockRecord.ToList()
    End Function
End Class

