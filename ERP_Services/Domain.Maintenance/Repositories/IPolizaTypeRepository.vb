
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
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del tipo de poliza
''' </summary>
''' <remarks></remarks>
Public Interface IPolizaTypeRepository
    Inherits IRepository(Of PolizaType)

    ''' <summary>
    ''' funcion que lista todas los tipos de poliza
    ''' </summary>
    ''' <returns>Lista de tipos de poliza</returns>
    Function ListAllPolizaType() As List(Of PolizaType)
    ''' <summary>
    ''' consulta para retornar un tipo de poliza teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codepolizatype">el codigo del tipo de poliza</param>
    ''' <returns>Objeto tipo poliza</returns>
    Function GetPolizaType(ByVal codepolizatype As String, Optional tracking As Boolean = True) As PolizaType

    ''' <summary>
    ''' funcion para almacenar el tipo de poliza
    ''' </summary>
    ''' <param name="PolizaType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePolizaType(PolizaType As PolizaType) As Boolean
End Interface
