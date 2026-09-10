'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/09/2014
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
Imports Presentation.Common

#End Region

Public Class FrmOtherDeductions
    Implements IOtherWithholdingDeductions, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IOtherWithholdingDeductions.Status
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
    Public Property Sequence As InventorySequence Implements IOtherWithholdingDeductions.Sequense
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IOtherWithholdingDeductions.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IOtherWithholdingDeductions.MyTag
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
    Public Property Code As String Implements IOtherWithholdingDeductions.Code
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
    ''' Obtiene o establece el id del concepto de cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptId As Integer? Implements IOtherWithholdingDeductions.AccountPayableConceptId
        Get
            Return INDsleAcountPayableConcept.EditValue
        End Get
        Set(value As Integer?)
            INDsleAcountPayableConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de conceptos de cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayableConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IOtherWithholdingDeductions.AccountPayableConceptXpo
        Get
            Return INDsleAcountPayableConcept.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAcountPayableConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el valor base
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeductionValue As Decimal? Implements IOtherWithholdingDeductions.DeductionValue
        Get
            Return INDtxtBaseValue.EditValue
        End Get
        Set(value As Decimal?)
            INDtxtBaseValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del redondeo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RoundId As Integer? Implements IOtherWithholdingDeductions.RoundType
        Get
            Return INDsleRound.EditValue
        End Get
        Set(value As Integer?)
            INDsleRound.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdType As Integer Implements IOtherWithholdingDeductions.Type
        Get
            Return INDsleType.EditValue
        End Get
        Set(value As Integer)
            INDsleType.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador 
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As POtherWithholdingDeductions

    ''' <summary>
    ''' Variable que contiene la lista con el tipo(1: Retencion, 2: Deducción)
    ''' </summary>
    Dim Type As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista con el tipo de redondeo(10: Decima, 100: Centecima, 1000: Milesima)
    ''' </summary>
    Dim RoundType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Representa la entidad otras deducciones y retenciones
    ''' </summary>
    ''' <remarks></remarks>
    Dim otherWithholdingDeductions As OtherWithholdingDeduction

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
        'Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If otherWithholdingDeductions IsNot Nothing AndAlso otherWithholdingDeductions.Id > -1 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Using Model As New MOtherWithholdingDeductions(Me.Tag.ToString())
        '            AsyncLoader(True)
        '            otherWithholdingDeductions.MarkAsDeleted()
        '            Dim result = Await Model.DeleteOtherWithholdingDeduction(otherWithholdingDeductions)
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



        If Me.otherWithholdingDeductions IsNot Nothing AndAlso Me.otherWithholdingDeductions.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MOtherWithholdingDeductions(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteOtherWithholdingDeduction(Me.otherWithholdingDeductions)
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
        'If ValidateControls() = False Then
        '    Exit Sub
        'End If
        'AssigningValues()
        'Using model As New MOtherWithholdingDeductions(Me.Tag.ToString())
        '    AsyncLoader(True)
        '    Dim Result = Await model.SaveOtherWithholdingDeduction(otherWithholdingDeductions, _idCurrentSequense)
        '    AsyncLoader(False)
        '    If Result.StateResult = True Then
        '        If otherWithholdingDeductions.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            'Se descarta la secuencia numerica usada
        '            If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
        '                Me.DicSequense(Me._sequense.InventorySequenceDetail(0).Id).RemoveAt(0)
        '            End If
        '            If Me._sequense.Sequential Then
        '                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
        '            Else
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
        '            End If
        '        ElseIf otherWithholdingDeductions.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '        End If
        '        Me.otherWithholdingDeductions = Result.ObjectEmbbeded
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



        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MOtherWithholdingDeductions(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of OtherWithholdingDeduction) = Await Model.SaveOtherWithholdingDeduction(Me.otherWithholdingDeductions, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If otherWithholdingDeductions.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.otherWithholdingDeductions = result.ObjectEmbbeded
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
            Await NewOtherWithholdingDeductions()
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)},
                                New ColumnInfo() With {.Caption = "Concepto", .FieldName = "AccountPayableConceptId.CodeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.6)},
                              New ColumnInfo() With {.Caption = "Tipo", .FieldName = "TypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListOtherWitholdingDeductions
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IOtherWithholdingDeductions.ActionsOnControls
        Set(value As Boolean)
            INDlyOtherDeductions.BeginUpdate()
            INDbtnCode.Enabled = Not value

            If otherWithholdingDeductions IsNot Nothing AndAlso otherWithholdingDeductions.Id > 0 Then
                INDsleType.Enabled = False
            Else
                If otherWithholdingDeductions Is Nothing Then
                    INDsleType.Enabled = False 
                Else
                    INDsleType.Enabled = True
                End If

            End If
            INDsleAcountPayableConcept.Enabled = value
            INDtxtBaseValue.Enabled = value
            INDsleRound.Enabled = value
            INDlyOtherDeductions.EndUpdate()

            If value Then
                If INDsleType.Enabled Then
                    INDsleType.Focus()
                Else
                    INDsleAcountPayableConcept.Focus()
                End If
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
        If Me.otherWithholdingDeductions IsNot Nothing AndAlso Me.otherWithholdingDeductions.Id > 0 Then
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
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.otherWithholdingDeductions.Code, Me.otherWithholdingDeductions.Type), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & CStr(Me.Tag) & "_" & Me.otherWithholdingDeductions.Code & "#$", .IdForm = CStr(Me.Tag), _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.otherWithholdingDeductions.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.otherWithholdingDeductions.Code, Me.otherWithholdingDeductions.Type)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.otherWithholdingDeductions.Code)
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
        'INDlyOtherDeductions.BeginUpdate()

        'Code = String.Empty
        'IdType = Nothing
        'DeductionValue = Nothing
        'AccountPayableConceptId = Nothing
        'INDsleAcountPayableConcept.Properties.NullText = String.Empty
        'RoundId = Nothing
        'Status = True

        'INDlyItemBaseValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'INDlyItemAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'INDlyItemRound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        ' BarraBotones.CleanAuditBasic()


        ' otherWithholdingDeductions = Nothing
        'Me.BarraBotones.StatusRecordVisible = False
        ' DeleteBlockedRecord()
        ' Me._doc = Nothing
        ' Me.BarraBotones.EnableBarItems()
        ' Me.BarraBotones.DisableBarDocument()
        'INDsleType.Properties.Buttons.Item(1).Visible = False
        'INDsleRound.Properties.Buttons.Item(1).Visible = False
        'ActionsOnControls = False
        'INDlyOtherDeductions.EndUpdate()


        INDlyOtherDeductions.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing


        Code = String.Empty
        IdType = Nothing
        DeductionValue = Nothing
        AccountPayableConceptId = Nothing
        INDsleAcountPayableConcept.Properties.NullText = String.Empty
        RoundId = Nothing
        Status = True

        INDlyItemBaseValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'Limpiar controles
        otherWithholdingDeductions = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyOtherDeductions.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With otherWithholdingDeductions
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Type = IdType

            .AccountPayableConceptId = AccountPayableConceptId
            If IdType = 1 Then
                .RoundType = RoundId
                .DeductionValue = Nothing
            Else
                .RoundType = Nothing
                .DeductionValue = DeductionValue
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
        'Using Model As New MOtherWithholdingDeductions(CStr(Me.Tag))
        '    AsyncLoader(True)
        '    Dim resultOperation = Await Model.GetOtherWithholdingDeduction(INDbtnCode.Text.Trim)
        '    'INDlyOtherDeductions.BeginUpdate()
        '    otherWithholdingDeductions = resultOperation.ObjectEmbbeded
        '    If Not otherWithholdingDeductions Is Nothing Then
        '        If otherWithholdingDeductions.Id > 0 Then
        '            Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
        '                Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(otherWithholdingDeductions.Id))
        '                With otherWithholdingDeductions
        '                    LayoutControls.SetCustomFieldsValue(.CustomProperties)

        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
        '                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

        '                    Code = .Code
        '                    IdType = .Type

        '                    AccountPayableConceptId = .AccountPayableConceptId
        '                    INDsleAcountPayableConcept.Properties.NullText = .AccountPayableConceptDescription
        '                    RoundId = .RoundType

        '                    DeductionValue = .DeductionValue
        '                    Status = .Status
        '                End With
        '                Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.otherWithholdingDeductions.Code)
        '                If result.Id = 0 Then
        '                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '                    state.State = Domain.Base.Entities.ObjectState.Added
        '                    record = New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .FormId = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .RecordId = otherWithholdingDeductions.Id}
        '                    Dim operation = Await ModelRecord.SaveBlockRecord(record)
        '                    record = operation.ObjectEmbbeded
        '                Else
        '                    record = result
        '                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '                End If
        '                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        '                Me.BarraBotones.SetDocuments(otherWithholdingDeductions.Id)
        '                AsyncLoader(False)
        '                If IdType = 1 Then
        '                    INDlyItemAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '                    INDlyItemRound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '                Else
        '                    INDlyItemBaseValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '                End If
        '                ActionsOnControls = True
        '            End Using
        '        Else
        '            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            'Me.Code = String.Empty
        '            AsyncLoader(False)
        '            If Me._sequense.IsManual Then
        '                Await Me.NewOtherWithholdingDeductions()
        '            Else
        '                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '                Code = String.Empty
        '                Deshacer()
        '                INDbtnCode.Focus()
        '            End If
        '        End If
        '    Else
        '        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        'Me.Code = String.Empty
        '        AsyncLoader(False)
        '        If Me._sequense.IsManual Then
        '            Await Me.NewOtherWithholdingDeductions()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Code = String.Empty
        '            Deshacer()
        '            INDbtnCode.Focus()
        '        End If
        '    End If
        'End Using
        ''INDlyOtherDeductions.EndUpdate()



        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MOtherWithholdingDeductions(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetOtherWithholdingDeduction(INDbtnCode.Text.Trim)
                    INDlyOtherDeductions.BeginUpdate()
                    otherWithholdingDeductions = resultOperation.ObjectEmbbeded
                    If otherWithholdingDeductions IsNot Nothing AndAlso otherWithholdingDeductions.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(otherWithholdingDeductions.Id))
                            With otherWithholdingDeductions
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                IdType = .Type

                                AccountPayableConceptId = .AccountPayableConceptId
                                INDsleAcountPayableConcept.Properties.NullText = .AccountPayableConceptDescription
                                RoundId = .RoundType

                                DeductionValue = .DeductionValue
                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.otherWithholdingDeductions.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = otherWithholdingDeductions.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(otherWithholdingDeductions.Id, Me.Tag.ToString(), Nothing, GetType(OtherWithholdingDeduction).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewOtherWithholdingDeductions()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyOtherDeductions.EndUpdate()
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
    Private Async Function NewOtherWithholdingDeductions() As Task
        'otherWithholdingDeductions = New OtherWithholdingDeduction()
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


        otherWithholdingDeductions = New OtherWithholdingDeduction() With {.Status = True}
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
        '    Using model As New MOtherWithholdingDeductions(Me.Tag.ToString())
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


        If Not String.IsNullOrEmpty(Me.otherWithholdingDeductions.Code) Then
            Try
                Using model As New MOtherWithholdingDeductions(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.otherWithholdingDeductions.Status
                    Dim result As ActionResult(Of OtherWithholdingDeduction) = Await model.ChangeState(Me.otherWithholdingDeductions.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.otherWithholdingDeductions = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
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
    ''' Llena los controles con las tuplas
    ''' </summary>
    Private Sub InitializeSearch()
        Type = New List(Of Tuple(Of Integer, String))
        Type.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Retention")))
        Type.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Deduction")))
        INDsleType.Properties.DataSource = Type.ToList

        RoundType = New List(Of Tuple(Of Integer, String))
        RoundType.Add(New Tuple(Of Integer, String)(1, "Peso"))
        RoundType.Add(New Tuple(Of Integer, String)(10, ResourceManager.GetString("Tenth")))
        RoundType.Add(New Tuple(Of Integer, String)(100, ResourceManager.GetString("Hundredth")))
        RoundType.Add(New Tuple(Of Integer, String)(1000, ResourceManager.GetString("Thousandth")))
        INDsleRound.Properties.DataSource = RoundType.ToList
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        Type = Nothing
        RoundType = Nothing
        _sequence = Nothing
        otherWithholdingDeductions = Nothing
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
    Private Sub FrmOtherDeductions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlyOtherDeductions, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New POtherWithholdingDeductions(Me)
        Presenter.GetSequense()
        InitializeSearch()
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
    Private Sub FrmOtherDeductions_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
        ''        Await Me.NewOtherWithholdingDeductions()
        ''    Else
        ''        Await Me.LoadControls()
        ''    End If
        ''End If
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
        '            Await Me.NewOtherWithholdingDeductions()
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
                    Await Me.NewOtherWithholdingDeductions()
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
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmOtherDeductions_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRetentionConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAcountPayableConcept.QueryPopUp
        If INDsleType.EditValue Is Nothing Then
            Exit Sub
        End If

        If INDsleType.EditValue = 1 Then
            Presenter.InitializeAccountPayableConcept(True)
        Else
            Presenter.InitializeAccountPayableConcept(False)
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del control de concepto de retencion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRetentionConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAcountPayableConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Payments.FrmAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using

            If INDsleType.EditValue = 1 Then
                Presenter.InitializeAccountPayableConcept(True)
            Else
                Presenter.InitializeAccountPayableConcept(False)
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue Is Nothing Then
            INDlyItemBaseValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemRound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        'INDsleType.Properties.Buttons.Item(1).Visible = False
        'INDsleRound.Properties.Buttons.Item(1).Visible = False
        Select Case INDsleType.EditValue
            Case 1 'Retencion
                INDlyItemBaseValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemBaseValue.AllowHide = True
                INDlyItemAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAccountPayableConcept.AllowHide = False
                INDlyItemRound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemRound.AllowHide = False
            Case 2 'Deduccion
                INDlyItemBaseValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemBaseValue.AllowHide = False
                INDlyItemAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemAccountPayableConcept.AllowHide = False
                INDlyItemRound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemRound.AllowHide = True
            Case Else
                INDlyItemBaseValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemBaseValue.AllowHide = True
                INDlyItemRound.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemRound.AllowHide = True
                INDlyItemAccountPayableConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemAccountPayableConcept.AllowHide = True
        End Select

        AccountPayableConceptId = Nothing
        DeductionValue = Nothing
        RoundId = Nothing
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

    Private Sub FrmOtherDeductions_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleType.Properties.Buttons.Item(1).Visible = False
        INDsleRound.Properties.Buttons.Item(1).Visible = False
        INDbtnCode.Focus()
    End Sub
End Class