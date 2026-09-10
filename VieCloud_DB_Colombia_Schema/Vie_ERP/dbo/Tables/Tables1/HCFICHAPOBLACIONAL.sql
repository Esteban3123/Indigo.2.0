CREATE TABLE [dbo].[HCFICHAPOBLACIONAL] (
    [ID]                  INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDFICHANOTIFICACION] INT      NOT NULL,
    [CODPOBLACION]        TINYINT  NOT NULL,
    [CODDIAGNO]           CHAR (4) NULL,
    CONSTRAINT [PK_HCFICHA110P] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCFICHAPOBLACIONAL_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHAPOBLACIONAL] NOCHECK CONSTRAINT [FK_HCFICHAPOBLACIONAL_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de diagnóstico CIE-10 (4 caracteres) asociado a la población en la ficha de notificación; tipo CHAR(4), permite NULL', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de clasificación poblacional: 1=Discapacitados, 2=Desplazados, 3=Migrantes, 4=Carcelarios, 5=Gestantes, 6=Indigentes, 7=Población infantil ICBF, 8=Madres comunitarias, 9=Desmovilizados, 10=Centros psiquiátricos, 11=Víctimas violencia armada, 12=Otros grupos, 13=Indígenas; tipo TINYINT, clave foránea RIPS/salud pública', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODPOBLACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de población:   1=Discapacitados   2=Desplazados   3=Migrantes   4=Carcelarios   5=Gestantes   6=Indigentes   7=Población infaltil a cargo del ICBF   8=Madres comunitarias   9=Desmovilizados   10=Centros psiquiátricos   11=Víctimas de violencia armada   12=Otros grupos poblacionales   13=Indigenas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODPOBLACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'CODPOBLACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador (FK) de la ficha de notificación vinculada; referencia a [HCFICHANOTIFICACION].[ID]; permite rastrear ingreso, atención o notificación epidemiológica del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'guarda  el ID  de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autonumérico (IDENTITY) de cada registro de población en la ficha; clave primaria clustered, tipo INT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registros de población asociados a las fichas de notificación en salud pública, vinculando cada ficha con el grupo poblacional del paciente y el diagnóstico (CIE-10) correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHAPOBLACIONAL';
