CREATE TABLE [dbo].[HCEXAENSITC] (
    [ID]        INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [URISERVIC] CHAR (50) NOT NULL,
    [CODCENATE] CHAR (10) NULL,
    CONSTRAINT [PK_HCEXAENSITC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCEXAENSITC_CODCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCEXAENSITC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCEXAENSITC] NOCHECK CONSTRAINT [FK_HCEXAENSITC_CODCENATE];


GO
ALTER TABLE [dbo].[HCEXAENSITC] NOCHECK CONSTRAINT [FK_HCEXAENSITC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Direccion de los servicios Web para poder realizar a examenes en sitio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITC', @level2type = N'COLUMN', @level2name = N'URISERVIC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCEXAENSITC', @level2type = N'COLUMN', @level2name = N'ID';

