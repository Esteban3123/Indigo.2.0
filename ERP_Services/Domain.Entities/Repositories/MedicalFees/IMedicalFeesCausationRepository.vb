'************************************************************
' Assembly         : Domain.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IMedicalFeesCausationRepository
    Inherits IRepository(Of MedicalFeesCausation)

    ''' <summary>
    ''' Obtiene una causacion de honorario medico
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesCausationById(id As Integer) As MedicalFeesCausation
    ''' <summary>
    ''' Obtiene una causacion de honorario por id del detalle de la factura
    ''' </summary>
    ''' <param name="InvoiceDetailId">The invoice detail identifier.</param>
    ''' <returns></returns>
    Function GetMedicalFeesCausationByInvoiceDetailId(InvoiceDetailId As Integer) As MedicalFeesCausation
    ''' <summary>
    ''' Obtiene una causacion de honorario medico y agregado de contrato
    ''' </summary>
    ''' <param name="ServiceOrderDetailId">Id del detalle de serivicio</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMedicalFeesCausationByServiceOrderDetailId(InvoiceDetailId As Integer, ServiceOrderDetailId As Integer, ServiceOrderDetailSurgicalId? As Integer) As MedicalFeesCausation

    '' <summary>
    '' Genera el reconocimiento contable de causaciones pendientes por unidad operativa y proveedor
    '' </summary>
    '' <param name="supplierId">ID del proveedor (médico o agremiación)</param>
    '' <param name="operatingUnitId">ID de la unidad operativa</param>
    '' <param name="recognitionDate">Fecha del reconocimiento</param>
    '' <param name="userCode">Código del usuario</param>
    '' <returns>Resultado del stored procedure</returns>
    '' <remarks>
    '' Este método llamará al SP MedicalFees.SP_GenerateCausationRecognition
    '' El SP consultará internamente la vista ViewListCausationwithoutRecognition
    '' filtrada por SupplierId y OperatingUnitId, y generará los asientos contables correspondientes
    '' </remarks>
    Function SP_GenerateCausationRecognition(supplierId As Integer, operatingUnitId As Integer, recognitionDate As DateTime, userCode As String) As SP_GenerateCausationRecognition_Result

    '' <summary>
    '' Reversa un reconocimiento de causación
    '' </summary>
    '' <param name="causationRecognitionId">ID del reconocimiento a reversar</param>
    '' <param name="userCode">Código del usuario</param>
    '' <returns>Resultado del stored procedure</returns>
    '' <remarks>
    '' Este método llamará al SP MedicalFees.SP_ReverseCausationRecognition
    '' </remarks>
    Function SP_ReverseCausationRecognition(causationRecognitionId As Integer, userCode As String) As SP_ReverseCausationRecognition_Result

    ''' <summary>
    ''' Obtiene candidatos elegibles para auto-causación via SP_GetCandidatesForAutoCausation.
    ''' Retorna todas las unidades operativas.
    ''' </summary>
    ''' <returns>Lista de candidatos CUPS sin causación activa</returns>
    Function GetCandidatesForAutoCausation() As List(Of SP_GetCandidatesForAutoCausation_Result)

    ''' <summary>
    ''' Obtiene candidatos con batching via SP_GetCandidatesForAutoCausation.
    ''' Retorna todas las unidades operativas.
    ''' </summary>
    ''' <param name="batchSize">Cantidad máxima de candidatos a retornar</param>
    ''' <returns>Lista acotada de candidatos</returns>
    Function GetCandidatesForAutoCausationBatched(batchSize As Integer) As List(Of SP_GetCandidatesForAutoCausation_Result)

End Interface
