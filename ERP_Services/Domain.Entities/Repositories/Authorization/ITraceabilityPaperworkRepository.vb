'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 02/06/2020
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface ITraceabilityPaperworkRepository
    Inherits IRepository(Of TraceabilityPaperwork)

    Function GetTraceabilityPaperworkById(id As Integer) As TraceabilityPaperwork

    Function SP_AssignTraceabilityPaperwork(xml As String) As SP_AssignTraceabilityPaperwork_Result

    Function SP_SaveTraceabilityPaperwork(xml As String) As SP_SaveTraceabilityPaperwork_Result

    Function SP_SaveAcceptanceAuthorization(xml As String) As SP_SaveAcceptanceAuthorization_Result

End Interface
