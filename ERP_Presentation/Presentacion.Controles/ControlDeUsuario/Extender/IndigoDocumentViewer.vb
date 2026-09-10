'***********************************************************************
' Assembly         : Presentation.Controls
' Author           : Juan Fernando Tamayo
' Created          : 2015-08-28
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.IO
Imports System.ComponentModel
Imports DevExpress.XtraPrinting.Preview
Imports DevExpress.XtraReports.UI
Imports DevExpress.XtraPrinting
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Reporter

#End Region

<ProvideProperty("ExtendProperties", GetType(DocumentViewer))> _
Public Class IndigoDocumentViewer
    Inherits Component
    Implements IExtenderProvider
    Implements ISupportInitialize

#Region "Fields"

    ''' <summary>
    ''' Controles extendidos
    ''' </summary>
    Private _extendedControls As Hashtable
    ''' <summary>
    ''' Formulario padre de donde es el reporte
    ''' </summary>
    Private _parentForm As Form
    ''' <summary>
    ''' Permisos del formulario padre
    ''' </summary>
    Private _permissions As Dictionary(Of Integer, String)

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._extendedControls = New Hashtable()
        InitializeComponent()
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna los permisos usados en el frontal padre
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property Permissions As Dictionary(Of Integer, String)
        Get
            Return Me._permissions
        End Get
        Set(value As Dictionary(Of Integer, String))
            Me._permissions = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el formulario usado como padre del reporte
    ''' </summary>
    ''' <value>Formulario padre del reporte</value>
    ''' <returns>El formulario padre del reporte</returns>
    Public Property ParentForm As Form
        Get
            Return Me._parentForm
        End Get
        Set(value As Form)
            Me._parentForm = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene un valor que indica si el control extiende sus propiedades
    ''' </summary>
    ''' <param name="d">Control que extiende sus propiedades</param>
    ''' <returns>Un valor que indica si el control extiende sus propiedades</returns>
    Public Function GetExtendProperties(ByVal d As DocumentViewer) As Boolean
        Return Me.EnsurePropertiesExists(d).ExtendProperties
    End Function

    ''' <summary>
    ''' Asigna un valor que indica si el control extiende sus propiedades
    ''' </summary>
    ''' <param name="d">Control que extiende sus propiedades</param>
    ''' <param name="value">Valor a asignar</param>
    Public Sub SetExtendProperties(ByVal d As DocumentViewer, ByVal value As Boolean)
        Me.EnsurePropertiesExists(d).ExtendProperties = value
    End Sub

#End Region

#Region "Class"

    ''' <summary>
    ''' Encapsula las 
    ''' </summary>
    Public Class Properties

        ''' <summary>
        ''' Obtiene o asigna el reporte cargado en el visor
        ''' </summary>
        Public Property Report As XtraReport
        ''' <summary>
        ''' Obtiene o asigna la ruta y nombre del reporte
        ''' </summary>
        Public Property ReportPath As String
        ''' <summary>
        ''' Obtiene o asigna un valor que indica si el control exiende sus propiedades
        ''' </summary>
        Public Property ExtendProperties As Boolean

    End Class

#End Region

#Region "Methods"

#Region "IExtenderProvider"

    Public Function CanExtend(extendee As Object) As Boolean Implements IExtenderProvider.CanExtend
        Return True
    End Function

#End Region

#Region "ISupportInitialize"

    Public Sub BeginInit() Implements ISupportInitialize.BeginInit

    End Sub

    Public Sub EndInit() Implements ISupportInitialize.EndInit
        For Each de As DictionaryEntry In Me._extendedControls
            Dim viewer As DocumentViewer = CType(de.Key, DocumentViewer)
            Me.EnsurePropertiesExists(viewer) 'Agregamos el control extendido para crear el conjunto de propiedades
            AddHandler viewer.DocumentChanged, AddressOf Viewer_DocumentChanged
        Next
    End Sub

#End Region

    ''' <summary>
    ''' Asegura que existan propiedades para un control extendido determinado
    ''' </summary>
    ''' <param name="key">Control extendido</param>
    ''' <returns>Conjunto de propiedades</returns>
    Private Function EnsurePropertiesExists(ByVal key As Object) As Properties
        Dim p As Properties = DirectCast(Me._extendedControls(key), Properties)
        If p Is Nothing Then
            p = New Properties()
            p.Report = Nothing
            p.ExtendProperties = True
            Me._extendedControls(key) = p
        End If
        Return p
    End Function

    ''' <summary>
    ''' Inicializa los componentes y el contenedor
    ''' </summary>
    Private Sub InitializeComponent()
        Contenedor = New System.ComponentModel.Container()
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se configura 
    ''' </summary>
    Private Sub Viewer_DocumentChanged(ByVal sender As Object, ByVal e As EventArgs)
        Dim viewer As DocumentViewer = CType(sender, DocumentViewer)
        If Me.EnsurePropertiesExists(viewer).Report IsNot Nothing AndAlso viewer.DocumentSource IsNot Nothing AndAlso TypeOf viewer.DocumentSource Is XtraReport AndAlso Not CType(viewer.DocumentSource, XtraReport).Equals(Me.EnsurePropertiesExists(viewer).Report) Then
            Me.EnsurePropertiesExists(viewer).Report = Nothing
        End If
        If Me.EnsurePropertiesExists(viewer).Report Is Nothing AndAlso viewer.DocumentSource IsNot Nothing AndAlso TypeOf viewer.DocumentSource Is XtraReport Then
            If Me._parentForm Is Nothing Then
                Me._parentForm = viewer.FindForm()
            End If

            If _parentForm IsNot Nothing AndAlso TypeOf _parentForm Is FormBase Then
                CType(_parentForm, FormBase).BarraBotones.ActualizarPermisosBarra(_parentForm.Tag)
                Me._permissions = CType(_parentForm, FormBase).BarraBotones.PermissionsForm
            End If

            Me.EnsurePropertiesExists(viewer).Report = CType(viewer.DocumentSource, XtraReport)

            If _parentForm IsNot Nothing AndAlso _permissions IsNot Nothing Then
                Me.EnsurePropertiesExists(viewer).ReportPath = Path.Combine(ConfigurationFile.Instance.ReportsPath, (_parentForm.Tag & "." & Me.EnsurePropertiesExists(viewer).Report.GetType().Name & "." & "Report.repx"))

                If File.Exists(Me.EnsurePropertiesExists(viewer).ReportPath) Then
                    Me.EnsurePropertiesExists(viewer).Report.LoadLayout(Me.EnsurePropertiesExists(viewer).ReportPath)
                End If

                'Me.EnsurePropertiesExists(viewer).Report.CreateDocument()

                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Save, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Open, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.SendFile, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Print, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.PrintDirect, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportPdf, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.None)
                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.None)

                Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.AddCommandHandler(New CustomizeCommandHandler(Me.EnsurePropertiesExists(viewer).Report, Me.EnsurePropertiesExists(viewer).ReportPath))

                If Me._permissions.ContainsKey(PermissionsActionsForm.ImprimirReporte) OrElse Me._permissions.ContainsKey(PermissionsActionsForm.ImprimirDetallado) OrElse Me._permissions.ContainsKey(PermissionsActionsForm.ImprimirFacturaAnulada) OrElse Me._permissions.ContainsKey(PermissionsActionsForm.ImprimirTirilla) Then
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Print, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.PrintDirect, CommandVisibility.All)
                End If
                If Me._permissions.ContainsKey(PermissionsActionsForm.ExportarFormatoLectura) Then
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportPdf, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportGraphic, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXps, CommandVisibility.All)
                End If
                If Me._permissions.ContainsKey(PermissionsActionsForm.ExportarFormatoEscritura) Then
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportCsv, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportHtm, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportMht, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportRtf, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportTxt, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXls, CommandVisibility.All)
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.ExportXlsx, CommandVisibility.All)
                End If
                If Me._permissions.ContainsKey(PermissionsActionsForm.PersonalizarReporte) Then
                    Me.EnsurePropertiesExists(viewer).Report.PrintingSystem.SetCommandVisibility(PrintingSystemCommand.Customize, CommandVisibility.All)
                End If
            End If
        End If
    End Sub

#End Region

End Class

''' <summary>
''' Provee un manejador para el comando de personalización de reporte
''' </summary>
Public Class CustomizeCommandHandler
    Implements DevExpress.XtraPrinting.ICommandHandler

#Region "Fields"

    ''' <summary>
    ''' Reporte a interceptar
    ''' </summary>
    Private _report As XtraReport
    ''' <summary>
    ''' Ruta a la carpeta de personaliación de reportes
    ''' </summary>
    Private _pathReport As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="report">Reporte a interceptar</param>
    Public Sub New(ByVal report As XtraReport, ByVal pathReport As String)
        Me._report = report
        Me._pathReport = pathReport
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Manejador del comando personalizar
    ''' </summary>
    Private Sub Customize()
        Me._report.DataSource = Nothing
        Dim tool As Interceptor = New Interceptor(Me._report, Me._pathReport, True)
        tool.Show()
    End Sub

#Region "ICommandHandler"

    ''' <summary>
    ''' Valida si el comando ejecutado se puede manejar
    ''' </summary>
    Public Function CanHandleCommand(command As PrintingSystemCommand, printControl As IPrintControl) As Boolean Implements ICommandHandler.CanHandleCommand
        Return (command = PrintingSystemCommand.Customize)
    End Function

    ''' <summary>
    ''' Ejecuta el manejador correspondiente al comando ejecutado
    ''' </summary>
    Public Sub HandleCommand(command As PrintingSystemCommand, args() As Object, printControl As IPrintControl, ByRef handled As Boolean) Implements ICommandHandler.HandleCommand
        handled = Me.CanHandleCommand(command, printControl)
        If command = PrintingSystemCommand.Customize Then
            Me.Customize()
        End If
    End Sub

#End Region

#End Region

End Class