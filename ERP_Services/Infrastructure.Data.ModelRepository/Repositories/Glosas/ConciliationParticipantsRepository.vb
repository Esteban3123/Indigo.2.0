'************************************************************
' Assembly         : Infraestructure.Data.GlosasRepository
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities

#End Region

''' <summary>
''' Repositorio Participantes Conciliación
''' </summary>
Public Class ConciliationParticipantsRepository

    Inherits GenericRepository(Of ConciliationParticipants)
    Implements IConciliationParticipantsRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    '''Inicializa la nueva instancia de clase.
    ''' </summary>
    ''' <param name="contex">El contexto.</param>
    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    ''' <summary>
    ''' Funcion que obtiene todas los participantes de conciliaciónes.
    ''' </summary>
    ''' <returns>Lista de Participantes Conciliación</returns>
    Public Function ListAllConciliationC() As List(Of ConciliationParticipants) Implements IConciliationParticipantsRepository.ListAllConciliationParticipants
        Dim Busqueda = From e In _context.ConciliationParticipants
                                 Select e

        Return Busqueda.ToList
    End Function

    ''' <summary>
    ''' Funcion que obtiene un participante de conciliación especifico.
    ''' </summary>
    ''' <param name="Id">Id Participante Conciliación</param>
    ''' <returns>Objeto Participante Conciliación</returns>
    Public Function GetConciliationParticipantsById(Id As String) As ConciliationParticipants Implements IConciliationParticipantsRepository.GetConciliationParticipantsById
        Dim Conciliation = From e In _context.ConciliationParticipants
        Where e.Id = CInt(Id)
        Select e
        If Conciliation.Count > 0 Then
            Dim ConciliationData = Conciliation.SingleOrDefault
            ConciliationData.OriginalValue = (From e In _context.ConciliationParticipants.AsNoTracking
        Where e.Id = CInt(Id)
        Select e).SingleOrDefault
            Return ConciliationData
        Else
            Return New ConciliationParticipants
        End If
    End Function

    ''' <summary>
    ''' Funcion que obtiene participantes de conciliación especificos.
    ''' </summary>
    ''' <param name="Id">Id Conciliación Cabecera</param>
    ''' <returns>Lista de Participantes Conciliación</returns>
    Public Function ListConciliationParticipantsByIdConciliationC(Id As String) As List(Of ConciliationParticipants) Implements IConciliationParticipantsRepository.ListConciliationParticipantsByIdConciliationC
        Dim Busqueda = From e In _context.ConciliationParticipants
        Where e.ConciliationCId = CInt(Id)
        Select e
        Return Busqueda.ToList
    End Function

End Class
