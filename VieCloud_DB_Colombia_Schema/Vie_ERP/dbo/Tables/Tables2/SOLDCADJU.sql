CREATE TABLE [dbo].[SOLDCADJU] (
    [Autonumerico] TINYINT         IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [FechCre]      DATE            NOT NULL,
    [CodUsuCrea]   TINYINT         NULL,
    [DocAdju]      VARBINARY (MAX) NOT NULL,
    [PROVAUTO]     NUMERIC (18)    NULL,
    [COTIAUTON]    INT             NULL,
    [PRODAUTON]    INT             NULL,
    CONSTRAINT [PK_SOLDOCADJU] PRIMARY KEY CLUSTERED ([Autonumerico] ASC),
    CONSTRAINT [FK_SOLDCADJU_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON]),
    CONSTRAINT [FK_SOLDCADJU_SOLPROVEE] FOREIGN KEY ([PROVAUTO]) REFERENCES [dbo].[SOLPROVEE] ([PROVAUTO])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al producto en solicitud (FK → SOLPRODUC.PRODAUTON, INT) que se ajusta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PRODAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la cotización vinculada al ajuste (INT, referencia a proceso de cotización)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'COTIAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la cotizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'COTIAUTON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'COTIAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Referencia al proveedor (FK → SOLPROVEE.PROVAUTO, NUMERIC 18) asociado al ajuste de solicitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PROVAUTO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'PROVAUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documento adjunto al ajuste: archivo, imagen o PDF en formato binario (VARBINARY MAX, almacenamiento de archivo PII/confidencial)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'DocAdju';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el documento adjunto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'DocAdju';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'DocAdju';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del usuario que creó el registro de ajuste (TINYINT, usuario del sistema)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'CodUsuCrea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el codigo del usuario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'CodUsuCrea';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'CodUsuCrea';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de creación del ajuste de solicitud (DATE, formato YYYY-MM-DD)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'FechCre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene la fecha de creacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'FechCre';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'FechCre';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único (TINYINT, PK) de cada registro de ajuste de solicitud de compra en SOLDCADJU', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contiene el autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'Autonumerico';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU', @level2type = N'COLUMN', @level2name = N'Autonumerico';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Documentos adjuntos a solicitudes o cotizaciones de proveedores. Guarda los archivos (PDF, imágenes u otros) vinculados a una solicitud de compra, cotización o producto de proveedor, junto con el usuario que los cargó y la fecha de creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'SOLDCADJU';
