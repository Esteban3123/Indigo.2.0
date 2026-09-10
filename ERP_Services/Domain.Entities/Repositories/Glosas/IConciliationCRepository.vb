'************************************************************
' Assembly         : Domain.Glosas
' Author           : Juan Diego Diaz
' Created          : 22-05-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base

#End Region

''' <summary>
''' Interfaz del repositorio de Conciliación Cabeceras.
''' </summary>
Public Interface IConciliationCRepository
    Inherits IRepository(Of ConciliationC)

    ''' <summary>
    ''' Obtiene una cabecera de conciliacion especifica.
    ''' </summary>
    ''' <param name="Id">El Id de la conciliación cabecera</param>
    ''' <returns>Objeto Conciliación Cabecera</returns>
    Function GetConciliationC(ByVal Id As String, Optional ByVal tracking As Boolean = True) As ConciliationC

    ''' <summary>
    ''' Obtiene una cabecera de conciliacion especifica.
    ''' </summary>
    ''' <param name="Consecutive">Consecutivo de la conciliación cabecera</param>
    ''' <returns>Objeto Conciliación Cabecera</returns>
    Function GetConciliationCByConsecutive(ByVal Consecutive As String) As ConciliationC

    ''' <summary>
    ''' Guarda y confirma una conciliación
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Function SP_SaveConciliation(xml As String, UserCode As String) As SP_SaveConciliation_Result

End Interface
