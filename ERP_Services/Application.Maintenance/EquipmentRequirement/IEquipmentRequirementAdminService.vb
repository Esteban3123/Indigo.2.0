
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la historia del equipo
''' </summary>
''' <remarks></remarks>
Public Interface IEquipmentRequirementAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas los requerimientos del equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllEquipmentRequirement() As List(Of EquipmentRequirement)
End Interface
