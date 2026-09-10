'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 14/03/2014
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
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Common
Imports Presentation.Common
Imports Presentation.Accounting
Imports System.Text

#End Region

Public Class FrmConceptsNotes
    Implements IConceptsNotes, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el tipo de concepto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ConceptType As Integer? Implements IConceptsNotes.ConceptType
        Get
            Return INDsleConceptType.EditValue
        End Get
        Set(value As Integer?)
            INDsleConceptType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si afecta el presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AffectBudget As Boolean? Implements IConceptsNotes.AffectBudget
        Get
            Return INDrgAffectBudget.EditValue
        End Get
        Set(value As Boolean?)
            INDrgAffectBudget.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el comportamiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Behavior As Integer? Implements IConceptsNotes.Behavior
        Get
            Return INDgleBehavior.EditValue
        End Get
        Set(value As Integer?)
            INDgleBehavior.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IConceptsNotes.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IConceptsNotes.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>Secuencia numerica del formulario</value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequense As PaymentsSecuence Implements IConceptsNotes.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As PaymentsSecuence)
            Me._sequense = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PaymentsSecuenceDetail In Me._sequense.PaymentsSecuenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo de un concepto de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeConceptsNotes As String Implements IConceptsNotes.CodeConceptsNotes
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
    ''' Obtiene o establece el nombre de un concepto de nota
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameConceptsNotes As String Implements IConceptsNotes.NameConceptsNotes
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado de los registros de los conceptos de notas
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IConceptsNotes.Status
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
    ''' Contiene el listado de las cuentas contables
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountsXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsNotes.AccountsXpo
        Get
            Return CType(INDgleAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAccount As Integer? Implements IConceptsNotes.IdAccount
        Get
            Return INDgleAccount.EditValue
        End Get
        Set(value As Integer?)
            INDgleAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite establecer si es Tipo categoria Trabajadores Independientes o no.
    ''' </summary>
    Public Property TypeCategoryIndependentWorkers As Boolean Implements IConceptsNotes.TypeCategoryIndependentWorkers
        Get
            Return CType(INDrgTypeCategoryIndependentWorkers.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDrgTypeCategoryIndependentWorkers.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsNotes.RetentionConceptXpo
        Get
            Return INDgleRetentionConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleRetentionConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del concepto de retencion 383
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionConcept383Xpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsNotes.RetentionConcept383Xpo
        Get
            Return INDgleRetentionConcept383.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleRetentionConcept383.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la cuenta contable Retencion 383
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionMainAccount383Xpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IConceptsNotes.RetentionMainAccount383Xpo
        Get
            Return CType(INDgleRetentionMainAccount383.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDgleRetentionMainAccount383.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece si maneja retencion
    ''' </summary>
    ''' <returns></returns>
    Public Property ManageRetention As Boolean Implements IConceptsNotes.ManageRetention
        Get
            Return CBool(INDrgManageRetention.EditValue)
        End Get
        Set(value As Boolean)
            INDrgManageRetention.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Dim Presenter As PConceptsNotes

    ''' <summary>
    ''' Variable que contiene la entidad de conceptos de pago
    ''' </summary>
    ''' <remarks></remarks>
    Dim paymentNoteConcept As AccountPayableConceptNotes


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
    Private _sequense As Domain.Entities.PaymentsSecuence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim _behavior As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Representa el listado de tipo de concepto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _conceptType As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Obtiene o establece el id del concepto de retencion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionConceptId As Integer? Implements IConceptsNotes.RetentionConceptId
        Get
            Return INDgleRetentionConcept.EditValue
        End Get
        Set(value As Integer?)
            INDgleRetentionConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del concepto Retención 383
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionConcept383Id As Integer? Implements IConceptsNotes.RetentionConcept383Id
        Get
            Return INDgleRetentionConcept383.EditValue
        End Get
        Set(value As Integer?)
            INDgleRetentionConcept383.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable Retención 383
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RetentionMainAccount383Id As Integer? Implements IConceptsNotes.RetentionMainAccount383Id
        Get
            Return INDgleRetentionMainAccount383.EditValue
        End Get
        Set(value As Integer?)
            INDgleRetentionMainAccount383.EditValue = value
        End Set
    End Property

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar
        If Me.paymentNoteConcept IsNot Nothing AndAlso Me.paymentNoteConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MConceptsNotes(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeletePaymentNoteConceptAsync(Me.paymentNoteConcept)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
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
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MConceptsNotes(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of AccountPayableConceptNotes) = Await Model.SavePaymentNoteConceptAsync(Me.paymentNoteConcept, Me._idCurrentSequense)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If paymentNoteConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._idCurrentSequense).RemoveAt(0)
                        End If
                    End If
                    Me.paymentNoteConcept = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
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
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Await NewConceptsNotes()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.paymentNoteConcept IsNot Nothing AndAlso Me.paymentNoteConcept.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
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
    ''' Llena el control de comportamiento con el listado
    ''' </summary>
    Private Sub InitializeTuples()
        _behavior = New List(Of Tuple(Of Integer, String))
        _behavior.Add(New Tuple(Of Integer, String)(0, "No Aplica"))
        _behavior.Add(New Tuple(Of Integer, String)(1, "Liberar Recursos"))
        INDgleBehavior.Properties.DataSource = _behavior.ToList()
        INDgleBehavior.Properties.Buttons(1).Visible = False

        _conceptType = New List(Of Tuple(Of Integer, String))
        _conceptType.Add(New Tuple(Of Integer, String)(1, "General"))
        _conceptType.Add(New Tuple(Of Integer, String)(2, "Especifico"))
        INDsleConceptType.Properties.DataSource = _conceptType.ToList()
        INDsleConceptType.Properties.Buttons(1).Visible = False
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConceptsNotes.ActionsOnControls
        Set(value As Boolean)
            INDlycConceptsNotes.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDgleAccount.Enabled = value
            INDrgAffectBudget.Enabled = value
            INDsleConceptType.Enabled = value
            INDrgManageTax.Enabled = value
            INDrgManageRetention.Enabled = value
            INDrgTypeCategoryIndependentWorkers.Enabled = value
            INDgleRetentionConcept.Enabled = value
            INDlycConceptsNotes.EndUpdate()
            If value Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Cuenta Contable", .FieldName = "IdAccount.NumberName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListConceptsNotes
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
        CodeConceptsNotes = ReturnValue
        If CodeConceptsNotes IsNot String.Empty Then
            Await LoadControls()
            If Not INDbteCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentNoteConcept.Code, Me.paymentNoteConcept.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.paymentNoteConcept.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentNoteConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.paymentNoteConcept.Code, Me.paymentNoteConcept.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.paymentNoteConcept.Code)
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
        INDlycConceptsNotes.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True

        'Limpiar controles

        CodeConceptsNotes = String.Empty
        NameConceptsNotes = String.Empty
        IdAccount = Nothing
        INDgleAccount.Properties.NullText = String.Empty
        Behavior = Nothing
        AffectBudget = Nothing
        ConceptType = Nothing
        paymentNoteConcept = Nothing
        TypeCategoryIndependentWorkers = False
        Me.ManageRetention = False
        INDgleRetentionConcept.Properties.NullText = String.Empty
        RetentionConceptId = Nothing
        INDgleRetentionConcept383.Properties.NullText = String.Empty
        RetentionConcept383Id = Nothing
        INDgleRetentionMainAccount383.Properties.NullText = String.Empty
        RetentionMainAccount383Id = Nothing
        HideControlsRetention383()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycConceptsNotes.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With paymentNoteConcept
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeConceptsNotes
            .Name = NameConceptsNotes
            .ManageRetention = ManageRetention
            .ManageTax = INDrgManageTax.EditValue
            .TypeCategoryIndependentWorkers = TypeCategoryIndependentWorkers
            If INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IdAccount = IdAccount
            Else
                .IdAccount = Nothing
            End If
            .AffectBudget = AffectBudget
            .ConceptType = ConceptType
            If INDlyItemBehavior.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Behavior = Behavior
            Else
                .Behavior = Nothing
            End If

            If INDlciRetentionConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RetentionConceptId = RetentionConceptId
            Else
                .RetentionConceptId = Nothing
            End If

            If INDlciRetentionConcept383.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RetentionConcept383Id = RetentionConcept383Id
            Else
                .RetentionConcept383Id = Nothing
            End If

            If INDlciRetentionMainAccount383.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .RetentionMainAccount383Id = RetentionMainAccount383Id
            Else
                .RetentionMainAccount383Id = Nothing
            End If
        End With
    End Sub

    Private Sub HideControlsRetention383()
        INDlciRetentionConcept383.HideControl()
        INDlciRetentionMainAccount383.HideControl()
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(CodeConceptsNotes) AndAlso Not String.IsNullOrWhiteSpace(CodeConceptsNotes) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MConceptsNotes(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetPaymentNoteConceptAsync(INDbteCode.Text.Trim)
                    INDlycConceptsNotes.BeginUpdate()
                    paymentNoteConcept = resultOperation.ObjectEmbbeded
                    If paymentNoteConcept IsNot Nothing AndAlso paymentNoteConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(paymentNoteConcept.Id))
                            With paymentNoteConcept
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                CodeConceptsNotes = .Code
                                NameConceptsNotes = .Name
                                IdAccount = .IdAccount
                                INDgleAccount.Properties.NullText = .NumberNameAccount
                                AffectBudget = .AffectBudget
                                ConceptType = .ConceptType
                                If AffectBudget = True Then
                                    Behavior = .Behavior
                                End If
                                Status = .Status
                                ManageRetention = paymentNoteConcept.ManageRetention
                                INDrgManageTax.EditValue = paymentNoteConcept.ManageTax
                                TypeCategoryIndependentWorkers = .TypeCategoryIndependentWorkers

                                If TypeCategoryIndependentWorkers Then
                                    RetentionConcept383Id = .RetentionConcept383Id
                                    INDgleRetentionConcept383.Properties.NullText = .RetentionConcept383CodeName
                                    RetentionMainAccount383Id = .RetentionMainAccount383Id
                                    INDgleRetentionMainAccount383.Properties.NullText = .RetentionMainAccount383NumberName
                                    RetentionConceptId = Nothing
                                    INDgleRetentionConcept.Properties.NullText = String.Empty
                                Else
                                    RetentionConcept383Id = Nothing
                                    INDgleRetentionConcept383.Properties.NullText = String.Empty
                                    RetentionMainAccount383Id = Nothing
                                    INDgleRetentionMainAccount383.Properties.NullText = String.Empty
                                    RetentionConceptId = .RetentionConceptId
                                    INDgleRetentionConcept.Properties.NullText = .RetentionConceptCodeName
                                End If

                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.paymentNoteConcept.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordPayments With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = paymentNoteConcept.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(paymentNoteConcept.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConceptNotes).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequense.IsManual Then
                            Await Me.NewConceptsNotes()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            CodeConceptsNotes = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlycConceptsNotes.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewConceptsNotes() As Task
        paymentNoteConcept = New AccountPayableConceptNotes() With {.Status = True}
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequense.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequense = Me._sequense.PaymentsSecuenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequense.Sequential Then
                Me.CodeConceptsNotes = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                        Me.CodeConceptsNotes = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayments(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequense)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequense))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequense)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequense)).Count > 0 Then
                            Me.CodeConceptsNotes = Me.DicSequense(CInt(Me._idCurrentSequense))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeConceptsNotes = ResourceManager.GetString("LabelOrTextboxNew")
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
        If Not String.IsNullOrEmpty(Me.paymentNoteConcept.Code) Then
            Try
                Using model As New MConceptsNotes(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not paymentNoteConcept.Status
                    Dim result As ActionResult(Of AccountPayableConceptNotes) = Await model.ChangeState(Me.paymentNoteConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.paymentNoteConcept = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
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

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        SearchMode = Nothing
        Presenter = Nothing
        paymentNoteConcept = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _behavior = Nothing
        _conceptType = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConceptsNotes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PConceptsNotes(Me)
        Presenter.GetSequense()
        LoadStatus()
        Deshacer()
        InitializeTuples()
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConceptsNotes_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(CodeConceptsNotes.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeConceptsNotes) Then
                    Await Me.NewConceptsNotes()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
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
    Private Sub FrmConceptsNotes_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al hacer clic en el boton del control de la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.Initialize()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de afectacion de presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgAffectBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgAffectBudget.EditValueChanged
        If INDrgAffectBudget.EditValue Is Nothing Then
            INDlyItemBehavior.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemBehavior.AllowHide = True
        Else
            If INDrgAffectBudget.EditValue = True Then
                INDlyItemBehavior.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemBehavior.AllowHide = False
            Else
                INDlyItemBehavior.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemBehavior.AllowHide = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptType.EditValueChanged
        If ConceptType IsNot Nothing Then
            If ConceptType = 1 Then
                INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAccount.AllowHide = True
            Else
                INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAccount.AllowHide = False
            End If
        Else
            INDlyItemAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemAccount.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' evento cuando maneja retencion o no
    ''' </summary>
    Private Sub TrueOrFalseManageRetention() Handles INDrgManageRetention.EditValueChanged
        If ManageRetention Then
            Me.INDlciRetentionConcept.HideControl(Not ManageRetention)
            Me.INDlyTypeCategoryIndependentWorkers.HideControl(Not ManageRetention)
            INDrgManageTax.EditValue = False
        Else
            Me.INDlciRetentionConcept.HideControl()
            Me.INDlyTypeCategoryIndependentWorkers.HideControl()
            Me.TypeCategoryIndependentWorkers = False
            Me.RetentionConceptId = Nothing
            Me.INDgleRetentionConcept.Properties.NullText = String.Empty
        End If
    End Sub

    Private Sub TrueOrFalseManageTax() Handles INDrgManageTax.EditValueChanged
        If INDrgManageTax.EditValue Then
            ManageRetention = False
        End If
    End Sub

    Private Sub INDrgTypeCategoryIndependentWorkers_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgTypeCategoryIndependentWorkers.EditValueChanged
        If TypeCategoryIndependentWorkers Then
            INDlciRetentionConcept383.ShowLayout()
            INDlciRetentionMainAccount383.ShowLayout()
            INDlciRetentionConcept.HideLayout()
            RetentionConceptId = Nothing
        Else
            HideControlsRetention383()
            RetentionConcept383Id = Nothing
            RetentionMainAccount383Id = Nothing
            INDlciRetentionConcept.ShowLayout()
        End If
    End Sub
#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleAccount.QueryPopUp
        If INDgleAccount.Properties.DataSource Is Nothing Then
            Presenter.Initialize()
        End If
    End Sub

    Private Sub INDgleRetentionConcept383_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleRetentionConcept383.QueryPopUp
        If RetentionConcept383Xpo Is Nothing Then
            Presenter.InitializeRetentionConcept383()
        End If
    End Sub

    Private Sub INDgleRetentionConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleRetentionConcept.QueryPopUp
        If RetentionConceptXpo Is Nothing Then
            Presenter.InitializeRetentionConcept()
        End If
    End Sub

    Private Sub INDgleRetentionMainAccount383_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleRetentionMainAccount383.QueryPopUp
        If RetentionMainAccount383Xpo Is Nothing Then
            Presenter.InitializeRetentionMainAccount383()
        End If
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbteCode.ButtonClick
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
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PaymentsSecuenceDetail IsNot Nothing Then
                If Not Me._sequense.PaymentsSecuenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

End Class