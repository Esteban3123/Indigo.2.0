'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 25-03-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Inventory.MVP

#End Region

Public Class PopupCUMReturn

#Region "EVENTS"



    Public Event GetCUMReturn(senser As Object, e As GetCUMReturnEventArgs)
#End Region

#Region "GLOBALS"
    ''' <summary>
    ''' numero del ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Dim _admissionNumber As String
    ''' <summary>
    ''' codigo del producto de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _productCode As String
    ''' <summary>
    ''' cantidad que se va a entregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim _quantityDeliver As Integer
    ''' <summary>
    ''' listado del inventario fisico por el codigo 
    ''' </summary>
    ''' <remarks></remarks>
    Dim listPharmaceuticalDispensingDetailBatchSerialCrystalProduct As List(Of PharmaceuticalDispensingDetailBatchSerial)

#End Region

#Region "PROPERTIES"
    ''' <summary>
    ''' propiedad para pasar el numero del ingreso
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property AdmissionNumber As String
        Set(value As String)
            _admissionNumber = value
        End Set
    End Property

    Dim _productType As Integer
    Public WriteOnly Property ProductType As Integer
        Set(value As Integer)
            _productType = value
        End Set
    End Property

    Public WriteOnly Property Product As String
        Set(value As String)
            INDLcgMain.Text = value
        End Set
    End Property

    Public WriteOnly Property CodeProduct As String
        Set(value As String)
            _productCode = value
        End Set
    End Property

    Public WriteOnly Property QuantityDeliver As Integer
        Set(value As Integer)
            _quantityDeliver = value
            INDLblQuantity.Text = "Cantidad Solicitada: " + value.ToString()
        End Set
    End Property

    Dim _CantidadFisico As Integer
    Public WriteOnly Property CantidadFisico As Integer
        Set(value As Integer)
            _CantidadFisico = value
        End Set
    End Property

    ''' <summary>
    ''' listado de el inventario fisico que se pasa desde el formulario principal
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listPharmaceuticalDispensingDetailBatchSerial As List(Of PharmaceuticalDispensingDetailBatchSerial)
    Public WriteOnly Property ListPharmaceuticalDispensingDetailBatchSerial As List(Of PharmaceuticalDispensingDetailBatchSerial)
        Set(value As List(Of PharmaceuticalDispensingDetailBatchSerial))
            _listPharmaceuticalDispensingDetailBatchSerial = value
        End Set
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

    Dim _functionalUnitCode As String
    Public WriteOnly Property FunctionalUnitCode As String
        Set(value As String)
            _functionalUnitCode = value
        End Set
    End Property

    ''' <summary>
    ''' ORIDEVMED : 1 - Traslado Cama, 2 - egreso cama, 5 - Hoja d Gasto Qx, 4 - Enfermeria
    ''' </summary>
    Dim _ORIDEVMEDCUM As Integer
    Public WriteOnly Property ORIDEVMEDCUM As Integer
        Set(value As Integer)
            _ORIDEVMEDCUM = value
        End Set
    End Property

    Public Property BatchCode As String = ""
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub PopupCUM_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGvCUM.OptionsView.ShowAutoFilterRow = False
    End Sub
#End Region

#Region "Shown"
    Private Sub PopupCUM_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MDashBoardPharmacy(Me.Tag)
            listPharmaceuticalDispensingDetailBatchSerialCrystalProduct = model.ListPharmaceuticalDispensingDetailBatchSerial(_admissionNumber, _functionalUnitCode, _productCode, _productType, BatchCode)
            If listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.Count > 0 Then
                If _listPharmaceuticalDispensingDetailBatchSerial IsNot Nothing Then
                    For Each item In _listPharmaceuticalDispensingDetailBatchSerial
                        Dim pharmaceuticalDispensingDetailBatchSerialTmp = listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.Find(Function(x) x.Id = item.Id)
                        If pharmaceuticalDispensingDetailBatchSerialTmp IsNot Nothing Then
                            pharmaceuticalDispensingDetailBatchSerialTmp.DevolutionQuantity = item.DevolutionQuantity
                        End If
                    Next
                End If
                INDGcCUM.DataSource = Nothing
                INDGcCUM.DataSource = listPharmaceuticalDispensingDetailBatchSerialCrystalProduct
            Else
                IndigoGridControl1.RefreshGrid(INDGcCUM)
                INDLciAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Using
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        'If listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.FindAll(Function(x) x.DevolutionQuantity > 0).Count > 0 Then
        If Not {5, 6}.Contains(_ORIDEVMEDCUM) AndAlso _CantidadFisico < listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.Sum(Function(x) x.DevolutionQuantity) Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad del producto en fisico es menor a la cantidad a devolver "
            Exit Sub
        End If

        Dim args As New GetCUMReturnEventArgs
        args.ListPharmaceuticalDispensingDetailBatchSerial = listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.FindAll(Function(x) x.DevolutionQuantity > 0)


        RaiseEvent GetCUMReturn(Nothing, args)
        'End If
        Me.Close()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub PopupCUM_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            'If listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.Count > 0 Then
            '    e.SuppressKeyPress = True
            'Else
            Me.Close()
        End If
        'End If
    End Sub
#End Region

#Region "EditValueChanging"
    Private Sub INDRptSeQuantity_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDRptSeQuantity.EditValueChanging
        If e.NewValue Is String.Empty Then
            Exit Sub
        End If
        Dim listTmp = New List(Of PharmaceuticalDispensingDetailBatchSerial)(listPharmaceuticalDispensingDetailBatchSerialCrystalProduct.ToArray())
        Dim PharmaceuticalDispensingDetailBatchSerialTmp = DirectCast(INDGvCUM.GetFocusedRow, PharmaceuticalDispensingDetailBatchSerial)
        If e.NewValue > PharmaceuticalDispensingDetailBatchSerialTmp.OutstandingQuantity Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad del producto " + PharmaceuticalDispensingDetailBatchSerialTmp.CodeNameProduct + " es menor a la cantidad a devolver "
            e.Cancel = True
            Exit Sub
        End If
        listTmp.Remove(PharmaceuticalDispensingDetailBatchSerialTmp)
        Dim quantity = listTmp.Sum(Function(x) x.DevolutionQuantity)
        quantity += e.NewValue
        If quantity > _quantityDeliver Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad a devolver por todos los productos supera la cantidad solicitada"
            e.Cancel = True
            Exit Sub
        End If

        If Not {5, 6}.Contains(_ORIDEVMEDCUM) AndAlso e.NewValue > _CantidadFisico Then
            Mensaje(EeventViewerImages.Advertencia) = "La cantidad del producto en fisico es menor a la cantidad a devolver"
            e.Cancel = True
            Exit Sub
        End If

        PharmaceuticalDispensingDetailBatchSerialTmp.DevolutionQuantity = e.NewValue
    End Sub
#End Region
#End Region

End Class

''' <summary>
''' clase para retornar en el evento de seleccionar productos
''' </summary>
''' <remarks></remarks>
Public Class GetCUMReturnEventArgs
    Inherits EventArgs

    Property ListPharmaceuticalDispensingDetailBatchSerial As List(Of PharmaceuticalDispensingDetailBatchSerial)
End Class