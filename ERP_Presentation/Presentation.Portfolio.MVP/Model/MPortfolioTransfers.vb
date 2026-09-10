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

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent

#End Region

Public Class MPortfolioTransfers
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
    ''' Establece las facturas que se pegaron en la rejilla
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SetBillsTransfersCopyPaste(data As List(Of List(Of String)), PortfolioTransfer As PortfolioTransfer, CompanyType As Integer, OperatingUnitId As Integer) As Task(Of ActionResult(Of List(Of PortfolioTransferDetail)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SetBillsTransfersCopyPasteAsync(data, PortfolioTransfer, CompanyType, OperatingUnitId)
    End Function

    ''' <summary>
    ''' obtener la nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioTransferById(id As Integer) As PortfolioTransfer
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioTransferById(id)
    End Function

    ''' <summary>
    ''' obtener la nota por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetPortfolioTransfersByCode(code As String) As Task(Of PortfolioTransfer)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioTransfersByCodeAsync(code, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' obtiene el detalle del traslado
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioTransfersDetailByIdPortfolioTransfers(idPortfolioTransfer As Integer) As List(Of PortfolioTransferDetail)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GatPortfolioTransferDetailByIdPortfolioTransfer(idPortfolioTransfer)
    End Function

    ''' <summary>
    ''' lista los otros conceptos de traslados
    ''' </summary>
    ''' <param name="idPortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer As Integer) As List(Of PortfolioTransferOtherConcept)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetPortfolioTransferOtherConceptByIdPortfolioTransfer(idPortfolioTransfer)
    End Function

    ''' <summary>
    ''' guardar una nota
    ''' </summary>
    ''' <param name="PortfolioTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SavePortfolioTransfer(PortfolioTransfer As PortfolioTransfer) As Task(Of ActionResult(Of PortfolioTransfer))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.SavePortfolioTransferAsync(PortfolioTransfer, Me._indigoSessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta EL TRM de las monedas origne vs destino
    ''' </summary>
    ''' <param name="FromCurrencyId"></param>
    ''' <param name="ToCurrencyId"></param>
    ''' <returns></returns>
    Public Async Function GetTRMbyCurrencyId(ToCurrencyId As Integer, FromCurrencyId As Integer, Optional DateTrm As Date? = Nothing, Optional entityName As String = Nothing) As Task(Of ActionResult(Of TRM))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetTRMbyCurrencyIdAsync(ToCurrencyId, FromCurrencyId, Me._indigoSessionValues, DateTrm, entityName)
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