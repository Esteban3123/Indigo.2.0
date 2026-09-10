
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre el riesgo del equipo
''' </summary>
''' <remarks></remarks>
Public Interface IPhysicalRiskAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todas los riesgos del equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllPhysicalRisk() As List(Of PhysicalRisk)

End Interface
