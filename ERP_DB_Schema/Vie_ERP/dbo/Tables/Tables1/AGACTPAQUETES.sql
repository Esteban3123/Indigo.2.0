CREATE TABLE [dbo].[AGACTPAQUETES] (
    [ID]                       INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED]                CHAR (3)  NOT NULL,
    [CODSERIPS]                CHAR (20) NOT NULL,
    [IDAGPAQUETE]              INT       NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT       NULL,
    CONSTRAINT [PK_AGACTPAQUETES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGACTPAQUETES_AGACTIMED] FOREIGN KEY ([CODACTMED]) REFERENCES [dbo].[AGACTIMED] ([CODACTMED]),
    CONSTRAINT [FK_AGACTPAQUETES_AGPAQUETES] FOREIGN KEY ([IDAGPAQUETE]) REFERENCES [dbo].[AGPAQUETES] ([ID]),
    CONSTRAINT [FK_AGACTPAQUETES_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [IX_AGACTPAQUETES] UNIQUE NONCLUSTERED ([CODACTMED] ASC, [CODSERIPS] ASC, [IDAGPAQUETE] ASC)
);


GO
ALTER TABLE [dbo].[AGACTPAQUETES] NOCHECK CONSTRAINT [FK_AGACTPAQUETES_AGACTIMED];


GO
ALTER TABLE [dbo].[AGACTPAQUETES] NOCHECK CONSTRAINT [FK_AGACTPAQUETES_AGPAQUETES];


GO
ALTER TABLE [dbo].[AGACTPAQUETES] NOCHECK CONSTRAINT [FK_AGACTPAQUETES_INCUPSIPS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de tabla AGPAQUETE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'IDAGPAQUETE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo consecutivo que identifica la actividad medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTPAQUETES', @level2type = N'COLUMN', @level2name = N'ID';

