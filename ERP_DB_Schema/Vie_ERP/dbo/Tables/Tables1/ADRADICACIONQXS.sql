CREATE TABLE [dbo].[ADRADICACIONQXS] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRADICACIONQX]           INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_ADRADICACIONQXS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ADRADICACIONQXS_ADRADICACIONQX] FOREIGN KEY ([IDRADICACIONQX]) REFERENCES [dbo].[ADRADICACIONQX] ([ID]),
    CONSTRAINT [FK_ADRADICACIONQXS_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[ADRADICACIONQXS] NOCHECK CONSTRAINT [FK_ADRADICACIONQXS_ADRADICACIONQX];


GO
ALTER TABLE [dbo].[ADRADICACIONQXS] NOCHECK CONSTRAINT [FK_ADRADICACIONQXS_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la Radicacion procedimiento quirúrgico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'IDRADICACIONQX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADRADICACIONQXS', @level2type = N'COLUMN', @level2name = N'ID';

