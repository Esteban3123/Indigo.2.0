'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 24/09/2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports System.Text
Imports Presentation.Contract.MVP
Imports Infrastructure.Data.Xpo.CrystalRepository
Imports DevExpress.Xpo
#End Region

Public Class FrmCupsSubGroup
    Implements ICupsSubGroup

#Region "Properties"

    ''' <summary>
    ''' Establece si se muestra en dashboard de imagenologia
    ''' </summary>
    ''' <returns></returns>
    Public Property ShowOnImagingDashboard As Boolean? Implements ICupsSubGroup.ShowOnImagingDashboard
        Get
            Return INDsleShowOnImagingDashboard.EditValue
        End Get
        Set(value As Boolean?)
            INDsleShowOnImagingDashboard.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements ICupsSubGroup.Status
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
    ''' Obtiene la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As ContractSequence Implements ICupsSubGroup.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As ContractSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.ContractSequenceDetail In Me._sequense.ContractSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICupsSubGroup.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ICupsSubGroup.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements ICupsSubGroup.Code
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
    ''' Obtiene o establece el nombre del subgrupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameCSG As String Implements ICupsSubGroup.NameCSG
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CupsGroupXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICupsSubGroup.CupsGroupXpo
        Get
            Return CType(INDsleCupsGroup.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCupsGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de grupos de imagenologia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ImagingGroups As XPInstantFeedbackSource Implements ICupsSubGroup.ImagingGroups
        Get
            Return INDSleImagingGroup.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleImagingGroup.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Description As String Implements ICupsSubGroup.Description
        Get
            Return INDmemoDescription.Text
        End Get
        Set(value As String)
            INDmemoDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del grupo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCupGroup As Integer? Implements ICupsSubGroup.IdCupGroup
        Get
            Return INDsleCupsGroup.EditValue
        End Get
        Set(value As Integer?)
            INDsleCupsGroup.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de grupo de imagenologia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdImagingGroup As Integer? Implements ICupsSubGroup.IdImagingGroup
        Get
            Return INDSleImagingGroup.EditValue
        End Get
        Set(value As Integer?)
            INDSleImagingGroup.EditValue = value


        End Set
    End Property

    'Public Property ImagingGroup As Domain.Crystal.Entities.RISGRIMAGE Implements ICupsSubGroup.ImagingGroup
    '    Get
    '        Return _imagingGroup
    '    End Get
    '    Set(value As Domain.Crystal.Entities.RISGRIMAGE)
    '        _imagingGroup = value
    '        If _imagingGroup IsNot Nothing Then
    '            'INDSleImagingGroup.Properties.NullText = String.Concat(_imagingGroup.CODIGO, " - ", _imagingGroup.NOMBRE)
    '            INDSleImagingGroup.Properties.NullText = _imagingGroup.CodigoNombre
    '        Else
    '            INDSleImagingGroup.Properties.NullText = ""
    '        End If
    '    End Set
    'End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Listado de si o no
    ''' </summary>
    Private ListShow As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PCupsSubGroup

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Contract"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.ContractSequence

    ''' <summary>
    ''' Representa la entidad de grupo de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim cupsSubGroup As CupsSubgroup

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
    Private record As BlockRecordContract

    'Private _imagingGroup As Domain.Crystal.Entities.RISGRIMAGE

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If Not SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If cupsSubGroup IsNot Nothing AndAlso cupsSubGroup.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MCupsSubGroup(Me.Tag.ToString())
                    AsyncLoader(True)
                    cupsSubGroup.MarkAsDeleted()
                    Dim result = Await Model.DeleteCupsSubGroup(cupsSubGroup)
                    If result.StateResult = True Then
                        'Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        SearchMode = False
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        Using model As New MCupsSubGroup(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveCupsSubGroup(cupsSubGroup, _idCurrentSequense)
            AsyncLoader(False)
            If Result.StateResult = True Then
                If cupsSubGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                ElseIf cupsSubGroup.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.cupsSubGroup = Result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                SearchMode = False
                Me.Deshacer()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                End If
            End If
        End Using
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        Await NewCupsSubGroup()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)},
                              New ColumnInfo() With {.Caption = "Grupo CUPS", .FieldName = "CupsGroupId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListCupsSubGroup
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa las tuplas
    ''' </summary>
    Private Sub InitializeTuple()
        ListShow = New List(Of Tuple(Of Boolean, String))
        ListShow.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListShow.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleShowOnImagingDashboard.Properties.DataSource = ListShow
    End Sub

    ''' <summary>
    ''' Valida el codigo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Async Function ValidateCode() As Task
        If INDbtnCode.Text = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty", NAME_MODULE)
            INDbtnCode.Focus()
        Else
            Await Me.LoadControls()
        End If
    End Function

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICupsSubGroup.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDmemoDescription.Enabled = value
            INDsleCupsGroup.Enabled = value
            If value Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.cupsSubGroup IsNot Nothing AndAlso Me.cupsSubGroup.Id > 0 Then
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
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsSubGroup.Code, Me.cupsSubGroup.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.cupsSubGroup.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsSubGroup.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.cupsSubGroup.Code, Me.cupsSubGroup.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.cupsSubGroup.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyCupsSubGroup.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        NameCSG = String.Empty
        Status = True
        Description = String.Empty
        IdCupGroup = Nothing
        INDsleCupsGroup.Properties.NullText = String.Empty
        INDSleImagingGroup.Properties.NullText = String.Empty

        ShowOnImagingDashboard = Nothing
        INDlyItemShowOnImagingDashboard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemShowOnImagingDashboard.AllowHide = True

        IdImagingGroup = Nothing
        INDLciImagingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciImagingGroup.AllowHide = True

        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        INDlyCupsSubGroup.EndUpdate()

        cupsSubGroup = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With cupsSubGroup
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameCSG
            .Description = Description
            .CupsGroupId = IdCupGroup

            If INDlyItemShowOnImagingDashboard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ShowOnImagingDashboard = ShowOnImagingDashboard
                .IdRisGrImage = IdImagingGroup
            Else
                .ShowOnImagingDashboard = Nothing
                .IdRisGrImage = Nothing
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        Using Model As New MCupsSubGroup(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetCupsSubgroup(INDbtnCode.Text.Trim)
            AsyncLoader(False)
            cupsSubGroup = resultOperation.ObjectEmbbeded
            If Not cupsSubGroup Is Nothing Then
                If cupsSubGroup.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(cupsSubGroup.Id))
                        With cupsSubGroup
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            NameCSG = .Name
                            Description = .Description
                            IdCupGroup = .CupsGroupId
                            INDsleCupsGroup.Properties.NullText = .CupsGroupDescription

                            ShowOnImagingDashboard = .ShowOnImagingDashboard
                            IdImagingGroup = .IdRisGrImage
                            Status = .Status

                            'Si hay grupo de imagenologia
                            If .IdRisGrImage IsNot Nothing AndAlso .IdRisGrImage > 0 Then
                                Dim image = Presenter.GetImagingGroupById(.IdRisGrImage)
                                If image IsNot Nothing Then
                                    INDSleImagingGroup.Properties.NullText = String.Concat(image.CODIGO, " - ", image.NOMBRE)
                                End If
                            End If

                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.cupsSubGroup.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordContract With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = cupsSubGroup.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(cupsSubGroup.Id)
                        ActionsOnControls = True
                    End Using
                Else
                    CreateNew()
                End If
            Else
                CreateNew()
            End If
        End Using
    End Function

    ''' <summary>
    ''' Crea la entidad cups
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateNew()
        Status = True
        cupsSubGroup = New CupsSubgroup With {.Status = True}
        ActionsOnControls = True
        INDtxtName.Focus()
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewCupsSubGroup() As Task
        cupsSubGroup = New CupsSubgroup() With {.Status = True}
        If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
            Me._idCurrentSequense = Me._sequense.ContractSequenceDetail(0).Id
        ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
            If Me.Sequense.ContractSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                Me._idCurrentSequense = Me._sequense.ContractSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                Else
                    Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                        Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
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
        Else
            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Using model As New MCupsSubGroup(Me.Tag.ToString())
                AsyncLoader(True)
                Dim state As Boolean = Not cupsSubGroup.Status
                Dim Result = Await model.ChangeState(Code, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    cupsSubGroup = Result.ObjectEmbbeded
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

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        Presenter = Nothing
        _sequense = Nothing
        cupsSubGroup = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequense = Nothing
        record = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCupsSubGroup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCupsSubGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PCupsSubGroup(Me)
        InitializeTuple()
        LoadStatus()
        Deshacer()
        SearchMode = False
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCupsSubGroup_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await ValidateCode()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCupsSubGroup_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del mas del control de cupsGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCupsGroup_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCupsGroup.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmCupsGroup With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeCupsGroup()
        End If
    End Sub


#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control cupsGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCupsGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCupsGroup.QueryPopUp
        If INDsleCupsGroup.Properties.DataSource Is Nothing Then
            Presenter.InitializeCupsGroup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control cupsGroup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleImagingGroup_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleImagingGroup.QueryPopUp
        If INDSleImagingGroup.Properties.DataSource Is Nothing Then
            Presenter.GetImagingGroupActive()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de grupo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCupsGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCupsGroup.EditValueChanged
        If IdCupGroup IsNot Nothing Then
            Dim cupsGroupXpo = Presenter.GetCupsGroupById(IdCupGroup)
            If cupsGroupXpo IsNot Nothing Then
                If cupsGroupXpo.ImagingType Then
                    INDlyItemShowOnImagingDashboard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemShowOnImagingDashboard.AllowHide = False
                Else
                    INDlyItemShowOnImagingDashboard.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemShowOnImagingDashboard.AllowHide = True
                    INDLciImagingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDLciImagingGroup.AllowHide = True
                End If
            End If
        End If
    End Sub

    '''' <summary>
    '''' Evento que se dispara al cambiar el valor del control de grupos de imagenologia
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    'Private Sub INDSleImagingGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleImagingGroup.EditValueChanged
    '    ImagingGroupValueChanged()
    'End Sub

    'Public Sub ImagingGroupValueChanged()
    '    If IdImagingGroup IsNot Nothing Then

    '            Dim Result = Presenter.GetImagingGroupById(IdImagingGroup.GetValueOrDefault)
    '            AsyncLoader(False)
    '            If Result.StateResult = True Then
    '                'cupsSubGroup = Result.ObjectEmbbeded
    '                'Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
    '                Dim imagingGroup = Result.ObjectEmbbeded
    '                If imagingGroup IsNot Nothing Then
    '                    INDLciImagingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
    '                    INDLciImagingGroup.AllowHide = False
    '                Else
    '                    INDLciImagingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    '                    INDLciImagingGroup.AllowHide = True
    '                End If

    '            Else
    '                If Result.MessageResult(0) = ErrorConcurrencia Then
    '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
    '                Else
    '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
    '                End If
    '            End If

    '    End If
    'End Sub



#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
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
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
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
        SearchMode = False
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

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.ContractSequenceDetail IsNot Nothing Then
            If Me._sequense.ContractSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.ContractSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

    Private Sub INDsleShowOnImagingDashboard_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleShowOnImagingDashboard.EditValueChanged
        If ShowOnImagingDashboard Then
            INDLciImagingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciImagingGroup.AllowHide = False
        Else
            IdImagingGroup = Nothing
            INDLciImagingGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciImagingGroup.AllowHide = True
        End If
    End Sub

    'Private Sub INDSleImagingGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleImagingGroup.EditValueChanged
    '    If IdImagingGroup IsNot Nothing Then
    '        Presenter.GetImagingGroupById(IdImagingGroup.GetValueOrDefault())
    '    Else
    '        INDSleImagingGroup.Properties.NullText = ""
    '    End If
    'End Sub



#End Region

End Class