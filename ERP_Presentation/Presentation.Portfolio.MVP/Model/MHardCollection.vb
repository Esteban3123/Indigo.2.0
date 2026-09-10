'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 06-05-2014
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
Imports Infrastructure.Data.Xpo.PortfolioRepository
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo

#End Region

Public Class MHardCollection
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

    Public Async Function SetCopyPasteOrImportFileHardCollection(dataImportFile As List(Of Domain.Base.Entities.ImportFileRow), dataCopyPaste As List(Of List(Of String))) As Task(Of Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.HardCollectionDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SetCopyPasteOrImportFileHardCollectionAsync(dataImportFile, dataCopyPaste)
    End Function

    Public Function GetPortfolioAccountReceivableById(id As Integer) As PortfolioAccountReceivableXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetPortfolioAccountReceivableXpo(id)
    End Function

    Public Function ListBillsHardCollection() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.ListBillsHardCollection()
    End Function

    Public Async Function GetHardCollectionByCode(code As String) As Task(Of HardCollection)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetHardCollectionByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SaveHardCollection(HardCollection As HardCollection) As Task(Of ActionResult(Of HardCollection))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SaveHardCollectionAsync(HardCollection, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Function GetHardCollectionDetail(hardCollectionId As Integer) As List(Of HardCollectionDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetHardCollectionDetailByHardCollectionId(hardCollectionId)
    End Function

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
