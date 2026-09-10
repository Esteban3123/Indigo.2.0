
'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Hector Rodriguez R
' Created          : 11-04-2019
'
' Copyright        : (c) . All rights reserved.
' About            : PBI3499
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ICheckCashingControlRepository
    Inherits IRepository(Of CheckCashingControl)

    ''' <summary>
    ''' Obtiene un control de cheque por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCheckCashingControlById(id As Integer) As CheckCashingControl
End Interface
