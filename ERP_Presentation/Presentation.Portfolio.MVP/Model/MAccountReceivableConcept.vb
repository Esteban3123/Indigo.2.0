'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
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

Public Class MAccountReceivableConcept
    Implements IDisposable

#Region "fields"
    ''' <summary>
    ''' Referencia a los valores de session
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String
#End Region

#Region "Builder"
    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal tag As String)
        _tagForm = tag
        _indigoSessionValues = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para guardar un concepto
    ''' </summary>
    ''' <param name="accountReceivableConcept">The portfolio concept.</param>
    ''' <returns></returns>
    Public Async Function SavePortfolioConcept(ByVal accountReceivableConcept As AccountReceivableConcept, ByVal idSequense As Int64) As Task(Of ActionResult(Of AccountReceivableConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveAccountReceivableConceptAsync(accountReceivableConcept, idSequense, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para eliminar un cencepto
    ''' </summary>
    ''' <param name="accountReceivableConcept">The portfolio concept.</param>
    ''' <returns></returns>
    Public Async Function DeletePortfolioConcept(ByVal accountReceivableConcept As AccountReceivableConcept) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.DeleteAccountReceivableConceptAsync(accountReceivableConcept, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetPortfolioConcept(ByVal code As String) As Task(Of AccountReceivableConcept)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetAccountReceivableConceptByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function
    ''' <summary>
    ''' obtiene un documento de cuenta x cobrar por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableConceptById(id As Integer) As AccountReceivableConcept
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetAccountReceivableConceptById(id)
    End Function

    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of AccountReceivableConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ChangeStateAsync(code, state, Me._indigoSessionValues.AuditMessageWcf)
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
