
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
Public Interface IConsumableRepository
    Inherits IRepository(Of Consumable)

    ''' <summary>
    ''' funcion que lista todas los Consumable
    ''' </summary>
    ''' <returns>Lista de Consumable</returns>
    Function ListAllConsumable() As List(Of Consumable)
    ''' <summary>
    ''' consulta para retornar un Consumable teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeConsumable">el codigo de la Consumable</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetConsumable(ByVal codeConsumable As String, Optional Tracking As Boolean = False) As Consumable

    ''' <summary>
    ''' funcion para almacenar un Consumable
    ''' </summary>
    ''' <param name="Consumable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveConsumable(Consumable As Consumable) As Boolean
End Interface
