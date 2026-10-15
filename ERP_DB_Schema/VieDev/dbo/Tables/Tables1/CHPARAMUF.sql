CREATE TABLE [dbo].[CHPARAMUF] (
    [CODCENATE] CHAR (10) NOT NULL,
    [UFUCODIGO] CHAR (10) NOT NULL,
    [EXDOSITRA] BIT       NOT NULL,
    [EXDOSIEGR] BIT       NOT NULL,
    [DESMEDTRA] CHAR (1)  NULL,
    [DESMEDEGR] CHAR (1)  NULL,
    [PERCAMCAN] BIT       NULL,
    [EXIINPARA] BIT       NOT NULL,
    CONSTRAINT [PK_CHPARAMUF] PRIMARY KEY CLUSTERED ([CODCENATE] ASC, [UFUCODIGO] ASC),
    CONSTRAINT [FK_CHPARAMUF_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_CHPARAMUF_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[CHPARAMUF] NOCHECK CONSTRAINT [FK_CHPARAMUF_ADCENATEN];


GO
ALTER TABLE [dbo].[CHPARAMUF] NOCHECK CONSTRAINT [FK_CHPARAMUF_INUNIFUNC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir destino de medicamentos sobrantes en Egresos  1-Exigir destino de medicamentos(Devolutivo a farmacia)  2-Exigir destino de medicamentos(Especificado por el usuario Devolutivo - Ninguno)  3-Exigir destino de medicamentos(Ninguno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDEGR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Exigir destino de medicamentos sobrantes en Traslados.    1 -Exigir destino de medicamentos(Devolutivo a farmacia)  2 - Exigir destino de medicamentos(Traslado a unidad destino).  3- Exigir destino de medicamentos(Especificado por el usuario Devolutivo - Traslado).  4- Exigir destino de medicamentos(Ninguno)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CHPARAMUF', @level2type = N'COLUMN', @level2name = N'DESMEDTRA';

