CREATE TABLE [Payroll].[BlockRecordPayroll] (
    [Id]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FormId]    INT           NOT NULL,
    [RecordId]  INT           NOT NULL,
    [CodUser]   VARCHAR (250) NOT NULL,
    [NameUser]  VARCHAR (250) NOT NULL,
    [BlockDate] DATETIME      NOT NULL,
    CONSTRAINT [PK_BlockRecordPayroll] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se bloqueó el registro de nómina; marca temporal del bloqueo para auditoría de cambios en payroll.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha y hora en que se bloqueo el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'BlockDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'BlockDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre completo del usuario que ejecutó el bloqueo; identificación legible del profesional o administrador de nómina responsable.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'NameUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'NameUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del usuario (VARCHAR 250) que realizó el bloqueo; identificador del empleado o cuenta de sistema que bloqueó el registro de payroll.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código del usuario quien bloquea el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'CodUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'CodUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del registro de nómina bloqueado; referencia al documento o comprobante de pago que fue objeto del bloqueo.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'RecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro que se encuentra bloqueado', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'RecordId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'RecordId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (INT) del formulario o proceso de nómina que contiene el registro bloqueado; referencia al grupo o lote de pago asociado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del formulario en el que se encuentra bloqueado el registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'FormId';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'FormId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (INT IDENTITY) del registro de bloqueo en la tabla de auditoría; clave primaria que registra cada acción de bloqueo en payroll.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id del registro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de bloqueos de registros en la nómina: guarda qué usuario bloqueó un registro de liquidación o formulario de nómina y en qué fecha, para controlar el acceso y la edición de registros durante el proceso de nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'BlockRecordPayroll';
