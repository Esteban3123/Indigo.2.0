'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29/01/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPrivateBudgetRepository

    Inherits IRepository(Of PrivateBudget)

    ''' <summary>
    ''' Obtiene el registro
    ''' </summary>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListPrivateBudget(Optional tracking As Boolean = True) As List(Of SP_ListPrivateBudget_Result)

    Function GetListPrivateBudgetById(Id As Integer, Optional tracking As Boolean = True) As PrivateBudget

    Function GetListPrivateBudgetByData(PrivateBudgetItemsStructureId As Integer, MainAccountId As Integer?, ThirdPartyId As Integer?, CostCenterId As Integer?, Month As Integer, Optional tracking As Boolean = True) As PrivateBudget

End Interface
