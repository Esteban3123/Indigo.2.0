'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/09/2014
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.FixedAssetRepository
#End Region

Public Class MIPSService
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
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetIPSService(ByVal code As String) As Task(Of Domain.Base.Entities.ActionResult(Of IPSService))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetIPSServiceAsync(code, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Copiar y pegar para la rejilla de plantilla de procedimientos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CopyAndPasteIPSService(data As List(Of List(Of String)), ServiceManual As Integer) As Task(Of ActionResult(Of List(Of SurgicalProcedureService), List(Of Tuple(Of String, Integer))))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.CopyAndPasteIPSServiceAsync(data, ServiceManual)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetIPSServiceByIdSimple(ByVal id As Integer) As Domain.Base.Entities.ActionResult(Of IPSService)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetIPSServiceById(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' se obtiene el servicio de materiales de sutura por id del servcio IPS padre
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetMaterialIPSServiceByParentId(ByVal id As Integer, classService As EClassService) As ActionResult(Of List(Of IPSService))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetSurgicalProcedureServiceByParentIPSId(id, classService)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetIPSServiceById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of IPSService))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetIPSServiceByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveIPSService(ByVal record As IPSService, ByVal idSequense As Int64) As Task(Of ActionResult(Of IPSService))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.SaveIPSServiceAsync(record, idSequense, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteIPSService(ByVal record As IPSService) As Task(Of ActionResult)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.DeleteIPSServiceAsync(record, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function ChangeState(ByVal code As String, ByVal state As Boolean) As Task(Of ActionResult(Of IPSService))
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoContract.ChangeStateIPSServiceAsync(code, state, Me.Indigo.AuditMessageWcf)
    End Function

    Public Function GetCupsHomologationByIPSServiceId(idIPSService As Integer) As List(Of Domain.Entities.CupsHomologation)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetCupsHomologationByIPSServiceId(idIPSService)
    End Function

    Public Function GetSurgicalProcedureServiceByIPSServiceId(idIPSService As Integer) As List(Of Domain.Entities.SurgicalProcedureService)
        Me.Indigo.AuditMessageWcf.Functional = _tagForm
        Return IndigoConecta.Instancia.CurrentCloud.IndigoContract.GetSurgicalProcedureServiceByIPSServiceId(idIPSService)
    End Function

    Public Function GetGeneralLedgerIVAById(ByVal id As Integer) As ActionResult(Of GeneralLedgerIVA)
        Using scope As New OperationContextScope(IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.InnerChannel)
            Me.Indigo.AuditMessageWcf.Functional = _tagForm
            Dim mess As New MessageHeader(Of AuditMessage)(Me.Indigo.AuditMessageWcf)
            Dim header As System.ServiceModel.Channels.MessageHeader = mess.GetUntypedHeader(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            OperationContext.Current.OutgoingMessageHeaders.Add(header)
            Return IndigoConecta.Instancia.CurrentCloud.IndigoAccounting.GetGeneralLedgerIVAById(id)
        End Using
    End Function

    Public Function GetMainAccountById(Id As Integer) As GeneralLedgerMainAccountsXpo
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PaymentsService.GetXPOObject(Of GeneralLedgerMainAccountsXpo)($"Id = {Id}")
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
