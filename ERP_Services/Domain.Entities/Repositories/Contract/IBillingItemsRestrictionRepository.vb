'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IBillingItemsRestrictionRepository
    Inherits IRepository(Of BillingItemsRestriction)

    ''' <summary>
    ''' Obtiene una definicion de tarifa por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingItemsRestriction(code As String) As BillingItemsRestriction

    ''' <summary>
    ''' Obtiene una definicion de tarifa por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingItemsRestrictionById(id As Integer) As BillingItemsRestriction

End Interface
