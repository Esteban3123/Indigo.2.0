CREATE TABLE [dbo].[INCLAPROV] (
    [CODCLAPROV] CHAR (2)  NOT NULL,
    [CODPROVEE]  CHAR (15) NOT NULL,
    CONSTRAINT [PK_INCLAP] PRIMARY KEY CLUSTERED ([CODCLAPROV] ASC, [CODPROVEE] ASC),
    CONSTRAINT [FK_INCLAPROV_INCLASESP] FOREIGN KEY ([CODCLAPROV]) REFERENCES [dbo].[INCLASESP] ([CODCLAPROV]),
    CONSTRAINT [FK_INCLAPROV_INPROVEED] FOREIGN KEY ([CODPROVEE]) REFERENCES [dbo].[INPROVEED] ([CODPROVEE])
);




GO



GO



GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del Proveedor (FK a INPROVEED). Identificador único de 15 caracteres que referencia el proveedor de servicios, suministros o productos en el sistema. Sinónimos: NIT del proveedor, código de acreedor, identificación del distribuidor.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODPROVEE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODPROVEE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la Clase de Proveedor (FK a INCLASESP). Clasificación de 2 caracteres que categoriza el tipo o naturaleza del proveedor (farmacéutico, laboratorio, distribuidor, etc.). Define la segmentación contractual y normativa RIPS.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODCLAPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Clase de Proveedor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODCLAPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV', @level2type = N'COLUMN', @level2name = N'CODCLAPROV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Clasificación de proveedores: relaciona cada proveedor con su tipo o clase de proveedor. Se usa para agrupar y filtrar proveedores según su categoría en el sistema de compras o contratos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCLAPROV';
