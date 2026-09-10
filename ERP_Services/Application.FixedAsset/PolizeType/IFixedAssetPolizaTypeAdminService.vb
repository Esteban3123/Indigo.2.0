
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad tipos de poliza
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetPolicyTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas los tipos de poliza
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPolizaType() As List(Of FixedAssetPolicyType)


    ''' <summary>
    ''' funcion que sirve para eliminar un tipo de poliza
    ''' </summary>
    ''' <param name="PolizaType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeletePolizaType(ByVal PolizaType As FixedAssetPolicyType, ByVal audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' funcion que sirve para guardar un tipo de poliza
    ''' </summary>
    ''' <param name="PolizaType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePolizaType(ByVal PolizaType As FixedAssetPolicyType, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetPolicyType)

    ''' <summary>
    ''' funciona que sirve para listar un tipo de PolizaType
    ''' </summary>
    ''' <param name="codePolizaType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPolizaType(ByVal codePolizaType As String, audit As AuditMessage) As FixedAssetPolicyType
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStatePoliza(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of FixedAssetPolicyType)
End Interface
