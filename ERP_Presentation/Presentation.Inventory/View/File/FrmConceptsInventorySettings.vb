'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2014
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

Public Class FrmConceptsInventorySettings
    Implements IAdjustmentConcept, ICustomizableForm

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IAdjustmentConcept.Status
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
    Public Property Sequence As InventorySequence Implements IAdjustmentConcept.Sequense
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
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAdjustmentConcept.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IAdjustmentConcept.MyTag
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
    Public Property Code As String Implements IAdjustmentConcept.Code
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
    Public Property NameA As String Implements IAdjustmentConcept.NameA
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    '''  Obtiene o establece la clase del movimiento 
    ''' </summary>
    ''' <value>1 - Ajuste 2 - Traslado</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MovementClass As Byte Implements IAdjustmentConcept.MovementClass
        Get
            Return INDsleMovementClass.EditValue
        End Get
        Set(value As Byte)
            INDsleMovementClass.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de la cuenta contable de ajuste
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AdjustmentAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAdjustmentConcept.AdjustmentAccountXpo
        Get
            Return CType(INDsleAdjustmentAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleAdjustmentAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si afecta promedio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AffectAverageCost As Boolean Implements IAdjustmentConcept.AffectAverageCost
        Get
            Return INDrgAffectsAveragesCost.EditValue
        End Get
        Set(value As Boolean)
            INDrgAffectsAveragesCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de ajuste
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdAdjustmentAccount As Integer Implements IAdjustmentConcept.IdAdjustmentAccount
        Get
            Return INDsleAdjustmentAccount.EditValue
        End Get
        Set(value As Integer)
            INDsleAdjustmentAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo si la cuenta de ajuste maneja centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterId As Integer Implements IAdjustmentConcept.CostCenterId
        Get
            Return INDsleCostCenterId.EditValue
        End Get
        Set(value As Integer)
            INDsleCostCenterId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CostCenterXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAdjustmentConcept.CostCenterXpo
        Get
            Return CType(INDsleCostCenterId.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleCostCenterId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del movimiento
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdConceptType As Integer Implements IAdjustmentConcept.IdConceptType
        Get
            Return INDsleConceptType.EditValue
        End Get
        Set(value As Integer)
            INDsleConceptType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdIvaAccount As Integer? Implements IAdjustmentConcept.IdIvaAccount
        Get
            Return INDsleIvaAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleIvaAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de la cuenta contable iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IvaAccountXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAdjustmentConcept.IvaAccountXpo
        Get
            Return CType(INDsleIvaAccount.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleIvaAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si afecta iva
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IvaAffects As Boolean Implements IAdjustmentConcept.IvaAffects
        Get
            Return INDrgIvaAffects.EditValue
        End Get
        Set(value As Boolean)
            INDrgIvaAffects.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si mustra los productos vencidos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ShowExpiredProduct As Boolean Implements IAdjustmentConcept.ShowExpiredProduct
        Get
            Return INDrgShowExpiredProducts.EditValue
        End Get
        Set(value As Boolean)
            INDrgShowExpiredProducts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IAdjustmentConcept.UserXpo
        Get
            Return CType(INDsleUsers.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUsers.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdUser As Integer? Implements IAdjustmentConcept.IdUser
        Get
            Return INDsleUsers.EditValue
        End Get
        Set(value As Integer?)
            INDsleUsers.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Conceptos de movimientos por usuario
    ''' </summary>
    ''' <returns></returns>
    Public Property _listAdjustmentConceptUser As List(Of AdjustmentConceptUser)
        Get
            Return TryCast(INDgcUsers.DataSource, List(Of AdjustmentConceptUser))
        End Get
        Set(value As List(Of AdjustmentConceptUser))
            INDgcUsers.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa el presentador de grupos
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PAdjustmentConcept

    ''' <summary>
    ''' Variable que contiene la lista con la naturaleza de la cuenta
    ''' </summary>
    Dim InventoryAdjustmentType As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Variable que contiene la lista con la clase de movimiento
    ''' </summary>
    ''' <remarks></remarks>
    Dim movementClassList As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.InventorySequence

    ''' <summary>
    ''' Representa la entidad de concepto de ajuste
    ''' </summary>
    ''' <remarks></remarks>
    Dim adjustmentConcept As AdjustmentConcept

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

    ''' <summary>
    ''' Rpresenta la entidad de cuenta contable
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Property _Puc As MainAccounts

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    '''' <summary>
    '''' Usuario xpo
    '''' </summary>
    '''' <remarks></remarks>
    'Private _usersXpo As Infrastructure.Data.Xpo.SecurityRepository.UserXpo


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
        'If _searchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        '    End If
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If adjustmentConcept IsNot Nothing AndAlso adjustmentConcept.Id > -1 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Using Model As New MAdjustmentConcept(Me.Tag.ToString())
        '            AsyncLoader(True)
        '            adjustmentConcept.MarkAsDeleted()
        '            Dim result = Await Model.DeleteAdjustmentConcept(adjustmentConcept)
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

        If Me.adjustmentConcept IsNot Nothing AndAlso Me.adjustmentConcept.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MAdjustmentConcept(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteAdjustmentConcept(Me.adjustmentConcept)
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
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MAdjustmentConcept(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of AdjustmentConcept) = Await Model.SaveAdjustmentConcept(Me.adjustmentConcept, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If adjustmentConcept.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.adjustmentConcept = result.ObjectEmbbeded
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
        If Me._sequence Is Nothing OrElse Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewAdjustmentConcept()
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
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Clase del Movimiento", .FieldName = "MovementClassName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Tipo Movimiento", .FieldName = "ConceptTypeName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListAdjustmentConcept
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que oculta los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub HideControls(ByVal type As Integer)
        If type = 1 Then
            'INDlyItemAffectsAveragesCost.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'INDlyItemAffectsAveragesCost.AllowHide = False
            INDlyItemShowExpiredProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemShowExpiredProducts.AllowHide = True
            ShowExpiredProduct = False
            INDlyItemIvaAffects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIvaAffects.AllowHide = True
            IvaAffects = False
            INDlyItemIvaAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIvaAccount.AllowHide = True
            IdIvaAccount = Nothing
            INDsleIvaAccount.Properties.NullText = String.Empty
            AffectAverageCost = False
        Else
            'INDlyItemAffectsAveragesCost.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            'INDlyItemAffectsAveragesCost.AllowHide = True
            INDlyItemShowExpiredProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemShowExpiredProducts.AllowHide = False
            INDlyItemIvaAffects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemIvaAffects.AllowHide = False
            INDlyItemIvaAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIvaAccount.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' carga las tuplas del formulario
    ''' </summary>
    Private Sub InitializeTuples()
        InventoryAdjustmentType = New List(Of Tuple(Of Integer, String))
        InventoryAdjustmentType.Add(New Tuple(Of Integer, String)(1, ResourceManager.GetString("Input")))
        InventoryAdjustmentType.Add(New Tuple(Of Integer, String)(2, ResourceManager.GetString("Output")))
        INDsleConceptType.Properties.DataSource = InventoryAdjustmentType.ToList
        INDsleConceptType.Properties.Buttons(1).Visible = False

        movementClassList = New List(Of Tuple(Of Integer, String))
        movementClassList.Add(New Tuple(Of Integer, String)(1, "Ajuste"))
        movementClassList.Add(New Tuple(Of Integer, String)(2, "Traslado"))
        INDsleMovementClass.Properties.DataSource = movementClassList.ToList
        INDsleMovementClass.Properties.Buttons(1).Visible = False
    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAdjustmentConcept.ActionsOnControls
        Set(value As Boolean)
            INDlyAdjustmentConcept.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDsleConceptType.Enabled = value
            INDrgShowExpiredProducts.Enabled = value
            INDsleAdjustmentAccount.Enabled = value
            INDsleCostCenterId.Enabled = value
            INDsleMovementClass.Enabled = value
            INDgcUsers.Enabled = value
            INDsleUsers.Enabled = value
            INDlyAdjustmentConcept.EndUpdate()
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
        If Me.adjustmentConcept IsNot Nothing AndAlso Me.adjustmentConcept.Id > 0 Then
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
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.adjustmentConcept.Code, Me.adjustmentConcept.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.adjustmentConcept.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.adjustmentConcept.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.adjustmentConcept.Code, Me.adjustmentConcept.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.adjustmentConcept.Code)
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
        INDlyAdjustmentConcept.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        Me._doc = Nothing
        Code = String.Empty
        NameA = String.Empty
        IdConceptType = Nothing
        AffectAverageCost = Nothing
        ShowExpiredProduct = Nothing
        MovementClass = Nothing
        IdAdjustmentAccount = Nothing
        INDsleAdjustmentAccount.Properties.NullText = String.Empty
        IvaAffects = Nothing
        IdIvaAccount = Nothing
        INDsleIvaAccount.Properties.NullText = String.Empty
        CostCenterId = Nothing
        INDsleCostCenterId.Properties.NullText = String.Empty
        Status = True
        INDsleUsers.EditValue = Nothing
        INDgcUsers.DataSource = Nothing

        INDlyItemConceptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemShowExpiredProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAffectsAveragesCost.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemIvaAffects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemIvaAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'Limpiar controles
        adjustmentConcept = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyAdjustmentConcept.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With adjustmentConcept
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameA
            .MovementClass = MovementClass
            .ConceptType = IdConceptType

            If _listAdjustmentConceptUser IsNot Nothing Then

                For Each Item In _listAdjustmentConceptUser
                    .AdjustmentConceptUser.Add(Item)
                Next
            End If

            If INDlyItemAffectsAveragesCost.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AffectsAverageCost = AffectAverageCost
            Else
                .AffectsAverageCost = False
            End If

            If INDlyItemShowExpiredProducts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ShowExpiredProduct = ShowExpiredProduct
            Else
                .ShowExpiredProduct = False
            End If

            '.ShowInactiveProduct = ShowInactiveProduct
            .AdjustmentAccountId = IdAdjustmentAccount

            If INDlyItemIvaAffects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IvaAffects = IvaAffects
            Else
                .IvaAffects = False
            End If

            If INDlyItemIvaAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .IvaAccountId = IdIvaAccount
            Else
                .IvaAccountId = Nothing
            End If

            If INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CostCenterId = CostCenterId
            Else
                .CostCenterId = Nothing
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

        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MAdjustmentConcept(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetAdjustmentConcept(INDbtnCode.Text.Trim)
                    INDlyAdjustmentConcept.BeginUpdate()
                    adjustmentConcept = resultOperation.ObjectEmbbeded
                    If adjustmentConcept IsNot Nothing AndAlso adjustmentConcept.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(adjustmentConcept.Id))
                            With adjustmentConcept
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                Code = .Code
                                NameA = .Name
                                MovementClass = .MovementClass
                                IdConceptType = .ConceptType
                                AffectAverageCost = .AffectsAverageCost
                                ShowExpiredProduct = .ShowExpiredProduct
                                'ShowInactiveProduct = .ShowInactiveProduct

                                IdAdjustmentAccount = .AdjustmentAccountId
                                INDsleAdjustmentAccount.Properties.NullText = .MainAccountAdjustmentDescription

                                IvaAffects = .IvaAffects

                                IdIvaAccount = .IvaAccountId
                                INDsleIvaAccount.Properties.NullText = .MainAccountIvaDescription

                                If .CostCenterId IsNot Nothing Then
                                    CostCenterId = .CostCenterId
                                    INDsleCostCenterId.Properties.NullText = .CostCenterDescription
                                End If

                                Status = .Status

                                _listAdjustmentConceptUser = .AdjustmentConceptUser.ToList()
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.adjustmentConcept.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = adjustmentConcept.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(adjustmentConcept.Id, Me.Tag.ToString(), Nothing, GetType(AdjustmentConcept).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewAdjustmentConcept()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlyAdjustmentConcept.EndUpdate()
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
    Private Async Function NewAdjustmentConcept() As Task

        adjustmentConcept = New AdjustmentConcept() With {.Status = True}
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
        '    Using model As New MAdjustmentConcept(Me.Tag.ToString())
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




        If Not String.IsNullOrEmpty(Me.adjustmentConcept.Code) Then
            Try
                Using model As New MAdjustmentConcept(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not adjustmentConcept.Status
                    Dim result As ActionResult(Of AdjustmentConcept) = Await model.ChangeState(Me.adjustmentConcept.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.adjustmentConcept = result.ObjectEmbbeded
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

    Private Sub AddUser(_usersXpo As Infrastructure.Data.Xpo.SecurityRepository.UserXpo)
        If _usersXpo Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontró información del usuario. Vuelva a intentarlo"
            INDsleUsers.Focus()
            Exit Sub
        End If
        If _listAdjustmentConceptUser Is Nothing Then
            _listAdjustmentConceptUser = New List(Of AdjustmentConceptUser)
        Else
            Dim Duplicated As Integer = _listAdjustmentConceptUser.FindAll(Function(x) x.UserId = Me.IdUser).ToList().Count
            If Duplicated > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserDuplicated", NAME_MODULE)
                INDsleUsers.Focus()
                Exit Sub
            End If
        End If
        Dim AdjustmentConceptUser As New AdjustmentConceptUser
        With AdjustmentConceptUser
            .UserId = _usersXpo.Id
            .UserCode = _usersXpo.UserCode
            .FullNameUser = _usersXpo.IdPerson.Fullname
        End With
        _listAdjustmentConceptUser.Add(AdjustmentConceptUser)
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UserAggregatedSatisfactory", NAME_MODULE)
        INDsleUsers.EditValue = Nothing
        INDsleUsers.Focus()
        INDgcUsers.RefreshDataSource()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        InventoryAdjustmentType = Nothing
        movementClassList = Nothing
        _sequence = Nothing
        adjustmentConcept = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        record = Nothing
        _Puc = Nothing
        _searchMode = Nothing
        '_usersXpo = Nothing
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConceptsInventorySettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDlyAdjustmentConcept, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PAdjustmentConcept(Me)
        Presenter.GetSequense()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewUsersGrid, ListActions)
        IndigoGridControl1.RefreshGrid(INDgcUsers)
        InitializeTuples()
        LoadStatus()
        Deshacer()

        'INDsleConceptType.Properties.Buttons.Item(1).Visible = False
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmConceptsInventorySettings_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
                    Await Me.NewAdjustmentConcept()
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
    Private Sub FrmConceptsInventorySettings_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de movimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleConceptType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleConceptType.EditValueChanged
        If INDsleConceptType.EditValue IsNot Nothing Then
            HideControls(INDsleConceptType.EditValue)
        Else
            INDlyItemAffectsAveragesCost.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemAffectsAveragesCost.AllowHide = True
            INDlyItemIvaAffects.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIvaAffects.AllowHide = True
            INDlyItemIvaAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIvaAccount.AllowHide = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de cuenta de ajuste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleAdjustmentAccount_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAdjustmentAccount.EditValueChanged
        If INDsleAdjustmentAccount.EditValue Is Nothing Then
            Exit Sub
        End If
        Dim model As New MAdjustmentConcept(CStr(Me.Tag))
        If INDsleAdjustmentAccount.EditValue IsNot Nothing OrElse IdAdjustmentAccount = 0 Then
            Exit Sub
        End If
        _Puc = Await model.GetAccountById(INDsleAdjustmentAccount.EditValue)
        If _Puc IsNot Nothing Then
            If _Puc.HandlesCostCenter Then
                INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlciCostCenterId.AllowHide = False
            Else
                INDlciCostCenterId.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlciCostCenterId.AllowHide = True

                CostCenterId = Nothing
                INDsleCostCenterId.Properties.NullText = String.Empty
            End If

        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de clase de movimiento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleMovementClass_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleMovementClass.EditValueChanged
        If MovementClass = Nothing Then
            Exit Sub
        End If
        If MovementClass = 1 Then
            INDlyItemConceptType.ShowLayout()
            INDlyItemAffectsAveragesCost.ShowLayout()
            IdConceptType = Nothing
        Else
            INDlyItemConceptType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemConceptType.AllowHide = False
            INDlyItemAffectsAveragesCost.HideLayout()
            IdConceptType = 2
            HideControls(INDsleConceptType.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control afecta iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgIvaAffects_EditValueChanged(sender As Object, e As EventArgs) Handles INDrgIvaAffects.EditValueChanged
        If IvaAffects = True Then
            INDlyItemIvaAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemIvaAccount.AllowHide = False
        Else
            INDlyItemIvaAccount.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemIvaAccount.AllowHide = True
            IdIvaAccount = Nothing
        End If
    End Sub
    'Private Sub INDsleUsers_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUsers.EditValueChanged
    '    If INDsleUsers.EditValue IsNot Nothing Then
    '        _usersXpo = DirectCast(DirectCast(viewUserSearch.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.SecurityRepository.UserXpo)
    '    End If
    'End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable ajuste
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdjustmentAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleAdjustmentAccount.QueryPopUp
        If INDsleAdjustmentAccount.Properties.DataSource Is Nothing Then
            Presenter.InitializeAdjustmentAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de cuenta contable iva
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleIvaAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleIvaAccount.QueryPopUp
        If INDsleIvaAccount.Properties.DataSource Is Nothing Then
            Presenter.InitializeIvaAccount()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de centro de costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCostCenterId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenterId.QueryPopUp
        If CostCenterXpo Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' evento para consulta los usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUsers.QueryPopUp
        If UserXpo Is Nothing Then
            Presenter.InitializeUsers()
        End If
    End Sub
#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton del mas en el control de cuentas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleAdjustmentAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleAdjustmentAccount.ButtonClick, INDsleIvaAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeAccounts()
        End If
    End Sub

#End Region

#Region "Click"
    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDbtnAddUser.Click
        If INDsleUsers.EditValue IsNot Nothing Then
            AddUser(TryCast(TryCast(viewUserSearch.GetFocusedRow,
                    DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)?.OriginalRow,
                    Infrastructure.Data.Xpo.SecurityRepository.UserXpo))
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", NAME_MODULE)
            INDsleUsers.Focus()
        End If
    End Sub
#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim AdjustUser As AdjustmentConceptUser = CType(viewUsersGrid.GetFocusedRow, AdjustmentConceptUser)
        If AdjustUser.Id <> 0 Then
            AdjustUser.MarkAsDeleted()
        End If
        _listAdjustmentConceptUser.Remove(AdjustUser)
        INDgcUsers.RefreshDataSource()
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
        _searchMode = False
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

    Private Sub INDsleCostCenterId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenterId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("517", Nothing, True)
        End If
    End Sub
End Class