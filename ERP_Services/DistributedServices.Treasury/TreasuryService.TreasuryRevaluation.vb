'***********************************************************************
' Assembly         : DistributedServices.Treasury
' Author           : Giovanny Plazas
' Created          : 17-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Treasury
Imports Domain.Entities
Imports Microsoft.Practices.Unity
#End Region

Partial Class TreasuryService

#Region "Methods"

    Public Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements ITreasuryService.CalculateRevaluation
        Using service As ITreasuryRevaluationAdminService = Container.Current.Resolve(Of ITreasuryRevaluationAdminService)()
            Return service.CalculateRevaluation(Month, Year, Status, UserCode)
        End Using
    End Function

#End Region

End Class
