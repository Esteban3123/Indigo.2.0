'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities

Public Interface IProfessionalRiskRepository
    Inherits IRepository(Of ProfessionalRisk)

    ''' <summary>
    ''' Lista todos los Niveles de Riesgos Profesionales
    ''' </summary>
    ''' <returns>Lista de Riesgos Profesionales</returns>
    ''' <remarks></remarks>
    Function ListAllProfessionalRisk() As List(Of ProfessionalRisk)

    ''' <summary>
    ''' Obtiene un Riesgo Profesional
    ''' </summary>
    ''' <param name="code">Código del Riesgo Profesional</param>
    ''' <returns>Riesgo Profesional</returns>
    ''' <remarks></remarks>
    Function GetProfessionalRisk(ByVal code As String, Optional tracking As Boolean = True) As ProfessionalRisk

End Interface
