CREATE TABLE [dbo].[AGENSALAD] (
    [CODCONCEC]                INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [DURACSERVI]               CHAR (6)  NULL,
    [DURAVARIAB]               BIT       NULL,
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_AGENSALAD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGENSALAD_AGENSALAC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[AGENSALAC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[AGENSALAD] NOCHECK CONSTRAINT [FK_AGENSALAD_AGENSALAC];


GO
CREATE NONCLUSTERED INDEX [IX_AGENSALAD]
    ON [dbo].[AGENSALAD]([CODCONCEC] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAD', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración variable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAD', @level2type = N'COLUMN', @level2name = N'DURAVARIAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Duración del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAD', @level2type = N'COLUMN', @level2name = N'DURACSERVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'código del servicio IPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGENSALAD', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

