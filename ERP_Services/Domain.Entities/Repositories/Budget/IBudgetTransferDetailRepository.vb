'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface IBudgetTransferDetailRepository
    Inherits IRepository(Of BudgetTransferDetail)

    ''' <summary>
    ''' Obtiene un detalle del traslado
    ''' </summary>
    '''<param name="Id">Id del traslado</param>
    ''' <returns></returns>
    Function GetBudgetTransferDetailById(Id As Integer) As BudgetTransferDetail

End Interface
