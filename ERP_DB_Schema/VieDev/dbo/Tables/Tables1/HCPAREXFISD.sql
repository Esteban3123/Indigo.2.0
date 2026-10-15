CREATE TABLE [dbo].[HCPAREXFISD] (
    [ID]          INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCABECERA]  INT          NOT NULL,
    [CODIGOCAMPO] VARCHAR (10) NOT NULL,
    [VISIBLE]     BIT          NOT NULL,
    [OBLIGATORIO] BIT          NOT NULL,
    [PRECONSULTA] BIT          CONSTRAINT [DF_HCPAREXFISD_PRECONSULTA] DEFAULT ((0)) NULL,
    CONSTRAINT [PK_HCPAREXFISD] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPAREXFISD_HCPAREXFISC] FOREIGN KEY ([IDCABECERA]) REFERENCES [dbo].[HCPAREXFISC] ([ID])
);


GO
ALTER TABLE [dbo].[HCPAREXFISD] NOCHECK CONSTRAINT [FK_HCPAREXFISD_HCPAREXFISC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo que me va indicar si funciona para una preconsulta en la consulta externa.    Esta columna en la parametrizacion del examen fisico solo se visualizará cuando se cumple dos condiciones relacionadas a continuación:    1. La Unidad funcional seleccionada sea de tipo “Consulta externa”.     2. El Tipo de examen físico seleccionado sea “Control examen físico básico”.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'PRECONSULTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si es obligatorio   1 = Si   0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si es visible  1 = Si      0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'VISIBLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el codigo campo examen fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'CODIGOCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Id de la cabecera; campo que me tiene la relacion con la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'IDCABECERA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPAREXFISD', @level2type = N'COLUMN', @level2name = N'ID';

