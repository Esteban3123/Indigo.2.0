CREATE TABLE [dbo].[HCLOGSUMD] (
    [ID]         INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [LOGSUMID]   INT           NOT NULL,
    [PROCODCUM]  VARCHAR (50)  NULL,
    [PROCODIGO]  CHAR (20)     NOT NULL,
    [PRONOMBRE]  VARCHAR (500) NOT NULL,
    [CODLOTE]    VARCHAR (50)  NOT NULL,
    [INNLOTSER]  INT           NULL,
    [CANTIDAD]   INT           NOT NULL,
    [INNMSUMPA]  INT           NULL,
    [NUMSUMINIS] VARCHAR (50)  NULL,
    [INNDOCUME]  INT           NULL,
    CONSTRAINT [PK_HCLOGSUMD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el IdInnsumpa', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'INNMSUMPA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda la Cantidad producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Codigo del lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'CODLOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Nombre del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PRONOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del producto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'PROCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda logos id', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLOGSUMD', @level2type = N'COLUMN', @level2name = N'LOGSUMID';

