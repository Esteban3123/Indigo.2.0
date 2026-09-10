'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface INoteConceptRepository
    Inherits IRepository(Of NoteConcepts)

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetNoteConcept(ByVal code As String) As NoteConcepts

    ''' <summary>
    ''' Obtiene un concepto de nota por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetNoteConceptById(ByVal Id As Integer) As NoteConcepts

    ''' <summary>
    ''' Obtiene una lista de conceptos de nota
    ''' </summary>
    ''' <param name="codeList">The code.</param>
    ''' <returns></returns>
    Function GetNoteConceptList(ByVal codeList As List(Of String)) As List(Of NoteConcepts)


End Interface
