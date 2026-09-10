
'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Hector Rodriguez R
' Created          : 21/11/2019
'
' Copyright        : (c) . All rights reserved.
' About            : PBI3499
'***********************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region

Public Interface ICashFlowReclassificationRepository
    Inherits IRepository(Of CashFlowReclassification)

    ''' <summary>
    ''' Obtiene un control de cheque por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCashFlowReclassificationById(id As Integer) As CashFlowReclassification
End Interface
