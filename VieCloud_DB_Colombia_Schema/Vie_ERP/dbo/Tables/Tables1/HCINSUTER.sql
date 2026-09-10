CREATE TABLE [dbo].[HCINSUTER] (
    [CODCONSEC] NUMERIC (18)                                                                     NOT NULL,
    [CODPRODUC] CHAR (20)                                                                        NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CANPROUSA] INT                                                                              NOT NULL,
    [FECREGSIS] DATETIME                                                                         CONSTRAINT [DF_HCINSUTER_FECREGSIS] DEFAULT ([Common].[getdate]()) NOT NULL,
    CONSTRAINT [PK_HCINSUTER] PRIMARY KEY CLUSTERED ([CODCONSEC] ASC, [CODPRODUC] ASC),
    CONSTRAINT [FK_HCINSUTER_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCINSUTER_HCPROCTER] FOREIGN KEY ([CODCONSEC]) REFERENCES [dbo].[HCPROCTER] ([CODCONSEC]),
    CONSTRAINT [FK_HCINSUTER_IHLISTPRO] FOREIGN KEY ([CODPRODUC]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC]),
    CONSTRAINT [FK_HCINSUTER_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCINSUTER].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');



GO
CREATE NONCLUSTERED INDEX [IX_HCINSUTER_IPCODPACI_NUMINGRES_CODCENATE_UFUCODIGO]
    ON [dbo].[HCINSUTER]([IPCODPACI] ASC, [NUMINGRES] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de registro del consumo de producto en el sistema (DATETIME, valor por defecto actual)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de registro en el sistema', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'FECREGSIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'FECREGSIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad de unidades del producto utilizado/consumido en la atención (INT, cantidad numérica)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CANPROUSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de producto usado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CANPROUSA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CANPROUSA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador de la Unidad Funcional donde se consumió el producto (CHAR 10, departamento/servicio)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Centro de Atención, sede u hospital donde se registró el consumo (CHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso o admisión del paciente a la institución (CHAR 10, referencia a episodio de atención)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del Paciente, equivalente a cédula/documento de identidad (VARCHAR 25, PII enmascarado, FK a INPACIENT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código interno del Producto consumido en la atención (CHAR 20, FK a IHLISTPRO, medicamento/insumo/dispositivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODPRODUC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Consecutivo secuencial del registro de consumo de producto en historia clínica (NUMERIC 18, clave primaria, FK a HCPROCTER)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Consecutivo del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODCONSEC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER', @level2type = N'COLUMN', @level2name = N'CODCONSEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro de insumos o materiales utilizados durante la atención clínica de un paciente en un ingreso específico. Guarda qué productos o insumos fueron usados, en qué cantidad, por qué paciente y en qué unidad funcional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINSUTER';
