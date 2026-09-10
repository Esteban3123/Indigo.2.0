Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Public Interface ILicensingConceptsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los conceptos de licencia 
    ''' </summary>
    ''' <returns>Lista de conceptos de licencia </returns>
    Function ListAllLicensingConcepts() As List(Of LicensingConcepts)

    ''' <summary>
    ''' Elimina un concepto de licencia
    ''' </summary>
    ''' <param name="licensingConcepts">concepto de licencia</param>
    ''' <returns></returns>
    Function DeleteLicensingConcepts(ByVal licensingConcepts As LicensingConcepts, ByVal audit As AuditMessage) As ActionMessageResult(Of LicensingConcepts)

    ''' <summary>
    ''' Guarda o edita un concepto de licencia 
    ''' </summary>
    ''' <param name="licensingConcepts">concepto de licencia</param>
    ''' <returns></returns>
    Function SavelicensingConcepts(ByVal licensingConcepts As LicensingConcepts, audit As AuditMessage, Optional idSequence As Int64 = 0) As ActionResult(Of LicensingConcepts)

    ''' <summary>
    ''' Obtiene un actualiza un salario minimo
    ''' </summary>
    ''' <param name="code">codigo del concepto de licencia</param>
    ''' <returns> Salario minimo</returns>
    Function UpdatelicensingConcepts(ByVal code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of LicensingConcepts)

    ''' <summary>
    ''' Obtiene un concepto de licencia por código
    ''' </summary>
    ''' <param name="code">código</param>
    ''' <returns></returns>
    Function GetLicensingConcepts(ByVal code As String, ByVal audit As AuditMessage) As LicensingConcepts

    ''' <summary>
    ''' Obtiene un concepto de licencia por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetLicensingConceptsById(ByVal Id As Integer) As LicensingConcepts
End Interface
