'***********************************************************************
' Assembly         : Domain.Security
' Author           : Juan Diego Diaz M.
' Created          : 12-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Base
Imports System.Dynamic
#End Region
Public Interface IBasicAuditDataRepository

    ''' <summary>
    ''' Función para guardar auditoria
    ''' </summary>
    ''' <param name="entity">Nombre de la Entidad</param>
    ''' <param name="form">Formulario</param>
    ''' <param name="registerId">Id de la Entidad</param>
    ''' <param name="userName">Nombre de Usuario</param>
    ''' <param name="userCode">Código de Usuario</param>
    ''' <param name="operation">Operación</param>
    ''' <param name="dateAudit">Fecha Auditoria</param>
    ''' <param name="company">Compañia</param>
    ''' <param name="count">Cantidad de registros del mismo tipo a insertar</param>
    ''' <returns>Un objeto CreateBasicAuditData_Result</returns>
    Function SaveBasicAudit(entity As String, form As Integer?, registerId As String, userName As String, userCode As String, userMachine As String, Parameters As String, ReportName As String, operation As Byte?, dateAudit As Date?, company As String, containerSecurity As String, Optional ByVal count As Nullable(Of Integer) = 1) As CreateBasicAuditData_Result

    ''' <summary>
    ''' Obtiene el numero total de impresiones y exportación de una entidad
    ''' </summary>
    ''' <param name="entityName">Nombre de la entidad</param>
    ''' <param name="entityKey">Id de la entidad</param>
    ''' <returns>Número total de impresiones</returns>
    Function GetTotalPrint(ByVal entityName As String, ByVal entityKey As Integer, ByVal company As String) As Integer

End Interface
