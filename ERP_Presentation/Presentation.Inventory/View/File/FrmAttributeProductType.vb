'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 18/09/2014
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
Imports Presentation.Accounting
Imports System.Text
Imports Presentation.Inventory.MVP
Imports Presentation.Payroll

#End Region

Public Class FrmAttributeProductType
    Implements IAttributeProductType, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo de la lista de opciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeOptions As String Implements IAttributeProductType.CodeOptions
        Get
            Return INDtxtCodeOptions.EditValue
        End Get
        Set(value As String)
            INDtxtCodeOptions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el nombre de la lista de opciones
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameOptions As String Implements IAttributeProductType.NameOptions
        Get
            Return INDtxtNameOptions.EditValue
        End Get
        Set(value As String)
            INDtxtNameOptions.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IAttributeProductType.Status
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
    Public Property Sequence As InventorySequence Implements IAttributeProductType.Sequense
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAttributeProductType.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IAttributeProductType.MyTag
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
    Public Property Code As String Implements IAttributeProductType.Code
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
    Public Property NameA As String Implements IAttributeProductType.NameA
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de dato
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdDataType As Integer Implements IAttributeProductType.IdDataType
        Get
            Return INDsleDataType.EditValue
        End Get
        Set(value As Integer)
            INDsleDataType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdProductType As Integer Implements IAttributeProductType.IdProductType
        Get
            Return INDsleProductType.EditValue
        End Get
        Set(value As Integer)
            INDsleProductType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del tipo de producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ProductTypeXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAttributeProductType.ProductTypeXpo
        Get
            Return INDsleProductType.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleProductType.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador de atributos de tipo de producto
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAttributeProductType

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim DataTypeList As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Representa la entidad de atributos
    ''' </summary>
    ''' <remarks></remarks>
    Dim attributeProductType As AttributeProductType

    ''' <summary>
    ''' Obtiene o establece el listado de opciones de los atributos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListAttributeProductTypeOptionList As List(Of AttributeProductTypeOptionList)

    ''' <summary>
    ''' Obtiene o establece el listado de eliminados de opciones de los atributos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteAttributeProductTypeOptionList As List(Of AttributeProductTypeOptionList)

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordInventory

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
        ' Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If attributeProductType IsNot Nothing AndAlso attributeProductType.Id > -1 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Using Model As New MAttributeProductType(Me.Tag.ToString())
        '            AsyncLoader(True)
        '            'attributeProductType.MarkAsDeleted()
        '            Dim result = Await Model.DeleteAttributeProductType(attributeProductType)
        '            If result.StateResult = True Then
        '                Await Me.DeleteDocumentIndexed()
        '                AsyncLoader(False)
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                Me.Deshacer()
        '            Else
        '                AsyncLoader(False)
        '                If result.MessageResult(0) = "-999" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                ElseIf result.MessageResult(0) = "-000" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                End If
        '            End If
        '        End Using
        '    End If
        'End If


        If Me.attributeProductType IsNot Nothing AndAlso Me.attributeProductType.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MAttributeProductType(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteAttributeProductType(Me.attributeProductType)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        'If ValidateControls() = True Then
        '    If viewOptions.RowCount = 0 Then
        '        'Valido que solo exija las opciones en el tipo de dato 4 - Lista de Opciones
        '        If INDsleDataType.EditValue = 4 Then
        '            Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una opción"
        '            Exit Sub
        '        End If
        '    End If
        'Else
        '    Exit Sub
        'End If
        'AssigningValues()
        'Using model As New MAttributeProductType(Me.Tag.ToString())
        '    AsyncLoader(True)
        '    Dim Result = Await model.SaveAttributeProductType(attributeProductType, _idCurrentSequense)
        '    AsyncLoader(False)
        '    If Result.StateResult = True Then
        '        If attributeProductType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            'Se descarta la secuencia numerica usada
        '            If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
        '                Me.DicSequense(Me._sequense.InventorySequenceDetail(0).Id).RemoveAt(0)
        '            End If
        '            If Me._sequense.Sequential Then
        '                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
        '            Else
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
        '            End If
        '        ElseIf attributeProductType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '        End If
        '        Me.attributeProductType = Result.ObjectEmbbeded
        '        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '        AsyncLoader(False)
        '        Me.Deshacer()
        '    Else
        '        If Result.MessageResult(0) = ErrorConcurrencia Then
        '            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '        Else
        '            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '        End If
        '    End If
        'End Using


        If ValidateControls() = True Then
            If viewOptions.RowCount = 0 Then
                'Valido que solo exija las opciones en el tipo de dato 4 - Lista de Opciones
                If INDsleDataType.EditValue = 4 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Se debe agregar mínimo una opción"
                    Exit Sub
                End If
            End If
        Else
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MAttributeProductType(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of AttributeProductType) = Await Model.SaveAttributeProductType(Me.attributeProductType, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If attributeProductType.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.attributeProductType = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewAttributeProductType()
        End If
    End Sub

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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Nombre", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo Producto", .FieldName = "ProductTypeId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo Dato", .FieldName = "DataTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAttributeProductType
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Llena el control de tipo de dato
    ''' </summary>
    Private Sub InitializeSearch()
        DataTypeList = New List(Of Tuple(Of Integer, String))
        DataTypeList.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("DateType", NAME_MODULE)))
        DataTypeList.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("NumberType", NAME_MODULE)))
        DataTypeList.Add(New Tuple(Of Integer, String)(3, ResourceManager.GetString("TextType", NAME_MODULE)))
        DataTypeList.Add(New Tuple(Of Integer, String)(4, ResourceManager.GetString("OptionsList", NAME_MODULE)))
        INDsleDataType.Properties.DataSource = DataTypeList.ToList
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAttributeProductType.ActionsOnControls
        Set(value As Boolean)
            INDlyAttributeProductType.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleProductType.Enabled = value
            INDsleDataType.Enabled = value
            INDlyAttributeProductType.EndUpdate()
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
        Me.ViewModeEditHold = True
        If Me.attributeProductType IsNot Nothing AndAlso Me.attributeProductType.Id > 0 Then
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
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.attributeProductType.Code, Me.attributeProductType.Name), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.attributeProductType.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.attributeProductType.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.attributeProductType.Code, Me.attributeProductType.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.attributeProductType.Code)
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
        'INDlyAttributeProductType.BeginUpdate()
        'ActionsOnControls = False
        'Code = String.Empty
        'NameA = String.Empty
        'IdProductType = Nothing
        'INDsleProductType.Properties.NullText = String.Empty
        'IdDataType = Nothing
        'CodeOptions = String.Empty
        'NameOptions = String.Empty
        'ListAttributeProductTypeOptionList = Nothing
        'ListDeleteAttributeProductTypeOptionList = Nothing
        'INDgcOptionList.DataSource = Nothing

        ' Status = True
        ' BarraBotones.CleanAuditBasic()
        ' INDlyAttributeProductType.EndUpdate()

        'attributeProductType = Nothing
        ' Me.BarraBotones.StatusRecordVisible = False
        ' DeleteBlockedRecord()
        'Me._doc = Nothing
        'Me.BarraBotones.EnableBarItems()
        ' Me.BarraBotones.DisableBarDocument()



        INDlyAttributeProductType.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        Code = String.Empty
        NameA = String.Empty
        IdProductType = Nothing
        INDsleProductType.Properties.NullText = String.Empty
        IdDataType = Nothing
        CodeOptions = String.Empty
        NameOptions = String.Empty
        ListAttributeProductTypeOptionList = Nothing
        ListDeleteAttributeProductTypeOptionList = Nothing
        INDgcOptionList.DataSource = Nothing
        'Limpiar controles
        attributeProductType = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyAttributeProductType.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With attributeProductType
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameA
            .ProductTypeId = IdProductType
            .DataType = IdDataType

            If INDlygOptionList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
                If attributeProductType.Id > 0 Then
                    If ListDeleteAttributeProductTypeOptionList IsNot Nothing Then
                        For Each item As AttributeProductTypeOptionList In ListDeleteAttributeProductTypeOptionList
                            .AttributeProductTypeOptionList.Add(item)
                        Next
                    End If
                    If attributeProductType.AttributeProductTypeOptionList.Count > 0 Then
                        While attributeProductType.AttributeProductTypeOptionList.Count > 0
                            attributeProductType.AttributeProductTypeOptionList.Item(0).MarkAsDeleted()
                        End While
                    End If
                Else
                    attributeProductType.AttributeProductTypeOptionList = Nothing
                End If
            Else
                If ListDeleteAttributeProductTypeOptionList IsNot Nothing Then
                    For Each item As AttributeProductTypeOptionList In ListDeleteAttributeProductTypeOptionList
                        .AttributeProductTypeOptionList.Add(item)
                    Next
                End If
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
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'Using Model As New MAttributeProductType(CStr(Me.Tag))
        '    AsyncLoader(True)
        '    Dim resultOperation = Await Model.GetAttributeProductType(INDbtnCode.Text.Trim)
        '    INDlyAttributeProductType.BeginUpdate()
        '    attributeProductType = resultOperation.ObjectEmbbeded
        '    If Not attributeProductType Is Nothing Then
        '        If attributeProductType.Id > 0 Then
        '            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(attributeProductType.Id))
        '                With attributeProductType
        '                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                    Code = .Code
        '                    NameA = .Name
        '                    IdProductType = .ProductTypeId
        '                    INDsleProductType.Properties.NullText = .ProductTypeDescription
        '                    IdDataType = .DataType

        '                    ListAttributeProductTypeOptionList = .AttributeProductTypeOptionList.ToList
        '                    INDgcOptionList.DataSource = Nothing
        '                    INDgcOptionList.DataSource = ListAttributeProductTypeOptionList

        '                    Status = .Status
        '                End With
        '                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.attributeProductType.Code)
        '                If result.Id = 0 Then
        '                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                    state.State = Domain.Base.Entities.ObjectState.Added
        '                    record = New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = attributeProductType.Id}
        '                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                    record = operation.ObjectEmbbeded
        '                Else
        '                    record = result
        '                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                End If
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.SetDocuments(attributeProductType.Id)
        '                AsyncLoader(False)
        '                ActionsOnControls = True
        '            End Using
        '        Else
        '            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            'Me.Code = String.Empty
        '            AsyncLoader(False)
        '            If Me._sequense.IsManual Then
        '                Await Me.NewAttributeProductType()
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.Code = String.Empty
        '                Deshacer()
        '                INDbtnCode.Focus()
        '            End If
        '        End If
        '    Else
        '        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        'Me.Code = String.Empty
        '        AsyncLoader(False)
        '        If Me._sequense.IsManual Then
        '            Await Me.NewAttributeProductType()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Me.Code = String.Empty
        '            Deshacer()
        '            INDbtnCode.Focus()
        '        End If
        '    End If
        'End Using
        'INDlyAttributeProductType.EndUpdate()


        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MAttributeProductType(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetAttributeProductType(INDbtnCode.Text.Trim)
                    INDlyAttributeProductType.BeginUpdate()
                    attributeProductType = resultOperation.ObjectEmbbeded
                    If attributeProductType IsNot Nothing AndAlso attributeProductType.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(attributeProductType.Id))
                            With attributeProductType
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                NameA = .Name
                                IdProductType = .ProductTypeId
                                INDsleProductType.Properties.NullText = .ProductTypeDescription
                                IdDataType = .DataType

                                ListAttributeProductTypeOptionList = .AttributeProductTypeOptionList.ToList
                                INDgcOptionList.DataSource = Nothing
                                INDgcOptionList.DataSource = ListAttributeProductTypeOptionList

                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.attributeProductType.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = attributeProductType.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(attributeProductType.Id, Me.Tag.ToString(), Nothing, GetType(AttributeProductType).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewAttributeProductType()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyAttributeProductType.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewAttributeProductType() As Task
        'attributeProductType = New AttributeProductType()
        'If Me._sequense.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequense = Me._sequense.InventorySequenceDetail(0).Id
        '    ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequense = Me._sequense.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Function
        '        End If
        '    End If
        '    If Not Me._sequense.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                Me.ActionsOnControls = True
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '            Else
        '                Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                    Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
        '                End Using
        '                If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
        '                    Me.Code = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
        '                    Me.ActionsOnControls = True
        '                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '            Me.ActionsOnControls = True
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '        End If
        '    Else
        '        Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
        '        Me.ActionsOnControls = True
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        '    End If
        'End If



        attributeProductType = New AttributeProductType() With {.Status = True}
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

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        'If Not String.IsNullOrEmpty(Code) Then
        '    Dim state As Boolean
        '    Select Case Status
        '        Case CBool(eActionsStatusRecords.Active)
        '            state = True
        '        Case CBool(eActionsStatusRecords.Inactive)
        '            state = False
        '    End Select
        '    Using model As New MAttributeProductType(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result = Await model.ChangeState(Code, state)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Else
        '    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        'End If



        If Not String.IsNullOrEmpty(Me.attributeProductType.Code) Then
            Try
                Using model As New MAttributeProductType(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not attributeProductType.Status
                    Dim result As ActionResult(Of AttributeProductType) = Await model.ChangeState(Me.attributeProductType.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.attributeProductType = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Agrega un item a la lista de opciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddOptionList()
        If ValidatePopup() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty", "Commons")
            INDtxtCodeOptions.Focus()
            Exit Sub
        End If

        If ListAttributeProductTypeOptionList Is Nothing Then
            ListAttributeProductTypeOptionList = New List(Of AttributeProductTypeOptionList)
        End If

        Dim attributeProductTypeOptionList As New AttributeProductTypeOptionList
        attributeProductTypeOptionList.Code = CodeOptions
        attributeProductTypeOptionList.Name = NameOptions

        ListAttributeProductTypeOptionList.Add(attributeProductTypeOptionList)
        attributeProductType.AttributeProductTypeOptionList.Add(attributeProductTypeOptionList)
        INDgcOptionList.DataSource = Nothing
        INDgcOptionList.DataSource = ListAttributeProductTypeOptionList
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OptionAgregated", NAME_MODULE)

        If attributeProductType.Id > 0 Then
            attributeProductType.MarkAsModified()
        End If

        CodeOptions = String.Empty
        NameOptions = String.Empty
        INDtxtCodeOptions.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un item a la lista de opciones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteOptionList()
        Dim aptol As AttributeProductTypeOptionList = CType(viewOptions.GetFocusedRow, AttributeProductTypeOptionList)
        If aptol.Id <> 0 Then
            If ListDeleteAttributeProductTypeOptionList Is Nothing Then
                ListDeleteAttributeProductTypeOptionList = New List(Of AttributeProductTypeOptionList)
            End If
            aptol.MarkAsDeleted()
            ListDeleteAttributeProductTypeOptionList.Add(aptol)
        End If
        ListAttributeProductTypeOptionList.Remove(aptol)
        attributeProductType.AttributeProductTypeOptionList.Remove(aptol)
        If aptol.Id > 0 Then
            attributeProductType.MarkAsModified()
        End If
        INDgcOptionList.DataSource = Nothing
        INDgcOptionList.DataSource = ListAttributeProductTypeOptionList
    End Sub

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidatePopup() As Boolean
        If CodeOptions = String.Empty Then
            Return False
        End If
        If NameOptions = String.Empty Then
            Return False
        End If
        Return True
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        DataTypeList = Nothing
        _sequence = Nothing
        attributeProductType = Nothing
        ListAttributeProductTypeOptionList = Nothing
        ListDeleteAttributeProductTypeOptionList = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAttributeProductType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlyAttributeProductType, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAttributeProductType(Me)
        Presenter.GetSequense()
        InitializeSearch()
        INDsleDataType.Properties.Buttons.Item(1).Visible = False

        IndigoGridControl1.RefreshGrid(INDgcOptionList)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewOptions, ListActions)
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
    Private Sub FrmAttributeProductType_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
        ''If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        ''    If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
        ''        Await Me.NewAttributeProductType()
        ''    Else
        ''        Await Me.LoadControls()
        ''    End If
        ''End If
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(Code) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(Code) Then
        '            Await Me.NewAttributeProductType()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If


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
                    Await Me.NewAttributeProductType()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter o f4 al popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceInformation_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceInformation.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceInformation.ShowPopup()
            INDtxtCodeOptions.Focus()
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
    Private Sub FrmAttributeProductType_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de tipo de dato
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleDataType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleDataType.EditValueChanged
        If INDsleDataType.EditValue IsNot Nothing Then
            If INDsleDataType.EditValue <> 4 Then
                INDlygOptionList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDlygOptionList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        Else
            INDlygOptionList.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de tipo de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProductType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductType.QueryPopUp
        If INDsleProductType.Properties.DataSource Is Nothing Then
            Presenter.InitializeProductType()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de tipo de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleProductType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmProductType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeProductType()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AddOptionList()
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteOptionList()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteOptionList()
    End Sub

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
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.InventorySequenceDetail IsNot Nothing Then
        '    If Me._sequense.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.InventorySequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If


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

#End Region
    
End Class