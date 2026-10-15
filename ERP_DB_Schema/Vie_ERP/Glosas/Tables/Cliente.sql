CREATE TABLE [Glosas].[Cliente] (
    [VIE18Id] INT          NULL,
    [NIT]     VARCHAR (20) NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nit', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'NIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Desconocido', @level0type = N'SCHEMA', @level0name = N'Glosas', @level1type = N'TABLE', @level1name = N'Cliente', @level2type = N'COLUMN', @level2name = N'VIE18Id';

