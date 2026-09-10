'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 06/01/2021
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Threading
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.MixingStation.MVP
#End Region

Public Class FrmAddMedicinesProduction

#Region "Event"
    ''' <summary>
    ''' Evento para agregar un Medicamento
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event AddMedicinesProductionArgs(sender As Object, e As EventArgs)

#End Region

#Region "Variables"
    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Private Presenter As PMedicinesProduction

    ''' <summary>
    ''' Id de la central de mezclas
    ''' </summary>
    Public CMConfigurationId As Integer

    ''' <summary>
    ''' Representa la entidad del centro de mezclas
    ''' </summary>
    Public _MedicinesProduction As New MedicinesProduction

    ''' <summary>
    ''' DataSource del centro de Atención
    ''' </summary>
    Public _DataSource As List(Of ViewListCMCenterAttentionXpo)

    ''' <summary>
    ''' Define si el formulario esta editando o no
    ''' </summary>
    ''' <remarks></remarks>
    Private _editModeForm As Boolean

#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' propiedad para establecer si se va a editar un registro 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property EditMode As Boolean
        Set(value As Boolean)
            _editModeForm = value
        End Set
    End Property

    ''' <summary>
    ''' Aciones sobre los Controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean
        Set(value As Boolean)
            If Not _editModeForm Then
                INDlyRoot.BeginUpdate()
                INDsleUnitDoseType.Enabled = value
                If value Then
                    INDsleUnitDoseType.Focus()
                Else
                    INDsleATC.Focus()
                End If
                INDlyRoot.EndUpdate()
            End If
        End Set
    End Property

#End Region

#Region "Methods and Functions"
    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que asigna los valores
    ''' </summary>
    Private Sub AssigningValues()
        With _MedicinesProduction
            .CMConfigurationId = CMConfigurationId
            .ATCId = INDsleATC.EditValue
            .UnitDoseTypeId = INDsleUnitDoseType.EditValue
            .Status = True
            .CenterAttentionId = INDsleCenterAttention.EditValue
            .AllowsRemnant = INDCtrAllowsRemant.EditValue
        End With
    End Sub

    ''' <summary>
    ''' Guardar un medicamento en producción 
    ''' </summary>
    Public Async Sub Guardar()
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Me.AsyncLoader(True)
        Try
            Using model As New MMedicinesProduction(Me.Tag.ToString())
                Dim result = Await model.SaveMedicinesProduction(Me._MedicinesProduction)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Me.AsyncLoader(False)
                    Deshacer()
                    RaiseEvent AddMedicinesProductionArgs(Nothing, Nothing)
                    Me.Close()
                Else
                    Me.AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Me.AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    ''' <summary>
    ''' Carga los datos del medicamento en el formulario
    ''' </summary>
    Private Sub LoadControls()
        If _MedicinesProduction IsNot Nothing Then
            With _MedicinesProduction
                INDsleATC.EditValue = .ATCId
                INDsleUnitDoseType.EditValue = .UnitDoseTypeId
                INDsleCenterAttention.EditValue = .CenterAttentionId
                INDCtrAllowsRemant.EditValue = .AllowsRemnant
                .Status = True
            End With
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlyRoot.BeginUpdate()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        INDsleATC.EditValue = Nothing
        INDsleUnitDoseType.EditValue = Nothing
        INDsleCenterAttention.EditValue = Nothing
        _editModeForm = False
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        ActionsOnControls = False
        INDlyRoot.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que llena el datasource del label de tipos de dosis unitarias
    ''' </summary>
    Private Sub DataSourceUnitDoseTytpe()
        INDsleUnitDoseType.Properties.DataSource = Nothing
        If Not String.IsNullOrEmpty(INDsleCenterAttention.EditValue) Then
            ActionsOnControls = True

            Using model As New MMedicinesProduction(Me.Tag)
                Dim getItem = model.GetProductionLinesByCMCenterAttention(INDsleCenterAttention.EditValue)

                If getItem IsNot Nothing Then

                    Dim result = Presenter.InitializeUnitDoseTypeByProductionLine(getItem)
                    If result IsNot Nothing Then
                        Dim res = result.Select(Function(m) New With {
                                                 Key .UnitDoseTypeId = m.Id_UnitDoseType.Id,
                                                 Key .Code = m.Id_UnitDoseType.Code,
                                                 Key .Description = m.Id_UnitDoseType.Description,
                                                 Key .CodeDescription = m.Id_UnitDoseType.CodeDescription
                                             }).Distinct()?.ToList()

                        INDsleUnitDoseType.Properties.DataSource = res
                    End If
                End If
            End Using
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMedicinesProduction_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyRoot, True)
        Me.indigo = SessionValues.Instance
        Presenter = New PMedicinesProduction()
        LoadStatus()

        If _editModeForm Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "Shown"
    ''' <summary>
    ''' Se ejecuta al pintarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmMedicinesProduction_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        ActionsOnControls = False
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Abre el form de central de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATC.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(343, Nothing, True)
            Using Model As New MBusqueda
                INDsleATC.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListATC)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de centros de atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCenterAttention_ButtonClick_1(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCenterAttention.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            ActionsOnControls = False
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de dosois unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUnitDoseType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2067, Nothing, True)
            DataSourceUnitDoseTytpe()
        End If
    End Sub

#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Datasource Atc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATC.QueryPopUp
        If INDsleATC.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleATC.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListATC)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Datasource centros de atención por Central de Mezclas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCenterAttention_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCenterAttention.QueryPopUp
        If INDsleCenterAttention.Properties.DataSource Is Nothing Then
            _DataSource = Presenter.ListCenterattention(CMConfigurationId)
            If _DataSource Is Nothing OrElse _DataSource.Count = 0 Then
                INDsleCenterAttention.Properties.DataSource = Nothing
                Me.Mensaje(EeventViewerImages.Advertencia) = "No existen centros de atención asociado a la central de mezclas seleccionada"
                Exit Sub
            End If
            INDsleCenterAttention.Properties.DataSource = _DataSource
        End If
    End Sub

#End Region


#Region "EditValueChanged"
    ''' <summary>
    ''' Se dispara al cambiar el valor del control de centro atención
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCenterAttention_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCenterAttention.EditValueChanged
        DataSourceUnitDoseTytpe()
    End Sub

#End Region

#End Region

#Region "BarButton Events"
    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub Deshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

#End Region

End Class