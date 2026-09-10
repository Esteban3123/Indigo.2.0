
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

Public Interface IEquipmentAllRepository
    Inherits IRepository(Of Equipment)

    ''' <summary>
    ''' funcion que lista todas los equipos
    ''' </summary>
    ''' <returns>Lista de areas</returns>
    Function ListAllEquipment() As List(Of Equipment)
    ''' <summary>
    ''' consulta para retornar un equipo
    ''' </summary>
    ''' <param name="codeEquipment">el codigo del equipo</param>
    ''' <returns>Objeto area</returns>
    Function GetEquipment(ByVal codeEquipment As String, Optional Tracking As Boolean = False) As Equipment

    ''' <summary>
    ''' funcion para almacenar un equipo
    ''' </summary>
    ''' <param name="Equipment"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveEquipment(Equipment As Equipment) As Boolean
End Interface
