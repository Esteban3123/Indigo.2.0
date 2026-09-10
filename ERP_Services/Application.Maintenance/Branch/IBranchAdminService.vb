
#Region "Imports"
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Interface IBranchAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las sucursales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllBranch() As List(Of Branch)


    ''' <summary>
    ''' funcion que sirve para eliminar una sucursal
    ''' </summary>
    ''' <param name="Branch"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteBranch(ByVal Branch As Branch, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="Branch"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveBranch(ByVal Branch As Branch, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Branch)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="codeBranch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBranch(ByVal codeBranch As String) As Branch


    ''' <summary>
    ''' funcion para listar todos los centros de costo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllCostCenter() As List(Of CostCenter)

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Branch)
End Interface
