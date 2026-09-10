#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
<ServiceContract()>
Public Interface IPayrollLicensingConcepts

    ''' <summary>
    ''' Lista todos los conceptos de licencia
    ''' </summary>
    ''' <returns>Lista todos los conceptos de licencia</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function listAllLicensingConcepts(ByVal session As SessionValues) As List(Of LicensingConcepts)
    ''' <summary>
    ''' Elimina un concepto de licencia
    ''' </summary>
    ''' <param name="licensingConcepts">concepto de licencia</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteLicensingConcepts(ByVal licensingConcepts As LicensingConcepts, session As SessionValues) As ActionMessageResult(Of LicensingConcepts)
    ''' <summary>
    ''' Guarda o edita un concepto de licencia
    ''' </summary>
    ''' <param name="licensingConcepts">Concepto de licencia</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveLicensingConcepts(ByVal licensingConcepts As LicensingConcepts, session As SessionValues, idSequence As Long) As ActionResult(Of LicensingConcepts)
    ''' <summary>
    ''' Actualiza un concepto de licencia
    ''' </summary>
    ''' <param name="code">Código del concepto de licencia</param>
    ''' <returns> Salario minimo</returns>
    <OperationContract()>
    Function UpdateLicensingConcepts(code As String, status As Boolean, session As SessionValues) As ActionResult(Of LicensingConcepts)
    ''' <summary>
    ''' Obtiene un concepto de licencia por código
    ''' </summary>
    ''' <param name="code">Año</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLicensingConcepts(ByVal code As String, session As SessionValues) As LicensingConcepts
    ''' <summary>
    ''' Obtiene un concepto de licencia por Id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLicensingConceptsById(ByVal Id As Integer, session As SessionValues) As LicensingConcepts

End Interface
