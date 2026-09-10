'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Threading
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Billing.MVP
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP
#End Region

Public Class FrmRequestUnitDoseInventory
    Implements IRequestUnitDoseInventory

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListRequestUnitDoseInventoryDetail As List(Of RequestUnitDoseInventoryDetail)

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListDeleteRequestUnitDoseInventoryDetail As List(Of RequestUnitDoseInventoryDetail)

    ''' <summary>
    ''' Cabacera
    ''' </summary>
    Dim RequestUnitDoseInventory As RequestUnitDoseInventory

    ''' <summary>
    ''' Tabla de detalle
    ''' </summary>
    Dim RequestUnitDoseInventoryDetail As RequestUnitDoseInventoryDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PRequestUnitDoseInventory

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As MixingStationSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMixingStation

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRequestUnitDoseInventory.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRequestUnitDoseInventory.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleCMConfiguration.Enabled = value
            INDsleCareCenter.Enabled = value
            INDsleWarehouse.Enabled = value
            INDdteDocumentDate.Enabled = value
            INDbtnRequests.Enabled = value
            INDgcRequest.Enabled = value
            INDlyRoot.EndUpdate()

            If value Then
                INDsleCMConfiguration.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IRequestUnitDoseInventory.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IRequestUnitDoseInventory.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequense As MixingStationSequence Implements IRequestUnitDoseInventory.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As MixingStationSequence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MixingStationSequenceDetail In Me._sequense.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

#End Region

#Region "ICrudBase"

    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If

        If RequestUnitDoseInventory.Status <> 3 Then 'Anular
            If (ListRequestUnitDoseInventoryDetail Is Nothing OrElse Not ListRequestUnitDoseInventoryDetail.Any(Function(rudid) rudid.ChangeTracker.State <> ObjectState.Deleted)) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar como mínimo una solicitud."
                Exit Sub
            End If
        End If

        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MRequestUnitDoseInventory(Me.Tag.ToString())
                Dim result = Await model.SaveRequestUnitDoseInventory(RequestUnitDoseInventory, _idCurrentSequense, _idOperativeUnit)
                If result.StateResult Then
                    If RequestUnitDoseInventory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.MixingStationSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        Mensaje(EeventViewerImages.Informacion) = result.Message
                    ElseIf RequestUnitDoseInventory.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If RequestUnitDoseInventory.Status = 3 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                        ElseIf RequestUnitDoseInventory.Status = 2 Then
                            Mensaje(EeventViewerImages.Informacion) = result.Message
                        ElseIf RequestUnitDoseInventory.Status = 1 Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If

                    Me.RequestUnitDoseInventory = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.AsyncLoader(False)
                    Me.Deshacer()
                Else
                    Me.AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequense Is Nothing OrElse _sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequense.IsManual Then
            Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
            Deshacer()
        Else
            Await NewRequestUnitDoseInventory()
        End If
    End Sub

    ''' <summary>
    ''' Deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListRequestUnitDoseInventory
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    Private Sub GetDetails()
        INDviewRequest.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub() GetListRequestDetail(), tokenAsync.Token)
    End Sub

    ''' <summary>
    ''' Obtiene los detalles de la tabla de estabilidad
    ''' </summary>
    Private Sub GetListRequestDetail()
        Dim result = Presenter.GetListDetails(RequestUnitDoseInventory.Id)

        If result IsNot Nothing AndAlso result.Count > 0 Then
            ListRequestUnitDoseInventoryDetail = New List(Of RequestUnitDoseInventoryDetail)
            For Each itemDetail In result
                Dim detail = New RequestUnitDoseInventoryDetail
                With detail
                    .Id = itemDetail.Id
                    .RequestUnitDoseInventoryId = itemDetail.RequestUnitDoseInventoryId.Id

                    .UnitDoseTypeId = itemDetail.UnitDoseTypeId.Id
                    .UnitDoseTypeCodeName = itemDetail.UnitDoseTypeId.CodeDescription

                    Dim ItemType As Integer
                    If {EUnitDoseTypeClass.Refilling, EUnitDoseTypeClass.Repackaging}.Contains(itemDetail.UnitDoseTypeId.MSClass) Then
                        ItemType = 1
                    Else 'NPT, Antibioticoterapia, Citostatico, Unguento, Magistral, No aplica
                        ItemType = 2
                    End If

                    .ItemType = ItemType
                    .ItemTypeName = If(ItemType = 1, "Medicamento", "Paquete")

                    .ATCId = Nothing
                    If itemDetail.ATCId IsNot Nothing Then
                        .ATCId = itemDetail.ATCId.Id
                        .ItemDescription = itemDetail.ATCId.CodeName
                    End If

                    .PackageId = Nothing
                    If itemDetail.PackageId IsNot Nothing Then
                        .PackageId = itemDetail.PackageId.Id
                        .ItemDescription = itemDetail.PackageId.CodeName
                    End If

                    .Quantity = itemDetail.Quantity

                    If itemDetail.AdministrationRouteId IsNot Nothing Then
                        .AdministrationRouteId = itemDetail.AdministrationRouteId.Id
                        .AdministrationRouteCodeName = itemDetail.AdministrationRouteId.CodeName
                    End If
                End With

                ListRequestUnitDoseInventoryDetail.Add(detail)
            Next
        End If

        If Not tokenAsync.IsCancellationRequested Then
            INDgcRequest.SafeInvoke(Sub()
                                        INDviewRequest.HideLoadingPanel()
                                        INDgcRequest.DataSource = ListRequestUnitDoseInventoryDetail
                                    End Sub)
        End If
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Asigna los valores a la entidad principal
    ''' </summary>
    Private Sub AssigningValues()
        With RequestUnitDoseInventory
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .CMConfigurationId = INDsleCMConfiguration.EditValue
            .CareCenterCode = INDsleCareCenter.EditValue
            .WarehouseId = INDsleWarehouse.EditValue
            .DocumentDate = INDdteDocumentDate.EditValue

            .RequestUnitDoseInventoryDetail.Clear()

            If ListRequestUnitDoseInventoryDetail IsNot Nothing AndAlso ListRequestUnitDoseInventoryDetail.Count > 0 Then
                ListRequestUnitDoseInventoryDetail.ForEach(Sub(item) .RequestUnitDoseInventoryDetail.Add(item))
            End If

            If ListDeleteRequestUnitDoseInventoryDetail IsNot Nothing AndAlso ListDeleteRequestUnitDoseInventoryDetail.Count > 0 Then
                ListDeleteRequestUnitDoseInventoryDetail.ForEach(Sub(item) .RequestUnitDoseInventoryDetail.Add(item.MarkAsDeleted()))
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Code = String.Empty
        INDsleCMConfiguration.EditValue = Nothing
        INDsleCMConfiguration.Properties.NullText = String.Empty
        INDsleCareCenter.EditValue = Nothing
        INDsleCareCenter.Properties.NullText = String.Empty
        INDsleWarehouse.EditValue = Nothing
        INDsleWarehouse.Properties.NullText = String.Empty
        INDdteDocumentDate.EditValue = Nothing
        INDgcRequest.DataSource = Nothing
        RequestUnitDoseInventory = Nothing
        RequestUnitDoseInventoryDetail = Nothing
        ListRequestUnitDoseInventoryDetail = Nothing
        ListDeleteRequestUnitDoseInventoryDetail = Nothing
        INDlyRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Bloquea el registro
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.RequestUnitDoseInventory IsNot Nothing AndAlso Me.RequestUnitDoseInventory.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.RequestUnitDoseInventory.Code, Me.RequestUnitDoseInventory.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.RequestUnitDoseInventory.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.RequestUnitDoseInventory.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.RequestUnitDoseInventory.Code, Me.RequestUnitDoseInventory.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.RequestUnitDoseInventory.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la información
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Using Model As New MRequestUnitDoseInventory(CStr(Me.Tag))
                AsyncLoader(True)
                RequestUnitDoseInventory = (Await Model.GetRequestUnitDoseInventory(INDbtnCode.Text.Trim)).ObjectEmbbeded
                If RequestUnitDoseInventory IsNot Nothing AndAlso RequestUnitDoseInventory.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(RequestUnitDoseInventory.Id))
                        With RequestUnitDoseInventory
                            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationUser"), .ConfirmationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideUser"), .AnnulmentUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("OverrideDate"), .AnnulmentDate)

                            Me.BarraBotones.StatusRecordVisible = True
                            Me.BarraBotones.StatusRecord = .Status.ToString()
                            If .Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            ElseIf .Status = 2 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If

                            Code = .Code

                            INDsleCMConfiguration.EditValue = .CMConfigurationId
                            INDsleCMConfiguration.Properties.NullText = .CMConfigurationCodeName

                            INDsleCareCenter.EditValue = .CareCenterCode
                            INDsleCareCenter.Properties.NullText = .CareCenterCodeName

                            INDsleWarehouse.EditValue = .WarehouseId
                            INDsleWarehouse.Properties.NullText = .WarehouseCodeName

                            INDdteDocumentDate.EditValue = .DocumentDate
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.RequestUnitDoseInventory.Code)
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = RequestUnitDoseInventory.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(RequestUnitDoseInventory.Id)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                    GetDetails()
                Else
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Await Me.NewRequestUnitDoseInventory()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDbtnCode.Focus()
                    End If
                End If
            End Using
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewRequestUnitDoseInventory() As Task
        If Me._sequense.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No existe secuencia numérica para el formulario"
            INDbtnCode.Focus()
            Exit Function
        End If
        RequestUnitDoseInventory = New RequestUnitDoseInventory
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.MixingStationSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.MixingStationSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Not Me._sequense.Sequential Then
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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
            Else
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            End If
            BarraBotones.StatusRecordVisible = True
            BarraBotones.StatusRecord = "1"
        End If
        INDdteDocumentDate.EditValue = GetDateServer()
    End Function

    ''' <summary>
    ''' Metodo que abre el from para agregar los RIAS
    ''' </summary>
    Private Sub OpenFormRequestUnitDoseInventoryDetail(EditMode As Boolean)
        Using formulario As New FrmRequestUnitDoseInventoryDetail()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddRequestUnitDoseInventoryDetailArgs, AddressOf ReturnAddEventArgs
            formulario.EditModeDetail = EditMode
            If EditMode Then
                If RequestUnitDoseInventoryDetail.ATCId IsNot Nothing Then
                    formulario.ListRequestUnitDoseInventoryDetailCompare = (From x In ListRequestUnitDoseInventoryDetail Where x.ATCId <> RequestUnitDoseInventoryDetail.ATCId).ToList()
                Else
                    formulario.ListRequestUnitDoseInventoryDetailCompare = (From x In ListRequestUnitDoseInventoryDetail Where x.PackageId <> RequestUnitDoseInventoryDetail.PackageId).ToList()
                End If
            Else
                formulario.ListRequestUnitDoseInventoryDetailCompare = ListRequestUnitDoseInventoryDetail
            End If
            formulario.RequestUnitDoseInventoryDetail = RequestUnitDoseInventoryDetail
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(885, 700)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que agrega el detalle a la entidad principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReturnAddEventArgs(sender As Object, e As AddRequestUnitDoseInventoryDetail)
        If e IsNot Nothing Then

            If e.EditMode = False Then 'Si se esta insertando
                If ListRequestUnitDoseInventoryDetail Is Nothing Then
                    ListRequestUnitDoseInventoryDetail = New List(Of RequestUnitDoseInventoryDetail)
                End If
                ListRequestUnitDoseInventoryDetail.Add(e.RequestUnitDoseInventoryDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else 'Si se esta actualizando
                ListRequestUnitDoseInventoryDetail.Remove(RequestUnitDoseInventoryDetail)
                ListRequestUnitDoseInventoryDetail.Insert(IndexEditRecord, e.RequestUnitDoseInventoryDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDgcRequest.DataSource = Nothing
            INDgcRequest.DataSource = ListRequestUnitDoseInventoryDetail
        End If
    End Sub

    ''' <summary>
    ''' Edita un detalle
    ''' </summary>
    Private Sub EditDetail()
        RequestUnitDoseInventoryDetail = DirectCast(INDviewRequest.GetFocusedRow(), RequestUnitDoseInventoryDetail)
        IndexEditRecord = ListRequestUnitDoseInventoryDetail.IndexOf(RequestUnitDoseInventoryDetail)
        OpenFormRequestUnitDoseInventoryDetail(True)
    End Sub

    ''' <summary>
    ''' Elimina un RIAS
    ''' </summary>
    Private Sub DeleteDetail()
        Dim entityDelete = DirectCast(INDviewRequest.GetFocusedRow(), RequestUnitDoseInventoryDetail)
        If entityDelete.Id > 0 Then
            If ListDeleteRequestUnitDoseInventoryDetail Is Nothing Then
                ListDeleteRequestUnitDoseInventoryDetail = New List(Of RequestUnitDoseInventoryDetail)
            End If
            ListDeleteRequestUnitDoseInventoryDetail.Add(entityDelete)
        End If
        ListRequestUnitDoseInventoryDetail.Remove(entityDelete)
        INDgcRequest.DataSource = Nothing
        INDgcRequest.DataSource = ListRequestUnitDoseInventoryDetail
        Mensaje(EeventViewerImages.Informacion) = "Detalle eliminado de la rejilla correctamente"
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

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PRequestUnitDoseInventory(Me)
        Presenter.GetSequence()
        LoadStatus()
        Deshacer()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewRequest, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewRequest.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento click del boton agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnRequests_Click(sender As Object, e As EventArgs) Handles INDbtnRequests.Click
        RequestUnitDoseInventoryDetail = Nothing
        OpenFormRequestUnitDoseInventoryDetail(False)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Enter del campo código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewRequestUnitDoseInventory()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditDetail()
            Case "Remove"
                DeleteDetail()
        End Select
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se ejecuta al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseInventory_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Se ejecuta al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestUnitDoseInventory_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If

        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMConfiguration_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCMConfiguration.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2063, Nothing, True)
            INDsleCMConfiguration.Properties.DataSource = Presenter.InitializeCMConfiguration()
        End If
    End Sub



    ''' <summary>
    ''' Abre el form de almacén
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleWarehouse_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleWarehouse.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(302, Nothing, True)
            If INDsleCareCenter.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(INDsleCareCenter.EditValue) Then
                INDsleWarehouse.Properties.DataSource = Presenter.InitializeWarehouse(INDsleCareCenter.EditValue)
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Datasource central de mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMConfiguration_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCMConfiguration.QueryPopUp
        If INDsleCMConfiguration.Properties.DataSource Is Nothing Then
            INDsleCMConfiguration.Properties.DataSource = Presenter.InitializeCMConfiguration()
        End If
    End Sub

    ''' <summary>
    ''' Datasurce almacenes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleWarehouse.QueryPopUp
        If INDsleWarehouse.Properties.DataSource Is Nothing AndAlso INDsleCareCenter.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(INDsleCareCenter.EditValue) Then
            INDsleWarehouse.Properties.DataSource = Presenter.InitializeWarehouse(INDsleCareCenter.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del centro atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCareCenter.QueryPopUp
        If INDsleCMConfiguration.EditValue IsNot Nothing Then
            Dim DataSource = Presenter.InitializeCareCenter(INDsleCMConfiguration.EditValue)
            If DataSource Is Nothing OrElse DataSource.Count = 0 Then
                INDsleCareCenter.Properties.DataSource = Nothing
                Me.Mensaje(EeventViewerImages.Advertencia) = "No existen centros de atención asociados a la central de mezclas seleccionada"
                Exit Sub
            End If
            INDsleCareCenter.Properties.DataSource = DataSource
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de centro atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCareCenter_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCareCenter.EditValueChanged
        If INDsleCareCenter.EditValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(INDsleCareCenter.EditValue) Then
            INDsleWarehouse.EditValue = Nothing
            INDsleWarehouse.Properties.NullText = String.Empty
            INDsleWarehouse.Properties.DataSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCMConfiguration_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCMConfiguration.EditValueChanged
        INDsleCareCenter.Properties.DataSource = Nothing
        INDsleCareCenter.EditValue = Nothing
        INDsleCareCenter.Properties.NullText = String.Empty
    End Sub
#End Region

#End Region

#Region "BarButtons"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        RequestUnitDoseInventory.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        RequestUnitDoseInventory.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        RequestUnitDoseInventory.Status = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        RequestUnitDoseInventory.Status = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.MixingStationSequenceDetail IsNot Nothing Then
            If Me._sequense.MixingStationSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.MixingStationSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ click anular
    ''' </summary>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            RequestUnitDoseInventory.Status = 3
            Guardar()
        End If
    End Sub

#End Region

End Class