'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Juan F. Tamayo
' Created          : 2014-01-10
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2017-01-10
' Description      : Formulario de customización de layouts
'                    con funcionalidades adicionales y controles
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.XtraLayout
Imports DevExpress.XtraLayout.Dragging
Imports DevExpress.XtraLayout.Customization
Imports DevExpress.Utils.Controls

#End Region

''' <summary>
''' Define una nueva ventana de personalización para layouts, agregando
''' nuevas funciones y herramientas
''' </summary>
Public Class ToolsCustomizationForm

#Region "Fields"

    ''' <summary>
    ''' Encapsula una referencia del Layout que se esta personalizando
    ''' </summary>
    Private WithEvents _layoutCurrenCustomization As LayoutControl

    ''' <summary>
    ''' Almacena el ultimo punto en que se hizo Drag
    ''' </summary>
    Private _lastDragPoint As Point

    ''' <summary>
    ''' Diccionario de controles permitidos en la ventana de personalización
    ''' </summary>
    Private _dicControls As Dictionary(Of String, Type)

    ''' <summary>
    ''' Se encarga de la presentación visual en el evento de arrastrar y soltar
    ''' </summary>
    Private _dragVisualizer As DraggingVisualizer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el objeto de animación al momento de arrastrar y soltar
    ''' </summary>
    ''' <returns>Objeto de animación</returns>
    Public ReadOnly Property DragBoundsVisualizer As DraggingVisualizer
        Get
            If Me._dragVisualizer Is Nothing AndAlso Me._layoutCurrenCustomization IsNot Nothing Then
                Me._dragVisualizer = New DraggingVisualizer(Me._layoutCurrenCustomization)
            End If
            Return Me._dragVisualizer
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles customisables en la lista
    ''' </summary>
    Private Sub LoadCustomControls()
        For Each e As KeyValuePair(Of String, Type) In CustomHelper.ListCustomControls()
            Me.INDlstControls.Items.Add(e.Key, Me.GetIndexImage(e.Key))
        Next
    End Sub

    ''' <summary>
    ''' Obtiene el indice de la imagen correspondiente al nombre del control
    ''' </summary>
    ''' <param name="name">Nombre del control</param>
    ''' <returns>Indice de la imagen</returns>
    Private Function GetIndexImage(ByVal name As String) As Int32
        Try
            Dim idx = Me.INDimcControls.Images.IndexOf(Me.INDimcControls.Images(name.Trim()))
            If idx > -1 Then
                Return idx
            Else
                Return Me.INDimcControls.Images.IndexOf(Me.INDimcControls.Images("Nothing"))
            End If
        Catch
            Return Me.INDimcControls.Images.IndexOf(Me.INDimcControls.Images("Nothing"))
        End Try
    End Function

    ''' <summary>
    ''' Crea un objeto <see cref="System.Windows.Forms.MouseEventArgs"/>
    ''' </summary>
    ''' <param name="de">Objeto de tipo <see cref="System.Windows.Forms.DragEventArgs"/> usado como base</param>
    ''' <returns>El objeto <see cref="System.Windows.Forms.MouseEventArgs"/></returns>
    Private Function CreateMouseEventArgs(ByVal de As DragEventArgs) As MouseEventArgs
        Dim p = Point.Empty
        If de IsNot Nothing Then
            p = New Point(de.X, de.Y)
        End If
        If Me._layoutCurrenCustomization IsNot Nothing Then
            p = Me._layoutCurrenCustomization.PointToClient(p)
        End If
        Return New MouseEventArgs(MouseButtons.Left, 0, p.X, p.Y, 0)
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se inicializa el formulario
    ''' </summary>
    Private Sub CustomizationFormCustom_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Size = New Size(574, 569)
        Me.StartPosition = FormStartPosition.CenterScreen
        Me._layoutCurrenCustomization = DirectCast(Me.OwnerControl, LayoutControl)
        Me._layoutCurrenCustomization.AllowDrop = True
        Me.LoadCustomControls()
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la acción arrastrar y soltar mientras se tenga presionado el botón izquierdo del mouse
    ''' </summary>
    Private Sub INDlstControls_MouseMove(sender As Object, e As MouseEventArgs) Handles INDlstControls.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left AndAlso Me.INDlstControls.SelectedValue IsNot Nothing AndAlso Not Me.INDlstControls.SelectedValue.ToString().Equals(String.Empty) Then
            Me.INDlstControls.DoDragDrop(Me.INDlstControls.SelectedValue.ToString(), DragDropEffects.Copy Or DragDropEffects.Move)
        End If
    End Sub

    ''' <summary>
    ''' Aqui inicia el arrastrado sobre el LayoutControl
    ''' </summary>
    Private Sub Layout_DragEnter(sender As Object, e As DragEventArgs) Handles _layoutCurrenCustomization.DragEnter
        Me.DragBoundsVisualizer.ProcessMessageFromDesigner(EventType.MouseEnter, CreateMouseEventArgs(e))
        e.Effect = DragDropEffects.Copy
    End Sub

    ''' <summary>
    ''' Aqui calcula el ultimo punto del LayoutControl en el que se arrastro el control
    ''' </summary>
    Private Sub Layout_DragOver(sender As Object, e As DragEventArgs) Handles _layoutCurrenCustomization.DragOver
        Dim mea = Me.CreateMouseEventArgs(e)
        DragBoundsVisualizer.ProcessMessageFromDesigner(EventType.MouseMove, mea)
        Me._lastDragPoint = New Point(mea.X, mea.Y)
    End Sub

    ''' <summary>
    ''' Aqui termina el arrastrado sobre el LayoutControl
    ''' </summary>
    Private Sub Layout_DragLeave(sender As Object, e As EventArgs) Handles _layoutCurrenCustomization.DragLeave
        Me.DragBoundsVisualizer.ProcessMessageFromDesigner(EventType.MouseLeave, CreateMouseEventArgs(Nothing))
    End Sub

    ''' <summary>
    ''' Aqui se suelta el control sobre el LayoutControl
    ''' </summary>
    Private Sub Layout_DragDrop(sender As Object, e As DragEventArgs) Handles _layoutCurrenCustomization.DragDrop
        Dim controlName As String = e.Data.GetData(DataFormats.Text).ToString()
        Dim t As Type = CustomHelper.ListCustomControls()(controlName)
        Dim cont As Control = Nothing
        If t IsNot Nothing Then
            cont = Activator.CreateInstance(t)
        End If
        If cont IsNot Nothing Then
            'Esto es temporal para el demo
            DirectCast(cont, ICustom(Of TextEditCustom)).TypeOfData = TypeCode.String

            Dim item As LayoutControlItem = CType(Me._layoutCurrenCustomization.CreateLayoutItem(Nothing), LayoutControlItem)

            item.AppearanceItemCaption.Font = New Font("Segoe UI Light", 12.0!)
            item.TextSize = New Size(135, 21)
            item.TextToControlDistance = 12
            item.Name = Guid.NewGuid().ToString()
            item.Text = "Layout Item"
            cont.Font = New Font("Segoe UI, 12pt", 12.0!)
            cont.Name = Guid.NewGuid().ToString()
            item.ControlName = cont.Name
            Dim controller As New LayoutItemDragController(item, Me._layoutCurrenCustomization.Root, Me._lastDragPoint)
            controller.DragWildItem()
            Me._lastDragPoint = Point.Empty
            item.Control = cont
        End If
        Me.DragBoundsVisualizer.HideDragBounds()
    End Sub

#End Region

End Class