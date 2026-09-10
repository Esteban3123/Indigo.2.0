
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la historia del equipo
''' </summary>
''' <remarks></remarks>
Public Interface IEquipmentHistoryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas las historias del equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllEquipmentHistory() As List(Of EquipmentHistory)


End Interface
