'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2020
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
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP
#End Region

Public Class FrmTransportationAssistant
    Implements ITransportationAssistant

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Cabacera
    ''' </summary>
    Dim TransportationAssistant As TransportationAssistant

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PTransportationAssistant

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
    Public Property Status As Boolean Implements ITransportationAssistant.Status
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ITransportationAssistant.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Activa los controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ITransportationAssistant.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDsleAssistantType.Enabled = value
            INDslePosition.Enabled = value
            INDsleEmployee.Enabled = value
            INDsleIdentificationType.Enabled = value
            INDtxtIdentificationNumber.Enabled = value
            INDtxtName.Enabled = value
            INDtxtLastName.Enabled = value
            INDsleSex.Enabled = value
            INDdteBirthDate.Enabled = value
            INDlyRoot.EndUpdate()

            If value Then
                INDsleAssistantType.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements ITransportationAssistant.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements ITransportationAssistant.Code
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
    Public Property Sequense As MixingStationSequence Implements ITransportationAssistant.Sequense
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
        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MTransportationAssistant(Me.Tag.ToString())
                Dim result = Await model.SaveTransportationAssistant(TransportationAssistant, _idCurrentSequense, _idOperativeUnit)
                If result.StateResult Then
                    If TransportationAssistant.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.MixingStationSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequense.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf TransportationAssistant.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.TransportationAssistant = result.ObjectEmbbeded
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
            Await NewTransportationAssistant()
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
        If TransportationAssistant IsNot Nothing AndAlso TransportationAssistant.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MTransportationAssistant(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteTransportationAssistant(TransportationAssistant, indigo.TransactionalContainer)
                    If result.StateResult Then
                        Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "AssistantTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Identificación", .FieldName = "IdentificationNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "FullName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.15)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListTransportationAssistant
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
        Dim listAssistantType = New List(Of Tuple(Of Byte, String))()
        listAssistantType.Add(New Tuple(Of Byte, String)(1, "Interno"))
        listAssistantType.Add(New Tuple(Of Byte, String)(2, "Contratado"))
        INDsleAssistantType.Properties.DataSource = listAssistantType

        Dim listIdentificationType = New List(Of Tuple(Of Integer, String))
        listIdentificationType.Add(New Tuple(Of Integer, String)(0, "Cédula de Ciudadanía (CC)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(1, "Cédula de Extranjería (CE)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(2, "Tarjeta de Identidad (TI)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(3, "Registro Civil (RC)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(4, "Pasaporte (PA)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(5, "Adulto Sin Identificación (AS)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(6, "Menor Sin Identificación (MS)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(7, "Nit (NI)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(8, "Número único de identificación personal (NU)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(9, "Certificado Nacido Vivo (CN)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(10, "Carnet Diplomático (CD)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(11, "Salvoconducto (SC)"))
        listIdentificationType.Add(New Tuple(Of Integer, String)(12, "Permiso especial de Permanencia (PE)"))
        INDsleIdentificationType.Properties.DataSource = listIdentificationType

        Dim listSex = New List(Of Tuple(Of Integer, String))
        listSex.Add(New Tuple(Of Integer, String)(1, "Masculino"))
        listSex.Add(New Tuple(Of Integer, String)(2, "Femenino"))
        INDsleSex.Properties.DataSource = listSex
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
        With TransportationAssistant
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .AssistantType = INDsleAssistantType.EditValue

            .PositionId = Nothing
            If INDlyItemPosition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PositionId = INDslePosition.EditValue
            End If

            .EmployeeId = Nothing
            If INDlyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .EmployeeId = INDsleEmployee.EditValue
            End If

            .IdentificationType = INDsleIdentificationType.EditValue
            .IdentificationNumber = INDtxtIdentificationNumber.EditValue
            .Name = INDtxtName.EditValue
            .LastName = INDtxtLastName.EditValue
            .Sex = INDsleSex.EditValue
            .BirthDate = INDdteBirthDate.EditValue

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
        INDsleAssistantType.EditValue = Nothing

        INDslePosition.EditValue = Nothing
        INDslePosition.Properties.NullText = String.Empty
        INDlyItemPosition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPosition.AllowHide = True

        INDsleEmployee.EditValue = Nothing
        INDsleEmployee.Properties.NullText = String.Empty
        INDlyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemEmployee.AllowHide = True

        INDsleIdentificationType.EditValue = Nothing
        INDtxtIdentificationNumber.EditValue = Nothing
        INDtxtName.EditValue = Nothing
        INDtxtLastName.EditValue = Nothing
        INDsleSex.EditValue = Nothing
        INDdteBirthDate.EditValue = Nothing

        TransportationAssistant = Nothing
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
            Using model As New MTransportationAssistant(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not Me.TransportationAssistant.Status
                Dim Result = Await model.ChangeStateTransportationAssistant(Code, state, _idOperativeUnit)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.TransportationAssistant = Result.ObjectEmbbeded
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
        If Me.TransportationAssistant IsNot Nothing AndAlso Me.TransportationAssistant.Id > 0 Then
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.TransportationAssistant.Code, Me.TransportationAssistant.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.TransportationAssistant.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.TransportationAssistant.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.TransportationAssistant.Code, Me.TransportationAssistant.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.TransportationAssistant.Code)
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
            Using Model As New MTransportationAssistant(CStr(Me.Tag))
                AsyncLoader(True)
                TransportationAssistant = (Await Model.GetTransportationAssistant(INDbtnCode.Text.Trim)).ObjectEmbbeded
                If TransportationAssistant IsNot Nothing AndAlso TransportationAssistant.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                        record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(TransportationAssistant.Id))
                        With TransportationAssistant
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            INDsleAssistantType.EditValue = .AssistantType

                            INDslePosition.EditValue = .PositionId
                            INDslePosition.Properties.NullText = .PositionDescription

                            INDsleEmployee.EditValue = .EmployeeId
                            INDsleEmployee.Properties.NullText = .EmployeeDescription

                            INDsleIdentificationType.EditValue = .IdentificationType
                            INDtxtIdentificationNumber.EditValue = .IdentificationNumber
                            INDtxtName.EditValue = .Name
                            INDtxtLastName.EditValue = .LastName
                            INDsleSex.EditValue = .Sex
                            INDdteBirthDate.EditValue = .BirthDate

                            Status = .Status
                        End With

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.TransportationAssistant.Code)
                        If record.Id = 0 Then
                            record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = TransportationAssistant.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(TransportationAssistant.Id)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        If TransportationAssistant.AssistantType = 1 Then
                            INDsleIdentificationType.Enabled = False
                            INDtxtIdentificationNumber.Enabled = False
                            INDtxtName.Enabled = False
                            INDtxtLastName.Enabled = False
                            INDsleSex.Enabled = False
                            INDdteBirthDate.Enabled = False
                        End If
                    End Using
                Else
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Await Me.NewTransportationAssistant()
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
    Private Async Function NewTransportationAssistant() As Task
        TransportationAssistant = New TransportationAssistant() With {.Status = True}
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
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmTransportationAssistant_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        InitializeTuples()
        Presenter = New PTransportationAssistant(Me)
        Presenter.GetSequence()
        LoadStatus()
        Deshacer()
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
                    Await Me.NewTransportationAssistant()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Se ejecuta al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmTransportationAssistant_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Se ejecuta al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmTransportationAssistant_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el formulario de cargos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePosition_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePosition.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(542, Nothing, True)
            INDslePosition.Properties.DataSource = Presenter.InitializePosition()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEmployee_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEmployee.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(529, Nothing, True)
            If INDslePosition.EditValue IsNot Nothing Then
                INDsleEmployee.Properties.DataSource = Presenter.InitializeEmployees(INDslePosition.EditValue)
            End If
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Se dispara cuando se despliega el control de cargos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePosition_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePosition.QueryPopUp
        If INDslePosition.Properties.DataSource Is Nothing Then
            INDslePosition.Properties.DataSource = Presenter.InitializePosition()
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al desplegar el control de empleados
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEmployee_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleEmployee.QueryPopUp
        If INDsleEmployee.Properties.DataSource Is Nothing AndAlso INDslePosition.EditValue IsNot Nothing Then
            INDsleEmployee.Properties.DataSource = Presenter.InitializeEmployees(INDslePosition.EditValue)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de tipo auxiliar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAssistantType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAssistantType.EditValueChanged
        If INDsleAssistantType.EditValue IsNot Nothing Then
            If INDsleAssistantType.EditValue = 1 Then 'Interno
                INDlyItemPosition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPosition.AllowHide = False
                INDlyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemEmployee.AllowHide = False

                INDsleIdentificationType.Enabled = False
                INDtxtIdentificationNumber.Enabled = False
                INDtxtName.Enabled = False
                INDtxtLastName.Enabled = False
                INDsleSex.Enabled = False
                INDdteBirthDate.Enabled = False

                INDsleIdentificationType.EditValue = Nothing
                INDtxtIdentificationNumber.EditValue = Nothing
                INDtxtName.EditValue = Nothing
                INDtxtLastName.EditValue = Nothing
                INDsleSex.EditValue = Nothing
                INDdteBirthDate.EditValue = Nothing
            Else 'Contratado
                INDlyItemPosition.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPosition.AllowHide = True
                INDlyItemEmployee.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemEmployee.AllowHide = True

                INDsleIdentificationType.Enabled = True
                INDtxtIdentificationNumber.Enabled = True
                INDtxtName.Enabled = True
                INDtxtLastName.Enabled = True
                INDsleSex.Enabled = True
                INDdteBirthDate.Enabled = True

                INDsleEmployee.EditValue = Nothing
                INDsleEmployee.Properties.NullText = String.Empty
                INDsleEmployee.Properties.DataSource = Nothing

                INDsleIdentificationType.EditValue = Nothing
                INDtxtIdentificationNumber.EditValue = Nothing
                INDtxtName.EditValue = Nothing
                INDtxtLastName.EditValue = Nothing
                INDsleSex.EditValue = Nothing
                INDdteBirthDate.EditValue = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de cargos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePosition_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePosition.EditValueChanged
        If INDslePosition.EditValue IsNot Nothing Then
            INDsleEmployee.EditValue = Nothing
            INDsleEmployee.Properties.NullText = String.Empty
            INDsleEmployee.Properties.DataSource = Nothing

            INDsleIdentificationType.EditValue = Nothing
            INDtxtIdentificationNumber.EditValue = Nothing
            INDtxtName.EditValue = Nothing
            INDtxtLastName.EditValue = Nothing
            INDsleSex.EditValue = Nothing
            INDdteBirthDate.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de empleado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEmployee.EditValueChanged
        If INDsleEmployee.EditValue IsNot Nothing Then
            Dim employeeXpo = Presenter.GetEmployeeById(INDsleEmployee.EditValue)
            If employeeXpo IsNot Nothing Then
                Dim personXpo = Presenter.GetPersonById(employeeXpo.ThirdPartyId.PersonId)
                If personXpo IsNot Nothing Then
                    INDsleIdentificationType.EditValue = personXpo.IdentificationTypeId.ID
                    INDtxtIdentificationNumber.EditValue = employeeXpo.ThirdPartyId.Nit
                    INDtxtName.EditValue = personXpo.FirstName + " " + personXpo.SecondName
                    INDtxtLastName.EditValue = personXpo.FirstLastName + " - " + personXpo.SecondLastName
                    INDsleSex.EditValue = personXpo.Gender
                    INDdteBirthDate.EditValue = personXpo.BirthDate
                End If
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

#End Region

End Class