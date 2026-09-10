CREATE TABLE [dbo].[PADORDENMEDICAS] (
    [ID]           INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDPADCONTROL] INT           NOT NULL,
    [CODPRODUC]    CHAR (20)     NOT NULL,
    [CANTIDAD]     INT           NOT NULL,
    [CODVIAADM]    VARCHAR (20)  NOT NULL,
    [FRECUENCIA]   VARCHAR (500) NOT NULL,
    CONSTRAINT [PK_PHCORDMEDICA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PHCORDMEDICA_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_PHCORDMEDICA_PHDCONTROL] FOREIGN KEY ([IDPADCONTROL]) REFERENCES [dbo].[PADCONTROL] ([ID]),
    CONSTRAINT [FK_PHDORDENMEDICAS_HCVIAADMI] FOREIGN KEY ([CODVIAADM]) REFERENCES [dbo].[HCVIAADMI] ([CODVIAADM])
);


GO
ALTER TABLE [dbo].[PADORDENMEDICAS] NOCHECK CONSTRAINT [FK_PHCORDMEDICA_IHLISTPRO];




GO
ALTER TABLE [dbo].[PADORDENMEDICAS] NOCHECK CONSTRAINT [FK_PHCORDMEDICA_IHLISTPRO];


GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción de frecuencia del medicamento: intervalo, ritmo o patrón de administración (ej: cada 8 horas, diario, cada 12 horas). Guía temporal para dosificación de fármacos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion de frecuencia medicamento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'FRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de vía de administración del medicamento (oral, intravenosa, intramuscular, tópica, etc.). FK a tabla HCVIAADMI. Ruta de ingreso del fármaco al paciente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Via de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODVIAADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades o dosis del producto a administrar por cada frecuencia. Número entero de unidades del medicamento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'cantidad de producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del producto farmacéutico o medicamento (FK a tabla IHLISTPRO). Identificador único del fármaco prescrito en la orden médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'codigo del producto IHLISTPRO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID de relación con tabla de control PADCONTROL. Vínculo a la atención/ingreso/control del paciente asociado a esta orden médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID relacion tabla control de PHD (PHDCONTROL)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'IDPADCONTROL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY). Clave primaria de la orden médica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Líneas de detalle de órdenes médicas de pacientes: registra cada medicamento o producto ordenado, con su cantidad, vía de administración y frecuencia de dosificación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'PADORDENMEDICAS';
