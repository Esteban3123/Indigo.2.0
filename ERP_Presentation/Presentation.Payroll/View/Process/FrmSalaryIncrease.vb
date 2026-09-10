'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 03-07-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.XtraEditors
Imports Presentation.Payroll.MVP
Imports Presentation.Common.MVP
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.Utils.Drawing
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Controls
Imports DevExpress.Utils.Design.DesignTimeTools
Imports System.Windows.Forms
Imports DevExpress.XtraSpreadsheet
Imports DevExpress.Spreadsheet

#End Region

Public Class FrmSalaryIncrease
    Implements ISalaryIncrease

#Region "Fields and globals"
    ''' <summary>
    ''' Variable para manejar el presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PSalaryIncrease

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MSalaryIncrease(MyBase.Tag)

    ''' <summary>
    ''' Variable donde se obtiene el objeto del Cálculo del Nuevo Sueldo
    ''' </summary>
    ''' <remarks></remarks>
    Dim IncreaseSalaryList As Object

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private myStream As String = Nothing

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Private listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    ''' <summary>
    ''' coleccion de filas que se van a procesar
    ''' </summary>
    ''' <remarks></remarks>
    Private rows As RowCollection

    ''' <summary>
    ''' Variable que contiene Resultado del ArchivoImportado
    ''' </summary>
    Private _salaryIncrease As SP_SetIncreaseSalaryFromFile_Result()

    ''' <summary>
    ''' Variable que contiene el listado del archivo
    ''' </summary>
    Private accumulatedSuccess As New List(Of SP_SetIncreaseSalaryFromFile_Result)()

    ''' <summary>
    ''' Variable que contiene Errores del Resultado del ArchivoImportado
    ''' </summary>
    Private _salaryIncreaseError As SP_SetIncreaseSalaryFromFile_Result()

    ''' <summary>
    ''' Variable que contiene el listado de errores del archivo
    ''' </summary>
    Private accumulatedErrors As New List(Of SP_SetIncreaseSalaryFromFile_Result)()

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private listErrosImportFile As List(Of String())

    Dim ListOfRetroactive As List(Of RetroactiveC)

    Dim AproximnationValue As Byte
#End Region

#Region "Properties"
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISalaryIncrease.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Public Property FunctionalUnitId As Integer? Implements ISalaryIncrease.FunctionalUnitId

    Public Property GroupId As Integer Implements ISalaryIncrease.GroupId

    Public Property ModificationReasonContractId As Integer Implements ISalaryIncrease.ModificationReasonContractId

    Public Property PositionId As Integer? Implements ISalaryIncrease.PositionId
#End Region

#Region "Bar Buttons Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        Model = Nothing
        IncreaseSalaryList = Nothing
        ListOfRetroactive = Nothing
    End Sub
    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Click liquidar del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickLiquidar() Handles BarraBotones.ClickLiquidar
        ExecuteIncreaseSalary()
    End Sub

    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar

        Try
            If MessageIndigo.Show("¿Desea usted confirmar el Aumento de Salarios de los Empleados?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Await ConfirmIncreaseSalary()
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
            AsyncLoader(False)
        End Try

    End Sub

#End Region


    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch

    End Sub

#Region "Métodos y Funciones"
    Private Sub FrmSalaryIncrease_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        presenter = New PSalaryIncrease(Me)

        SetCurrencyFormat(presenter.LoadPayrollSettings().CurrencyId.Abbreviation)

        Using model As New MSalaryIncrease(MyBase.Tag)
            INDSlGroup.Properties.DataSource = model.ListAllGroups()
            INDSlFuncionalUnit.Properties.DataSource = model.ListAllFunctionalUnit()
            INDSlPosition.Properties.DataSource = model.ListAllPosition()
            INDSlContractModificationReason.Properties.DataSource = model.ListAllContractModificationReason()
        End Using
        INDSlFuncionalUnit.EditValue = Nothing
        INDSlPosition.EditValue = Nothing
        BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True

        INDBtnExportStructure.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Cédula", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Fecha de inicio", .Comment = "Formato Fecha (DD/MM/AAAA)"},
                            New ExcelColumn With {.Name = "Codigo razon modificación", .Comment = "Según lo conceptos creados en el formulario (Razones de modificaciones de contratos)", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "% Aumento", .Comment = "Campo debe ser formato porcentaje", .Type = ExcelColumnType.Number},
                            New ExcelColumn With {.Name = "Aproximar sueldo", .Comment = "Ninguna = 0 /A la Décima = 1/ A la Centésima = 2/ A la Milésima = 3", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Sueldo", .Comment = "Sueldo actual del empleado", .Type = ExcelColumnType.Number},
                            New ExcelColumn With {.Name = "Incremento de", .Comment = "Valor numérico que desea aumentar al empleado", .Type = ExcelColumnType.Number},
                            New ExcelColumn With {.Name = "Con retroactivo", .Comment = "Si / No", .Type = ExcelColumnType.Text},
                            New ExcelColumn With {.Name = "Fecha inicio retroactivo", .Comment = "Si en la columna (Con Retroactivo) esta marca con (NO) deja vacío, de lo contrario ingresa la fecha de inicio de retroactivo"},
                            New ExcelColumn With {.Name = "Pago x nomina retroactivo", .Comment = "Si / No", .Type = ExcelColumnType.Number}
                        }
                    })

        LoadData()
    End Sub

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        INDColSalary = Window.Utils.FormatGrid(INDColSalary, _currencyAbbreviation)
        INDColIncrementValue = Window.Utils.FormatGrid(INDColIncrementValue, _currencyAbbreviation)
        INDColNewSalary = Window.Utils.FormatGrid(INDColNewSalary, _currencyAbbreviation)
    End Sub

    Private Sub LoadData()

        Dim ListAproxValue = New List(Of Tuple(Of Integer, String))
        ListAproxValue.Add(New Tuple(Of Integer, String)(0, "Ninguna"))
        ListAproxValue.Add(New Tuple(Of Integer, String)(10, "A la Décima"))
        ListAproxValue.Add(New Tuple(Of Integer, String)(100, "A la Centésima"))
        ListAproxValue.Add(New Tuple(Of Integer, String)(1000, "A la Milésima"))

        INDSlRateAprox.Properties.DataSource = ListAproxValue

    End Sub

    Private Sub FrmSalaryIncrease_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDSlGroup.Enabled Then
            INDSlGroup.Focus()
        End If
    End Sub

    Private Sub INDSlFuncionalUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlFuncionalUnit.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDSlFuncionalUnit.EditValue = Nothing
        End If
    End Sub

    Private Sub INDSlPosition_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlPosition.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDSlPosition.EditValue = Nothing
        End If
    End Sub

    Private Sub CleanControls()
        INDSlFuncionalUnit.EditValue = Nothing
        INDSlGroup.EditValue = Nothing
        INDSlPosition.EditValue = Nothing
        INDSlContractModificationReason.EditValue = Nothing
        INDDeInitialDate.EditValue = Nothing
        INDDeRetroactivoDate.EditValue = Nothing
        INDSEIncreasePercentage.EditValue = 0
        INDRgRetroactivo.EditValue = 0
        INDDeRetroactivoDate.EditValue = Nothing
        INDLciRetroactivoDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLycgRetroactive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciPayrollPaid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True
        INDGcNewSalary.DataSource = Nothing
        INDGcRetroactivo.DataSource = Nothing
        IncreaseSalaryList = Nothing
        _salaryIncreaseError = Nothing
        _salaryIncrease = Nothing
        INDSlGroup.Focus()
    End Sub


    Public Function ValidateControlsIncrease() As Boolean

        ValidateControlsIncrease = True

        If INDSlGroup.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(seleccioneGrupo, Eform.groups)
            INDSlGroup.Focus()
            ValidateControlsIncrease = False
            Exit Function
        End If

        If INDSEIncreasePercentage.EditValue <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(PorcentajeCero, Eform.AumentoSalario)
            INDSEIncreasePercentage.Focus()
            ValidateControlsIncrease = False
            Exit Function
        End If

        If INDRgRetroactivo.SelectedIndex = 0 And INDDeRetroactivoDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FechaRetroactivo, Eform.AumentoSalario)
            INDDeRetroactivoDate.Focus()
            ValidateControlsIncrease = False
            Exit Function
        End If

        If INDSlContractModificationReason.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No seleccionó la Razón de Modificación de Contrato"
            INDSlContractModificationReason.Focus()
            ValidateControlsIncrease = False
            Exit Function
        End If

        If INDDeInitialDate.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No seleccionó la Fecha de Inicio del Otro Si para el Aumento de Salarios"
            INDDeInitialDate.Focus()
            ValidateControlsIncrease = False
            Exit Function
        End If

        Return ValidateControlsIncrease

    End Function

    Private Async Function ExecuteIncreaseSalary() As Task
        Try

            If ValidateControlsIncrease() = False Then
                Exit Function
            End If

            Using model As New MSalaryIncrease(MyBase.Tag)
                AsyncLoader(True)

                Dim ObjResultObj = Await model.ExecuteIncreaseSalary(INDSlGroup.EditValue, INDSlFuncionalUnit.EditValue, INDSlPosition.EditValue, INDSEIncreasePercentage.EditValue, False, INDSlRateAprox.EditValue, INDSlContractModificationReason.EditValue, INDDeInitialDate.EditValue, INDRgPayrollPaid.EditValue, INDRgRetroactivo.EditValue, INDDeRetroactivoDate.EditValue, INDRgPayrollPaid.EditValue)
                AsyncLoader(False)

                If ObjResultObj.FirstOrDefault.StatusField = "001" Then

                    INDGcNewSalary.DataSource = ObjResultObj

                    If INDRgRetroactivo.EditValue = 1 Then
                        If INDDeRetroactivoDate.EditValue IsNot Nothing Then
                            ListOfRetroactive = Await model.ExecuteRetroactive(INDSlGroup.EditValue, INDSEIncreasePercentage.EditValue, INDDeRetroactivoDate.EditValue, INDRgPayrollPaid.EditValue, INDSlFuncionalUnit.EditValue, INDSlPosition.EditValue)
                            INDGcRetroactivo.DataSource = ListOfRetroactive

                        Else
                            Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado una Fecha para el Retroactivo"

                        End If
                    End If

                    Mensaje(EeventViewerImages.Informacion) = ObjResultObj.FirstOrDefault.MessageField
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                ElseIf ObjResultObj.FirstOrDefault.StatusField = "888" Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                    Mensaje(EeventViewerImages.Advertencia) = ObjResultObj.FirstOrDefault.MessageField
                Else
                    Mensaje(EeventViewerImages.Informacion) = ObjResultObj.FirstOrDefault.MessageField
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                End If

            End Using

        Catch ex As Exception
            AsyncLoader(False)
        End Try
        'If IncreaseSalaryList IsNot Nothing Then
        '    Mensaje(EeventViewerImages.Informacion) = "Se ha liquidado correctamente"
        '    INDGcNewSalary.DataSource = IncreaseSalaryList.List
        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        'Else
        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        'End If

    End Function



    ''' <summary>
    ''' Controla las filas del archivo Excel
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              If rows.Item(x).SpreadsheetRowToList(10).All(Function(cell) String.IsNullOrEmpty(cell)) Then
                                                  Return
                                              End If
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(10)})

                                              End SyncLock

                                          End Sub)
    End Sub

    ''' <summary>
    ''' Función para importar archivo
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ImportFile() As Task

        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Function
        End If

        AsyncLoader(True)
        Try
            'obtengo la rura del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            AsyncLoader(True)
            Await LoadImportFile()

            ' Valida que todo haya salido perfecto
            If _salaryIncrease.IsNotNullAndAny() And (If(_salaryIncreaseError?.Length, 0) = 0) Then
                Mensaje(EeventViewerImages.Informacion) = _salaryIncrease.FirstOrDefault.MessageField
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Liquidar) = True

                ' Cargar el DataSource con los resultados filtrados y asigna InitialDate
                INDGcNewSalary.DataSource = Nothing
                INDGcNewSalary.DataSource = _salaryIncrease
                INDDeInitialDate.EditValue = _salaryIncrease.FirstOrDefault.InitialDate

                ' Valida que hayan registros correctos y errores
            ElseIf _salaryIncrease.IsNotNullAndAny() And _salaryIncreaseError.IsNotNullAndAny() Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Liquidar) = True

                ' Cargar el DataSource con los resultados filtrados y asigna InitialDate
                INDGcNewSalary.DataSource = Nothing
                INDGcNewSalary.DataSource = _salaryIncrease
                INDDeInitialDate.EditValue = _salaryIncrease.FirstOrDefault.InitialDate

                ' Valida que solo existan errores
            ElseIf _salaryIncreaseError.IsNotNullAndAny() And (If(_salaryIncreaseError?.Length, 0) = 0) Then
                Mensaje(EeventViewerImages.Informacion) = _salaryIncreaseError.FirstOrDefault.MessageField
                INDGcNewSalary.DataSource = Nothing
            End If

            ''Retroactivo
            If _salaryIncrease.IsNotNullAndAny Then
                Await retroactiveForFile(accumulatedSuccess)
            End If

            'Lista los errores
            If _salaryIncreaseError.IsNotNullAndAny() Then
                Mensaje(EeventViewerImages.Advertencia) = $"El archivo presentó error en {_salaryIncreaseError.Length} registros"

                ' Crear una lista para almacenar los errores como cadenas
                Dim ListError As New List(Of String)

                If _salaryIncreaseError IsNot Nothing AndAlso _salaryIncreaseError?.Length > 0 Then

                    ' Iterar sobre cada elemento en _salaryIncreaseError y agregar información relevante a ListError
                    For Each errorItem As SP_SetIncreaseSalaryFromFile_Result In _salaryIncreaseError
                        ' Construir una cadena con información relevante del errorItem
                        Dim errorString As String = $"{errorItem.MessageField} - {errorItem.NitEmployee}"

                        ' Agregar la cadena a ListError
                        ListError.Add(errorString)
                    Next
                End If

                Using formulario As New FrmListErrors(ListError)
                    formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using

            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
            AsyncLoader(False)
        End Try
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Carga el archivo importando y la operacion de validacion con los nuevos salarios establecidos
    ''' </summary>
    Private Async Function LoadImportFile() As Task
        Await Task.Run(Sub()
                           accumulatedSuccess.Clear()
                           accumulatedErrors.Clear()
                           _salaryIncreaseError = Nothing
                           Dim totalProcessedItems As Integer = 0
                           Dim indexSend = 0
                           listErrosImportFile = New List(Of String())
                           ' HashSet para detectar duplicados 
                           Dim globalProcessedNits As New HashSet(Of String)()
                           Dim ssc = New SpreadsheetControl()
                           ssc.AllowDrop = False
                           ssc.LoadDocument(myStream)

                           Dim workBook As IWorkbook = ssc.Document
                           rows = workBook.Worksheets(0).Rows
                           If rows.LastUsedIndex <= 0 Then
                               Invoke(Sub()
                                          Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                      End Sub)
                               Exit Sub
                           End If
                           Dim totalItems = rows.LastUsedIndex
                           ' Calcular itemsSend de forma proporcional
                           Dim totalItemsCount = totalItems + 1
                           Dim numberOfBatches As Integer = CInt(Math.Ceiling(Math.Sqrt(totalItemsCount)))
                           Dim itemsSend As Integer = CInt(Math.Ceiling(totalItemsCount / numberOfBatches))

                           While (totalItems + 1) > totalProcessedItems
                               Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                               indexSend = totalProcessedItems
                               totalProcessedItems += quantityDetailsToProcess
                               listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                               SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)

                               Try
                                   Using model As New MSalaryIncrease(MyBase.Tag)
                                       AsyncLoader(True)
                                       If listRows.Any() Then
                                           ' Filtrar duplicados antes de enviar al stored procedure
                                           Dim uniqueRows As New List(Of ImportFileRow)()
                                           Dim duplicateRows As New List(Of ImportFileRow)()

                                           For Each row As ImportFileRow In listRows
                                               If row.Row.Count > 0 AndAlso Not String.IsNullOrEmpty(row.Row(0)) Then
                                                   Dim cedula = row.Row(0)
                                                   If globalProcessedNits.Add(cedula) Then
                                                       uniqueRows.Add(row)
                                                       globalProcessedNits.Add(cedula)
                                                   Else
                                                       duplicateRows.Add(row)
                                                   End If
                                               End If
                                           Next

                                           ' Procesar solo los registros unicos
                                           If uniqueRows.Any() Then
                                               Dim ObjResult = model.SetIncreaseSalaryFromFile(uniqueRows, Nothing)
                                               Dim Result = ObjResult

                                               ' Filtrar Result con elementos con StatusField = '001'
                                               Dim filterResults = Result.Where(Function(x) x.StatusField = "001").ToArray()
                                               If filterResults.Any() Then
                                                   accumulatedSuccess.AddRange(filterResults)
                                               End If

                                               ' Filtrar Result con elementos con StatusField = '777'
                                               Dim filteredErrors = Result.Where(Function(x) x.StatusField = "777").ToArray()
                                               If filteredErrors.Any() Then
                                                   accumulatedErrors.AddRange(filteredErrors)
                                               End If
                                           End If

                                           ' Agregar duplicados como errores directamente
                                           If duplicateRows.Any() Then
                                               For Each dupRow As ImportFileRow In duplicateRows
                                                   If dupRow.Row.Count > 0 AndAlso Not String.IsNullOrEmpty(dupRow.Row(0)) Then
                                                       accumulatedErrors.Add(New SP_SetIncreaseSalaryFromFile_Result With {
                                                           .StatusField = "777",
                                                           .MessageField = $"Empleado con cédula {dupRow.Row(0)} está duplicado en el archivo",
                                                           .NitEmployee = dupRow.Row(0)
                                                       })
                                                   End If
                                               Next
                                           End If
                                       End If
                                   End Using
                               Catch ex As Exception
                                   Throw
                               End Try

                           End While
                           If accumulatedSuccess.Any() Then
                               _salaryIncrease = accumulatedSuccess.ToArray()
                           End If
                           If accumulatedErrors.Any() Then
                               _salaryIncreaseError = accumulatedErrors.ToArray()
                           End If
                       End Sub)
    End Function

    ''' <summary>
    ''' Retroactivo por empleado
    ''' </summary>
    ''' <param name="resultFile"></param>
    ''' <returns></returns>
    Private Async Function retroactiveForFile(resultFile As List(Of SP_SetIncreaseSalaryFromFile_Result)) As Task

        Dim recordsWithRetroactive = resultFile.Where(Function(x) x.StatusField = "001" AndAlso x.WithRetroactive = 1 AndAlso x.RetroactiveInitialDate IsNot Nothing).ToList()
        Dim ListRetroactive As New List(Of RetroactiveC)
        Dim objRetroactive As New List(Of RetroactiveC)

        If recordsWithRetroactive IsNot Nothing AndAlso recordsWithRetroactive.Count > 0 Then
            INDLciRetroactivoDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLycgRetroactive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPayrollPaid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            For Each record In recordsWithRetroactive
                Dim groupId As Integer = record.GroupId
                Dim employeeId As Integer = record.EmployeeId
                Dim retroactiveDate As Date = record.RetroactiveInitialDate.Value
                Dim percentageIncrease As Decimal = record.PercentageIncrease
                Dim payrollPaid As Byte = CByte(record.PayrollPaidRetroactive)

                Using model As New MSalaryIncrease(MyBase.Tag)
                    objRetroactive = Await model.ExecuteRetroactive(groupId, percentageIncrease, retroactiveDate, payrollPaid, 0, 0, employeeId)
                    ListRetroactive.AddRange(objRetroactive)
                End Using
            Next
        Else
            INDLciRetroactivoDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLycgRetroactive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPayrollPaid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        INDGcRetroactivo.DataSource = ListRetroactive

    End Function

    ''' <summary>
    ''' Confirma el aumento de salario y guarda en las respectivas tablas
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ConfirmIncreaseSalary() As Task
        Try
            ' Se verifica si se seleccionó retroactivo para seleccionar el método correcto
            If INDRgRetroactivo.EditValue = 1 Then
                If ValidateControls() = False Then
                    Exit Function
                End If

                ''Método de confirmación con retroactivo
                Using model As New MSalaryIncrease(MyBase.Tag)
                    AsyncLoader(True)
                    Dim ObjResultObj = Await model.ExecuteIncreaseSalary(INDSlGroup.EditValue, INDSlFuncionalUnit.EditValue, INDSlPosition.EditValue, INDSEIncreasePercentage.EditValue, True, INDSlRateAprox.EditValue, INDSlContractModificationReason.EditValue, INDDeInitialDate.EditValue, INDRgPayrollPaid.EditValue, INDRgRetroactivo.EditValue, INDDeRetroactivoDate.EditValue, INDRgPayrollPaid.EditValue)
                    AsyncLoader(False)

                    If ObjResultObj.Any(Function(x) x.StatusField = "002") Then
                        Mensaje(EeventViewerImages.Informacion) = ObjResultObj.Where(Function(x) x.StatusField = "002").FirstOrDefault().MessageField
                        INDGcNewSalary.DataSource = Nothing
                    ElseIf ObjResultObj.Any(Function(x) x.StatusField = "999") Then
                        Mensaje(EeventViewerImages.Advertencia) = ObjResultObj.Where(Function(x) x.StatusField = "999").FirstOrDefault().MessageField
                    ElseIf ObjResultObj.Any(Function(x) x.StatusField = "888") Then
                        Mensaje(EeventViewerImages.Advertencia) = ObjResultObj.Where(Function(x) x.StatusField = "888").FirstOrDefault().MessageField
                    End If

                End Using
                CleanControls()
                ''Fin método

            Else 'Se ejecuta función que permite aumento de salario con valores de rejilla modificados y desde archivo Excel

                Dim IncreaseSalaryData As List(Of SP_IncreaseEmployeSalary_Result)

                If _salaryIncrease Is Nothing Then
                    IncreaseSalaryData = INDGcNewSalary.DataSource 'DataSource cargado por función ExecuteIncreaseSalary
                Else ' DataSource cargado por Archivo Excel
                    IncreaseSalaryData =
                    _salaryIncrease.Select(Function(item) New SP_IncreaseEmployeSalary_Result With {
                        .Id = item.Id,
                        .StatusField = item.StatusField,
                        .MessageField = item.MessageField,
                        .ContractId = item.ContractId,
                        .InitialContractNumber = item.InitialContractNumber,
                        .NitEmployee = item.NitEmployee,
                        .InitialDate = item.InitialDate,
                        .ModificationReasonId = item.ModificationReasonId,
                        .EmployeeName = item.EmployeeName,
                        .JobBondingDate = item.JobBondingDate,
                        .BasicSalary = item.BasicSalary,
                        .ValueIncrease = item.ValueIncrease,
                        .NewSalary = item.NewSalary,
                        .PercentageIncrease = item.PercentageIncrease,
                        .PositionCode = item.PositionCode,
                        .PositionName = item.PositionName,
                        .FunctionalUnitCode = item.FunctionalUnitCode,
                        .FunctionalUnitName = item.FunctionalUnitName}).ToList()
                End If

                ' Se valida que se haya llenado el DataSource por cualquiera de las dos funciones
                If IncreaseSalaryData Is Nothing Then
                    Exit Function
                End If

                'Método Nuevo
                Using Model As New MSalaryIncrease(MyBase.Tag)
                    AsyncLoader(True)
                    Dim ObjResult = Await Model.ConfirmIncreaseSalary(IncreaseSalaryData, INDSEIncreasePercentage.EditValue, INDSlContractModificationReason.EditValue, INDDeInitialDate.EditValue)
                    AsyncLoader(False)

                    If ObjResult.StatusField = "002" Then
                        Mensaje(EeventViewerImages.Informacion) = ObjResult.MessageField
                        INDGcNewSalary.DataSource = Nothing
                    ElseIf ObjResult.StatusField = "999" Then
                        Mensaje(EeventViewerImages.Advertencia) = ObjResult.MessageField
                    End If

                End Using
                CleanControls()
                'Fin método

            End If

        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try

    End Function


    Private Function CreateObjectResult(ObjResultObj As ActionResult(Of String)) As ActionResult(Of Object)
        Dim ObjResult As New ActionResult(Of Object)

        Dim AAA = Utils.DeserializeJsonToObject(ObjResultObj.ObjectEmbbeded)

        ObjResult.StateResult = ObjResultObj.StateResult
        ObjResult.ObjectEmbbeded = AAA

        Return ObjResult

    End Function

#Region "EditValueChanged"
    Private Sub INDSlGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlGroup.EditValueChanged
        If INDSlGroup.Properties.DataSource IsNot Nothing Then

            Dim Group = DirectCast(DirectCast(SearchLookUpEdit1View.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.PayrollRepository.PayrollGroupXpo)

            INDSlRateAprox.EditValue = Group.PayrollParameterId.AproximationValue

        End If
    End Sub

    Private Sub INDRgRetroactivo_EditValueChanged(sender As Object, e As EventArgs) Handles INDRgRetroactivo.EditValueChanged
        If INDRgRetroactivo.EditValue = 1 Then
            INDLciRetroactivoDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLycgRetroactive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciPayrollPaid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciRetroactivoDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLycgRetroactive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciPayrollPaid.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            INDRgRetroactivo.EditValue = 0
            INDDeRetroactivoDate.EditValue = Nothing
            INDRgPayrollPaid.EditValue = 0
        End If
    End Sub

    Private Sub INDRgRetroactivo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDRgRetroactivo.SelectedIndexChanged

    End Sub

    Private Async Sub INDBtnImportFileProducts_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileProducts.Click
        Await ImportFile()
    End Sub

    ''' <summary>
    ''' Evento para convertir el valor numérico de WithRetroactive a texto
    ''' </summary>
    Private Sub INDGvNewSalary_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvNewSalary.CustomColumnDisplayText
        ' Solo procesar la columna WithRetroactive
        If e.Column.FieldName = "WithRetroactive" Then
            ' Obtener el valor de la celda
            Dim retroactiveValue As Object = e.Value
            ' Convertir el valor numérico a texto
            If retroactiveValue IsNot Nothing Then
                Dim intValue As Integer = Convert.ToInt32(retroactiveValue)
                If intValue = 1 Then
                    e.DisplayText = "SI"
                Else
                    e.DisplayText = "NO"
                End If
            End If
        End If
    End Sub

#End Region




#End Region

End Class