CREATE TABLE [dbo].[CONPRUCOP] (
    [IDCONPRCP] INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDGRUPUSC] TINYINT   NOT NULL,
    [CODUSUARI] CHAR (20) NOT NULL,
    [IDCONCPRE] TINYINT   NOT NULL,
    [CONVALRES] INT       NOT NULL,
    [CONFCHPR]  DATETIME  NOT NULL,
    CONSTRAINT [PK_CONPRUCOP] PRIMARY KEY CLUSTERED ([IDCONPRCP] ASC),
    CONSTRAINT [FK_CONPRUCOP_CONCPREGC] FOREIGN KEY ([IDCONCPRE]) REFERENCES [dbo].[CONCPREGC] ([IDCONCPRE]),
    CONSTRAINT [FK_CONPRUCOP_CONGRPUSC] FOREIGN KEY ([IDGRUPUSC]) REFERENCES [dbo].[CONGRPUSC] ([IDGRUPUSC])
);


GO
ALTER TABLE [dbo].[CONPRUCOP] NOCHECK CONSTRAINT [FK_CONPRUCOP_CONCPREGC];


GO
ALTER TABLE [dbo].[CONPRUCOP] NOCHECK CONSTRAINT [FK_CONPRUCOP_CONGRPUSC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha presentación pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONFCHPR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntuación por la respuesta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CONVALRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la pregunta presentada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONCPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario que presenta la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico Acumulacion de puntos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPRUCOP', @level2type = N'COLUMN', @level2name = N'IDCONPRCP';

