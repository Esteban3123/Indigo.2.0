CREATE TABLE [Contract].[Homologacion] (
    [CUPS] NVARCHAR (255) NULL,
    [IPS]  NVARCHAR (255) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio IPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'IPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del servicio CUPS', @level0type = N'SCHEMA', @level0name = N'Contract', @level1type = N'TABLE', @level1name = N'Homologacion', @level2type = N'COLUMN', @level2name = N'CUPS';

