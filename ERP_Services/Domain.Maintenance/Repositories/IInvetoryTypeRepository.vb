
'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 08-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************



#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region


''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase de tipo de inventario
''' </summary>
''' <remarks></remarks>
Public Interface IInvetoryTypeRepository
    Inherits IRepository(Of InventoryType)

    ''' <summary>
    ''' funcion que lista todas los tipos de inventario
    ''' </summary>
    ''' <returns>Lista de tipos de inventarios</returns>
    Function ListAllInventoryType() As List(Of InventoryType)
    ''' <summary>
    ''' consulta para retornar un tipo de inventario teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeInventarioType">el codigo de tipo de inventario</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetInventoryType(ByVal codeInventarioType As String, Optional Tracking As Boolean = True) As InventoryType

    ''' <summary>
    ''' funcion para almacenar el tipo de inventario
    ''' </summary>
    ''' <param name="InventoryType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveInventoryType(InventoryType As InventoryType) As Boolean
End Interface
