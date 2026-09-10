'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 26-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Inventory.MVP
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls

#End Region
Public Class CtrBatchSerial

#Region "EVENT"
    ''' <summary>
    ''' evnto que se dispara cuando la cantidad del lote cambie
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event ChangeQuantity(sender As Object, e As ChangeQuantityEventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' representa la entidad de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private _product As InventoryProduct
    ''' <summary>
    ''' formulario dond esta el control
    ''' </summary>
    ''' <remarks></remarks>
    Private _formOwner As FormBase
    ''' <summary>
    ''' listado de los seriales por el id del producto
    ''' </summary>
    ''' <remarks></remarks>
    Private listBatchSerial As List(Of BatchSerial)
    ''' <summary>
    ''' entidad de lotes
    ''' </summary>
    ''' <remarks></remarks>
    Private BatchSerial As BatchSerial
    ''' <summary>
    ''' bandera para saber si se esta agregando un lote
    ''' </summary>
    ''' <remarks></remarks>
    Private flagAddBatchSerial As Boolean
    ''' <summary>
    ''' bandera para saber si se obtiene todo los lostes o solo los que no esten vencidos
    ''' </summary>
    ''' <remarks></remarks>
    Private getAllBatchSerial As Boolean
    ''' <summary>
    ''' id del actual almacen del cual se esta seleccionando el lote
    ''' </summary>
    ''' <remarks></remarks>
    Private _warehouseId As Integer
    ''' <summary>
    ''' identifica si el movimiento que se esta haciendo es de entrada y salida
    ''' </summary>
    ''' <remarks></remarks>
    Private _remissionType As Integer
    ''' <summary>
    ''' 
    ''' </summary>
    Private _Custodty As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    Private _AdmissionNumber As String
    ''' <summary>
    ''' variable para identificar si se está abriendo el Ctr desde el control de inventario
    ''' </summary>
    Private _IsInventoryControl As Boolean = False
#End Region

#Region "PROPERTIES"

    Public WriteOnly Property RemissionType As ERemissionType
        Set(value As ERemissionType)
            If value = ERemissionType.Input Then
                _remissionType = 1
                INDLciCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciExperidDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLciAddBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                _remissionType = 2
                INDLciCode.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciExperidDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLciAddBatchSerial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Set
    End Property
    
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, _formOwner)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, _formOwner)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property
    ''' <summary>
    ''' formulario donde esta el control para poder lanzar los mensajes al usuario
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property FormOwner As FormBase
        Set(value As FormBase)
            _formOwner = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para asignar la entidad de producto
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Product As InventoryProduct
        Set(value As InventoryProduct)
            _product = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para asignar el id del actual almacen del cual se esta seleccionando el lote
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property WarehouseId As Integer
        Set(value As Integer)
            _warehouseId = value
        End Set
    End Property
    ''' <summary>
    ''' 
    ''' </summary>
    Public WriteOnly Property AdmissionNumber As String
        Set(value As String)
            _AdmissionNumber = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Public WriteOnly Property Custody As Boolean
        Set(value As Boolean)
            _Custodty = value
        End Set
    End Property

    ''' <summary>
    ''' variable para identificar si se está abriendo el Ctr desde el control de inventario
    ''' </summary>
    Public WriteOnly Property IsInventoryControl As Boolean
        Set(value As Boolean)
            _IsInventoryControl = value
        End Set
    End Property

#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para establecer el foco en la rejilla del control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetFocusGrid()
        INDGcBatchSerial.Focus()
    End Sub
    ''' <summary>
    ''' iguala las cantidades de los items del listado de lotes
    ''' </summary>
    ''' <param name="_listSetQuantityBatchSerial"></param>
    ''' <remarks></remarks>
    Public Sub SetQuantityBatchSerial(_listSetQuantityBatchSerial As IEnumerable(Of Object))
        For Each item In _listSetQuantityBatchSerial
            Dim batch = listBatchSerial.Find(Function(x) x.Id = item.BatchSerialId)
            If batch IsNot Nothing Then
                batch.Quantity = item.Quantity
            End If
        Next
        INDGcBatchSerial.DataSource = Nothing
        INDGcBatchSerial.DataSource = listBatchSerial
    End Sub
    ''' <summary>
    ''' metodo que retorna los lotes que tienen cantidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListBatchSerial() As List(Of BatchSerial)
        Dim listBatchSerialReturn As New List(Of BatchSerial)
        If listBatchSerial IsNot Nothing AndAlso listBatchSerial.Count > 0 Then
            listBatchSerialReturn.AddRange(listBatchSerial.FindAll(Function(x) x.Quantity > 0))
        End If
        Return listBatchSerialReturn
    End Function
    ''' <summary>
    ''' metodo para establecer el datasource de lotes a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetListBatchSerial(Optional allBatchSerial As Boolean = True)
        If DesignMode = False Then
            getAllBatchSerial = allBatchSerial
            Using model As New MCtrBatchSerial(Me.Tag)
                If listBatchSerial Is Nothing Then
                    If _Custodty Then
                        If getAllBatchSerial = False Then
                            listBatchSerial = model.ListBatchSerialCustodyByProductId(_AdmissionNumber, _product.Id, GetServerDate, _warehouseId, _remissionType)
                        Else
                            listBatchSerial = model.ListBatchSerialCustodyByProductId(_AdmissionNumber, _product.Id, Nothing, _warehouseId, _remissionType)
                        End If
                    Else
                        If getAllBatchSerial = False Then
                            listBatchSerial = model.ListBatchSerialByProductId(_product.Id, GetServerDate, _warehouseId, _remissionType)
                        Else
                            listBatchSerial = model.ListBatchSerialByProductId(_product.Id, Nothing, _warehouseId, _remissionType)
                        End If
                    End If
                    
                End If
                INDGcBatchSerial.DataSource = Nothing
                INDGcBatchSerial.DataSource = listBatchSerial
            End Using
        End If
    End Sub

    ''' <summary>
    ''' limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDTxtCode.EditValue = Nothing
        INDDteExpiredDate.EditValue = GetServerDate().AddDays(1)
        INDDteExpiredDate.Properties.MinValue = INDDteExpiredDate.EditValue
        INDGcBatchSerial.DataSource = Nothing
        BatchSerial = Nothing
        If flagAddBatchSerial = False Then
            listBatchSerial = Nothing
        End If
    End Sub
    ''' <summary>
    ''' retorna la fecha del servidor
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetServerDate() As DateTime
        If DesignMode = False Then
            Using model As New MformBase
                Return model.GetDateServer()
            End Using
        End If
    End Function
#End Region

#Region "HANDLES"
    Private Async Sub INDBtnAddBatch_Click(sender As Object, e As EventArgs) Handles INDBtnAddBatch.Click
        If INDTxtCode.EditValue Is Nothing OrElse String.IsNullOrWhiteSpace(CStr(INDTxtCode.EditValue)) Then
            Mensaje(EeventViewerImages.Advertencia) = INDLciCode.Text + ResourceManager.GetString("Empty")
            Exit Sub
        End If

        Dim batchCode As String = CStr(INDTxtCode.EditValue).Trim()

        If INDLciExperidDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDDteExpiredDate.EditValue Is Nothing OrElse Not IsDate(INDDteExpiredDate.EditValue) Then
                Mensaje(EeventViewerImages.Advertencia) = INDLciExperidDate.Text + ResourceManager.GetString("Empty")
                Exit Sub
            End If
            'Se valida que no se pueda agregar un lote con el mismo código y fecha vencimiento
            If CType(INDGcBatchSerial.DataSource, List(Of BatchSerial)) IsNot Nothing AndAlso CType(INDGcBatchSerial.DataSource, List(Of BatchSerial)).Count > 0 Then
                If (From x In CType(INDGcBatchSerial.DataSource, List(Of BatchSerial)) Where x.BatchCode = batchCode AndAlso CDate(x.ExpirationDate).Date = CDate(INDDteExpiredDate.EditValue).Date Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya existe un lote con código " + batchCode + " y fecha de vencimiento " + INDDteExpiredDate.EditValue.ToString + " en la lista"
                    Exit Sub
                End If
            End If
            'Se valida que no se pueda agregar un lote con el mismo código
            If CType(INDGcBatchSerial.DataSource, List(Of BatchSerial)) IsNot Nothing AndAlso CType(INDGcBatchSerial.DataSource, List(Of BatchSerial)).Count > 0 Then
                If (From x In CType(INDGcBatchSerial.DataSource, List(Of BatchSerial)) Where x.BatchCode = batchCode Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "Ya existe un lote con código " + batchCode + " en la lista"
                    Exit Sub
                End If
            End If
        End If

        Try
            INDBtnAddBatch.Enabled = False
            BatchSerial = New BatchSerial
            With BatchSerial
                .ProductId = _product.Id
                .Type = 1
                .BatchCode = batchCode
                .ExpirationDate = INDDteExpiredDate.EditValue
            End With
            Using model As New MCtrBatchSerial(Me.Tag)
                Dim result = Await model.SaveBatchSerial(BatchSerial)
                INDBtnAddBatch.Enabled = True
                If result.StateResult = True Then
                    Dim listBatchSerialTmp As New List(Of BatchSerial)
                    If listBatchSerial IsNot Nothing Then
                        For Each BatchSerial In listBatchSerial.Where(Function(bs) bs.Quantity > 0)
                            listBatchSerialTmp.Add(BatchSerial)
                        Next
                    End If

                    listBatchSerial = Nothing
                    SetListBatchSerial(getAllBatchSerial)

                    Dim args As New ChangeQuantityEventArgs With {.Quantity = 0}
                    If listBatchSerial IsNot Nothing Then
                        For Each BatchSerial In listBatchSerialTmp
                            Dim bs = listBatchSerial.Where(Function(d) d.Id = BatchSerial.Id).FirstOrDefault()
                            If bs IsNot Nothing Then
                                bs.Quantity = BatchSerial.Quantity
                            End If
                        Next

                        INDGcBatchSerial.DataSource = Nothing
                        INDGcBatchSerial.DataSource = listBatchSerial
                        args.Quantity = listBatchSerial.Sum(Function(x) x.Quantity)
                    End If
                    RaiseEvent ChangeQuantity(Nothing, args)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            INDBtnAddBatch.Enabled = True
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub

    Private Sub INDRptSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim batch = DirectCast(INDGvBatchSerial.GetFocusedRow(), BatchSerial)
        If getAllBatchSerial = False Then
            If batch.ExpirationDate < GetServerDate() Then
                Mensaje(EeventViewerImages.Advertencia) = "La fecha de vencimiento del lote es menor a la fecha del sistema"
                e.Cancel = True
                Exit Sub
            End If
        End If
        If Not Me._remissionType = 1 AndAlso e.NewValue > batch.OutstandingQuantity AndAlso Not _IsInventoryControl Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad ingresada supera la cantidad disponible del lote (" & batch.OutstandingQuantity & ")"
            e.Cancel = True
            Exit Sub
        End If

        batch.Quantity = e.NewValue
        Dim args As New ChangeQuantityEventArgs
        args.Quantity = listBatchSerial.Sum(Function(x) x.Quantity)
        RaiseEvent ChangeQuantity(Nothing, args)
    End Sub
#End Region

End Class


Public Enum ERemissionType As Integer
    Input = 1
    Output = 2
End Enum