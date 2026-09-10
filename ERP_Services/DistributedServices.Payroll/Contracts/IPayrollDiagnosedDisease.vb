'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

<ServiceContract()>
Public Interface IPayrollDiagnosedDisease

    ''' <summary>
    ''' Elimina una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteDiagnosedDisease(ByVal diagnosedDisease As DiagnosedDisease, session As SessionValues) As ActionResult

    ''' <summary>
    ''' Almacena una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveDiagnosedDisease(ByVal diagnosedDisease As DiagnosedDisease, session As SessionValues, ByVal idSequense As Int64) As ActionResult(Of DiagnosedDisease)

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="code">Código de la Enfermedad Diagnosticada</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDiagnosedDisease(ByVal code As String, ByVal tracking As Boolean, session As SessionValues) As ActionResult(Of DiagnosedDisease)

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada por ID
    ''' </summary>
    ''' <param name="ID">ID de la Enfermedad Diagnosticada</param>
    ''' <param name="session">Objeto con valores de la sesión</param>
    ''' <returns>Enfermedad Diagnosticada</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetDiagnosedDiseaseById(ByVal ID As String, ByVal tracking As Boolean, session As SessionValues) As ActionResult(Of DiagnosedDisease)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeStateDiagnosedDisease(ByVal code As String, ByVal state As Boolean, session As SessionValues) As ActionResult(Of DiagnosedDisease)

End Interface
