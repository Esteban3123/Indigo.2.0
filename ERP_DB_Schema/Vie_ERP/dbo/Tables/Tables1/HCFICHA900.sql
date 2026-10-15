CREATE TABLE [dbo].[HCFICHA900] (
    [ID]                  INT           IDENTITY (1, 1) NOT NULL,
    [IDFICHANOTIFICACION] INT           NOT NULL,
    [CODDIAGNO]           CHAR (4)      NULL,
    [SOSPECHAEVENTO]      INT           NOT NULL,
    [OTROSEVENTOS]        VARCHAR (20)  NULL,
    [VERSION]             VARCHAR (20)  NULL,
    [JSON]                VARCHAR (MAX) NULL,
    CONSTRAINT [PK_HCFICHA900] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [CK_HCFICHA900_JSON] CHECK (isjson([JSON])=(1)),
    CONSTRAINT [FK_HCFICHA900_HCFICHANOTIFICACION] FOREIGN KEY ([IDFICHANOTIFICACION]) REFERENCES [dbo].[HCFICHANOTIFICACION] ([ID])
);


GO
ALTER TABLE [dbo].[HCFICHA900] NOCHECK CONSTRAINT [CK_HCFICHA900_JSON];


GO
ALTER TABLE [dbo].[HCFICHA900] NOCHECK CONSTRAINT [FK_HCFICHA900_HCFICHANOTIFICACION];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nuevas columnas en formato JSON', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA900', @level2type = N'COLUMN', @level2name = N'JSON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Versión de la ficha de notificación ---> Si es valor nulo = primer versión ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA900', @level2type = N'COLUMN', @level2name = N'VERSION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Contiene la respuesta a la pregunta de la sospecha del evento de la ficha 900 cuando la sospecha del evento es Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA900', @level2type = N'COLUMN', @level2name = N'OTROSEVENTOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 = Síndrome mano, pie, boca,  2 =Conjuntivitis, 3 = Accidente escorpionico/ picadura por animales ponzoñosos, 4 = Brucelosis, 5 = Hepatitis aguda de origen desconocido, 6 = Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA900', @level2type = N'COLUMN', @level2name = N'SOSPECHAEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA900', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el Id de la ficha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA900', @level2type = N'COLUMN', @level2name = N'IDFICHANOTIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCFICHA900', @level2type = N'COLUMN', @level2name = N'ID';

