'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Rafael Patiño
' Created          : 17-09-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : ""
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent
Imports Presentation.CloudAgent.IndigoReference.Glosas
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Domain.Security.Entities
Imports Infrastructure.Data.Xpo

''' <summary>
''' Modelo que se comunica con los servicios corresporndientes al funcional
''' </summary>
Public Class Menlistmentconciliation
    Implements IDisposable

    ''' <summary>
    ''' Variable para inicializar los valores de sesion
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String

#Region "Builders"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Sub New(Tag As String)
        Me._tagForm = Tag
    End Sub

#End Region

#Region "Metodos"

    ''' <summary>
    ''' Obtiene un cliente por su nit
    ''' </summary>
    ''' <param name="nit">Nit del cliente</param>
    ''' <returns>El cliente</returns>
    Public Async Function GetCustomerByNit(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.GetCustomerAsync(nit.Trim(), Me.Indigo)
    End Function

    ''' <summary>
    ''' Obtiene una lista de facturas por el nit usando las entidades XPO
    ''' </summary>
    ''' <param name="ListInvoice">Nit a consultar</param>
    ''' <returns>Un objeto <see cref="DevExpress.Xpo.XPInstantFeedbackSource" /></returns>
    Public Function ListXpoInvoicesByNitExportExcel(ByVal ListInvoice As List(Of String), ByVal OnlyMovements As Boolean) As Object
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).GlosasService.GetInvoiceExportExcel(ListInvoice, OnlyMovements)
    End Function

    ''' <summary>
    ''' Funcion para Listar las facturas que esta Lista para ser conciliadas
    ''' </summary>
    ''' <param name="Nit">entidad de la facturas</param>
    ''' <param name="DateInicial">fecha inicial</param>
    ''' <param name="DateEND">fecha final</param>
    ''' <returns>lista de facturas a conciliar</returns>
    ''' <remarks></remarks>
    Public Function ListGlosaPortfolioExportExcel(Nit As String, DateInicial As Date, DateEND As Date) As List(Of GlosaPortfolioGlosada)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoGlosas.ListGlosaPortfolioExportExcel(Nit.Trim(), DateInicial, DateEND, Me.Indigo)
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


