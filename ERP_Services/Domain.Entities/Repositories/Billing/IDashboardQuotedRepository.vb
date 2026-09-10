'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/07/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IDashboardQuotedRepository
    Inherits IRepository(Of DashboardQuoted)

    Function SP_SaveDashboardQuoted(xml As String, userCode As String) As SP_SaveDashboardQuoted_Result

End Interface
