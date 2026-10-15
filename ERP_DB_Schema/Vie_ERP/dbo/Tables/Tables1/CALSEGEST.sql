CREATE TABLE [dbo].[CALSEGEST] (
    [ID]              INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]    INT       NOT NULL,
    [ESTADO]          INT       NOT NULL,
    [FECHAREGISTRO]   DATETIME  NOT NULL,
    [USUARIOREGISTRO] CHAR (20) NOT NULL,
    CONSTRAINT [PK_CALSEGEST] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALSEGEST_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALSEGEST_SEGusuaru] FOREIGN KEY ([USUARIOREGISTRO]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[CALSEGEST] NOCHECK CONSTRAINT [FK_CALSEGEST_CALREPORTE];


GO
ALTER TABLE [dbo].[CALSEGEST] NOCHECK CONSTRAINT [FK_CALSEGEST_SEGusuaru];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realizó el Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado al que pasa el registro:     1=Reportado    2=Descartado    3=Modificado    4=Visado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGEST', @level2type = N'COLUMN', @level2name = N'ID';

