'***********************************************************************
' Assembly         : Application.Base
' Author           : Juan Diego Diaz M.
' Created          : 16-11-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.SecurityRepository
Imports Infrastructure.CrossCutting.Base
Imports System.Configuration

#End Region

Public NotInheritable Class IndigoAuditBasic
#Region "Execution Thread"
    ''' <summary>
    ''' Ejecuta el proceso de auditoria basica en un hilo aparte
    ''' </summary>
    Public Shared Async Function Execute(Entity As String, Tag As String, idRegister As Integer, UserName As String, UserCode As String, UserMachine As String, FechaTransaccion As DateTime, Operacion As ActionsAudit, Company As String, Optional ContainerSecurity As String = "", Optional Parameters As String = "", Optional ReportName As String = "", Optional ByVal count As Integer = 1) As Task
        Await Task.Factory.StartNew(Sub()
                                        If ContainerSecurity = "" Then
                                            ContainerSecurity = ConfigurationManager.AppSettings(Infrastructure.CrossCutting.Base.ConfigurationFile.SECURITY_CONTAINER_PARAMETER_NAME).ToString
                                        End If
                                        Dim context As New GenesisEntities()
                                        context.CreateBasicAuditData(Entity, Tag, idRegister, UserName, UserCode, UserMachine, Parameters, ReportName, Operacion, FechaTransaccion, Company, ContainerSecurity, count)
                                    End Sub)
    End Function

#End Region

End Class
