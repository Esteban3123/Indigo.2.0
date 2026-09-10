CREATE TABLE [dbo].[AGEPRQXMS] (
    [AUTONUMER] INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODPROQXM] INT       NOT NULL,
    [CODSERIPS] CHAR (20) NOT NULL,
    CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGEPRQXMS_AGEPROQXM] FOREIGN KEY ([CODPROQXM]) REFERENCES [dbo].[AGEPROQXM] ([AUTONUMER]),
    CONSTRAINT [FK_AGEPRQXMS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único de procedimientos y servicios RIPS (CUPS), referencia a catálogo nacional de servicios y procedimientos sanitarios. Clave foránea a tabla INCUPSIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de cirugía programada múltiple, identificador de intervención quirúrgica con múltiples procedimientos asociados. Referencia a tabla AGEPROQXM.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODPROQXM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'CODIGO CIRUGIA PROGRAMADA MULTIPLE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODPROQXM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'CODPROQXM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico (Identity), clave primaria única y secuencial de registro en tabla de detalle de procedimientos de cirugía programada múltiple.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'AUTONUMER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS', @level2type = N'COLUMN', @level2name = N'AUTONUMER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de servicios (códigos CUPS) asociados a cada paquete o programa quirúrgico. Relaciona los procedimientos médico-quirúrgicos con sus servicios de salud correspondientes para el agendamiento de cirugías.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEPRQXMS';
