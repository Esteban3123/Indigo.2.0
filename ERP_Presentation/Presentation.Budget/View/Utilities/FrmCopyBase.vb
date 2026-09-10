'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/09/2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls
#End Region

Public Class FrmCopyBase
    Implements ICopyBase

#Region "Properties"

    ''' <summary>
    ''' Actualizar rubros
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateParameterizedCategories As Boolean? Implements ICopyBase.UpdateParameterizedCategories
        Get
            Return INDsleUpdateParameterizedCategories.EditValue
        End Get
        Set(value As Boolean?)
            INDsleUpdateParameterizedCategories.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Actualizar dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UpdateParameterizedDependencies As Boolean? Implements ICopyBase.UpdateParameterizedDependencies
        Get
            Return INDsleUpdateParameterizedDependencies.EditValue
        End Get
        Set(value As Boolean?)
            INDsleUpdateParameterizedDependencies.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Rubros de gastos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryExpense As Boolean? Implements ICopyBase.CategoryExpense
        Get
            Return INDsleCategoryExpense.EditValue
        End Get
        Set(value As Boolean?)
            INDsleCategoryExpense.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Rubros de Ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryIncome As Boolean? Implements ICopyBase.CategoryIncome
        Get
            Return INDsleCategoryIncome.EditValue
        End Get
        Set(value As Boolean?)
            INDsleCategoryIncome.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Conceptos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Concepts As Boolean? Implements ICopyBase.Concepts
        Get
            Return INDsleConcepts.EditValue
        End Get
        Set(value As Boolean?)
            INDsleConcepts.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Dependencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Dependency As Boolean? Implements ICopyBase.Dependency
        Get
            Return INDsleDependency.EditValue
        End Get
        Set(value As Boolean?)
            INDsleDependency.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipos de Gastos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ExpenseType As Boolean? Implements ICopyBase.ExpenseType
        Get
            Return INDsleExpenseType.EditValue
        End Get
        Set(value As Boolean?)
            INDsleExpenseType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipos de ingresos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IncomeType As Boolean? Implements ICopyBase.IncomeType
        Get
            Return INDsleIncomeType.EditValue
        End Get
        Set(value As Boolean?)
            INDsleIncomeType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Recursos
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Resource As Boolean? Implements ICopyBase.Resource
        Get
            Return INDsleResource.EditValue
        End Get
        Set(value As Boolean?)
            INDsleResource.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Entidad presupuestal destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityIdDestiny As Integer? Implements ICopyBase.BudgetEntityIdDestiny
        Get
            Return INDsleBudgetEntityDestiny.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntityDestiny.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Entidad presupuestal origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityIdSource As Integer? Implements ICopyBase.BudgetEntityIdSource
        Get
            Return INDsleBudgetEntitySource.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntitySource.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource entidad presupuestal origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpoSource As XPInstantFeedbackSource Implements ICopyBase.BudgetEntityXpoSource
        Get
            Return INDsleBudgetEntitySource.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntitySource.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource Entidad presupuestal destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpoDestiny As XPInstantFeedbackSource Implements ICopyBase.BudgetEntityXpoDestiny
        Get
            Return INDsleBudgetEntityDestiny.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBudgetEntityDestiny.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' LayoutControls
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICopyBase.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Tag del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ICopyBase.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Vigencia destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityIdDestiny As Integer? Implements ICopyBase.ValidityIdDestiny
        Get
            Return INDsleValidityDestiny.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidityDestiny.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Vigencia origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityIdSource As Integer? Implements ICopyBase.ValidityIdSource
        Get
            Return INDsleValiditySource.EditValue
        End Get
        Set(value As Integer?)
            INDsleValiditySource.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource vigencia destino
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpoDestiny As XPCollection Implements ICopyBase.ValidityXpoDestiny
        Get
            Return INDsleValidityDestiny.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidityDestiny.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource vigencia origen
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpoSource As XPCollection Implements ICopyBase.ValidityXpoSource
        Get
            Return INDsleValiditySource.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValiditySource.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador del form
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PCopyBase

    ''' <summary>
    ''' Representa a la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim CopyBase As CopyBase

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

#End Region

#Region "Methods"

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDlyPrincipal.BeginUpdate()
        'Origen
        BudgetEntityIdSource = Nothing
        ValidityIdSource = Nothing
        ValidityXpoSource = Nothing
        'Destino
        BudgetEntityIdDestiny = Nothing
        ValidityIdDestiny = Nothing
        ValidityXpoDestiny = Nothing
        'Objetos a copiar
        Resource = False
        Dependency = False
        Concepts = False
        IncomeType = False
        ExpenseType = False
        CategoryIncome = False
        CategoryExpense = False
        UpdateParameterizedDependencies = False
        UpdateParameterizedCategories = False

        INDlyPrincipal.EndUpdate()
        BarraBotones.CleanAuditBasic()
        BarraBotones.StatusRecordVisible = False
        CopyBase = Nothing
        INDsleBudgetEntitySource.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que valida que la vigencia de origen no
    ''' sea igual a la vigencia destino
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ValidateBudgetEntityAndValidity()
        If BudgetEntityIdSource IsNot Nothing AndAlso BudgetEntityIdDestiny IsNot Nothing _
            AndAlso ValidityIdSource IsNot Nothing AndAlso ValidityIdDestiny IsNot Nothing Then
            'Se valida que la vigencia no sea la misma tanto para el origen como para el destino
            'siempre y cuando la entidad presupuestal sea la misma
            If BudgetEntityIdSource = BudgetEntityIdDestiny AndAlso ValidityIdSource = ValidityIdDestiny Then
                Mensaje(EeventViewerImages.Advertencia) = "La vigencia destino no puede ser igual a la vigencia origen."
                ValidityIdDestiny = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        CopyBase = New CopyBase
        With CopyBase
            .BudgetEntityIdSource = BudgetEntityIdSource
            .ValidityIdSource = ValidityIdSource
            .BudgetEntityIdDestiny = BudgetEntityIdDestiny
            .ValidityIdDestiny = ValidityIdDestiny
            .Resource = Resource
            .Dependency = Dependency
            .Concepts = Concepts
            .IncomeType = IncomeType
            .ExpenseType = ExpenseType
            .CategoryIncome = CategoryIncome
            .CategoryExpense = CategoryExpense
            .UpdateParameterizedDependencies = UpdateParameterizedDependencies
            .UpdateParameterizedCategories = UpdateParameterizedCategories
        End With
    End Sub

    ''' <summary>
    ''' Valida que hayan escogido al menos un item en true
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsBoolean() As Boolean
        If Resource = False AndAlso Dependency = False AndAlso Concepts = False AndAlso IncomeType = False AndAlso ExpenseType = False AndAlso CategoryIncome = False AndAlso CategoryExpense = False AndAlso UpdateParameterizedDependencies = False AndAlso UpdateParameterizedCategories = False Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo que abre el form de errores de la importación
    ''' </summary>
    ''' <param name="ListErrors"></param>
    ''' <remarks></remarks>
    Private Sub OpenErrors(ListErrors As List(Of Tuple(Of String, Integer)))
        Me.Cursor = ChangeCursorIndigo()
        Using Formulario As New FrmListErrors(ListErrors)
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 920
            Formulario.Height = 600
            Dim frm As New FrmTransparent(Formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            frm.ShowDialog(Me)
        End Using
    End Sub

#End Region

#Region "ICrudBase"
    ''' <summary>
    ''' Metodo buscar sin usarse
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Deshace lo que se haya hecho
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlySave)
    End Sub
    ''' <summary>
    ''' Metodo eliminar si usarse
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Guarda la copia de presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        If ValidateControlsBoolean() = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedOneObjectToCopy", NAME_MODULE)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using model As New MCopyBase(Me.Tag.ToString())
                AsyncLoader(True)
                Dim Result = Await model.SaveCopyBase(CopyBase)
                If Result.StateResult = True Then

                    'Se envia listErrors al form de errores
                    If Result.ObjectEmbbeded.ListErrors IsNot Nothing AndAlso Result.ObjectEmbbeded.ListErrors.Count > 0 Then
                        OpenErrors(Result.ObjectEmbbeded.ListErrors)
                    End If

                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("BudgetCopySatisfactory", NAME_MODULE)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    AsyncLoader(False)
                    If Result.Message = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Metodo actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
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
    ''' <summary>
    ''' Metodo para crear nuevas Copybase, sin usarse
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub
    ''' <summary>
    ''' Metodo para abrir busqueda en el visor, sin usarse
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Libera la memoria del frm al cerrar lo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        CopyBase = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCopyBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyPrincipal, True)
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        Presenter = New PCopyBase(Me)
        '******************************

        Deshacer()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control entidad presupuestal origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntitySource_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetEntitySource.QueryPopUp
        If BudgetEntityXpoSource Is Nothing Then
            BudgetEntityXpoSource = Presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control entidad presupuestal destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntityDestiny_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetEntityDestiny.QueryPopUp
        If BudgetEntityXpoDestiny Is Nothing Then
            BudgetEntityXpoDestiny = Presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de vigencia origen
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValiditySource_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleValiditySource.QueryPopUp
        If ValidityXpoSource Is Nothing AndAlso BudgetEntityIdSource IsNot Nothing Then
            ValidityXpoSource = Presenter.InitializeValidity(BudgetEntityIdSource)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de vigencia destino
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidityDestiny_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleValidityDestiny.QueryPopUp
        If ValidityXpoDestiny Is Nothing AndAlso BudgetEntityIdDestiny IsNot Nothing Then
            ValidityXpoDestiny = Presenter.InitializeValidity(BudgetEntityIdDestiny)
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntitySource_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntitySource.ButtonClick, INDsleBudgetEntityDestiny.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
            BudgetEntityXpoSource = Presenter.InitializeBudgetEntity()
            BudgetEntityXpoDestiny = Presenter.InitializeBudgetEntity()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntitySource_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntitySource.EditValueChanged, INDsleBudgetEntityDestiny.EditValueChanged, INDsleValiditySource.EditValueChanged, INDsleValidityDestiny.EditValueChanged
        ValidateBudgetEntityAndValidity()
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Cuando se pintan las columnas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvValidity_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidity.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDColVStatus.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                Case 2
                    e.DisplayText = obtenerRecurso(Activa, Eform.BudgetEntities)
                Case 3
                    e.DisplayText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                Case Else

            End Select
        End If
    End Sub

    ''' <summary>
    ''' Cuando se pintan las columnas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvValidityDestiny_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidityDestiny.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDColVStatusDestiny.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                Case 2
                    e.DisplayText = obtenerRecurso(Activa, Eform.BudgetEntities)
                Case 3
                    e.DisplayText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                Case Else

            End Select
        End If
    End Sub

#End Region

#End Region

#Region "BarraBotones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir

    End Sub

#End Region

End Class