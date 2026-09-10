'***********************************************************************
' Assembly         : Application.Budget
' Author           : Juan Carlos Bermudez
' Created          : 02-09-2015
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
    ''' obtiene un compromiso codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetCommitmentByCode(code As String, BudgetaryValidityId As Integer, audit As AuditMessage) As Domain.Entities.Commitment Implements IBudgetServiceCommitment.GetCommitmentByCode
        Using service As ICommitmentAdminService = Container.Current.Resolve(Of ICommitmentAdminService)()
            Return service.GetCommitmentByCode(code, BudgetaryValidityId, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene un compromiso por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCommitmentById(id As Integer) As Domain.Entities.Commitment Implements IBudgetServiceCommitment.GetCommitmentById
        Using service As ICommitmentAdminService = Container.Current.Resolve(Of ICommitmentAdminService)()
            Return service.GetCommitmentById(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un compromiso
    ''' </summary>
    ''' <param name="commitment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SaveCommitment(commitment As Commitment, listCommitmentDetailDelete As List(Of Integer), audit As AuditMessage) As ActionResult(Of Commitment) Implements IBudgetServiceCommitment.SaveCommitment
        Using service As ICommitmentAdminService = Container.Current.Resolve(Of ICommitmentAdminService)()
            Return service.SaveCommitment(commitment, listCommitmentDetailDelete, audit)
        End Using
    End Function

End Class