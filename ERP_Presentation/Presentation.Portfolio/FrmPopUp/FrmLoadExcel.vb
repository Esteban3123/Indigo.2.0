'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Rafael Patiño
' Created          : 2013-07-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Portfolio.MVP
Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Domain.Base.Entities
Imports System.Data.OleDb
Imports System.IO
Imports Infrastructure.CrossCutting.Resources


''' <summary>
''' Formulario modal para cargar documnetos excel
''' </summary>
Public Class FrmLoadexcel

#Region "Propiedades y variables"
    ''' <summary>
    ''' Ruta de archivo a cargar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PathFile As String
        Get
            Return Me.INDlblPath.Text
        End Get
        Set(value As String)
            Me.INDlblPath.Text = value
        End Set
    End Property

    Dim _listConDetail As List(Of PortfolioConciliationDetail)

    ''' <summary>
    ''' Facturas del oficio que se va cargar por excel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListInvoice As List(Of PortfolioConciliationDetail)
        Get
            Return Me._listConDetail
        End Get
        Set(value As List(Of PortfolioConciliationDetail))
            _listConDetail = value
        End Set
    End Property

    ''' <summary>
    ''' Mensaje 
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Private WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Dim _ModuleName As String

    ''' <summary>
    ''' Bandera para indicar de que modulo se esta cargando el Excel - CON = Conciliacion - REC = Recepcion de Objeciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ModuleName As String
        Get
            Return Me._ModuleName
        End Get
        Set(value As String)
            Me._ModuleName = value
        End Set
    End Property

    ''' <summary>
    ''' clase de conexion archivo 
    ''' </summary>
    ''' <remarks></remarks>
    Private cb As OleDbConnectionStringBuilder

    ''' <summary>
    ''' datatable De lectura de Datos del excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtDatoExcel As New DataTable("Datos")

    ''' <summary>
    ''' datatable de almacen de datos de registro con glosas
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtDatos As New DataTable("Datos")

    ''' <summary>
    ''' Modelo de recepcion de objeciones
    ''' </summary>
    ''' <remarks></remarks>
    Private MPortfolioConciliation As MPortfolioConciliation


#End Region

#Region "Metodos y Funciones"
    Private Function ConnectionOleDb()
        cb = New OleDbConnectionStringBuilder()
        cb.DataSource = PathFile
        If Path.GetExtension(PathFile).ToUpper() = ".XLS" Then
            cb.Provider = "Microsoft.Jet.OLEDB.4.0"
            cb.Add("Extended Properties", "Excel 8.0;HDR=YES;IMEX=0;")
        ElseIf Path.GetExtension(PathFile).ToUpper() = ".XLSX" Then
            cb.Provider = "Microsoft.ACE.OLEDB.12.0"
            cb.Add("Extended Properties", "Excel 12.0 Xml;HDR=YES;IMEX=0;")
        End If
    End Function

    ''' <summary>
    ''' Funcion para validar y carga glosas desde excel
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub ReadDataGlosa()
        Try
            AsyncLoader(True)
            dtDatoExcel = New DataTable("Datos")
            dtDatos = New DataTable("Datos")

            If cb Is Nothing Then
                ConnectionOleDb()
            End If
            Using conn As New OleDbConnection(cb.ConnectionString)
                'Abrimos la conexión
                conn.Open()
                Using cmd As OleDbCommand = conn.CreateCommand()
                    cmd.CommandType = CommandType.Text
                    cmd.CommandText = "SELECT * FROM [Sheet$]"
                    'Guardamos los datos en el DataTable
                    Dim da As New OleDbDataAdapter(cmd)
                    da.Fill(dtDatoExcel)
                End Using
                'Cerramos la conexión
                conn.Close()
            End Using

            Dim datarow() As DataRow = dtDatoExcel.Select("[Numerofactura] IS NOT NULL AND [Numerodocumento] IS NOT NULL")
            dtDatos = dtDatoExcel.Clone
            If ListInvoice.Count > 0 Then
                For Each item As DataRow In datarow
                    Dim InvoiceRow = (From c In ListInvoice Select c.DescInvoiceNumber).FirstOrDefault
                    If InvoiceRow = item.Item("NumeroFactura") Then
                        Mensaje(EeventViewerImages.Advertencia) = ("Esta factura ya estan cargadas: " & InvoiceRow)
                        'AsyncLoader(False)
                        'Exit Sub
                    End If
                Next


            End If


            If dtDatoExcel.Rows.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmLoadExcel_NOExisteDatosPertinenteExcel", "Glosas")
                AsyncLoader(False)
                Exit Sub
            End If

            Dim dtset As New DataSet
            dtset.Tables.Add(dtDatoExcel)
            Dim result As ActionResult = Await MPortfolioConciliation.ValidateExcelData(dtset)
            If result.StateResult = True Then
                If result.MessageResult.Count > 0 Then
                    INDgcMessage.DataSource = From e In result.MessageResult Select New With {.Message = e.ToString}
                End If
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    INDgcMessage.DataSource = From e In result.MessageResult Select New With {.Message = e.ToString}
                End If
            End If
            INDgcMessage.RefreshDataSource()
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
#End Region

#Region "Eventos"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listConDetail = Nothing
        _ModuleName = Nothing
        cb = Nothing
        dtDatoExcel = Nothing
        dtDatos = Nothing
        MPortfolioConciliation = Nothing
    End Sub


    Private Sub FrmLoadexcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ModuleName = "CON" Then
            MPortfolioConciliation = New MPortfolioConciliation("2221")
        End If
        Me.ToolBar.Visible = False
    End Sub


    Private Sub INDbtnLoad_Click(sender As Object, e As EventArgs) Handles INDbtnLoad.Click
        If ModuleName = "CON" Then
            ReadDataGlosa()
        End If
    End Sub

#End Region


End Class