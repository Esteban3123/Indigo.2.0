'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Rafael Patiño
' Created          : 2014-10-10
'
' Last Modified By : Rafael Patiño
' Last Modified On : 2014-10-10
' Description      : Vista del del frontal de pago parciales
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Glosas.MVP

#End Region

''' <summary>
''' Vista del frontal de pago parciales
''' </summary>

Public Class FrmPartialPayment
    Implements IPartialPayments


#Region "property"

    Public Property Comment As String Implements IPartialPayments.Comment
        Get
            Return Me.INDmeComment.Text
        End Get
        Set(value As String)
            Me.INDmeComment.Text = value
        End Set
    End Property

    Public Property Consecutive As Long Implements IPartialPayments.Consecutive
        Get
            Return Me.INDbteConsecutive.EditValue
        End Get
        Set(value As Long)
            Me.INDbteConsecutive.EditValue = value
        End Set
    End Property

    Public Property DateDocument As Date Implements IPartialPayments.DateDocument
        Get
            Return Me.INDdeDocumentDate.EditValue
        End Get
        Set(value As Date)
            Me.INDdeDocumentDate.EditValue = value
        End Set
    End Property

    Public Property DateRadicate As Date Implements IPartialPayments.DateRadicate
        Get
            Return Me.INDlblRadicateDate.Text
        End Get
        Set(value As Date)
            Me.INDlblRadicateDate.Text = value
        End Set
    End Property


    Public Property Nit As String Implements IPartialPayments.Nit
        Get
            Return Me.INDbteNit.EditValue
        End Get
        Set(value As String)
            Me.INDbteNit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el estado del oficio
    ''' </summary>
    Public Property StatusDocument As String Implements IPartialPayments.StatusDocument
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As String)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

#End Region

#Region "Fields"

    Const NAME_MODULE As String = "Glosas"
    ''' <summary>
    ''' objeto de pagos parciales
    ''' </summary>
    Dim Objeto As PartialPaymentsC

    ''' <summary>
    ''' Objeto que contiene el tercero
    ''' </summary>
    Dim CustomerTmp As Domain.Entities.Customer

    ''' <summary>
    ''' Variable que se utiliza para instanciar el modelo-
    ''' </summary>
    Dim Model As MPartialPayments

    ''' <summary>
    ''' Variable que se utiliza para instanciar el presentador
    ''' </summary>
    Dim Presenter As PPartialPayments

    ''' <summary>
    ''' Variable que contiene los valores de session
    ''' </summary>
    Dim IndigoSessionValues As SessionValues

    ''' <summary>
    ''' Variable para almacenar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Dim _ListPartialPaymentsD As List(Of PartialPaymentsD)

    ''' <summary>
    ''' Almacena el nombre del control que hizo el llamado al metodo buscar
    ''' </summary>
    Private _openFindSenser As String

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Bandera para saber si el registro esta bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagBlockRecord As Boolean

    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceCustomers As XPInstantFeedbackSource
    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Parametros de Glosas
    ''' </summary>
    Private _parameterGlosas As TimeParameters

#End Region

#Region "CRUD Operations"

    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        If SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException
    End Sub

    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Try
            AssigningValues()
            AsyncLoader(True)
            Dim result As ActionResult(Of PartialPaymentsC) = Await Model.SavePartialPaymentsC(Objeto)
            If result.StateResult = True Then
                Objeto.Id = result.MessageResult(0).ToString
                INDbteConsecutive.Text = result.MessageResult(1).ToString
                If Objeto.State = 1 Then
                    'actualizamos el objeto
                    If INDbteConsecutive.Text IsNot Nothing Then
                        Dim objResult As ActionResult(Of PartialPaymentsC) = Await Model.GetPartialPayments(INDbteConsecutive.Text)
                        If objResult.StateResult = True Then
                            Objeto = objResult.ObjectEmbbeded
                        Else
                            If objResult.MessageResult IsNot Nothing AndAlso objResult.MessageResult.Count > 0 Then
                                Dim strMessage As New System.Text.StringBuilder()
                                For Each item As String In objResult.MessageResult
                                    strMessage.AppendLine(item.ToString())
                                Next
                                Mensaje(EeventViewerImages.Advertencia) = strMessage.ToString
                            End If
                        End If
                        Me._ListPartialPaymentsD = (From e In Objeto.PartialPaymentsD Select e).ToList()
                        Me.INDgcPartialPaymentsD.DataSource = _ListPartialPaymentsD
                        Me.INDgcPartialPaymentsD.RefreshDataSource()
                    End If
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                ElseIf Objeto.State = 2 Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesConfirmadoCorrectamente)
                End If
                'Se indexa la información
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                Else
                    If result.MessageResult IsNot Nothing Then
                        For Each itemMensaje As String In result.MessageResult
                            Mensaje(EeventViewerImages.Informacion) = itemMensaje
                        Next
                    End If
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Informacion) = ex.Message.ToString
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
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
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.RibbonPageProcesos.Visible = False
    End Sub

    ''' <summary>
    ''' click confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If Objeto IsNot Nothing Then
            Objeto.State = 2 'confirmamos
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me.ConfirmRadicate()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Click en Anular OFicio
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If Objeto IsNot Nothing Then
            Objeto.State = 4 'confirmamos
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaAnular, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                InvalidatePayments()
            End If
        End If
    End Sub


    ''' <summary>
    ''' Metodo para anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub InvalidatePayments()
        Dim StateTmp As String = Objeto.State
        AsyncLoader(True)
        Dim result As ActionResult(Of PartialPaymentsC) = Await Model.InvalidatePaymentsC(Objeto)
        AsyncLoader(False)
        If result.StateResult = True Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesAnuladoCorrectamente)
            SearchMode = False
            Deshacer()
        Else
            If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
            Else
                Objeto.State = StateTmp
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador)
            End If
        End If
    End Sub


    Private Async Sub ConfirmRadicate()
        Try
            If Me.Objeto IsNot Nothing AndAlso Me.Objeto.PartialPaymentsD.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
                Exit Sub
            End If
            'Dim serverDate = Await Model.GetServerDate
            With Me.Objeto
                .ConfirmUser = Me.indigo.UserIndigoId
            End With
            Me.AsyncLoader(True)
            Dim result = Await Me.Model.ConfirmPartialPaymentsC(Me.Objeto)
            Me.AsyncLoader(False)
            'actualizamos el objeto, la lista de facturas
            If INDbteConsecutive.Text IsNot Nothing Then
                Objeto = Nothing
                AsyncLoader(True)
                Dim resultObj As ActionResult(Of PartialPaymentsC) = Await Model.GetPartialPayments(INDbteConsecutive.Text)
                If resultObj.StateResult = True Then
                    Objeto = resultObj.ObjectEmbbeded
                Else
                    If resultObj.MessageResult IsNot Nothing AndAlso resultObj.MessageResult.Count > 0 Then
                        Dim strMessage As New System.Text.StringBuilder()
                        For Each item As String In resultObj.MessageResult
                            strMessage.AppendLine(item.ToString())
                        Next
                        Mensaje(EeventViewerImages.Advertencia) = strMessage.ToString
                    End If
                End If
                AsyncLoader(False)
                Me._ListPartialPaymentsD = (From e In Objeto.PartialPaymentsD Select e).ToList()
                Me.INDgcPartialPaymentsD.DataSource = _ListPartialPaymentsD
                Me.INDgcPartialPaymentsD.RefreshDataSource()
            End If
            If result.StateResult = True Then
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    Dim strMessage As New System.Text.StringBuilder()
                    For Each item As String In result.MessageResult
                        strMessage.AppendLine(item.ToString())
                    Next
                    Mensaje(EeventViewerImages.Informacion) = strMessage.ToString
                End If
                SearchMode = False
                Me.Deshacer()
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    If result.MessageResult(0).ToString = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Dim strMessage As New System.Text.StringBuilder()
                        For Each item As String In result.MessageResult
                            strMessage.AppendLine(item.ToString())
                        Next
                        Mensaje(EeventViewerImages.Advertencia) = strMessage.ToString
                    End If
                End If
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
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
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Throw New NotImplementedException
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
        'CleanControls()
        LoadControls()
    End Sub


    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub


#End Region

#Region "Metodos"


    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer

        Dim detailString As String = " [ "
        For Each d As Domain.Entities.PartialPaymentsD In Me._ListPartialPaymentsD
            If detailString.Length = 3 Then
                detailString &= String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.PatientCode.Trim(), d.PatientName.Trim().ToLower())
            Else
                detailString &= " - " & String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.PatientCode.Trim(), d.PatientName.Trim().ToLower())
            End If
        Next
        detailString &= " ]"

        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Objeto.Customer.Nit.Trim(), Me.Objeto.Customer.Name.Trim().ToLower(), Me.Objeto.RadicatedConsecutive.ToString, detailString), .CreationDate = dateServer, .CreationUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity =  "$#" & Me.Tag & "_" & Me.Objeto.RadicatedConsecutive & "#$", .IdForm = Me.Tag, .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Objeto.RadicatedConsecutive), .Update = dateServer, .UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.Objeto.Customer.Nit.Trim(), Me.Objeto.Customer.Name.Trim().ToLower(), Me.Objeto.RadicatedConsecutive.ToString, detailString)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.Objeto.RadicatedConsecutive)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StateInvalidate"), .StatusColor = System.Drawing.Color.OrangeRed})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        Me.BarraBotones.StatusRecord = "1" 'Sin confirmar
        Objeto = Nothing
        CustomerTmp = Nothing
        INDbteConsecutive.Text = String.Empty
        Nit = Nothing
        INDbteNit.EditValue = Nothing
        INDbteNit.DisplayNullText = String.Empty
        INDdeDocumentDate.EditValue = Date.Now
        Dim d As DateTime = GetServerDate()
        INDlblRadicateDate.Tag = d
        INDlblRadicateDate.Text = d.ToString("D", indigo.Culture)
        INDmeComment.Text = String.Empty
        Me.INDtxtValuePayments.EditValue = 0
        _ListPartialPaymentsD = New List(Of PartialPaymentsD)
        Me.INDgcPartialPaymentsD.DataSource = _ListPartialPaymentsD
        Me.INDgcPartialPaymentsD.RefreshDataSource()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        BlockedRecord()
        Me.BarraBotones.StatusRecordVisible = False
        '  Me.IndigoGridControl1.RefreshGrid(Me.INDgcPartialPaymentsD)
        Me.INDsleSearchInvoice.Properties.DataSource = Nothing
        Me.INDbteConsecutive.Focus()
        Me.BarraBotones.CleanAuditBasic()
        ActionsOnControls = False
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(ByVal value As Boolean)
            INDbteConsecutive.Enabled = Not value
            INDbteNit.Enabled = value
            INDlblRadicateDate.Enabled = value
            INDmeComment.Enabled = value
            INDsleSearchInvoice.Enabled = value
            INDtxtValuePayments.Enabled = value
            INDgcPartialPaymentsD.Enabled = value
            INDbtnAddInvoice.Enabled = value
            INDpceAddInvoice.Enabled = value
            INDdeDocumentDate.Enabled = value
            If value = True Then
                INDbteNit.Focus()
            Else
                INDbteConsecutive.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Elimina el Registro Bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Async Sub BlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
        Me.BarraBotones.EnableBarItems()
        Me.FlagBlockRecord = False
    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Objeto
            .DocumentDate = INDdeDocumentDate.EditValue
            .CustomerId = CustomerTmp.Id
            .State = 1
            .Comment = INDmeComment.Text
            For Each item As PartialPaymentsD In _ListPartialPaymentsD
                If item.ChangeTracker.State = ObjectState.Added Then
                    .PartialPaymentsD.Add(item)
                End If
            Next
        End With
    End Sub

    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Select Case Me._openFindSenser.ToUpper
            Case Me.INDbteConsecutive.Name.ToUpper() 'Abre el frontal para buscar consecutivo
                AbrirBusquedaConsecutive()
                'Case Me.INDbteNit.Name.ToUpper() 'Abre el frontal para buscar nits
                '    AbrirBusquedaCustomers()
            Case Else 'Si esta sobre un control en donde no aplica el metodo buscar
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoAplicaBuscar, Eform.Comunes)
        End Select
    End Sub

    ''' <summary>
    ''' Abrirs the busqueda consecutive.
    ''' </summary>
    Public Sub AbrirBusquedaConsecutive()
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValueObjection
        With FormSearchObjects
            .ValorSolicitado = "RadicatedConsecutive"
            .ListaColumnas = {New ColumnInfo With {.Caption = "Entidad", .FieldName = "NitName"}, New ColumnInfo With {.Caption = "N° Factura", .FieldName = "InvoiceNumber"}, New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate"}, New ColumnInfo With {.Caption = "N° Consecutivo", .FieldName = "RadicatedConsecutive"}, New ColumnInfo With {.Caption = "Estado", .FieldName = "State"}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInvoicePartialPayments
            BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            .FormParent = Me
            .ShowSearch(False)
        End With
        SearchMode = True
    End Sub

    ' ''' <summary>
    ' ''' Abrirs the busqueda de terceros
    ' ''' </summary>
    'Public Sub AbrirBusquedaCustomers()
    '    FormSearchObjects = New FrmBusqueda
    '    AddHandler FormSearchObjects.ReturnValueWithList, AddressOf ReturnValueCustomers
    '    'lista de string de solicitados
    '    Dim listSolicitado As List(Of String) = New List(Of String)
    '    listSolicitado.Add("Nit")
    '    listSolicitado.Add("Name")
    '    listSolicitado.Add("Id")
    '    With FormSearchObjects
    '        .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit"}, New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"}}.ToList()
    '        .ListadoSolicitud = listSolicitado
    '        .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
    '        .FormParent = Me
    '        .ShowSearch(False)
    '    End With
    'End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValueObjection(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteConsecutive.Text = ReturnValue
        BlockedRecord()
        BarraBotones.RibbonPagEform.Visible = True
        LoadControls()
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    'Private Sub ReturnValueCustomers(ByVal ReturnValue As String, ByVal ReturnObject As Object, ByVal ReturnList As List(Of String))
    '    'variable de la lista de string devueltos
    '    Dim listDevuelto As List(Of String)
    '    listDevuelto = ReturnList
    '    Dim ctomerTmp = CType(ReturnObject, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
    '    Dim ctomer = CType(ctomerTmp.OriginalRow, Infrastructure.Data.Xpo.GlosasRepository.GlosasCustomerXpo)
    '    Me.CustomerTmp = New Domain.Entities.Customer With {.Id = ctomer.Id, .Nit = ctomer.Nit.ToString().Trim(), .Name = ctomer.Name.ToString().Trim(), .State = ctomer.State}
    '    If (FormSearchObjects.ListadoDevolucion.Count > 0) Then
    '        If Me.CustomerTmp IsNot Nothing Then
    '            Using model As New MPartialPayments(Me.Tag)
    '                Me.INDsleSearchInvoice.Properties.DataSource = model.ListXpoInvoicesByPartialPayments(Me.CustomerTmp.Nit.Trim())
    '            End Using
    '        Else
    '            Me.INDbteNit.Focus()
    '        End If
    '        INDbteNit.Text = FormSearchObjects.ListadoDevolucion.Item(0)
    '    End If
    'End Sub


    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Me.BarraBotones.StatusRecordVisible = True
        'Dim d As DateTime = GetServerDate()
        'INDlblRadicateDate.Tag = d
        'INDlblRadicateDate.Text = d.ToString("D", indigo.Culture)
        ActionsOnControls = True
        INDbteConsecutive.Enabled = False
        If INDbteConsecutive.Text <> String.Empty Then
            AsyncLoader(True)
            Dim result As ActionResult(Of PartialPaymentsC) = Await Model.GetPartialPayments(INDbteConsecutive.Text)
            If result.StateResult = True Then
                Objeto = result.ObjectEmbbeded
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                    Dim strMessage As New System.Text.StringBuilder()
                    For Each item As String In result.MessageResult
                        strMessage.AppendLine(item.ToString())
                    Next
                    Mensaje(EeventViewerImages.Advertencia) = strMessage.ToString
                End If
            End If
            AsyncLoader(False)
        End If
        If Not Objeto Is Nothing Then
            If Objeto.Id > 0 Then
                Dim result As New Domain.Entities.BlockRecord
                result = Await Model.GetBlockRecord(Me.Tag, Objeto.Id)
                With Objeto
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.INDsleSearchInvoice.Properties.DataSource = Model.ListXpoInvoicesByPartialPayments(.Customer.Nit)
                    INDbteNit.EditValue = .CustomerId
                    INDbteNit.DisplayNullText = .Customer.Nit & " - " & .Customer.Name
                    INDdeDocumentDate.EditValue = .DocumentDate
                    Me.CustomerTmp = .Customer
                    INDmeComment.Text = .Comment
                    Me.BarraBotones.SetDocuments(Me.Objeto.Id)
                    Me._ListPartialPaymentsD = (From e In Objeto.PartialPaymentsD Select e).ToList()
                    Me.INDgcPartialPaymentsD.DataSource = _ListPartialPaymentsD
                    Me.INDgcPartialPaymentsD.RefreshDataSource()
                    Me.StatusDocument = .State
                    If .State = "2" Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                    ElseIf .State = "4" Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcessWithPrint)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = False
                    End If
                End With
                Me.INDgcPartialPaymentsD.RefreshDataSource()
                'envio a bloquear el registro
                If result IsNot Nothing Then
                    If result.Id = 0 Then
                        '  LogicaBotonActualizar(True)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Objeto.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                        FlagBlockRecord = False
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        Me.FlagBlockRecord = True
                    End If
                End If

                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Objeto.RadicatedConsecutive)
            Else

                If INDbteConsecutive.Text <> String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ConsecutivoObjecionNoExiste, RegisterObjection)
                    INDbteConsecutive.Text = String.Empty
                    ActionsOnControls = False
                    INDbteConsecutive.Enabled = True
                    INDbteConsecutive.Focus()
                Else
                    INDbteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
                    Objeto = New PartialPaymentsC
                    Me.BarraBotones.PrepareToolbar(eAction.Save)
                End If

            End If
        Else
            INDbteConsecutive.Text = obtenerRecurso(LabelNuevo, RecepcionObjeciones)
            Objeto = New PartialPaymentsC
            Me.BarraBotones.PrepareToolbar(eAction.Save)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
        End If
    End Sub


    ''' <summary>
    ''' Elimina Item de la rejilla de detalle de objeciones
    ''' </summary>
    Private Async Sub DeleteItem()
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        If Me.INDgvPartialPaymentsD.SelectedRowsCount > 1 Then '
            If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Dim lista = New List(Of PartialPaymentsD)
                Dim listaEliminar = New List(Of PartialPaymentsD)
                For Each item As Integer In INDgvPartialPaymentsD.GetSelectedRows()
                    If item > -1 Then
                        Dim data As PartialPaymentsD = TryCast(INDgvPartialPaymentsD.GetRow(item), Domain.Entities.PartialPaymentsD)
                        If data IsNot Nothing Then
                            If Objeto.State <> "2" And Objeto.State <> "4" Then
                                lista.Add(data)
                                listaEliminar.Add(data)
                            End If
                        End If
                    End If
                Next

                For Each Data As PartialPaymentsD In lista 'limpiamos los iteam que aun no han sidio guardados
                    If Data.Id = 0 Then
                        If listaEliminar.Contains(Data) Then
                            Me._ListPartialPaymentsD.Remove(Data)
                            Objeto.PartialPaymentsD.Remove(Data)
                            listaEliminar.Remove(Data)
                        End If
                    End If
                Next
                Me.INDgcPartialPaymentsD.RefreshDataSource()

                If listaEliminar.Count > 0 Then
                    Dim result As ActionResult
                    AsyncLoader(True)

                    result = Await Model.DeletePartialPaymentsD(listaEliminar)
                    Dim objResult As ActionResult(Of PartialPaymentsC) = Await Model.GetPartialPayments(INDbteConsecutive.Text)
                    If objResult.StateResult = True Then
                        Objeto = objResult.ObjectEmbbeded
                        Me.INDgcPartialPaymentsD.DataSource = (From e In Objeto.PartialPaymentsD Select e).ToList()
                    Else
                        If objResult.MessageResult IsNot Nothing AndAlso objResult.MessageResult.Count > 0 Then
                            Dim strMessage As New System.Text.StringBuilder()
                            For Each item As String In objResult.MessageResult
                                strMessage.AppendLine(item.ToString())
                            Next
                            Mensaje(EeventViewerImages.Advertencia) = strMessage.ToString
                        End If
                    End If
                    AsyncLoader(False)
                    Me.INDgcPartialPaymentsD.RefreshDataSource()
                    If result.StateResult = True Then
                        For Each item As PartialPaymentsD In listaEliminar
                            Me._ListPartialPaymentsD.Remove(item)
                            Objeto.PartialPaymentsD.Remove(item)
                        Next
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            Dim strmessage As New StringBuilder
                            For Each item In result.MessageResult
                                strmessage.AppendLine(item.ToString)
                            Next
                            Mensaje(EeventViewerImages.Advertencia) = strmessage.ToString
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador, Eform.Comunes)
                        End If
                    End If
                End If

            End If
        Else
            Dim obj As PartialPaymentsD = Me.INDgvPartialPaymentsD.GetRow(Me.INDgvPartialPaymentsD.FocusedRowHandle)
            If Objeto.State <> "2" And Objeto.State <> "4" Then  'no se pueden eliminar facturas de oficio confirmado o anulados
                If (obj.Id = 0) Then
                    Dim found = Me._ListPartialPaymentsD.Find(Function(value As PartialPaymentsD) value.InvoiceNumber = obj.InvoiceNumber)
                    Me._ListPartialPaymentsD.Remove(found)
                    Objeto.PartialPaymentsD.Remove(found)
                Else
                    DeleteItemDetail(CType(obj, PartialPaymentsD))
                End If
                Me.INDgcPartialPaymentsD.RefreshDataSource()
                If Me._ListPartialPaymentsD.Count = 0 Or Me._ListPartialPaymentsD.TrueForAll(Function(x) x.ChangeTracker.State = ObjectState.Deleted) Then
                    Me.INDbteNit.Enabled = True
                End If
            End If
        End If
    End Sub


    ''' <summary>
    ''' METODO: Eliminar De la Columna de Acciones
    ''' </summary>
    Public Async Sub DeleteItemDetail(ByVal _partialPaymentsD As PartialPaymentsD)
        If _partialPaymentsD IsNot Nothing Then
            If _partialPaymentsD.Id > 0 Then
                If Objeto.State = 4 Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeRealizarAccionAnulado, RecepcionObjeciones)
                    Exit Sub
                End If
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim result As ActionResult
                    AsyncLoader(True)
                    Dim ListDeleteService As New List(Of PartialPaymentsD)
                    ListDeleteService.Add(_partialPaymentsD)
                    result = Await Model.DeletePartialPaymentsD(ListDeleteService)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        Me._ListPartialPaymentsD.Remove(_partialPaymentsD)
                        Me.INDgcPartialPaymentsD.RefreshDataSource()

                        Dim objResult As ActionResult(Of PartialPaymentsC) = Await Model.GetPartialPayments(INDbteConsecutive.Text)
                        If objResult.StateResult = True Then
                            Objeto = objResult.ObjectEmbbeded
                            Me.INDgcPartialPaymentsD.DataSource = (From e In Objeto.PartialPaymentsD Select e).ToList()
                        Else
                            If objResult.MessageResult IsNot Nothing AndAlso objResult.MessageResult.Count > 0 Then
                                Dim strMessage As New System.Text.StringBuilder()
                                For Each item As String In objResult.MessageResult
                                    strMessage.AppendLine(item.ToString())
                                Next
                                Mensaje(EeventViewerImages.Advertencia) = strMessage.ToString
                            End If
                        End If

                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeEliminarPorMovimiento, Eform.RecepcionObjeciones)
                    End If
                    AsyncLoader(False)
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(NoPuedeEliminarPorMovimiento)
            End If
        Else
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesSeleccioneRegistroEliminar)
        End If
    End Sub



    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomers()
        Using msearch As New MBusqueda
            DatasourceCustomers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Customers)
            INDbteNit.Datasource = DatasourceCustomers
        End Using
    End Sub


    Private Async Function INDbteNit_KeyDown(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        CustomerTmp = Await Model.GetCustomerByNit(nit)
        If CustomerTmp IsNot Nothing AndAlso CustomerTmp.Id > 0 Then
            INDdeDocumentDate.Focus()
        Else
            Me.INDbteNit.DisplayNullText = String.Empty
            Me.INDbteNit.EditValue = Nothing
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ClienteNoExite, Eform.Conciliation)
            INDbteNit.Focus()
        End If
        Return CustomerTmp
    End Function

    ''' <summary>
    ''' Logica para cargar los controles dependiendo si maneja o no decimales.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadParameters() As Task
        Using model As New MTimeParameters(Me.Tag)
            Me._parameterGlosas = Await model.GetTimeParameters("0", Me._idOperativeUnit)
            If Me._parameterGlosas Is Nothing OrElse Me._parameterGlosas.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de glosas para la unidad operativa seleccionada"
                Exit Function
            ElseIf Me._parameterGlosas.ManageDecimals = False Then 'No maneja decimales.
                'Grid cabecera
                For Each item In INDgvPartialPaymentsD.Columns
                    If item.DisplayFormat.FormatString = "C" OrElse item.DisplayFormat.FormatString = "c" Then
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                'Mascara
                INDRepSpinEValorPago.EditMask = "N00"
            Else
                INDRepSpinEValorPago.EditMask = String.Empty
            End If
        End Using
    End Function

#End Region

#Region "Eventos"

#Region "FormClosing"

#End Region

#Region "Constructor"
    ''' <summary>
    ''' Initialize
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _openFindSenser = "INDbteConsecutive".ToUpper()
    End Sub
#End Region

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Objeto = Nothing
        CustomerTmp = Nothing
        Model = Nothing
        Presenter = Nothing
        _ListPartialPaymentsD = Nothing
        _openFindSenser = Nothing
        record = Nothing
        FlagBlockRecord = Nothing
        DatasourceCustomers = Nothing
        SearchMode = Nothing
    End Sub

    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmPartialPayment_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me._funct = AddressOf GenerateDoc

        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MPartialPayments(Me.Tag)
        Me.indigo = SessionValues.Instance
        '******************************'
        Me.INDbteNit.FuncQueryOnKeyEnterPressed = AddressOf Me.INDbteNit_KeyDown
        Me.INDbteNit.View.OptionsView.ShowGroupPanel = False
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecordEnabled = False

        IndigoGridView1.MoreInfoColunmns(INDgvPartialPaymentsD)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvPartialPaymentsD, ListActions)
        IndigoGridControl1.RefreshGrid(INDgcPartialPaymentsD)

        'Me.IndigoGridControl1.SetHoldSize(Me.INDgcPartialPaymentsD, True)
        ' Me.IndigoGridControl1.RefreshGrid(Me.INDgcPartialPaymentsD)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Eliminar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        Presenter = New PPartialPayments(Me)
        Deshacer()
        SearchMode = False
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await LoadParameters()
    End Sub

    Private Sub FrmPartialPayment_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        If INDbteConsecutive.Enabled Then
            INDbteConsecutive.Focus()
        End If
    End Sub
#End Region

#Region "EditValueChanged"

    Private Async Sub INDbteNit_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDbteNit.EditValueChanged
        If Me.INDbteNit.EditValue IsNot Nothing Then
            Me.CustomerTmp = Await Model.GetCustomerById(Me.INDbteNit.EditValue)

        End If
    End Sub

    Private Sub INDsleSearchInvoice_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSearchInvoice.EditValueChanged
        Me.INDbtnAddInvoice.Enabled = True
    End Sub

#End Region

#Region "QueryPopup"
    Private Sub INDsleSearchInvoice_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSearchInvoice.QueryPopUp
        If CustomerTmp IsNot Nothing AndAlso CustomerTmp.Id > 0 Then
            Me.INDsleSearchInvoice.Properties.DataSource = Model.ListXpoInvoicesByPartialPayments(Me.CustomerTmp.Nit)
        End If
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteNit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDbteNit.QueryPopUp
        If INDbteNit.Datasource Is Nothing Then
            LoadXpoCustomers()
        End If
    End Sub
#End Region

#Region "GotFocus"
    ''' <summary>
    ''' Pone la variable _openFindSender en vacio para evitar
    ''' abrir el formulario de busqueda en un control que no aplique la funcionalidad
    ''' </summary>
    Private Sub OpenFindSender_LostFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.LostFocus
        Me._openFindSenser = String.Empty
    End Sub

    ''' <summary>
    ''' Asigna el nombre del sender a la variable _openFindSender para habilitar
    ''' la funcionalidad de buscar en el control que corresponda
    ''' </summary>
    Private Sub OpenFindSender_GotFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.GotFocus
        Me._openFindSenser = CType(sender, System.Windows.Forms.Control).Name.ToUpper()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub INDbteConsecutive_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbteConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
                Exit Sub
            End If
            ' Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
            Me.BarraBotones.StatusRecord = "1" 'Sin confirmar
            LoadControls()
            INDbteConsecutive.Enabled = False
        ElseIf e.KeyCode = Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub
    ''' <summary>
    ''' presion sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupContainerEdit1_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceAddInvoice.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Or e.KeyCode = Keys.F4 Then
            INDpceAddInvoice.ShowPopup()
            INDsleSearchInvoice.Focus()
        End If
    End Sub
#End Region

#Region "Popup"

#End Region

#Region "Click"


    Private Async Sub INDbtnAddInvoice_Click(sender As Object, e As EventArgs) Handles INDbtnAddInvoice.Click
        Me.INDbtnAddInvoice.Enabled = False
        Try
            If Me.INDtxtValuePayments.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPartialPayments_NoValuePayments", "Glosas")
                Me.INDtxtValuePayments.Focus()
                Exit Sub
            End If

            If Me.INDsleSearchInvoice.EditValue IsNot Nothing AndAlso Not Me.INDsleSearchInvoice.Text.Trim().Equals(String.Empty) Then
                If (From p In Me._ListPartialPaymentsD Where p.InvoiceNumber.Equals(Me.INDsleSearchInvoice.Text.Trim()) Select p).Any Then
                    'Mensaje de que ya existe la factura!
                    Me.Mensaje(EeventViewerImages.Advertencia) = "La factura ya esta en la lista"
                    Me.INDsleSearchInvoice.EditValue = Nothing
                    Exit Sub
                Else
                    AsyncLoader(True)
                    Dim itemInvoice = Await Model.GetInvoiceByNumber(Me.CustomerTmp.Nit.Trim(), Me.INDsleSearchInvoice.Text.Trim(), Nothing)
                    AsyncLoader(False)
                    If itemInvoice IsNot Nothing AndAlso itemInvoice.Id > 0 Then
                        If Me.INDtxtValuePayments.EditValue > itemInvoice.BalanceGlosa Then
                            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPartialPayments_ValuePaymentsMay", "Glosas")
                            Me.INDtxtValuePayments.Focus()
                            Exit Sub
                        End If
                        Dim _TmpObjectionsReceptionD As New PartialPaymentsD
                        With _TmpObjectionsReceptionD
                            .PortfolioGlosaId = itemInvoice.Id
                            .InvoiceNumber = itemInvoice.InvoiceNumber.Trim
                            .InvoiceDate = itemInvoice.InvoiceDate
                            .RadicatedDate = itemInvoice.RadicatedDate
                            .RadicatedNumber = itemInvoice.RadicatedNumber
                            .PatientCode = If(itemInvoice.PatientCode Is Nothing, String.Empty, itemInvoice.PatientCode.Trim)
                            .ContractCode = If(itemInvoice.ContractCode Is Nothing, String.Empty, itemInvoice.ContractCode.Trim)
                            .PatientName = If(itemInvoice.PatientName Is Nothing, String.Empty, itemInvoice.PatientName.Trim)
                            .State = "1"
                            .ValuePendingConciliation = itemInvoice.BalanceGlosa
                            .ValuePayments = FormatCurrency(Me.INDtxtValuePayments.EditValue, 2)
                        End With
                        Me._ListPartialPaymentsD.Add(_TmpObjectionsReceptionD)
                        Me.INDgcPartialPaymentsD.DataSource = _ListPartialPaymentsD
                        Me.INDgcPartialPaymentsD.RefreshDataSource()
                        Me.INDsleSearchInvoice.EditValue = Nothing
                        If Me.Objeto IsNot Nothing AndAlso Me.Objeto.Id > 0 Then
                            Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                        End If
                    Else
                        'La factura no existe
                        Me.Mensaje(EeventViewerImages.Advertencia) = "La factura ya esta en la lista"
                    End If
                End If
                Me.INDsleSearchInvoice.Focus()
            Else
                Me.INDsleSearchInvoice.Focus()
                Me.INDsleSearchInvoice.ShowPopup()
            End If
            Me.INDtxtValuePayments.EditValue = 0
            Me.INDbtnAddInvoice.Enabled = True
        Catch ex As Exception
            Me.AsyncLoader(False)
            Me.INDbtnAddInvoice.Enabled = True
            Throw ex
        End Try
        Me.INDbtnAddInvoice.Enabled = True
    End Sub

    'Private Sub INDbtnEliminar_Click(sender As Object, e As EventArgs) Handles INDbtnEliminar.Click
    '    If FlagBlockRecord = True Then
    '        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
    '    Else
    '        DeleteItem()
    '    End If
    'End Sub
#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Dim action As DevExpress.XtraBars.BarButtonItem = DirectCast(sender, DevExpress.XtraBars.BarButtonItem)
        Select Case (action.Tag.ToString)
            Case Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                DeleteItem()
        End Select
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
        DeleteItem()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDbteConsecutive_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteConsecutive.ButtonClick
        AbrirBusquedaConsecutive()
    End Sub
#End Region

#Region "ShowingEditor"

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgvPartialPaymentsD_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgvPartialPaymentsD.PopupMenuShowing
        If e.Menu Is Nothing Then
            e.Menu = New DevExpress.XtraGrid.Menu.GridViewMenu(sender)
        End If

        If Objeto Is Nothing Then
            Exit Sub
        End If

        If Objeto.State = 1 Or Objeto.State Is Nothing Then
            e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(MenuEliminar, RecepcionObjeciones), AddressOf DeleteItem, My.Resources.eliminarLineaAzul))
        End If
    End Sub
#End Region

#Region "DoubleClick"

#End Region

#Region "ClickBack"


#End Region

#Region "IdEntityLoaded"

#End Region

#Region "Activated"

#End Region

#Region "OpenFormButtonClick"
    Private Sub INDbteNit_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDbteNit.OpenFormButtonClick
        OpenForm(503, Nothing, True)
        LoadXpoCustomers()
    End Sub
#End Region

#Region "CellValueChanged"
    ''' <summary>
    ''' controla qque el valor de pago no sea superior al valor pendiente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvPartialPaymentsD_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles INDgvPartialPaymentsD.CellValueChanged
        If e.Column.FieldName = "ValuePayments" Then
            Dim ObjD As PartialPaymentsD = CType(Me.INDgvPartialPaymentsD.GetFocusedRow, PartialPaymentsD)
            If ObjD IsNot Nothing Then
                If e.Value > ObjD.ValuePendingConciliation Then
                    ObjD.ValuePayments = 0
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FrmPartialPayments_ValuePaymentsMay", "Glosas")
                End If
            End If
        End If
    End Sub
#End Region


#End Region

  

End Class