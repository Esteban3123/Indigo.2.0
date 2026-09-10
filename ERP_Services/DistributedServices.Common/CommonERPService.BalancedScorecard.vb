Imports System.Configuration
Imports System.Data.SqlClient
Imports DistributedServices.Common
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base

Partial Public Class CommonERPService
    Implements ICommonERPBalancedScorecard

    Public Function SaveBalancedScorecard(Id As Integer, name As String, data As String, session As SessionValues) As ActionResult Implements ICommonERPBalancedScorecard.SaveBalancedScorecard
        Dim conx = String.Format(ConfigurationManager.ConnectionStrings(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS).ConnectionString, "", session.TransactionalContainer)
        Using sqlWebConexion As New SqlConnection(conx)
            Try
                Dim query As String = $"SELECT * FROM [Common].[BalancedScorecard] WHERE Id = {Id}"
                Dim da As SqlDataAdapter = New SqlDataAdapter(query, sqlWebConexion)
                da.SelectCommand.CommandTimeout = 90
                If sqlWebConexion.State = ConnectionState.Closed Then
                    sqlWebConexion.Open()
                End If
                Dim ds As New DataSet
                da.Fill(ds, "Datos")
                Dim dtResult As DataTable = ds.Tables("Datos")
                da = Nothing
                ds = Nothing
                If dtResult.Rows.Count = 0 Then
                    query = $"INSERT INTO [Common].[BalancedScorecard] (Name, Value, CreationUser, CreationDate) VALUES('{name}', '{data}', '{session.UserIndigo}', GETDATE())"
                Else
                    query = $"UPDATE [Common].[BalancedScorecard] SET Value = '{data}', ModificationUser = '{session.UserIndigo}', ModificationDate = GETDATE() WHERE Id = {Id}"
                End If
                Using SQL As New SqlCommand(query, sqlWebConexion)
                    'aumento el tiempo
                    SQL.CommandTimeout = 90
                    'tipo de comando
                    SQL.CommandType = CommandType.Text
                    Try
                        If sqlWebConexion.State = ConnectionState.Closed Then
                            sqlWebConexion.Open()
                        End If
                        'Ejecutar
                        SQL.ExecuteNonQuery()
                        Return New ActionResult(True, "")
                    Catch ex As Exception
                        Throw ex
                    Finally
                        sqlWebConexion.Close()
                    End Try
                End Using
            Catch ex As Exception
                Return New ActionResult(False, ex.Message)
            End Try
        End Using
    End Function
End Class
