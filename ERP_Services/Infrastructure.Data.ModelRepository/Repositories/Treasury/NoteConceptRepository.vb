'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities


Public Class NoteConceptRepository
    Inherits GenericRepository(Of NoteConcepts)
    Implements INoteConceptRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un concepto de nota
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetNoteConcept(code As String) As NoteConcepts Implements INoteConceptRepository.GetNoteConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As NoteConcepts In Me._context.NoteConcepts.Include("MainAccounts").Include("CashFlowConcept").AsNoTracking Where d.Code.Equals(code.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As NoteConcepts In Me._context.NoteConcepts.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New NoteConcepts()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto de nota por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetNoteConceptById(Id As Integer) As NoteConcepts Implements INoteConceptRepository.GetNoteConceptById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As NoteConcepts In Me._context.NoteConcepts.Include("MainAccounts").Include("CashFlowConcept").AsNoTracking Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As NoteConcepts In Me._context.NoteConcepts.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New NoteConcepts()
        End If
    End Function
    ''' <summary>
    ''' Obtiene una lista de conceptos
    ''' </summary>
    ''' <param name="codeList"></param>
    ''' <returns></returns>
    Public Function GetNoteConceptList(codeList As List(Of String)) As List(Of NoteConcepts) Implements INoteConceptRepository.GetNoteConceptList
        If codeList Is Nothing OrElse codeList.Count = 0 Then
            Return New List(Of NoteConcepts)
        End If
        Return (From d In Me._context.NoteConcepts.Include("MainAccounts").Include("CashFlowConcept").AsNoTracking
                Where codeList.Any(Function(code) d.Code.Contains(code.Trim()))
                Select d).ToList()
    End Function

End Class
