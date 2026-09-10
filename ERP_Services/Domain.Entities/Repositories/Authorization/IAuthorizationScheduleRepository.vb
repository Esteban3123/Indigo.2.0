'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/05/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IAuthorizationScheduleRepository
    Inherits IRepository(Of AuthorizationSchedule)

    Function GetAuthorizationScheduleById(id As Integer) As AuthorizationSchedule

    Function SP_SaveAuthorizationSchedule(xml As String) As SP_SaveAuthorizationSchedule_Result

End Interface
