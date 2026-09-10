
#Region "Imports"
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Interface ILocationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todos las ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllLocation() As List(Of Location)


    ''' <summary>
    ''' funcion que sirve para eliminar una ubicacion
    ''' </summary>
    ''' <param name="Location"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteLocation(ByVal Location As Location, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar una ubicacion
    ''' </summary>
    ''' <param name="Location"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveLocation(ByVal Location As List(Of Location), ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funciona que sirve para listar una ubicacion
    ''' </summary>
    ''' <param name="codeLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLocation(ByVal codeLocation As String) As Location

    ''' <summary>
    ''' funcion para almacenar todos las ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListLocation() As List(Of Location)
End Interface
