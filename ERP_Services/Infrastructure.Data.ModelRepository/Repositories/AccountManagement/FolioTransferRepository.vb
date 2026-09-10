Imports Domain.AccountManagement.Model
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class FolioTransferRepository
    Inherits GenericRepository(Of FolioTransfer)
    Implements IFolioTransferRepository, Inject

    Private _context As IGlobalModelUnitOfWork

    Public Sub New(context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un traslado de folio por su Id
    ''' </summary>
    ''' <param name="folioTransferId"></param>
    ''' <returns></returns>
    Public Function GetFolioTransferById(folioTransferId As Integer) As FolioTransfer Implements IFolioTransferRepository.GetFolioTransferById
        Return _context.FolioTransfer.Include("RejectionReason").FirstOrDefault(Function(f) f.Id = folioTransferId)
    End Function

    ''' <summary>
    ''' Obtiene una lista de todos los eventos de folios y también permite filtrar por numero admission, codigo de paciente o centro de atención
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="patientCode">Código del paciente</param>
    ''' <param name="attentionCenter">Centro de atención</param>
    ''' <returns></returns>
    Public Function ListFolioEvents(attentionCenter As String, Optional admissionNumber As String = Nothing, Optional patientCode As String = Nothing, Optional userCode As String = Nothing) As List(Of VFolioTraceabilityProperties) Implements IFolioTransferRepository.ListFolioEvents
        Dim acParam As Object = If(String.IsNullOrWhiteSpace(admissionNumber), DBNull.Value, admissionNumber.Trim())
        Dim pcParam As Object = If(String.IsNullOrWhiteSpace(patientCode), DBNull.Value, patientCode.Trim())
        Dim atParam As Object = If(String.IsNullOrWhiteSpace(attentionCenter), DBNull.Value, attentionCenter.Trim())
        Dim ucParam As Object = If(String.IsNullOrWhiteSpace(userCode), DBNull.Value, userCode.Trim())

        Dim params = New List(Of (String, Object)) From {
            ("@AttentionCenter", atParam),
            ("@UserCode", ucParam),
            ("@AdmissionNumber", acParam),
            ("@PatientCode", pcParam)
        }

        Dim query = Me.ExecuteStoredProcedure(Of VFolioTraceabilityProperties)("[AccountManagement].[SP_GetFolioTraceability]", params)
        Return If(query?.ToList(), New List(Of VFolioTraceabilityProperties)())

    End Function

    ''' <summary>
    ''' Obtiene una lista de traslados de folios de acuerdo a un centro de atención y un código de usuario
    ''' </summary>
    ''' <param name="attentionCenter"></param>
    ''' <param name="userCode"></param>
    ''' <returns></returns>
    Public Function ListFolioTransferRequests(attentionCenter As String, userCode As String, managementAreaCode As String) As List(Of VDashboardProperties) Implements IFolioTransferRepository.ListFolioTransferRequests
        Dim params = New List(Of (String, Object)) From {
                        ("@attentionCenter", attentionCenter.Trim()),
                        ("@userCode", userCode.Trim()),
                        ("@managementAreaCode", managementAreaCode.Trim())
                    }

        Dim sql = "
        SELECT
            LTRIM(RTRIM(AD.NUMINGRES))          AS AdmissionNumber,
            IP.IPNOMCOMP                        AS PatientFullName,
            AD.IFECHAING                        AS AdmissionDate,
            AD.IPCODPACI                        AS PatientCode,
            UF.UFUDESCRI                        AS FunctionalUnitName,
            CG.Name                             AS CareGroupName,
            AD.CODICAMHO                        AS BedNumber,
            DIAG.NOMDIAGNO                      AS Diagnosis,
            RCD.FolioOrder                      AS FolioNumber,
            RCD.TotalFolio                      AS FolioTotalValue,
    
            CASE RCD.FolioType
                WHEN 1 THEN 'EAPB con contrato'
                WHEN 2 THEN 'EAPB sin contrato'
                WHEN 3 THEN 'Particulares'
                WHEN 4 THEN 'Aseguradoras'
                ELSE ''
            END                                 AS FolioType,

            CASE RCD.Status
                WHEN 1 THEN 'Registrado'
                WHEN 2 THEN 'Facturado'
                WHEN 3 THEN 'Bloqueado'
                WHEN 4 THEN 'Anulado'
                WHEN 5 THEN 'Reconocimiento Ingresos'
                WHEN 6 THEN 'Factura Asociada'
                WHEN 7 THEN 'Folio Cerrado'
                ELSE ''
            END                                 AS FolioStatusDescription,

            FT.ReceivingUser                    AS ReceivingUser,
            AD.CODUSUCRE                        AS AdmissionCreationUser,
            AD.CODUSUMOD                        AS AdmissionModificationUser,

            CASE FT.TransferStatus
                WHEN 1 THEN 'Sin traslado'
                WHEN 2 THEN 'Pendiente de aceptación'
                WHEN 3 THEN 'Aceptada'
                WHEN 4 THEN 'Rechazada'
                ELSE 'Sin traslado'
            END                                 AS TransferStatus,

            FT.PreviousUser                     AS PreviousUser, 
            concat(MAP.Code, ' - ', MAP.Name)   AS PreviousManagementArea,
			MAP.Code			                AS PreviousManagementAreaCode,

            ''                                  AS Comments,
            I.InvoiceNumber                     AS InvoiceNumber,
			concat(MAR.Code, ' - ', MAR.Name)   AS AdmissionManagementArea,
			FT.CreationDate			            AS TransferDate,
			I.InvoicedUser			            AS InvoiceUser,
            FT.Id                               AS FolioTransferId

        FROM AccountManagement.FolioTransfer FT
        JOIN ADINGRESO AD WITH (NOLOCK) ON AD.NUMINGRES = FT.AdmissionNumber
        JOIN Billing.RevenueControlDetail RCD WITH (NOLOCK) ON RCD.Id = FT.RevenueControlDetailId
        JOIN Contract.CareGroup CG WITH (NOLOCK) ON CG.Id = RCD.CareGroupId
        LEFT JOIN Billing.Invoice I WITH (NOLOCK) ON I.RevenueControlDetailId = RCD.Id
        JOIN INPACIENT IP WITH (NOLOCK) ON IP.IPCODPACI = AD.IPCODPACI
        JOIN INUNIFUNC UF WITH (NOLOCK) ON UF.UFUCODIGO = AD.UFUCODIGO	
        LEFT JOIN INDIAGNOS DIAG WITH (NOLOCK) ON DIAG.CODDIAGNO = AD.CODDIAING
        JOIN AccountManagement.ManagementAreas MAP ON MAP.Id = FT.ManagementAreaId

        OUTER APPLY (
                SELECT TOP 1 MA.Id, MA.Name, MA.Code
                FROM AccountManagement.ManagementAreasUser MAU WITH (NOLOCK)
                JOIN AccountManagement.ManagementAreas MA WITH (NOLOCK) ON MA.Id = MAU.ManagementAreasId
                WHERE MAU.Usercode = FT.ReceivingUser
                    AND MA.Code = @managementAreaCode
                ORDER BY MAU.Id
        ) MAR

		where AD.CODCENATE = @attentionCenter 
            AND FT.ReceivingUser = @userCode
            AND Ft.TransferStatus = 2 --Pendientes por aceptar
            AND MAR.Code IS NOT NULL -- Asegurar que el usuario pertenece al área de gestión especificada
            AND MAR.Id = FT.ManagementAreaId
        "
        Dim query = Me.ExecuteQueryDR(Of VDashboardProperties)(sql, params)
        Return If(query?.ToList(), New List(Of VDashboardProperties)())

    End Function

End Class
