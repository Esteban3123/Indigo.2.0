'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IBillingGroupRepository
    Inherits IRepository(Of BillingGroup)
    ''' <summary>
    ''' obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingGroupByCode(code As String) As BillingGroup
    ''' <summary>
    ''' obtiene un grupo por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBillingGroupById(id As Integer) As BillingGroup
End Interface
