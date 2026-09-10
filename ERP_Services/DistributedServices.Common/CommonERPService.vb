'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 11-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Configuration
Imports System.Data.SqlClient
Imports System.ServiceModel.Activation
Imports DistributedServices.Authentication
Imports DistributedServices.Common
Imports Infrastructure.CrossCutting.Base

<JwtMessageServiceBehavior>
<ServiceBehavior(InstanceContextMode:=InstanceContextMode.PerCall)>
<AspNetCompatibilityRequirements(RequirementsMode:=AspNetCompatibilityRequirementsMode.Allowed)>
Public Class CommonERPService
    Implements ICommonERPService

    Public Function GetDataTable(procedureName As String, parameters As List(Of String()), session As SessionValues) As DataTable Implements ICommonERPService.GetDataTable
        Try
            Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "" _
                                     , session.TransactionalContainer)
            Using connection As New SqlConnection(conx)
                connection.Open()
                Dim command As New SqlCommand(procedureName)
                Try
                    command.Connection = connection
                    command.CommandType = CommandType.StoredProcedure
                    If parameters IsNot Nothing AndAlso parameters.Any() Then
                        For Each pr As String() In parameters
                            command.Parameters.Add(New SqlParameter("@" & pr(0), pr(1)))
                        Next
                    End If


                    Dim IndigoReader As SqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)

                    Dim dtDatos As New DataTable("SP_Report")
                    dtDatos.Load(IndigoReader, LoadOption.Upsert)
                    IndigoReader.Close()

                    Return dtDatos
                Catch ex As Exception
                    'IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
                    Return Nothing
                Finally
                    connection.Close()
                End Try
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
