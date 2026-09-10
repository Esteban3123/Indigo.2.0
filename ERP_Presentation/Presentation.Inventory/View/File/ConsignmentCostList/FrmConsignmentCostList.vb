'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-01-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************


#Region "Imports"
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraBars.Controls
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
#End Region


Public Class FrmConsignmentCostList
    Implements IConsignmentCostList


#Region "Properties"

    ''' <summary>
    ''' Representa el presentador del frm
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PConsignmentCostList

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As BillingSequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private idOperativeUnit As Int32

    ''' <summary>
    ''' id del proveedor
    ''' </summary>
    ''' <returns></returns>
    Public Property SupplierId As Integer Implements IConsignmentCostList.SupplierId
        Get
            Return INDSluSupplier.EditValue
        End Get
        Set(value As Integer)
            INDSluSupplier.EditValue = value
        End Set
    End Property
    Public Property Status As Boolean Implements IConsignmentCostList.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property
    Public Property EffectiveDate As Date Implements IConsignmentCostList.EffectiveDate
        Get
            Return INDDeDate.DateTime.Date
        End Get
        Set(value As Date)
            INDDeDate.DateTime = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IConsignmentCostList.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements IConsignmentCostList.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property Sequense As ConsignmentCostList Implements IConsignmentCostList.Sequense
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As ConsignmentCostList)
            Throw New NotImplementedException()
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

#End Region

#Region "Variables"
    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Private ConsignmentCostList As ConsignmentCostList


    '' <summary>
    '' Listado de eliminados de los ConsignmentCostListDetail
    '' </summary>
    '' <remarks></remarks>
    Private ListDeleteConsignmentCostList As List(Of ConsignmentCostListDetail)

    ''' <summary>
    ''' Posición del detalle que se va a editar
    ''' </summary>
    Private indexOfDetail As Integer

    ''' <summary>
    ''' Listado de ConsignmentCostListDetail
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteConsignmentCostListDetailRecord As List(Of ConsignmentCostListDetailRecord)

    ''' *******Variables importacion Excel*******

    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Private myStream As String = Nothing

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private listErrosImportFile As List(Of String())

    ''' <summary>
    ''' listado  que almacena los mensajes de los productos que se importan pero estos estan duplicados
    ''' </summary>
    ''' <remarks></remarks>
    Private duplicatedProductsFile As List(Of String)

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Private rows As RowCollection

    ''' <summary>
    ''' items procesados
    ''' </summary>
    ''' <remarks></remarks>
    Private totalProcessedItems As Integer = 0

    ''' <summary>
    ''' items que se van a enviar en cada proceso
    ''' </summary>
    ''' <remarks></remarks>
    Const itemsSend As Integer = 300

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Private listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    ''' <summary>
    ''' Indica la fecha con la que fue guardad el registro
    ''' </summary>
    Private SaveDate

    ''' <summary>
    ''' Lista de registro modificados
    ''' </summary>
    Private ListUpdateCreateConsignmentCostList As List(Of ConsignmentCostListDetail) = New List(Of ConsignmentCostListDetail)


#End Region

#Region "Load"

    ''' <summary>
    ''' Carga principales controles del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmConsignmentCostList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(INDlcRoot, True)
        Me.idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        LoadStatus()
        ConsignmentCostList = New ConsignmentCostList
        Presenter = New PConsignmentCostList(Me)
        EnabledControls()
        SetListActions()
        INDSluSupplier.Properties.DataSource = Presenter.ListSupplierByWarehouseConsignment()
        ValidateDateSelection()
        '''Inicializa  el excel a exportar
        INDEsbProductRate.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Código Producto", .Comment = "Código del producto"},
                            New ExcelColumn With {.Name = "Costo Nuevo", .Comment = "Valor costo nuevo"}
                        }
                    })
    End Sub
#End Region

#Region "Methods"
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub
    ''' <summary>
    '''  activa o desactiva controles
    ''' </summary>
    ''' <param name="value"></param>
    Public Sub EnabledControls(Optional value As Boolean = False)
        INDgcProduct.Enabled = value
        INDbtnAddProduct.Enabled = value
        INDBtnImportFileProducts.Enabled = value
        INDEsbProductRate.Enabled = value
    End Sub
    ''' <summary>
    ''' Valida que no se pueda seleccionar la fecha actual,
    ''' numberDay: numero de dias que este disponible el calendario
    ''' </summary>
    Public Sub ValidateDateSelection(Optional numberDay As Integer = 1)
        INDDeDate.Properties.MinValue = DateTime.Today.AddDays(numberDay)
        INDDeDate.EditValue = Nothing
    End Sub

    Public Property SupplierXpo As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDSluSupplier.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSluSupplier.Properties.DataSource = value
        End Set
    End Property

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    '''  Valida que la fecha al momento de actualizar no exista en el mismo rango que fue guardada
    ''' </summary>
    ''' <returns></returns>
    Public Function validateDateUpdate() As Integer
        Dim op = 0 ''No cumple
        If Me.ConsignmentCostList.Id > 0 Then
            Dim DateSave = SaveDate
            If DateSave.Equals(INDDeDate.EditValue) Then
                Dim sel = MessageIndigo.Show(String.Format("¿Existe una lista con la misma fecha de vigencia, desea actualizarla?", Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(d) d.SelectOption).Count()), MessageType.Question, Me.Text, Botones.SiNo)
                If sel = System.Windows.Forms.DialogResult.Yes Then
                    op = 1 '' Opcion Si
                Else
                    op = 2 '' Opcion No
                End If
            Else
                op = 3 ''Fechas distintas
            End If
        End If
        Return op
    End Function


    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Dim errors = ValidateControlsForms()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        AssigningValues()

        If validateDateUpdate() = 2 Then
            Exit Sub
        End If

        Try
            Using Model As New MConsignmentCostList(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ConsignmentCostList) = Await Model.SaveConsignmentCostList(Me.ConsignmentCostList)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    Me.ConsignmentCostList = result.ObjectEmbbeded
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDSluSupplier.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDSluSupplier.Enabled = False
            Throw ex
        End Try


    End Sub


    Private Function ValidateControlsForms() As String
        Dim errorList As New StringBuilder()
        If (INDDeDate.EditValue Is Nothing Or INDDeDate.EditValue Is String.Empty) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Fecha"))
        End If
        If (INDSluSupplier.EditValue Is Nothing) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Proveedor"))
        End If
        If INDgvProducts.RowCount = 0 Then
            errorList.AppendLine(String.Format("Se debe agregar minimo un producto", "Lista de productos"))
        End If
        Return errorList.ToString()
    End Function

    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.ConsignmentCostList IsNot Nothing AndAlso Me.ConsignmentCostList.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MConsignmentCostList(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteConsignmentCostList(Me.ConsignmentCostList)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    Throw ex
                End Try
            End If
        End If
    End Sub
    ''' <summary>
    ''' Abre formulario popup para agregar producto
    ''' </summary>
    ''' <param name="editMode"></param>
    Private Sub OpenFormDetail(editMode As Boolean)
        Using frm As New FrmConsignmentCostListDetail()
            Me.Cursor = BaseClass.ChangeCursorIndigo()
            frm.ListConsignmentProducts = ConsignmentCostList.ConsignmentCostListDetail.ToList()
            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            AddHandler frm.AddConsignmentProductDetail, AddressOf ReturnAddConsignmentProductDetail
            Dim transparent = New Base.FrmTransparent(frm, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub
    ''' <summary>
    ''' Obtiene lo que enviando el popup el listado de productos, para agregarlo en el listado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ReturnAddConsignmentProductDetail(sender As Object, e As AddConsignmentProductEventArgs)
        Dim existingItem = Me.ConsignmentCostList.ConsignmentCostListDetail.FirstOrDefault(Function(x) x.ProductId = e.ConsignmentCostListDetail.ProductId)
        If existingItem IsNot Nothing Then
            Dim index = Me.ConsignmentCostList.ConsignmentCostListDetail.IndexOf(existingItem)
            existingItem.CostNew = e.ConsignmentCostListDetail.CostNew
            Me.ConsignmentCostList.ConsignmentCostListDetail(index).CostNew = e.ConsignmentCostListDetail.CostNew
            ListUpdateCreateConsignmentCostList.Add(existingItem)
        Else
            Me.ConsignmentCostList.ConsignmentCostListDetail.Add(e.ConsignmentCostListDetail)
            ListUpdateCreateConsignmentCostList.Add(e.ConsignmentCostListDetail)
        End If
        INDgcProduct.DataSource = Nothing
        INDgcProduct.DataSource = ConsignmentCostList.ConsignmentCostListDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
    End Sub

    ''' <summary>
    ''' Crea los botones en la columna Acciones de la tabla
    ''' </summary>
    Private Sub SetListActions()
        IndigoGridView1.SetListAcction(INDgvProducts, {eAcciones.PorcentualIncrement, eAcciones.Remove}.ToList(), False, False)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvProducts.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

    Private Sub AssigningValues()
        ''Limpia el objeto del detalle, de forma que solo se enviaran los registros que fueron afectados
        ConsignmentCostList.ConsignmentCostListDetail.Clear()
        With ConsignmentCostList
            .SupplierId = SupplierId
            .EffectiveDate = EffectiveDate
            .Status = True
            .OperatingUnitId = idOperativeUnit

            ''Recorre listado de registros modificados
            ListUpdateCreateConsignmentCostList.ForEach(Sub(item)
                                                            .ConsignmentCostListDetail.Add(item)
                                                        End Sub)

            ''Recorre listado de registros eliminados
            If ListDeleteConsignmentCostList IsNot Nothing AndAlso ListDeleteConsignmentCostList.Count > 0 Then
                ListDeleteConsignmentCostList.ForEach(Sub(item)
                                                          item.MarkAsDeleted()
                                                          .ConsignmentCostListDetail.Add(item)
                                                      End Sub)
            End If

            ''Recorre listado de registros del historico eliminados
            If ListDeleteConsignmentCostListDetailRecord IsNot Nothing AndAlso ListDeleteConsignmentCostListDetailRecord.Count > 0 Then
                ListDeleteConsignmentCostListDetailRecord.ForEach(Sub(item)
                                                                      Dim detail = .ConsignmentCostListDetail.Where(Function(x) x.Id = item.ConsignmentCostListDetailId).FirstOrDefault
                                                                      If detail IsNot Nothing Then
                                                                          item.MarkAsDeleted()
                                                                          detail.ConsignmentCostListDetailRecord.Add(item)
                                                                      End If
                                                                  End Sub)
            End If
        End With
    End Sub




    Private Sub CleanControls()
        INDlcRoot.BeginUpdate()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Status = True
        ListDeleteConsignmentCostList = New List(Of ConsignmentCostListDetail)
        ListDeleteConsignmentCostListDetailRecord = New List(Of ConsignmentCostListDetailRecord)
        ListUpdateCreateConsignmentCostList = New List(Of ConsignmentCostListDetail)
        ConsignmentCostList = New ConsignmentCostList
        INDDeDate.EditValue = String.Empty
        INDSluSupplier.Properties.NullText = Nothing
        INDSluSupplier.EditValue = Nothing
        INDgcProduct.DataSource = Nothing
        EnabledControls()
        ValidateDateSelection()
        idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        INDSluSupplier.Enabled = True
        INDlcRoot.EndUpdate()
    End Sub

    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(INDSluSupplier.EditValue) AndAlso Not String.IsNullOrWhiteSpace(INDSluSupplier.EditValue) Then
            Try
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If
                Using Model As New MConsignmentCostList(CStr(Me.Tag))
                    AsyncLoader(True)
                    ConsignmentCostList = Await Model.GetConsignmentCostListBySupplierId(INDSluSupplier.EditValue, idOperativeUnit)
                    Me.BarraBotones.StatusRecordVisible = False
                    INDlcRoot.BeginUpdate()
                    If ConsignmentCostList IsNot Nothing Then
                        With ConsignmentCostList
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            EffectiveDate = .EffectiveDate
                            SupplierId = .SupplierId
                            INDgcProduct.DataSource = Nothing
                            SaveDate = .EffectiveDate
                            Status = .Status
                            Me.BarraBotones.StatusRecordVisible = True
                            INDgcProduct.DataSource = ConsignmentCostList.ConsignmentCostListDetail.ToList()
                        End With
                        Me.BarraBotones.PrepareToolbar(eAction.Update)
                    Else
                        ConsignmentCostList = New ConsignmentCostList
                    End If
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    AsyncLoader(False)
                    ActionsOnControls = True
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConsignmentCostList.ActionsOnControls
        Set(value As Boolean)
            INDSluSupplier.Focus()
        End Set
    End Property

    ''' <summary>
    ''' Elimina fila seleccionada
    ''' </summary>
    Private Sub RemoveItemSelected()
        If Not Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(d) d.SelectOption).Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un registro a eliminar"
            Exit Sub
        End If

        If MessageIndigo.Show(String.Format("¿Desea eliminar los registros seleccionados ({0})?", Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(d) d.SelectOption).Count()), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ListDeleteConsignmentCostList Is Nothing Then
                ListDeleteConsignmentCostList = New List(Of ConsignmentCostListDetail)
            End If

            If ListDeleteConsignmentCostListDetailRecord Is Nothing Then
                ListDeleteConsignmentCostListDetailRecord = New List(Of ConsignmentCostListDetailRecord)
            End If
            Dim index = 0
            Dim list = Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(d) d.SelectOption).ToList()
            While index < list.Count
                Dim item = list(index)
                If item.Id > 0 Then
                    For Each record In item.ConsignmentCostListDetailRecord
                        ''record.MarkAsDeleted()
                        ListDeleteConsignmentCostListDetailRecord.Add(record)
                    Next
                    item.MarkAsDeleted()
                    ListDeleteConsignmentCostList.Add(item)
                Else
                    Me.ConsignmentCostList.ConsignmentCostListDetail.Remove(item)
                End If
                index += 1
            End While
            ''Devuelve detalle con datos ya eliminados
            INDgcProduct.DataSource = Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
            INDgcProduct.RefreshDataSource()
        End If

        ''Valida en el listado de guardados, si se elimino, tambien se debe eliminar en listado de guardar
        Dim indexDetail = 0
        While indexDetail < ListUpdateCreateConsignmentCostList.Count
            Dim item = ListUpdateCreateConsignmentCostList(indexDetail)
            If Not ConsignmentCostList.ConsignmentCostListDetail.Contains(item) Then
                ListUpdateCreateConsignmentCostList.Remove(item)
            End If
            indexDetail += 1
        End While

    End Sub


    ''' <summary>
    ''' Realiza el calculo incremento porcentual
    ''' </summary>
    Private Sub IncrementPorcentual()
        For Each item As ConsignmentCostListDetail In Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(x) x.SelectOption)
            item.CostNew = ((item.CostNew) * INDSeIncrement.EditValue) + item.CostNew

            ''Se valida que no se vaya adicionar un mismo registro
            Dim existingItem As ConsignmentCostListDetail = ListUpdateCreateConsignmentCostList.Find(Function(x) x.Id = item.Id)
            If existingItem IsNot Nothing Then
                existingItem.CostNew = item.CostNew
            Else
                ListUpdateCreateConsignmentCostList.Add(item)
            End If
        Next
        INDgcProduct.DataSource = Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
        INDgcProduct.RefreshDataSource()
        INDSeIncrement.EditValue = 0
    End Sub

    ''' <summary>
    ''' Expande las filas agrupadas
    ''' </summary>
    Private Sub ExpandAllGroups()
        INDgvProducts.ExpandAllGroups()
    End Sub

    Private Async Function ImportFile() As Task
        If INDSluSupplier.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un proveedor"
            Exit Function
        End If
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
            'obtengo la ruta del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If
            AsyncLoader(True)
            Await LoadImportFileServices()
            ShowDuplicateProductsForm(duplicatedProductsFile)

            If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = $"El archivo presento error en {listErrosImportFile.Count} registros"

                Dim ErrorsExcel As New SpreadsheetControl
                ErrorsExcel.CreateNewDocument()
                ErrorsExcel.Document.Worksheets.ActiveWorksheet = ErrorsExcel.Document.Worksheets(0)
                Dim worksheet As Worksheet = ErrorsExcel.Document.Worksheets.ActiveWorksheet

                worksheet.Cells(0, 0).Value = "Codigo Producto"
                worksheet.Cells(0, 1).Value = "Costo Nuevo"
                worksheet.DefaultColumnWidth = 250
                Dim rows = 1
                For Each dato As String() In listErrosImportFile
                    Dim Columns = 0
                    For Each item In dato
                        worksheet.Cells(rows, Columns).Value = item
                        Columns += 1
                    Next
                    rows += 1
                Next

                Dim fileName As String = System.IO.Path.GetTempPath() & INDSluSupplier.EditValue & ".xlsx"
                ErrorsExcel.SaveDocument(fileName)
                System.Diagnostics.Process.Start(fileName)
            Else
                INDgcProduct.DataSource = ConsignmentCostList.ConsignmentCostListDetail.ToList()
            End If

        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
            AsyncLoader(False)
        End Try
        AsyncLoader(False)
    End Function

    Private Function LoadImportFileServices() As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrosImportFile = New List(Of String())
                                         Dim ssc = New SpreadsheetControl()
                                         ssc.AllowDrop = False
                                         ssc.LoadDocument(myStream)

                                         Dim workBook As IWorkbook = ssc.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Exit Sub
                                         End If
                                         Dim indexSend = 0
                                         totalProcessedItems = 0
                                         Dim totalItems = rows.LastUsedIndex
                                         While (totalItems + 1) > totalProcessedItems
                                             Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                             indexSend = totalProcessedItems
                                             totalProcessedItems += quantityDetailsToProcess
                                             listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                             SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)
                                             Using model As New MConsignmentCostList(Me.Tag.ToString())
                                                 Dim result = model.SetConsignmentConsListDetailFromFile(listRows.ToList())
                                                 If result.ListMessageResult IsNot Nothing And result.ListMessageResult.Count > 0 Then
                                                     listErrosImportFile.AddRange(result.ListMessageResult)
                                                 End If
                                                 If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                                                     ProcessConsignmentDetail(result.ObjectEmbbeded)
                                                 End If
                                             End Using
                                         End While
                                     End Sub)
    End Function

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(14)})
                                              End SyncLock
                                          End Sub)
    End Sub


    ''' <summary>
    ''' Cambia el estado del registro
    ''' </summary>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.ConsignmentCostList.Id) Then
            Try
                Using model As New MConsignmentCostList(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.ConsignmentCostList.Status
                    Dim result As ActionResult(Of ConsignmentCostList) = Await model.UpdateStateConsignmentCostList(Me.ConsignmentCostList.Id, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.ConsignmentCostList = result.ObjectEmbbeded
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDSluSupplier.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDSluSupplier.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = "No es posible modificar el estado"
        End If
    End Function

#Region "CopyPasteGridControl"
    Private Async Sub GetDataCopyPaste(sender, e)
        If sender.Equals(INDgcProduct) Then
            INDgcProduct.DataSource = Nothing
            If e.Rows(0).Item(0).Contains("Código Producto") Then
                e.Rows.Remove(e.Rows.ElementAt(0))
            End If

            AsyncLoader(True)
            Using Model As New MConsignmentCostList(Me.Tag.ToString())
                Dim result = Await Model.SetConsignmentConsListDetailFromCopyandPaste(e.Rows)
                'si ocurrio un error
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                End If

                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    ProcessConsignmentDetail(result.ObjectEmbbeded)
                    ShowDuplicateProductsForm(duplicatedProductsFile)
                End If
                INDgcProduct.DataSource = Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted).ToList()
            End Using
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Valida duplicados en archivo excel y en el copy paste
    ''' </summary>
    ''' <param name="items"></param>
    ''' <returns></returns>
    Function ProcessConsignmentDetail(ByVal items As Object)
        duplicatedProductsFile = New List(Of String)()
        For Each item In items
            Dim exists As Boolean = False
            For Each existingItem In Me.ConsignmentCostList.ConsignmentCostListDetail
                If existingItem.ProductId = item.ProductId AndAlso existingItem.CostNew = item.CostNew Then
                    exists = True
                    Exit For
                ElseIf existingItem.ProductId = item.ProductId AndAlso existingItem.CostNew <> item.CostNew Then
                    existingItem.CostNew = item.CostNew
                    item = existingItem
                    Exit For
                End If
            Next

            If exists Then
                Dim message As String = ""
                message = "El producto ya existe con el mismo costo y no se agregó: " & item.ProductCodeName
                duplicatedProductsFile.Add(message)
            Else
                ListUpdateCreateConsignmentCostList.Add(item)
                Me.ConsignmentCostList.ConsignmentCostListDetail.Add(item)
            End If
        Next
    End Function


    ''' <summary>
    ''' Muestra el resultado de duplicados en el cargue o copy paste excel
    ''' </summary>
    ''' <param name="duplicatedProductsFile"></param>
    Sub ShowDuplicateProductsForm(ByVal duplicatedProductsFile As List(Of String))
        If duplicatedProductsFile.Count > 0 Then
            Using formulario As New FrmListErrors(duplicatedProductsFile)
                formulario.Title = "Resultado validación importación datos"
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
            End Using
        End If
    End Sub

#End Region

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento cuando se da clic en el combobox del proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSluSupplier_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSluSupplier.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Maintenance.FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            INDSluSupplier.Properties.DataSource = Presenter.ListSupplierByWarehouseConsignment()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se ejecuta cuando se da clic al boton de agregar producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddProduct_Click(sender As Object, e As EventArgs)
        OpenFormDetail(False)
    End Sub

    ''' <summary>
    ''' Eventos de la columna Accion de la tabla lista de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction '', IndigoGridView1.ContexMenuActions
        Dim ConsignmentCostListDetail = DirectCast(INDgvProducts.GetFocusedRow(), ConsignmentCostListDetail)
        Dim button As Control = TryCast(sender, Control)
        'BarButtonItem
        Select Case (sender.tag)
            Case "PorcentualIncrement"
                OpenPopupContainer(sender, button)
            Case "Remove"
                RemoveItemSelected()
        End Select
    End Sub

    ''' <summary>
    ''' Abre el popup container para el incremento porcentual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="Button"></param>
    Private Sub OpenPopupContainer(sender, Button)
        If Not Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(d) d.SelectOption).Any() Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un registro para realizar el incremento porcentual"
            Exit Sub
        End If
        INDSeIncrement.EditValue = 0
        Dim editor As DevExpress.XtraEditors.SimpleButton = CType(sender, DevExpress.XtraEditors.SimpleButton)
        Dim point As Point = editor.PointToScreen(New Point(0, 0))
        ' Ajusta la posición X para que el popup se muestre al lado de la flecha desplegable de la celda.
        point.X -= editor.Width
        ' Ajusta la posición Y para que el popup se alinee con la fila.
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = INDgvProducts
        Dim info As DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo = CType(view.GetViewInfo(), DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo)
        Dim rowHeight As Integer = info.CalcRowHeight(Graphics.FromHwnd(IntPtr.Zero), view.FocusedRowHandle, view.RowHeight, True)
        point.Y -= editor.Width
        INDPccPorcentualIncrement.Location = point
        INDPccPorcentualIncrement.Show()
        Button.Parent.Parent.Hide()
        Task.Delay(200).ContinueWith(Sub(t) AddHandler LayoutControl2.MouseLeave, AddressOf LayoutControl2_MouseLeave)
    End Sub

    ''' <summary>
    '''  Obtiene la informacion cuando este control se cambia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSluSupplier_Properties_EditValueChanged(sender As Object, e As EventArgs) Handles INDSluSupplier.Properties.EditValueChanged
        INDDeDate.EditValue = String.Empty
        INDgcProduct.DataSource = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.StatusRecordVisible = False
        If INDSluSupplier.EditValue IsNot Nothing And INDSluSupplier.EditValue IsNot "" Then
            LoadControls()
            EnabledControls(True)
        End If
    End Sub

    ''' <summary>
    ''' Realiza el calculo de forma masiva o individual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbCalculation_Click(sender As Object, e As EventArgs) Handles INDSbCalculation.Click
        If INDSeIncrement.EditValue IsNot Nothing Then
            IncrementPorcentual()
            ExpandAllGroups()
        Else
            Mensaje(EeventViewerImages.Advertencia) = "Debe indicar el porcentaje a incrementar"
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' Elimina el negativo en el control SpinEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeIncrement_Properties_EditValueChanged(sender As Object, e As EventArgs) Handles INDSeIncrement.Properties.EditValueChanged
        If INDSeIncrement.Text.Contains("-") Then
            INDSeIncrement.Text = INDSeIncrement.Text.Replace("-", "")
        End If
    End Sub
    ''' <summary>
    ''' No permite ingresar -
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSeIncrement_Properties_KeyPress(sender As Object, e As KeyPressEventArgs) Handles INDSeIncrement.Properties.KeyPress
        If e.KeyChar = "-" Then
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Importa archivo excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnImportFileProducts_Click(sender As Object, e As EventArgs) Handles INDBtnImportFileProducts.Click
        Await ImportFile()
    End Sub

    ''' <summary>
    ''' CopyPaste de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        GetDataCopyPaste(sender, e)
    End Sub

    ''' <summary>
    ''' Oculta popup de incremento porcentual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub LayoutControl2_MouseLeave(sender As Object, e As EventArgs) Handles LayoutControl2.MouseLeave
        Dim cursorPosition As Point = LayoutControl2.PointToClient(Cursor.Position)
        ' Verifica si el cursor aún está dentro de los límites del panel
        If Not LayoutControl2.ClientRectangle.Contains(cursorPosition) Then
            ' Si no, oculta el PopupContainerControl
            INDPccPorcentualIncrement.Hide()
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando se edita el valor en la celda costo nuevo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDRiCostNew_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRiCostNew.EditValueChanging
        Dim valueCost As String = e.NewValue.ToString()
        Dim row As ConsignmentCostListDetail = INDgvProducts.GetFocusedRow
        ' Validar si la fila actual está seleccionada
        If Not row.SelectOption Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar la fila para actualizar el costo nuevo."
            e.Cancel = True
            Exit Sub
        End If
        ''Valida que no se vaya el valor vacio
        If valueCost = "" Then
            e.Cancel = True
            INDRiCostNew.NullText = e.OldValue
            Exit Sub
        End If

        ''Valida duplicados y  agrega al listado a guardar
        Dim existingItem As ConsignmentCostListDetail = ListUpdateCreateConsignmentCostList.Find(Function(x) x.Id = row.Id)
        If existingItem Is Nothing Then
            ListUpdateCreateConsignmentCostList.Add(row)
        End If
    End Sub

#End Region

#Region "Bar button"

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        ''Eliminar()
    End Sub

    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        INDDeDate.EditValue = String.Empty
        INDgcProduct.DataSource = Nothing
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        If operatingUnit IsNot Nothing Then
            Me.idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
            LoadControls()
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        ''Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        ''Throw New NotImplementedException()
    End Sub

#End Region

#Region "Selection"

    ''' <summary>
    ''' Método que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDProCheckSelectOption_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDProCheckSelectOption.EditValueChanging
        If e IsNot Nothing Then
            Dim row As ConsignmentCostListDetail = INDgvProducts.GetFocusedRow()
            If row IsNot Nothing Then
                row.SelectOption = e.NewValue
                VisibleCheck()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que se encarga de poner o no visible el check en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub VisibleCheck()
        If Me.ConsignmentCostList.ConsignmentCostListDetail IsNot Nothing AndAlso Me.ConsignmentCostList.ConsignmentCostListDetail.Count > 0 Then
            If Me.ConsignmentCostList.ConsignmentCostListDetail.Where(Function(item) item.SelectOption = True).Count = Me.ConsignmentCostList.ConsignmentCostListDetail.Count Then
                Me.INDColSelect.Image = Global.Presentation.Inventory.My.Resources.Resources.check
            Else
                Me.INDColSelect.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
            End If
        End If
    End Sub

    ''' <summary>
    ''' Método que se encarga de seleccionar los item visibles (con o sin filtro al dar doble click en el chck de la columna Sel.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDgcProduct_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles INDgcProduct.MouseDoubleClick
        If Me.ConsignmentCostList.ConsignmentCostListDetail IsNot Nothing AndAlso Me.ConsignmentCostList.ConsignmentCostListDetail.Count > 0 Then
            Dim hitPoint = Me.INDgvProducts.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDColSelect") Then

                    Dim listFilterXpCollection = INDgvProducts.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectOption = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectOption = False
                        Next
                        Me.INDColSelect.Image = Global.Presentation.Inventory.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectOption = True
                        Next
                        cont = (From l In listFilterXpCollection Where l.SelectOption = True).Count
                        If cont = Me.ConsignmentCostList.ConsignmentCostListDetail.Count Then
                            Me.INDColSelect.Image = Global.Presentation.Inventory.My.Resources.Resources.check
                        End If
                    End If
                    Me.INDgcProduct.RefreshDataSource()
                    Me.INDgcProduct.Invalidate()
                End If
            End If
        End If
    End Sub


#End Region

#Region "Show record details"

    ''' <summary>
    ''' Agrega los detalles del concepto al subnivel del grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvProducts_MasterRowGetChildList(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetChildListEventArgs) Handles INDgvProducts.MasterRowGetChildList
        Dim detail = INDgvProducts.GetFocusedObject(Of ConsignmentCostListDetail)
        If e.ChildList Is Nothing Then
            If detail IsNot Nothing Then
                If detail.Id > 0 Then
                    INDgvConsignmetCostListDetailRecord.ShowLoadingPanel()
                    ''Obtiene los 5 primero ordenados por fecha de creacion
                    Dim orderedRecords = (From r In detail.ConsignmentCostListDetailRecord).OrderByDescending(Function(x) x.CreationDate).ToList()
                    Dim records = orderedRecords.ToList()
                    e.ChildList = records
                    ''ListConsignmentCostListDetails.ToList().Where(Function(d) d.Id = detail.Id).FirstOrDefault.ConsignmentCostListDetailRecord.ToList()
                    INDgvConsignmetCostListDetailRecord.HideLoadingPanel()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Relaciona la rejilla principal con el subnivel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvProducts_MasterRowGetRelationName(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationNameEventArgs) Handles INDgvProducts.MasterRowGetRelationName
        e.RelationName = "ConsignmetCostListDetailRecord"
    End Sub

    ''' <summary>
    ''' Establece la relación que tiene la rejilla maestra con el detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvProducts_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles INDgvProducts.MasterRowGetRelationCount
        e.RelationCount = 1
    End Sub

    ''' <summary>
    ''' Abre popup para adicionar detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles INDbtnAddProduct.Click
        OpenFormDetail(False)
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

#End Region

End Class

''' <summary>
''' Clase , para que el suscriptor a ese evento será notificado y podrá ejecutar su propio código en respuesta
''' </summary>
Public Class AddConsignmentProductEventArgs
    Inherits EventArgs
    Property ConsignmentCostListDetail As ConsignmentCostListDetail
    Property EditMode As Boolean
End Class