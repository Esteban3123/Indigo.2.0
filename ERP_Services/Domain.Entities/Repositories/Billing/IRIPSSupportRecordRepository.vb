'************************************************************
' Assembly         : Domain.Entities
' Author           : Generated
' Created          : 2024
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports System.Threading.Tasks

#End Region

Public Interface IRIPSSupportRecordRepository
    Inherits IRepository(Of RIPSSupportRecord)

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su identificador
    ''' </summary>
    ''' <param name="id">Identificador del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    Function GetRIPSSupportRecordByIdAsync(id As Integer) As Task(Of RIPSSupportRecord)

    ''' <summary>
    ''' Obtiene un registro de soporte RIPS por su código
    ''' </summary>
    ''' <param name="code">Código del registro de soporte RIPS</param>
    ''' <returns>Registro de soporte RIPS</returns>
    Function GetRIPSSupportRecordByCodeAsync(code As String) As Task(Of RIPSSupportRecord)

    ''' <summary>
    ''' Lista todos los registros de soporte RIPS
    ''' </summary>
    ''' <returns>Lista de registros de soporte RIPS</returns>
    Function ListAllRIPSSupportRecordsAsync() As Task(Of List(Of RIPSSupportRecord))

    ''' <summary>
    ''' Obtiene Status y Code sin tracking (para validaciones)
    ''' </summary>
    Function GetRIPSSupportRecordStatusByIdAsync(id As Integer) As Task(Of Tuple(Of Byte, String))

End Interface
