'***********************************************************************
' Assembly         : Infrastructure.Data.BudgetRepository
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BudgetInstitutionsRepository
    Inherits GenericRepository(Of BudgetaryEntity)
    Implements IBudgetInstitutionRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

#Region "Construct"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region


#Region "Methods"
    ''' <summary>
    ''' Obtiene una entidad presupuestal por código
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Public Function GetBudgetInstitution(code As String, Optional tracking As Boolean = True) As BudgetaryEntity Implements IBudgetInstitutionRepository.GetBudgetInstitution
        Dim budgetEntity = (From e In _context.BudgetaryEntity.Include("BudgetaryValidity1")
                     Where e.Code = code
                     Select e).FirstOrDefault
        If budgetEntity IsNot Nothing Then

            Dim thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = budgetEntity.ThirdPartyId Select t).FirstOrDefault
            budgetEntity.ThirdPartyDescription = thirdParty.Nit + " - " + thirdParty.Name

            If budgetEntity.BudgetaryValidity1 IsNot Nothing AndAlso budgetEntity.BudgetaryValidity1.Count > 0 Then
                For Each item As BudgetaryValidity In budgetEntity.BudgetaryValidity1
                    'Representante Legal
                    thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = item.LegalRepresentativeId Select t).FirstOrDefault
                    item.LegalRepresentativeDescription = thirdParty.Nit + " - " + thirdParty.Name
                    'Jefe de Presupuesto
                    thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = item.ChiefBudgetOfficerId Select t).FirstOrDefault
                    item.BudgetBossDescription = thirdParty.Nit + " - " + thirdParty.Name
                    'Jefe Financiero
                    thirdParty = (From t In _context.ThirdParty.AsNoTracking Where t.Id = item.ChiefFinancialOfficerId Select t).FirstOrDefault
                    item.FinancialBossDescription = thirdParty.Nit + " - " + thirdParty.Name
                Next
            End If

            budgetEntity.OriginalValue = (From e In _context.BudgetaryEntity.AsNoTracking.Include("BudgetaryValidity1")
                                Where e.Code = code
                                Select e).FirstOrDefault
            Return budgetEntity
        Else
            Return New BudgetaryEntity()
        End If
    End Function
#End Region


End Class
