'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 05-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ICommitmentModificationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' obtiene una modificacion de compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCommitmentModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As CommitmentModification
    ''' <summary>
    ''' obtiene una modificacion de compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCommitmentModificationById(id As Integer) As CommitmentModification
    ''' <summary>
    ''' Guarda una modificacion de compromiso
    ''' </summary>
    ''' <param name="commitmentModification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveCommitmentModification(CommitmentModification As CommitmentModification, listCommitmentModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of CommitmentModification)

End Interface
