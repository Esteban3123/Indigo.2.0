CREATE TABLE [dbo].[ADRES4505CENATEN] (
    [ID]           INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDADRES4505C] INT       NOT NULL,
    [CODCENATE]    CHAR (10) NOT NULL,
    CONSTRAINT [PK_ADRES4505CENATEN] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRES4505CENATEN_ADRES4505C] FOREIGN KEY ([IDADRES4505C]) REFERENCES [dbo].[ADRES4505C] ([ID]),
    CONSTRAINT [FK_ADRES4505CENATEN_ADRES4505CENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención (centro de salud, clínica, hospital, unidad funcional). Clave foránea hacia tabla ADCENATEN. Identifica la institución o sede donde se presta el servicio de salud.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (FK) que vincula con la tabla ADRES4505C. Almacena la referencia a la dirección o localización asociada al centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'IDADRES4505C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'ID que guarda en la tabla ADRES4505C', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'IDADRES4505C';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'IDADRES4505C';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autonumérico de identidad de la tabla (INT IDENTITY). Clave primaria clusterizada que identifica unívocamente cada registro de relación entre dirección y centro de atención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relaciona los registros del reporte ADRES 4505 con los centros de atención correspondientes. Permite identificar qué centros de atención (sedes o IPS) están asociados a cada registro del informe ADRES 4505.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRES4505CENATEN';
