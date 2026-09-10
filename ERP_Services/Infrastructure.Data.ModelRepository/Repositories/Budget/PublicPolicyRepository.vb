'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Duván Albeiro Mejia Cortes
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PublicPolicyRepository
    Inherits GenericRepository(Of PublicPolicy)
    Implements IPublicPolicyRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Builder"

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"
#End Region

End Class
