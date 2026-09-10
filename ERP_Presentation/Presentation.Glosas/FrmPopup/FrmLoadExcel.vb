'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Rafael Patiño
' Created          : 2013-07-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Glosas.MVP
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
Imports ClosedXML.Excel
Imports System.Collections.Concurrent

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

    Dim _listObjD As List(Of GlosaObjectionsReceptionD)

    ''' <summary>
    ''' Facturas del oficio que se va cargar por excel
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ListInvoice As List(Of GlosaObjectionsReceptionD)
        Get
            Return Me._listObjD
        End Get
        Set(value As List(Of GlosaObjectionsReceptionD))
            _listObjD = value
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

    Dim _ConciliationC As ConciliationC

    Public Property ConciliationC As ConciliationC
        Get
            Return Me._ConciliationC
        End Get
        Set(value As ConciliationC)
            Me._ConciliationC = value
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
    Private MObjectionsReception As MObjectionsReception

    ''' <summary>
    ''' Modelo de conciliaciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Mconciliation As MConciliation

#End Region

#Region "Events"
    Public Event RefreshDocument()
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

            Dim datarow() As DataRow = dtDatoExcel.Select("[ValorGlosa] IS NOT NULL AND [CodigoConcepto] IS NOT NULL AND [CodigoResponsable] IS NOT NULL")
            dtDatos = dtDatoExcel.Clone
            If ListInvoice.Count > 0 Then
                For Each item As DataRow In datarow
                    If ListInvoice.Exists(Function(c As GlosaObjectionsReceptionD) c.InvoiceNumber = item.Item("Factura")) = True Then
                        dtDatos.ImportRow(item)
                    End If
                Next
            Else
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmLoadExcel_NOExisteDatosEnRadicado", "Glosas")
                AsyncLoader(False)
                Exit Sub
            End If


            If dtDatos.Rows.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmLoadExcel_NOExisteDatosPertinenteExcel", "Glosas")
                AsyncLoader(False)
                Exit Sub
            End If

            Dim dtset As New DataSet
            dtset.Tables.Add(dtDatos)
            Dim result As ActionResult = Await MObjectionsReception.ValidateExcelData(dtset)
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


    Private Async Sub ReadDataConciliation()
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

            '    Dim datarow() As DataRow = dtDatoExcel.Select("[Valor Pendiente] > 0 AND ([Valor IPS conciliacion] >0 or [Valor EAPB conciliacion] >0)") 'validacion no se puede tener encuenta ya que pueden venir valores en cero en estas dos clumnas cuando un item de factura tiene mas de un movimiento
            Dim datarow() As DataRow = dtDatoExcel.Select("[Valor Pendiente] > 0 ")
            dtDatos = dtDatoExcel.Clone
            For Each item As DataRow In datarow
                dtDatos.ImportRow(item)
            Next

            If dtDatos.Rows.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmLoadExcel_NOExisteDatosConciliacion", "Glosas")
                AsyncLoader(False)
                Exit Sub
            End If

            Dim dtset As New DataSet
            dtset.Tables.Add(dtDatos)
            Dim result As ActionResult(Of ConciliationC) = Await Mconciliation.ValidateExcelDataConciliation(dtset, _ConciliationC)
            If result.StateResult = True Then
                If result.MessageResult.Count > 0 Then
                    INDgcMessage.DataSource = From e In result.MessageResult Select New With {.Message = e.ToString}
                End If
                If result.ObjectEmbbeded IsNot Nothing Then
                    Me.ConciliationC = result.ObjectEmbbeded
                End If
            Else
                If result.MessageResult.Count > 0 Then
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

    Private Async Sub ReadCoordination()
        Try
            AsyncLoader(True)
            ' Crear una tabla DataTable para almacenar los datos
            Dim dataTable As New DataTable()
            Dim ds As New DataSet
            Using workbook As New XLWorkbook(PathFile)
                ' Obtener la hoja con los datos
                Dim worksheet = workbook.Worksheet(1)
                ' Obtener el rango de datos
                Dim range = worksheet.RangeUsed()
                Dim columnsExcel = range.ColumnCount()
                If columnsExcel <> 26 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Estructura del archivo invalida"
                    AsyncLoader(False)
                    Exit Sub
                End If
                ' Recorrer el rango y agregar las columnas al DataTable
                For col As Integer = 1 To columnsExcel
                    dataTable.Columns.Add(range.Cell(1, col).Value.ToString())
                Next

                Dim rows As New ConcurrentBag(Of DataRow)

                ' Recorrer el rango y agregar las filas a la colección en paralelo
                Parallel.For(2, range.RowCount() + 1,
                    Sub(row)
                        Dim newRow As DataRow = dataTable.NewRow()
                        For col As Integer = 1 To columnsExcel
                            newRow(col - 1) = range.Cell(row, col).Value.ToString()
                        Next
                        rows.Add(newRow)
                    End Sub)
                ' Recorrer cada fila en la colección 'rows'
                For Each row As DataRow In rows
                    dataTable.Rows.Add(row)
                Next
                ds.Tables.Add(dataTable)
            End Using

            Using model As New MCoodination("")

                Dim listResult As New List(Of String)
                Dim batchSize As Integer = 200
                Dim totalRows As Integer = ds.Tables(0).Rows.Count

                'recorremos el dataset por lotes establecidos en la variable batchSize
                For startIndex As Integer = 0 To totalRows - 1 Step batchSize
                    Dim endIndex As Integer = Math.Min(startIndex + batchSize - 1, totalRows - 1)

                    ' Crear un nuevo DataSet para el lote actual
                    Dim batchDs As New DataSet

                    'Se clona el nombre y la estructura del dataset original para crear un nuevo dataset que es el que lleva la informacion por lotes
                    Dim batchTable As New DataTable(ds.Tables.Item(0).TableName)
                    batchTable = ds.Tables.Item(0).Clone()

                    'Se agrega la infromacion al nuevo data set segun el lote que sea
                    For i As Integer = startIndex To endIndex
                        batchTable.ImportRow(ds.Tables.Item(0).Rows(i))
                    Next
                    batchDs.Tables.Add(batchTable)

                    'Ejecutamos el carge por lote
                    Dim result = Await model.ChargueExcelDataCoordination(batchDs)
                    If result.StateResult Then
                        Parallel.ForEach(result.MessageResult, Sub(message)
                                                                   listResult.Add(message?.ToString())
                                                               End Sub)
                    Else
                        ' Manejar el caso cuando hay un error en el procesamiento del lote
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If

                Next

                If listResult.Any() Then
                    INDgcMessage.DataSource = listResult.Select(Function(e) New With {.Message = e})
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

#End Region

#Region "Eventos"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _listObjD = Nothing
        _ModuleName = Nothing
        cb = Nothing
        dtDatoExcel = Nothing
        dtDatos = Nothing
        MObjectionsReception = Nothing
        Mconciliation = Nothing
    End Sub


    Private Sub FrmLoadexcel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ModuleName = "CON" Then
            Mconciliation = New MConciliation("522")
        ElseIf ModuleName = "REC" Then
            MObjectionsReception = New MObjectionsReception("508")
        End If
        Me.ToolBar.Visible = False
    End Sub

    ''' <summary>
    ''' Clic sobre el boton cargar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnLoad_Click(sender As Object, e As EventArgs) Handles INDbtnLoad.Click
        If ModuleName = "CON" Then
            ReadDataConciliation() 'Carga desde conciliación
        ElseIf ModuleName = "REC" Then
            ReadDataGlosa() 'Carga desde recepción
        ElseIf ModuleName = "COR" Then
            ReadCoordination() 'Carga desde coordinación
        End If
    End Sub

    ''' <summary>
    ''' Evento closing del frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmLoadexcel_FormClosing(sender As Object, e As Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        RaiseEvent RefreshDocument()
    End Sub

#End Region


End Class