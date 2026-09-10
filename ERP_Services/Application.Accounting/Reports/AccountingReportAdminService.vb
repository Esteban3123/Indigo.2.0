#Region "Imports"

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Configuration
Imports System.Data.SqlClient
Imports System.Text

#End Region

Public Class AccountingReportAdminService
    Implements IAccountingReportAdminService

#Region "Builder"

    Private _FormatoExogena As IFormatosExogena

    Public Sub New(FormatoExogena As IFormatosExogena)
        _FormatoExogena = FormatoExogena
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportBalances] realizado para cargar los datos del reporte balance de pruebas
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportBalances(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet Implements IAccountingReportAdminService.GetReportBalances
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportBalances] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportBalances")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportThirdPartyBalance] realizado para cargar los datos del reporte saldos de terceros
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportThirdPartyBalance(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet Implements IAccountingReportAdminService.GetReportThirdPartyBalance
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportThirdPartyBalance] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportThirdPartyBalance")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportResulStatus] realizado para cargar los datos del reporte de resultados
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportResulStatus(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet Implements IAccountingReportAdminService.GetReportResulStatus
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportResulStatus] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportResulStatus")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure [GeneralLedger].[SP_ReportResulStatusComparative] realizado para cargar los datos del reporte de resultados comparativo
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportResulStatusComparative(criterias As Dictionary(Of String, String), Session As Infrastructure.CrossCutting.Base.SessionValues) As DataSet Implements IAccountingReportAdminService.GetReportResulStatusComparative
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportResulStatusComparative] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportResulStatusComparative")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para generar la informacion de exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportExogenousFormat(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReportAdminService.GetReportExogenousFormat
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportExogenousFormat] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportExogenousFormat")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para generar la informacion de exogena
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GenerateExogenaFormats(criterias As Dictionary(Of String, String), Session As SessionValues) As ActionResult(Of String) Implements IAccountingReportAdminService.GenerateExogenaFormats
        Try
            Dim Format As String = criterias("Format")
            Dim result As New ActionResult(Of String) With {.StateResult = True, .Message = Format}

            Select Case Format
                Case "1001"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1001(criterias)
                Case "1003"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1003(criterias)
                Case "1004"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1004(criterias)
                Case "1005"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1005(criterias)
                Case "1006"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1006(criterias)
                Case "1007"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1007(criterias)
                Case "1008"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1008(criterias)
                Case "1009"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1009(criterias)
                Case "1010"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1010(criterias)
                Case "1011"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1011(criterias)
                Case "1012"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1012(criterias)
                Case "1056"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1056(criterias)
                Case "1647"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat1647(criterias)
                Case "2275"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat2275(criterias)
                Case "2276"
                    result.ObjectEmbbeded = _FormatoExogena.GenerarExogenaXMLFormat2276(criterias)
                Case Else
                    result.StateResult = False
                    result.Message = "No se generó ningun archivo"
            End Select

            Return result
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return New ActionResult(Of String) With {.StateResult = False, .Message = Utils.GetInnerExceptionMessageToString(ex)}
        End Try
    End Function

    ''' <summary>
    ''' Funcion para generar la circular unica
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportSingleCircular(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReportAdminService.GetReportSingleCircular
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportSingleCircular] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportSingleCircular")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Funcion para consultar el reporte de conciliación de módulos
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetReportReconcileModule(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingReportAdminService.GetReportReconcileModule
        Try
            Dim xmlCriterias = Utils.DictionaryToXML(criterias)

            Dim ds As New DataSet
            Dim query As String = "EXEC [GeneralLedger].[SP_ReportReconcileModule] '" & xmlCriterias & "'"
            Dim dt = Me.GetDatatable(query, Session, "ReportReconcileModule")
            ds.Tables.Add(dt.Copy())
            Return ds
        Catch ex As Exception
            IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy", Session)
            Return Nothing
        End Try
    End Function

#End Region

#Region "Methods Privates"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Comando"></param>
    ''' <param name="session"></param>
    ''' <param name="nameDt"></param>
    ''' <returns></returns>
    Public Function GetDatatable(ByVal Comando As String, session As SessionValues, nameDt As String) As System.Data.DataTable
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
                da.SelectCommand.CommandTimeout = 36000
                Dim ds As New DataSet
                da.Fill(ds, nameDt)
                GetDatatable = ds.Tables(nameDt)
                da = Nothing
                ds = Nothing
                conexion.Close()
                Return GetDatatable
            Catch ex As Exception
                IndigoManagementExceptions.HandleException(ex, "ApplicationPolicy")
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

            _FormatoExogena = Nothing
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
