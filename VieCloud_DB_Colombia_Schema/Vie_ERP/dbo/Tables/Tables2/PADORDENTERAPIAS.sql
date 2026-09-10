CREATE TABLE [dbo].[PADORDENTERAPIAS] (
    [ID]                       INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADCONTROL]             INT           NOT NULL,
    [CODSERIPS]                CHAR (20)     NOT NULL,
    [CANTIDAD]                 INT           NOT NULL,
    [DURACION]                 INT           NOT NULL,
    [UNIDADMEDIDA]             INT           NOT NULL,
    [MOTIVO]                   VARCHAR (MAX) NULL,
    [IDDESCRIPCIONRELACIONADA] INT           NULL,
    CONSTRAINT [PK_PHDORDTERAPIAS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PHDORDTERAPIAS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_PHDORDTERAPIAS_PHDCONTROL] FOREIGN KEY ([IDPADCONTROL]) REFERENCES [dbo].[PADCONTROL] ([ID])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia a VIE ERP (contract.CUPSEntityContractDescriptions); relación de interconsulta y contrato de cobertura CUPS para facturación y auditoría de terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) - CUPS interconsulta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones, justificación clínica o notas de la orden de terapia (VARCHAR MAX); causas, diagnósticos relacionados o recomendaciones del profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'MOTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo observacion de terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'MOTIVO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'MOTIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad temporal de duración (INT): 1=Días, 2=Meses; define cómo se interpreta el campo DURACION', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'UNIDADMEDIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'undiad de medidad de la cantidad de duracion: 5 terapias durante 2 meses.    1 - Dias  2 - Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'UNIDADMEDIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'UNIDADMEDIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Período o lapso temporal en que se ejecuta la terapia (INT); ej: 30 días, 2 meses; interpretado según UNIDADMEDIDA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'DURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad de duracion de la terapia ej:    10 sesiones de terapia durante 30 dias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'DURACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'DURACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número total de sesiones, aplicaciones o dosis de terapia ordenadas (INT); ej: 10 sesiones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad de terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de servicio de terapia según nomenclatura CUPS/RIPS (CHAR 20); referencia a catálogo INCUPSIPS para identificar tipo de terapia (Fonoaudiología, Fisioterapia, Psicología, etc.)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de servicio de terapias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clave foránea (FK) que relaciona con tabla PADCONTROL; identificador del control/atención del paciente (cabecera PHD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id con la tabla cabecera de control PHD (PHDCONTROL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincremental (INT IDENTITY) de la orden de terapia en la tabla PADORDENTERAPIAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Detalle de servicios o terapias ordenados dentro de una orden médica de terapias. Registra cada ítem de la orden indicando el servicio (CUPS), la cantidad, duración y unidad de medida prescrita, junto con el motivo clínico de la solicitud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENTERAPIAS';
