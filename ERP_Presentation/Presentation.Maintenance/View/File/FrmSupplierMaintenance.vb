'***********************************************************************
' Assembly         : Presentacion.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 27-10-2014
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"
Imports Presentation.Maintenance.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Maintenance.Entities
Imports Presentation.Common.MVP
Imports System.Runtime.CompilerServices
Imports DevExpress.XtraEditors
Imports System.Windows.Forms

Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Common
Imports Presentation.Common
Imports Presentation.Payroll
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Xpo
Imports Presentation.Accounting
Imports DevExpress.Utils.Menu

#End Region


''' <summary>
''' Clase que contiene todo el comportamiento de la vista......
''' </summary>
Public Class FrmSupplierMaintenance
    Implements ISupplierMaintenance, ICustomizableForm

#Region "Variable Globales Propiedades Intefaz y Load"

    Dim listAdress As List(Of Domain.Maintenance.Entities.AddressMaintenance)

    Dim Pivote As Boolean = False

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    

    ''' <summary>
    ''' Obtiene o establece los dias de plazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TimeLimitDays As Integer Implements ISupplierMaintenance.TimeLimitDays
        Get
            Return INDseTimeLimitDays.EditValue
        End Get
        Set(value As Integer)
            INDseTimeLimitDays.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo del fabricante
    ''' </summary>
    Public Property CodeSupplier As String Implements ISupplierMaintenance.CodeSupplier
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
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
    ''' Esta propiedad contiene el nombre del fabricante
    ''' </summary>
    Public Property NameSupplier As String Implements ISupplierMaintenance.NameSupplier
        Get
            Return INDtxtName.Text.Trim
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el Apellido del fabricante
    ''' </summary>
    Public Property LastNameSupplier As String Implements ISupplierMaintenance.LastNameSupplier
        Get
            Return INDtxtLastName.Text.Trim
        End Get
        Set(value As String)
            INDtxtLastName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo CMMS
    ''' </summary>
    Public Property CodeCMMS As String Implements ISupplierMaintenance.CodeCMMS
        Get
            Return INDtxtCodeCMMS.Text.Trim
        End Get
        Set(value As String)
            INDtxtCodeCMMS.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene la direccion del sitio web del fabricante
    ''' </summary>
    Public Property WebSiteSupplier As String Implements ISupplierMaintenance.WebSiteSupplier
        Get
            Return INDtxtWebSite.Text.Trim
        End Get
        Set(value As String)
            INDtxtWebSite.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el tipo de proveedor 
    ''' </summary>
    Public Property StateSupplier As Boolean Implements ISupplierMaintenance.StateSupplier
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
    ''' Activa o inactiva los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ISupplierMaintenance.ActionsOnControls
        Set(value As Boolean)
            INDlySupplier.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtLastName.Enabled = value
            INDseTimeLimitDays.Enabled = value
            INDckRetentionIVA.Enabled = value
            INDtxtCodeCMMS.Enabled = value
            INDtxtWebSite.Enabled = value
            INDsleCity.Enabled = value
            INDChkManufacturer.Enabled = value
            INDChkResponsibleWarranty.Enabled = value
            INDChkSeller.Enabled = value

            INDpceContactData.Enabled = value
            INDlySupplier.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PSupplierMaintenance

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.MaintenanceSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordMaintenance

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Maintenance"

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements ISupplierMaintenance.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As MaintenanceSequence Implements ISupplierMaintenance.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As MaintenanceSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MaintenanceSequenceDetail In Me._sequence.MaintenanceSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Contiene el listado de las ciudades
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ISupplierMaintenance.CitiesXpo
        Get
            Return CType(INDsleCity.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la ciudad de la dependencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdCity As Integer Implements ISupplierMaintenance.IdCity
        Get
            Return CInt(INDsleCity.EditValue)
        End Get
        Set(value As Integer)
            INDsleCity.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Variable que contiene la entidad Proveedores Mantenimiento
    ''' </summary>
    Dim Supplier As Domain.Maintenance.Entities.SupplierMaintenance

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que maneja el listado de los telefonos agregados al control
    ''' </summary>
    Dim ListadoEliminadosTelefono As New List(Of Domain.Maintenance.Entities.PhoneMaintenance)

    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Dim ListadoEliminadosDireccion As New List(Of Domain.Maintenance.Entities.AddressMaintenance)

    ''' <summary>
    '''  Variable que maneja el listado de los Direccions agregados al control
    ''' </summary>
    Dim ListadoAgregados As New List(Of Domain.Maintenance.Entities.AddressMaintenance)

    ''' <summary>
    '''  Variable que maneja el listado de los Email agregados al control
    ''' </summary>
    Dim ListadoEliminadosEmail As New List(Of Domain.Maintenance.Entities.EmailMaintenance)

    ''' <summary>
    ''' Listado de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDistributionLine As List(Of DistributionLines)

    ''' <summary>
    ''' Listado de eliminados de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDeleteDistributionLine As List(Of DistributionLines)

    ''' <summary>
    ''' Representa la entidad de lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Dim distributionLine As DistributionLines

    ''' <summary>
    ''' Listado de las lineas de distribucion que tiene el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim listSupplierDistributionLines As List(Of SuppliersDistributionLines)

    ''' <summary>
    ''' Listado de eliminados de las lineas de distribucion que tiene el proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Dim listDeleteSupplierDistributionLines As List(Of SuppliersDistributionLines)

    Dim mode As Boolean

#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
            Exit Sub
        End If
        AssigningValues()
        Using model As New MSupplierMaintenance(Me.Tag.ToString())
            AsyncLoader(True)
            Dim Result = Await model.SaveSupplier(Supplier)
            AsyncLoader(False)
            If Result.StateResult = True Then
                Me.Supplier = Result.ObjectEmbbeded
                If Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Me.Supplier.Nit)
                ElseIf Supplier.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Supplier.ChangeTracker.State = ObjectState.Unchanged Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                End If
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Me.Deshacer()
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                Else
                    INDbteCode.Focus()
                    Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString
                End If
            End If
        End Using
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar un fabricante.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Supplier IsNot Nothing AndAlso Supplier.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = Windows.Forms.DialogResult.Yes Then
                Using Model As New MSupplierMaintenance(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim result = Await Model.DeleteSupplier(Supplier)
                    If result.StateResult = True Then
                        If Me._doc IsNot Nothing Then
                            Await Me.DeleteDocumentIndexed()
                        End If
                        'Await Me.DeleteDocumentIndexed()
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        AsyncLoader(False)

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
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        Pivote = True
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ValorSolicitado = "Codigo"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.SupplierMaintenance
            BarraBotones.PrepareToolbar(eAction.OnlyFind)
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, Botones.Aceptar, Base.Icono.Errores)
            End If
        End Set
    End Property
#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        CodeSupplier = String.Empty
        NameSupplier = String.Empty
        LastNameSupplier = String.Empty
        TimeLimitDays = 0
        StateSupplier = True
        ListDistributionLine = Nothing
        listDeleteDistributionLine = Nothing
        listDeleteSupplierDistributionLines = Nothing
        listSupplierDistributionLines = Nothing

        BarraBotones.CleanAuditBasic()

        INDckRetentionIVA.UnCheckAll()
        CodeCMMS = String.Empty
        CtrContacts.LimpiarControles()
        WebSiteSupplier = String.Empty
        INDsleCity.EditValue = Nothing
        Supplier = Nothing
        DeleteBlockedRecord()
        listAdress = Nothing
        Pivote = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDChkManufacturer.EditValue = False
        INDChkResponsibleWarranty.EditValue = False
        INDChkSeller.EditValue = False
        Me._doc = Nothing

    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()

        AsyncLoader(True)
        Using Model As New MSupplierMaintenance(CStr(Me.Tag))
            Supplier = Await Model.GetSupplier(INDbteCode.Text.Trim)
            If Not Supplier Is Nothing Then
                If Supplier.Id > 0 Then
                    Using ModelRecord As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
                        Me.BarraBotones.StatusRecordVisible = True
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(Supplier.Id))
                        With Supplier
                            'Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            CodeSupplier = .Nit
                            NameSupplier = .Names
                            LastNameSupplier = .LastNames
                            TimeLimitDays = .TimeLimitDays
                            CodeCMMS = .CodeCMMS
                            WebSiteSupplier = .WebSite
                            If .PermanentRetention = True Then
                                INDckRetentionIVA.Items(0).CheckState = Windows.Forms.CheckState.Checked
                            End If
                            If .NotIva = True Then
                                INDckRetentionIVA.Items(1).CheckState = Windows.Forms.CheckState.Checked
                            End If
                            IdCity = .IdCity

                            INDChkManufacturer.EditValue = .Manufacturer
                            INDChkResponsibleWarranty.EditValue = .ResponsibleWarranty
                            INDChkSeller.EditValue = .Seller

                            StateSupplier = .Status
                            'Control Datos de Contacto

                            CtrContacts.EstablecerDataSourceDireccion = .AddressMaintenance.ToList()
                            CtrContacts.EstablecerDataSourceTelefono = .PhoneMaintenance.ToList()
                            CtrContacts.EstablecerDataSourceEmail = .EmailMaintenance.ToList()
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Supplier.Nit)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordMaintenance With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = Supplier.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(Supplier.Id)
                        ActionsOnControls = True
                    End Using
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.Save)
                    ActionsOnControls = True
                End If
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                Me.CodeSupplier = String.Empty
            End If
        End Using
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        If INDtxtName.Text = String.Empty Then
            INDtxtName.Focus()
            Return False
        End If
        
        If INDsleCity.EditValue = Nothing Then
            INDsleCity.Focus()
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Supplier
            .Nit = CodeSupplier
            .Names = NameSupplier
            .LastNames = LastNameSupplier
            .CompleteName = NameSupplier + " " + LastNameSupplier
            .TimeLimitDays = TimeLimitDays

            .PermanentRetention = INDckRetentionIVA.Items(0).CheckState
            .NotIva = INDckRetentionIVA.Items(1).CheckState
            .CodeCMMS = CodeCMMS
            .WebSite = WebSiteSupplier
            .IdCity = IdCity

            .Manufacturer = INDChkManufacturer.EditValue
            .ResponsibleWarranty = INDChkResponsibleWarranty.EditValue
            .Seller = INDChkSeller.EditValue

            .Status = Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active

            If Pivote = False AndAlso Supplier.Id > 0 Then
                Supplier.MarkAsModified()
            End If

            ''*********Datos Contacto
            If Supplier IsNot Nothing Then
                If Object.Equals(.AddressMaintenance, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosDireccion.Count - 1
                        ListadoEliminadosDireccion.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .AddressMaintenance.Add(ListadoEliminadosDireccion.Item(i))
                        Supplier.MarkAsModified()
                        ListadoEliminadosDireccion.Clear()
                    Next
                End If
                If Object.Equals(.PhoneMaintenance, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosTelefono.Count - 1
                        ListadoEliminadosTelefono.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .PhoneMaintenance.Add(ListadoEliminadosTelefono.Item(i))
                        Supplier.MarkAsModified()
                        ListadoEliminadosTelefono.Clear()
                    Next
                End If
                If Object.Equals(.EmailMaintenance, Nothing) = False Then
                    For i As Integer = 0 To ListadoEliminadosEmail.Count - 1
                        ListadoEliminadosEmail.Item(i).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
                        .EmailMaintenance.Add(ListadoEliminadosEmail.Item(i))
                        Supplier.MarkAsModified()
                        ListadoEliminadosEmail.Clear()
                    Next
                End If
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para eliminar las direcciones agregadas al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarDireccion() Handles CtrContacts.EliminarDireccion
        If Supplier.AddressMaintenance.Count > 0 Then
            ListadoEliminadosDireccion.Add(Supplier.AddressMaintenance.Item(CtrContacts.ItemSelecionadoDireccion))
            Supplier.AddressMaintenance.RemoveAt(CtrContacts.ItemSelecionadoDireccion)
            CtrContacts.EstablecerDataSourceDireccion = Supplier.AddressMaintenance
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar los telefono agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarTelefono() Handles CtrContacts.EliminarTelefono
        If Supplier.PhoneMaintenance.Count > 0 Then
            ListadoEliminadosTelefono.Add(Supplier.PhoneMaintenance.Item(CtrContacts.ItemSelecionadoTelefono))
            Supplier.PhoneMaintenance.RemoveAt(CtrContacts.ItemSelecionadoTelefono)
            CtrContacts.EstablecerDataSourceTelefono = Supplier.PhoneMaintenance
        End If
    End Sub

    ''' <summary>
    ''' Evento para eliminar los Email agregados al control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos1_EliminarEmail() Handles CtrContacts.EliminarEmail
        If Supplier.EmailMaintenance.Count > 0 Then
            ListadoEliminadosEmail.Add(Supplier.EmailMaintenance.Item(CtrContacts.ItemSelecionadoEmail))
            Supplier.EmailMaintenance.RemoveAt(CtrContacts.ItemSelecionadoEmail)
            CtrContacts.EstablecerDataSourceEmail = Supplier.EmailMaintenance
        End If
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de direcciones  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos_InsertoNuevaDireccion() Handles CtrContacts.InsertoNuevaDireccion
        If Supplier.AddressMaintenance Is Nothing Then
            Supplier.AddressMaintenance = New Domain.Base.Entities.TrackableCollection(Of Domain.Maintenance.Entities.AddressMaintenance)
        End If
        If Supplier.AddressMaintenance.Where(Function(e) e.Addresss = CtrContacts.Direccion).Count = 0 Then
            Supplier.AddressMaintenance.Add(New Domain.Maintenance.Entities.AddressMaintenance With {.Addresss = CtrContacts.Direccion, .Synchronized = "1", .State = True})
        End If
        CtrContacts.EstablecerDataSourceDireccion = Nothing
        CtrContacts.EstablecerDataSourceDireccion = Supplier.AddressMaintenance

    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de correos  del control de datos de contacto
    ''' </summary>
    Private Sub CtrContactos_InsertoNuevoEmail() Handles CtrContacts.InsertoNuevoEmail

        If Supplier.EmailMaintenance Is Nothing Then
            Supplier.EmailMaintenance = New Domain.Base.Entities.TrackableCollection(Of Domain.Maintenance.Entities.EmailMaintenance)
        End If
        If Supplier.EmailMaintenance.Where(Function(e) e.Email = CtrContacts.Email).Count = 0 Then
            Supplier.EmailMaintenance.Add(New Domain.Maintenance.Entities.EmailMaintenance With {.Email = CtrContacts.Email, .Synchronized = "1", .State = True})
        End If
        CtrContacts.EstablecerDataSourceEmail = Nothing
        CtrContacts.EstablecerDataSourceEmail = Supplier.EmailMaintenance
    End Sub

    ''' <summary>
    ''' Evento para agregar al listado de Telefonos  del control de datos de contacto.
    ''' </summary>
    Private Sub CtrContactos_InsertoNuevoTelefono() Handles CtrContacts.InsertoNuevoTelefono
        If Supplier.PhoneMaintenance Is Nothing Then
            Supplier.PhoneMaintenance = New Domain.Base.Entities.TrackableCollection(Of Domain.Maintenance.Entities.PhoneMaintenance)
        End If
        If Supplier.PhoneMaintenance.Where(Function(e) e.Phone = CtrContacts.Telefono).Count = 0 Then
            Supplier.PhoneMaintenance.Add(New Domain.Maintenance.Entities.PhoneMaintenance With {.Phone = CtrContacts.Telefono, .PhoneType = CShort(CtrContacts.TipoTelefono), .Synchronized = "1", .State = True})
        End If
        CtrContacts.EstablecerDataSourceTelefono = Nothing
        CtrContacts.EstablecerDataSourceTelefono = Supplier.PhoneMaintenance
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        Using Model As New MBlockRecordAndSequenseMaintenance(CStr(Me.Tag))
            If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Supplier IsNot Nothing AndAlso Me.Supplier.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        'If Not String.IsNullOrEmpty(CodeSupplier) Then
        '    Dim state As Boolean
        '    Select Case StateSupplier
        '        Case CBool(eActionsStatusRecords.Active)
        '            state = True
        '        Case CBool(eActionsStatusRecords.Inactive)
        '            state = False
        '    End Select
        '    Using model As New MSupplier(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result = Await model.ChangeState(CodeSupplier, state)
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


        If Not String.IsNullOrEmpty(Me.Supplier.Nit) Then
            Try
                Using model As New MSupplierMaintenance(Me.Tag)
                    Dim state As Boolean = Status = eActionsStatusRecords.Active
                    AsyncLoader(True)
                    Dim result As ActionResult(Of Domain.Maintenance.Entities.SupplierMaintenance) = Await model.ChangeState(Me.Supplier.Nit, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Supplier = result.ObjectEmbbeded
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
    End Function

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString("FrmSupplierMaintenance" & "_IndexContent", NAME_MODULE), Me.Supplier.Nit, Me.Supplier.Names), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.Supplier.Nit & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString("FrmSupplierMaintenance" & "_IndexTitle", NAME_MODULE), Me.Supplier.Nit), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString("FrmSupplierMaintenance" & "_IndexContent", NAME_MODULE), Me.Supplier.Nit, Me.Supplier.Names)
            Me._doc.Title = String.Format(ResourceManager.GetString("FrmSupplierMaintenance" & "_IndexTitle", NAME_MODULE), Me.Supplier.Nit)
            Return Me._doc
        End If
    End Function




#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        listAdress = Nothing
        Pivote = Nothing
        _idOperativeUnit = Nothing
        Supplier = Nothing
        ListadoEliminadosTelefono = Nothing
        ListadoEliminadosDireccion = Nothing
        ListadoAgregados = Nothing
        ListadoEliminadosEmail = Nothing
        ListDistributionLine = Nothing
        listDeleteDistributionLine = Nothing
        distributionLine = Nothing
        listSupplierDistributionLines = Nothing
        listDeleteSupplierDistributionLines = Nothing
        mode = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlySupplier, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        indigo = SessionValues.Instance
        _presenter = New PSupplierMaintenance(Me)
        _presenter.Initialize()
        _presenter.GetSequense()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        LoadStatus()
        Deshacer()

    End Sub

    Protected Overrides Async Sub OnShown(e As EventArgs)
        Await Me.LayoutControls.LoadDefinitionAsync()
        MyBase.OnShown(e)
    End Sub

    ''' <summary>
    ''' Evento enter del control codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If String.IsNullOrEmpty(INDbteCode.Text.Trim()) Then
                Pivote = True
            Else
                Me.LoadControls()
            End If

        ElseIf e.KeyCode = Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSupplier_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled = True Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmSupplier_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de ciudad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmCity With {.ViewModeEditHold = True, .StartPosition = Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            _presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New Presentation.Common.FrmThirdParty With {.ViewModeEditHold = True, .StartPosition = Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            _presenter.Initialize()
        End If
    End Sub

    ''' <summary>
    ''' Evento que abre el formulario de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = Windows.Forms.FormStartPosition.CenterParent}, False)
                pop.Show()
            End Using
            _presenter.Initialize()
        End If
    End Sub



    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceContactData_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceContactData.CloseUp
        If e.CloseMode = PopupCloseMode.Cancel Then
            INDtxtWebSite.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar las teclas de enter y F4
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceContactData_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceContactData.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceContactData.ShowPopup()
        End If
    End Sub

#End Region

#Region "Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
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
        CleanControls()
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
        mode = True
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
        mode = False
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento de la barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit)
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MaintenanceSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MaintenanceSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class