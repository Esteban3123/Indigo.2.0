Imports System.Runtime.CompilerServices
Imports System.Globalization
Imports Infrastructure.CrossCutting.Base

Public Module Extension

    ''' <summary>
    ''' Funcion para convertir un decimal a formato de moneda
    ''' </summary>
    ''' <param name="value">Valor que se desea convertir</param>
    ''' <param name="numberDecimal">Numero de decimales que se va tener</param>
    ''' <returns>Numero formateado a la localizacion configurada</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function MoneyFormat(value As Decimal, Optional numberDecimal As Integer = 2, Optional Culture As CultureInfo = Nothing) As String
        'Dim culture As CultureInfo = CultureInfo.CreateSpecificCulture()
        Return value.ToString("C" & numberDecimal, If(Culture, SessionValues.Instance.Culture))
    End Function

    ''' <summary>
    ''' Funcion para convertir un entero a formato de moneda
    ''' </summary>
    ''' <param name="value">Valor que se desea convertir</param>
    ''' <param name="numberDecimal">Numero de decimales que se va tener</param>
    ''' <returns>Numero formateado a la localizacion configurada</returns>
    ''' <remarks></remarks>
    <Extension()>
    Public Function MoneyFormat(value As Integer, Optional numberDecimal As Integer = 2, Optional Culture As CultureInfo = Nothing) As String
        'Dim culture As CultureInfo = CultureInfo.CreateSpecificCulture("es-CO")
        Return value.ToString("C" & numberDecimal, If(Culture, SessionValues.Instance.Culture))
    End Function

    ''' <summary>
    ''' Hides the control.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub HideControl(layout As DevExpress.XtraLayout.LayoutControlItem, Optional hide As Boolean = True)
        If hide Then
            layout.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            layout.AllowHide = True
        Else
            layout.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            layout.AllowHide = False
        End If
    End Sub

    ''' <summary>
    ''' Hides the control.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub ShowLayout(layout As DevExpress.XtraLayout.LayoutControlItem)
        layout.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        layout.AllowHide = False
    End Sub

    ''' <summary>
    ''' Hides the control.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub HideLayout(layout As DevExpress.XtraLayout.LayoutControlItem)
        layout.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        layout.AllowHide = True
    End Sub

    ''' <summary>
    ''' Hides the control.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub HideControl(layout As DevExpress.XtraLayout.LayoutControlGroup, Optional hide As Boolean = True)
        If hide Then
            layout.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            layout.AllowHide = True
        Else
            layout.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            layout.AllowHide = False
        End If
    End Sub

    ''' <summary>
    ''' Aplica visibilidad a una colección de controles de Layout de DevExpress.
    ''' </summary>
    ''' <param name="controls">Colección de controles Layout (LayoutControlGroup o LayoutControlItem)</param>
    ''' <param name="isVisible">True para mostrar, False para ocultar</param>

    <Extension()>
    Public Sub ApplyControlVisibility(controls As IEnumerable(Of Object), isVisible As Boolean)
        For Each ctrl In controls
            If TypeOf ctrl Is DevExpress.XtraLayout.LayoutControlGroup Then
                Dim layoutGroup = CType(ctrl, DevExpress.XtraLayout.LayoutControlGroup)
                layoutGroup.HideControl(Not isVisible)

            ElseIf TypeOf ctrl Is DevExpress.XtraLayout.LayoutControlItem Then
                Dim layoutItem = CType(ctrl, DevExpress.XtraLayout.LayoutControlItem)
                If isVisible Then
                    layoutItem.ShowLayout()
                Else
                    layoutItem.HideLayout()
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Hides the control.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub HideControl(column As DevExpress.XtraGrid.Columns.GridColumn, Optional hide As Boolean = True, Optional visibleIndex As Integer? = Nothing)
        column.Visible = Not hide
        column.OptionsColumn.ShowInCustomizationForm = Not hide
        If Not hide AndAlso visibleIndex IsNot Nothing Then
            column.VisibleIndex = visibleIndex
        End If
    End Sub

    ''' <summary>
    ''' Hides the control.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub HideColumn(column As DevExpress.XtraGrid.Columns.GridColumn, Optional visibleIndex As Integer? = Nothing)
        column.Visible = False
        column.OptionsColumn.ShowInCustomizationForm = False
        If visibleIndex IsNot Nothing Then
            column.VisibleIndex = visibleIndex
        End If
    End Sub

    ''' <summary>
    ''' Hides the control.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub ShowColumn(column As DevExpress.XtraGrid.Columns.GridColumn, Optional visibleIndex As Integer? = Nothing)
        column.Visible = True
        column.OptionsColumn.ShowInCustomizationForm = True
        If visibleIndex IsNot Nothing Then
            column.VisibleIndex = visibleIndex
        End If
    End Sub


    ''' <summary>
    ''' Create column in gridview.
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub SetDataMainGridColumn(column As DevExpress.XtraGrid.Columns.GridColumn, caption As String, fieldName As String, Optional visibleIndex As Integer? = Nothing)
        column.Caption = caption
        column.FieldName = fieldName
        column.OptionsColumn.AllowEdit = False
        column.OptionsColumn.AllowFocus = False

        column.ShowColumn(visibleIndex)
    End Sub

    ''' <summary>
    ''' Formato númerico para las columnas de la rejilla
    ''' </summary>
    ''' <param name="layout">The layout.</param>
    ''' <param name="hide">if set to <c>true</c> [hide].</param>
    <Extension()>
    Public Sub SetFormatNumericGridColumn(column As DevExpress.XtraGrid.Columns.GridColumn)
        column.DisplayFormat.FormatString = "c2"
        column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
    End Sub

    <Extension>
    Public Function IsNotNullAndAny(Of T)(list As IEnumerable(Of T))
        Return list IsNot Nothing AndAlso list.Any()
    End Function

    <Extension>
    Public Function IsNotNull(value As Object)
        Return value IsNot Nothing
    End Function

    <Extension>
    Public Function IsNull(value As Object)
        Return value Is Nothing
    End Function

#Region "GridView"
    ''' <summary>
    ''' Obtiene el objeto que lleva el foco en la rejilla
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="view"></param>
    ''' <returns></returns>
    <Extension>
    Public Function GetFocusedObject(Of T)(ByVal view As DevExpress.XtraGrid.Views.Grid.GridView) As T
        Dim o = view.GetFocusedRow()
        If view.GridControl.DataSource IsNot Nothing Then
            Dim isServerModeFlag = view.GridControl.DataSource.GetType().Name.EndsWith("InstantFeedbackSource")
            If isServerModeFlag Then 'Si la fuente de datos es de tipo servidor (e.g XPInstantFeedbackSource)
                If TypeOf o Is DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread Then
                    Return CType(o, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                Else
                    Return Nothing
                End If
            Else
                Return o
            End If
        End If
        Return Nothing
    End Function
#End Region

#Region "SearchLookUp"
    ''' <summary>
    ''' Obtiene el objeto seleccionado en la fuente de datos según el EditValue
    ''' </summary>
    ''' <param name="obj">Objeto que extiende el metodo</param>
    ''' <returns>Objeto seleccionado</returns>
    <Extension>
    Public Function GetFocusedObject(Of T)(ByVal obj As DevExpress.XtraEditors.SearchLookUpEdit) As T
        If obj.Properties.DataSource IsNot Nothing AndAlso obj.Properties.ValueMember IsNot Nothing AndAlso Not obj.Properties.ValueMember.Trim().Equals(String.Empty) AndAlso obj.EditValue IsNot Nothing Then
            If obj.Properties.View.DataController.GetAllFilteredAndSortedRows().Count > 0 Then 'Se ha aplicado un filtro o un ordenamiento
                Dim isServerModeFlag = obj.Properties.DataSource.GetType().Name.EndsWith("InstantFeedbackSource")
                For Each o In obj.Properties.View.DataController.GetAllFilteredAndSortedRows()
                    If isServerModeFlag Then 'Si la fuente de datos es de tipo servidor (e.g XPInstantFeedbackSource)
                        Dim entity = CType(o, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
                        Dim propInf = entity.GetType().GetProperty(obj.Properties.ValueMember)
                        If propInf IsNot Nothing Then
                            Dim val = propInf.GetValue(entity)
                            If val.Equals(obj.EditValue) Then
                                Return entity
                            End If
                        End If
                    Else
                        Dim propInf = o.GetType().GetProperty(obj.Properties.ValueMember)
                        If propInf IsNot Nothing Then
                            Dim val = propInf.GetValue(o)
                            If val.Equals(obj.EditValue) Then
                                Return o
                            End If
                        End If
                    End If
                Next
            Else 'Se obtiene de forma normal
                Return obj.Properties.View.GetFocusedRow()
            End If
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Obtiene el objeto seleccionado en la fuente de datos según el EditValue
    ''' </summary>
    ''' <param name="obj">Objeto que extiende el metodo</param>
    ''' <returns>Objeto seleccionado</returns>
    <Extension>
    Public Function GetFocusedRow(Of T)(ByVal obj As DevExpress.XtraEditors.SearchLookUpEdit) As T
        If obj.Properties.View.GetFocusedRow().GetType().Name.Equals(GetType(DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).Name) Then
            Return DirectCast(obj.Properties.View.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        Else
            Return obj.Properties.View.GetFocusedRow()
        End If
    End Function

#End Region
End Module
