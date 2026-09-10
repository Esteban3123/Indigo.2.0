'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports Domain.Base.Entities
Imports Presentation.Base
Imports System.ServiceModel
#End Region

Public Class MDispersionFunds
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Efectúa los pagos de la programación
    ''' </summary>
    ''' <returns></returns>
    Public Async Function MakeSchedulePayment(ByVal SchedulePayment As SchedulePayment, ByVal idSequence As Int64, ByVal sequenceC As Domain.Entities.TreasurySequence) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.MakeSchedulePaymentAsync(SchedulePayment, Me._indigoSessionValues.AuditMessageWcf, idSequence, sequenceC)
    End Function

    ''' <summary>
    ''' metodo para generar archivos para bancos
    ''' </summary>
    ''' <param name="SchedulePayment"></param>
    ''' <param name="bankId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GenerateBankFile(SchedulePayment As SchedulePayment, bankId As Integer, Optional optionalParameters As List(Of String) = Nothing) As Task(Of ActionResult(Of String))
        If optionalParameters Is Nothing Then
            optionalParameters = New List(Of String)
        End If
        optionalParameters.Insert(0, If(String.IsNullOrEmpty(_indigoSessionValues.IndigoVerificationDigitNit), String.Empty, _indigoSessionValues.IndigoVerificationDigitNit))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoTreasury.GenerateBankFileAsync(SchedulePayment, bankId, _indigoSessionValues.IndigoCompanyNit, _indigoSessionValues.IndigoCompanyName, optionalParameters)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: desechar estado administrado (objetos administrados).
            End If

            ' TODO: liberar recursos no administrados (objetos no administrados) e invalidar Finalize() below.
            ' TODO: Establecer campos grandes como Null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: invalidar Finalize() sólo si la instrucción Dispose(ByVal disposing As Boolean) anterior tiene código para liberar recursos no administrados.
    'Protected Overrides Sub Finalize()
    '    ' No cambie este código. Ponga el código de limpieza en la instrucción Dispose(ByVal disposing As Boolean) anterior.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Visual Basic agregó este código para implementar correctamente el patrón descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class