'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cesar Collazos
' Created          : 01/12/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Billing.POCO
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Exceptions

#End Region

''' <summary>
''' Servicio para validación de mayoría de edad en procesos de liquidación y facturación
''' </summary>
Public Class LiquidationAgeValidationAdminService
    Implements ILiquidationAgeValidationAdminService

#Region "Constants"

    ''' <summary>
    ''' Edad mínima legal para facturación (18 años)
    ''' </summary>
    Private Const LEGAL_AGE As Integer = 18

    ''' <summary>
    ''' Tipo de persona natural (aplica validación de edad)
    ''' </summary>
    Private Const PERSON_TYPE_NATURAL As Byte = 1

#End Region

#Region "Fields"

    Private ReadOnly _thirdPartyRepository As IThirdPartyRepository
    Private ReadOnly _settingsBillingRepository As ISettingsBillingRepository
    Private ReadOnly _adacompanRepository As IAdacompanRepository

#End Region

#Region "Constructor"

    ''' <summary>
    ''' Inicializa una nueva instancia del servicio de validación de edad
    ''' </summary>
    ''' <param name="thirdPartyRepository">Repositorio de terceros</param>
    ''' <param name="settingsBillingRepository">Repositorio de configuración de facturación</param>
    ''' <param name="adacompanRepository">Repositorio de acompañantes/responsables de ingreso</param>
    Public Sub New(thirdPartyRepository As IThirdPartyRepository,
                   settingsBillingRepository As ISettingsBillingRepository,
                   adacompanRepository As IAdacompanRepository)
        _thirdPartyRepository = thirdPartyRepository
        _settingsBillingRepository = settingsBillingRepository
        _adacompanRepository = adacompanRepository
    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Valida si el tercero cumple con la mayoría de edad para facturación
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero a validar</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa para obtener parámetros</param>
    ''' <returns>Resultado de la validación con información del responsable sugerido si aplica</returns>
    Public Function ValidateAgeOfMajority(thirdPartyId As Integer, operativeUnitId As Integer) As ActionResult(Of AgeValidationResult) Implements ILiquidationAgeValidationAdminService.ValidateAgeOfMajority
        Try
            Dim result As New AgeValidationResult()

            ' Verificar si el parámetro está activo
            If Not IsAgeValidationEnabled(operativeUnitId) Then
                result.IsValid = True
                result.RequiresResponsible = False
                result.ValidationMessage = "La validación de mayoría de edad no está habilitada."
                Return New ActionResult(Of AgeValidationResult) With {
                    .StateResult = True,
                    .ObjectEmbbeded = result
                }
            End If

            ' Obtener información del tercero con la persona asociada
            Dim thirdParty = _thirdPartyRepository.FirstOrDefault(Function(t) t.Id = thirdPartyId, False, {"Person"})

            If thirdParty Is Nothing Then
                Return New ActionResult(Of AgeValidationResult) With {
                    .StateResult = False,
                    .Message = "No se encontró el tercero especificado."
                }
            End If

            ' Verificar si es persona jurídica (no aplica validación)
            If thirdParty.PersonType <> PERSON_TYPE_NATURAL Then
                result.IsValid = True
                result.IsLegalPerson = True
                result.RequiresResponsible = False
                result.ValidationMessage = "Es persona jurídica, no aplica validación de edad."
                Return New ActionResult(Of AgeValidationResult) With {
                    .StateResult = True,
                    .ObjectEmbbeded = result
                }
            End If

            ' Calcular edad
            Dim age = CalculateAge(thirdParty.Person?.BirthDate)
            result.Age = age

            ' Validar mayoría de edad
            If age >= LEGAL_AGE Then
                result.IsValid = True
                result.RequiresResponsible = False
                result.ValidationMessage = $"El tercero tiene {age} años, es mayor de edad."
            Else
                result.IsValid = False
                result.RequiresResponsible = True
                result.ValidationMessage = $"El paciente/responsable tiene {age} años y es menor de edad. Debe asignar un responsable mayor de 18 años."
            End If

            Return New ActionResult(Of AgeValidationResult) With {
                .StateResult = True,
                .ObjectEmbbeded = result
            }

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of AgeValidationResult) With {
                .StateResult = False,
                .Message = $"Error al validar mayoría de edad: {ex.Message}"
            }
        End Try
    End Function

    ''' <summary>
    ''' Valida si el tercero cumple con la mayoría de edad para facturación
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero a validar</param>
    ''' <param name="operativeUnitId">Id de la unidad operativa para obtener parámetros</param>
    ''' <param name="admissionNumber">Número de ingreso para buscar responsable sugerido</param>
    ''' <returns>Resultado de la validación con información del responsable sugerido si aplica</returns>
    Public Async Function ValidateAgeOfMajorityAsync(thirdPartyId As Integer, operativeUnitId As Integer, admissionNumber As String) As Threading.Tasks.Task(Of ActionResult(Of AgeValidationResult)) Implements ILiquidationAgeValidationAdminService.ValidateAgeOfMajorityAsync
        Return Await Threading.Tasks.Task.Run(Function()
                                                  Dim validationResult = ValidateAgeOfMajority(thirdPartyId, operativeUnitId)

                                                  ' Si requiere responsable, buscar sugerencia desde el ingreso
                                                  If validationResult.StateResult AndAlso
                                                     validationResult.ObjectEmbbeded IsNot Nothing AndAlso
                                                     validationResult.ObjectEmbbeded.RequiresResponsible AndAlso
                                                     Not String.IsNullOrEmpty(admissionNumber) Then

                                                      Dim suggestedResult = GetSuggestedResponsibleFromAdmission(admissionNumber)
                                                      If suggestedResult.StateResult AndAlso suggestedResult.ObjectEmbbeded.HasValue Then
                                                          Dim suggestedThirdParty = _thirdPartyRepository.FirstOrDefault(Function(t) t.Id = suggestedResult.ObjectEmbbeded.Value, False, {"Person"})
                                                          If suggestedThirdParty IsNot Nothing Then
                                                              ' Verificar que el responsable sugerido sea mayor de edad
                                                              Dim suggestedAge = CalculateAge(suggestedThirdParty.Person?.BirthDate)
                                                              If suggestedAge >= LEGAL_AGE Then
                                                                  validationResult.ObjectEmbbeded.SuggestedResponsibleThirdPartyId = suggestedThirdParty.Id
                                                                  validationResult.ObjectEmbbeded.SuggestedResponsibleName = suggestedThirdParty.Name
                                                                  validationResult.ObjectEmbbeded.SuggestedResponsibleNit = suggestedThirdParty.Nit
                                                              End If
                                                          End If
                                                      End If
                                                  End If

                                                  Return validationResult
                                              End Function)
    End Function

    ''' <summary>
    ''' Obtiene el tercero responsable sugerido desde la información del ingreso
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Id del tercero responsable si existe y está registrado en el sistema</returns>
    ''' <remarks>
    ''' Busca en la información de informantes del ingreso si existe un responsable registrado como tercero
    ''' </remarks>
    Public Function GetSuggestedResponsibleFromAdmission(admissionNumber As String) As ActionResult(Of Integer?) Implements ILiquidationAgeValidationAdminService.GetSuggestedResponsibleFromAdmission
        Try
            If String.IsNullOrWhiteSpace(admissionNumber) Then
                Return New ActionResult(Of Integer?) With {
                    .StateResult = True,
                    .ObjectEmbbeded = Nothing,
                    .Message = "No se proporcionó número de ingreso."
                }
            End If

            ' Responsable asociado en el ingreso (ADACOMPAN)
            Dim responsible = _adacompanRepository.GetResponsibleByAdmissionNumber(admissionNumber)

            If responsible Is Nothing OrElse String.IsNullOrWhiteSpace(responsible.IDACOMPAN) Then
                Return New ActionResult(Of Integer?) With {
                    .StateResult = True,
                    .ObjectEmbbeded = Nothing,
                    .Message = "No se encontró un responsable registrado en la información del ingreso."
                }
            End If

            ' Buscar en ThirdParty por el NIT del responsable
            Dim responsibleNit = responsible.IDACOMPAN.Trim()
            Dim thirdParty = _thirdPartyRepository.FirstOrDefault(Function(t) t.Nit = responsibleNit)

            If thirdParty IsNot Nothing Then
                Return New ActionResult(Of Integer?) With {
                    .StateResult = True,
                    .ObjectEmbbeded = thirdParty.Id,
                    .Message = $"Se encontró el responsable: {thirdParty.Name}"
                }
            Else
                ' El responsable existe en ADACOMPAN pero no está registrado como tercero en el sistema
                Return New ActionResult(Of Integer?) With {
                    .StateResult = True,
                    .ObjectEmbbeded = Nothing,
                    .Message = $"El responsable con identificación {responsibleNit} no está registrado como tercero en el sistema."
                }
            End If

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return New ActionResult(Of Integer?) With {
                .StateResult = False,
                .Message = $"Error al buscar responsable sugerido: {ex.Message}"
            }
        End Try
    End Function

    ''' <summary>
    ''' Verifica si el parámetro de validación de mayoría de edad está activo
    ''' </summary>
    ''' <param name="operativeUnitId">Id de la unidad operativa</param>
    ''' <returns>True si el parámetro está activo</returns>
    Public Function IsAgeValidationEnabled(operativeUnitId As Integer) As Boolean Implements ILiquidationAgeValidationAdminService.IsAgeValidationEnabled
        Try
            Dim settings = _settingsBillingRepository.FirstOrDefault(Function(s) s.IdOperatingUnit = operativeUnitId)
            Return settings IsNot Nothing AndAlso settings.ValidateAgeOfMajority

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Calcula la edad en años cumplidos
    ''' </summary>
    ''' <param name="birthDate">Fecha de nacimiento</param>
    ''' <returns>Edad en años cumplidos</returns>
    Public Function CalculateAge(birthDate As Date?) As Integer Implements ILiquidationAgeValidationAdminService.CalculateAge
        If Not birthDate.HasValue Then
            Return 0
        End If

        Dim today = Date.Today
        Dim age = today.Year - birthDate.Value.Year

        ' Ajustar si aún no ha cumplido años este año
        If birthDate.Value.Date > today.AddYears(-age) Then
            age -= 1
        End If

        Return Math.Max(0, age)
    End Function

    ''' <summary>
    ''' Verifica si el tercero es persona jurídica (no aplica validación de edad)
    ''' </summary>
    ''' <param name="thirdPartyId">Id del tercero</param>
    ''' <returns>True si es persona jurídica</returns>
    Public Function IsLegalPerson(thirdPartyId As Integer) As Boolean Implements ILiquidationAgeValidationAdminService.IsLegalPerson
        Try
            Dim thirdParty = _thirdPartyRepository.FirstOrDefault(Function(t) t.Id = thirdPartyId)
            Return thirdParty IsNot Nothing AndAlso thirdParty.PersonType <> PERSON_TYPE_NATURAL

        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
            Return False
        End Try
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' Dispose managed resources
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class

