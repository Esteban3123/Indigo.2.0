'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Henry Alejandro Vargas Polania
' Created          : 31-12-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
#End Region

Public Class PopupPhysicalInventory

    Private _warehouseId As Integer
    'Private _revenueControlId As Integer?
    Private _admissionNumber As String

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(WarehouseId As Integer)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _warehouseId = WarehouseId
    End Sub

    'Sub New(RevenueControlId As Integer?, WarehouseId As Integer)
    Sub New(AdmissionNumber As string, WarehouseId As Integer)
        ' TODO: Complete member initialization 
        InitializeComponent()
        _admissionNumber = AdmissionNumber
        _warehouseId = WarehouseId
    End Sub

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupPhysicalInventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using Model As New MInventoryControl("")
            If _admissionNumber Is Nothing Then
                INDGcPhysicalInventory.DataSource = Model.ListPhysicalInventoryByWarehouseId(_warehouseId)
            Else
                INDGcPhysicalInventory.DataSource = Model.ListPhysicalInventoryCustodyByWarehouseId(_admissionNumber, _warehouseId)
            End If
            
        End Using
    End Sub

    ''' <summary>
    ''' cierra el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupPhysicalInventory_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

End Class