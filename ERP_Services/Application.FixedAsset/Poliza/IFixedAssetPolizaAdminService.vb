
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad poliza
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetPolicyAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas las polizas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPoliza() As List(Of FixedAssetPolicy)


    ''' <summary>
    ''' funcion que sirve para eliminar una poliza
    ''' </summary>
    ''' <param name="Poliza"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeletePoliza(ByVal Poliza As FixedAssetPolicy, ByVal audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' funcion que sirve para guardar una poliza
    ''' </summary>
    ''' <param name="Poliza"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePoliza(ByVal Poliza As FixedAssetPolicy, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetPolicy)

    ''' <summary>
    ''' funciona que sirve para listar una poliza
    ''' </summary>
    ''' <param name="codePoliza"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPoliza(ByVal codePoliza As String, audit As AuditMessage) As FixedAssetPolicy
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStatePoliza(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetPolicy)
End Interface
