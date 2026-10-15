CREATE TABLE [dbo].[HCPARCONO] (
    [CODVIAADM]                CHAR (3)        NOT NULL,
    [DESVIAADM]                CHAR (50)       NOT NULL,
    [RANGMINIM]                DECIMAL (18, 2) NULL,
    [RANGMAXIM]                DECIMAL (18, 2) NULL,
    [ESTADO]                   BIT             NOT NULL,
    [CODSERIPS]                CHAR (20)       NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    [GasType]                  INT             NULL,
    CONSTRAINT [PK_HCPARCONO] PRIMARY KEY CLUSTERED ([CODVIAADM] ASC),
    CONSTRAINT [FK_HCPARCONO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS])
);


GO
ALTER TABLE [dbo].[HCPARCONO] NOCHECK CONSTRAINT [FK_HCPARCONO_INCUPSIPS];


GO
CREATE NONCLUSTERED INDEX [IX_HCPARCONO]
    ON [dbo].[HCPARCONO]([CODVIAADM] ASC, [CODSERIPS] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que almacena el  Tipo de gas    1 - Oxigeno  2 - Oxido Nititico  3 - Oxihelio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'GasType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del servicio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'estado del registo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'rango maximo de litros por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMAXIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'rango minimo de litros por minuto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'RANGMINIM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la via de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'DESVIAADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la Vida de administracion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARCONO', @level2type = N'COLUMN', @level2name = N'CODVIAADM';

