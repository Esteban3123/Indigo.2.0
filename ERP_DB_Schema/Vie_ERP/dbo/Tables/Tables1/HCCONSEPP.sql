CREATE TABLE [dbo].[HCCONSEPP] (
    [CODSERIPS]  CHAR (20)      NOT NULL,
    [DESTIPPLAN] CHAR (30)      NOT NULL,
    [TIPPROIPS]  CHAR (1)       NOT NULL,
    [TIPANEPRO]  CHAR (1)       NOT NULL,
    [ASPPROIPS]  VARCHAR (4000) NULL,
    [RIEPROIPS]  VARCHAR (4000) NULL,
    [DETPROIPS]  VARCHAR (4000) NULL,
    CONSTRAINT [PK_HCCONSEPP] PRIMARY KEY CLUSTERED ([CODSERIPS] ASC, [DESTIPPLAN] ASC),
    CONSTRAINT [FK_HCCONSEPP_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCCONSEPP] NOCHECK CONSTRAINT [FK_HCCONSEPP_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detalle Adicional del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DETPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Riesgos del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'RIEPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aspectos del Procedimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'ASPPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Anestesia utilizada en el procedimiento  1: General  2: Local  3: Regional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPANEPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Procedimiento IPS  1: Procedimiento Especial  2: Procedimiento Quirurgico  3: Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'TIPPROIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Plantilla Justificacion Medicamento No POS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'DESTIPPLAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCONSEPP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';

