Imports Presentation.Taxes.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.Windows.Forms
Imports System.IO
Imports System.ComponentModel

Public Class FrmLoadPlaneCollection
    Implements ILoadPlaneCollection

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.


    End Sub

    Private WithEvents timer As Timers.Timer

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(Me.Tag)
        BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Validar) = False
        BarraBotones.OcultarBotonesSinPermisos(Base.EbuttonsWithoutPermission.Deshacer) = False
    End Sub

    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        INDGcDetail.DataSource = Nothing
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        INDLcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDPbcProcess.EditValue = 0
        INDTxtProgress.EditValue = String.Empty
        DataCount = 0
        listDataReturn = Nothing
    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements Base.IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    Public WriteOnly Property ActionsOnControls As Boolean Implements ILoadPlaneCollection.ActionsOnControls
        Set(value As Boolean)
            INDGcDetail.Enabled = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements ILoadPlaneCollection.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    Public ReadOnly Property MyTag As Object Implements ILoadPlaneCollection.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Dim listDataReturn As List(Of Tuple(Of Integer, String, String, String))


    Private Async Sub BarraBotones_Click_Validar() Handles BarraBotones.Click_Validar
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
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
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False
                listData = Nothing
                Exit Sub
            End Try


            DataCount = listData.Count
            INDPbcProcess.Properties.Step = 1
            INDPbcProcess.Properties.PercentView = True
            INDPbcProcess.Properties.Maximum = DataCount
            INDPbcProcess.Properties.Minimum = 0
            INDLcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Using model As New MLoadPlaneCollection(MyTag)
                Timer1.Start()
                Dim result = Await model.ValidateLoadPlaneCollection(listData)
                listData = Nothing
                INDLcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Timer1.Stop()
                Select Case result.StatusCode
                    Case Domain.Base.Entities.eStatusResult.SUCCESS
                        listDataReturn = result.ObjectEmbbeded
                        INDGcDetail.DataSource = listDataReturn
                        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False

                        If listDataReturn.FindAll(Function(x) x.Item1 = 2 Or x.Item1 = 3).Count = 0 Then
                            If MessageIndigo.Show("El archivo no presento errores, desea confirmarlo", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                                Confirm()
                            End If
                        End If
                    Case Domain.Base.Entities.eStatusResult.WARNING
                        Deshacer()
                        Mensaje(EeventViewerImages.Advertencia) = result.Message
                    Case Domain.Base.Entities.eStatusResult.EXCEPTION
                        Deshacer()
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                End Select
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False

            End Using
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
    End Sub

    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If MessageIndigo.Show("Solo se confirmaran los registros sin errores, desea confirmar el archivo", MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Confirm()
        End If
    End Sub

    Dim DataCount As Integer

    Private Async Sub Confirm()
        Using model As New MLoadPlaneCollection(MyTag)
            Dim listData = (From d In listDataReturn Where d.Item1 = 1 Select d.Item4).ToList()
            DataCount = listData.Count
            INDPbcProcess.EditValue = 0
            INDPbcProcess.Properties.Maximum = DataCount
            Timer1.Start()
            INDLcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Dim result = Await model.SaveLoadPlaneCollection(listData)
            Timer1.Stop()
            Select Case result.StatusCode
                Case Domain.Base.Entities.eStatusResult.SUCCESS
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    Deshacer()
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Validar) = False
                Case Domain.Base.Entities.eStatusResult.WARNING
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                Case Domain.Base.Entities.eStatusResult.EXCEPTION
                    Mensaje(EeventViewerImages.MensajeError) = result.Message
            End Select
            INDLcgProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = False

        End Using

    End Sub

    

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Using model As New MLoadPlaneCollection(MyTag)
            Dim count = model.GetCountResultLoadPlaneCollection()
            If count = 0 Then
                INDTxtProgress.EditValue = "Iniciando el Proceso de Validación..."
                Exit Sub
            End If
            If count = DataCount Then
                Timer1.Stop()
                If INDPbcProcess.InvokeRequired Then
                    INDPbcProcess.BeginInvoke(Sub()
                                                  INDTxtProgress.EditValue = "Finalizando el proceso..."
                                                  INDPbcProcess.EditValue = DataCount
                                                  INDPbcProcess.PerformStep()
                                              End Sub)
                Else
                    INDTxtProgress.EditValue = "Finalizando el proceso..."
                    INDPbcProcess.EditValue = DataCount
                    INDPbcProcess.PerformStep()

                End If
            Else
                If INDPbcProcess.InvokeRequired Then
                    INDPbcProcess.BeginInvoke(Sub()
                                                  INDTxtProgress.EditValue = "Items procesados " & count.ToString() & " de " & DataCount.ToString
                                                  If count > INDPbcProcess.EditValue Then
                                                      INDPbcProcess.EditValue = count
                                                      INDPbcProcess.PerformStep()
                                                  End If

                                              End Sub)
                Else
                    INDTxtProgress.EditValue = "Items procesados " & count.ToString() & " de " & DataCount.ToString
                    If count > INDPbcProcess.EditValue Then
                        INDPbcProcess.EditValue = count
                        INDPbcProcess.PerformStep()
                    End If
                End If
            End If
        End Using
    End Sub
End Class