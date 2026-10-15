CREATE TABLE [dbo].[TriageReportSendEmail] (
    [TriageReportSendEmailId] NUMERIC (18)   IDENTITY (1, 1) NOT NULL,
    [AUTO]                    INT            NOT NULL,
    [Email]                   VARCHAR (60)   NOT NULL,
    [Observations]            VARCHAR (2000) NULL,
    CONSTRAINT [PK_TriageReportSendEmail] PRIMARY KEY CLUSTERED ([TriageReportSendEmailId] ASC),
    CONSTRAINT [FK_ADPA3047E_AUTO] FOREIGN KEY ([AUTO]) REFERENCES [dbo].[ADPA3047E] ([AUTO])
);


GO
ALTER TABLE [dbo].[TriageReportSendEmail] NOCHECK CONSTRAINT [FK_ADPA3047E_AUTO];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones que quedan en el cuerpo del correo al que se le envía el reporte del triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Email al que se enviará el reporte de triage', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'Email';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del autonumérico de la tabla ADPA3047E (Admisión parámetros 3047 - Entidades)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de cabecera de email al que se envía la notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'TriageReportSendEmail', @level2type = N'COLUMN', @level2name = N'TriageReportSendEmailId';

