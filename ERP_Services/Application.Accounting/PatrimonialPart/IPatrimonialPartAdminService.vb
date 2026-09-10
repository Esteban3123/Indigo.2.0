'***********************************************************************
' Assembly         : Application.Accounting
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPatrimonialPartAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Saves the patrimonial part.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SavePatrimonialPart(ByVal patrimonialPart As Shareholding, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of Shareholding)

    ''' <summary>
    ''' Updates the state
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStatePatrimonialPart(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of Shareholding)

    ''' <summary>
    ''' Deletes the card.
    ''' </summary>
    ''' <param name="patrimonialPart">The patrimonial part.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeletePatrimonialPart(ByVal patrimonialPart As Shareholding, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Gets the card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetPatrimonialPart(ByVal code As String, ByVal audit As AuditMessage) As Shareholding

End Interface
