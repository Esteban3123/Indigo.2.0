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
Imports Domain.Entities
#End Region

''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetInsuranceRepository
    Inherits IRepository(Of FixedAssetInsurance)

    ''' <summary>
    ''' funcion que lista todas las aseguradoras
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllInsurance() As List(Of FixedAssetInsurance)
    ''' <summary>
    ''' consulta para retornar una aseguradora teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeInsurance">el codigo de la aseguradora</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetInsurance(ByVal codeInsurance As String, Optional Tracking As Boolean = False) As FixedAssetInsurance


End Interface
