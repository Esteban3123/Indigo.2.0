
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
Public Interface ILocationRespository
    Inherits IRepository(Of Location)

    ''' <summary>
    ''' funcion que lista todas los ubicaciones
    ''' </summary>
    ''' <returns>Lista de ubicaciones</returns>
    Function ListAllLocation() As List(Of Location)
    ''' <summary>
    ''' consulta para retornar una ubicacion teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeLocation">el codigo de la ubicacion</param>
    ''' <returns>Objeto ubicacion</returns>
    Function GetLocation(ByVal codeLocation As String) As Location

    ''' <summary>
    ''' funcion para almacenar una ubicacion
    ''' </summary>
    ''' <param name="Location"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveLocation(Location As Location) As Boolean

    ''' <summary>
    ''' funcion para almacenar todos los tipos de ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListLocation() As List(Of Location)
End Interface
