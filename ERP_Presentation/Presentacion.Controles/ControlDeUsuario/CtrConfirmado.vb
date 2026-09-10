'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 09-04-2011
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base
#End Region


''' <summary>
''' Clase que contiene toda la funcionalidad del control Confirmado
''' </summary>
Public Class CtrConfirmado

    Private _Valor As Confirm
    ''' <summary>
    ''' Propiedad que obtiene y establece si el control esta en estado confirmado o sin confirmar y cambia de color y de mensaje dependiendo del estado
    ''' </summary>
    Public Property Valor As Confirm
        Get
            Return _Valor
        End Get
        Set(value As Confirm)
            _Valor = value
            If _Valor = Confirm.Confirmed Then
                Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))
                INDlblText.Text = obtenerRecurso(Confirmado, Eform.CtrConfirmado)
            ElseIf _Valor = Confirm.NotConfirmed Or _Valor = Confirm.Vacio Then
                Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(122, Byte), Integer))
                INDlblText.Text = obtenerRecurso(SinConfirmar, Eform.CtrConfirmado)
                'ElseIf _Valor = Confirm.Vacio Then
                'Me.BackColor = System.Drawing.Color.White
                'INDlblText.Text = String.Empty
            ElseIf _Valor = Confirm.Invalidate Then
                Me.BackColor = System.Drawing.Color.OrangeRed
                INDlblText.Text = obtenerRecurso(Anulado, Eform.CtrConfirmado)
            ElseIf _Valor = Confirm.OfficeResponse Then
                Me.BackColor = System.Drawing.Color.OrangeRed
                INDlblText.Text = obtenerRecurso(OficioConRespuesta, Eform.CtrConfirmado)
            End If
        End Set
    End Property

End Class

