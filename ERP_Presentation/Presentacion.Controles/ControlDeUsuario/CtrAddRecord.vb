'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 15-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias importadas"
#End Region

''' <summary>
''' Clase con toda la funcionalidad del control para agregar nuevos Registros a una rejilla
''' </summary>
Public Class CtrAddRecord

#Region "Eventos"

    ''' <summary>
    ''' Ocurre cuando se dispara el popup
    ''' </summary>
    Public Event QueryPopUp(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

    Public Event Closed(ByVal sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs)

    Private Sub INDpceAddRecord_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDpceAddRecord.QueryPopUp
        RaiseEvent QueryPopUp(sender, e)
    End Sub



#End Region

#Region "Propiedades"
    ''' <summary>
    ''' Propiedad que obtiene y establece el popupcontrol asociado al popupcontaineredit
    ''' </summary>
    ''' <value>
    ''' el pop up control.
    ''' </value>
    Public Property PopUpControl As DevExpress.XtraEditors.PopupContainerControl
        Get
            Return INDpceAddRecord.Properties.PopupControl
        End Get
        Set(value As DevExpress.XtraEditors.PopupContainerControl)
            INDpceAddRecord.Properties.PopupControl = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene y establece si se puede cambiar el tamaño del popupcontrol
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [pop up sizeable]; otherwise, <c>false</c>.
    ''' </value>
    Public Property PopUpSizeable As Boolean
        Get
            Return INDpceAddRecord.Properties.PopupSizeable
        End Get
        Set(value As Boolean)
            INDpceAddRecord.Properties.PopupSizeable = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que obtiene y establece si se muestra el boton cerrar en el popupcontrol
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [show popup close button]; otherwise, <c>false</c>.
    ''' </value>
    Public Property ShowPopupCloseButton As Boolean
        Get
            Return INDpceAddRecord.Properties.ShowPopupCloseButton
        End Get
        Set(value As Boolean)
            INDpceAddRecord.Properties.ShowPopupCloseButton = value
        End Set
    End Property
#End Region

#Region "Metodos"
    ''' <summary>
    ''' Evento Clic en el boton para hacer el show popup
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDbtnAddRecord_Click(sender As Object, e As EventArgs) Handles INDbtnAddRecord.Click
        INDpceAddRecord.ShowPopup()
    End Sub
#End Region

    Private Sub INDpceAddRecord_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceAddRecord.Closed
        RaiseEvent Closed(sender, e)
    End Sub
End Class
