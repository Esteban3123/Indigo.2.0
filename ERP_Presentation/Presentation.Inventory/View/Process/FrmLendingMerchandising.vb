Imports Presentation.Base
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Domain.Base.Entities
Imports DevExpress.Utils.Menu
Imports System.Windows.Forms
Imports System.ComponentModel

Public Class FrmLendingMerchandising
    Implements ILendingMerchandising, ICustomizableForm

#Region "Constant"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence
    ''' <summary>
    ''' Contiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Dim dateServerVariable As DateTime
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory
    ''' <summary>
    ''' Listado de registros bloqueados
    ''' </summary>
    ''' <remarks></remarks>
    Private ListBlockRecord As List(Of BlockRecordInventory)
    ''' <summary>
    ''' Variable que representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PLendingMerchandising
    ''' <summary>
    ''' Objeto cabecera de prestamo de mercancia
    ''' </summary>
    ''' <remarks></remarks>
    Dim LoanMerchandise As LoanMerchandise
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64
    ''' <summary>
    ''' Prefijo seleccionado
    ''' </summary>
    Private _prefixSelected As String
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Variable que representa la entidad de parametros
    ''' </summary>
    ''' <remarks></remarks>
    Dim _settingsInventory As SettingInventory
    ''' <summary>
    ''' Lista de tipo de prestamos
    ''' </summary>
    ''' <remarks></remarks>
    Dim leanType As List(Of Tuple(Of Integer, String))
    ''' <summary>
    ''' listado del detalle de prestamo para eliminar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listLoanMerchandiseDetailDelete As List(Of LoanMerchandiseDetail)
    ''' <summary>
    ''' Lista de detalle de prestamo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim listLoanMerchandiseDetail As List(Of LoanMerchandiseDetail)
    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim indexEditRecord As Integer
    ''' <summary>
    ''' Objeto detalle de un prestamo de mercancia
    ''' </summary>
    ''' <remarks></remarks>
    Dim LoanMerchandiseDetail As LoanMerchandiseDetail
    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer
#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Obtiene o Asigna el consecutivo del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ILendingMerchandising.Code
        Get
            If (INDbteConsecutive.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteConsecutive.EditValue
            End If
        End Get
        Set(value As String)
            INDbteConsecutive.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna la lista de productos en la rejilla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceProducts As DevExpress.Xpo.XPCollection Implements ILendingMerchandising.DataSourceProducts
        Get
            Return Me.INDgcLoanMerchandiseDetail.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            Me.INDgcLoanMerchandiseDetail.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna la lista de almacenes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceStock As DevExpress.Xpo.XPInstantFeedbackSource Implements ILendingMerchandising.DatasourceStock
        Get
            Return Me.INDsleStock.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            Me.INDsleStock.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna la lista de terceros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceThird As DevExpress.Xpo.XPInstantFeedbackSource Implements ILendingMerchandising.DataSourceThird
        Get
            Return Me.INDsleThird.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            Me.INDsleThird.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o asigna los tipo de solicitud de prestamo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DataSourceTypeOfLoan As List(Of Tuple(Of Integer, String)) Implements ILendingMerchandising.DataSourceTypeOfLoan
        Get
            Return Me.INDgleLeanType.Properties.DataSource
        End Get
        Set(value As List(Of Tuple(Of Integer, String)))
            Me.INDgleLeanType.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna la fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateDocument As Date Implements ILendingMerchandising.DateDocument
        Get
            Return INDdeDateDocument.EditValue
        End Get
        Set(value As Date)
            INDdeDateDocument.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna el comentario 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements ILendingMerchandising.Observation
        Get
            Return INDmeObservation.EditValue
        End Get
        Set(value As String)
            INDmeObservation.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Action control del formulario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ILendingMerchandising.ActionsOnControls
        Set(value As Boolean)
            INDlycLendingMerchandising.BeginUpdate()
            INDbteConsecutive.Enabled = Not value
            INDdeDateDocument.Enabled = value
            INDgleLeanType.Enabled = value
            INDsleStock.Enabled = value
            INDsleThird.Enabled = value
            INDmeObservation.Enabled = value
            INDsbAddProducts.Enabled = False
            INDgcLoanMerchandiseDetail.Enabled = value
            INDlycLendingMerchandising.EndUpdate()
            If value Then
                INDdeDateDocument.Focus()
            Else
                INDbteConsecutive.Focus()
            End If
        End Set
    End Property
    ''' <summary>
    ''' tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ILendingMerchandising.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Control de secuencias numericas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As InventorySequence Implements ILendingMerchandising.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property
    Public Property Status As Boolean Implements ILendingMerchandising.Status
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
    ''' <summary>
    ''' Obtiene o Asigna el Id del tipo de prestamo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdLeanType As Integer? Implements ILendingMerchandising.IdLeanType
        Get
            Return INDgleLeanType.EditValue
        End Get
        Set(value As Integer?)
            INDgleLeanType.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o Asigna el ID del almacen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdStores As Integer? Implements ILendingMerchandising.IdStores
        Get
            Return INDsleStock.EditValue
        End Get
        Set(value As Integer?)
            INDsleStock.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o Asigna el ID del tercero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdThirdParty As Integer? Implements ILendingMerchandising.IdThirdParty
        Get
            Return INDsleThird.EditValue
        End Get
        Set(value As Integer?)
            INDsleThird.EditValue = value
        End Set
    End Property
#End Region

#Region "Methods"

    ''' <summary>
    ''' Método utilizado para cargar los parametros
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadParameters() As Task
        Using Model As New MEntranceVoucher(Me.MyTag)
            _settingsInventory = Await Model.GetSettingInventory(_idOperativeUnit)
            If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
                Deshacer()
                Exit Function
            End If

            Me.ValidateDate()
        End Using
    End Function

    ''' <summary>
    ''' Consulta la fecha de los parametros y establece la fecha minima y maxima de la fecha del documento
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateDate()
        If _settingsInventory Is Nothing OrElse _settingsInventory.Id = 0 Then
            Exit Sub
        End If

        Dim dateMin As DateTime = Convert.ToDateTime(_settingsInventory.Year.ToString + "/" + _settingsInventory.Month.ToString + "/01")
        INDdeDateDocument.Properties.MinValue = dateMin
        INDdeDateDocument.Properties.MaxValue = GetDateServer()
    End Sub

    ''' <summary>
    ''' Obtiene el id del detalle de secuencia por el prefijo seleccionado
    ''' </summary>
    ''' <param name="prefix">Prefijo a buscar</param>
    ''' <returns>Id del detalle de secuencia</returns>
    Private Function GetIdSequenceByPrefix(ByVal prefix As String) As Int64
        If Me._sequence IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing AndAlso Me._sequence.InventorySequenceDetail.Any(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)) Then
            Return Me._sequence.InventorySequenceDetail.Where(Function(d) d.Prefix IsNot Nothing AndAlso d.Prefix.Trim().Equals(prefix)).FirstOrDefault().Id
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub GetDate()
        Using model As New MLendingMerchandising(CStr(Tag))
            dateServerVariable = Await model.GetServerDate()
            DateDocument = dateServerVariable
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlycLendingMerchandising.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        DateDocument = Nothing
        IdLeanType = Nothing
        INDgleLeanType.Properties.NullText = String.Empty
        INDgleLeanType.EditValue = Nothing
        IdStores = Nothing
        INDsleStock.Properties.NullText = String.Empty
        INDsleStock.EditValue = Nothing
        IdThirdParty = Nothing
        INDsleThird.Properties.NullText = String.Empty
        INDsleThird.EditValue = Nothing
        listLoanMerchandiseDetail = Nothing
        listLoanMerchandiseDetailDelete = Nothing
        INDgcLoanMerchandiseDetail.DataSource = Nothing
        INDmeObservation.EditValue = Nothing
        INDsleStock.Properties.ReadOnly = False
        INDgleLeanType.Properties.ReadOnly = False
        Me._doc = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.ValidateDate()
        GetDate()
        INDlycLendingMerchandising.EndUpdate()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteConsecutive.Text = ReturnValue
        If INDbteConsecutive.Text <> String.Empty Then
            Await LoadControls()
            If INDbteConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteConsecutive.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MLendingMerchandising(CStr(Me.Tag))
                    AsyncLoader(True)
                    LoanMerchandise = Await Model.GetLoanMerchadiseByCode(INDbteConsecutive.Text.Trim)
                    INDlycLendingMerchandising.BeginUpdate()
                    If LoanMerchandise IsNot Nothing AndAlso LoanMerchandise.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(LoanMerchandise.Id))
                            listLoanMerchandiseDetail = Model.ListLoanMerchandiseDetailByIdLoanMerchandise(LoanMerchandise.Id, False)
                            INDgcLoanMerchandiseDetail.DataSource = Nothing
                            INDgcLoanMerchandiseDetail.DataSource = listLoanMerchandiseDetail
                            With LoanMerchandise
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)
                                Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.StatusRecordVisible = True
                                BarraBotones.OperatingUnitValue = .OperatingUnitId
                                Select Case .Status
                                    Case 1
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                                    Case Else
                                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                        ReadOnlyControls(True)
                                End Select

                                If .Status <> 1 Then
                                    INDdeDateDocument.Properties.MinValue = .DocumentDate
                                End If

                                Code = .Code
                                DateDocument = .DocumentDate
                                IdLeanType = .LoanType
                                INDsleThird.Properties.NullText = .CodeNamethird
                                IdThirdParty = .ThirdPartyId
                                INDsleStock.Properties.NullText = .CodeNameWareHouse
                                IdStores = .WarehouseId
                                Observation = .Observation
                                BarraBotones.StatusRecord = .Status.ToString()
                                INDgleLeanType.Properties.ReadOnly = True
                                If IdLeanType = 1 Then
                                    INDsleStock.Properties.ReadOnly = False
                                Else
                                    INDsleStock.Properties.ReadOnly = True
                                End If
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.LoanMerchandise.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = LoanMerchandise.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(LoanMerchandise.Id, Me.Tag.ToString(), Nothing, GetType(LoanMerchandise).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Activar) = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, LoanMerchandise.Id, 0, LoanMerchandise.Id)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDsbAddProducts.Enabled = True
                            If LoanMerchandise.Status <> 1 Then
                                INDsbAddProducts.Enabled = False
                            End If
                            INDdeDateDocument.Focus()
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewLoanMerchandise()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbteConsecutive.Focus()
                        End If
                    End If
                    INDlycLendingMerchandising.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteConsecutive.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Llena el control con el listado de tipos de prestamos
    ''' </summary>
    Private Sub CreateLeanType()
        leanType = New List(Of Tuple(Of Integer, String))
        leanType.Add(New Tuple(Of Integer, String)(1, "Entrada"))
        leanType.Add(New Tuple(Of Integer, String)(2, "Salida"))
        INDgleLeanType.Properties.DataSource = leanType.ToList()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateRegistered"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para generar el registro de bloqueo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub GenerateBlockRecord()
        Using model As New MBlockRecordAndSequense(MyTag)
            Dim result = Await model.GetBlockRecord(Me.Tag, Me.LoanMerchandise.Id)
            If result IsNot Nothing AndAlso result.Id = 0 Then
                Dim state = New Domain.Base.Entities.ObjectChangeTracker
                state.State = Domain.Base.Entities.ObjectState.Added
                record = New BlockRecordInventory With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .FormId = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .RecordId = Me.LoanMerchandise.Id}
                Dim operation = Await model.SaveBlockRecord(record)
                record = operation.ObjectEmbbeded
            Else
                Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
                record = result
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Editar un LoanMerchandiseDetail
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditProduct()
        LoanMerchandiseDetail = DirectCast(INDgvLoanMerchandiseDetail.GetFocusedRow(), LoanMerchandiseDetail)
        indexEditRecord = listLoanMerchandiseDetail.IndexOf(LoanMerchandiseDetail)
        Using formulario As New FrmPopUpAddProductsLoanMerchandise
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddProductLoanMerchandiseDetail, AddressOf ReturnAddLoanMerchandiseDetail
            formulario.Size = New Drawing.Size(800, 730)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.LoanMerchandiseDetailEdit = LoanMerchandiseDetail
            formulario.WareHouseId = IdStores
            formulario.PresentationProduct = LoanMerchandiseDetail.PresentationProduct
            formulario.EditMode = True
            If IdLeanType = 1 Then
                formulario.LoanType = FrmPopUpAddProductsLoanMerchandise.ELeanType.Input
            Else
                formulario.LoanType = FrmPopUpAddProductsLoanMerchandise.ELeanType.Output
            End If
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Eliminar un LoanMerchandiseDetail
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteProduc()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            LoanMerchandiseDetail = DirectCast(INDgvLoanMerchandiseDetail.GetFocusedRow(), LoanMerchandiseDetail)
            If LoanMerchandiseDetail.Id > 0 Then
                If listLoanMerchandiseDetailDelete Is Nothing Then
                    listLoanMerchandiseDetailDelete = New List(Of LoanMerchandiseDetail)
                End If
                While LoanMerchandiseDetail.LoanMerchandiseDetailBatchSerial.Count > 0
                    If LoanMerchandiseDetail.LoanMerchandiseDetailBatchSerial(0).Id > 0 Then
                        LoanMerchandiseDetail.LoanMerchandiseDetailBatchSerial(0).MarkAsDeleted()
                    Else
                        LoanMerchandiseDetail.LoanMerchandiseDetailBatchSerial.Remove(LoanMerchandiseDetail.LoanMerchandiseDetailBatchSerial(0))
                    End If
                End While
                LoanMerchandiseDetail.MarkAsDeleted()
                listLoanMerchandiseDetailDelete.Add(LoanMerchandiseDetail)
            End If
            listLoanMerchandiseDetail.Remove(LoanMerchandiseDetail)
            INDgcLoanMerchandiseDetail.DataSource = Nothing
            INDgcLoanMerchandiseDetail.DataSource = listLoanMerchandiseDetail
            INDgleLeanType.Properties.ReadOnly = If(listLoanMerchandiseDetail.Any(), True, False)
        End If
    End Sub

    ''' <summary>
    ''' Agregar un LoanMerchandiseDetail
    ''' </summary>
    ''' <param name="p1"></param>
    ''' <remarks></remarks>
    Private Sub OpenAddProducts(p1 As Object)
        Using Formulario As New FrmPopUpAddProductsLoanMerchandise()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler Formulario.AddProductLoanMerchandiseDetail, AddressOf ReturnAddLoanMerchandiseDetail
            Formulario.Size = New Drawing.Size(800, 730)
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
            Formulario.WareHouseId = IdStores
            If IdLeanType = 1 Then
                Formulario.LoanType = FrmPopUpAddProductsLoanMerchandise.ELeanType.Input
            Else
                Formulario.LoanType = FrmPopUpAddProductsLoanMerchandise.ELeanType.Output
            End If
            Dim transparent = New Base.FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
            Me.Cursor = System.Windows.Forms.Cursors.Default
        End Using
    End Sub

    ''' <summary>
    ''' metodo para obtener lo que se retorna del formulario modal de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddLoanMerchandiseDetail(sender As Object, e As AddProductLoanMerchandiseDetailEventArgs)
        If listLoanMerchandiseDetail Is Nothing Then
            listLoanMerchandiseDetail = New List(Of LoanMerchandiseDetail)
        End If
        If e.EditMode = True Then
            listLoanMerchandiseDetail.Remove(LoanMerchandiseDetail)
            listLoanMerchandiseDetail.Insert(indexEditRecord, e.LoanMerchandiseDetail)
        Else
            listLoanMerchandiseDetail.Add(e.LoanMerchandiseDetail)
        End If
        INDgcLoanMerchandiseDetail.DataSource = Nothing
        INDgcLoanMerchandiseDetail.DataSource = listLoanMerchandiseDetail
        INDgleLeanType.Properties.ReadOnly = True
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva cuenta por pagar
    ''' </summary>
    Private Async Function NewLoanMerchandise() As Task
        If Me._settingsInventory Is Nothing OrElse Me._settingsInventory.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SettingParameter", NAME_MODULE)
            Exit Function
        End If

        LoanMerchandise = New LoanMerchandise()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
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
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
        BarraBotones.StatusRecordVisible = True
        BarraBotones.StatusRecord = "1"
        GetDate()
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.LoanMerchandise IsNot Nothing AndAlso Me.LoanMerchandise.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Code = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Code = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' metodo para generar kla indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim content = String.Format(ResourceManager.GetString("FrmLendingMerchandising_IndexContent ", NAME_MODULE), LoanMerchandise.Code, If(INDsleThird.Text = String.Empty, INDsleThird.Properties.NullText, INDsleThird.Text), DateDocument, If(INDsleStock.Text = String.Empty, INDsleStock.Properties.NullText, INDsleStock.Text))
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = content,
                .CreationDate = dateServer,
                .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName,
                .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.LoanMerchandise.Code & "#$",
                .IdForm = Me.Tag,
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.LoanMerchandise.Code),
                .Update = dateServer,
                .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = content
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.LoanMerchandise.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' asigna los valores a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        With LoanMerchandise
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DateDocument
            .LoanType = IdLeanType
            .OperatingUnitId = BarraBotones.OperatingUnitValue
            .WarehouseId = IdStores
            .ThirdPartyId = IdThirdParty
            .Prefix = Me._prefixSelected
            .Observation = Observation
            .Status = 1
            For Each item In listLoanMerchandiseDetail
                .LoanMerchandiseDetail.Add(item)
            Next
            If listLoanMerchandiseDetailDelete IsNot Nothing Then
                For Each item In listLoanMerchandiseDetailDelete
                    .LoanMerchandiseDetail.Add(item)
                Next
            End If

        End With
    End Sub

#End Region

#Region "CRUD"

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If LoanMerchandise IsNot Nothing AndAlso LoanMerchandise.Status < 3 Then
            If ValidateControls() = True Then
                If INDgvLoanMerchandiseDetail.RowCount = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddLoanMerchandiseDetail", NAME_MODULE)
                    Exit Sub
                End If
            Else
                Exit Sub
            End If
            AssigningValues()
        End If
        Try
            Using model As New MLendingMerchandising(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveLoanMerchadise(LoanMerchandise, _idCurrentSequence, Me._sequence)
                If result.StateResult = True Then
                    If LoanMerchandise.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    LoanMerchandise = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, LoanMerchandise.Id, 0, LoanMerchandise.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, LoanMerchandise.Id, 0, LoanMerchandise.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, LoanMerchandise.Id, 0, LoanMerchandise.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteConsecutive.Enabled = False
                    If result.StateResult = False And result.StateResultAux = False Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If LoanMerchandise.Id > 0 Then
                        LoanMerchandise = Await model.GetLoanMerchadiseByCode(Code)
                    Else
                        LoanMerchandise = New LoanMerchandise
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' metodo para guardar y confirmar o para actualizar y confirmar
    ''' </summary>
    ''' <param name="action"></param>
    ''' <remarks></remarks>
    Private Async Sub SaveOrUpdateAndConfirm(action As Integer)
        If ValidateControls() = True Then
            If INDgvLoanMerchandiseDetail.RowCount = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("AddLoanMerchandiseDetail", NAME_MODULE)
                Exit Sub
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MLendingMerchandising(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveAndConfirmLoadMerchadise(LoanMerchandise, _idCurrentSequence, action, Me._sequence)
                If result.StateResult = True And result.StateResultAux = True Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    LoanMerchandise = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, LoanMerchandise.Id, 0, LoanMerchandise.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = True And result.StateResultAux = False Then
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    LoanMerchandise = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.BarraBotones.PrintReport(PrintReportAction.Confirm, LoanMerchandise.Id, 0, LoanMerchandise.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                ElseIf result.StateResult = False And result.StateResultAux = False Then
                    AsyncLoader(False)
                    INDbteConsecutive.Enabled = False
                    If result.MessageResult IsNot Nothing Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    End If
                    If LoanMerchandise.Id > 0 Then
                        LoanMerchandise = Await model.GetLoanMerchadiseByCode(Code)
                    Else
                        LoanMerchandise = New LoanMerchandise
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteConsecutive.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Throw New NotImplementedException
    End Sub

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewLoanMerchandise()
        End If
    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Almacen", .FieldName = "WarehouseId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Almacen", .FieldName = "ThirdPartyId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Tipo préstamo", .FieldName = "LoanTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAllLoanMerchandise
            BarraBotones.PrepareToolbar(eAction.OnlyFind)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Evento que se dispara la presionar click en la barra de botones en buscar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de guardar y confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        SaveOrUpdateAndConfirm(1)
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barra botones: Nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        varImp = 2
        Guardar()
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
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso operatingUnit.Id <> Me._idOperativeUnit Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.InventorySequenceDetail IsNot Nothing Then
                If Not Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar

    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de actualizar confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        SaveOrUpdateAndConfirm(2)
    End Sub

    ''' <summary>
    ''' Barra botones: Click anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            LoanMerchandise.Status = 3
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        ' Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, ListAddBill.Item(0).Id, 0, ListAddBill.Item(0).Code)
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, LoanMerchandise.Id, 0, LoanMerchandise.Id)
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        dateServerVariable = Nothing
        record = Nothing
        ListBlockRecord = Nothing
        Presenter = Nothing
        LoanMerchandise = Nothing
        _idCurrentSequence = Nothing
        _prefixSelected = Nothing
        _idOperativeUnit = Nothing
        _settingsInventory = Nothing
        leanType = Nothing
        listLoanMerchandiseDetailDelete = Nothing
        listLoanMerchandiseDetail = Nothing
        indexEditRecord = Nothing
        LoanMerchandiseDetail = Nothing
        varImp = Nothing
    End Sub

    Private Async Sub FrmLendingMerchandising_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlycLendingMerchandising, True)

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PLendingMerchandising(Me)
        IndigoGridControl1.RefreshGrid(INDgcLoanMerchandiseDetail)
        Presenter.GetSequense()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await Me.LoadParameters()

        LoadStatus()
        IndigoGridView1.MoreInfoColunmns(INDgvLoanMerchandiseDetail)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDgvLoanMerchandiseDetail, ListActions)
        CreateLeanType()
        Deshacer()
    End Sub
#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDtxtConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteConsecutive.KeyDown
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
                    Await Me.NewLoanMerchandise()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountsPayable_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de consecutivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtxtConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteConsecutive.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleStock_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleStock.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmStores With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeStores()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en el boton del control de Terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThird_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThird.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Common.FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeThirdParty()
        End If
    End Sub



#End Region

#Region "EditValueChanged"

    'Private Sub INDgleLeanType_EditValueChanged(sender As Object, e As EventArgs) Handles INDgleLeanType.EditValueChanged
    '    If INDsleStock.EditValue IsNot Nothing AndAlso INDgleLeanType.EditValue IsNot Nothing Then
    '        INDsbAddProducts.Enabled = True
    '    Else
    '        INDsbAddProducts.Enabled = True
    '    End If
    'End Sub

    Private Sub INDsleStock_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleStock.EditValueChanged
        If INDsleStock.EditValue IsNot Nothing AndAlso INDgleLeanType.EditValue IsNot Nothing Then
            INDsbAddProducts.Enabled = True
        Else
            INDsbAddProducts.Enabled = True
        End If
        If Me.INDsleStock.EditValue IsNot Nothing Then
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("O") Then
                Dim store = If(INDGdvStock.DataSource IsNot Nothing, DirectCast(DirectCast(INDGdvStock.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.InventoryRepository.WarehouseXpo), Nothing)
                Me._idCurrentSequence = Me.GetIdSequenceByPrefix(If(store IsNot Nothing, store.Prefix, Me.LoanMerchandise.Prefix))
                Me._prefixSelected = If(store IsNot Nothing, store.Prefix, Me.LoanMerchandise.Prefix)
            End If
        End If
    End Sub
#End Region

#Region "Click_ButtonAction"

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditProduct()
            Case "Remove"
                DeleteProduc()
        End Select
    End Sub

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditProduct()
            Case "Remove"
                DeleteProduc()
        End Select
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al hacer click en el boton de agregar factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAddProducts_Click(sender As Object, e As EventArgs) Handles INDsbAddProducts.Click
        OpenAddProducts(Nothing)
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmLendingMerchandising_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteConsecutive.Enabled Then
            INDbteConsecutive.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de Almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleStock_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleStock.QueryPopUp
        If DatasourceStock Is Nothing Then
            Presenter.InitializeStores()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThird_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThird.QueryPopUp
        If DataSourceThird Is Nothing Then
            Presenter.InitializeThirdParty()
        End If
    End Sub

    Private Sub INDRptPceBatchSerial_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRptPceBatchSerial.QueryPopUp
        Dim record = DirectCast(INDgvLoanMerchandiseDetail.GetFocusedRow, LoanMerchandiseDetail)
        INDGcBatchSerial.DataSource = Nothing
        INDGcBatchSerial.DataSource = record.LoanMerchandiseDetailBatchSerial
    End Sub

#End Region

#End Region

End Class