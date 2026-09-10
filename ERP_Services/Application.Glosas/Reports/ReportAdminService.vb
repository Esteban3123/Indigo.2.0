#Region "Imports"

Imports System.Configuration
Imports System.Data.SqlClient
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions

#End Region

Public Class ReportAdminService
    Implements IReportAdminService

#Region "Builder"

    Public Sub New()
    End Sub

#End Region

#Region "Methods"

    Public Function GetReportListObjectionsReception(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IReportAdminService.GetReportListObjectionsReception
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [Glosas].[SP_ReportListObjectionsReception] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportListObjectionsReception")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Methods Privates"

    Private Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
        Dim connectionString = String.Empty
        If ConfigurationManager.ConnectionStrings("CONX_GENESIS_REPORTS") IsNot Nothing Then
            connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS_REPORTS, String.Empty, session.TransactionalContainer, False)
        Else
            connectionString = Infrastructure.CrossCutting.Base.Utils.GetEntityConnectionString(Infrastructure.CrossCutting.Base.ConfigurationFile.CONX_GENESIS, String.Empty, session.TransactionalContainer, False)
        End If
        Using conexion As New SqlConnection(connectionString)
            Try
                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If
                Dim da As SqlDataAdapter = New SqlDataAdapter(Comando, conexion)
                da.SelectCommand.CommandTimeout = 30000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", session)
                Return Nothing
            Finally
                conexion.Close()
            End Try
        End Using
    End Function

#End Region

#Region "IDisposable Support"

    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then

            End If

            IndigoGC.Execute()
        End If
        disposedValue = True
    End Sub

    ' Visual Basic agrega este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
