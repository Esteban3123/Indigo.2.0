'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICardAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCard(ByVal card As Cards, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Cards)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStateCard(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Cards)

    ''' <summary>
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="card">The card.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCard(ByVal card As Cards, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Gets the card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetCard(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of Cards)

    ''' <summary>
    ''' metodo para obtener una tarjeta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCardById(id As Integer) As Cards

End Interface
