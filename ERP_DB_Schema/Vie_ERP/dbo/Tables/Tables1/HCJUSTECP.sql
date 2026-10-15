CREATE TABLE [dbo].[HCJUSTECP] (
    [CODCONCEC] INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [NOMPLANTI] VARCHAR (100) NOT NULL,
    [CODSERIPS] CHAR (20)     NOT NULL,
    [DESAYUDIA] VARCHAR (MAX) NOT NULL,
    [DESJUSTEC] VARCHAR (MAX) NOT NULL,
    [DESINCAYU] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_HCJUSTECP] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC)
);


GO
CREATE NONCLUSTERED INDEX [IX_HCJUSTECP]
    ON [dbo].[HCJUSTECP]([CODSERIPS] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion de la Incidencia de ayuda diagnostica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESINCAYU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'justificacion tecnica cientifica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESJUSTEC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descirpcion de las ayudas diagnosticas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'DESAYUDIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre o descripcion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'NOMPLANTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Concecutivo interno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCJUSTECP', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

