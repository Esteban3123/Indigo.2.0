CREATE TABLE [dbo].[HCESCALASRESULTITEMS] (
    [ID]                    INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCESCALAS]           INT           NOT NULL,
    [TAGPREGUNTA]           VARCHAR (4)   NOT NULL,
    [TEXTOPREGUNTA]         VARCHAR (MAX) NOT NULL,
    [TEXTOITEMSELECCIONADO] VARCHAR (MAX) NOT NULL,
    [VALORITEMSELECCIONADO] VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_HCESCALASRESULTITEMS] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCESCALASRESULTITEMS_HCESCALAS] FOREIGN KEY ([IDHCESCALAS]) REFERENCES [dbo].[HCESCALAS] ([ID])
);


GO
ALTER TABLE [dbo].[HCESCALASRESULTITEMS] NOCHECK CONSTRAINT [FK_HCESCALASRESULTITEMS_HCESCALAS];


GO
CREATE NONCLUSTERED INDEX [IX_HCESCALASRESULTITEMS_IDHCESCALAS]
    ON [dbo].[HCESCALASRESULTITEMS]([IDHCESCALAS] ASC)
    INCLUDE([TEXTOITEMSELECCIONADO], [TEXTOPREGUNTA], [VALORITEMSELECCIONADO]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda  el valor de los items seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'VALORITEMSELECCIONADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda lo selecionado item', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOITEMSELECCIONADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el  texto de la pregunta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TEXTOPREGUNTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el tag de la pregunta ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'TAGPREGUNTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Relación con la tabla HCESCALAS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'IDHCESCALAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESCALASRESULTITEMS', @level2type = N'COLUMN', @level2name = N'ID';

