Imports System.ComponentModel

Public Class CtrActivoInactivo

    Private _Valor As Boolean
    <DefaultValue(True)> _
    Public Property Valor As Boolean
        Get
            Return _Valor
        End Get
        Set(value As Boolean)
            _Valor = value
            INDchkEstado.EditValue = value
            If _Valor = False Then
                Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))
            ElseIf _Valor = True Then
                Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))
            End If
        End Set
    End Property

    Private _controlColor As StateColor
    <DefaultValue(StateColor.Normal)> _
    Public Property controlColor As StateColor
        Get
            Return _controlColor
        End Get
        Set(value As StateColor)
            _controlColor = value
            If value = StateColor.Bloqueo Then
                Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(202, Byte), Integer), CType(CType(81, Byte), Integer), CType(CType(0, Byte), Integer))
            ElseIf value = StateColor.Normal Then
                Valor = Me._Valor
            End If
        End Set
    End Property

    Private _NombreEstado As String
    <DefaultValue("")> _
    Public Property NombreEstado As String
        Get
            Return _NombreEstado
        End Get
        Set(value As String)
            _NombreEstado = value
            INDlabelEstado.Text = value
        End Set
    End Property

    Private _VisibleNombreEstado As Boolean
    <DefaultValue(False)> _
    Public Property VisibleNombreEstado As Boolean
        Get
            Return _VisibleNombreEstado
        End Get
        Set(value As Boolean)
            _VisibleNombreEstado = value
            INDlabelEstado.Visible = value
        End Set
    End Property

    Private _ItemsEstado As DevExpress.XtraEditors.Controls.RadioGroupItemCollection = New DevExpress.XtraEditors.Controls.RadioGroupItemCollection()
    Public Property ItemsEstado As DevExpress.XtraEditors.Controls.RadioGroupItemCollection
        Get
            Return _ItemsEstado
        End Get
        Set(value As DevExpress.XtraEditors.Controls.RadioGroupItemCollection)
            _ItemsEstado = value
            INDchkEstado.Properties.Items.Clear()
            INDchkEstado.Properties.Items.Assign(value)
        End Set
    End Property

    Private Sub INDchkEstado_EditValueChanged(sender As Object, e As EventArgs) Handles INDchkEstado.EditValueChanged
        Valor = CBool(INDchkEstado.EditValue)
    End Sub

    Public Sub Limpiar()
        INDchkEstado.EditValue = Nothing
        Me.BackColor = Color.MistyRose
    End Sub

End Class

''' <summary>
''' Enumera los colores para la barra de estado
''' </summary>
<Serializable()>
Public Enum StateColor
    ''' <summary>
    ''' Bloqueo
    ''' </summary>
    Bloqueo
    ''' <summary>
    ''' Normal
    ''' </summary>
    Normal
End Enum
