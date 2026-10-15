CREATE TABLE [dbo].[HCIMARADIO] (
    [ID]          INT             IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODIGO]      VARCHAR (3)     NOT NULL,
    [NOMBREFOTO]  VARCHAR (200)   NOT NULL,
    [ESTADO]      INT             NOT NULL,
    [FOTO]        VARBINARY (MAX) NULL,
    [FECREGISTRO] DATETIME        NOT NULL,
    [USUCREACION] CHAR (20)       NOT NULL,
    [TIPO]        INT             NULL,
    CONSTRAINT [PK_HCIMARADIO] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HCIMARADIO]
    ON [dbo].[HCIMARADIO]([CODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo: 1.Radioterapia, 2.Informe otros procedimientos (dashboar médicos)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'USUCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FECREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Foto De Radioterapia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'FOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Foto 1 sI 2 No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre Foto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'NOMBREFOTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Imagen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'CODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCIMARADIO', @level2type = N'COLUMN', @level2name = N'ID';

