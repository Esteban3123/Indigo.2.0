-- Confirms an owner reference and catches up a discharge that preceded materialization.
CREATE PROCEDURE [Clinical].[ConfirmBillingAppointment]
    @ExternalConsultationId numeric(18,0), @AppointmentId int,
    @PatientCode varchar(25), @AdmissionNumber varchar(10),
    @ContainerId nvarchar(64), @MessageId nvarchar(500), @CorrelationId nvarchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    IF @ExternalConsultationId <= 0 OR @AppointmentId <= 0
       OR NULLIF(LTRIM(RTRIM(@MessageId)), '') IS NULL
       OR NULLIF(LTRIM(RTRIM(@ContainerId)), '') IS NULL
        THROW 51000, 'Invalid billing appointment confirmation.', 1;
    BEGIN TRY
        BEGIN TRANSACTION;
        DECLARE @CurrentId int, @Status int, @ClinicalRecordId int,
                @StoredReference varchar(20), @Found bit = 0;
        SELECT @Found = 1, @CurrentId = TRY_CONVERT(int, NUMCONCIT),
               @StoredReference = NUMCONCIT, @Status = CONESTADO, @ClinicalRecordId = IDHCHISPACA
        FROM dbo.ADCONCOEX WITH (UPDLOCK, HOLDLOCK)
        WHERE CODCONCEC = @ExternalConsultationId AND IPCODPACI = @PatientCode AND NUMINGRES = @AdmissionNumber;
        IF @Found = 0
            THROW 51001, 'Owner consultation does not match patient and admission.', 1;
        IF NULLIF(LTRIM(RTRIM(@StoredReference)), '') IS NOT NULL
           AND (@CurrentId IS NULL OR (@CurrentId <> 0 AND @CurrentId <> @AppointmentId))
            THROW 51002, 'Owner consultation already references another appointment.', 1;
        DECLARE @InboxKey nvarchar(500) = CONVERT(varchar(64), HASHBYTES('SHA2_256',
            CONCAT(@MessageId, '|', @ExternalConsultationId, '|', @AppointmentId)), 2);
        IF EXISTS (SELECT 1 FROM Billing.ProcessedInbox WITH (UPDLOCK, HOLDLOCK)
                   WHERE ConsumerName = N'BillingAppointmentOwnerConfirmation' AND MessageId = @InboxKey)
        BEGIN
            COMMIT;
            RETURN;
        END;
        UPDATE dbo.ADCONCOEX SET NUMCONCIT = CONVERT(varchar(20), @AppointmentId)
        WHERE CODCONCEC = @ExternalConsultationId;

        -- The clinical writer locks the same row before reading the appointment ID.
        IF @Status = 3 AND ISNULL(@CurrentId, 0) <> @AppointmentId
        BEGIN
            DECLARE @UserCode varchar(20), @Folio nvarchar(10);
            SELECT @UserCode = RTRIM(CODUSUARI), @Folio = RTRIM(NUMEFOLIO)
            FROM dbo.HCHISPACA
            WHERE ID = @ClinicalRecordId AND IPCODPACI = @PatientCode AND NUMINGRES = @AdmissionNumber;
            IF NULLIF(@UserCode, '') IS NULL OR NULLIF(@Folio, '') IS NULL
                THROW 51003, 'Completed consultation lacks its clinical record identity.', 1;
            DECLARE @OccurredAt datetime2(7) = SYSUTCDATETIME();
            DECLARE @BusinessId nvarchar(255) = CONCAT('clinical-record:', RTRIM(@PatientCode), ':', @Folio, ':appointment:', @AppointmentId);
            DECLARE @EventId uniqueidentifier = CONVERT(uniqueidentifier, CONVERT(binary(16), HASHBYTES('SHA2_256', CONCAT(@ContainerId, '|', @BusinessId))));
            DECLARE @Data nvarchar(max) = (SELECT @AppointmentId AS appointmentId, '1' AS statusCode,
                @UserCode AS userCode, CAST(0 AS bit) AS preventPatientNotification,
                CAST(0 AS bit) AS noOpWhenExpectedStatusDoesNotMatch FOR JSON PATH, WITHOUT_ARRAY_WRAPPER);
            DECLARE @Payload nvarchar(max) = (SELECT CONVERT(varchar(36), @EventId) AS eventId,
                CONVERT(varchar(36), @EventId) AS commandId, CONVERT(varchar(20), @AppointmentId) AS aggregateId,
                'Appointment' AS aggregateType, 'scheduling.appointment-status-change-requested.v1' AS eventType,
                1 AS schemaVersion,
                DATEDIFF_BIG(MICROSECOND, CONVERT(datetime2, '0001-01-01'), @OccurredAt) * 10 AS aggregateVersion,
                DATEDIFF_BIG(MICROSECOND, CONVERT(datetime2, '0001-01-01'), @OccurredAt) * 10 AS sequence,
                CONCAT(CONVERT(varchar(33), @OccurredAt, 126), 'Z') AS occurredAt,
                @CorrelationId AS correlationId, @BusinessId AS causationId,
                'ehr-owner-services' AS producer, @ContainerId AS containerId, 'OwnerService' AS channel,
                'CHANGE_APPOINTMENT_STATUS' AS action, JSON_QUERY(@Data) AS data
                FOR JSON PATH, WITHOUT_ARRAY_WRAPPER);
            INSERT INTO Clinical.OutboxEvent (EventType, AggregateType, AggregateId, PayloadJson, OccurredAtUtc)
            VALUES ('scheduling.appointment-status-change-requested.v1', 'Appointment',
                    CONVERT(varchar(20), @AppointmentId), @Payload, @OccurredAt);
        END;
        INSERT INTO Billing.ProcessedInbox (ConsumerName, MessageId, TenantId)
        VALUES (N'BillingAppointmentOwnerConfirmation', @InboxKey, @ContainerId);
        COMMIT;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK;
        THROW;
    END CATCH;
END;
