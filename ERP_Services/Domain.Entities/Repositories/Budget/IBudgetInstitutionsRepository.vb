'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 04-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IBudgetInstitutionRepository
    Inherits IRepository(Of BudgetaryEntity)

    ''' <summary>
    ''' Obtiene una entidad presupuestal por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetBudgetInstitution(code As String, Optional tracking As Boolean = True) As BudgetaryEntity

End Interface
