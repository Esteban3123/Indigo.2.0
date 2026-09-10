CREATE TABLE [dbo].[RIASDIAGNOS] (
    [ID]        INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRIAS]    INT      NOT NULL,
    [CODDIAGNO] CHAR (4) NOT NULL,
    CONSTRAINT [PK_RIASDIAGNOS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASDIAGNOS_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_RIASDIAGNOS_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del diagnóstico (CIE-10), referencia a catálogo de diagnósticos clínicos. Clave foránea a tabla INDIAGNOS. Tipo CHAR(4), permite búsquedas por enfermedad, condición médica o código diagnóstico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID del registro RIAS (Registro Individual de Atención en Salud), referencia a la atención/ingreso del paciente. Clave foránea a tabla RIAS. Permite vincular diagnósticos a cada atención o episodio clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID RIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY) de cada diagnóstico registrado en RIAS. Clave primaria de la tabla RIASDIAGNOS. Facilita auditoría y trazabilidad de diagnósticos por atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Diagnósticos asociados a cada registro RIAS (Rutas Integrales de Atención en Salud). Relaciona cada atención o intervención RIAS con uno o más códigos de diagnóstico CIE-10 del paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASDIAGNOS';
