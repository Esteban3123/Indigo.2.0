CREATE TABLE [dbo].[ADCONFSER] (
    [CODCONCEC]                INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [SUSCEPTIB]                BIT       NOT NULL,
    [INAPLICA]                 CHAR (1)  NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_ADCONFSER] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADCONFSER_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[ADCONFSER] NOCHECK CONSTRAINT [FK_ADCONFSER_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la Descripción relacionada  ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONFSER', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';

