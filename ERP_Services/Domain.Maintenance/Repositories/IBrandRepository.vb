
'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 02-04-2014
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
Public Interface IBrandRepository
    Inherits IRepository(Of Brand)

    ''' <summary>
    ''' funcion que lista todas las marcas
    ''' </summary>
    ''' <returns>Lista de marcas</returns>
    Function ListAllBrand() As List(Of Brand)
    ''' <summary>
    ''' consulta para retornar una marca teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeBrand">el codigo de la marca</param>
    ''' <returns>Objeto marca</returns>
    Function GetBrand(ByVal codeBrand As String) As Brand

    ''' <summary>
    ''' funcion para almacenar la marca
    ''' </summary>
    ''' <param name="Brand"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveBrand(Brand As Brand) As Boolean


End Interface
