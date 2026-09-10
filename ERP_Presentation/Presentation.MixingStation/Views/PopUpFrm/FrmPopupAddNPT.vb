Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

Public Class FrmPopupAddNPT
    Implements INPTConfiguration

    Dim Presenter As New PNPTConfiguration(Me)

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements INPTConfiguration.MyLayoutControl
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements INPTConfiguration.ActionsOnControls
        Set(value As Boolean)
            Throw New NotImplementedException()
        End Set
    End Property

    Public ReadOnly Property MyTag As Object Implements INPTConfiguration.MyTag
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property ListNPTConfiguration As List(Of NPTConfiguration) Implements INPTConfiguration.ListNPTConfiguration
        Get
            Throw New NotImplementedException()
        End Get
        Set(value As List(Of NPTConfiguration))
            Throw New NotImplementedException()
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Evento de confirmación de etiquetado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event AddNPTConfiguration(sender As Object, e As NPTConfiguration)

    Public Sub IsLoading(Optional value As Boolean = True)
        INDMpbProgress.Visible = value
    End Sub

    Private Sub INDSleInventoryProduct_Properties_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleInventoryProduct.Properties.QueryPopUp, INDSleInventoryProduct.QueryPopUp
        If INDSleInventoryProduct.Properties.DataSource Is Nothing Then
            INDSleInventoryProduct.Properties.DataSource = Presenter.QueryNPTInventoryProduct
        End If
    End Sub

    Private Sub FrmPopupAddNPT_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Close()
        End If
    End Sub

    ''' <summary>
    ''' valida que los campos del popup no quede vacio
    ''' </summary>
    ''' <returns></returns>
    Private Function Validations() As Boolean
        If INDSleInventoryProduct.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un Producto"
            Return False
        ElseIf INDspnEndWeight.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El peso final debe ser mayor a 0"
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        INDLcDeliveryTime.BeginUpdate()
        INDSleInventoryProduct.EditValue = Nothing
        INDspnInitialWeight.EditValue = 0
        INDspnEndWeight.EditValue = 0
        INDLcDeliveryTime.EndUpdate()

        INDSleInventoryProduct.Focus()
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If Not Validations() Then Return

        RaiseEvent AddNPTConfiguration(Me, New NPTConfiguration With
                                       {
                                            .InitialWeight = INDspnInitialWeight.EditValue,
                                            .EndWeight = INDspnEndWeight.EditValue,
                                            .ProductId = INDSleInventoryProduct.EditValue
                                        })
        CleanControls()
    End Sub

    Private Sub FrmPopupAddNPT_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSleInventoryProduct.Focus()
    End Sub

End Class