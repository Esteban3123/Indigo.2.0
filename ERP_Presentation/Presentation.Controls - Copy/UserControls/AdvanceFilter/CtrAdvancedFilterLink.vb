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

Imports System
Imports System.Text
Imports System.Globalization

#End Region

Friend Class CtrAdvancedFilterLink

#Region "Consts"

    ''' <summary>
    ''' Define la cultura usada para formatear tipos de dato numericos y fecha
    ''' </summary>
    Private Const CULTURE As String = "es-ES"
    ''' <summary>
    ''' Define el retardo que se toma para la espera en el evento click
    ''' </summary>
    Private Const RELAY As Double = 50

#End Region

#Region "Delegates"

    ''' <summary>
    ''' Delegado para ejecutar el evento EditFilter
    ''' </summary>
    Private Delegate Sub OnEditFilterDelegate(ByVal sender As Object, ByVal e As AdvancedFilterLinkEventArgs)

#End Region

#Region "Fields"

    ''' <summary>
    ''' Timer para realizar el retardo en el evento click
    ''' </summary>
    Private WithEvents _relay As Timers.Timer

    ''' <summary>
    ''' Bandera usada para controlar el evento click
    ''' </summary>
    Private _clickFlag As Boolean = False
    ''' <summary>
    ''' Encapsula el campo de filtrado
    ''' </summary>
    ''' <remarks></remarks>
    Private _field As ObjectField
    ''' <summary>
    ''' Encapsula el criterio de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Private _criteria As CriteriaAdvancedFilter
    ''' <summary>
    ''' Encapsula el dato a buscar
    ''' </summary>
    ''' <remarks></remarks>
    Private _data As List(Of Object)
    ''' <summary>
    ''' Encapsula el operador lógico
    ''' </summary>
    ''' <remarks></remarks>
    Private _operator As LogicalOperatorAdvancedFilter
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

    Public Property Field As ObjectField
        Get
            Return Me._field
        End Get
        Set(value As ObjectField)
            Me._field = value
            Me._data = Nothing
            Me.RefreshDisplayText()
        End Set
    End Property

    Public Property Criteria As CriteriaAdvancedFilter
        Get
            Return Me._criteria
        End Get
        Set(value As CriteriaAdvancedFilter)
            Me._criteria = value
            Me.RefreshDisplayText()
        End Set
    End Property

    Public Property Data As List(Of Object)
        Get
            Return Me._data
        End Get
        Set(value As List(Of Object))
            Me._data = value
            Me.RefreshDisplayText()
        End Set
    End Property

    Public Property LogicalOperator As LogicalOperatorAdvancedFilter
        Get
            Return Me._operator
        End Get
        Set(value As LogicalOperatorAdvancedFilter)
            Me._operator = value
            Me.RefreshDisplayText()
        End Set
    End Property

    Public Property StringFormatDate As String
        Get
            Return Me._stringFormatDate.Trim()
        End Get
        Set(value As String)
            Me._stringFormatDate = value.Trim()
        End Set
    End Property

    Public Property StringFormatDateTime As String
        Get
            Return Me._stringFormatDateTime.Trim()
        End Get
        Set(value As String)
            Me._stringFormatDateTime = value.Trim()
        End Set
    End Property

    Public Property DisplayStringFormatDate As String
        Get
            Return Me._displayStringFormatDate.Trim()
        End Get
        Set(value As String)
            Me._displayStringFormatDate = value.Trim()
            Me.RefreshDisplayText()
        End Set
    End Property

    Public Property DisplayStringFormatDateTime As String
        Get
            Return Me._displayStringFormatDateTime.Trim()
        End Get
        Set(value As String)
            Me._displayStringFormatDateTime = value.Trim()
            Me.RefreshDisplayText()
        End Set
    End Property

#End Region

#Region "Builders"

    Public Sub New()
        InitializeComponent()
        Me._field = Nothing
        Me._criteria = Nothing
        Me._data = Nothing
        Me._operator = Nothing
        Me.RefreshDisplayText()
        Me._relay = New Timers.Timer(RELAY)
        Me._relay.Enabled = False
    End Sub

    Public Sub New(ByVal field As ObjectField, ByVal criteria As CriteriaAdvancedFilter, ByVal data As List(Of Object), ByVal logicalOperator As LogicalOperatorAdvancedFilter)
        InitializeComponent()
        Me._field = field
        Me._criteria = criteria
        Me._data = data
        Me._operator = logicalOperator
        Me.RefreshDisplayText()
        Me._relay = New Timers.Timer(RELAY)
        Me._relay.Enabled = False
    End Sub

#End Region

#Region "Handlers"

    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        Me._clickFlag = True
        RaiseEvent DeleteFilter(Me, New AdvancedFilterLinkEventArgs With {.Filter = Me})
    End Sub

    Private Sub INDbteCode_Click(sender As Object, e As EventArgs) Handles INDbteCode.Click
        Me._relay.Enabled = True
    End Sub

    Private Sub _relay_Elapsed(sender As Object, e As Timers.ElapsedEventArgs) Handles _relay.Elapsed
        Me._relay.Enabled = False
        If Not Me._clickFlag Then
            Me.OnEditFilter(Me, New AdvancedFilterLinkEventArgs With {.Filter = Me})
        Else
            Me._clickFlag = False
        End If
    End Sub

    Private Sub OnEditFilter(ByVal sender As Object, ByVal e As AdvancedFilterLinkEventArgs)
        If Me.InvokeRequired Then
            Dim dl As New OnEditFilterDelegate(AddressOf OnEditFilter)
            Me.Invoke(dl, New Object() {sender, e})
        Else
            RaiseEvent EditFilter(sender, e)
        End If
    End Sub

    Private Sub INDbteCode_GotFocus(sender As Object, e As EventArgs) Handles INDbteCode.GotFocus
        Me.INDlblAux.Focus()
    End Sub

    Private Sub INDbteCode_MouseEnter(sender As Object, e As EventArgs) Handles INDbteCode.MouseEnter
        Me.INDbteCode.Font = New Font("Segoe UI", 12.0!, FontStyle.Underline)
        Me.INDbteCode.ForeColor = Color.FromArgb(0, 144, 223)
        Me.INDbteCode.Cursor = Cursors.Hand
        Me.INDbteCode.Properties.Buttons(0).Image = Me.IMCIcons.Images(1)
    End Sub

    Private Sub INDbteCode_MouseLeave(sender As Object, e As EventArgs) Handles INDbteCode.MouseLeave
        Me.INDbteCode.Font = New Font("Segoe UI", 12.0!, FontStyle.Regular)
        Me.INDbteCode.ForeColor = Color.Black
        Me.INDbteCode.Cursor = Cursors.Default
        Me.INDbteCode.Properties.Buttons(0).Image = Me.IMCIcons.Images(0)
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la cadena de codigo
    ''' </summary>
    ''' <returns>Cadena de codigo</returns>
    Public Function GetFilterCode() As String
        Dim sb As New StringBuilder()
        sb.Append(Me._field.ValueMember & " " & Me._criteria.ValueMember & " ")
        If Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.Between Then
            If Me._field.DataType = eTypes.E_String OrElse Me._field.DataType = eTypes.E_Char Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 1 Then
                    sb.Append("'" & Me._data(0) & "' and '" & Me._data(1) & "'")
                End If
            ElseIf Me._field.DataType = eTypes.E_DateTime OrElse Me._field.DataType = eTypes.E_Date Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 1 Then
                    sb.Append("'" & CType(Me._data(0), DateTime).ToString(Microsoft.VisualBasic.IIf(Me._field.DataType = eTypes.E_DateTime, Me._stringFormatDateTime, Me._stringFormatDate)) & "' and '" & CType(Me._data(1), DateTime).ToString(Microsoft.VisualBasic.IIf(Me._field.DataType = eTypes.E_DateTime, Me._stringFormatDateTime, Me._stringFormatDate)) & "'")
                End If
            Else
                If Me._data IsNot Nothing AndAlso Me._data.Count > 1 Then
                    Select Case Me._field.DataType
                        Case eTypes.E_Byte
                            sb.Append(CType(Me._data(0), Byte).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_SByte
                            sb.Append(CType(Me._data(0), SByte).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Decimal
                            sb.Append(CType(Me._data(0), Decimal).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Double
                            sb.Append(CType(Me._data(0), Double).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int16
                            sb.Append(CType(Me._data(0), Int16).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int32
                            sb.Append(CType(Me._data(0), Int32).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int64
                            sb.Append(CType(Me._data(0), Int64).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt16
                            sb.Append(CType(Me._data(0), UInt16).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt32
                            sb.Append(CType(Me._data(0), UInt32).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt64
                            sb.Append(CType(Me._data(0), UInt64).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Single
                            sb.Append(CType(Me._data(0), Single).ToString(New CultureInfo(CULTURE, True)) & " and " & CType(Me._data(1), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case Else
                            sb.Append(Me._data(0) & " and " & Me._data(1))
                    End Select
                End If
            End If
        ElseIf Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.StartWith Then
            If Me._field.DataType = eTypes.E_String OrElse Me._field.DataType = eTypes.E_Char Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append("'" & Me._data(0) & "%'")
                End If
            End If
        ElseIf Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.Contains Then
            If Me._field.DataType = eTypes.E_String OrElse Me._field.DataType = eTypes.E_Char Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append("'%" & Me._data(0) & "%'")
                End If
            End If
        ElseIf Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.EndWith Then
            If Me._field.DataType = eTypes.E_String OrElse Me._field.DataType = eTypes.E_Char Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append("'%" & Me._data(0) & "'")
                End If
            End If
        ElseIf Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.Equals Then
            If Me._field.DataType = eTypes.E_String OrElse Me._field.DataType = eTypes.E_Char Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append("'" & Me._data(0) & "'")
                End If
            ElseIf Me._field.DataType = eTypes.E_DateTime OrElse Me._field.DataType = eTypes.E_Date Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append("'" & CType(Me._data(0), DateTime).ToString(Microsoft.VisualBasic.IIf(Me._field.DataType = eTypes.E_DateTime, Me._stringFormatDateTime, Me._stringFormatDate)) & "'")
                End If
            Else
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    Select Case Me._field.DataType
                        Case eTypes.E_Byte
                            sb.Append(CType(Me._data(0), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_SByte
                            sb.Append(CType(Me._data(0), SByte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Decimal
                            sb.Append(CType(Me._data(0), Decimal).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Double
                            sb.Append(CType(Me._data(0), Double).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int16
                            sb.Append(CType(Me._data(0), Int16).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int32
                            sb.Append(CType(Me._data(0), Int32).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int64
                            sb.Append(CType(Me._data(0), Int64).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt16
                            sb.Append(CType(Me._data(0), UInt16).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt32
                            sb.Append(CType(Me._data(0), UInt32).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt64
                            sb.Append(CType(Me._data(0), UInt64).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Single
                            sb.Append(CType(Me._data(0), Single).ToString(New CultureInfo(CULTURE, True)))
                        Case Else
                            sb.Append(Me._data(0))
                    End Select
                End If
            End If
        ElseIf Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.NotEquals Then
            If Me._field.DataType = eTypes.E_String OrElse Me._field.DataType = eTypes.E_Char Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append("'" & Me._data(0) & "'")
                End If
            ElseIf Me._field.DataType = eTypes.E_DateTime OrElse Me._field.DataType = eTypes.E_Date Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append("'" & CType(Me._data(0), DateTime).ToString(Microsoft.VisualBasic.IIf(Me._field.DataType = eTypes.E_DateTime, Me._stringFormatDateTime, Me._stringFormatDate)) & "'")
                End If
            Else
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    Select Case Me._field.DataType
                        Case eTypes.E_Byte
                            sb.Append(CType(Me._data(0), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_SByte
                            sb.Append(CType(Me._data(0), SByte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Decimal
                            sb.Append(CType(Me._data(0), Decimal).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Double
                            sb.Append(CType(Me._data(0), Double).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int16
                            sb.Append(CType(Me._data(0), Int16).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int32
                            sb.Append(CType(Me._data(0), Int32).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int64
                            sb.Append(CType(Me._data(0), Int64).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt16
                            sb.Append(CType(Me._data(0), UInt16).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt32
                            sb.Append(CType(Me._data(0), UInt32).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt64
                            sb.Append(CType(Me._data(0), UInt64).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Single
                            sb.Append(CType(Me._data(0), Single).ToString(New CultureInfo(CULTURE, True)))
                        Case Else
                            sb.Append(Me._data(0))
                    End Select
                End If
            End If
        ElseIf Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.LessThan OrElse Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.LessOrEquals OrElse Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.GreaterThan OrElse Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.GreaterOrEquals Then
            If Me._field.DataType = eTypes.E_Double OrElse Me._field.DataType = eTypes.E_Decimal OrElse Me._field.DataType = eTypes.E_Byte OrElse Me._field.DataType = eTypes.E_Int16 OrElse Me._field.DataType = eTypes.E_Int32 OrElse Me._field.DataType = eTypes.E_Int64 OrElse Me._field.DataType = eTypes.E_UInt16 OrElse Me._field.DataType = eTypes.E_UInt32 OrElse Me._field.DataType = eTypes.E_UInt64 OrElse Me._field.DataType = eTypes.E_Single OrElse Me._field.DataType = eTypes.E_SByte Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    Select Case Me._field.DataType
                        Case eTypes.E_Byte
                            sb.Append(CType(Me._data(0), Byte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_SByte
                            sb.Append(CType(Me._data(0), SByte).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Decimal
                            sb.Append(CType(Me._data(0), Decimal).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Double
                            sb.Append(CType(Me._data(0), Double).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int16
                            sb.Append(CType(Me._data(0), Int16).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int32
                            sb.Append(CType(Me._data(0), Int32).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Int64
                            sb.Append(CType(Me._data(0), Int64).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt16
                            sb.Append(CType(Me._data(0), UInt16).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt32
                            sb.Append(CType(Me._data(0), UInt32).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_UInt64
                            sb.Append(CType(Me._data(0), UInt64).ToString(New CultureInfo(CULTURE, True)))
                        Case eTypes.E_Single
                            sb.Append(CType(Me._data(0), Single).ToString(New CultureInfo(CULTURE, True)))
                        Case Else
                            sb.Append(Me._data(0))
                    End Select
                End If
            ElseIf Me._field.DataType = eTypes.E_DateTime OrElse Me._field.DataType = eTypes.E_Date Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                    sb.Append(Me._data(0))
                End If
            End If
        End If
        If Me._operator IsNot Nothing Then
            sb.Insert(0, Me._operator.ValueMember & " ")
        End If
        Return sb.ToString()
    End Function

    Public Overrides Function ToString() As String
        Dim sb As New StringBuilder()
        sb.Append("[" & Me._field.DisplayMember & "] " & Me._criteria.DisplayMember & " ")
        If Me._criteria.TypeCriteria = eCriteriaAdvancedFilter.Between Then
            If Me._field.DataType = eTypes.E_DateTime OrElse Me._field.DataType = eTypes.E_Date Then
                If Me._data IsNot Nothing AndAlso Me._data.Count > 1 Then
                    sb.Append("[" & CType(Me._data(0), DateTime).ToString(Microsoft.VisualBasic.IIf(Me._field.DataType = eTypes.E_DateTime, Me._displayStringFormatDateTime, Me._displayStringFormatDate)) & "] - [" & CType(Me._data(1), DateTime).ToString(Microsoft.VisualBasic.IIf(Me._field.DataType = eTypes.E_DateTime, Me._displayStringFormatDateTime, Me._displayStringFormatDate)) & "]")
                End If
            ElseIf Me._data IsNot Nothing AndAlso Me._data.Count > 1 Then
                sb.Append("[" & Me._data(0) & " - " & Me._data(1) & "]")
            End If
        ElseIf Me._field.DataType = eTypes.E_DateTime OrElse Me._field.DataType = eTypes.E_Date Then
            If Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
                sb.Append("[" & CType(Me._data(0), DateTime).ToString(Microsoft.VisualBasic.IIf(Me._field.DataType = eTypes.E_DateTime, Me._displayStringFormatDateTime, Me._displayStringFormatDate)) & "]")
            End If
        ElseIf Me._data IsNot Nothing AndAlso Me._data.Count > 0 Then
            sb.Append("[" & Me._data(0) & "]")
        End If
        If Me._operator IsNot Nothing Then
            sb.Insert(0, Me._operator.DisplayMember & " ")
        End If
        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Refresca el texto mostrado como código para mostrar del filtro
    ''' </summary>
    Private Sub RefreshDisplayText()
        If Me._field IsNot Nothing AndAlso Me._criteria IsNot Nothing AndAlso Me._data IsNot Nothing Then
            Me.INDbteCode.Text = Me.ToString()
        Else
            Me.INDbteCode.Text = Me.Name
        End If
    End Sub

#End Region

#Region "Events"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event DeleteFilter(ByVal sender As Object, ByVal e As AdvancedFilterLinkEventArgs)
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Public Event EditFilter(ByVal sender As Object, ByVal e As AdvancedFilterLinkEventArgs)

#End Region

End Class

Friend Class AdvancedFilterLinkEventArgs
    Inherits EventArgs

    Public Property Filter As CtrAdvancedFilterLink

End Class

