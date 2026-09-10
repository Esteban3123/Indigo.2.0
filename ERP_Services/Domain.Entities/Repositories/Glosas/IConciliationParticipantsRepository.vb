'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Participantes de Conciliación.
''' </summary>
Public Interface IConciliationParticipantsRepository
    Inherits IRepository(Of ConciliationParticipants)

    ''' <summary>
    ''' Lista todas los participantes de conciliaciones.
    ''' </summary>
    ''' <returns>Lista Participantes Conciliación</returns>
    Function ListAllConciliationParticipants() As List(Of ConciliationParticipants)
    ''' <summary>
    ''' Obtiene un participante de conciliacion especifico.
    ''' </summary>
    ''' <param name="code">El código del participante de conciliacion</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    Function GetConciliationParticipantsById(ByVal code As String) As ConciliationParticipants
    ''' <summary>
    ''' Obtiene un participante de conciliacion especifico.
    ''' </summary>
    ''' <param name="code">El código de la cabecera de conciliación</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    Function ListConciliationParticipantsByIdConciliationC(ByVal code As String) As List(Of ConciliationParticipants)


End Interface
