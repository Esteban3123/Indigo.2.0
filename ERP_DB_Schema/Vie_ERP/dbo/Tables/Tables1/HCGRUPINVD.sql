CREATE TABLE [dbo].[HCGRUPINVD] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCGRUPINVC]             INT       NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_HCGRUPINVD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCGRUPINVD_HCGRUPINVC] FOREIGN KEY ([IDHCGRUPINVC]) REFERENCES [dbo].[HCGRUPINVC] ([ID]),
    CONSTRAINT [FK_HCGRUPINVD_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [IX_HCGRUPINVD] UNIQUE NONCLUSTERED ([IDHCGRUPINVC] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC)
);


GO
ALTER TABLE [dbo].[HCGRUPINVD] NOCHECK CONSTRAINT [FK_HCGRUPINVD_HCGRUPINVC];


GO
ALTER TABLE [dbo].[HCGRUPINVD] NOCHECK CONSTRAINT [FK_HCGRUPINVD_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código del CUPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla GRUGESPROINC (grupos gestion procedimientos invasivos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'IDHCGRUPINVC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCGRUPINVD', @level2type = N'COLUMN', @level2name = N'ID';

