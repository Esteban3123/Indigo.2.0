'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Giovanny Plazas
' Created          : 17-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
#End Region

<ServiceContract()>
Public Interface ITreasuryServiceRevaluation

    <OperationContract()>
    Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult)

End Interface
