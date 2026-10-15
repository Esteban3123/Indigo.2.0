CREATE TABLE [dbo].[HCPARRECC] (
    [ID]          INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODCENATE]   CHAR (10) NULL,
    [UFUCODIGO]   CHAR (10) NULL,
    [RECOESCRITA] BIT       NOT NULL,
    [TIPOUF]      INT       NULL,
    CONSTRAINT [PK_HCPARRECC] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARRECC_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARRECC_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCPARRECC] NOCHECK CONSTRAINT [FK_HCPARRECC_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARRECC] NOCHECK CONSTRAINT [FK_HCPARRECC_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de unidad funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'TIPOUF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Establece si permtite recomendación escrita 0-> no, 1-> si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'RECOESCRITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Código de la Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id autonumerico cabecera de parametros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARRECC', @level2type = N'COLUMN', @level2name = N'ID';

