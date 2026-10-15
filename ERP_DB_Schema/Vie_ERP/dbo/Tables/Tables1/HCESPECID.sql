CREATE TABLE [dbo].[HCESPECID] (
    [CODCONCEC]   INT                                                                                    NOT NULL,
    [CODCAMPO]    CHAR (3)                                                                               NOT NULL,
    [DESEXAME]    VARCHAR (250) MASKED WITH (FUNCTION = 'partial(0, "ExaminationPhysical_Ofuscado", 0)') NOT NULL,
    [OBLIGATORIO] BIT                                                                                    NOT NULL,
    CONSTRAINT [PK_HCESPECID] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC, [CODCAMPO] ASC),
    CONSTRAINT [FK_HCESPECID_HCESPECIC] FOREIGN KEY ([CODCONCEC]) REFERENCES [dbo].[HCESPECIC] ([CODCONCEC])
);


GO
ALTER TABLE [dbo].[HCESPECID] NOCHECK CONSTRAINT [FK_HCESPECID_HCESPECIC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica Si el campo es obligatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Examen fisico urgencias', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'DESEXAME';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del campo del Examen Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCAMPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la cabecera', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCESPECID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

