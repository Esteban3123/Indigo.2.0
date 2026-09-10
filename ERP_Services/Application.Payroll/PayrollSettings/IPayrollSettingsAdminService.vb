#Region "Imports"
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IPayrollSettingsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPayrollSettings() As PayrollSettings

    ''' <summary>
    ''' funcion que sirve para eliminar los Parámetros de Nómina
    ''' </summary>
    ''' <param name="PayrollSettings">PayrollSettings</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePayrollSettings(ByVal PayrollSettings As PayrollSettings, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="PayrollSettings">PayrollSettings</param>
    ''' <param name="audit"></param>
    ''' <returns>ActionResult(Of PayrollSettings)</returns>
    ''' <remarks></remarks>
    Function SavePayrollSettings(ByVal PayrollSettings As PayrollSettings, ByVal audit As AuditMessage) As ActionResult(Of PayrollSettings)

End Interface
