'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cesar Collazos
' Created          : 01/12/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Billing.POCO

''' <summary>
''' Interface para el servicio de validación de mayoría de edad en liquidación
''' </summary>
Public Interface ILiquidationAgeValidationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Valida si el tercero cumple con la mayoría de edad para facturación
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero a validar</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa para obtener parámetros</param>
    ''' <returns>Resultado de la validación con información del responsable sugerido si aplica</returns>
    Function ValidateAgeOfMajority(thirdPartyId As Integer, operativeUnitId As Integer) As ActionResult(Of AgeValidationResult)

    ''' <summary>
    ''' Valida si el tercero cumple con la mayoría de edad para facturación (versión asíncrona)
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero a validar</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa para obtener parámetros</param>
    ''' <param name="admissionNumber">Número de ingreso para buscar responsable sugerido</param>
    ''' <returns>Resultado de la validación con información del responsable sugerido si aplica</returns>
    Function ValidateAgeOfMajorityAsync(thirdPartyId As Integer, operativeUnitId As Integer, admissionNumber As String) As Threading.Tasks.Task(Of ActionResult(Of AgeValidationResult))

    ''' <summary>
    ''' Obtiene el tercero responsable sugerido desde la información del ingreso
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Id del tercero responsable si existe y está registrado en el sistema</returns>
    Function GetSuggestedResponsibleFromAdmission(admissionNumber As String) As ActionResult(Of Integer?)

    ''' <summary>
    ''' Verifica si el parámetro de validación de mayoría de edad está activo
    ''' </summary>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <returns>True si el parámetro está activo</returns>
    Function IsAgeValidationEnabled(operativeUnitId As Integer) As Boolean

    ''' <summary>
    ''' Calcula la edad en años cumplidos
    ''' </summary>
    ''' <param name="birthDate">Fecha de nacimiento</param>
    ''' <returns>Edad en años cumplidos</returns>
    Function CalculateAge(birthDate As Date?) As Integer

    ''' <summary>
    ''' Verifica si el tercero es persona jurídica (no aplica validación de edad)
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero</param>
    ''' <returns>True si es persona jurídica</returns>
    Function IsLegalPerson(thirdPartyId As Integer) As Boolean

End Interface

