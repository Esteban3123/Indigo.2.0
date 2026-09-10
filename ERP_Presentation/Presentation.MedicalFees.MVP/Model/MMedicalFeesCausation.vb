'***********************************************************************
' Assembly         : Presentacion.MedicalFees.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
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
Imports Infrastructure.Data.Xpo.CrystalRepository

#End Region

Public Class MMedicalFeesCausation
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
    ''' lista los profesionales de la salud
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessional() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessional()
    End Function

    ''' <summary>
    ''' lista los profesionales de la salud con xpCollection
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalXpInstantFeedBackSource() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessional()
    End Function

    ''' <summary>
    ''' lista los profesionales de la salud con xpCollection filtrado por la especialidad
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthCareProfessionalSpecialtyXpCollection() As XPCollection(Of HealthCareProfessionalXpo)
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListHealthCareProfessionalSpecialtyXpCollection()
    End Function

    ''' <summary>
    ''' Consulta los contratos que tiene asociado el médico
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractByHealthProfessionalCode(code As String) As XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).MedicalFeesService.ListHealthProfessionalContractByHealthProfessionalCode(code)
    End Function

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function GetMedicalFeesCausationById(ByVal id As Integer) As Task(Of Domain.Base.Entities.ActionResult(Of MedicalFeesCausation))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.GetMedicalFeesCausationByIdAsync(id, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Guarda o actualiza la entidad
    ''' </summary>
    ''' <param name="record"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function SaveMedicalFeesCausation(ByVal record As List(Of MedicalFeesCausation), ByVal ListMedicalFeesNotes As List(Of MedicalFeesNote), ByVal ListDeleteMedicalFeesNotes As List(Of MedicalFeesNote)) As Task(Of ActionResult(Of List(Of MedicalFeesCausation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.SaveMedicalFeesCausationAsync(record, ListMedicalFeesNotes, ListDeleteMedicalFeesNotes, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="ListInfo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteMedicalFeesCausation(ListInfo As List(Of Tuple(Of Integer, Integer)), Company As String) As Task(Of ActionResult(Of List(Of Tuple(Of String, Integer, Integer))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.DeleteMedicalFeesCausationAsync(ListInfo, Company, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Elimina la entidad
    ''' </summary>
    ''' <param name="recordId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteMedicalFeesCausationFromRemoveFees(ByVal recordId As Integer) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.DeleteMedicalFeesCausationFromRemoveFeesAsync(recordId, Me.Indigo.AuditMessageWcf)
    End Function

    ''' <summary>
    ''' Calcula el valor a causar
    ''' </summary>
    ''' <param name="CupsEntityId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="RateManualId"></param>
    ''' <param name="ValueTotal"></param>
    ''' <param name="presentation"></param>
    ''' <param name="IPSServiceId"></param>
    ''' <param name="IPSServiceCodeName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CausedValue(MedicalFeesCausationId As Integer, RateManualType As Integer, CupsEntityId As Integer, CareGroupId As Integer, RateManualId As Integer, ValueTotal As Decimal, presentation As Integer, IPSServiceId As Integer,
                                      IPSServiceCodeName As String, healthProfessionalCode As String, thirdPartyDescription As String, IPSServiceIdParent As Integer?,
                                      ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId As Integer, MedicalFeesContractId As Integer,
                                      Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As Task(Of ActionResult(Of List(Of CupsHomologation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.CausedValueAsync(MedicalFeesCausationId, RateManualType, CupsEntityId, CareGroupId, RateManualId, ValueTotal, presentation, IPSServiceId, IPSServiceCodeName,
                                                                                             healthProfessionalCode, thirdPartyDescription, IPSServiceIdParent, ServiceOrderDetailId,
                                                                                             ServiceOrderDetailSurgicalId, MedicalFeesContractId, ListCupsHomologation)
    End Function


    Public Async Function CauseInvoice(invoiceDetail As ViewListNoSurgical, Optional listCupsHomologation As List(Of CupsHomologation) = Nothing) As Task(Of ActionResult(Of List(Of CupsHomologation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.CauseInvoiceAsync(invoiceDetail, Indigo.AuditMessageWcf, listCupsHomologation)
    End Function

    Public Async Function CauseInvoiceQx(invoiceDetail As ViewListSurgicalAndPackage, Optional listCupsHomologation As List(Of CupsHomologation) = Nothing) As Task(Of ActionResult(Of List(Of CupsHomologation)))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.CauseInvoiceQxAsync(invoiceDetail, Indigo.AuditMessageWcf, listCupsHomologation)
    End Function

    ''' <summary>
    ''' Metodo que causa los valores masivamente
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function CauseMassively(ListNoSurgical As List(Of NoQxEntity), ListSurgical As List(Of QxEntity), Optional ListCupsHomologation As List(Of CupsHomologation) = Nothing) As Task(Of Domain.Base.Entities.ActionResult(Of Tuple(Of List(Of NoQxEntity), List(Of QxEntity))))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoMedicalFees.CauseMassivelyAsync(ListNoSurgical, ListSurgical, ListCupsHomologation)
    End Function

    ''' <summary>
    ''' Metodo que retorna la consulta de la vista filtrada por id de la factura
    ''' </summary>
    ''' <param name="InvoiceId"></param>
    ''' <returns></returns>
    Public Async Function GetViewListNoSurgical(InvoiceId As Integer, invoiceDetailId As Integer?) As Task(Of List(Of ViewListNoSurgical))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoBilling.GetViewListNoSurgicalAsync(InvoiceId, invoiceDetailId, Me.Indigo.AuditMessageWcf)
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
