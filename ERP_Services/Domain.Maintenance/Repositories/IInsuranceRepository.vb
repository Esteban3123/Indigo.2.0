'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 04-08-2013
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
Public Interface IInsuranceRepository
    Inherits IRepository(Of Insurance)


    ''' <summary>
    ''' funcion que lista todas las aseguradoras
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllInsurance() As List(Of Insurance)
    ''' <summary>
    ''' consulta para retornar una aseguradora teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeInsurance">el codigo de la aseguradora</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetInsurance(ByVal codeInsurance As String, Optional Tracking As Boolean = False) As Insurance

    ''' <summary>
    ''' funcion para almacenar la aseguradora
    ''' </summary>
    ''' <param name="Insurance"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveInsurance(Insurance As Insurance) As Boolean

End Interface
