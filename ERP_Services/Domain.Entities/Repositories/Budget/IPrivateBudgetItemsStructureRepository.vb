'***********************************************************************
' Assembly         : Domain.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IPrivateBudgetItemsStructureRepository
    Inherits IRepository(Of PrivateBudgetItemsStructure)

    ''' <summary>
    ''' Obtiene el registro por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPrivateBudgetItemsStructure(code As String, Optional tracking As Boolean = True) As PrivateBudgetItemsStructure

    ''' <summary>
    ''' Obtiene el registro por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPrivateBudgetItemsStructureById(id As String, Optional tracking As Boolean = True) As PrivateBudgetItemsStructure

End Interface
