CREATE TABLE [Admissions].[CodeTransferAudit] (
    [Id]                         INT           IDENTITY (1, 1) NOT NULL,
    [PreviousPatientCode]        VARCHAR (25)  NOT NULL,
    [PreviousPatientName]        VARCHAR (500) NOT NULL,
    [PreviousIdentificationType] INT           NOT NULL,
    [NewpatientCode]             VARCHAR (25)  NOT NULL,
    [NewpatientName]             VARCHAR (500) NOT NULL,
    [IdentificationTypeNew]      INT           NOT NULL,
    [UserCode]                   CHAR (20)     NOT NULL,
    [Observations]               VARCHAR (250) NOT NULL,
    [RegistrationDate]           DATETIME      NOT NULL,
    CONSTRAINT [PK__CodeTran__3214EC077152B97B] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_CodeTransferAudit_SEGusuaru] FOREIGN KEY ([UserCode]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);




GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora del registro de la transferencia de código de paciente. Tipo DATETIME, marca cuándo se realizó la auditoria del cambio de identificación.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda la fecha del registro ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'RegistrationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'RegistrationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones o notas explicativas sobre el motivo de la transferencia de código. VARCHAR(250), campo de texto libre para documentar razones o contexto del cambio.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda observaciones', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'Observations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'Observations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario profesional de la salud o administrativo que ejecutó la transferencia. CHAR(20), referencia a tabla SEGusuaru. Identifica quién autorizó el cambio.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Codigo usuario', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'UserCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'UserCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del paciente nuevo (cédula, pasaporte, documento extranjero, etc.). INT, referencia a dominio de tipos de identificación para búsqueda por documento nuevo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'IdentificationTypeNew';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda Tipo de identificacion del paciente nuevo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'IdentificationTypeNew';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'IdentificationTypeNew';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente con el nuevo código asignado. VARCHAR(500), preserva nombre post-transferencia para auditoría de identidad.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Nombre paciente Nuevo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente nuevo o actualizado tras la transferencia. VARCHAR(25), identificador principal post-cambio, equivalente a ID de atención o ingreso nuevo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda codigo de Paciente Nuevo', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'NewpatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de identificación del paciente anterior (cédula, pasaporte, documento, etc.). INT, referencia a dominio de tipos para búsqueda por documento previo.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousIdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Tipo de identidad paciente Anterior', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousIdentificationType';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousIdentificationType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del paciente con el código anterior. VARCHAR(500), preserva nombre pre-transferencia para trazabilidad y auditoría.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Guarda el Nombre del Paciente Anterior', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientName';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientName';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código anterior del paciente antes de la transferencia o cambio de identificación. VARCHAR(25), identificador original para búsqueda histórica y reconciliación de registros.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda el codigo del paciente Anterior ', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientCode';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'PreviousPatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría de transferencias de código de paciente. Guarda el historial de cambios cuando se traslada o fusiona la identificación de un paciente a otro código, incluyendo los datos anteriores y nuevos, el usuario que realizó el cambio y la fecha del evento.', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit';
GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único interno del registro de auditoría (consecutivo automático).', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Admissions', @level1type = N'TABLE', @level1name = N'CodeTransferAudit', @level2type = N'COLUMN', @level2name = N'Id';
