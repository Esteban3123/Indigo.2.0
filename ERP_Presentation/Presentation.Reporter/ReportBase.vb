' ***********************************************************************
' Assembly         : Presentation.Reporter
' Author           : Juan Diego Diaz
' Created          : 2014-01-03
' 
' Copyright        : (c) . All rights reserved.
' ***********************************************************************

#Region "Imports"

Imports DevExpress.XtraReports.UI
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo.Metadata
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.IO
Imports DevExpress.XtraPrinting

#End Region

''' <summary>
''' Clase base para los reportes
''' </summary>
Public Class ReportBase
    Implements IDisposable

#Region "Delegates"

#End Region

#Region "Fields and Constants"
    ' ''' <summary>
    ' ''' Variable para inicializar los valores de sesion
    ' ''' </summary>
    'Dim IndigoSessionValues As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Constante con ruta los reportes por defecto
    ''' </summary>
    Public Shared pathReportDefault = Infrastructure.CrossCutting.Base.ConfigurationFile.Instance.LocalReportsPath  '"C:\Users\JuanDDiaz\Desktop\DefaultReports\"
    ''' <summary>
    ''' Constante con ruta los reportes personalizados
    ''' </summary>
    Public Shared pathReportCustomize = Infrastructure.CrossCutting.Base.ConfigurationFile.Instance.ServerReportsPath '"C:\Users\JuanDDiaz\Desktop\CustomizeReports\"
#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para ejecutar la visualización de un reporte
    ''' </summary>
    ''' <param name="iReport">Enumeración que determina el reporte a mostrar</param>
    Public Function ExecReport(iReport As IReport, ReportPath As String) As Task(Of XtraReport)
        Return Task(Of XtraReport).Factory.StartNew(Function()
                                                        Dim Report = InitializateReport(iReport, ReportPath)
                                                        If Report IsNot Nothing Then
                                                            'Return New ReportPrintTool(Report)
                                                            Return Report 'New ReportPrintToolExt(Report)
                                                        Else
                                                            Return Nothing
                                                        End If
                                                    End Function)
    End Function

    ''' <summary>
    ''' Función para inicializar los reportes y verificar si existen sus definiciones
    ''' </summary>
    ''' <param name="Report">Reporte a cargar</param>
    ''' <returns>XtraReport</returns>
    Private Function InitializateReport(Report As IReport, Optional ReportPath As String = "", Optional isEditReport As Boolean = False) As XtraReport
        If Not ReportPath.Equals("") AndAlso Not ReportPath.Contains("{0}") Then
            If IO.File.Exists(ReportPath) Then
                CType(Report, XtraReport).LoadLayout(ReportPath)
            Else
                MessageBox.Show(obtenerRecurso(DefinitionReport, Reportes))
                Report = Nothing
            End If
        End If
        If Report IsNot Nothing AndAlso Not isEditReport Then
            Report.CargarDataSource()
            Report.CargarImagenes()
        End If
        Return Report
    End Function

    ''' <summary>
    ''' Metodo para ejecutar la edición de un reporte
    ''' </summary>
    ''' <param name="IReport">Enumeración que determina el reporte a mostrar</param>
    Public Sub EditReport(IReport As IReport, ReportPath As String, Optional ByVal flagUniqueName As Boolean = False)
        Dim Report = InitializateReport(IReport, ReportPath, True)
        If Report IsNot Nothing Then
            Dim Tool As Interceptor = New Interceptor(Report, ReportPath, flagUniqueName)
            Tool.Show()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para ejecutar la edición de un reporte
    ''' </summary>
    ''' <param name="IReport">Enumeración que determina el reporte a mostrar</param>
    Public Function EditReportWithShowDialog(IReport As IReport, ReportPath As String) As DialogResult
        Dim Report = InitializateReport(IReport, ReportPath, True)
        If Report IsNot Nothing Then
            Dim Tool As Interceptor = New Interceptor(Report, ReportPath)
            Return Tool.ShowDialog()
        Else
            Return DialogResult.Ignore
        End If
    End Function

    ''' <summary>
    ''' Metodo para imprimir la edición de un reporte
    ''' </summary>
    ''' <param name="IReport">Enumeración que determina el reporte a mostrar</param>
    Public Function PrintReport(IReport As IReport, Optional ReportPath As String = "") As Task(Of ReportPrintTool)
        Return Task(Of ReportPrintTool).Factory.StartNew(Function()
                                                             Dim Report = InitializateReport(IReport, ReportPath)
                                                             If Report IsNot Nothing Then
                                                                 Return New ReportPrintTool(Report)
                                                             Else
                                                                 Return Nothing
                                                             End If
                                                         End Function)

    End Function

#End Region

#Region "Statics"

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class