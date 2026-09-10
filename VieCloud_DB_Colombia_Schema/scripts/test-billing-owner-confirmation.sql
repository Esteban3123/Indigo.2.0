:ON ERROR EXIT
USE master;
GO
IF N'$(TestDatabase)' NOT LIKE N'CodexBillingOwnerConfirmation[_]%'
    THROW 51099, 'Only an isolated synthetic test database is allowed.', 1;
CREATE DATABASE [$(TestDatabase)];
GO
USE [$(TestDatabase)];
GO
SET QUOTED_IDENTIFIER ON;
SET ANSI_NULLS ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;
GO
CREATE SCHEMA Clinical;
GO
CREATE SCHEMA Billing;
GO
CREATE TABLE dbo.ADCONCOEX (CODCONCEC numeric(18,0) PRIMARY KEY, NUMCONCIT char(20) NULL,
    CONESTADO int NOT NULL, IDHCHISPACA int NULL, IPCODPACI varchar(25), NUMINGRES char(10));
CREATE TABLE dbo.HCHISPACA (ID int PRIMARY KEY, IPCODPACI varchar(25), NUMINGRES char(10),
    CODUSUARI char(20), NUMEFOLIO nchar(10));
GO
:r Vie_Erp/Clinical/Tables/OutboxEvent.sql
:r Vie_Erp/Billing/Tables/ProcessedInbox.sql
:r "Vie_Erp/Clinical/Stored Procedures/ConfirmBillingAppointment.sql"
GO
INSERT dbo.ADCONCOEX VALUES (10, '0', 1, NULL, 'synthetic', 'AD1'),
    (20, NULL, 3, 200, 'synthetic', 'AD2'), (30, '0', 3, NULL, 'synthetic', 'AD3');
INSERT dbo.HCHISPACA VALUES (200, 'synthetic', 'AD2', 'clinician', '7');
EXEC Clinical.ConfirmBillingAppointment 10, 101, 'synthetic', 'AD1', 'test', 'event-1', 'correlation';
IF (SELECT TRY_CONVERT(int,NUMCONCIT) FROM dbo.ADCONCOEX WHERE CODCONCEC=10) <> 101
    THROW 51100, 'The owner ID was not updated.', 1;
IF EXISTS(SELECT 1 FROM Clinical.OutboxEvent) THROW 51101, 'Premature clinical completion.', 1;
EXEC Clinical.ConfirmBillingAppointment 10, 101, 'synthetic', 'AD1', 'test', 'event-1', 'correlation';
IF (SELECT COUNT(*) FROM Billing.ProcessedInbox) <> 1 THROW 51102, 'Duplicate inbox entry.', 1;
BEGIN TRY
    EXEC Clinical.ConfirmBillingAppointment 10, 999, 'synthetic', 'AD1', 'test', 'conflict', 'correlation';
    THROW 51103, 'A conflicting appointment was accepted.', 1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER() <> 51002 THROW;
END CATCH;
EXEC Clinical.ConfirmBillingAppointment 20, 202, 'synthetic', 'AD2', 'test', 'event-2', 'correlation';
IF (SELECT COUNT(*) FROM Clinical.OutboxEvent) <> 1 THROW 51104, 'Missing delayed clinical event.', 1;
IF NOT EXISTS (SELECT 1 FROM Clinical.OutboxEvent WHERE JSON_VALUE(PayloadJson,'$.data.appointmentId')='202'
    AND JSON_VALUE(PayloadJson,'$.data.statusCode')='1' AND JSON_VALUE(PayloadJson,'$.data.userCode')='clinician'
    AND JSON_VALUE(PayloadJson,'$.action')='CHANGE_APPOINTMENT_STATUS'
    AND JSON_VALUE(PayloadJson,'$.containerId')='test') THROW 51105, 'Invalid event contract.', 1;
EXEC Clinical.ConfirmBillingAppointment 20, 202, 'synthetic', 'AD2', 'test', 'event-2', 'correlation';
EXEC Clinical.ConfirmBillingAppointment 20, 202, 'synthetic', 'AD2', 'test', 'republished', 'correlation';
IF (SELECT COUNT(*) FROM Clinical.OutboxEvent) <> 1 THROW 51106, 'Completion duplicated.', 1;
BEGIN TRY
    EXEC Clinical.ConfirmBillingAppointment 30, 303, 'synthetic', 'AD3', 'test', 'missing-folio', 'correlation';
    THROW 51107, 'Missing clinical identity was accepted.', 1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER() <> 51003 THROW;
END CATCH;
IF (SELECT TRY_CONVERT(int,NUMCONCIT) FROM dbo.ADCONCOEX WHERE CODCONCEC=30) <> 0
    THROW 51108, 'Failure did not roll back the owner update.', 1;
IF (SELECT COUNT(*) FROM Billing.ProcessedInbox) <> 3
    THROW 51109, 'Failure committed inbox.', 1;
BEGIN TRY
    EXEC Clinical.ConfirmBillingAppointment 10, 101, 'different', 'AD1', 'test', 'bad-patient', 'correlation';
    THROW 51110, 'Patient mismatch accepted.', 1;
END TRY
BEGIN CATCH
    IF ERROR_NUMBER() <> 51001 THROW;
END CATCH;
PRINT 'PASS: confirmation, redelivery, republish, conflict, discharge-before-confirmation, atomic rollback and patient isolation.';
