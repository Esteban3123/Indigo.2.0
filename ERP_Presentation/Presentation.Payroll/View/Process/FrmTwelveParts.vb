Imports Infrastructure.CrossCutting.Base
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports Presentation.Base


Public Class FrmTwelveParts

    Dim ListEmployee As List(Of Employee)


    Private Sub FrmTwelveParts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.OnlyUndo)

        InitializeTuple()

        _idOperativeUnit = BarraBotones.OperatingUnitValue

        Deshacer()

        _searchMode = False
    End Sub

    Private Sub FrmTwelveParts_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If INDSlType.Enabled Then
            INDSlType.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Inicializa los search que van quemados
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        'Tipo de adquisición
        DatasourceTwelveParts = New List(Of Tuple(Of Integer, String))
        DatasourceTwelveParts.Add(New Tuple(Of Integer, String)(1, "Cuotas Partes x Cobrar"))
        DatasourceTwelveParts.Add(New Tuple(Of Integer, String)(2, "Cuotas Partes x Pagar"))

        INDSlType.Properties.DataSource = DatasourceTwelveParts
    End Sub

    Private Async Sub INDBtnCalculate_Click(sender As Object, e As EventArgs) Handles INDBtnCalculate.Click
        Try
            Using model As New MEmployee("529")
                AsyncLoader(True)
                ListEmployee = Await model.GetEmployeePensionary()

                If ListEmployee IsNot Nothing And ListEmployee.Count > 0 Then
                    INDGcDatas.DataSource = ListEmployee
                End If

                AsyncLoader(False)

            End Using
        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Sub

#Region "Bar Buttons Events"

    ''' <summary>
    '''Evento load de la barra de botones
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

   
    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

 
    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub


#End Region

    Private Function Deshacer()
        INDGcDatas.DataSource = Nothing
    End Function
End Class