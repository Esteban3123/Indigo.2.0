CREATE TABLE [dbo].[HCHOMIPRESDCI] (
    [ID]          INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODMIPRES]   VARCHAR (6)    NOT NULL,
    [DESCRIPCION] VARCHAR (1500) NOT NULL,
    [CODINDIGO]   CHAR (20)      NOT NULL,
    [ESTADO]      BIT            NOT NULL,
    CONSTRAINT [PK__HCHOMIPRESDCI] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCHOMIPRESDCI_IHLISTPRO] FOREIGN KEY ([CODINDIGO]) REFERENCES [dbo].[IHLISTPRO] ([CODPRODUC])
);




GO



GO
CREATE NONCLUSTERED INDEX [IX_HCHOMIPRESDCI_CODINDIGO]
    ON [dbo].[HCHOMIPRESDCI]([CODINDIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado de registro en MIPRES: activo (1) o inactivo (0). Bit que indica si el mapeo DCI está vigente en el sistema de medicamentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado de registro en mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de medicamento en Indigo Vie Cloud (FK a IHLISTPRO.CODPRODUC). Identificador único del producto farmacéutico en catálogo interno.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODINDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código de medicamento en Indigo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODINDIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODINDIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción DCI (Denominación Común Internacional). Nombre genérico del principio activo del medicamento según estándares farmacéuticos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripción DCI', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código DCI en MIPRES (Ministerio de Protección Social). Identificador de medicamento registrado en sistema nacional de prescripción electrónica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código DCI mipres', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODMIPRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'CODMIPRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (IDENTITY) de la tabla HCHOMIPRESDCI. Clave primaria secuencial para mapeo de medicamentos DCI.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Identificador de la tabla  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ID';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Catálogo de equivalencias entre los códigos MIPRES (sistema de prescripción de tecnologías no financiadas por la UPC del Ministerio de Salud) y los códigos internos de Indigo. Permite identificar qué producto o servicio MIPRES corresponde a cada ítem registrado en el sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCHOMIPRESDCI';
