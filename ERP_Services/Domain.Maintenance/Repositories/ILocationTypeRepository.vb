
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
Public Interface ILocationTypeRepository
    Inherits IRepository(Of LocationType)

    ''' <summary>
    ''' funcion que lista todas los tipos de ubicacion
    ''' </summary>
    ''' <returns>Lista tipos de ubicacion</returns>
    Function ListAllLocationType() As List(Of LocationType)
End Interface
