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
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface IPolizaRepository
    Inherits IRepository(Of Poliza)

    ''' <summary>
    ''' funcion que lista todas las polizas
    ''' </summary>
    ''' <returns>Lista de Compañias</returns>
    Function ListAllPoliza() As List(Of Poliza)
    ''' <summary>
    ''' consulta para retornar una poliza teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codePoliza">el codigo de la sucursal</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetPoliza(ByVal codePoliza As String, Optional ByVal tracking As Boolean = True) As Poliza

    ''' <summary>
    ''' funcion para almacenar la poliza
    ''' </summary>
    ''' <param name="Poliza"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePoliza(Poliza As Poliza) As Boolean
End Interface
