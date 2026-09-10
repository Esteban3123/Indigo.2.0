'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Jorge Leonardo Vernaza 
' Created          : 25-06-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Clase con toda la funcionalidad del control de que implementa el boton de navegacion Atras
''' </summary>
Public Class CtrNavigation

#Region "Eventos - Propiedades - Variables Globales"
    ''' <summary>
    ''' Evento que dispara al dar click en el boton atras del control
    ''' </summary>
    Public Event ClickBack()

    ''' <summary>
    ''' Propiedad para establecer el grupo que va estar en el control
    ''' </summary>
    ''' <value>
    ''' el grupo.
    ''' </value>
    Public WriteOnly Property Group As DevExpress.XtraLayout.LayoutControlGroup
        Set(value As DevExpress.XtraLayout.LayoutControlGroup)
            If value IsNot Nothing Then
                value.Location = New Point(0, 0)
                value.AppearanceGroup.Font = New System.Drawing.Font("Segoe UI", 16.0!)
                value.AppearanceGroup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
                Me.INDlygGroup.Add(value)
            End If
        End Set
    End Property
#End Region

#Region "Metodos"
    Public Property HideGroupContent As Boolean

    ''' <summary>
    ''' Evento clic en el picture edit que dispara el evento clicback
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDpeBack_Click(sender As Object, e As EventArgs) Handles INDpeBack.Click
        If INDpeBack.Enabled = True Then
            RaiseEvent ClickBack()
        End If
    End Sub

    ''' <summary>
    ''' evento que cambia la imagen cuando el mouse esta sobre el control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDpeBack_MouseEnter(sender As Object, e As EventArgs) Handles INDpeBack.MouseEnter
        INDpeBack.Image = My.Resources.Arrow2
    End Sub

    ''' <summary>
    ''' evento que cambia la imagen cuando el mouse sale de el control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDpeBack_MouseLeave(sender As Object, e As EventArgs) Handles INDpeBack.MouseLeave
        INDpeBack.Image = My.Resources.Arrow1
    End Sub

    ''' <summary>
    ''' evento que cambia la imagen cuando el mouse esta sobre presionando
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
    Private Sub INDpeBack_MouseDown(sender As Object, e As MouseEventArgs) Handles INDpeBack.MouseDown
        INDpeBack.Image = My.Resources.Arrow3
    End Sub

    ''' <summary>
    ''' evento que cambia la imagen cuando el mouse deja de presional el control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
    Private Sub INDpeBack_MouseUp(sender As Object, e As MouseEventArgs) Handles INDpeBack.MouseUp
        INDpeBack.Image = My.Resources.Arrow2
    End Sub

    Private Sub CtrNavigation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If HideGroupContent Then
            INDlygGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
#End Region

End Class
