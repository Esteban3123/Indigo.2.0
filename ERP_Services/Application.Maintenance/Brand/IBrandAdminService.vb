
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre marcas
''' </summary>
''' <remarks></remarks>
Public Interface IBrandAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas las marcas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllBrand() As List(Of Brand)


    ''' <summary>
    ''' funcion que sirve para eliminar una marca
    ''' </summary>
    ''' <param name="Brand"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteBrand(ByVal Brand As Brand, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar una marca
    ''' </summary>
    ''' <param name="Brand"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveBrand(ByVal Brand As Brand, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funciona que sirve para listar una marca
    ''' </summary>
    ''' <param name="codeBrand"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBrand(ByVal codeBrand As String) As Brand

End Interface
