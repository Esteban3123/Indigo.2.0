CREATE TABLE [dbo].[CALSEGDESCA] (
    [ID]              INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]    INT           NOT NULL,
    [IDCALMOTIANULAC] INT           NOT NULL,
    [OBSERVACION]     VARCHAR (200) NOT NULL,
    [FECHAREGISTRO]   DATETIME      NOT NULL,
    [USUARIOREGISTRO] CHAR (20)     NOT NULL,
    CONSTRAINT [PK_CALSEGDESCA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALSEGDESCA_CALMOTIANULAC] FOREIGN KEY ([IDCALMOTIANULAC]) REFERENCES [dbo].[CALMOTIANULAC] ([ID]),
    CONSTRAINT [FK_CALSEGDESCA_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALSEGDESCA_SEGusuaru] FOREIGN KEY ([USUARIOREGISTRO]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[CALSEGDESCA] NOCHECK CONSTRAINT [FK_CALSEGDESCA_CALMOTIANULAC];


GO
ALTER TABLE [dbo].[CALSEGDESCA] NOCHECK CONSTRAINT [FK_CALSEGDESCA_CALREPORTE];


GO
ALTER TABLE [dbo].[CALSEGDESCA] NOCHECK CONSTRAINT [FK_CALSEGDESCA_SEGusuaru];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario que realizó el registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'USUARIOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha y Hora del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observación ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID del motivo de descarte o anulacion, tabla CALMOTIANULAC', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALMOTIANULAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID Autonumérico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALSEGDESCA', @level2type = N'COLUMN', @level2name = N'ID';

