CREATE TABLE [dbo].[HCPARANTECEDENTESD] (
    [ID]                   INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCPARANTECEDENTESC] INT           NOT NULL,
    [CODIGOCAMPO]          INT           NOT NULL,
    [NOMBREANTECEDENTE]    VARCHAR (MAX) NULL,
    [VISIBLE]              BIT           NOT NULL,
    [OBLIGATORIO]          BIT           NOT NULL,
    [TIPO]                 INT           NULL,
    CONSTRAINT [PK_HCPARANTECEDENTESD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARANTECEDENTESD_HCPARANTECEDENTESD] FOREIGN KEY ([IDHCPARANTECEDENTESC]) REFERENCES [dbo].[HCPARANTECEDENTESC] ([ID])
);


GO
ALTER TABLE [dbo].[HCPARANTECEDENTESD] NOCHECK CONSTRAINT [FK_HCPARANTECEDENTESD_HCPARANTECEDENTESD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Ginecoloicos 2-OBSTETRICOS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'TIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo Obligatorio si ó No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo VIsible si ó no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del antecendente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'NOMBREANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del campo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla cabecera.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'IDHCPARANTECEDENTESC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARANTECEDENTESD', @level2type = N'COLUMN', @level2name = N'ID';

