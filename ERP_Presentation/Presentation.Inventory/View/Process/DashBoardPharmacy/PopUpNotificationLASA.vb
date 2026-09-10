Imports DevExpress.Xpo
Imports Domain.Crystal.Entities
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Inventory.MVP

Public Class PopUpNotificationLASA

#Region "Propeties"

    ''' <summary>
    ''' Tipo de documento origen
    ''' </summary>
    Public Source As DashboardPharmacySource

    ''' <summary>
    ''' Listado de detalles a validar
    ''' </summary>
    Public ListDashboardPharmacyDetail As List(Of ViewDashboardPharmacyDetail)

    ''' <summary>
    ''' Evento que se llama al dar click en el boton aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AcceptMessage(sender As Object, e As ResponseEventArgs)

#End Region

#Region "Methods"

    Public Function LoadLASAMedications() As Boolean
        Dim listLASAMedications As XPCollection(Of ViewLASAMedication) = Nothing

        If Me.ListDashboardPharmacyDetail IsNot Nothing AndAlso Me.ListDashboardPharmacyDetail.Any(Function(d) d.Tipo = 1) Then
            Using model As New MDashBoardPharmacy(Me.Tag)
                Dim listATCs = ListDashboardPharmacyDetail.Where(Function(d) d.Tipo = 1).Select(Function(d) String.Concat("'", d.Producto.Split(" - ").ElementAt(0).Trim(), "'")).Distinct().ToList()
                listLASAMedications = model.ListViewLASAMedication(listATCs)
            End Using
        End If

        If listLASAMedications IsNot Nothing AndAlso listLASAMedications.Any() Then
            INDgcLASAMedication.DataSource = listLASAMedications
        Else
            RaiseEvent AcceptMessage(Nothing, New ResponseEventArgs With {.Source = Me.Source, .ListDashboardPharmacyDetail = Me.ListDashboardPharmacyDetail, .AcceptResponse = True})
            Return False
        End If

        Return True
    End Function

    Private Sub INDbtnYes_Click(sender As Object, e As EventArgs) Handles INDbtnYes.Click
        RaiseEvent AcceptMessage(Nothing, New ResponseEventArgs With {.Source = Me.Source, .ListDashboardPharmacyDetail = Me.ListDashboardPharmacyDetail, .AcceptResponse = True})
        Me.Close()
    End Sub

#End Region

#Region "HANDLES"

#Region "Load"

    ''' <summary>
    ''' load del del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopUpNotificationLASA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadLASAMedications()
    End Sub

#End Region

#End Region

End Class

Public Enum DashboardPharmacySource As Byte
    ManualDelivery = 1
    CUM = 2
End Enum

Public Class ResponseEventArgs
    Inherits EventArgs

    ''' <summary>
    ''' Permite saber cual fue el documento fuente
    ''' </summary>
    Public Source As DashboardPharmacySource

    ''' <summary>
    ''' Listado de detalles a validar
    ''' </summary>
    Public ListDashboardPharmacyDetail As List(Of ViewDashboardPharmacyDetail)

    ''' <summary>
    ''' Permite saber si fue cerrado con aceptar o con cancelar
    ''' </summary>
    Public AcceptResponse As Boolean

End Class