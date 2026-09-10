'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Sergio Fernandez
' Created          : 02-11-2011
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Public Class CtrFirmaDigital
    Property limpiar As Boolean
    ''' <summary>
    ''' Evento load del ctr Firma.Carga la firma que se manda desde el frmFirma
    ''' </summary>

    Private Sub CtrFirmaDigital_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ' PictureEdit1.EditValue = FrmFirma.enviar

    End Sub
    ''' <summary>
    ''' Handles the Click event of the SimpleButton1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click
        Dim firma As New FrmFirma
        firma.ShowDialog()
        'If firma.limpiar = True Then
        '    LimpiarControles()
        '    Exit Sub
        'End If
        'PictureEdit1.EditValue = firma.enviar
        firma.Dispose()
    End Sub
    WriteOnly Property Capturar As Object
        Set(ByVal value As Object)
            '  PictureEdit1.EditValue = FrmFirma.enviar

        End Set
    End Property

    ''' <summary>
    ''' evento Cancelar del control ctrfirmadigital.
    ''' </summary>
    Private Sub INDBtnCancelar_Click(sender As System.Object, e As System.EventArgs) Handles INDBtnCancelar.Click
        'PictureEdit1.EditValue = Global.Presentation.Controls.My.Resources.Resources.firma_digital02ssssss
    End Sub

    ''' <summary>
    '''  Funcion que Limpia el control.
    ''' </summary>
    ''' <returns></returns>
    Public Function LimpiarControles() As Boolean
        'PictureEdit1.EditValue = Global.Presentation.Controls.My.Resources.Resources.firma_digital02ssssss
        Return True
    End Function

    ''' <summary>
    ''' Propiedad que guardar  la imagen.
    ''' </summary>
    ''' <value>The guardarimagen.</value>
    Public Property guardarimagenFirma As Image
        Get
            Return PictureEdit1.Image
        End Get
        Set(ByVal value As Image)
            PictureEdit1.Image = value
        End Set
    End Property

    Public Sub EstablecerImagenFirmaDigital()
        'PictureEdit1.EditValue = Global.Presentation.Controls.My.Resources.Resources.firma_digital02ssssss
    End Sub
    Public Property EnviarImagen As Object
        Get
            Return PictureEdit1.Image
        End Get
        Set(ByVal value As Object)
            PictureEdit1.EditValue = value
        End Set
    End Property
End Class
