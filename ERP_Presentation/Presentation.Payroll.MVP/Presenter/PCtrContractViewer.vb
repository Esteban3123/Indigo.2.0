'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Jose Luis Rojas
' Created          : 21-09-2013
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
Imports Domain.Payroll.Entities
Imports Presentation.Controls.MVP
Imports Presentation.CloudAgent
#End Region

''' <summary>
''' Presentador del control del visor de contratos
''' </summary>
Public Class PCtrContractViewer

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As ICtrContractViewer

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As ICtrContractViewer)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Inicializa los controles necesarios
    ''' </summary>
    Public Sub Initializes()
        Using modelo As New MBusqueda()
            'Razones de retiro
            View.RetirementReasonDatasourceXPO = modelo.ConsultarEntidades(eDataSource.RetirementReason)
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que verifica si el contrato tiene nominas confirmadas para bloquear las acciones 
    ''' </summary>
    ''' <param name="contractId">Id del contrato a verificar</param>
    ''' <returns>Verdadero si existen nominas confirmadas, falso en caso contrario</returns>
    Public Async Function CheckConfirmedLiquidationByContractId(contractId As Integer) As Task(Of Boolean)
        Dim result As Boolean
        Using model As New MContract
            result = Await model.LiquidationConfirmatedByContractAsync(contractId)
        End Using
        Return result
    End Function

    ''' <summary>
    ''' Metodo que verifica si el contrato tiene nominas confirmadas para bloquear las acciones 
    ''' </summary>
    ''' <param name="contractId">Id del contrato a verificar</param>
    ''' <returns>Verdadero si existen nominas confirmadas, falso en caso contrario</returns>
    Public Async Function CheckSchedulesByContractId(contractId As Integer) As Task(Of Boolean)
        Dim result As Boolean
        Using model As New MSchedule
            Dim lsd As List(Of ScheduleDetail) = Await model.GetScheduleDetailByContractId(contractId)

            If lsd.Count > 0 Then
                result = True
            Else
                result = False
            End If

        End Using
            Return result
    End Function

    ''' <summary>
    ''' Metodo que elimina las nominas no confirmadas para eliminar un contrato
    ''' </summary>
    ''' <param name="contractId">Id del contrato a verificar</param>
    ''' <returns>Verdadero si existen nominas confirmadas, falso en caso contrario</returns>
    Public Async Function DeleteNotConfirmedLiquidations(contractId As Integer) As Task(Of Boolean)
        Dim result As Boolean
        Using model As New MContract
            result = Await model.DeleteNotConfirmedLiquidationByContractAsync(contractId)
        End Using
        Return result
    End Function

End Class
