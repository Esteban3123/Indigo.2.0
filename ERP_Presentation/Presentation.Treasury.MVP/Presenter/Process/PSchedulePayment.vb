'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán
' Created          : 21-08-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Domain.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Domain.Base.Entities
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo.PaymentsRepository

#End Region

Public Class PSchedulePayment
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As ISchedulePayment

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As ISchedulePayment)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene los parametros de pagos por unidad operativa
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GetSettingsPaymentsByOperatingUnitId(OperatingUnitId As Integer)
        Dim filter As String = "IdOperatingUnit = " & OperatingUnitId
        Me.View.PaymentsSettingPaymentsXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetCollection(Of PaymentsSettingPaymentsXpo)(Nothing, filter).FirstOrDefault()
    End Sub

    ''' <summary>
    ''' Obtiene una secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequense = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Initializes the commision account.
    ''' </summary>
    Public Async Function InitializeSchedulePaymentDatasource() As Task
        Using Model As New MSchedulePayment(Me.View.MyTag)
            Dim _schedule = Await Model.GetSPSchedulePayment("")
            If _schedule.StateResult Then
                Me.View.SchedulePaymentDatasource = _schedule.ObjectEmbbeded
                Me.View.OriginalDatasource = _schedule.ObjectEmbbeded
            End If
        End Using
    End Function

    ''' <summary>
    ''' Initializes the schedule payment datasource with code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    Public Async Function InitializeSchedulePaymentDatasourceWithCode(code As String) As Task
        Using Model As New MSchedulePayment(Me.View.MyTag)
            Dim _resultSchedule = Await Model.GetSchedulePaymentDatasourceWithPayment(code)
            If Not _resultSchedule.StateResult And Me.View.Status = 1 Then
                Me.View.Mensaje(EeventViewerImages.Advertencia) = _resultSchedule.Message
                If _resultSchedule.ObjectEmbbeded IsNot Nothing AndAlso _resultSchedule.ObjectEmbbeded.Count > 0 Then
                    Me.View.SchedulePaymentDatasource = _resultSchedule.ObjectEmbbeded
                    Me.View.OriginalDatasource = _resultSchedule.ObjectEmbbeded
                Else
                    Await InitializeSchedulePaymentDatasource()
                End If
            Else
                Me.View.SchedulePaymentDatasource = _resultSchedule.ObjectEmbbeded
                Me.View.OriginalDatasource = _resultSchedule.ObjectEmbbeded
            End If
        End Using
    End Function

End Class