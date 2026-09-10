'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ITreasuryNoteRepository
    Inherits IRepository(Of TreasuryNote)

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    Function GetTreasuryNote(ByVal code As String) As TreasuryNote

    ''' <summary>
    ''' Obtiene una nota de tesoreria por id
    ''' </summary>
    Function GetTreasuryNoteById(ByVal Id As Integer) As TreasuryNote


    Function ListTreasuryNoteMassiveConfirm(listDocuments As List(Of String)) As List(Of TreasuryNote)
End Interface
