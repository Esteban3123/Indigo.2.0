'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MedicalFeesCausationRepository
    Inherits GenericRepository(Of MedicalFeesCausation)
    Implements IMedicalFeesCausationRepository

#Region "Fields"

    ''' <summary>
    ''' Contexto
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene una causacion de honorario medico
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesCausationById(id As Integer) As MedicalFeesCausation Implements IMedicalFeesCausationRepository.GetMedicalFeesCausationById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.MedicalFeesCausation Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As MedicalFeesCausation In Me._context.MedicalFeesCausation.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New MedicalFeesCausation()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una causacion de honorario medico y agregado de contrato
    ''' </summary>
    ''' <param name="ServiceOrderDetailId">Id del detalle de serivicio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetMedicalFeesCausationByServiceOrderDetailId(InvoiceDetailId As Integer, ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId? As Integer) As MedicalFeesCausation Implements IMedicalFeesCausationRepository.GetMedicalFeesCausationByServiceOrderDetailId
        If InvoiceDetailId = 0 Then
            Throw New ArgumentNullException("InvoiceDetailId")
        End If
        If ServiceOrderDetailId = 0 Then
            Throw New ArgumentNullException("ServiceOrderDetailId")
        End If
        Dim res = (From d In Me._context.MedicalFeesCausation.Include("MedicalFeesContract") Where d.InvoiceDetailId = InvoiceDetailId And d.ServiceOrderDetailId = ServiceOrderDetailId And d.ServiceOrderDetailSurgicalId = ServiceOrderDetailSurgicalId Select d).ToList
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As MedicalFeesCausation In Me._context.MedicalFeesCausation.AsNoTracking() Where d.InvoiceDetailId = InvoiceDetailId And d.ServiceOrderDetailId = ServiceOrderDetailId And d.ServiceOrderDetailSurgicalId = ServiceOrderDetailSurgicalId Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New MedicalFeesCausation()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una causacion de honorario por id del detalle de la factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">The invoice detail identifier.</param>
    ''' <returns></returns>
    Public Function GetMedicalFeesCausationByInvoiceDetailId(InvoiceDetailId As Integer) As MedicalFeesCausation Implements IMedicalFeesCausationRepository.GetMedicalFeesCausationByInvoiceDetailId
        Dim query = (From m In _context.MedicalFeesCausation Where m.InvoiceDetailId = InvoiceDetailId Select m).FirstOrDefault()
        If query IsNot Nothing AndAlso query.Id > 0 Then
            Return query
        Else
            Return New MedicalFeesCausation()
        End If
    End Function

    ''' <summary>
    ''' Genera el reconocimiento contable de causaciones pendientes por proveedor
    ''' 📌 Recibe supplierId porque cada proveedor genera su propio reconocimiento con su comprobante contable
    ''' </summary>
    Public Function SP_GenerateCausationRecognition(supplierId As Integer, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As SP_GenerateCausationRecognition_Result Implements IMedicalFeesCausationRepository.SP_GenerateCausationRecognition
        DirectCast(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_GenerateCausationRecognition(supplierId, operatingUnitId, recognitionDate, userCode).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Reversa un reconocimiento de causación específico
    ''' 📌 Recibe solo causationRecognitionId porque cada reconocimiento ya está asociado a un único proveedor
    ''' </summary>
    Public Function SP_ReverseCausationRecognition(causationRecognitionId As Integer, userCode As String) As SP_ReverseCausationRecognition_Result Implements IMedicalFeesCausationRepository.SP_ReverseCausationRecognition
        DirectCast(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseCausationRecognition(causationRecognitionId, userCode).FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene candidatos elegibles para auto-causación via SP_GetCandidatesForAutoCausation.
    ''' Sin límite de batch (usa 100000 como máximo práctico).
    ''' Retorna todas las unidades operativas.
    ''' </summary>
    Public Function GetCandidatesForAutoCausation() As List(Of SP_GetCandidatesForAutoCausation_Result) Implements IMedicalFeesCausationRepository.GetCandidatesForAutoCausation
        Return GetCandidatesForAutoCausationBatched(100000)
    End Function

    ''' <summary>
    ''' Obtiene candidatos elegibles para auto-causación via SP_GetCandidatesForAutoCausation con batching.
    ''' Retorna todas las unidades operativas.
    ''' </summary>
    Public Function GetCandidatesForAutoCausationBatched(batchSize As Integer) As List(Of SP_GetCandidatesForAutoCausation_Result) Implements IMedicalFeesCausationRepository.GetCandidatesForAutoCausationBatched
        Try
            DirectCast(_context, System.Data.Entity.Infrastructure.IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
            Return _context.SP_GetCandidatesForAutoCausation(batchSize)?.ToList()
        Catch ex As Exception
            Return New List(Of SP_GetCandidatesForAutoCausation_Result)()
        End Try
    End Function


#End Region

End Class
