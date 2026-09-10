Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Controls.MVP

Public Class FrmPopupConceptLiquidationAdjusments

#Region "EVENTS"
    ''' <summary>
    ''' Evento para adicionar un nuevo ajuste a los conceptos de liquidación de rentas para trabajadores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddConceptLiquidationAdjustment(sender As Object, e As AddAdjustmentsEventArgs)
#End Region

#Region "Variables"
    ''' <summary>
    ''' Variable que contiene el tipo de concepto que se esta agregando del movimiento
    ''' </summary>
    Private ConceptType As ExemptIncomeType

#End Region

#Region "Builder"

    Public Sub New(_Source As ExemptIncomeType)
        ' This call is required by the designer.
        InitializeComponent()

        ConceptType = _Source
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

    ''' <summary>
    ''' Llena el control para la naturaleza del movimiento
    ''' </summary>
    Private Sub LoadDatasources()
        Dim Nature As New List(Of Tuple(Of Integer, String))
        Nature = New List(Of Tuple(Of Integer, String))
        Nature.Add(New Tuple(Of Integer, String)(1, "Débito"))
        Nature.Add(New Tuple(Of Integer, String)(2, "Crédito"))
        INDSleNature.Properties.DataSource = Nature.ToList()
    End Sub

    Private Function ValidateControls()
        If String.IsNullOrEmpty(INDSeIncome.EditValue) OrElse INDSeIncome.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ingrese el valor del ingreso para el ajuste"
            Return False
        End If

        If INDSleNature.EditValue Is Nothing OrElse INDSleNature.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una naturaleza para el movimiento"
            Return False
        End If

        If String.IsNullOrEmpty(INDSeValue.EditValue) OrElse INDSeValue.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Ingrese un valor para el ajuste"
            Return False
        End If

        If String.IsNullOrWhiteSpace(INDMeObservations.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "No se ha agregado una observación"
            Return False
        End If

        If INDSeIncome.EditValue <= INDSeValue.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = "Los ingresos no pueden ser inferiores al valor ajustado"
            Return False
        End If

        Return True
    End Function

#End Region

    Private Sub FrmPopupExemptIncomeDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        LoadDatasources()
    End Sub

    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If ValidateControls() = False Then
            Exit Sub
        End If

        Dim NewConceptLiquidationAdjustment As New AccountPayableDetailConceptLiquidationAdjusments
        NewConceptLiquidationAdjustment.ConceptType = ConceptType
        NewConceptLiquidationAdjustment.TotalIncome = INDSeIncome.EditValue
        NewConceptLiquidationAdjustment.Nature = INDSleNature.EditValue
        NewConceptLiquidationAdjustment.Value = INDSeValue.EditValue
        NewConceptLiquidationAdjustment.Observations = INDMeObservations.EditValue
        NewConceptLiquidationAdjustment.Status = 1
        NewConceptLiquidationAdjustment.UUID = Guid.NewGuid().ToString()

        Dim args As New AddAdjustmentsEventArgs
        args.NewAdjustment = NewConceptLiquidationAdjustment
        RaiseEvent AddConceptLiquidationAdjustment(Nothing, args)

        Close()
    End Sub


End Class