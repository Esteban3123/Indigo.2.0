'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Andrés Steven Rojas
' Created          : 31/10/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICardCollectionsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro de recaudo de tarjetas
    ''' </summary>
    ''' <param name="cardCollections">The card collections.</param>
    ''' <param name="audit">The audit.</param>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Function SaveCardCollections(ByVal cardCollections As CardCollections, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CardCollections)

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function GetCardCollections(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CardCollections)

    ''' <summary>
    ''' Obtiene un registro de recaudo de tarjetas por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetCardCollectionsById(id As Integer) As CardCollections

End Interface


