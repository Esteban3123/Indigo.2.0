'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 05-09-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Budget
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class BudgetService

    ''' <summary>
    ''' obtiene una modificacion de compromiso por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCommitmentModificationByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.CommitmentModification Implements IBudgetServiceCommitmentModification.GetCommitmentModificationByCode
        Using service As ICommitmentModificationAdminService = Container.Current.Resolve(Of ICommitmentModificationAdminService)()
            Return service.GetCommitmentModificationByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una modificacion de compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCommitmentModificationById(id As Integer) As Domain.Entities.CommitmentModification Implements IBudgetServiceCommitmentModification.GetCommitmentModificationById
        Using service As ICommitmentModificationAdminService = Container.Current.Resolve(Of ICommitmentModificationAdminService)()
            Return service.GetCommitmentModificationById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una modificacion de compromiso
    ''' </summary>
    ''' <param name="commitmentModification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCommitmentModification(CommitmentModification As CommitmentModification, listCommitmentModificationDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of CommitmentModification) Implements IBudgetServiceCommitmentModification.SaveCommitmentModification
        Using service As ICommitmentModificationAdminService = Container.Current.Resolve(Of ICommitmentModificationAdminService)()
            Return service.SaveCommitmentModification(CommitmentModification, listCommitmentModificationDetailDelete, audit)
        End Using
    End Function

End Class