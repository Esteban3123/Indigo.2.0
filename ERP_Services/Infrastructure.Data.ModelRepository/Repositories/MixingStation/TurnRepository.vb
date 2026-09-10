'***********************************************************************
' Assembly         : Infrastructure.Data.MixinStationRepository
' Author           : Yoe Andres Cardenas
' Created          : 15/04/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base

Public Class TurnRepository
    Inherits GenericRepository(Of Turn)
    Implements ITurnRepository, Inject
    ''' <summary>
    ''' Contexto de Turn
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Turn
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
    ''' <summary>
    ''' Lista todos los turnos
    ''' </summary>
    ''' <returns>Lista de turnos</returns>
    ''' <remarks></remarks>
    Public Function ListAllTurn() As List(Of Turn) Implements ITurnRepository.ListAllTurn
        Dim turn = From e In _context.Turn
                   Select e
        Return turn.ToList()
    End Function
    ''' <summary>
    ''' Obtiene un turno especifivo
    ''' </summary>
    ''' <param name="code">Codigo del turno</param>
    ''' <returns>Turno</returns>
    ''' <remarks></remarks>
    Public Function GetTurn(code As String, Optional tracking As Boolean = True) As Turn Implements ITurnRepository.GetTurn
        Dim turn = From e In _context.Turn
                   Where e.Code = code
                   Select e
        If turn.Count > 0 Then
            Dim ObjTurn = Nothing
            If tracking = False Then
                ObjTurn = (From e In _context.Turn.AsNoTracking
                           Where e.Code = code
                           Select e).SingleOrDefault
            Else
                ObjTurn = turn.SingleOrDefault
            End If
            Return ObjTurn
        Else
            Return New Turn()
        End If
    End Function
    ''' <summary>
    ''' Obtiene un turno por el identificador
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetTurnById(id As String, Optional tracking As Boolean = True) As Turn Implements ITurnRepository.GetTurnById
        Dim turn = From e In _context.Turn
                   Where e.Id = id
                   Select e
        If turn.Count > 0 Then
            Dim ObjTurn = Nothing
            If tracking = False Then
                ObjTurn = (From e In _context.Turn.AsNoTracking
                           Where e.Id = id
                           Select e).SingleOrDefault
            Else
                ObjTurn = turn.SingleOrDefault
            End If
            Return ObjTurn
        Else
            Return New Turn()
        End If
    End Function
End Class