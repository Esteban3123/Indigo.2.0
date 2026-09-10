
#Region "Imports"
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad aseguradora
''' </summary>
''' <remarks></remarks>
Public Interface IInsuranceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllInsurance() As List(Of Insurance)

    ''' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteInsurance(ByVal Insurance As Insurance, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveInsurance(ByVal Insurance As Insurance, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Insurance)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInsurance(ByVal codeInsurance As String) As Insurance

    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Insurance)

End Interface
