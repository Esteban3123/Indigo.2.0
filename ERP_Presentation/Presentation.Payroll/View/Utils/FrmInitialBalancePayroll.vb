'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Carlos Mario Arias Rubiano
' Created          : 04/05/2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Design
Imports DevExpress.XtraEditors
Imports DevExpress.Data
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports System.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass


Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions

Imports Domain.Base.Entities
Imports Presentation.Common

Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Spreadsheet
Imports System.Text
Imports Domain.Entities
Imports System.IO
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.Data.Xpo

#End Region

Public Class FrmInitialBalancePayroll

#Region "Builder"

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        AddHandler bwInitialBalance.DoWork, AddressOf bwListPayments_DoWork
        AddHandler bwInitialBalance.RunWorkerCompleted, AddressOf bwInitialBalance_RunWorkerCompleted
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' coleccion de filas que se van a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection

    ''' <summary>
    ''' Representa al listado de los registros de copiar y pegar
    ''' </summary>
    Private RowsPasteToGrid As List(Of List(Of String))

    ''' <summary>
    ''' Listado del CopyPaste
    ''' </summary>
    Dim ListCopyPaste As List(Of List(Of String))

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New List(Of ImportFileRow)()

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    ''' <summary>
    ''' Asyncrono para validar el archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private bwInitialBalance As BackgroundWorker = New BackgroundWorker

    ''' <summary>
    ''' Listado que devuelve el sp que valida el excel con los registros en OK
    ''' </summary>
    Private ListSP_ImportFileInitialBalancePayroll_Result As List(Of SP_ImportFileInitialBalancePayroll_Result)

    ''' <summary>
    ''' Listado de errores que devuelve el sp
    ''' </summary>
    Private ListErrors As List(Of Tuple(Of String, Integer))

    ''' <summary>
    ''' Resultado de la consulta al sp
    ''' </summary>
    Private result As ActionResult(Of List(Of SP_ImportFileInitialBalancePayroll_Result))

    ''' <summary>
    ''' Permite saber si viene desde copiar y pegar o de importar archivo
    ''' </summary>
    Private IsCopyPaste As Boolean

    Private _CurrencyAbbreviation As String

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        rows = Nothing
        RowsPasteToGrid = Nothing
        ListCopyPaste = Nothing
        listRows = Nothing
        myStream = Nothing
        bwInitialBalance = Nothing
        ListSP_ImportFileInitialBalancePayroll_Result = Nothing
        ListErrors = Nothing
        result = Nothing
        IsCopyPaste = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmInitialBalancePayroll_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewInfo, ListActions)
        BarButtonsConfigure()
        INDEsbBills.AddRangeColumns("Cédula", "Fecha Nómina", "Días Trabajados", "Total Devengado", "Total Deducido", "Base Pensión", "Aporte Pensión Empleado",
                                    "Cod. Conc. Apor. Pen. Empl.", "Aporte Pensión Patrono", "Cod. Conc. Apor. Pen. Patr.", "Base Salud", "Aporte Salud Empleado",
                                    "Cod. Apor. Salud Empl.", "Aporte Salud Patrono", "Cod. Apor. Salud Patr.", "Base Primas", "Provisión Primas", "Cod. Conc. Prov. Primas",
                                    "Base Vacaciones", "Provisión Vacaciones", "Cod. Prov. Vaca.", "Base Cesantías", "Provisión Cesantías", "Cod. Prov. Cesan.", "Provisión Int. Cesantías",
                                    "Cod. Prov. Int. Cesa.", "Valor Incapacidad Ambulatoria", "Cod. Conc. Val. Inca. Amb.", "Valor Incapacidad Hospitalaria", "Cod. Conc. Val. Inca. Hosp.",
                                    "Valor Licencia Maternidad", "Cod. Conc. Val. Lic. Mater.", "Base Sena", "Aporte Sena", "Cod. Conc. Apor. Sena", "Base ICBF", "Aporte ICBF",
                                    "Cod. Conc. Apor. ICBF", "Base Caja Compensación Familiar", "Aporte Caja Compensación", "Cod. Conc. Caja Compe.", "Base Retención", "Valor Retención",
                                    "Cod. Conc. Val. Rete.", "Recargos", "Cod. Conc. Reaca.", "Días Sanción", "Horas Extras", "Cod. Conc. Hor. Ext.", "Recargos Nocturnos Festivos", "Cod. Conc. Recar. Noct. Fest.", "Valor Dominical Ordinario", "Cod. Conc. Valor Dominical Or.")
        CurrencyAbbreviation = LoadPayrollSettings.CurrencyId.Abbreviation
        SetCurrencyFormat(CurrencyAbbreviation)

    End Sub


#End Region
#Region "Properthies"
    ''' <summary>
    ''' Propiedad  moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyAbbreviation As String
        Get
            Return _CurrencyAbbreviation
        End Get
        Set(value As String)
            _CurrencyAbbreviation = value
        End Set
    End Property
#End Region
#Region "PasteToGrid"

    ''' <summary>
    ''' Evento que se dispara al presionar control + v sobre la rejilla de información
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewInfo_KeyDown(sender As Object, e As KeyEventArgs) Handles INDviewInfo.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then 'Ctrl+V
            If Clipboard.ContainsText Then
                If Clipboard.GetText() IsNot Nothing AndAlso Not Clipboard.GetText().Trim().Equals(String.Empty) Then
                    RowsPasteToGrid = New List(Of List(Of String))
                    GetRows()
                    PasteToGrid()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Obtiene los registros del excel
    ''' </summary>
    Private Sub GetRows()
        'Se obtiene el objeto del clip board
        Dim dataObject = Clipboard.GetDataObject()

        'Se convierte el objeto en formato html, se hace de esta manera porque de otras formas no me traía las celdas finales en blanco
        Dim dataHtml As String = dataObject.GetData(DataFormats.Html)

        'Si no encuentra la tabla dentro de lo copiado se sale
        If dataHtml.IndexOf("<tr") < 0 Then
            Exit Sub
        End If

        'Se obtiene la nueva cadena de tr
        Dim trHtmlString As String = dataHtml.Substring(dataHtml.IndexOf("<tr"), dataHtml.LastIndexOf("/tr>") - dataHtml.IndexOf("<tr") + "/tr>".Length)

        'Se obtiene las lineas con el tr
        Dim linesHtml() As String = Split(trHtmlString, "/tr>" + Microsoft.VisualBasic.Constants.vbCrLf)

        'Se recorre el string de tr
        For Each line As String In linesHtml
            'Se obtiene la nueva cadena de td
            Dim tdHtmlString As String = line.Substring(line.IndexOf("<td"), line.LastIndexOf("/td>") - line.IndexOf("<td") + "/td>".Length)

            'Se obtiene el array con los td para poder sacar los valores
            Dim items() As String = Split(tdHtmlString, "</td>")

            'Array final que se agrega al listado
            Dim finallyArray As New List(Of String)

            'Se recorre los items para poder armar el array final
            For Each lineTd As String In items
                If Not (lineTd IsNot Nothing AndAlso lineTd IsNot String.Empty) Then 'Si la linea trae vacío
                    Continue For
                End If
                'Se declara el valor para el listado final
                Dim valueString As String = ""
                If ((lineTd.Length - 1) - lineTd.LastIndexOf(">")) > 0 Then 'Si trae el valor al final
                    valueString = lineTd.Substring(lineTd.LastIndexOf(">"), (lineTd.Length - 1) - lineTd.LastIndexOf(">") + 1) 'Se obtiene el valor
                    valueString = valueString.Replace(">", "") 'Se reemplaza el valor de > en el valor final con vacio
                    valueString = valueString.Replace("&nbsp;", "")
                End If
                'Se agrega al listado
                finallyArray.Add(valueString)
            Next
            'Se agrega al listado final el listado de string que se armó
            RowsPasteToGrid.Add(New List(Of String)(finallyArray))
        Next
    End Sub

    ''' <summary>
    ''' Método que se ejecuta al momento de pegar info a la rejilla desde un excel
    ''' </summary>
    Private Sub PasteToGrid()
        AsyncLoader(True)

        'Se asigna el listado del CopyPaste
        ListCopyPaste = RowsPasteToGrid

        'Si el listado no trae nd se sale del método
        If Not (ListCopyPaste IsNot Nothing AndAlso ListCopyPaste.Count > 0) Then
            AsyncLoader(False)
            Exit Sub
        End If

        'Se llama el asyncrono para validar el excel siempre y cuando no haya un asyncrono en ejecución
        If bwInitialBalance.IsBusy Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
            Exit Sub
        End If

        'Si asigna el valor correspondiente a CopyPaste
        IsCopyPaste = True

        'Se ejecuta el asyncrono
        bwInitialBalance.RunWorkerAsync()
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu con botón en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteItem()
    End Sub

    ''' <summary>
    ''' Menu desplegable en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteItem()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddEmployee_Click(sender As Object, e As EventArgs) Handles INDbtnAddEmployee.Click
        OpenFormAddEmployee()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de importar archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        ImportFile()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmInitialBalancePayroll_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnAddEmployee.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el form de agregar el empleado
    ''' </summary>
    Private Sub OpenFormAddEmployee()
        Using formulario As New PopUpPayrollOpeningBalances(True)
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddEntityStoreProcedureEventArgs, AddressOf ReturnAddEventArgs
            formulario.TextForm = "Agregar Empleado"
            formulario.ListValidate = ListSP_ImportFileInitialBalancePayroll_Result
            formulario.Size = New System.Drawing.Size(850, 550)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega la entidad a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddEntityStoreProcedure)
        If ListSP_ImportFileInitialBalancePayroll_Result Is Nothing Then
            ListSP_ImportFileInitialBalancePayroll_Result = New List(Of SP_ImportFileInitialBalancePayroll_Result)
        End If
        ListSP_ImportFileInitialBalancePayroll_Result.Add(e.SP_ImportFileInitialBalancePayroll_Result)
        INDgcInformation.DataSource = Nothing
        INDgcInformation.DataSource = ListSP_ImportFileInitialBalancePayroll_Result
    End Sub

    ''' <summary>
    ''' Metodo que guarda los saldos iniciales
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function SaveInitialBalance() As Task
        'Se valida que hayan registros en la rejilla
        If ListSP_ImportFileInitialBalancePayroll_Result Is Nothing OrElse ListSP_ImportFileInitialBalancePayroll_Result.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No hay registros en la rejilla para poder guardar"
            Exit Function
        End If
        Using model As New MInitialBalancePayroll(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SP_SaveInitialBalancePayroll(ListSP_ImportFileInitialBalancePayroll_Result)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Using formulario As New FrmListErrors(Result.ObjectEmbbeded)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
                Me.Deshacer()
            Else
                Mensaje(EeventViewerImages.Advertencia) = Result.Message
            End If
        End Using
    End Function

    ''' <summary>
    ''' Inicia el backgroundWorker para consultar el listado de pagos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwListPayments_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        e.Cancel = False

        'Se instancia el listado que se va a enviar para validar la info
        listRows = New List(Of ImportFileRow)

        'Se agrega las filas del excel al listado que se va a enviar
        If IsCopyPaste Then 'Si viene desde CopyPaste
            SetRowCopyPaste(0, ListCopyPaste.Count - 1)
        Else 'Si viene desde importar archivo
            SetRow(1, rows.LastUsedIndex + 1)
        End If

        If listRows Is Nothing OrElse listRows.Count = 0 Then 'Validamos que haya valores en el listado
            e.Cancel = True
            Exit Sub
        End If

        Using model As New MInitialBalancePayroll(Me.Tag)

            'Consumimos el sp que se creó para validar el archivo de excel
            result = model.SP_ImportFileInitialBalancePayroll(listRows)

            'Limpiamos el listado de errores
            ListErrors = Nothing

            'Si no retorna error la consulta
            If result.StateResult Then

                'Se instancia el listado de errores
                ListErrors = New List(Of Tuple(Of String, Integer))

                'Si el listado está vacio
                If ListSP_ImportFileInitialBalancePayroll_Result Is Nothing OrElse ListSP_ImportFileInitialBalancePayroll_Result.Count = 0 Then
                    'Se asigna al listado que va a la rejilla los registros en OK de la validación del sp
                    ListSP_ImportFileInitialBalancePayroll_Result = (From x In result.ObjectEmbbeded Where x.StatusField = 1 Select x).ToList()
                Else 'Si el listado ya está lleno se realiza la validacion del empleado
                    For Each item In (From x In result.ObjectEmbbeded Where x.StatusField = 1 Select x).ToList()
                        If (From x In ListSP_ImportFileInitialBalancePayroll_Result Where x.EmployeeId = item.EmployeeId AndAlso CDate(x.PayrollDate).Month = CDate(item.PayrollDate).Month AndAlso CDate(x.PayrollDate).Year = CDate(item.PayrollDate).Year Select x).Count > 0 Then
                            ListErrors.Add(New Tuple(Of String, Integer)("La cédula del empleado " + item.Nit + " ya existe en la lista con el mes " + CDate(item.PayrollDate).Month.ToString() + " y el año " + CDate(item.PayrollDate).Year.ToString(), 2))
                        Else
                            ListSP_ImportFileInitialBalancePayroll_Result.Add(item)
                        End If
                    Next
                End If

                'Si hay errores se asignan al listado de errores para posteriormente mostrarlos en el form de errores
                If (From x In result.ObjectEmbbeded Where x.StatusField = 0 Select x).Count > 0 Then

                    'Se recorre el resultado de errores para asignarlos al listado de errores
                    For Each itemError In (From x In result.ObjectEmbbeded Where x.StatusField = 0 Select x).ToList
                        ListErrors.Add(New Tuple(Of String, Integer)(itemError.MessageField, 2))
                    Next

                End If

            End If
        End Using
    End Sub

    ''' <summary>
    ''' Termina el backgroundWorker de consultar el listado de pagos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub bwInitialBalance_RunWorkerCompleted(ByVal sender As Object, ByVal e As RunWorkerCompletedEventArgs)
        If e.Cancelled Then 'Si hubo alguna cancelación
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No hay datos en el listado para poder validar"
            Exit Sub
        End If

        'Si hubo error en la ejecución del sp se devuelve
        If result.StateResult = False Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = result.Message
            Exit Sub
        End If

        AsyncLoader(False)

        'Se asigna el listado del sp a la rejilla
        INDgcInformation.DataSource = Nothing
        INDgcInformation.DataSource = ListSP_ImportFileInitialBalancePayroll_Result

        'Se valida que si hay errores se despliegue el form de errores con los mensajes
        If ListErrors IsNot Nothing AndAlso ListErrors.Count > 0 Then
            Using formulario As New FrmListErrors(ListErrors)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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


    ''' <summary>
    ''' Metodo que importa los items del excel a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ImportFile()
        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = False
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)

        'Si el ususario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            AsyncLoader(False)
            Exit Sub
        End If

        Try
            'Obtengo la ruta del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Sub
            End If

            'Se obtiene el documento excel
            Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
            sddf.AllowDrop = False
            sddf.LoadDocument(myStream)
            Dim workBook As IWorkbook = sddf.Document

            rows = workBook.Worksheets(0).Rows
            If rows.LastUsedIndex = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                AsyncLoader(False)
                Exit Sub
            End If

        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo"
            Exit Sub
        End Try

        'Se llama el asyncrono para validar el excel siempre y cuando no haya un asyncrono en ejecución
        If bwInitialBalance.IsBusy Then
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = "No puede continuar porque hay un proceso que no ha terminado su ejecución"
            Exit Sub
        End If

        'Si asigna el valor correspondiente a importacion
        IsCopyPaste = False

        'Se ejecuta el asyncrono
        bwInitialBalance.RunWorkerAsync()
    End Sub

    ''' <summary>
    ''' Obtiene el registro de la fila y lo inserta en el listado
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        For i As Integer = indexSend To indexEnd
            If (From info In rows.Item(i) Where info.Value.ToObject() IsNot Nothing Select info).Count > 0 Then
                listRows.Add(New ImportFileRow With {.IndexRow = i + 1, .Row = rows.Item(i).SpreadsheetRowToList(53)})
            End If
        Next
    End Sub

    ''' <summary>
    ''' Metodo que convierte el listado del CopyPaste al listado que se envia al sp para que valide la info
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRowCopyPaste(indexSend As Integer, indexEnd As Integer)
        For i As Integer = indexSend To indexEnd
            listRows.Add(New ImportFileRow With {.IndexRow = i + 1, .Row = ConvertInfo(ListCopyPaste(i))})
        Next
    End Sub

    ''' <summary>
    ''' Convierte la información del CopyPaste a objeto de la entidad del listado que se envia al sp
    ''' </summary>
    ''' <returns></returns>
    Private Function ConvertInfo(data As List(Of String)) As List(Of Object)
        Dim res As New List(Of Object)
        For x As Integer = 0 To data.Count - 1
            res.Add(data(x))
        Next
        Return res
    End Function

    ''' <summary>
    ''' Configura los botones de la barra botones
    ''' </summary>
    Private Sub BarButtonsConfigure()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
    End Sub

    ''' <summary>
    ''' Deshace los cambios en el form
    ''' </summary>
    Private Sub Deshacer()
        BarButtonsConfigure()
        CleanControls()
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        ListSP_ImportFileInitialBalancePayroll_Result = Nothing
        ListErrors = Nothing
        myStream = String.Empty
        INDgcInformation.DataSource = Nothing
        rows = Nothing
        listRows = Nothing
        result = Nothing
        ListCopyPaste = Nothing
        RowsPasteToGrid = Nothing
    End Sub

    ''' <summary>
    ''' Elimina uno o más items a la vez
    ''' </summary>  
    Private Sub DeleteItem()
        If MessageIndigo.Show("Está seguro de eliminar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        If INDviewInfo.GetSelectedRows IsNot Nothing AndAlso INDviewInfo.GetSelectedRows.Length > 0 Then
            Dim ListDelete As New List(Of SP_ImportFileInitialBalancePayroll_Result)
            For i = 0 To INDviewInfo.GetSelectedRows.Count - 1
                Dim row As SP_ImportFileInitialBalancePayroll_Result = INDviewInfo.GetRow(INDviewInfo.GetSelectedRows(i))
                ListDelete.Add(row)
            Next
            ListDelete.ForEach(Sub(x) ListSP_ImportFileInitialBalancePayroll_Result.Remove(x))
            INDgcInformation.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' Establece los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        GridColumn4 = Window.Utils.FormatGrid(GridColumn4, _currencyAbbreviation)
        GridColumn5 = Window.Utils.FormatGrid(GridColumn5, _currencyAbbreviation)
        GridColumn6 = Window.Utils.FormatGrid(GridColumn6, _currencyAbbreviation)
    End Sub

#End Region
    ''' <summary>
    ''' Obtiene los datos de la moneda oficial
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadPayrollSettings() As PayrollSettingsXpo
        Return XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.GetCollection(Of PayrollSettingsXpo).FirstOrDefault()
    End Function
#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Deshacer de la barra botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Guarda los saldos iniciales
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await SaveInitialBalance()
    End Sub

#End Region

End Class