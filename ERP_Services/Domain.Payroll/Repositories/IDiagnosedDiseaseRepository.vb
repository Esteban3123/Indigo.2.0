'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IDiagnosedDiseaseRepository
    Inherits IRepository(Of DiagnosedDisease)

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="code">Código de la Enfermedad Diagnosticada</param>
    ''' <param name="tracking">Tracking</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    ''' <remarks></remarks>
    Function GetDiagnosedDisease(code As String, tracking As Boolean) As DiagnosedDisease

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada por ID
    ''' </summary>
    ''' <param name="ID">Id de la Enfermedad Diagnosticada</param>
    ''' <param name="tracking">Tracking</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    ''' <remarks></remarks>
    Function GetDiagnosedDiseaseById(ID As Integer, tracking As Boolean) As DiagnosedDisease


End Interface
