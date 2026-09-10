'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 07/10/2014
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
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo

#End Region

Public Class MIPSServiceGroup
    Implements IDisposable

#Region "Fields"

    '' <summary>
    ''' Variable que contiene la instancia de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues
    ''' <summary>
    ''' Tago del formulario
    ''' </summary>
    Private _tagForm As String



#End Region

#Region "Builder"

    ''' <summary>
    ''' Contructor
    ''' </summary>
    ''' <param name="Tag">tag del form</param>
    ''' <remarks></remarks>
    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' lista los centros de costo por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListBranchOffice() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetBranchOffice()
    End Function

    ''' <summary>
    ''' lista los centros de costo por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetFunctionalUnit()
    End Function

    ''' <summary>
    ''' lista los centros de costo por estado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListCostCenterByStatus() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.GetCostCenterByState(True)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicio IPS
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetIPSServiceGroup(ByVal code As String) As Task(Of BillingConcept)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetIPSServiceGroupAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Obtiene un grupo de servicios ips por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetIPSServiceGroupById(ByVal id As Integer) As Task(Of BillingConcept)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoContract.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetIPSServiceGroupByIdAsync(id)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o actualiza grupo de servicios ips
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveIPSServiceGroup(ByVal IPSServiceGroup As BillingConcept, ByVal idSequense As Int64) As Task(Of ActionResult(Of BillingConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveIPSServiceGroupAsync(IPSServiceGroup, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina un grupo de servicios ips
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteIPSServiceGroup(ByVal IPSServiceGroup As BillingConcept) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteIPSServiceGroupAsync(IPSServiceGroup, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeStateIPSServiceGroup(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of BillingConcept))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateIPSServiceGroupAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Async Function CopyAndPasteBillingConceptCostCenter(data As List(Of List(Of String))) As Task(Of ActionResult(Of List(Of BillingConceptCostCenter)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.CopyAndPasteBillingConceptCostCenterAsync(data)
    End Function

    ''' <summary>
    ''' lista los conceptos de facturacion segun el tipo de servicio, en este caso "servicio primario"
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAssociatedMainServiceId(TypeService As Byte, Id As Integer, type As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).BillingService.ListAssociatedMainServiceId(TypeService, Id, type)
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

    ''' <summary>
    ''' obtiene las cuentas contables
    ''' </summary>
    ''' <param name="ClassType"></param>
    ''' <returns></returns>
    Function GetMainAccounts(Optional ClassType As Byte? = Nothing) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, ClassType:=ClassType)
    End Function

    Function GetBillinConceptByType(type As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListBillingConceptByTypeAndStatus(type, True)
    End Function

End Class
