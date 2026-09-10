'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Kevin Garay Rodriguez
' Created          : 05-03-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base

#End Region
''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class MManualConcept
    Inherits ModelBase
    Implements IDisposable

#Region "Construct"

    Shared TAG As String = "335"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <remarks></remarks>
    Sub New()
        MyBase.New(TAG)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Funcion que obtiene un concepto manual por su consecutivo
    ''' </summary>
    ''' <param name="consecutive"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetManualConcepts(consecutive As Integer) As Task(Of ManualConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetManualConceptsAsync(consecutive, Indigo)
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un Concepto Manual
    ''' </summary>
    ''' <param name="manualConcept"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveManualConcepts(manualConcept As ManualConcepts) As Task(Of ActionMessageResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveManualConceptAsync(manualConcept, Indigo)
    End Function

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <param name="_date">fecha que se quiere iniciar</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetManualConceptsByConceptAndDate(_conceptId As Integer, _contractNumber As Integer, _listDate As List(Of Date), ProcessType As Byte, Optional _otherDate As Date = Nothing) As Task(Of ManualConcepts)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetManualConceptsByConceptAndDateAsync(_conceptId, _contractNumber, _listDate, ProcessType, Indigo, _otherDate)
    End Function

    ''' <summary>
    ''' Funcion que sirve para verificar un concepto manual ya ha sido registrado a un empleado
    ''' </summary>
    ''' <param name="_conceptId">id del concepto</param>
    ''' <param name="_contractNumber">numero de contrato</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetManualConceptsByConceptAndEndContractTrueAsync(_conceptId As Integer, _contractNumber As Integer, ProcessType As Byte) As Task(Of Boolean)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetManualConceptsByConceptAndEndContractTrueAsync(_conceptId, _contractNumber, ProcessType, Indigo)
    End Function
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Para detectar llamadas redundantes

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: eliminar estado administrado (objetos administrados).
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

    ' Visual Basic agregó este código para implementar correctamente el modelo descartable.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' No cambie este código. Coloque el código de limpieza en Dispose(disposing As Boolean).
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class