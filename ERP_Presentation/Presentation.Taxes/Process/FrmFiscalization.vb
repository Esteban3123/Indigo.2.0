'***********************************************************************
' Assembly         : Presentacion.Taxes
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2016
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports System.Drawing
Imports Presentation.Base.BaseClass
Imports System.Text
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports Presentation.Taxes.MVP
Imports System.Windows.Forms
Imports System.IO

#End Region

Public Class FrmFiscalization
    Implements IFiscalization

#Region "Properties"

    ''' <summary>
    ''' Layout del form
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFiscalization.MyLayoutControl
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
    Public ReadOnly Property MyTag As Object Implements IFiscalization.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Año de liquidación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property Year As Integer Implements IFiscalization.Year
        Get
            Return CtrDateNavigator1.GetYear
        End Get
    End Property

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

#End Region

#Region "Variables"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Taxes"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PFiscalization

    ''' <summary>
    ''' Contador de registros
    ''' </summary>
    ''' <remarks></remarks>
    Dim DataCount As Integer

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
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga las rejillas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInformation()
        If INDpceLoad.Properties.NullText = String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe realizar primero el cargue de la base gravable DIAN"
            Exit Sub
        End If
        Try
            AsyncLoader(True)

            INDgcPrivatePublic.DataSource = Nothing

            INDgcDian.DataSource = Nothing
            INDgcDian.DataSource = Presenter.ListViewDifferenceDIANAndPrivateLiquidation(Year)

            INDgcReteICA.DataSource = Nothing
            INDgcReteICA.DataSource = Presenter.ListViewDiscountReteIcaAndPrivateDeclaration(Year)

            INDgcOmissive.DataSource = Nothing
            INDgcOmissive.DataSource = Presenter.ListViewOmissives(Year)

            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Abre el cuadro de dialogo para importar el archivo de la DIAN
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function OpenFileDialogDIAN() As Task
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        If openFileDialog1.ShowDialog() <> System.Windows.Forms.DialogResult.OK Then
            Exit Function
        End If

        Dim listData As New List(Of String)
        Try

            Using fs As New FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using sr As New StreamReader(fs)
                    While (sr.Peek() >= 0)
                        listData.Add(sr.ReadLine())
                    End While
                    sr.Close()
                End Using
                fs.Close()
            End Using
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo"
            listData = Nothing
            Exit Function
        End Try

        DataCount = listData.Count
        INDPbcProcess.Properties.Step = 1
        INDPbcProcess.Properties.PercentView = True
        INDPbcProcess.Properties.Maximum = DataCount
        INDPbcProcess.Properties.Minimum = 0
        INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Using model As New MFiscalization(MyTag)
            Timer1.Start()
            Dim result = Await model.SP_ValidateTaxBase(listData, Year)
            listData = Nothing
            INDlyItemProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Timer1.Stop()
            Select Case result.StatusCode
                Case Domain.Base.Entities.eStatusResult.SUCCESS
                    INDgcLoad.DataSource = result.ObjectEmbbeded
                    INDpceLoad.Properties.NullText = openFileDialog1.FileName
                Case Domain.Base.Entities.eStatusResult.WARNING
                    Deshacer()
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                Case Domain.Base.Entities.eStatusResult.EXCEPTION
                    Deshacer()
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
            End Select
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False

        End Using

    End Function

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDpceLoad.Properties.NullText = String.Empty
        INDgcLoad.DataSource = Nothing
        INDgcPrivatePublic.DataSource = Nothing
        INDgcDian.DataSource = Nothing
        INDgcOmissive.DataSource = Nothing
        INDgcReteICA.DataSource = Nothing
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)

    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Presenter = Nothing
        DataCount = Nothing
    End Sub
    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFiscalization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyFiscalization, True)
        IndigoGridControl1.AcceptXPO = True
        Me.indigo = SessionValues.Instance
        Presenter = New PFiscalization(Me)
        Deshacer()
        CtrDateNavigator1.HowShowControl = CtrDateNavigator.EHowShowControl.OnlyYear
        IndigoGridControl1.RefreshGrid(INDgcPrivatePublic)
        IndigoGridControl1.RefreshGrid(INDgcDian)
        IndigoGridControl1.RefreshGrid(INDgcOmissive)
        IndigoGridControl1.RefreshGrid(INDgcReteICA)
        IndigoGridControl1.RefreshGrid(INDgcLoad)
        TabbedControlGroup1.SelectedTabPageIndex = 1
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form dialog para importar el archivo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceLoad_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDpceLoad.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis Then
            If viewPrivatePublic.RowCount > 0 OrElse viewDian.RowCount > 0 OrElse viewOmissive.RowCount > 0 OrElse viewReteIca.RowCount > 0 Then
                If MessageIndigo.Show("Al volver a cargar otro archivo se perderán los datos en las rejillas, Desea continuar?", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If
            OpenFileDialogDIAN()
        End If
    End Sub

#End Region

#Region "Tick"

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim count = Presenter.GetCountTempFile()
        If count = 0 Then
            'INDTxtProgress.EditValue = "Iniciando el Proceso de Validación..."
            Exit Sub
        End If
        If count = DataCount Then
            Timer1.Stop()
            If INDPbcProcess.InvokeRequired Then
                INDPbcProcess.BeginInvoke(Sub()
                                              'INDTxtProgress.EditValue = "Finalizando el proceso..."
                                              INDPbcProcess.EditValue = DataCount
                                              INDPbcProcess.PerformStep()
                                          End Sub)
            Else
                'INDTxtProgress.EditValue = "Finalizando el proceso..."
                INDPbcProcess.EditValue = DataCount
                INDPbcProcess.PerformStep()

            End If
        Else
            If INDPbcProcess.InvokeRequired Then
                INDPbcProcess.BeginInvoke(Sub()
                                              'INDTxtProgress.EditValue = "Items procesados " & count.ToString() & " de " & DataCount.ToString
                                              If count > INDPbcProcess.EditValue Then
                                                  INDPbcProcess.EditValue = count
                                                  INDPbcProcess.PerformStep()
                                              End If

                                          End Sub)
            Else
                'INDTxtProgress.EditValue = "Items procesados " & count.ToString() & " de " & DataCount.ToString
                If count > INDPbcProcess.EditValue Then
                    INDPbcProcess.EditValue = count
                    INDPbcProcess.PerformStep()
                End If
            End If
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento click del boton generar consulta
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnGenerate_Click(sender As Object, e As EventArgs) Handles INDbtnGenerate.Click
        TabbedControlGroup1.SelectedTabPageIndex = 1
        LoadInformation()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
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

#End Region

End Class