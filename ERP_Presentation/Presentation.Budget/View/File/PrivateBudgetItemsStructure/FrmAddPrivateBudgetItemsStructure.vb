'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 29/01/2018
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Budget.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text
Imports Domain.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Controls.MVP
Imports DevExpress.Utils.Menu
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common

#End Region

Public Class FrmAddPrivateBudgetItemsStructure
    Implements IPrivateBudgetItemsStructure

#Region "Properties"

    ''' <summary>
    ''' Id cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Public Property MainAccountId As Integer? Implements IPrivateBudgetItemsStructure.MainAccountId
        Get
            Return INDsleMainAccount.EditValue
        End Get
        Set(value As Integer?)
            INDsleMainAccount.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Public Property MainAccountXpo As XPInstantFeedbackSource Implements IPrivateBudgetItemsStructure.MainAccountXpo
        Get
            Return INDsleMainAccount.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleMainAccount.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id del tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdpartyId As Integer? Implements IPrivateBudgetItemsStructure.ThirdpartyId
        Get
            Return INDsleThirdParty.EditValue
        End Get
        Set(value As Integer?)
            INDsleThirdParty.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource tercero
    ''' </summary>
    ''' <returns></returns>
    Public Property ThirdPartyXpo As XPInstantFeedbackSource Implements IPrivateBudgetItemsStructure.ThirdPartyXpo
        Get
            Return INDsleThirdParty.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleThirdParty.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Id centro costo
    ''' </summary>
    ''' <returns></returns>
    Public Property CostCenterId As Integer? Implements IPrivateBudgetItemsStructure.CostCenterId
        Get
            Return INDsleCostCenter.EditValue
        End Get
        Set(value As Integer?)
            INDsleCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource centro costo
    ''' </summary>
    ''' <returns></returns>
    Public Property CostCenterXpo As XPInstantFeedbackSource Implements IPrivateBudgetItemsStructure.CostCenterXpo
        Get
            Return INDsleCostCenter.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCostCenter.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Valores no presupuestados
    ''' </summary>
    ''' <returns></returns>
    Public Property UnBudgetValues As Integer? Implements IPrivateBudgetItemsStructure.UnBudgetValues
        Get
            Return INDsleUnBudgetValues.EditValue
        End Get
        Set(value As Integer?)
            INDsleUnBudgetValues.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Control orden de compra
    ''' </summary>
    ''' <returns></returns>
    Public Property PurchaseOrderControl As Boolean? Implements IPrivateBudgetItemsStructure.PurchaseOrderControl
        Get
            Return INDslePurchaseOrderControl.EditValue
        End Get
        Set(value As Boolean?)
            INDslePurchaseOrderControl.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Email
    ''' </summary>
    ''' <returns></returns>
    Public Property EmailNotification As String Implements IPrivateBudgetItemsStructure.EmailNotification
        Get
            Return INDtxtEmailNotification.EditValue
        End Get
        Set(value As String)
            INDtxtEmailNotification.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Periodicidad
    ''' </summary>
    ''' <returns></returns>
    Public Property Periodicity As Integer? Implements IPrivateBudgetItemsStructure.Periodicity
        Get
            Return INDslePeriodicity.EditValue
        End Get
        Set(value As Integer?)
            INDslePeriodicity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Alerta ejecucion presupuestal
    ''' </summary>
    ''' <returns></returns>
    Public Property ExecutionAlertPercentage As Decimal? Implements IPrivateBudgetItemsStructure.ExecutionAlertPercentage
        Get
            Return INDseExecutionAlertPercentage.EditValue
        End Get
        Set(value As Decimal?)
            INDseExecutionAlertPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Permite exceder presupuesto
    ''' </summary>
    ''' <returns></returns>
    Public Property AllowExceedBudget As Boolean? Implements IPrivateBudgetItemsStructure.AllowExceedBudget
        Get
            Return INDsleAllowExceedBudget.EditValue
        End Get
        Set(value As Boolean?)
            INDsleAllowExceedBudget.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' % exceder
    ''' </summary>
    ''' <returns></returns>
    Public Property ExceedBudgetPercentage As Decimal? Implements IPrivateBudgetItemsStructure.ExceedBudgetPercentage
        Get
            Return INDseExceedBudgetPercentage.EditValue
        End Get
        Set(value As Decimal?)
            INDseExceedBudgetPercentage.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Control presupuestal
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetControl As Integer? Implements IPrivateBudgetItemsStructure.BudgetControl
        Get
            Return INDsleBudgetControl.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetControl.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As Integer Implements IPrivateBudgetItemsStructure.Type
        Get
            Return INDsleType.EditValue
        End Get
        Set(value As Integer)
            INDsleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IPrivateBudgetItemsStructure.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IPrivateBudgetItemsStructure.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la secuencia numerica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As BudgetSequence Implements IPrivateBudgetItemsStructure.Sequense
        Get
            Return Me._sequense
        End Get
        Set(value As BudgetSequence)
            Me._sequense = value
            Me.DicSequense.Clear()
            For Each seq As Domain.Entities.BudgetSequenceDetail In Me._sequense.BudgetSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el codigo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IPrivateBudgetItemsStructure.Code
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
    ''' Descripción
    ''' </summary>
    ''' <returns></returns>
    Public Property Description As String Implements IPrivateBudgetItemsStructure.Description
        Get
            Return INDtxtDescription.EditValue
        End Get
        Set(value As String)
            INDtxtDescription.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Id del padre
    ''' </summary>
    ''' <returns></returns>
    Public Property ParentId As Integer? Implements IPrivateBudgetItemsStructure.ParentId
        Get
            Return INDsleParent.EditValue
        End Get
        Set(value As Integer?)
            INDsleParent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource del padre
    ''' </summary>
    ''' <returns></returns>
    Public Property ParentXpo As XPInstantFeedbackSource Implements IPrivateBudgetItemsStructure.ParentXpo
        Get
            Return INDsleParent.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleParent.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Permite saber el id del padre
    ''' </summary>
    Public PivotParentId As Integer

    ''' <summary>
    ''' Permite saber la descripción del padre
    ''' </summary>
    Public PivotParentDescription As String

    ''' <summary>
    ''' Permite saber que accion tomar
    ''' 0 - Nada(Carga el form normal)
    ''' 1 - Agrega Nivel
    ''' 2 - Modifica
    ''' </summary>
    Public EditMode As Integer

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador de concepto de notas
    ''' </summary>
    Dim Presenter As PPrivateBudgetItemsStructure

    ''' <summary>
    ''' Variable que contiene la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim PrivateBudgetItemsStructure As PrivateBudgetItemsStructure

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordBudget

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64

    ''' <summary>
    ''' Evento que se ejecuta para actualizar los niveles de la estructura
    ''' </summary>
    Public Event RefreshDatasourceStruct()

    ''' <summary>
    ''' Listado de tipos
    ''' </summary>
    Dim ListTypes As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de valores no presupuestados
    ''' </summary>
    Dim ListUnBudgetValues As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de control orden compra
    ''' </summary>
    Dim ListPurchaseOrderControl As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Listado de periodicidad
    ''' </summary>
    Dim ListPeriodicity As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de permite exceder presupuesto
    ''' </summary>
    Dim ListAllowExceedBudget As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Listado de control presupuestal
    ''' </summary>
    Dim ListBudgetControl As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Listado de detalles
    ''' </summary>
    Dim ListPrivateBudgetItemsStructureDetail As List(Of PrivateBudgetItemsStructureDetail)

    ''' <summary>
    ''' Listado de detalles eliminados
    ''' </summary>
    Dim ListDeletePrivateBudgetItemsStructureDetail As List(Of PrivateBudgetItemsStructureDetail)

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
        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'If Not SearchMode Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'End If
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If PrivateBudgetItemsStructure IsNot Nothing AndAlso PrivateBudgetItemsStructure.Id > -1 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using Model As New MPrivateBudgetItemsStructure(Me.Tag.ToString())
                    AsyncLoader(True)
                    PrivateBudgetItemsStructure.MarkAsDeleted()
                    Dim result = Await Model.DeletePrivateBudgetItemsStructure(PrivateBudgetItemsStructure)
                    If result.StateResult = True Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                        RaiseEvent RefreshDatasourceStruct()
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
        If ValidateControls() = True Then
            If Type > 1 AndAlso (ListPrivateBudgetItemsStructureDetail Is Nothing OrElse ListPrivateBudgetItemsStructureDetail.Count = 0) Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe agregar detalles a la rejilla"
                Exit Sub
            End If
            AssigningValues()
            Using model As New MPrivateBudgetItemsStructure(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SavePrivateBudgetItemsStructure(PrivateBudgetItemsStructure, _idCurrentSequense)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    If PrivateBudgetItemsStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        'Se descarta la secuencia numerica usada
                        If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
                            Me.DicSequense(Me._sequense.BudgetSequenceDetail(0).Id).RemoveAt(0)
                        End If
                        If Me._sequense.Sequential Then
                            Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Result.ObjectEmbbeded.Code)
                        Else
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        End If
                    ElseIf PrivateBudgetItemsStructure.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                    End If
                    Me.PrivateBudgetItemsStructure = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                    RaiseEvent RefreshDatasourceStruct()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequense.IsManual Then
            Deshacer()
        Else
            Me.NewPrivateBudgetItemsStructure()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina un detalle de la rejilla
    ''' </summary>
    Private Sub DeleteDetail()
        Dim entity = CType(INDviewDetail.GetFocusedRow, PrivateBudgetItemsStructureDetail)

        If entity.ThirdPartyId Is Nothing AndAlso entity.CostCenterId Is Nothing AndAlso BudgetControl <> 1 AndAlso INDviewDetail.RowCount > 1 Then
            Mensaje(EeventViewerImages.Advertencia) = "Se debe eliminar primero los otros detalles"
            Exit Sub
        End If

        If MessageIndigo.Show("Desea eliminar el registro?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        ListPrivateBudgetItemsStructureDetail.Remove(entity)

        If entity.Id > 0 Then
            If ListDeletePrivateBudgetItemsStructureDetail Is Nothing Then
                ListDeletePrivateBudgetItemsStructureDetail = New List(Of PrivateBudgetItemsStructureDetail)
            End If
            ListDeletePrivateBudgetItemsStructureDetail.Add(entity.MarkAsDeleted())
        End If

        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListPrivateBudgetItemsStructureDetail

        If INDviewDetail.RowCount = 0 Then
            INDsleType.Properties.ReadOnly = False
            INDsleBudgetControl.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Agrega los detalles a la rejilla
    ''' </summary>
    Private Sub AddDetail()
        Dim listErros As New StringBuilder

        If MainAccountId Is Nothing Then
            listErros.AppendLine("Debe agregar una cuenta contable")
        End If

        If INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If ThirdpartyId Is Nothing Then
                listErros.AppendLine("Debe agregar un tercero")
            End If
        End If

        If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If CostCenterId Is Nothing Then
                listErros.AppendLine("Debe agregar un centro costo")
            End If
        End If

        If listErros.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErros.ToString()
            Exit Sub
        End If

        If ListPrivateBudgetItemsStructureDetail Is Nothing Then
            ListPrivateBudgetItemsStructureDetail = New List(Of PrivateBudgetItemsStructureDetail)
        Else
            Dim count As Integer = 0
            Select Case BudgetControl
                Case 1 'Ninguno
                    count = (From x In ListPrivateBudgetItemsStructureDetail Where x.MainAccountId = MainAccountId Select x).Count()
                Case 2 'Tercero
                    count = (From x In ListPrivateBudgetItemsStructureDetail Where x.MainAccountId = MainAccountId AndAlso x.ThirdPartyId IsNot Nothing AndAlso x.ThirdPartyId = ThirdpartyId Select x).Count()
                Case 3 'Centro costo
                    count = (From x In ListPrivateBudgetItemsStructureDetail Where x.MainAccountId = MainAccountId AndAlso x.CostCenterId IsNot Nothing AndAlso x.CostCenterId = CostCenterId Select x).Count()
                Case 4, 5
                    count = (From x In ListPrivateBudgetItemsStructureDetail Where x.MainAccountId = MainAccountId AndAlso x.ThirdPartyId IsNot Nothing AndAlso x.ThirdPartyId = ThirdpartyId AndAlso x.CostCenterId IsNot Nothing AndAlso x.CostCenterId = CostCenterId Select x).Count()
            End Select
            If count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro en la rejilla con la información suministrada"
                INDsleMainAccount.Focus()
                Exit Sub
            End If
        End If

        Dim entity As New PrivateBudgetItemsStructureDetail
        With entity
            .MainAccountId = MainAccountId
            .MainAccountDescription = INDsleMainAccount.Text
            If INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ThirdPartyId = ThirdpartyId
                .ThirdPartyDescription = INDsleThirdParty.Text
            Else
                .ThirdPartyId = Nothing
                .ThirdPartyDescription = String.Empty
            End If
            If INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CostCenterId = CostCenterId
                .CostCenterDescription = INDsleCostCenter.Text
            Else
                .CostCenterId = Nothing
                .CostCenterDescription = String.Empty
            End If
        End With
        ListPrivateBudgetItemsStructureDetail.Add(entity)
        Mensaje(EeventViewerImages.Informacion) = "Detalle agregado correctamente"
        INDgcDetail.DataSource = Nothing
        INDgcDetail.DataSource = ListPrivateBudgetItemsStructureDetail
        CleanControlPopups()
        INDsleType.Properties.ReadOnly = True
        INDsleBudgetControl.Properties.ReadOnly = True
        INDsleMainAccount.Focus()
    End Sub

    ''' <summary>
    ''' Inicializa la información de las tuplas
    ''' </summary>
    Private Sub InitializeTuples()
        ListTypes = New List(Of Tuple(Of Integer, String))
        ListTypes.Add(New Tuple(Of Integer, String)(1, "Grupo"))
        ListTypes.Add(New Tuple(Of Integer, String)(2, "Rubro Venta"))
        ListTypes.Add(New Tuple(Of Integer, String)(3, "Rubro Gasto"))
        INDsleType.Properties.DataSource = ListTypes

        ListUnBudgetValues = New List(Of Tuple(Of Integer, String))
        ListUnBudgetValues.Add(New Tuple(Of Integer, String)(1, "Advertir"))
        ListUnBudgetValues.Add(New Tuple(Of Integer, String)(2, "No Permitir"))
        ListUnBudgetValues.Add(New Tuple(Of Integer, String)(3, "Ninguno"))
        INDsleUnBudgetValues.Properties.DataSource = ListUnBudgetValues

        ListPurchaseOrderControl = New List(Of Tuple(Of Boolean, String))
        ListPurchaseOrderControl.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListPurchaseOrderControl.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDslePurchaseOrderControl.Properties.DataSource = ListPurchaseOrderControl

        ListPeriodicity = New List(Of Tuple(Of Integer, String))
        ListPeriodicity.Add(New Tuple(Of Integer, String)(1, "Diario"))
        ListPeriodicity.Add(New Tuple(Of Integer, String)(2, "Semanal"))
        ListPeriodicity.Add(New Tuple(Of Integer, String)(3, "Quincenal"))
        ListPeriodicity.Add(New Tuple(Of Integer, String)(4, "Mensual"))
        INDslePeriodicity.Properties.DataSource = ListPeriodicity

        ListAllowExceedBudget = New List(Of Tuple(Of Boolean, String))
        ListAllowExceedBudget.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListAllowExceedBudget.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleAllowExceedBudget.Properties.DataSource = ListAllowExceedBudget

        ListBudgetControl = New List(Of Tuple(Of Integer, String))
        ListBudgetControl.Add(New Tuple(Of Integer, String)(1, "Ninguno"))
        ListBudgetControl.Add(New Tuple(Of Integer, String)(2, "Tercero"))
        ListBudgetControl.Add(New Tuple(Of Integer, String)(3, "Centro Costo"))
        ListBudgetControl.Add(New Tuple(Of Integer, String)(4, "Centro Costo / Tercero"))
        ListBudgetControl.Add(New Tuple(Of Integer, String)(5, "Tercero / Centro Costo"))
        INDsleBudgetControl.Properties.DataSource = ListBudgetControl
    End Sub

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPrivateBudgetItemsStructure.ActionsOnControls
        Set(value As Boolean)
            INDlyRoot.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtDescription.Enabled = value
            INDsleParent.Enabled = value
            INDsleType.Enabled = value
            INDlyRoot.EndUpdate()
            If value Then
                INDtxtDescription.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad establece si los controles estan o no habilitados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsValidateControls As Boolean
        Set(value As Boolean)
            INDlyItemUnBudgetValues.AllowHide = value
            'INDlyItemPurchaseOrderControl.AllowHide = value
            INDlyItemPeriodicity.AllowHide = value
            'INDlyItemExecutionAlertPercentage.AllowHide = value
            'INDlyItemAllowExceedBudget.AllowHide = value
            INDlyItemBudgetControl.AllowHide = value
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
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Description", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.7)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListPrivateBudgetItemsStructure
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

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
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PrivateBudgetItemsStructure.Code, Me.PrivateBudgetItemsStructure.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.PrivateBudgetItemsStructure.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PrivateBudgetItemsStructure.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.PrivateBudgetItemsStructure.Code, Me.PrivateBudgetItemsStructure.Description)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.PrivateBudgetItemsStructure.Code)
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
        INDlyRoot.BeginUpdate()
        ActionsOnControls = False
        Code = String.Empty
        Description = String.Empty
        ParentId = Nothing
        INDsleParent.Properties.NullText = String.Empty
        Type = Nothing
        INDsleType.Properties.ReadOnly = False
        INDsleBudgetControl.Properties.ReadOnly = False
        ParentXpo = Nothing
        UnBudgetValues = Nothing
        PurchaseOrderControl = Nothing
        EmailNotification = Nothing
        Periodicity = Nothing
        ExecutionAlertPercentage = Nothing
        AllowExceedBudget = Nothing
        ExceedBudgetPercentage = Nothing
        BudgetControl = Nothing
        ActionsValidateControls = True
        CleanControlPopups()
        INDgcDetail.DataSource = Nothing
        ListPrivateBudgetItemsStructureDetail = Nothing
        ListDeletePrivateBudgetItemsStructureDetail = Nothing

        INDlyItemPurchaseOrderControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPurchaseOrderControl.AllowHide = True
        INDlyItemExecutionAlertPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemExecutionAlertPercentage.AllowHide = True
        INDlyItemAllowExceedBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemAllowExceedBudget.AllowHide = True

        INDlyItemExceedBudgetPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemExceedBudgetPercentage.AllowHide = True
        INDlygAccountInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDpceAddDetail.Properties.ReadOnly = True
        BarraBotones.CleanAuditBasic()
        INDlyRoot.EndUpdate()
        PrivateBudgetItemsStructure = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    Private Sub CleanControlPopups()
        MainAccountId = Nothing
        ThirdpartyId = Nothing
        CostCenterId = Nothing
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With PrivateBudgetItemsStructure
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Description = Description
            .ParentId = ParentId
            .Type = Type
            If INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .UnBudgetValues = UnBudgetValues
                If INDlyItemPurchaseOrderControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .PurchaseOrderControl = PurchaseOrderControl
                Else
                    .PurchaseOrderControl = Nothing
                End If
                .EmailNotification = EmailNotification
                .Periodicity = Periodicity
                If INDlyItemExecutionAlertPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .ExecutionAlertPercentage = ExecutionAlertPercentage
                Else
                    .ExecutionAlertPercentage = Nothing
                End If
                If INDlyItemAllowExceedBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .AllowExceedBudget = AllowExceedBudget
                Else
                    .AllowExceedBudget = Nothing
                End If
                If INDlyItemExceedBudgetPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .ExceedBudgetPercentage = ExceedBudgetPercentage
                Else
                    .ExceedBudgetPercentage = Nothing
                End If
                .BudgetControl = BudgetControl
            Else
                .UnBudgetValues = Nothing
                .PurchaseOrderControl = Nothing
                .EmailNotification = Nothing
                .Periodicity = Nothing
                .ExecutionAlertPercentage = Nothing
                .AllowExceedBudget = Nothing
                .ExceedBudgetPercentage = Nothing
                .BudgetControl = Nothing
            End If

            If ListPrivateBudgetItemsStructureDetail IsNot Nothing AndAlso ListPrivateBudgetItemsStructureDetail.Count > 0 Then
                ListPrivateBudgetItemsStructureDetail.ForEach(Sub(item) .PrivateBudgetItemsStructureDetail.Add(item))
            End If

            If ListDeletePrivateBudgetItemsStructureDetail IsNot Nothing AndAlso ListDeletePrivateBudgetItemsStructureDetail.Count > 0 Then
                ListDeletePrivateBudgetItemsStructureDetail.ForEach(Sub(item) .PrivateBudgetItemsStructureDetail.Add(item))
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
        Using Model As New ModelBaseBudget(CStr(Me.Tag))
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
        Using Model As New MPrivateBudgetItemsStructure(CStr(Me.Tag))
            AsyncLoader(True)
            Dim resultOperation = Await Model.GetPrivateBudgetItemsStructure(INDbtnCode.Text.Trim)
            PrivateBudgetItemsStructure = resultOperation.ObjectEmbbeded
            INDlyRoot.BeginUpdate()
            If Not PrivateBudgetItemsStructure Is Nothing Then
                If PrivateBudgetItemsStructure.Id > 0 Then
                    Using ModelRecord As New ModelBaseBudget(CStr(Me.Tag))
                        Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(PrivateBudgetItemsStructure.Id))
                        With PrivateBudgetItemsStructure
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)

                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            Code = .Code
                            Description = .Description
                            ParentId = .ParentId
                            INDsleParent.Properties.NullText = .PrivateBudgetItemsStructureDescription
                            Type = .Type

                            UnBudgetValues = .UnBudgetValues
                            PurchaseOrderControl = .PurchaseOrderControl
                            EmailNotification = .EmailNotification
                            Periodicity = .Periodicity
                            ExecutionAlertPercentage = .ExecutionAlertPercentage
                            AllowExceedBudget = .AllowExceedBudget
                            ExceedBudgetPercentage = .ExceedBudgetPercentage
                            BudgetControl = .BudgetControl

                            If .PrivateBudgetItemsStructureDetail IsNot Nothing AndAlso .PrivateBudgetItemsStructureDetail.Count > 0 Then
                                ListPrivateBudgetItemsStructureDetail = .PrivateBudgetItemsStructureDetail.ToList()
                                INDgcDetail.DataSource = Nothing
                                INDgcDetail.DataSource = ListPrivateBudgetItemsStructureDetail
                            End If
                        End With
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.PrivateBudgetItemsStructure.Code)
                        If result.Id = 0 Then
                            Dim state = New Domain.Base.Entities.ObjectChangeTracker
                            state.State = Domain.Base.Entities.ObjectState.Added
                            record = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = CInt(Me.Tag), .CodUser = Me.indigo.UserIndigo, .IdRecord = PrivateBudgetItemsStructure.Id}
                            Dim operation = Await ModelRecord.SaveBlockRecord(record)
                            record = operation.ObjectEmbbeded
                        Else
                            record = result
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        End If
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.SetDocuments(PrivateBudgetItemsStructure.Id)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        INDsleType.Properties.ReadOnly = False
                        If PrivateBudgetItemsStructure.CountChild > 0 Then
                            INDsleType.Properties.ReadOnly = True
                        End If
                        If INDviewDetail.RowCount > 0 Then
                            INDsleType.Properties.ReadOnly = True
                            INDsleBudgetControl.Properties.ReadOnly = True
                        End If
                    End Using
                Else
                    'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    'Me.Code = String.Empty
                    AsyncLoader(False)
                    If Me._sequense.IsManual Then
                        Me.NewPrivateBudgetItemsStructure()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Me.Code = String.Empty
                        Deshacer()
                        INDbtnCode.Focus()
                    End If
                End If
            Else
                'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                'Me.Code = String.Empty.Trim
                AsyncLoader(False)
                If Me._sequense.IsManual Then
                    Me.NewPrivateBudgetItemsStructure()
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                    Me.Code = String.Empty
                    Deshacer()
                    INDbtnCode.Focus()
                End If
            End If
        End Using
        INDlyRoot.EndUpdate()
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewPrivateBudgetItemsStructure() As Task
        Me.PrivateBudgetItemsStructure = New PrivateBudgetItemsStructure()
        If Me._sequense.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail(0).Id
            ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me.Sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
                    Me._idCurrentSequense = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
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
                        Using model As New ModelBaseBudget(CStr(Me.Tag))
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
        End If
    End Function

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.PrivateBudgetItemsStructure IsNot Nothing AndAlso Me.PrivateBudgetItemsStructure.Id > 0 Then
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

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        PrivateBudgetItemsStructure = Nothing
        record = Nothing
        _idOperativeUnit = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        ListTypes = Nothing
        ListUnBudgetValues = Nothing
        ListPurchaseOrderControl = Nothing
        ListPeriodicity = Nothing
        ListAllowExceedBudget = Nothing
        ListBudgetControl = Nothing
        ListPrivateBudgetItemsStructureDetail = Nothing
        ListDeletePrivateBudgetItemsStructureDetail = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddPrivateBudgetItemsStructure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PPrivateBudgetItemsStructure(Me)
        Presenter.GetSequense()
        Presenter.LoadDefinitionLayout()
        InitializeTuples()
        LoadStatus()

        Dim listActions As New List(Of eAcciones)
        listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewDetail, listActions)

        If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
            Deshacer()
            If EditMode = 1 Then 'Agrega nivel
                ParentId = PivotParentId
                INDsleParent.Properties.NullText = PivotParentDescription
            End If
        Else
            LoadControls()
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddPrivateBudgetItemsStructure_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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
            If _sequense Is Nothing OrElse _sequense.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequense.IsManual Then
                If Not String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.LoadControls()
                End If
            Else
                If String.IsNullOrEmpty(INDbtnCode.Text.Trim()) Then
                    Await Me.NewPrivateBudgetItemsStructure()
                Else
                    Await Me.LoadControls()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar las teclas en el popup control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceAddDetail.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceAddDetail.ShowPopup()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se ejecuta al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddPrivateBudgetItemsStructure_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el padre
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleParent_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleParent.QueryPopUp
        If ParentXpo Is Nothing Then
            Presenter.InitializeParent(PrivateBudgetItemsStructure)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar la cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMainAccount_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccount.QueryPopUp
        If MainAccountXpo Is Nothing Then
            Presenter.InitializeMainAccount(BudgetControl)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el tercero
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If ThirdPartyXpo Is Nothing Then
            Presenter.InitializeThirdParty()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCostCenter.QueryPopUp
        If CostCenterXpo Is Nothing Then
            Presenter.InitializeCostCenter()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If Type <> 0 Then
            If Type = 1 Then 'Si el tipo es grupo se ocultan los layoutGroup
                INDlygAccountInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                ActionsValidateControls = True
            Else 'Si el tipo es rubro se muestra el layoutGroup

                'Si el tipo de ventas se ocultan los siguiente controles
                If Type = 2 Then 'Venta
                    INDlyItemPurchaseOrderControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPurchaseOrderControl.AllowHide = True
                    INDlyItemExecutionAlertPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemExecutionAlertPercentage.AllowHide = True
                    INDlyItemAllowExceedBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemAllowExceedBudget.AllowHide = True
                Else 'Gasto
                    INDlyItemPurchaseOrderControl.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemPurchaseOrderControl.AllowHide = False
                    INDlyItemExecutionAlertPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemExecutionAlertPercentage.AllowHide = False
                    INDlyItemAllowExceedBudget.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemAllowExceedBudget.AllowHide = False
                End If

                INDlygAccountInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlygGeneralInformation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                ActionsValidateControls = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleAllowExceedBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleAllowExceedBudget.EditValueChanged
        If AllowExceedBudget IsNot Nothing Then
            If AllowExceedBudget Then
                INDlyItemExceedBudgetPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemExceedBudgetPercentage.AllowHide = False
            Else
                INDlyItemExceedBudgetPercentage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemExceedBudgetPercentage.AllowHide = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el control presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleBudgetControl_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetControl.EditValueChanged
        If BudgetControl IsNot Nothing Then
            INDpceAddDetail.Properties.ReadOnly = False
            MainAccountXpo = Nothing
            Select Case BudgetControl
                Case 1 'Ninguno
                    INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    GridColumn10.Visible = False
                    GridColumn11.Visible = False
                Case 2 'Tercero
                    INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    GridColumn10.Visible = True
                    GridColumn11.Visible = False
                    GridColumn10.VisibleIndex = 1
                Case 3 'Centro costo
                    INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    GridColumn10.Visible = False
                    GridColumn11.Visible = True
                    GridColumn11.VisibleIndex = 1
                Case 4, 5 '4. Centro costo/Tercero ó 5. Tercero/Centro costo
                    INDlyItemThirdParty.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemCostCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    GridColumn10.Visible = True
                    GridColumn11.Visible = True
                    GridColumn10.VisibleIndex = 1
                    GridColumn11.VisibleIndex = 2
            End Select
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceAddDetail_Popup(sender As Object, e As EventArgs) Handles INDpceAddDetail.Popup
        INDsleMainAccount.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de cuenta contable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMainAccount_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccount.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeMainAccount(BudgetControl)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de centro costo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(517, Nothing, True)
            Presenter.InitializeCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de terceros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleThirdParty.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(532, Nothing, True)
            Presenter.InitializeThirdParty()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddDetail_Click(sender As Object, e As EventArgs) Handles INDbtnAddDetail.Click
        AddDetail()
    End Sub

#End Region

#Region "ContextMenu"
    ''' <summary>
    ''' Evento para eliminar un detalle de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteDetail()
    End Sub
    ''' <summary>
    ''' Evento para eliminar un detalle de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteDetail()
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
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BudgetSequenceDetail IsNot Nothing Then
            If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

#End Region

End Class