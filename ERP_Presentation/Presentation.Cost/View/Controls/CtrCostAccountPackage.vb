Imports System.Text
Imports DevExpress.XtraEditors
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

Public Class CtrCostAccountPackage

#Region "Properties and Variables"
    Private _productionCenterHomologation As CostProductionCenterHomologation
    Public Property LegalBookId As Integer

    ''' <summary>
    ''' Ocurre cuando se da click en el boton agregar
    ''' </summary>
    Public Event AddHomologation(ByVal productHomologation As CostProductionCenterHomologation)

    Public Property AllowSecondaryDistribution As Boolean
        Get
            Return INDRgAllowSecondaryDistribution.EditValue
        End Get
        Set(value As Boolean)
            INDRgAllowSecondaryDistribution.EditValue = value
        End Set
    End Property

    Public ReadOnly Property MainAccountOrigin As SearchLookUpEdit
        Get
            Return INDsleMainAccountOrigin
        End Get
    End Property

    Public ReadOnly Property MainAccountDestination As SearchLookUpEdit
        Get
            Return INDsleMainAccountDestination
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    Public Property Description As String
        Get
            Return INDmeDescription.Text
        End Get
        Set(value As String)
            INDmeDescription.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de origen
    ''' </summary>
    Public Property AccountOriginId As Integer
        Get
            Return INDsleMainAccountOrigin.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccountOrigin.EditValue = value
        End Set
    End Property

    Private _showSecondaryDistribution As Boolean = False
    Public WriteOnly Property ShowSecondaryDistribution As Boolean
        Set(value As Boolean)
            _showSecondaryDistribution = value
            If value Then
                INDLciAllowSecondaryDistribution.ShowLayout()
            Else
                INDLciAllowSecondaryDistribution.HideLayout()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de destino
    ''' </summary>
    Private Property AccountTargetId As Integer?
        Get
            If INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                Return INDsleMainAccountDestination.EditValue
            Else
                Return Nothing
            End If
        End Get
        Set(value As Integer?)
            INDsleMainAccountDestination.EditValue = value
        End Set
    End Property

#End Region

#Region "Events"
    ''' <summary>
    ''' Handles the Load event of the CtrAccountPackage control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrAccountPackage_Load(sender As Object, e As EventArgs) Handles Me.Load
        CleanControls()
    End Sub

    Public Sub LoadHomologation(productionCenterHomolo As CostProductionCenterHomologation)
        _productionCenterHomologation = productionCenterHomolo
        AccountOriginId = productionCenterHomolo.AccountOriginId
        MainAccountOrigin.Properties.NullText = productionCenterHomolo.FullNameAccountOrigin
        AllowSecondaryDistribution = productionCenterHomolo.AllowSecondaryDistribution
        Description = productionCenterHomolo.Description

        INDsbAdd.Text = "Editar"
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then
            If _productionCenterHomologation Is Nothing Then
                _productionCenterHomologation = New CostProductionCenterHomologation()
            End If

            With _productionCenterHomologation
                .Description = Description
                .AccountOriginId = AccountOriginId
                .AccountTargetId = AccountTargetId
                .FullNameAccountOrigin = INDsleMainAccountOrigin.Text
                .FullNameAccountDestination = INDsleMainAccountDestination.Text
                .AllowSecondaryDistribution = INDRgAllowSecondaryDistribution.EditValue
            End With

            RaiseEvent AddHomologation(_productionCenterHomologation)
            CleanControls()
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        INDsleMainAccountOrigin.EditValue = Nothing
        INDsleMainAccountDestination.EditValue = Nothing
        INDmeDescription.Text = String.Empty
        INDsleMainAccountOrigin.Properties.NullText = String.Empty
        _productionCenterHomologation = Nothing
        INDsbAdd.Text = "Agregar"
        INDsleMainAccountOrigin.Focus()
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function validateControls() As Boolean
        Dim errorList As New StringBuilder()
        If INDsleMainAccountOrigin.EditValue Is Nothing Then
            errorList.AppendLine("Cuenta Contable Origen")
        End If
        If INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always And INDsleMainAccountDestination.EditValue Is Nothing Then
            errorList.AppendLine("Cuenta Contable Destino")
        End If
        If INDmeDescription.Text = String.Empty Then
            errorList.AppendLine("Detalle")
        End If
        If errorList.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format("Hay Campos sin diligenciar {0}", errorList.ToString())
            Return False
        Else
            Return True
        End If
    End Function
#End Region

End Class
