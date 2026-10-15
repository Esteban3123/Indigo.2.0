CREATE TABLE [dbo].[CONPPRCTP] (
    [IDCONPRPRAC] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDGRUPUSC]   TINYINT       NOT NULL,
    [CODUSUARI]   CHAR (20)     NOT NULL,
    [CONFCHSOL]   DATETIME      NOT NULL,
    [CONTIPPUN]   INT           NOT NULL,
    [CONVALRES]   INT           NULL,
    [CODUSUREG]   CHAR (20)     NULL,
    [CONCOMENT]   VARCHAR (250) NULL,
    [CONVALIDP]   BIT           NULL,
    [CONFCHPRE]   DATETIME      NULL,
    CONSTRAINT [PK_CONPPRCTP] PRIMARY KEY CLUSTERED ([IDCONPRPRAC] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de presentacion prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Valida la pregunta presentada', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALIDP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Comentario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONCOMENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo usuario que realiza el obsequio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Puntuación por la respuesta u obsequio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONVALRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de puntuacion 0- Ejercicio Practico, 1-Obsequio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONTIPPUN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de solicitud de la prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CONFCHSOL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del usuario solicita/Presenta prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'CODUSUARI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo código grupo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDGRUPUSC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico respuesta de prueba de concocimientos practicos u obsequios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CONPPRCTP', @level2type = N'COLUMN', @level2name = N'IDCONPRPRAC';

