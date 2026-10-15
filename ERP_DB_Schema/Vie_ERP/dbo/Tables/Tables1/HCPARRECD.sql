CREATE TABLE [dbo].[HCPARRECD] (
    [CODIGO]      VARCHAR (5)   NOT NULL,
    [DESCRIPCION] VARCHAR (500) NOT NULL,
    [ESTADO]      BIT           NOT NULL,
    [CABECERAID]  INT           NOT NULL,
    CONSTRAINT [PK_HCPARREC] PRIMARY KEY CLUSTERED ([CODIGO] ASC),
    CONSTRAINT [FK_HCPARRECD_HCPARRECC] FOREIGN KEY ([CABECERAID]) REFERENCES [dbo].[HCPARRECC] ([ID])
);


GO
ALTER TABLE [dbo].[HCPARRECD] NOCHECK CONSTRAINT [FK_HCPARRECD_HCPARRECC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECD', @level2type = N'COLUMN', @level2name = N'CABECERAID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado 1-> Activo 0-> Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECD', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la recomendacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECD', @level2type = N'COLUMN', @level2name = N'CODIGO';

