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
Imports Infrastructure.Data.Xpo
#End Region

Public Class MAccountReceivable
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
    ''' obtener una cuota de la factura por id
    ''' </summary>
    ''' <param name="idAccountReceivable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableById(idAccountReceivable As Integer) As AccountReceivable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetAccountReceivableById(idAccountReceivable)
    End Function

    Public Function GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber As String, idCustomer As Integer) As AccountReceivable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetAccountReceivableByInvoiceNumberAndCustomer(invoiceNumber, idCustomer)
    End Function
    ''' <summary>
    ''' obtiene una factura por numero
    ''' </summary>
    ''' <param name="invoiceNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountReceivableByInvoiceNumber(invoiceNumber As String) As AccountReceivable
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPortfolio.GetAccountReceivableByInvoiceNumber(invoiceNumber)
    End Function

    ''' <summary>
    ''' Funcion para obtener los datos de la cuenta contable
    ''' </summary>
    ''' <param name="id">El id del fabricante</param>
    ''' <returns></returns>
    Public Function GetMainAccountById(Id As Integer) As Infrastructure.Data.Xpo.PortfolioRepository.GeneralLedgerMainAccountsXpo
        Dim filtroConsulta As String = "Id = " & Id
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).PortfolioService.GetCollection(Of Infrastructure.Data.Xpo.PortfolioRepository.GeneralLedgerMainAccountsXpo)(Nothing, filtroConsulta).FirstOrDefault
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
