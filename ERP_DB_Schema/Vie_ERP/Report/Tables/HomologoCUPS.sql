CREATE TABLE [Report].[HomologoCUPS] (
    [Id]                  INT           NULL,
    [Cups]                VARCHAR (20)  NULL,
    [DescripcionCups]     VARCHAR (300) NULL,
    [CupsHomologo]        VARCHAR (20)  NULL,
    [DescripcionHomologo] VARCHAR (300) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción homologo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'DescripcionHomologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CUPS homologo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'CupsHomologo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción CUPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'DescripcionCups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'CUPS', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'Cups';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'HomologoCUPS', @level2type = N'COLUMN', @level2name = N'Id';

