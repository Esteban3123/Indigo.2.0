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
Imports Domain.Entities
#End Region

''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface IFixedAssetPolicyRepository
    Inherits IRepository(Of FixedAssetPolicy)

    ''' <summary>
    ''' funcion que lista todas las polizas
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllPoliza() As List(Of FixedAssetPolicy)
    ''' <summary>
    ''' consulta para retornar una poliza teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codePoliza">el codigo de la sucursal</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetPoliza(ByVal codePoliza As String, Optional ByVal tracking As Boolean = True) As FixedAssetPolicy
End Interface
