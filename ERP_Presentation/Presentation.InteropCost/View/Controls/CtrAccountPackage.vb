Imports DevExpress.Xpo
Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports DevExpress.Data.PLinq
Imports DevExpress.XtraEditors
Imports Presentation.InteropCost.MVP
Imports DevExpress.XtraGrid.Views.Grid
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Infrastructure.Data.Xpo.InteropCostRepository
Imports Infrastructure.Data.Xpo

Public Class CtrAccountPackage

#Region "Properties and Variables"
    ''' <summary>
    ''' Ocurre cuando se da click en el boton agregar
    ''' </summary>
    Public Event AddHomologation(ByVal productHomologation As ProductionCenterHomologation)



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

    ' ''' <summary>
    ' ''' Obtiene o establece el datasource de las cuentas de Origen
    ' ''' </summary>
    'Public Property DatasourceMainAccountOrigin As XPInstantFeedbackSource
    '    Get
    '        Return CType(INDsleMainAccountOrigin.Properties.DataSource, XPInstantFeedbackSource)
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        INDsleMainAccountOrigin.Properties.DataSource = value
    '    End Set
    'End Property

    ' ''' <summary>
    ' ''' Ontiene o establece el datasource de las cuentas de destino
    ' ''' </summary>
    'Private Property DatasourceMainAccountDestination As XPInstantFeedbackSource
    '    Get
    '        Return CType(INDsleMainAccountDestination.Properties.DataSource, XPInstantFeedbackSource)
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        INDsleMainAccountDestination.Properties.DataSource = value
    '    End Set
    'End Property

    ''' <summary>
    ''' Obtiene o establece la descripcion
    ''' </summary>
    Private Property Description As String
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
    Private Property AccountOriginId As Integer
        Get
            Return INDsleMainAccountOrigin.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccountOrigin.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta de destino
    ''' </summary>
    Private Property AccountTargetId As Integer
        Get
            Return INDsleMainAccountDestination.EditValue
        End Get
        Set(value As Integer)
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

    ''' <summary>
    ''' Handles the Click event of the INDsbAdd control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        If validateControls() Then

            'Se consulta la cuenta contable que eligieron en cuenta origen para posteriormente realizar una validación
            Dim accountXpo = GetCTNCUENTAById(INDsleMainAccountOrigin.EditValue)

            'Se valida si la clase de la cuenta es 5 no se repita la origen y destino
            If accountXpo.CTNCLASE.CLACODIGO <> 5 AndAlso INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                If INDsleMainAccountOrigin.EditValue = INDsleMainAccountDestination.EditValue Then
                    Mensaje(EeventViewerImages.Advertencia) = "La cuenta origen y la cuenta destino no pueden ser iguales"
                    Exit Sub
                End If
            End If

            Dim _productionCenterHomolo As New ProductionCenterHomologation()
            With _productionCenterHomolo
                .Description = Description
                .AccountOriginId = AccountOriginId
                .AccountOrigin = accountXpo.CUECODIGO
                .FullNameAccountOrigin = INDsleMainAccountOrigin.Text
                If INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                    .AccountTargetId = AccountTargetId
                    .AccountTarget = GetCTNCUENTAById(INDsleMainAccountDestination.EditValue).CUECODIGO
                    .FullNameAccountDestination = INDsleMainAccountDestination.Text
                End If
            End With
            RaiseEvent AddHomologation(_productionCenterHomolo)
            CleanControls()
        End If
    End Sub

    Private Function GetCTNCUENTAById(ByVal id As Integer) As CTNCUENTAXpo
        Dim filtroConsulta As String = "OID = " & id
        Return XpoServiceEx.Instance(SessionValues.Instance.InteropCostContainer).InteropCostService.GetCollection(Of CTNCUENTAXpo)(Nothing, filtroConsulta).FirstOrDefault()
        'Return XpoServiceEx.Instance(SessionValues.Instance.InteropCostContainer).InteropCostService.GetCTNCUENTAById(id)
    End Function

#End Region

#Region "Methods"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Public Sub CleanControls()
        INDsleMainAccountOrigin.EditValue = Nothing
        INDsleMainAccountDestination.EditValue = Nothing
        INDmeDescription.Text = String.Empty
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
        If INDliMainAccountDestination.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDsleMainAccountDestination.EditValue Is Nothing Then
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

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleMainAccountDestination control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleMainAccountDestination_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleMainAccountDestination.QueryPopUp
        If INDsleMainAccountDestination.Properties.DataSource Is Nothing Then
            'Using model As New MProductionCenter("")
            '    INDsleMainAccountDestination.Properties.DataSource = model.ListMainAccountErp()
            'End Using
        End If
    End Sub
End Class
