'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/03/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Payments.MVP
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
Imports System.Text
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class FrmAccountPayableTransfer
    Implements IAccountPayableTransfer, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IAccountPayableTransfer.Code
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
    ''' Obtiene o establece la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitSourceId As Integer? Implements IAccountPayableTransfer.FilingUnitSourceId
        Get
            Return INDsleFilingUnitSource.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnitSource.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitSourceXpo As List(Of FilingUnit) Implements IAccountPayableTransfer.FilingUnitSourceXpo
        Get
            Return INDsleFilingUnitSource.Properties.DataSource
        End Get
        Set(value As List(Of FilingUnit))
            INDsleFilingUnitSource.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la unidad de radicacion destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitTargetId As Integer? Implements IAccountPayableTransfer.FilingUnitTargetId
        Get
            Return INDsleFilingUnitTarget.EditValue
        End Get
        Set(value As Integer?)
            INDsleFilingUnitTarget.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la unidad de radicacion destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FilingUnitTargetXpo As DevExpress.Xpo.XPCollection Implements IAccountPayableTransfer.FilingUnitTargetXpo
        Get
            Return INDsleFilingUnitTarget.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleFilingUnitTarget.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAccountPayableTransfer.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IAccountPayableTransfer.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As PaymentsSecuence Implements IAccountPayableTransfer.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PaymentsSecuence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequence.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Bandera para search de unidad radicacion (True=Consulta, False=No Consulta)
    ''' </summary>
    ''' <remarks></remarks>
    Private banFilingUnit As Boolean = True

    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Dim Presenter As PAccountPayableTransfer

    ''' <summary>
    ''' Variable que contiene la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim accountPayableTransfer As AccountPayableTransfer

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPayments

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Payments"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Listado de eliminados de los detalles de traslado de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteAccountPayableTransferDetail As List(Of AccountPayableTransferDetail)

    ''' <summary>
    ''' Lista todas las facturas que esten asociadas a la cxp
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListBillsPopup As List(Of AccountPayable)

    ''' <summary>
    ''' Representa el listado de xpcollection para cuentas por pagar
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListXpCollectionAccountPayable As XPCollection
    ''' <summary>
    ''' lista de reembolsos
    ''' </summary>
    ''' <remarks></remarks>
    Dim listXpoRefund As XPCollection(Of Infrastructure.Data.Xpo.TreasuryRepository.RefundXpo)
    ''' <summary>
    ''' Detalles del traslado de facturas
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListAccountPayableTransferDetail As List(Of AccountPayableTransferDetail)

    ''' <summary>
    ''' Variable para saber si confirma (True = Si confirma, False = No confirma)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banConfirm As Boolean

    ''' <summary>
    ''' Variable para saber si guardan y confirman, o si actualizan y confirman (True = GuardarConfirmar, False = ActualizarConfirmar)
    ''' </summary>
    ''' <remarks></remarks>
    Dim banSaveAndConfirm As Boolean

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

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
        Try
            If accountPayableTransfer IsNot Nothing AndAlso accountPayableTransfer.Id > -1 Then
                If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MAccountPayableTransfer(Me.Tag.ToString())
                        AsyncLoader(True)
                        accountPayableTransfer.MarkAsDeleted()
                        Dim result = Await Model.DeleteAccountPayableTransfer(accountPayableTransfer)
                        If result.StateResult = True Then
                            Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
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
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        Else
            If ListAccountPayableTransferDetail Is Nothing OrElse ListAccountPayableTransferDetail.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay Facturas agregadas en la rejilla."
                Exit Sub
            End If
        End If
        AssigningValues()
        Try
            Using model As New MAccountPayableTransfer(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveAccountPayableTransfer(accountPayableTransfer, _idCurrentSequence)
                If Result.StateResult = True Then
                    If accountPayableTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                        If Me._sequence.Sequential Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf accountPayableTransfer.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        If banConfirm Then
                            If banSaveAndConfirm Then
                                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SaveAndConfirmDontJournalVoucher"), Result.ObjectEmbbeded.Code)
                            Else
                                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConfirmationMessage")
                            End If
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        End If
                    End If
                    Me.accountPayableTransfer = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, accountPayableTransfer.Id, 0, accountPayableTransfer.Id)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, accountPayableTransfer.Id, 0, accountPayableTransfer.Id)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, accountPayableTransfer.Id, 0, accountPayableTransfer.Id)
                    End Select
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    ElseIf Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
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
            Await NewAccountPayableTransfer()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que agrega los detalles al traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddAccountPayableTransferDetail()
        Dim listErrors As String = ValidateControlsPopup()
        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors
            Exit Sub
        End If

        'Selecciono los items que esten filtrados
        'Dim listFilterXpCollection = viewBillsPopup.DataController.GetAllFilteredAndSortedRows()

        'Saco los items que fueron seleccionados en el popup y los inserto en otro listado
        Dim ListBillsPopupCompare = (From e In ListXpCollectionAccountPayable Where e.SelectedItem = True).ToList

        Dim listErrorsRepeat As New StringBuilder
        If ListAccountPayableTransferDetail Is Nothing Then
            ListAccountPayableTransferDetail = New List(Of AccountPayableTransferDetail)
        Else
            'Valido que los items seleccionados no existan en la lista de la rejilla
            Dim listRepeat = (From e In ListBillsPopupCompare Where e.SelectedItem = True And ListAccountPayableTransferDetail.Any(Function(y)
                                                                                                                                       If y.AccountPayableId = e.Id Then
                                                                                                                                           Return True
                                                                                                                                       Else
                                                                                                                                           Return False
                                                                                                                                       End If
                                                                                                                                   End Function) = True).ToList


            If listRepeat IsNot Nothing AndAlso listRepeat.Count > 0 Then
                'Remuevo del ListBillsPopupCompare los repetidos e inserto en el mensaje los que estan repetidos
                For Each itemRemove In listRepeat
                    ListBillsPopupCompare.Remove(itemRemove)
                    listErrorsRepeat.AppendLine("- La Factura " & itemRemove.BillNumber & " con Código " & itemRemove.Code & " ya existe en la lista.")
                Next
            End If
        End If

        If ListBillsPopupCompare IsNot Nothing AndAlso ListBillsPopupCompare.Count > 0 Then
            'Recorro el listado ya listo para poder agregar la info al listado principal
            For Each item In ListBillsPopupCompare
                Dim accountPayableTransferDetail As New AccountPayableTransferDetail
                With accountPayableTransferDetail
                    .AccountPayableSupplierDescription = item.IdSupplier.CodeName
                    .AccountPayableConsecutive = item.Code
                    .AccountPayableBillNumber = item.BillNumber
                    .AccountPayableDocumentDate = item.DocumentDate
                    .AccountPayableId = item.Id
                    .Status = 1
                    .StatusName = "Pendiente por Aceptación"
                End With
                ListAccountPayableTransferDetail.Add(accountPayableTransferDetail)
            Next

            For Each item In ListXpCollectionAccountPayable
                item.SelectedItem = False
            Next
            Me.INDGclState.Image = Global.Presentation.Payments.My.Resources.Resources.undcheck
            INDgcBillPopup.RefreshDataSource()
            Mensaje(EeventViewerImages.Informacion) = "Facturas agregadas correctamente."
        End If
        If listErrorsRepeat.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrorsRepeat.ToString
        End If
        INDSleTransferType.Properties.ReadOnly = True
        INDgcBill.DataSource = Nothing
        INDgcBill.DataSource = ListAccountPayableTransferDetail
        INDPceTransfer.ShowPopup()
        INDbtnAddBill.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If ListXpCollectionAccountPayable IsNot Nothing AndAlso ListXpCollectionAccountPayable.Count > 0 Then
            Dim cont = (From e In ListXpCollectionAccountPayable Where e.SelectedItem = True).Count
            If cont = 0 Then
                listErrors.AppendLine("- No hay Facturas seleccionadas para agregar.")
            End If
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo que elimina un detalle de traslado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        If accountPayableTransfer.Status > 1 Then
            Exit Sub
        End If
        Dim _accountPayableTransferDetail As AccountPayableTransferDetail = viewBill.GetFocusedRow
        ListAccountPayableTransferDetail.Remove(_accountPayableTransferDetail)

        If _accountPayableTransferDetail.Id > 0 Then
            If ListDeleteAccountPayableTransferDetail Is Nothing Then
                ListDeleteAccountPayableTransferDetail = New List(Of AccountPayableTransferDetail)
            End If
            _accountPayableTransferDetail.MarkAsDeleted()
            ListDeleteAccountPayableTransferDetail.Add(_accountPayableTransferDetail)
        End If

        If _accountPayableTransferDetail.Id = 0 AndAlso accountPayableTransfer.AccountPayableTransferDetail IsNot Nothing AndAlso accountPayableTransfer.AccountPayableTransferDetail.Count > 0 Then
            Dim cont = accountPayableTransfer.AccountPayableTransferDetail.ToList.FindAll(Function(item) item.AccountPayableId = _accountPayableTransferDetail.AccountPayableId).Count
            If cont > 0 Then
                accountPayableTransfer.AccountPayableTransferDetail.Remove(_accountPayableTransferDetail)
            End If
        End If

        INDgcBill.DataSource = Nothing
        INDgcBill.DataSource = ListAccountPayableTransferDetail
        If ListAccountPayableTransferDetail.Count = 0 Then
            INDSleTransferType.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que consulta todas las facturas que pertenecen a la unidad de radicación escogida
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultBills()
        'AsyncLoader(True)
        Using model As New MAccountPayable(Tag)
            ListXpCollectionAccountPayable = model.ListAccountPayableByFilingUnitId(FilingUnitSourceId)
            INDgcBillPopup.DataSource = Nothing
            INDgcBillPopup.DataSource = ListXpCollectionAccountPayable
        End Using
        'AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Valida que la unidad de radicacion destino no sea igual a la de origen
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateFilingUnitTarget()
        If FilingUnitTargetId IsNot Nothing AndAlso FilingUnitSourceId IsNot Nothing Then
            If FilingUnitTargetId = FilingUnitSourceId Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DontSelectEqualsFilingUnit", NAME_MODULE)
                FilingUnitTargetId = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.accountPayableTransfer IsNot Nothing AndAlso Me.accountPayableTransfer.Id > 0 Then
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
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAccountPayableTransfer.ActionsOnControls
        Set(value As Boolean)
            INDlyAccountPayableTransfer.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDSleTransferType.Enabled = value
            INDPceTransfer.Enabled = value
            INDsleFilingUnitSource.Enabled = value
            INDsleFilingUnitTarget.Enabled = value
            INDgcBill.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value
            INDlyAccountPayableTransfer.EndUpdate()
            If value Then
                INDSleTransferType.Focus()
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

        Dim listItemsColumnEditType As New List(Of Tuple(Of String, Byte))
        listItemsColumnEditType.Add(New Tuple(Of String, Byte)("Cuenta Por Pagar", 1))
        listItemsColumnEditType.Add(New Tuple(Of String, Byte)("Reembolsos", 2))


        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "TranferType", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25), .ColumnEdit = True, .ListItemsDatasourceColumEdit = listItemsColumnEditType},
                              New ColumnInfo() With {.Caption = "Origen", .FieldName = "FilingUnitSourceId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Destino", .FieldName = "FilingUnitTargetId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableTransfer
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountPayableTransfer.Code),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.accountPayableTransfer.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountPayableTransfer.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.accountPayableTransfer.Code)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.accountPayableTransfer.Code)
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
        listStates.Add(New StatusRecord With {.StatusValue = "4", .StatusName = ResourceManager.GetString("StateEvaluated"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyAccountPayableTransfer.BeginUpdate()
        ReadOnlyControls(False)
        ActionsOnControls = False
        Code = String.Empty
        INDSleTransferType.EditValue = 1
        INDSleTransferType.Properties.ReadOnly = False
        FilingUnitSourceId = Nothing
        FilingUnitTargetId = Nothing
        ListAccountPayableTransferDetail = Nothing
        ListDeleteAccountPayableTransferDetail = Nothing
        INDgcBill.DataSource = Nothing
        BarraBotones.CleanAuditBasic()
        INDlyAccountPayableTransfer.EndUpdate()
        accountPayableTransfer = Nothing
        CleanControlsPopup()
        Me.BarraBotones.StatusRecordVisible = False
        'Me.BarraBotones.StatusRecord = 1
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.ReassignOperatingUnit()

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
        With accountPayableTransfer
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .TranferType = INDSleTransferType.EditValue
            .FilingUnitSourceId = FilingUnitSourceId
            .FilingUnitTargetId = FilingUnitTargetId

            If ListAccountPayableTransferDetail IsNot Nothing AndAlso ListAccountPayableTransferDetail.Count > 0 Then
                For Each itemDetail As AccountPayableTransferDetail In ListAccountPayableTransferDetail
                    .AccountPayableTransferDetail.Add(itemDetail)
                Next
            End If

            If ListDeleteAccountPayableTransferDetail IsNot Nothing AndAlso ListDeleteAccountPayableTransferDetail.Count > 0 Then
                For Each itemDetailDelete As AccountPayableTransferDetail In ListDeleteAccountPayableTransferDetail
                    .AccountPayableTransferDetail.Add(itemDetailDelete)
                Next
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
            Using Model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
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
                Using Model As New MAccountPayableTransfer(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetAccountPayableTransfer(INDbtnCode.Text.Trim)
                    accountPayableTransfer = resultOperation.ObjectEmbbeded
                    INDlyAccountPayableTransfer.BeginUpdate()
                    If accountPayableTransfer IsNot Nothing AndAlso accountPayableTransfer.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(accountPayableTransfer.Id))
                            With accountPayableTransfer
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                Code = .Code
                                INDSleTransferType.EditValue = .TranferType
                                banFilingUnit = False
                                FilingUnitSourceId = .FilingUnitSourceId
                                FilingUnitTargetId = .FilingUnitTargetId
                                banFilingUnit = True

                                ListAccountPayableTransferDetail = .AccountPayableTransferDetail.ToList
                                INDSleTransferType.Properties.ReadOnly = True
                                Me.BarraBotones.StatusRecord = .Status.ToString
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.accountPayableTransfer.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = accountPayableTransfer.Id})
                                ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            If accountPayableTransfer.Status <> 1 Then
                                ReadOnlyControls(True)
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                            Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                            End If

                            Me.BarraBotones.SetDocuments(accountPayableTransfer.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableTransfer).Name)
                            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                            Me.BarraBotones.PrintReport(PrintReportAction.None, accountPayableTransfer.Id, 0, accountPayableTransfer.Id, accountPayableTransfer.TranferType)
                            AsyncLoader(False)
                            ActionsOnControls = True
                            INDgcBill.DataSource = Nothing
                            INDgcBill.DataSource = ListAccountPayableTransferDetail
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewAccountPayableTransfer()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                        Me.BarraBotones.StatusRecord = "1"
                    End If
                    INDlyAccountPayableTransfer.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If

        'Try
        '    Me.BarraBotones.StatusRecordVisible = True
        '    Using Model As New MAccountPayableTransfer(CStr(Me.Tag))
        '        AsyncLoader(True)
        '        INDlyAccountPayableTransfer.BeginUpdate()
        '        Dim resultOperation = Await Model.GetAccountPayableTransfer(INDbtnCode.Text.Trim)
        '        accountPayableTransfer = resultOperation.ObjectEmbbeded
        '        If Not accountPayableTransfer Is Nothing Then
        '            If accountPayableTransfer.Id > 0 Then
        '                Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
        '                    Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(accountPayableTransfer.Id))
        '                    With accountPayableTransfer
        '                        LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                        Code = .Code
        '                        INDSleTransferType.EditValue = .TranferType
        '                        banFilingUnit = False
        '                        FilingUnitSourceId = .FilingUnitSourceId
        '                        FilingUnitTargetId = .FilingUnitTargetId
        '                        banFilingUnit = True

        '                        ListAccountPayableTransferDetail = .AccountPayableTransferDetail.ToList
        '                        INDSleTransferType.Properties.ReadOnly = True
        '                        Me.BarraBotones.StatusRecord = .Status.ToString
        '                    End With
        '                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.accountPayableTransfer.Code)
        '                    If result.Id = 0 Then
        '                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                        state.State = Domain.Base.Entities.ObjectState.Added
        '                        record = New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = accountPayableTransfer.Id}
        '                        Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                        record = operation.ObjectEmbbeded
        '                    Else
        '                        record = result
        '                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                    End If

        '                    If accountPayableTransfer.Status <> 1 Then
        '                        ReadOnlyControls(True)
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '                    Else
        '                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
        '                    End If

        '                    Me.BarraBotones.SetDocuments(accountPayableTransfer.Id)
        '                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        '                    Me.BarraBotones.PrintReport(PrintReportAction.None, accountPayableTransfer.Id, 0, accountPayableTransfer.Id, accountPayableTransfer.TranferType)
        '                    AsyncLoader(False)
        '                    ActionsOnControls = True
        '                    INDgcBill.DataSource = Nothing
        '                    INDgcBill.DataSource = ListAccountPayableTransferDetail
        '                End Using
        '            Else
        '                'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                'Me.Code = String.Empty
        '                AsyncLoader(False)
        '                If Me._sequence.IsManual Then
        '                    Me.NewAccountPayableTransfer()
        '                Else
        '                    AsyncLoader(False)
        '                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                    Me.Code = String.Empty
        '                    Deshacer()
        '                    INDbtnCode.Focus()
        '                End If
        '            End If
        '        Else
        '            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            'Me.Code = String.Empty
        '            AsyncLoader(False)
        '            If Me._sequence.IsManual Then
        '                Me.NewAccountPayableTransfer()
        '            Else
        '                AsyncLoader(False)
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Me.Code = String.Empty
        '                Deshacer()
        '                INDbtnCode.Focus()
        '            End If
        '            Me.BarraBotones.StatusRecord = "1"
        '        End If
        '    End Using
        '    INDlyAccountPayableTransfer.EndUpdate()
        'Catch ex As Exception
        '    AsyncLoader(False)
        '    Throw ex
        'End Try
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewAccountPayableTransfer() As Task
        accountPayableTransfer = New AccountPayableTransfer()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
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
        Me.BarraBotones.StatusRecord = "1"

        'Me.accountPayableTransfer = New AccountPayableTransfer()
        'If Me._sequence.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail(0).Id
        '    ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me.Sequense.PaymentsSecuenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequence = Me._sequence.PaymentsSecuenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
        '                Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
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
        'Me.BarraBotones.StatusRecord = "1"
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDgcBillPopup.DataSource = Nothing
        ListBillsPopup = Nothing
        ListXpCollectionAccountPayable = Nothing
        INDGcRefund.DataSource = Nothing
        listXpoRefund = Nothing
        Me.INDGclState.Image = Global.Presentation.Payments.My.Resources.Resources.undcheck
    End Sub

    ''' <summary>
    ''' Metodo que anula un traslado de factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function Anular() As Task
        Using model As New MAccountPayableTransfer(Me.Tag.ToString())
            Try
                AsyncLoader(True)
                Dim Result = Await model.AnnularAccountPayableTransfer(accountPayableTransfer)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    Me.BarraBotones.PrintReport(PrintReportAction.Cancel, accountPayableTransfer.Id, 0, accountPayableTransfer.Id)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    If Result.MessageResult(0) IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString
                    End If
                End If
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End Using
    End Function

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        banFilingUnit = Nothing
        Presenter = Nothing
        accountPayableTransfer = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        ListDeleteAccountPayableTransferDetail = Nothing
        ListBillsPopup = Nothing
        ListXpCollectionAccountPayable = Nothing
        listXpoRefund = Nothing
        ListAccountPayableTransferDetail = Nothing
        banConfirm = Nothing
        banSaveAndConfirm = Nothing
        varImp = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountPayableTransfer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyAccountPayableTransfer, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAccountPayableTransfer(Me)
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
        IndigoGridControl1.RefreshGrid(INDgcBill)
        IndigoGridControl1.RefreshGrid(INDgcBillPopup)
        Dim listTransferType As New List(Of Tuple(Of Byte, String))
        listTransferType.Add(New Tuple(Of Byte, String)(1, "Cuenta Por Pagar"))
        listTransferType.Add(New Tuple(Of Byte, String)(2, "Reembolsos"))
        INDSleTransferType.Properties.DataSource = listTransferType
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewBill, ListActions)
        IndigoGridView1.MoreInfoColunmns(viewBill)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewBill.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            ElseIf col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountPayableTransfer_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
                    Await Me.NewAccountPayableTransfer()
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
    Private Sub INDpceBill_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPceTransfer.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDPceTransfer.ShowPopup()
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAccountPayableTransfer_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"


#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad de radicacion inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnitSource_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnitSource.EditValueChanged
        If FilingUnitSourceId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnitSource.ValidateFilingUnit()
        End If
        If FilingUnitSourceId IsNot Nothing Then
            If INDSleTransferType.EditValue = 1 Then
                ConsultBills()
            Else
                GetRefund()
            End If
        Else
            CleanControlsPopup()
        End If
        ValidateFilingUnitTarget()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de unidad de radicacion destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnitTarget_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleFilingUnitTarget.EditValueChanged
        If FilingUnitTargetId IsNot Nothing AndAlso banFilingUnit = True Then
            INDsleFilingUnitTarget.ValidateFilingUnit()
        End If
        ValidateFilingUnitTarget()
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara cuando cambia el valor del repositorio de check sobre la rejilla de facturas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepCheckSelectedItem_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepCheckSelectedItem.EditValueChanging
        Dim _accountPayable As AccountPayableXpo = viewBillsPopup.GetFocusedRow()
        If _accountPayable IsNot Nothing Then
            _accountPayable.SelectedItem = e.NewValue
            INDgcBillPopup.RefreshDataSource()

            If ListXpCollectionAccountPayable IsNot Nothing AndAlso ListXpCollectionAccountPayable.Count > 0 Then
                Dim listFilterXpCollection = viewBillsPopup.DataController.GetAllFilteredAndSortedRows()
                Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectedItem = True).Count
                If cont = listFilterXpCollection.Count Then
                    Me.INDGclState.Image = Global.Presentation.Payments.My.Resources.Resources.check
                Else
                    Me.INDGclState.Image = Global.Presentation.Payments.My.Resources.Resources.undcheck
                End If
            End If

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
    Private Sub INDbtnAddBill_Click(sender As Object, e As EventArgs) Handles INDbtnAddBill.Click
        AddAccountPayableTransferDetail()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de unidad de radicacion destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFilingUnitTarget_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleFilingUnitTarget.QueryPopUp
        If FilingUnitTargetXpo Is Nothing Then
            Presenter.InitializeFilingUnit()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar los controles del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmAccountPayableTransfer_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbtnCode.Focus()
        'Se carga el primer control de filingUnitSource
        Using model As New MFilingUnit(Tag)
            Dim x As ActionResult(Of List(Of FilingUnit)) = Await model.GetFilingUnitByUser(indigo.UserIndigo)
            Dim listFilingUnit As List(Of FilingUnit) = x.ObjectEmbbeded
            FilingUnitSourceXpo = listFilingUnit
        End Using
        'Se carga el segundo control de filingUnitTarget
        Presenter.InitializeFilingUnit()
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBill_Popup(sender As Object, e As EventArgs) Handles INDPceTransfer.Popup
        INDbtnAddBill.Focus()
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteDetail()
    End Sub

#End Region

#Region "DataSourceChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del datasource de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewBill_DataSourceChanged(sender As Object, e As EventArgs) Handles viewBill.DataSourceChanged
        If ListAccountPayableTransferDetail IsNot Nothing AndAlso ListAccountPayableTransferDetail.Count > 0 Then
            viewBill.ExpandAllGroups()
        End If
    End Sub

#End Region

#Region "MouseDoubleClick"

    ''' <summary>
    ''' Evento que se dispara al presionar doble click en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgcBillPopup_MouseDoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDgcBillPopup.MouseDoubleClick
        If ListXpCollectionAccountPayable IsNot Nothing AndAlso ListXpCollectionAccountPayable.Count > 0 Then
            Dim hitPoint = Me.viewBillsPopup.CalcHitInfo(e.Location)
            If hitPoint.Column IsNot Nothing Then
                If hitPoint.InColumn AndAlso hitPoint.Column.Name.Equals("INDGclState") Then

                    Dim listFilterXpCollection = viewBillsPopup.DataController.GetAllFilteredAndSortedRows()
                    Dim cont As Integer = (From l In listFilterXpCollection Where l.SelectedItem = True).Count

                    If cont = listFilterXpCollection.Count Then
                        For Each item In listFilterXpCollection
                            item.SelectedItem = False
                        Next
                        Me.INDGclState.Image = Global.Presentation.Payments.My.Resources.Resources.undcheck
                    Else
                        For Each item In listFilterXpCollection
                            item.SelectedItem = True
                        Next
                        Me.INDGclState.Image = Global.Presentation.Payments.My.Resources.Resources.check
                    End If
                    Me.INDgcBillPopup.RefreshDataSource()
                    Me.INDgcBillPopup.Invalidate()
                End If
            End If
        End If
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
        banConfirm = False
        banSaveAndConfirm = False
        accountPayableTransfer.Status = 1
        varImp = 2
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
        banConfirm = False
        banSaveAndConfirm = False
        accountPayableTransfer.Status = 1
        varImp = 1
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
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, accountPayableTransfer.Id, 0, accountPayableTransfer.Id, accountPayableTransfer.TranferType)
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequence.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        banConfirm = True
        banSaveAndConfirm = False
        accountPayableTransfer.Status = 2
        varImp = 3
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_Anular
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        accountPayableTransfer.Status = 3
        Anular()
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        banConfirm = True
        banSaveAndConfirm = True
        accountPayableTransfer.Status = 2
        varImp = 3
        Guardar()
    End Sub

#End Region

    Private Sub INDSleTransferType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleTransferType.EditValueChanged
        If INDSleTransferType.EditValue = 1 Then
            INDPceTransfer.Properties.PopupControl = INDpopupBill
        Else
            INDPceTransfer.Properties.PopupControl = INDPccRefund
        End If
        INDsleFilingUnitSource.EditValue = Nothing
    End Sub

    Private Sub GetRefund()
        'AsyncLoader(True)
        Using model As New MAccountPayable(Tag)
            listXpoRefund = model.ListRefundByTransfer(FilingUnitSourceId)
            INDGcRefund.DataSource = Nothing
            INDGcRefund.DataSource = listXpoRefund
        End Using
        'AsyncLoader(False)
    End Sub

    Private Sub INDBtnAddRefund_Click(sender As Object, e As EventArgs) Handles INDBtnAddRefund.Click
        If INDGvRefund.GetSelectedRows().Count() = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar mínimo un reembolso"
            Exit Sub
        End If
        Dim errors As New StringBuilder
        For Each item In INDGvRefund.GetSelectedRows()
            Dim refund = DirectCast(INDGvRefund.GetRow(item), Infrastructure.Data.Xpo.TreasuryRepository.RefundXpo)
            If ListAccountPayableTransferDetail IsNot Nothing Then
                Dim refundAdded = ListAccountPayableTransferDetail.Find(Function(x) x.RefundId = refund.Id)
                If refundAdded IsNot Nothing Then
                    errors.AppendLine("El reembolso " & refund.Code & " ya esta agregado")
                    Continue For
                End If
            End If

            Dim accountPayableTransferDetail As New AccountPayableTransferDetail
            With accountPayableTransferDetail
                .AccountPayableConsecutive = refund.Code
                .AccountPayableDocumentDate = refund.InitialDate
                .RefundId = refund.Id
                .Status = 1
                .StatusName = "Pendiente por Aceptación"
            End With
            If ListAccountPayableTransferDetail Is Nothing Then
                ListAccountPayableTransferDetail = New List(Of AccountPayableTransferDetail)
            End If
            ListAccountPayableTransferDetail.Add(accountPayableTransferDetail)
        Next
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
        End If
        INDSleTransferType.Properties.ReadOnly = True
        INDGvRefund.ClearSelection()
        INDgcBill.DataSource = Nothing
        INDgcBill.DataSource = ListAccountPayableTransferDetail
        INDPceTransfer.ShowPopup()
        INDBtnAddRefund.Focus()
    End Sub

    Private Sub INDPceTransfer_EditValueChanged(sender As Object, e As EventArgs) Handles INDPceTransfer.EditValueChanged

    End Sub
End Class