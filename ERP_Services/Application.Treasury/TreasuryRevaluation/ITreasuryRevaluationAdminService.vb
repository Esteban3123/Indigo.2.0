'***********************************************************************
' Assembly         : Application.Treasury
' Author           : Giovanny Plazas Lozano
' Created          : 17-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities

Public Interface ITreasuryRevaluationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion para calcular y/o confirmar la revalorizacion
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function CalculateRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult)

End Interface