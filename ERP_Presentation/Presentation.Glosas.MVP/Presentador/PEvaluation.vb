'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 2013-07-03
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports System.Runtime.CompilerServices

#End Region

''' <summary>
''' Presentador del frontal de evaluación
''' </summary>
''' 
Public Class PEvaluation

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de evaluación
    ''' </summary>
    Private _view As IEvaluation
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista</param>
    Public Sub New(ByRef view As IEvaluation)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region

End Class

Public Enum StatesGlosaObjectionC

    ''' <summary>
    ''' Sin Confirmar
    ''' </summary>
    SinConfirmar = 1
    ''' <summary>
    ''' Confirmado Radicado
    ''' </summary>
    ConfirmadoRadicado = 2
    ''' <summary>
    ''' Oficio Con Respuesta Enviada
    ''' </summary>
    OficioConRespuestaEnviada = 3
    ''' <summary>
    ''' Anulado
    ''' </summary>
    Anulado = 4

End Enum

Public Enum StatesGlosaMovements

    ''' <summary>
    ''' Pendiente Evaluar Glosa
    ''' </summary>
    PendienteEvaluarGlosa = 1
    ''' <summary>
    ''' Glosa Evaluada
    ''' </summary>
    GlosaEvaluada = 2
    ''' <summary>
    ''' Pendiente Evaluar Reiteracion
    ''' </summary>
    PendienteEvaluarReiteracion = 3
    ''' <summary>
    ''' Reiteracion Evaluada
    ''' </summary>
    ReiteracionEvaluada = 4
    ''' <summary>
    ''' Pendiente Conciliar
    ''' </summary>
    PendienteConciliar = 5
    ''' <summary>
    ''' Conciliado
    ''' </summary>
    Conciliado = 6

End Enum



Public Enum StatesGlosaPortfolio

    ''' <summary>
    ''' Pediente Confirmado
    ''' </summary>
    PedienteConfirmado = 1
    ''' <summary>
    ''' Pendiente Evaluacion Glosa
    ''' </summary>
    PendienteEvaluacionGlosa = 2
    ''' <summary>
    ''' Pendiente Envio De Oficio
    ''' </summary>
    PendienteEnvioDeOficio = 3
    ''' <summary>
    ''' Pendiente Confirmar Reiteracion
    ''' </summary>
    PendienteConfirmarReiteracion = 4
    ''' <summary>
    ''' Pendiente Evaluacion Reiteracion
    ''' </summary>
    PendienteEvaluacionReiteracion = 5
    ''' <summary>
    ''' Pendiente Envio De Oficio Reiteracion
    ''' </summary>
    PendienteEnvioDeOficioReiteracion = 6
    ''' <summary>
    ''' Pendiente Conciliado
    ''' </summary>
    PendienteConciliado = 7
    ''' <summary>
    ''' Conciliado
    ''' </summary>
    Conciliado = 8
    ''' <summary>
    ''' Pendiente De Solucion Juridica
    ''' </summary>
    PendienteDeSolucionJuridica = 9
    ''' <summary>
    ''' Cartera Castigada
    ''' </summary>
    CarteraCastigada = 10
    ''' <summary>
    ''' Glosa Con Respuesta
    ''' </summary>
    GlosaConRespuesta = 11
    ''' <summary>
    ''' Estado final
    ''' </summary>
    FinGlosa = 12

End Enum

<Obsolete()>
Public Enum ConceptsGlosaEvaluation

    ''' <summary>
    ''' Glosa O Devolucion Injustificada
    ''' </summary>
    GlosaODevolucionInjustificada = 996
    ''' <summary>
    ''' No Subsanada
    ''' </summary>
    NoSubsanada = 997
    ''' <summary>
    ''' Subsanada Parcial
    ''' </summary>
    SubsanadaParcial = 998
    ''' <summary>
    ''' Subsanada
    ''' </summary>
    Subsanada = 999

    ''' <summary>
    ''' Devolucion extemporanea
    ''' </summary>
    ''' <remarks></remarks>
    devolucion = 995

End Enum

Public Enum ConceptsGlosaEvaluationByType

    ''' <summary>
    ''' Glosa O Devolucion Injustificada
    ''' </summary>
    GlosaODevolucionInjustificada = 5
    ''' <summary>
    ''' No Subsanada
    ''' </summary>
    NoSubsanada = 8
    ''' <summary>
    ''' Subsanada Parcial
    ''' </summary>
    SubsanadaParcial = 6
    ''' <summary>
    ''' Subsanada
    ''' </summary>
    Subsanada = 7

    '*** Devolucion extemporanea ****
    ' 995
    DevolucionInjustificada = 9
    DevolucionJustificada = 10
    '****************************
End Enum

Public Enum GlosaDocumentType

    ''' <summary>
    ''' Glosa
    ''' </summary>
    GlosaFirst = 1
    ''' <summary>
    ''' Reiteración
    ''' </summary>
    Reiteration = 2

End Enum

''' <summary>
''' Metodos extendidos para las operaciones de evaluación
''' </summary>
Public Module EvaluationExtendedMethods

    ''' <summary>
    ''' Obtiene el estado de una objeción o reiteracion segun los parametros de tiempo
    ''' </summary>
    ''' <param name="obj">Entidad de tipo <see cref=" Domain.Entities.GlosaObjectionsReceptionD" /></param>
    ''' <returns>Estado</returns>
    <Extension()>
    Public Function GetTimeParameter(ByVal obj As  Domain.Entities.GlosaObjectionsReceptionD) As String
        Dim state As String = String.Empty

        Return state
    End Function

End Module