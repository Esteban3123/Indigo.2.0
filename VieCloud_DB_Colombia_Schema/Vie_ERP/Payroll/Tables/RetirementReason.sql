CREATE TABLE [Payroll].[RetirementReason] (
    [Id]                TINYINT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Code]              VARCHAR (20)  NOT NULL,
    [Name]              VARCHAR (100) NULL,
    [Compensation]      BIT           NOT NULL,
    [Severance]         BIT           CONSTRAINT [DF_RetirementReason_Serevance] DEFAULT ((0)) NOT NULL,
    [SeveranceInterest] BIT           CONSTRAINT [DF_RetirementReason_SeveranceInterest] DEFAULT ((0)) NOT NULL,
    [Prenotice]         BIT           CONSTRAINT [DF_RetirementReason_Prenotice] DEFAULT ((0)) NOT NULL,
    [Bonus]             BIT           CONSTRAINT [DF_RetirementReason_Bonus] DEFAULT ((0)) NOT NULL,
    [Vacations]         BIT           CONSTRAINT [DF_RetirementReason_Vacations] DEFAULT ((0)) NOT NULL,
    [State]             BIT           NOT NULL,
    [CreationUser]      VARCHAR (20)  NOT NULL,
    [CreationDate]      DATETIME      NOT NULL,
    [ModificationUser]  VARCHAR (20)  NULL,
    [ModificationDate]  DATETIME      NULL,
    [TimeStamp]         ROWVERSION    NOT NULL,
    CONSTRAINT [PK_RetirementReason__Id] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_RetirementReason__Code] UNIQUE NONCLUSTERED ([Code] ASC)
);


GO
CREATE NONCLUSTERED INDEX [PK_RetirementReason_CodeState]
    ON [Payroll].[RetirementReason]([Code] ASC, [State] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal automática (TIMESTAMP) que registra el instante exacto de creación, modificación o cambio de estado del motivo de retiro en la base de datos.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se modificó por última vez el registro del motivo de retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que realizó la última modificación o actualización del motivo de retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora (DATETIME) en que se creó originalmente el registro del motivo de retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario (VARCHAR 20) que creó el registro inicial del motivo de retiro en el sistema.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado activo/inactivo (BIT) del motivo de retiro: 1=Activo, 0=Inactivo. Controla si el motivo está disponible para uso en nómina.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del motivo de retiro 1- Activo 0- Inactivo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el motivo de retiro genera derecho al pago de vacaciones acumuladas: 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Vacations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Vacaciones', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Vacations';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Vacations';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que define si el motivo de retiro incluye pago de prima, aguinaldo o bonificación: 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Bonus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Prima/Aguinaldo', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Bonus';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Bonus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que establece si el motivo de retiro requiere o genera derecho a preaviso: 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Prenotice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Preaviso', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Prenotice';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Prenotice';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si el motivo de retiro genera intereses sobre cesantías acumuladas: 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'SeveranceInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Intereses de cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'SeveranceInterest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'SeveranceInterest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que especifica si el motivo de retiro genera derecho a cesantías (prestaciones sociales por años de servicio): 1=Sí, 0=No.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Severance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cesantías', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Severance';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Severance';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador (BIT) que determina si el motivo de retiro genera indemnización compensatoria al empleado: 1=Genera, 0=No genera.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Compensation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Genera o no indemnizacion 1 - Genera, 0 - No Genera ', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Compensation';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Compensation';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre descriptivo (VARCHAR 100) del motivo de retiro, despido o terminación de contrato (ej: Dimisión, Despido justificado, Jubilación, Renuncia).', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del motivo de Retiro.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Name';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único (VARCHAR 20) del motivo de retiro, utilizado como identificador corto en nómina y reportes RIPS.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Motivo de Retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador numérico autoincremental (TINYINT) único de cada motivo de retiro en la tabla.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de los motivo de retiro', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de motivos o causas de retiro del personal en nómina. Define qué conceptos de liquidación (indemnización, cesantías, intereses, preaviso, prima, vacaciones) aplican según cada causal de terminación del contrato laboral.', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Payroll', @level1type = N'TABLE', @level1name = N'RetirementReason';
