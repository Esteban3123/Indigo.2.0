'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 15-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Presentation.CloudAgent
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Data.Filtering
Imports Infrastructure.Data.Xpo.BillingRepository

#End Region

Public Class MSettingBilling
    Implements IDisposable

#Region "Fields"

    ''' <summary>
    ''' Instancia a los valores de sesión
    ''' </summary>
    Private _sessionValues As SessionValues

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    Private _tagForm As String

#End Region

#Region "Build"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="tag">Tag del funcional</param>
    Public Sub New(ByVal tag As String)
        Me._tagForm = tag
        Me._sessionValues = SessionValues.Instance
        Me._sessionValues.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene un parametro de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetSettingsBillingByIdUnitOperative(ByVal OperatingUnitId As Integer) As Task(Of ActionResult(Of SettingsBilling))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetSettingBillingByUnitOperativeFormAsync(OperatingUnitId)
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro de parametros de inventario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveSettingBilling(ByVal record As SettingsBilling) As Task(Of ActionResult(Of SettingsBilling))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveSettingsBillingAsync(record, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza el registro de parametros de inventario
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveCustomTRM(ByVal record As List(Of CustomTRM)) As Task(Of ActionResult(Of CustomTRM))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.SaveCustomTRMAsync(record, Me._sessionValues.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Consulta todos los trm especificos segun su unidad operativa
    ''' </summary>
    ''' <param name="OperatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetCustormTRM(OperatingUnitId As Integer) As Task(Of ActionResult(Of List(Of CustomTRM)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetCustomTRMAsync(OperatingUnitId, True)
    End Function

    ''' <summary>
    ''' Obtiene un parametro de honorarios medicos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CountBillingInvoice() As Integer
        Return IndigoConecta.Instancia.CurrentCloud.IndigoBilling.CountBillingInvoice()
    End Function

    ''' <summary>
    ''' Lista todas las divisas con su abreviatura segun ISO 4217
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCustomTRM(ByVal criteria As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CustomTRMXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(CustomTRMXpo))
        Dim filter As CriteriaOperator = CriteriaOperator.Parse(criteria)
        Return New XPInstantFeedbackSource(_classEntity, Nothing, criteria)
    End Function

#End Region

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
