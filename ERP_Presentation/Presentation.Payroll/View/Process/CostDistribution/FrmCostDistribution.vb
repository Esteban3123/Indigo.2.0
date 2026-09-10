'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 29-07-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Payroll.MVP
Imports Domain.Entities


Public Class FrmCostDistribution
    Implements ICostDistribution

#Region "Global Variables"
    ''' <summary>
    ''' variable para controlar el presentador del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PCostDistribution

    ''' <summary>
    ''' variable para controlar el modelo del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MCostDistribution

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordPayroll
#End Region

#Region "Properties"

    ''' <summary>
    ''' Establece el Datasource de Groups
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Datasource_Groups As Object Implements ICostDistribution.Datasource_Groups
        Set(value As Object)
            INDsleGroup.Properties.DataSource = value
        End Set
    End Property
#End Region
#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        DeleteBlockedRecord()
        presenter = Nothing
        model = Nothing
        PathFunctionalDefinitions = Nothing
    End Sub

    Private Sub FrmCostDistribution_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        '******************************'
        'Me._funct = AddressOf GenerateDoc
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloCostDistribution.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If

        presenter = New PCostDistribution(Me)
        presenter.Initializes()
    End Sub
#End Region

#Region "Bar buttons events"
    ''' <summary>
    ''' Evento que se ejecuta al cargar la barra botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(Me.Tag)
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Private Sub Frm_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDsleGroup.Enabled Then
            INDsleGroup.Focus()
        End If
    End Sub
#End Region

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' Método para generar el registro de bloqueo del grupo
    ''' </summary>
    ''' <param name="groupId">Id del grupo a bloquear</param>
    ''' <returns>True si el bloqueo se creó exitosamente, False si ya está bloqueado por otro usuario</returns>
    ''' <remarks></remarks>
    Private Async Function GenerateBlockRecord(groupId As Integer) As Task(Of Boolean)
        Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
            Dim result = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), groupId.ToString())
            If result Is Nothing OrElse result.Id = 0 Then
                Me.BarraBotones.ShowXtraMessage(String.Empty, ImagesXtraLabel.Warning, indigo.UserIndigo)
                ' No está bloqueado, crear bloqueo
                Dim state = New ObjectChangeTracker
                state.State = ObjectState.Added
                _record = New BlockRecordPayroll With {
                    .BlockDate = Date.Now,
                    .ChangeTracker = state,
                    .NameUser = indigo.UserIndigoName,
                    .FormId = Me.Tag,
                    .CodUser = indigo.UserIndigo,
                    .RecordId = groupId
                }
                Dim operation = Await ModelRecord.SaveBlockRecord(_record)
                If operation IsNot Nothing AndAlso operation.StateResult Then
                    _record = operation.ObjectEmbbeded
                    Return True
                Else
                    Return False
                End If
            Else
                ' Ya está bloqueado por otro usuario
                _record = result
                If result.CodUser.Equals(indigo.UserIndigo) Then
                    Return True
                Else
                    ' Es otro usuario, mostrar mensaje de error
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    Return False
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Método que elimina el registro guardado para concurrencia
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(indigo.UserIndigo) Then
            Using ModelRecord As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                Await ModelRecord.DeleteBlockRecord(_record)
            End Using
            _record = Nothing
        End If
    End Sub

    Private Async Sub INDsleGroup_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleGroup.EditValueChanged
        ' Eliminar bloqueo anterior si existe
        DeleteBlockedRecord()
        INDgleLastLiquidationDate.Properties.DataSource = Nothing
        If INDsleGroup.EditValue IsNot Nothing AndAlso CStr(INDsleGroup.EditValue) <> "" Then
            Using modelLiquidation As New MPayrollLiquidation
                AsyncLoader(True)
                Dim _dates = Await modelLiquidation.GetLiquidationDatesByGroup(INDsleGroup.EditValue)
                AsyncLoader(False)
                If _dates IsNot Nothing AndAlso _dates.Count > 0 Then
                    INDgleLastLiquidationDate.Properties.DataSource = _dates
                End If
            End Using
            Dim groupId As Integer = CInt(INDsleGroup.EditValue)
            Dim blockResult = Await GenerateBlockRecord(groupId)
            If Not blockResult Then
                ' El grupo está bloqueado por otro usuario, limpiar la selección
                INDsleGroup.EditValue = Nothing
                INDgleLastLiquidationDate.Properties.DataSource = Nothing
            End If
        End If
    End Sub

    Private Async Sub INDProcessButton_Click(sender As Object, e As EventArgs) Handles INDProcessButton.Click
        Try
            If INDsleGroup.EditValue IsNot Nothing Then
                Dim groupId As Integer = CInt(INDsleGroup.EditValue)
                Dim blockResult = Await GenerateBlockRecord(groupId)

                If Not blockResult Then
                    Return
                End If
            End If

            Using model As New MCostDistribution
                AsyncLoader(True)
                Dim CostDistribution As ActionResult(Of List(Of String)) = Await model.GenerateCostDistributionAsync(INDsleGroup.EditValue, INDgleLastLiquidationDate.EditValue)
                AsyncLoader(False)
                If CostDistribution.StateResult = True Then
                    If CostDistribution.MessageResult IsNot Nothing Then
                        Dim frmMessage As FrmAlertCostDistribution = New FrmAlertCostDistribution()
                        frmMessage.StartPosition = FormStartPosition.CenterScreen
                        frmMessage.INDGCMessage.DataSource = CostDistribution.MessageResult
                        frmMessage.INDLciLabelMensaje.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                        Dim frmTransparent As New FrmTransparent(frmMessage, False)

                        frmTransparent.ShowDialog()
                    End If
                Else
                    If CostDistribution.MessageResult IsNot Nothing Then
                        Dim frmMessage As FrmAlertCostDistribution = New FrmAlertCostDistribution()
                        frmMessage.StartPosition = FormStartPosition.CenterScreen
                        frmMessage.INDGCMessage.DataSource = CostDistribution.MessageResult
                        frmMessage.INDLciLabelMensaje.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        Dim frmTransparent As New FrmTransparent(frmMessage, False)

                        frmTransparent.ShowDialog()
                    ElseIf CostDistribution.Message IsNot Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = CostDistribution.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
            Throw ex
        End Try

    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
End Class