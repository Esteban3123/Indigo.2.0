'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Giovanny Plazas Lozano
' Created          : 17-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base

#End Region

Public Class TreasuryRevaluationRepository
    Inherits BaseRepository
    Implements ITreasuryRevaluationRepository

#Region "Builder"

    'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para ejecutar el store procedure de la revaluacion
    ''' </summary>
    ''' <param name="Month"></param>
    ''' <param name="Year"></param>
    ''' <param name="Status"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function CalculateTreasuryRevaluation(Month As Integer, Year As Integer, Status As Integer, UserCode As String) As List(Of RevaluationResult) Implements ITreasuryRevaluationRepository.CalculateTreasuryRevaluation
        Dim parameters As IEnumerable(Of (String, Object)) = {
                    ("@month", Month), ("@year", Year), ("@status", Status), ("@userCode", UserCode)
                }

        Dim result = ExecuteStoredProcedure(Of RevaluationResult)("[Treasury].[SPCurrencyRevaluation]", parameters)

        Return result
    End Function
#End Region

End Class
