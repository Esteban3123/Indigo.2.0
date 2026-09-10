'***********************************************************************
' Assembly         : Domain.Common
' Author           : Juan Diego Diaz
' Created          : 03-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base


Public Interface IBlockRecordRepository

    Inherits IRepository(Of BlockRecord)

    ''' <summary>
    ''' Lista Todos los registros bloqueados
    ''' </summary>
    ''' <returns>Registros bloqueados</returns>
    Function ListAllBlockRecord() As List(Of BlockRecord)

    ''' <summary>
    ''' Obtiene una registro de bloqueo según parametros
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <param name="IdRecord">Id del registro</param>
    ''' <returns>Registro bloqueado</returns>
    Function GetBlockRecordByIdformAndIdRecord(ByVal IdForm As String, ByVal IdRecord As String, Optional tracking As Boolean = True) As BlockRecord

    ''' <summary>
    ''' Lista Todos los registros bloqueados por Formulario
    ''' </summary>
    ''' <param name="IdForm">Id del formulario</param>
    ''' <returns>Registros bloqueados</returns>
    Function ListBlockRecordByIdForm(IdForm As String) As List(Of BlockRecord)


    ''' <summary>
    ''' Limpiar regsitro de bloqueo
    ''' </summary>
    ''' <param name="CodUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_UnlockBlockRecord(ByVal CodUser As String) As SP_UnlockBlockRecord_Result

End Interface
