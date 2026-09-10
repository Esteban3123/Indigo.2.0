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
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP

#End Region

Public Class CtrPhysicalInventory

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

    Private _movementType As InventoryStaticServices.MovementType

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
    ''' Cantidad maxima permitida para un producto importado
    ''' </summary>
    ''' <remarks></remarks>
    Private _MaxQuantity As Integer

    ''' <summary>
    ''' id del almacen
    ''' </summary>
    ''' <remarks></remarks>
    Private _wareHouseId As Integer

    ''' <summary>
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Public listPhysicalInventory As List(Of PhysicalInventory)

    ''' <summary>
    ''' listado del inventario fisico
    ''' </summary>
    ''' <remarks></remarks>
    Private listPhysicalInventoryCustody As List(Of PhysicalInventoryCustody)

    ''' <summary>
    ''' 
    ''' </summary>
    Private _custody As Boolean
    ''' <summary>
    ''' 
    ''' </summary>
    Private _patientCode As String
    ''' <summary>
    ''' 
    ''' </summary>
    Private _admissionNumber As String
#End Region

#Region "PROPERTIES"
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
    ''' Cantidad maxima permitida para el producto que viene de una importación
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MaxQuantity As Integer
        Set(value As Integer)
            _MaxQuantity = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad para asignar la entidad de producto
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property MovementType As InventoryStaticServices.MovementType
        Set(value As InventoryStaticServices.MovementType)
            _movementType = value
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
            If _product IsNot Nothing Then
                INDTxtProduct.Text = _product.Code + " - " + _product.Name
                INDTxtManufacturer.Text = _product.ManufacturerDescription
            End If
        End Set
    End Property
    ''' <summary>
    ''' propiedad para establecer el id del almacen
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property WareHouseId As Integer
        Set(value As Integer)
            _wareHouseId = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Public WriteOnly Property Custody As Boolean
        Set(value As Boolean)
            _custody = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Public WriteOnly Property PatientCode As String
        Set(value As String)
            _patientCode = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    Public WriteOnly Property AdmissionNumber As String
        Set(value As String)
            _admissionNumber = value
        End Set
    End Property


#End Region

#Region "METHODS"
    ''' <summary>
    ''' metodo para establecer el foco en la rejilla del control
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetFocusGrid()
        INDGcPhysicalInventory.Focus()
    End Sub
    Public Sub CleanControls()
        INDTxtProduct.Text = String.Empty
        INDTxtManufacturer.Text = String.Empty
        INDGcPhysicalInventory.DataSource = Nothing
        listPhysicalInventory = Nothing
        listPhysicalInventoryCustody = Nothing
    End Sub
    ''' <summary>
    ''' metodo que retorna los inventarios fisicos con cantidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListPhysicalInventory() As List(Of PhysicalInventory)
        Dim listPhysicalInventoryReturn As New List(Of PhysicalInventory)
        If listPhysicalInventory IsNot Nothing AndAlso listPhysicalInventory.Count > 0 Then
            listPhysicalInventoryReturn.AddRange(listPhysicalInventory.FindAll(Function(x) x.QuantityDeliver > 0))
        End If
        Return listPhysicalInventoryReturn
    End Function

    ''' <summary>
    ''' metodo que retorna los inventarios fisicos con cantidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListPhysicalInventoryCustody() As List(Of PhysicalInventoryCustody)
        Dim listPhysicalInventoryReturn As New List(Of PhysicalInventoryCustody)
        listPhysicalInventoryReturn.AddRange(listPhysicalInventoryCustody.FindAll(Function(x) x.QuantityDeliver > 0))
        Return listPhysicalInventoryReturn
    End Function

    Public Sub SetListPhysicalInventory()
        If Not DesignMode Then
            Using model As New MCtrPhysicalInventory(Me.Tag)
                If _custody Then
                    If listPhysicalInventoryCustody Is Nothing Then
                        listPhysicalInventoryCustody = model.GetListPhysicalInventoryCustody(_patientCode, _admissionNumber, _product.Id, _wareHouseId)
                    End If
                    INDGcPhysicalInventory.DataSource = Nothing
                    INDGcPhysicalInventory.DataSource = listPhysicalInventoryCustody
                Else
                    If listPhysicalInventory Is Nothing Then
                        listPhysicalInventory = model.GetListPhysicalInventory(_product.Id, _wareHouseId, If(_movementType = InventoryStaticServices.MovementType.Input, True, False))
                    End If
                    If listPhysicalInventory?.Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = $"El producto {_product.Code} - {_product.Name} no tiene unidades disponibles"
                        Exit Sub
                    Else
                        INDGcPhysicalInventory.DataSource = Nothing
                        INDGcPhysicalInventory.DataSource = listPhysicalInventory
                    End If
                End If
            End Using
        End If
    End Sub

    Public Sub SetQuantityPhysicalInventory(_listSetQuantityPhysicalInventory As IEnumerable(Of Object))
        If _custody Then
            Dim physicalInventory = Nothing
            For Each item In _listSetQuantityPhysicalInventory
                physicalInventory = listPhysicalInventoryCustody.Find(Function(x) x.Id = item.PhysicalInventoryCustodyId)
                If physicalInventory IsNot Nothing Then
                    physicalInventory.QuantityDeliver = item.Quantity
                End If
            Next
            INDGcPhysicalInventory.DataSource = Nothing
            INDGcPhysicalInventory.DataSource = listPhysicalInventoryCustody
        Else
            Dim physicalInventory = Nothing
            For Each item In _listSetQuantityPhysicalInventory
                physicalInventory = listPhysicalInventory.Find(Function(x) x.Id = item.PhysicalInventoryId)
                If physicalInventory IsNot Nothing Then
                    physicalInventory.QuantityDeliver = item.Quantity
                End If
            Next
            INDGcPhysicalInventory.DataSource = Nothing
            INDGcPhysicalInventory.DataSource = listPhysicalInventory
        End If

    End Sub

    Public Sub SetQuantityPhysicalInventoryByManualDispensing(_listTemp As List(Of PhysicalInventory))
        Dim physicalInventory = Nothing
        For Each item In _listTemp
            physicalInventory = listPhysicalInventory.Find(Function(x) x.Id = item.Id)
            If physicalInventory IsNot Nothing Then
                physicalInventory.QuantityDeliver = item.QuantityDeliver
            End If
        Next
        INDGcPhysicalInventory.DataSource = Nothing
        INDGcPhysicalInventory.DataSource = listPhysicalInventory
    End Sub
#End Region

#Region "HANDLES"
    Private Sub INDRptSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If

        Dim physicalInventoryTmp = Nothing
        If _custody Then
            physicalInventoryTmp = DirectCast(INDGvPhysicalInventory.GetFocusedRow, PhysicalInventoryCustody)
            If physicalInventoryTmp.Quantity < Convert.ToInt32(e.NewValue) Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad maxima que se puede seleccionar es " + physicalInventoryTmp.Quantity.ToString()
                Exit Sub
            End If
        Else
            physicalInventoryTmp = DirectCast(INDGvPhysicalInventory.GetFocusedRow, PhysicalInventory)
            If _movementType = InventoryStaticServices.MovementType.OutPut Then
                If physicalInventoryTmp.Quantity < Convert.ToInt32(e.NewValue) Then
                    e.Cancel = True
                    Mensaje(EeventViewerImages.Advertencia) = "La cantidad maxima que se puede seleccionar es " + physicalInventoryTmp.Quantity.ToString()
                    Exit Sub
                End If

                If _MaxQuantity > 0 Then
                    If _MaxQuantity < Convert.ToInt32(e.NewValue) Then
                        e.Cancel = True
                        Mensaje(EeventViewerImages.Advertencia) = "La cantidad maxima que se puede seleccionar es " + _MaxQuantity.ToString()
                        Exit Sub
                    End If
                End If
            End If
        End If

        physicalInventoryTmp.QuantityDeliver = e.NewValue
        Dim args As New ChangeQuantityEventArgs
        args.Quantity = listPhysicalInventory.Sum(Function(x) x.QuantityDeliver)
        RaiseEvent ChangeQuantity(Nothing, args)
    End Sub
#End Region

End Class