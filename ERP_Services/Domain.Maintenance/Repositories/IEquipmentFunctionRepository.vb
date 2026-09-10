
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

Public Interface IEquipmentFunctionRepository
    Inherits IRepository(Of EquipmentFunction)

    ''' <summary>
    ''' funcion que lista todas las funciones del equipo
    ''' </summary>
    ''' <returns>Lista de funciones del equipo</returns>
    Function ListAllEquipmentFunction() As List(Of EquipmentFunction)

End Interface
