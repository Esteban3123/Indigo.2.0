CREATE TABLE [dbo].[RIASCUPS] (
    [ID]               INT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]        CHAR (20)   NOT NULL,
    [IDRIAS]           INT         NOT NULL,
    [CONCEPTORIPSRIAS] VARCHAR (2) NOT NULL,
    CONSTRAINT [PK_RIASCUPS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_RIASCUPS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_RIASCUPS_RIAS] FOREIGN KEY ([IDRIAS]) REFERENCES [dbo].[RIAS] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de finalidad o propósito de la consulta/procedimiento en RIPS: accidentes (trabajo, tránsito, rábico, ofídico, otro), eventos catastróficos, lesiones (agresión, autoinfligida), sospechas de maltrato (físico, emocional) o abuso/violencia sexual, enfermedades (general, laboral), diagnóstico, procedimientos terapéuticos, protección específica o detección temprana. VARCHAR(2), valores 01-20 según clasificación RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CONCEPTORIPSRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Finalidad de la consulta:    01 = Accidente de trabajo (AC)  02 = Accidente de tránsito (AC)  03 = Accidente rábico  (AC)  04 = Accidente ofídico  (AC)  05 = Otro tipo de accidente  (AC)  06 = Evento catastrófico  (AC)  07 = Lesión por agresión  (AC)  08 = Lesión auto infligida  (AC)  09 = Sospecha de maltrato físico  (AC)  10 = Sospecha de abuso sexual  (AC)  11 = Sospecha de violencia sexual  (AC)  12 = Sospecha de maltrato emocional  (AC)  13 = Enfermedad general  (AC)  14 = Enfermedad laboral  (AC)  15 = Otra  (AC)    Finalidad del procedimiento:  16 = Diagnóstico (AP)  17 = Terapéutico  (AP)  18 = Protección específica (AP)  19 = Detección temprana de enfermedad general (AP)  20 = Detección temprana de enfermedad laboral (AP)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CONCEPTORIPSRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CONCEPTORIPSRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único de la solicitud/orden RIAS (Red Integral de Atención en Salud) asociada a este cupón o servicio. Clave foránea a tabla RIAS. INT, referencia FK.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de la tabla de RIAS (RIAS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'IDRIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'IDRIAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio o procedimiento según nomenclatura CUPS-IPS del prestador de salud. Clave foránea a tabla INCUPSIPS. CHAR(20), referencia FK para vincular con catálogo de servicios/procedimientos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Servicio IPS (Tabla INCUPSIS)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (PK) de cada registro en la tabla RIASCUPS. INT IDENTITY, clave primaria clustered.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los códigos de servicio CUPS con los grupos o conceptos RIAS (Rutas Integrales de Atención en Salud), permitiendo clasificar cada procedimiento o servicio bajo la ruta y el concepto RIPS-RIAS correspondiente para reportes y facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPS';
