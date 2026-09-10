
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
Public Interface ITechnicalLogRepository
    Inherits IRepository(Of TechnicalLog)

    ''' <summary>
    ''' funcion que lista todas los registros tecnicos
    ''' </summary>
    ''' <returns>Lista de registros tecnicos</returns>
    Function ListAllTechnicalLog() As List(Of TechnicalLog)
    ''' <summary>
    ''' consulta para retornar un registro tecnico teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeTechnicalLog">el codigo del registro tecnico</param>
    ''' <returns>Objeto Compañia</returns>
    Function GetTechnicalLog(ByVal codeTechnicalLog As String, Optional tracking As Boolean = True) As TechnicalLog

    ''' <summary>
    ''' funcion para almacenar un registro tecnico
    ''' </summary>
    ''' <param name="TechnicalLog"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveTechnicalLog(TechnicalLog As TechnicalLog) As Boolean
End Interface
