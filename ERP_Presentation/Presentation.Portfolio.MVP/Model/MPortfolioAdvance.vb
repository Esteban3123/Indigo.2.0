'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-07-2014
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

Public Class MPortfolioAdvance
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
    ''' Guarda un anticipo
    ''' </summary>
    ''' <param name="idSequence">The identifier sequence.</param>
    ''' <returns></returns>
    Public Async Function SavePortfolioAdvance(ByVal portfolioAdvance As PortfolioAdvance, ByVal idSequence As Int64) As Task(Of ActionResult(Of PortfolioAdvance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SavePortfolioAdvanceAsync(portfolioAdvance, idSequence, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un anticipo
    ''' </summary>
    ''' <returns></returns>
    Public Async Function DeletePortfolioAdvance(ByVal portfolioAdvance As PortfolioAdvance) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.DeletePortfolioAdvanceAsync(portfolioAdvance, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Async Function GetPortfolioAdvance(ByVal code As String) As Task(Of ActionResult(Of PortfolioAdvance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioAdvanceAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por Id de manera asincrona
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Async Function GetPortfolioAdvanceByIdAsync(ByVal Id As Integer) As Task(Of ActionResult(Of PortfolioAdvance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioAdvanceByIdAsync(Id)
    End Function

    '' <summary>
    '' Obtiene un anticipo por Id
    '' </summary>
    '' <param name="id">The identifier.</param>
    '' <returns></returns>
    Public Function GetPortfolioAdvanceById(ByVal Id As Integer) As Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance
        Dim filter As String = "Id = " & Id
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.Portfolio_PortfolioAdvance)(Nothing, filter).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene un anticipo por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceByIdSimple(ByVal Id As Integer) As ActionResult(Of PortfolioAdvance)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioAdvanceById(Id)
    End Function

    ''' <summary>
    ''' metodo asincrono para obtener un anticipo por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Async Function GetPortfolioAdvanceByIdSimpleAsync(ByVal Id As Integer) As Task(Of ActionResult(Of PortfolioAdvance))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioAdvanceByIdAsync(Id)
    End Function

    ''' <summary>
    ''' Lista todos los anticipos por Id del tercero
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListPorfolioAdvanceByThirdId(ByVal ThirdId As Integer) As Task(Of ActionResult(Of List(Of PortfolioAdvance)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListPorfolioAdvanceByThirdIdAsync(ThirdId)
    End Function

    ''' <summary>
    ''' Lists the porfolio advance by third identifier with balance.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Public Async Function ListPorfolioAdvanceByThirdIdAndAdmissionWithBalance(ByVal ThirdId As Integer, admission As String) As Task(Of ActionResult(Of List(Of PortfolioAdvance)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ListPorfolioAdvanceByThirdIdAndAdmissionWithBalanceAsync(ThirdId, admission)
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