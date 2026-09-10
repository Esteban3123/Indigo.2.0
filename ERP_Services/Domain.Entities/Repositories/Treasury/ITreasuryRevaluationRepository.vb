'***********************************************************************
' Assembly         : Domain.Treasury
' Author           : Giovanny Plazas Lozano
' Created          : 17-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

#End Region

Public Interface ITreasuryRevaluationRepository

    Function CalculateTreasuryRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult)

End Interface
