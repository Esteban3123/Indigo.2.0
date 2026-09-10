'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 10-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.CloudAgent
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
#End Region

''' <summary>
''' Model de conexion con los servicios distribuidos de Pais
''' </summary>
Public Class MCurrency
    Inherits ModelBase
    Implements IDisposable

    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Id del frontal
    ''' </summary>
    Private _tagForm As String
    ''' <summary>
    ''' Contiene el tag del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared TAG As String = "2060"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="TAG">tag del formulario</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal TAG As String)
        MyBase.New(TAG)
        Me._tagForm = TAG
        Me._indigoSessionValues = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Obtiene el pais por codigo
    ''' </summary>
    ''' <param name="code">Codigo del pais</param>
    ''' <returns>Currency</returns>
    Public Function GetCurrency(ByVal code As String) As Currency
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCurrency(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el pais por codigo
    ''' </summary>
    ''' <param name="code">Codigo del pais</param>
    ''' <returns>Currency</returns>
    Public Function GetAllCurrency() As List(Of Currency)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCurrency(Indigo)
    End Function

    ''' <summary>
    ''' Obtiene el pais por codigo asincrono
    ''' </summary>
    ''' <param name="code">Codigo del pais</param>
    ''' <returns>Currency</returns>
    Public Async Function GetCurrencyAsync(ByVal code As String) As Task(Of Currency)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCurrencyAsync(code, Indigo)
    End Function

    ''' <summary>
    ''' Obtiene la moneda por el Id
    ''' </summary>
    ''' <param name="Id">Id de la moneda</param>
    ''' <returns>Currency</returns>
    Public Async Function GetCurrencyById(ByVal Id As String) As Task(Of Currency)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetCurrencyByIdAsync(Id, Indigo)
    End Function

    ''' <summary>
    ''' Guarda el pais
    ''' </summary>
    ''' <param name="Currency">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Function SaveCurrency(ByVal Currency As Currency, idSequence As Long) As ActionResult(Of Currency)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveCurrency(Currency, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Actualiza el estado del registro
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Async Function UpdateCurrency(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of Currency))
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.InnerChannel)
            Me._indigoSessionValues.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me._indigoSessionValues.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)

            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.UpdateCurrencyAsync(code, state, Indigo)
        End Using
    End Function

    ''' <summary>
    ''' Guarda el pais asincrono
    ''' </summary>
    ''' <param name="Currency">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Async Function SaveCurrencyAsync(ByVal Currency As Currency, idSequence As Long) As Task(Of ActionResult(Of Currency))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveCurrencyAsync(Currency, Indigo, idSequence)
    End Function

    ''' <summary>
    ''' Borra el pais seleccionado
    ''' </summary>
    ''' <param name="Currency">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Function DeleteCurrency(ByVal Currency As Currency) As ActionMessageResult(Of Currency)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteCurrency(Currency, Indigo)
    End Function

    ''' <summary>
    ''' Borra el pais seleccionado Asincrono
    ''' </summary>
    ''' <param name="Currency">pais</param>
    ''' <returns>Si se hizo o no </returns>
    Public Async Function DeleteCurrencyAsync(ByVal Currency As Currency) As Task(Of ActionMessageResult(Of Currency))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.DeleteCurrencyAsync(Currency, Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los paises
    ''' </summary>
    ''' <returns>lista de paises</returns>
    Public Function ListAllCountries() As List(Of Currency)
        Return IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCurrency(Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los paises asincrono
    ''' </summary>
    ''' <returns>lista de paises</returns>
    Public Async Function ListAllCountriesAsync() As Task(Of List(Of Currency))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.ListAllCurrencyAsync(Indigo)
    End Function

    ''' <summary>
    ''' Lista todos los campos nulos
    ''' </summary>
    ''' <returns>lista de paises</returns>
    Public Async Function GetFieldsNULLAsync() As Task(Of DataSet)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.GetFieldsNULLAsync("Currency", Me.Indigo)
    End Function

    ''' <summary>
    ''' Lista todas las divisas con su abreviatura segun ISO 4217
    ''' </summary>
    ''' <returns></returns>
    Public Function GetISOCurrency() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListAllISO4217()
    End Function

    ''' <summary>
    ''' Lista todas las divisas con su abreviatura segun ISO 4217
    ''' </summary>
    ''' <returns></returns>
    Public Function GetISOCurrencyWithoutOfficial(officialCurrencyId As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListAllISO4217WithoutOfficialCurrency(officialCurrencyId)
    End Function
    ''' <summary>
    ''' Lista todas las divisas con su abreviatura segun ISO 4217
    ''' </summary>
    ''' <returns></returns>
    Public Function GetISOCurrency(ByVal criteria As String) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).CommonService.ListAllISO4217(criteria)
    End Function

    ''' <summary>
    ''' Obtienesla moneda por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCurrencybyIdXpo(id As Integer) As CommonCurrencyXpo
        Return XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).CommonService.GetXPOObject(Of CommonCurrencyXpo)($"Id ={id}")
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
