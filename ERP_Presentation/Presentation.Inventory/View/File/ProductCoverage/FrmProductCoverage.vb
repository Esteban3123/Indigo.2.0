'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraBars
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmProductCoverage
    Implements IProductCoverage, ICustomizableForm

#Region "Properties and Variables"

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection
    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()
    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    ''' <summary>
    ''' nombre del modulo
    ''' </summary>
    Public Const MODULE_NAME As String = "Inventory"

    ''' <summary>
    ''' Variable que contiene la cabecera de la secuencia
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Variable que contiene el id de la secuencia detalle
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' The _state open pop up product
    ''' </summary>
    Private _stateOpenPopUpProduct As Boolean

    ''' <summary>
    ''' Total items Procesados
    ''' </summary>
    Private totalProcessedItems As Integer

    ''' <summary>
    ''' Total items
    ''' </summary>
    Private totalItems As Integer

    ''' <summary>
    ''' Cantidad de registros mandados a procesar
    ''' </summary>
    Private Const itemsSend As Integer = 600

    ''' <summary>
    ''' Almacena el listado de errores al importar
    ''' </summary>
    Private listErrorsImportFile As List(Of String)
    ''' <summary>
    ''' Variable para  la informacion del formato numerico
    ''' </summary>
    Private FormatNumber As Globalization.NumberFormatInfo

    ''' <summary>
    ''' Variable para determinar si contiene impuestos
    ''' </summary>
    ''' <remarks></remarks>
    Public flagIncludeTax As Boolean

    ''' <summary>
    ''' Obtiene el layout del frontal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IProductCoverage.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IProductCoverage.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    Public Property Sequence As Domain.Entities.InventorySequence Implements IProductCoverage.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' variable que contiene la entidad
    ''' </summary>
    Dim _productRate As ProductRate

    ''' <summary>
    ''' variable que se utiliza para saber si el frontal entra por modo busqueda o modo edicion
    ''' </summary>
    Dim _searchMode As Boolean

    ''' <summary>
    ''' variable que contiene el presentador
    ''' </summary>
    Dim _presenter As PProductCoverage

    ''' <summary>
    ''' entidad que almacena el registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInventory

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Public ListProductTemplate As List(Of ProductRateDetail)

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Public ListProductRateCondition As List(Of ProductRateGeneral)

    ''' <summary>
    ''' Listado de eliminados de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteProductTemplate As List(Of ProductRateDetail)

    ''' <summary>
    ''' Posición del detalle que se va a editar
    ''' </summary>
    Dim indexOfDetail As Integer

    ''' <summary>
    ''' Posición del detalle que se va a editar
    ''' </summary>
    Dim isViewMode As Boolean = False

    ''' <summary>
    ''' Entidad del detalle de la tarifa
    ''' </summary>
    Dim ProductRateDetail As ProductRateDetail

    Private ListDeleteCondition As List(Of ProductRateGeneral)

#Region "Properties Entity"
    ''' <summary>
    ''' Obtiene o establece el código
    ''' </summary>
    Public Property Code As String Implements IProductCoverage.Code
        Get
            If INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre del cubrimiento
    ''' </summary>
    Public Property NameCoverage As String Implements IProductCoverage.NameCoverage
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IProductCoverage.Status
        Get
            Return BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

#End Region

#End Region

#Region "Methods"

    Private Sub OpenFormDetail(editMode As Boolean)
        Using frm As New FrmProductRateDetail()
            AddHandler frm.SaveProductRate, AddressOf SaveRateDetail
            frm.ViewModeEditHold = True
            frm.flagIncludeTax = Me.flagIncludeTax
            frm.ToolBar.Visible = False
            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            frm.FrmProductCoverage = Me
            frm.EditMode = editMode
            frm.ProductRateDetail = ProductRateDetail
            Dim transparent As New FrmTransparent(frm, False)
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Public Sub DeleteRateConditions()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If ListDeleteCondition Is Nothing Then
                ListDeleteCondition = New List(Of ProductRateGeneral)
            End If
            Dim row = DirectCast(viewRules.GetFocusedRow(), ProductRateGeneral)
            Dim _indexEditRecord = Me.ListProductRateCondition.IndexOf(row)
            If ListProductRateCondition.Item(_indexEditRecord).Id > 0 Then
                ListProductRateCondition.Item(_indexEditRecord).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
            Else
                ListProductRateCondition.RemoveAt(_indexEditRecord)
            End If
            INDgcRules.DataSource = ListProductRateCondition.Where(Function(x) x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList()
            INDgcRules.RefreshDataSource()
        End If
    End Sub

    Private Sub EditDetail()
        ProductRateDetail = DirectCast(INDgvProducts.GetFocusedRow, ProductRateDetail)
        indexOfDetail = ListProductTemplate.IndexOf(ProductRateDetail)
        OpenFormDetail(True)
    End Sub

    Private Sub InactivateOrActivate(Value As Integer)
        If MessageIndigo.Show("Esta seguro que desea cambiar el estado", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim listHandlesSelected = (From x In INDgvProducts.GetSelectedRows() Select DirectCast(INDgvProducts.GetRow(x), ProductRateDetail)).ToList()

        listHandlesSelected.ToList().ForEach(Sub(x)
                                                 x.Status = Value
                                             End Sub)

        INDgcProduct.DataSource = ListProductTemplate
        INDgcProduct.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Metodo que pone el check en contratados y cotizados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectOptionsGrid(optionCheckContrated As Boolean?, optionCheckQuoted As Boolean?)
        Dim view As GridView = INDgvProducts
        Dim listHandlesSelected = view.GetSelectedRows
        If listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0 Then
            For i = 0 To listHandlesSelected.Count - 1
                Dim row As ProductRateDetail = view.GetRow(listHandlesSelected(i))
                If optionCheckContrated IsNot Nothing Then
                    row.Contracted = optionCheckContrated.Value
                End If
                If optionCheckQuoted IsNot Nothing Then
                    row.Quoted = optionCheckQuoted.Value
                End If
            Next
        End If
        INDgcProduct.RefreshDataSource()
    End Sub

    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(14)})
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' Valida que el producto no exista en el rango de fechas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateProductWithDates(_initialDate As DateTime, _endDate As DateTime, _productId As Integer, listCompare As List(Of ProductRateDetail)) As Integer
        Dim list As List(Of ProductRateDetail) = listCompare.FindAll(Function(item) ((_initialDate >= item.InitialDate AndAlso _initialDate <= item.EndDate) OrElse (_endDate >= item.InitialDate AndAlso _endDate <= item.EndDate) OrElse (_initialDate < item.InitialDate) AndAlso (_endDate > item.EndDate)) AndAlso (_productId = item.ProductId))
        Return list.Count
    End Function

    ''' <summary>
    ''' Saves the rate detail.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="AddProductRateEventArgs"/> instance containing the event data.</param>
    Private Sub SaveRateDetail(sender As Object, e As AddProductRateEventArgs)
        If e IsNot Nothing AndAlso e.ProductRateDetail IsNot Nothing Then
            If e.EditMode = False Then
                If ListProductTemplate Is Nothing Then
                    ListProductTemplate = New List(Of ProductRateDetail)
                End If
                ListProductTemplate.Add(e.ProductRateDetail)
            Else
                ListProductTemplate.Remove(ProductRateDetail)
                ListProductTemplate.Insert(indexOfDetail, e.ProductRateDetail)
            End If

            INDgcProduct.DataSource = Nothing
            INDgcProduct.DataSource = ListProductTemplate
            INDgcProduct.RefreshDataSource()
            INDgvProducts.ExpandAllGroups()
        End If
    End Sub

    ''' <summary>
    ''' Saves the rate detail.
    ''' </summary>
    ''' <param name="sender">The sender.</param>
    ''' <param name="e">The <see cref="AddProductRateEventArgs"/> instance containing the event data.</param>
    Private Sub SaveRateCondition(sender As Object, e As AddInfoToGridFormPrincipal)
        If e IsNot Nothing AndAlso e.ProductRateGeneralReturn IsNot Nothing Then
            If e.ModeEdit = False Then
                If ListProductRateCondition Is Nothing Then
                    ListProductRateCondition = New List(Of ProductRateGeneral)
                End If
                If e.ProductRateGeneralReturn?.Any() Then
                    For Each item In e.ProductRateGeneralReturn
                        If Not ListProductRateCondition.Contains(item) Then
                            ListProductRateCondition.Add(item)
                        End If
                    Next
                End If

            Else
                If e.ProductRateGeneralReturn?.Any() Then
                    For Each item In e.ProductRateGeneralReturn
                        ListProductRateCondition.Remove(item)
                        ListProductRateCondition.Insert(indexOfDetail, item)
                    Next
                End If
            End If

            INDgcRules.DataSource = Nothing
            INDgcRules.DataSource = ListProductRateCondition.Where(Function(x) x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList()
            INDgcRules.RefreshDataSource()
        End If
    End Sub

    ''' <summary>
    ''' News the ProductCoverage.
    ''' </summary>
    Private Async Function NewProductCoverage() As Task
        _productRate = New ProductRate() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProductTemplateInventory
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Devuelve el valor del OpenSearch.
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Cargamos los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Generates the document.
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._productRate.Code, Me._productRate.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._productRate.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._productRate.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", MODULE_NAME), Me._productRate.Code, Me._productRate.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", MODULE_NAME), Me._productRate.Code)
        End If
        Return Me._doc
    End Function

    ''' <summary>
    ''' Metodo que carga el listado xpo de los detalles y los convierte a entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadListDetail(productRateId As Integer) As Task
        If ListProductTemplate Is Nothing Then
            ListProductTemplate = New List(Of ProductRateDetail)
        End If
        INDgvProducts.ShowLoadingPanel()
        ListProductTemplate = Await Task.Factory.StartNew(Function()
                                                              Dim listXpo As XPCollection = _presenter.ListProductRateDetailByProductRateId(productRateId)

                                                              If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                                                                  For Each itemXpo As ProductRateDetailXpo In listXpo
                                                                      Dim productRateDetail As New ProductRateDetail
                                                                      productRateDetail.StartTracking()
                                                                      With productRateDetail
                                                                          .Id = itemXpo.Id
                                                                          .ProductRateId = itemXpo.ProductRateId.Id
                                                                          .RateClass = itemXpo.RateClass

                                                                          If itemXpo.RateClass = 1 Then
                                                                              .ProductId = itemXpo.ProductId.Id
                                                                              .ProductCodeName = itemXpo.ProductId.CodeName
                                                                              .ProductCode = itemXpo.ProductId.Code
                                                                              .ProductName = itemXpo.ProductId.Name
                                                                              .ProductControl = itemXpo.ProductId.ProductControl
                                                                              .ProductWithPriceControl = itemXpo.ProductId.ProductWithPriceControl
                                                                          ElseIf itemXpo.RateClass = 2 Then
                                                                              .PackageId = itemXpo.PackageId
                                                                              .PackageCode = itemXpo.Package.Code
                                                                              .PackageName = itemXpo.Package.Name
                                                                              .DoseType = itemXpo.DoseType
                                                                          End If

                                                                          .RateClassName = IIf(itemXpo.RateClass = 1, "Producto", "Paquete")
                                                                          .LiquidationType = itemXpo.LiquidationType
                                                                          .RateType = itemXpo.RateType
                                                                          .PercentageBasedOn = itemXpo.PercentageBasedOn
                                                                          .Percentage = itemXpo.Percentage

                                                                          If itemXpo.CupsId IsNot Nothing Then
                                                                              .CupsId = itemXpo.CupsId.Id
                                                                              .CupsCodeName = itemXpo.CupsId.CodeDescription
                                                                          End If
                                                                          If itemXpo.ContractDescriptionId IsNot Nothing Then
                                                                              .ContractDescriptionId = itemXpo.ContractDescriptionId.Id
                                                                              .ContractDesCodeName = itemXpo.ContractDescriptionId.CodeName
                                                                          End If

                                                                          .Status = itemXpo.Status
                                                                          .InitialDate = itemXpo.InitialDate
                                                                          .EndDate = itemXpo.EndDate
                                                                          .SalesValue = itemXpo.SalesValue
                                                                          .SalesValueWithSurcharge = itemXpo.SalesValueWithSurcharge
                                                                          .Contracted = itemXpo.Contracted
                                                                          .Quoted = itemXpo.Quoted
                                                                          .Observations = itemXpo.Observations
                                                                          .ATCPackageId = itemXpo.Package?.ATCId
                                                                          .MarkAsUnchanged()
                                                                      End With

                                                                      If itemXpo.DoseType = 2 Then
                                                                          Dim packageDetails = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetCollection(Of ProductRateDetailPackageXpo)(Function(m) m.ProductRateDetailId = itemXpo.Id)

                                                                          If packageDetails IsNot Nothing AndAlso packageDetails.Any() Then
                                                                              packageDetails.ForEach(Sub(m)
                                                                                                         Dim p = New ProductRateDetailPackage With {
                                                                                        .Id = m.Id,
                                                                                        .ItemCodeName = m.PackageDetail.ItemCodeName,
                                                                                        .ProductRateDetailId = m.ProductRateDetailId,
                                                                                        .PackageDetailId = m.PackageDetailId,
                                                                                        .ProductId = m.ProductId,
                                                                                        .Quantity = m.Quantity,
                                                                                        .DoseNumber = m.DoseNumber,
                                                                                        .ProductCodeName = m.InventoryProduct.CodeName,
                                                                                        .MainMedicine = m.PackageDetail.MainMedicine,
                                                                                        .Thinner = m.PackageDetail.Thinner,
                                                                                        .Vehicle = m.PackageDetail.Vehicle
                                                                                       }
                                                                                                         p.StartTracking()
                                                                                                         p.MarkAsUnchanged()
                                                                                                         productRateDetail.ProductRateDetailPackage.Add(p)
                                                                                                     End Sub)
                                                                          End If
                                                                      End If

                                                                      ListProductTemplate.Add(productRateDetail)
                                                                  Next
                                                              End If

                                                              Return ListProductTemplate
                                                          End Function)
        INDgvProducts.HideLoadingPanel()
        INDgcProduct.DataSource = ListProductTemplate
        INDgvProducts.ExpandAllGroups()
    End Function

    Public Async Function loadGeneralRate(ProductRateId As Integer) As Task
        If ListProductRateCondition Is Nothing Then
            ListProductRateCondition = New List(Of ProductRateGeneral)
        End If
        ListProductRateCondition = Await Task.Factory.StartNew(Function()
                                                                   Dim listXpo As XPCollection = _presenter.ListProductRateConditionsByProductRateId(_productRate.Id)

                                                                   If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                                                                       For Each itemXpo As ProductRateGeneralXpo In listXpo
                                                                           Dim ProductRateGeneral As New ProductRateGeneral
                                                                           ProductRateGeneral.StartTracking()
                                                                           With ProductRateGeneral
                                                                               .Id = itemXpo.Id
                                                                               .ProductRateId = itemXpo.ProductRateId.Id
                                                                               .RuleType = itemXpo.RuleType

                                                                               .ProductTypeId = itemXpo.ProductTypeId?.Id
                                                                               .ProductGroupId = itemXpo.ProductGroupId?.Id
                                                                               .ProductSubGroupId = itemXpo.ProductSubGroupId?.Id
                                                                               .InitialDate = itemXpo.InitialDate
                                                                               .EndDate = itemXpo.EndDate

                                                                               .RateType = itemXpo.RateType
                                                                               .ConditionType = itemXpo.ConditionType
                                                                               .PercentageBasedOn = itemXpo.PercentageBasedOn
                                                                               .Percentage = itemXpo.Percentage
                                                                               .SalesValue = itemXpo.SalesValue
                                                                               .Observations = itemXpo.Observations
                                                                               .EntityName = itemXpo.EntityName
                                                                               .MarkAsUnchanged()

                                                                               If itemXpo.ProductRateGeneralConditionXpo.Count > 0 Then
                                                                                   For Each item In itemXpo.ProductRateGeneralConditionXpo
                                                                                       Dim newDetail As ProductRateGeneralCondition = New ProductRateGeneralCondition
                                                                                       newDetail.ProductRateGeneralId = item.ProductRateGeneralId.Id
                                                                                       newDetail.InitialValue = item.InitialValue
                                                                                       newDetail.EndValue = item.EndValue
                                                                                       newDetail.Percentage = item.Percentage
                                                                                       newDetail.SalesValue = item.SalesValue
                                                                                       newDetail.MarkAsUnchanged()
                                                                                       .ProductRateGeneralCondition.Add(newDetail)
                                                                                   Next
                                                                               End If
                                                                           End With
                                                                           ListProductRateCondition.Add(ProductRateGeneral)
                                                                       Next
                                                                   End If

                                                                   Return ListProductRateCondition
                                                               End Function)
        INDgcRules.DataSource = ListProductTemplate
    End Function

    ''' <summary>
    ''' Metodo que carga el listado xpo de los detalles y los convierte a entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListDetailAux(_ProductRateDetail As List(Of ProductRateDetail))
        For Each item As ProductRateDetail In _ProductRateDetail
            If ListProductTemplate Is Nothing Then
                ListProductTemplate = New List(Of ProductRateDetail)
            End If

            ListProductTemplate.Add(item)
        Next
        INDgcProduct.DataSource = Nothing
        INDgcProduct.DataSource = ListProductTemplate
        INDgvProducts.ExpandAllGroups()
    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MProductRate(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetProductTemplate(Me.Code)
                    _productRate = resultOperation.ObjectEmbbeded
                    INDlcRoot.BeginUpdate()
                    If _productRate IsNot Nothing AndAlso _productRate.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_productRate.Id))
                            With _productRate
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                NameCoverage = .Name
                                Status = .Status
                            End With
                            Await LoadListDetail(_productRate.Id)

                            ''general
                            Await loadGeneralRate(_productRate.Id)

                            INDgcRules.DataSource = ListProductRateCondition
                            INDgcRules.RefreshDataSource()
                            INDgcRules.DataSource = ListProductRateCondition

                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._productRate.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _productRate.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_productRate.Id, Me.Tag.ToString(), Nothing, GetType(ProductRate).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewProductCoverage()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", MODULE_NAME)
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlcRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function



    ''' <summary>
    ''' Esta propiedad establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProductCoverage.ActionsOnControls
        Set(value As Boolean)

            INDlcRoot.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsbAddDetail.Enabled = value
            INDbtnAddRules.Enabled = value
            INDgcProduct.Enabled = value
            INDEsbProductRate.Enabled = value
            INDBtnImportFile.Enabled = value
            INDlcRoot.EndUpdate()

            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()
        INDlcgRoot.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        'Limpiar controles
        Code = String.Empty
        NameCoverage = Nothing
        INDgcProduct.DataSource = Nothing
        INDgcRules.DataSource = Nothing

        _stateOpenPopUpProduct = False
        ListProductTemplate = Nothing
        ListDeleteProductTemplate = Nothing
        ListProductRateCondition = Nothing

        totalProcessedItems = 0
        totalItems = 0
        listErrorsImportFile = Nothing

        _productRate = Nothing
        ProductRateDetail = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlcgRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With _productRate
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameCoverage
            .ProductRateDetail.Clear()
            'Saco los item que fueron agregados o que fueron editados para enviarlos a guardar
            Dim ListSave As List(Of ProductRateDetail) = ListProductTemplate.FindAll(Function(item) item.ChangeTracker.State = ObjectState.Added OrElse item.ChangeTracker.State = ObjectState.Modified)
            If ListSave IsNot Nothing AndAlso ListSave.Count > 0 Then
                ListSave.ForEach(Sub(item)
                                     .ProductRateDetail.Add(item)
                                 End Sub)
            End If

            'Se mete en el listado de guardar los items eliminados
            If ListDeleteProductTemplate IsNot Nothing AndAlso ListDeleteProductTemplate.Count > 0 Then
                ListDeleteProductTemplate.ForEach(Sub(item)
                                                      .ProductRateDetail.Add(item)
                                                  End Sub)
            End If
            If ListProductRateCondition?.Any() Then
                ListProductRateCondition.ForEach(Sub(item)
                                                     .ProductRateGeneral.Add(item)
                                                 End Sub)
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

#End Region

#Region "ICrud"
    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Eliminars this instance.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me._productRate IsNot Nothing AndAlso Me._productRate.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    For Each item In ListProductRateCondition
                        _productRate.ProductRateGeneral.Add(item)
                    Next

                    Using Model As New MProductRate(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteProductTemplate(Me._productRate)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        If INDgvProducts.RowCount = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar minimo una tarifa"
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MProductRate(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ProductRate) = Await Model.SaveProductTemplate(Me._productRate, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _productRate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._productRate = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Updates the state.
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me._productRate.Code) Then
            Try
                Using model As New MProductRate(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me._productRate.Status
                    Dim result As ActionResult(Of ProductRate) = Await model.UpdateStateProductTemplate(Me._productRate.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._productRate = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            If Not String.IsNullOrEmpty(Code.Trim()) Then
                Deshacer()
            Else
                Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            End If
        Else
            If String.IsNullOrEmpty(Code) Then
                Await Me.NewProductCoverage()
            Else
                Deshacer()
            End If
        End If
    End Sub
#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        _stateOpenPopUpProduct = Nothing
        listErrorsImportFile = Nothing
        _productRate = Nothing
        _searchMode = Nothing
        _presenter = Nothing
        _record = Nothing
        ListProductTemplate = Nothing
        ListDeleteProductTemplate = Nothing
        ListProductRateCondition = Nothing
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmProductCoverage control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrmProductCoverage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        INDEsbProductRate.AddExcelSheets(New ExcelSheet With {
                        .Columns = New List(Of ExcelColumn) From {
                            New ExcelColumn With {.Name = "Código Producto"},
                            New ExcelColumn With {.Name = "Tipo Liquidacion", .Comment = "1.Tarifa, 2.Servicio, 3.Tarifa y Servicio"},
                            New ExcelColumn With {.Name = "Tipo Tarifa", .Comment = "0.N/A, 1.Tarifa Fija , 2.Porcentaje"},
                            New ExcelColumn With {.Name = "Tipo Porcentaje", .Comment = "0.N/A, 1.Costo Promedio Ponderado , 2.Ultimo Costo"},
                            New ExcelColumn With {.Name = "CUPS", .Comment = "Codigo CUPS"},
                            New ExcelColumn With {.Name = "Descripcion Relacionada", .Comment = "Codigo Descripcion"},
                            New ExcelColumn With {.Name = "Fecha Inicial"},
                            New ExcelColumn With {.Name = "Fecha Final"},
                            New ExcelColumn With {.Name = "Porcentaje", .Comment = "Se debe diligenciar sin el signo %"},
                            New ExcelColumn With {.Name = "Precio de Venta"},
                            New ExcelColumn With {.Name = "Precio con Recargo"},
                            New ExcelColumn With {.Name = "Contratado"},
                            New ExcelColumn With {.Name = "Al Cotizar"},
                            New ExcelColumn With {.Name = "Observaciones"}
                        }
                    })

        Me.LayoutControls.SetIsCustomizable(Me.INDlcRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        FormatNumber = Me.indigo.CurrencyNumbertFormat
        _presenter = New PProductCoverage(Me)
        _presenter.GetSequence()
        '_presenter.LoadDefinitionLayout()
        IndigoGridControl1.RefreshGrid(INDgcProduct)
        If FormSearchObjects Is Nothing Then
            _searchMode = False
        End If
        IndigoGridView1.MoreInfoColunmns(INDgvProducts)
        INDgvProducts.ExpandAllGroups()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.View)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView11.SetListAcction(viewRules, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRules.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

        LoadStatus()
        Deshacer()

        Using model As New MCompanySettings(Me.Tag)
            Dim companySetting = Await model.GetCompanySettings()
            Me.flagIncludeTax = companySetting.SalePriceIncludeTax
        End Using
        changeNumericFormatByCurrency(FormatNumber)
    End Sub
#End Region

#Region "IdEntity"
    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me._productRate IsNot Nothing AndAlso Me._productRate.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region

#Region "Closing"
    ''' <summary>
    ''' Handles the FormClosing event of the FrmProductCoverage control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductCoverage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Handles the KeyDown event of the INDbteCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewProductCoverage()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub
#End Region

#Region "ContextMenu"

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        InactivateOrActivate(0)
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Handles the Click event of the INDsbAddDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddDetail_Click(sender As Object, e As EventArgs) Handles INDsbAddDetail.Click
        OpenFormDetail(False)
    End Sub

    ''' <summary>
    ''' Carga el archivo en lotes
    ''' </summary>
    ''' <returns></returns>
    Private Function loadImportFile() As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrorsImportFile = New List(Of String)
                                         Dim sddf = New DevExpress.XtraSpreadsheet.SpreadsheetControl()
                                         sddf.AllowDrop = False
                                         sddf.LoadDocument(myStream)
                                         Dim workBook As IWorkbook = sddf.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Exit Sub
                                         End If
                                         totalProcessedItems = 0
                                         totalItems = rows.LastUsedIndex
                                         Dim indexSend As Integer = 0
                                         While (totalItems + 1) > totalProcessedItems
                                             Dim quantityDetailsToProcess = If((totalItems + 1) < (totalProcessedItems + itemsSend), ((totalItems + 1) - totalProcessedItems), itemsSend)
                                             indexSend = totalProcessedItems
                                             totalProcessedItems += quantityDetailsToProcess
                                             listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()
                                             SetRow(indexSend + If(indexSend = 0, 1, 0), totalProcessedItems)
                                             Using model As New MProductRate(Me.Tag.ToString())
                                                 Dim result = model.SetCopyPasteOrImportFileProductRate(listRows.ToList(), Nothing)
                                                 If Not result?.MessageResult.Any() Then
                                                     If ListProductTemplate IsNot Nothing AndAlso ListProductTemplate.Count > 0 Then
                                                         For Each item In result.ObjectEmbbeded
                                                             If ValidateProductWithDates(item.InitialDate, item.EndDate, item.ProductId, ListProductTemplate) = 0 Then
                                                                 ListProductTemplate.Add(item)
                                                             End If
                                                         Next
                                                     Else
                                                         ListProductTemplate = result.ObjectEmbbeded
                                                     End If
                                                 Else
                                                     listErrorsImportFile.AddRange(result.MessageResult)
                                                 End If
                                             End Using
                                         End While
                                     End Sub)
    End Function

    Private Async Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        If _productRate.Id > 0 AndAlso _productRate.Status = False Then
            Mensaje(EeventViewerImages.Advertencia) = "La tarifa esta inactiva"
            Exit Sub
        End If
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Await Me.loadImportFile()
                    INDgcProduct.DataSource = Nothing
                    INDgcProduct.DataSource = ListProductTemplate
                    INDgvProducts.ExpandAllGroups()
                    If listErrorsImportFile?.Any Then
                        Using formulario As New FrmListErrors(listErrorsImportFile)
                            formulario.StartPosition = FormStartPosition.CenterParent
                            Dim transparent As New FrmTransparent(formulario, False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            transparent.ShowDialog(Me)
                        End Using
                    End If
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                End If
                AsyncLoader(False)
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Menu de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView11_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView11.ContexMenuActions, IndigoGridView11.Click_ButtonAction
        Dim btn As ProductRateGeneral = viewRules.GetFocusedRow()
        Select Case (sender.Tag)
            Case "View"
                viewRateCondition()
            Case "Remove"
                DeleteRateConditions()
        End Select
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar por primera vez el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProductCoverage_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteCode.Focus()
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDrepCheckContrated_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckContrated.EditValueChanging
        Dim entity = DirectCast(INDgvProducts.GetFocusedRow, ProductRateDetail)
        entity.Contracted = e.NewValue
    End Sub

    Private Sub INDrepCheckQuoted_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckQuoted.EditValueChanging
        Dim entity = DirectCast(INDgvProducts.GetFocusedRow, ProductRateDetail)
        entity.Quoted = e.NewValue
    End Sub

#End Region

#Region "CopyPaste"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If _productRate.Id > 0 AndAlso _productRate.Status = False Then
            Mensaje(EeventViewerImages.Advertencia) = "La tarifa esta inactiva"
            Exit Sub
        End If
        AsyncLoader(True)
        Using Model As New MProductRate(Me.Tag.ToString())
            Dim result = Await Model.SetCopyPasteOrImportFileProductRateAsync(Nothing, e.Rows)

            'si ocurrio un error
            If result.StatusCode = eStatusResult.EXCEPTION Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                AsyncLoader(False)
                Me.Cursor = System.Windows.Forms.Cursors.Default
                Exit Sub
            End If
            If ListProductTemplate IsNot Nothing AndAlso ListProductTemplate.Count > 0 Then
                For Each item In result.ObjectEmbbeded
                    If ValidateProductWithDates(item.InitialDate, item.EndDate, item.ProductId, ListProductTemplate) = 0 Then
                        ListProductTemplate.Add(item)
                    Else
                        result.MessageResult.Add("El producto " + item.ProductCodeName + " ya existe en el listado con las fechas " + item.InitialDate.ToString + " y " + item.EndDate.ToString)
                    End If
                Next
            Else
                ListProductTemplate = result.ObjectEmbbeded
            End If

            INDgcProduct.DataSource = Nothing
            INDgcProduct.DataSource = ListProductTemplate
            INDgvProducts.ExpandAllGroups()

            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default

        End Using
        AsyncLoader(False)
    End Sub

#End Region

#Region "PopupMenuShowing"

    Private Sub INDgvProducts_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgvProducts.PopupMenuShowing
        INDbarButtonSelectContrated.Visibility = BarItemVisibility.Always
        INDbarButtonUnSelectContrated.Visibility = BarItemVisibility.Always
        INDbarButtonSelectQuoted.Visibility = BarItemVisibility.Always
        INDbarButtonUnSelectQuoted.Visibility = BarItemVisibility.Always
        INDbarButtonEdit.Visibility = BarItemVisibility.Always
        INDbarButtonInactivate.Visibility = BarItemVisibility.Never
        INDBtnActivate.Visibility = BarItemVisibility.Never

        Dim Selected = (From x In INDgvProducts.GetSelectedRows() Select DirectCast(INDgvProducts.GetRow(x), ProductRateDetail)).ToList()

        If Selected.Where(Function(x) x.Status = 0).Count = Selected.Count Then
            INDbarButtonInactivate.Visibility = BarItemVisibility.Never
            INDBtnActivate.Visibility = BarItemVisibility.Always
        End If

        If Selected.Where(Function(x) x.Status = 1).Count = Selected.Count Then
            INDbarButtonInactivate.Visibility = BarItemVisibility.Always
            INDBtnActivate.Visibility = BarItemVisibility.Never
        End If

        PopupMenuActions.Manager = BarManager
        PopupMenuActions.ShowPopup(INDgvProducts.GridControl.PointToScreen(e.Point))
    End Sub

#End Region

#Region "ItemClick"

    Private Sub INDbarButtonEdit_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDbarButtonEdit.ItemClick
        EditDetail()
    End Sub

    Private Sub INDbarButtonUnSelectContrated_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDbarButtonUnSelectContrated.ItemClick
        SelectOptionsGrid(False, Nothing)
    End Sub

    Private Sub INDbarButtonUnSelectQuoted_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDbarButtonUnSelectQuoted.ItemClick
        SelectOptionsGrid(Nothing, False)
    End Sub

    Private Sub INDbarButtonInactivate_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDbarButtonInactivate.ItemClick
        InactivateOrActivate(0)
    End Sub

    Private Sub INDbarButtonSelectContrated_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDbarButtonSelectContrated.ItemClick
        SelectOptionsGrid(True, Nothing)
    End Sub

    Private Sub INDbarButtonSelectQuoted_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDbarButtonSelectQuoted.ItemClick
        SelectOptionsGrid(Nothing, True)
    End Sub

    Private Sub INDBtnActivate_ItemClick(sender As Object, e As ItemClickEventArgs) Handles INDBtnActivate.ItemClick
        InactivateOrActivate(1)
    End Sub
#End Region

#End Region

#Region "BarButtonEvents"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
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
        Deshacer()
        Nuevo()
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    Private Sub INDbtnAddRules_Click(sender As Object, e As EventArgs) Handles INDbtnAddRules.Click
        OpenAddRule(False)
    End Sub

    ''' <summary>
    ''' Metodo que abre el form de agregar reglas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenAddRule(modeEdit As Boolean, Optional definitionRateDetail As ProductRateGeneral = Nothing)
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmAddRule()
            AddHandler Formulario.AddInfoToGridFormPrincipal, AddressOf SaveRateCondition
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 1090
            Formulario.Height = 768
            Formulario.ModeEditAll = modeEdit
            Formulario.IsViewMode = isViewMode
            If definitionRateDetail IsNot Nothing Then 'Se envian estos dos parametros solo cuando se va a editar
                Formulario.ProductRateGeneralEdit = definitionRateDetail
            End If
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

    Private Sub viewRateCondition()
        isViewMode = True
        Dim data = DirectCast(viewRules.GetFocusedRow, ProductRateGeneral)
        OpenAddRule(True, data)
        isViewMode = False
    End Sub
#End Region

End Class