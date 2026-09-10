Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls

Public Class FrmPopupEditExemptIncome

#Region "EVENTS"
    ''' <summary>
    ''' evento para adicionar un valor modificado
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddValuesModificated(sender As Object, e As AddValuesModificatedEventArgs)
#End Region

#Region "Variables"

    Private ValueModificated As AccountPayableDetailConceptLiquidationValuesModificated

#End Region

#Region "Builder"

    Public Sub New(_valueModificated As AccountPayableDetailConceptLiquidationValuesModificated)
        ' This call is required by the designer.
        InitializeComponent()

        ValueModificated = _valueModificated
    End Sub

#End Region

#Region "Methods"

    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Handles"

    ''' <summary>
    ''' Evento que se dispara cuando se abre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupExemptIncomeDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDSeNewValue.EditValue = ValueModificated.NewValue
        INDMeObservations.EditValue = ValueModificated.Observations
    End Sub

    Private Sub INDSbAccept_Click(sender As Object, e As EventArgs) Handles INDSbAccept.Click
        If ValueModificated.PreviousValue = INDSeNewValue.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor original no ha sido modificado"
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(INDMeObservations.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha agregado una observación"
            Exit Sub
        End If

        ValueModificated.NewValue = INDSeNewValue.EditValue
        ValueModificated.Observations = INDMeObservations.EditValue

        Dim args As New AddValuesModificatedEventArgs
        args.ValueModificated = Me.ValueModificated
        RaiseEvent AddValuesModificated(Nothing, args)

        Close()
    End Sub

#End Region

End Class