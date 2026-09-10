CREATE TABLE [dbo].[INDAUDITOR] (
    [CODAUDITO]   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODUSUARI]   CHAR (20)     NULL,
    [FECREGISTRO] DATETIME      NULL,
    [NOMEQUIPO]   CHAR (20)     NULL,
    [MENSAJEAUD]  VARCHAR (MAX) NULL,
    CONSTRAINT [PK_INDAUDITOR] PRIMARY KEY CLUSTERED ([CODAUDITO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_INDAUDITOR_FECREGISTRO]
    ON [dbo].[INDAUDITOR]([FECREGISTRO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del mensaje de auditoría, contenido detallado del evento registrado (tipo: VARCHAR MAX, permite texto extenso para trazabilidad y análisis de cambios en el sistema)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'MENSAJEAUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción del mensaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'MENSAJEAUD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'MENSAJEAUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre del equipo de cómputo, servidor o estación de trabajo desde donde se originó la acción auditada (identificador de dispositivo, IP o hostname)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'NOMEQUIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nombre del equipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'NOMEQUIPO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'NOMEQUIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del evento de auditoría en el sistema (timestamp DATETIME, marca temporal del cambio o acción registrada)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que ejecutó la acción auditada, referencia al profesional de salud o administrador del sistema (FK a tabla de usuarios, trazabilidad de responsable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODUSUARI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de auditoría, identificador secuencial de registro en tabla INDAUDITOR (PK INT IDENTITY, clave primaria para cada evento auditado)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODAUDITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo auditor de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODAUDITO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR', @level2type = N'COLUMN', @level2name = N'CODAUDITO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de auditoría del sistema: guarda un historial de acciones, eventos o cambios realizados por los usuarios, incluyendo quién lo hizo, cuándo y desde qué equipo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INDAUDITOR';
