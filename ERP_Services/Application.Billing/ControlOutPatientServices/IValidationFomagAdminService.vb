Imports System.Threading.Tasks
Imports Domain.Base.Entities

Public Interface IValidationFomagAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Valida si el paciente tiene afiliacion al FOMAG
    ''' </summary>
    ''' <param name="codigoPaciente"></param>
    ''' <param name="companyNit"></param>
    ''' <returns></returns>
    Function ValidatePatientAsync(codigoPaciente As String, companyNit As String) As Task(Of ActionResult)
End Interface
