'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Jorge Leonardo Vernaza
' Created          : 06-04-2011
'
' Last Modified By : Faiber Julian Mora D.
' Last Modified On : 10-05-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Security.Entities
#End Region


''' <summary>
''' Clase que contiene todo el comportamiento de la vista
''' </summary>
Public Class FrmupdateDateconfirm
    Implements IUpdateDateConfirm, ICustomizableForm

#Region "Variable Globales Propiedades Intefaz y Load"

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MRadicateInvoice

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Variable de sesión
    ''' </summary>
    Private session As SessionValues

    ''' <summary>
    ''' Encapsula el consecutivo del radicado
    ''' </summary>
    ''' <remarks></remarks>
    Dim radicatedConsecutive As String

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Model = Nothing
        record = Nothing
        radicatedConsecutive = Nothing
    End Sub


    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrMCustomer_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.INDlycgRoot.AllowCustomizeChildren = True
        Me.INDlycRoot.AllowCustomization = True
        Me.INDlycRoot.RegisterUserCustomizationForm(GetType(Presentation.Controls.ToolsCustomizationForm))
        Me.INDlycRoot.RegisterCustomPropertyGridWrapper(GetType(DevExpress.XtraLayout.LayoutControlItem), GetType(Presentation.Controls.LayoutControlItemPropertyGridWrapper))
        Me._doc = Nothing
        Me.session = SessionValues.Instance
        Me.BarraBotones.StatusRecordVisible = True
        Me.ActionsOnControls = False
    End Sub

#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            Using Model As New MRadicateInvoice(Me.Tag)
                AsyncLoader(True)
                Dim Result = Await Model.UpdateConfirmDate(Me.INDbteNumberRadicate.Text, INDdateConfirm.EditValue)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                    Deshacer()
                Else
                    INDbteNumberRadicate.Enabled = False
                    If Result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.MessageResult(0).ToString
                    End If
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            INDbteNumberRadicate.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(ByVal value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Sub Buscar() Implements IcrudBase.Buscar
        Throw New NotImplementedException
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar
        Throw New NotImplementedException
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Metodo que abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Me.AbrirBusquedaConsecutive()
    End Sub

#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        INDbteNumberRadicate.EditValue = Nothing
        '  INDbteNumberRadicate.Properties.ReadOnly = True
        INDtxtUserConfirm.EditValue = Nothing
        INDLcUserConfirm.HideControl(True)
        INDdateConfirm.EditValue = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.DisableBarDocument()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            INDbteNumberRadicate.Enabled = Not value
            INDLcUserConfirm.HideControl(Not value)
            INDdateConfirm.Enabled = value
            INDdateConfirm.Properties.ReadOnly = value
            If value = True Then
                INDdateConfirm.Focus()
            Else
                INDbteNumberRadicate.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteNumberRadicate.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDbteNumberRadicate.Text.ToString) Then
                LoadControls()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Try
            Dim radicateC As RadicateInvoiceC
            Dim userConfirm As User
            Using Model As New MRadicateInvoice(509)
                If INDbteNumberRadicate.EditValue Is Nothing OrElse INDbteNumberRadicate.EditValue Is String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = "El consecutivo de cartera no se cargo correctamente"
                    Exit Sub
                End If
                AsyncLoader(True)
                radicateC = Await Model.GetRadicateInvoiceC(INDbteNumberRadicate.EditValue)
                If radicateC.Id = 0 Then
                    AsyncLoader(False)
                    INDbteNumberRadicate.Enabled = False
                    Mensaje(EeventViewerImages.Advertencia) = "El número del radicado no existe en el ERP"
                    Exit Sub
                End If
                INDbteNumberRadicate.EditValue = radicateC.RadicatedConsecutive
                INDdateConfirm.EditValue = radicateC.ConfirmDate
                'Verifica si tiene fecha de confirmación almacenada
                If radicateC.ConfirmDate IsNot Nothing Then
                    userConfirm = Await Model.GetUserById(radicateC.ConfirmUser)
                    If userConfirm Is Nothing OrElse userConfirm.Id = 0 Then
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Advertencia) = "La información del radicado no se cargo completamente"
                        CleanControls()
                        Exit Sub
                    End If
                    INDLcUserConfirm.HideControl(False)
                    INDtxtUserConfirm.EditValue = userConfirm.Person.Fullname
                    If MessageIndigo.Show("El radicado número " + Convert.ToString(radicateC.RadicatedConsecutive) + " tiene fecha de confirmación de " + radicateC.ConfirmDate.Value.ToString("MMMM dd \de yyyy") + ", desea modificar la fecha de confirmación?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        INDdateConfirm.Properties.ReadOnly = False
                    Else
                        INDdateConfirm.Properties.ReadOnly = True
                    End If
                Else
                    'habilita directamente el control de fecha para agregarla y oculta el de nombre de quien confirma
                    INDLcUserConfirm.HideControl(True)
                    INDtxtUserConfirm.EditValue = Nothing
                    INDdateConfirm.Enabled = True
                    INDdateConfirm.Properties.ReadOnly = False
                    INDdateConfirm.EditValue = Nothing
                End If
                AsyncLoader(False)
                INDbteNumberRadicate.Enabled = False
                Me.INDdateConfirm.Enabled = True
                Me.INDdateConfirm.Focus()
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteNumberRadicate.Enabled = False
            Throw ex
        End Try
    End Sub

#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.AbrirBusquedaConsecutive()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar

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
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    Private Sub INDbteNumberRadicate_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteNumberRadicate.ButtonClick
        Me.AbrirBusquedaConsecutive()
    End Sub

    ''' <summary>
    ''' Abrirs the busqueda consecutive.
    ''' </summary>
    Public Sub AbrirBusquedaConsecutive()
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValueObjection
        With FormSearchObjects
            .ValorSolicitado = "RadicatedConsecutive"
            .ListaColumnas = {
                New ColumnInfo With {.Caption = "Entidad", .FieldName = "NitName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                New ColumnInfo With {.Caption = "N° Factura", .FieldName = "InvoiceNumber", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.21)},
                New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)},
                New ColumnInfo With {.Caption = "N° Consecutivo", .FieldName = "RadicatedConsecutive", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.19)},
                New ColumnInfo With {.Caption = "Estado", .FieldName = "State", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.1)}}.ToList()
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListInvoiceRadicateConfirm
            BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            .FormParent = Me
            .ShowSearch(False)
        End With
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteNumberRadicate.Enabled Then
            INDbteNumberRadicate.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValueObjection(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        'INDbteNumberRadicate.Text = ReturnValue
        Me.radicatedConsecutive = ReturnValue
        INDbteNumberRadicate.EditValue = radicatedConsecutive
        LoadControls()
    End Sub


#End Region

End Class