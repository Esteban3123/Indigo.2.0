'***********************************************************************
' Assembly         : Presentacion.Portfolio.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/10/2016
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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Domain.Portfolio.Model
#End Region

Public Class MPortfolioProvision
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

    Public Async Function CopyAndPastePortfolioProvision(Data As List(Of List(Of String)), CourtDate As Date, Process As Integer, OperatingUnitId As Integer, applyDeterioration As Byte, Percentage As Decimal, Expectative As Integer) As Task(Of ActionResult(Of List(Of PortfolioProvisionDetail), List(Of Tuple(Of String, Integer))))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.CopyAndPastePortfolioProvisionAsync(Data, CourtDate, Process, OperatingUnitId, applyDeterioration, Percentage, Expectative)
        End Using
    End Function

    Public Async Function GetPortfolioProvision(code As String) As Task(Of ActionResult(Of PortfolioProvision))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioProvisionAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function GetPortfolioProvisionById(Id As Integer) As Task(Of ActionResult(Of PortfolioProvision))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioProvisionByIdAsync(Id, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function SavePortfolioProvision(PortfolioProvision As PortfolioProvision, ByVal listPortfolioProvisionDetailDelete As List(Of Integer)) As Task(Of ActionResult(Of PortfolioProvision))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SavePortfolioProvisionAsync(PortfolioProvision, listPortfolioProvisionDetailDelete, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function AnnularPortfolioProvision(PortfolioProvision As PortfolioProvision) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.AnnularPortfolioProvisionAsync(PortfolioProvision, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    Public Async Function ConfirmPortfolioProvision(PortfolioProvision As PortfolioProvision, ByVal listPortfolioProvisionDetailDelete As List(Of Integer), ByVal operativeUnitId As Integer) As Task(Of ActionResult(Of PortfolioProvision))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.ConfirmPortfolioProvisionAsync(PortfolioProvision, listPortfolioProvisionDetailDelete, Me._indigoSessionValues.AuditMessageWcf, operativeUnitId)
    End Function
    ''' <summary>
    ''' Obtiene los libros contables que se encuentran activos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetActivesLegalBooks() As List(Of VieBotXpo)
        Dim filter As String = "LegalBookId.TypeBook <> 3"
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).AccountingService.GetCollectionAsList(Of VieBotXpo)(Nothing, filter)
    End Function

    ''' <summary>
    ''' Obtiene el deterioro de cartera de acuerdo a la clasificación
    ''' </summary>
    ''' <param name="closingDate"></param>
    ''' <param name="operativeUnitId"></param>
    ''' <returns></returns>
    Public Async Function GetPortfolioDeteriorationByClassification(closingDate As Date, operativeUnitId As Integer) As Task(Of ActionResult(Of List(Of PortfolioDeteriorationByClassificationDTO)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioDeteriorationByClassificationAsync(closingDate, operativeUnitId)
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
