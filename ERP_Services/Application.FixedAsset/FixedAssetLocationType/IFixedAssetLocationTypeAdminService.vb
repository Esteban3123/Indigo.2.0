
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad tipos de ubicacion
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetLocationTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todos los tipos de ubicacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllLocationType() As List(Of FixedAssetLocationType)

End Interface
