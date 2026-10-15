CREATE TABLE [dbo].[AGACTMDDD] (
    [AUTONUMER]                INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODACTMED]                CHAR (3)        NOT NULL,
    [CODSERIPS]                CHAR (20)       NOT NULL,
    [TIPOPARAM]                NCHAR (1)       NOT NULL,
    [CONCECREC]                INT             NULL,
    [CODPRODUC]                CHAR (20)       NULL,
    [CANTRECUR]                INT             NULL,
    [REACALAUT]                BIT             NULL,
    [MEDIOCONT]                BIT             NULL,
    [APLRANEDA]                BIT             NULL,
    [MLPRODUCT]                NUMERIC (10, 2) NULL,
    [KGPRODUCT]                NUMERIC (10, 2) NULL,
    [EDADESDE]                 TINYINT         NULL,
    [EDAHASTA]                 TINYINT         NULL,
    [PRODAUTON]                INT             NULL,
    [IDDESCRIPCIONRELACIONADA] INT             NULL,
    CONSTRAINT [PK_AGACTMDDD] PRIMARY KEY CLUSTERED ([AUTONUMER] ASC),
    CONSTRAINT [FK_AGACTMDDD_AGERECURS] FOREIGN KEY ([CONCECREC]) REFERENCES [dbo].[AGERECURS] ([CODCONCEC]),
    CONSTRAINT [FK_AGACTMDDD_SOLPRODUC] FOREIGN KEY ([PRODAUTON]) REFERENCES [dbo].[SOLPRODUC] ([PRODAUTON])
);


GO
ALTER TABLE [dbo].[AGACTMDDD] NOCHECK CONSTRAINT [FK_AGACTMDDD_AGERECURS];


GO
ALTER TABLE [dbo].[AGACTMDDD] NOCHECK CONSTRAINT [FK_AGACTMDDD_SOLPRODUC];


GO
CREATE NONCLUSTERED INDEX [IX_AGACTMDDD]
    ON [dbo].[AGACTMDDD]([CODSERIPS] ASC, [CODACTMED] ASC, [IDDESCRIPCIONRELACIONADA] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de descripcion de relacion. relacion con  VIE ERP  (contract.CUPSEntityContractDescriptions) ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'IDDESCRIPCIONRELACIONADA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene el autonumerico del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'PRODAUTON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Hasta (Años)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDAHASTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Edad Desde (Años)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'EDADESDE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'KiloGramos(kg) del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'KGPRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'MiliLitros(ml) del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MLPRODUCT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica rango de edad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'APLRANEDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Medios de contraste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'MEDIOCONT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Realizar Calculo Automatico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'REACALAUT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'cantidad utilizada de los recursos-productos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CANTRECUR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Interno del Producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODPRODUC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero consecutivo que identifica el recurso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CONCECREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'define tipo de parametro agregado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'TIPOPARAM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo del servicio ips asociado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de la actividad medica asociada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'CODACTMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'autonumerico ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGACTMDDD', @level2type = N'COLUMN', @level2name = N'AUTONUMER';

