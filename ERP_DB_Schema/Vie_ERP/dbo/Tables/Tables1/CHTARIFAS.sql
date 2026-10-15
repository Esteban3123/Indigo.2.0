CREATE TABLE [dbo].[CHTARIFAS] (
    [CODPLAHOS] CHAR (3)     NOT NULL,
    [CODTIPEST] CHAR (3)     NOT NULL,
    [CODICAMAS] INT          NOT NULL,
    [TIPDISPON] INT          NOT NULL,
    [CODSERIPS] CHAR (20)    NOT NULL,
    [INDAUDFOR] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_CHTARIFAS] PRIMARY KEY CLUSTERED ([CODPLAHOS] ASC, [CODTIPEST] ASC, [CODICAMAS] ASC),
    CONSTRAINT [FK_CHTARIFAS_CHCAMASHO] FOREIGN KEY ([CODICAMAS]) REFERENCES [dbo].[CHCAMASHO] ([CODICAMAS]),
    CONSTRAINT [FK_CHTARIFAS_COPLANTIL] FOREIGN KEY ([CODPLAHOS]) REFERENCES [dbo].[COPLANTIL] ([CODPLANTI]) ON UPDATE CASCADE
);


GO
ALTER TABLE [dbo].[CHTARIFAS] NOCHECK CONSTRAINT [FK_CHTARIFAS_CHCAMASHO];


GO
ALTER TABLE [dbo].[CHTARIFAS] NOCHECK CONSTRAINT [FK_CHTARIFAS_COPLANTIL];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Servicio IPS a Facturar', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Disponibilidad de la Cama para el plan de beneficios:  1: Disponible  2: Requiere de Autorizacion para asignarla  3: No Disponible', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'TIPDISPON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Cama', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODICAMAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Plantilla de Hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHTARIFAS', @level2type = N'COLUMN', @level2name = N'CODPLAHOS';

