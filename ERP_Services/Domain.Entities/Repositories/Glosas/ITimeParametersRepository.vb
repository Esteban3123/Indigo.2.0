'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 24-06-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Domain.Base.Entities

#End Region

''' <summary>
''' Interfaz del repositorio de Parametros Tiempo.
''' </summary>
Public Interface ITimeParametersRepository
    Inherits IRepository(Of TimeParameters)
    ''' <summary>
    ''' Obtiene el registro por defecto de Parametros glosas
    ''' </summary>
    ''' <returns>Objeto Parametros Tiempo</returns>
    Function GetTimeParametersDefault(ByVal _IdIOperatingUnit As Integer) As TimeParameters
    ''' <summary>
    ''' Lista todos los registros de Parametros Tiempo.
    ''' </summary>
    ''' <returns>Lista de objetos de Parametros Tiempo</returns>
    Function ListAllTimeParameters(ByVal _IdIOperatingUnit As Integer, Optional Control As String = "") As List(Of TimeParameters)
    ''' <summary>
    ''' Obtiene un registro de Parametros Tiempo especifico.
    ''' </summary>
    ''' <param name="Id">El Id del registro Parametros Tiempo</param>
    ''' <returns>Objeto Parametros Tiempo</returns>
    Function GetTimeParameters(ByVal Id As String) As TimeParameters

    ''' <summary>
    ''' Obtiene el primer o unico parametro de tiempo
    ''' </summary>
    ''' <returns>Objeto Parametros Tiempo</returns>
    Function GetTimeParametersSingleOrDefault(ByVal _IdIOperatingUnit As Integer, Entity As String) As TimeParameters

    ''' <summary>
    ''' Obtiene una lista de registros para mostrar los limites de tiempos 
    ''' en cada etapa del proceso de glosas
    ''' </summary>
    ''' <param name="Invoice">Numero Factura</param>
    Function ListControlParametersTime(Invoice As String, Entity As String, ByVal _IdIOperatingUnit As Integer) As List(Of ControlParametersTime)


    Function GeneratePoortfolioReclasification(operatingUnitId As Integer, radicateInvoiceId As Integer, codeUser As String) As ActionResult

End Interface
