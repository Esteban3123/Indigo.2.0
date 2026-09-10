'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/12/2020
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Text
Imports System.Threading
Imports System.Windows.Forms
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
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP
#End Region

Public Class FrmContractExternalClients
    Implements IContractExternalClients

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListContractExternalClientsDetail As List(Of ContractExternalClientsDetail)

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListDeleteContractExternalClientsDetail As List(Of ContractExternalClientsDetail)

    ''' <summary>
    ''' Cabacera
    ''' </summary>
    Dim ContractExternalClients As ContractExternalClients

    ''' <summary>
    ''' Tabla de detalle
    ''' </summary>
    Dim ContractExternalClientsDetail As ContractExternalClientsDetail

    ''' <summary>
    ''' indice del registro que se esta editando para luego insertarlo en la misma posicion que estaba
    ''' </summary>
    ''' <remarks></remarks>
    Dim IndexEditRecord As Integer

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PContractExternalClients

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
    ''' Entidad del detalle de grupo de atencion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ContractExternalClientsDefinitionRate As ContractExternalClientsDefinitionRate

    ''' <summary>
    ''' Listado de eliminados de definiciones de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListContractExternalClientsDefinitionRate As List(Of ContractExternalClientsDefinitionRate)

    ''' <summary>
    ''' True = modificar y False = guardar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim modeModify As Boolean = False

    ''' <summary>
    ''' Id del Cliente
    ''' </summary>
    Dim CustomerId As Integer

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
    ''' Estado
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements IContractExternalClients.Status
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
    ''' Layout
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IContractExternalClients.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IContractExternalClients.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleContractType.Enabled = value
            INDdteDocumentDate.Enabled = value
            INDdteInitialDate.Enabled = value
            INDdteEndDate.Enabled = value
            INDmemoContractObject.Enabled = value
            INDtxtContractNumber.Enabled = value
            INDsleCustomerId.Enabled = value
            INDsleSaleMode.Enabled = value
            INDsleTechnicalSupervision.Enabled = value
            INDsleProductRateId.Enabled = value
            INDmemoClauses.Enabled = value
            INDmemoAnnexes.Enabled = value
            INDtxtQuoteNumber.Enabled = value
            INDdteQuoteDate.Enabled = value
            INDtxtActNumber.Enabled = value
            INDdteActDate.Enabled = value
            INDtxtNegotiationType.Enabled = value
            INDtxtApprovedBy.Enabled = value
            INDbtnItem.Enabled = value
            INDgcItem.Enabled = value
            INDsleManagesMaquila.Enabled = value
            INDpceDefinitionRate.Enabled = value
            INDgcDefinitionRate.Enabled = value
            INDlyRoot.EndUpdate()

            If value Then
                INDsleContractType.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IContractExternalClients.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IContractExternalClients.Code
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
    Public Property Sequense As MixingStationSequence Implements IContractExternalClients.Sequense
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

    ''' <summary>
    ''' Establece el datasource de la definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefinitionRateXpo As XPInstantFeedbackSource Implements IContractExternalClients.DefinitionRateXpo
        Get
            Return INDSleRateDefinition.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleRateDefinition.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el Id de definicion de tarifa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DefinitionRateId As Integer Implements IContractExternalClients.DefinitionRateId
        Get
            Return INDSleRateDefinition.EditValue
        End Get
        Set(value As Integer)
            INDSleRateDefinition.EditValue = value
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
        If INDlyItemWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleWarehouse.EditValue Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("InvalidFields"), INDlyItemWarehouse.Text.ToString())
            Exit Sub
        End If

        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MContractExternalClients(Me.Tag.ToString())
                Dim result = Await model.SaveContractExternalClients(ContractExternalClients, _idCurrentSequense, _idOperativeUnit)
                If result.StateResult Then
                    If ContractExternalClients.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.MixingStationSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequense.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf ContractExternalClients.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.ContractExternalClients = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.AsyncLoader(False)
                    Me.Deshacer()
                Else
                    Me.AsyncLoader(False)
                    If result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    ElseIf Not String.IsNullOrEmpty(result.Message) Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
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
            Await NewContractExternalClients()
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

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If ContractExternalClients IsNot Nothing AndAlso ContractExternalClients.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MContractExternalClients(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteContractExternalClients(ContractExternalClients, indigo.TransactionalContainer)
                    If result.StateResult Then
                        Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar contrato porque se encuentra asociado a un centro de atención externo"
                    End If
                End Using
            End If
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "ContractTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                              New ColumnInfo() With {.Caption = "No. Contrato", .FieldName = "ContractNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Cliente", .FieldName = "Customer.NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.35)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListContractExternalClients
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
    ''' Inicializa los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listContractType = New List(Of Tuple(Of Byte, String))()
        listContractType.Add(New Tuple(Of Byte, String)(1, "Fijo"))
        listContractType.Add(New Tuple(Of Byte, String)(2, "Variable"))
        INDsleContractType.Properties.DataSource = listContractType

        Dim listSaleMode = New List(Of Tuple(Of Byte, String))()
        listSaleMode.Add(New Tuple(Of Byte, String)(1, "Crédito"))
        listSaleMode.Add(New Tuple(Of Byte, String)(2, "Contado"))
        INDsleSaleMode.Properties.DataSource = listSaleMode

        Dim listYesNot = New List(Of Tuple(Of Boolean, String))()
        listYesNot.Add(New Tuple(Of Boolean, String)(True, "Si"))
        listYesNot.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleTechnicalSupervision.Properties.DataSource = listYesNot
        INDsleManagesMaquila.Properties.DataSource = listYesNot
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
        With ContractExternalClients
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code

            .ContractType = INDsleContractType.EditValue
            .DocumentDate = INDdteDocumentDate.EditValue
            .InitialDate = INDdteInitialDate.EditValue
            .EndDate = INDdteEndDate.EditValue
            .ContractObject = INDmemoContractObject.EditValue
            .ContractNumber = INDtxtContractNumber.EditValue
            .CustomerId = INDsleCustomerId.EditValue
            .SaleMode = INDsleSaleMode.EditValue
            .TechnicalSupervision = INDsleTechnicalSupervision.EditValue
            .ProductRateId = INDsleProductRateId.EditValue
            .Clauses = INDmemoClauses.EditValue
            .Annexes = INDmemoAnnexes.EditValue
            .QuoteNumber = INDtxtQuoteNumber.EditValue
            .QuoteDate = INDdteQuoteDate.EditValue
            .ActNumber = INDtxtActNumber.EditValue
            .ActDate = INDdteActDate.EditValue
            .NegotiationType = INDtxtNegotiationType.EditValue
            .ApprovedBy = INDtxtApprovedBy.EditValue
            .ManagesMaquila = INDsleManagesMaquila.EditValue
            .WarehouseId = INDsleWarehouse.EditValue

            If ListContractExternalClientsDetail IsNot Nothing AndAlso ListContractExternalClientsDetail.Count > 0 Then
                ListContractExternalClientsDetail.ForEach(Sub(item) .ContractExternalClientsDetail.Add(item))
            End If

            If ListDeleteContractExternalClientsDetail IsNot Nothing AndAlso ListDeleteContractExternalClientsDetail.Count > 0 Then
                ListDeleteContractExternalClientsDetail.ForEach(Sub(item) .ContractExternalClientsDetail.Add(item.MarkAsDeleted()))
            End If

            If ListContractExternalClientsDefinitionRate IsNot Nothing AndAlso ListContractExternalClientsDefinitionRate.Count > 0 Then
                ListContractExternalClientsDefinitionRate.ForEach(Sub(item) .ContractExternalClientsDefinitionRate.Add(item))
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
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Status = True
        Code = String.Empty

        INDsleContractType.EditValue = Nothing
        INDdteDocumentDate.EditValue = Nothing
        INDdteInitialDate.EditValue = Nothing
        INDdteEndDate.EditValue = Nothing
        INDmemoContractObject.EditValue = Nothing
        INDtxtContractNumber.EditValue = Nothing
        INDsleCustomerId.EditValue = Nothing
        INDsleCustomerId.Properties.NullText = String.Empty
        INDsleSaleMode.EditValue = Nothing
        INDsleTechnicalSupervision.EditValue = Nothing
        INDsleProductRateId.EditValue = Nothing
        INDsleProductRateId.Properties.NullText = String.Empty
        INDmemoClauses.EditValue = Nothing
        INDmemoAnnexes.EditValue = Nothing
        INDtxtQuoteNumber.EditValue = Nothing
        INDdteQuoteDate.EditValue = Nothing
        INDtxtActNumber.EditValue = Nothing
        INDdteActDate.EditValue = Nothing
        INDtxtNegotiationType.EditValue = Nothing
        INDtxtApprovedBy.EditValue = Nothing

        INDgcItem.DataSource = Nothing
        ContractExternalClients = Nothing
        ContractExternalClientsDetail = Nothing
        ListContractExternalClientsDetail = Nothing
        ListDeleteContractExternalClientsDetail = Nothing

        ContractExternalClientsDefinitionRate = Nothing
        ListContractExternalClientsDefinitionRate = Nothing
        INDsleManagesMaquila.EditValue = Nothing
        INDsleWarehouse.EditValue = Nothing
        INDsleWarehouse.Properties.NullText = String.Empty
        ListContractExternalClientsDefinitionRate = Nothing
        ContractExternalClientsDefinitionRate = Nothing
        INDgcDefinitionRate.DataSource = Nothing

        CleanControlsPopup()

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
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MContractExternalClients(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not Me.ContractExternalClients.Status
                Dim Result = Await model.ChangeStateContractExternalClients(Code, state, _idOperativeUnit)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.ContractExternalClients = Result.ObjectEmbbeded
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.ContractExternalClients IsNot Nothing AndAlso Me.ContractExternalClients.Id > 0 Then
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ContractExternalClients.Code, Me.ContractExternalClients.ContractNumber),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.ContractExternalClients.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ContractExternalClients.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.ContractExternalClients.Code, Me.ContractExternalClients.ContractNumber)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.ContractExternalClients.Code)
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

            Me.BarraBotones.StatusRecordVisible = True
            Using Model As New MContractExternalClients(CStr(Me.Tag))
                AsyncLoader(True)
                ContractExternalClients = (Await Model.GetContractExternalClients(INDbtnCode.Text.Trim)).ObjectEmbbeded
                If ContractExternalClients IsNot Nothing AndAlso ContractExternalClients.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(ContractExternalClients.Id))
                        With ContractExternalClients
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code

                            INDsleContractType.EditValue = .ContractType
                            INDdteDocumentDate.EditValue = .DocumentDate
                            INDdteInitialDate.EditValue = .InitialDate
                            INDdteEndDate.EditValue = .EndDate
                            INDmemoContractObject.EditValue = .ContractObject
                            INDtxtContractNumber.EditValue = .ContractNumber
                            INDsleCustomerId.EditValue = .CustomerId
                            INDsleCustomerId.Properties.NullText = .CustomerDescription
                            INDsleSaleMode.EditValue = .SaleMode
                            INDsleTechnicalSupervision.EditValue = .TechnicalSupervision
                            INDsleProductRateId.EditValue = .ProductRateId
                            INDsleProductRateId.Properties.NullText = .ProductRateDescription
                            INDmemoClauses.EditValue = .Clauses
                            INDmemoAnnexes.EditValue = .Annexes
                            INDtxtQuoteNumber.EditValue = .QuoteNumber
                            INDdteQuoteDate.EditValue = .QuoteDate
                            INDtxtActNumber.EditValue = .ActNumber
                            INDdteActDate.EditValue = .ActDate
                            INDtxtNegotiationType.EditValue = .NegotiationType
                            INDtxtApprovedBy.EditValue = .ApprovedBy
                            INDsleManagesMaquila.EditValue = .ManagesMaquila
                            INDsleWarehouse.EditValue = .WarehouseId
                            INDsleWarehouse.Properties.NullText = .WareHouseCodeName

                            Status = .Status

                            ListContractExternalClientsDetail = .ContractExternalClientsDetail.ToList()
                            INDgcItem.DataSource = Nothing
                            INDgcItem.DataSource = ListContractExternalClientsDetail
                            ListContractExternalClientsDefinitionRate = New List(Of ContractExternalClientsDefinitionRate)
                            ListContractExternalClientsDefinitionRate = .ContractExternalClientsDefinitionRate.ToList()
                            INDgcDefinitionRate.DataSource = ListContractExternalClientsDefinitionRate
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.ContractExternalClients.Code)
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = ContractExternalClients.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(ContractExternalClients.Id)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Await Me.NewContractExternalClients()
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
    Private Async Function NewContractExternalClients() As Task

        INDsleManagesMaquila.EditValue = False
        ContractExternalClients = New ContractExternalClients() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.MixingStationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequense))
                        End Using
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
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

    ''' <summary>
    ''' Metodo que abre el from para agregar los RIAS
    ''' </summary>
    Private Sub OpenFormContractExternalClientsDetail(EditMode As Boolean)
        Using formulario As New FrmContractExternalClientsDetail()
            Me.Cursor = ChangeCursorIndigo()
            AddHandler formulario.AddContractExternalClientsDetailArgs, AddressOf ReturnAddEventArgs
            formulario.EditModeDetail = EditMode
            If EditMode Then
                formulario.ListContractExternalClientsDetailCompare = (From x In ListContractExternalClientsDetail Where Not x.Equals(ContractExternalClientsDetail)).ToList()
            Else
                formulario.ListContractExternalClientsDetailCompare = ListContractExternalClientsDetail
            End If
            formulario.ContractExternalClientsDetail = ContractExternalClientsDetail
            formulario.ToolBar.Visible = False
            formulario.Size = New System.Drawing.Size(700, 600)
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
    Private Sub ReturnAddEventArgs(sender As Object, e As AddContractExternalClientsDetail)
        If e IsNot Nothing Then

            If e.EditMode = False Then 'Si se esta insertando
                If ListContractExternalClientsDetail Is Nothing Then
                    ListContractExternalClientsDetail = New List(Of ContractExternalClientsDetail)
                End If
                ListContractExternalClientsDetail.Add(e.ContractExternalClientsDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
            Else 'Si se esta actualizando
                ListContractExternalClientsDetail.Remove(ContractExternalClientsDetail)
                ListContractExternalClientsDetail.Insert(IndexEditRecord, e.ContractExternalClientsDetail)
                Mensaje(EeventViewerImages.Informacion) = "Detalle modificado correctamente"
            End If

            INDgcItem.DataSource = Nothing
            INDgcItem.DataSource = ListContractExternalClientsDetail
        End If
    End Sub

    ''' <summary>
    ''' Edita un detalle
    ''' </summary>
    Private Sub EditDetail()
        ContractExternalClientsDetail = DirectCast(INDviewItem.GetFocusedRow(), ContractExternalClientsDetail)
        IndexEditRecord = ListContractExternalClientsDetail.IndexOf(ContractExternalClientsDetail)
        OpenFormContractExternalClientsDetail(True)
    End Sub

    ''' <summary>
    ''' Elimina un RIAS
    ''' </summary>
    Private Sub DeleteDetail()
        Dim entityDelete = DirectCast(INDviewItem.GetFocusedRow(), ContractExternalClientsDetail)
        If entityDelete.Id > 0 Then
            If ListDeleteContractExternalClientsDetail Is Nothing Then
                ListDeleteContractExternalClientsDetail = New List(Of ContractExternalClientsDetail)
            End If
            ListDeleteContractExternalClientsDetail.Add(entityDelete.MarkAsDeleted())
        End If
        ListContractExternalClientsDetail.Remove(entityDelete)
        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListContractExternalClientsDetail
        Mensaje(EeventViewerImages.Informacion) = "Detalle eliminado de la rejilla correctamente"
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDsleDefinitionRate.Properties.NullText = String.Empty
        modeModify = False
        INDSleRateDefinition.EditValue = Nothing
        INDSleRateDefinition.Properties.NullText = String.Empty
        INDDeInitialDate.EditValue = Nothing
        INDDeFinalDate.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Añade o modifica el detalle en la rejilla
    ''' </summary>
    Private Sub AddDefinitionRate()
        Dim errors As String = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If ListContractExternalClientsDefinitionRate Is Nothing Then
            ListContractExternalClientsDefinitionRate = New List(Of ContractExternalClientsDefinitionRate)
        Else
            Dim cont As Integer = 0
            If modeModify = False Then
                cont = ValidateList(ListContractExternalClientsDefinitionRate)
            Else
                Dim ListCompare = New List(Of ContractExternalClientsDefinitionRate)(ListContractExternalClientsDefinitionRate)
                ListCompare.Remove(ContractExternalClientsDefinitionRate)
                cont = ValidateList(ListCompare)
            End If
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = $"La definicion de tarifa {INDSleRateDefinition.Text} ya existe en la lista con las fechas {INDDeInitialDate.EditValue} - {INDDeFinalDate.EditValue}"
                Exit Sub
            End If
        End If
        If modeModify = False Then
            ContractExternalClientsDefinitionRate = New ContractExternalClientsDefinitionRate
        End If
        With ContractExternalClientsDefinitionRate
            .DefinitionRateId = DefinitionRateId
            .DefinitionRateDescription = INDSleRateDefinition.Text
            .InitialDate = INDDeInitialDate.EditValue
            .EndDate = INDDeFinalDate.EditValue
        End With
        If modeModify = False Then
            ListContractExternalClientsDefinitionRate.Add(ContractExternalClientsDefinitionRate)
            Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente."
        Else
            Mensaje(EeventViewerImages.Informacion) = "Detalle editado correctamente."
        End If
        INDgcDefinitionRate.DataSource = Nothing
        INDgcDefinitionRate.DataSource = ListContractExternalClientsDefinitionRate.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
        CleanControlsPopup()
        INDSleRateDefinition.Focus()
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If DefinitionRateId = 0 Then
            listErrors.AppendLine("Debe ingresar una definición de tarifa.")
        End If
        If Object.Equals(INDDeInitialDate.EditValue, Nothing) = True Then
            listErrors.AppendLine("Debe ingresar una fecha inicial.")
        End If
        If Object.Equals(INDDeFinalDate.EditValue, Nothing) = True Then
            listErrors.AppendLine("Debe ingresar una fecha final.")
        End If
        Return listErrors.ToString
    End Function

    Private Function ValidateList(ListValidate As List(Of ContractExternalClientsDefinitionRate)) As Integer
        Dim list As List(Of ContractExternalClientsDefinitionRate) = ListValidate.FindAll(Function(item) ((INDDeInitialDate.EditValue >= item.InitialDate AndAlso INDDeInitialDate.EditValue <= item.EndDate) OrElse (INDDeFinalDate.EditValue >= item.InitialDate AndAlso INDDeFinalDate.EditValue <= item.EndDate) OrElse (INDDeInitialDate.EditValue < item.InitialDate) AndAlso (INDDeFinalDate.EditValue > item.EndDate)) AndAlso (item.ChangeTracker.State <> ObjectState.Deleted))
        Return list.Count
    End Function

    ''' <summary>
    ''' Metodo que edita la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDefinitionRate()
        modeModify = True
        ContractExternalClientsDefinitionRate = viewDefinitionRate.GetFocusedRow
        With ContractExternalClientsDefinitionRate
            DefinitionRateId = .DefinitionRateId
            INDsleDefinitionRate.Properties.NullText = .DefinitionRateDescription
            INDDeInitialDate.EditValue = .InitialDate
            INDDeFinalDate.EditValue = .EndDate
        End With
        INDpceDefinitionRate.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo que elimina la definicion de tarifa
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDefinitionRate()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        ContractExternalClientsDefinitionRate = viewDefinitionRate.GetFocusedRow
        If ContractExternalClientsDefinitionRate.Id > 0 Then
            ContractExternalClientsDefinitionRate.MarkAsDeleted()
        Else
            ListContractExternalClientsDefinitionRate.Remove(ContractExternalClientsDefinitionRate)
        End If
        INDgcDefinitionRate.DataSource = Nothing
        INDgcDefinitionRate.DataSource = ListContractExternalClientsDefinitionRate.FindAll(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)

    End Sub
#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractExternalClients_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        InitializeTuples()
        Presenter = New PContractExternalClients(Me)
        Presenter.GetSequence()
        LoadStatus()
        Deshacer()

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewItem, ListActions)
        IndigoGridView2.SetListAcction(viewDefinitionRate, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewItem.Columns
            If col.Name = "colActions" Then
                col.Width = 50
            End If
        Next

        INDEsbBill1.AddExcelSheets(New ExcelSheet With {
                    .Columns = New List(Of ExcelColumn) From {
                        New ExcelColumn With {.Name = "Tipo", .Comment = "1. Medicamento" & vbCrLf & "2. Insumo" & vbCrLf & "3. Producto" & vbCrLf & "4. Servicio"},
                        New ExcelColumn With {.Name = "Código"},
                        New ExcelColumn With {.Name = "Suministrado Por", .Comment = "0. No Aplica" & vbCrLf & "1. Central de Mezclas" & vbCrLf & "2. Cliente"}
                }
                    })
    End Sub

#End Region

#Region "PasteToGrid"

    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid

        Try

            AsyncLoader(True)

            Dim listErrors As New List(Of String)

            Using model As New MContractExternalClients(MyTag)
                Dim result = Await model.ImportExceptionsRawMaterial(e.Rows)

                AsyncLoader(False)
                listErrors = result.MessageResult
                If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then
                    result.MessageResult.AddRange(SetExceptions(result.ObjectEmbbeded))
                End If
            End Using

            If listErrors.Count > 0 Then
                Using formulario As New FrmListErrors(listErrors)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
            Me.Cursor = System.Windows.Forms.Cursors.Default

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = ex.Message
        End Try
    End Sub

    Private Function SetExceptions(result As List(Of Domain.Entities.ContractExternalClientsDetail)) As List(Of String)
        Dim listErrors As New List(Of String)

        If ListContractExternalClientsDetail IsNot Nothing AndAlso ListContractExternalClientsDetail.Count > 0 Then
            For Each item In result
                If ListContractExternalClientsDetail.Any(Function(ap) ap.SourceCodeName = item.SourceCodeName) Then
                    listErrors.Add("La factura " & item.SourceCodeName & " ya esta agregada.")
                    Continue For
                End If

                ListContractExternalClientsDetail.Add(item)
            Next
        Else
            ListContractExternalClientsDetail = result
        End If

        INDgcItem.DataSource = Nothing
        INDgcItem.DataSource = ListContractExternalClientsDetail

        ActionsOnControls = True
        Return listErrors
    End Function
#End Region

#Region "Click"

    ''' <summary>
    ''' Evento click del boton agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnItem_Click(sender As Object, e As EventArgs) Handles INDbtnItem.Click
        ContractExternalClientsDetail = Nothing
        OpenFormContractExternalClientsDetail(False)
    End Sub

    ''' <summary>
    ''' añade la definicion de tarifa a la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtAdd_Click(sender As Object, e As EventArgs) Handles INDbtAdd.Click
        AddDefinitionRate()
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
                    Await Me.NewContractExternalClients()
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
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditDefinitionRate()
            Case "Remove"
                DeleteDefinitionRate()
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
    Private Sub FrmContractExternalClients_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Se ejecuta al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmContractExternalClients_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de clientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCustomerId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCustomerId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(503, Nothing, True)
            INDsleCustomerId.Properties.DataSource = Presenter.InitializeCustomer()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de tarifa productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProductRateId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductRateId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(306, Nothing, True)
            INDsleProductRateId.Properties.DataSource = Presenter.InitializeProductRate()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCustomerId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCustomerId.QueryPopUp
        If INDsleCustomerId.Properties.DataSource Is Nothing Then
            INDsleCustomerId.Properties.DataSource = Presenter.InitializeCustomer()
        End If
    End Sub

    ''' <summary>
    ''' Al desplegar el control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProductRateId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductRateId.QueryPopUp
        If INDsleProductRateId.Properties.DataSource Is Nothing Then
            INDsleProductRateId.Properties.DataSource = Presenter.InitializeProductRate()
        End If
    End Sub

    ''' <summary>
    ''' Almacenes por cliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleWarehouse_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleWarehouse.QueryPopUp
        If INDsleWarehouse.Properties.DataSource Is Nothing Then
            CustomerId = INDsleCustomerId.EditValue
            Dim ThirdPartyId = Presenter.GetThirdPartyId(CustomerId).ThirdPartyId
            Dim Supplier = Presenter.GetSupplier(ThirdPartyId)
            INDsleWarehouse.Properties.DataSource = Nothing
            If Supplier IsNot Nothing Then
                INDsleWarehouse.Properties.DataSource = Presenter.GetWareHouseBySupplier(Supplier.Id)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Definicion de tarifa querypopup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleRateDefinition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRateDefinition.QueryPopUp
        If INDSleRateDefinition.Properties.DataSource Is Nothing Then
            DefinitionRateXpo = Presenter.InitializeDefinitionRate()
        End If
    End Sub

#End Region

#Region "Popup"

#End Region

#Region "CloseUp"


#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar si maneja almacen Maquila
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleManagesMaquila_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleManagesMaquila.EditValueChanged
        If INDsleManagesMaquila.EditValue IsNot Nothing AndAlso INDsleManagesMaquila.EditValue = True Then
            INDlyItemWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemWarehouse.Enabled = True
        Else
            INDlyItemWarehouse.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDsleWarehouse.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' validacion fecha 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdteInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialDate.EditValueChanged, INDdteEndDate.EditValueChanged
        If INDdteEndDate.EditValue Is Nothing OrElse INDdteInitialDate.EditValue Is Nothing Then
            Exit Sub
        End If

        If INDdteEndDate.EditValue < INDdteInitialDate.EditValue Then
            Dim InitialDate As DateTime = INDdteEndDate.EditValue
            INDdteInitialDate.EditValue = InitialDate.AddDays(-1)
            Me.Mensaje(EeventViewerImages.Advertencia) = "La fecha inicial No puede ser mayor a la final"
        End If
    End Sub

    ''' <summary>
    ''' pone que la fecha final no puede ser menor a la fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDDeInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDDeInitialDate.EditValueChanged, INDDeFinalDate.EditValueChanged
        If INDDeInitialDate.EditValue Is Nothing AndAlso INDDeFinalDate.EditValue Is Nothing Then
            Exit Sub
        End If

        If INDDeInitialDate.EditValue IsNot Nothing And INDDeFinalDate.EditValue Is Nothing Then
            Dim DateIn As DateTime = INDDeInitialDate.EditValue
            INDDeFinalDate.Properties.MinValue = INDDeInitialDate.EditValue
            INDDeFinalDate.EditValue = DateAdd(DateInterval.Minute, -1, DateAdd(DateInterval.Day, 1, DateIn))
        End If
        If INDDeInitialDate.EditValue IsNot Nothing AndAlso INDDeFinalDate.EditValue IsNot Nothing Then
            Dim DateFin As DateTime = INDDeFinalDate.EditValue
            If INDDeFinalDate.EditValue < INDDeInitialDate.EditValue Then
                INDDeInitialDate.EditValue = DateAdd(DateInterval.Day, -1, DateFin.AddMinutes(1))
                Me.Mensaje(EeventViewerImages.Advertencia) = "La fecha inicial No puede ser mayor a la final"
            End If
            If DateFin.Hour <> 23 AndAlso DateFin.Minute <> 59 Then
                INDDeFinalDate.EditValue = DateFin.AddDays(1).AddMinutes(-1)
            End If
        End If
    End Sub

#End Region

#End Region

#Region "BarButton Events"

    ''' <summary>
    ''' Evento barra de botones Activo - Inactivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
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
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.MixingStationSequenceDetail IsNot Nothing Then
                If Not Me._sequense.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCustomerId_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCustomerId.EditValueChanged
        INDsleWarehouse.Properties.DataSource = Nothing
    End Sub

#End Region

End Class