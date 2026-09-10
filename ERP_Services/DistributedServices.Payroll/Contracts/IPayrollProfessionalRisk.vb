'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()> _
Public Interface IPayrollProfessionalRisk

    ''' <summary>
    ''' Lista todos los Riesgos Profesionales
    ''' </summary>
    ''' <returns>Lista de Riesgos Profesionales</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllProfessionalRisk(session As SessionValues) As List(Of ProfessionalRisk)

    ''' <summary>
    ''' Obtiene un Riesgo Profesional
    ''' </summary>
    ''' <param name="code">Código del Riesgo Profesional</param>
    ''' <returns>Riesgo Profesional</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetProfessionalRisk(ByVal code As String, session As SessionValues) As ProfessionalRisk

    ''' <summary>
    ''' Almacena o Actualiza un Riesgo Profesional
    ''' </summary>
    ''' <param name="professionalRisk">Riesgo Profesional</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function SaveProfessionalRisk(ByVal professionalRisk As ProfessionalRisk, session As SessionValues) As Boolean

    ''' <summary>
    ''' Elimina un Riesgo Profesional
    ''' </summary>
    ''' <param name="professionalRisk">Riesgo Profesional</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeleteProfessionalRisk(ByVal professionalRisk As ProfessionalRisk, session As SessionValues) As ActionMessageResult(Of ProfessionalRisk)

End Interface
