#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Common.MVP
#End Region

Public Class FrmSettingBillingCurrency
    Implements ICrudBase
#Region "Variables"
    ''' <summary>
    ''' Muestre y escribe el mensaje de retorno por las operaciones
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Variable de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim flag As Boolean = False

#End Region

#Region "Properties"

    Public Property EditMode As Boolean

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordBilling

    Public Property CostInventoryGroupDetail As CostInventoryGroupDetail

    Public Property ListCostInventoryGroupDetail As List(Of CostInventoryGroupDetail)

    ''' <summary>
    ''' Propiedad que establece la fecha en que se tomó de medicion para la tasa representativa del mercado
    ''' </summary>
    Public Property InitialMeasurementDate As Date?
        Get
            Return INDdteInitialMeasurementDate.EditValue
        End Get
        Set(value As Date?)
            INDdteInitialMeasurementDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la fecha en que se tomó de medicion para la tasa representativa del mercado
    ''' </summary>
    Public Property FinalMeasurementDate As Date?
        Get
            Return INDdteFinalMeasurementDate.DateTime
        End Get
        Set(value As Date?)
            INDdteFinalMeasurementDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el valor de la tasa representativa del mercado
    ''' </summary>
    Public Property Value As Double
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Double)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el valor de la tasa representativa del mercado
    ''' </summary>
    Public Property CurrencyToConvertId As Integer
        Get
            Return INDsleCurrencyToConvert.EditValue
        End Get
        Set(value As Integer)
            INDsleCurrencyToConvert.EditValue = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad que establece el datasource de la rejilla
    ''' </summary>
    Private Property ListTRM As List(Of CustomTRM)
        Get
            Return TryCast(INDgcCurrency.DataSource, List(Of CustomTRM))
        End Get
        Set(value As List(Of CustomTRM))
            INDgcCurrency.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' Propiedad con la lista de los items eliminados d ela rejilla
    ''' </summary>
    Private Property _listTRMDelete As List(Of CustomTRM)


    ''' <summary>
    ''' Propiedad con la lista de todos los items que seran enviados a guardar
    ''' </summary>
    Private Property _listAllTRM As List(Of CustomTRM)
#End Region

#Region "Events"

    Private Async Sub CtrSettingBillingCurrency_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
        CleanPopUp()

        Using Model As New MSettingBilling("")
            Dim result = Await Model.GetCustormTRM(Indigo.IndigoOperatingUnitId)
            If result Is Nothing OrElse Not result?.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = "No se encontraron registros de TRM."
                Exit Sub
            End If
            Me.ListTRM = result.ObjectEmbbeded
        End Using

        Me.AddActionsColumns()
    End Sub

#End Region
#Region "Click"
    ''' <summary>
    ''' evento que se dispara cuando se da click en el menu de acciones de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction, IndigoGridView2.ContexMenuActions
        Dim BasicBillingGifts = viewCurrencyGrid.GetFocusedRow()
        Select Case (sender.Tag)
            Case "Edit"
                editTrm()
            Case "Remove"
                deleteTrm()
        End Select
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando se da click en agregar trm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAddGrid_Click(sender As Object, e As EventArgs) Handles INDSbAddGridTRM.Click
        addNewTRM()
    End Sub

    ''' <summary>
    ''' evento que se dispara cuando se da click en +
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceAddValueCurrency_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPceAddValueCurrency.ButtonClick
        CleanPopUp()
    End Sub

#End Region
#Region "Guardar"
    ''' <summary>
    ''' Guarda el trm especifico
    ''' </summary>
    Public Async Sub Guardar() Handles BarraBotones.ClickGuardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        Try
            Using model As New MSettingBilling("")
                AsyncLoader(True)
                _listAllTRM = ListTRM
                ''se agregan los eliminados
                If _listTRMDelete?.Any() Then
                    For Each item In _listTRMDelete
                        _listAllTRM.Add(item)
                    Next
                End If
                Dim Result = Await model.SaveCustomTRM(_listAllTRM.ToList())
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = "Se ha guardado de manera correcta el TRM especifico."
                    AsyncLoader(False)

                    DeleteBlockedRecord()
                    CleanPopUp()
                    Me.Close()
                Else
                    AsyncLoader(False)
                    If Result.MessageResult Is Nothing Then
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub
#End Region
#Region "QueryPopup"
    ''' <summary>
    ''' Metodo para mostrar los datos de la  moneda
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrencyToConvert_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCurrencyToConvert.QueryPopUp
        If INDsleCurrencyToConvert.Properties.DataSource Is Nothing Then
            Using Model As New MCurrency("")
                INDsleCurrencyToConvert.Properties.DataSource = Model.GetISOCurrencyWithoutOfficial(Indigo.OfficialCurrencyId)
            End Using
        End If
    End Sub
#End Region
#Region "EditValueChanged"
    ''' <summary>
    ''' Evento que se dispara al editar la fecha inicial 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdteInitialMeasurementDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteInitialMeasurementDate.EditValueChanged
        If flag Then
            Exit Sub
        End If
        If InitialMeasurementDate IsNot Nothing AndAlso FinalMeasurementDate IsNot Nothing Then
            If InitialMeasurementDate > FinalMeasurementDate Then
                Mensaje(EeventViewerImages.MensajeError) = "La fecha menor no puede ser mayor a la Final"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al editar la fecha final 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDdteFinalMeasurementDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDdteFinalMeasurementDate.EditValueChanged
        If flag Then
            Exit Sub
        End If
        If InitialMeasurementDate IsNot Nothing AndAlso FinalMeasurementDate IsNot Nothing Then
            If FinalMeasurementDate < InitialMeasurementDate Then
                Mensaje(EeventViewerImages.MensajeError) = "La fecha mayor no puede ser menor a la inicial"
            End If
        End If
    End Sub
#End Region
#Region "Popup"
    Private Sub INDPceAddValueCurrency_Popup(sender As Object, e As EventArgs) Handles INDPceAddValueCurrency.Popup
        If EditMode Then
            LoadControls()
        End If
    End Sub
#End Region
#Region "Methods"
    ''' <summary>
    ''' Metodo que valida y  agrega el trm a la rejilla
    ''' </summary>
    Sub addNewTRM()
        If InitialMeasurementDate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debes especificar la fecha inicial"
            Exit Sub
        End If
        If FinalMeasurementDate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debes especificar la fecha final"
            Exit Sub
        End If
        If INDsleCurrencyToConvert.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debes especificar la moneda"
            Exit Sub
        End If
        If String.IsNullOrEmpty(INDtxtValue.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debes indicar un valor"
            INDtxtValue.Focus()
            Exit Sub
        End If

        If Me.ListTRM Is Nothing Then
            Me.ListTRM = New List(Of CustomTRM)
        End If
        If ListTRM?.Any() Then
            Dim cont As Integer
            Dim row As CustomTRM
            If EditMode Then
                row = DirectCast(viewCurrencyGrid.GetFocusedRow(), CustomTRM)
            End If
            ''se valida quitando de la lista el item que se esta editando
            If ListTRM.Contains(row) Then
                Dim compareList = New List(Of CustomTRM)
                compareList.Add(row)
                Dim newList As IEnumerable(Of CustomTRM) = ListTRM.Except(compareList).ToList()
                cont = ValidateList(newList)
            Else
                cont = ValidateList(ListTRM.ToList())
            End If
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El trm  ya existe en la lista con las fechas " + InitialMeasurementDate.ToString + " - " + FinalMeasurementDate.ToString
                Exit Sub
            End If
        End If

        If EditMode Then
            Dim row = DirectCast(viewCurrencyGrid.GetFocusedRow(), CustomTRM)
            Dim _indexEditRecord = Me.ListTRM.IndexOf(row)
            ListTRM.Item(_indexEditRecord).InitialMeasurementDate = INDdteInitialMeasurementDate.DateTime
            ListTRM.Item(_indexEditRecord).FinalMeasurementDate = INDdteFinalMeasurementDate.DateTime
            ListTRM.Item(_indexEditRecord).OperatingUnitId = Indigo.IndigoOperatingUnitId
            ListTRM.Item(_indexEditRecord).CurrencyId = CurrencyToConvertId
            ListTRM.Item(_indexEditRecord).Value = Math.Round(1 / Value, 5)
            ListTRM.Item(_indexEditRecord).OfficialCurrencyId = Indigo.OfficialCurrencyId
            ListTRM.Item(_indexEditRecord).ValueOfficialToCurrency = Value
            ListTRM.Item(_indexEditRecord).CurrencyName = INDsleCurrencyToConvert.Text
            INDgcCurrency.RefreshDataSource()
            CleanPopUp()
            Exit Sub
        End If

        Dim CustomTrm = New Domain.Entities.CustomTRM
        CustomTrm.InitialMeasurementDate = INDdteInitialMeasurementDate.DateTime
        CustomTrm.FinalMeasurementDate = INDdteFinalMeasurementDate.DateTime
        CustomTrm.OperatingUnitId = Indigo.IndigoOperatingUnitId
        CustomTrm.CurrencyId = CurrencyToConvertId
        CustomTrm.Value = Math.Round(1 / Value, 5)
        CustomTrm.OfficialCurrencyId = Indigo.OfficialCurrencyId
        CustomTrm.ValueOfficialToCurrency = Value
        CustomTrm.CurrencyName = INDsleCurrencyToConvert.Text
        ListTRM.Add(CustomTrm)
        INDgcCurrency.RefreshDataSource()
        CleanPopUp()
    End Sub

    ''' <summary>
    ''' Metodo para limpiar los campos del popup container de trm
    ''' </summary>
    Sub CleanPopUp()
        flag = True
        CurrencyToConvertId = Nothing
        Value = 0
        InitialMeasurementDate = Date.Now()
        FinalMeasurementDate = DateAdd(DateInterval.Day, 1, Date.Now)
        INDsleCurrency.Text = Indigo.CurrencyISO4217
        INDsleCurrency.Properties.NullText = Indigo.CurrencyISO4217
        INDgcCurrency.RefreshDataSource()
        flag = False
        EditMode = False
    End Sub

    ''' <summary>
    ''' Deletes the blocked record.
    ''' </summary>
    Public Async Sub DeleteBlockedRecord()
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.Indigo.UserIndigo) Then
            Using Model As New MVP.MBlockRecordAndSequense(CStr(Me.Tag))
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Valida que en el listado no exista la misma definicion de tarifa y fechas
    ''' </summary>
    ''' <param name="listValidate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateList(listValidate As List(Of CustomTRM)) As Integer
        Dim list As List(Of CustomTRM) = listValidate.FindAll(Function(item) ((InitialMeasurementDate >= item.InitialMeasurementDate AndAlso InitialMeasurementDate <= item.FinalMeasurementDate) OrElse (FinalMeasurementDate >= item.InitialMeasurementDate AndAlso FinalMeasurementDate <= item.FinalMeasurementDate) OrElse ((InitialMeasurementDate < item.InitialMeasurementDate) AndAlso (FinalMeasurementDate > item.FinalMeasurementDate))))
        Return list.Count
    End Function

    ''' <summary>
    ''' Se agregan las opciones de menu para la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView2.SetListAcction(viewCurrencyGrid, ListActions)

        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewCurrencyGrid.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next

    End Sub

    ''' <summary>
    ''' Se cargan los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadControls()
        Dim row = DirectCast(viewCurrencyGrid.GetFocusedRow(), CustomTRM)
        CurrencyToConvertId = row.CurrencyId
        Value = row.ValueOfficialToCurrency
        InitialMeasurementDate = row.InitialMeasurementDate
        FinalMeasurementDate = row.FinalMeasurementDate
        INDsleCurrency.Properties.NullText = Indigo.CurrencyISO4217
        CurrencyToConvertId = row.CurrencyId
        INDsleCurrencyToConvert.Properties.NullText = row.CurrencyName
        INDgcCurrency.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Metodo cuando se esta editando un registro de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub editTrm()
        EditMode = True
        INDPceAddValueCurrency.ShowPopup()
    End Sub

    ''' <summary>
    ''' Metodo cuando se esta eliminando un registro de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub deleteTrm()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _listTRMDelete Is Nothing Then
                _listTRMDelete = New List(Of CustomTRM)
            End If
            Dim row = DirectCast(viewCurrencyGrid.GetFocusedRow(), CustomTRM)
            Dim _indexEditRecord = Me.ListTRM.IndexOf(row)
            ListTRM.Item(_indexEditRecord).ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
            _listTRMDelete.Add(ListTRM.Item(_indexEditRecord))
            ListTRM.Remove(row)
            INDgcCurrency.DataSource = ListTRM.Where(Function(x) x.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Deleted).ToList()
            INDgcCurrency.RefreshDataSource()
            CleanPopUp()
        End If

    End Sub
#End Region
#Region "CRUDBASE"
    ''' <summary>
    ''' Evento que se dispara al dar click en Deshacer de la barra de botones
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanPopUp()
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar
        Throw New NotImplementedException()
    End Sub

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Throw New NotImplementedException()
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Throw New NotImplementedException()
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
        Throw New NotImplementedException()
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        Throw New NotImplementedException()
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Throw New NotImplementedException()
    End Sub

    Private Sub ICrudBase_Guardar() Implements ICrudBase.Guardar
        Throw New NotImplementedException()
    End Sub


#End Region

End Class