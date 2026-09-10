
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
Public Interface IBranchRepository
    Inherits IRepository(Of Branch)

    ''' <summary>
    ''' funcion que lista todas las sucursales
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllBranch() As List(Of Branch)
    ''' <summary>
    ''' consulta para retornar una sucursal teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeBranch">el codigo de la sucursal</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetBranch(ByVal codeBranch As String, Optional Tracking As Boolean = False) As Branch

    ''' <summary>
    ''' funcion para almacenar la sucursal
    ''' </summary>
    ''' <param name="Branch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveBranch(Branch As Branch) As Boolean


    ''' <summary>
    ''' funcion para listar todos los centros de costo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllCostCenter() As List(Of CostCenter)
End Interface
