'***********************************************************************
' Assembly         : Infrastructure.Data.SecurityRepository
' Author           : Juan Diego Diaz M.
' Created          : 13-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Security.Entities
Imports Domain.Security
Imports Infrastructure.CrossCutting.Interface
Imports System.Configuration

#End Region

''' <summary>
''' Repositorio para Auditoria Basica
''' </summary>
Public Class BasicAuditRepository
    Implements IBasicAuditDataRepository

    'Devuelve el contexto en este repositorio 
    Private _context As ISeguridadUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia de <see cref="GroupRepository" /> clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As ISeguridadUnitOfWork)
        _context = contex
    End Sub

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
    ''' <returns>Un objeto CreateBasicAuditData_Result</returns>
    Public Function SaveBasicAudit(entity As String, form As Integer?, registerId As String, userName As String, userCode As String, userMachine As String, Parameters As String, ReportName As String, operation As Byte?, dateAudit As Date?, company As String, containerSecurity As String, Optional ByVal count As Nullable(Of Integer) = 1) As CreateBasicAuditData_Result Implements IBasicAuditDataRepository.SaveBasicAudit
        Dim BasicAudit = (From e In _context.CreateBasicAuditData(entity, form, registerId, userName, userCode, userMachine, Parameters, ReportName, operation, dateAudit, company, containerSecurity, count)
                         Select e).FirstOrDefault

        If BasicAudit IsNot Nothing Then
            Return BasicAudit
        Else
            Return New CreateBasicAuditData_Result
        End If
    End Function

    ''' <summary>
    ''' Obtiene el numero total de impresiones y exportación de una entidad
    ''' </summary>
    ''' <param name="entityName">Nombre de la entidad</param>
    ''' <param name="entityKey">Id de la entidad</param>
    ''' <returns>Número total de impresiones</returns>
   Public Function GetTotalPrint(entityName As String, entityKey As Integer, ByVal company As String) As Integer Implements IBasicAuditDataRepository.GetTotalPrint
        Using conx As New ConectionSQL(ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME))
            Dim dt As DataTable = conx.ExecuteCommand_Data("SELECT COUNT(Id) AS TOTAL FROM [Audit].[BasicAudit] WHERE [Company] = '" & company & "' AND [Operation] >= 7 AND  [Operation] <= 8 AND [Entity] = '" & entityName & "' AND [RegisterId] = " & entityKey)
            If dt.Rows.Count > 0 Then
                Return CInt(dt.Rows(0)("TOTAL"))
            End If
        End Using

        Return 0
    End Function
End Class


