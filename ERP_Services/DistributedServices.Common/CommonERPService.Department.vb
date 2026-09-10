#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Application.Common
Imports Infrastructure.CrossCutting.IOC
Imports Domain.Entities

Imports Domain.Base.Entities

#End Region


Partial Class CommonERPService

    Public Function DeleteDepartment(department As Department, session As SessionValues) As ActionMessageResult(Of Department) Implements ICommonERPService.DeleteDepartment
        Using DepartmentAdmin As IDepartmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDepartmentAdminService)()
            Return DepartmentAdmin.DeleteDepartment(department, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetDepartment(code As String, IdCountry As String, session As SessionValues) As Department Implements ICommonERPService.GetDepartment
        Using DepartmentAdmin As IDepartmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDepartmentAdminService)()
            Return DepartmentAdmin.GetDepartment(code, IdCountry)
        End Using
    End Function

    Public Function GetDepartments(IdCountry As String, session As SessionValues) As List(Of Department) Implements ICommonERPService.GetDepartments
        Using DepartmentAdmin As IDepartmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDepartmentAdminService)()
            Return DepartmentAdmin.GetDepartments(IdCountry)
        End Using
    End Function

    Public Function ListAllDepartment(session As SessionValues) As List(Of Department) Implements ICommonERPService.ListAllDepartment
        Using DepartmentAdmin As IDepartmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDepartmentAdminService)()
            Return DepartmentAdmin.ListAllDepartment()
        End Using
    End Function

    Public Function SaveDepartment(department As Department, session As SessionValues) As ActionResult(Of Department) Implements ICommonERPService.SaveDepartment
        Using DepartmentAdmin As IDepartmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDepartmentAdminService)()
            Return DepartmentAdmin.SaveDepartment(department, session.AuditMessageWcf)
        End Using
    End Function

    Public Function GetDepartmentById(ByVal idDepartment As Integer, session As SessionValues) As Department Implements ICommonERPService.GetDepartmentById
        Using DepartmentAdmin As IDepartmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDepartmentAdminService)()
            Return DepartmentAdmin.GetDepartmentById(idDepartment, session.AuditMessageWcf)
        End Using
    End Function


    ''' <summary>
    ''' Metodo que cambia el estado del registro
    ''' </summary>
    ''' <param name="code">Codigo Ciudad</param>
    ''' <returns>booleano</returns>
    ''' <remarks></remarks>
    Public Function ChangeStateDepartment(code As String, IdCountry As String, state As Boolean, session As Infrastructure.CrossCutting.Base.SessionValues) As ActionResult(Of Department) Implements ICommonERPService.ChangeStateDepartment
        Using DepartmentService As IDepartmentAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IDepartmentAdminService)()
            Return DepartmentService.ChangeStateDepartment(code, IdCountry, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
