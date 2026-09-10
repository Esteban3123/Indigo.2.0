'***********************************************************************
' Assembly         : Presentacion.FixedAsset
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/06/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.FixedAsset.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Maintenance
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Payments.MVP
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Inventory
Imports System.ComponentModel
#End Region

Public Class FrmFixedAssetChangePlate
    Implements IFixedAssetChangePlate, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IFixedAssetChangePlate.Code
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
    ''' Fecha del documento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DocumentDate As Date? Implements IFixedAssetChangePlate.DocumentDate
        Get
            Return INDdteDocumentDate.EditValue
        End Get
        Set(value As Date?)
            INDdteDocumentDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFixedAssetChangePlate.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IFixedAssetChangePlate.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Detalles
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Observation As String Implements IFixedAssetChangePlate.Observation
        Get
            Return INDmemoObservations.EditValue
        End Get
        Set(value As String)
            INDmemoObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As FixedAssetSequence Implements IFixedAssetChangePlate.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As FixedAssetSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.FixedAssetSequenceDetail In Me._sequence.FixedAssetSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PhysicalAssetId As Integer? Implements IFixedAssetChangePlate.PhysicalAssetId
        Get
            Return INDslePhysicalAsset.EditValue
        End Get
        Set(value As Integer?)
            INDslePhysicalAsset.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource activo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property PhysicalAssetXpo As XPInstantFeedbackSource Implements IFixedAssetChangePlate.PhysicalAssetXpo
        Get
            Return INDslePhysicalAsset.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDslePhysicalAsset.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Nueva placa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NewPlate As String Implements IFixedAssetChangePlate.NewPlate
        Get
            Return INDtxtNewPlate.EditValue
        End Get
        Set(value As String)
            INDtxtNewPlate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Placa antigua
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property OldPlate As String Implements IFixedAssetChangePlate.OldPlate
        Get
            Return INDtxtOldPlate.EditValue
        End Get
        Set(value As String)
            INDtxtOldPlate.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable que contiene el objeto torre
    ''' </summary>
    Dim FixedAssetChangePlate As FixedAssetChangePlate

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFixedAssetChangePlate

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordFixedAsset

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "FixedAssets"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.FixedAssetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListFixedAssetChangePlateDetail As List(Of FixedAssetChangePlateDetail)

    ''' <summary>
    ''' Listado de eliminados
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteFixedAssetChangePlateDetail As List(Of FixedAssetChangePlateDetail)

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
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Try
            If ValidateControls() = True Then
                If FixedAssetChangePlate.Status <> 3 Then
                    If ListFixedAssetChangePlateDetail Is Nothing OrElse ListFixedAssetChangePlateDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un activo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetChangePlate(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.SaveFixedAssetChangePlate(FixedAssetChangePlate, _idCurrentSequence)
                    If Result.StateResult = True Then
                        If FixedAssetChangePlate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            'Se descarta la secuencia numerica usada
                            If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                                Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                            End If
                            If FixedAssetChangePlate.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            ElseIf FixedAssetChangePlate.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                            End If
                        ElseIf FixedAssetChangePlate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            If FixedAssetChangePlate.Status = 3 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                            ElseIf FixedAssetChangePlate.Status = 2 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                            ElseIf FixedAssetChangePlate.Status = 1 Then
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                            End If
                        End If
                        Me.FixedAssetChangePlate = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        AsyncLoader(False)
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                        If Result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.MensajeError) = Result.Message
                        ElseIf Result.StatusCode = eStatusResult.WARNING Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewFixedAssetChangePlate()
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Confirmar()
        Try
            If ValidateControls() = True Then
                If FixedAssetChangePlate.Status <> 3 Then
                    If ListFixedAssetChangePlateDetail Is Nothing OrElse ListFixedAssetChangePlateDetail.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar al menos un activo."
                        Exit Sub
                    End If
                    AssigningValues()
                End If
                Using model As New MFixedAssetChangePlate(MyTag)
                    AsyncLoader(True)
                    Dim Result = Await model.ConfirmFixedAssetChangePlate(FixedAssetChangePlate, _idCurrentSequence)
                    If Result.StatusCode = eStatusResult.SUCCESS Then
                        If FixedAssetChangePlate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveConfirm"), Result.ObjectEmbbeded.Code)
                        ElseIf FixedAssetChangePlate.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateConfirm")
                        End If
                        Me.FixedAssetChangePlate = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                        AsyncLoader(False)
                        PhysicalAssetXpo = Nothing
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                        If Result.StatusCode = eStatusResult.WARNING Then
                            Mensaje(EeventViewerImages.Advertencia) = Result.Message
                        ElseIf Result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.MensajeError) = Result.Message
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Agrega un detalle a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddDetail()
        Dim errors = ValidatePopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        If ListFixedAssetChangePlateDetail Is Nothing Then
            ListFixedAssetChangePlateDetail = New List(Of FixedAssetChangePlateDetail)
        End If
        If ListFixedAssetChangePlateDetail.Where(Function(item) item.FixedAssetPhysicalAssetId = PhysicalAssetId).Count > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "EL activo " + INDslePhysicalAsset.Text + " ya existe en la lista"
            Exit Sub
        End If
        Dim FixedAssetChangePlateDetail As New FixedAssetChangePlateDetail
        With FixedAssetChangePlateDetail
            .FixedAssetPhysicalAssetId = PhysicalAssetId
            .PhysicalAssetDescription = INDslePhysicalAsset.Text
            .OldPlate = OldPlate
            .NewPlate = NewPlate
        End With
        ListFixedAssetChangePlateDetail.Add(FixedAssetChangePlateDetail)
        INDgcDetails.DataSource = Nothing
        INDgcDetails.DataSource = ListFixedAssetChangePlateDetail
        Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
        CleanPopup()
        INDslePhysicalAsset.Focus()
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanPopup()
        PhysicalAssetId = Nothing
        INDslePhysicalAsset.Properties.NullText = String.Empty
        OldPlate = String.Empty
        NewPlate = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePopup() As String
        Dim errors As New StringBuilder
        If PhysicalAssetId Is Nothing Then
            errors.AppendLine("Debe seleccionar un activo")
        End If
        If NewPlate Is String.Empty OrElse NewPlate Is Nothing Then
            errors.AppendLine("Debe ingresar una placa nueva")
        End If
        Return errors.ToString
    End Function

    ''' <summary>
    ''' Elimina el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveDetail()
        Dim FixedAssetChangePlateDetail = DirectCast(INDviewDetails.GetFocusedRow(), FixedAssetChangePlateDetail)
        ListFixedAssetChangePlateDetail.Remove(FixedAssetChangePlateDetail)

        If FixedAssetChangePlateDetail.Id > 0 Then
            If ListDeleteFixedAssetChangePlateDetail Is Nothing Then
                ListDeleteFixedAssetChangePlateDetail = New List(Of FixedAssetChangePlateDetail)
            End If
            FixedAssetChangePlateDetail.MarkAsDeleted()
            ListDeleteFixedAssetChangePlateDetail.Add(FixedAssetChangePlateDetail)
        End If

        INDgcDetails.DataSource = Nothing
        INDgcDetails.DataSource = ListFixedAssetChangePlateDetail
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.FixedAssetChangePlate IsNot Nothing AndAlso Me.FixedAssetChangePlate.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFixedAssetChangePlate.ActionsOnControls
        Set(value As Boolean)
            INDlyChangePlate.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDdteDocumentDate.Enabled = value
            INDmemoObservations.Enabled = value
            INDpceDetails.Enabled = value
            INDgcDetails.Enabled = value
            INDlyChangePlate.EndUpdate()
            If value Then
                INDdteDocumentDate.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

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
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Fecha Documento", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.33)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.34)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListFixedAssetChangePlate
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            Await LoadControls()
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetChangePlate.Code, FixedAssetChangePlate.DocumentDate),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.FixedAssetChangePlate.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetChangePlate.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.FixedAssetChangePlate.Code, FixedAssetChangePlate.DocumentDate)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.FixedAssetChangePlate.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyChangePlate.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        DocumentDate = Nothing
        Observation = Nothing
        INDgcDetails.DataSource = Nothing
        ListFixedAssetChangePlateDetail = Nothing
        ListDeleteFixedAssetChangePlateDetail = Nothing
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        FixedAssetChangePlate = Nothing
        INDlyChangePlate.EndUpdate()

        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With FixedAssetChangePlate
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .DocumentDate = DocumentDate
            .Detail = Observation
            '.OperatingUnitId = Me.BarraBotones.OperatingUnit.Id

            .FixedAssetChangePlateDetail.Clear()
            If ListFixedAssetChangePlateDetail IsNot Nothing AndAlso ListFixedAssetChangePlateDetail.Count > 0 Then
                ListFixedAssetChangePlateDetail.ForEach(Sub(item)
                                                            .FixedAssetChangePlateDetail.Add(item)
                                                        End Sub)
            End If

            If ListDeleteFixedAssetChangePlateDetail IsNot Nothing AndAlso ListDeleteFixedAssetChangePlateDetail.Count > 0 Then
                ListDeleteFixedAssetChangePlateDetail.ForEach(Sub(item)
                                                                  .FixedAssetChangePlateDetail.Add(item)
                                                              End Sub)
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Me.BarraBotones.StatusRecordVisible = True
                Using Model As New MFixedAssetChangePlate(CStr(Me.Tag))
                    AsyncLoader(True)
                    FixedAssetChangePlate = (Await Model.GetFixedAssetChangePlate(INDbtnCode.Text.Trim)).ObjectEmbbeded
                    INDlyChangePlate.BeginUpdate()
                    If FixedAssetChangePlate IsNot Nothing AndAlso FixedAssetChangePlate.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetChangePlate.Id))
                            With FixedAssetChangePlate
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)

                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                DocumentDate = .DocumentDate
                                Observation = .Detail
                                BarraBotones.StatusRecord = .Status.ToString

                                ListFixedAssetChangePlateDetail = .FixedAssetChangePlateDetail.ToList
                                INDgcDetails.DataSource = Nothing
                                INDgcDetails.DataSource = ListFixedAssetChangePlateDetail
                            End With
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetChangePlate.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetChangePlate.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.SetDocuments(FixedAssetChangePlate.Id, Me.Tag.ToString(), Nothing, GetType(FixedAssetChangePlate).Name)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            If FixedAssetChangePlate.Status = 1 Then
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                                ReadOnlyControls(True)
                            End If

                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetChangePlate.Id, 0, FixedAssetChangePlate.Id, _idOperativeUnit)
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewFixedAssetChangePlate()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyChangePlate.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If

        'Me.BarraBotones.StatusRecordVisible = True
        'Try
        '    AsyncLoader(True)
        '    Using Model As New MFixedAssetChangePlate(MyTag)
        '        Dim resultOperation = Await Model.GetFixedAssetChangePlate(INDbtnCode.Text.Trim)
        '        INDlyChangePlate.BeginUpdate()
        '        FixedAssetChangePlate = resultOperation.ObjectEmbbeded
        '        If Not FixedAssetChangePlate Is Nothing Then
        '            If FixedAssetChangePlate.Id > 0 Then
        '                Using ModelRecord As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(FixedAssetChangePlate.Id))
        '                    With FixedAssetChangePlate
        '                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                        Code = .Code
        '                        DocumentDate = .DocumentDate
        '                        Observation = .Detail
        '                        BarraBotones.StatusRecord = .Status.ToString

        '                        ListFixedAssetChangePlateDetail = .FixedAssetChangePlateDetail.ToList
        '                        INDgcDetails.DataSource = Nothing
        '                        INDgcDetails.DataSource = ListFixedAssetChangePlateDetail
        '                    End With
        '                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FixedAssetChangePlate.Code)
        '                    If result.Id = 0 Then
        '                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                        state.State = Domain.Base.Entities.ObjectState.Added
        '                        record = New BlockRecordFixedAsset With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = FixedAssetChangePlate.Id}
        '                        Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                        record = operation.ObjectEmbbeded
        '                    Else
        '                        record = result
        '                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                    End If
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                    Me.BarraBotones.SetDocuments(FixedAssetChangePlate.Id)
        '                    AsyncLoader(False)
        '                    ActionsOnControls = True
        '                    If FixedAssetChangePlate.Status = 1 Then
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        '                    Else
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '                        ReadOnlyControls(True)
        '                    End If
        '                End Using
        '                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '                Me.BarraBotones.PrintReport(PrintReportAction.None, FixedAssetChangePlate.Id, 0, FixedAssetChangePlate.Id, _idOperativeUnit)
        '            Else
        '                AsyncLoader(False)
        '                If Me._sequence.IsManual Then
        '                    Me.NewFixedAssetChangePlate()
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                    Me.Code = String.Empty
        '                    Deshacer()
        '                    INDbtnCode.Focus()
        '                End If
        '            End If
        '        Else
        '            AsyncLoader(False)
        '            If Me._sequence.IsManual Then
        '                Me.NewFixedAssetChangePlate()
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.Code = String.Empty
        '                Deshacer()
        '                INDbtnCode.Focus()
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    Throw ex
        'End Try
        'INDlyChangePlate.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewFixedAssetChangePlate() As Task
        Me.FixedAssetChangePlate = New FixedAssetChangePlate()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = "1"
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
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



        'If _sequence Is Nothing OrElse _sequence.Id = 0 Then
        '    Mensaje(EeventViewerImages.Advertencia) = "El formulario no tiene parametrizada la secuencia numérica"
        '    Exit Sub
        'End If
        'Me.FixedAssetChangePlate = New FixedAssetChangePlate()
        'Me.BarraBotones.StatusRecordVisible = True
        'Me.BarraBotones.StatusRecord = "1"
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.FixedAssetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.FixedAssetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Sub
        '        End If
        '    End If
        '    If Not Me._sequence.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequenceFixedAsset(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'End If
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FixedAssetChangePlate = Nothing
        Presenter = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListFixedAssetChangePlateDetail = Nothing
        ListDeleteFixedAssetChangePlateDetail = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetChangePlate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyChangePlate, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFixedAssetChangePlate(Me)
        IndigoGridControl1.RefreshGrid(INDgcDetails)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewDetails, ListActions)
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFixedAssetChangePlate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un concepto de notas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
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
                    Await Me.NewFixedAssetChangePlate()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDetails_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceDetails.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.F4 OrElse e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDpceDetails.ShowPopup()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "MenuContext"

    ''' <summary>
    ''' Despliega los botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        RemoveDetail()
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        RemoveDetail()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePhysicalAsset_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePhysicalAsset.EditValueChanged
        If PhysicalAssetId IsNot Nothing Then
            'Dim xpo = DirectCast(DirectCast(INDviewSearchPhysical.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.FixedAssetRepository.FixedAssetPhysicalAssetXpo)
            Dim xpo = Presenter.GetPhysicalById(PhysicalAssetId)
            If xpo IsNot Nothing Then
                OldPlate = xpo.Plate
            End If
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar los detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceDetails_Popup(sender As Object, e As EventArgs) Handles INDpceDetails.Popup
        INDslePhysicalAsset.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePhysicalAsset_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePhysicalAsset.QueryPopUp
        If PhysicalAssetXpo Is Nothing Then
            Presenter.InitializePhysicalAsset()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddAssets_Click(sender As Object, e As EventArgs) Handles INDbtnAddAssets.Click
        AddDetail()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

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
        FixedAssetChangePlate.Status = 1
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
        FixedAssetChangePlate.Status = 1
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        FixedAssetChangePlate.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        FixedAssetChangePlate.Status = 2
        Confirmar()
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        FixedAssetChangePlate.Status = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, FixedAssetChangePlate.Id, 0, FixedAssetChangePlate.Id, _idOperativeUnit)
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
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.FixedAssetSequenceDetail IsNot Nothing Then
                If Not Me._sequence.FixedAssetSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class