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

Imports Presentation.Base.BaseClass
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports System.Linq
Imports System.Collections.ObjectModel

#End Region

<Serializable()>
Public Class CtrAdvancedFilter

#Region "Events"

    ''' <summary>
    ''' Se lanza cuando los valores ingresados por el usuario son inválidos
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event InvalidValues(ByVal sender As Object, ByVal e As EventArgs)
    ''' <summary>
    ''' Se lanza cuando el usuario a iniciado la ejecución de la busqueda segun los filtros creados
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event RunSearch(ByVal sender As Object, ByVal e As RunSearchEventArgs)

#End Region

#Region "Fields"

    ''' <summary>
    ''' Encapsula el indice del filtro que se encuentra en edicion
    ''' </summary>
    Private _currentFilterIndex As Integer = -1
    ''' <summary>
    ''' Encapsula la lista de campos
    ''' </summary>
    Private _listField As List(Of ObjectField)
    ''' <summary>
    ''' Encapsula la lista de operadores lógicos
    ''' </summary>
    Private _listOperators As List(Of LogicalOperatorAdvancedFilter)

    ''' <summary>
    ''' Encapsula la lista de cantidades maximas de resultados al ejecutar la busqueda
    ''' </summary>
    Private WithEvents _listTopResult As New ObservableCollection(Of String)(New String() {50, 100, 150, 200})
    ''' <summary>
    ''' Encapsula el formato usado para los tipo de dato Date
    ''' </summary>
    Private _stringFormatDate As String = "yyyyMMdd"
    ''' <summary>
    ''' Encapsula el formato usado para los tipo de dato DateTime
    ''' </summary>
    Private _stringFormatDateTime As String = "yyyyMMdd HH:mm:ss"
    ''' <summary>
    ''' Encapsula el formato usado para los tipo de dato Date para mostrar
    ''' </summary>
    Private _displayStringFormatDate As String = "dd MMM yyyy"
    ''' <summary>
    ''' Encapsula el formato usado para los tipo de dato DateTime para mostrar
    ''' </summary>
    Private _displayStringFormatDateTime As String = "dd MMM yyyy - HH:mm:ss"

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene la lista de cantidades máximas de resultados por busqueda
    ''' </summary>
    ''' <returns>La lista de cantidades máximas</returns>
    <Category("Indigo"), Description("Obtiene la lista de cantidades máximas de resultados por busqueda")>
    <Browsable(False)>
    Public Property ListTopResults As ObservableCollection(Of String)
        Get
            Return Me._listTopResult
        End Get
        Set(value As ObservableCollection(Of String))
            Me._listTopResult = value
            Me._listTopResult_CollectionChanged(Me._listTopResult, Nothing)
        End Set
    End Property

    ''' <summary>
    ''' Propiedad Para asignar los campos de filtro
    ''' </summary>
    <Browsable(False)>
    Public Property ListFields As List(Of ObjectField)
        Get
            Return Me._listField
        End Get
        Set(value As List(Of ObjectField))
            If value IsNot Nothing AndAlso value.Count > 0 Then
                Me._listField = value
                Me.INDcbeFields.Properties.DataSource = Me._listField
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el titulo del control
    ''' </summary>
    ''' <value>El titulo del control</value>
    ''' <returns>El titulo del control</returns>
    <Category("Indigo"), Description("Obtiene o asigna el titulo del control")>
    Public Property Title As String
        Get
            Return Me.INDlycgHeader.Text.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.INDlycgHeader.Text = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto al label del combo de campos
    ''' </summary>
    ''' <value>Text para el label</value>
    ''' <returns>El texto del label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el texto al label del combo de campos")>
    Public Property FieldsLabel As String
        Get
            Return Me.INDlyciFields.Text.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.INDlyciFields.Text = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto al label del combo de criterios
    ''' </summary>
    ''' <value>Texto para el label</value>
    ''' <returns>El texto del label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el texto al label del combo de criterios")>
    Public Property CriteriaLabel As String
        Get
            Return Me.INDlyciCriteria.Text.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.INDlyciCriteria.Text = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto al label del campo de datos
    ''' </summary>
    ''' <value>Texto para el label</value>
    ''' <returns>El texto del label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el texto al label del campo de datos")>
    Public Property DataLabel As String
        Get
            Return Me.INDctrAdvancedFilter.DisplayText.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.INDctrAdvancedFilter.DisplayText = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto al label del campo de operadores
    ''' </summary>
    ''' <value>Texto para el label</value>
    ''' <returns>El texto del label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el texto al label del campo de operadores")>
    Public Property OperatorLabel As String
        Get
            Return Me.INDlyciOperator.Text.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.INDlyciOperator.Text = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el texto al label de la lista de cantidades máximas en resultados
    ''' </summary>
    ''' <value>Texto para el label</value>
    ''' <returns>El texto del label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el texto al label de la lista de cantidades máximas en resultados")>
    Public Property TopResultsLabel As String
        Get
            Return Me.INDlyciResultsNumber.Text.Trim()
        End Get
        Set(value As String)
            Me.INDlyciResultsNumber.Text = value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el titulo del subgrupo, lista de filtros
    ''' </summary>
    ''' <value>Texto para el label</value>
    ''' <returns>El texto del label</returns>
    <Category("Indigo"), Description("Obtiene o asigna el titulo del subgrupo, lista de filtros")>
    Public Property TitleFilterlistLabel As String
        Get
            Return Me.INDlblTitleFilterlist.Text.Trim()
        End Get
        Set(value As String)
            If value IsNot Nothing Then
                Me.INDlblTitleFilterlist.Text = value.Trim()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la cadena de formato usada para los tipos de dato Date
    ''' </summary>
    ''' <value>Cadena de formato</value>
    ''' <returns>La cadena de dormato</returns>
    <Category("Indigo"), Description("Obtiene o asigna la cadena de formato usada para los tipos de dato Date")>
    Public Property StringFormatDate As String
        Get
            Return Me._stringFormatDate.Trim()
        End Get
        Set(value As String)
            Me._stringFormatDate = value.Trim()
            Me.RefreshStringsFormat()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la cadena de formato usada para los tipos de dato DateTime
    ''' </summary>
    ''' <value>Cadena de formato</value>
    ''' <returns>La cadena de dormato</returns>
    <Category("Indigo"), Description("Obtiene o asigna la cadena de formato usada para los tipos de dato DateTime")>
    Public Property StringFormatDateTime As String
        Get
            Return Me._stringFormatDateTime.Trim()
        End Get
        Set(value As String)
            Me._stringFormatDateTime = value.Trim()
            Me.RefreshStringsFormat()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la cadena de formato usada para mostrar los tipos de dato Date
    ''' </summary>
    ''' <value>Cadena de formato</value>
    ''' <returns>La cadena de dormato</returns>
    <Category("Indigo"), Description("Obtiene o asigna la cadena de formato usada para mostrar los tipos de dato Date")>
    Public Property DsplayStringFormatDate As String
        Get
            Return Me._displayStringFormatDate.Trim()
        End Get
        Set(value As String)
            Me._displayStringFormatDate = value.Trim()
            Me.RefreshStringsFormat()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la cadena de formato usada para mostrar los tipos de dato DateTime
    ''' </summary>
    ''' <value>Cadena de formato</value>
    ''' <returns>La cadena de dormato</returns>
    <Category("Indigo"), Description("Obtiene o asigna la cadena de formato usada para mostrar los tipos de dato DateTime")>
    Public Property DisplayStringFormatDateTime As String
        Get
            Return Me._displayStringFormatDateTime.Trim()
        End Get
        Set(value As String)
            Me._displayStringFormatDateTime = value.Trim()
            Me.RefreshStringsFormat()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene la cadena de codigo SQL generada a partir de los filtros creados
    ''' </summary>
    ''' <returns>Cadena SQL</returns>
    <Browsable(False)>
    Public ReadOnly Property QueryString As String
        Get
            Dim sql As String = ""
            For i As Integer = 0 To Me.INDlycFilters.Root.Items.ItemCount - 1
                sql &= " " & CType(CType(Me.INDlycFilters.Root.Item(i), LayoutControlItem).Control, CtrAdvancedFilterLink).GetFilterCode()
            Next
            Return sql.Trim()
        End Get
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
    ''' Asigna el foco al control que le corresponda
    ''' </summary>
    Private Sub SetFocusControls()
        If Me.INDlyciOperator.Visibility = Utils.LayoutVisibility.Always Then
            Me.INDcbeOperator.Focus()
        Else
            Me.INDcbeFields.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles de sus valores y los deja por defecto
    ''' </summary>
    ''' <param name="allControls">Valor que indica si se limpia tambien los filtros</param>
    ''' <remarks></remarks>
    Private Sub ClearControls(Optional ByVal allControls As Boolean = False)
        Me.INDcbeOperator.EditValue = Me._listOperators(0)
        Me.INDcbeFields.EditValue = Nothing
        Me.INDbtnAdd.Image = INDimcAddIcons.Images(0)
        Me.INDctrAdvancedFilter.Reset()
        If allControls Then
            Me.INDlycFilters.Clear()
            Me.INDlyciOperator.Visibility = Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Refresca las cadenas de formato en el control CtrAdvancedFilterLink
    ''' </summary>
    Private Sub RefreshStringsFormat()
        For i As Integer = 0 To Me.INDlycRoot.Root.Items.ItemCount - 1
            CType(CType(Me.INDlycRoot.Root.Item(i), LayoutControlItem).Control, CtrAdvancedFilterLink).StringFormatDate = Me._stringFormatDate
            CType(CType(Me.INDlycRoot.Root.Item(i), LayoutControlItem).Control, CtrAdvancedFilterLink).StringFormatDateTime = Me._stringFormatDateTime
            CType(CType(Me.INDlycRoot.Root.Item(i), LayoutControlItem).Control, CtrAdvancedFilterLink).DisplayStringFormatDate = Me._displayStringFormatDate
            CType(CType(Me.INDlycRoot.Root.Item(i), LayoutControlItem).Control, CtrAdvancedFilterLink).DisplayStringFormatDate = Me._displayStringFormatDate
        Next
    End Sub

    ''' <summary>
    ''' Obtiene el indice del item quecontiene el control
    ''' </summary>
    ''' <param name="ctrl">Control a buscar</param>
    ''' <returns>Indice del item que contiene el control</returns>
    Private Function GetIndexItem(ByRef ctrl As CtrAdvancedFilterLink) As Integer
        For i As Integer = 0 To Me.INDlycFilters.Root.Items.ItemCount - 1
            If CType(CType(Me.INDlycFilters.Root.Item(i), LayoutControlItem).Control, CtrAdvancedFilterLink).Tag.ToString().Equals(ctrl.Tag.ToString()) Then
                Return i
            End If
        Next
        Return -1
    End Function

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se lanza el evento de ejecución de la busqueda
    ''' </summary>
    Private Sub INDcbeResultsNumber_KeyDown(sender As Object, e As KeyEventArgs) Handles INDcbeResultsNumber.KeyDown
        If e.KeyCode.Equals(Keys.Enter) Then
            Dim resultsNumber As Integer
            resultsNumber = If(Integer.TryParse(Me.INDcbeResultsNumber.EditValue, resultsNumber), resultsNumber, 2147483647)
            RaiseEvent RunSearch(Me, New RunSearchEventArgs With {.QueryString = Me.QueryString, .TopResult = resultsNumber})
        End If
    End Sub

    ''' <summary>
    ''' Aqui se cambia el data source de la lista de número de resultados
    ''' </summary>
    Private Sub _listTopResult_CollectionChanged(sender As Object, e As Specialized.NotifyCollectionChangedEventArgs) Handles _listTopResult.CollectionChanged
        Me.INDcbeResultsNumber.Properties.Items.Clear()
        Me.INDcbeResultsNumber.EditValue = Nothing
        Me.INDcbeResultsNumber.Refresh()
        If Me._listTopResult IsNot Nothing Then
            If Me._listTopResult.Count > 0 Then
                Me.INDcbeResultsNumber.Properties.Items.AddRange(Me._listTopResult)
                Me.INDcbeResultsNumber.EditValue = Me.INDcbeResultsNumber.Properties.Items(0)
                Me.INDcbeResultsNumber.Refresh()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se activa cuando un filtro debe ser eliminado
    ''' </summary>
    Private Sub OnDeleteFilter(sender As Object, e As AdvancedFilterLinkEventArgs)
        Dim index = Me.GetIndexItem(CType(sender, CtrAdvancedFilterLink))
        If index = 0 AndAlso Me.INDlycFilters.Root.Items.ItemCount > 1 Then
            CType(CType(Me.INDlycFilters.Root.Item(1), LayoutControlItem).Control, CtrAdvancedFilterLink).LogicalOperator = Nothing
        End If
        CType(Me.INDlycFilters.Root.Item(index), LayoutControlItem).Control.Parent = Nothing
        CType(Me.INDlycFilters.Root.Item(index), LayoutControlItem).Control.Dispose()
        Me.ClearControls(Me.INDlycFilters.Root.Items.ItemCount <= 0)
        Me.SetFocusControls()
    End Sub

    ''' <summary>
    ''' Se activa cuando un filtro es seleccionado para edición
    ''' </summary>
    Private Sub OnEditFilter(sender As Object, e As AdvancedFilterLinkEventArgs)
        Me._currentFilterIndex = Me.GetIndexItem(CType(sender, CtrAdvancedFilterLink))
        If Me._currentFilterIndex > -1 Then
            Me.ClearControls()
            Me.INDlyciData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If CType(sender, CtrAdvancedFilterLink).LogicalOperator IsNot Nothing Then
                Me.INDcbeOperator.EditValue = CType(sender, CtrAdvancedFilterLink).LogicalOperator
                Me.INDlyciOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                Me.INDcbeOperator.EditValue = Nothing
                Me.INDlyciOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
            Me.INDcbeFields.EditValue = CType(sender, CtrAdvancedFilterLink).Field
            Me.INDcbeCriteria.EditValue = CType(sender, CtrAdvancedFilterLink).Criteria
            Me.INDctrAdvancedFilter.SetEditValues(CType(sender, CtrAdvancedFilterLink).Data)
            Me.INDbtnAdd.Image = INDimcAddIcons.Images(2)
        End If
        Me.SetFocusControls()
    End Sub

    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If Me.INDcbeFields.EditValue IsNot Nothing AndAlso Me.INDcbeCriteria.EditValue IsNot Nothing AndAlso Me.INDctrAdvancedFilter.IsValidValues Then
            If Me._currentFilterIndex < 0 Then
                Dim item = Me.INDlycFilters.Root.AddItem()
                item.ShowInCustomizationForm = False
                item.AllowHide = False
                item.Control = New CtrAdvancedFilterLink(Me.INDcbeFields.EditValue, Me.INDcbeCriteria.EditValue, Me.INDctrAdvancedFilter.GetEditValues(), Microsoft.VisualBasic.IIf(Me.INDlycFilters.Root.Items.ItemCount > 1, Me.INDcbeOperator.EditValue, Nothing))
                CType(item.Control, CtrAdvancedFilterLink).Tag = System.Guid.NewGuid().ToString()
                CType(item.Control, CtrAdvancedFilterLink).StringFormatDate = Me._stringFormatDate
                CType(item.Control, CtrAdvancedFilterLink).StringFormatDateTime = Me._stringFormatDateTime
                CType(item.Control, CtrAdvancedFilterLink).DisplayStringFormatDate = Me._displayStringFormatDate
                CType(item.Control, CtrAdvancedFilterLink).DisplayStringFormatDate = Me._displayStringFormatDate
                AddHandler CType(item.Control, CtrAdvancedFilterLink).DeleteFilter, AddressOf OnDeleteFilter
                AddHandler CType(item.Control, CtrAdvancedFilterLink).EditFilter, AddressOf OnEditFilter
                item.TextVisible = False
                item.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom
                item.MinSize = item.Control.MinimumSize
                item.MaxSize = New Size(0, item.MinSize.Height)
                Me.INDlyciOperator.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.ClearControls()
            Else
                If Me._currentFilterIndex > 0 Then
                    CType(CType(Me.INDlycFilters.Root.Item(Me._currentFilterIndex), LayoutControlItem).Control, CtrAdvancedFilterLink).LogicalOperator = Me.INDcbeOperator.EditValue
                End If
                CType(CType(Me.INDlycFilters.Root.Item(Me._currentFilterIndex), LayoutControlItem).Control, CtrAdvancedFilterLink).Field = Me.INDcbeFields.EditValue
                CType(CType(Me.INDlycFilters.Root.Item(Me._currentFilterIndex), LayoutControlItem).Control, CtrAdvancedFilterLink).Criteria = Me.INDcbeCriteria.EditValue
                CType(CType(Me.INDlycFilters.Root.Item(Me._currentFilterIndex), LayoutControlItem).Control, CtrAdvancedFilterLink).Data = Me.INDctrAdvancedFilter.GetEditValues()
                Me.ClearControls()
                Me._currentFilterIndex = -1
            End If
        Else
            RaiseEvent InvalidValues(Me, New EventArgs())
        End If
        Me.SetFocusControls()
    End Sub

    Private Sub INDcbeFields_EditValueChanged(sender As Object, e As EventArgs) Handles INDcbeFields.EditValueChanged
        If Me.INDcbeFields.EditValue IsNot Nothing Then
            Me.INDcbeCriteria.Properties.DataSource = CType(Me.INDcbeFields.EditValue, ObjectField).ListCriteria
            Me.INDcbeCriteria.EditValue = Nothing
        Else
            Me.INDcbeCriteria.EditValue = Nothing
            Me.INDcbeCriteria.Properties.DataSource = Nothing
        End If
    End Sub

    Private Sub INDcbeCriteria_EditValueChanged(sender As Object, e As EventArgs) Handles INDcbeCriteria.EditValueChanged
        If Me.INDcbeCriteria.EditValue IsNot Nothing Then
            Me.INDctrAdvancedFilter.Criteria = CType(Me.INDcbeCriteria.EditValue, CriteriaAdvancedFilter).TypeCriteria
            Me.INDctrAdvancedFilter.DataType = CType(Me.INDcbeFields.EditValue, ObjectField).DataType
        Else
            Me.INDctrAdvancedFilter.ResetControls()
        End If
    End Sub

    Private Sub CtrAdvancedFilter_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._listOperators = New List(Of LogicalOperatorAdvancedFilter)()
        Me._listOperators.Add(New LogicalOperatorAdvancedFilter(eLogicalOperatorAdvancedFilter.E_And, "Y"))
        Me._listOperators.Add(New LogicalOperatorAdvancedFilter(eLogicalOperatorAdvancedFilter.E_Or, "O"))
        Me.INDcbeOperator.Properties.DataSource = Me._listOperators
        Me.INDcbeOperator.EditValue = Me._listOperators(0)
        If Me._listField IsNot Nothing AndAlso Me._listField.Count > 0 Then
            Me.INDcbeFields.Properties.DataSource = Me._listField
        End If
    End Sub

    Private Sub INDbtnNew_MouseEnter(sender As Object, e As EventArgs) Handles INDbtnNew.MouseEnter
        Me.INDbtnNew.Cursor = Cursors.Default
        Me.INDbtnNew.Image = INDimcAddIcons.Images(7)
    End Sub

    Private Sub INDbtnNew_MouseLeave(sender As Object, e As EventArgs) Handles INDbtnNew.MouseLeave
        Me.INDbtnNew.Cursor = Cursors.Default
        Me.INDbtnNew.Image = INDimcAddIcons.Images(6)
    End Sub

    Private Sub INDbtnRunSearch_MouseEnter(sender As Object, e As EventArgs) Handles INDbtnRunSearch.MouseEnter
        Me.INDbtnRunSearch.Cursor = Cursors.Default
        Me.INDbtnRunSearch.Image = INDimcAddIcons.Images(9)
    End Sub

    Private Sub INDbtnRunSearch_MouseLeave(sender As Object, e As EventArgs) Handles INDbtnRunSearch.MouseLeave
        Me.INDbtnRunSearch.Cursor = Cursors.Default
        Me.INDbtnRunSearch.Image = INDimcAddIcons.Images(8)
    End Sub

    Private Sub INDbtnAdd_MouseEnter(sender As Object, e As EventArgs) Handles INDbtnAdd.MouseEnter
        Me.INDbtnAdd.Cursor = Cursors.Hand
        If Me._currentFilterIndex = -1 Then
            Me.INDbtnAdd.Image = INDimcAddIcons.Images(1)
        Else
            Me.INDbtnAdd.Image = INDimcAddIcons.Images(3)
        End If
    End Sub

    Private Sub INDbtnAdd_MouseLeave(sender As Object, e As EventArgs) Handles INDbtnAdd.MouseLeave
        Me.INDbtnAdd.Cursor = Cursors.Default
        If Me._currentFilterIndex = -1 Then
            Me.INDbtnAdd.Image = INDimcAddIcons.Images(0)
        Else
            Me.INDbtnAdd.Image = INDimcAddIcons.Images(2)
        End If
    End Sub

    Private Sub INDbtnClean_MouseEnter(sender As Object, e As EventArgs) Handles INDbtnClean.MouseEnter
        Me.INDbtnClean.Cursor = Cursors.Hand
        Me.INDbtnClean.Image = INDimcAddIcons.Images(5)
    End Sub

    Private Sub INDbtnClean_MouseLeave(sender As Object, e As EventArgs) Handles INDbtnClean.MouseLeave
        Me.INDbtnClean.Cursor = Cursors.Default
        Me.INDbtnClean.Image = INDimcAddIcons.Images(4)
    End Sub

    Private Sub INDbtnClean_Click(sender As Object, e As EventArgs) Handles INDbtnClean.Click
        Me.ClearControls(True)
        Me._currentFilterIndex = -1
    End Sub

    Private Sub INDbtnNew_Click(sender As Object, e As EventArgs) Handles INDbtnNew.Click
        Me.ClearControls()
        Me._currentFilterIndex = -1
    End Sub

    ''' <summary>
    ''' Se activa cuando es presionada la tecla ENTER en el ultimo control del control de ingreso de datos del usuario
    ''' </summary>
    Private Sub INDctrAdvancedFilter_EnterKeyPressedOnLastControl(sender As Object, e As EventArgs) Handles INDctrAdvancedFilter.EnterKeyPressedOnLastControl
        Me.INDbtnAdd_Click(Me.INDbtnAdd, New EventArgs())
    End Sub

    ''' <summary>
    ''' Aqui se le da el foco al control de datos
    ''' </summary>
    Private Sub INDcbeCriteria_KeyDown(sender As Object, e As KeyEventArgs) Handles INDcbeCriteria.KeyDown
        If e.KeyCode.Equals(Keys.Enter) OrElse e.KeyCode.Equals(Keys.Tab) Then
            Me.INDctrAdvancedFilter.SetFocus()
        End If
    End Sub

    ''' <summary>
    ''' Se lanza el evento de ejecutar la busqueda
    ''' </summary>
    Private Sub INDbtnRunSearch_Click(sender As Object, e As EventArgs) Handles INDbtnRunSearch.Click
        Dim resultsNumber As Integer
        resultsNumber = If(Integer.TryParse(Me.INDcbeResultsNumber.EditValue, resultsNumber), resultsNumber, 2147483647)
        RaiseEvent RunSearch(Me, New RunSearchEventArgs With {.QueryString = Me.QueryString, .TopResult = resultsNumber})
    End Sub

#End Region

End Class

#Region "Class"

''' <summary>
''' Encapsula los argumentos del evento RunSearch
''' </summary>
Public Class RunSearchEventArgs

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna la cadena de filtros para la busqueda
    ''' </summary>
    ''' <value>Cadena de filtros para la busqueda</value>
    ''' <returns>La cadena de filtros para la busqueda</returns>
    Public Property QueryString As String
    ''' <summary>
    ''' Obtiene o asigna el maximo de resultados a retornar en la busqueda
    ''' </summary>
    ''' <value>Maximo de resultados</value>
    ''' <returns>El maximo de resultados</returns>
    Public Property TopResult As Integer

#End Region

End Class

''' <summary>
''' Encapsula los datos del campo a filtrar
''' </summary>
<Serializable()>
Public Class ObjectField

#Region "Fields"

    Private _displayMember As String
    Private _dataType As eTypes
    Private _valueMember As String
    Private _listCriteria As List(Of CriteriaAdvancedFilter)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene la instancia del objeto
    ''' </summary>
    ''' <returns>Instancia del objeto</returns>
    <Browsable(False)>
    Public ReadOnly Property This As ObjectField
        Get
            Return Me
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre para mostrar del campo
    ''' </summary>
    ''' <value>Nombre del campo</value>
    ''' <returns>El nombre del campo</returns>
    Public Property DisplayMember() As String
        Get
            Return Me._displayMember
        End Get
        Set(value As String)
            Me._displayMember = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el tipo de dato del campo
    ''' </summary>
    ''' <value>Tipo de dato del campo</value>
    ''' <returns>El tipo de dato del campo</returns>
    Public Property DataType() As eTypes
        Get
            Return Me._dataType
        End Get
        Set(value As eTypes)
            Me._dataType = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nombre del campo en el origen de datos
    ''' </summary>
    ''' <value>Nombre del campo</value>
    ''' <returns>El nombre del campo</returns>
    Public Property ValueMember() As String
        Get
            Return Me._valueMember
        End Get
        Set(value As String)
            Me._valueMember = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de criterios aceptados por el campo
    ''' </summary>
    ''' <value>Lista de criterios</value>
    ''' <returns>La lista de criterios</returns>
    Public Property ListCriteria As List(Of CriteriaAdvancedFilter)
        Get
            Return Me._listCriteria
        End Get
        Set(value As List(Of CriteriaAdvancedFilter))
            Me._listCriteria = value
        End Set
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me._displayMember = String.Empty
        Me._valueMember = String.Empty
        Me._dataType = eTypes.E_Nothing
        Me._listCriteria = New List(Of CriteriaAdvancedFilter)()
    End Sub

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    ''' <param name="display">Nombre para mostrar</param>
    ''' <param name="value">Nombre del campo en el origen de datos</param>
    ''' <param name="dataType">Tipo de dato del campo</param>
    ''' <param name="listCriteria">Lista de criterios soportados por el campo</param>
    Public Sub New(ByVal display As String, value As String, ByVal dataType As eTypes, ByVal listCriteria As List(Of CriteriaAdvancedFilter))
        Me._displayMember = display.Trim()
        Me._valueMember = value.Trim()
        Me._dataType = dataType
        Me._listCriteria = listCriteria
    End Sub

#End Region

End Class

''' <summary>
''' Encapsula los Datos de un criterio de consulta
''' </summary>
<Serializable()>
Public Class CriteriaAdvancedFilter

#Region "Properties"

    ''' <summary>
    ''' Obtiene o asigna el tipo de criterio
    ''' </summary>
    ''' <value>Tipo de criterio</value>
    ''' <returns>El tipo de criterio</returns>
    Public Property TypeCriteria As eCriteriaAdvancedFilter

    ''' <summary>
    ''' Obtiene o asigna el valor usuado para generar la cadena de consulta
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Browsable(False)>
    Public ReadOnly Property ValueMember As String
        Get
            Return Me.GetValueMember()
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor a Mostrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DisplayMember As String

    ''' <summary>
    ''' Obtiene la instancia del objeto
    ''' </summary>
    ''' <returns>Instancia del objeto</returns>
    <Browsable(False)>
    Public ReadOnly Property This As CriteriaAdvancedFilter
        Get
            Return Me
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.TypeCriteria = eCriteriaAdvancedFilter.Equals
        Me.DisplayMember = String.Empty
    End Sub

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    ''' <param name="typeCriteria">Tipo de criterio</param>
    ''' <param name="display">Texto para mostrar</param>
    Public Sub New(ByVal typeCriteria As eCriteriaAdvancedFilter, ByVal display As String)
        Me.TypeCriteria = typeCriteria
        Me.DisplayMember = display.Trim()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el valor para mostrar segun el tipo de criterio
    ''' </summary>
    ''' <returns>Simbolo para mostrar como valor de criterio</returns>
    Private Function GetValueMember() As String
        Select Case Me.TypeCriteria
            Case eCriteriaAdvancedFilter.Between
                Return "between"
            Case eCriteriaAdvancedFilter.Contains
                Return "like"
            Case eCriteriaAdvancedFilter.EndWith
                Return "like"
            Case eCriteriaAdvancedFilter.Equals
                Return "="
            Case eCriteriaAdvancedFilter.GreaterOrEquals
                Return ">="
            Case eCriteriaAdvancedFilter.GreaterThan
                Return ">"
            Case eCriteriaAdvancedFilter.LessOrEquals
                Return "<="
            Case eCriteriaAdvancedFilter.LessThan
                Return "<"
            Case eCriteriaAdvancedFilter.NotEquals
                Return "!="
            Case eCriteriaAdvancedFilter.StartWith
                Return "like"
        End Select
    End Function

    Public Overrides Function ToString() As String
        Return Me.DisplayMember.Trim()
    End Function

#End Region

End Class

''' <summary>
''' Encapsula los datos de un operador logico
''' </summary>
<Serializable()>
Public Class LogicalOperatorAdvancedFilter

#Region "Properties"

    ''' <summary>
    ''' Obtiene la instancia del objeto
    ''' </summary>
    ''' <returns>Instancia del objeto</returns>
    <Browsable(False)>
    Public ReadOnly Property This As LogicalOperatorAdvancedFilter
        Get
            Return Me
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el valor usuado como simbolo del operador para generar la cadena de consulta
    ''' </summary>
    ''' <value>Simbolo usado como valor del operador</value>
    ''' <returns>El simbolo usado como valor del operador</returns>
    <Browsable(False)>
    Public ReadOnly Property ValueMember As String
        Get
            Return Me.GetValueMember()
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna el valor de texto para mostrar del operador
    ''' </summary>
    ''' <value></value>
    ''' <returns>Texto para mostrar</returns>
    ''' <remarks>El texto para mostrar</remarks>
    Public Property DisplayMember As String

    ''' <summary>
    ''' Obtiene o asigna el tipo de operador
    ''' </summary>
    Public Property TypeOperator As eLogicalOperatorAdvancedFilter

#End Region

#Region "Builders"

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        Me.TypeOperator = eLogicalOperatorAdvancedFilter.E_And
        Me.DisplayMember = String.Empty
    End Sub

    ''' <summary>
    ''' Obtiene una nueva instancia de la clase
    ''' </summary>
    ''' <param name="typeOperator">Tipo de operador</param>
    ''' <param name="display">Texto para mostrar</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal typeOperator As eLogicalOperatorAdvancedFilter, ByVal display As String)
        Me.TypeOperator = typeOperator
        Me.DisplayMember = display.Trim()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el simbolo usado como valor dependiendo del tipo de operador
    ''' </summary>
    ''' <returns>Simbolo usado como valor</returns>
    Private Function GetValueMember() As String
        Select Case Me.TypeOperator
            Case eLogicalOperatorAdvancedFilter.E_And
                Return "and"
            Case eLogicalOperatorAdvancedFilter.E_Or
                Return "or"
        End Select
    End Function

    Public Overrides Function ToString() As String
        Return Me.DisplayMember.Trim()
    End Function

#End Region

End Class

#End Region

#Region "Enums"

''' <summary>
''' Enumera los tipos de criterios
''' </summary>
<Serializable()>
Public Enum eCriteriaAdvancedFilter
    ''' <summary>
    ''' Igual
    ''' </summary>
    Equals
    ''' <summary>
    ''' Diferente
    ''' </summary>
    NotEquals
    ''' <summary>
    ''' Menor que
    ''' </summary>
    LessThan
    ''' <summary>
    ''' Menor o igual
    ''' </summary>
    LessOrEquals
    ''' <summary>
    ''' Mayor que
    ''' </summary>
    GreaterThan
    ''' <summary>
    ''' Mayor o igual
    ''' </summary>
    GreaterOrEquals
    ''' <summary>
    ''' Inicia con
    ''' </summary>
    StartWith
    ''' <summary>
    ''' Finaliza con
    ''' </summary>
    EndWith
    ''' <summary>
    ''' Contiene
    ''' </summary>
    Contains
    ''' <summary>
    ''' Esta entre
    ''' </summary>
    Between
End Enum

''' <summary>
''' Enumera los tipos de operadores lógicos
''' </summary>
<Serializable()>
Public Enum eLogicalOperatorAdvancedFilter
    ''' <summary>
    ''' Y
    ''' </summary>
    E_And
    ''' <summary>
    ''' O
    ''' </summary>
    E_Or
End Enum

#End Region