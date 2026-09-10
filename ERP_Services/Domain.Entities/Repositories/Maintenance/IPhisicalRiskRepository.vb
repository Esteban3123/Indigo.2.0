
'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************



#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region



''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>

Public Interface IPhisicalRiskRepository
    Inherits IRepository(Of PhysicalRisk)

    ''' <summary>
    ''' funcion que lista todas los riesgos del equipo
    ''' </summary>
    ''' <returns>Lista los riesgos del equipo</returns>
    Function ListAllPhysicalRisk() As List(Of PhysicalRisk)

End Interface
