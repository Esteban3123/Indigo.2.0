CREATE TABLE [dbo].[HCINTEMED] (
    [CODPRODUA] CHAR (20)     NOT NULL,
    [CODPRODUB] CHAR (20)     NOT NULL,
    [AUTO]      INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NIVRIESGO] CHAR (1)      NOT NULL,
    [OBSERVACI] VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCINTEMED] PRIMARY KEY CLUSTERED ([AUTO] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_HCINTEMED__CODPRODUA]
    ON [dbo].[HCINTEMED]([CODPRODUA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de riesgo 1-Graves 2-Moderadas 3-Leves', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'NIVRIESGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'AUTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto A', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la iteracciones medicamentos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCINTEMED', @level2type = N'COLUMN', @level2name = N'CODPRODUA';

