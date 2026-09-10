'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IProfessionalRiskAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista los Riesgos Profesionales
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
    Function GetProfessionalRisk(ByVal code As String) As ProfessionalRisk

    ''' <summary>
    ''' Almacena o Actualiza un Riesgo Profesional
    ''' </summary>
    ''' <param name="professionalRisk">Objeto Riesgo Profesional</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveProfessionalRisk(ByVal professionalRisk As ProfessionalRisk, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Elimina un Riesgo Profesional
    ''' </summary>
    ''' <param name="professionalRisk">Objeto Riesgo Profesional
    ''' </param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteProfessionalRisk(ByVal professionalRisk As ProfessionalRisk, ByVal audit As AuditMessage) As ActionMessageResult(Of ProfessionalRisk)

End Interface
