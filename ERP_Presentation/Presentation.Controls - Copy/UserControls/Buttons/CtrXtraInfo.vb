'***********************************************************************
' Assembly         : Presentacion.Controles
' Author           : Juan F. Tamayo
' Created          : 2013-08-24
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-08-24
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports System.Collections.ObjectModel

#End Region

Public Class CtrXtraInfo

#Region "Fields"

    ''' <summary>
    ''' Encapsula la lista de propiedades a mostrar
    ''' </summary>
    Private WithEvents _listProperties As New ObservableCollection(Of XtraInfoProperty)
    ''' <summary>
    ''' Encapsula el ancho a asignar en el texto del LayoutControlItem
    ''' </summary>
    Private _widthTextLabel As Integer = 135

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene la lista de propiedades a mostrar en el control
    ''' </summary>
    ''' <returns>La lista de propiedades</returns>
    <Browsable(False)>
    Public ReadOnly Property ListProperties As ObservableCollection(Of XtraInfoProperty)
        Get
            Return Me._listProperties
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto del titulo del control
    ''' </summary>
    ''' <value>Texto del titulo</value>
    ''' <returns>El texto de titulo</returns>
    <Category("Indigo"), Description("Obtiene o asigna el texto del titulo del control")>
    Public Property Title As String
        Get
            Return Me.INDlycgRoot.Text
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.INDlycgRoot.Text = value.Trim()
            Else
                Me.INDlycgRoot.Text = String.Empty
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el ancho del label en la propiedad
    ''' </summary>
    ''' <value>Ancho del label</value>
    ''' <returns>El ancho del label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el ancho del label en la propiedad")>
    Public Property WidthTextLabel As Integer
        Get
            Return Me._widthTextLabel
        End Get
        Set(value As Integer)
            Me._widthTextLabel = value
            Me.RefreshWidthTextLabels()
        End Set
    End Property

    <Browsable(False)>
    Public Overrides Property MaximumSize As Size
        Get
            Return MyBase.MaximumSize
        End Get
        Set(value As Size)
            MyBase.MaximumSize = MyBase.MaximumSize
        End Set
    End Property

    <Browsable(False)>
    Public Overrides Property MinimumSize As Size
        Get
            Return MyBase.MinimumSize
        End Get
        Set(value As Size)
            MyBase.MinimumSize = MyBase.MinimumSize
        End Set
    End Property

#End Region

#Region "Builders"



#End Region

#Region "Methods"

    ''' <summary>
    ''' Refresca el ancho del texto en cada uno de los LayoutControlItems
    ''' </summary>
    Private Sub RefreshWidthTextLabels()
        For i As Integer = 0 To Me.INDlycRoot.Root.Items.ItemCount - 1
            If Me.INDlycRoot.Root.Item(i).GetType() Is GetType(LayoutControlItem) Then
                Me.INDlycRoot.Root.Item(i).TextSize = New Size(Me._widthTextLabel, Me.INDlycRoot.Root.Item(i).TextSize.Height)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Genera los controles respectivos a cada propiedad de la lista
    ''' </summary>
    Private Sub GenerateControls()
        Me.INDlycRoot.Root.Clear()
        If Me._listProperties IsNot Nothing AndAlso Me._listProperties.Count > 0 Then
            For Each p As XtraInfoProperty In Me._listProperties
                Dim item = Me.INDlycRoot.Root.AddItem(p.DisplayName, New LabelControl())
                CType(item.Control, LabelControl).Text = p.DisplayValue
                item.ShowInCustomizationForm = False
                item.AllowHide = False
                item.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
                item.MinSize = New Size(356, p.HeightDisplayValue)
                item.MaxSize = New Size(0, p.HeightDisplayValue)
                item.TextAlignMode = TextAlignModeItem.CustomSize
                item.TextToControlDistance = 12
                item.TextSize = New Size(Me._widthTextLabel, 21)
                item.TextVisible = True
                item.AppearanceItemCaption.Font = New Font("Segoe UI", 9.75!, FontStyle.Regular)
                item.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
                CType(item.Control, LabelControl).Appearance.Font = New Font("Segoe UI Light", 9.75!)
                CType(item.Control, LabelControl).Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top
                CType(item.Control, LabelControl).AutoSizeMode = LabelAutoSizeMode.Vertical
            Next
        End If
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se actualiza las propiedades a mostrar en el control
    ''' </summary>
    Private Sub _listProperties_CollectionChanged(sender As Object, e As Specialized.NotifyCollectionChangedEventArgs) Handles _listProperties.CollectionChanged
        Me.GenerateControls()
    End Sub

#End Region

End Class

#Region "Class"

''' <summary>
''' Encapsula los datos de una propiedad del objeto que quiero mostrar
''' </summary>
<Serializable()>
Public Class XtraInfoProperty

#Region "Fields"

    ''' <summary>
    ''' Encapsula el nombre para mostrar de la propiedad
    ''' </summary>
    Private _displayName As String
    ''' <summary>
    ''' Encapsula el valor de la propiedad a mostrar
    ''' </summary>
    Private _displayValue As String
    ''' <summary>
    ''' Encapsula el alto usado para el texto
    ''' </summary>
    Private _heightDisplayValue As Integer = 36

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el nombre para mostrar de la propiedad
    ''' </summary>
    ''' <value>Nombre para mostrar</value>
    ''' <returns>El nombre para mostrar</returns>
    Public Property DisplayName As String
        Get
            Return Me._displayName.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._displayName = value.Trim()
            Else
                Me._displayName = String.Empty
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor para mostrar de la propiedad
    ''' </summary>
    ''' <value>Valor para mostrar</value>
    ''' <returns>El valor para mostrar</returns>
    Public Property DisplayValue As String
        Get
            Return Me._displayValue.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me._displayValue = value.Trim()
            Else
                Me._displayValue = String.Empty
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el alto del valor a mostrar
    ''' </summary>
    ''' <value>Alto del valor a mostrar</value>
    ''' <returns>El alto del valor a mostrar</returns>
    Public Property HeightDisplayValue As Integer
        Get
            Return Me._heightDisplayValue
        End Get
        Set(value As Integer)
            Me._heightDisplayValue = value
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    ''' <param name="displayName">Nombre para mostrar de la propiedad</param>
    ''' <param name="displayValue">Valor para mostrar de la propiedad</param>
    ''' <param name="heightDisplayValue">Alto del valor a mostrar</param>
    Public Sub New(ByVal displayName As String, ByVal displayValue As String, Optional ByVal heightDisplayValue As Integer = 25)
        If displayName IsNot Nothing Then
            Me._displayName = displayName.Trim()
        Else
            Me._displayName = String.Empty
        End If
        If displayValue IsNot Nothing Then
            Me._displayValue = displayValue.Trim()
        Else
            Me._displayValue = String.Empty
        End If
        Me._heightDisplayValue = heightDisplayValue
    End Sub

#End Region

#Region "Methods"



#End Region

End Class

#End Region
