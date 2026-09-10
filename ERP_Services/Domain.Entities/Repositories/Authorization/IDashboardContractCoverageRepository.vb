'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 01/07/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IDashboardContractCoverageRepository
    Inherits IRepository(Of ServiceOrderDetail)

    Function SP_SaveContractCoverage(xml As String) As SP_SaveContractCoverage_Result

End Interface
