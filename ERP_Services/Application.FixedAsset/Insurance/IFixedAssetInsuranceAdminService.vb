
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad aseguradora
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetInsuranceAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todas las aseguradoras
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllInsurance() As List(Of FixedAssetInsurance)


    ''' <summary>
    ''' funcion que sirve para eliminar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteInsurance(ByVal Insurance As FixedAssetInsurance, ByVal audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' funcion que sirve para guardar una aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveInsurance(ByVal Insurance As FixedAssetInsurance, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of FixedAssetInsurance)

    ''' <summary>
    ''' funciona que sirve para listar una aseguradora
    ''' </summary>
    ''' <param name="codeInsurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetInsurance(ByVal codeInsurance As String) As FixedAssetInsurance

    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetInsurance)

End Interface
