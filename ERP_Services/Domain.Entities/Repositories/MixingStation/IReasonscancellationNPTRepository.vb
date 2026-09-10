'***********************************************************************
' Assembly         : Domain.MixingStation
' Author           : Andres Alarcon
' Created          : 24/10/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IReasonscancellationNPTRepository
    Inherits IRepository(Of ReasonscancellationNPT), Inject

End Interface